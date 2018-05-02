Imports System.IO
Imports System.Threading
Public Class Tool_Combine
    Dim TaxonTime(,) As String
    Dim NumofNode As Integer
    Dim NumofTaxon As Integer
    Dim Tree_Export_Char() As String
    Dim Combine_Node(,) As String
    Dim maxtime As Single
    Public Sub get_tree_length(ByVal Treeline As String)
        Dim Combine_Node_row(,) As Single
        Dim Poly_terminal_xy(,) As Single
        Dim Combine_Node_col(,) As Single
        Dim taxon_array() As String
        ReDim Combine_Node(NumofNode - 1, 9) '0 root,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点
        ReDim Combine_Node_row(NumofNode - 1, 2)
        ReDim Combine_Node_col(NumofNode - 1, 2)
        ReDim Poly_terminal_xy(NumofTaxon - 1, 2)
        ReDim TaxonTime(NumofTaxon - 1, 2)
        ReDim taxon_array(NumofTaxon - 1)
        For i As Integer = 0 To NumofNode - 1
            Combine_Node(i, 0) = 0
            Combine_Node(i, 1) = ""
            Combine_Node(i, 2) = ""
            Combine_Node(i, 3) = ""
            Combine_Node(i, 6) = "1.00"
            Combine_Node(i, 7) = "0"
            Combine_Node(i, 8) = "0"
        Next
        Dim tree_char() As String
        ReDim tree_char(NumofTaxon * 7)
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

        For i As Integer = 1 To char_id
            If Tree_Export_Char(i).Contains(":") Then
                If Tree_Export_Char(i - 1) <> ")" Then
                    '物种
                    TaxonTime(CInt(tree_char(i).Split(New Char() {":"c})(0)) - 1, 0) = tree_char(i).Split(New Char() {":"c})(1)
                    tree_char(i) = tree_char(i).Split(New Char() {":"c})(0)
                End If
            End If
        Next

        Dim point_1, point_2 As Integer
        point_1 = 0
        point_2 = 0
        Dim Temp_node(,) As String
        ReDim Temp_node(NumofNode, 6) '0 节点位置,1 末端, 2 子节点, 4 左侧个数, 5 右侧个数, 6 支持率
        For i As Integer = 0 To NumofNode - 1
            Temp_node(i, 0) = ""
            Temp_node(i, 1) = ""
            Temp_node(i, 2) = ""
            Temp_node(i, 4) = "32768"
            Temp_node(i, 5) = "0"
            Temp_node(i, 6) = "1"
        Next
        For i As Integer = 1 To char_id
            Select Case tree_char(i)
                Case "("
                    l_c += 1
                    Temp_node(point_1, 0) = i
                    point_1 += 1
                Case ")"
                    r_c += 1
                    Combine_Node(point_2, 1) = Temp_node(point_1 - 1, 1)
                    Combine_Node(point_2, 2) = Temp_node(point_1 - 1, 2)
                    Combine_Node(point_2, 4) = Temp_node(point_1 - 1, 4)
                    Combine_Node(point_2, 5) = Temp_node(point_1 - 1, 5)
                    For j As Integer = Temp_node(point_1 - 1, 0) To i
                        If tree_char(j) <> "(" And tree_char(j) <> ")" Then
                            If tree_char(j) <> "," Then
                                If tree_char(j - 1) <> ")" Then
                                    Combine_Node(point_2, 3) += tree_char(j)
                                End If
                            Else
                                Combine_Node(point_2, 3) += tree_char(j)
                            End If
                        End If
                    Next
                    If point_1 > 1 Then
                        Temp_node(point_1 - 2, 2) = point_2.ToString + "," + Temp_node(point_1 - 2, 2)
                        Temp_node(point_1 - 2, 4) = min(Val(Temp_node(point_1 - 2, 4)), (Val(Combine_Node(point_2, 5)) + Val(Combine_Node(point_2, 4))) / 2)
                        Temp_node(point_1 - 2, 5) = max(Val(Temp_node(point_1 - 2, 5)), (Val(Combine_Node(point_2, 5)) + Val(Combine_Node(point_2, 4))) / 2)
                    End If
                    point_2 += 1
                    point_1 -= 1
                    Temp_node(point_1, 0) = ""
                    Temp_node(point_1, 1) = ""
                    Temp_node(point_1, 2) = ""
                    Temp_node(point_1, 4) = "32768"
                    Temp_node(point_1, 5) = "0"
                Case ","

                Case Else
                    If tree_char(i - 1) = ")" Then
                        '读取支持率
                        If tree_char(i).Contains(":") Then
                            If Val(tree_char(i).Split(New Char() {":"c})(0)) > 1 Then
                                Combine_Node(point_2 - 1, 6) = (Val(tree_char(i)) / 100).ToString("F2")
                            Else
                                Combine_Node(point_2 - 1, 6) = Val(tree_char(i)).ToString("F2")
                            End If
                            Combine_Node(point_2 - 1, 7) = tree_char(i).Split(New Char() {":"c})(1)
                        Else
                            If Val(tree_char(i)) > 1 Then
                                Combine_Node(point_2 - 1, 6) = (Val(tree_char(i)) / 100).ToString("F2")
                            Else
                                Combine_Node(point_2 - 1, 6) = Val(tree_char(i)).ToString("F2")
                            End If
                        End If

                    Else
                        taxon_array(tx) = tree_char(i)
                        tx += 1
                        Temp_node(point_1 - 1, 1) += tree_char(i) + ","
                        Temp_node(point_1 - 1, 4) = min(Val(Temp_node(point_1 - 1, 4)), tx)
                        Temp_node(point_1 - 1, 5) = max(Val(Temp_node(point_1 - 1, 4)), tx)
                    End If
            End Select
        Next
        make_chain(NumofNode - 1)
        maxtime = 0
        For i As Integer = 0 To NumofTaxon - 1
            If maxtime < Val(TaxonTime(i, 1)) Then
                maxtime = Val(TaxonTime(i, 1))
            End If
        Next
    End Sub

    Public Sub make_chain(ByVal n As Integer)
        If Combine_Node(n, 2) <> "" Then
            Dim anc_node() As String = Combine_Node(n, 2).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    Combine_Node(CInt(j), 8) = (Val(Combine_Node(CInt(j), 7)) + Val(Combine_Node(n, 8))).ToString
                    make_chain(CInt(j))
                End If
            Next
        End If
        If Combine_Node(n, 1) <> "" Then
            Dim anc_node() As String = Combine_Node(n, 1).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    TaxonTime(CInt(j) - 1, 1) = (Val(TaxonTime(CInt(j) - 1, 0)) + Val(Combine_Node(n, 8))).ToString
                End If
            Next
        End If
    End Sub
    Public Sub combine_result()
        Dim final_result_list() As String
        Dim final_p_list() As Integer
        Dim final_value_list() As Single
        Dim final_prob_list(,) As Single
        Dim node_line() As String
        Dim TaxonName() As String
        Dim TaxonDis() As String

        Dim D_id() As Integer
        Dim Treeline, Tree_first As String
        Dim file_id As Integer = 1
        Dim temp_id As Integer = 0
        Dim TempName() As String
        Dim sr_tree As StreamReader = New StreamReader(ListBox1.Items(0).ToString)
        Dim line As String = ""
        Dim AreaStr As String = ""
        Dim has_prob As Boolean = False
        Do
            line = sr_tree.ReadLine
        Loop Until line.StartsWith("[TREE]")
        Tree_first = sr_tree.ReadLine.Split(New Char() {"="c})(1).Replace(";", "")
        NumofTaxon = Tree_first.Length - Tree_first.Replace(",", "").Length + 1
        NumofNode = Tree_first.Length - Tree_first.Replace("(", "").Length
        get_tree_length(Tree_first)

        ReDim node_line(NumofNode - 1)
        ReDim final_result_list(NumofNode - 1)
        ReDim final_p_list(NumofNode - 1)
        ReDim final_value_list(NumofNode - 1)
        For i As Integer = 0 To NumofNode - 1
            node_line(i) = ""
            final_result_list(i) = " "
            final_p_list(i) = 0
            final_value_list(i) = 0
            node_line(i) = "#"
            Dim temp_array() As String = Combine_Node(i, 3).Split(New Char() {","c})
            Array.Sort(temp_array)
            For Each j As String In temp_array
                If j <> "" Then
                    node_line(i) += j + "#"
                End If
            Next
        Next
        sr_tree.Close()

        Dim sr_taxon As StreamReader = New StreamReader(ListBox1.Items(0).ToString)
        ReDim TaxonName(NumofTaxon - 1)
        ReDim TaxonDis(NumofTaxon - 1)
        Do
            line = sr_taxon.ReadLine
        Loop Until line.StartsWith("[TAXON]")
        For i As Integer = 0 To NumofTaxon - 1
            line = sr_taxon.ReadLine
            TaxonName(i) = line.Split(New Char() {"	"c})(1)
            TaxonDis(i) = line.Split(New Char() {"	"c})(2)
        Next
        Do
            If line.StartsWith("[PROBABILITY]") Then
                has_prob = True
            End If
            line = sr_taxon.ReadLine
        Loop Until line = ""
        sr_taxon.Close()
        For i As Integer = 1 To NumofTaxon
            For Each c As Char In TaxonDis(i - 1)
                If Asc(c) >= Asc("A") And Asc(c) <= Asc("Z") Then
                    If AreaStr.Contains(c) = False Then
                        AreaStr = AreaStr + c.ToString
                    End If
                End If
            Next
        Next
        ReDim final_prob_list(NumofNode - 1, AreaStr.Length * 2)
        For temp_id = 0 To NumofNode - 1
            For k As Integer = 1 To AreaStr.Length * 2
                final_prob_list(temp_id, k) = 0
            Next
        Next
        For f As Integer = 1 To ListBox1.Items.Count
            curr_num = f
            Dim file_path As String = ListBox1.Items(f - 1).ToString
            If has_prob Then
                has_prob = False
                Dim sr_prob As StreamReader = New StreamReader(file_path)
                line = sr_prob.ReadLine
                Do
                    If line.StartsWith("[PROBABILITY]") Then
                        has_prob = True
                    End If
                    line = sr_prob.ReadLine
                Loop Until line = ""
                sr_prob.Close()
            End If
            Dim sr As StreamReader = New StreamReader(file_path)
            Do
                line = sr.ReadLine
            Loop Until line.StartsWith("[TAXON]")
            Dim Temparray As Integer = 1
            ReDim D_id(NumofTaxon - 1)
            ReDim TempName(NumofTaxon - 1)
            For i As Integer = 0 To NumofTaxon - 1
                line = sr.ReadLine
                TempName(i) = line.Split(New Char() {"	"c})(1)
            Next
            Do
                line = sr.ReadLine
            Loop Until line.StartsWith("[TREE]")
            Treeline = sr.ReadLine.Split(New Char() {"="c})(1).Replace(";", "")
            NumofTaxon = Treeline.Length - Treeline.Replace(",", "").Length + 1
            NumofNode = Treeline.Length - Treeline.Replace("(", "").Length

            For i As Integer = 0 To NumofTaxon - 1
                D_id(i) = Array.IndexOf(TempName, TaxonName(i)) + 1
                If D_id(i) <= 0 Then
                    MsgBox("Can not find " + TaxonName(i) + " in " + file_path)
                    Exit For
                End If
            Next
            get_tree_length(Treeline)
            Dim temp_node_line() As String
            ReDim temp_node_line(NumofNode - 1)
            For i As Integer = 0 To NumofNode - 1
                temp_node_line(i) = "#"
                Dim temp_array() As String = Combine_Node(i, 3).Split(New Char() {","c})
                For k As Integer = 0 To UBound(temp_array)
                    If temp_array(k) <> "" Then
                        temp_array(k) = (Array.IndexOf(D_id, CInt(temp_array(k))) + 1).ToString
                        If temp_array(k) = 0 Then
                            MsgBox("error!!")
                        End If
                    End If
                Next
                Array.Sort(temp_array)
                For Each j As String In temp_array
                    If j <> "" Then
                        temp_node_line(i) += j + "#"
                    End If
                Next
            Next
            Dim temp_result_list() As String
            ReDim temp_result_list(NumofNode - 1)
            temp_id = 0
            Do
                If line.StartsWith("node") Then
                    temp_result_list(temp_id) = line.Split(New Char() {":"c})(1)
                    temp_id += 1
                End If
                line = sr.ReadLine
            Loop Until temp_id = NumofNode
            If has_prob Then
                Do While line.StartsWith("[PROBABILITY]") = False
                    line = sr.ReadLine
                Loop
                line = sr.ReadLine
                For temp_id = 0 To NumofNode - 1
                    line = sr.ReadLine
                    For k As Integer = 1 To AreaStr.Length * 2
                        final_prob_list(temp_id, k) += Val(line.Split("	")(k))
                    Next
                Next
            End If

            sr.Close()

            For temp_id = 0 To NumofNode - 1
                Dim temp_node_id As Integer = Array.IndexOf(node_line, temp_node_line(temp_id))
                If temp_node_id >= 0 Then
                    Dim temp_p1() As String = temp_result_list(temp_id).Split(New Char() {" "c})
                    Dim temp_p2() As String = final_result_list(temp_node_id).Split(New Char() {" "c})
                    For k As Integer = 1 To UBound(temp_p1) Step 2
                        Dim temp_p_id As Integer = Array.IndexOf(temp_p2, temp_p1(k))
                        If temp_p_id < 0 Then
                            ReDim Preserve temp_p2(UBound(temp_p2) + 2)
                            temp_p2(UBound(temp_p2) - 1) = temp_p1(k)
                            temp_p2(UBound(temp_p2)) = temp_p1(k + 1)
                        Else
                            temp_p2(temp_p_id + 1) = (Val(temp_p2(temp_p_id + 1)) + Val(temp_p1(k + 1))).ToString("F2")
                        End If
                    Next
                    final_result_list(temp_node_id) = ""
                    For Each l As String In temp_p2
                        If l <> "" Then
                            final_result_list(temp_node_id) += " " + l
                        End If
                    Next
                    final_p_list(temp_node_id) += 1
                End If
            Next
            file_id += 1
        Next
        For i As Integer = 0 To NumofNode - 1
            Dim temp_p1() As String = final_result_list(i).Split(New Char() {" "c})
            For k As Integer = 2 To UBound(temp_p1) Step 2
                final_value_list(i) += Val(temp_p1(k))
            Next
        Next
        For i As Integer = 0 To NumofNode - 1
            Dim temp_p1() As String = final_result_list(i).Split(New Char() {" "c})
            Dim temp_a() As String
            Dim temp_p() As Single
            ReDim temp_a(UBound(temp_p1) / 2)
            ReDim temp_p(UBound(temp_p1) / 2)
            For k As Integer = 2 To UBound(temp_p1) Step 2
                temp_p(k / 2) = Val(temp_p1(k)) / final_p_list(i)
                temp_a(k / 2) = temp_p1(k - 1)
            Next
            Array.Sort(temp_p, temp_a)
            final_result_list(i) = ""
            For l As Integer = 0 To UBound(temp_a)
                If temp_a(l) <> "" Then
                    final_result_list(i) = " " + temp_a(l) + " " + temp_p(l).ToString("F2") + final_result_list(i)
                End If
            Next
        Next
       
            Dim swf As New StreamWriter(root_path + "temp\combine_result.txt", False)
            swf.WriteLine("Combined result file")
            swf.WriteLine("[TAXON]")
            For i As Integer = 1 To NumofTaxon
                swf.WriteLine(i.ToString + "	" + TaxonName(i - 1) + "	" + TaxonDis(i - 1))
            Next
            swf.WriteLine("[TREE]")
            swf.WriteLine("Tree=" + Tree_first + ";")
            swf.WriteLine("[RESULT]")
            swf.WriteLine("Combined results:")
            get_tree_length(Tree_first)
            For i As Integer = 0 To NumofNode - 1
                Dim t_list() As String = Combine_Node(i, 3).Split(New Char() {","c})
                swf.WriteLine("node " + (NumofTaxon + i + 1).ToString + " (anc. of terminals " + t_list(0) + "-" + t_list(UBound(t_list)) + "):" + final_result_list(i))
            Next
            If has_prob Then
                swf.WriteLine("[PROBABILITY]")
                Dim temp_log As String = "	"
                For i As Integer = 1 To RangeStr.Length
                    temp_log = temp_log + Chr(i + 64).ToString + "(0)	" + Chr(i + 64).ToString + "(1)	"
                Next
                swf.WriteLine(temp_log)
                For temp_id = 0 To NumofNode - 1
                    temp_log = "node " + (NumofTaxon + temp_id + 1).ToString + ":"
                    For k As Integer = 1 To AreaStr.Length * 2
                        temp_log += "	" + (final_prob_list(temp_id, k) / ListBox1.Items.Count).ToString("F6")
                    Next
                    swf.WriteLine(temp_log)
                Next
            End If
            swf.Close()
        timer_id = 1
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        opendialog.Multiselect = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                For Each f As String In opendialog.FileNames
                    If ListBox1.Items.IndexOf(f) < 0 Then
                        ListBox1.Items.Add(f)
                    End If
                Next
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        curr_num = 0
        timer_id = 0
        Timer1.Enabled = True
        Dim th1 As New Thread(AddressOf combine_result)
        th1.Start()
    End Sub

    Private Sub Tool_Combine_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
    Private Sub Tool_Combine_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            ListBox1.Items.Clear()
        End If
    End Sub
    Dim curr_num As Integer
    Dim timer_id As Integer = 0
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Select Case timer_id
            Case 0
                ProgressBar1.Value = CInt(curr_num / ListBox1.Items.Count * 100)
            Case 1
                Timer1.Enabled = False
                ProgressBar1.Value = 0
                Dim opendialog As New SaveFileDialog
                opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
                opendialog.FileName = ""
                opendialog.DefaultExt = ".txt"
                opendialog.CheckFileExists = False
                opendialog.CheckPathExists = True
                Dim resultdialog As DialogResult = opendialog.ShowDialog()
                If resultdialog = DialogResult.OK Then
                    File.Copy(root_path + "temp\combine_result.txt", opendialog.FileName, True)
                End If
        End Select

    End Sub

    Private Sub Tool_Combine_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TargetOS = "macos" Then
            Me.TopMost = False
        End If
    End Sub
End Class