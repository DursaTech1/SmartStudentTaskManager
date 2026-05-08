<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAddEditTask
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
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

    ' All controls declared here — built entirely in code, no drag-drop designer
    Friend WithEvents pnlTitleBar    As Panel
    Friend WithEvents btnClose        As Button
    Friend WithEvents lblFormTitle    As Label
    Friend WithEvents pnlCenter       As Panel   ' scrollable center area
    Friend WithEvents pnlCard         As Panel   ' white card centered in pnlCenter
    Friend WithEvents Label1          As Label
    Friend WithEvents txtTitle        As TextBox
    Friend WithEvents Label2          As Label
    Friend WithEvents txtDescription  As TextBox
    Friend WithEvents LabelCategory   As Label
    Friend WithEvents cmbCategory     As ComboBox
    Friend WithEvents Label3          As Label
    Friend WithEvents dtpDueDate      As DateTimePicker
    Friend WithEvents Label4          As Label
    Friend WithEvents cmbPriority     As ComboBox
    Friend WithEvents Label5          As Label
    Friend WithEvents cmbStatus       As ComboBox
    Friend WithEvents chkIsRecurring  As CheckBox
    Friend WithEvents lblRecurringNote As Label
    Friend WithEvents lblTagLabel     As Label
    Friend WithEvents txtTag          As TextBox
    Friend WithEvents lblNotes        As Label
    Friend WithEvents txtNotes        As TextBox
    Friend WithEvents btnSave         As Button
    Friend WithEvents btnCancel       As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar = New Panel()
        btnClose = New Button()
        lblFormTitle = New Label()
        pnlCenter = New Panel()
        pnlCard = New Panel()
        Label1 = New Label()
        txtTitle = New TextBox()
        Label2 = New Label()
        txtDescription = New TextBox()
        LabelCategory = New Label()
        cmbCategory = New ComboBox()
        Label3 = New Label()
        dtpDueDate = New DateTimePicker()
        Label4 = New Label()
        cmbPriority = New ComboBox()
        Label5 = New Label()
        cmbStatus = New ComboBox()
        chkIsRecurring = New CheckBox()
        lblRecurringNote = New Label()
        lblTagLabel = New Label()
        txtTag = New TextBox()
        lblNotes = New Label()
        txtNotes = New TextBox()
        btnSave = New Button()
        btnCancel = New Button()
        pnlTitleBar.SuspendLayout()
        pnlCenter.SuspendLayout()
        pnlCard.SuspendLayout()
        SuspendLayout()
        ' 
        ' pnlTitleBar
        ' 
        pnlTitleBar.BackColor = Color.FromArgb(CByte(30), CByte(27), CByte(75))
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(lblFormTitle)
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Location = New Point(0, 0)
        pnlTitleBar.Name = "pnlTitleBar"
        pnlTitleBar.Size = New Size(970, 48)
        pnlTitleBar.TabIndex = 1
        ' 
        ' btnClose
        ' 
        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(239), CByte(68), CByte(68))
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 12F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Location = New Point(922, 0)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(48, 48)
        btnClose.TabIndex = 0
        btnClose.Text = "✕"
        btnClose.UseVisualStyleBackColor = False
        ' 
        ' lblFormTitle
        ' 
        lblFormTitle.AutoSize = True
        lblFormTitle.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        lblFormTitle.ForeColor = Color.White
        lblFormTitle.Location = New Point(20, 14)
        lblFormTitle.Name = "lblFormTitle"
        lblFormTitle.Size = New Size(102, 30)
        lblFormTitle.TabIndex = 1
        lblFormTitle.Text = "Add Task"
        ' 
        ' pnlCenter
        ' 
        pnlCenter.AutoScroll = True
        pnlCenter.BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        pnlCenter.Controls.Add(pnlCard)
        pnlCenter.Dock = DockStyle.Fill
        pnlCenter.Location = New Point(0, 48)
        pnlCenter.Name = "pnlCenter"
        pnlCenter.Size = New Size(970, 652)
        pnlCenter.TabIndex = 0
        ' 
        ' pnlCard
        ' 
        pnlCard.BackColor = Color.White
        pnlCard.Controls.Add(Label1)
        pnlCard.Controls.Add(txtTitle)
        pnlCard.Controls.Add(Label2)
        pnlCard.Controls.Add(txtDescription)
        pnlCard.Controls.Add(LabelCategory)
        pnlCard.Controls.Add(cmbCategory)
        pnlCard.Controls.Add(Label3)
        pnlCard.Controls.Add(dtpDueDate)
        pnlCard.Controls.Add(Label4)
        pnlCard.Controls.Add(cmbPriority)
        pnlCard.Controls.Add(Label5)
        pnlCard.Controls.Add(cmbStatus)
        pnlCard.Controls.Add(chkIsRecurring)
        pnlCard.Controls.Add(lblRecurringNote)
        pnlCard.Controls.Add(lblTagLabel)
        pnlCard.Controls.Add(txtTag)
        pnlCard.Controls.Add(lblNotes)
        pnlCard.Controls.Add(txtNotes)
        pnlCard.Controls.Add(btnSave)
        pnlCard.Controls.Add(btnCancel)
        pnlCard.Location = New Point(0, 0)
        pnlCard.Name = "pnlCard"
        pnlCard.Padding = New Padding(40, 24, 40, 24)
        pnlCard.Size = New Size(700, 660)
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label1.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        Label1.Location = New Point(0, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(103, 25)
        Label1.TabIndex = 0
        Label1.Text = "Task Title *"
        ' 
        ' txtTitle
        ' 
        txtTitle.BorderStyle = BorderStyle.FixedSingle
        txtTitle.Font = New Font("Segoe UI", 11F)
        txtTitle.Location = New Point(0, 22)
        txtTitle.MaxLength = 200
        txtTitle.Name = "txtTitle"
        txtTitle.Size = New Size(600, 37)
        txtTitle.TabIndex = 1
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label2.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        Label2.Location = New Point(0, 76)
        Label2.Name = "Label2"
        Label2.Size = New Size(110, 25)
        Label2.TabIndex = 2
        Label2.Text = "Description"
        ' 
        ' txtDescription
        ' 
        txtDescription.BorderStyle = BorderStyle.FixedSingle
        txtDescription.Font = New Font("Segoe UI", 11F)
        txtDescription.Location = New Point(0, 98)
        txtDescription.MaxLength = 1000
        txtDescription.Multiline = True
        txtDescription.Name = "txtDescription"
        txtDescription.ScrollBars = ScrollBars.Vertical
        txtDescription.Size = New Size(600, 90)
        txtDescription.TabIndex = 3
        ' 
        ' LabelCategory
        ' 
        LabelCategory.AutoSize = True
        LabelCategory.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        LabelCategory.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        LabelCategory.Location = New Point(0, 206)
        LabelCategory.Name = "LabelCategory"
        LabelCategory.Size = New Size(91, 25)
        LabelCategory.TabIndex = 4
        LabelCategory.Text = "Category"
        ' 
        ' cmbCategory
        ' 
        cmbCategory.DropDownStyle = ComboBoxStyle.DropDownList
        cmbCategory.FlatStyle = FlatStyle.Flat
        cmbCategory.Font = New Font("Segoe UI", 11F)
        cmbCategory.Items.AddRange(New Object() {"General", "Math", "Science", "History", "Literature", "Extracurricular", "Personal"})
        cmbCategory.Location = New Point(0, 228)
        cmbCategory.Name = "cmbCategory"
        cmbCategory.Size = New Size(288, 38)
        cmbCategory.TabIndex = 5
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label3.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        Label3.Location = New Point(312, 206)
        Label3.Name = "Label3"
        Label3.Size = New Size(106, 25)
        Label3.TabIndex = 6
        Label3.Text = "Due Date *"
        ' 
        ' dtpDueDate
        ' 
        dtpDueDate.Font = New Font("Segoe UI", 11F)
        dtpDueDate.Format = DateTimePickerFormat.Short
        dtpDueDate.Location = New Point(312, 228)
        dtpDueDate.Name = "dtpDueDate"
        dtpDueDate.Size = New Size(288, 37)
        dtpDueDate.TabIndex = 7
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label4.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        Label4.Location = New Point(0, 282)
        Label4.Name = "Label4"
        Label4.Size = New Size(88, 25)
        Label4.TabIndex = 8
        Label4.Text = "Priority *"
        ' 
        ' cmbPriority
        ' 
        cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPriority.FlatStyle = FlatStyle.Flat
        cmbPriority.Font = New Font("Segoe UI", 11F)
        cmbPriority.Items.AddRange(New Object() {"Low", "Medium", "High"})
        cmbPriority.Location = New Point(0, 304)
        cmbPriority.Name = "cmbPriority"
        cmbPriority.Size = New Size(288, 38)
        cmbPriority.TabIndex = 9
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Label5.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        Label5.Location = New Point(312, 282)
        Label5.Name = "Label5"
        Label5.Size = New Size(77, 25)
        Label5.TabIndex = 10
        Label5.Text = "Status *"
        ' 
        ' cmbStatus
        ' 
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList
        cmbStatus.FlatStyle = FlatStyle.Flat
        cmbStatus.Font = New Font("Segoe UI", 11F)
        cmbStatus.Items.AddRange(New Object() {"Pending", "Completed"})
        cmbStatus.Location = New Point(312, 304)
        cmbStatus.Name = "cmbStatus"
        cmbStatus.Size = New Size(288, 38)
        cmbStatus.TabIndex = 11
        ' 
        ' chkIsRecurring
        ' 
        chkIsRecurring.AutoSize = True
        chkIsRecurring.Font = New Font("Segoe UI", 10F)
        chkIsRecurring.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        chkIsRecurring.Location = New Point(0, 358)
        chkIsRecurring.Name = "chkIsRecurring"
        chkIsRecurring.Size = New Size(343, 32)
        chkIsRecurring.TabIndex = 12
        chkIsRecurring.Text = "🔄  Recurring task (repeats weekly)"
        ' 
        ' lblRecurringNote
        ' 
        lblRecurringNote.AutoSize = True
        lblRecurringNote.Font = New Font("Segoe UI", 9F, FontStyle.Italic)
        lblRecurringNote.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        lblRecurringNote.Location = New Point(300, 362)
        lblRecurringNote.Name = "lblRecurringNote"
        lblRecurringNote.Size = New Size(121, 25)
        lblRecurringNote.TabIndex = 13
        lblRecurringNote.Text = "Recurs weekly"
        lblRecurringNote.Visible = False
        ' 
        ' lblTagLabel
        ' 
        lblTagLabel.AutoSize = True
        lblTagLabel.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblTagLabel.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        lblTagLabel.Location = New Point(0, 396)
        lblTagLabel.Name = "lblTagLabel"
        lblTagLabel.Size = New Size(134, 25)
        lblTagLabel.TabIndex = 14
        lblTagLabel.Text = "Tag  (optional)"
        ' 
        ' txtTag
        ' 
        txtTag.BorderStyle = BorderStyle.FixedSingle
        txtTag.Font = New Font("Segoe UI", 11F)
        txtTag.Location = New Point(0, 418)
        txtTag.MaxLength = 30
        txtTag.Name = "txtTag"
        txtTag.PlaceholderText = "e.g. exam, group project"
        txtTag.Size = New Size(600, 37)
        txtTag.TabIndex = 15
        ' 
        ' lblNotes
        ' 
        lblNotes.AutoSize = True
        lblNotes.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblNotes.ForeColor = Color.FromArgb(CByte(55), CByte(65), CByte(81))
        lblNotes.Location = New Point(0, 472)
        lblNotes.Name = "lblNotes"
        lblNotes.Size = New Size(156, 25)
        lblNotes.TabIndex = 16
        lblNotes.Text = "Notes  (optional)"
        ' 
        ' txtNotes
        ' 
        txtNotes.BorderStyle = BorderStyle.FixedSingle
        txtNotes.Font = New Font("Segoe UI", 10.5F)
        txtNotes.Location = New Point(0, 494)
        txtNotes.MaxLength = 2000
        txtNotes.Multiline = True
        txtNotes.Name = "txtNotes"
        txtNotes.PlaceholderText = "Add links, reminders, or extra context…"
        txtNotes.ScrollBars = ScrollBars.Vertical
        txtNotes.Size = New Size(600, 80)
        txtNotes.TabIndex = 17
        ' 
        ' btnSave
        ' 
        btnSave.BackColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        btnSave.Cursor = Cursors.Hand
        btnSave.FlatAppearance.BorderSize = 0
        btnSave.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(67), CByte(56), CByte(202))
        btnSave.FlatStyle = FlatStyle.Flat
        btnSave.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        btnSave.ForeColor = Color.White
        btnSave.Location = New Point(0, 592)
        btnSave.Name = "btnSave"
        btnSave.Size = New Size(290, 48)
        btnSave.TabIndex = 18
        btnSave.Text = "💾  Save Task"
        btnSave.UseVisualStyleBackColor = False
        ' 
        ' btnCancel
        ' 
        btnCancel.BackColor = Color.FromArgb(CByte(238), CByte(242), CByte(255))
        btnCancel.Cursor = Cursors.Hand
        btnCancel.FlatAppearance.BorderSize = 0
        btnCancel.FlatAppearance.MouseOverBackColor = Color.FromArgb(CByte(224), CByte(231), CByte(255))
        btnCancel.FlatStyle = FlatStyle.Flat
        btnCancel.Font = New Font("Segoe UI Semibold", 11F, FontStyle.Bold)
        btnCancel.ForeColor = Color.FromArgb(CByte(79), CByte(70), CByte(229))
        btnCancel.Location = New Point(310, 592)
        btnCancel.Name = "btnCancel"
        btnCancel.Size = New Size(290, 48)
        btnCancel.TabIndex = 19
        btnCancel.Text = "Cancel"
        btnCancel.UseVisualStyleBackColor = False
        ' 
        ' frmAddEditTask
        ' 
        AutoScaleDimensions = New SizeF(10F, 25F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.FromArgb(CByte(244), CByte(246), CByte(251))
        ClientSize = New Size(970, 700)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize = New Size(600, 500)
        Name = "frmAddEditTask"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Add / Edit Task"
        WindowState = FormWindowState.Maximized
        pnlTitleBar.ResumeLayout(False)
        pnlTitleBar.PerformLayout()
        pnlCenter.ResumeLayout(False)
        pnlCard.ResumeLayout(False)
        pnlCard.PerformLayout()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Button1 As Button
End Class
