Imports System.ComponentModel
Imports System.IO
Public Class Tool_TvS
    Dim user_tree As Object
    Dim timer_id As Integer = 0
    Dim timer_count As Integer = 0
    Private Sub Timer_R_Tick(sender As Object, e As EventArgs) Handles Timer_R.Tick
        Try
            Select Case timer_id
                Case 0
                    ProgressBar1.Value = 0
                    Label6.Text = ""
                Case 1
                    If File.Exists(root_path + "temp\MPS.state") Then
                        Try
                            Dim sr_state As New StreamReader(root_path + "temp\MPS.state")
                            timer_count = CSng(sr_state.ReadLine())
                            sr_state.Close()
                            If timer_count < (MainWindow.DataGridView1.ColumnCount - 3) Then
                                Label6.Text = timer_count.ToString & "/" & (MainWindow.DataGridView1.ColumnCount - 3).ToString
                                ProgressBar1.Value = timer_count / (MainWindow.DataGridView1.ColumnCount - 3) * 10000
                            Else
                                timer_id = 0
                                If File.Exists(root_path + "temp\mps_result.txt") Then
                                    Dim sr_result As New StreamReader(root_path + "temp\mps_result.txt")
                                    Dim line As String = sr_result.ReadLine
                                    line = sr_result.ReadLine
                                    Do
                                        ListView1.Items.Add(New ListViewItem(line.Split(vbTab)))
                                        line = sr_result.ReadLine
                                    Loop Until line Is Nothing
                                    sr_result.Close()
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Label6.Text = "Loading"
                        ProgressBar1.Value = 6180
                    End If
                Case 2
                    If File.Exists(root_path + "temp\MPS.state") Then
                        Try
                            Dim sr_state As New StreamReader(root_path + "temp\MPS.state")
                            timer_count = CSng(sr_state.ReadLine())
                            sr_state.Close()
                            If timer_count < CInt(MainWindow.TreeBox.Text) Then
                                Label6.Text = timer_count.ToString & "/" & (MainWindow.TreeBox.Text).ToString
                                ProgressBar1.Value = timer_count / CInt(MainWindow.TreeBox.Text) * 10000
                            Else
                                timer_id = 0
                                If File.Exists(root_path + "temp\mps_result.txt") Then
                                    Dim sr_result As New StreamReader(root_path + "temp\mps_result.txt")
                                    Dim line As String = sr_result.ReadLine
                                    Dim new_list As List(Of ListViewItem) = New List(Of ListViewItem)()
                                    line = sr_result.ReadLine
                                    Do
                                        new_list.Add(New ListViewItem(line.Split(vbTab)))
                                        line = sr_result.ReadLine
                                    Loop Until line Is Nothing
                                    sr_result.Close()
                                    ListView1.Items.AddRange(new_list.ToArray)
                                End If
                            End If
                        Catch ex As Exception

                        End Try
                    Else
                        Label6.Text = "Loading"
                        ProgressBar1.Value = 6180
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Tool_TvS_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Timer_R.Enabled = True
    End Sub

    Private Sub Tool_TvS_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            CheckForIllegalCrossThreadCalls = False
            ListView1.Items.Clear()
        End If
    End Sub
    Public Sub claculate_score(ByVal temp_mode As Integer, ByVal current_index As Integer, ByVal input_tree As String, ByVal progress_id As Integer)
        Dim status As String = "OK"
        Dim score As Single = -1
        Dim temp_type As String = "continuous"

        Dim sr_body As New StreamReader(root_path + "Plug-ins\MPS\body.R")
        Dim mps_body As String = sr_body.ReadToEnd
        sr_body.Close()
        Dim sw_mps As New StreamWriter(root_path + "temp\run_mps.r", True, System.Text.Encoding.Default)

        Dim trait_value() As String
        ReDim trait_value(dtView.Count - 1)
        Dim trait_name As String = "c('"
        trait_name &= join_array(make_seq(dtView.Count, 1), "','") & "')"

        Dim trait_value_text As String = ""
        Dim total_count As Integer = 0
        Select Case temp_mode
            Case 1
                For i As Integer = 1 To dtView.Count
                    If IsNumeric(dtView.Item(i - 1).Item(current_index)) Then
                        trait_value(i - 1) = CSng(dtView.Item(i - 1).Item(current_index))
                    Else
                        trait_value(i - 1) = "NA"
                    End If

                Next
                trait_value_text = "c(" + join_array(trait_value, ",") & ")"
            Case 0, 2
                For i As Integer = 1 To dtView.Count
                    trait_value(i - 1) = dtView.Item(i - 1).Item(current_index)
                Next
                trait_value_text = "c('" + join_array(trait_value, "','") & "')"

                Dim temp_str As String = ""
                For i As Integer = 1 To dtView.Count
                    For Each c As Char In dtView.Item(i - 1).Item(current_index).ToString.ToUpper
                        If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                            If temp_str.Contains(c) = False Then
                                temp_str = temp_str + c.ToString
                            End If
                        End If
                    Next
                Next

                Dim range_list() As Char = temp_str
                Dim state_group() As String
                Array.Sort(range_list)
                MainWindow.DataGridView1.EndEdit()
                ReDim state_group(temp_str.Length - 1)
                For i As Integer = 1 To dtView.Count
                    For Each c As Char In dtView.Item(i - 1).Item(current_index).ToString.ToUpper
                        If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                            state_group(Asc(c) - Asc("A")) += "," + i.ToString
                        End If
                    Next
                Next
                For i As Integer = 0 To UBound(state_group)
                    state_group(i) = state_group(i).Remove(0, 1)
                Next

                Dim temp_tree As New Ploy_Tree(input_tree)

                For j As Integer = 0 To UBound(state_group)
                    Dim k_count As Integer = 1000000000
                    If Array.IndexOf(temp_tree.Node_Chain, state_group(j)) >= 0 Or state_group(j).Contains(",") = False Then
                        k_count = 0
                    Else
                        For k As Integer = 0 To temp_tree.Node_Number - 2
                            k_count = Math.Min(comp_list(state_group(j), temp_tree.Node_Chain(k)), k_count)
                        Next
                    End If
                    total_count += k_count
                Next
            Case Else
                For i As Integer = 1 To dtView.Count
                    trait_value(i - 1) = dtView.Item(i - 1).Item(current_index)
                Next
                trait_value_text = "c('" + join_array(trait_value, "','") & "')"
        End Select

        If ModelTestToolStripMenuItem.Checked Then
            mps_body = mps_body.Replace("#model_test#", "model_test=TRUE")
        Else
            mps_body = mps_body.Replace("#model_test#", "model_test=FALSE")
        End If
        mps_body = mps_body.Replace("#D_count#", total_count.ToString)
        mps_body = mps_body.Replace("#trait_value#", trait_value_text)
        mps_body = mps_body.Replace("#trait_name#", trait_name).Replace("#tree_value#", input_tree)
        mps_body = mps_body.Replace("#trait_id#", current_index - 1)
        mps_body = mps_body.Replace("#progress#", progress_id)
        mps_body = mps_body.Replace("#trait_mode#", temp_mode)
        mps_body = mps_body.Replace("#trait_title#", MainWindow.DataGridView1.Columns(current_index + 1).HeaderText)

        sw_mps.Write(mps_body)
        sw_mps.Close()
    End Sub

    Private Sub FindBestFitTreeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindBestFitTreeToolStripMenuItem.Click
        user_tree = New Ploy_Tree(MainWindow.FinalTreeBox.Text)
        timer_id = 1
        timer_count = 0
        ListView1.Items.Clear()
        If File.Exists(root_path + "temp\MPS.state") Then
            File.Delete(root_path + "temp\MPS.state")
        End If
        Dim th1 As New Threading.Thread(AddressOf tree_vs_states)
        th1.Start()
    End Sub
    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = Me.ListView1.SelectedItems(0)
            StartTreeView = True

            Dim Tree_view_form As New View_Tree
            Tree_view_form.tree_view_limit = True
            Tree_view_form.current_state = CInt(lvi.SubItems(0).Text) + 1
            Tree_view_form.show_my_tree = lvi.SubItems(3).Text
            Tree_view_form.Show()
        End If
    End Sub
    Public Sub tree_vs_states()
        Dim sr_header As New StreamReader(root_path + "Plug-ins\MPS\header.R")
        Dim mps_header As String = sr_header.ReadToEnd.Replace("#lib_path#", lib_path)
        sr_header.Close()
        Dim sw_header As New StreamWriter(root_path + "temp\run_mps.r", False, System.Text.Encoding.Default)
        sw_header.Write(mps_header)
        sw_header.Close()
        For i As Integer = 2 To MainWindow.DataGridView1.ColumnCount - 2
            Dim temp_mode As Integer = 0
            Dim has_num As Boolean = False
            Dim isSingle As Boolean = True
            Dim has_char As Boolean = False
            For j As Integer = 1 To dtView.Count
                If IsNumeric(dtView.Item(j - 1).Item(i)) Then
                    has_num = True
                End If
                If IsNumeric(dtView.Item(j - 1).Item(i)) = False And dtView.Item(j - 1).Item(i).ToString <> "?" And dtView.Item(j - 1).Item(i).ToString <> "" Then
                    has_char = True
                End If
                If dtView.Item(j - 1).Item(i).ToString.Length > 1 Then
                    isSingle = False
                End If
            Next

            If has_num = True And has_char = False Then
                temp_mode = 1
            ElseIf has_num = False And has_char = True Then
                If isSingle Then
                    temp_mode = 2
                Else
                    temp_mode = 0
                End If
            Else
                MsgBox("Error in loading characters, please check your state data!")
                Exit Sub
            End If

            claculate_score(temp_mode, i, user_tree.Tree_Line, i - 1)
        Next

        Dim sr_footer As New StreamReader(root_path + "Plug-ins\MPS\footer.R")
        Dim mps_footer As String = sr_footer.ReadToEnd
        sr_footer.Close()
        Dim sw_footer As New StreamWriter(root_path + "temp\run_mps.r", True, System.Text.Encoding.Default)
        sw_footer.Write(mps_footer)
        sw_footer.Close()

        Dim sr_run As New StreamWriter(root_path + "temp\run_mps.bat", False, System.Text.Encoding.Default)
        sr_run.WriteLine("""" + rscript + """" + " run_mps.r")
        sr_run.Close()

        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_mps.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = False 'HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardOutput = HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardInput = HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardError = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)

    End Sub

    Private Sub FullTableToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FullTableToolStripMenuItem.Click
        If ListView1.Items.Count > 0 Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".csv"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim sw As New StreamWriter(opendialog.FileName)
                For i As Integer = 0 To ListView1.Items.Count - 1
                    sw.WriteLine("""" + ListView1.Items(i).SubItems(0).Text + """" + "," + """" + ListView1.Items(i).SubItems(1).Text + """" + "," + """" + ListView1.Items(i).SubItems(2).Text + """" + "," + """" + ListView1.Items(i).SubItems(3).Text + """" + "," + """" + ListView1.Items(i).SubItems(4).Text + """" + "," + """" + ListView1.Items(i).SubItems(5).Text + """")
                Next
                sw.Close()
            End If
        End If

    End Sub
    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        Dim columnsort As New ColumnSort(e.Column)
        columnsort.bAscending = (ListView1.Sorting = SortOrder.Ascending)
        If columnsort.bAscending Then
            ListView1.Sorting = SortOrder.Descending
        Else
            ListView1.Sorting = SortOrder.Ascending
        End If
        ListView1.ListViewItemSorter = columnsort
        ListView1.ListViewItemSorter = Nothing
    End Sub

    Private Sub Tool_TvS_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub StateVsTreesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StateVsTreesToolStripMenuItem.Click
        user_tree = New Ploy_Tree(MainWindow.FinalTreeBox.Text)
        timer_id = 2
        timer_count = 0
        ListView1.Items.Clear()
        If File.Exists(root_path + "temp\MPS.state") Then
            File.Delete(root_path + "temp\MPS.state")
        End If
        Dim th1 As New Threading.Thread(AddressOf state_vs_trees)
        th1.Start()
    End Sub
    Public Sub state_vs_trees()
        Dim sr_header As New StreamReader(root_path + "Plug-ins\MPS\header.R")
        Dim mps_header As String = sr_header.ReadToEnd.Replace("#lib_path#", lib_path)
        sr_header.Close()
        Dim sw_header As New StreamWriter(root_path + "temp\run_mps.r", False, System.Text.Encoding.Default)
        sw_header.Write(mps_header)
        sw_header.Close()
        Dim sr As New StreamReader(root_path + "temp" + path_char + "clean_num.trees")
        Dim line As String = sr.ReadLine
        Dim count As Integer = 0
        Do
            Dim temp_mode As Integer = 0
            Dim has_num As Boolean = False
            Dim has_char As Boolean = False

            Dim isSingle As Boolean = True
            For j As Integer = 1 To dtView.Count
                If IsNumeric(dtView.Item(j - 1).Item(state_index)) Then
                    has_num = True
                End If
                If IsNumeric(dtView.Item(j - 1).Item(state_index)) = False And dtView.Item(j - 1).Item(state_index).ToString <> "?" And dtView.Item(j - 1).Item(state_index).ToString <> "" Then
                    has_char = True
                End If
                If dtView.Item(j - 1).Item(state_index).ToString.Length > 1 Then
                    isSingle = False
                End If
            Next
            If has_num = True And has_char = False Then
                temp_mode = 1
            ElseIf has_num = False And has_char = True Then
                If isSingle Then
                    temp_mode = 2
                Else
                    temp_mode = 0
                End If
            Else
                MsgBox("Error in loading characters, please check your state data!")
                Exit Sub
            End If
            count += 1
            claculate_score(temp_mode, state_index, line, count)
            line = sr.ReadLine
        Loop Until line Is Nothing
        sr.Close()

        Dim sr_footer As New StreamReader(root_path + "Plug-ins\MPS\footer.R")
        Dim mps_footer As String = sr_footer.ReadToEnd
        sr_footer.Close()
        Dim sw_footer As New StreamWriter(root_path + "temp\run_mps.r", True, System.Text.Encoding.Default)
        sw_footer.Write(mps_footer)
        sw_footer.Close()

        Dim sr_run As New StreamWriter(root_path + "temp\run_mps.bat", False, System.Text.Encoding.Default)
        sr_run.WriteLine("""" + rscript + """" + " run_mps.r")
        sr_run.Close()

        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_mps.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = False 'HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardOutput = HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardInput = HideCMDWindowToolStripMenuItem.Checked
        'startInfo.RedirectStandardError = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)

    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub ModelTestToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ModelTestToolStripMenuItem.Click

    End Sub
End Class