Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Drawing.Printing
Public Class frmDashboard
    Private currentFilter As String = "All"
    Private reminderService As TaskReminderService
    Private refreshTimer As Timer
    Private isDarkMode As Boolean = False
    Private currentSortColumn As String = ""
    Private currentSortAsc As Boolean = True
    Private searchDebounceTimer As Timer  ' 300 ms debounce for search

    ' DLL imports for dragging a borderless form
    Public Const WM_NCLBUTTONDOWN As Integer = &HA1
    Public Const HT_CAPTION As Integer = &H2

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(hWnd As IntPtr, Msg As Integer, wParam As Integer, lParam As Integer) As Integer
    End Function

    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    Private Sub frmDashboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If GlobalVariables.CurrentUser Is Nothing Then
            MessageBox.Show("Session error. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
            Return
        End If

        ThemeManager.ApplyTheme(Me)
        ApplyPerfectLayout()

        pnlDashboardView.Visible = True
        pnlDashboardView.BringToFront()
        pnlManageTasksView.Visible = False
        pnlCalendarView.Visible = False
        SetActiveSidebarButton(btnNavDashboard)

        lblWelcome.Text = $"Welcome, {If(GlobalVariables.CurrentUser?.Username, "User")}!"

        SetupDataGridViews()
        LoadDashboardMetrics()
        LoadTasks()

        ' Re-style action bar every time the panel becomes visible
        AddHandler pnlManageTasksView.VisibleChanged,
            Sub(s As Object, ev As EventArgs)
                If pnlManageTasksView.Visible Then StyleActionBarButtons()
            End Sub

        reminderService = New TaskReminderService() With {
            .LookAhead    = TimeSpan.FromHours(24),
            .PollInterval = TimeSpan.FromMinutes(1)
        }
        AddHandler reminderService.TimesUp, AddressOf OnTimesUp
        reminderService.Start()

        refreshTimer = New Timer() With {.Interval = 60_000}
        AddHandler refreshTimer.Tick, Sub()
                                          LoadDashboardMetrics()
                                          LoadTasks()
                                      End Sub
        refreshTimer.Start()
    End Sub

    Private Sub ApplyPerfectLayout()
        ' Form polish
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.DoubleBuffered = True

        ' Backgrounds
        pnlMainContent.BackColor = ThemeManager.BackgroundColor
        pnlDashboardView.BackColor = ThemeManager.BackgroundColor
        pnlManageTasksView.BackColor = ThemeManager.BackgroundColor
        pnlCalendarView.BackColor = ThemeManager.BackgroundColor

        ' Sidebar & title bar
        pnlSidebar.BackColor = ThemeManager.SidebarColor
        pnlTitleBar.BackColor = ThemeManager.TitleBarColor

        ' App title
        lblAppTitle.Text = "📚 Smart Task"
        lblAppTitle.Font = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold)
        lblAppTitle.ForeColor = Color.White
        lblAppTitle.Padding = New Padding(0, 10, 0, 0)

        ' Welcome label
        lblWelcome.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.White

        ' Section header
        lblRecentActivity.Text = "📋 Upcoming Tasks"
        lblRecentActivity.Font = New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold)
        lblRecentActivity.ForeColor = ThemeManager.TextColor

        ' ── Manage Tasks toolbar redesign ─────────────────────────────────────
        ' Search box — wider, taller, clean
        txtSearch.PlaceholderText = "Search by title, category, tag, priority…"
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.BackColor = Color.White
        txtSearch.ForeColor = ThemeManager.TextColor
        txtSearch.Font = New Font("Segoe UI", 11.0F)

        ' Search clear button — subtle
        btnSearchClear.BackColor = Color.White
        btnSearchClear.ForeColor = ThemeManager.MutedTextColor
        btnSearchClear.FlatAppearance.MouseOverBackColor = ThemeManager.BorderColor
        btnSearchClear.FlatAppearance.BorderSize = 0
        btnSearchClear.Visible = False

        ' ── Status filter pill buttons — indigo palette, NOT orange ──────────
        StyleFilterPill(btnFilterAll,       active:=True)
        StyleFilterPill(btnFilterPending,   active:=False)
        StyleFilterPill(btnFilterCompleted, active:=False)

        ' ── Advanced filter apply button — indigo, NOT orange ─────────────────
        btnApplyAdvancedFilter.BackColor = ThemeManager.PrimaryColor
        btnApplyAdvancedFilter.ForeColor = Color.White
        btnApplyAdvancedFilter.FlatStyle = FlatStyle.Flat
        btnApplyAdvancedFilter.FlatAppearance.BorderSize = 0
        btnApplyAdvancedFilter.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryHoverColor
        btnApplyAdvancedFilter.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btnApplyAdvancedFilter.Text = "⚡ Apply"
        btnApplyAdvancedFilter.Cursor = Cursors.Hand

        ' ── Task count label ──────────────────────────────────────────────────
        lblTaskCount.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblTaskCount.ForeColor = ThemeManager.PrimaryColor

        ' ── Action buttons — bottom bar ───────────────────────────────────────
        StyleActionBarButtons()

        ' Progress bar styling
        pbCompletion.Style = ProgressBarStyle.Continuous
        pbCompletion.Minimum = 0
        pbCompletion.Maximum = 100
        pbCompletion.Value = 0
        pbCompletion.ForeColor = ThemeManager.SuccessColor
        pbCompletion.BackColor = ThemeManager.BorderColor

        ' Card styling with accent colours
        ThemeManager.ApplyCardStyle(pnlCardTotal, ThemeManager.PrimaryColor)
        ThemeManager.ApplyCardStyle(pnlCardPending, ThemeManager.WarningColor)
        ThemeManager.ApplyCardStyle(pnlCardCompleted, ThemeManager.SuccessColor)
        ThemeManager.ApplyCardStyle(pnlCardOverdue, ThemeManager.DangerColor)

        ' Card title labels
        lblTitleTotal.ForeColor = ThemeManager.MutedTextColor
        lblTitleTotal.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        lblTitlePending.ForeColor = ThemeManager.MutedTextColor
        lblTitlePending.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        lblTitleCompleted.ForeColor = ThemeManager.MutedTextColor
        lblTitleCompleted.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)
        lblTitleOverdue.ForeColor = ThemeManager.MutedTextColor
        lblTitleOverdue.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular)

        ' Card count labels
        lblCountTotal.ForeColor = ThemeManager.PrimaryColor
        lblCountTotal.Font = ThemeManager.CountFont
        lblCountPending.ForeColor = ThemeManager.WarningColor
        lblCountPending.Font = ThemeManager.CountFont
        lblCountCompleted.ForeColor = ThemeManager.SuccessColor
        lblCountCompleted.Font = ThemeManager.CountFont
        lblCountOverdue.ForeColor = ThemeManager.DangerColor
        lblCountOverdue.Font = ThemeManager.CountFont

        ' Grids polish
        ThemeManager.StyleDataGridView(dgvTasks)
        ThemeManager.StyleDataGridView(dgvRecentTasks)
        ThemeManager.StyleDataGridView(dgvCalendarTasks)

        ' Row hover effect on all grids
        For Each dgv As DataGridView In {dgvTasks, dgvRecentTasks, dgvCalendarTasks}
            AddGridRowHover(dgv)
        Next

        ' Empty state panels — styled with icon + message
        ThemeManager.StyleEmptyState(pnlEmptyTasks, lblEmptyTasks, "📭", "No tasks yet — click ＋ Add Task to get started.")
        ThemeManager.StyleEmptyState(pnlEmptyRecent, lblEmptyRecent, "🎉", "You have no upcoming tasks.")
        ThemeManager.StyleEmptyState(pnlEmptyCalendar, lblEmptyCalendar, "📅", "No tasks due on this day.")
        For Each pnl As Panel In {pnlEmptyTasks, pnlEmptyRecent, pnlEmptyCalendar}
            pnl.Visible = False
        Next

        ' Action bar top border (since it's docked bottom)
        AddHandler flpActionBar.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                Using p As New Pen(ThemeManager.BorderColor, 1)
                    ev.Graphics.DrawLine(p, 0, 0, flpActionBar.Width, 0)
                End Using
            End Sub
    End Sub

    ''' <summary>Adds a subtle hover highlight to grid rows.</summary>
    Private Sub AddGridRowHover(dgv As DataGridView)
        If dgv.Tag IsNot Nothing AndAlso dgv.Tag.ToString() = "hover-wired" Then Return
        dgv.Tag = "hover-wired"
        AddHandler dgv.CellMouseEnter,
            Sub(s As Object, e As DataGridViewCellEventArgs)
                If e.RowIndex < 0 Then Return
                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                If row.Selected Then Return
                row.DefaultCellStyle.BackColor = ThemeManager.PrimaryLightColor
            End Sub
        AddHandler dgv.CellMouseLeave,
            Sub(s As Object, e As DataGridViewCellEventArgs)
                If e.RowIndex < 0 Then Return
                Dim row As DataGridViewRow = dgv.Rows(e.RowIndex)
                If row.Selected Then Return
                row.DefaultCellStyle.BackColor = Color.Empty
            End Sub
    End Sub

    ''' <summary>Applies definitive styles to all 7 action bar buttons. Called twice — once
    ''' during ApplyPerfectLayout and once via BeginInvoke after the message loop starts,
    ''' to guarantee ThemeManager cannot overwrite them.</summary>
    Private Sub StyleActionBarButtons()
        PaintButton(btnAddTask,      "＋  Add Task",    ThemeManager.PrimaryColor,  Color.White,                  0)
        PaintButton(btnEditTask,     "✏  Edit",         Color.White,                ThemeManager.TextColor,       1)
        PaintButton(btnDeleteTask,   "🗑  Delete",       ThemeManager.DangerColor,   Color.White,                  0)
        PaintButton(btnViewDetails,  "🔍  Details",      Color.White,                ThemeManager.TextColor,       1)
        PaintButton(btnToggleStatus, "✔  Toggle",        ThemeManager.SuccessColor,  Color.White,                  0)
        PaintButton(btnDuplicateTask,"⧉  Duplicate",    Color.White,                ThemeManager.TextColor,       1)
        PaintButton(btnPrintTasks,   "🖨  Print",        Color.White,                ThemeManager.TextColor,       1)
        flpActionBar.Refresh()
    End Sub

    ''' <summary>Wires a custom Paint handler to a button so its background/text
    ''' are drawn by GDI+ — immune to WinForms visual-style overrides.</summary>
    Private Shared Sub PaintButton(btn As Button, label As String,
                                   bgColor As Color, fgColor As Color,
                                   borderWidth As Integer)
        ' Set properties
        btn.Text = label
        btn.ForeColor = fgColor
        btn.BackColor = bgColor
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = borderWidth
        btn.FlatAppearance.BorderColor = ThemeManager.BorderColor
        btn.FlatAppearance.MouseOverBackColor = bgColor
        btn.FlatAppearance.MouseDownBackColor = bgColor
        btn.UseVisualStyleBackColor = False
        btn.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btn.Height = 42
        btn.Cursor = Cursors.Hand

        ' Remove any previous Paint handler to avoid stacking
        If btn.Tag IsNot Nothing AndAlso btn.Tag.ToString() = "paint-wired" Then Return
        btn.Tag = "paint-wired"

        Dim capturedBg As Color = bgColor
        Dim capturedFg As Color = fgColor
        Dim capturedBorder As Integer = borderWidth

        AddHandler btn.Paint,
            Sub(s As Object, e As PaintEventArgs)
                Dim b As Button = DirectCast(s, Button)
                Dim g As Graphics = e.Graphics
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Fill background
                Using br As New SolidBrush(capturedBg)
                    g.FillRectangle(br, b.ClientRectangle)
                End Using

                ' Border
                If capturedBorder > 0 Then
                    Using p As New Pen(ThemeManager.BorderColor, 1)
                        Dim r As New Rectangle(0, 0, b.Width - 1, b.Height - 1)
                        g.DrawRectangle(p, r)
                    End Using
                End If

                ' Text
                Dim sf As New StringFormat() With {
                    .Alignment = StringAlignment.Center,
                    .LineAlignment = StringAlignment.Center
                }
                Using tb As New SolidBrush(capturedFg)
                    g.DrawString(b.Text, b.Font, tb, New RectangleF(0, 0, b.Width, b.Height), sf)
                End Using
                sf.Dispose()
            End Sub
    End Sub

    ''' <summary>Style a status filter pill button. Active = filled indigo, inactive = ghost.</summary>
    Private Sub StyleFilterPill(btn As Button, active As Boolean)
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btn.Cursor = Cursors.Hand
        btn.Height = 34
        If active Then
            btn.BackColor = ThemeManager.PrimaryColor
            btn.ForeColor = Color.White
            btn.FlatAppearance.BorderColor = ThemeManager.PrimaryColor
            btn.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryHoverColor
        Else
            btn.BackColor = Color.White
            btn.ForeColor = ThemeManager.PrimaryColor
            btn.FlatAppearance.BorderColor = ThemeManager.BorderColor
            btn.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryLightColor
        End If
    End Sub

    ''' <summary>Ghost button — white bg, indigo text, subtle border.</summary>
    Private Sub StyleGhostButton(btn As Button, label As String)
        btn.BackColor = Color.White
        btn.ForeColor = ThemeManager.TextColor
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 1
        btn.FlatAppearance.BorderColor = ThemeManager.BorderColor
        btn.FlatAppearance.MouseOverBackColor = ThemeManager.PrimaryLightColor
        btn.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btn.Text = label
        btn.Cursor = Cursors.Hand
        btn.Height = 42
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        DismissTimesUpBanner()
        If searchDebounceTimer IsNot Nothing Then
            searchDebounceTimer.Stop()
            searchDebounceTimer.Dispose()
            searchDebounceTimer = Nothing
        End If
        If refreshTimer IsNot Nothing Then
            refreshTimer.Stop()
            refreshTimer.Dispose()
            refreshTimer = Nothing
        End If
        If reminderService IsNot Nothing Then
            reminderService.Dispose()
            reminderService = Nothing
        End If
        MyBase.OnFormClosed(e)
    End Sub

    ' --- TIMES UP HANDLER ────────────────────────────────────────────────────

    ''' <summary>Called when one or more tasks just hit their due time.
    ''' Shows a dismissible red banner at the top of the dashboard and
    ''' refreshes the grid so overdue rows turn red immediately.</summary>
    Private Sub OnTimesUp(taskIDs As List(Of Integer), titles As List(Of String))
        ' Must run on UI thread
        If Me.InvokeRequired Then
            Me.Invoke(Sub() OnTimesUp(taskIDs, titles))
            Return
        End If

        ' Refresh grid so the rows turn red
        LoadTasks()
        LoadDashboardMetrics()

        ' Build message
        Dim msg As String
        If titles.Count = 1 Then
            msg = $"⏰  Time's Up!   ""{titles(0)}"" is now due."
        Else
            msg = $"⏰  Time's Up!   {titles.Count} tasks are now due: " &
                  String.Join(", ", titles.Take(3)) &
                  If(titles.Count > 3, "…", "")
        End If

        ShowTimesUpBanner(msg)
    End Sub

    Private _timesUpBanner As Panel = Nothing

    ''' <summary>Shows a red dismissible banner at the top of pnlMainContent.</summary>
    Private Sub ShowTimesUpBanner(message As String)
        ' Remove any existing banner
        DismissTimesUpBanner()

        _timesUpBanner = New Panel() With {
            .BackColor = ColorTranslator.FromHtml("#EF4444"),
            .Dock      = DockStyle.Top,
            .Height    = 44,
            .Name      = "pnlTimesUpBanner"
        }

        Dim lbl As New Label() With {
            .AutoSize  = False,
            .Dock      = DockStyle.Fill,
            .Font      = New Font("Segoe UI Semibold", 10.5F, FontStyle.Bold),
            .ForeColor = Color.White,
            .Padding   = New Padding(16, 0, 0, 0),
            .Text      = message,
            .TextAlign = ContentAlignment.MiddleLeft
        }

        Dim btnDismiss As New Button() With {
            .BackColor = Color.Transparent,
            .Dock      = DockStyle.Right,
            .FlatStyle = FlatStyle.Flat,
            .Font      = New Font("Segoe UI", 11.0F, FontStyle.Bold),
            .ForeColor = Color.White,
            .Size      = New Size(44, 44),
            .Text      = "✕",
            .Cursor    = Cursors.Hand
        }
        btnDismiss.FlatAppearance.BorderSize = 0
        btnDismiss.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#DC2626")
        AddHandler btnDismiss.Click, Sub(s As Object, ev As EventArgs) DismissTimesUpBanner()

        _timesUpBanner.Controls.Add(lbl)
        _timesUpBanner.Controls.Add(btnDismiss)

        ' Insert at top of main content area
        pnlMainContent.Controls.Add(_timesUpBanner)
        _timesUpBanner.BringToFront()

        ' Auto-dismiss after 30 seconds
        Dim autoClose As New Timer() With {.Interval = 30_000}
        AddHandler autoClose.Tick,
            Sub(s As Object, ev As EventArgs)
                autoClose.Stop()
                autoClose.Dispose()
                DismissTimesUpBanner()
            End Sub
        autoClose.Start()
    End Sub

    Private Sub DismissTimesUpBanner()
        If _timesUpBanner IsNot Nothing Then
            pnlMainContent.Controls.Remove(_timesUpBanner)
            _timesUpBanner.Dispose()
            _timesUpBanner = Nothing
        End If
    End Sub

    ' --- CUSTOM TITLE BAR DRAGGING & CONTROLS ---

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    Private Sub lblWelcome_MouseDown(sender As Object, e As MouseEventArgs) Handles lblWelcome.MouseDown
        If e.Button = MouseButtons.Left Then
            ReleaseCapture()
            SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0)
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnMaximize_Click(sender As Object, e As EventArgs) Handles btnMaximize.Click
        If Me.WindowState = FormWindowState.Normal Then
            Me.WindowState = FormWindowState.Maximized
        Else
            Me.WindowState = FormWindowState.Normal
        End If
    End Sub

    ' --- SIDEBAR NAVIGATION ---

    Private Sub SetActiveSidebarButton(active As Button)
        For Each btn As Button In {btnNavDashboard, btnNavManageTasks, btnNavCalendar}
            btn.BackColor = Color.Transparent
            btn.ForeColor = Color.White
        Next
        active.BackColor = ThemeManager.SidebarActiveColor
        active.ForeColor = Color.White
    End Sub

    Private Sub btnNavDashboard_Click(sender As Object, e As EventArgs) Handles btnNavDashboard.Click
        SetActiveSidebarButton(btnNavDashboard)
        LoadDashboardMetrics()
        pnlDashboardView.Visible = True
        pnlDashboardView.BringToFront()
        pnlManageTasksView.Visible = False
        pnlCalendarView.Visible = False
    End Sub

    Private Sub btnNavManageTasks_Click(sender As Object, e As EventArgs) Handles btnNavManageTasks.Click
        SetActiveSidebarButton(btnNavManageTasks)
        LoadTasks()
        pnlManageTasksView.Visible = True
        pnlManageTasksView.BringToFront()
        pnlDashboardView.Visible = False
        pnlCalendarView.Visible = False
        StyleActionBarButtons()
    End Sub

    Private Sub btnNavCalendar_Click(sender As Object, e As EventArgs) Handles btnNavCalendar.Click
        SetActiveSidebarButton(btnNavCalendar)
        pnlCalendarView.Visible = True
        pnlCalendarView.BringToFront()
        pnlDashboardView.Visible = False
        pnlManageTasksView.Visible = False
        calTasks.SelectionStart = DateTime.Today
        calTasks.SelectionEnd = DateTime.Today
        LoadCalendarTasks(DateTime.Today)
    End Sub

    Private Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to logout?", "Confirm Logout",
                                                    MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If result = DialogResult.Yes Then
            GlobalVariables.CurrentUser = Nothing
            Dim loginForm As New frmLogin()
            loginForm.Show()
            Me.Close()
        End If
    End Sub

    Private Sub btnExportCSV_Click(sender As Object, e As EventArgs)
        ExportToCSV
    End Sub

    Private Sub btnExportJSON_Click(sender As Object, e As EventArgs)
        ExportToJSON
    End Sub

    ' --- DATA LOADING ---

    Private Sub SetupDataGridViews()
        With dgvTasks
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
        End With
        AddHandler dgvTasks.CellDoubleClick, AddressOf dgvTasks_CellDoubleClick
        AddHandler dgvTasks.ColumnHeaderMouseClick, AddressOf dgvTasks_ColumnHeaderMouseClick

        ' Right-click context menu
        Dim ctx As New ContextMenuStrip()
        ctx.Font = New Font("Segoe UI", 10.0F)
        Dim miAdd    As New ToolStripMenuItem("＋ Add Task",    Nothing, Sub(s, e) btnAddTask_Click(s, e))
        Dim miEdit   As New ToolStripMenuItem("✏  Edit",        Nothing, Sub(s, e) btnEditTask_Click(s, e))
        Dim miDelete As New ToolStripMenuItem("🗑  Delete",      Nothing, Sub(s, e) btnDeleteTask_Click(s, e))
        Dim miView   As New ToolStripMenuItem("🔍  View Details",Nothing, Sub(s, e) btnViewDetails_Click(s, e))
        Dim miToggle As New ToolStripMenuItem("✔  Toggle Status",Nothing, Sub(s, e) btnToggleStatus_Click(s, e))
        Dim miDupe   As New ToolStripMenuItem("⧉  Duplicate",   Nothing, Sub(s, e) btnDuplicateTask_Click(s, e))
        miDelete.ForeColor = ThemeManager.DangerColor
        ctx.Items.AddRange({miAdd, New ToolStripSeparator(), miEdit, miDelete,
                            New ToolStripSeparator(), miView, miToggle, miDupe})
        dgvTasks.ContextMenuStrip = ctx

        With dgvRecentTasks
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
        End With
        AddHandler dgvRecentTasks.CellDoubleClick, AddressOf dgvRecentTasks_CellDoubleClick

        With dgvCalendarTasks
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect
            .MultiSelect = False
            .ReadOnly = True
            .AllowUserToAddRows = False
        End With
        AddHandler dgvCalendarTasks.CellDoubleClick, AddressOf dgvCalendarTasks_CellDoubleClick

        ' Keyboard shortcuts on the form
        Me.KeyPreview = True
        AddHandler Me.KeyDown, AddressOf Dashboard_KeyDown

        ' Search clear button
        AddHandler btnSearchClear.Click, AddressOf btnSearchClear_Click

        ' ── Drag-and-drop on dgvTasks to reorder / change status ─────────────
        dgvTasks.AllowDrop = True
        AddHandler dgvTasks.MouseDown,  AddressOf dgvTasks_DragMouseDown
        AddHandler dgvTasks.DragEnter,  AddressOf dgvTasks_DragEnter
        AddHandler dgvTasks.DragOver,   AddressOf dgvTasks_DragOver
        AddHandler dgvTasks.DragDrop,   AddressOf dgvTasks_DragDrop

        ' Tooltips for action buttons
        Dim tt As New ToolTip() With {.InitialDelay = 400, .ShowAlways = True}
        tt.SetToolTip(btnAddTask,      "Add new task  (Ctrl+N)")
        tt.SetToolTip(btnEditTask,     "Edit selected task  (Ctrl+E)")
        tt.SetToolTip(btnDeleteTask,   "Delete selected task  (Del)")
        tt.SetToolTip(btnViewDetails,  "View task details  (Enter)")
        tt.SetToolTip(btnToggleStatus, "Toggle Pending ↔ Completed  (Ctrl+T)")
        tt.SetToolTip(btnDuplicateTask,"Duplicate task  (Ctrl+D)")
        tt.SetToolTip(btnPrintTasks,   "Print task list  (Ctrl+P)")
        tt.SetToolTip(txtSearch,       "Search across title, description, category, tag, priority")

        ' Style empty state panels
        For Each pnl As Panel In {pnlEmptyTasks, pnlEmptyRecent, pnlEmptyCalendar}
            pnl.BackColor = ThemeManager.SurfaceColor
        Next
        For Each lbl As Label In {lblEmptyTasks, lblEmptyRecent, lblEmptyCalendar}
            lbl.ForeColor = ThemeManager.MutedTextColor
        Next
    End Sub

    ' ── Safe helper: get integer from scalar, returns 0 on null ─────────────
    Private Shared Function SafeScalar(result As Object) As Integer
        If result Is Nothing OrElse result Is DBNull.Value Then Return 0
        Return Convert.ToInt32(result)
    End Function

    ' ── Safe helper: get selected task ID, returns -1 if none ────────────────
    Private Function SelectedTaskID() As Integer
        If dgvTasks.CurrentRow Is Nothing Then Return -1
        If Not dgvTasks.Columns.Contains("TaskID") Then Return -1
        Dim v = dgvTasks.CurrentRow.Cells("TaskID").Value
        If v Is Nothing OrElse v Is DBNull.Value Then Return -1
        Return Convert.ToInt32(v)
    End Function

    Private Sub LoadDashboardMetrics()
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            Dim uid As Integer = GlobalVariables.CurrentUser.UserID
            Dim p As MySqlParameter() = {New MySqlParameter("@UserID", uid)}
            Dim op As MySqlParameter() = {New MySqlParameter("@UserID", uid),
                                           New MySqlParameter("@Today", DateTime.Today)}

            Dim total     As Integer = SafeScalar(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID", p))
            Dim pending   As Integer = SafeScalar(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND Status='Pending'", p))
            Dim completed As Integer = SafeScalar(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND Status='Completed'", p))
            Dim overdue   As Integer = SafeScalar(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND Status='Pending' AND DueDate<@Today", op))

            lblCountTotal.Text     = total.ToString()
            lblCountPending.Text   = pending.ToString()
            lblCountCompleted.Text = completed.ToString()
            lblCountOverdue.Text   = overdue.ToString()

            ' Progress bar
            If total > 0 Then
                Dim pct As Integer = CInt(Math.Round(completed / CDbl(total) * 100))
                pbCompletion.Value  = Math.Min(pct, 100)
                lblProgressPct.Text = $"{pct}%"
                lblProgressPct.ForeColor = If(pct >= 80, ThemeManager.SuccessColor,
                                           If(pct >= 40, ThemeManager.WarningColor, ThemeManager.DangerColor))
            Else
                pbCompletion.Value  = 0
                lblProgressPct.Text = "0%"
                lblProgressPct.ForeColor = ThemeManager.MutedTextColor
            End If

            ' Recent tasks grid
            Dim dtRecent As DataTable = DatabaseHelper.GetDataTable(
                "SELECT TaskID,Title,DueDate,Priority,Status,Category FROM Tasks WHERE UserID=@UserID ORDER BY DueDate ASC LIMIT 10", p)
            If dtRecent IsNot Nothing Then
                AddComputedColumns(dtRecent)
                dgvRecentTasks.DataSource = dtRecent
                If dgvRecentTasks.Columns.Contains("TaskID") Then dgvRecentTasks.Columns("TaskID").Visible = False
                ThemeManager.StyleDataGridView(dgvRecentTasks)
                ColorCodeGrid(dgvRecentTasks)
                dgvRecentTasks.Visible = dtRecent.Rows.Count > 0
                pnlEmptyRecent.Visible = dtRecent.Rows.Count = 0
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("LoadDashboardMetrics error: " & ex.Message)
        End Try
    End Sub

    Private Sub LoadTasks()
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            Dim query As String = "SELECT TaskID,Title,DueDate,Priority,Status,Category,IsRecurring,Tag FROM Tasks WHERE UserID=@UserID"
            Dim p As MySqlParameter() = {New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)}

            Select Case currentFilter
                Case "Completed" : query &= " AND Status='Completed'"
                Case "Pending"   : query &= " AND Status='Pending'"
            End Select
            query &= " ORDER BY DueDate ASC"

            Dim dt As DataTable = DatabaseHelper.GetDataTable(query, p)
            If dt Is Nothing Then Return
            AddComputedColumns(dt)
            dgvTasks.DataSource = dt
            lblTaskCount.Text = If(currentFilter = "All", $"Total: {dt.Rows.Count} tasks", $"{currentFilter}: {dt.Rows.Count} tasks")

            If dgvTasks.Columns.Contains("TaskID") Then dgvTasks.Columns("TaskID").Visible = False
            If dgvTasks.Columns.Contains("IsRecurring") Then dgvTasks.Columns("IsRecurring").Visible = False
            ColorCodeGrid(dgvTasks)
            ColorCodeDaysLeft(dgvTasks)

            ' Empty state
            Dim hasRows As Boolean = dt.Rows.Count > 0
            dgvTasks.Visible = hasRows
            pnlEmptyTasks.Visible = Not hasRows
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("LoadTasks error: " & ex.Message)
        End Try
    End Sub

    Private Sub AddComputedColumns(dt As DataTable)
        If dt Is Nothing Then Return

        ' Use String so we can display "Time's Up!" instead of a negative number
        If Not dt.Columns.Contains("DaysLeft") Then
            dt.Columns.Add("DaysLeft", GetType(String))
        End If

        For Each row As DataRow In dt.Rows
            If row.Table.Columns.Contains("DueDate") AndAlso row("DueDate") IsNot DBNull.Value Then
                Dim due As DateTime = Convert.ToDateTime(row("DueDate"))
                Dim status As String = If(row.Table.Columns.Contains("Status") AndAlso
                                          row("Status") IsNot DBNull.Value,
                                          row("Status").ToString(), "")
                Dim days As Integer = CInt(Math.Floor((due.Date - DateTime.Today).TotalDays))

                If status = "Completed" Then
                    row("DaysLeft") = If(days >= 0, days.ToString(), "Done")
                ElseIf days < 0 Then
                    row("DaysLeft") = "⏰ Time's Up!"
                ElseIf days = 0 Then
                    row("DaysLeft") = "Today"
                Else
                    row("DaysLeft") = days.ToString()
                End If
            Else
                row("DaysLeft") = ""
            End If
        Next
    End Sub

    Private Sub ColorCodeGrid(grid As DataGridView)
        ThemeManager.ColorCodeGridByPriority(grid)
        ' Tint overdue+pending rows with a soft red background
        If Not grid.Columns.Contains("Status") OrElse Not grid.Columns.Contains("DaysLeft") Then Return
        For Each row As DataGridViewRow In grid.Rows
            If row.Cells("Status").Value?.ToString() = "Pending" Then
                Dim daysVal As String = row.Cells("DaysLeft").Value?.ToString()
                If daysVal IsNot Nothing AndAlso daysVal.Contains("Time's Up") Then
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FEE2E2")
                    row.DefaultCellStyle.ForeColor = ColorTranslator.FromHtml("#991B1B")
                End If
            End If
        Next
    End Sub

    ''' <summary>Color-codes the DaysLeft cell text.</summary>
    Private Sub ColorCodeDaysLeft(grid As DataGridView)
        If Not grid.Columns.Contains("DaysLeft") Then Return
        For Each row As DataGridViewRow In grid.Rows
            If row.Cells("DaysLeft").Value Is Nothing Then Continue For
            Dim val As String = row.Cells("DaysLeft").Value.ToString()
            Dim status As String = If(grid.Columns.Contains("Status") AndAlso
                                      row.Cells("Status").Value IsNot Nothing,
                                      row.Cells("Status").Value.ToString(), "")

            If status = "Completed" OrElse val = "Done" Then
                row.Cells("DaysLeft").Style.ForeColor = ThemeManager.MutedTextColor
                row.Cells("DaysLeft").Style.Font = New Font("Segoe UI", 9.5F)

            ElseIf val.Contains("Time's Up") Then
                ' Bold red pulsing text
                row.Cells("DaysLeft").Style.ForeColor = ThemeManager.DangerColor
                row.Cells("DaysLeft").Style.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)

            ElseIf val = "Today" Then
                row.Cells("DaysLeft").Style.ForeColor = ThemeManager.WarningColor
                row.Cells("DaysLeft").Style.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)

            ElseIf Integer.TryParse(val, Nothing) Then
                Dim d As Integer = Integer.Parse(val)
                If d >= 3 Then
                    row.Cells("DaysLeft").Style.ForeColor = ThemeManager.SuccessColor
                    row.Cells("DaysLeft").Style.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
                Else
                    row.Cells("DaysLeft").Style.ForeColor = ThemeManager.WarningColor
                    row.Cells("DaysLeft").Style.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
                End If
            End If
        Next
    End Sub

    Private Sub dgvTasks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        btnViewDetails_Click(sender, EventArgs.Empty)
    End Sub

    Private Sub dgvRecentTasks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            If dgvRecentTasks.CurrentRow Is Nothing Then Return
            If Not dgvRecentTasks.Columns.Contains("TaskID") Then Return
            Dim taskID As Integer = Convert.ToInt32(dgvRecentTasks.CurrentRow.Cells("TaskID").Value)
            Dim detailsForm As New frmTaskDetails(taskID)
            detailsForm.ShowDialog()
        Catch
        End Try
    End Sub

    Private Sub dgvCalendarTasks_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
        If e.RowIndex < 0 Then Return
        Try
            If dgvCalendarTasks.CurrentRow Is Nothing Then Return
            If Not dgvCalendarTasks.Columns.Contains("TaskID") Then Return
            Dim taskID As Integer = Convert.ToInt32(dgvCalendarTasks.CurrentRow.Cells("TaskID").Value)
            Dim detailsForm As New frmTaskDetails(taskID)
            detailsForm.ShowDialog()
        Catch
        End Try
    End Sub

    ' --- CALENDAR LOGIC ---

    Private Sub calTasks_DateSelected(sender As Object, e As DateRangeEventArgs) Handles calTasks.DateSelected
        LoadCalendarTasks(e.Start)
    End Sub

    Private Sub LoadCalendarTasks(selectedDate As DateTime)
        lblCalendarDate.Text = "Tasks for " & selectedDate.ToString("MMMM dd, yyyy")
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            Dim p As MySqlParameter() = {
                New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID),
                New MySqlParameter("@SelectedDate", selectedDate)
            }
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT TaskID,Title,Priority,Status FROM Tasks WHERE UserID=@UserID AND DATE(DueDate)=DATE(@SelectedDate)", p)
            If dt Is Nothing Then Return
            dgvCalendarTasks.DataSource = dt
            If dgvCalendarTasks.Columns.Contains("TaskID") Then dgvCalendarTasks.Columns("TaskID").Visible = False
            ColorCodeGrid(dgvCalendarTasks)
            Dim hasRows As Boolean = dt.Rows.Count > 0
            dgvCalendarTasks.Visible = hasRows
            pnlEmptyCalendar.Visible = Not hasRows
            If Not hasRows Then lblCalendarDate.Text &= "  — no tasks"
        Catch ex As Exception
            MessageBox.Show("Error loading calendar tasks: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- TASK MANAGEMENT ---

    Private Sub btnAddTask_Click(sender As Object, e As EventArgs) Handles btnAddTask.Click
        Dim addEditForm As New frmAddEditTask()
        If addEditForm.ShowDialog() = DialogResult.OK Then
            LoadTasks()
            LoadDashboardMetrics()
        End If
    End Sub

    Private Sub btnEditTask_Click(sender As Object, e As EventArgs) Handles btnEditTask.Click
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then
            MessageBox.Show("Please select a task to edit.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim addEditForm As New frmAddEditTask(taskID)
        If addEditForm.ShowDialog() = DialogResult.OK Then
            LoadTasks() : LoadDashboardMetrics()
        End If
    End Sub

    Private Sub btnDeleteTask_Click(sender As Object, e As EventArgs) Handles btnDeleteTask.Click
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then
            MessageBox.Show("Please select a task to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim title As String = If(dgvTasks.CurrentRow.Cells("Title").Value IsNot Nothing,
                                  dgvTasks.CurrentRow.Cells("Title").Value.ToString(), "(untitled)")
        If MessageBox.Show($"Delete ""{title}""?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                DatabaseHelper.ExecuteNonQuery(
                    "DELETE FROM Tasks WHERE TaskID=@TaskID AND UserID=@UserID",
                    {New MySqlParameter("@TaskID", taskID),
                     New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})
                LoadTasks() : LoadDashboardMetrics()
            Catch ex As Exception
                MessageBox.Show("Error deleting task: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnViewDetails_Click(sender As Object, e As EventArgs) Handles btnViewDetails.Click
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then
            MessageBox.Show("Please select a task to view.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Call New frmTaskDetails(taskID).ShowDialog()
    End Sub

    Private Sub btnToggleStatus_Click(sender As Object, e As EventArgs) Handles btnToggleStatus.Click
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then
            MessageBox.Show("Please select a task.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Dim currentStatus As String = If(dgvTasks.CurrentRow.Cells("Status").Value IsNot Nothing,
                                          dgvTasks.CurrentRow.Cells("Status").Value.ToString(), "Pending")
        Dim newStatus As String = If(currentStatus = "Pending", "Completed", "Pending")
        Try
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Tasks SET Status=@Status, CompletedAt=@CompletedAt WHERE TaskID=@TaskID AND UserID=@UserID",
                {New MySqlParameter("@Status", newStatus),
                 New MySqlParameter("@CompletedAt", If(newStatus = "Completed", CObj(DateTime.Now), CObj(DBNull.Value))),
                 New MySqlParameter("@TaskID", taskID),
                 New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})

            ' Auto-regenerate recurring task when completed
            If newStatus = "Completed" Then
                TryRegenerateRecurringTask(taskID)
            End If

            LoadTasks() : LoadDashboardMetrics()
        Catch ex As Exception
            MessageBox.Show("Error updating status: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>If the task is recurring, insert the next weekly occurrence.</summary>
    Private Sub TryRegenerateRecurringTask(taskID As Integer)
        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT * FROM Tasks WHERE TaskID=@TaskID AND UserID=@UserID AND IsRecurring=TRUE",
                {New MySqlParameter("@TaskID", taskID),
                 New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return

            Dim row As DataRow = dt.Rows(0)
            Dim nextDue As DateTime = Convert.ToDateTime(row("DueDate")).AddDays(7)
            Dim tag As Object = If(dt.Columns.Contains("Tag") AndAlso Not IsDBNull(row("Tag")), row("Tag"), CObj(DBNull.Value))

            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Tasks (UserID,Title,Description,DueDate,Priority,Status,Category,IsRecurring,Notes,Tag) " &
                "VALUES (@UserID,@Title,@Description,@DueDate,@Priority,'Pending',@Category,TRUE,@Notes,@Tag)",
                {New MySqlParameter("@UserID",      GlobalVariables.CurrentUser.UserID),
                 New MySqlParameter("@Title",       row("Title").ToString()),
                 New MySqlParameter("@Description", row("Description").ToString()),
                 New MySqlParameter("@DueDate",     nextDue),
                 New MySqlParameter("@Priority",    row("Priority").ToString()),
                 New MySqlParameter("@Category",    If(IsDBNull(row("Category")), "General", row("Category").ToString())),
                 New MySqlParameter("@Notes",       If(IsDBNull(row("Notes")), CObj(DBNull.Value), row("Notes"))),
                 New MySqlParameter("@Tag",         tag)})
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Recurring task regeneration failed: " & ex.Message)
        End Try
    End Sub

    ' --- FILTERING & EXPORT ---

    Private Sub btnFilterAll_Click(sender As Object, e As EventArgs) Handles btnFilterAll.Click
        currentFilter = "All"
        StyleFilterPill(btnFilterAll, True)
        StyleFilterPill(btnFilterPending, False)
        StyleFilterPill(btnFilterCompleted, False)
        LoadTasks()
    End Sub

    Private Sub btnFilterPending_Click(sender As Object, e As EventArgs) Handles btnFilterPending.Click
        currentFilter = "Pending"
        StyleFilterPill(btnFilterAll, False)
        StyleFilterPill(btnFilterPending, True)
        StyleFilterPill(btnFilterCompleted, False)
        LoadTasks()
    End Sub

    Private Sub btnFilterCompleted_Click(sender As Object, e As EventArgs) Handles btnFilterCompleted.Click
        currentFilter = "Completed"
        StyleFilterPill(btnFilterAll, False)
        StyleFilterPill(btnFilterPending, False)
        StyleFilterPill(btnFilterCompleted, True)
        LoadTasks()
    End Sub

    Private Sub btnApplyAdvancedFilter_Click(sender As Object, e As EventArgs) Handles btnApplyAdvancedFilter.Click
        If GlobalVariables.CurrentUser Is Nothing Then Return
        ' Validate date range
        If dtpFilterFrom.Value.Date > dtpFilterTo.Value.Date Then
            MessageBox.Show("'From' date must be before or equal to 'To' date.", "Invalid Date Range",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Dim query As String = "SELECT TaskID,Title,DueDate,Priority,Status,Category,IsRecurring,Tag FROM Tasks WHERE UserID=@UserID AND DueDate>=@FromDate AND DueDate<=@ToDate"
            Dim parameters As New List(Of MySqlParameter) From {
                New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID),
                New MySqlParameter("@FromDate", dtpFilterFrom.Value.Date),
                New MySqlParameter("@ToDate", dtpFilterTo.Value.Date.AddDays(1).AddTicks(-1))
            }
            If cmbFilterPriority.SelectedItem IsNot Nothing AndAlso cmbFilterPriority.SelectedItem.ToString() <> "All" Then
                query &= " AND Priority=@Priority"
                parameters.Add(New MySqlParameter("@Priority", cmbFilterPriority.SelectedItem.ToString()))
            End If
            query &= " ORDER BY DueDate ASC"

            Dim dt As DataTable = DatabaseHelper.GetDataTable(query, parameters.ToArray())
            If dt Is Nothing Then Return
            AddComputedColumns(dt)
            dgvTasks.DataSource = dt
            lblTaskCount.Text = If(dt.Rows.Count = 0, "No tasks match the filter", $"Filtered: {dt.Rows.Count} tasks")
            If dgvTasks.Columns.Contains("TaskID") Then dgvTasks.Columns("TaskID").Visible = False
            If dgvTasks.Columns.Contains("IsRecurring") Then dgvTasks.Columns("IsRecurring").Visible = False
            ColorCodeGrid(dgvTasks)
            ColorCodeDaysLeft(dgvTasks)
            Dim hasRows As Boolean = dt.Rows.Count > 0
            dgvTasks.Visible = hasRows
            pnlEmptyTasks.Visible = Not hasRows
        Catch ex As Exception
            MessageBox.Show("Error applying filters: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        ' Show/hide clear button
        btnSearchClear.Visible = txtSearch.Text.Length > 0

        ' Cancel any pending debounce
        If searchDebounceTimer IsNot Nothing Then
            searchDebounceTimer.Stop()
            searchDebounceTimer.Dispose()
            searchDebounceTimer = Nothing
        End If

        If String.IsNullOrWhiteSpace(txtSearch.Text) Then
            LoadTasks()
            Return
        End If

        ' Start 300 ms debounce
        searchDebounceTimer = New Timer() With {.Interval = 300}
        AddHandler searchDebounceTimer.Tick,
            Sub(s, ev)
                searchDebounceTimer.Stop()
                searchDebounceTimer.Dispose()
                searchDebounceTimer = Nothing
                ExecuteSearch(txtSearch.Text.Trim())
            End Sub
        searchDebounceTimer.Start()
    End Sub

    Private Sub ExecuteSearch(term As String)
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            Dim p As MySqlParameter() = {
                New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID),
                New MySqlParameter("@Search", "%" & term & "%")
            }
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT TaskID,Title,DueDate,Priority,Status,Category,Tag FROM Tasks " &
                "WHERE UserID=@UserID AND (Title LIKE @Search OR Description LIKE @Search " &
                "OR Category LIKE @Search OR Priority LIKE @Search" &
                (If(True, " OR IFNULL(Tag,'') LIKE @Search", "")) &
                ") ORDER BY DueDate ASC", p)
            If dt Is Nothing Then Return
            AddComputedColumns(dt)
            dgvTasks.DataSource = dt
            lblTaskCount.Text = If(dt.Rows.Count = 0, "No tasks found", $"Found: {dt.Rows.Count} tasks")
            If dgvTasks.Columns.Contains("TaskID") Then dgvTasks.Columns("TaskID").Visible = False
            If dgvTasks.Columns.Contains("IsRecurring") Then dgvTasks.Columns("IsRecurring").Visible = False
            ColorCodeGrid(dgvTasks)
            ColorCodeDaysLeft(dgvTasks)
            Dim hasRows As Boolean = dt.Rows.Count > 0
            dgvTasks.Visible = hasRows
            pnlEmptyTasks.Visible = Not hasRows
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Search error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnSearchClear_Click(sender As Object, e As EventArgs)
        If searchDebounceTimer IsNot Nothing Then
            searchDebounceTimer.Stop()
            searchDebounceTimer.Dispose()
            searchDebounceTimer = Nothing
        End If
        txtSearch.Text = ""
        btnSearchClear.Visible = False
        LoadTasks()
    End Sub

    Private Sub ExportToCSV()
        Try
            Dim dt As DataTable = TryCast(dgvTasks.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                MessageBox.Show("There are no tasks to export.", "Nothing to Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim sfd As New SaveFileDialog()
            sfd.Filter = "CSV files (*.csv)|*.csv"
            sfd.FileName = "TasksExport_" & DateTime.Now.ToString("yyyyMMdd") & ".csv"

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim sb As New StringBuilder()

                ' Excluded columns
                Dim excluded As New HashSet(Of String)({"TaskID", "DaysLeft"})

                ' Headers
                Dim headers As New List(Of String)()
                For Each col As DataColumn In dt.Columns
                    If Not excluded.Contains(col.ColumnName) Then
                        headers.Add(CsvEscape(col.ColumnName))
                    End If
                Next
                sb.AppendLine(String.Join(",", headers))

                ' Rows
                For Each row As DataRow In dt.Rows
                    Dim fields As New List(Of String)()
                    For Each col As DataColumn In dt.Columns
                        If Not excluded.Contains(col.ColumnName) Then
                            Dim raw As String = If(row(col.ColumnName) Is DBNull.Value, "", row(col.ColumnName).ToString())
                            fields.Add(CsvEscape(raw))
                        End If
                    Next
                    sb.AppendLine(String.Join(",", fields))
                Next

                ' Write with UTF-8 BOM for Excel compatibility
                File.WriteAllText(sfd.FileName, sb.ToString(), New System.Text.UTF8Encoding(True))
                ' Open containing folder
                Process.Start("explorer.exe", $"/select,""{sfd.FileName}""")
            End If
        Catch ex As Exception
            MessageBox.Show("Error exporting data: " & ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>RFC 4180 CSV field escaping.</summary>
    Private Shared Function CsvEscape(value As String) As String
        If value.Contains(",") OrElse value.Contains("""") OrElse value.Contains(vbCr) OrElse value.Contains(vbLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If
        Return value
    End Function

    ' --- PRINTING LOGIC ---
    Private printDocument As New PrintDocument()
    Private _printData As DataTable
    Private _printPageIndex As Integer

    Private Sub btnPrintTasks_Click(sender As Object, e As EventArgs) Handles btnPrintTasks.Click
        _printData = TryCast(dgvTasks.DataSource, DataTable)
        _printPageIndex = 0

        RemoveHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage

        Using dlg As New PrintPreviewDialog()
            dlg.Document = printDocument
            dlg.WindowState = FormWindowState.Maximized
            dlg.ShowDialog()
        End Using
    End Sub

    Private _printRowIndex As Integer = 0

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        _printPageIndex += 1
        Dim g As Graphics = e.Graphics
        Dim pageW As Integer = e.PageBounds.Width
        Dim pageH As Integer = e.PageBounds.Height
        Dim marginX As Integer = 40
        Dim y As Integer = 40

        ' ── Header ───────────────────────────────────────────────────────────
        Using hFont As New Font("Segoe UI", 13, FontStyle.Bold)
        Using sFont As New Font("Segoe UI", 9)
            g.DrawString("📚 Smart Task Manager — Task Report", hFont, Brushes.Black, marginX, y)
            y += 26
            Dim user As String = If(GlobalVariables.CurrentUser IsNot Nothing, GlobalVariables.CurrentUser.Username, "Unknown")
            g.DrawString($"User: {user}   |   Printed: {DateTime.Now:g}", sFont, Brushes.Gray, marginX, y)
            y += 20
            g.DrawLine(New Pen(Color.LightGray, 1.5F), marginX, y, pageW - marginX, y)
            y += 10
        End Using : End Using

        ' ── Column definitions ────────────────────────────────────────────────
        Dim cols As New List(Of String) From {"Title", "DueDate", "Priority", "Status", "Category", "Tag", "DaysLeft"}
        Dim colWidths As New Dictionary(Of String, Integer) From {
            {"Title", 210}, {"DueDate", 80}, {"Priority", 65}, {"Status", 70}, {"Category", 80}, {"Tag", 70}, {"DaysLeft", 55}
        }

        ' Filter to only columns that exist in the data
        Dim activeCols As New List(Of String)()
        For Each c In cols
            If _printData IsNot Nothing AndAlso _printData.Columns.Contains(c) Then activeCols.Add(c)
        Next

        ' ── Column headers ────────────────────────────────────────────────────
        Using boldFont As New Font("Segoe UI Semibold", 9, FontStyle.Bold)
        Using normalFont As New Font("Segoe UI", 9)
            Dim x As Integer = marginX
            For Each col In activeCols
                g.DrawString(col, boldFont, Brushes.Black, x, y)
                x += colWidths(col)
            Next
            y += 18
            g.DrawLine(Pens.Black, marginX, y, pageW - marginX, y)
            y += 6

            ' ── Rows ─────────────────────────────────────────────────────────
            If _printData Is Nothing OrElse _printData.Rows.Count = 0 Then
                g.DrawString("No tasks to display.", normalFont, Brushes.Gray, marginX, y)
                e.HasMorePages = False
                Return
            End If

            Dim rowIdx As Integer = _printRowIndex
            Dim altBrush As New SolidBrush(ColorTranslator.FromHtml("#F9FAFB"))
            Dim footerH As Integer = 30

            While rowIdx < _printData.Rows.Count
                If y + 18 > pageH - footerH Then
                    _printRowIndex = rowIdx
                    e.HasMorePages = True
                    altBrush.Dispose()
                    GoTo DrawFooter
                End If

                Dim row As DataRow = _printData.Rows(rowIdx)
                ' Alternating row background
                If rowIdx Mod 2 = 1 Then
                    g.FillRectangle(altBrush, marginX, y, pageW - marginX * 2, 18)
                End If

                x = marginX
                For Each col In activeCols
                    Dim val As String = If(row(col) IsNot DBNull.Value, row(col).ToString(), "")
                    Dim maxChars As Integer = CInt(colWidths(col) / 6.5)
                    If val.Length > maxChars Then val = val.Substring(0, maxChars - 1) & "…"
                    g.DrawString(val, normalFont, Brushes.Black, x, y)
                    x += colWidths(col)
                Next
                y += 18
                rowIdx += 1
            End While

            _printRowIndex = 0
            e.HasMorePages = False
            altBrush.Dispose()
        End Using : End Using

DrawFooter:
        ' ── Footer ───────────────────────────────────────────────────────────
        Using fFont As New Font("Segoe UI", 8)
            Dim footerY As Integer = pageH - 25
            g.DrawLine(New Pen(Color.LightGray, 1F), marginX, footerY - 4, pageW - marginX, footerY - 4)
            Dim pageLabel As String = $"Page {_printPageIndex}"
            Dim sz As SizeF = g.MeasureString(pageLabel, fFont)
            g.DrawString(pageLabel, fFont, Brushes.Gray, (pageW - sz.Width) / 2, footerY)
        End Using
    End Sub

    ' --- DUPLICATE TASK ---

    Private Sub btnDuplicateTask_Click(sender As Object, e As EventArgs) Handles btnDuplicateTask.Click
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then
            MessageBox.Show("Please select a task to duplicate.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT * FROM Tasks WHERE TaskID=@TaskID AND UserID=@UserID",
                {New MySqlParameter("@TaskID", taskID),
                 New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then Return

            Dim row As DataRow = dt.Rows(0)
            Dim tag As Object = If(dt.Columns.Contains("Tag") AndAlso Not IsDBNull(row("Tag")), row("Tag"), CObj(DBNull.Value))
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Tasks (UserID,Title,Description,DueDate,Priority,Status,Category,IsRecurring,Notes,Tag) VALUES (@UserID,@Title,@Description,@DueDate,@Priority,'Pending',@Category,@IsRecurring,@Notes,@Tag)",
                {New MySqlParameter("@UserID",      GlobalVariables.CurrentUser.UserID),
                 New MySqlParameter("@Title",       "Copy of " & row("Title").ToString()),
                 New MySqlParameter("@Description", row("Description").ToString()),
                 New MySqlParameter("@DueDate",     Convert.ToDateTime(row("DueDate")).AddDays(1)),
                 New MySqlParameter("@Priority",    row("Priority").ToString()),
                 New MySqlParameter("@Category",    If(IsDBNull(row("Category")), "General", row("Category").ToString())),
                 New MySqlParameter("@IsRecurring", If(IsDBNull(row("IsRecurring")), False, Convert.ToBoolean(row("IsRecurring")))),
                 New MySqlParameter("@Notes",       If(IsDBNull(row("Notes")), CObj(DBNull.Value), row("Notes"))),
                 New MySqlParameter("@Tag",         tag)})
            LoadTasks() : LoadDashboardMetrics()
        Catch ex As Exception
            MessageBox.Show("Error duplicating task: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' --- DRAG AND DROP — reorder tasks or drag to change status ─────────────

    Private _dragSourceRow As Integer = -1
    Private _dragInsertLine As Integer = -1

    ''' <summary>Start drag when user holds mouse down on a row.</summary>
    Private Sub dgvTasks_DragMouseDown(sender As Object, e As MouseEventArgs)
        If e.Button <> MouseButtons.Left Then Return
        Dim hit As DataGridView.HitTestInfo = dgvTasks.HitTest(e.X, e.Y)
        If hit.RowIndex < 0 Then Return
        _dragSourceRow = hit.RowIndex
        dgvTasks.Rows(hit.RowIndex).Selected = True
        ' Start drag with the task ID as data
        Dim taskID As Integer = SelectedTaskID()
        If taskID < 0 Then Return
        dgvTasks.DoDragDrop(taskID.ToString(), DragDropEffects.Move)
    End Sub

    ''' <summary>Accept move drops.</summary>
    Private Sub dgvTasks_DragEnter(sender As Object, e As DragEventArgs)
        If e.Data.GetDataPresent(GetType(String)) Then
            e.Effect = DragDropEffects.Move
        Else
            e.Effect = DragDropEffects.None
        End If
    End Sub

    ''' <summary>Highlight the drop target row while dragging.</summary>
    Private Sub dgvTasks_DragOver(sender As Object, e As DragEventArgs)
        If Not e.Data.GetDataPresent(GetType(String)) Then Return
        e.Effect = DragDropEffects.Move
        Dim pt As Point = dgvTasks.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = dgvTasks.HitTest(pt.X, pt.Y)
        If hit.RowIndex >= 0 AndAlso hit.RowIndex <> _dragSourceRow Then
            _dragInsertLine = hit.RowIndex
            dgvTasks.Invalidate()
        End If
    End Sub

    ''' <summary>On drop: swap the two rows in the DataTable and persist new order,
    ''' OR if dropped on the Status column toggle the status.</summary>
    Private Sub dgvTasks_DragDrop(sender As Object, e As DragEventArgs)
        _dragInsertLine = -1
        If Not e.Data.GetDataPresent(GetType(String)) Then Return

        Dim pt As Point = dgvTasks.PointToClient(New Point(e.X, e.Y))
        Dim hit As DataGridView.HitTestInfo = dgvTasks.HitTest(pt.X, pt.Y)
        If hit.RowIndex < 0 OrElse hit.RowIndex = _dragSourceRow Then Return

        Dim srcTaskID As Integer = Integer.Parse(e.Data.GetData(GetType(String)).ToString())

        ' ── Drop on Status column → toggle status ────────────────────────────
        If dgvTasks.Columns.Contains("Status") AndAlso
           hit.ColumnIndex = dgvTasks.Columns("Status").Index Then
            Try
                Dim currentStatus As String = dgvTasks.Rows(hit.RowIndex).Cells("Status").Value?.ToString()
                Dim newStatus As String = If(currentStatus = "Pending", "Completed", "Pending")
                DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Tasks SET Status=@Status, CompletedAt=@CompletedAt WHERE TaskID=@TaskID AND UserID=@UserID",
                    {New MySqlParameter("@Status", newStatus),
                     New MySqlParameter("@CompletedAt", If(newStatus = "Completed", CObj(DateTime.Now), CObj(DBNull.Value))),
                     New MySqlParameter("@TaskID", srcTaskID),
                     New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})
                LoadTasks() : LoadDashboardMetrics()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Drag-drop status toggle error: " & ex.Message)
            End Try
            Return
        End If

        ' ── Drop on another row → swap rows visually in the DataTable ─────────
        Try
            Dim dt As DataTable = TryCast(dgvTasks.DataSource, DataTable)
            If dt Is Nothing Then Return

            ' Clone the source row data
            Dim srcRow As DataRow = dt.Rows(_dragSourceRow)
            Dim srcValues(dt.Columns.Count - 1) As Object
            srcRow.ItemArray.CopyTo(srcValues, 0)

            Dim dstRow As DataRow = dt.Rows(hit.RowIndex)
            Dim dstValues(dt.Columns.Count - 1) As Object
            dstRow.ItemArray.CopyTo(dstValues, 0)

            ' Swap
            srcRow.ItemArray = dstValues
            dstRow.ItemArray = srcValues

            dgvTasks.ClearSelection()
            dgvTasks.Rows(hit.RowIndex).Selected = True
            ColorCodeGrid(dgvTasks)
            ColorCodeDaysLeft(dgvTasks)
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Drag-drop reorder error: " & ex.Message)
        End Try
        _dragSourceRow = -1
    End Sub

    ' --- SORT BY COLUMN ---

    Private Sub dgvTasks_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        If dgvTasks.DataSource Is Nothing Then Return
        Dim colName As String = dgvTasks.Columns(e.ColumnIndex).DataPropertyName
        If String.IsNullOrEmpty(colName) Then colName = dgvTasks.Columns(e.ColumnIndex).Name

        currentSortAsc = If(currentSortColumn = colName, Not currentSortAsc, True)
        currentSortColumn = colName

        Try
            Dim dt As DataTable = TryCast(dgvTasks.DataSource, DataTable)
            If dt Is Nothing Then Return
            dt.DefaultView.Sort = $"{colName} {If(currentSortAsc, "ASC", "DESC")}"
            dgvTasks.DataSource = dt.DefaultView.ToTable()
            If dgvTasks.Columns.Contains("TaskID") Then dgvTasks.Columns("TaskID").Visible = False
            ColorCodeGrid(dgvTasks)
            For Each col As DataGridViewColumn In dgvTasks.Columns
                col.HeaderText = col.HeaderText.Replace(" ▲", "").Replace(" ▼", "")
            Next
            If e.ColumnIndex < dgvTasks.Columns.Count Then
                dgvTasks.Columns(e.ColumnIndex).HeaderText &= If(currentSortAsc, " ▲", " ▼")
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Sort error: " & ex.Message)
        End Try
    End Sub

    ' --- DARK MODE TOGGLE ---

    Private Sub btnDarkMode_Click(sender As Object, e As EventArgs) Handles btnDarkMode.Click
        isDarkMode = Not isDarkMode
        ApplyDarkMode(isDarkMode)
        btnDarkMode.Text = If(isDarkMode, "☀ Light Mode", "🌙 Dark Mode")
    End Sub

    Private Sub ApplyDarkMode(dark As Boolean)
        Dim bg      As Color = If(dark, ColorTranslator.FromHtml("#0F172A"), ThemeManager.BackgroundColor)
        Dim surface As Color = If(dark, ColorTranslator.FromHtml("#1E293B"), ThemeManager.SurfaceColor)
        Dim text    As Color = If(dark, Color.White, ThemeManager.TextColor)
        Dim sidebar As Color = If(dark, ColorTranslator.FromHtml("#0F172A"), ThemeManager.SidebarColor)
        Dim titleBg As Color = If(dark, ColorTranslator.FromHtml("#0F172A"), ThemeManager.TitleBarColor)

        Me.BackColor = bg
        pnlMainContent.BackColor = bg
        pnlDashboardView.BackColor = bg
        pnlManageTasksView.BackColor = bg
        pnlCalendarView.BackColor = bg
        pnlSidebar.BackColor = sidebar
        pnlTitleBar.BackColor = titleBg

        ' Dashboard labels
        For Each ctrl As Control In pnlDashboardView.Controls
            If TypeOf ctrl Is Label Then
                Dim lbl As Label = DirectCast(ctrl, Label)
                If lbl.Name <> "lblProgressPct" Then lbl.ForeColor = text
            End If
        Next

        ' Manage tasks labels
        For Each ctrl As Control In pnlManageTasksView.Controls
            If TypeOf ctrl Is Label Then DirectCast(ctrl, Label).ForeColor = text
        Next

        ' Cards
        For Each c As Panel In {pnlCardTotal, pnlCardPending, pnlCardCompleted, pnlCardOverdue}
            c.BackColor = surface
            For Each child As Control In c.Controls
                If TypeOf child Is Label Then DirectCast(child, Label).BackColor = surface
            Next
        Next

        ' Grids
        For Each dgv As DataGridView In {dgvTasks, dgvRecentTasks, dgvCalendarTasks}
            dgv.BackgroundColor = surface
            dgv.DefaultCellStyle.BackColor = surface
            dgv.DefaultCellStyle.ForeColor = text
            dgv.AlternatingRowsDefaultCellStyle.BackColor = If(dark,
                ColorTranslator.FromHtml("#263548"), ColorTranslator.FromHtml("#F9FAFB"))
        Next

        ' Search box
        txtSearch.BackColor = surface
        txtSearch.ForeColor = text

        ' Sidebar nav buttons — keep active state visible
        SetActiveSidebarButton(If(pnlDashboardView.Visible, btnNavDashboard,
                               If(pnlManageTasksView.Visible, btnNavManageTasks, btnNavCalendar)))
    End Sub

    ' --- KEYBOARD SHORTCUTS ---

    Private Sub Dashboard_KeyDown(sender As Object, e As KeyEventArgs)
        ' Only act when Manage Tasks view is active
        If pnlManageTasksView.Visible Then
            Select Case True
                Case e.Control AndAlso e.KeyCode = Keys.N
                    e.SuppressKeyPress = True
                    btnAddTask_Click(sender, EventArgs.Empty)
                Case e.Control AndAlso e.KeyCode = Keys.E
                    e.SuppressKeyPress = True
                    btnEditTask_Click(sender, EventArgs.Empty)
                Case e.Control AndAlso e.KeyCode = Keys.D
                    e.SuppressKeyPress = True
                    btnDuplicateTask_Click(sender, EventArgs.Empty)
                Case e.Control AndAlso e.KeyCode = Keys.S
                    e.SuppressKeyPress = True
                    ExportToCSV()
                Case e.Control AndAlso e.KeyCode = Keys.P
                    e.SuppressKeyPress = True
                    btnPrintTasks_Click(sender, EventArgs.Empty)
                Case e.KeyCode = Keys.Delete AndAlso Not e.Control
                    If dgvTasks.Focused OrElse dgvTasks.ContainsFocus Then
                        e.SuppressKeyPress = True
                        btnDeleteTask_Click(sender, EventArgs.Empty)
                    End If
                Case e.KeyCode = Keys.Enter AndAlso Not e.Control
                    If dgvTasks.Focused OrElse dgvTasks.ContainsFocus Then
                        e.SuppressKeyPress = True
                        btnViewDetails_Click(sender, EventArgs.Empty)
                    End If
            End Select
        End If
        ' F5 refreshes from any view
        If e.KeyCode = Keys.F5 Then
            e.SuppressKeyPress = True
            LoadDashboardMetrics()
            LoadTasks()
        End If
    End Sub

    ' --- EXPORT TO JSON ---

    Private Sub ExportToJSON()
        Try
            Dim dt As DataTable = TryCast(dgvTasks.DataSource, DataTable)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                MessageBox.Show("There are no tasks to export.", "Nothing to Export",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim sfd As New SaveFileDialog() With {
                .Filter   = "JSON files (*.json)|*.json",
                .FileName = "TasksExport_" & DateTime.Now.ToString("yyyyMMdd") & ".json"
            }
            If sfd.ShowDialog() <> DialogResult.OK Then Return

            Dim excluded As New HashSet(Of String)({"TaskID", "DaysLeft"})
            Dim sb As New System.Text.StringBuilder()
            sb.AppendLine("[")
            Dim rows As New List(Of String)()
            For Each row As DataRow In dt.Rows
                Dim fields As New List(Of String)()
                For Each col As DataColumn In dt.Columns
                    If excluded.Contains(col.ColumnName) Then Continue For
                    Dim val As String = row(col.ColumnName).ToString() _
                        .Replace("\", "\\").Replace("""", "\""") _
                        .Replace(vbCr, "\r").Replace(vbLf, "\n").Replace(vbTab, "\t")
                    fields.Add($"    ""{col.ColumnName}"": ""{val}""")
                Next
                rows.Add("  {" & vbCrLf & String.Join("," & vbCrLf, fields) & vbCrLf & "  }")
            Next
            sb.Append(String.Join("," & vbCrLf, rows))
            sb.AppendLine(vbCrLf & "]")

            File.WriteAllText(sfd.FileName, sb.ToString(), New System.Text.UTF8Encoding(False))
            Process.Start("explorer.exe", $"/select,""{sfd.FileName}""")
        Catch ex As Exception
            MessageBox.Show("Error exporting JSON: " & ex.Message, "Export Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class