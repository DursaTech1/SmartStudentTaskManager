Imports MySql.Data.MySqlClient

Public Class DatabaseHelper
    ' UPDATE THIS CONNECTION STRING WITH YOUR MYSQL DETAILS
    Private Shared ConnectionString As String = "Server=localhost;Database=TaskManagerDB;Uid=root;Pwd=Murad096070;"

    Public Shared Function GetConnection() As MySqlConnection
        Return New MySqlConnection(ConnectionString)
    End Function

    Public Shared Function ExecuteNonQuery(ByVal query As String, ByVal parameters As MySqlParameter()) As Integer
        Using conn As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                conn.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Shared Function ExecuteReader(ByVal query As String, ByVal parameters As MySqlParameter()) As DataTable
        ' Returns a disconnected DataTable — safe to use after connection closes
        Return GetDataTable(query, parameters)
    End Function

    Public Shared Function ExecuteScalar(ByVal query As String, ByVal parameters As MySqlParameter()) As Object
        Using conn As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                conn.Open()
                Return cmd.ExecuteScalar()
            End Using
        End Using
    End Function

    Public Shared Function GetDataTable(ByVal query As String, ByVal parameters As MySqlParameter()) As DataTable
        Using conn As MySqlConnection = GetConnection()
            Using cmd As New MySqlCommand(query, conn)
                If parameters IsNot Nothing Then
                    cmd.Parameters.AddRange(parameters)
                End If
                Using da As New MySqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    Return dt
                End Using
            End Using
        End Using
    End Function

    Public Shared Sub RunMigrations()
        Try
            Using conn As MySqlConnection = GetConnection()
                conn.Open()

                Dim checks As New Dictionary(Of String, String) From {
                    {"Category",    "ALTER TABLE Tasks ADD Category VARCHAR(50) DEFAULT 'General'"},
                    {"IsRecurring", "ALTER TABLE Tasks ADD IsRecurring BOOLEAN DEFAULT FALSE"},
                    {"Notes",       "ALTER TABLE Tasks ADD Notes TEXT NULL"},
                    {"Tag",         "ALTER TABLE Tasks ADD Tag VARCHAR(30) NULL"},
                    {"CompletedAt", "ALTER TABLE Tasks ADD CompletedAt DATETIME NULL"}
                }

                For Each kv In checks
                    Dim q As String = "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Tasks' AND COLUMN_NAME=@col"
                    Using cmd As New MySqlCommand(q, conn)
                        cmd.Parameters.AddWithValue("@col", kv.Key)
                        If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                            Using alter As New MySqlCommand(kv.Value, conn)
                                alter.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                Next

                ' Ensure Users table has Email and Role columns
                Dim userChecks As New Dictionary(Of String, String) From {
                    {"Email", "ALTER TABLE Users ADD Email VARCHAR(254) NULL"},
                    {"Role",  "ALTER TABLE Users ADD Role VARCHAR(50) DEFAULT 'Student'"}
                }
                For Each kv In userChecks
                    Dim q As String = "SELECT COUNT(*) FROM information_schema.COLUMNS WHERE TABLE_SCHEMA=DATABASE() AND TABLE_NAME='Users' AND COLUMN_NAME=@col"
                    Using cmd As New MySqlCommand(q, conn)
                        cmd.Parameters.AddWithValue("@col", kv.Key)
                        If Convert.ToInt32(cmd.ExecuteScalar()) = 0 Then
                            Using alter As New MySqlCommand(kv.Value, conn)
                                alter.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                Next

                ' Subtasks table
                Using cmd As New MySqlCommand(
                    "CREATE TABLE IF NOT EXISTS Subtasks (" &
                    "SubtaskID INT AUTO_INCREMENT PRIMARY KEY," &
                    "TaskID INT NOT NULL," &
                    "Title VARCHAR(255) NOT NULL," &
                    "IsCompleted BOOLEAN DEFAULT FALSE," &
                    "FOREIGN KEY (TaskID) REFERENCES Tasks(TaskID) ON DELETE CASCADE)", conn)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            System.Diagnostics.Debug.WriteLine("Migration failed: " & ex.Message)
        End Try
    End Sub
End Class