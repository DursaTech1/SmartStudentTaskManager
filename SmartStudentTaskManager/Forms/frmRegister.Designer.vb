<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRegister
    Inherits System.Windows.Forms.Form

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

    Private components As System.ComponentModel.IContainer

    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        pnlTitleBar        = New Panel()
        btnClose           = New Button()
        lblTitle           = New Label()
        pnlCenter          = New Panel()
        pnlRegisterBox     = New Panel()
        lblHeading         = New Label()
        lblSubtitle        = New Label()
        ' Section: Personal Info
        lblSectionPersonal = New Label()
        lblFullNameLbl     = New Label()
        txtFullName        = New TextBox()
        lblStudentIDLbl    = New Label()
        txtStudentID       = New TextBox()
        ' Section: Account Info
        lblSectionAccount  = New Label()
        lblUsernameLbl     = New Label()
        txtUsername        = New TextBox()
        lblEmailLbl        = New Label()
        txtEmail           = New TextBox()
        ' Section: Password
        lblSectionPassword = New Label()
        lblPasswordLbl     = New Label()
        txtPassword        = New TextBox()
        lblConfirmLbl      = New Label()
        txtConfirmPassword = New TextBox()
        ' Buttons
        btnRegister        = New Button()
        btnCancel          = New Button()

        pnlTitleBar.SuspendLayout()
        pnlCenter.SuspendLayout()
        pnlRegisterBox.SuspendLayout()
        SuspendLayout()

        ' ── Title Bar ────────────────────────────────────────────────────────
        pnlTitleBar.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(lblTitle)
        pnlTitleBar.Dock     = DockStyle.Top
        pnlTitleBar.Name     = "pnlTitleBar"
        pnlTitleBar.Size     = New Size(560, 44)
        pnlTitleBar.TabIndex = 0

        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#EF4444")
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font      = New Font("Segoe UI", 11.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Name      = "btnClose"
        btnClose.Size      = New Size(44, 44)
        btnClose.TabIndex  = 99
        btnClose.Text      = "✕"

        lblTitle.AutoSize  = True
        lblTitle.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location  = New Point(16, 13)
        lblTitle.Name      = "lblTitle"
        lblTitle.TabIndex  = 0
        lblTitle.Text      = "Smart Student Task Manager"

        ' ── Center Panel ─────────────────────────────────────────────────────
        pnlCenter.BackColor = ColorTranslator.FromHtml("#F4F6FB")
        pnlCenter.Controls.Add(pnlRegisterBox)
        pnlCenter.Dock     = DockStyle.Fill
        pnlCenter.Name     = "pnlCenter"
        pnlCenter.TabIndex = 1

        ' ── Register Card (500×680) ───────────────────────────────────────────
        ' Layout: heading(y=20) + subtitle(y=60) + 3 sections + buttons
        ' Each section: section label + 1-2 rows of label+input (row pitch = 72px)
        pnlRegisterBox.BackColor = Color.White
        pnlRegisterBox.Controls.Add(lblHeading)
        pnlRegisterBox.Controls.Add(lblSubtitle)
        pnlRegisterBox.Controls.Add(lblSectionPersonal)
        pnlRegisterBox.Controls.Add(lblFullNameLbl)
        pnlRegisterBox.Controls.Add(txtFullName)
        pnlRegisterBox.Controls.Add(lblStudentIDLbl)
        pnlRegisterBox.Controls.Add(txtStudentID)
        pnlRegisterBox.Controls.Add(lblSectionAccount)
        pnlRegisterBox.Controls.Add(lblUsernameLbl)
        pnlRegisterBox.Controls.Add(txtUsername)
        pnlRegisterBox.Controls.Add(lblEmailLbl)
        pnlRegisterBox.Controls.Add(txtEmail)
        pnlRegisterBox.Controls.Add(lblSectionPassword)
        pnlRegisterBox.Controls.Add(lblPasswordLbl)
        pnlRegisterBox.Controls.Add(txtPassword)
        pnlRegisterBox.Controls.Add(lblConfirmLbl)
        pnlRegisterBox.Controls.Add(txtConfirmPassword)
        pnlRegisterBox.Controls.Add(btnRegister)
        pnlRegisterBox.Controls.Add(btnCancel)
        pnlRegisterBox.Name     = "pnlRegisterBox"
        pnlRegisterBox.Size     = New Size(500, 680)
        pnlRegisterBox.TabIndex = 0

        ' ── Heading (y=20) ────────────────────────────────────────────────────
        lblHeading.AutoSize   = False
        lblHeading.Font       = New Font("Segoe UI", 17.0F, FontStyle.Bold)
        lblHeading.ForeColor  = ColorTranslator.FromHtml("#1E1B4B")
        lblHeading.Location   = New Point(0, 20)
        lblHeading.Name       = "lblHeading"
        lblHeading.Size       = New Size(500, 38)
        lblHeading.TabIndex   = 0
        lblHeading.Text       = "Create New Account"
        lblHeading.TextAlign  = ContentAlignment.MiddleCenter

        ' ── Subtitle (y=60) ───────────────────────────────────────────────────
        lblSubtitle.AutoSize   = False
        lblSubtitle.Font       = New Font("Segoe UI", 10.0F)
        lblSubtitle.ForeColor  = ColorTranslator.FromHtml("#6B7280")
        lblSubtitle.Location   = New Point(0, 60)
        lblSubtitle.Name       = "lblSubtitle"
        lblSubtitle.Size       = New Size(500, 22)
        lblSubtitle.TabIndex   = 1
        lblSubtitle.Text       = "Fill in your details to get started"
        lblSubtitle.TextAlign  = ContentAlignment.MiddleCenter

        ' ── Section: Personal Information (y=96) ─────────────────────────────
        lblSectionPersonal.AutoSize  = True
        lblSectionPersonal.Font      = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        lblSectionPersonal.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        lblSectionPersonal.Location  = New Point(50, 96)
        lblSectionPersonal.Name      = "lblSectionPersonal"
        lblSectionPersonal.TabIndex  = 2
        lblSectionPersonal.Text      = "PERSONAL INFORMATION"

        ' Full Name (label y=118, input y=138)
        lblFullNameLbl.AutoSize  = True
        lblFullNameLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblFullNameLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblFullNameLbl.Location  = New Point(50, 118)
        lblFullNameLbl.Name      = "lblFullNameLbl"
        lblFullNameLbl.TabIndex  = 3
        lblFullNameLbl.Text      = "Full Name *"

        txtFullName.BorderStyle     = BorderStyle.FixedSingle
        txtFullName.Font            = New Font("Segoe UI", 11.0F)
        txtFullName.Location        = New Point(50, 138)
        txtFullName.Name            = "txtFullName"
        txtFullName.PlaceholderText = "e.g. Ahmad Murad"
        txtFullName.Size            = New Size(400, 36)
        txtFullName.TabIndex        = 4

        ' Student ID (label y=190, input y=210) — optional
        lblStudentIDLbl.AutoSize  = True
        lblStudentIDLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblStudentIDLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblStudentIDLbl.Location  = New Point(50, 190)
        lblStudentIDLbl.Name      = "lblStudentIDLbl"
        lblStudentIDLbl.TabIndex  = 5
        lblStudentIDLbl.Text      = "Student ID  (optional)"

        txtStudentID.BorderStyle     = BorderStyle.FixedSingle
        txtStudentID.Font            = New Font("Segoe UI", 11.0F)
        txtStudentID.Location        = New Point(50, 210)
        txtStudentID.Name            = "txtStudentID"
        txtStudentID.PlaceholderText = "e.g. STU-2024-001"
        txtStudentID.Size            = New Size(400, 36)
        txtStudentID.TabIndex        = 6

        ' ── Section: Account Information (y=262) ─────────────────────────────
        lblSectionAccount.AutoSize  = True
        lblSectionAccount.Font      = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        lblSectionAccount.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        lblSectionAccount.Location  = New Point(50, 262)
        lblSectionAccount.Name      = "lblSectionAccount"
        lblSectionAccount.TabIndex  = 7
        lblSectionAccount.Text      = "ACCOUNT INFORMATION"

        ' Username (label y=284, input y=304)
        lblUsernameLbl.AutoSize  = True
        lblUsernameLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblUsernameLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblUsernameLbl.Location  = New Point(50, 284)
        lblUsernameLbl.Name      = "lblUsernameLbl"
        lblUsernameLbl.TabIndex  = 8
        lblUsernameLbl.Text      = "Username *"

        txtUsername.BorderStyle     = BorderStyle.FixedSingle
        txtUsername.Font            = New Font("Segoe UI", 11.0F)
        txtUsername.Location        = New Point(50, 304)
        txtUsername.Name            = "txtUsername"
        txtUsername.PlaceholderText = "Choose a username (min. 3 chars)"
        txtUsername.Size            = New Size(400, 36)
        txtUsername.TabIndex        = 9

        ' Email (label y=356, input y=376)
        lblEmailLbl.AutoSize  = True
        lblEmailLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblEmailLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblEmailLbl.Location  = New Point(50, 356)
        lblEmailLbl.Name      = "lblEmailLbl"
        lblEmailLbl.TabIndex  = 10
        lblEmailLbl.Text      = "Email Address *"

        txtEmail.BorderStyle     = BorderStyle.FixedSingle
        txtEmail.Font            = New Font("Segoe UI", 11.0F)
        txtEmail.Location        = New Point(50, 376)
        txtEmail.Name            = "txtEmail"
        txtEmail.PlaceholderText = "your@email.com"
        txtEmail.Size            = New Size(400, 36)
        txtEmail.TabIndex        = 11

        ' ── Section: Password (y=428) ─────────────────────────────────────────
        lblSectionPassword.AutoSize  = True
        lblSectionPassword.Font      = New Font("Segoe UI Semibold", 9.0F, FontStyle.Bold)
        lblSectionPassword.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        lblSectionPassword.Location  = New Point(50, 428)
        lblSectionPassword.Name      = "lblSectionPassword"
        lblSectionPassword.TabIndex  = 12
        lblSectionPassword.Text      = "PASSWORD"

        ' Password (label y=450, input y=470)
        lblPasswordLbl.AutoSize  = True
        lblPasswordLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblPasswordLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblPasswordLbl.Location  = New Point(50, 450)
        lblPasswordLbl.Name      = "lblPasswordLbl"
        lblPasswordLbl.TabIndex  = 13
        lblPasswordLbl.Text      = "Password *  (min. 6 characters)"

        txtPassword.BorderStyle     = BorderStyle.FixedSingle
        txtPassword.Font            = New Font("Segoe UI", 11.0F)
        txtPassword.Location        = New Point(50, 470)
        txtPassword.Name            = "txtPassword"
        txtPassword.PasswordChar    = "●"c
        txtPassword.PlaceholderText = "Min. 6 characters"
        txtPassword.Size            = New Size(192, 36)
        txtPassword.TabIndex        = 14

        ' Confirm Password (same row, right half)
        lblConfirmLbl.AutoSize  = True
        lblConfirmLbl.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblConfirmLbl.ForeColor = ColorTranslator.FromHtml("#374151")
        lblConfirmLbl.Location  = New Point(258, 450)
        lblConfirmLbl.Name      = "lblConfirmLbl"
        lblConfirmLbl.TabIndex  = 15
        lblConfirmLbl.Text      = "Confirm Password *"

        txtConfirmPassword.BorderStyle     = BorderStyle.FixedSingle
        txtConfirmPassword.Font            = New Font("Segoe UI", 11.0F)
        txtConfirmPassword.Location        = New Point(258, 470)
        txtConfirmPassword.Name            = "txtConfirmPassword"
        txtConfirmPassword.PasswordChar    = "●"c
        txtConfirmPassword.PlaceholderText = "Repeat password"
        txtConfirmPassword.Size            = New Size(192, 36)
        txtConfirmPassword.TabIndex        = 16

        ' ── Create Account button (y=526) ─────────────────────────────────────
        btnRegister.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnRegister.Cursor    = Cursors.Hand
        btnRegister.FlatAppearance.BorderSize = 0
        btnRegister.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4338CA")
        btnRegister.FlatStyle = FlatStyle.Flat
        btnRegister.Font      = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnRegister.ForeColor = Color.White
        btnRegister.Location  = New Point(50, 526)
        btnRegister.Name      = "btnRegister"
        btnRegister.Size      = New Size(400, 46)
        btnRegister.TabIndex  = 17
        btnRegister.Text      = "🎓  Create Account"

        ' ── Back to Login button (y=584) ──────────────────────────────────────
        btnCancel.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        btnCancel.Cursor    = Cursors.Hand
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E0E7FF")
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnCancel.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        btnCancel.Location  = New Point(150, 584)
        btnCancel.Name      = "btnCancel"
        btnCancel.Size      = New Size(200, 38)
        btnCancel.TabIndex  = 18
        btnCancel.Text      = "← Back to Login"

        ' ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode       = AutoScaleMode.Font
        BackColor           = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize          = New Size(560, 720)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize     = New Size(560, 720)
        Name            = "frmRegister"
        StartPosition   = FormStartPosition.CenterScreen
        WindowState     = FormWindowState.Maximized
        Text            = "Register"
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlCenter.ResumeLayout(False)
        pnlRegisterBox.ResumeLayout(False)
        pnlRegisterBox.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTitleBar        As Panel
    Friend WithEvents btnClose           As Button
    Friend WithEvents lblTitle           As Label
    Friend WithEvents pnlCenter          As Panel
    Friend WithEvents pnlRegisterBox     As Panel
    Friend WithEvents lblHeading         As Label
    Friend WithEvents lblSubtitle        As Label
    Friend WithEvents lblSectionPersonal As Label
    Friend WithEvents lblFullNameLbl     As Label
    Friend WithEvents txtFullName        As TextBox
    Friend WithEvents lblStudentIDLbl    As Label
    Friend WithEvents txtStudentID       As TextBox
    Friend WithEvents lblSectionAccount  As Label
    Friend WithEvents lblUsernameLbl     As Label
    Friend WithEvents txtUsername        As TextBox
    Friend WithEvents lblEmailLbl        As Label
    Friend WithEvents txtEmail           As TextBox
    Friend WithEvents lblSectionPassword As Label
    Friend WithEvents lblPasswordLbl     As Label
    Friend WithEvents txtPassword        As TextBox
    Friend WithEvents lblConfirmLbl      As Label
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents btnRegister        As Button
    Friend WithEvents btnCancel          As Button
End Class
