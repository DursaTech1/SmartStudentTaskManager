Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmLogin

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture()
    End Sub

    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(ByVal hWnd As System.IntPtr, ByVal wMsg As Integer, ByVal wParam As Integer, ByVal lParam As Integer)
    End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)

        ' Card shadow + rounded corners — wired here, not in Designer
        AddHandler pnlLoginBox.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                ev.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                For i As Integer = 4 To 1 Step -1
                    Dim sr As New Rectangle(i, i, pnlLoginBox.Width - 1, pnlLoginBox.Height - 1)
                    Using sp As New Pen(Color.FromArgb(12 - i * 2, 0, 0, 0), 1)
                        ThemeManager.DrawRoundedRect(ev.Graphics, sp, sr, 12)
                    End Using
                Next
                Dim r As New Rectangle(0, 0, pnlLoginBox.Width - 5, pnlLoginBox.Height - 5)
                Using bg As New SolidBrush(Color.White)
                    ThemeManager.FillRoundedRect(ev.Graphics, bg, r, 12)
                End Using
                Using bp As New Pen(ColorTranslator.FromHtml("#E5E7EB"), 1)
                    ThemeManager.DrawRoundedRect(ev.Graphics, bp, r, 12)
                End Using
            End Sub

        ' Re-apply Exit button as ghost (ThemeManager may have coloured it)
        btnExit.BackColor = Color.White
        btnExit.ForeColor = ColorTranslator.FromHtml("#6B7280")
        btnExit.FlatAppearance.BorderSize = 1
        btnExit.FlatAppearance.BorderColor = ColorTranslator.FromHtml("#E5E7EB")
        btnExit.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#FEF2F2")
        btnExit.UseVisualStyleBackColor = False

        CenterCard()
        AddHandler Me.Resize, AddressOf HandleResize

        ' Focus border on inputs
        AddFocusBorder(txtUsername)
        AddFocusBorder(txtPassword)
    End Sub

    Private Sub AddFocusBorder(txt As TextBox)
        AddHandler txt.Enter, Sub(s As Object, e As EventArgs) txt.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        AddHandler txt.Leave, Sub(s As Object, e As EventArgs) txt.BackColor = Color.White
    End Sub

    Private Sub SetFieldError(txt As TextBox, hasError As Boolean)
        txt.BackColor = If(hasError, ColorTranslator.FromHtml("#FEF2F2"), Color.White)
    End Sub

    Private Sub CenterCard()
        pnlLoginBox.Left = (pnlCenter.ClientSize.Width - pnlLoginBox.Width) \ 2
        pnlLoginBox.Top = (pnlCenter.ClientSize.Height - pnlLoginBox.Height) \ 2
        If pnlLoginBox.Top < 16 Then pnlLoginBox.Top = 16
    End Sub

    Private Sub HandleResize(sender As Object, e As EventArgs)
        CenterCard()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Application.Exit()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        ' Reset field states
        SetFieldError(txtUsername, False)
        SetFieldError(txtPassword, False)

        Dim hasError As Boolean = False
        If txtUsername.Text.Trim() = "" Then
            SetFieldError(txtUsername, True) : txtUsername.Focus() : hasError = True
        End If
        If txtPassword.Text = "" Then
            SetFieldError(txtPassword, True)
            If Not hasError Then txtPassword.Focus()
            hasError = True
        End If
        If hasError Then Return

        Try
            Dim passwordHash As String = SecurityHelper.HashPassword(txtPassword.Text)

            Dim dt As DataTable = Nothing
            Try
                Dim query As String = "SELECT UserID, Username, Email, Role FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash"
                Dim parameters As MySqlParameter() = {
                    New MySqlParameter("@Username", txtUsername.Text.Trim()),
                    New MySqlParameter("@PasswordHash", passwordHash)
                }
                dt = DatabaseHelper.GetDataTable(query, parameters)
            Catch
                ' Email column may not exist — try without it
                Dim query As String = "SELECT UserID, Username, Role FROM Users WHERE Username = @Username AND PasswordHash = @PasswordHash"
                Dim parameters As MySqlParameter() = {
                    New MySqlParameter("@Username", txtUsername.Text.Trim()),
                    New MySqlParameter("@PasswordHash", passwordHash)
                }
                dt = DatabaseHelper.GetDataTable(query, parameters)
            End Try

            If dt IsNot Nothing AndAlso dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                GlobalVariables.CurrentUser = New User() With {
                    .UserID = Convert.ToInt32(row("UserID")),
                    .Username = row("Username").ToString(),
                    .Email = If(dt.Columns.Contains("Email") AndAlso Not IsDBNull(row("Email")), row("Email").ToString(), ""),
                    .Role = If(dt.Columns.Contains("Role") AndAlso Not IsDBNull(row("Role")), row("Role").ToString(), "Student")
                }
                Dim dashboard As New frmDashboard()
                dashboard.Show()
                Me.Hide()
            Else
                SetFieldError(txtPassword, True)
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim registerForm As New frmRegister()
        registerForm.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

End Class
