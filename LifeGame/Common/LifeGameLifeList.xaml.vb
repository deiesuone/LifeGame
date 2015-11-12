Public Class LifeGameLifeList
    Public Event SendLifeGameSave(sender As Object, e As SendLifeGameSaveEventArgs)

    Public Sub SetData(obj As LifeGameSave)
        Dim item As New ListBoxItem With {.Content = obj}
        Me.lstObj.Items.Add(item)
        Me.lstObj.ScrollIntoView(item)
        Me.lstObj.SelectedIndex = Me.lstObj.Items.Count - 1
    End Sub

    Public Sub Clear()
        Me.lstObj.Items.Clear()
    End Sub

    Private Sub lstObj_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles lstObj.MouseLeftButtonUp
        Dim lst As ListBox = DirectCast(sender, ListBox)
        If lst.SelectedItems.Count = 1 Then
            RaiseEvent SendLifeGameSave(Me, New SendLifeGameSaveEventArgs(DirectCast(DirectCast(lst.SelectedItems.Item(0), ListBoxItem).Content, LifeGameSave)))
        End If
    End Sub
End Class

Public Class SendLifeGameSaveEventArgs
    Public Property LifeGameSave As LifeGameSave
    Public Sub New(obj As LifeGameSave)
        LifeGameSave = obj
    End Sub
End Class