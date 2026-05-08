Imports System.Runtime.InteropServices
Imports System.Drawing.Drawing2D

Public Class frmAnalytics

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture() : End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) : End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub frmAnalytics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        LoadAnalytics()
    End Sub

    ' ── Card background painter ───────────────────────────────────────────────
    Private Shared Sub PaintWhiteCard(sender As Object, e As PaintEventArgs)
        Dim p As Panel = DirectCast(sender, Panel)
        Dim g As Graphics = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias
        g.Clear(ColorTranslator.FromHtml("#F4F6FB"))

        ' Soft shadow
        For i As Integer = 4 To 1 Step -1
            Using sb As New SolidBrush(Color.FromArgb(8 * i, 0, 0, 0))
                g.FillRectangle(sb, New Rectangle(i, i, p.Width - 1, p.Height - 1))
            End Using
        Next

        ' White card fill
        Using br As New SolidBrush(Color.White)
            g.FillRectangle(br, New Rectangle(0, 0, p.Width - 5, p.Height - 5))
        End Using

        ' Subtle border
        Using pen As New Pen(ColorTranslator.FromHtml("#E5E7EB"), 1)
            g.DrawRectangle(pen, New Rectangle(0, 0, p.Width - 6, p.Height - 6))
        End Using
    End Sub

    ' ── Load all analytics data and wire chart painters ───────────────────────
    Private Sub LoadAnalytics()
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Dim uid As Integer = GlobalVariables.CurrentUser.UserID

        ' Remove stale handlers
        RemoveHandler pnlPriorityChart.Paint, Nothing
        RemoveHandler pnlStatusChart.Paint,   Nothing
        RemoveHandler pnlWeeklyChart.Paint,   Nothing
        RemoveHandler pnlRecsCard.Paint,      Nothing

        ' Priority chart
        AddHandler pnlPriorityChart.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                PaintWhiteCard(s, ev)
                Dim data = AnalyticsHelper.GetPriorityBreakdown(uid)
                Dim inner As New Rectangle(20, 44, pnlPriorityChart.Width - 44, pnlPriorityChart.Height - 60)
                DrawPriorityBars(ev.Graphics, inner, data)
            End Sub

        ' Status donut
        AddHandler pnlStatusChart.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                PaintWhiteCard(s, ev)
                Dim data = AnalyticsHelper.GetStatusBreakdown(uid)
                Dim inner As New Rectangle(20, 44, pnlStatusChart.Width - 44, pnlStatusChart.Height - 60)
                DrawStatusDonut(ev.Graphics, inner, data)
            End Sub

        ' Weekly bars
        AddHandler pnlWeeklyChart.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                PaintWhiteCard(s, ev)
                Dim data = AnalyticsHelper.GetWeeklyData(uid)
                Dim inner As New Rectangle(20, 44, pnlWeeklyChart.Width - 44, pnlWeeklyChart.Height - 60)
                DrawWeeklyBars(ev.Graphics, inner, data)
            End Sub

        ' Recs card background
        AddHandler pnlRecsCard.Paint, AddressOf PaintWhiteCard

        ' Build recommendation items list
        Dim recs = TaskRecommendationEngine.GetRecommendations(uid, 5)
        Dim items As New List(Of (Text As String, Color As Color))

        If recs.Count = 0 Then
            items.Add(("No pending tasks — great job! Keep it up.", ColorTranslator.FromHtml("#10B981")))
        Else
            For Each r In recs
                Dim accent As Color = ColorTranslator.FromHtml("#4F46E5")
                Dim cleanText As String = StripEmoji(r.Reason)
                If r.Reason.Contains("OVERDUE") OrElse r.Reason.Contains("immediately") Then
                    accent = ColorTranslator.FromHtml("#EF4444")
                ElseIf r.Reason.Contains("Due today") OrElse (r.Reason.Contains("Due in") AndAlso r.Reason.Contains("min")) Then
                    accent = ColorTranslator.FromHtml("#EF4444")
                ElseIf r.Reason.Contains("tomorrow") OrElse r.Reason.Contains("today") Then
                    accent = ColorTranslator.FromHtml("#F59E0B")
                ElseIf r.Reason.Contains("2 days") OrElse r.Reason.Contains("3 days") Then
                    accent = ColorTranslator.FromHtml("#F59E0B")
                End If
                items.Add((cleanText, accent))
            Next
        End If

        Dim warning As String = TaskRecommendationEngine.GetWorkloadWarning(uid)
        If warning <> "" Then items.Add((StripEmoji(warning), ColorTranslator.FromHtml("#F59E0B")))

        Dim tip As String = TaskRecommendationEngine.GetProductivityTip(uid)
        If tip <> "" Then items.Add((StripEmoji(tip), ColorTranslator.FromHtml("#4F46E5")))

        Dim studyMins As Integer = AnalyticsHelper.GetWeeklyStudyMinutes(uid)
        If studyMins > 0 Then
            items.Add(($"You've studied {studyMins} min this week using the Pomodoro timer. Keep it up!", ColorTranslator.FromHtml("#10B981")))
        End If

        ' Wire the custom paint handler for recommendation rows
        RemoveHandler pnlRecsItems.Paint, Nothing
        Dim capturedItems = items
        AddHandler pnlRecsItems.Paint,
            Sub(s As Object, ev As PaintEventArgs)
                DrawRecommendationRows(ev.Graphics,
                    New Rectangle(0, 0, pnlRecsItems.Width, pnlRecsItems.Height),
                    capturedItems)
            End Sub
        pnlRecsItems.Invalidate()

        pnlPriorityChart.Invalidate()
        pnlStatusChart.Invalidate()
        pnlWeeklyChart.Invalidate()
        pnlRecsCard.Invalidate()
    End Sub

    ' ── Custom chart renderers ────────────────────────────────────────────────

    Private Shared Sub DrawPriorityBars(g As Graphics, bounds As Rectangle,
                                        data As Dictionary(Of String, Integer))
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim colors As New Dictionary(Of String, Color) From {
            {"High",   ColorTranslator.FromHtml("#EF4444")},
            {"Medium", ColorTranslator.FromHtml("#F59E0B")},
            {"Low",    ColorTranslator.FromHtml("#10B981")}
        }
        Dim maxVal As Integer = Math.Max(1, data.Values.Max())
        Dim barH   As Integer = 28
        Dim gap    As Integer = 18
        Dim labelW As Integer = 64
        Dim y      As Integer = bounds.Y

        Using labelFont As New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Using valFont   As New Font("Segoe UI", 9.0F)
            For Each kv In data
                ' Label
                g.DrawString(kv.Key, labelFont, Brushes.DimGray,
                             New RectangleF(bounds.X, y, labelW, barH),
                             New StringFormat() With {.LineAlignment = StringAlignment.Center})

                ' Bar background (light gray track)
                Dim trackW As Integer = bounds.Width - labelW - 52
                Using trackBr As New SolidBrush(ColorTranslator.FromHtml("#F3F4F6"))
                    g.FillRectangle(trackBr, bounds.X + labelW, y + 4, trackW, barH - 8)
                End Using

                ' Colored bar
                Dim barW As Integer = If(maxVal > 0, CInt((kv.Value / CDbl(maxVal)) * trackW), 0)
                barW = Math.Max(barW, 4)
                Using br As New SolidBrush(colors(kv.Key))
                    g.FillRectangle(br, bounds.X + labelW, y + 4, barW, barH - 8)
                End Using

                ' Value badge
                Dim badgeX As Integer = bounds.X + labelW + trackW + 8
                Using badgeBr As New SolidBrush(ColorTranslator.FromHtml("#F3F4F6"))
                    g.FillRectangle(badgeBr, badgeX, y + 4, 36, barH - 8)
                End Using
                g.DrawString(kv.Value.ToString(), valFont, Brushes.DimGray,
                             New RectangleF(badgeX, y, 36, barH),
                             New StringFormat() With {
                                 .Alignment = StringAlignment.Center,
                                 .LineAlignment = StringAlignment.Center})

                y += barH + gap
            Next
        End Using : End Using
    End Sub

    Private Shared Sub DrawStatusDonut(g As Graphics, bounds As Rectangle,
                                       data As Dictionary(Of String, Integer))
        g.SmoothingMode = SmoothingMode.AntiAlias
        Dim colors As New Dictionary(Of String, Color) From {
            {"Completed", ColorTranslator.FromHtml("#10B981")},
            {"Pending",   ColorTranslator.FromHtml("#F59E0B")},
            {"Overdue",   ColorTranslator.FromHtml("#EF4444")}
        }
        Dim total As Integer = data.Values.Sum()

        ' Donut area (left portion of bounds)
        Dim donutSize As Integer = Math.Min(bounds.Height, bounds.Width \ 2) - 10
        Dim cx As Integer = bounds.X + donutSize \ 2 + 10
        Dim cy As Integer = bounds.Y + bounds.Height \ 2
        Dim r  As Integer = donutSize \ 2
        Dim innerR As Integer = CInt(r * 0.58)

        If total = 0 Then
            Using f As New Font("Segoe UI", 11.0F)
                g.DrawString("No data yet", f, Brushes.LightGray,
                             New RectangleF(bounds.X, bounds.Y, bounds.Width, bounds.Height),
                             New StringFormat() With {.Alignment = StringAlignment.Center,
                                                      .LineAlignment = StringAlignment.Center})
            End Using
            Return
        End If

        ' Draw pie slices
        Dim startAngle As Single = -90.0F
        For Each kv In data
            If kv.Value = 0 Then Continue For
            Dim sweep As Single = CSng(kv.Value / CDbl(total) * 360.0)
            Using br As New SolidBrush(colors(kv.Key))
                g.FillPie(br, cx - r, cy - r, r * 2, r * 2, startAngle, sweep)
            End Using
            startAngle += sweep
        Next

        ' White donut hole
        g.FillEllipse(Brushes.White, cx - innerR, cy - innerR, innerR * 2, innerR * 2)

        ' Center percentage text
        Dim pct As Integer = If(total > 0, CInt(data("Completed") / CDbl(total) * 100), 0)
        Using bigFont As New Font("Segoe UI Semibold", 16.0F, FontStyle.Bold)
        Using smallFont As New Font("Segoe UI", 8.5F)
            g.DrawString($"{pct}%", bigFont, Brushes.DimGray,
                         New RectangleF(cx - 30, cy - 18, 60, 22),
                         New StringFormat() With {.Alignment = StringAlignment.Center})
            g.DrawString("done", smallFont, Brushes.Gray,
                         New RectangleF(cx - 30, cy + 4, 60, 16),
                         New StringFormat() With {.Alignment = StringAlignment.Center})
        End Using : End Using

        ' Legend (right side)
        Dim lx As Integer = cx + r + 24
        Dim ly As Integer = cy - CInt(data.Count * 22 / 2)
        Using f As New Font("Segoe UI", 9.5F)
            For Each kv In data
                Using br As New SolidBrush(colors(kv.Key))
                    g.FillRectangle(br, lx, ly + 3, 14, 14)
                End Using
                g.DrawString($"{kv.Key}  ({kv.Value})", f, Brushes.DimGray, lx + 20, ly)
                ly += 26
            Next
        End Using
    End Sub

    Private Shared Sub DrawWeeklyBars(g As Graphics, bounds As Rectangle,
                                      data As List(Of AnalyticsHelper.WeeklyPoint))
        g.SmoothingMode = SmoothingMode.AntiAlias
        If data.Count = 0 Then Return

        Dim maxVal As Integer = Math.Max(1, data.Max(Function(p) Math.Max(p.Total, 1)))
        Dim barW   As Integer = CInt((bounds.Width - 20) / data.Count) - 12
        barW = Math.Max(barW, 8)
        Dim x As Integer = bounds.X + 10
        Dim chartH As Integer = bounds.Height - 30  ' leave room for day labels

        Using doneColor As New SolidBrush(ColorTranslator.FromHtml("#4F46E5"))
        Using totalColor As New SolidBrush(ColorTranslator.FromHtml("#EEF2FF"))
        Using dayFont As New Font("Segoe UI", 8.5F)
        Using valFont As New Font("Segoe UI Semibold", 8.0F, FontStyle.Bold)
            For Each pt In data
                Dim totalH As Integer = CInt((pt.Total / CDbl(maxVal)) * chartH)
                Dim doneH  As Integer = CInt((pt.Done  / CDbl(maxVal)) * chartH)
                Dim barY   As Integer = bounds.Bottom - 24

                ' Total bar (light background)
                If totalH > 0 Then
                    g.FillRectangle(totalColor, x, barY - totalH, barW, totalH)
                End If
                ' Done bar (indigo foreground)
                If doneH > 0 Then
                    g.FillRectangle(doneColor, x, barY - doneH, barW, doneH)
                End If

                ' Value on top of done bar
                If pt.Done > 0 Then
                    g.DrawString(pt.Done.ToString(), valFont, Brushes.DimGray,
                                 New RectangleF(x, barY - doneH - 16, barW, 14),
                                 New StringFormat() With {.Alignment = StringAlignment.Center})
                End If

                ' Day label
                g.DrawString(pt.Day, dayFont, Brushes.Gray,
                             New RectangleF(x, barY + 4, barW, 16),
                             New StringFormat() With {.Alignment = StringAlignment.Center})

                x += barW + 12
            Next
        End Using : End Using : End Using : End Using
    End Sub

    ' ── Strip emoji/symbol characters that GDI+ can't render ─────────────────
    Private Shared Function StripEmoji(text As String) As String
        If String.IsNullOrEmpty(text) Then Return text
        ' Remove characters outside the Basic Multilingual Plane (surrogate pairs = emoji)
        ' and common symbol ranges that GDI+ Segoe UI can't render
        Dim sb As New System.Text.StringBuilder(text.Length)
        Dim i As Integer = 0
        While i < text.Length
            Dim c As Char = text(i)
            If Char.IsHighSurrogate(c) AndAlso i + 1 < text.Length AndAlso Char.IsLowSurrogate(text(i + 1)) Then
                ' Skip surrogate pair (emoji)
                i += 2
                Continue While
            End If
            Dim cp As Integer = AscW(c)
            ' Keep: printable ASCII, Latin, common punctuation, em/en dash
            If (cp >= 32 AndAlso cp <= 126) OrElse
               (cp >= 160 AndAlso cp <= 591) OrElse
               cp = 8212 OrElse cp = 8211 OrElse cp = 8230 Then
                sb.Append(c)
            End If
            i += 1
        End While
        Return sb.ToString().Trim(" "c, "-"c)
    End Function

    ' ── Recommendation rows painter ──────────────────────────────────────────
    Private Shared Sub DrawRecommendationRows(g As Graphics, bounds As Rectangle,
                                              items As List(Of (Text As String, Color As Color)))
        g.SmoothingMode    = SmoothingMode.AntiAlias
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias

        Dim rowH    As Integer = 44
        Dim gap     As Integer = 8
        Dim accentW As Integer = 4
        Dim radius  As Integer = 8
        Dim y       As Integer = bounds.Y + 4

        Using rowBg  As New SolidBrush(ColorTranslator.FromHtml("#F8FAFF"))
        Using textBr As New SolidBrush(ColorTranslator.FromHtml("#1E1B4B"))
        Using textFont As New Font("Segoe UI", 10.0F)
            For Each item In items
                If y + rowH > bounds.Bottom Then Exit For

                Dim rowRect As New Rectangle(bounds.X + 4, y, bounds.Width - 12, rowH)

                ' Row background with rounded corners
                Using path As New GraphicsPath()
                    Dim d As Integer = radius * 2
                    path.AddArc(rowRect.X, rowRect.Y, d, d, 180, 90)
                    path.AddArc(rowRect.Right - d, rowRect.Y, d, d, 270, 90)
                    path.AddArc(rowRect.Right - d, rowRect.Bottom - d, d, d, 0, 90)
                    path.AddArc(rowRect.X, rowRect.Bottom - d, d, d, 90, 90)
                    path.CloseFigure()
                    g.FillPath(rowBg, path)
                End Using

                ' Colored left accent bar (rounded left side)
                Using accentBr As New SolidBrush(item.Color)
                    Using accentPath As New GraphicsPath()
                        Dim d As Integer = radius * 2
                        accentPath.AddArc(rowRect.X, rowRect.Y, d, d, 180, 90)
                        accentPath.AddLine(rowRect.X + radius, rowRect.Y, rowRect.X + accentW + 2, rowRect.Y)
                        accentPath.AddLine(rowRect.X + accentW + 2, rowRect.Bottom, rowRect.X + radius, rowRect.Bottom)
                        accentPath.AddArc(rowRect.X, rowRect.Bottom - d, d, d, 90, 90)
                        accentPath.CloseFigure()
                        g.FillPath(accentBr, accentPath)
                    End Using
                End Using

                ' Colored dot indicator
                Dim dotX As Integer = rowRect.X + accentW + 12
                Dim dotY As Integer = rowRect.Y + (rowH - 10) \ 2
                Using dotBr As New SolidBrush(item.Color)
                    g.FillEllipse(dotBr, dotX, dotY, 10, 10)
                End Using

                ' Text
                Dim textX As Integer = dotX + 18
                Dim textRect As New RectangleF(textX, rowRect.Y, rowRect.Right - textX - 8, rowH)
                Dim sf As New StringFormat() With {.LineAlignment = StringAlignment.Center,
                                                    .Trimming = StringTrimming.EllipsisCharacter}
                g.DrawString(item.Text, textFont, textBr, textRect, sf)
                sf.Dispose()

                y += rowH + gap
            Next
        End Using : End Using : End Using
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        LoadAnalytics()
    End Sub
End Class
