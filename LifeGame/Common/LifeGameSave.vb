Public Class LifeGameSave
    Property data As New List(Of LifeGameSaveData)

    Public Overrides Function ToString() As String
        Return Now.ToString("yyyy/MM/dd HH:mm:ss")
    End Function
End Class
