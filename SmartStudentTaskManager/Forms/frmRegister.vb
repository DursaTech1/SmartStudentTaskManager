Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmRegister

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

    Private Sub frmRegister_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)

        ' Card shadow + rounded corners — wired here, not in Designer
        AddHandler pnlRegisterBox.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                ev.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                For i As Integer = 4 To 1 Step -1
                    Dim sr As New Rectangle(i, i, pnlRegisterBox.Width - 1, pnlRegisterBox.Height - 1)
                    Using sp As New Pen(Color.FromArgb(12 - i * 2, 0, 0, 0), 1)
                        ThemeManager.DrawRoundedRect(ev.Graphics, sp, sr, 12)
                    End Using
                Next
                Dim r As New Rectangle(0, 0, pnlRegisterBox.Width - 5, pnlRegisterBox.Height - 5)
                Using bg As New SolidBrush(Color.White)
                    ThemeManager.FillRoundedRect(ev.Graphics, bg, r, 12)
                End Using
                Using bp As New Pen(ColorTranslator.FromHtml("#E5E7EB"), 1)
                    ThemeManager.DrawRoundedRect(ev.Graphics, bp, r, 12)
                End Using
            End Sub
        CenterCard()
        AddHandler Me.Resize, AddressOf HandleResize

        ' Focus borders
        For Each txt As TextBox In {txtUsername, txtEmail, txtPassword, txtConfirmPassword}
            AddFocusBorder(txt)
        Next
    End Sub

    Private Sub AddFocusBorder(txt As TextBox)
        AddHandler txt.Enter, Sub(s As Object, e As EventArgs) txt.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        AddHandler txt.Leave, Sub(s As Object, e As EventArgs) txt.BackColor = Color.White
    End Sub

    Private Sub SetFieldError(txt As TextBox, hasError As Boolean)
        txt.BackColor = If(hasError, ColorTranslator.FromHtml("#FEF2F2"), Color.White)
    End Sub

    Private Sub CenterCard()
        pnlRegisterBox.Left = (pnlCenter.ClientSize.Width  - pnlRegisterBox.Width)  \ 2
        pnlRegisterBox.Top  = (pnlCenter.ClientSize.Height - pnlRegisterBox.Height) \ 2
        If pnlRegisterBox.Top < 16 Then pnlRegisterBox.Top = 16
    End Sub

    Private Sub HandleResize(sender As Object, e As EventArgs)
        CenterCard()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        If Not ValidateRegistration() Then Return
        Try
            Dim username     As String = txtUsername.Text.Trim()
            Dim email        As String = txtEmail.Text.Trim()
            Dim passwordHash As String = SecurityHelper.HashPassword(txtPassword.Text)

            Dim exists As Long = Convert.ToInt64(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Users WHERE Username = @Username",
                    {New MySqlParameter("@Username", username)}))

            If exists > 0 Then
                MessageBox.Show("Username already exists! Please choose another.", "Registration Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim result As Integer = DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Users (Username, PasswordHash, Email, Role) VALUES (@Username, @PasswordHash, @Email, 'Student')",
                {New MySqlParameter("@Username", username),
                 New MySqlParameter("@PasswordHash", passwordHash),
                 New MySqlParameter("@Email", email)})

            If result > 0 Then
                MessageBox.Show("Registration successful! You can now login.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("Registration failed! Please try again.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function ValidateRegistration() As Boolean
        ' Reset all
        For Each txt As TextBox In {txtUsername, txtEmail, txtPassword, txtConfirmPassword}
            SetFieldError(txt, False)
        Next

        If String.IsNullOrWhiteSpace(txtUsername.Text) Then
            SetFieldError(txtUsername, True) : txtUsername.Focus() : Return False
        End If
        If String.IsNullOrWhiteSpace(txtEmail.Text) OrElse Not txtEmail.Text.Contains("@") OrElse Not txtEmail.Text.Contains(".") Then
            SetFieldError(txtEmail, True) : txtEmail.Focus() : Return False
        End If
        If String.IsNullOrWhiteSpace(txtPassword.Text) OrElse txtPassword.Text.Length < 6 Then
            SetFieldError(txtPassword, True) : txtPassword.Focus() : Return False
        End If
        If txtPassword.Text <> txtConfirmPassword.Text Then
            SetFieldError(txtConfirmPassword, True) : txtConfirmPassword.Focus() : Return False
        End If
        Return True
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

End Class
