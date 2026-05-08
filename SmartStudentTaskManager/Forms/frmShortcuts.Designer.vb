<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmShortcuts
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

    Friend WithEvents pnlTitleBar As Panel
    Friend WithEvents btnClose    As Button
    Friend WithEvents lblTitle    As Label
    Friend WithEvents pnlContent  As Panel
    Friend WithEvents btnClose2   As Button

    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        pnlTitleBar = New Panel()
        btnClose    = New Button()
        lblTitle    = New Label()
        pnlContent  = New Panel()
        btnClose2   = New Button()

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
        lblTitle.Text = "⌨  Keyboard Shortcuts"

        ' Content panel (rows added in code-behind)
        pnlContent.BackColor = Color.White
        pnlContent.Dock = DockStyle.Fill
        pnlContent.AutoScroll = True

        ' Close button at bottom
        btnClose2.BackColor = ColorTranslator.FromHtml("#4F46E5")
        btnClose2.Cursor = Cursors.Hand
        btnClose2.Dock = DockStyle.Bottom
        btnClose2.FlatAppearance.BorderSize = 0
        btnClose2.FlatStyle = FlatStyle.Flat
        btnClose2.Font = New Font("Segoe UI Semibold", 10.0F, FontStyle.Bold)
        btnClose2.ForeColor = Color.White
        btnClose2.Height = 44
        btnClose2.Text = "Close  (Esc)"
        btnClose2.UseVisualStyleBackColor = False

        ' Form
        AutoScaleDimensions = New SizeF(10.0F, 25.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(460, 500)
        Controls.Add(pnlContent)
        Controls.Add(btnClose2)
        Controls.Add(pnlTitleBar)
        FormBorderStyle = FormBorderStyle.None
        KeyPreview = True
        Name = "frmShortcuts"
        StartPosition = FormStartPosition.CenterParent
        Text = "Keyboard Shortcuts"

        ResumeLayout(False)
    End Sub
End Class
