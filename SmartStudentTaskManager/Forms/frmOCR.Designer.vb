<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmOCR
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then components.Dispose()
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub
    Private components As System.ComponentModel.IContainer

    Friend WithEvents pnlTitleBar       As Panel
    Friend WithEvents btnClose          As Button
    Friend WithEvents lblTitleBar       As Label
    Friend WithEvents pnlCenter         As Panel
    Friend WithEvents pnlCard           As Panel

    ' Left: image upload + preview
    Friend WithEvents pnlLeft           As Panel
    Friend WithEvents btnUploadImage    As Button
    Friend WithEvents lblImagePath      As Label
    Friend WithEvents picPreview        As PictureBox
    Friend WithEvents btnExtract        As Button
    Friend WithEvents lblStatus         As Label

    ' Right: extracted text + parsed fields
    Friend WithEvents pnlRight          As Panel
    Friend WithEvents lblExtractedLbl   As Label
    Friend WithEvents txtExtractedText  As TextBox
    Friend WithEvents btnParse          As Button
    Friend WithEvents lblTitleLbl       As Label
    Friend WithEvents txtTitle          As TextBox
    Friend WithEvents lblSubjectLbl     As Label
    Friend WithEvents txtSubject        As TextBox
    Friend WithEvents lblDescLbl        As Label
    Friend WithEvents txtDescription    As TextBox
    Friend WithEvents lblDueDateLbl     As Label
    Friend WithEvents dtpDueDate        As DateTimePicker
    Friend WithEvents lblPriorityLbl    As Label
    Friend WithEvents cmbPriority       As ComboBox
    Friend WithEvents btnCreateTask     As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar      = New Panel()
        btnClose         = New Button()
        lblTitleBar      = New Label()
        pnlCenter        = New Panel()
        pnlCard          = New Panel()
        pnlLeft          = New Panel()
        btnUploadImage   = New Button()
        lblImagePath     = New Label()
        picPreview       = New PictureBox()
        btnExtract       = New Button()
        lblStatus        = New Label()
        pnlRight         = New Panel()
        lblExtractedLbl  = New Label()
        txtExtractedText = New TextBox()
        btnParse         = New Button()
        lblTitleLbl      = New Label()
        txtTitle         = New TextBox()
        lblSubjectLbl    = New Label()
        txtSubject       = New TextBox()
        lblDescLbl       = New Label()
        txtDescription   = New TextBox()
        lblDueDateLbl    = New Label()
        dtpDueDate       = New DateTimePicker()
        lblPriorityLbl   = New Label()
        cmbPriority      = New ComboBox()
        btnCreateTask    = New Button()

        SuspendLayout()

        ' ── Title Bar ────────────────────────────────────────────────────────
        pnlTitleBar.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Height = 50
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(lblTitleBar)

        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#EF4444")
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Size = New Size(50, 50)
        btnClose.Text = "✕"
        btnClose.UseVisualStyleBackColor = False

        lblTitleBar.AutoSize = True
        lblTitleBar.Font = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold)
        lblTitleBar.ForeColor = Color.White
        lblTitleBar.Location = New Point(20, 12)
        lblTitleBar.Text = "📷  OCR / Image Upload — Extract Assignment Details"

        ' ── Center ───────────────────────────────────────────────────────────
        pnlCenter.BackColor = ColorTranslator.FromHtml("#F4F6FB")
        pnlCenter.Dock = DockStyle.Fill
        pnlCenter.Controls.Add(pnlCard)

        ' ── Card (1100 × 660) ─────────────────────────────────────────────────
        pnlCard.BackColor = Color.White
        pnlCard.Size = New Size(1100, 660)
        pnlCard.Controls.Add(pnlLeft)
        pnlCard.Controls.Add(pnlRight)

        ' ── LEFT PANEL (400px) ────────────────────────────────────────────────
        pnlLeft.BackColor = ColorTranslator.FromHtml("#F9FAFB")
        pnlLeft.BorderStyle = BorderStyle.FixedSingle
        pnlLeft.Location = New Point(0, 0)
        pnlLeft.Size = New Size(400, 660)
        pnlLeft.Padding = New Padding(20)
        pnlLeft.Controls.Add(btnUploadImage)
        pnlLeft.Controls.Add(lblImagePath)
        pnlLeft.Controls.Add(picPreview)
        pnlLeft.Controls.Add(btnExtract)
        pnlLeft.Controls.Add(lblStatus)

        btnUploadImage.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnUploadImage.Cursor = Cursors.Hand
        btnUploadImage.FlatAppearance.BorderSize = 0
        btnUploadImage.FlatStyle = FlatStyle.Flat
        btnUploadImage.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnUploadImage.ForeColor = Color.White
        btnUploadImage.Location = New Point(20, 20)
        btnUploadImage.Size = New Size(360, 50)
        btnUploadImage.Text = "📁  Upload Image / Screenshot"
        btnUploadImage.UseVisualStyleBackColor = False

        lblImagePath.AutoSize = False
        lblImagePath.Font = New Font("Segoe UI", 9.5F, FontStyle.Italic)
        lblImagePath.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblImagePath.Location = New Point(20, 78)
        lblImagePath.Size = New Size(360, 24)
        lblImagePath.Text = "No image selected"
        lblImagePath.TextAlign = ContentAlignment.MiddleLeft

        picPreview.BackColor = ColorTranslator.FromHtml("#E5E7EB")
        picPreview.BorderStyle = BorderStyle.FixedSingle
        picPreview.Location = New Point(20, 110)
        picPreview.Size = New Size(360, 280)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.Visible = False

        btnExtract.BackColor = ColorTranslator.FromHtml("#10B981")
        btnExtract.Cursor = Cursors.Hand
        btnExtract.Enabled = False
        btnExtract.FlatAppearance.BorderSize = 0
        btnExtract.FlatStyle = FlatStyle.Flat
        btnExtract.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        btnExtract.ForeColor = Color.White
        btnExtract.Location = New Point(20, 408)
        btnExtract.Size = New Size(360, 50)
        btnExtract.Text = "🔍  Extract Text (OCR)"
        btnExtract.UseVisualStyleBackColor = False

        lblStatus.AutoSize = False
        lblStatus.Font = New Font("Segoe UI", 10.0F)
        lblStatus.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblStatus.Location = New Point(20, 470)
        lblStatus.Size = New Size(360, 60)
        lblStatus.Text = "Upload an image to begin. OCR will extract text automatically." & vbCrLf & "You can also type text manually in the right panel."
        lblStatus.TextAlign = ContentAlignment.TopLeft

        ' ── RIGHT PANEL (700px) ───────────────────────────────────────────────
        pnlRight.BackColor = Color.White
        pnlRight.Location = New Point(400, 0)
        pnlRight.Size = New Size(700, 660)
        pnlRight.Padding = New Padding(30, 24, 30, 24)
        pnlRight.Controls.Add(lblExtractedLbl)
        pnlRight.Controls.Add(txtExtractedText)
        pnlRight.Controls.Add(btnParse)
        pnlRight.Controls.Add(lblTitleLbl)
        pnlRight.Controls.Add(txtTitle)
        pnlRight.Controls.Add(lblSubjectLbl)
        pnlRight.Controls.Add(txtSubject)
        pnlRight.Controls.Add(lblDescLbl)
        pnlRight.Controls.Add(txtDescription)
        pnlRight.Controls.Add(lblDueDateLbl)
        pnlRight.Controls.Add(dtpDueDate)
        pnlRight.Controls.Add(lblPriorityLbl)
        pnlRight.Controls.Add(cmbPriority)
        pnlRight.Controls.Add(btnCreateTask)

        lblExtractedLbl.AutoSize = True
        lblExtractedLbl.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblExtractedLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblExtractedLbl.Location = New Point(0, 0)
        lblExtractedLbl.Text = "EXTRACTED / MANUAL TEXT  (edit as needed)"

        txtExtractedText.BorderStyle = BorderStyle.FixedSingle
        txtExtractedText.Font = New Font("Segoe UI", 10.0F)
        txtExtractedText.Location = New Point(0, 24)
        txtExtractedText.Multiline = True
        txtExtractedText.PlaceholderText = "Paste or type assignment text here, or use OCR to extract from image…"
        txtExtractedText.ScrollBars = ScrollBars.Vertical
        txtExtractedText.Size = New Size(640, 120)

        btnParse.BackColor = ColorTranslator.FromHtml("#F59E0B")
        btnParse.Cursor = Cursors.Hand
        btnParse.FlatAppearance.BorderSize = 0
        btnParse.FlatStyle = FlatStyle.Flat
        btnParse.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnParse.ForeColor = Color.White
        btnParse.Location = New Point(0, 152)
        btnParse.Size = New Size(200, 38)
        btnParse.Text = "🧠  Auto-Parse Fields"
        btnParse.UseVisualStyleBackColor = False

        ' Task title
        lblTitleLbl.AutoSize = True
        lblTitleLbl.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblTitleLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblTitleLbl.Location = New Point(0, 204)
        lblTitleLbl.Text = "TASK TITLE *"

        txtTitle.BorderStyle = BorderStyle.FixedSingle
        txtTitle.Font = New Font("Segoe UI", 11.0F)
        txtTitle.Location = New Point(0, 226)
        txtTitle.MaxLength = 200
        txtTitle.PlaceholderText = "e.g. Database Assignment"
        txtTitle.Size = New Size(640, 36)

        ' Subject / Category
        lblSubjectLbl.AutoSize = True
        lblSubjectLbl.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblSubjectLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblSubjectLbl.Location = New Point(0, 276)
        lblSubjectLbl.Text = "SUBJECT / CATEGORY"

        txtSubject.BorderStyle = BorderStyle.FixedSingle
        txtSubject.Font = New Font("Segoe UI", 11.0F)
        txtSubject.Location = New Point(0, 298)
        txtSubject.PlaceholderText = "e.g. Database Systems"
        txtSubject.Size = New Size(310, 36)

        ' Description
        lblDescLbl.AutoSize = True
        lblDescLbl.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblDescLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblDescLbl.Location = New Point(330, 276)
        lblDescLbl.Text = "DESCRIPTION (optional)"

        txtDescription.BorderStyle = BorderStyle.FixedSingle
        txtDescription.Font = New Font("Segoe UI", 11.0F)
        txtDescription.Location = New Point(330, 298)
        txtDescription.PlaceholderText = "Additional details…"
        txtDescription.Size = New Size(310, 36)

        ' Due date
        lblDueDateLbl.AutoSize = True
        lblDueDateLbl.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblDueDateLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblDueDateLbl.Location = New Point(0, 348)
        lblDueDateLbl.Text = "DUE DATE"

        dtpDueDate.Font = New Font("Segoe UI", 11.0F)
        dtpDueDate.Format = DateTimePickerFormat.Short
        dtpDueDate.Location = New Point(0, 370)
        dtpDueDate.Size = New Size(200, 36)
        dtpDueDate.Value = DateTime.Today.AddDays(7)

        ' Priority
        lblPriorityLbl.AutoSize = True
        lblPriorityLbl.Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        lblPriorityLbl.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblPriorityLbl.Location = New Point(220, 348)
        lblPriorityLbl.Text = "PRIORITY"

        cmbPriority.DropDownStyle = ComboBoxStyle.DropDownList
        cmbPriority.FlatStyle = FlatStyle.Flat
        cmbPriority.Font = New Font("Segoe UI", 11.0F)
        cmbPriority.Items.AddRange(New Object() {"Low", "Medium", "High"})
        cmbPriority.Location = New Point(220, 370)
        cmbPriority.SelectedIndex = 1
        cmbPriority.Size = New Size(150, 36)

        ' Create task button
        btnCreateTask.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnCreateTask.Cursor = Cursors.Hand
        btnCreateTask.FlatAppearance.BorderSize = 0
        btnCreateTask.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4338CA")
        btnCreateTask.FlatStyle = FlatStyle.Flat
        btnCreateTask.Font = New Font("Segoe UI Semibold", 12.0F, FontStyle.Bold)
        btnCreateTask.ForeColor = Color.White
        btnCreateTask.Location = New Point(0, 430)
        btnCreateTask.Size = New Size(640, 52)
        btnCreateTask.Text = "✅  Create Task from Extracted Data"
        btnCreateTask.UseVisualStyleBackColor = False

        ' ── Form ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize = New Size(1200, 760)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize = New Size(1200, 760)
        Name = "frmOCR"
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        Text = "OCR Image Upload"

        ResumeLayout(False)
    End Sub
End Class
