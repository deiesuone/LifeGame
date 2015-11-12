Public Class LifeGameBoardCell
    Public Property X As Integer
    Public Property Y As Integer
    Public Property Status As BoardCellStatus
    Public Property NextStatus As BoardCellStatus

    Public Enum BoardCellStatus
        Alive
        Dead
    End Enum

    Public Sub Init(x As Integer, y As Integer)
        Me.X = x
        Me.Y = y
        Me.Status = BoardCellStatus.Dead
        Me.NextStatus = BoardCellStatus.Dead
        Me.Update()
    End Sub

    Public Sub Update()
        Me.Status = Me.NextStatus
        Select Case Me.Status
            Case BoardCellStatus.Alive
                Me.lblBackColor.Background = Brushes.Black
            Case BoardCellStatus.Dead
                Me.lblBackColor.Background = Brushes.White
        End Select
    End Sub

    Public Sub StatusChange()
        Select Case Me.Status
            Case BoardCellStatus.Alive
                Me.Status = BoardCellStatus.Dead
            Case BoardCellStatus.Dead
                Me.Status = BoardCellStatus.Alive
        End Select
        Me.NextStatus = Me.Status
    End Sub

    Private Sub lblBackColor_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs) Handles lblBackColor.MouseLeftButtonUp
        Me.StatusChange()
        Me.Update()
    End Sub
End Class
