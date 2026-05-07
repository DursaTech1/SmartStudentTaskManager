Imports System.Windows.Forms

Module Program
    <STAThread>
    Public Sub Main()
        Application.EnableVisualStyles()
        Application.SetCompatibleTextRenderingDefault(False)
        DatabaseHelper.RunMigrations()
        Application.Run(New frmLogin())
    End Sub
End Module