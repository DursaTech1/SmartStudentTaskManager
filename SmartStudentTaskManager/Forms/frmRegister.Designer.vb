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
        pnlTitleBar       = New Panel()
        btnClose          = New Button()
        lblTitle          = New Label()
        pnlCenter         = New Panel()
        pnlRegisterBox    = New Panel()
        Label1            = New Label()
        lblSubtitle       = New Label()
        Label2            = New Label()
        txtUsername       = New TextBox()
        Label3            = New Label()
        txtEmail          = New TextBox()
        Label4            = New Label()
        txtPassword       = New TextBox()
        Label5            = New Label()
        txtConfirmPassword = New TextBox()
        btnRegister       = New Button()
        btnCancel         = New Button()
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

        ' ── Register Card (460×530) ───────────────────────────────────────────
        ' Row pitch: label(18) + gap(4) + input(36) + gap(18) = 76px per row
        ' Rows: heading(40) + subtitle(22) + gap(16) = 78 before row1
        ' Row1 y=78, Row2 y=154, Row3 y=230, Row4 y=306, btn y=382, back y=440
        pnlRegisterBox.BackColor = Color.White
        pnlRegisterBox.Controls.Add(Label1)
        pnlRegisterBox.Controls.Add(lblSubtitle)
        pnlRegisterBox.Controls.Add(Label2)
        pnlRegisterBox.Controls.Add(txtUsername)
        pnlRegisterBox.Controls.Add(Label3)
        pnlRegisterBox.Controls.Add(txtEmail)
        pnlRegisterBox.Controls.Add(Label4)
        pnlRegisterBox.Controls.Add(txtPassword)
        pnlRegisterBox.Controls.Add(Label5)
        pnlRegisterBox.Controls.Add(txtConfirmPassword)
        pnlRegisterBox.Controls.Add(btnRegister)
        pnlRegisterBox.Controls.Add(btnCancel)
        pnlRegisterBox.Name     = "pnlRegisterBox"
        pnlRegisterBox.Size     = New Size(460, 490)
        pnlRegisterBox.TabIndex = 0

        ' Heading  (y=20)
        Label1.AutoSize   = False
        Label1.Font       = New Font("Segoe UI", 17.0F, FontStyle.Bold)
        Label1.ForeColor  = ColorTranslator.FromHtml("#1E1B4B")
        Label1.Location   = New Point(0, 20)
        Label1.Name       = "Label1"
        Label1.Size       = New Size(460, 38)
        Label1.TabIndex   = 0
        Label1.Text       = "Create New Account"
        Label1.TextAlign  = ContentAlignment.MiddleCenter

        ' Subtitle  (y=60)
        lblSubtitle.AutoSize   = False
        lblSubtitle.Font       = New Font("Segoe UI", 10.0F)
        lblSubtitle.ForeColor  = ColorTranslator.FromHtml("#6B7280")
        lblSubtitle.Location   = New Point(0, 60)
        lblSubtitle.Name       = "lblSubtitle"
        lblSubtitle.Size       = New Size(460, 22)
        lblSubtitle.TabIndex   = 11
        lblSubtitle.Text       = "Fill in the details below to get started"
        lblSubtitle.TextAlign  = ContentAlignment.MiddleCenter

        ' ── Row 1: Username  (label y=96, input y=116) ───────────────────────
        Label2.AutoSize  = True
        Label2.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = ColorTranslator.FromHtml("#374151")
        Label2.Location  = New Point(50, 96)
        Label2.Name      = "Label2"
        Label2.TabIndex  = 1
        Label2.Text      = "Username"

        txtUsername.BorderStyle    = BorderStyle.FixedSingle
        txtUsername.Font           = New Font("Segoe UI", 11.0F)
        txtUsername.Location       = New Point(50, 116)
        txtUsername.Name           = "txtUsername"
        txtUsername.PlaceholderText = "Choose a username"
        txtUsername.Size           = New Size(360, 36)
        txtUsername.TabIndex       = 2

        ' ── Row 2: Email  (label y=168, input y=188) ─────────────────────────
        Label3.AutoSize  = True
        Label3.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label3.ForeColor = ColorTranslator.FromHtml("#374151")
        Label3.Location  = New Point(50, 168)
        Label3.Name      = "Label3"
        Label3.TabIndex  = 3
        Label3.Text      = "Email Address"

        txtEmail.BorderStyle    = BorderStyle.FixedSingle
        txtEmail.Font           = New Font("Segoe UI", 11.0F)
        txtEmail.Location       = New Point(50, 188)
        txtEmail.Name           = "txtEmail"
        txtEmail.PlaceholderText = "your@email.com"
        txtEmail.Size           = New Size(360, 36)
        txtEmail.TabIndex       = 4

        ' ── Row 3: Password  (label y=240, input y=260) ──────────────────────
        Label4.AutoSize  = True
        Label4.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label4.ForeColor = ColorTranslator.FromHtml("#374151")
        Label4.Location  = New Point(50, 240)
        Label4.Name      = "Label4"
        Label4.TabIndex  = 5
        Label4.Text      = "Password"

        txtPassword.BorderStyle    = BorderStyle.FixedSingle
        txtPassword.Font           = New Font("Segoe UI", 11.0F)
        txtPassword.Location       = New Point(50, 260)
        txtPassword.Name           = "txtPassword"
        txtPassword.PasswordChar   = "●"c
        txtPassword.PlaceholderText = "Min. 6 characters"
        txtPassword.Size           = New Size(360, 36)
        txtPassword.TabIndex       = 6

        ' ── Row 4: Confirm Password  (label y=312, input y=332) ──────────────
        Label5.AutoSize  = True
        Label5.Font      = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label5.ForeColor = ColorTranslator.FromHtml("#374151")
        Label5.Location  = New Point(50, 312)
        Label5.Name      = "Label5"
        Label5.TabIndex  = 7
        Label5.Text      = "Confirm Password"

        txtConfirmPassword.BorderStyle    = BorderStyle.FixedSingle
        txtConfirmPassword.Font           = New Font("Segoe UI", 11.0F)
        txtConfirmPassword.Location       = New Point(50, 332)
        txtConfirmPassword.Name           = "txtConfirmPassword"
        txtConfirmPassword.PasswordChar   = "●"c
        txtConfirmPassword.PlaceholderText = "Repeat your password"
        txtConfirmPassword.Size           = New Size(360, 36)
        txtConfirmPassword.TabIndex       = 8

        ' ── Create Account button  (y=386) ────────────────────────────────────
        btnRegister.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnRegister.Cursor    = Cursors.Hand
        btnRegister.FlatAppearance.BorderSize = 0
        btnRegister.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4338CA")
        btnRegister.FlatStyle = FlatStyle.Flat
        btnRegister.Font      = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnRegister.ForeColor = Color.White
        btnRegister.Location  = New Point(50, 386)
        btnRegister.Name      = "btnRegister"
        btnRegister.Size      = New Size(360, 46)
        btnRegister.TabIndex  = 9
        btnRegister.Text      = "Create Account"

        ' ── Back to Login button  (y=444, centered) ───────────────────────────
        btnCancel.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        btnCancel.Cursor    = Cursors.Hand
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#E0E7FF")
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnCancel.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        btnCancel.Location  = New Point(130, 444)
        btnCancel.Name      = "btnCancel"
        btnCancel.Size      = New Size(200, 38)
        btnCancel.TabIndex  = 10
        btnCancel.Text      = "← Back to Login"

        ' ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode       = AutoScaleMode.Font
        BackColor           = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize          = New Size(560, 620)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize     = New Size(560, 620)
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
    Friend WithEvents Label1             As Label
    Friend WithEvents lblSubtitle        As Label
    Friend WithEvents Label2             As Label
    Friend WithEvents txtUsername        As TextBox
    Friend WithEvents Label3             As Label
    Friend WithEvents txtEmail           As TextBox
    Friend WithEvents Label4             As Label
    Friend WithEvents txtPassword        As TextBox
    Friend WithEvents Label5             As Label
    Friend WithEvents txtConfirmPassword As TextBox
    Friend WithEvents btnRegister        As Button
    Friend WithEvents btnCancel          As Button
End Class
