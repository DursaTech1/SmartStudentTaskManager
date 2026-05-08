Imports System.Runtime.InteropServices

''' <summary>Keyboard shortcut legend dialog.</summary>
Public Class frmShortcuts

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture() : End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) : End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub frmShortcuts_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        ThemeManager.ApplyTheme(Me)

        ' Build the shortcut table
        Dim shortcuts As New List(Of (String, String)) From {
            ("Ctrl + N",      "Add new task"),
            ("Ctrl + E",      "Edit selected task"),
            ("Delete",        "Delete selected task"),
            ("Enter",         "View task details"),
            ("Ctrl + D",      "Duplicate task"),
            ("Ctrl + T",      "Toggle task status"),
            ("Ctrl + P",      "Print task list"),
            ("Ctrl + S",      "Export to CSV"),
            ("F5",            "Refresh dashboard"),
            ("Escape",        "Close this dialog")
        }

        Dim y As Integer = 16
        For Each s In shortcuts
            Dim pnlRow As New Panel() With {
                .BackColor = If(y Mod 64 = 16, ColorTranslator.FromHtml("#F9FAFB"), Color.White),
                .Location = New Point(0, y),
                .Size = New Size(460, 36)
            }

            Dim lblKey As New Label() With {
                .AutoSize = False,
                .BackColor = ColorTranslator.FromHtml("#EEF2FF"),
                .Font = New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold),
                .ForeColor = ColorTranslator.FromHtml("#4F46E5"),
                .Location = New Point(16, 6),
                .Size = New Size(130, 24),
                .Text = s.Item1,
                .TextAlign = ContentAlignment.MiddleCenter
            }

            Dim lblDesc As New Label() With {
                .AutoSize = True,
                .Font = New Font("Segoe UI", 10.0F),
                .ForeColor = ColorTranslator.FromHtml("#374151"),
                .Location = New Point(162, 8),
                .Text = s.Item2
            }

            pnlRow.Controls.Add(lblKey)
            pnlRow.Controls.Add(lblDesc)
            pnlContent.Controls.Add(pnlRow)
            y += 36
        Next

        ' Resize form to fit
        pnlContent.Height = y + 16
        Me.ClientSize = New Size(460, pnlTitleBar.Height + pnlContent.Height + btnClose2.Height + 16)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnClose2_Click(sender As Object, e As EventArgs) Handles btnClose2.Click
        Me.Close()
    End Sub

    Protected Overrides Sub OnKeyDown(e As KeyEventArgs)
        If e.KeyCode = Keys.Escape Then Me.Close()
        MyBase.OnKeyDown(e)
    End Sub
End Class
