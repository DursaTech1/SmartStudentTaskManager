<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmPomodoro
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

    Friend WithEvents pnlTitleBar  As Panel
    Friend WithEvents btnClose     As Button
    Friend WithEvents lblTitle     As Label
    Friend WithEvents pnlCenter    As Panel
    Friend WithEvents pnlTimer     As Panel
    Friend WithEvents lblPhase     As Label
    Friend WithEvents lblTime      As Label
    Friend WithEvents pbProgress   As ProgressBar
    Friend WithEvents lblMotivation As Label
    Friend WithEvents lblSessions  As Label
    Friend WithEvents flpButtons   As FlowLayoutPanel
    Friend WithEvents btnStartPause As Button
    Friend WithEvents btnReset     As Button
    Friend WithEvents btnSkip      As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar  = New Panel()
        btnClose     = New Button()
        lblTitle     = New Label()
        pnlCenter    = New Panel()
        pnlTimer     = New Panel()
        lblPhase     = New Label()
        lblTime      = New Label()
        pbProgress   = New ProgressBar()
        lblMotivation = New Label()
        lblSessions  = New Label()
        flpButtons   = New FlowLayoutPanel()
        btnStartPause = New Button()
        btnReset     = New Button()
        btnSkip      = New Button()

        SuspendLayout()

        ' Title bar
        pnlTitleBar.BackColor = ColorTranslator.FromHtml("#1E1B4B")
        pnlTitleBar.Dock = DockStyle.Top
        pnlTitleBar.Height = 48
        pnlTitleBar.Controls.Add(btnClose)
        pnlTitleBar.Controls.Add(lblTitle)

        btnClose.Dock = DockStyle.Right
        btnClose.FlatAppearance.BorderSize = 0
        btnClose.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#EF4444")
        btnClose.FlatStyle = FlatStyle.Flat
        btnClose.Font = New Font("Segoe UI", 12.0F, FontStyle.Bold)
        btnClose.ForeColor = Color.White
        btnClose.Size = New Size(48, 48)
        btnClose.Text = "✕"
        btnClose.UseVisualStyleBackColor = False

        lblTitle.AutoSize = True
        lblTitle.Font = New Font("Segoe UI Semibold", 11.0F, FontStyle.Bold)
        lblTitle.ForeColor = Color.White
        lblTitle.Location = New Point(20, 14)
        lblTitle.Text = "🍅  Pomodoro Timer"

        ' Center panel
        pnlCenter.BackColor = ColorTranslator.FromHtml("#F4F6FB")
        pnlCenter.Dock = DockStyle.Fill
        pnlCenter.Controls.Add(pnlTimer)

        ' Timer card
        pnlTimer.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        pnlTimer.Size = New Size(380, 420)
        pnlTimer.Padding = New Padding(30)
        pnlTimer.Controls.Add(lblPhase)
        pnlTimer.Controls.Add(lblTime)
        pnlTimer.Controls.Add(pbProgress)
        pnlTimer.Controls.Add(lblMotivation)
        pnlTimer.Controls.Add(lblSessions)
        pnlTimer.Controls.Add(flpButtons)

        lblPhase.AutoSize = False
        lblPhase.Dock = DockStyle.Top
        lblPhase.Font = New Font("Segoe UI Semibold", 13.0F, FontStyle.Bold)
        lblPhase.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        lblPhase.Height = 40
        lblPhase.Text = "🎯 Focus Time"
        lblPhase.TextAlign = ContentAlignment.MiddleCenter

        lblTime.AutoSize = False
        lblTime.Dock = DockStyle.Top
        lblTime.Font = New Font("Segoe UI", 64.0F, FontStyle.Bold)
        lblTime.ForeColor = ColorTranslator.FromHtml("#1E1B4B")
        lblTime.Height = 110
        lblTime.Text = "25:00"
        lblTime.TextAlign = ContentAlignment.MiddleCenter

        pbProgress.Dock = DockStyle.Top
        pbProgress.Height = 12
        pbProgress.Maximum = 100
        pbProgress.Minimum = 0
        pbProgress.Style = ProgressBarStyle.Continuous
        pbProgress.Value = 0

        lblMotivation.AutoSize = False
        lblMotivation.Dock = DockStyle.Top
        lblMotivation.Font = New Font("Segoe UI", 10.0F, FontStyle.Italic)
        lblMotivation.ForeColor = ColorTranslator.FromHtml("#6B7280")
        lblMotivation.Height = 50
        lblMotivation.Padding = New Padding(0, 8, 0, 0)
        lblMotivation.Text = "Stay focused — you're doing great! 💪"
        lblMotivation.TextAlign = ContentAlignment.TopCenter

        lblSessions.AutoSize = False
        lblSessions.Dock = DockStyle.Top
        lblSessions.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        lblSessions.ForeColor = ColorTranslator.FromHtml("#4F46E5")
        lblSessions.Height = 30
        lblSessions.Text = "Sessions: 0"
        lblSessions.TextAlign = ContentAlignment.MiddleCenter

        ' Buttons
        flpButtons.Dock = DockStyle.Bottom
        flpButtons.FlowDirection = FlowDirection.LeftToRight
        flpButtons.Height = 56
        flpButtons.Padding = New Padding(0, 8, 0, 0)
        flpButtons.WrapContents = False
        flpButtons.Controls.Add(btnStartPause)
        flpButtons.Controls.Add(btnReset)
        flpButtons.Controls.Add(btnSkip)

        For Each btn As Button In {btnStartPause, btnReset, btnSkip}
            btn.FlatAppearance.BorderSize = 0
            btn.FlatStyle = FlatStyle.Flat
            btn.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
            btn.ForeColor = Color.White
            btn.Height = 42
            btn.Cursor = Cursors.Hand
            btn.UseVisualStyleBackColor = False
            btn.Margin = New Padding(0, 0, 8, 0)
        Next

        btnStartPause.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnStartPause.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4338CA")
        btnStartPause.Size = New Size(130, 42)
        btnStartPause.Text = "▶  Start"

        btnReset.BackColor = ColorTranslator.FromHtml("#6B7280")
        btnReset.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#4B5563")
        btnReset.Size = New Size(90, 42)
        btnReset.Text = "↺  Reset"

        btnSkip.BackColor = ColorTranslator.FromHtml("#10B981")
        btnSkip.FlatAppearance.MouseOverBackColor = ColorTranslator.FromHtml("#059669")
        btnSkip.Size = New Size(90, 42)
        btnSkip.Text = "⏭  Skip"

        ' Form
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = ColorTranslator.FromHtml("#F4F6FB")
        ClientSize = New Size(440, 520)
        Controls.Add(pnlCenter)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        MinimumSize = New Size(440, 520)
        Name = "frmPomodoro"
        StartPosition = FormStartPosition.CenterScreen
        WindowState = FormWindowState.Maximized
        Text = "Pomodoro Timer"

        ResumeLayout(False)
    End Sub
End Class
