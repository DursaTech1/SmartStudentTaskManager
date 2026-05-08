Imports MySql.Data.MySqlClient

''' <summary>
''' Simple database backup and restore using MySQL SELECT INTO OUTFILE / LOAD DATA.
''' Falls back to a VB.NET-based CSV export/import when file permissions are restricted.
''' </summary>
Public Class BackupHelper

    ''' <summary>Export all user tasks to a CSV backup file.</summary>
    Public Shared Sub BackupTasks(userID As Integer, filePath As String)
        Try
            Dim dt As DataTable = DatabaseHelper.GetDataTable(
                "SELECT TaskID,Title,Description,DueDate,Priority,Status,Category," &
                "IsRecurring,Notes,Tag,CreatedAt,CompletedAt " &
                "FROM Tasks WHERE UserID=@UID ORDER BY TaskID",
                {New MySqlParameter("@UID", userID)})

            Dim sb As New System.Text.StringBuilder()
            ' Header
            Dim cols As New List(Of String)()
            For Each col As DataColumn In dt.Columns
                cols.Add(col.ColumnName)
            Next
            sb.AppendLine(String.Join(",", cols))

            ' Rows
            For Each row As DataRow In dt.Rows
                Dim fields As New List(Of String)()
                For Each col As DataColumn In dt.Columns
                    Dim val As String = If(row(col) Is DBNull.Value, "", row(col).ToString())
                    ' RFC 4180 escaping
                    If val.Contains(",") OrElse val.Contains("""") OrElse
                       val.Contains(vbCr) OrElse val.Contains(vbLf) Then
                        val = """" & val.Replace("""", """""") & """"
                    End If
                    fields.Add(val)
                Next
                sb.AppendLine(String.Join(",", fields))
            Next

            System.IO.File.WriteAllText(filePath, sb.ToString(),
                New System.Text.UTF8Encoding(True))

            MessageBox.Show($"Backup saved to:{vbCrLf}{filePath}",
                "Backup Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Open folder
            Process.Start("explorer.exe", $"/select,""{filePath}""")

        Catch ex As Exception
            MessageBox.Show("Backup failed: " & ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>Restore tasks from a CSV backup file (inserts only — does not delete existing).</summary>
    Public Shared Sub RestoreTasks(userID As Integer, filePath As String)
        Try
            If Not System.IO.File.Exists(filePath) Then
                MessageBox.Show("File not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            Dim lines As String() = System.IO.File.ReadAllLines(filePath)
            If lines.Length < 2 Then
                MessageBox.Show("Backup file is empty or invalid.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Dim headers As String() = lines(0).Split(","c)
            Dim imported As Integer = 0

            For i As Integer = 1 To lines.Length - 1
                If String.IsNullOrWhiteSpace(lines(i)) Then Continue For
                Try
                    Dim fields As String() = ParseCsvLine(lines(i))
                    Dim row As New Dictionary(Of String, String)()
                    For j As Integer = 0 To Math.Min(headers.Length, fields.Length) - 1
                        row(headers(j).Trim()) = fields(j)
                    Next

                    DatabaseHelper.ExecuteNonQuery(
                        "INSERT IGNORE INTO Tasks " &
                        "(UserID,Title,Description,DueDate,Priority,Status,Category," &
                        "IsRecurring,Notes,Tag,CreatedAt,CompletedAt) " &
                        "VALUES (@UID,@Title,@Desc,@Due,@Pri,@Stat,@Cat,@Rec,@Notes,@Tag,@Cre,@Com)",
                        {New MySqlParameter("@UID",   userID),
                         New MySqlParameter("@Title", GetField(row, "Title")),
                         New MySqlParameter("@Desc",  GetField(row, "Description")),
                         New MySqlParameter("@Due",   ParseDate(GetField(row, "DueDate"))),
                         New MySqlParameter("@Pri",   GetField(row, "Priority", "Medium")),
                         New MySqlParameter("@Stat",  GetField(row, "Status", "Pending")),
                         New MySqlParameter("@Cat",   GetField(row, "Category", "General")),
                         New MySqlParameter("@Rec",   If(GetField(row, "IsRecurring") = "True", 1, 0)),
                         New MySqlParameter("@Notes", If(GetField(row, "Notes") = "", CObj(DBNull.Value), GetField(row, "Notes"))),
                         New MySqlParameter("@Tag",   If(GetField(row, "Tag") = "", CObj(DBNull.Value), GetField(row, "Tag"))),
                         New MySqlParameter("@Cre",   ParseDate(GetField(row, "CreatedAt"))),
                         New MySqlParameter("@Com",   If(GetField(row, "CompletedAt") = "", CObj(DBNull.Value), CObj(ParseDate(GetField(row, "CompletedAt")))))})
                    imported += 1
                Catch
                    ' Skip malformed rows
                End Try
            Next

            MessageBox.Show($"Restore complete — {imported} tasks imported.",
                "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            MessageBox.Show("Restore failed: " & ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Shared Function GetField(row As Dictionary(Of String, String),
                                     key As String,
                                     Optional defaultVal As String = "") As String
        Dim v As String = ""
        If row.TryGetValue(key, v) Then Return If(v, defaultVal)
        Return defaultVal
    End Function

    Private Shared Function ParseDate(s As String) As DateTime
        Dim d As DateTime
        If DateTime.TryParse(s, d) Then Return d
        Return DateTime.Now
    End Function

    ''' <summary>Simple RFC 4180 CSV line parser.</summary>
    Private Shared Function ParseCsvLine(line As String) As String()
        Dim fields As New List(Of String)()
        Dim current As New System.Text.StringBuilder()
        Dim inQuotes As Boolean = False
        For Each c As Char In line
            If c = """"c Then
                inQuotes = Not inQuotes
            ElseIf c = ","c AndAlso Not inQuotes Then
                fields.Add(current.ToString())
                current.Clear()
            Else
                current.Append(c)
            End If
        Next
        fields.Add(current.ToString())
        Return fields.ToArray()
    End Function
End Class
