Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Drawing.Drawing2D

''' <summary>Modern two-column profile page with avatar, stats, and edit form.</summary>
Public Class frmProfile

    Private _avatarPath As String = ""

    ' ── Win32 for borderless drag ──────────────────────────────────────────────
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture() : End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) : End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    ' ── Form Load ─────────────────────────────────────────────────────────────
    Private Sub frmProfile_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        ' ── Left panel gradient ───────────────────────────────────────────
        AddHandler pnlLeft.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                Using br As New LinearGradientBrush(
                    New Point(0, 0),
                    New Point(0, pnlLeft.Height),
                    ColorTranslator.FromHtml("#1E1B4B"),
                    ColorTranslator.FromHtml("#2D2A6E"))
                    ev.Graphics.FillRectangle(br, pnlLeft.ClientRectangle)
                End Using
            End Sub

        ' ── Card drop shadow ──────────────────────────────────────────────
        AddHandler pnlCenter.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                Dim g As Graphics = ev.Graphics
                g.SmoothingMode = SmoothingMode.AntiAlias
                Dim r As Rectangle = pnlCard.Bounds
                For i As Integer = 6 To 1 Step -1
                    Dim alpha As Integer = CInt(18 * (7 - i))
                    Using shadowBrush As New SolidBrush(Color.FromArgb(alpha, 0, 0, 0))
                        g.FillRectangle(shadowBrush,
                            New Rectangle(r.X - i, r.Y + i, r.Width + i * 2, r.Height + i))
                    End Using
                Next
            End Sub

        ' ── Avatar ring: white ring + clipped photo or initials ─────────────
        AddHandler pnlAvatarRing.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                Dim g As Graphics = ev.Graphics
                g.SmoothingMode    = SmoothingMode.AntiAlias
                g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

                Dim w As Integer = pnlAvatarRing.Width
                Dim h As Integer = pnlAvatarRing.Height

                ' Ring thickness
                Dim ring As Integer = 5
                Dim innerRect As New Rectangle(ring, ring, w - ring * 2, h - ring * 2)

                ' Step 1 — fill the entire panel background to match the left panel
                Using bgBr As New SolidBrush(ColorTranslator.FromHtml("#1E1B4B"))
                    g.FillRectangle(bgBr, 0, 0, w, h)
                End Using
                Using ringBr As New SolidBrush(Color.White)
                    g.FillEllipse(ringBr, 0, 0, w - 1, h - 1)
                End Using

                ' Step 3 — clip to inner circle and draw photo or indigo+initials
                Using clipPath As New GraphicsPath()
                    clipPath.AddEllipse(innerRect)
                    g.SetClip(clipPath)

                    If _avatarPath <> "" AndAlso File.Exists(_avatarPath) Then
                        Try
                            Using img As Image = Image.FromFile(_avatarPath)
                                g.DrawImage(img, innerRect)
                            End Using
                            g.ResetClip()
                            ' Step 4 — redraw white ring on top to sharpen the edge
                            Using ringPen As New Pen(Color.White, ring * 2)
                                g.DrawEllipse(ringPen, ring, ring, w - ring * 2, h - ring * 2)
                            End Using
                            Return
                        Catch
                        End Try
                    End If

                    ' No photo — draw indigo fill
                    Using indigoBr As New SolidBrush(ColorTranslator.FromHtml("#4F46E5"))
                        g.FillEllipse(indigoBr, innerRect)
                    End Using
                    g.ResetClip()

                    ' Step 4 — draw initials centered in the inner circle
                    Dim initials As String = If(lblAvatarInitials.Text <> "", lblAvatarInitials.Text, "?")
                    Using fnt As New Font("Segoe UI", 32.0F, FontStyle.Bold, GraphicsUnit.Point)
                    Using sf As New StringFormat() With {
                            .Alignment     = StringAlignment.Center,
                            .LineAlignment = StringAlignment.Center}
                    Using tb As New SolidBrush(Color.White)
                        g.DrawString(initials, fnt, tb,
                            New RectangleF(innerRect.X, innerRect.Y,
                                           innerRect.Width, innerRect.Height), sf)
                    End Using : End Using : End Using
                End Using
            End Sub

        ' lblAvatarInitials is hidden — used only as a data store for the initials string
        lblAvatarInitials.Visible = False

        ' Stat cards use plain white background (no custom paint needed)

        ' ── Load user data ────────────────────────────────────────────────
        If GlobalVariables.CurrentUser IsNot Nothing Then
            Dim u = GlobalVariables.CurrentUser
            txtUsername.Text  = u.Username
            txtEmail.Text     = u.Email
            txtFullName.Text  = If(u.FullName <> "", u.FullName, "")
            txtStudentID.Text = If(u.StudentID <> "", u.StudentID, "")
            _avatarPath       = If(u.ProfilePicturePath <> "", u.ProfilePicturePath, "")

            lblAvatarInitials.Text = u.Initials
            lblDisplayName.Text    = u.DisplayName
            lblRoleBadge.Text      = If(u.Role <> "", u.Role, "Student")
            lblMemberSince.Text    = If(u.CreatedAt > DateTime.MinValue,
                $"Member since {u.CreatedAt:MMM yyyy}", "")

            LoadStats(u.UserID)
            pnlAvatarRing.Invalidate()
        End If

        ' ── Focus highlight on editable textboxes ─────────────────────────
        For Each txt As TextBox In {txtEmail, txtFullName, txtStudentID,
                                     txtCurrentPassword, txtNewPassword, txtConfirmPassword}
            AddHandler txt.Enter, Sub(s As Object, ev As EventArgs)
                                      DirectCast(s, TextBox).BackColor = ColorTranslator.FromHtml("#EEF2FF")
                                  End Sub
            AddHandler txt.Leave, Sub(s As Object, ev As EventArgs)
                                      DirectCast(s, TextBox).BackColor = Color.White
                                  End Sub
        Next

        CenterCard()
        AddHandler Me.Resize, Sub(s As Object, ev As EventArgs) CenterCard()
        AddHandler pnlRight.Resize, Sub(s As Object, ev As EventArgs) CenterCard()
    End Sub

    ' ── Center card — repositions content panel when right panel resizes ─────
    Private Sub CenterCard()
        If pnlRight Is Nothing OrElse pnlRightContent Is Nothing Then Return
        Dim x As Integer = Math.Max(40, (pnlRight.ClientSize.Width - pnlRightContent.Width) \ 2)
        pnlRightContent.Location = New Point(x, 36)
        pnlCenter.Invalidate()
    End Sub

    ' ── Load stats from DB ────────────────────────────────────────────────────
    Private Sub LoadStats(userID As Integer)
        Try
            Dim total As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UID",
                    {New MySqlParameter("@UID", userID)}))

            Dim done As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UID AND Status='Completed'",
                    {New MySqlParameter("@UID", userID)}))

            Dim weekDone As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UID AND Status='Completed' AND CompletedAt>=@W",
                    {New MySqlParameter("@UID", userID),
                     New MySqlParameter("@W", DateTime.Today.AddDays(-7))}))

            lblStatTasks.Text  = total.ToString()
            lblStatDone.Text   = done.ToString()
            lblStatStreak.Text = weekDone.ToString()

            ' Update info chips
            Dim u = GlobalVariables.CurrentUser
            If u IsNot Nothing Then
                lblChipEmail.Text     = "  " & If(u.Email <> "", u.Email, "-")
                lblChipStudentID.Text = "  " & If(u.StudentID <> "", u.StudentID, "No Student ID")
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Profile stats error: " & ex.Message)
        End Try
    End Sub

    ' ── Upload profile picture ────────────────────────────────────────────────
    Private Sub btnUploadPicture_Click(sender As Object, e As EventArgs) Handles btnUploadPicture.Click
        Using ofd As New OpenFileDialog()
            ofd.Title  = "Select Profile Picture"
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp"
            If ofd.ShowDialog() = DialogResult.OK Then
                _avatarPath = ofd.FileName
                pnlAvatarRing.Invalidate()
            End If
        End Using
    End Sub

    ' ── Save changes ──────────────────────────────────────────────────────────
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If GlobalVariables.CurrentUser Is Nothing Then Return

        Dim email As String = txtEmail.Text.Trim()
        If Not email.Contains("@") OrElse Not email.Contains(".") Then
            SetError(txtEmail, True)
            MessageBox.Show("Please enter a valid email address.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        SetError(txtEmail, False)

        Try
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Users SET Email=@Email, FullName=@FullName, StudentID=@StudentID, ProfilePicturePath=@PicPath WHERE UserID=@UserID",
                {New MySqlParameter("@Email",     email),
                 New MySqlParameter("@FullName",  txtFullName.Text.Trim()),
                 New MySqlParameter("@StudentID", txtStudentID.Text.Trim()),
                 New MySqlParameter("@PicPath",   If(_avatarPath = "", CObj(DBNull.Value), _avatarPath)),
                 New MySqlParameter("@UserID",    GlobalVariables.CurrentUser.UserID)})

            GlobalVariables.CurrentUser.Email     = email
            GlobalVariables.CurrentUser.FullName  = txtFullName.Text.Trim()
            GlobalVariables.CurrentUser.StudentID = txtStudentID.Text.Trim()
            GlobalVariables.CurrentUser.ProfilePicturePath = _avatarPath

            Dim displayName As String = GlobalVariables.CurrentUser.DisplayName
            lblDisplayName.Text    = displayName
            lblAvatarInitials.Text = GlobalVariables.CurrentUser.Initials

            ' Refresh chips
            lblChipEmail.Text     = "  " & email
            lblChipStudentID.Text = "  " & If(txtStudentID.Text.Trim() <> "", txtStudentID.Text.Trim(), "No Student ID")

            If txtCurrentPassword.Text.Length > 0 OrElse txtNewPassword.Text.Length > 0 Then
                If Not ChangePassword() Then Return
            End If

            MessageBox.Show("Profile updated successfully! " & Chr(10) & "Changes saved.", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error saving profile: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ' ── Change password ───────────────────────────────────────────────────────
    Private Function ChangePassword() As Boolean
        If txtCurrentPassword.Text = "" Then
            MessageBox.Show("Enter your current password to change it.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If txtNewPassword.Text.Length < 6 Then
            SetError(txtNewPassword, True)
            MessageBox.Show("New password must be at least 6 characters.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If txtNewPassword.Text <> txtConfirmPassword.Text Then
            SetError(txtConfirmPassword, True)
            MessageBox.Show("New passwords do not match.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If

        Dim dtHash As DataTable = DatabaseHelper.GetDataTable(
            "SELECT PasswordHash FROM Users WHERE UserID=@UserID",
            {New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})

        If dtHash Is Nothing OrElse dtHash.Rows.Count = 0 OrElse
           Not SecurityHelper.VerifyPassword(txtCurrentPassword.Text, dtHash.Rows(0)("PasswordHash").ToString()) Then
            SetError(txtCurrentPassword, True)
            MessageBox.Show("Current password is incorrect.", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End If

        DatabaseHelper.ExecuteNonQuery(
            "UPDATE Users SET PasswordHash=@Hash WHERE UserID=@UserID",
            {New MySqlParameter("@Hash",   SecurityHelper.HashPassword(txtNewPassword.Text)),
             New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)})

        SetError(txtCurrentPassword, False)
        SetError(txtNewPassword, False)
        SetError(txtConfirmPassword, False)
        Return True
    End Function

    ' ── Helpers ───────────────────────────────────────────────────────────────
    Private Sub SetError(txt As TextBox, hasError As Boolean)
        txt.BackColor = If(hasError, ColorTranslator.FromHtml("#FEF2F2"), Color.White)
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnMinimize_Click(sender As Object, e As EventArgs) Handles btnMinimize.Click
        Me.WindowState = FormWindowState.Minimized
    End Sub

End Class
