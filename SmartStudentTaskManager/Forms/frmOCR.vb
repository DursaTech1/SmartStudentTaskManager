Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

''' <summary>
''' OCR / Image Upload feature.
''' Allows users to upload an assignment image or screenshot.
''' Uses Windows OCR (Windows.Media.Ocr) via PowerShell interop when available,
''' with a manual text-entry fallback.
''' Parses extracted text to detect assignment title, due date, and subject.
''' </summary>
Public Class frmOCR

    Private _imagePath As String = ""
    Private _extractedText As String = ""

    <DllImport("user32.DLL", EntryPoint:="ReleaseCapture")>
    Private Shared Sub ReleaseCapture() : End Sub
    <DllImport("user32.DLL", EntryPoint:="SendMessage")>
    Private Shared Sub SendMessage(hWnd As IntPtr, wMsg As Integer, wParam As Integer, lParam As Integer) : End Sub

    Private Sub pnlTitleBar_MouseDown(sender As Object, e As MouseEventArgs) Handles pnlTitleBar.MouseDown
        ReleaseCapture()
        SendMessage(Me.Handle, &H112&, &HF012&, 0)
    End Sub

    Private Sub frmOCR_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized
        ThemeManager.ApplyTheme(Me)
        pnlTitleBar.BackColor = ThemeManager.TitleBarColor
        CenterCard()
        AddHandler Me.Resize, Sub(s As Object, ev As EventArgs) CenterCard()
    End Sub

    Private Sub CenterCard()
        If pnlCard Is Nothing OrElse pnlCenter Is Nothing Then Return
        pnlCard.Location = New Point(
            Math.Max(0, (pnlCenter.ClientSize.Width  - pnlCard.Width)  \ 2),
            Math.Max(20, (pnlCenter.ClientSize.Height - pnlCard.Height) \ 2))
    End Sub

    ' ── Upload image ─────────────────────────────────────────────────────────

    Private Sub btnUploadImage_Click(sender As Object, e As EventArgs) Handles btnUploadImage.Click
        Using ofd As New OpenFileDialog()
            ofd.Title  = "Select Assignment Image or Screenshot"
            ofd.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tiff"
            If ofd.ShowDialog() <> DialogResult.OK Then Return
            _imagePath = ofd.FileName
            lblImagePath.Text = Path.GetFileName(_imagePath)

            ' Show preview
            Try
                picPreview.Image?.Dispose()
                picPreview.Image = Image.FromFile(_imagePath)
                picPreview.Visible = True
            Catch
                picPreview.Visible = False
            End Try

            ' Attempt OCR
            btnExtract.Enabled = True
            lblStatus.Text = "Image loaded. Click 'Extract Text' to run OCR."
            lblStatus.ForeColor = ThemeManager.PrimaryColor
        End Using
    End Sub

    ' ── Extract text via Windows OCR (PowerShell) ────────────────────────────

    Private Sub btnExtract_Click(sender As Object, e As EventArgs) Handles btnExtract.Click
        If _imagePath = "" Then Return
        lblStatus.Text = "Running OCR…"
        lblStatus.ForeColor = ThemeManager.WarningColor
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()

        Try
            _extractedText = RunWindowsOCR(_imagePath)
            If String.IsNullOrWhiteSpace(_extractedText) Then
                lblStatus.Text = "OCR returned no text. Please type the text manually below."
                lblStatus.ForeColor = ThemeManager.WarningColor
            Else
                txtExtractedText.Text = _extractedText
                lblStatus.Text = "✅ Text extracted successfully. Review and edit below."
                lblStatus.ForeColor = ThemeManager.SuccessColor
                ParseAndFill(_extractedText)
            End If
        Catch ex As Exception
            lblStatus.Text = "OCR unavailable — please type the text manually."
            lblStatus.ForeColor = ThemeManager.MutedTextColor
            System.Diagnostics.Debug.WriteLine("OCR error: " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' Runs Windows OCR via a PowerShell script that uses Windows.Media.Ocr.
    ''' Requires Windows 10/11 with the language pack installed.
    ''' </summary>
    Private Shared Function RunWindowsOCR(imagePath As String) As String
        ' PowerShell script that uses Windows.Media.Ocr
        Dim ps As String =
            "[Windows.Media.Ocr.OcrEngine,Windows.Foundation,ContentType=WindowsRuntime] | Out-Null;" &
            "[Windows.Storage.StorageFile,Windows.Foundation,ContentType=WindowsRuntime] | Out-Null;" &
            "[Windows.Graphics.Imaging.BitmapDecoder,Windows.Foundation,ContentType=WindowsRuntime] | Out-Null;" &
            "$path = '" & imagePath.Replace("'", "''") & "';" &
            "$file = [Windows.Storage.StorageFile]::GetFileFromPathAsync($path).GetAwaiter().GetResult();" &
            "$stream = $file.OpenAsync([Windows.Storage.FileAccessMode]::Read).GetAwaiter().GetResult();" &
            "$decoder = [Windows.Graphics.Imaging.BitmapDecoder]::CreateAsync($stream).GetAwaiter().GetResult();" &
            "$bitmap = $decoder.GetSoftwareBitmapAsync().GetAwaiter().GetResult();" &
            "$engine = [Windows.Media.Ocr.OcrEngine]::TryCreateFromUserProfileLanguages();" &
            "$result = $engine.RecognizeAsync($bitmap).GetAwaiter().GetResult();" &
            "Write-Output $result.Text"

        Dim psi As New System.Diagnostics.ProcessStartInfo("powershell.exe") With {
            .Arguments = $"-NoProfile -NonInteractive -Command ""{ps}""",
            .RedirectStandardOutput = True,
            .RedirectStandardError = True,
            .UseShellExecute = False,
            .CreateNoWindow = True
        }

        Using proc As New System.Diagnostics.Process()
            proc.StartInfo = psi
            proc.Start()
            Dim output As String = proc.StandardOutput.ReadToEnd()
            proc.WaitForExit(15_000)
            Return output.Trim()
        End Using
    End Function

    ' ── Parse extracted text ─────────────────────────────────────────────────

    Private Sub btnParse_Click(sender As Object, e As EventArgs) Handles btnParse.Click
        Dim text As String = txtExtractedText.Text.Trim()
        If text = "" Then
            MessageBox.Show("Please enter or extract some text first.", "No Text",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        ParseAndFill(text)
    End Sub

    ''' <summary>
    ''' Heuristic parser: looks for assignment title, due date, and subject
    ''' in the extracted text using common patterns found in assignment notices.
    ''' </summary>
    Private Sub ParseAndFill(text As String)
        ' ── Detect title ─────────────────────────────────────────────────────
        ' Look for lines containing "assignment", "task", "project", "homework"
        Dim titlePatterns As String() = {
            "(?:assignment|task|project|homework|lab|quiz|exam)[:\s]+(.+)",
            "(?:title|subject|topic)[:\s]+(.+)",
            "^(.{10,80})$"   ' fallback: first non-empty line of reasonable length
        }
        Dim detectedTitle As String = ""
        For Each pattern In titlePatterns
            Dim m As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase Or RegexOptions.Multiline)
            If m.Success Then
                detectedTitle = m.Groups(If(m.Groups.Count > 1, 1, 0)).Value.Trim()
                If detectedTitle.Length > 5 Then Exit For
            End If
        Next
        If detectedTitle <> "" Then txtTitle.Text = detectedTitle

        ' ── Detect due date ───────────────────────────────────────────────────
        ' Patterns: "due: 15 Jan 2025", "deadline: 2025-01-15", "submit by January 15"
        Dim datePatterns As String() = {
            "(?:due|deadline|submit(?:\s+by)?|due\s+date)[:\s]+(\d{1,2}[\/\-\.]\d{1,2}[\/\-\.]\d{2,4})",
            "(?:due|deadline|submit(?:\s+by)?|due\s+date)[:\s]+(\d{1,2}\s+\w+\s+\d{4})",
            "(?:due|deadline|submit(?:\s+by)?|due\s+date)[:\s]+(\w+\s+\d{1,2},?\s+\d{4})",
            "(\d{1,2}[\/\-\.]\d{1,2}[\/\-\.]\d{2,4})",
            "(\d{4}[\/\-\.]\d{1,2}[\/\-\.]\d{1,2})"
        }
        Dim detectedDate As DateTime = DateTime.MinValue
        For Each pattern In datePatterns
            Dim m As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If m.Success Then
                Dim dateStr As String = m.Groups(If(m.Groups.Count > 1, 1, 0)).Value.Trim()
                If DateTime.TryParse(dateStr, detectedDate) Then Exit For
            End If
        Next
        If detectedDate > DateTime.MinValue Then
            dtpDueDate.Value = detectedDate
        End If

        ' ── Detect subject ────────────────────────────────────────────────────
        Dim subjectPatterns As String() = {
            "(?:subject|course|class|module)[:\s]+(.+)",
            "(?:for|in)[:\s]+(.{5,50})\s+(?:class|course|subject)"
        }
        Dim detectedSubject As String = ""
        For Each pattern In subjectPatterns
            Dim m As Match = Regex.Match(text, pattern, RegexOptions.IgnoreCase)
            If m.Success Then
                detectedSubject = m.Groups(If(m.Groups.Count > 1, 1, 0)).Value.Trim()
                If detectedSubject.Length > 2 Then Exit For
            End If
        Next
        If detectedSubject <> "" Then txtSubject.Text = detectedSubject

        lblStatus.Text = "✅ Parsing complete. Review the fields below and click 'Create Task'."
        lblStatus.ForeColor = ThemeManager.SuccessColor
    End Sub

    ' ── Create task from extracted data ──────────────────────────────────────

    Private Sub btnCreateTask_Click(sender As Object, e As EventArgs) Handles btnCreateTask.Click
        If String.IsNullOrWhiteSpace(txtTitle.Text) Then
            MessageBox.Show("Please enter a task title.", "Validation",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtTitle.Focus()
            Return
        End If

        Try
            Dim uid As Integer = GlobalVariables.CurrentUser.UserID
            Dim title As String = txtTitle.Text.Trim()
            Dim subject As String = If(txtSubject.Text.Trim() <> "", txtSubject.Text.Trim(), "General")
            Dim dueDate As DateTime = dtpDueDate.Value
            Dim priority As String = If(cmbPriority.SelectedItem IsNot Nothing,
                                        cmbPriority.SelectedItem.ToString(), "Medium")

            ' Insert task
            DatabaseHelper.ExecuteNonQuery(
                "INSERT INTO Tasks (UserID, Title, Description, DueDate, Priority, Status, Category, Notes) " &
                "VALUES (@UserID, @Title, @Desc, @Due, @Pri, 'Pending', @Cat, @Notes)",
                {New MySqlParameter("@UserID", uid),
                 New MySqlParameter("@Title",  title),
                 New MySqlParameter("@Desc",   If(txtDescription.Text.Trim() <> "", txtDescription.Text.Trim(), CObj(DBNull.Value))),
                 New MySqlParameter("@Due",    dueDate),
                 New MySqlParameter("@Pri",    priority),
                 New MySqlParameter("@Cat",    subject),
                 New MySqlParameter("@Notes",  If(_extractedText <> "", CObj("OCR Source: " & _extractedText.Substring(0, Math.Min(500, _extractedText.Length))), CObj(DBNull.Value)))})

            ' Log OCR extraction
            Try
                DatabaseHelper.ExecuteNonQuery(
                    "INSERT INTO OCRExtractions (UserID, ImagePath, ExtractedText, DetectedTitle, DetectedDueDate, DetectedSubject, TaskCreated) " &
                    "VALUES (@UID, @ImgPath, @Text, @Title, @Due, @Subject, TRUE)",
                    {New MySqlParameter("@UID",     uid),
                     New MySqlParameter("@ImgPath", If(_imagePath <> "", CObj(_imagePath), CObj(DBNull.Value))),
                     New MySqlParameter("@Text",    If(_extractedText <> "", CObj(_extractedText.Substring(0, Math.Min(2000, _extractedText.Length))), CObj(DBNull.Value))),
                     New MySqlParameter("@Title",   title),
                     New MySqlParameter("@Due",     If(dueDate > DateTime.MinValue, CObj(dueDate), CObj(DBNull.Value))),
                     New MySqlParameter("@Subject", subject)})
            Catch
                ' Non-critical — ignore logging errors
            End Try

            MessageBox.Show($"✅ Task ""{title}"" created successfully!", "Task Created",
                            MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Error creating task: " & ex.Message, "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class
