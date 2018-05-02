Imports System.IO
Public Class Config_Traits

    Dim commandlines(2048) As String
    Dim Select_Node_Num As Integer
    Dim Tree_Node_Num As Integer
    Dim traits_result_path, traits_result_file As String
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim isselectnode As Boolean = False
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
        For i As Integer = 0 To 2048
            commandlines(i) = ""
        Next

        commandlines(0) = ComboBox6.SelectedIndex + 1
        commandlines(100) = ComboBox1.SelectedIndex + 1

        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                commandlines(200 + nodeView.Count - i) = "AddNode Node" + (nodeView.Count - i + 1).ToString + " " + Poly_Node((nodeView.Count - i), 3).Replace(",", " ")
            End If
            If DataGridView2.Rows(i - 1).Cells(3).FormattedValue.ToString <> "" Then
                commandlines(1000 + i) = "Fossil Node" + i.ToString + " " + DataGridView2.Rows(i - 1).Cells(3).FormattedValue.ToString.ToUpper + " " + Poly_Node(i - 1, 3).Replace(",", " ")
            End If
        Next
        commandlines(2000) = "Sample " + TextBox1.Text
        commandlines(2001) = "Iterations " + TextBox3.Text
        commandlines(2002) = "BurnIn " + TextBox4.Text
        commandlines(2003) = "MLTries " + TextBox6.Text
        commandlines(2004) = ComboBox2.Text
        commandlines(2005) = ComboBox3.Text
        commandlines(2006) = ComboBox5.Text
        commandlines(2007) = ComboBox4.Text
        commandlines(2008) = TextBox2.Text.Replace(Chr(13), vbLf)
        commandlines(2048) = "run"
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ""
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            export_omitted(opendialog.FileName + ".trees", root_path + "temp" + path_char + "lg_t2.tre")
            Dim dw As New StreamWriter(opendialog.FileName + ".dat", False)
            For i As Integer = 1 To dtView.Count
                dtView.Item(i - 1).Item(2) = dtView.Item(i - 1).Item(2).ToString.Replace(" ", "")
                If dtView.Item(i - 1).Item(2) = "" Or dtView.Item(i - 1).Item(2) = "\" Then
                    dw.WriteLine(dtView.Item(i - 1).Item(1) + "	" + "-")
                Else
                    dw.WriteLine(dtView.Item(i - 1).Item(1) + "	" + dtView.Item(i - 1).Item(2))
                End If
            Next
            dw.Close()
            Dim dw1 As New StreamWriter(opendialog.FileName + ".ini", False)
            For i As Integer = 0 To 2048
                If commandlines(i) <> "" Then
                        dw1.Write(commandlines(i) + vbLf)
                End If
            Next
            dw1.Close()
            Dim f_name As String = opendialog.FileName.Substring(opendialog.FileName.LastIndexOf("\") + 1)
            Dim f_path As String = opendialog.FileName.Substring(0, opendialog.FileName.Length - f_name.Length)


            'Dim res_dialog As DialogResult = MsgBox("Do you want to use BayesTraitsV2.0.2 in RASP?", MsgBoxStyle.YesNo)
            'If res_dialog = Windows.Forms.DialogResult.Yes Then
            'End If
            TextBox5.Text = "Use following command to run BayesTraits:" + vbCrLf + "Mac OS:" + vbCrLf _
                + "./BayesTraitsV2 " + f_name + ".trees " + f_name + ".dat < " + f_name + ".ini" + vbCrLf + "Windows:" + vbCrLf _
                + "BayesTraitsV2.exe " + f_name + ".trees " + f_name + ".dat < " + f_name + ".ini"
            If TargetOS = "win32" Then
                Dim msg_reslut As DialogResult = MsgBox("Do you want to run BayesTraitsV2.0.2 automatically?", MsgBoxStyle.YesNo)
                If msg_reslut = Windows.Forms.DialogResult.Yes Then
                    File.Copy(root_path + "Plug-ins\BayesTraitsV2.0.2.exe", f_path + "BayesTraitsV2.exe", True)
                    If File.Exists(f_path + "endtraits") Then
                        File.Delete(f_path + "endtraits")
                    End If
                    If File.Exists(opendialog.FileName + ".dat.log.txt") Then
                        File.Delete(opendialog.FileName + ".dat.log.txt")
                    End If
                    If File.Exists(opendialog.FileName + ".dat.log.txt.Stones.txt") Then
                        File.Delete(opendialog.FileName + ".dat.log.txt.Stones.txt")
                    End If
                    Dim dw2 As New StreamWriter(opendialog.FileName + ".bat", False)
                    dw2.WriteLine("BayesTraitsV2.exe " + f_name + ".trees " + f_name + ".dat < " + f_name + ".ini")
                    'dw2.WriteLine("pause")
                    dw2.WriteLine("cls>endtraits")
                    dw2.Close()
                    Dim startInfo As New ProcessStartInfo
                    startInfo.FileName = f_name + ".bat"
                    startInfo.WorkingDirectory = f_path
                    Process.Start(startInfo)
                    traits_result_path = f_path
                    traits_result_file = opendialog.FileName + ".dat.log.txt"
                    TimerTraits.Enabled = True
                End If
            End If
        End If
    End Sub

    Public Sub export_omitted(ByVal export_file_name As String, ByVal source_file_name As String)
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
        Dim rt As New StreamReader(source_file_name)
        Dim line As String
        Dim o_num As Integer = 0
        Dim tree_num As Integer = 0
        For i As Integer = 1 To CInt(MainWindow.BurninBox.Text)
            line = rt.ReadLine
            tree_num = tree_num + 1
        Next
        line = rt.ReadLine
        Do
            wt.WriteLine("tree NEWTREE_" + o_num.ToString + " = " + line)
            line = rt.ReadLine
            tree_num = tree_num + 1
        Loop Until line Is Nothing
        wt.WriteLine("End;")
        rt.Close()
        wt.Close()
    End Sub

    Private Sub Bayestraits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 1
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
                DataGridView2.Columns(1).Width = 125
                DataGridView2.Columns(2).Width = 50
                DataGridView2.Columns(3).Width = 75
                DataGridView2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(3).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(0).ReadOnly = True
                DataGridView2.Columns(1).ReadOnly = True
                DataGridView2.Columns(2).ReadOnly = True
                DataGridView2.Columns(2).Visible = True

                For i As Integer = 1 To DataGridView2.Rows.Count
                    DataGridView2.Rows(i - 1).Cells(2).Value = True
                Next
            End If
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
            swf.WriteLine(dtView.Item(i - 1).Item(0).ToString + "	" + dtView.Item(i - 1).Item(1).ToString + "	" + dtView.Item(i - 1).Item(2).ToString.ToUpper)
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
            If File.Exists(traits_result_path + "BayesTraitsV2.exe") Then
                File.Delete(traits_result_path + "BayesTraitsV2.exe")
            End If
            TimerTraits.Enabled = False
            RangeStr = ""
            For i As Integer = 1 To dtView.Count
                For Each c As Char In dtView.Item(i - 1).Item(2).ToString.ToUpper
                    If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                        If RangeStr.Contains(c) = False Then
                            RangeStr = RangeStr + c.ToString
                        End If
                    ElseIf dtView.Item(i - 1).Item(2).ToString <> "-" And dtView.Item(i - 1).Item(2).ToString <> "\" Then
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
            If File.Exists(traits_result_file) Then
                read_traits_file(traits_result_file)
            Else
                MsgBox("Please check your data and commands and try again!")
            End If
        End If
    End Sub

    Private Sub LoadLogToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadLogToolStripMenuItem.Click
        RangeStr = ""
        For i As Integer = 1 To dtView.Count
            For Each c As Char In dtView.Item(i - 1).Item(2).ToString.ToUpper
                If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                    If RangeStr.Contains(c) = False Then
                        RangeStr = RangeStr + c.ToString
                    End If
                ElseIf dtView.Item(i - 1).Item(2).ToString <> "-" And dtView.Item(i - 1).Item(2).ToString <> "\" Then
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
End Class