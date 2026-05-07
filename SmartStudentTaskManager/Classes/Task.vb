Public Class Task
    Public Property TaskID As Integer
    Public Property UserID As Integer
    Public Property Title As String
    Public Property Description As String
    Public Property DueDate As DateTime
    Public Property Priority As String
    Public Property Status As String
    Public Property CreatedAt As DateTime
    Public Property CompletedAt As DateTime?
    Public Property Category As String
    Public Property IsRecurring As Boolean

    Public Sub New()
    End Sub

    Public Sub New(ByVal title As String, ByVal description As String, ByVal dueDate As DateTime,
                   ByVal priority As String, ByVal status As String, ByVal category As String, ByVal isRecurring As Boolean)
        Me.Title = title
        Me.Description = description
        Me.DueDate = dueDate
        Me.Priority = priority
        Me.Status = status
        Me.Category = category
        Me.IsRecurring = isRecurring
    End Sub
End Class