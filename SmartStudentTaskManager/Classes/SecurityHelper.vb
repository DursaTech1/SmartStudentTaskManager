Imports System.Security.Cryptography
Imports System.Text

Public Class SecurityHelper
    Public Shared Function HashPassword(ByVal password As String) As String
        Using sha256 As SHA256 = SHA256.Create()
            Dim hashedBytes As Byte() = sha256.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower()
        End Using
    End Function

    Public Shared Function VerifyPassword(ByVal inputPassword As String, ByVal storedHash As String) As Boolean
        Dim inputHash As String = HashPassword(inputPassword)
        Return String.Equals(inputHash, storedHash, StringComparison.OrdinalIgnoreCase)
    End Function
End Class