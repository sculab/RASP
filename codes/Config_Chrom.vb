Imports System.IO
Public Class Config_Chrom
    Dim Distribution() As String
    Dim TaxonName() As String
    Dim TaxonTime(,) As String
    Dim taxon_array() As String
    Dim has_length As Boolean
    Dim Tree_Export_Char() As String
    Dim Chrom_tree As String

    Public Sub Read_Poly_Tree(ByVal Treeline As String)
        ReDim Poly_Node(taxon_num - 1 - 1, 9) '0 root,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点
        ReDim TaxonTime(taxon_num - 1, 2)
        has_length = False
        If Treeline.Contains(":") Then
            has_length = True
        End If
        ReDim taxon_array(taxon_num - 1)
        For i As Integer = 0 To taxon_num - 1 - 1
            Poly_Node(i, 0) = 0
            Poly_Node(i, 1) = ""
            Poly_Node(i, 2) = ""
            Poly_Node(i, 3) = ""
            Poly_Node(i, 6) = "1.00"
            Poly_Node(i, 7) = "0"
            Poly_Node(i, 8) = "0"
        Next
        Dim tree_char() As String
        ReDim tree_char(taxon_num * 7)
        Dim char_id As Integer = 0
        Dim l_c As Integer = 0
        Dim r_c As Integer = 0
        Dim tx As Integer = 0
        Dim last_symb As Boolean = True
        For Each i As Char In Treeline
            Select Case i
                Case "("
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True

                Case ")"
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True
                Case ","
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True
                Case Else
                    If last_symb Then
                        char_id += 1
                        tree_char(char_id) = i
                        last_symb = False
                    Else
                        tree_char(char_id) += i
                    End If
            End Select
        Next
        ReDim Tree_Export_Char(char_id)
        For i As Integer = 1 To char_id
            Tree_Export_Char(i) = tree_char(i)
        Next
        Dim tree_node As Integer = 0
        If has_length Then
            For i As Integer = 1 To char_id
                If Tree_Export_Char(i).Contains(":") Then
                    If Tree_Export_Char(i - 1) <> ")" Then
                        TaxonTime(CInt(tree_char(i).Split(New Char() {":"c})(0)) - 1, 0) = tree_char(i).Split(New Char() {":"c})(1)
                    Else
                        tree_node += 1
                        tree_char(i) = ":" + tree_char(i).Split(New Char() {":"c})(1)
                    End If
                End If
                If Tree_Export_Char(i).Contains(";") Then
                    tree_char(i) = ";"
                End If
            Next
        End If

        Chrom_tree = ""
        For i As Integer = 1 To char_id
            Chrom_tree += tree_char(i)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Read_Poly_Tree(tree_show_with_value)
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ""
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Dim f_name As String = opendialog.FileName.Substring(opendialog.FileName.LastIndexOf("\") + 1)
            Dim f_path As String = opendialog.FileName.Substring(0, opendialog.FileName.Length - f_name.Length)
            Dim dw As New StreamWriter(opendialog.FileName + ".dat", False)
            For i As Integer = 1 To dtView.Count
                dtView.Item(i - 1).Item(state_index) = dtView.Item(i - 1).Item(state_index).ToString.Replace(" ", "")
                If dtView.Item(i - 1).Item(state_index) = "" Or dtView.Item(i - 1).Item(state_index) = "\" Then
                    dw.WriteLine(">" + dtView.Item(i - 1).Item(0).ToString + vbCrLf + "X")
                Else
                    dw.WriteLine(">" + dtView.Item(i - 1).Item(0).ToString + vbCrLf + dtView.Item(i - 1).Item(state_index))
                End If
            Next
            dw.Close()
            Dim dw1 As New StreamWriter(opendialog.FileName + ".tree", False)
            dw1.WriteLine(Chrom_tree)
            dw1.Close()
            dw.Close()
            Dim dw2 As New StreamWriter(opendialog.FileName + ".cfg", False)
            dw2.WriteLine("_mainType " + ComboBox1.Text)
            dw2.WriteLine("_outDir " + f_name)
            dw2.WriteLine("_dataFile " + f_name + ".dat")
            dw2.WriteLine("_treeFile " + f_name + ".tree")
            dw2.WriteLine("_maxChrNum " + TextBox1.Text)
            dw2.WriteLine("_minChrNum " + TextBox2.Text)
            dw2.WriteLine("_simulationsNum " + TextBox3.Text)
            dw2.WriteLine("_baseNumberR " + TextBox6.Text)
            dw2.WriteLine("_baseNumber " + TextBox7.Text)
            dw2.WriteLine("_bOptBaseNumber " + TextBox8.Text)
            If CheckBox1.Checked Then
                dw2.WriteLine(TextBox4.Text)
            End If
            dw2.Close()

            TextBox5.Text = "Use following command to run ChromEvol:" + vbCrLf _
                + "chromEvol.exe .\" + f_name + ".cfg" + vbCrLf _
                + "Results will be saved in: " + vbCrLf + opendialog.FileName + vbCrLf _
                + "Look over models_summary.txt to find the model with minimal AIC" + vbCrLf _
             + "You could use 'File->Translate result' to load result into RASP."
            If File.Exists(f_path + "chromEvol.exe") Then
                Dim msg_reslut As DialogResult = MsgBox("Find ChromEvol. Run it now?", MsgBoxStyle.YesNo)
                If msg_reslut = Windows.Forms.DialogResult.Yes Then
                    Dim dw3 As New StreamWriter(opendialog.FileName + ".bat", False)
                    dw3.WriteLine("chromEvol.exe .\" + f_name + ".cfg")
                    dw3.WriteLine("pause")
                    dw3.WriteLine("del " + f_name + ".bat /Q")
                    dw3.Close()
                    Dim startInfo As New ProcessStartInfo
                    startInfo.FileName = f_name + ".bat"
                    startInfo.WorkingDirectory = f_path
                    Process.Start(startInfo)
                End If
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub Config_Chrom_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub TextBox1_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox1.MouseDown
        TextBox5.Text = "The maximal chromosome number allowed. Negative values (-X): Set the maximal chromosome number allowed to be X units larger than the maximal chromosome number observed in the data file."
    End Sub

    Private Sub TextBox2_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox2.MouseDown
        TextBox5.Text = "The minimal chromosome number allowed. Negative values (-X): Set the minimal chromosome number allowed to be X units smaller than the minimal chromosome number observed in the data file."
    End Sub

    Private Sub TextBox3_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox3.MouseDown
        TextBox5.Text = "The number of simulations for computing the expectation of the number of changes of certain transition type along each branch. Note: This step is computationally extensive. Lower values results in faster computations with decreased accuracy."
    End Sub

    Private Sub FormatTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles FormatTreeToolStripMenuItem.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "tree file|*.tree|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = "*.tree"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Dim f_name As String = opendialog.FileName.Substring(opendialog.FileName.LastIndexOf("\") + 1)
            Dim f_path As String = opendialog.FileName.Substring(0, opendialog.FileName.Length - f_name.Length)
            Dim tree_line As String
            Dim sr As New StreamReader(opendialog.FileName, False)
            tree_line = sr.ReadToEnd
            sr.Close()
            Translate_tree(tree_line, opendialog.FileName + "_Translated.tree")
            TextBox5.Text = "Tree file saved to:" + vbCrLf + opendialog.FileName + "_Translated.tree"
        End If

    End Sub
    Public Sub Translate_tree(ByVal tree_line As String, ByVal save_path As String)
        Dim sw As New StreamWriter(save_path, False)
        sw.WriteLine("#NEXUS")
        sw.WriteLine("Begin trees;")
        sw.WriteLine("   Translate")
        For i As Integer = 1 To dtView.Count - 1
            sw.WriteLine(dtView.Item(i - 1).Item(0).ToString + " " + dtView.Item(i - 1).Item(1).ToString + ",")
        Next
        sw.WriteLine(dtView.Item(dtView.Count - 1).Item(0).ToString + " " + dtView.Item(dtView.Count - 1).Item(1).ToString)
        sw.WriteLine("		;")


        For i As Integer = 1 To dtView.Count
            tree_line = tree_line.Replace("(" + dtView.Item(i - 1).Item(0).ToString + "-" + dtView.Item(i - 1).Item(state_index).ToString + ",", "($%*" + dtView.Item(i - 1).Item(0).ToString + "$%*,")
            tree_line = tree_line.Replace("," + dtView.Item(i - 1).Item(0).ToString + "-" + dtView.Item(i - 1).Item(state_index).ToString + ")", ",$%*" + dtView.Item(i - 1).Item(0).ToString + "$%*)")
            tree_line = tree_line.Replace("," + dtView.Item(i - 1).Item(0).ToString + "-" + dtView.Item(i - 1).Item(state_index).ToString + ",", ",$%*" + dtView.Item(i - 1).Item(0).ToString + "$%*,")
            tree_line = tree_line.Replace("," + dtView.Item(i - 1).Item(0).ToString + "-" + dtView.Item(i - 1).Item(state_index).ToString + ":", ",$%*" + dtView.Item(i - 1).Item(0).ToString + "$%*:")
            tree_line = tree_line.Replace("(" + dtView.Item(i - 1).Item(0).ToString + "-" + dtView.Item(i - 1).Item(state_index).ToString + ":", "($%*" + dtView.Item(i - 1).Item(0).ToString + "$%*:")
        Next
        If tree_line.Contains("/") = False Then
            For i As Integer = 1 To dtView.Count - 1
                tree_line = tree_line.Replace("[N" + i.ToString + "-", "")
            Next
            tree_line = tree_line.Replace("]", "")
        End If
        tree_line = tree_line.Replace("$%*", "")
        sw.WriteLine("tree con = [&R] " + tree_line)
        sw.WriteLine("End;")
        sw.Close()
    End Sub

    Public Sub read_ChromEvol(ByVal file_path As String)
        Dim Treeline As String

        Treeline = tree_show_with_value.Replace(";", "")

        Dim NumofTaxon As Integer = Treeline.Length - Treeline.Replace(",", "").Length + 1
        Dim NumofNode As Integer = Treeline.Length - Treeline.Replace("(", "").Length

        Dim result_f() As String
        ReDim result_f(NumofNode - 1)
        Dim line As String
        Dim info_text As String = ""
        If File.Exists(file_path + "expectations.txt") Then
            Dim sr_exp As New StreamReader(file_path + "expectations.txt", False)
            info_text = sr_exp.ReadToEnd
            For i As Integer = 1 To NumofNode
                info_text = info_text.Replace("N" + i.ToString + ":", "$Node " + Left_to_right(i + NumofTaxon, Treeline).ToString + ":$")
                info_text = info_text.Replace("N" + i.ToString + "	", "$Node " + Left_to_right(i + NumofTaxon, Treeline).ToString + "	$")
            Next
            info_text = info_text.Replace("$", "")
            sr_exp.Close()
        End If

        Dim sr As New StreamReader(file_path + "ancestorsProbs.txt", False)
        line = sr.ReadLine
        line = sr.ReadLine
        Do
            Dim temp() As String = line.Split("	")
            If temp.Length > 1 Then
                If temp(0).StartsWith("N") Then
                    Dim temp_node_id As Integer = CInt(temp(0).Replace("N", "")) + NumofTaxon
                    temp_node_id = Left_to_right(temp_node_id, Treeline)
                    Dim temp_id(0) As Integer
                    Dim temp_prob(0) As Single
                    For i As Integer = 1 To UBound(temp)
                        If IsNumeric(temp(i)) Then
                            ReDim Preserve temp_id(i - 1)
                            ReDim Preserve temp_prob(i - 1)
                            temp_id(i - 1) = i
                            temp_prob(i - 1) = temp(i)
                        End If
                    Next
                    Array.Sort(temp_prob, temp_id, New scomparer)
                    Dim t_list() As String = Poly_Node(temp_node_id - NumofTaxon - 1, 3).Split(New Char() {","c})
                    result_f(temp_node_id - NumofTaxon - 1) = "node " + temp_node_id.ToString + " (anc. of terminals " + t_list(0) + "-" + t_list(UBound(t_list)) + "):"
                    For k As Integer = 0 To UBound(temp_prob)
                        result_f(temp_node_id - NumofTaxon - 1) += " " + temp_id(k).ToString + " " + (temp_prob(k) * 100).ToString("F2")
                    Next
                End If
            End If
            line = sr.ReadLine
        Loop Until line Is Nothing
        sr.Close()

        Dim swf As StreamWriter
        swf = New StreamWriter(root_path + "temp" + path_char + "analysis_result.log", False)
        swf.WriteLine("ChromEvol Result file (Number)")
        swf.WriteLine("[TAXON]")
        For i As Integer = 1 To dtView.Count
            swf.WriteLine(dtView.Item(i - 1).Item(0).ToString + "	" + dtView.Item(i - 1).Item(1).ToString + "	" + dtView.Item(i - 1).Item(state_index).ToString.ToUpper)
        Next
        swf.WriteLine("[TREE]")
        swf.WriteLine("Tree=" + tree_show_with_value)
        swf.WriteLine("[RESULT]")
        If best_model <> "" Then
            swf.WriteLine("Result of ChromEvol (" + best_model + " model, AIC=" + best_AIC(1).ToString("F2") + "):")
        Else
            swf.WriteLine("Result of ChromEvol:")
        End If

        For Each i As String In result_f
            swf.WriteLine(i)
        Next
        If info_text <> "" Then
            swf.WriteLine("[SUPPLEMENT]")
            swf.Write(info_text)
            swf.WriteLine("[END]")
        End If
        swf.Close()
        Me.Hide()
        tree_view_title = "ChromEvol Result"
        StartTreeView = True
        Dim Tree_view_form As New View_Tree
        Tree_view_form.Show()
    End Sub

    Private Sub TranslateResultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TranslateResultToolStripMenuItem.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "ancestorsProbs file|ancestorsProbs.txt|ALL Files(*.*)|*.*"
        opendialog.FileName = "ancestorsProbs.txt"
        opendialog.DefaultExt = "ancestorsProbs.txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            best_model=""
            read_ChromEvol(opendialog.FileName.Replace("ancestorsProbs.txt", ""))
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        'If ComboBox1.Text <> "All_Models" Then
        '    MsgBox("Only 'All_Models' is supported by auto run!")
        '    Exit Sub
        'End If
        Dim f_name As String = "ChromEvolResult"
        Dim f_path As String = root_path + "Plug-ins" + path_char
        Read_Poly_Tree(tree_show_with_value)
        Try
            If My.Computer.FileSystem.DirectoryExists(f_path + f_name) Then
                My.Computer.FileSystem.DeleteDirectory(f_path + f_name, FileIO.DeleteDirectoryOption.DeleteAllContents)
            End If
        Catch ex As Exception
            MsgBox("Please stop previous analysis!")
            Exit Sub
        End Try
        Dim dw As New StreamWriter(f_path + f_name + ".dat", False)
        For i As Integer = 1 To dtView.Count
            dtView.Item(i - 1).Item(state_index) = dtView.Item(i - 1).Item(state_index).ToString.Replace(" ", "")
            If dtView.Item(i - 1).Item(state_index) = "" Or dtView.Item(i - 1).Item(state_index) = "\" Then
                dw.WriteLine(">" + dtView.Item(i - 1).Item(0).ToString + vbCrLf + "X")
            Else
                dw.WriteLine(">" + dtView.Item(i - 1).Item(0).ToString + vbCrLf + dtView.Item(i - 1).Item(state_index).ToString.ToUpper)
            End If
        Next
        dw.Close()
        Dim dw1 As New StreamWriter(f_path + f_name + ".tree", False)
        dw1.WriteLine(Chrom_tree)
        dw1.Close()
        dw.Close()
        Dim dw2 As New StreamWriter(f_path + f_name + ".cfg", False)
        dw2.WriteLine("_mainType " + ComboBox1.Text)
        dw2.WriteLine("_outDir " + f_name)
        dw2.WriteLine("_dataFile " + f_name + ".dat")
        dw2.WriteLine("_treeFile " + f_name + ".tree")
        dw2.WriteLine("_maxChrNum " + TextBox1.Text)
        dw2.WriteLine("_minChrNum " + TextBox2.Text)
        If TextBox3.Text <> "" Then
            dw2.WriteLine("_simulationsNum " + TextBox3.Text)
        End If
        If TextBox6.Text <> "" Then
            dw2.WriteLine("_baseNumberR " + TextBox6.Text)
        End If
        If TextBox7.Text <> "" Then
            dw2.WriteLine("_baseNumber " + TextBox7.Text)
        End If
        If TextBox8.Text <> "" Then
            dw2.WriteLine("_bOptBaseNumber " + TextBox8.Text)
        End If
        If CheckBox1.Checked Then
            dw2.WriteLine(TextBox4.Text)
        End If
        dw2.Close()

        TextBox5.Text = "Use following command to run ChromEvol:" + vbCrLf _
            + "chromEvol.exe .\" + f_name + ".cfg" + vbCrLf _
            + "Results will be saved in: " + vbCrLf + f_path + f_name
        If File.Exists(f_path + "chromEvol.exe") Then
            If File.Exists(f_path + "endtchromevol") Then
                File.Delete(f_path + "endtchromevol")
            End If
            If File.Exists(f_path + f_name + path_char + "models_summary.txt") Then
                File.Delete(f_path + f_name + path_char + "models_summary.txt")
            End If
            Dim dw3 As New StreamWriter(f_path + f_name + ".bat", False)
            dw3.WriteLine("chromEvol.exe .\" + f_name + ".cfg")
            dw3.WriteLine("cls>endtchromevol")
            dw3.Close()
            Dim startInfo As New ProcessStartInfo
            If TargetOS = "macos" Then
                startInfo.FileName = "wineconsole"
                startInfo.WorkingDirectory = f_path
                startInfo.Arguments = f_name + ".bat"
            Else
                startInfo.FileName = f_name + ".bat"
                startInfo.WorkingDirectory = f_path
            End If
            Process.Start(startInfo)
            ComboBox1.Enabled = False
            TimerChromEvol.Enabled = True
            ChromEvol_result_path = f_path
            ChromEvol_result_name = f_name

        End If
    End Sub
    Dim ChromEvol_result_path As String
    Dim ChromEvol_result_name As String
    Dim best_model As String
    Dim best_AIC(1) As Single
    Private Sub TimerChromEvol_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerChromEvol.Tick
        If File.Exists(ChromEvol_result_path + "endtchromevol") Then
            File.Delete(ChromEvol_result_path + "endtchromevol")
            TimerChromEvol.Enabled = False
            ComboBox1.Enabled = True
            If ComboBox1.Text = "All_Models" Then
                If File.Exists(ChromEvol_result_path + ChromEvol_result_name + path_char + "models_summary.txt") Then
                    Dim sr As New StreamReader(ChromEvol_result_path + ChromEvol_result_name + path_char + "models_summary.txt")
                    Dim line As String = sr.ReadLine
                    line = sr.ReadLine
                    best_AIC(1) = 1.0E+15
                    best_model = ""
                    Do
                        If line <> "" Then
                            Dim temp() As String = line.Split("	")
                            If temp.Length > 1 Then
                                If CSng(temp(2)) < best_AIC(1) Then
                                    best_AIC(0) = CSng(temp(1))
                                    best_AIC(1) = CSng(temp(2))
                                    best_model = temp(0)
                                End If
                            End If
                        End If
                        line = sr.ReadLine
                    Loop Until line Is Nothing
                    If best_model <> "" Then
                        read_ChromEvol(ChromEvol_result_path + ChromEvol_result_name + path_char + best_model + path_char)
                    Else
                        MsgBox("Please check your data and commands and try again!")
                    End If
                Else
                    MsgBox("Please check your data and commands and try again!")
                End If
            Else
                best_model = ""
                If File.Exists(ChromEvol_result_path + ChromEvol_result_name + path_char + "ancestorsProbs.txt") Then
                    read_ChromEvol(ChromEvol_result_path + ChromEvol_result_name + path_char)
                End If
            End If

        End If
    End Sub

    Private Sub Config_Chrom_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If TimerChromEvol.Enabled = True Then
            Dim msg_reslut As DialogResult = MsgBox("ChromEvol is still running, stop it?", MsgBoxStyle.YesNo)
            If msg_reslut = Windows.Forms.DialogResult.Yes Then
                TimerChromEvol.Enabled = False
                ComboBox1.Enabled = True
            End If
        End If
    End Sub

    Private Sub TextBox7_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox7.MouseDown
        TextBox5.Text = "A specified chromosome number which characterizes a phylogenetic group. This is NOT the chromosome number at the root of the phylogeny."
    End Sub

    Private Sub TextBox6_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox6.MouseDown
        TextBox5.Text = "Rate for transitions by base number."
    End Sub

    Private Sub TextBox8_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles TextBox8.MouseDown
        TextBox5.Text = "Defines whether the base-number is optimized by the program or not. Set this parameter to 1 in order to optimize, and to 0 in order to keep base-number fixed to the value given by Base Number (recommended)"
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        TextBox4.ReadOnly = CheckBox1.Checked Xor True
    End Sub
End Class