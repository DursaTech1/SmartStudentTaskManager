Imports System.Drawing
Imports System.Windows.Forms

Public Module ThemeManager

    ' ── Palette ──────────────────────────────────────────────────────────────
    Public ReadOnly BackgroundColor As Color = ColorTranslator.FromHtml("#F4F6FB")
    Public ReadOnly SurfaceColor As Color = Color.White
    Public ReadOnly SurfaceAltColor As Color = ColorTranslator.FromHtml("#EEF2FF")

    Public ReadOnly TextColor As Color = ColorTranslator.FromHtml("#1A1D2E")
    Public ReadOnly MutedTextColor As Color = ColorTranslator.FromHtml("#6B7280")

    Public ReadOnly PrimaryColor As Color = ColorTranslator.FromHtml("#4F46E5")
    Public ReadOnly PrimaryHoverColor As Color = ColorTranslator.FromHtml("#4338CA")
    Public ReadOnly PrimaryLightColor As Color = ColorTranslator.FromHtml("#EEF2FF")

    Public ReadOnly DangerColor As Color = ColorTranslator.FromHtml("#EF4444")
    Public ReadOnly DangerHoverColor As Color = ColorTranslator.FromHtml("#DC2626")

    Public ReadOnly SuccessColor As Color = ColorTranslator.FromHtml("#10B981")
    Public ReadOnly SuccessHoverColor As Color = ColorTranslator.FromHtml("#059669")

    Public ReadOnly WarningColor As Color = ColorTranslator.FromHtml("#F59E0B")
    Public ReadOnly WarningHoverColor As Color = ColorTranslator.FromHtml("#D97706")

    Public ReadOnly BorderColor As Color = ColorTranslator.FromHtml("#E5E7EB")
    Public ReadOnly BorderFocusColor As Color = ColorTranslator.FromHtml("#4F46E5")

    Public ReadOnly SidebarColor As Color = ColorTranslator.FromHtml("#1E1B4B")
    Public ReadOnly SidebarHoverColor As Color = ColorTranslator.FromHtml("#312E81")
    Public ReadOnly SidebarActiveColor As Color = ColorTranslator.FromHtml("#4F46E5")
    Public ReadOnly TitleBarColor As Color = ColorTranslator.FromHtml("#1E1B4B")

    ' Priority colours
    Public ReadOnly PriorityHighBg As Color = ColorTranslator.FromHtml("#FEE2E2")
    Public ReadOnly PriorityHighFg As Color = ColorTranslator.FromHtml("#991B1B")
    Public ReadOnly PriorityMediumBg As Color = ColorTranslator.FromHtml("#FEF3C7")
    Public ReadOnly PriorityMediumFg As Color = ColorTranslator.FromHtml("#92400E")
    Public ReadOnly PriorityLowBg As Color = ColorTranslator.FromHtml("#D1FAE5")
    Public ReadOnly PriorityLowFg As Color = ColorTranslator.FromHtml("#065F46")

    ' ── Fonts ────────────────────────────────────────────────────────────────
    Public ReadOnly MainFont As New Font("Segoe UI", 10.0F, FontStyle.Regular, GraphicsUnit.Point)
    Public ReadOnly SmallFont As New Font("Segoe UI", 9.0F, FontStyle.Regular, GraphicsUnit.Point)
    Public ReadOnly SemiBoldFont As New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold, GraphicsUnit.Point)
    Public ReadOnly HeaderFont As New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold, GraphicsUnit.Point)
    Public ReadOnly TitleFont As New Font("Segoe UI Semibold", 18.0F, FontStyle.Bold, GraphicsUnit.Point)
    Public ReadOnly CountFont As New Font("Segoe UI", 28.0F, FontStyle.Bold, GraphicsUnit.Point)

    ' ── Entry point ──────────────────────────────────────────────────────────
    Public Sub ApplyTheme(ByVal frm As Form)
        frm.BackColor = BackgroundColor
        frm.ForeColor = TextColor
        frm.Font = MainFont
        frm.AutoScaleMode = AutoScaleMode.Font
        ApplyThemeToControls(frm.Controls)
    End Sub

    Private Sub ApplyThemeToControls(ByVal controls As Control.ControlCollection)
        For Each ctrl As Control In controls
            Select Case True
                Case TypeOf ctrl Is Button
                    StyleButton(DirectCast(ctrl, Button))
                Case TypeOf ctrl Is TextBox
                    StyleTextBox(DirectCast(ctrl, TextBox))
                Case TypeOf ctrl Is ComboBox
                    StyleComboBox(DirectCast(ctrl, ComboBox))
                Case TypeOf ctrl Is Label
                    StyleLabel(DirectCast(ctrl, Label))
                Case TypeOf ctrl Is DataGridView
                    StyleDataGridView(DirectCast(ctrl, DataGridView))
                Case TypeOf ctrl Is GroupBox
                    StyleGroupBox(DirectCast(ctrl, GroupBox))
                Case TypeOf ctrl Is CheckBox
                    StyleCheckBox(DirectCast(ctrl, CheckBox))
                Case TypeOf ctrl Is DateTimePicker
                    ctrl.Font = MainFont
                    ctrl.ForeColor = TextColor
                Case TypeOf ctrl Is Panel OrElse TypeOf ctrl Is TableLayoutPanel
                    StylePanel(ctrl)
            End Select
            If ctrl.Controls.Count > 0 Then ApplyThemeToControls(ctrl.Controls)
        Next
    End Sub

    ' ── Panel / Container ────────────────────────────────────────────────────
    Private Sub StylePanel(ByVal pnl As Control)
        Dim n As String = If(pnl.Name, "").ToLowerInvariant()
        If n.Contains("sidebar") Then
            pnl.BackColor = SidebarColor : pnl.ForeColor = Color.White
        ElseIf n.Contains("titlebar") Then
            pnl.BackColor = TitleBarColor : pnl.ForeColor = Color.White
        ElseIf n.Contains("card") OrElse n.Contains("loginbox") OrElse
               n.Contains("registerbox") OrElse n = "pnlcontent" OrElse n = "pnlmeta" Then
            pnl.BackColor = SurfaceColor : pnl.ForeColor = TextColor
        ElseIf n.Contains("maincontent") OrElse n.Contains("view") OrElse n = "pnlcenter" Then
            pnl.BackColor = BackgroundColor : pnl.ForeColor = TextColor
        ElseIf n = "pnlactionbar" Then
            pnl.BackColor = SurfaceColor
        End If
    End Sub

    ' ── Card with shadow ─────────────────────────────────────────────────────
    Public Sub ApplyCardStyle(ByVal pnl As Panel, Optional accentColor As Color? = Nothing)
        If pnl Is Nothing Then Return
        If pnl.Tag IsNot Nothing AndAlso pnl.Tag.ToString() = "card-styled" Then Return
        pnl.Tag = "card-styled"
        pnl.BackColor = SurfaceColor
        pnl.Padding = New Padding(12)
        Dim accent As Color = If(accentColor.HasValue, accentColor.Value, PrimaryColor)

        AddHandler pnl.Paint,
            Sub(sender As Object, e As PaintEventArgs)
                Dim g As Graphics = e.Graphics
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Soft drop shadow (3 layers)
                For i As Integer = 3 To 1 Step -1
                    Dim alpha As Integer = 18 - i * 4
                    Dim sr As New Rectangle(i, i, pnl.Width - 1, pnl.Height - 1)
                    Using sp As New Pen(Color.FromArgb(alpha, 0, 0, 0), 1)
                        DrawRoundedRect(g, sp, sr, 10)
                    End Using
                Next

                ' White card background
                Dim r As New Rectangle(0, 0, pnl.Width - 4, pnl.Height - 4)
                Using bg As New SolidBrush(SurfaceColor)
                    FillRoundedRect(g, bg, r, 10)
                End Using

                ' Border
                Using borderPen As New Pen(BorderColor, 1.0F)
                    DrawRoundedRect(g, borderPen, r, 10)
                End Using

                ' Accent left bar
                Using accentBrush As New SolidBrush(accent)
                    Dim ar As New Rectangle(0, 0, 5, pnl.Height - 4)
                    FillRoundedRect(g, accentBrush, ar, 4)
                End Using
            End Sub
        pnl.Invalidate()
    End Sub

    ' ── Rounded fill helper ───────────────────────────────────────────────────
    Public Sub FillRoundedRect(g As Graphics, brush As Brush, r As Rectangle, radius As Integer)
        Dim d As Integer = radius * 2
        Using path As New Drawing2D.GraphicsPath()
            path.AddArc(r.X, r.Y, d, d, 180, 90)
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
            path.CloseFigure()
            g.FillPath(brush, path)
        End Using
    End Sub

    Public Sub DrawRoundedRect(g As Graphics, pen As Pen, r As Rectangle, radius As Integer)
        Dim d As Integer = radius * 2
        Using path As New Drawing2D.GraphicsPath()
            path.AddArc(r.X, r.Y, d, d, 180, 90)
            path.AddArc(r.Right - d, r.Y, d, d, 270, 90)
            path.AddArc(r.Right - d, r.Bottom - d, d, d, 0, 90)
            path.AddArc(r.X, r.Bottom - d, d, d, 90, 90)
            path.CloseFigure()
            g.DrawPath(pen, path)
        End Using
    End Sub

    ' Legacy helper
    Public Sub ApplyBlueBorder(ByVal ctrl As Control)
        If ctrl Is Nothing Then Return
        If ctrl.Tag IsNot Nothing AndAlso ctrl.Tag.ToString() = "bordered" Then Return
        ctrl.Tag = "bordered"
        AddHandler ctrl.Paint,
            Sub(sender As Object, e As PaintEventArgs)
                Using p As New Pen(BorderColor, 1.5F)
                    Dim r As Rectangle = ctrl.ClientRectangle
                    r.Width -= 1 : r.Height -= 1
                    e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                    e.Graphics.DrawRectangle(p, r)
                End Using
            End Sub
        ctrl.Invalidate()
    End Sub

    ' ── Button ───────────────────────────────────────────────────────────────
    Private Sub StyleButton(ByVal btn As Button)
        Dim n As String = If(btn.Name, "").ToLowerInvariant()
        btn.FlatStyle = FlatStyle.Flat
        btn.FlatAppearance.BorderSize = 0
        btn.Cursor = Cursors.Hand
        btn.Font = SemiBoldFont
        btn.UseVisualStyleBackColor = False

        ' ── Skip action bar buttons — styled explicitly in ApplyPerfectLayout ─
        If btn.Parent IsNot Nothing AndAlso
           (btn.Parent.Name = "pnlActionBar" OrElse btn.Parent.Name = "flpActionBar") Then Return

        ' Window chrome
        If n.Contains("close") AndAlso (btn.Parent IsNot Nothing AndAlso btn.Parent.Name.ToLowerInvariant().Contains("titlebar")) Then
            btn.BackColor = Color.Transparent : btn.ForeColor = Color.White
            btn.FlatAppearance.MouseOverBackColor = DangerColor
            btn.FlatAppearance.MouseDownBackColor = DangerHoverColor : Return
        End If
        If n.Contains("minimize") OrElse n.Contains("maximize") Then
            btn.BackColor = Color.Transparent : btn.ForeColor = Color.White
            btn.FlatAppearance.MouseOverBackColor = SidebarHoverColor
            btn.FlatAppearance.MouseDownBackColor = SidebarHoverColor : Return
        End If

        ' Sidebar / nav
        If n.Contains("nav") OrElse n.Contains("logout") OrElse
           (btn.Parent IsNot Nothing AndAlso btn.Parent.Name.ToLowerInvariant().Contains("sidebar")) Then
            btn.BackColor = Color.Transparent : btn.ForeColor = Color.White
            btn.FlatAppearance.MouseOverBackColor = SidebarHoverColor
            btn.FlatAppearance.MouseDownBackColor = SidebarActiveColor
            btn.TextAlign = ContentAlignment.MiddleLeft
            btn.Padding = New Padding(20, 0, 0, 0) : Return
        End If

        ' Danger — only explicit delete buttons and standalone exit (not inside login/register cards)
        Dim parentName As String = If(btn.Parent IsNot Nothing, btn.Parent.Name.ToLowerInvariant(), "")
        Dim inCard As Boolean = parentName.Contains("loginbox") OrElse parentName.Contains("registerbox")
        If (n = "btndelete" OrElse n = "btndeletetask" OrElse n = "btndeletesubtask" OrElse
            (n.Contains("exit") AndAlso Not inCard)) Then
            btn.BackColor = DangerColor : btn.ForeColor = Color.White
            btn.FlatAppearance.MouseOverBackColor = DangerHoverColor
            btn.FlatAppearance.MouseDownBackColor = DangerHoverColor : Return
        End If

        ' Ghost / secondary
        If n = "btncancel" OrElse n = "btnback" Then
            btn.BackColor = PrimaryLightColor : btn.ForeColor = PrimaryColor
            btn.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E0E7FF")
            btn.FlatAppearance.MouseDownBackColor = ColorTranslator.FromHtml("#C7D2FE") : Return
        End If

        ' Success / toggle
        If n.Contains("toggle") OrElse n.Contains("markall") Then
            btn.BackColor = SuccessColor : btn.ForeColor = Color.White
            btn.FlatAppearance.MouseOverBackColor = SuccessHoverColor
            btn.FlatAppearance.MouseDownBackColor = SuccessHoverColor : Return
        End If

        ' Filter/print/export/duplicate — ghost style
        If n.Contains("filter") OrElse n.Contains("print") OrElse n.Contains("export") OrElse n.Contains("duplicate") Then
            btn.BackColor = Color.White : btn.ForeColor = TextColor
            btn.FlatAppearance.BorderSize = 1
            btn.FlatAppearance.BorderColor = BorderColor
            btn.FlatAppearance.MouseOverBackColor = PrimaryLightColor
            btn.FlatAppearance.MouseDownBackColor = PrimaryLightColor : Return
        End If

        ' Default primary
        btn.BackColor = PrimaryColor : btn.ForeColor = Color.White
        btn.FlatAppearance.MouseOverBackColor = PrimaryHoverColor
        btn.FlatAppearance.MouseDownBackColor = PrimaryHoverColor
    End Sub

    ' ── TextBox ──────────────────────────────────────────────────────────────
    Private Sub StyleTextBox(ByVal txt As TextBox)
        txt.BackColor = SurfaceColor
        txt.ForeColor = TextColor
        txt.BorderStyle = BorderStyle.FixedSingle
        txt.Font = MainFont
    End Sub

    ' ── ComboBox ─────────────────────────────────────────────────────────────
    Private Sub StyleComboBox(ByVal cmb As ComboBox)
        cmb.BackColor = SurfaceColor : cmb.ForeColor = TextColor
        cmb.FlatStyle = FlatStyle.Flat : cmb.Font = MainFont
    End Sub

    ' ── CheckBox ─────────────────────────────────────────────────────────────
    Private Sub StyleCheckBox(ByVal chk As CheckBox)
        chk.ForeColor = TextColor : chk.Font = MainFont : chk.BackColor = Color.Transparent
    End Sub

    ' ── Label ────────────────────────────────────────────────────────────────
    Private Sub StyleLabel(ByVal lbl As Label)
        lbl.BackColor = Color.Transparent
        lbl.ForeColor = TextColor
        lbl.Font = MainFont
        Dim n As String = If(lbl.Name, "").ToLowerInvariant()
        Dim onDark As Boolean = (lbl.Parent IsNot Nothing AndAlso
            (lbl.Parent.BackColor.GetBrightness() < 0.3F OrElse
             lbl.Parent.Name.ToLowerInvariant().Contains("titlebar") OrElse
             lbl.Parent.Name.ToLowerInvariant().Contains("sidebar")))

        Select Case True
            Case n = "lblapptitle"
                lbl.Font = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold, GraphicsUnit.Point)
                lbl.ForeColor = Color.White
            Case n = "lblwelcome"
                lbl.Font = SemiBoldFont : lbl.ForeColor = Color.White
            Case n = "lblformtitle" OrElse n = "lbltitle"
                lbl.Font = SemiBoldFont : lbl.ForeColor = Color.White
            Case n = "lblrecentactivity" OrElse n = "lblcalendardate"
                lbl.Font = HeaderFont : lbl.ForeColor = TextColor
            Case n.StartsWith("lblcount")
                lbl.Font = CountFont : lbl.ForeColor = PrimaryColor
            Case n.StartsWith("lbltitle")
                lbl.Font = MainFont : lbl.ForeColor = MutedTextColor
            Case n.StartsWith("lbl")
                lbl.Font = SemiBoldFont
                lbl.ForeColor = If(onDark, Color.White, TextColor)
        End Select
        If onDark Then lbl.ForeColor = Color.White
    End Sub

    ' ── GroupBox ─────────────────────────────────────────────────────────────
    Private Sub StyleGroupBox(ByVal grp As GroupBox)
        grp.ForeColor = MutedTextColor : grp.Font = SemiBoldFont : grp.BackColor = BackgroundColor
    End Sub

    ' ── DataGridView ─────────────────────────────────────────────────────────
    Public Sub StyleDataGridView(ByVal dgv As DataGridView)
        dgv.EnableHeadersVisualStyles = False
        dgv.BackgroundColor = SurfaceColor
        dgv.BorderStyle = BorderStyle.None
        dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        dgv.GridColor = BorderColor

        dgv.DefaultCellStyle.BackColor = SurfaceColor
        dgv.DefaultCellStyle.ForeColor = TextColor
        dgv.DefaultCellStyle.Font = MainFont
        dgv.DefaultCellStyle.SelectionBackColor = PrimaryLightColor
        dgv.DefaultCellStyle.SelectionForeColor = PrimaryColor
        dgv.DefaultCellStyle.Padding = New Padding(6, 0, 6, 0)

        dgv.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F9FAFB")
        dgv.AlternatingRowsDefaultCellStyle.ForeColor = TextColor

        dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        dgv.ColumnHeadersDefaultCellStyle.Font = SemiBoldFont
        dgv.ColumnHeadersDefaultCellStyle.Padding = New Padding(6, 0, 6, 0)
        dgv.ColumnHeadersHeight = 44
        dgv.RowTemplate.Height = 40

        dgv.RowHeadersVisible = False
        dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgv.Cursor = Cursors.Hand
    End Sub

    ' ── Priority colour helper ────────────────────────────────────────────────
    Public Sub ColorCodeGridByPriority(ByVal grid As DataGridView)
        For Each row As DataGridViewRow In grid.Rows
            If Not grid.Columns.Contains("Priority") Then Continue For
            If row.Cells("Priority").Value Is Nothing Then Continue For
            Select Case row.Cells("Priority").Value.ToString()
                Case "High"
                    row.DefaultCellStyle.BackColor = PriorityHighBg
                    row.DefaultCellStyle.ForeColor = PriorityHighFg
                Case "Medium"
                    row.DefaultCellStyle.BackColor = PriorityMediumBg
                    row.DefaultCellStyle.ForeColor = PriorityMediumFg
                Case "Low"
                    row.DefaultCellStyle.BackColor = PriorityLowBg
                    row.DefaultCellStyle.ForeColor = PriorityLowFg
            End Select
        Next
    End Sub

    ' ── Styled empty-state panel ──────────────────────────────────────────────
    Public Sub StyleEmptyState(pnl As Panel, lbl As Label, icon As String, message As String)
        If pnl Is Nothing OrElse lbl Is Nothing Then Return
        pnl.BackColor = SurfaceColor

        ' Icon label (large emoji)
        Dim lblIcon As Label = Nothing
        For Each c As Control In pnl.Controls
            If c.Name = "lblEmptyIcon" Then lblIcon = DirectCast(c, Label) : Exit For
        Next
        If lblIcon Is Nothing Then
            lblIcon = New Label() With {
                .Name = "lblEmptyIcon",
                .AutoSize = False,
                .Font = New Font("Segoe UI", 36.0F),
                .TextAlign = ContentAlignment.MiddleCenter,
                .BackColor = Color.Transparent,
                .ForeColor = MutedTextColor,
                .Dock = DockStyle.None
            }
            pnl.Controls.Add(lblIcon)
        End If
        lblIcon.Text = icon
        lblIcon.Size = New Size(pnl.Width, 70)
        lblIcon.Location = New Point(0, CInt(pnl.Height / 2) - 70)

        lbl.AutoSize = False
        lbl.Font = New Font("Segoe UI", 12.0F, FontStyle.Regular)
        lbl.ForeColor = MutedTextColor
        lbl.TextAlign = ContentAlignment.MiddleCenter
        lbl.Text = message
        lbl.Size = New Size(pnl.Width, 40)
        lbl.Location = New Point(0, CInt(pnl.Height / 2) + 10)
        lbl.BackColor = Color.Transparent
    End Sub

End Module
