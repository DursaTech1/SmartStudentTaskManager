Imports System.Security.Cryptography
Imports System.Text

''' <summary>
''' Handles password hashing and verification.
''' New passwords use PBKDF2-SHA256 with a random salt (format: "pbkdf2:<salt>:<hash>").
''' Legacy SHA256 plain hashes are still verified for backward compatibility.
''' </summary>
Public Class SecurityHelper

    Private Const Pbkdf2Iterations As Integer = 100_000
    Private Const Pbkdf2HashBytes As Integer = 32   ' 256-bit output
    Private Const Pbkdf2SaltBytes As Integer = 16   ' 128-bit salt
    Private Const Pbkdf2Prefix As String = "pbkdf2:"

    ''' <summary>Hashes a password using PBKDF2-SHA256 with a random salt.</summary>
    Public Shared Function HashPassword(ByVal password As String) As String
        Dim saltBytes(Pbkdf2SaltBytes - 1) As Byte
        Using rng As RandomNumberGenerator = RandomNumberGenerator.Create()
            rng.GetBytes(saltBytes)
        End Using

        Dim hashBytes As Byte() = Pbkdf2Derive(password, saltBytes)

        Dim salt As String = Convert.ToBase64String(saltBytes)
        Dim hash As String = Convert.ToBase64String(hashBytes)
        Return Pbkdf2Prefix & salt & ":" & hash
    End Function

    ''' <summary>
    ''' Verifies a plaintext password against a stored hash.
    ''' Supports both PBKDF2 (new) and legacy SHA256 (old) formats.
    ''' </summary>
    Public Shared Function VerifyPassword(ByVal inputPassword As String, ByVal storedHash As String) As Boolean
        If String.IsNullOrEmpty(storedHash) Then Return False

        If storedHash.StartsWith(Pbkdf2Prefix) Then
            ' New PBKDF2 format: "pbkdf2:<salt_b64>:<hash_b64>"
            Dim parts As String() = storedHash.Substring(Pbkdf2Prefix.Length).Split(":"c)
            If parts.Length <> 2 Then Return False
            Try
                Dim saltBytes As Byte() = Convert.FromBase64String(parts(0))
                Dim expectedHash As Byte() = Convert.FromBase64String(parts(1))
                Dim actualHash As Byte() = Pbkdf2Derive(inputPassword, saltBytes)
                Return CryptographicEquals(actualHash, expectedHash)
            Catch
                Return False
            End Try
        Else
            ' Legacy SHA256 format (plain hex string)
            Dim inputHash As String = LegacySha256(inputPassword)
            Return String.Equals(inputHash, storedHash, StringComparison.OrdinalIgnoreCase)
        End If
    End Function

    ''' <summary>
    ''' Returns True if the stored hash is in the legacy SHA256 format.
    ''' Use this to prompt an upgrade on next login.
    ''' </summary>
    Public Shared Function IsLegacyHash(ByVal storedHash As String) As Boolean
        Return Not storedHash.StartsWith(Pbkdf2Prefix)
    End Function

    ' ── Private helpers ──────────────────────────────────────────────────────

    Private Shared Function Pbkdf2Derive(password As String, salt As Byte()) As Byte()
        Return Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password),
            salt,
            Pbkdf2Iterations,
            HashAlgorithmName.SHA256,
            Pbkdf2HashBytes)
    End Function

    Private Shared Function LegacySha256(password As String) As String
        Using sha As SHA256 = SHA256.Create()
            Dim bytes As Byte() = sha.ComputeHash(Encoding.UTF8.GetBytes(password))
            Return BitConverter.ToString(bytes).Replace("-", "").ToLower()
        End Using
    End Function

    ''' <summary>Constant-time byte array comparison to prevent timing attacks.</summary>
    Private Shared Function CryptographicEquals(a As Byte(), b As Byte()) As Boolean
        If a.Length <> b.Length Then Return False
        Dim diff As Integer = 0
        For i As Integer = 0 To a.Length - 1
            diff = diff Or (a(i) Xor b(i))
        Next
        Return diff = 0
    End Function

End Class
