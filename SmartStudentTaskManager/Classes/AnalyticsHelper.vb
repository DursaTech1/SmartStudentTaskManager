Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient
Imports System.Linq

''' <summary>
''' Draws GDI+ analytics charts directly onto Panel controls.
''' No third-party charting library required.
''' </summary>
Public Class AnalyticsHelper

    Public Class WeeklyPoint
        Public Property Day As String
        Public Property Done As Integer
        Public Property Total As Integer
    End Class

    ' ── Fetch data ────────────────────────────────────────────────────────────

    Public Shared Function GetPriorityBreakdown(userID As Integer) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer) From {
            {"High", 0}, {"Medium", 0}, {"Low", 0}
        }
        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT Priority, COUNT(*) AS Cnt FROM Tasks WHERE UserID=@UID AND Status='Pending' GROUP BY Priority",
                {New MySqlParameter("@UID", userID)})
            For Each row As DataRow In dt.Rows
                Dim key As String = row("Priority").ToString()
                If result.ContainsKey(key) Then result(key) = Convert.ToInt32(row("Cnt"))
            Next
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Analytics priority error: " & ex.Message)
        End Try
        Return result
    End Function

    Public Shared Function GetStatusBreakdown(userID As Integer) As Dictionary(Of String, Integer)
        Dim result As New Dictionary(Of String, Integer) From {
            {"Completed", 0}, {"Pending", 0}, {"Overdue", 0}
        }
        Try
            Dim p As MySqlParameter() = {New MySqlParameter("@UID", userID),
                                          New MySqlParameter("@Now", DateTime.Now)}
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT " &
                "SUM(CASE WHEN Status='Completed' THEN 1 ELSE 0 END) AS Done, " &
                "SUM(CASE WHEN Status='Pending' AND DueDate>=@Now THEN 1 ELSE 0 END) AS Pending, " &
                "SUM(CASE WHEN Status='Pending' AND DueDate<@Now THEN 1 ELSE 0 END) AS Overdue " &
                "FROM Tasks WHERE UserID=@UID", p)
            If dt.Rows.Count > 0 Then
                result("Completed") = If(IsDBNull(dt.Rows(0)("Done")),    0, Convert.ToInt32(dt.Rows(0)("Done")))
                result("Pending")   = If(IsDBNull(dt.Rows(0)("Pending")), 0, Convert.ToInt32(dt.Rows(0)("Pending")))
                result("Overdue")   = If(IsDBNull(dt.Rows(0)("Overdue")), 0, Convert.ToInt32(dt.Rows(0)("Overdue")))
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Analytics status error: " & ex.Message)
        End Try
        Return result
    End Function

    Public Shared Function GetWeeklyData(userID As Integer) As List(Of WeeklyPoint)
        Dim result As New List(Of WeeklyPoint)()
        Try
            For i As Integer = 6 To 0 Step -1
                Dim day As DateTime = DateTime.Today.AddDays(-i)
                Dim p As MySqlParameter() = {
                    New MySqlParameter("@UID",  userID),
                    New MySqlParameter("@Day",  day),
                    New MySqlParameter("@Next", day.AddDays(1))
                }
                Dim total As Integer = Convert.ToInt32(
                    DatabaseHelper.ExecuteScalar(
                        "SELECT COUNT(*) FROM Tasks WHERE UserID=@UID AND DueDate>=@Day AND DueDate<@Next", p))
                Dim done As Integer = Convert.ToInt32(
                    DatabaseHelper.ExecuteScalar(
                        "SELECT COUNT(*) FROM Tasks WHERE UserID=@UID AND Status='Completed' AND CompletedAt>=@Day AND CompletedAt<@Next", p))
                result.Add(New WeeklyPoint() With {
                    .Day = day.ToString("ddd"), .Done = done, .Total = total})
            Next
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Analytics weekly error: " & ex.Message)
        End Try
        Return result
    End Function

    ''' <summary>Returns total Pomodoro study minutes for the current week.</summary>
    Public Shared Function GetWeeklyStudyMinutes(userID As Integer) As Integer
        Try
            Dim weekStart As DateTime = DateTime.Today.AddDays(-7)
            Dim result As Object = DatabaseHelper.ExecuteScalar(
                "SELECT COALESCE(SUM(DurationMinutes),0) FROM StudySessions WHERE UserID=@UID AND StartedAt>=@W AND SessionType='Work'",
                {New MySqlParameter("@UID", userID),
                 New MySqlParameter("@W",   weekStart)})
            Return Convert.ToInt32(result)
        Catch
            Return 0
        End Try
    End Function

    ' ── Draw charts ───────────────────────────────────────────────────────────

    ''' <summary>Horizontal bar chart — priority breakdown.</summary>
    Public Shared Sub DrawPriorityBars(g As Graphics, bounds As Rectangle,
                                       data As Dictionary(Of String, Integer))
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim colors As New Dictionary(Of String, Color) From {
            {"High",   ColorTranslator.FromHtml("#EF4444")},
            {"Medium", ColorTranslator.FromHtml("#F59E0B")},
            {"Low",    ColorTranslator.FromHtml("#10B981")}
        }

        Dim maxVal As Integer = Math.Max(1, data.Values.Max())
        Dim barH As Integer = 32
        Dim gap As Integer = 16
        Dim labelW As Integer = 70
        Dim y As Integer = bounds.Y + 10

        Using labelFont As New Font("Segoe UI Semibold", 9.5F, FontStyle.Bold)
        Using valFont   As New Font("Segoe UI", 9.0F)
            For Each kv In data
                Dim barW As Integer = CInt((kv.Value / CDbl(maxVal)) * (bounds.Width - labelW - 50))
                barW = Math.Max(barW, 4)

                ' Label
                g.DrawString(kv.Key, labelFont, Brushes.DimGray,
                             New RectangleF(bounds.X, y, labelW, barH),
                             New StringFormat() With {.LineAlignment = StringAlignment.Center})

                ' Bar
                Using br As New SolidBrush(colors(kv.Key))
                    g.FillRectangle(br, bounds.X + labelW, y, barW, barH)
                End Using

                ' Value
                g.DrawString(kv.Value.ToString(), valFont, Brushes.DimGray,
                             bounds.X + labelW + barW + 6, y + 8)

                y += barH + gap
            Next
        End Using : End Using
    End Sub

    ''' <summary>Donut chart — status breakdown.</summary>
    Public Shared Sub DrawStatusDonut(g As Graphics, bounds As Rectangle,
                                      data As Dictionary(Of String, Integer))
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Dim colors As New Dictionary(Of String, Color) From {
            {"Completed", ColorTranslator.FromHtml("#10B981")},
            {"Pending",   ColorTranslator.FromHtml("#F59E0B")},
            {"Overdue",   ColorTranslator.FromHtml("#EF4444")}
        }

        Dim total As Integer = data.Values.Sum()
        If total = 0 Then
            Using f As New Font("Segoe UI", 11.0F)
                g.DrawString("No data yet", f, Brushes.LightGray,
                             New RectangleF(bounds.X, bounds.Y + bounds.Height / 2 - 15, bounds.Width, 30),
                             New StringFormat() With {.Alignment = StringAlignment.Center})
            End Using
            Return
        End If

        Dim cx As Integer = bounds.X + bounds.Width \ 2
        Dim cy As Integer = bounds.Y + bounds.Height \ 2
        Dim r  As Integer = Math.Min(bounds.Width, bounds.Height) \ 2 - 10
        Dim innerR As Integer = CInt(r * 0.55)

        Dim startAngle As Single = -90.0F
        For Each kv In data
            If kv.Value = 0 Then Continue For
            Dim sweep As Single = CSng(kv.Value / CDbl(total) * 360.0)
            Using br As New SolidBrush(colors(kv.Key))
                g.FillPie(br, cx - r, cy - r, r * 2, r * 2, startAngle, sweep)
            End Using
            startAngle += sweep
        Next

        ' Inner white circle (donut hole)
        g.FillEllipse(Brushes.White, cx - innerR, cy - innerR, innerR * 2, innerR * 2)

        ' Center text
        Using bigFont As New Font("Segoe UI Semibold", 14.0F, FontStyle.Bold)
        Using smallFont As New Font("Segoe UI", 8.5F)
            Dim pct As Integer = If(total > 0, CInt(data("Completed") / CDbl(total) * 100), 0)
            g.DrawString($"{pct}%", bigFont, Brushes.DimGray,
                         New RectangleF(cx - 30, cy - 18, 60, 22),
                         New StringFormat() With {.Alignment = StringAlignment.Center})
            g.DrawString("done", smallFont, Brushes.Gray,
                         New RectangleF(cx - 30, cy + 4, 60, 16),
                         New StringFormat() With {.Alignment = StringAlignment.Center})
        End Using : End Using

        ' Legend
        Dim lx As Integer = bounds.X + 8
        Dim ly As Integer = bounds.Bottom - 22
        Using f As New Font("Segoe UI", 8.5F)
            For Each kv In data
                Using br As New SolidBrush(colors(kv.Key))
                    g.FillRectangle(br, lx, ly, 12, 12)
                End Using
                g.DrawString($"{kv.Key} ({kv.Value})", f, Brushes.DimGray, lx + 16, ly)
                lx += 100
            Next
        End Using
    End Sub

    ''' <summary>Weekly bar chart — tasks done per day.</summary>
    Public Shared Sub DrawWeeklyBars(g As Graphics, bounds As Rectangle,
                                     data As List(Of WeeklyPoint))
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        If data.Count = 0 Then Return

        Dim maxVal As Integer = Math.Max(1, data.Max(Function(p) p.Total))
        Dim barW   As Integer = CInt((bounds.Width - 20) / data.Count) - 8
        Dim x      As Integer = bounds.X + 10

        Using doneColor As New SolidBrush(ColorTranslator.FromHtml("#4F46E5"))
        Using totalColor As New SolidBrush(ColorTranslator.FromHtml("#E0E7FF"))
        Using dayFont As New Font("Segoe UI", 8.5F)
            For Each pt In data
                Dim totalH As Integer = CInt((pt.Total / CDbl(maxVal)) * (bounds.Height - 40))
                Dim doneH  As Integer = CInt((pt.Done  / CDbl(maxVal)) * (bounds.Height - 40))
                Dim barY   As Integer = bounds.Bottom - 24

                ' Total bar (background)
                If totalH > 0 Then
                    g.FillRectangle(totalColor, x, barY - totalH, barW, totalH)
                End If
                ' Done bar (foreground)
                If doneH > 0 Then
                    g.FillRectangle(doneColor, x, barY - doneH, barW, doneH)
                End If

                ' Day label
                g.DrawString(pt.Day, dayFont, Brushes.Gray,
                             New RectangleF(x, barY + 4, barW, 16),
                             New StringFormat() With {.Alignment = StringAlignment.Center})

                x += barW + 8
            Next
        End Using : End Using : End Using
    End Sub
End Class
