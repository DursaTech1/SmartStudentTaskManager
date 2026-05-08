Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmAddEditTask
    Private isEditMode As Boolean = False
    Private editTaskID As Integer = 0

    ' Character counter labels (created in code, not designer)
    Private lblTitleCount As Label
    Private lblDescCount As Label
    Private lblNotesCount As Label
    Private lblTagCount As Label

    Private Const MaxTitle As Integer = 200
    Private Const MaxDesc As Integer = 1000
    Private Const MaxNotes As Integer = 2000
    Private Const MaxTag As Integer = 30

    ' Form Dragging API
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

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Public Sub New()
        InitializeComponent()
        isEditMode = False
    End Sub

    Public Sub New(ByVal taskID As Integer)
        InitializeComponent()
        isEditMode = True
        editTaskID = taskID
        LoadTaskData()
        Me.Text = "Edit Task"
        btnSave.Text = "Update Task"
    End Sub

    Private Sub LoadTaskData()
        Try
            Dim query As String = "SELECT * FROM Tasks WHERE TaskID = @TaskID AND UserID = @UserID"
            Dim parameters As MySqlParameter() = {
                New MySqlParameter("@TaskID", editTaskID),
                New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)
            }

            Dim dt As DataTable = DatabaseHelper.GetDataTable(query, parameters)
            If dt.Rows.Count > 0 Then
                Dim row As DataRow = dt.Rows(0)
                txtTitle.Text = row("Title").ToString()
                txtDescription.Text = row("Description").ToString()
                dtpDueDate.Value = Convert.ToDateTime(row("DueDate"))
                cmbPriority.SelectedItem = row("Priority").ToString()
                cmbStatus.SelectedItem = row("Status").ToString()
                If Not IsDBNull(row("Category")) Then
                    cmbCategory.SelectedItem = row("Category").ToString()
                End If
                If Not IsDBNull(row("IsRecurring")) Then
                    chkIsRecurring.Checked = Convert.ToBoolean(row("IsRecurring"))
                End If
                If Not IsDBNull(row("Notes")) Then
                    txtNotes.Text = row("Notes").ToString()
                End If
                If dt.Columns.Contains("Tag") AndAlso Not IsDBNull(row("Tag")) Then
                    txtTag.Text = row("Tag").ToString()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show("Error loading task: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmAddEditTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)

        ' Center the card in pnlCenter
        CenterCard()
        AddHandler Me.Resize, Sub(s As Object, ev As EventArgs) CenterCard()

        ' Card shadow + rounded corners
        AddHandler pnlCard.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                ev.Graphics.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                For i As Integer = 4 To 1 Step -1
                    Dim sr As New Rectangle(i, i, pnlCard.Width - 1, pnlCard.Height - 1)
                    Using sp As New Pen(Color.FromArgb(10 * i, 0, 0, 0), 1)
                        ev.Graphics.DrawRectangle(sp, sr)
                    End Using
                Next
                Using bg As New SolidBrush(Color.White)
                    ev.Graphics.FillRectangle(bg, New Rectangle(0, 0, pnlCard.Width - 5, pnlCard.Height - 5))
                End Using
                Using bp As New Pen(ColorTranslator.FromHtml("#E5E7EB"), 1)
                    ev.Graphics.DrawRectangle(bp, New Rectangle(0, 0, pnlCard.Width - 5, pnlCard.Height - 5))
                End Using
            End Sub

        cmbPriority.Items.Clear()
        cmbPriority.Items.AddRange({"Low", "Medium", "High"})
        cmbStatus.Items.Clear()
        cmbStatus.Items.AddRange({"Pending", "Completed"})

        If Not isEditMode Then
            cmbPriority.SelectedIndex = 1 ' Medium
            cmbStatus.SelectedIndex = 0 ' Pending
            cmbCategory.SelectedIndex = 0 ' General
            dtpDueDate.Value = DateTime.Today.AddDays(3)
        End If

        SetupCharacterCounters()
        UpdateAllCounters()

        ' Show "Recurs weekly" label in edit mode when recurring
        If isEditMode AndAlso chkIsRecurring.Checked Then
            lblRecurringNote.Visible = True
        End If
    End Sub

    Private Sub CenterCard()
        If pnlCard Is Nothing OrElse pnlCenter Is Nothing Then Return
        Dim x As Integer = Math.Max(0, (pnlCenter.ClientSize.Width - pnlCard.Width) \ 2)
        Dim y As Integer = Math.Max(20, (pnlCenter.ClientSize.Height - pnlCard.Height) \ 2)
        pnlCard.Location = New Point(x, y)
    End Sub

    Private Sub SetupCharacterCounters()
        ' Title counter
        lblTitleCount = New Label() With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 8.5F),
            .ForeColor = ThemeManager.MutedTextColor,
            .Location = New Point(txtTitle.Right - 70, txtTitle.Bottom + 2),
            .Name = "lblTitleCount"
        }
        pnlCard.Controls.Add(lblTitleCount)
        lblTitleCount.BringToFront()

        ' Description counter
        lblDescCount = New Label() With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 8.5F),
            .ForeColor = ThemeManager.MutedTextColor,
            .Location = New Point(txtDescription.Right - 80, txtDescription.Bottom + 2),
            .Name = "lblDescCount"
        }
        pnlCard.Controls.Add(lblDescCount)
        lblDescCount.BringToFront()

        ' Notes counter
        lblNotesCount = New Label() With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 8.5F),
            .ForeColor = ThemeManager.MutedTextColor,
            .Location = New Point(txtNotes.Right - 90, txtNotes.Bottom + 2),
            .Name = "lblNotesCount"
        }
        pnlCard.Controls.Add(lblNotesCount)
        lblNotesCount.BringToFront()

        ' Tag counter
        lblTagCount = New Label() With {
            .AutoSize = True,
            .Font = New Font("Segoe UI", 8.5F),
            .ForeColor = ThemeManager.MutedTextColor,
            .Location = New Point(txtTag.Right - 60, txtTag.Bottom + 2),
            .Name = "lblTagCount"
        }
        pnlCard.Controls.Add(lblTagCount)
        lblTagCount.BringToFront()

        ' Wire up events
        AddHandler txtTitle.TextChanged, AddressOf UpdateAllCounters
        AddHandler txtDescription.TextChanged, AddressOf UpdateAllCounters
        AddHandler txtNotes.TextChanged, AddressOf UpdateAllCounters
        AddHandler txtTag.TextChanged, AddressOf UpdateAllCounters

        ' Enforce max lengths
        txtTitle.MaxLength = MaxTitle
        txtDescription.MaxLength = MaxDesc
        txtNotes.MaxLength = MaxNotes
        txtTag.MaxLength = MaxTag
    End Sub

    Private Sub UpdateAllCounters(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        UpdateCounter(lblTitleCount, txtTitle.Text.Length, MaxTitle)
        UpdateCounter(lblDescCount, txtDescription.Text.Length, MaxDesc)
        UpdateCounter(lblNotesCount, txtNotes.Text.Length, MaxNotes)
        UpdateCounter(lblTagCount, txtTag.Text.Length, MaxTag)
    End Sub

    Private Sub UpdateCounter(lbl As Label, current As Integer, max As Integer)
        If lbl Is Nothing Then Return
        lbl.Text = $"{current}/{max}"
        lbl.ForeColor = If(current >= max, ThemeManager.DangerColor, ThemeManager.MutedTextColor)
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If ValidateTask() Then
            Try
                Dim title As String = txtTitle.Text.Trim()
                Dim description As String = txtDescription.Text.Trim()
                Dim dueDate As DateTime = dtpDueDate.Value
                Dim priority As String = cmbPriority.SelectedItem.ToString()
                Dim status As String = cmbStatus.SelectedItem.ToString()
                Dim category As String = If(cmbCategory.SelectedItem IsNot Nothing, cmbCategory.SelectedItem.ToString(), "General")
                Dim isRecurring As Boolean = chkIsRecurring.Checked
                Dim notes As String = txtNotes.Text.Trim()
                Dim tag As String = txtTag.Text.Trim()

                If isEditMode Then
                    ' Update existing task
                    Dim query As String = "UPDATE Tasks SET Title = @Title, Description = @Description, DueDate = @DueDate, Priority = @Priority, Status = @Status, Category = @Category, IsRecurring = @IsRecurring, Notes = @Notes, Tag = @Tag WHERE TaskID = @TaskID AND UserID = @UserID"
                    Dim parameters As MySqlParameter() = {
                        New MySqlParameter("@Title", title),
                        New MySqlParameter("@Description", description),
                        New MySqlParameter("@DueDate", dueDate),
                        New MySqlParameter("@Priority", priority),
                        New MySqlParameter("@Status", status),
                        New MySqlParameter("@Category", category),
                        New MySqlParameter("@IsRecurring", isRecurring),
                        New MySqlParameter("@Notes", If(notes = "", CObj(DBNull.Value), notes)),
                        New MySqlParameter("@Tag", If(tag = "", CObj(DBNull.Value), tag)),
                        New MySqlParameter("@TaskID", editTaskID),
                        New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)
                    }

                    DatabaseHelper.ExecuteNonQuery(query, parameters)
                    MessageBox.Show("Task updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    ' Add new task
                    Dim query As String = "INSERT INTO Tasks (UserID, Title, Description, DueDate, Priority, Status, Category, IsRecurring, Notes, Tag) VALUES (@UserID, @Title, @Description, @DueDate, @Priority, @Status, @Category, @IsRecurring, @Notes, @Tag)"
                    Dim parameters As MySqlParameter() = {
                        New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID),
                        New MySqlParameter("@Title", title),
                        New MySqlParameter("@Description", description),
                        New MySqlParameter("@DueDate", dueDate),
                        New MySqlParameter("@Priority", priority),
                        New MySqlParameter("@Status", status),
                        New MySqlParameter("@Category", category),
                        New MySqlParameter("@IsRecurring", isRecurring),
                        New MySqlParameter("@Notes", If(notes = "", CObj(DBNull.Value), notes)),
                        New MySqlParameter("@Tag", If(tag = "", CObj(DBNull.Value), tag))
                    }

                    DatabaseHelper.ExecuteNonQuery(query, parameters)
                    MessageBox.Show("Task added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If

                Me.DialogResult = DialogResult.OK
                Me.Close()
            Catch ex As Exception
                MessageBox.Show("Error saving task: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Function ValidateTask() As Boolean
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MessageBox.Show("Please enter a task title.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus() : Return False
        End If
        If txtTitle.Text.Trim().Length > 200 Then
            MessageBox.Show("Task title must be 200 characters or fewer.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus() : Return False
        End If
        If dtpDueDate.Value.Date < DateTime.Today Then
            If MessageBox.Show("Due date is in the past. Continue anyway?", "Past Due Date",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then Return False
        End If
        If cmbPriority.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a priority.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        If cmbStatus.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return False
        End If
        Return True
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub pnlCenter_Paint(sender As Object, e As PaintEventArgs) Handles pnlCenter.Paint

    End Sub

    Private Sub pnlCard_Paint(sender As Object, e As PaintEventArgs) Handles pnlCard.Paint

    End Sub

    Private Sub txtNotes_TextChanged(sender As Object, e As EventArgs) Handles txtNotes.TextChanged

    End Sub
End Class