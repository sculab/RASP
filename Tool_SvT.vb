Imports System.IO
Imports System.Runtime.InteropServices
Public Class Tool_SvT
    Public range_list() As Char
    Public state_group() As String
    Dim list_trees As New ArrayList
    Dim weight_trees() As Single
    Dim timer_id As Integer = 0
    Dim timer_count As Integer
    Dim tree_value_arrray() As Single
    Dim taxon_value_array() As Single
    Private Sub Tool_State_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        Timer_R.Enabled = True
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
        If File.Exists(root_path + "temp\gene_name.txt") Then
            File.Delete(root_path + "temp\gene_name.txt")
        End If
    End Sub
    Public Sub find_best_tree()
        Select Case state_mode
            Case 0
                If ListBox1.SelectedIndices.Count >= 1 Then
                    ListView1.Items.Clear()
                    ListView1.Columns(1).Text = "Diff."
                    ReDim weight_trees(list_trees.Count - 1)
                    Dim tree_id() As Integer
                    ReDim tree_id(list_trees.Count - 1)
                    For i As Integer = 0 To list_trees.Count - 1
                        timer_count += 1
                        tree_id(i) = i
                        Dim total_count As Integer = 0
                        For Each j As Integer In ListBox1.SelectedIndices
                            Dim k_count As Integer = 1000000000
                            If Array.IndexOf(list_trees(i).Node_Chain, state_group(j)) >= 0 Or state_group(j).Contains(",") = False Then
                                k_count = 0
                            Else
                                For k As Integer = 0 To list_trees(i).Node_Number - 2
                                    k_count = Math.Min(comp_list(state_group(j), list_trees(i).Node_Chain(k)), k_count)
                                Next
                            End If
                            total_count += k_count
                        Next
                        weight_trees(i) = total_count
                    Next
                    Array.Sort(weight_trees, tree_id, New scomparer)
                    Dim new_list As List(Of ListViewItem) = New List(Of ListViewItem)()
                    For i As Integer = list_trees.Count - 1 To 0 Step -1
                        new_list.Add(New ListViewItem({(tree_id(i) + 1).ToString, weight_trees(i).ToString, list_trees(tree_id(i)).Tree_Line}))
                    Next
                    ListView1.Items.AddRange(new_list.ToArray)
                Else
                    MsgBox("One state should be selected at least!")
                End If
            Case 1
                ListView1.Items.Clear()
                ListView1.Columns(1).Text = "Score"
                ReDim tree_value_arrray(dtView.Count * (dtView.Count - 1) / 2)
                ReDim taxon_value_array(dtView.Count * (dtView.Count - 1) / 2)
                ReDim weight_trees(list_trees.Count - 1)
                Dim count As Integer = 0
                For i As Integer = 1 To dtView.Count - 1
                    For j As Integer = i + 1 To dtView.Count
                        count += 1
                        If dtView.Item(i - 1).Item(state_index).ToString = "" Or dtView.Item(j - 1).Item(state_index).ToString = "" Or dtView.Item(i - 1).Item(state_index).ToString = "?" Or dtView.Item(j - 1).Item(state_index).ToString = "?" Then
                            taxon_value_array(count) = 1 / 0
                        Else
                            taxon_value_array(count) = Math.Abs(CSng(dtView.Item(i - 1).Item(state_index)) - CSng(dtView.Item(j - 1).Item(state_index)))
                        End If
                    Next
                Next
                Dim x_min As Single = matrix_Min_positive(taxon_value_array, 1)
                Dim x_max As Single = matrix_Max_positive(taxon_value_array, 1)
                For i As Integer = 1 To UBound(taxon_value_array)
                    taxon_value_array(i) = (taxon_value_array(i) - x_min) / (x_max - x_min)
                Next
                Dim x_average As Single = AVERAGE_positive(taxon_value_array, 1)
                Dim tree_id() As Integer
                ReDim tree_id(list_trees.Count - 1)
                For k As Integer = 0 To list_trees.Count - 1
                    timer_count += 1
                    tree_id(k) = k
                    count = 0
                    For i As Integer = 1 To dtView.Count - 1
                        For j As Integer = i + 1 To dtView.Count
                            count += 1
                            tree_value_arrray(count) = node_distance(list_trees(k), i, j)
                        Next
                    Next
                    Dim t_min As Single = matrix_Min_positive(tree_value_arrray, 1)
                    If t_min < 0 Then
                        MsgBox(list_trees(k).Tree_Line)
                    End If
                    Dim t_max As Single = matrix_Max_positive(tree_value_arrray, 1)
                    For i As Integer = 1 To UBound(tree_value_arrray)
                        tree_value_arrray(i) = (tree_value_arrray(i) - t_min) / (t_max - t_min)
                    Next
                    Dim t_average As Single = AVERAGE_positive(tree_value_arrray, 1)

                    Dim coco_up As Single = 0
                    Dim coco_down_1 As Single = 0
                    Dim coco_down_2 As Single = 0
                    For i As Integer = 1 To UBound(tree_value_arrray)
                        If taxon_value_array(i) <> 1 / 0 Then
                            coco_up += (taxon_value_array(i) - x_average) * (tree_value_arrray(i) - t_average)
                            coco_down_1 += (taxon_value_array(i) - x_average) ^ 2
                            coco_down_2 += (tree_value_arrray(i) - t_average) ^ 2
                        End If
                    Next
                    weight_trees(k) = coco_up / ((coco_down_1 * coco_down_2) ^ 0.5)
                Next
                Array.Sort(weight_trees, tree_id, New lcomparer)
                Dim new_list As List(Of ListViewItem) = New List(Of ListViewItem)()
                For i As Integer = list_trees.Count - 1 To 0 Step -1
                    new_list.Add(New ListViewItem({(tree_id(i) + 1).ToString, weight_trees(i).ToString, list_trees(tree_id(i)).Tree_Line}))
                Next
                ListView1.Items.AddRange(new_list.ToArray)
        End Select
        timer_id = 0
    End Sub
    Private Sub FindBestFitTreeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindBestFitTreeToolStripMenuItem.Click
        timer_count = 0
        timer_id = 1
        Dim th1 As New Threading.Thread(AddressOf find_best_tree)
        th1.Start()
    End Sub
    Public Function cal_matrix_index(ByVal i As Integer, ByVal j As Integer, ByVal t As Integer) As Integer
        Return (2 * t - i - 1) * i / 2 - t + j
    End Function
    Public Function node_distance(ByVal tree As Object, ByVal terminal_id1 As Integer, ByVal terminal_id2 As Integer) As Single
        Dim common_node As Integer = -1
        Dim node_list1() As String = tree.Terminal_Chain(terminal_id1 - 1).Split(",")
        Dim node_list2() As String = tree.Terminal_Chain(terminal_id2 - 1).Split(",")
        For i As Integer = 1 To UBound(node_list1)
            If Array.IndexOf(node_list2, node_list1(i)) >= 0 Then
                common_node = node_list1(i)
                Exit For
            End If
        Next
        If tree.Has_Length Then
            Return tree.Terminal_Total_Length(terminal_id1 - 1) + tree.Terminal_Total_Length(terminal_id2 - 1) - 2 * tree.Node_Total_Length(common_node)
        Else
            Return Math.Max(tree.Time_Length(terminal_id1 - 1), tree.Time_Length(terminal_id2 - 1)) * 2 - 2 * tree.Node_Total_Length(common_node)
        End If
    End Function
    Public Function comp_list(ByVal str1 As String, ByVal str2 As String) As Integer
        Dim arr1() As String = str1.Split(",")
        Dim arr2() As String = str2.Split(",")
        Dim count As Integer = 0
        For Each i As String In arr1
            If Array.IndexOf(arr2, i) >= 0 Then
                count += 1
            End If
        Next
        Return arr1.Length + arr2.Length - 2 * count
    End Function

    Private Sub Tool_State_MenuComplete(sender As Object, e As EventArgs) Handles Me.MenuComplete

    End Sub

    Private Sub Tool_State_VisibleChanged(sender As Object, e As EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            MainWindow.DataGridView1.Enabled = False
            Select Case state_mode
                Case 0
                    ListBox1.Items.Clear()
                    ListView1.Items.Clear()
                    range_list = RangeStr
                    Array.Sort(range_list)
                    For Each i As Char In range_list
                        ListBox1.Items.Add(i)
                        ListBox1.SetSelected(ListBox1.Items.Count - 1, True)
                    Next
                    MainWindow.DataGridView1.EndEdit()
                    ReDim state_group(RangeStr.Length - 1)
                    For i As Integer = 1 To dtView.Count
                        For Each c As Char In dtView.Item(i - 1).Item(state_index).ToString.ToUpper
                            state_group(Asc(c) - Asc("A")) += "," + i.ToString
                        Next
                    Next
                    For i As Integer = 0 To UBound(state_group)
                        state_group(i) = state_group(i).Remove(0, 1)
                    Next
                Case 1
                    ListBox1.Items.Clear()
                    ListView1.Items.Clear()
                    ListBox1.Items.Add(MainWindow.DataGridView1.Columns(state_index + 1).HeaderText)
                    For i As Integer = 1 To dtView.Count
                        ListBox1.Items.Add(dtView.Item(i - 1).Item(state_index).ToString)
                    Next
            End Select

            timer_count = 0
            timer_id = 3
            Dim th1 As New Threading.Thread(AddressOf load_trees)
            th1.Start()

        End If

    End Sub

    Private Sub Tool_State_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        MainWindow.DataGridView1.Enabled = True
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub ViewTreeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        '
    End Sub



    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        If Me.ListView1.SelectedItems.Count > 0 Then
            Dim lvi As ListViewItem = Me.ListView1.SelectedItems(0)
            StartTreeView = True

            Dim Tree_view_form As New View_Tree
            Tree_view_form.tree_view_limit = True
            Tree_view_form.show_my_tree = lvi.SubItems(2).Text
            Tree_view_form.Show()
        End If
    End Sub

    Private Sub Timer_R_Tick(sender As Object, e As EventArgs) Handles Timer_R.Tick
        Try
            Select Case timer_id
                Case 0
                    ProgressBar1.Value = 0
                    Label6.Text = ""
                Case 1
                    Label6.Text = timer_count.ToString & "/" & list_trees.Count.ToString
                    ProgressBar1.Value = timer_count / list_trees.Count * 10000
                Case 3
                    Label6.Text = "loading"
                    ProgressBar1.Value = timer_count / CInt(MainWindow.TreeBox_P.Text) * 10000
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TreesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles TreesToolStripMenuItem.Click
        If ListView1.Items.Count > 0 Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Trees File (*.trees)|*.trees;*.TREES|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".trees"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                export_omitted(opendialog.FileName)

            End If
        End If
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
                    sw.WriteLine("""" + ListView1.Items(i).SubItems(0).Text + """" + "," + """" + ListView1.Items(i).SubItems(1).Text + """" + "," + """" + ListView1.Items(i).SubItems(2).Text + """")
                Next
                sw.Close()
            End If
        End If

    End Sub

    Public Sub export_omitted(ByVal export_file_name As String)
        Dim wt As New StreamWriter(export_file_name, False)
        wt.WriteLine("#NEXUS")
        wt.WriteLine("")
        wt.WriteLine("Begin taxa;")
        wt.WriteLine("	Dimensions ntax=" + dtView.Count.ToString + ";")
        wt.WriteLine("	Taxlabels")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(1))
        Next

        wt.WriteLine("		;")
        wt.WriteLine("End;")
        wt.WriteLine("")
        wt.WriteLine("Begin trees;")
        wt.WriteLine("	Translate")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(0) + " " + dtView.Item(i)(1) + ",")
        Next
        wt.WriteLine(";")

        For i As Integer = 0 To ListView1.Items.Count - 1
            wt.WriteLine("tree id_" + ListView1.Items(i).SubItems(0).Text + "_" + ListView1.Items(i).SubItems(1).Text + "=" + ListView1.Items(i).SubItems(2).Text + ";")
        Next

        wt.WriteLine("End;")
        wt.Close()
    End Sub

    Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub
End Class