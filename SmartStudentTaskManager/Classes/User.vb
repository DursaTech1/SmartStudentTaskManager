Public Class User
    Public Property UserID As Integer
    Public Property Username As String
    Public Property Email As String
    Public Property FullName As String = ""
    Public Property StudentID As String = ""
    Public Property ProfilePicturePath As String = ""
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

    ''' <summary>Returns the display name: FullName if set, otherwise Username.</summary>
    Public ReadOnly Property DisplayName As String
        Get
            Return If(Not String.IsNullOrWhiteSpace(FullName), FullName, Username)
        End Get
    End Property

    ''' <summary>Returns up to 2 uppercase initials from the display name.</summary>
    Public ReadOnly Property Initials As String
        Get
            Dim name As String = DisplayName
            If name.Length >= 2 Then Return name.Substring(0, 2).ToUpper()
            Return name.ToUpper()
        End Get
    End Property
End Class

' Global variable to store current logged-in user
Public Module GlobalVariables
    Public Property CurrentUser As User
End Module