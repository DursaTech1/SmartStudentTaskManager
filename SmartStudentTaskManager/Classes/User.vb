Public Class User
    Public Property UserID As Integer
    Public Property Username As String
    Public Property Email As String
    Public Property Role As String
    Public Property CreatedAt As DateTime

    Public Sub New()
    End Sub

    Public Sub New(ByVal userID As Integer, ByVal username As String, ByVal email As String, ByVal role As String)
        Me.UserID = userID
        Me.Username = username
        Me.Email = email
        Me.Role = role
    End Sub
End Class

' Global variable to store current logged-in user
Public Module GlobalVariables
    Public Property CurrentUser As User
End Module