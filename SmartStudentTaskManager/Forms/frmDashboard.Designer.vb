<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDashboard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlSidebar = New Panel()
        btnLogout = New Button()
        btnDarkMode = New Button()
        pnlSpacer3 = New Panel()
        btnNavCalendar = New Button()
        pnlSpacer2 = New Panel()
        btnNavManageTasks = New Button()
        pnlSpacer1 = New Panel()
        btnNavPomodoro = New Button()
        btnNavAnalytics = New Button()
        btnNavProfile = New Button()
        lblAppTitle = New Label()
        pnlTitleBar = New Panel()
        btnClose = New Button()
        btnMaximize = New Button()
        btnMinimize = New Button()
        lblWelcome = New Label()
        pnlMainContent = New Panel()
        pnlDashboardView = New Panel()
        dgvRecentTasks = New DataGridView()
        pnlEmptyRecent = New Panel()
        lblEmptyRecent = New Label()
        lblRecentActivity = New Label()
        pnlProgressRow = New Panel()
        lblProgressPct = New Label()
        pbCompletion = New ProgressBar()
        lblProgressTitle = New Label()
        tlpCards = New TableLayoutPanel()
        pnlCardOverdue = New Panel()
        lblCountOverdue = New Label()
        lblTitleOverdue = New Label()
        pnlCardCompleted = New Panel()
        lblCountCompleted = New Label()
        lblTitleCompleted = New Label()
        pnlCardPending = New Panel()
        lblCountPending = New Label()
        lblTitlePending = New Label()
        pnlCardTotal = New Panel()
        lblCountTotal = New Label()
        lblTitleTotal = New Label()
        pnlManageTasksView = New Panel()
        flpActionBar = New FlowLayoutPanel()
        btnAddTask = New Button()
        btnEditTask = New Button()
        btnDeleteTask = New Button()
        btnViewDetails = New Button()
        btnToggleStatus = New Button()
        btnDuplicateTask = New Button()
        btnPrintTasks = New Button()
        dgvTasks = New DataGridView()
        pnlEmptyTasks = New Panel()
        lblEmptyTasks = New Label()
        pnlDivider = New Panel()
        pnlToolbar = New Panel()
        lblManageTitle = New Label()
        lblTaskCount = New Label()
        txtSearch = New TextBox()
        btnSearchClear = New Button()
        btnFilterAll = New Button()
        btnFilterPending = New Button()
        btnFilterCompleted = New Button()
        lblFilterDate = New Label()
        dtpFilterFrom = New DateTimePicker()
        lblFilterTo = New Label()
        dtpFilterTo = New DateTimePicker()
        lblFilterPriority = New Label()
        cmbFilterPriority = New ComboBox()
        btnApplyAdvancedFilter = New Button()
        pnlDividerTop = New Panel()
        pnlCalendarView = New Panel()
        dgvCalendarTasks = New DataGridView()
        pnlEmptyCalendar = New Panel()
        lblEmptyCalendar = New Label()
        calTasks = New MonthCalendar()
        lblCalendarDate = New Label()
        btnNavDashboard = New Button()
        pnlSidebar.SuspendLayout()
        pnlTitleBar.SuspendLayout()
        pnlMainContent.SuspendLayout()
        pnlDashboardView.SuspendLayout()
        CType(dgvRecentTasks, ComponentModel.ISupportInitialize).BeginInit()
        pnlEmptyRecent.SuspendLayout()
        pnlProgressRow.SuspendLayout()
        tlpCards.SuspendLayout()
        pnlCardOverdue.SuspendLayout()
        pnlCardCompleted.SuspendLayout()
        pnlCardPending.SuspendLayout()
        pnlCardTotal.SuspendLayout()
        pnlManageTasksView.SuspendLayout()
        flpActionBar.SuspendLayout()
        CType(dgvTasks, ComponentModel.ISupportInitialize).BeginInit()
        pnlEmptyTasks.SuspendLayout()
        pnlToolbar.SuspendLayout()
        pnlCalendarView.SuspendLayout()
        CType(dgvCalendarTasks, ComponentModel.ISupportInitialize).BeginInit()
        pnlEmptyCalendar.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlSidebar
        ' 
        pnlSidebar.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        ' Bottom-docked controls added first (they anchor to the bottom)
        pnlSidebar.Controls.Add(btnLogout)
        pnlSidebar.Controls.Add(btnDarkMode)
        ' Top-docked controls: last added = topmost visible
        pnlSidebar.Controls.Add(btnNavAnalytics)
        pnlSidebar.Controls.Add(btnNavPomodoro)
        pnlSidebar.Controls.Add(btnNavCalendar)
        pnlSidebar.Controls.Add(btnNavManageTasks)
        pnlSidebar.Controls.Add(btnNavProfile)
        pnlSidebar.Controls.Add(btnNavDashboard)
        pnlSidebar.Controls.Add(lblAppTitle)
        pnlSidebar.Dock = DockStyle.Left
        pnlSidebar.Location = New Point(0, 0)
        pnlSidebar.Name = "pnlSidebar"
        pnlSidebar.Size = New Size(275, 750)
        pnlSidebar.TabIndex = 0
        ' 
        ' btnLogout
        ' 
        btnLogout.Dock = DockStyle.Bottom
        btnLogout.FlatAppearance.BorderSize = 0
        btnLogout.FlatStyle = FlatStyle.Flat
        btnLogout.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnLogout.ForeColor = Color.White
        btnLogout.Location = New Point(0, 690)
        btnLogout.Name = "btnLogout"
        btnLogout.Padding = New Padding(20, 0, 0, 0)
        btnLogout.Size = New Size(275, 60)
        btnLogout.TabIndex = 4
        btnLogout.Text = "🚪 Logout"
        btnLogout.TextAlign = ContentAlignment.MiddleLeft
        btnLogout.UseVisualStyleBackColor = True
        ' 
        ' btnDarkMode
        ' 
        btnDarkMode.Dock = DockStyle.Bottom
        btnDarkMode.FlatAppearance.BorderSize = 0
        btnDarkMode.FlatStyle = FlatStyle.Flat
        btnDarkMode.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnDarkMode.ForeColor = Color.White
        btnDarkMode.Location = New Point(0, 477)
        btnDarkMode.Name = "btnDarkMode"
        btnDarkMode.Padding = New Padding(20, 0, 0, 0)
        btnDarkMode.Size = New Size(275, 72)
        btnDarkMode.TabIndex = 10
        btnDarkMode.Text = "🌙 Dark Mode"
        btnDarkMode.TextAlign = ContentAlignment.MiddleLeft
        btnDarkMode.UseVisualStyleBackColor = True
        ' 
        ' pnlSpacer3
        ' 
        pnlSpacer3.BackColor = Color.Transparent
        pnlSpacer3.Dock = DockStyle.Top
        pnlSpacer3.Location = New Point(0, 462)
        pnlSpacer3.Name = "pnlSpacer3"
        pnlSpacer3.Size = New Size(275, 15)
        pnlSpacer3.TabIndex = 9
        ' 
        ' btnNavCalendar
        ' 
        btnNavCalendar.Dock = DockStyle.Top
        btnNavCalendar.FlatAppearance.BorderSize = 0
        btnNavCalendar.FlatStyle = FlatStyle.Flat
        btnNavCalendar.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavCalendar.ForeColor = Color.White
        btnNavCalendar.Location = New Point(0, 402)
        btnNavCalendar.Name = "btnNavCalendar"
        btnNavCalendar.Padding = New Padding(20, 0, 0, 0)
        btnNavCalendar.Size = New Size(275, 60)
        btnNavCalendar.TabIndex = 8
        btnNavCalendar.Text = "📅 Calendar"
        btnNavCalendar.TextAlign = ContentAlignment.MiddleLeft
        btnNavCalendar.UseVisualStyleBackColor = True
        ' 
        ' pnlSpacer2
        ' 
        pnlSpacer2.BackColor = Color.Transparent
        pnlSpacer2.Dock = DockStyle.Top
        pnlSpacer2.Location = New Point(0, 387)
        pnlSpacer2.Name = "pnlSpacer2"
        pnlSpacer2.Size = New Size(275, 15)
        pnlSpacer2.TabIndex = 7
        ' 
        ' btnNavManageTasks
        ' 
        btnNavManageTasks.Dock = DockStyle.Top
        btnNavManageTasks.FlatAppearance.BorderSize = 0
        btnNavManageTasks.FlatStyle = FlatStyle.Flat
        btnNavManageTasks.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavManageTasks.ForeColor = Color.White
        btnNavManageTasks.Location = New Point(0, 327)
        btnNavManageTasks.Name = "btnNavManageTasks"
        btnNavManageTasks.Padding = New Padding(20, 0, 0, 0)
        btnNavManageTasks.Size = New Size(275, 60)
        btnNavManageTasks.TabIndex = 2
        btnNavManageTasks.Text = "📋 Manage Tasks"
        btnNavManageTasks.TextAlign = ContentAlignment.MiddleLeft
        btnNavManageTasks.UseVisualStyleBackColor = True
        ' 
        ' pnlSpacer1
        ' 
        pnlSpacer1.BackColor = Color.Transparent
        pnlSpacer1.Dock = DockStyle.Top
        pnlSpacer1.Location = New Point(0, 312)
        pnlSpacer1.Name = "pnlSpacer1"
        pnlSpacer1.Size = New Size(275, 15)
        pnlSpacer1.TabIndex = 6
        ' 
        ' btnNavPomodoro
        ' 
        btnNavPomodoro.Dock = DockStyle.Top
        btnNavPomodoro.FlatAppearance.BorderSize = 0
        btnNavPomodoro.FlatStyle = FlatStyle.Flat
        btnNavPomodoro.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavPomodoro.ForeColor = Color.White
        btnNavPomodoro.Location = New Point(0, 252)
        btnNavPomodoro.Name = "btnNavPomodoro"
        btnNavPomodoro.Padding = New Padding(20, 0, 0, 0)
        btnNavPomodoro.Size = New Size(275, 60)
        btnNavPomodoro.TabIndex = 11
        btnNavPomodoro.Text = "🍅 Pomodoro"
        btnNavPomodoro.TextAlign = ContentAlignment.MiddleLeft
        btnNavPomodoro.UseVisualStyleBackColor = True
        ' 
        ' btnNavAnalytics
        ' 
        btnNavAnalytics.Dock = DockStyle.Top
        btnNavAnalytics.FlatAppearance.BorderSize = 0
        btnNavAnalytics.FlatStyle = FlatStyle.Flat
        btnNavAnalytics.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavAnalytics.ForeColor = Color.White
        btnNavAnalytics.Location = New Point(0, 192)
        btnNavAnalytics.Name = "btnNavAnalytics"
        btnNavAnalytics.Padding = New Padding(20, 0, 0, 0)
        btnNavAnalytics.Size = New Size(275, 60)
        btnNavAnalytics.TabIndex = 12
        btnNavAnalytics.Text = "📊 Analytics"
        btnNavAnalytics.TextAlign = ContentAlignment.MiddleLeft
        btnNavAnalytics.UseVisualStyleBackColor = True
        ' 
        ' btnNavProfile
        ' 
        btnNavProfile.Dock = DockStyle.Top
        btnNavProfile.FlatAppearance.BorderSize = 0
        btnNavProfile.FlatStyle = FlatStyle.Flat
        btnNavProfile.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavProfile.ForeColor = Color.White
        btnNavProfile.Location = New Point(0, 72)
        btnNavProfile.Name = "btnNavProfile"
        btnNavProfile.Padding = New Padding(20, 0, 0, 0)
        btnNavProfile.Size = New Size(275, 60)
        btnNavProfile.TabIndex = 14
        btnNavProfile.Text = "👤 My Profile"
        btnNavProfile.TextAlign = ContentAlignment.MiddleLeft
        btnNavProfile.UseVisualStyleBackColor = True
        ' 
        ' lblAppTitle
        ' 
        lblAppTitle.Dock = DockStyle.Top
        lblAppTitle.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        lblAppTitle.ForeColor = Color.White
        lblAppTitle.Location = New Point(0, 0)
        lblAppTitle.Name = "lblAppTitle"
        lblAppTitle.Size = New Size(275, 72)
        lblAppTitle.TabIndex = 0
        lblAppTitle.Text = "Task Manager"
        lblAppTitle.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlTitleBar
        ' 
        pnlTitleBar.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(btnMaximize)
        pnlTitleBar.Controls.Add(btnMinimize)
        pnlTitleBar.Controls.Add(lblWelcome)
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Location = New Point(275, 0)
        pnlTitleBar.Name = "pnlTitleBar"
        pnlTitleBar.Size = New Size(1005, 50)
        pnlTitleBar.TabIndex = 1
        ' 
        ' btnClose
        ' 
        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(855, 0)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(50, 50)
        btnClose.TabIndex = 2
        btnClose.Text = "✕"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' btnMaximize
        ' 
        btnMaximize.Dock = DockStyle.Right
        btnMaximize.FlatAppearance.BorderSize = 0
        btnMaximize.FlatStyle = FlatStyle.Flat
        btnMaximize.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnMaximize.ForeColor = Color.White
        btnMaximize.Location = New Point(905, 0)
        btnMaximize.Name = "btnMaximize"
        btnMaximize.Size = New Size(50, 50)
        btnMaximize.TabIndex = 3
        btnMaximize.Text = "□"
        btnMaximize.UseVisualStyleBackColor = True
        ' 
        ' btnMinimize
        ' 
        btnMinimize.Dock = DockStyle.Right
        btnMinimize.FlatAppearance.BorderSize = 0
        btnMinimize.FlatStyle = FlatStyle.Flat
        btnMinimize.Font = New Font("Segoe UI", 14.0F, FontStyle.Bold)
        btnMinimize.ForeColor = Color.White
        btnMinimize.Location = New Point(955, 0)
        btnMinimize.Name = "btnMinimize"
        btnMinimize.Size = New Size(50, 50)
        btnMinimize.TabIndex = 1
        btnMinimize.Text = "-"
        btnMinimize.UseVisualStyleBackColor = True
        ' 
        ' lblWelcome
        ' 
        lblWelcome.AutoSize = True
        lblWelcome.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        lblWelcome.ForeColor = Color.White
        lblWelcome.Location = New Point(20, 9)
        lblWelcome.Name = "lblWelcome"
        lblWelcome.Size = New Size(121, 32)
        lblWelcome.TabIndex = 0
        lblWelcome.Text = "Welcome!"
        ' 
        ' pnlMainContent
        ' 
        pnlMainContent.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        pnlMainContent.Controls.Add(pnlDashboardView)
        pnlMainContent.Controls.Add(pnlManageTasksView)
        pnlMainContent.Controls.Add(pnlCalendarView)
        pnlMainContent.Dock = DockStyle.Fill
        pnlMainContent.Location = New Point(275, 50)
        pnlMainContent.Name = "pnlMainContent"
        pnlMainContent.Size = New Size(1005, 700)
        pnlMainContent.TabIndex = 2
        ' 
        ' pnlDashboardView
        ' 
        pnlDashboardView.Controls.Add(dgvRecentTasks)
        pnlDashboardView.Controls.Add(pnlEmptyRecent)
        pnlDashboardView.Controls.Add(lblRecentActivity)
        pnlDashboardView.Controls.Add(pnlProgressRow)
        pnlDashboardView.Controls.Add(tlpCards)
        pnlDashboardView.Dock = DockStyle.Fill
        pnlDashboardView.Location = New Point(0, 0)
        pnlDashboardView.Name = "pnlDashboardView"
        pnlDashboardView.Size = New Size(1005, 700)
        pnlDashboardView.TabIndex = 0
        ' 
        ' dgvRecentTasks
        ' 
        dgvRecentTasks.AllowUserToAddRows = False
        dgvRecentTasks.AllowUserToDeleteRows = False
        dgvRecentTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvRecentTasks.Dock = DockStyle.Fill
        dgvRecentTasks.Location = New Point(0, 270)
        dgvRecentTasks.Name = "dgvRecentTasks"
        dgvRecentTasks.ReadOnly = True
        dgvRecentTasks.RowHeadersWidth = 62
        dgvRecentTasks.Size = New Size(1005, 430)
        dgvRecentTasks.TabIndex = 2
        ' 
        ' pnlEmptyRecent
        ' 
        pnlEmptyRecent.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlEmptyRecent.Controls.Add(lblEmptyRecent)
        pnlEmptyRecent.Location = New Point(30, 290)
        pnlEmptyRecent.Name = "pnlEmptyRecent"
        pnlEmptyRecent.Size = New Size(945, 380)
        pnlEmptyRecent.TabIndex = 3
        pnlEmptyRecent.Visible = False
        ' 
        ' lblEmptyRecent
        ' 
        lblEmptyRecent.Dock = DockStyle.Fill
        lblEmptyRecent.Font = New Font("Segoe UI", 13.0F)
        lblEmptyRecent.Location = New Point(0, 0)
        lblEmptyRecent.Name = "lblEmptyRecent"
        lblEmptyRecent.Size = New Size(945, 380)
        lblEmptyRecent.TabIndex = 0
        lblEmptyRecent.Text = "You have no upcoming tasks."
        lblEmptyRecent.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblRecentActivity
        ' 
        lblRecentActivity.Dock = DockStyle.Top
        lblRecentActivity.Font = New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold)
        lblRecentActivity.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblRecentActivity.Location = New Point(0, 226)
        lblRecentActivity.Name = "lblRecentActivity"
        lblRecentActivity.Padding = New Padding(16, 0, 0, 0)
        lblRecentActivity.Size = New Size(1005, 44)
        lblRecentActivity.TabIndex = 1
        lblRecentActivity.Text = "📋 Tasks"
        lblRecentActivity.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' pnlProgressRow
        ' 
        pnlProgressRow.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        pnlProgressRow.Controls.Add(lblProgressPct)
        pnlProgressRow.Controls.Add(pbCompletion)
        pnlProgressRow.Controls.Add(lblProgressTitle)
        pnlProgressRow.Dock = DockStyle.Top
        pnlProgressRow.Location = New Point(0, 190)
        pnlProgressRow.Name = "pnlProgressRow"
        pnlProgressRow.Padding = New Padding(16, 6, 16, 6)
        pnlProgressRow.Size = New Size(1005, 36)
        pnlProgressRow.TabIndex = 4
        ' 
        ' lblProgressPct
        ' 
        lblProgressPct.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblProgressPct.AutoSize = True
        lblProgressPct.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblProgressPct.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        lblProgressPct.Location = New Point(1725, 207)
        lblProgressPct.Name = "lblProgressPct"
        lblProgressPct.Size = New Size(39, 25)
        lblProgressPct.TabIndex = 5
        lblProgressPct.Text = "0%"
        ' 
        ' pbCompletion
        ' 
        pbCompletion.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        pbCompletion.Location = New Point(130, 204)
        pbCompletion.Name = "pbCompletion"
        pbCompletion.Size = New Size(1585, 22)
        pbCompletion.Style = ProgressBarStyle.Continuous
        pbCompletion.TabIndex = 4
        ' 
        ' lblProgressTitle
        ' 
        lblProgressTitle.AutoSize = True
        lblProgressTitle.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblProgressTitle.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblProgressTitle.Location = New Point(30, 207)
        lblProgressTitle.Name = "lblProgressTitle"
        lblProgressTitle.Size = New Size(117, 25)
        lblProgressTitle.TabIndex = 3
        lblProgressTitle.Text = "Completion:"
        ' 
        ' tlpCards
        ' 
        tlpCards.ColumnCount = 4
        tlpCards.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlpCards.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlpCards.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlpCards.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 25.0F))
        tlpCards.Controls.Add(pnlCardOverdue, 3, 0)
        tlpCards.Controls.Add(pnlCardCompleted, 2, 0)
        tlpCards.Controls.Add(pnlCardPending, 1, 0)
        tlpCards.Controls.Add(pnlCardTotal, 0, 0)
        tlpCards.Dock = DockStyle.Top
        tlpCards.Location = New Point(0, 0)
        tlpCards.Name = "tlpCards"
        tlpCards.RowCount = 1
        tlpCards.RowStyles.Add(New RowStyle(SizeType.Percent, 100.0F))
        tlpCards.Size = New Size(1005, 190)
        tlpCards.TabIndex = 0
        ' 
        ' pnlCardOverdue
        ' 
        pnlCardOverdue.BackColor = Color.White
        pnlCardOverdue.Controls.Add(lblCountOverdue)
        pnlCardOverdue.Controls.Add(lblTitleOverdue)
        pnlCardOverdue.Dock = DockStyle.Fill
        pnlCardOverdue.Location = New Point(773, 20)
        pnlCardOverdue.Margin = New Padding(20)
        pnlCardOverdue.Name = "pnlCardOverdue"
        pnlCardOverdue.Size = New Size(212, 150)
        pnlCardOverdue.TabIndex = 3
        ' 
        ' lblCountOverdue
        ' 
        lblCountOverdue.Dock = DockStyle.Fill
        lblCountOverdue.Font = New Font("Segoe UI", 28.0F, FontStyle.Bold)
        lblCountOverdue.ForeColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        lblCountOverdue.Location = New Point(0, 40)
        lblCountOverdue.Name = "lblCountOverdue"
        lblCountOverdue.Size = New Size(212, 110)
        lblCountOverdue.TabIndex = 1
        lblCountOverdue.Text = "0"
        lblCountOverdue.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTitleOverdue
        ' 
        lblTitleOverdue.Dock = DockStyle.Top
        lblTitleOverdue.Font = New Font("Segoe UI", 10.0F)
        lblTitleOverdue.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblTitleOverdue.Location = New Point(0, 0)
        lblTitleOverdue.Name = "lblTitleOverdue"
        lblTitleOverdue.Size = New Size(212, 40)
        lblTitleOverdue.TabIndex = 0
        lblTitleOverdue.Text = "⚠️ Overdue"
        lblTitleOverdue.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlCardCompleted
        ' 
        pnlCardCompleted.BackColor = Color.White
        pnlCardCompleted.Controls.Add(lblCountCompleted)
        pnlCardCompleted.Controls.Add(lblTitleCompleted)
        pnlCardCompleted.Dock = DockStyle.Fill
        pnlCardCompleted.Location = New Point(522, 20)
        pnlCardCompleted.Margin = New Padding(20)
        pnlCardCompleted.Name = "pnlCardCompleted"
        pnlCardCompleted.Size = New Size(211, 150)
        pnlCardCompleted.TabIndex = 2
        ' 
        ' lblCountCompleted
        ' 
        lblCountCompleted.Dock = DockStyle.Fill
        lblCountCompleted.Font = New Font("Segoe UI", 28.0F, FontStyle.Bold)
        lblCountCompleted.ForeColor = Color.FromArgb(CByte(16), CByte(185), CByte(129))
        lblCountCompleted.Location = New Point(0, 40)
        lblCountCompleted.Name = "lblCountCompleted"
        lblCountCompleted.Size = New Size(211, 110)
        lblCountCompleted.TabIndex = 1
        lblCountCompleted.Text = "0"
        lblCountCompleted.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTitleCompleted
        ' 
        lblTitleCompleted.Dock = DockStyle.Top
        lblTitleCompleted.Font = New Font("Segoe UI", 10.0F)
        lblTitleCompleted.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblTitleCompleted.Location = New Point(0, 0)
        lblTitleCompleted.Name = "lblTitleCompleted"
        lblTitleCompleted.Size = New Size(211, 40)
        lblTitleCompleted.TabIndex = 0
        lblTitleCompleted.Text = "🎉 Completed"
        lblTitleCompleted.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlCardPending
        ' 
        pnlCardPending.BackColor = Color.White
        pnlCardPending.Controls.Add(lblCountPending)
        pnlCardPending.Controls.Add(lblTitlePending)
        pnlCardPending.Dock = DockStyle.Fill
        pnlCardPending.Location = New Point(271, 20)
        pnlCardPending.Margin = New Padding(20)
        pnlCardPending.Name = "pnlCardPending"
        pnlCardPending.Size = New Size(211, 150)
        pnlCardPending.TabIndex = 1
        ' 
        ' lblCountPending
        ' 
        lblCountPending.Dock = DockStyle.Fill
        lblCountPending.Font = New Font("Segoe UI", 28.0F, FontStyle.Bold)
        lblCountPending.ForeColor = Color.FromArgb(CByte(245), CByte(158), CByte(11))
        lblCountPending.Location = New Point(0, 40)
        lblCountPending.Name = "lblCountPending"
        lblCountPending.Size = New Size(211, 110)
        lblCountPending.TabIndex = 1
        lblCountPending.Text = "0"
        lblCountPending.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTitlePending
        ' 
        lblTitlePending.Dock = DockStyle.Top
        lblTitlePending.Font = New Font("Segoe UI", 10.0F)
        lblTitlePending.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblTitlePending.Location = New Point(0, 0)
        lblTitlePending.Name = "lblTitlePending"
        lblTitlePending.Size = New Size(211, 40)
        lblTitlePending.TabIndex = 0
        lblTitlePending.Text = "⏳ Pending"
        lblTitlePending.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlCardTotal
        ' 
        pnlCardTotal.BackColor = Color.White
        pnlCardTotal.Controls.Add(lblCountTotal)
        pnlCardTotal.Controls.Add(lblTitleTotal)
        pnlCardTotal.Dock = DockStyle.Fill
        pnlCardTotal.Location = New Point(20, 20)
        pnlCardTotal.Margin = New Padding(20)
        pnlCardTotal.Name = "pnlCardTotal"
        pnlCardTotal.Size = New Size(211, 150)
        pnlCardTotal.TabIndex = 0
        ' 
        ' lblCountTotal
        ' 
        lblCountTotal.Dock = DockStyle.Fill
        lblCountTotal.Font = New Font("Segoe UI", 28.0F, FontStyle.Bold)
        lblCountTotal.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        lblCountTotal.Location = New Point(0, 40)
        lblCountTotal.Name = "lblCountTotal"
        lblCountTotal.Size = New Size(211, 110)
        lblCountTotal.TabIndex = 1
        lblCountTotal.Text = "0"
        lblCountTotal.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblTitleTotal
        ' 
        lblTitleTotal.Dock = DockStyle.Top
        lblTitleTotal.Font = New Font("Segoe UI", 10.0F)
        lblTitleTotal.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblTitleTotal.Location = New Point(0, 0)
        lblTitleTotal.Name = "lblTitleTotal"
        lblTitleTotal.Size = New Size(211, 40)
        lblTitleTotal.TabIndex = 0
        lblTitleTotal.Text = "📝 Total Tasks"
        lblTitleTotal.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlManageTasksView
        ' 
        pnlManageTasksView.Controls.Add(flpActionBar)
        pnlManageTasksView.Controls.Add(dgvTasks)
        pnlManageTasksView.Controls.Add(pnlEmptyTasks)
        pnlManageTasksView.Controls.Add(pnlDivider)
        pnlManageTasksView.Controls.Add(pnlToolbar)
        pnlManageTasksView.Controls.Add(pnlDividerTop)
        pnlManageTasksView.Dock = DockStyle.Fill
        pnlManageTasksView.Location = New Point(0, 0)
        pnlManageTasksView.Name = "pnlManageTasksView"
        pnlManageTasksView.Size = New Size(1005, 700)
        pnlManageTasksView.TabIndex = 1
        pnlManageTasksView.Visible = False
        ' 
        ' flpActionBar
        ' 
        flpActionBar.BackColor = Color.White
        flpActionBar.Controls.Add(btnAddTask)
        flpActionBar.Controls.Add(btnEditTask)
        flpActionBar.Controls.Add(btnDeleteTask)
        flpActionBar.Controls.Add(btnViewDetails)
        flpActionBar.Controls.Add(btnToggleStatus)
        flpActionBar.Controls.Add(btnDuplicateTask)
        flpActionBar.Controls.Add(btnPrintTasks)
        flpActionBar.Dock = DockStyle.Bottom
        flpActionBar.Location = New Point(0, 642)
        flpActionBar.Name = "flpActionBar"
        flpActionBar.Padding = New Padding(12, 8, 12, 8)
        flpActionBar.Size = New Size(1005, 58)
        flpActionBar.TabIndex = 0
        flpActionBar.WrapContents = False
        ' 
        ' btnAddTask
        ' 
        btnAddTask.Location = New Point(12, 8)
        btnAddTask.Margin = New Padding(0, 0, 8, 0)
        btnAddTask.Name = "btnAddTask"
        btnAddTask.Size = New Size(130, 42)
        btnAddTask.TabIndex = 9
        ' 
        ' btnEditTask
        ' 
        btnEditTask.Location = New Point(150, 8)
        btnEditTask.Margin = New Padding(0, 0, 8, 0)
        btnEditTask.Name = "btnEditTask"
        btnEditTask.Size = New Size(100, 42)
        btnEditTask.TabIndex = 14
        ' 
        ' btnDeleteTask
        ' 
        btnDeleteTask.Location = New Point(258, 8)
        btnDeleteTask.Margin = New Padding(0, 0, 8, 0)
        btnDeleteTask.Name = "btnDeleteTask"
        btnDeleteTask.Size = New Size(110, 42)
        btnDeleteTask.TabIndex = 13
        ' 
        ' btnViewDetails
        ' 
        btnViewDetails.Location = New Point(376, 8)
        btnViewDetails.Margin = New Padding(0, 0, 8, 0)
        btnViewDetails.Name = "btnViewDetails"
        btnViewDetails.Size = New Size(110, 42)
        btnViewDetails.TabIndex = 12
        ' 
        ' btnToggleStatus
        ' 
        btnToggleStatus.Location = New Point(494, 8)
        btnToggleStatus.Margin = New Padding(0, 0, 8, 0)
        btnToggleStatus.Name = "btnToggleStatus"
        btnToggleStatus.Size = New Size(110, 42)
        btnToggleStatus.TabIndex = 11
        ' 
        ' btnDuplicateTask
        ' 
        btnDuplicateTask.Location = New Point(612, 8)
        btnDuplicateTask.Margin = New Padding(0, 0, 8, 0)
        btnDuplicateTask.Name = "btnDuplicateTask"
        btnDuplicateTask.Size = New Size(130, 42)
        btnDuplicateTask.TabIndex = 16
        ' 
        ' btnPrintTasks
        ' 
        btnPrintTasks.Location = New Point(750, 8)
        btnPrintTasks.Margin = New Padding(0)
        btnPrintTasks.Name = "btnPrintTasks"
        btnPrintTasks.Size = New Size(100, 42)
        btnPrintTasks.TabIndex = 15
        ' 
        ' dgvTasks
        ' 
        dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvTasks.Dock = DockStyle.Fill
        dgvTasks.Location = New Point(0, 162)
        dgvTasks.Name = "dgvTasks"
        dgvTasks.RowHeadersWidth = 62
        dgvTasks.Size = New Size(1005, 538)
        dgvTasks.TabIndex = 8
        ' 
        ' pnlEmptyTasks
        ' 
        pnlEmptyTasks.Controls.Add(lblEmptyTasks)
        pnlEmptyTasks.Dock = DockStyle.Fill
        pnlEmptyTasks.Location = New Point(0, 162)
        pnlEmptyTasks.Name = "pnlEmptyTasks"
        pnlEmptyTasks.Size = New Size(1005, 538)
        pnlEmptyTasks.TabIndex = 9
        pnlEmptyTasks.Visible = False
        ' 
        ' lblEmptyTasks
        ' 
        lblEmptyTasks.Dock = DockStyle.Fill
        lblEmptyTasks.Font = New Font("Segoe UI", 13.0F)
        lblEmptyTasks.Location = New Point(0, 0)
        lblEmptyTasks.Name = "lblEmptyTasks"
        lblEmptyTasks.Size = New Size(1005, 538)
        lblEmptyTasks.TabIndex = 0
        lblEmptyTasks.Text = "No tasks yet — click ＋ Add Task to get started."
        lblEmptyTasks.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' pnlDivider
        ' 
        pnlDivider.BackColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        pnlDivider.Dock = DockStyle.Top
        pnlDivider.Location = New Point(0, 161)
        pnlDivider.Name = "pnlDivider"
        pnlDivider.Size = New Size(1005, 1)
        pnlDivider.TabIndex = 10
        ' 
        ' pnlToolbar
        ' 
        pnlToolbar.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        pnlToolbar.Controls.Add(lblManageTitle)
        pnlToolbar.Controls.Add(lblTaskCount)
        pnlToolbar.Controls.Add(txtSearch)
        pnlToolbar.Controls.Add(btnSearchClear)
        pnlToolbar.Controls.Add(btnFilterAll)
        pnlToolbar.Controls.Add(btnFilterPending)
        pnlToolbar.Controls.Add(btnFilterCompleted)
        pnlToolbar.Controls.Add(lblFilterDate)
        pnlToolbar.Controls.Add(dtpFilterFrom)
        pnlToolbar.Controls.Add(lblFilterTo)
        pnlToolbar.Controls.Add(dtpFilterTo)
        pnlToolbar.Controls.Add(lblFilterPriority)
        pnlToolbar.Controls.Add(cmbFilterPriority)
        pnlToolbar.Controls.Add(btnApplyAdvancedFilter)
        pnlToolbar.Dock = DockStyle.Top
        pnlToolbar.Location = New Point(0, 1)
        pnlToolbar.Name = "pnlToolbar"
        pnlToolbar.Padding = New Padding(16, 8, 16, 8)
        pnlToolbar.Size = New Size(1005, 160)
        pnlToolbar.TabIndex = 11
        ' 
        ' lblManageTitle
        ' 
        lblManageTitle.AutoSize = True
        lblManageTitle.Font = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold)
        lblManageTitle.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblManageTitle.Location = New Point(0, 8)
        lblManageTitle.Name = "lblManageTitle"
        lblManageTitle.Size = New Size(231, 36)
        lblManageTitle.TabIndex = 0
        lblManageTitle.Text = "📋  Manage Tasks"
        ' 
        ' lblTaskCount
        ' 
        lblTaskCount.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        lblTaskCount.AutoSize = True
        lblTaskCount.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblTaskCount.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        lblTaskCount.Location = New Point(1625, 12)
        lblTaskCount.Name = "lblTaskCount"
        lblTaskCount.Size = New Size(129, 28)
        lblTaskCount.TabIndex = 1
        lblTaskCount.Text = "Total: 0 tasks"
        ' 
        ' txtSearch
        ' 
        txtSearch.BorderStyle = BorderStyle.FixedSingle
        txtSearch.Font = New Font("Segoe UI", 11.0F)
        txtSearch.Location = New Point(0, 46)
        txtSearch.MaxLength = 200
        txtSearch.Name = "txtSearch"
        txtSearch.Size = New Size(360, 37)
        txtSearch.TabIndex = 2
        ' 
        ' btnSearchClear
        ' 
        btnSearchClear.Cursor = Cursors.Hand
        btnSearchClear.FlatAppearance.BorderSize = 0
        btnSearchClear.FlatStyle = FlatStyle.Flat
        btnSearchClear.Font = New Font("Segoe UI", 10.0F, FontStyle.Bold)
        btnSearchClear.Location = New Point(364, 46)
        btnSearchClear.Name = "btnSearchClear"
        btnSearchClear.Size = New Size(36, 38)
        btnSearchClear.TabIndex = 3
        btnSearchClear.Text = "✕"
        btnSearchClear.Visible = False
        ' 
        ' btnFilterAll
        ' 
        btnFilterAll.Cursor = Cursors.Hand
        btnFilterAll.FlatStyle = FlatStyle.Flat
        btnFilterAll.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btnFilterAll.Location = New Point(412, 46)
        btnFilterAll.Name = "btnFilterAll"
        btnFilterAll.Size = New Size(76, 38)
        btnFilterAll.TabIndex = 4
        btnFilterAll.Text = "All"
        ' 
        ' btnFilterPending
        ' 
        btnFilterPending.Cursor = Cursors.Hand
        btnFilterPending.FlatStyle = FlatStyle.Flat
        btnFilterPending.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btnFilterPending.Location = New Point(496, 46)
        btnFilterPending.Name = "btnFilterPending"
        btnFilterPending.Size = New Size(100, 38)
        btnFilterPending.TabIndex = 5
        btnFilterPending.Text = "⏳ Pending"
        ' 
        ' btnFilterCompleted
        ' 
        btnFilterCompleted.Cursor = Cursors.Hand
        btnFilterCompleted.FlatStyle = FlatStyle.Flat
        btnFilterCompleted.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btnFilterCompleted.Location = New Point(604, 46)
        btnFilterCompleted.Name = "btnFilterCompleted"
        btnFilterCompleted.Size = New Size(110, 38)
        btnFilterCompleted.TabIndex = 6
        btnFilterCompleted.Text = "✅ Completed"
        ' 
        ' lblFilterDate
        ' 
        lblFilterDate.AutoSize = True
        lblFilterDate.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblFilterDate.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblFilterDate.Location = New Point(0, 108)
        lblFilterDate.Name = "lblFilterDate"
        lblFilterDate.Size = New Size(57, 25)
        lblFilterDate.TabIndex = 7
        lblFilterDate.Text = "From"
        ' 
        ' dtpFilterFrom
        ' 
        dtpFilterFrom.Font = New Font("Segoe UI", 10.0F)
        dtpFilterFrom.Format = DateTimePickerFormat.Short
        dtpFilterFrom.Location = New Point(46, 102)
        dtpFilterFrom.Name = "dtpFilterFrom"
        dtpFilterFrom.Size = New Size(130, 34)
        dtpFilterFrom.TabIndex = 8
        ' 
        ' lblFilterTo
        ' 
        lblFilterTo.AutoSize = True
        lblFilterTo.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblFilterTo.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblFilterTo.Location = New Point(184, 108)
        lblFilterTo.Name = "lblFilterTo"
        lblFilterTo.Size = New Size(31, 25)
        lblFilterTo.TabIndex = 9
        lblFilterTo.Text = "To"
        ' 
        ' dtpFilterTo
        ' 
        dtpFilterTo.Font = New Font("Segoe UI", 10.0F)
        dtpFilterTo.Format = DateTimePickerFormat.Short
        dtpFilterTo.Location = New Point(206, 102)
        dtpFilterTo.Name = "dtpFilterTo"
        dtpFilterTo.Size = New Size(130, 34)
        dtpFilterTo.TabIndex = 10
        ' 
        ' lblFilterPriority
        ' 
        lblFilterPriority.AutoSize = True
        lblFilterPriority.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblFilterPriority.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblFilterPriority.Location = New Point(348, 108)
        lblFilterPriority.Name = "lblFilterPriority"
        lblFilterPriority.Size = New Size(75, 25)
        lblFilterPriority.TabIndex = 11
        lblFilterPriority.Text = "Priority"
        ' 
        ' cmbFilterPriority
        ' 
        cmbFilterPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cmbFilterPriority.FlatStyle = FlatStyle.Flat
        cmbFilterPriority.Font = New Font("Segoe UI", 10.0F)
        cmbFilterPriority.Items.AddRange(New Object() {"All", "High", "Medium", "Low"})
        cmbFilterPriority.Location = New Point(402, 102)
        cmbFilterPriority.Name = "cmbFilterPriority"
        cmbFilterPriority.Size = New Size(110, 36)
        cmbFilterPriority.TabIndex = 12
        ' 
        ' btnApplyAdvancedFilter
        ' 
        btnApplyAdvancedFilter.Cursor = Cursors.Hand
        btnApplyAdvancedFilter.FlatAppearance.BorderSize = 0
        btnApplyAdvancedFilter.FlatStyle = FlatStyle.Flat
        btnApplyAdvancedFilter.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        btnApplyAdvancedFilter.Location = New Point(522, 102)
        btnApplyAdvancedFilter.Name = "btnApplyAdvancedFilter"
        btnApplyAdvancedFilter.Size = New Size(100, 34)
        btnApplyAdvancedFilter.TabIndex = 13
        btnApplyAdvancedFilter.Text = "⚡ Apply"
        ' 
        ' pnlDividerTop
        ' 
        pnlDividerTop.BackColor = Color.FromArgb(CByte(229), CByte(231), CByte(235))
        pnlDividerTop.Dock = DockStyle.Top
        pnlDividerTop.Location = New Point(0, 0)
        pnlDividerTop.Name = "pnlDividerTop"
        pnlDividerTop.Size = New Size(1005, 1)
        pnlDividerTop.TabIndex = 12
        ' 
        ' pnlCalendarView
        ' 
        pnlCalendarView.Controls.Add(dgvCalendarTasks)
        pnlCalendarView.Controls.Add(pnlEmptyCalendar)
        pnlCalendarView.Controls.Add(calTasks)
        pnlCalendarView.Controls.Add(lblCalendarDate)
        pnlCalendarView.Dock = DockStyle.Fill
        pnlCalendarView.Location = New Point(0, 0)
        pnlCalendarView.Name = "pnlCalendarView"
        pnlCalendarView.Size = New Size(1005, 700)
        pnlCalendarView.TabIndex = 2
        pnlCalendarView.Visible = False
        ' 
        ' dgvCalendarTasks
        ' 
        dgvCalendarTasks.AllowUserToAddRows = False
        dgvCalendarTasks.AllowUserToDeleteRows = False
        dgvCalendarTasks.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        dgvCalendarTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvCalendarTasks.Location = New Point(380, 80)
        dgvCalendarTasks.Name = "dgvCalendarTasks"
        dgvCalendarTasks.ReadOnly = True
        dgvCalendarTasks.RowHeadersWidth = 62
        dgvCalendarTasks.Size = New Size(595, 590)
        dgvCalendarTasks.TabIndex = 2
        ' 
        ' pnlEmptyCalendar
        ' 
        pnlEmptyCalendar.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        pnlEmptyCalendar.Controls.Add(lblEmptyCalendar)
        pnlEmptyCalendar.Location = New Point(380, 80)
        pnlEmptyCalendar.Name = "pnlEmptyCalendar"
        pnlEmptyCalendar.Size = New Size(595, 590)
        pnlEmptyCalendar.TabIndex = 3
        pnlEmptyCalendar.Visible = False
        ' 
        ' lblEmptyCalendar
        ' 
        lblEmptyCalendar.Dock = DockStyle.Fill
        lblEmptyCalendar.Font = New Font("Segoe UI", 13.0F)
        lblEmptyCalendar.Location = New Point(0, 0)
        lblEmptyCalendar.Name = "lblEmptyCalendar"
        lblEmptyCalendar.Size = New Size(595, 590)
        lblEmptyCalendar.TabIndex = 0
        lblEmptyCalendar.Text = "No tasks due on this day."
        lblEmptyCalendar.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' calTasks
        ' 
        calTasks.Location = New Point(30, 30)
        calTasks.MaxSelectionCount = 1
        calTasks.Name = "calTasks"
        calTasks.TabIndex = 0
        ' 
        ' lblCalendarDate
        ' 
        lblCalendarDate.AutoSize = True
        lblCalendarDate.Font = New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold)
        lblCalendarDate.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblCalendarDate.Location = New Point(380, 30)
        lblCalendarDate.Name = "lblCalendarDate"
        lblCalendarDate.Size = New Size(196, 38)
        lblCalendarDate.TabIndex = 1
        lblCalendarDate.Text = "Tasks for Date"
        ' 
        ' btnNavDashboard
        ' 
        btnNavDashboard.Dock = DockStyle.Top
        btnNavDashboard.FlatAppearance.BorderSize = 0
        btnNavDashboard.FlatStyle = FlatStyle.Flat
        btnNavDashboard.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnNavDashboard.ForeColor = Color.White
        btnNavDashboard.Location = New Point(0, 549)
        btnNavDashboard.Name = "btnNavDashboard"
        btnNavDashboard.Padding = New Padding(20, 0, 0, 0)
        btnNavDashboard.Size = New Size(275, 60)
        btnNavDashboard.TabIndex = 16
        btnNavDashboard.Text = "🏠 Dashboard"
        btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft
        btnNavDashboard.UseVisualStyleBackColor = True
        ' 
        ' frmDashboard
        ' 
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1280, 750)
        Controls.Add(pnlMainContent)
        Controls.Add(pnlTitleBar)
        Controls.Add(pnlSidebar)
        FormBorderStyle = FormBorderStyle.None
        Name = "frmDashboard"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Smart Student Task Manager"
        WindowState = FormWindowState.Maximized
        pnlSidebar.ResumeLayout(False)
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlMainContent.ResumeLayout(False)
        pnlDashboardView.ResumeLayout(False)
        CType(dgvRecentTasks, ComponentModel.ISupportInitialize).EndInit()
        pnlEmptyRecent.ResumeLayout(False)
        pnlProgressRow.ResumeLayout(False)
        pnlProgressRow.PerformLayout()
        tlpCards.ResumeLayout(False)
        pnlCardOverdue.ResumeLayout(False)
        pnlCardCompleted.ResumeLayout(False)
        pnlCardPending.ResumeLayout(False)
        pnlCardTotal.ResumeLayout(False)
        pnlManageTasksView.ResumeLayout(False)
        flpActionBar.ResumeLayout(False)
        CType(dgvTasks, ComponentModel.ISupportInitialize).EndInit()
        pnlEmptyTasks.ResumeLayout(False)
        pnlToolbar.ResumeLayout(False)
        pnlToolbar.PerformLayout()
        pnlCalendarView.ResumeLayout(False)
        pnlCalendarView.PerformLayout()
        CType(dgvCalendarTasks, ComponentModel.ISupportInitialize).EndInit()
        pnlEmptyCalendar.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlSidebar As Panel
    Friend WithEvents lblAppTitle As Label
    Friend WithEvents btnLogout As Button
    Friend WithEvents pnlSpacer3 As Panel
    Friend WithEvents btnNavCalendar As Button
    Friend WithEvents pnlSpacer2 As Panel
    Friend WithEvents btnNavManageTasks As Button
    Friend WithEvents pnlSpacer1 As Panel
    Friend WithEvents pnlTitleBar As Panel
    Friend WithEvents btnClose As Button
    Friend WithEvents btnMaximize As Button
    Friend WithEvents btnMinimize As Button
    Friend WithEvents lblWelcome As Label
    Friend WithEvents pnlMainContent As Panel
    Friend WithEvents pnlDashboardView As Panel
    Friend WithEvents tlpCards As TableLayoutPanel
    Friend WithEvents pnlCardTotal As Panel
    Friend WithEvents lblCountTotal As Label
    Friend WithEvents lblTitleTotal As Label
    Friend WithEvents pnlCardOverdue As Panel
    Friend WithEvents lblCountOverdue As Label
    Friend WithEvents lblTitleOverdue As Label
    Friend WithEvents pnlCardCompleted As Panel
    Friend WithEvents lblCountCompleted As Label
    Friend WithEvents lblTitleCompleted As Label
    Friend WithEvents pnlCardPending As Panel
    Friend WithEvents lblCountPending As Label
    Friend WithEvents lblTitlePending As Label
    Friend WithEvents dgvRecentTasks As DataGridView
    Friend WithEvents lblRecentActivity As Label
    Friend WithEvents pnlManageTasksView As Panel
    Friend WithEvents lblManageTitle As Label
    Friend WithEvents lblTaskCount As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents btnSearchClear As Button
    Friend WithEvents btnFilterAll As Button
    Friend WithEvents btnFilterPending As Button
    Friend WithEvents btnFilterCompleted As Button
    Friend WithEvents lblFilterDate As Label
    Friend WithEvents dtpFilterFrom As DateTimePicker
    Friend WithEvents lblFilterTo As Label
    Friend WithEvents dtpFilterTo As DateTimePicker
    Friend WithEvents lblFilterPriority As Label
    Friend WithEvents cmbFilterPriority As ComboBox
    Friend WithEvents btnApplyAdvancedFilter As Button
    Friend WithEvents pnlDivider As Panel
    Friend WithEvents pnlDividerTop As Panel
    Friend WithEvents pnlToolbar As Panel
    Friend WithEvents dgvTasks As DataGridView
    Friend WithEvents pnlEmptyTasks As Panel
    Friend WithEvents lblEmptyTasks As Label
    Friend WithEvents flpActionBar As FlowLayoutPanel
    Friend WithEvents btnAddTask As Button
    Friend WithEvents btnToggleStatus As Button
    Friend WithEvents btnViewDetails As Button
    Friend WithEvents btnDeleteTask As Button
    Friend WithEvents btnEditTask As Button
    Friend WithEvents btnDuplicateTask As Button
    Friend WithEvents btnPrintTasks As Button

    Friend WithEvents pnlCalendarView As Panel
    Friend WithEvents calTasks As MonthCalendar
    Friend WithEvents lblCalendarDate As Label
    Friend WithEvents dgvCalendarTasks As DataGridView
    Friend WithEvents pnlEmptyRecent As Panel
    Friend WithEvents lblEmptyRecent As Label
    Friend WithEvents pnlEmptyCalendar As Panel
    Friend WithEvents lblEmptyCalendar As Label
    Friend WithEvents btnDarkMode As Button
    Friend WithEvents lblProgressTitle As Label
    Friend WithEvents pbCompletion As ProgressBar
    Friend WithEvents lblProgressPct As Label
    Friend WithEvents pnlProgressRow As Panel
    Friend WithEvents btnNavPomodoro As Button
    Friend WithEvents btnNavAnalytics As Button
    Friend WithEvents btnNavProfile As Button
    Friend WithEvents btnNavDashboard As Button
End Class
