Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    Partial Friend Class MyApplication

        Private Sub MyApplication_Startup(sender As Object, e As StartupEventArgs) Handles Me.Startup
            ' Run DB migrations (adds Notes column, Subtasks table, etc.)
            Try
                DatabaseHelper.RunMigrations()
            Catch ex As Exception
                System.Diagnostics.Debug.WriteLine("Migration warning: " & ex.Message)
            End Try
        End Sub

    End Class
End Namespace
