Public Class LifeGameBoard
    Public Property XSize As Integer
    Public Property YSize As Integer

    Private _LifeGameBoardCellList As List(Of LifeGameBoardCell)

    Public Enum CellDirection
        UP
        DOWN
        LEFT
        RIGHT
        UP_LEFT
        UP_RIGHT
        DOWN_LEFT
        DOWN_RIGHT
    End Enum

    Public Sub New()
        Me.InitializeComponent()
    End Sub

    Public Sub Init(x As Integer, y As Integer)
        Me.XSize = x
        Me.YSize = y

        For i As Integer = 1 To Me.YSize
            Me.grdBoardCells.RowDefinitions.Add(New RowDefinition)
        Next

        For i As Integer = 1 To Me.XSize
            Me.grdBoardCells.ColumnDefinitions.Add(New ColumnDefinition)
        Next

        Me._LifeGameBoardCellList = New List(Of LifeGameBoardCell)

        Dim YCell As Integer = 1
        Dim XCell As Integer = 1
        For i As Integer = 1 To Me.XSize * Me.YSize
            If XCell > Me.YSize Then
                XCell = 1
                YCell += 1
            End If
            Dim cell As New LifeGameBoardCell
            cell.Init(XCell, YCell)
            Me._LifeGameBoardCellList.Add(cell)
            XCell += 1
        Next

        Me.UpdateAll()
    End Sub

    Public Function GetLifeGameBoardDirectionCell(x As Integer, y As Integer, direction As CellDirection) As LifeGameBoardCell
        Dim TargetX As Integer = x
        Dim TargetY As Integer = y

        Select Case direction
            Case CellDirection.UP
                TargetY -= 1
            Case CellDirection.DOWN
                TargetY += 1
            Case CellDirection.LEFT
                TargetX -= 1
            Case CellDirection.RIGHT
                TargetX += 1
            Case CellDirection.UP_LEFT
                TargetX -= 1
                TargetY -= 1
            Case CellDirection.UP_RIGHT
                TargetX += 1
                TargetY -= 1
            Case CellDirection.DOWN_LEFT
                TargetX -= 1
                TargetY += 1
            Case CellDirection.DOWN_RIGHT
                TargetX += 1
                TargetY += 1
        End Select

        Return Me.GetLifeGameBoardCell(TargetX, TargetY)
    End Function

    Public Function GetLifeGameBoardCell(x As Integer, y As Integer) As LifeGameBoardCell
        If x <= 0 Or Me.XSize < x Or y <= 0 Or Me.YSize < y Then
            Return Nothing
        End If

        Dim index As Integer = (x + (Me.YSize * (y - 1))) - 1

        If Me._LifeGameBoardCellList.Count <= index Or index < 0 Then
            Return Nothing
        End If

        Return Me._LifeGameBoardCellList.Item(index)
    End Function

    Public Sub UpdateAll()
        Me.grdBoardCells.Children.Clear()
        For Each item In Me._LifeGameBoardCellList
            item.SetValue(Grid.ColumnProperty, item.X - 1)
            item.SetValue(Grid.RowProperty, item.Y - 1)
            Me.grdBoardCells.Children.Add(item)
        Next
    End Sub

    Public Sub UpdateCell()
        For Each item In Me._LifeGameBoardCellList
            item.Update()
        Next
    End Sub

    Public Sub GenerationChange()
        For Each item In Me._LifeGameBoardCellList
            Me.GenerationJudge(item)
        Next
    End Sub

    Public Sub GenerationJudge(cell As LifeGameBoardCell)
        Dim AliveCount As Integer = 0
        Dim DeadCount As Integer = 0

        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.UP), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.DOWN), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.LEFT), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.RIGHT), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.UP_LEFT), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.UP_RIGHT), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.DOWN_LEFT), AliveCount, DeadCount)
        Me.DeadOrAliveCount(Me.GetLifeGameBoardDirectionCell(cell.X, cell.Y, CellDirection.DOWN_RIGHT), AliveCount, DeadCount)

        cell.NextStatus = cell.Status
        If cell.Status = LifeGameBoardCell.BoardCellStatus.Alive Then
            If Not (AliveCount = 2 Or AliveCount = 3) Then
                cell.NextStatus = LifeGameBoardCell.BoardCellStatus.Dead
            End If
        ElseIf cell.Status = LifeGameBoardCell.BoardCellStatus.Dead Then
            If AliveCount = 3 Then
                cell.NextStatus = LifeGameBoardCell.BoardCellStatus.Alive
            End If
        End If
    End Sub

    Public Sub DeadOrAliveCount(cell As LifeGameBoardCell, ByRef alive As Integer, ByRef dead As Integer)
        If cell Is Nothing Then
            Exit Sub
        End If

        If cell.Status = LifeGameBoardCell.BoardCellStatus.Alive Then
            alive += 1
        ElseIf cell.Status = LifeGameBoardCell.BoardCellStatus.Dead Then
            dead += 1
        End If
    End Sub

    Public Function GetLifeGameSave() As LifeGameSave
        Dim ret As New LifeGameSave
        For Each item In Me._LifeGameBoardCellList
            Dim data As New LifeGameSaveData
            data.x = item.X
            data.y = item.Y
            data.Status = item.Status
            data.NextStatus = item.NextStatus
            ret.data.Add(data)
        Next
        Return ret
    End Function

    Public Sub SetLifeGameSave(LifeGameSave As LifeGameSave)
        For Each item In LifeGameSave.data
            Dim cell As LifeGameBoardCell = Me.GetLifeGameBoardCell(item.x, item.y)
            cell.Status = item.Status
            cell.NextStatus = item.NextStatus
        Next
        Me.UpdateCell()
    End Sub
End Class
