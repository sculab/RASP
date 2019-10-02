Imports System.IO
Imports System.Runtime.InteropServices
Public Class Tool_Cluster
    <DllImport("CONSDLL.dll")> Public Shared Function ctree(ByVal vbOG As Int16, ByVal path As String, ByRef pros As Integer) As Integer
    End Function
    Dim count_matrix() As Single = {0}
    Dim list_trees As New ArrayList(0)
    Dim timer_id As Integer = 0
    Dim timer_gen() As Integer
    Dim timer_count As Integer
    Dim thread_num As Integer = 2
    Dim my_threads() As Threading.Thread
    Dim stopW As Stopwatch = New Stopwatch

    Public Sub make_fdw_matrix(ByVal thread_id As Integer)
        Dim count_arr As Integer = 1
        timer_gen(thread_id) = 0
        For i As Integer = 0 To list_trees.Count - 2
            Dim same_tree() As Integer = {i}
            For j As Integer = i + 1 To list_trees.Count - 1
                If (count_arr Mod thread_num) = thread_id Then
                    If count_matrix(count_arr) = -1 Then

                        Dim count_list() As Single = {0, 0, 0, 0}
                        Dim temp_sign() As Boolean
                        ReDim temp_sign(list_trees(j).Node_Number - 1)
						For k As Integer = 0 To UBound(temp_sign)
							temp_sign(k) = True
						Next
                        'A
                        'For k As Integer = 0 To list_trees(i).Node_Number - 2
                        '	Dim k_index As Integer = Array.IndexOf(list_trees(j).Node_Chain, list_trees(i).Node_Chain(k))
                        '	If k_index < 0 Then
                        '		count_list(1) += list_trees(i).Node_Weight_A(k)
                        '	Else
                        '		temp_sign(k_index) = False
                        '	End If
                        'Next
                        'For k As Integer = 0 To list_trees(j).Node_Number - 2
                        '	If temp_sign(k) Then
                        '		count_list(3) += list_trees(j).Node_Weight_A(k)
                        '	End If
                        'Next
                        'count_list(0) = list_trees(i).Chain_Sum_A
                        'count_list(2) = list_trees(j).Chain_Sum_A

                        'B
                        For k As Integer = 0 To list_trees(i).Node_Number - 2
                            Dim k_index As Integer = Array.IndexOf(list_trees(j).Node_Chain, list_trees(i).Node_Chain(k))
                            If k_index < 0 Then
                                count_list(1) += list_trees(i).Node_Weight_B(k)
                            Else
                                temp_sign(k_index) = False
                            End If
                        Next
                        For k As Integer = 0 To list_trees(j).Node_Number - 2
                            If temp_sign(k) Then
                                count_list(3) += list_trees(j).Node_Weight_B(k)
                            End If
                        Next
                        count_list(0) = list_trees(i).Chain_Sum_B
                        count_list(2) = list_trees(j).Chain_Sum_B

                        count_matrix(count_arr) = Math.Round((count_list(1) + count_list(3)) / (count_list(0) + count_list(2)), 4)

                        If count_matrix(count_arr) = 0 Then
                            ReDim Preserve same_tree(same_tree.Length)
                            same_tree(UBound(same_tree)) = j
                        End If
                    End If
                    timer_gen(thread_id) += 1
                End If
                count_arr += 1
            Next
            If same_tree.Length > 1 Then
                For p As Integer = 1 To UBound(same_tree)
                    For q As Integer = same_tree(p) + 1 To list_trees.Count - 1
                        count_matrix(cal_matrix_index(same_tree(p) + 1, q + 1, list_trees.Count - 1)) = count_matrix(cal_matrix_index(i + 1, q + 1, list_trees.Count - 1))
                    Next
                Next
            End If
        Next
    End Sub
    Public Sub make_SC_matrix(ByVal thread_id As Integer)
        Dim count_arr As Integer = 1
        timer_gen(thread_id) = 0
        For i As Integer = 0 To list_trees.Count - 2
            Dim same_tree() As Integer = {i}
            For j As Integer = i + 1 To list_trees.Count - 1
                If (count_arr Mod thread_num) = thread_id Then
                    If count_matrix(count_arr) = -1 Then
                        Dim count_sum As Integer = 0
                        For k As Integer = 0 To list_trees(i).Node_Number - 1
                            If Array.IndexOf(list_trees(j).Node_Chain, list_trees(i).Node_Chain(k)) >= 0 Then
                                count_sum += 1
                            End If
                        Next
                        If dist_type = "SN" Then
                            count_matrix(count_arr) = list_trees(i).Node_Number + list_trees(j).Node_Number - 2 * count_sum
                        Else
                            count_matrix(count_arr) = Math.Round((list_trees(i).Node_Number + list_trees(j).Node_Number - 2 * count_sum) / (list_trees(i).Node_Number + list_trees(j).Node_Number), 4)
                        End If

                        If count_matrix(count_arr) = 0 Then
                            ReDim Preserve same_tree(same_tree.Length)
                            same_tree(UBound(same_tree)) = j
                        End If
                    End If
                    timer_gen(thread_id) += 1
                End If
                count_arr += 1
            Next
            If same_tree.Length > 1 Then
                For p As Integer = 1 To UBound(same_tree)
                    For q As Integer = same_tree(p) + 1 To list_trees.Count - 1
                        count_matrix(cal_matrix_index(same_tree(p) + 1, q + 1, list_trees.Count - 1)) = count_matrix(cal_matrix_index(i + 1, q + 1, list_trees.Count - 1))
                    Next
                Next
            End If
        Next
    End Sub
    Public Function cal_matrix_index(ByVal p As Integer, ByVal q As Integer, ByVal t As Integer) As Integer
        Return (t + t - p + 1) * p / 2 - t + q - 1
    End Function
    Public Sub write_matrix(ByVal file_name As String)
        Dim sw1 As New StreamWriter(root_path + "temp\" + file_name + ".txt")
        For i As Integer = 1 To list_trees.Count
            For j As Integer = 1 To list_trees.Count
                If j = i Then
                    sw1.Write("0")
                ElseIf i < j Then
                    sw1.Write(count_matrix(cal_matrix_index(i, j, list_trees.Count - 1)).ToString)
                Else
                    sw1.Write(count_matrix(cal_matrix_index(j, i, list_trees.Count - 1)).ToString)
                End If
                If j <> list_trees.Count Then
                    sw1.Write(",")
                End If
            Next
            sw1.Write(vbCrLf)
        Next
        sw1.Close()
    End Sub
    Public Sub write_matrix_limit(ByVal limit As Single) '相似度
        Dim count_arr As Integer = 0
        Dim sw1 As New StreamWriter(root_path + "temp\matrix_1_" + limit.ToString + ".txt") '差异矩阵
        For i As Integer = 1 To list_trees.Count
            For j As Integer = 1 To list_trees.Count
                If j = i Then
                    sw1.Write("0")
                ElseIf i < j Then
                    If count_matrix(cal_matrix_index(i, j, list_trees.Count - 1)) > limit Then '差异度大于一定比例
                        sw1.Write("1") '差异为1
                    Else
                        sw1.Write(count_matrix(cal_matrix_index(i, j, list_trees.Count - 1)).ToString)
                    End If

                Else
                    If count_matrix(cal_matrix_index(j, i, list_trees.Count - 1)) > 1 - limit Then
                        sw1.Write("1") '差异为1
                    Else
                        sw1.Write(count_matrix(cal_matrix_index(j, i, list_trees.Count - 1)).ToString)
                    End If
                End If
                If j <> list_trees.Count Then
                    sw1.Write(",")
                End If
            Next
            sw1.Write(vbCrLf)
        Next
        sw1.Close()
    End Sub

    Private Sub ClusterTreeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClusterTreeToolStripMenuItem.Click


    End Sub
    Public Sub r_cluster()
        If CheckBox3.Checked Then
            write_matrix_limit(TextBox4.Text)
        Else
            write_matrix("matrix_1")
        End If
        Dim wr4 As New StreamWriter(root_path + "temp\run_cluster.r", False, System.Text.Encoding.Default)
        Dim sr1 As New StreamReader(root_path + "Plug-ins\CLUSTER\cluster_trees.R")
        Dim line As String = sr1.ReadToEnd.Replace("#lib_path#", lib_path)

        line = line.Replace("#method#", ComboBox1.Text)
        If CheckBox3.Checked Then
            line = line.Replace("#limit#", TextBox4.Text)
            line = line.Replace("#matrix#", "matrix_1_" + TextBox4.Text + ".txt")
        Else
            line = line.Replace("#limit#", "1")
            line = line.Replace("#matrix#", "matrix_1.txt")
        End If
        line = line.Replace("#k_value#", NumericUpDown4.Value.ToString)

        line = line.Replace("#expand#", "1")

		wr4.Write(line)
        sr1.Close()
        wr4.Close()
        Dim wr5 As New StreamWriter(root_path + "temp\run_cluster.bat", False, System.Text.Encoding.Default)
        wr5.WriteLine("""" + rscript + """" + " run_cluster.r")
        wr5.Close()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_cluster.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)
    End Sub
    Dim reset_sw As Boolean = True
    Public Sub r_mcl()
        If CheckBox3.Checked Then
            write_matrix_limit(TextBox4.Text)
        End If
        Dim wr4 As New StreamWriter(root_path + "temp\run_cluster.r", False, System.Text.Encoding.Default)
        Dim sr1 As New StreamReader(root_path + "Plug-ins\MCL\cluster_trees.R")
        Dim line As String = sr1.ReadToEnd.Replace("#lib_path#", lib_path)

        line = line.Replace("#expansion#", 2)
        line = line.Replace("#inflation#", 2)
        line = line.Replace("#max.iter#", 100)
        If CheckBox3.Checked Then
            line = line.Replace("#limit#", TextBox4.Text)
            line = line.Replace("#matrix#", "matrix_1_" + TextBox4.Text + ".txt")
        Else
            line = line.Replace("#limit#", "1")
            line = line.Replace("#matrix#", "matrix_1.txt")
        End If
        line = line.Replace("#expand#", "1")

        wr4.Write(line)
        sr1.Close()
        wr4.Close()
        Dim wr5 As New StreamWriter(root_path + "temp\run_cluster.bat", False, System.Text.Encoding.Default)
        wr5.WriteLine("""" + rscript + """" + " run_cluster.r")
        wr5.Close()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_cluster.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)
    End Sub
    Public Sub nor_matrix()
        Dim max_dis As Single = 0
        For i As Integer = 1 To list_trees.Count * (list_trees.Count - 1) / 2
            If max_dis < count_matrix(i) Then
                max_dis = count_matrix(i)
            End If
        Next
        For i As Integer = 1 To list_trees.Count * (list_trees.Count - 1) / 2
            count_matrix(i) = count_matrix(i) / max_dis
        Next
    End Sub

    Private Sub Timer_R_Tick(sender As Object, e As EventArgs) Handles Timer_R.Tick
        Try
            Select Case timer_id
                Case 0
                    'Label6.Text = ""
                    ProgressBar1.Value = 0
                Case 1
                    Dim sum_gen As Integer = sum(timer_gen)
                    If sum_gen < UBound(count_matrix) Then
                        ProgressBar1.Value = sum_gen / UBound(count_matrix) * 10000
                        Label6.Text = (stopW.ElapsedMilliseconds / 1000 / 60 / sum_gen * (UBound(count_matrix) - sum_gen)).ToString("F2") & "m ..."
                    ElseIf sum_gen = UBound(count_matrix) Then
                        stopW.Stop()
                        timer_id = 0
                        write_matrix("matrix_0")
                        'nor_matrix()
                        Label6.Text = dist_info
                        'draw_heatmap("matrix_0.txt")
                    End If

                Case 2
                    Label6.Text = "Working ..."
                    If File.Exists(root_path + "temp\clust_end.txt") Then
                        timer_id = 0
                        ListView1.Items.Clear()
                        ListView1.Items.Add(New ListViewItem({"dendrogram", "", ""}))
                        'Dim sum As Single = 0
                        Dim sr As New StreamReader(root_path + "temp\clust_groups.txt")
                        Dim line As String = sr.ReadLine
                        Do
                            Dim cols As New ListViewItem(line.Split(" "))
                            'sum += CSng(line.Split(" ")(1)) * (CSng(line.Split(" ")(1)) - 1) / 2 * CSng(line.Split(" ")(2))
                            ListView1.Items.Add(cols)
                            line = sr.ReadLine
                        Loop Until line Is Nothing
                        sr.Close()
                        ListView1.Items.Add(New ListViewItem({"All trees", list_trees.Count.ToString, ""}))
                        Label6.Text = dist_info
                    Else
                        If File.Exists(root_path + "temp\clust_iter.txt") Then
                            Dim sr As New StreamReader(root_path + "temp\clust_iter.txt")
                            Dim line As String = sr.ReadLine
                            sr.Close()
                            ProgressBar1.Value = CInt(line)
                        End If
                    End If
                Case 3
                    Label6.Text = "Loading ..."
                    ProgressBar1.Value = timer_count / CInt(MainWindow.TreeBox_P.Text) * 10000
                Case 4
                    Label6.Text = "Loading ..."
                    ProgressBar1.Value = timer_count
                Case 5
                    Label6.Text = "Working ..."
                    If File.Exists(root_path + "temp\clust_end.txt") Then
                        timer_id = 0
                        ListView1.Items.Clear()
                        ListView1.Items.Add(New ListViewItem({"", "", ""}))
                        'Dim sum As Single = 0
                        Dim sr As New StreamReader(root_path + "temp\clust_groups.txt")
                        Dim line As String = sr.ReadLine
                        Do
                            Dim cols As New ListViewItem(line.Split(" "))
                            ListView1.Items.Add(cols)
                            'sum += CSng(line.Split(" ")(1)) * (CSng(line.Split(" ")(1)) - 1) / 2 * CSng(line.Split(" ")(2))
                            line = sr.ReadLine
                        Loop Until line Is Nothing
                        sr.Close()
                        ListView1.Items.Add(New ListViewItem({"All trees", list_trees.Count.ToString, ""}))
                        Label6.Text = dist_info
                    Else
                        If File.Exists(root_path + "temp\clust_iter.txt") Then
                            Dim sr As New StreamReader(root_path + "temp\clust_iter.txt")
                            Dim line As String = sr.ReadLine
                            sr.Close()
                            ProgressBar1.Value = CInt(line)
                        End If
                    End If
                Case 6
                    Label6.Text = "Working"
                    If File.Exists(root_path + "temp\clust_end.txt") Then
                        timer_id = 0
                        'ListView1.Items.Clear()
                        'ListView1.Items.Add(New ListViewItem({"", "", ""}))
                        'ListView1.Items.Add(New ListViewItem({"Plot of k", "", ""}))
                        If File.Exists(root_path + "temp\test_k.png") Then
                            File.Copy(root_path + "temp\test_k.png", root_path + "temp\clust_temp.png", True)
                            PictureBox1.Load(root_path + "temp\clust_temp.png")
                            If File.Exists(root_path + "temp\best_k.txt") Then
                                Dim sr As New StreamReader(root_path + "temp\best_k.txt")
                                Dim line As String = sr.ReadLine
                                sr.Close()
                                NumericUpDown4.Value = CInt(line)
                            End If
                        End If
                        Label6.Text = dist_info
                    Else
                        If File.Exists(root_path + "temp\clust_iter.txt") Then
                            Dim sr As New StreamReader(root_path + "temp\clust_iter.txt")
                            Dim line As String = sr.ReadLine
                            sr.Close()
                            ProgressBar1.Value = CInt(line)
                        End If
                    End If
                Case 7
                    Label6.Text = "Working ..."
                    If File.Exists(root_path + "temp\mpest.end") Then
                        timer_id = 0
                        Dim th1 As New Threading.Thread(AddressOf load_matrix_ext)
                        th1.Start("mpest.trees_genetree.dis")
                        Label6.Text = dist_info

                    End If
                Case 8
                    Label6.Text = "Working ..."
                    If File.Exists(root_path + "temp\dist.end") Then
                        timer_id = 0
                        Dim th1 As New Threading.Thread(AddressOf load_matrix_ext)
                        th1.Start("matrix_" + dist_type + ".txt")
                        Label6.Text = dist_info
                    End If
                Case 9
                    Label6.Text = "Working"
                    If File.Exists(root_path + "temp\heatmap.end") Then
                        timer_id = 0
                        File.Copy(root_path + "temp\heatmap.png", root_path + "temp\clust_temp.png", True)
                        PictureBox1.Load(root_path + "temp\clust_temp.png")
                        Label6.Text = dist_info
                    End If
            End Select

        Catch ex As Exception

        End Try

    End Sub
    Dim dist_type As String = ""
    Dim dist_info As String = ""
    Public Sub load_matrix_ext(ByVal file_name As String)
        Dim temp_count As Integer = list_trees.Count
        Dim sr1 As New StreamReader(root_path + "temp" + path_char + file_name)
        ReDim count_matrix((temp_count - 1) * temp_count / 2)
        Dim count_arr As Integer = 0
        For i As Integer = 1 To (temp_count - 1) * temp_count / 2
            count_matrix(i) = -1
        Next
        'Dim max_count As Integer = 1 'list_trees(0).Taxon_Number * (list_trees(0).Taxon_Number - 1) * (list_trees(0).Taxon_Number - 2) / 6
        For i As Integer = 0 To temp_count - 1
            Dim temp_matrix() As String = sr1.ReadLine.Split("	")

            For j As Integer = i + 1 To temp_count - 1
                count_arr += 1
                count_matrix(count_arr) = CSng(temp_matrix(j))
            Next
            timer_count = i / temp_count * 10000
        Next
        sr1.Close()
        write_matrix("matrix_0")
        timer_id = 0
        'draw_heatmap("matrix_0.txt")
        'nor_matrix()
    End Sub
    Private Sub Tool_Tree_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        CheckForIllegalCrossThreadCalls = False
        Timer_R.Enabled = True
        ComboBox1.SelectedIndex = 8
        If TargetOS = "macos" Then
            HideCMDWindowToolStripMenuItem.Checked = True
        Else
            HideCMDWindowToolStripMenuItem.Visible = False
        End If
    End Sub

    Private Sub Tool_Tree_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        ClusterForm = False
        If my_threads IsNot Nothing Then
            For i As Integer = 0 To UBound(my_threads)
                If my_threads(i).ThreadState = Threading.ThreadState.Running Then
                    my_threads(i).Abort()
                End If
            Next
        End If
        CheckForIllegalCrossThreadCalls = True
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged
        If ListView1.SelectedIndices.Count > 0 Then
            Try
                PictureBox1.Image = Nothing
                If ListView1.SelectedIndices(0) = 0 Then
                    File.Copy(root_path + "temp\clust_tree.png", root_path + "temp\clust_temp.png", True)
                ElseIf ListView1.SelectedIndices(0) = ListView1.Items.Count - 1 Then
                    File.Copy(root_path + "temp\all_trees.png", root_path + "temp\clust_temp.png", True)

                Else
                    File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + ".png", root_path + "temp\clust_temp.png", True)
                    Dim id_sumer As New StreamReader(root_path + "temp\gene_name.txt")
                    Dim gene_name_list(0) As String
                    Dim gene_name_count(0) As Integer
                    Dim gene_name_count_cluster(0) As Integer
                    Dim line As String = id_sumer.ReadLine
                    Do While line Is Nothing = False
                        Dim name As String = line.Substring(0, line.LastIndexOf("_"))
                        If Array.IndexOf(gene_name_list, name) < 0 Then
                            ReDim Preserve gene_name_list(gene_name_list.Length)
                            gene_name_list(UBound(gene_name_list)) = name
                            ReDim Preserve gene_name_count(gene_name_count.Length)
                            gene_name_count(UBound(gene_name_count)) = 1
                            ReDim Preserve gene_name_count_cluster(gene_name_count_cluster.Length)
                            gene_name_count_cluster(UBound(gene_name_count_cluster)) = 0
                        Else
                            gene_name_count(Array.IndexOf(gene_name_list, name)) += 1
                        End If
                        line = id_sumer.ReadLine
                    Loop
                    id_sumer.Close()


                    Dim id_reader As New StreamReader(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_id.txt")
                    line = id_reader.ReadLine
                    Do While line Is Nothing = False
                        Dim name As String = line.Substring(0, line.LastIndexOf("_"))
                        If Array.IndexOf(gene_name_list, name) >= 0 Then
                            gene_name_count_cluster(Array.IndexOf(gene_name_list, name)) += 1
                        End If
                        line = id_reader.ReadLine
                    Loop
                    id_reader.Close()
                    TextBox1.Text = ""
                    For i As Integer = 1 To UBound(gene_name_count_cluster)
                        TextBox1.Text += gene_name_list(i) + vbTab + gene_name_count_cluster(i).ToString + "/" + gene_name_count(i).ToString + vbCrLf
                    Next
                End If
                PictureBox1.Load(root_path + "temp\clust_temp.png")
            Catch ex As Exception
            End Try
        End If
    End Sub

    Private Sub SaveMatrixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveMatrixToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "matrix_0.txt") Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".csv"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                File.Copy(root_path + "temp" + path_char + "matrix_0.txt", opendialog.FileName, True)
            End If
        Else
            MsgBox("No matrix to save!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub LoadMatrixToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadMatrixToolStripMenuItem.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".csv"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                Dim th1 As New Threading.Thread(AddressOf load_matrix_0)
                th1.Start(opendialog.FileName)
            End If
        End If
    End Sub
    Public Sub load_matrix_0(ByVal file_path As String)
        timer_id = 4
        File.Copy(file_path, root_path + "temp" + path_char + "matrix_0.txt", True)
        Dim sr0 As New StreamReader(root_path + "temp" + path_char + "matrix_0.txt")
        Dim temp_count As Integer = sr0.ReadLine.Split(",").Length
        sr0.Close()
        Dim sr1 As New StreamReader(root_path + "temp" + path_char + "matrix_0.txt")
        ReDim count_matrix((temp_count - 1) * temp_count / 2)
        Dim count_arr As Integer = 0
        For i As Integer = 1 To (temp_count - 1) * temp_count / 2
            count_matrix(i) = -1
        Next
        For i As Integer = 0 To temp_count - 1
            Dim temp_matrix() As String = sr1.ReadLine.Split(",")
            For j As Integer = i + 1 To temp_count - 1
                count_arr += 1
                count_matrix(count_arr) = CSng(temp_matrix(j))
            Next
            timer_count = i / temp_count * 10000
        Next
        sr1.Close()
        'nor_matrix()
        timer_id = 0
        MsgBox(temp_count.ToString & "*" & temp_count.ToString & " matrix loaded.")
    End Sub
    Private Sub StatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles StatisticsToolStripMenuItem.Click
        If count_matrix.Length > 1 Then
            Dim temp_matrix_0() As Single
            ReDim temp_matrix_0(count_matrix.Length - 2)
            For i As Integer = 0 To count_matrix.Length - 2
                temp_matrix_0(i) = 1 - count_matrix(i + 1)
            Next
            Dim Q() As Single = QUTER(temp_matrix_0)
            MsgBox("Median: " + MEDIAN(temp_matrix_0).ToString("F4") + Chr(13) +
                   "Mean: " + AVERAGE(temp_matrix_0).ToString("F4") + Chr(13) +
                   "STDEVP: " + STDEVP(temp_matrix_0).ToString("F4") + Chr(13) +
                   "QUARTILE: " + Q(0).ToString + "," + Q(1).ToString + "," + Q(2).ToString, MsgBoxStyle.OkOnly, "All trees")
            Dim count As Integer = -1
            Dim temp_matrix_1() As Single
            ReDim temp_matrix_1(count_matrix.Length - 2)
            For i As Integer = 0 To count_matrix.Length - 2
                If count_matrix(i + 1) > 0 Then
                    count += 1
                    temp_matrix_1(count) = 1 - count_matrix(i + 1)
                End If
            Next
            ReDim Preserve temp_matrix_1(count)
            Dim Q1() As Single = QUTER(temp_matrix_1)
            MsgBox("Median: " + MEDIAN(temp_matrix_1).ToString("F4") + Chr(13) +
                   "Mean: " + AVERAGE(temp_matrix_1).ToString("F4") + Chr(13) +
                   "STDEVP: " + STDEVP(temp_matrix_1).ToString("F4") + Chr(13) +
                   "QUARTILE: " + Q1(0).ToString + "," + Q1(1).ToString + "," + Q1(2).ToString, MsgBoxStyle.OkOnly, "Exclude identical trees")

        End If

    End Sub

    Private Sub LoadIntoRASPToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadIntoRASPToolStripMenuItem.Click
        If ListView1.SelectedIndices.Count > 0 Then
            Try
                MainWindow.close_data()
                MainWindow.load_tree_data(root_path + "temp\clust_" + (ListView1.SelectedIndices(0) + 1).ToString + ".trees")
                MainWindow.LoadConsensusTree(root_path + "temp\clust_" + (ListView1.SelectedIndices(0) + 1).ToString + "_con.tre")
                Me.Close()
            Catch ex As Exception
            End Try
        End If
    End Sub
    Public Sub convert_nameas(ByVal tree_file As String)
        Dim name_r As New StreamReader(root_path + "temp\taxon_name.txt")
        Dim temp_names(0) As String
        Dim name_line As String = name_r.ReadLine
        Dim tree_r As New StreamReader(tree_file)
        Dim tree_line As String = tree_r.ReadLine

        Do
            If name_line <> "" Then
                temp_names(UBound(temp_names)) = name_line
                ReDim Preserve temp_names(UBound(temp_names) + 1)
            End If
            name_line = name_r.ReadLine
        Loop Until name_line Is Nothing
        name_r.Close()

        Dim tree_w As New StreamWriter(tree_file + "_named")
        Do
            For i As Integer = 1 To UBound(temp_names)
                tree_line = tree_line.Replace("(" + (i - 1).ToString + ",", "($%*" + temp_names(i - 1).ToString + "$%*,")
                tree_line = tree_line.Replace("," + (i - 1).ToString + ")", ",$%*" + temp_names(i - 1).ToString + "$%*)")
                tree_line = tree_line.Replace("," + (i - 1).ToString + ",", ",$%*" + temp_names(i - 1).ToString + "$%*,")
            Next
            tree_line = tree_line.Replace("$%*", "")
            tree_w.WriteLine(tree_line)
            tree_line = tree_r.ReadLine
        Loop Until tree_line Is Nothing
        tree_r.Close()
        tree_w.Close()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Dim bPictureBoxDragging As Boolean
    Dim oPointClicked As Point
    Dim loactionClicked As Point
    Private Sub PictureBox1_MouseDown(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseDown
        PictureBox1.Cursor = Cursors.Hand
        bPictureBoxDragging = True
        oPointClicked = Me.PointToClient(PictureBox1.PointToScreen(New Point(e.X, e.Y)))
        loactionClicked = New Point(PictureBox1.Location.X, PictureBox1.Location.Y)
    End Sub

    Private Sub PictureBox1_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseUp
        bPictureBoxDragging = False
        PictureBox1.Cursor = Cursors.Default
    End Sub

    Private Sub PictureBox1_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseMove
        If (bPictureBoxDragging) Then
            Dim oMoveToPoint As Point
            oMoveToPoint = Me.PointToClient(PictureBox1.PointToScreen(New Point(e.X, e.Y)))
            oMoveToPoint.Offset(loactionClicked.X - oPointClicked.X, loactionClicked.Y - oPointClicked.Y)
            PictureBox1.Location = oMoveToPoint
        End If
    End Sub

    Private Sub ZoomOutToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomOutToolStripMenuItem.Click
        PictureBox1.Width *= 1.2
        PictureBox1.Height *= 1.2
    End Sub

    Private Sub ZoomInToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ZoomInToolStripMenuItem.Click
        PictureBox1.Width /= 1.2
        PictureBox1.Height /= 1.2
    End Sub

    Private Sub PictureBox1_MouseWheel(sender As Object, e As MouseEventArgs) Handles PictureBox1.MouseWheel
        Dim ratio As Single
        If e.Delta > 0 Then
            ratio = 1.05
        Else
            ratio = 1 / 1.05
        End If
        PictureBox1.Width *= ratio
        PictureBox1.Height *= ratio
    End Sub

    Private Sub SaveTreesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveTreesToolStripMenuItem.Click
        If ListView1.SelectedIndices.Count > 0 Then
            Dim opendialog As New FolderBrowserDialog
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
			If resultdialog = DialogResult.OK Then
				If ListView1.SelectedItems(0).SubItems(0).Text.ToLower.StartsWith("group") Then
                    File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + ".png", opendialog.SelectedPath + "\group" + ListView1.SelectedIndices(0).ToString + ".png", True)
                    File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + ".trees", opendialog.SelectedPath + "\group" + ListView1.SelectedIndices(0).ToString + ".trees", True)
                    'File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_con.tre", opendialog.SelectedPath + "\group" + ListView1.SelectedIndices(0).ToString + "_con.tre", True)
                    File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_id.txt", opendialog.SelectedPath + "\group" + ListView1.SelectedIndices(0).ToString + "_id.txt", True)
                ElseIf ListView1.SelectedItems(0).SubItems(0).Text.ToLower.StartsWith("all") Then
					File.Copy(root_path + "temp\all_trees.png", opendialog.SelectedPath + "\all_trees.png", True)
				ElseIf ListView1.SelectedItems(0).SubItems(0).Text.StartsWith("dendrogram") Then
					File.Copy(root_path + "temp\clust_tree.png", opendialog.SelectedPath + "\clust_tree.png", True)
				ElseIf ListView1.SelectedItems(0).SubItems(0).Text.ToLower.StartsWith("plot") Then
					File.Copy(root_path + "temp\all_trees.png", opendialog.SelectedPath + "\k_test.png", True)
					File.Copy(root_path + "temp\k_test.txt", opendialog.SelectedPath + "\k_test.txt", True)
				End If
			End If
		End If

    End Sub

    Private Sub SaveALLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveALLToolStripMenuItem.Click
        If ListView1.Items.Count > 0 Then
            Try
                Dim opendialog As New FolderBrowserDialog
                Dim resultdialog As DialogResult = opendialog.ShowDialog()
                If resultdialog = DialogResult.OK Then
                    For i As Integer = 1 To ListView1.Items.Count - 2
                        File.Copy(root_path + "temp\clust_" + i.ToString + ".png", opendialog.SelectedPath + "\group" + i.ToString + ".png", True)
                        File.Copy(root_path + "temp\clust_" + i.ToString + ".trees", opendialog.SelectedPath + "\group" + i.ToString + ".trees", True)
                        'File.Copy(root_path + "temp\clust_" + i.ToString + "_con.tre", opendialog.SelectedPath + "\group" + i.ToString + "_con.tre", True)
                        File.Copy(root_path + "temp\clust_" + i.ToString + "_id.txt", opendialog.SelectedPath + "\group" + i.ToString + "_id.txt", True)
                    Next
                    'File.Copy(root_path + "temp\mcl_groups.txt", opendialog.SelectedPath + "\mcl_groups.txt", True)
                    File.Copy(root_path + "temp\matrix_0.txt", opendialog.SelectedPath + "\matrix.csv", True)
                    File.Copy(root_path + "temp\gene_name.txt", opendialog.SelectedPath + "\gene_name.txt", True)
                    'File.Copy(root_path + "temp\matrix_1.txt", opendialog.SelectedPath + "\matrix_1.csv", True)
                End If
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub LeveledRFDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LeveledRFDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "clean_num.trees") Then
            CheckForIllegalCrossThreadCalls = False
            thread_num = NumericUpDown1.Value
            timer_id = 1
            ReDim count_matrix((list_trees.Count - 1) * list_trees.Count / 2)
            For i As Integer = 1 To (list_trees.Count - 1) * list_trees.Count / 2
                count_matrix(i) = -1
            Next
            ReDim timer_gen(thread_num - 1)
            stopW.Reset()
            stopW.Start()
            dist_type = "wSN"
            dist_info = "Using " + LeveledRFDistanceToolStripMenuItem.Text
            Label6.Text = ""
            ReDim my_threads(thread_num - 1)
            For i As Integer = 0 To thread_num - 1
                my_threads(i) = New Threading.Thread(AddressOf make_fdw_matrix)
                my_threads(i).Start(i)
            Next
        Else
            MsgBox("Do not find trees!")
        End If
    End Sub

    Private Sub RFDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RFDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "clean_num.trees") Then
            CheckForIllegalCrossThreadCalls = False
            thread_num = NumericUpDown1.Value
            timer_id = 1
            ReDim count_matrix((list_trees.Count - 1) * list_trees.Count / 2)
            For i As Integer = 1 To (list_trees.Count - 1) * list_trees.Count / 2
                count_matrix(i) = -1
            Next
            ReDim timer_gen(thread_num - 1)
            stopW.Reset()
            stopW.Start()
            dist_type = "nSN"
            dist_info = "Using " + RFDistanceToolStripMenuItem.Text
            Label6.Text = ""
            ReDim my_threads(thread_num - 1)
            For i As Integer = 0 To thread_num - 1
                my_threads(i) = New Threading.Thread(AddressOf make_SC_matrix)
                my_threads(i).Start(i)
            Next
        Else
            MsgBox("Do not find trees!")
        End If
    End Sub



    Private Sub Tool_Tree_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            timer_count = 0
            timer_id = 3
            Dim th1 As New Threading.Thread(AddressOf load_trees)
            th1.Start()
        End If
    End Sub
    Public Sub load_trees()
        Dim sr As New StreamReader(root_path + "temp" + path_char + "clean_num.trees")
        Dim line As String = sr.ReadLine
        list_trees.Clear()
        Do
            list_trees.Add(New Ploy_Tree(line))
            line = sr.ReadLine
            timer_count += 1
        Loop Until line Is Nothing
        sr.Close()
        timer_id = 0
        'If File.Exists(root_path + "temp\gene_name.txt") Then
        '    File.Delete(root_path + "temp\gene_name.txt")
        'End If
    End Sub

	Private Sub LoadGeneNamesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadGeneNamesToolStripMenuItem.Click
		Dim opendialog As New OpenFileDialog
		opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
		opendialog.FileName = ""
		opendialog.DefaultExt = ".txt"
		opendialog.CheckFileExists = False
		opendialog.CheckPathExists = True
		Dim resultdialog As DialogResult = opendialog.ShowDialog()
		If resultdialog = DialogResult.OK Then
			If File.Exists(opendialog.FileName) Then

				Dim sr As New StreamReader(opendialog.FileName)
				Dim sw As New StreamWriter(root_path + "temp\gene_name.txt")
				Dim line As String = sr.ReadLine
				Dim gene_count As Integer = 0
				Do
					gene_count += 1
					sw.WriteLine(line)
					line = sr.ReadLine
				Loop Until line Is Nothing Or line = ""
				sr.Close()
				sw.Close()
				If gene_count <> CInt(MainWindow.TreeBox_P.Text) Then
					MsgBox("You have " + MainWindow.TreeBox_P.Text + " trees, but load " + Str(gene_count) + " names.")
					File.Delete(root_path + "temp\gene_name.txt")
				End If
			End If
		End If

	End Sub

	Private Sub HierarchicalClusterToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HierarchicalClusterToolStripMenuItem.Click
        If File.Exists(root_path + "temp\matrix_0.txt") Then
            TextBox1.Text = ""
            ListView1.Items.Clear()
            If File.Exists(root_path + "temp\clust_end.txt") Then
                File.Delete(root_path + "temp\clust_end.txt")
            End If
            If File.Exists(root_path + "temp\clust_groups.txt") Then
                File.Delete(root_path + "temp\clust_groups.txt")
            End If
            If File.Exists(root_path + "temp\clust_iter.txt") Then
                File.Delete(root_path + "temp\clust_iter.txt")
            End If
            'If File.Exists(root_path + "temp\consense.exe") = False Then
            '	File.Copy(root_path + "Plug-ins\Phylip\consense.exe", root_path + "temp\consense.exe")
            'End If
            Label6.Text = "calculating.."
            ProgressBar1.Value = 100
            timer_id = 2
            reset_sw = True
            Dim th1 As New Threading.Thread(AddressOf r_cluster)
            th1.Start()
        Else
            MsgBox("Do not find matrix! Please calculate matrix first!")
        End If
	End Sub

    Private Sub MCLToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MCLToolStripMenuItem.Click
        If File.Exists(root_path + "temp\matrix_1.txt") Then
            ListView1.Items.Clear()
            If File.Exists(root_path + "temp\clust_end.txt") Then
                File.Delete(root_path + "temp\clust_end.txt")
            End If
            If File.Exists(root_path + "temp\clust_groups.txt") Then
                File.Delete(root_path + "temp\clust_groups.txt")
            End If
            If File.Exists(root_path + "temp\clust_iter.txt") Then
                File.Delete(root_path + "temp\clust_iter.txt")
            End If
			'If File.Exists(root_path + "temp\consense.exe") = False Then
			'    File.Copy(root_path + "Plug-ins\Phylip\consense.exe", root_path + "temp\consense.exe")
			'End If
			Label6.Text = "calculating.."
            ProgressBar1.Value = 100
            timer_id = 5
            reset_sw = True
            Dim th1 As New Threading.Thread(AddressOf r_mcl)
            th1.Start()

        Else
            MsgBox("Do not find matrix!")
        End If
    End Sub

    Private Sub ConsensusTreeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ConsensusTreeToolStripMenuItem.Click

    End Sub
    'Public Sub my_concensus()
    '    For j As Integer = 1 To ListView1.Items.Count - 1
    '        If File.Exists(root_path + "temp" + path_char + "outfile") Then
    '            File.Delete(root_path + "temp" + path_char + "outfile")
    '        End If
    '        If File.Exists(root_path + "temp" + path_char + "outtree") Then
    '            File.Delete(root_path + "temp" + path_char + "outtree")
    '        End If
    '        If File.Exists(root_path + "temp" + path_char + "intree") Then
    '            File.Delete(root_path + "temp" + path_char + "intree")
    '        End If
    '        Dim tree_count As Integer = 1
    '        If j = ListView1.Items.Count - 1 Then
    '            File.Copy(root_path + "temp\clean_num.trees", root_path + "temp\intree", True)
    '        Else
    '            File.Copy(root_path + "temp\clust_" + j.ToString + "_clean.trees", root_path + "temp\intree", True)
    '        End If
    '        tree_count = ListView1.Items(j).SubItems(1).Text
    '        Dim rt As New StreamReader(root_path + "temp\intree")
    '        Dim f_t_name() As String = rt.ReadLine.Replace("(", "").Replace(")", "").Replace(";", ",").Split(New Char() {","c})
    '        For i As Integer = 0 To UBound(f_t_name)
    '            If f_t_name(i).Contains(":") Then
    '                f_t_name(i) = f_t_name(i).Split(":")(0)
    '            End If
    '        Next
    '        rt.Close()
    '        MainWindow.DataGridView1.EndEdit()
    '        Dim og As String = "-1"
    '        For i As Integer = 1 To dtView.Count
    '            If MainWindow.DataGridView1.Rows(i - 1).Cells(0).FormattedValue.ToString = "True" Then
    '                og = i.ToString
    '            End If
    '        Next
    '        If og = "-1" Then
    '            Dim og_res As DialogResult = MsgBox("Please select one outgroup to build the consense tree.", MsgBoxStyle.Information)
    '            Exit Sub
    '        End If
    '        og = (Array.IndexOf(f_t_name, og) + 1)
    '        Dim temp_i As Int16 = CInt(og)
    '        ctree(temp_i, root_path, cons_tre)
    '        File.Copy(root_path + "temp\outtree", root_path + "temp\clust_" + j.ToString + "_con.tre", True)
    '    Next
    'End Sub

    Public Sub make_final_tree(ByVal tree_num As Integer)
        Dim line As String = ""
        Dim Temp As String = ""
        Dim rt As New StreamReader(root_path + "temp" + path_char + "outtree")
        Do
            Temp = rt.ReadLine
            line = line + Temp
        Loop Until Temp Is Nothing
        rt.Close()

        Dim f_t_name() As String
        ReDim f_t_name(dtView.Count)

        Dim tree_Temp1 As String = ""
        Dim tree_Temp As String = line.Replace("):", ")#")
        Dim tree_complete As String = ""
        Dim is_sym As Boolean = False
        Dim value As String = ""
        For Each tree_chr As Char In tree_Temp
            If tree_chr = ":" Then
                is_sym = True
            End If
            If tree_chr = "," Or tree_chr = "(" Or tree_chr = ")" Then
                is_sym = False
            End If
            If is_sym = False Then
                tree_Temp1 = tree_Temp1 + tree_chr.ToString
            End If
        Next

        For Each tree_chr As Char In tree_Temp1
            If tree_chr = "#" Then
                is_sym = True
            End If
            If tree_chr = "," Or tree_chr = "(" Or tree_chr = ")" Then
                is_sym = False
            End If
            If is_sym = False Then
                If value <> "" Then
                    tree_complete = tree_complete + (CSng(value) / tree_num).ToString("F2") + tree_chr.ToString
                    value = ""
                Else

                    tree_complete = tree_complete + tree_chr.ToString
                End If
            Else
                If tree_chr <> "#" Then
                    value = value + tree_chr
                End If
            End If

        Next

        tree_Temp1 = ""
        For Each tree_chr As Char In line
            If tree_chr = ":" Then
                is_sym = True
            End If
            If tree_chr = "," Or tree_chr = "(" Or tree_chr = ")" Then
                is_sym = False
            End If
            If is_sym = False Then
                tree_Temp1 = tree_Temp1 + tree_chr.ToString
            End If
        Next
        StartTreeView = True

        Dim Tree_view_form As New View_Tree
        Tree_view_form.show_my_tree = tree_complete.Replace("$%#", "").Replace(";", "1.00;")
        Tree_view_form.tree_view_limit = True
        Tree_view_form.Show()
    End Sub

	Private Sub RunClusterToolStripMenuItem_Click(sender As Object, e As EventArgs)

	End Sub

	Public Sub test_k()
        If CheckBox3.Checked Then
            write_matrix_limit(TextBox4.Text)
        Else
            write_matrix("matrix_1")
        End If
		Dim wr4 As New StreamWriter(root_path + "temp\run_cluster.r", False, System.Text.Encoding.Default)
		Dim sr1 As New StreamReader(root_path + "Plug-ins\CLUSTER\k_test.R")
		Dim line As String = sr1.ReadToEnd.Replace("#lib_path#", lib_path)

		line = line.Replace("#method#", ComboBox1.Text)
		If CheckBox3.Checked Then
			line = line.Replace("#limit#", TextBox4.Text)
            line = line.Replace("#matrix#", "matrix_1_" + TextBox4.Text + ".txt")
        Else
			line = line.Replace("#limit#", "1")
            line = line.Replace("#matrix#", "matrix_1.txt")
        End If

		line = line.Replace("#k_from#", NumericUpDown2.Value.ToString)
		line = line.Replace("#k_to#", NumericUpDown3.Value.ToString)

		wr4.Write(line)
		sr1.Close()
		wr4.Close()
		Dim wr5 As New StreamWriter(root_path + "temp\run_cluster.bat", False, System.Text.Encoding.Default)
		wr5.WriteLine("""" + rscript + """" + " run_cluster.r")
		wr5.Close()
		current_dir = Directory.GetCurrentDirectory
		Directory.SetCurrentDirectory(root_path + "temp\")
		Dim startInfo As New ProcessStartInfo
		startInfo.FileName = "run_cluster.bat"
		startInfo.WorkingDirectory = root_path + "temp"
		startInfo.UseShellExecute = False
		startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
		Process.Start(startInfo)
		Directory.SetCurrentDirectory(current_dir)
	End Sub

	Private Sub TestKToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles TestKToolStripMenuItem1.Click
		If File.Exists(root_path + "temp\matrix_0.txt") Then
			If NumericUpDown3.Value - NumericUpDown2.Value >= 2 Then
				ListView1.Items.Clear()
				If File.Exists(root_path + "temp\clust_end.txt") Then
					File.Delete(root_path + "temp\clust_end.txt")
				End If
				If File.Exists(root_path + "temp\clust_iter.txt") Then
					File.Delete(root_path + "temp\clust_iter.txt")
				End If
				If File.Exists(root_path + "temp\best_k.txt") Then
					File.Delete(root_path + "temp\best_k.txt")
				End If
				Label6.Text = "calculating.."
				ProgressBar1.Value = 100
				timer_id = 6
				reset_sw = True
				Dim th1 As New Threading.Thread(AddressOf test_k)
				th1.Start()
			Else
				MsgBox("The range of k should be greater than 3!")
			End If

		Else
			MsgBox("Do not find matrix!")
		End If
	End Sub

    Private Sub TestToolStripMenuItem_Click(sender As Object, e As EventArgs)
        MsgBox(node_distance(list_trees(0), 1, 9))
    End Sub
    Public Function node_distance(ByVal tree As Object, ByVal terminal_id1 As Integer, ByVal terminal_id2 As Integer)
        Dim common_node As Integer = -1
        Dim node_length As Integer = tree.Taxon_Number + 1
        For i As Integer = 0 To tree.Node_Number - 1
            Dim temp_chain() As String = tree.Node_Chain(i).Split(",")
            If Array.IndexOf(temp_chain, CStr(terminal_id1)) >= 0 And Array.IndexOf(temp_chain, CStr(terminal_id2)) > 0 Then
                If node_length > temp_chain.Length Then
                    node_length = temp_chain.Length
                    common_node = i
                End If
            End If
        Next
        If tree.Has_Length Then
            Return tree.Time_Length(terminal_id1 - 1) + tree.Time_Length(terminal_id2 - 1) - 2 * tree.Node_Total_Length(common_node)
        Else
            Return Math.Max(tree.Time_Length(terminal_id1 - 1), tree.Time_Length(terminal_id2 - 1)) * 2 - 2 * tree.Node_Total_Length(common_node)
        End If
    End Function

    Private Sub HideCMDWindowToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles HideCMDWindowToolStripMenuItem.Click

    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If ListView1.SelectedItems.Count > 0 Then
            '合并树
            'If ListView1.SelectedIndices(0) <> 0 Then
            '    If File.Exists(root_path + "temp" + path_char + "outfile") Then
            '        File.Delete(root_path + "temp" + path_char + "outfile")
            '    End If
            '    If File.Exists(root_path + "temp" + path_char + "outtree") Then
            '        File.Delete(root_path + "temp" + path_char + "outtree")
            '    End If
            '    If File.Exists(root_path + "temp" + path_char + "intree") Then
            '        File.Delete(root_path + "temp" + path_char + "intree")
            '    End If
            '    Dim tree_count As Integer = 1
            '    If ListView1.SelectedIndices(0) = ListView1.Items.Count - 1 Then
            '        File.Copy(root_path + "temp\clean_num.trees", root_path + "temp\intree", True)
            '    Else
            '        File.Copy(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_clean.trees", root_path + "temp\intree", True)
            '    End If
            '    tree_count = ListView1.SelectedItems(0).SubItems(1).Text
            '    Dim rt As New StreamReader(root_path + "temp\intree")
            '    Dim f_t_name() As String = rt.ReadLine.Replace("(", "").Replace(")", "").Replace(";", ",").Split(New Char() {","c})
            '    For i As Integer = 0 To UBound(f_t_name)
            '        If f_t_name(i).Contains(":") Then
            '            f_t_name(i) = f_t_name(i).Split(":")(0)
            '        End If
            '    Next
            '    rt.Close()
            '    MainWindow.DataGridView1.EndEdit()
            '    Dim og As String = "-1"
            '    For i As Integer = 1 To dtView.Count
            '        If MainWindow.DataGridView1.Rows(i - 1).Cells(0).FormattedValue.ToString = "True" Then
            '            og = i.ToString
            '        End If
            '    Next
            '    If og = "-1" Then
            '        Dim og_res As DialogResult = MsgBox("Please select one outgroup to build the consense tree.", MsgBoxStyle.Information)
            '        Exit Sub
            '    End If
            '    og = (Array.IndexOf(f_t_name, og) + 1)
            '    Dim temp_i As Int16 = CInt(og)

            '    current_dir = Directory.GetCurrentDirectory
            '    Directory.SetCurrentDirectory(root_path)
            '    ctree(temp_i, root_path, cons_tre)
            '    Directory.SetCurrentDirectory(current_dir)

            '    'File.Copy(root_path + "temp\intree", root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_con.tre", True)
            '    make_final_tree(tree_count)
            'End If
            '显示成分
            'If ListView1.SelectedIndices(0) <> 0 Then
            '    If ListView1.SelectedIndices(0) <> ListView1.Items.Count - 1 Then
            '        Dim id_sumer As New StreamReader(root_path + "temp\gene_name.txt")
            '        Dim gene_name_list(0) As String
            '        Dim gene_name_count(0) As Integer
            '        Dim gene_name_count_cluster(0) As Integer
            '        Dim line As String = id_sumer.ReadLine
            '        Do While line Is Nothing = False
            '            Dim name As String = line.Substring(0, line.LastIndexOf("_"))
            '            If Array.IndexOf(gene_name_list, name) < 0 Then
            '                ReDim Preserve gene_name_list(gene_name_list.Length)
            '                gene_name_list(UBound(gene_name_list)) = name
            '                ReDim Preserve gene_name_count(gene_name_count.Length)
            '                gene_name_count(UBound(gene_name_count)) = 1
            '                ReDim Preserve gene_name_count_cluster(gene_name_count_cluster.Length)
            '                gene_name_count_cluster(UBound(gene_name_count_cluster)) = 0
            '            Else
            '                gene_name_count(Array.IndexOf(gene_name_list, name)) += 1
            '            End If
            '            line = id_sumer.ReadLine
            '        Loop
            '        id_sumer.Close()


            '        Dim id_reader As New StreamReader(root_path + "temp\clust_" + ListView1.SelectedIndices(0).ToString + "_id.txt")
            '        line = id_reader.ReadLine
            '        Do While line Is Nothing = False
            '            Dim name As String = line.Substring(0, line.LastIndexOf("_"))
            '            If Array.IndexOf(gene_name_list, name) >= 0 Then
            '                gene_name_count_cluster(Array.IndexOf(gene_name_list, name)) += 1
            '            End If
            '            line = id_reader.ReadLine
            '        Loop
            '        id_reader.Close()
            '        TextBox1.Text = ""
            '        For i As Integer = 1 To UBound(gene_name_count_cluster)
            '            TextBox1.Text += gene_name_list(i) + vbTab + gene_name_count_cluster(i).ToString + "/" + gene_name_count(i).ToString + vbCrLf
            '        Next
            '    End If
            'End If
        End If
    End Sub

    Private Sub TripleDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TripleDistanceToolStripMenuItem.Click
        If dtView.Count <= 400 Then
            If File.Exists(root_path + "temp\mpest.end") Then
                File.Delete(root_path + "temp\mpest.end")
            End If
            Dim sr As New StreamReader(root_path + "temp\clean_num.trees")
            Dim sw_mpsettree As New StreamWriter(root_path + "temp\mpest.trees")
            Dim line As String = sr.ReadLine()
            Do
                sw_mpsettree.WriteLine(line.Replace("(", "(T").Replace(",", ",T").Replace("T(", "("))
                line = sr.ReadLine()
            Loop Until line Is Nothing
            sw_mpsettree.Close()
            sr.Close()

            Dim sw As New StreamWriter(root_path + "temp\contrl.txt")
            sw.WriteLine("mpest.trees")
            sw.WriteLine("1")
            sw.WriteLine("-1")
            sw.WriteLine("1")
            sw.WriteLine(list_trees.Count.ToString + " " + list_trees(0).Taxon_Number.ToString)
            For i As Integer = 1 To list_trees(0).Taxon_Number
                sw.WriteLine("T" + i.ToString + " 1 " + "T" + i.ToString)
            Next
            sw.WriteLine("0")
            sw.Close()
            Dim sw_run As New StreamWriter(root_path + "temp\run_mpest.bat", False, System.Text.Encoding.Default)
            sw_run.WriteLine("""" + root_path + "Plug-ins\mpest.exe" + """" + " contrl.txt")
            sw_run.WriteLine("echo end>mpest.end")
            sw_run.WriteLine("exit")
            sw_run.Close()
            timer_id = 7
            dist_type = "mpest"
            dist_info = "Using " + TripleDistanceToolStripMenuItem.Text
            current_dir = Directory.GetCurrentDirectory
            Directory.SetCurrentDirectory(root_path + "temp\")
            Dim startInfo As New ProcessStartInfo
            startInfo.FileName = "run_mpest.bat"
            startInfo.WorkingDirectory = root_path + "temp"
            startInfo.UseShellExecute = False
            startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
            Process.Start(startInfo)
            Directory.SetCurrentDirectory(current_dir)
        Else
            MsgBox("The mp-est included in RASP could not handle more than 400 species.")
        End If


    End Sub
    Private Sub SPRDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SPRDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "spr"
        dist_info = "Using " + SPRDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub
    Public Sub cal_dist(ByVal cal_dist_type As String)
        Dim wr1 As New StreamWriter(root_path + "temp\run_dist.r", False, System.Text.Encoding.Default)
        Dim sr1 As New StreamReader(root_path + "Plug-ins\CLUSTER\DIST.R")
        Dim line As String = sr1.ReadToEnd.Replace("#lib_path#", lib_path)
        line = line.Replace("#" + TargetOS + "#", "")
        line = line.Replace("#" + cal_dist_type + "#", "")
        wr1.Write(line)
        sr1.Close()
        wr1.Close()

        Dim sw_run As New StreamWriter(root_path + "temp\run_dist.bat", False, System.Text.Encoding.Default)
        sw_run.WriteLine("""" + rscript + """" + " run_dist.r")
        sw_run.WriteLine("echo end>dist.end")
        sw_run.WriteLine("exit")
        sw_run.Close()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_dist.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)

    End Sub
    Public Sub draw_heatmap(ByVal file_name As String)
        If File.Exists(root_path + "temp\heatmap.end") Then
            File.Delete(root_path + "temp\heatmap.end")
        End If
        Dim wr1 As New StreamWriter(root_path + "temp\run_heatmap.r", False, System.Text.Encoding.Default)
        Dim sr1 As New StreamReader(root_path + "Plug-ins\CLUSTER\heatmap.R")
        Dim line As String = sr1.ReadToEnd.Replace("#lib_path#", lib_path)

        line = line.Replace("#matrix#", file_name)

        wr1.Write(line)
        sr1.Close()
        wr1.Close()

        Dim sw_run As New StreamWriter(root_path + "temp\run_heatmap.bat", False, System.Text.Encoding.Default)
        sw_run.WriteLine("""" + rscript + """" + " run_heatmap.r")
        sw_run.WriteLine("echo end>heatmap.end")
        sw_run.WriteLine("exit")
        sw_run.Close()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_heatmap.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = HideCMDWindowToolStripMenuItem.Checked
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)

    End Sub
    Private Sub SubcladesDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SubcladesDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "clean_num.trees") Then
            CheckForIllegalCrossThreadCalls = False
            thread_num = NumericUpDown1.Value

            timer_id = 1
            ReDim count_matrix((list_trees.Count - 1) * list_trees.Count / 2)
            For i As Integer = 1 To (list_trees.Count - 1) * list_trees.Count / 2
                count_matrix(i) = -1
            Next
            ReDim timer_gen(thread_num - 1)
            stopW.Reset()
            stopW.Start()
            dist_type = "SN"
            dist_info = "Using " + SubcladesDistanceToolStripMenuItem.Text
            Label6.Text = ""
            ReDim my_threads(thread_num - 1)
            For i As Integer = 0 To thread_num - 1
                my_threads(i) = New Threading.Thread(AddressOf make_SC_matrix)
                my_threads(i).Start(i)
            Next
        Else
            MsgBox("Do not find trees!")
        End If
    End Sub

    Private Sub RFDistanceToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles RFDistanceToolStripMenuItem1.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "RF"
        dist_info = "Using " + RFDistanceToolStripMenuItem1.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub NormalizedRFDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalizedRFDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "nRF"
        dist_info = "Using " + NormalizedRFDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub NormalizedWeightedRFDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NormalizedWeightedRFDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "nwRF"
        dist_info = "Using " + NormalizedWeightedRFDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub PathDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PathDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "path"
        dist_info = "Using " + PathDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub WeightedPathDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles WeightedPathDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "wpath"
        dist_info = "Using " + WeightedPathDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub KFDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KFDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "KF"
        dist_info = "Using " + KFDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub

    Private Sub SaveGraphicToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveGraphicToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "clust_temp.png") Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "PNG File (*.png)|*.png;*.PNG|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".png"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                File.Copy(root_path + "temp" + path_char + "clust_temp.png", opendialog.FileName, True)
            End If
        Else
            MsgBox("No graphic to save!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub Label6_Click(sender As Object, e As EventArgs) Handles Label6.Click
        'If File.Exists(root_path + "temp\heatmap.png") Then
        '    File.Copy(root_path + "temp\heatmap.png", root_path + "temp\clust_temp.png", True)
        '    PictureBox1.Load(root_path + "temp\clust_temp.png")
        'End If
    End Sub


    Private Sub ShowHeatmapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowHeatmapToolStripMenuItem.Click
        If File.Exists(root_path + "temp\heatmap.png") Then
            File.Copy(root_path + "temp\heatmap.png", root_path + "temp\clust_temp.png", True)
            PictureBox1.Load(root_path + "temp\clust_temp.png")
        Else
            MsgBox("Could not find heatmap! Please make a heatmap first!")
        End If
    End Sub

    Private Sub MakeHeatmapToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MakeHeatmapToolStripMenuItem.Click
        If File.Exists(root_path + "temp" + path_char + "matrix_0.txt") Then
            timer_id = 9
            draw_heatmap("matrix_0.txt")
        Else
            MsgBox("Do not find matrix! Please calculate matrix first!")
        End If
    End Sub

    Private Sub KCDistanceToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles KCDistanceToolStripMenuItem.Click
        If File.Exists(root_path + "temp\dist.end") Then
            File.Delete(root_path + "temp\dist.end")
        End If
        dist_type = "KC"
        dist_info = "Using " + KCDistanceToolStripMenuItem.Text
        ProgressBar1.Value = 1600
        timer_id = 8
        reset_sw = True
        cal_dist(dist_type)
    End Sub
End Class