Imports System.Collections.Generic
Imports System.Drawing
Imports System.Windows.Forms
Imports MySql.Data.MySqlClient

Public Class TaskReminderService
    Private ReadOnly _notify As NotifyIcon
    Private ReadOnly _timer As Timer
    Private ReadOnly _notifiedTaskIds As HashSet(Of Integer)   ' upcoming reminders
    Private ReadOnly _timesUpTaskIds As HashSet(Of Integer)    ' already fired "Time's Up"

    Public Property LookAhead As TimeSpan = TimeSpan.FromHours(24)
    Public Property PollInterval As TimeSpan = TimeSpan.FromMinutes(2)

    ''' <summary>Raised on the UI thread when one or more tasks just hit their due time.</summary>
    Public Event TimesUp(taskIDs As List(Of Integer), titles As List(Of String))

    Public Sub New()
        _notifiedTaskIds = New HashSet(Of Integer)()
        _timesUpTaskIds  = New HashSet(Of Integer)()

        _notify = New NotifyIcon() With {
            .Icon    = SystemIcons.Exclamation,
            .Visible = True,
            .Text    = "Smart Student Task Manager"
        }

        _timer = New Timer()
        AddHandler _timer.Tick, AddressOf OnTick
    End Sub

    Public Sub Start()
        _timer.Interval = CInt(Math.Max(30_000, PollInterval.TotalMilliseconds))
        _timer.Start()
        CheckNow()
    End Sub

    Public Sub [Stop]()
        _timer.Stop()
    End Sub

    Public Sub Dispose()
        _timer.Stop()
        RemoveHandler _timer.Tick, AddressOf OnTick
        _timer.Dispose()
        _notify.Visible = False
        _notify.Dispose()
    End Sub

    Public Sub CheckNow()
        If GlobalVariables.CurrentUser Is Nothing Then Return

        Dim now       As DateTime = DateTime.Now
        Dim dueBefore As DateTime = now.Add(LookAhead)

        Try
            ' ── 1. Upcoming reminders (within LookAhead window) ───────────────
            Dim upcomingQuery As String =
                "SELECT TaskID, Title, DueDate " &
                "FROM Tasks " &
                "WHERE UserID=@UserID AND Status='Pending' " &
                "AND DueDate >= @Now AND DueDate <= @DueBefore " &
                "ORDER BY DueDate ASC LIMIT 20"

            Dim upcomingParams As MySqlParameter() = {
                New MySqlParameter("@UserID",    GlobalVariables.CurrentUser.UserID),
                New MySqlParameter("@Now",       now),
                New MySqlParameter("@DueBefore", dueBefore)
            }

            Dim dtUpcoming As DataTable = DatabaseHelper.GetDataTable(upcomingQuery, upcomingParams)
            For Each row As DataRow In dtUpcoming.Rows
                Dim taskId As Integer = Convert.ToInt32(row("TaskID"))
                If _notifiedTaskIds.Contains(taskId) Then Continue For
                _notifiedTaskIds.Add(taskId)
                ShowUpcomingReminder(row("Title").ToString(), Convert.ToDateTime(row("DueDate")))
            Next

            ' ── 2. "Time's Up!" — tasks whose DueDate is within the last 2 min ─
            Dim timesUpQuery As String =
                "SELECT TaskID, Title, DueDate " &
                "FROM Tasks " &
                "WHERE UserID=@UserID AND Status='Pending' " &
                "AND DueDate >= @WindowStart AND DueDate <= @WindowEnd " &
                "ORDER BY DueDate ASC LIMIT 20"

            ' Window = [now - 2 min, now]  so we catch tasks that just became due
            Dim timesUpParams As MySqlParameter() = {
                New MySqlParameter("@UserID",      GlobalVariables.CurrentUser.UserID),
                New MySqlParameter("@WindowStart", now.AddMinutes(-2)),
                New MySqlParameter("@WindowEnd",   now)
            }

            Dim dtDue As DataTable = DatabaseHelper.GetDataTable(timesUpQuery, timesUpParams)
            Dim newDueIDs    As New List(Of Integer)()
            Dim newDueTitles As New List(Of String)()

            For Each row As DataRow In dtDue.Rows
                Dim taskId As Integer = Convert.ToInt32(row("TaskID"))
                If _timesUpTaskIds.Contains(taskId) Then Continue For
                _timesUpTaskIds.Add(taskId)
                newDueIDs.Add(taskId)
                newDueTitles.Add(row("Title").ToString())
            Next

            If newDueIDs.Count > 0 Then
                ShowTimesUpNotification(newDueTitles)
                RaiseEvent TimesUp(newDueIDs, newDueTitles)
            End If

        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Reminder check failed: " & ex.Message)
        End Try
    End Sub

    Private Sub OnTick(sender As Object, e As EventArgs)
        CheckNow()
    End Sub

    ' ── Upcoming reminder (hours before due) ─────────────────────────────────
    Private Sub ShowUpcomingReminder(title As String, due As DateTime)
        Dim mins As Integer = CInt(Math.Round((due - DateTime.Now).TotalMinutes))
        Dim whenText As String = If(mins <= 0, "Due now",
                                 If(mins < 60, $"Due in {mins} min",
                                              "Due " & due.ToString("g")))

        _notify.BalloonTipTitle = "⏰ Upcoming Task"
        _notify.BalloonTipText  = $"{title}{Environment.NewLine}{whenText}"
        _notify.BalloonTipIcon  = ToolTipIcon.Info
        _notify.ShowBalloonTip(6_000)
    End Sub

    ' ── "Time's Up!" notification ─────────────────────────────────────────────
    Private Sub ShowTimesUpNotification(titles As List(Of String))
        Dim body As String
        If titles.Count = 1 Then
            body = $"⏰ Time's Up!{Environment.NewLine}{titles(0)}"
        Else
            body = $"⏰ Time's Up! {titles.Count} tasks are now due:{Environment.NewLine}" &
                   String.Join(", ", titles.Take(3)) &
                   If(titles.Count > 3, "…", "")
        End If

        _notify.BalloonTipTitle = "⏰ Time's Up!"
        _notify.BalloonTipText  = body
        _notify.BalloonTipIcon  = ToolTipIcon.Warning
        _notify.ShowBalloonTip(8_000)
    End Sub

    ''' <summary>Remove a task from the "times up" set so it can fire again
    ''' (e.g. after snooze or if the user re-opens the app).</summary>
    Public Sub ResetTimesUp(taskId As Integer)
        _timesUpTaskIds.Remove(taskId)
        _notifiedTaskIds.Remove(taskId)
    End Sub
End Class
