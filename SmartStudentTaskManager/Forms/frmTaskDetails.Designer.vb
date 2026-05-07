<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaskDetails
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
        btnCloseWindow = New Button()
        lblFormTitle = New Label()
        pnlContent = New Panel()
        lblTaskID = New Label()
        lblTitle = New Label()
        lblDescription = New Label()
        pnlMeta = New Panel()
        lblCategoryLbl = New Label()
        lblCategory = New Label()
        lblDueDateLbl = New Label()
        lblDueDate = New Label()
        lblPriorityLbl = New Label()
        lblPriority = New Label()
        lblStatusLbl = New Label()
        lblStatus = New Label()
        lblRecurringLbl = New Label()
        lblRecurring = New Label()
        lblCreatedLbl = New Label()
        lblCreated = New Label()
        lblCompletedLbl = New Label()
        lblCompleted = New Label()
        lblNotesHeader = New Label()
        lblNotesVal = New Label()
        btnClose = New Button()
        pnlTitleBar.SuspendLayout()
        pnlContent.SuspendLayout()
        pnlMeta.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlTitleBar
        ' 
        pnlTitleBar.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlTitleBar.Controls.Add(btnCloseWindow)
        pnlTitleBar.Controls.Add(lblFormTitle)
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Location = New Point(0, 0)
        pnlTitleBar.Name = "pnlTitleBar"
        pnlTitleBar.Size = New Size(1225, 44)
        pnlTitleBar.TabIndex = 0
        ' 
        ' btnCloseWindow
        ' 
        btnCloseWindow.Dock = DockStyle.Right
        btnCloseWindow.FlatAppearance.BorderSize = 0
        btnCloseWindow.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        btnCloseWindow.FlatStyle = FlatStyle.Flat
        btnCloseWindow.Font = New Font("Segoe UI", 11F, FontStyle.Bold)
        btnCloseWindow.ForeColor = Color.White
        btnCloseWindow.Location = New Point(1181, 0)
        btnCloseWindow.Name = "btnCloseWindow"
        btnCloseWindow.Size = New Size(44, 44)
        btnCloseWindow.TabIndex = 99
        btnCloseWindow.Text = "✕"
        ' 
        ' lblFormTitle
        ' 
        lblFormTitle.AutoSize = True
        lblFormTitle.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblFormTitle.ForeColor = Color.White
        lblFormTitle.Location = New Point(16, 13)
        lblFormTitle.Name = "lblFormTitle"
        lblFormTitle.Size = New Size(118, 28)
        lblFormTitle.TabIndex = 0
        lblFormTitle.Text = "Task Details"
        ' 
        ' pnlContent
        ' 
        pnlContent.BackColor = Color.White
        pnlContent.Controls.Add(lblTaskID)
        pnlContent.Controls.Add(lblTitle)
        pnlContent.Controls.Add(lblDescription)
        pnlContent.Controls.Add(pnlMeta)
        pnlContent.Controls.Add(lblNotesHeader)
        pnlContent.Controls.Add(lblNotesVal)
        pnlContent.Controls.Add(btnClose)
        pnlContent.Dock = DockStyle.Fill
        pnlContent.Location = New Point(0, 44)
        pnlContent.Name = "pnlContent"
        pnlContent.Size = New Size(1225, 693)
        pnlContent.TabIndex = 1
        ' 
        ' lblTaskID
        ' 
        lblTaskID.AutoSize = True
        lblTaskID.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblTaskID.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblTaskID.Location = New Point(32, 20)
        lblTaskID.Name = "lblTaskID"
        lblTaskID.Size = New Size(97, 25)
        lblTaskID.TabIndex = 1
        lblTaskID.Text = "Task ID: —"
        ' 
        ' lblTitle
        ' 
        lblTitle.Font = New Font("Segoe UI", 15F, FontStyle.Bold)
        lblTitle.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblTitle.Location = New Point(32, 44)
        lblTitle.Name = "lblTitle"
        lblTitle.Size = New Size(516, 34)
        lblTitle.TabIndex = 2
        lblTitle.Text = "Task Title"
        ' 
        ' lblDescription
        ' 
        lblDescription.Font = New Font("Segoe UI", 10F)
        lblDescription.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        lblDescription.Location = New Point(32, 86)
        lblDescription.Name = "lblDescription"
        lblDescription.Size = New Size(516, 64)
        lblDescription.TabIndex = 3
        lblDescription.Text = "Description"
        ' 
        ' pnlMeta
        ' 
        pnlMeta.BackColor = Color.White
        pnlMeta.Controls.Add(lblCategoryLbl)
        pnlMeta.Controls.Add(lblCategory)
        pnlMeta.Controls.Add(lblDueDateLbl)
        pnlMeta.Controls.Add(lblDueDate)
        pnlMeta.Controls.Add(lblPriorityLbl)
        pnlMeta.Controls.Add(lblPriority)
        pnlMeta.Controls.Add(lblStatusLbl)
        pnlMeta.Controls.Add(lblStatus)
        pnlMeta.Controls.Add(lblRecurringLbl)
        pnlMeta.Controls.Add(lblRecurring)
        pnlMeta.Controls.Add(lblCreatedLbl)
        pnlMeta.Controls.Add(lblCreated)
        pnlMeta.Controls.Add(lblCompletedLbl)
        pnlMeta.Controls.Add(lblCompleted)
        pnlMeta.Location = New Point(32, 162)
        pnlMeta.Name = "pnlMeta"
        pnlMeta.Size = New Size(516, 252)
        pnlMeta.TabIndex = 4
        ' 
        ' lblCategoryLbl
        ' 
        lblCategoryLbl.AutoSize = True
        lblCategoryLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblCategoryLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblCategoryLbl.Location = New Point(0, 0)
        lblCategoryLbl.Name = "lblCategoryLbl"
        lblCategoryLbl.Size = New Size(101, 25)
        lblCategoryLbl.TabIndex = 0
        lblCategoryLbl.Text = "CATEGORY"
        ' 
        ' lblCategory
        ' 
        lblCategory.AutoSize = True
        lblCategory.Font = New Font("Segoe UI", 10.5F)
        lblCategory.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblCategory.Location = New Point(0, 18)
        lblCategory.Name = "lblCategory"
        lblCategory.Size = New Size(34, 30)
        lblCategory.TabIndex = 1
        lblCategory.Text = "—"
        ' 
        ' lblDueDateLbl
        ' 
        lblDueDateLbl.AutoSize = True
        lblDueDateLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblDueDateLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblDueDateLbl.Location = New Point(258, 0)
        lblDueDateLbl.Name = "lblDueDateLbl"
        lblDueDateLbl.Size = New Size(95, 25)
        lblDueDateLbl.TabIndex = 2
        lblDueDateLbl.Text = "DUE DATE"
        ' 
        ' lblDueDate
        ' 
        lblDueDate.Font = New Font("Segoe UI", 10.5F)
        lblDueDate.ForeColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        lblDueDate.Location = New Point(258, 18)
        lblDueDate.Name = "lblDueDate"
        lblDueDate.Size = New Size(258, 22)
        lblDueDate.TabIndex = 3
        lblDueDate.Text = "—"
        ' 
        ' lblPriorityLbl
        ' 
        lblPriorityLbl.AutoSize = True
        lblPriorityLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblPriorityLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblPriorityLbl.Location = New Point(0, 54)
        lblPriorityLbl.Name = "lblPriorityLbl"
        lblPriorityLbl.Size = New Size(89, 25)
        lblPriorityLbl.TabIndex = 4
        lblPriorityLbl.Text = "PRIORITY"
        ' 
        ' lblPriority
        ' 
        lblPriority.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblPriority.Location = New Point(0, 72)
        lblPriority.Name = "lblPriority"
        lblPriority.Padding = New Padding(10, 2, 10, 2)
        lblPriority.Size = New Size(110, 26)
        lblPriority.TabIndex = 6
        lblPriority.Text = "Medium"
        lblPriority.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblStatusLbl
        ' 
        lblStatusLbl.AutoSize = True
        lblStatusLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblStatusLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblStatusLbl.Location = New Point(258, 54)
        lblStatusLbl.Name = "lblStatusLbl"
        lblStatusLbl.Size = New Size(75, 25)
        lblStatusLbl.TabIndex = 7
        lblStatusLbl.Text = "STATUS"
        ' 
        ' lblStatus
        ' 
        lblStatus.Font = New Font("Segoe UI Semibold", 10F, FontStyle.Bold)
        lblStatus.Location = New Point(258, 72)
        lblStatus.Name = "lblStatus"
        lblStatus.Padding = New Padding(10, 2, 10, 2)
        lblStatus.Size = New Size(110, 26)
        lblStatus.TabIndex = 7
        lblStatus.Text = "Pending"
        lblStatus.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' lblRecurringLbl
        ' 
        lblRecurringLbl.AutoSize = True
        lblRecurringLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblRecurringLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblRecurringLbl.Location = New Point(0, 108)
        lblRecurringLbl.Name = "lblRecurringLbl"
        lblRecurringLbl.Size = New Size(110, 25)
        lblRecurringLbl.TabIndex = 8
        lblRecurringLbl.Text = "RECURRING"
        ' 
        ' lblRecurring
        ' 
        lblRecurring.AutoSize = True
        lblRecurring.Font = New Font("Segoe UI", 10.5F)
        lblRecurring.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        lblRecurring.Location = New Point(0, 126)
        lblRecurring.Name = "lblRecurring"
        lblRecurring.Size = New Size(41, 30)
        lblRecurring.TabIndex = 9
        lblRecurring.Text = "No"
        ' 
        ' lblCreatedLbl
        ' 
        lblCreatedLbl.AutoSize = True
        lblCreatedLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblCreatedLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblCreatedLbl.Location = New Point(0, 162)
        lblCreatedLbl.Name = "lblCreatedLbl"
        lblCreatedLbl.Size = New Size(86, 25)
        lblCreatedLbl.TabIndex = 10
        lblCreatedLbl.Text = "CREATED"
        ' 
        ' lblCreated
        ' 
        lblCreated.AutoSize = True
        lblCreated.Font = New Font("Segoe UI", 9.5F, FontStyle.Italic)
        lblCreated.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblCreated.Location = New Point(0, 180)
        lblCreated.Name = "lblCreated"
        lblCreated.Size = New Size(31, 25)
        lblCreated.TabIndex = 11
        lblCreated.Text = "—"
        ' 
        ' lblCompletedLbl
        ' 
        lblCompletedLbl.AutoSize = True
        lblCompletedLbl.Font = New Font("Segoe UI Semibold", 9F, FontStyle.Bold)
        lblCompletedLbl.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblCompletedLbl.Location = New Point(258, 162)
        lblCompletedLbl.Name = "lblCompletedLbl"
        lblCompletedLbl.Size = New Size(115, 25)
        lblCompletedLbl.TabIndex = 12
        lblCompletedLbl.Text = "COMPLETED"
        ' 
        ' lblCompleted
        ' 
        lblCompleted.AutoSize = True
        lblCompleted.Font = New Font("Segoe UI", 9.5F, FontStyle.Italic)
        lblCompleted.ForeColor = Color.FromArgb(CByte(156), CByte(163), CByte(175))
        lblCompleted.Location = New Point(258, 180)
        lblCompleted.Name = "lblCompleted"
        lblCompleted.Size = New Size(31, 25)
        lblCompleted.TabIndex = 13
        lblCompleted.Text = "—"
        ' 
        ' lblNotesHeader
        ' 
        lblNotesHeader.AutoSize = True
        lblNotesHeader.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblNotesHeader.ForeColor = Color.FromArgb(CByte(107), CByte(114), CByte(128))
        lblNotesHeader.Location = New Point(32, 426)
        lblNotesHeader.Name = "lblNotesHeader"
        lblNotesHeader.Size = New Size(70, 25)
        lblNotesHeader.TabIndex = 11
        lblNotesHeader.Text = "NOTES"
        lblNotesHeader.Visible = False
        ' 
        ' lblNotesVal
        ' 
        lblNotesVal.Font = New Font("Segoe UI", 10F)
        lblNotesVal.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        lblNotesVal.Location = New Point(32, 446)
        lblNotesVal.Name = "lblNotesVal"
        lblNotesVal.Size = New Size(516, 52)
        lblNotesVal.TabIndex = 12
        lblNotesVal.Visible = False
        ' 
        ' btnClose
        ' 
        btnClose.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        btnClose.Cursor = Cursors.Hand
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(67), CByte(56), CByte(202))
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(140, 510)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(300, 46)
        btnClose.TabIndex = 13
        btnClose.Text = "Close"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' frmTaskDetails
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(1225, 737)
        Controls.Add(pnlContent)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize = New Size(580, 600)
        Name = "frmTaskDetails"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Task Details"
        WindowState = FormWindowState.Maximized
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlContent.ResumeLayout(False)
        pnlContent.PerformLayout()
        pnlMeta.ResumeLayout(False)
        pnlMeta.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents pnlTitleBar    As Panel
    Friend WithEvents btnCloseWindow As Button
    Friend WithEvents lblFormTitle   As Label
    Friend WithEvents pnlContent     As Panel
    Friend WithEvents lblTaskID      As Label
    Friend WithEvents lblTitle       As Label
    Friend WithEvents lblDescription As Label
    Friend WithEvents pnlMeta        As Panel
    Friend WithEvents lblCategoryLbl As Label
    Friend WithEvents lblCategory    As Label
    Friend WithEvents lblDueDateLbl  As Label
    Friend WithEvents lblDueDate     As Label
    Friend WithEvents lblPriorityLbl As Label
    Friend WithEvents lblPriority    As Label
    Friend WithEvents lblStatusLbl   As Label
    Friend WithEvents lblStatus      As Label
    Friend WithEvents lblRecurringLbl As Label
    Friend WithEvents lblRecurring   As Label
    Friend WithEvents lblCreatedLbl  As Label
    Friend WithEvents lblCreated     As Label
    Friend WithEvents lblCompletedLbl As Label
    Friend WithEvents lblCompleted   As Label
    Friend WithEvents lblNotesHeader As Label
    Friend WithEvents lblNotesVal    As Label
    Friend WithEvents btnClose       As Button
End Class
