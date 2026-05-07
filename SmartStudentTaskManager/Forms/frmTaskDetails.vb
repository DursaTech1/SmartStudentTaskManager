Imports MySql.Data.MySqlClient
Imports System.Runtime.InteropServices

Public Class frmTaskDetails
    Private taskID As Integer
    Private dgvSubtasks As DataGridView
    Private txtNewSubtask As TextBox
    Private btnAddSubtask As Button
    Private btnDeleteSubtask As Button
    Private btnMarkAllDone As Button
    Private lblSubtaskProgress As Label

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

    Private Sub btnCloseWindow_Click(sender As Object, e As EventArgs) Handles btnCloseWindow.Click
        Me.Close()
    End Sub

    Public Sub New(ByVal taskID As Integer)
        InitializeComponent()
        Me.taskID = taskID
    End Sub

    Private Sub frmTaskDetails_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)
        EnsureSubtaskUi()
        LoadTaskDetails()
        LoadSubtasks()
    End Sub

    Private Sub LoadTaskDetails()
        Try
            Dim query As String = "SELECT * FROM Tasks WHERE TaskID = @TaskID AND UserID = @UserID"
            Dim parameters As MySqlParameter() = {
                New MySqlParameter("@TaskID", taskID),
                New MySqlParameter("@UserID", GlobalVariables.CurrentUser.UserID)
            }
            Dim dt As DataTable = DatabaseHelper.GetDataTable(query, parameters)
            If dt.Rows.Count = 0 Then Return

            Dim row As DataRow = dt.Rows(0)

            lblTaskID.Text = "Task #" & row("TaskID").ToString()
            lblTitle.Text = row("Title").ToString()
            lblDescription.Text = If(row("Description").ToString().Trim() = "",
                                     "(No description)",
                                     row("Description").ToString())

            lblCategory.Text = If(IsDBNull(row("Category")), "General", row("Category").ToString())
            lblDueDate.Text = Convert.ToDateTime(row("DueDate")).ToString("MMM dd, yyyy  (dddd)")
            lblCreated.Text = Convert.ToDateTime(row("CreatedAt")).ToString("MMM dd, yyyy  hh:mm tt")

            lblCompleted.Text = If(row("CompletedAt") IsNot DBNull.Value,
                                   Convert.ToDateTime(row("CompletedAt")).ToString("MMM dd, yyyy  hh:mm tt"),
                                   "Not yet completed")

            lblRecurring.Text = If(Not IsDBNull(row("IsRecurring")) AndAlso Convert.ToBoolean(row("IsRecurring")),
                                   "Yes (Weekly)", "No")

            ' Priority badge
            Dim priority As String = row("Priority").ToString()
            lblPriority.Text = priority
            Select Case priority
                Case "High"
                    lblPriority.BackColor = ThemeManager.PriorityHighBg
                    lblPriority.ForeColor = ThemeManager.PriorityHighFg
                Case "Medium"
                    lblPriority.BackColor = ThemeManager.PriorityMediumBg
                    lblPriority.ForeColor = ThemeManager.PriorityMediumFg
                Case Else
                    lblPriority.BackColor = ThemeManager.PriorityLowBg
                    lblPriority.ForeColor = ThemeManager.PriorityLowFg
            End Select

            ' Status badge
            Dim status As String = row("Status").ToString()
            lblStatus.Text = status
            If status = "Completed" Then
                lblStatus.BackColor = ThemeManager.PriorityLowBg
                lblStatus.ForeColor = ThemeManager.PriorityLowFg
            Else
                lblStatus.BackColor = ThemeManager.PriorityMediumBg
                lblStatus.ForeColor = ThemeManager.PriorityMediumFg
            End If

            ' Overdue warning on due date
            If status = "Pending" AndAlso Convert.ToDateTime(row("DueDate")) < DateTime.Now Then
                lblDueDate.ForeColor = ThemeManager.DangerColor
                lblDueDate.Text &= "  ⚠ OVERDUE"
            End If

            ' Notes
            If Not IsDBNull(row("Notes")) AndAlso row("Notes").ToString().Trim() <> "" Then
                lblNotesHeader.Visible = True
                lblNotesVal.Text = row("Notes").ToString()
                lblNotesVal.Visible = True
                ' Push close button down
                btnClose.Top = lblNotesVal.Bottom + 20
            End If

        Catch ex As Exception
            MessageBox.Show("Error loading task details: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub EnsureSubtaskUi()
        If dgvSubtasks IsNot Nothing Then Return

        ' Expand form to two-column layout
        Me.MinimumSize = New Size(1020, 600)
        Me.ClientSize = New Size(1020, 600)

        Dim grp As New GroupBox() With {
            .Name = "grpSubtasks",
            .Text = "Subtasks / Checklist",
            .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
            .Location = New Point(600, 44),
            .Size = New Size(400, 556),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        }

        txtNewSubtask = New TextBox() With {
            .Name = "txtNewSubtask",
            .PlaceholderText = "New subtask title…",
            .Location = New Point(12, 36),
            .Size = New Size(264, 34),
            .Font = New Font("Segoe UI", 10.5F),
            .BorderStyle = BorderStyle.FixedSingle,
            .Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right
        }

        btnAddSubtask = New Button() With {
            .Name = "btnAddSubtask",
            .Text = "＋ Add",
            .Location = New Point(284, 34),
            .Size = New Size(100, 38),
            .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
            .Cursor = Cursors.Hand,
            .FlatStyle = FlatStyle.Flat,
            .BackColor = ThemeManager.PrimaryColor,
            .ForeColor = Color.White,
            .Anchor = AnchorStyles.Top Or AnchorStyles.Right
        }
        btnAddSubtask.FlatAppearance.BorderSize = 0
        AddHandler btnAddSubtask.Click, AddressOf btnAddSubtask_Click

        dgvSubtasks = New DataGridView() With {
            .Name = "dgvSubtasks",
            .Location = New Point(12, 82),
            .Size = New Size(372, 390),
            .Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right,
            .AllowUserToAddRows = False,
            .AllowUserToDeleteRows = False,
            .ReadOnly = False,
            .MultiSelect = False,
            .SelectionMode = DataGridViewSelectionMode.FullRowSelect,
            .RowHeadersVisible = False,
            .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        }
        ThemeManager.StyleDataGridView(dgvSubtasks)
        AddHandler dgvSubtasks.CellContentClick, AddressOf dgvSubtasks_CellContentClick

        btnDeleteSubtask = New Button() With {
            .Name = "btnDeleteSubtask",
            .Text = "🗑 Delete",
            .Location = New Point(12, 482),
            .Size = New Size(120, 40),
            .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
            .Cursor = Cursors.Hand,
            .FlatStyle = FlatStyle.Flat,
            .BackColor = ThemeManager.DangerColor,
            .ForeColor = Color.White,
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        }
        btnDeleteSubtask.FlatAppearance.BorderSize = 0
        AddHandler btnDeleteSubtask.Click, AddressOf btnDeleteSubtask_Click

        btnMarkAllDone = New Button() With {
            .Name = "btnMarkAllDone",
            .Text = "✔ Mark All Done",
            .Location = New Point(142, 482),
            .Size = New Size(160, 40),
            .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
            .Cursor = Cursors.Hand,
            .FlatStyle = FlatStyle.Flat,
            .BackColor = ThemeManager.SuccessColor,
            .ForeColor = Color.White,
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        }
        btnMarkAllDone.FlatAppearance.BorderSize = 0
        AddHandler btnMarkAllDone.Click, AddressOf btnMarkAllDone_Click

        lblSubtaskProgress = New Label() With {
            .Name = "lblSubtaskProgress",
            .Text = "0/0",
            .Location = New Point(312, 492),
            .Size = New Size(72, 24),
            .Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold),
            .ForeColor = ThemeManager.PrimaryColor,
            .TextAlign = ContentAlignment.MiddleRight,
            .Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        }

        grp.Controls.AddRange({txtNewSubtask, btnAddSubtask, dgvSubtasks,
                                btnDeleteSubtask, btnMarkAllDone, lblSubtaskProgress})
        Me.Controls.Add(grp)
    End Sub

    Private Sub LoadSubtasks()
        If dgvSubtasks Is Nothing OrElse GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT SubtaskID, Title, IsCompleted FROM Subtasks WHERE TaskID = @TaskID ORDER BY SubtaskID ASC",
                {New MySqlParameter("@TaskID", taskID)})

            dgvSubtasks.DataSource = dt
            If dgvSubtasks.Columns.Contains("SubtaskID") Then dgvSubtasks.Columns("SubtaskID").Visible = False
            If dgvSubtasks.Columns.Contains("IsCompleted") Then
                dgvSubtasks.Columns("IsCompleted").HeaderText = "Done"
                dgvSubtasks.Columns("IsCompleted").ReadOnly = False
            End If
            If dgvSubtasks.Columns.Contains("Title") Then dgvSubtasks.Columns("Title").ReadOnly = True
            UpdateSubtaskProgress(dt)
        Catch ex As Exception
            MessageBox.Show("Error loading subtasks: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub UpdateSubtaskProgress(dt As DataTable)
        If lblSubtaskProgress Is Nothing OrElse dt Is Nothing Then Return
        Dim total As Integer = dt.Rows.Count
        Dim done As Integer = dt.Rows.Cast(Of DataRow)().Count(
            Function(r) r.Table.Columns.Contains("IsCompleted") AndAlso
                        r("IsCompleted") IsNot DBNull.Value AndAlso
                        Convert.ToBoolean(r("IsCompleted")))
        lblSubtaskProgress.Text = $"{done}/{total}"
    End Sub

    Private Sub btnAddSubtask_Click(sender As Object, e As EventArgs)
        Dim title As String = If(txtNewSubtask IsNot Nothing, txtNewSubtask.Text.Trim(), "")
        If title = "" Then Return
        Try
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Subtasks (TaskID, Title, IsCompleted) VALUES (@TaskID, @Title, FALSE)",
                {New MySqlParameter("@TaskID", taskID), New MySqlParameter("@Title", title)})
            txtNewSubtask.Text = ""
            LoadSubtasks()
        Catch ex As Exception
            MessageBox.Show("Error adding subtask: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnDeleteSubtask_Click(sender As Object, e As EventArgs)
        If dgvSubtasks Is Nothing OrElse dgvSubtasks.CurrentRow Is Nothing Then Return
        If Not dgvSubtasks.Columns.Contains("SubtaskID") Then Return
        Try
            Dim id As Integer = Convert.ToInt32(dgvSubtasks.CurrentRow.Cells("SubtaskID").Value)
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Subtasks WHERE SubtaskID = @SubtaskID AND TaskID = @TaskID",
                {New MySqlParameter("@SubtaskID", id), New MySqlParameter("@TaskID", taskID)})
            LoadSubtasks()
        Catch ex As Exception
            MessageBox.Show("Error deleting subtask: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnMarkAllDone_Click(sender As Object, e As EventArgs)
        Try
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Subtasks SET IsCompleted = TRUE WHERE TaskID = @TaskID",
                {New MySqlParameter("@TaskID", taskID)})
            LoadSubtasks()
        Catch ex As Exception
            MessageBox.Show("Error updating subtasks: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvSubtasks_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
        If dgvSubtasks Is Nothing OrElse e.RowIndex < 0 Then Return
        If Not dgvSubtasks.Columns.Contains("IsCompleted") OrElse
           dgvSubtasks.Columns(e.ColumnIndex).Name <> "IsCompleted" Then Return
        If Not dgvSubtasks.Columns.Contains("SubtaskID") Then Return
        Try
            Dim id As Integer = Convert.ToInt32(dgvSubtasks.Rows(e.RowIndex).Cells("SubtaskID").Value)
            Dim newValue As Boolean = Convert.ToBoolean(dgvSubtasks.Rows(e.RowIndex).Cells("IsCompleted").EditedFormattedValue)
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Subtasks SET IsCompleted = @IsCompleted WHERE SubtaskID = @SubtaskID AND TaskID = @TaskID",
                {New MySqlParameter("@IsCompleted", newValue),
                 New MySqlParameter("@SubtaskID", id),
                 New MySqlParameter("@TaskID", taskID)})
            LoadSubtasks()
        Catch ex As Exception
            MessageBox.Show("Error updating subtask: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class
