Imports System.IO
Public Class Config_Traits


    Dim Select_Node_Num As Integer
    Dim traits_result_path, traits_result_file As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim isselectnode As Boolean = False
        Dim commandlines As String = ""
        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                isselectnode = True
            End If
        Next
        If isselectnode = False Then
            MsgBox("Select one node at least!", MsgBoxStyle.Information)
            Exit Sub
        End If
        If CInt(TextBox4.Text) >= CInt(TextBox3.Text) - CInt(TextBox1.Text) - 1 Then
            MsgBox("Number of discard samples is too large!")
            Exit Sub
        End If
        If CInt(TextBox4.Text) < 1000 Then
            MsgBox("Number of discard samples is too small!")
            Exit Sub
        End If

        Dim usertree As Boolean = False
        If final_tree.Replace(",", "").Length = final_tree.Replace("(", "").Length Then
            usertree = True
        End If
        Dim node_count As Integer = final_tree.Length - final_tree.Replace("(", "").Length

        Read_Poly_Node(final_tree.Replace(";", ""))

        commandlines += (ComboBox6.SelectedIndex + 1).ToString + vbLf
        commandlines += (ComboBox1.SelectedIndex + 1).ToString + vbLf

        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                Dim node_id As String = DataGridView2.Rows(nodeView.Count - i).Cells(0).FormattedValue.ToString
                node_id = node_id.Split(":")(0)
                commandlines += "AddTag TNode" + node_id + " " + Poly_Node((nodeView.Count - i), 3).Replace(",", " ") + vbLf
                commandlines += "AddNode Node" + node_id.ToString + " TNode" + node_id + vbLf
                If DataGridView2.Rows(i - 1).Cells(3).FormattedValue.ToString <> "" Then
                    commandlines += "Fossil FNode" + node_id + " TNode" + node_id + " " + DataGridView2.Rows(i - 1).Cells(3).FormattedValue.ToString.ToUpper + vbLf
                End If
            End If

        Next

        If ComboBox1.SelectedIndex = 0 Then
            commandlines += "MLTries " + TextBox6.Text + vbLf
        Else
            commandlines += "Sample " + TextBox1.Text + vbLf
            commandlines += "Iterations " + TextBox3.Text + vbLf
            commandlines += "BurnIn " + TextBox4.Text + vbLf
            commandlines += ComboBox2.Text + vbLf
            commandlines += ComboBox3.Text + vbLf
            commandlines += ComboBox5.Text + vbLf
            commandlines += ComboBox4.Text + vbLf
        End If
        commandlines += TextBox2.Text.Replace(Chr(13), vbLf) + vbLf
        commandlines += "run" + vbLf

        If MainWindow.CheckBox3.Checked Then
            make_rand_tree()
            export_omitted(root_path + "temp\trait.trees", root_path + "temp" + path_char + "random_clean_num.trees", True)
        Else
            export_omitted(root_path + "temp\trait.trees", root_path + "temp" + path_char + "clean_num.trees", False)
        End If
        Dim dw As New StreamWriter(root_path + "temp\trait.dat", False)
        For i As Integer = 1 To dtView.Count
            dtView.Item(i - 1).Item(state_index) = dtView.Item(i - 1).Item(state_index).ToString.ToUpper.Replace(" ", "")
            If dtView.Item(i - 1).Item(state_index) = "" Or dtView.Item(i - 1).Item(state_index) = "\" Then
                dw.WriteLine(dtView.Item(i - 1).Item(0).ToString + "	" + "-")
            Else
                dw.WriteLine(dtView.Item(i - 1).Item(0).ToString + "	" + dtView.Item(i - 1).Item(state_index).ToString.ToUpper)
            End If
        Next
        dw.Close()

        Dim dw1 As New StreamWriter(root_path + "temp\trait.ini", False)
        dw1.Write(commandlines)
        dw1.Close()

        If File.Exists(root_path + "temp\endtraits") Then
            File.Delete(root_path + "temp\endtraits")
        End If
        If File.Exists(root_path + "temp\trait.dat.log.txt") Then
            File.Delete(root_path + "temp\trait.dat.log.txt")
        End If
        If File.Exists(root_path + "temp\trait.dat.log.txt.Stones.txt") Then
            File.Delete(root_path + "temp\trait.dat.log.txt.Stones.txt")
        End If
        Dim dw2 As New StreamWriter(root_path + "temp\run_traits.bat", False)
        dw2.WriteLine("""" + root_path + "Plug-ins\BayesTraits.exe" + """" + " trait.trees trait.dat < trait.ini")
        dw2.WriteLine("cls>endtraits")
        dw2.Close()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path + "temp\")
        Dim startInfo As New ProcessStartInfo
        startInfo.FileName = "run_traits.bat"
        startInfo.WorkingDirectory = root_path + "temp"
        startInfo.UseShellExecute = False
        startInfo.CreateNoWindow = False
        Process.Start(startInfo)
        Directory.SetCurrentDirectory(current_dir)
        traits_result_path = root_path + "temp\"
        traits_result_file = root_path + "temp\trait.dat.log.txt"

        TimerTraits.Enabled = True

    End Sub

    Public Sub make_rand_tree()
        Dim seed As Integer = DateTime.Now.Millisecond
        If Global_seed <> "20180127" Then
            seed = Global_seed
        End If
        Dim rand As New System.Random(seed)
        Dim t As Integer = 0
        Dim wr As New StreamWriter(root_path + "temp" + path_char + "random_clean_num.trees", False)
        'Dim count As Integer = 1
        Dim ran_list(0) As Integer
        ReDim ran_list(CInt(MainWindow.RandomTextBox.Text))
        ran_list(0) = 0
        For i As Integer = 1 To CInt(MainWindow.RandomTextBox.Text)
            t = rand.Next(CInt(MainWindow.BurninBox.Text) + 1, CInt(MainWindow.TreeBox.Text))
            ran_list(i) = t
        Next
        Array.Sort(ran_list)
        Dim sr As New StreamReader(root_path + "temp" + path_char + "clean_num.trees")
        Dim line As String = ""
        For i As Integer = 1 To CInt(MainWindow.RandomTextBox.Text)
            For j As Integer = ran_list(i - 1) + 1 To ran_list(i)
                line = sr.ReadLine()
            Next
            If line <> "" Then
                wr.WriteLine(line)
            End If
        Next
        sr.Close()
        wr.Close()
        ProgressBar1.Value = 0

    End Sub
    Public Sub export_omitted(ByVal export_file_name As String, ByVal source_file_name As String, ByVal is_rand As Boolean)
        Dim wt As New StreamWriter(export_file_name, False)
        wt.WriteLine("#NEXUS")
        wt.WriteLine("")
        wt.WriteLine("Begin taxa;")
        wt.WriteLine("	Dimensions ntax=" + dtView.Count.ToString + ";")
        wt.WriteLine("	Taxlabels")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(0).ToString)
        Next

        wt.WriteLine("		;")
        wt.WriteLine("End;")
        wt.WriteLine("")
        wt.WriteLine("Begin trees;")
        wt.WriteLine("	Translate")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(0).ToString + " " + dtView.Item(i)(0).ToString + ",")
        Next
        wt.WriteLine(";")
        Dim rt As New StreamReader(source_file_name)
        Dim line As String
        Dim tree_num As Integer = 1
        If is_rand = False Then
            For i As Integer = 1 To CInt(MainWindow.BurninBox.Text)
                line = rt.ReadLine
                tree_num = tree_num + 1
            Next
        End If

        line = rt.ReadLine
        Do
            wt.WriteLine("tree TREE_" + tree_num.ToString + " = " + line)
            line = rt.ReadLine
            tree_num = tree_num + 1
        Loop Until line Is Nothing
        wt.WriteLine("End;")
        rt.Close()
        wt.Close()
    End Sub

    Private Sub Bayestraits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
        ComboBox4.SelectedIndex = 0
        ComboBox5.SelectedIndex = 1
        ComboBox6.SelectedIndex = 0
    End Sub

    Private Sub Config_Traits_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            If TimerTraits.Enabled = True Then
                Dim msg_reslut As DialogResult = MsgBox("BayesTraits is still running, stop it?", MsgBoxStyle.YesNo)
                If msg_reslut = Windows.Forms.DialogResult.Yes Then
                    TimerTraits.Enabled = False
                    ProgressBar1.Value = 0
                End If
            End If
            If DataGridView2.ColumnCount = 0 Then
                DataGridView2.DataSource = nodeView
                DataGridView2.RowHeadersVisible = False
                Dim select_node As New DataGridViewCheckBoxColumn
                select_node.HeaderText = "Select"
                DataGridView2.Columns.Add(select_node)
                Dim Fossil_node As New DataGridViewTextBoxColumn
                Fossil_node.HeaderText = "Fossil"
                DataGridView2.Columns.Add(Fossil_node)
                DataGridView2.Columns(0).Width = 75
                DataGridView2.Columns(1).Width = 205
                DataGridView2.Columns(2).Width = 50
                DataGridView2.Columns(3).Width = 75
                DataGridView2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(0).ReadOnly = True
                DataGridView2.Columns(1).ReadOnly = True
                DataGridView2.Columns(2).ReadOnly = True
                DataGridView2.Columns(2).Visible = False


            End If
            For i As Integer = 1 To DataGridView2.Rows.Count
                DataGridView2.Rows(i - 1).Cells(2).Value = True
            Next
        End If
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub
    Public Sub read_traits_file(ByVal infile As String)
        Dim line As String = ""
        Dim marg_ml As Single = 0
        If File.Exists(infile + ".Stones.txt") Then
            Dim sr_s As New StreamReader(infile + ".Stones.txt")
            Do
                If line.Contains("likelihood") Then
                    marg_ml = CSng(line.Split(":")(1))
                End If
                line = sr_s.ReadLine
            Loop Until line Is Nothing
        End If
        Dim sr As New StreamReader(infile)
        Do
            line = sr.ReadLine
            If line Is Nothing Then
                MsgBox("Could not load this file!")
                Exit Sub
            End If
        Loop Until line.Contains("Root P")
        Dim head_str() As String = line.Split("	")
        Dim node_result(,) As Single
        Dim area_count As Integer = RangeStr.Length
        Dim node_count As Integer = nodeView.Count
        Dim start_index As Integer = Array.IndexOf(head_str, "Root P(A)") + area_count - 1

        line = sr.ReadLine
        ReDim node_result(RangeStr.Length, nodeView.Count)
        For i As Integer = 0 To node_count
            For j As Integer = 0 To area_count
                node_result(j, i) = 0
            Next
        Next
        Do
            If line <> "" Then
                Dim temp_array() As String = line.Split("	")
                If temp_array.Length > start_index Then
                    For n As Integer = 0 To node_count - 1
                        For m As Integer = 1 To area_count
                            If IsNumeric(temp_array(start_index + m + n * area_count)) Then
                                node_result(m, node_count - n) += temp_array(start_index + m + n * area_count)
                                node_result(0, node_count - n) += 1
                            End If
                        Next
                    Next
                End If
            End If
            line = sr.ReadLine
        Loop Until line Is Nothing
        sr.Close()
        For m As Integer = 1 To area_count
            For n As Integer = 1 To node_count
                If node_result(0, n) > 0 Then
                    node_result(m, n) = node_result(m, n) / node_result(0, n) * area_count
                End If
            Next
        Next
        Dim swf As StreamWriter
        swf = New StreamWriter(root_path + "temp" + path_char + "analysis_result.log", False) 
        swf.WriteLine("BayesTraits Results")
        swf.WriteLine("[TAXON]")
        For i As Integer = 1 To dtView.Count
            swf.WriteLine(dtView.Item(i - 1).Item(0).ToString + "	" + dtView.Item(i - 1).Item(1).ToString + "	" + dtView.Item(i - 1).Item(state_index).ToString.ToUpper)
        Next
        swf.WriteLine("[TREE]")
        swf.WriteLine("Tree=" + tree_show_with_value)
        swf.WriteLine("[RESULT]")
        If marg_ml <> 0 Then
            swf.WriteLine("Result of BayesTraits (Log marginal likelihood = " + marg_ml.ToString + "):")
        Else
            swf.WriteLine("Result of BayesTraits:")
        End If

        For i As Integer = 1 To node_count
            Dim t_list() As String = Poly_Node(i - 1, 3).Split(New Char() {","c})
            Dim result As String = "node " + (i + taxon_num).ToString + " (anc. of terminals " + t_list(0) + "-" + t_list(UBound(t_list)) + "):"
            Dim t_area() As Char
            Dim t_value() As Single
            ReDim t_area(RangeStr.Length - 1)
            ReDim t_value(RangeStr.Length - 1)
            For j As Integer = 0 To area_count - 1
                t_area(j) = ChrW(65 + j)
                t_value(j) = node_result(j + 1, i)
            Next
            Array.Sort(t_value, t_area)
            For j As Integer = 0 To area_count - 1
                result = result + " " + t_area(area_count - 1 - j).ToString + " " + (t_value(area_count - 1 - j) * 100).ToString("F2")
            Next
            swf.WriteLine(result)
        Next
        swf.Close()
        tree_view_title = "BayesTraits Result"
        StartTreeView = True
        Dim Tree_view_form As New View_Tree
        Tree_view_form.Show()
        Me.Hide()
    End Sub
    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex > 0 Then
            ComboBox4.SelectedIndex = 0
        End If
    End Sub

    Private Sub ComboBox4_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox4.SelectedIndexChanged
        If ComboBox4.SelectedIndex > 0 Then
            ComboBox3.SelectedIndex = 0
        End If
    End Sub

    Private Sub TimerTraits_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerTraits.Tick
        If File.Exists(traits_result_path + "endtraits") Then
            File.Delete(traits_result_path + "endtraits")
            TimerTraits.Enabled = False

            If File.Exists(traits_result_file) Then
                TraitsView.traits_view_file = traits_result_file
                TraitsView.Show()
                ProgressBar1.Value = 9500
                read_traits_file(traits_result_file)
                Hide()
            Else
                MsgBox("Please check your data and commands!")
            End If
            ProgressBar1.Value = 0
        Else
            If File.Exists(traits_result_file) Then
                If File.Exists(traits_result_path + "temp_count.txt") = False Then

                    File.Copy(traits_result_file, traits_result_path + "temp_count.txt")
                    Dim sr As New StreamReader(traits_result_path + "temp_count.txt")
                    Dim line As String
                    Dim conut_line As Integer = 0
                    Dim start_count As Boolean = False
                    If ComboBox1.SelectedIndex = 0 Then
                        Do
                            line = sr.ReadLine

                            If line Is Nothing Then
                                Exit Do
                            End If
                            If start_count Then
                                conut_line += 1
                            Else
                                If line.StartsWith("Tree No") Then
                                    start_count = True
                                End If
                            End If
                        Loop
                        sr.Close()
                        If MainWindow.CheckBox3.Checked Then
                            ProgressBar1.Value = Math.Min(10000, CInt(conut_line / CInt(MainWindow.RandomTextBox.Text) * 9000))
                        Else
                            ProgressBar1.Value = Math.Min(10000, CInt(conut_line / CInt(MainWindow.TreeBox_P.Text) * 9000))

                        End If
                        File.Delete(traits_result_path + "temp_count.txt")
                    Else
                        Do
                            line = sr.ReadLine

                            If line Is Nothing Then
                                Exit Do
                            End If
                            If start_count Then
                                conut_line += 1
                            Else
                                If line.StartsWith("Iteration") Then
                                    start_count = True
                                End If
                            End If
                        Loop
                        sr.Close()
                        ProgressBar1.Value = Math.Min(10000, CInt(conut_line / ((CInt(TextBox3.Text) - CInt(TextBox4.Text)) / CInt(TextBox1.Text)) * 9000))
                        File.Delete(traits_result_path + "temp_count.txt")
                    End If

                End If
            End If
        End If
    End Sub

    Private Sub LoadLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        RangeStr = ""
        For i As Integer = 1 To dtView.Count
            For Each c As Char In dtView.Item(i - 1).Item(state_index).ToString.ToUpper
                If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                    If RangeStr.Contains(c) = False Then
                        RangeStr = RangeStr + c.ToString
                    End If
                ElseIf dtView.Item(i - 1).Item(state_index).ToString <> "-" And dtView.Item(i - 1).Item(state_index).ToString <> "\" Then
                    MsgBox("Distributions of Taxon " + dtView.Item(i - 1).Item(0).ToString + " should be letters!")
                    Exit Sub
                    MsgBox("Only result of Multistate could be load into RASP!")
                End If
            Next
        Next
        If RangeStr.Length = 1 Then
            MsgBox("There should be two different areas at least!")
            MsgBox("Only result of Multistate could be load into RASP!")
            Exit Sub
        End If
        For Each c As Char In RangeStr.ToUpper
            If AscW(c) - AscW("A") + 1 > RangeStr.Length Then
                MsgBox("Distributions should be Continuous letters! Please alter area '" + c + "'.")
                MsgBox("Only result of Multistate could be load into RASP!")
                Exit Sub
            End If
        Next
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "Log Text File (*.log.txt)|*.log.txt|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".log.txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                read_traits_file(opendialog.FileName)
            End If
        End If
    End Sub

    Private Sub Config_Traits_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
End Class