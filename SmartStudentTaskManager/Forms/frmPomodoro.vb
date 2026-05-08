Imports System.Runtime.InteropServices
Imports MySql.Data.MySqlClient

''' <summary>
''' Built-in Pomodoro / Study Timer
''' 25 min work → 5 min break, with session tracking and motivational messages.
''' </summary>
Public Class frmPomodoro

    ' ── State ────────────────────────────────────────────────────────────────
    Private _timer As Timer
    Private _secondsLeft As Integer
    Private _isWork As Boolean = True
    Private _sessionCount As Integer = 0
    Private _isRunning As Boolean = False

    Private Const WorkSeconds As Integer = 25 * 60
    Private Const ShortBreakSeconds As Integer = 5 * 60
    Private Const LongBreakSeconds As Integer = 15 * 60

    Private ReadOnly _workMessages As String() = {
        "Stay focused — you're doing great! 💪",
        "Deep work mode: silence distractions.",
        "One task at a time. You've got this!",
        "Focus is a superpower. Use it now.",
        "25 minutes of focus changes everything."
    }
    Private ReadOnly _breakMessages As String() = {
        "Great work! Take a well-earned break. ☕",
        "Stretch, breathe, hydrate. Back soon!",
        "Rest is part of productivity. Enjoy!",
        "Your brain needs this. Relax for 5 min.",
        "Break time! Step away from the screen."
    }

    ' ── DLL imports for borderless drag ──────────────────────────────────────
    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture() : End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) : End Sub

    ' ── Constructor ───────────────────────────────────────────────────────────
    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub frmPomodoro_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)

        _timer = New Timer() With {.Interval = 1000}
        AddHandler _timer.Tick, AddressOf OnTick

        ResetToWork()
        UpdateUI()
        CenterCard()
        AddHandler Me.Resize, Sub(s As Object, ev As EventArgs) CenterCard()
    End Sub

    Private Sub CenterCard()
        If pnlTimer Is Nothing OrElse pnlCenter Is Nothing Then Return
        pnlTimer.Location = New Point(
            Math.Max(0, (pnlCenter.ClientSize.Width  - pnlTimer.Width)  \ 2),
            Math.Max(20, (pnlCenter.ClientSize.Height - pnlTimer.Height) \ 2))
    End Sub

    ' ── Timer logic ───────────────────────────────────────────────────────────
    Private Sub OnTick(sender As Object, e As EventArgs)
        _secondsLeft -= 1
        UpdateDisplay()

        If _secondsLeft <= 0 Then
            _timer.Stop()
            _isRunning = False

            If _isWork Then
                _sessionCount += 1
                lblSessions.Text = $"Sessions: {_sessionCount}"
                ' Save completed work session to database
                SaveStudySession(25, "Work")
                ' Every 4 sessions → long break
                Dim breakSecs As Integer = If(_sessionCount Mod 4 = 0, LongBreakSeconds, ShortBreakSeconds)
                StartBreak(breakSecs)
            Else
                StartWork()
            End If
        End If
    End Sub

    ''' <summary>Saves a completed study session to the StudySessions table.</summary>
    Private Shared Sub SaveStudySession(durationMinutes As Integer, sessionType As String)
        If GlobalVariables.CurrentUser Is Nothing Then Return
        Try
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO StudySessions (UserID, StartedAt, DurationMinutes, SessionType) VALUES (@UID, @Start, @Dur, @Type)",
                {New MySqlParameter("@UID",   GlobalVariables.CurrentUser.UserID),
                 New MySqlParameter("@Start", DateTime.Now.AddMinutes(-durationMinutes)),
                 New MySqlParameter("@Dur",   durationMinutes),
                 New MySqlParameter("@Type",  sessionType)})
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("SaveStudySession error: " & ex.Message)
        End Try
    End Sub

    Private Sub StartWork()
        _isWork = True
        _secondsLeft = WorkSeconds
        lblPhase.Text = "🎯 Focus Time"
        lblPhase.ForeColor = ThemeManager.PrimaryColor
        pnlTimer.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        lblMotivation.Text = _workMessages(New Random().Next(_workMessages.Length))
        btnStartPause.Text = "▶  Start"
        _isRunning = False
        UpdateDisplay()
    End Sub

    Private Sub StartBreak(seconds As Integer)
        _isWork = False
        _secondsLeft = seconds
        lblPhase.Text = If(seconds = LongBreakSeconds, "☕ Long Break", "☕ Short Break")
        lblPhase.ForeColor = ThemeManager.SuccessColor
        pnlTimer.BackColor = ColorTranslator.FromHtml("#ECFDF5")
        lblMotivation.Text = _breakMessages(New Random().Next(_breakMessages.Length))
        btnStartPause.Text = "▶  Start Break"
        _isRunning = False
        UpdateDisplay()

        ' Auto-notify
        Dim msg As String = If(seconds = LongBreakSeconds,
            "🎉 4 sessions done! Take a 15-minute break.",
            "✅ Session complete! Take a 5-minute break.")
        MessageBox.Show(msg, "Pomodoro", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ResetToWork()
        _isWork = True
        _secondsLeft = WorkSeconds
        lblPhase.Text = "🎯 Focus Time"
        lblPhase.ForeColor = ThemeManager.PrimaryColor
        pnlTimer.BackColor = ColorTranslator.FromHtml("#EEF2FF")
        lblMotivation.Text = _workMessages(0)
    End Sub

    Private Sub UpdateDisplay()
        Dim mins As Integer = _secondsLeft \ 60
        Dim secs As Integer = _secondsLeft Mod 60
        lblTime.Text = $"{mins:D2}:{secs:D2}"

        ' Progress arc
        Dim total As Integer = If(_isWork, WorkSeconds, If(_secondsLeft > ShortBreakSeconds, LongBreakSeconds, ShortBreakSeconds))
        Dim pct As Double = 1.0 - (_secondsLeft / CDbl(total))
        pbProgress.Value = CInt(pct * 100)
    End Sub

    Private Sub UpdateUI()
        UpdateDisplay()
        lblSessions.Text = $"Sessions: {_sessionCount}"
    End Sub

    ' ── Button handlers ───────────────────────────────────────────────────────
    Private Sub btnStartPause_Click(sender As Object, e As EventArgs) Handles btnStartPause.Click
        If _isRunning Then
            _timer.Stop()
            _isRunning = False
            btnStartPause.Text = "▶  Resume"
        Else
            _timer.Start()
            _isRunning = True
            btnStartPause.Text = "⏸  Pause"
        End If
    End Sub

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        _timer.Stop()
        _isRunning = False
        ResetToWork()
        UpdateDisplay()
        btnStartPause.Text = "▶  Start"
    End Sub

    Private Sub btnSkip_Click(sender As Object, e As EventArgs) Handles btnSkip.Click
        _timer.Stop()
        _isRunning = False
        If _isWork Then
            _sessionCount += 1
            lblSessions.Text = $"Sessions: {_sessionCount}"
            ' Save partial session (time elapsed)
            Dim elapsed As Integer = (WorkSeconds - _secondsLeft) \ 60
            If elapsed > 0 Then SaveStudySession(elapsed, "Work (Skipped)")
            StartBreak(ShortBreakSeconds)
        Else
            StartWork()
        End If
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        _timer?.Stop()
        Me.Close()
    End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Protected Overrides Sub OnFormClosed(e As FormClosedEventArgs)
        _timer?.Stop()
        _timer?.Dispose()
        MyBase.OnFormClosed(e)
    End Sub
End Class
