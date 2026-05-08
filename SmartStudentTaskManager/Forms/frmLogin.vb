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
        SetFieldError(txtUsername, False)
        SetFieldError(txtPassword, False)

        Dim username As String = txtUsername.Text.Trim()
        Dim password As String = txtPassword.Text

        If username = "" Then
            SetFieldError(txtUsername, True) : txtUsername.Focus() : Return
        End If
        If password = "" Then
            SetFieldError(txtPassword, True) : txtPassword.Focus() : Return
        End If

        Try
            ' ── Step 1: check DB connection ──────────────────────────────────
            Dim testConn As Object = DatabaseHelper.ExecuteScalar("SELECT 1", Nothing)
            If testConn Is Nothing Then
                MessageBox.Show("Cannot connect to the database. Check your connection settings.",
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' ── Step 2: check user exists ─────────────────────────────────────
            Dim userCount As Object = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Users WHERE Username = @u",
                {New MySqlParameter("@u", username)})
            If userCount Is Nothing OrElse Convert.ToInt64(userCount) = 0 Then
                SetFieldError(txtUsername, True)
                MessageBox.Show("No account found with that username.", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ── Step 3: fetch stored hash ─────────────────────────────────────
            Dim storedHash As String = ""
            Try
                Dim hashObj As Object = DatabaseHelper.ExecuteScalar(
                    "SELECT PasswordHash FROM Users WHERE Username = @u",
                    {New MySqlParameter("@u", username)})
                If hashObj IsNot Nothing Then storedHash = hashObj.ToString()
            Catch ex As Exception
                MessageBox.Show("Error reading password hash: " & ex.Message,
                                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End Try

            If String.IsNullOrEmpty(storedHash) Then
                MessageBox.Show("Account has no password set. Please contact support.",
                                "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' ── Step 4: verify password ───────────────────────────────────────
            If Not SecurityHelper.VerifyPassword(password, storedHash) Then
                SetFieldError(txtPassword, True)
                MessageBox.Show("Incorrect password.", "Login Failed",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' ── Step 5: upgrade legacy SHA256 hash silently ───────────────────
            If SecurityHelper.IsLegacyHash(storedHash) Then
                Try
                    Dim newHash As String = SecurityHelper.HashPassword(password)
                    DatabaseHelper.ExecuteNonQuery(
                        "UPDATE Users SET PasswordHash=@h WHERE Username=@u",
                        {New MySqlParameter("@h", newHash),
                         New MySqlParameter("@u", username)})
                Catch
                    ' Non-fatal — login still succeeds even if upgrade fails
                End Try
            End If

            ' ── Step 6: load full user profile ───────────────────────────────
            Dim dtUser As DataTable = Nothing
            Try
                dtUser = DatabaseHelper.GetDataTable(
                    "SELECT UserID, Username, Email, Role, FullName, StudentID, ProfilePicturePath, CreatedAt " &
                    "FROM Users WHERE Username = @u",
                    {New MySqlParameter("@u", username)})
            Catch
                dtUser = DatabaseHelper.GetDataTable(
                    "SELECT UserID, Username, Email, Role FROM Users WHERE Username = @u",
                    {New MySqlParameter("@u", username)})
            End Try

            If dtUser Is Nothing OrElse dtUser.Rows.Count = 0 Then
                MessageBox.Show("Could not load user profile.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim row As DataRow = dtUser.Rows(0)
            GlobalVariables.CurrentUser = New User() With {
                .UserID             = Convert.ToInt32(row("UserID")),
                .Username           = row("Username").ToString(),
                .Email              = SafeString(row, "Email"),
                .Role               = If(SafeString(row, "Role") <> "", SafeString(row, "Role"), "Student"),
                .FullName           = SafeString(row, "FullName"),
                .StudentID          = SafeString(row, "StudentID"),
                .ProfilePicturePath = SafeString(row, "ProfilePicturePath"),
                .CreatedAt          = SafeDateTime(row, "CreatedAt")
            }

            Dim dashboard As New frmDashboard()
            dashboard.Show()
            Me.Hide()

        Catch ex As Exception
            MessageBox.Show("Unexpected error: " & ex.Message & Environment.NewLine & ex.StackTrace,
                            "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Returns an empty string if the column is missing or DBNull.</summary>
    Private Shared Function SafeString(row As DataRow, columnName As String) As String
        Try
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return row(columnName).ToString()
            End If
        Catch
        End Try
        Return ""
    End Function

    ''' <summary>Returns DateTime.MinValue if the column is missing or DBNull.</summary>
    Private Shared Function SafeDateTime(row As DataRow, columnName As String) As DateTime
        Try
            If row.Table.Columns.Contains(columnName) AndAlso Not IsDBNull(row(columnName)) Then
                Return Convert.ToDateTime(row(columnName))
            End If
        Catch
        End Try
        Return DateTime.MinValue
    End Function

    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim registerForm As New frmRegister()
        registerForm.ShowDialog()
    End Sub

    Private Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

End Class
