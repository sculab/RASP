Public Class Tool_Convert
    Dim state_max, state_min As Single

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged
        If state_max <> state_min Then
            make_list()
        End If

    End Sub
    Public Sub make_list()
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.AllowUserToDeleteRows = True
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.AllowUserToResizeColumns = True
        DataGridView1.AllowUserToResizeRows = True
        DataGridView1.Rows.Clear()
        DataGridView1.Rows.Insert(0, {(state_min + (NumericUpDown1.Value - 1) * (state_max - state_min) / NumericUpDown1.Value).ToString("F4"), state_max.ToString("F4"), ChrW(64 + NumericUpDown1.Value)})
        For i As Integer = NumericUpDown1.Value - 1 To 2 Step -1
            DataGridView1.Rows.Insert(0, {(state_min + (i - 1) * (state_max - state_min) / NumericUpDown1.Value).ToString("F4"), (state_min + i * (state_max - state_min) / NumericUpDown1.Value).ToString("F4"), ChrW(64 + i)})
        Next
        DataGridView1.Rows.Insert(0, {state_min.ToString("F4"), (state_min + (state_max - state_min) / NumericUpDown1.Value).ToString("F4"), "A"})
        DataGridView1.BackgroundColor = Color.LightGray
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToOrderColumns = False
        DataGridView1.AllowUserToResizeColumns = False
        DataGridView1.AllowUserToResizeRows = False
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim temp_name As String = InputBox("Please input the name of new state", "new state", "State")
        If temp_name <> "" Then
            Try
                MainWindow.Taxon_Dataset.Tables("Taxon Table").Columns.Add(temp_name)
            Catch ex As Exception
                MsgBox("The name of column is repeated! Please try again!")
                Exit Sub
            End Try
        End If

        dtView = MainWindow.Taxon_Dataset.Tables("Taxon Table").DefaultView
        MainWindow.DataGridView1.Sort(MainWindow.DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
        For i As Integer = 1 To dtView.Count
            For j As Integer = 1 To DataGridView1.Rows.Count
                If IsNumeric(dtView.Item(i - 1).Item(state_index).ToString) Then
                    'MsgBox(DataGridView1.Rows(j - 1).Cells("Column1").Value)
                    If CSng(dtView.Item(i - 1).Item(state_index).ToString) >= CSng(DataGridView1.Rows(j - 1).Cells("Column1").Value) Then
                        If CSng(dtView.Item(i - 1).Item(state_index).ToString) <= CSng(DataGridView1.Rows(j - 1).Cells("Column2").Value) Then
                            dtView.Item(i - 1).Item(MainWindow.Taxon_Dataset.Tables("Taxon Table").Columns.Count - 1) = DataGridView1.Rows(j - 1).Cells("Column3").Value
                            Exit For
                        End If
                    End If
                End If
            Next
        Next
        For i As Integer = 2 To MainWindow.DataGridView1.Columns.Count - 1
            MainWindow.DataGridView1.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
        MainWindow.DataGridView1.EndEdit()
        MainWindow.DataGridView1.Refresh()
        Me.Close()
    End Sub

    Private Sub Tool_Convert_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For i As Integer = 1 To dtView.Count
            Dim Temp_d As String = dtView.Item(i - 1).Item(state_index).ToString
            If IsNumeric(Temp_d) Then
                state_max = Int(CSng(Temp_d)) + 1
                state_min = Int(CSng(Temp_d))
                Exit For
            End If
        Next

        For i As Integer = 1 To dtView.Count
            Dim Temp_d As String = dtView.Item(i - 1).Item(state_index).ToString
            If IsNumeric(Temp_d) Then
                If CSng(Temp_d) > state_max Then
                    state_max = Int(CSng(Temp_d)) + 1
                End If
                If CSng(Temp_d) <state_min Then
                    state_min = Int(CSng(Temp_d))
                End If
            End If
        Next
        make_list()
    End Sub
End Class