<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogin
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
        pnlTitleBar = New Panel()
        btnClose    = New Button()
        btnMinimize = New Button()
        lblTitle    = New Label()
        pnlCenter   = New Panel()   ' fills the form below title bar, centers the card
        pnlLoginBox = New Panel()   ' the white card
        Label1      = New Label()   ' app title
        lblSubtitle = New Label()
        Label2      = New Label()   ' username label
        txtUsername = New TextBox()
        Label3      = New Label()   ' password label
        txtPassword = New TextBox()
        btnLogin    = New Button()
        btnRegister = New Button()
        btnExit     = New Button()
        pnlTitleBar.SuspendLayout()
        pnlCenter.SuspendLayout()
        pnlLoginBox.SuspendLayout()
        SuspendLayout()

        ' ── Title Bar (h=44) ─────────────────────────────────────────────────
        pnlTitleBar.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(btnMinimize)
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

        btnMinimize.Dock = DockStyle.Right
        btnMinimize.FlatAppearance.BorderSize = 0
        btnMinimize.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#312E81")
        btnMinimize.FlatStyle = FlatStyle.Flat
        btnMinimize.Font      = New Font("Segoe UI", 13.0F, FontStyle.Bold)
        btnMinimize.ForeColor = Color.White
        btnMinimize.Name      = "btnMinimize"
        btnMinimize.Size      = New Size(44, 44)
        btnMinimize.TabIndex  = 98
        btnMinimize.Text      = "─"

        lblTitle.AutoSize  = True
        lblTitle.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location  = New Point(16, 13)
        lblTitle.Name      = "lblTitle"
        lblTitle.TabIndex  = 0
        lblTitle.Text      = "Smart Student Task Manager"

        ' ── Center Panel (fills below title bar, used to vertically center card) ─
        pnlCenter.BackColor = ColorTranslator.FromHtml("#F4F6FB")
        pnlCenter.Controls.Add(pnlLoginBox)
        pnlCenter.Dock     = DockStyle.Fill
        pnlCenter.Name     = "pnlCenter"
        pnlCenter.TabIndex = 1

        ' ── Login Card (440×400, centered in pnlCenter via code) ─────────────
        pnlLoginBox.BackColor = Color.White
        pnlLoginBox.Controls.Add(Label1)
        pnlLoginBox.Controls.Add(lblSubtitle)
        pnlLoginBox.Controls.Add(Label2)
        pnlLoginBox.Controls.Add(txtUsername)
        pnlLoginBox.Controls.Add(Label3)
        pnlLoginBox.Controls.Add(txtPassword)
        pnlLoginBox.Controls.Add(btnLogin)
        pnlLoginBox.Controls.Add(btnRegister)
        pnlLoginBox.Controls.Add(btnExit)
        pnlLoginBox.Name     = "pnlLoginBox"
        pnlLoginBox.Size     = New Size(440, 400)
        pnlLoginBox.TabIndex = 0

        ' App title  (y=28, full width, centered)
        Label1.AutoSize   = False
        Label1.Font       = New Font("Segoe UI", 19.0F, FontStyle.Bold)
        Label1.ForeColor  = ColorTranslator.FromHtml("#1E1B4B")
        Label1.Location   = New Point(0, 24)
        Label1.Name       = "Label1"
        Label1.Size       = New Size(440, 40)
        Label1.TabIndex   = 0
        Label1.Text       = "📚 Smart Task Manager"
        Label1.TextAlign  = ContentAlignment.MiddleCenter

        ' Subtitle  (y=68)
        lblSubtitle.AutoSize   = False
        lblSubtitle.Font       = New Font("Segoe UI", 10.0F)
        lblSubtitle.ForeColor  = ColorTranslator.FromHtml("#6B7280")
        lblSubtitle.Location   = New Point(0, 68)
        lblSubtitle.Name       = "lblSubtitle"
        lblSubtitle.Size       = New Size(440, 22)
        lblSubtitle.TabIndex   = 10
        lblSubtitle.Text       = "Sign in to manage your tasks"
        lblSubtitle.TextAlign  = ContentAlignment.MiddleCenter

        ' Username label  (y=108)
        Label2.AutoSize  = True
        Label2.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = ColorTranslator.FromHtml("#374151")
        Label2.Location  = New Point(40, 108)
        Label2.Name      = "Label2"
        Label2.TabIndex  = 1
        Label2.Text      = "Username"

        ' Username input  (y=130)
        txtUsername.BorderStyle    = BorderStyle.FixedSingle
        txtUsername.Font           = New Font("Segoe UI", 11.0F)
        txtUsername.Location       = New Point(40, 130)
        txtUsername.Name           = "txtUsername"
        txtUsername.PlaceholderText = "Enter your username"
        txtUsername.Size           = New Size(360, 36)
        txtUsername.TabIndex       = 2

        ' Password label  (y=182)
        Label3.AutoSize  = True
        Label3.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label3.ForeColor = ColorTranslator.FromHtml("#374151")
        Label3.Location  = New Point(40, 182)
        Label3.Name      = "Label3"
        Label3.TabIndex  = 3
        Label3.Text      = "Password"

        ' Password input  (y=204)
        txtPassword.BorderStyle    = BorderStyle.FixedSingle
        txtPassword.Font           = New Font("Segoe UI", 11.0F)
        txtPassword.Location       = New Point(40, 204)
        txtPassword.Name           = "txtPassword"
        txtPassword.PasswordChar   = "●"c
        txtPassword.PlaceholderText = "Enter your password"
        txtPassword.Size           = New Size(360, 36)
        txtPassword.TabIndex       = 4

        ' Sign In button  (y=258)
        btnLogin.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnLogin.Cursor    = Cursors.Hand
        btnLogin.FlatAppearance.BorderSize = 0
        btnLogin.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4338CA")
        btnLogin.FlatStyle = FlatStyle.Flat
        btnLogin.Font      = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnLogin.ForeColor = Color.White
        btnLogin.Location  = New Point(40, 258)
        btnLogin.Name      = "btnLogin"
        btnLogin.Size      = New Size(360, 46)
        btnLogin.TabIndex  = 5
        btnLogin.Text      = "Sign In"

        ' Create Account button  (y=318)
        btnRegister.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        btnRegister.Cursor    = Cursors.Hand
        btnRegister.FlatAppearance.BorderSize = 0
        btnRegister.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E0E7FF")
        btnRegister.FlatStyle = FlatStyle.Flat
        btnRegister.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnRegister.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        btnRegister.Location  = New Point(40, 318)
        btnRegister.Name      = "btnRegister"
        btnRegister.Size      = New Size(172, 40)
        btnRegister.TabIndex  = 6
        btnRegister.Text      = "Create Account"

        ' Exit button  (y=318, right half)
        btnExit.BackColor = Color.White
        btnExit.Cursor    = Cursors.Hand
        btnExit.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#E5E7EB")
        btnExit.FlatAppearance.BorderSize  = 1
        btnExit.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#FEF2F2")
        btnExit.FlatStyle = FlatStyle.Flat
        btnExit.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnExit.ForeColor = ColorTranslator.FromHtml("#6B7280")
        btnExit.Location  = New Point(228, 318)
        btnExit.Name      = "btnExit"
        btnExit.Size      = New Size(172, 40)
        btnExit.TabIndex  = 7
        btnExit.Text      = "Exit"
        btnExit.UseVisualStyleBackColor = False

        ' ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode       = AutoScaleMode.Font
        BackColor           = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize          = New Size(560, 530)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize     = New Size(560, 530)
        Name            = "frmLogin"
        StartPosition   = FormStartPosition.CenterScreen
        WindowState     = FormWindowState.Maximized
        Text            = "Login"
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlCenter.ResumeLayout(False)
        pnlLoginBox.ResumeLayout(False)
        pnlLoginBox.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTitleBar As Panel
    Friend WithEvents btnClose    As Button
    Friend WithEvents btnMinimize As Button
    Friend WithEvents lblTitle    As Label
    Friend WithEvents pnlCenter   As Panel
    Friend WithEvents pnlLoginBox As Panel
    Friend WithEvents Label1      As Label
    Friend WithEvents lblSubtitle As Label
    Friend WithEvents Label2      As Label
    Friend WithEvents Label3      As Label
    Friend WithEvents txtUsername As TextBox
    Friend WithEvents txtPassword As TextBox
    Friend WithEvents btnLogin    As Button
    Friend WithEvents btnRegister As Button
    Friend WithEvents btnExit     As Button
End Class
