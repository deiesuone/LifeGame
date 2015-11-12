Imports System.Xml.Serialization
Imports System.IO
Imports Microsoft.Win32

Class MainWindow

    Private _UpdateTimer As New Timers.Timer
    Private _x As Integer = 50
    Private _y As Integer = 50
    Private Delegate Sub UpdateTimerDelegate()

    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Me.lgbMain.Init(Me._x, Me._y)
        With Me._UpdateTimer
            AddHandler Me._UpdateTimer.Elapsed, AddressOf Me.GenerationChangeDispatcher
            .Interval = 500
            .AutoReset = True
            .Stop()
        End With
    End Sub

    Private Sub GenerationChangeDispatcher(sender As Object, e As Timers.ElapsedEventArgs)
        Me.Dispatcher.BeginInvoke(New UpdateTimerDelegate(AddressOf Me.GenerationChange))
    End Sub

    Private Sub GenerationChange()
        Me.lgbMain.GenerationChange()
        Me.lgbMain.UpdateCell()
        If chkHistory.IsChecked Then
            Me.lstObj.SetData(Me.lgbMain.GetLifeGameSave())
        End If
    End Sub

    Private Sub btnTimer_Click(sender As Object, e As RoutedEventArgs) Handles btnTimer.Click
        If Me._UpdateTimer.Enabled Then
            Me._UpdateTimer.Stop()
            Me.btnTimer.Content = "進行"
        Else
            Me.lstObj.SetData(Me.lgbMain.GetLifeGameSave())
            Me._UpdateTimer.Start()
            Me.btnTimer.Content = "停止"
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As RoutedEventArgs) Handles btnSave.Click
        Try
            Me._UpdateTimer.Stop()
            Me.btnTimer.Content = "進行"

            Dim sfd As New SaveFileDialog
            sfd.Filter = "xmlファイル(*.xml)|*.xml"
            sfd.Title = "保存先を選択して下さい。"
            sfd.FileName = "LifeGame.xml"
            sfd.RestoreDirectory = True

            If Not sfd.ShowDialog() Then
                Exit Sub
            End If

            Dim LifeGameSave As LifeGameSave = Me.lgbMain.GetLifeGameSave
            Dim serializer As XmlSerializer = New XmlSerializer(GetType(LifeGameSave))
            Dim stream As FileStream = New FileStream(sfd.FileName, FileMode.Create)
            Try
                serializer.Serialize(stream, LifeGameSave)
            Finally
                stream.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show("なんかエラー出た。" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As RoutedEventArgs) Handles btnLoad.Click
        Try
            Me._UpdateTimer.Stop()
            Me.btnTimer.Content = "進行"

            Dim ofd As New OpenFileDialog
            ofd.Filter = "xmlファイル(*.xml)|*.xml"
            ofd.Title = "XMLファイルを選択して下さい。"
            ofd.RestoreDirectory = True

            If Not ofd.ShowDialog() Then
                Exit Sub
            End If

            Dim serializer As XmlSerializer = New XmlSerializer(GetType(LifeGameSave))
            Dim stream As FileStream = New FileStream(ofd.FileName, FileMode.Open)
            Try
                Me.lgbMain.SetLifeGameSave(DirectCast(serializer.Deserialize(stream), LifeGameSave))
            Finally
                stream.Close()
            End Try
        Catch ex As Exception
            MessageBox.Show("なんかエラー出た。" & vbCrLf & ex.Message)
        End Try
    End Sub

    Private Sub lstObj_SendLifeGameSave(sender As Object, e As SendLifeGameSaveEventArgs) Handles lstObj.SendLifeGameSave
        Me._UpdateTimer.Stop()
        Me.btnTimer.Content = "進行"
        Me.lgbMain.SetLifeGameSave(e.LifeGameSave)
    End Sub

    Private Sub btnListClear_Click(sender As Object, e As RoutedEventArgs) Handles btnHistoryClear.Click
        Me.lstObj.Clear()
    End Sub

    Private Sub btnHelp_Click(sender As Object, e As RoutedEventArgs) Handles btnHelp.Click
        Dim help As New LifeGameHelp
        help.Owner = Me
        help.Show()
    End Sub

    Private Sub sldTimer_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles sldTimer.ValueChanged
        Me._UpdateTimer.Interval = Me.sldTimer.Value
    End Sub
End Class
