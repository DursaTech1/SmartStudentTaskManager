Imports MySql.Data.MySqlClient

''' <summary>
''' AI-style task recommendation engine.
''' Scores pending tasks by urgency + priority and returns actionable suggestions.
''' </summary>
Public Class TaskRecommendationEngine

    Public Class Recommendation
        Public Property TaskID As Integer
        Public Property Title As String
        Public Property DueDate As DateTime
        Public Property Priority As String
        Public Property Score As Double
        Public Property Reason As String
    End Class

    ''' <summary>Returns top recommendations for the current user.</summary>
    Public Shared Function GetRecommendations(userID As Integer, Optional topN As Integer = 5) As List(Of Recommendation)
        Dim results As New List(Of Recommendation)()

        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT TaskID, Title, DueDate, Priority, Status, Category " &
                "FROM Tasks WHERE UserID=@UserID AND Status='Pending' " &
                "ORDER BY DueDate ASC LIMIT 50",
                {New MySqlParameter("@UserID", userID)})

            If dt Is Nothing Then Return results

            Dim now As DateTime = DateTime.Now

            For Each row As DataRow In dt.Rows
                Dim rec As New Recommendation()
                rec.TaskID   = Convert.ToInt32(row("TaskID"))
                rec.Title    = row("Title").ToString()
                rec.DueDate  = Convert.ToDateTime(row("DueDate"))
                rec.Priority = row("Priority").ToString()

                ' ── Scoring ──────────────────────────────────────────────────
                Dim hoursLeft As Double = (rec.DueDate - now).TotalHours
                Dim score As Double = 0

                ' Urgency: closer = higher score
                If hoursLeft < 0 Then
                    score += 100          ' overdue
                    rec.Reason = $"⚠️ OVERDUE — complete ""{rec.Title}"" immediately!"
                ElseIf hoursLeft < 2 Then
                    score += 90
                    rec.Reason = $"🔴 Due in {CInt(hoursLeft * 60)} min — start now!"
                ElseIf hoursLeft < 24 Then
                    score += 70
                    rec.Reason = $"🟡 Due today in {CInt(hoursLeft)} hrs — prioritise this."
                ElseIf hoursLeft < 48 Then
                    score += 50
                    rec.Reason = $"🟠 Due tomorrow — start today to avoid rush."
                ElseIf hoursLeft < 72 Then
                    score += 30
                    rec.Reason = $"📅 Due in {CInt(hoursLeft / 24)} days — plan ahead."
                Else
                    score += 10
                    rec.Reason = $"📋 Due {rec.DueDate:MMM dd} — schedule time for this."
                End If

                ' Priority weight
                Select Case rec.Priority
                    Case "High"   : score += 30
                    Case "Medium" : score += 15
                    Case "Low"    : score += 5
                End Select

                rec.Score = score
                results.Add(rec)
            Next

            ' Sort by score descending
            results.Sort(Function(a, b) b.Score.CompareTo(a.Score))

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Recommendation error: " & ex.Message)
        End Try

        Return results.Take(topN).ToList()
    End Function

    ''' <summary>Returns a workload warning if too many tasks are due soon.</summary>
    Public Shared Function GetWorkloadWarning(userID As Integer) As String
        Try
            Dim tomorrow As DateTime = DateTime.Today.AddDays(1)
            Dim count As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND Status='Pending' AND DueDate<=@Tomorrow",
                    {New MySqlParameter("@UserID",   userID),
                     New MySqlParameter("@Tomorrow", tomorrow)}))

            If count >= 5 Then
                Return $"⚠️ You have {count} tasks due by tomorrow. Consider starting the highest-priority ones now."
            ElseIf count >= 3 Then
                Return $"📌 {count} tasks due by tomorrow. Stay on track!"
            End If
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Workload warning error: " & ex.Message)
        End Try
        Return ""
    End Function

    ''' <summary>Returns a productivity tip based on completion rate this week.</summary>
    Public Shared Function GetProductivityTip(userID As Integer) As String
        Try
            Dim weekStart As DateTime = DateTime.Today.AddDays(-7)
            Dim p As MySqlParameter() = {
                New MySqlParameter("@UserID",    userID),
                New MySqlParameter("@WeekStart", weekStart)
            }
            Dim due As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND DueDate>=@WeekStart", p))
            Dim done As Integer = Convert.ToInt32(
                DatabaseHelper.ExecuteScalar(
                    "SELECT COUNT(*) FROM Tasks WHERE UserID=@UserID AND Status='Completed' AND CompletedAt>=@WeekStart", p))

            If due = 0 Then Return "📝 No tasks this week yet. Add some to get started!"
            Dim rate As Integer = CInt(done / CDbl(due) * 100)

            If rate >= 80 Then Return $"🌟 Excellent! {rate}% completion rate this week. Keep it up!"
            If rate >= 50 Then Return $"👍 Good progress — {rate}% done this week. Push for more!"
            Return $"💡 Only {rate}% done this week. Try the Pomodoro timer to boost focus."
        Catch
            Return ""
        End Try
    End Function
End Class
