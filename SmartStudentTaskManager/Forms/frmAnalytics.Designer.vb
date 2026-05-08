<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAnalytics
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

    ' Title bar
    Friend WithEvents pnlTitleBar        As Panel
    Friend WithEvents btnClose           As Button
    Friend WithEvents btnRefresh         As Button
    Friend WithEvents lblTitle           As Label

    ' Body
    Friend WithEvents pnlBody            As Panel
    Friend WithEvents tlpMain            As TableLayoutPanel

    ' Card 1 - Priority
    Friend WithEvents pnlCard1           As Panel
    Friend WithEvents lblPriorityTitle   As Label
    Friend WithEvents pnlPriorityChart   As Panel

    ' Card 2 - Status
    Friend WithEvents pnlCard2           As Panel
    Friend WithEvents lblStatusTitle     As Label
    Friend WithEvents pnlStatusChart     As Panel

    ' Card 3 - Weekly
    Friend WithEvents pnlCard3           As Panel
    Friend WithEvents lblWeeklyTitle     As Label
    Friend WithEvents pnlWeeklyChart     As Panel

    ' Card 4 - Recommendations
    Friend WithEvents pnlRecsCard        As Panel
    Friend WithEvents lblRecsTitle       As Label
    Friend WithEvents pnlRecsItems       As Panel

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar        = New Panel()
        btnClose           = New Button()
        btnRefresh         = New Button()
        lblTitle           = New Label()
        pnlBody            = New Panel()
        tlpMain            = New TableLayoutPanel()
        pnlCard1           = New Panel()
        lblPriorityTitle   = New Label()
        pnlPriorityChart   = New Panel()
        pnlCard2           = New Panel()
        lblStatusTitle     = New Label()
        pnlStatusChart     = New Panel()
        pnlCard3           = New Panel()
        lblWeeklyTitle     = New Label()
        pnlWeeklyChart     = New Panel()
        pnlRecsCard        = New Panel()
        lblRecsTitle       = New Label()
        pnlRecsItems       = New Panel()
        SuspendLayout()

        ' ── TITLE BAR ────────────────────────────────────────────────────────
        pnlTitleBar.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        pnlTitleBar.Dock      = DockStyle.Top
        pnlTitleBar.Height    = 54
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(btnRefresh)
        pnlTitleBar.Controls.Add(lblTitle)

        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#EF4444")
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font      = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Size      = New Size(54, 54)
        btnClose.Text      = "x"
        btnClose.UseVisualStyleBackColor = False

        btnRefresh.Dock = DockStyle.Right
        btnRefresh.FlatAppearance.BorderSize = 0
        btnRefresh.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#312E81")
        btnRefresh.FlatStyle = FlatStyle.Flat
        btnRefresh.Font      = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnRefresh.ForeColor = Color.White
        btnRefresh.Size      = New Size(120, 54)
        btnRefresh.Text      = "Refresh"
        btnRefresh.UseVisualStyleBackColor = False

        lblTitle.AutoSize  = True
        lblTitle.Font      = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location  = New Point(20, 14)
        lblTitle.Text      = "Analytics & Insights"

        ' ── BODY ─────────────────────────────────────────────────────────────
        pnlBody.BackColor = ColorTranslator.FromHtml("#F4F6FB")
        pnlBody.Dock      = DockStyle.Fill
        pnlBody.Padding   = New Padding(24, 20, 24, 20)
        pnlBody.Controls.Add(tlpMain)

        ' ── TABLE LAYOUT: 2 cols, 3 rows ──────────────────────────────────────
        ' Row 0: Priority (left) + Status (right)  ~35%
        ' Row 1: Weekly chart full width            ~30%
        ' Row 2: Recommendations full width         ~35%
        tlpMain.BackColor   = Color.Transparent
        tlpMain.Dock        = DockStyle.Fill
        tlpMain.ColumnCount = 2
        tlpMain.RowCount    = 3
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlpMain.ColumnStyles.Add(New ColumnStyle(SizeType.Percent, 50.0F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 35.0F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 30.0F))
        tlpMain.RowStyles.Add(New RowStyle(SizeType.Percent, 35.0F))
        tlpMain.GrowStyle   = TableLayoutPanelGrowStyle.FixedSize

        tlpMain.Controls.Add(pnlCard1, 0, 0)
        tlpMain.Controls.Add(pnlCard2, 1, 0)
        tlpMain.Controls.Add(pnlCard3, 0, 1)
        tlpMain.SetColumnSpan(pnlCard3, 2)
        tlpMain.Controls.Add(pnlRecsCard, 0, 2)
        tlpMain.SetColumnSpan(pnlRecsCard, 2)

        ' ── CARD 1: Priority Breakdown ────────────────────────────────────────
        pnlCard1.BackColor = Color.Transparent
        pnlCard1.Dock      = DockStyle.Fill
        pnlCard1.Margin    = New Padding(0, 0, 10, 10)
        pnlCard1.Controls.Add(lblPriorityTitle)
        pnlCard1.Controls.Add(pnlPriorityChart)

        lblPriorityTitle.AutoSize  = False
        lblPriorityTitle.Dock      = DockStyle.Top
        lblPriorityTitle.Font      = New Font("Segoe UI Semibold", 11.5F, FontStyle.Bold)
        lblPriorityTitle.ForeColor = ColorTranslator.FromHtml("#1E1B4B")
        lblPriorityTitle.Height    = 36
        lblPriorityTitle.Padding   = New Padding(4, 6, 0, 0)
        lblPriorityTitle.Text      = "Priority Breakdown"
        lblPriorityTitle.BackColor = Color.Transparent

        pnlPriorityChart.BackColor = Color.Transparent
        pnlPriorityChart.Dock      = DockStyle.Fill

        ' ── CARD 2: Status Overview ───────────────────────────────────────────
        pnlCard2.BackColor = Color.Transparent
        pnlCard2.Dock      = DockStyle.Fill
        pnlCard2.Margin    = New Padding(10, 0, 0, 10)
        pnlCard2.Controls.Add(lblStatusTitle)
        pnlCard2.Controls.Add(pnlStatusChart)

        lblStatusTitle.AutoSize  = False
        lblStatusTitle.Dock      = DockStyle.Top
        lblStatusTitle.Font      = New Font("Segoe UI Semibold", 11.5F, FontStyle.Bold)
        lblStatusTitle.ForeColor = ColorTranslator.FromHtml("#1E1B4B")
        lblStatusTitle.Height    = 36
        lblStatusTitle.Padding   = New Padding(4, 6, 0, 0)
        lblStatusTitle.Text      = "Status Overview"
        lblStatusTitle.BackColor = Color.Transparent

        pnlStatusChart.BackColor = Color.Transparent
        pnlStatusChart.Dock      = DockStyle.Fill

        ' ── CARD 3: Weekly Progress ───────────────────────────────────────────
        pnlCard3.BackColor = Color.Transparent
        pnlCard3.Dock      = DockStyle.Fill
        pnlCard3.Margin    = New Padding(0, 0, 0, 10)
        pnlCard3.Controls.Add(lblWeeklyTitle)
        pnlCard3.Controls.Add(pnlWeeklyChart)

        lblWeeklyTitle.AutoSize  = False
        lblWeeklyTitle.Dock      = DockStyle.Top
        lblWeeklyTitle.Font      = New Font("Segoe UI Semibold", 11.5F, FontStyle.Bold)
        lblWeeklyTitle.ForeColor = ColorTranslator.FromHtml("#1E1B4B")
        lblWeeklyTitle.Height    = 36
        lblWeeklyTitle.Padding   = New Padding(4, 6, 0, 0)
        lblWeeklyTitle.Text      = "Weekly Progress (last 7 days)"
        lblWeeklyTitle.BackColor = Color.Transparent

        pnlWeeklyChart.BackColor = Color.Transparent
        pnlWeeklyChart.Dock      = DockStyle.Fill

        ' ── CARD 4: AI Recommendations ───────────────────────────────────────
        pnlRecsCard.BackColor = Color.Transparent
        pnlRecsCard.Dock      = DockStyle.Fill
        pnlRecsCard.Margin    = New Padding(0, 0, 0, 0)
        pnlRecsCard.Controls.Add(lblRecsTitle)
        pnlRecsCard.Controls.Add(pnlRecsItems)

        lblRecsTitle.AutoSize  = False
        lblRecsTitle.Dock      = DockStyle.Top
        lblRecsTitle.Font      = New Font("Segoe UI Semibold", 11.5F, FontStyle.Bold)
        lblRecsTitle.ForeColor = ColorTranslator.FromHtml("#1E1B4B")
        lblRecsTitle.Height    = 36
        lblRecsTitle.Padding   = New Padding(4, 6, 0, 0)
        lblRecsTitle.Text      = "Recommendations"
        lblRecsTitle.BackColor = Color.Transparent

        pnlRecsItems.BackColor = Color.White
        pnlRecsItems.Dock      = DockStyle.Fill

        ' ── FORM ─────────────────────────────────────────────────────────────
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode       = AutoScaleMode.Font
        BackColor           = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize          = New Size(1280, 800)
        Controls.Add(pnlBody)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize     = New Size(900, 600)
        Name            = "frmAnalytics"
        StartPosition   = FormStartPosition.CenterScreen
        WindowState     = FormWindowState.Maximized
        Text            = "Analytics"

        ResumeLayout(False)
    End Sub
End Class
