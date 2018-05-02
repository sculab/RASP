'逆时间推测，将某时间前的所有分布地合并（除去外类群），得出扩散的顺序
'逆时间推测，将某时间前每个节点分布地的关系，得出是否有软隔离存在。硬隔离：由研究者指定的隔离，会覆盖软隔离。软隔离：由算法得出的隔离
'逆时间推测，得出灭绝事件发生的年代

Imports System.IO
Imports System.Threading
Public Class DIVA
    Dim date1 As Double
    Dim tree_char() As String
    Dim node0() As Integer
    Dim node1() As Integer
    Dim node2() As Integer
    Dim node3_0() As Boolean
    Dim node3_1() As Boolean
    Dim node4() As String

    Dim node5() As String

    Dim node7() As Integer
    Dim node8() As String
    Dim node9() As Integer

    Dim node_d() As Integer
    Dim node_c() As Integer
    Dim node_v() As Integer
    Dim node_e() As Integer

    Dim check_node0(,) As Integer
    Dim check_node1(,) As Integer
    Dim distrubition() As String
    Dim root_node As Integer = -1
    Dim max_level As Integer = 0
    Dim Extinction As Integer
    Dim node_clade() As String
    Dim area_num As Integer
    Dim tax_num As Integer
    Dim area_list As String
    Dim optinal_num As Integer
    Dim min_dis As Integer
    Dim Temp_min_dis As Integer
    Dim range_list() As String
    Dim min_v As Integer = 0
    Dim p1, p2, p3 As Integer
    Dim DIVA_range As String = ""
    Dim rang_num As Integer

    Public Function make_final_tree(ByVal taxon_num As Integer) As String
        Dim Temp As String = ""
        Dim f_t_name() As String
        ReDim f_t_name(taxon_num)
        Dim tree_Temp1 As String = ""
        Dim tree_Temp As String = RichTextBox1.Text
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
        Return tree_Temp1
    End Function
    Public Sub read_node(ByVal treeline As String, ByVal taxon_num As Integer)
        Dim l_c As Integer = 0
        Dim r_c As Integer = 0
        '0,级别 1,左分支 2,右分支 3,长度,4分布,5化石分布,6灭绝,7子节点,8节点链,9节点顺序,10最小扩散
        'ReDim node_select(taxon_num - 1)
        ReDim node0(taxon_num - 1)
        ReDim node1(taxon_num - 1)
        ReDim node2(taxon_num - 1)
        ReDim node3_0(taxon_num - 1)
        ReDim node3_1(taxon_num - 1)
        ReDim node4(taxon_num - 1)
        ReDim node5(taxon_num - 1)

        ReDim node_v(taxon_num - 1)
        ReDim node_e(taxon_num - 1)
        ReDim node_d(taxon_num - 1)
        ReDim node_c(taxon_num - 1)

        ReDim node7(taxon_num - 1)
        ReDim node8(taxon_num - 1)
        ReDim node9(taxon_num)

        ReDim check_node0(taxon_num - 1, taxon_num - 1)
        ReDim check_node1(taxon_num - 1, taxon_num - 1)
        ReDim tree_char(taxon_num * 4 - 3)
        For k As Integer = 0 To taxon_num - 1
            node3_0(k) = False
            node3_1(k) = False
        Next

        Dim node_num As Integer = 0
        Dim i As Integer = 0
        Dim Temp_node() As Integer
        ReDim Temp_node(taxon_num - 1)
        For Each Temp_char As Char In treeline
            Select Case Temp_char
                Case "("
                    l_c = l_c + 1
                    i = i + 1
                Case ")"
                    r_c = r_c + 1
                    i = i + 1
                Case ","
                    node_num = node_num + 1
                    node0(node_num) = l_c - r_c
                    If node0(node_num) = 1 Then
                        root_node = node_num
                    End If
                    If max_level < node0(node_num) Then
                        max_level = node0(node_num)
                    End If
                    i = i + 1
                    Temp_node(node_num) = i
                Case Else
                    If tree_char(i) = "(" Or tree_char(i) = ")" Or tree_char(i) = "," Then
                        i = i + 1
                    End If
            End Select
            tree_char(i) = tree_char(i) + Temp_char
        Next

        For n As Integer = 1 To taxon_num - 1
            If DataGridView2.Rows(n - 1).Cells(1).Value <> "" Then
                node5(n) = DataGridView2.Rows(n - 1).Cells(1).Value.ToString.ToUpper
            Else
                node5(n) = ""
            End If

            node3_0(n) = False
            node3_1(n) = False
            Dim node_str As String = ""
            l_c = 0
            r_c = 0
            i = Temp_node(n)
            Do
                i = i - 1
                Select Case tree_char(i)
                    Case "("
                        l_c = l_c + 1
                    Case ")"
                        r_c = r_c + 1
                    Case ","
                        If node0(Array.IndexOf(Temp_node, i)) - node0(n) = 1 Then
                            node_str = Array.IndexOf(Temp_node, i).ToString
                        End If
                    Case Else
                        If l_c = r_c Then
                            node_str = tree_char(i)
                            node3_0(n) = True
                        End If
                End Select
            Loop Until l_c = r_c
            node1(n) = node_str
            If node3_0(n) = False Then
                node7(CInt(node_str)) = n
            End If
            l_c = 0
            r_c = 0
            i = Temp_node(n)
            node_str = ""
            Do
                i = i + 1
                Select Case tree_char(i)
                    Case "("
                        l_c = l_c + 1
                    Case ")"
                        r_c = r_c + 1
                    Case ","
                        If node0(Array.IndexOf(Temp_node, i)) - node0(n) = 1 Then
                            node_str = Array.IndexOf(Temp_node, i).ToString
                        End If
                    Case Else
                        If l_c = r_c Then
                            node_str = tree_char(i)
                            node3_1(n) = True
                        End If

                End Select
            Loop Until l_c = r_c
            node2(n) = node_str
            If node3_1(n) = False Then
                node7(CInt(node_str)) = n
            End If
        Next
        Dim Temp_level As Integer = max_level
        Dim Temp_node9 As Integer = 1
        Do
            For j As Integer = 1 To taxon_num - 1
                If Temp_level = node0(j) Then
                    node9(Temp_node9) = j
                    Temp_node9 += 1
                End If
            Next
            Temp_level -= 1
        Loop Until Temp_level < 0
        Dim Temp_clade As Integer = 0
        For j As Integer = 1 To taxon_num - 1
            node8(j) = j
            Temp_clade = j
            Do While node7(Temp_clade) <> 0
                node8(j) = node8(j) + "," + node7(Temp_clade).ToString
                Temp_clade = node7(Temp_clade)
            Loop
        Next
        For j As Integer = 1 To taxon_num - 1
            Dim l As Integer = 1
            Dim m As Integer = 1
            For k As Integer = 1 To taxon_num - 1
                If (k < j And node0(k) = node0(j)) Or node0(k) > node0(j) Then
                    check_node0(j, l) = k
                    l += 1
                ElseIf node0(k) = node0(j) Or (node0(k) = node0(j) - 1 And k < node7(j)) Then
                    check_node1(j, m) = k
                    m += 1
                End If
            Next
        Next
    End Sub
    Private Function node_contain(ByVal num As Integer) As String
        Dim contain As String = ""
        Dim left_clade As String = node1(num)
        Dim right_clade As String = node2(num)
        If node3_0(num) Then
            contain = contain + left_clade + " "
        Else
            contain = contain + node_contain(left_clade) + " "
        End If
        If node3_1(num) Then
            contain = contain + right_clade + " "
        Else
            contain = contain + node_contain(right_clade) + " "
        End If
        Return contain
    End Function
    Public Sub Run_diva()
        For n As Integer = 1 To tax_num - 1
            RichTextBox1.AppendText("Clade " + n.ToString + ": " + node_clade(n).Replace("  ", " ") + Chr(10))
        Next
        RichTextBox1.AppendText("Clade	")
        For n As Integer = 1 To tax_num - 1
            RichTextBox1.AppendText(n.ToString + "	 ")
        Next
        RichTextBox1.AppendText(Chr(10))
        clean_run()
        'second_run = False
        fill_node(0, 1)
        'second_run = True
        'optinal_num = 0
        'fill_node(0, 1)
        'min_v = min_dis(0)

        RichTextBox1.AppendText("there are " + optinal_num.ToString + " alternative." + Chr(10))
        RichTextBox1.AppendText("Cost time: " + (CInt(System.DateTime.Now.Minute * 60 + System.DateTime.Now.Second + System.DateTime.Now.Millisecond / 1000) - date1).ToString("F3") + " Second" + Chr(10))
        RichTextBox1.AppendText(Chr(10))

        CheckForIllegalCrossThreadCalls = True
        'wr.Close()
    End Sub
    Public Function sum_dis() As Integer
        Temp_min_dis = 0
        For i As Integer = 1 To tax_num - 1
            Temp_min_dis += node_d(i) * p1 + node_c(i) * p3 + node_v(i) * p2 + node_e(i)
        Next
        Return Temp_min_dis
    End Function
    Public Sub add_list(ByVal list_index As Integer)
        area_list = "(" + Temp_min_dis.ToString + ")"
        For i As Integer = 1 To tax_num - 1
            area_list = area_list + "	" + node4(i) + "(" + (node_d(i)).ToString + "/" + (node_c(i)).ToString + "/" + (node_v(i)).ToString + "/" + (node_e(i)).ToString + ")"
        Next
        RichTextBox1.AppendText(area_list + Chr(10))
    End Sub
    Public Function check(ByVal node_num As Integer) As Boolean
        Dim sum As Integer = 0
        Dim check_id As Integer
        For i As Integer = 1 To tax_num - 1
            check_id = check_node0(node_num, i)
            If check_id > 0 Then
                sum = sum + node_d(check_id) * p1 + node_c(check_id) * p3 + node_v(check_id) * p2 + node_e(i)
            Else
                Exit For
            End If
        Next
        'If second_run Then
        If sum > min_dis Then
            Return False
        End If
        'Else
        '    If sum >= min_dis Then
        '        Return False
        '    End If
        'End If

        Return True
    End Function
    Public Function min(ByVal a As Integer, ByVal b As Integer) As Integer
        If a > b Then
            Return b
        Else
            Return a
        End If
    End Function
    Public Sub clean_run()
        min_dis = 32767
    End Sub
    Public Sub do_with_area(ByVal he_ji As Integer, ByVal bing_ji As Integer, ByVal three_bing_ji As Integer, ByVal node_ex As Integer, ByVal parent_area As String, ByVal node_num As Integer, ByVal node_id As Integer)
        node4(node_num) = parent_area
        If parent_area.Length = 1 Then
            node_d(node_num) = (he_ji - node_ex - 2 * three_bing_ji)
            node_c(node_num) = bing_ji
        Else
            node_d(node_num) = (he_ji - parent_area.Length)
            node_c(node_num) = bing_ji
        End If
        If three_bing_ji > 0 Then
            node_v(node_num) = 0
        Else
            node_v(node_num) = 1
        End If
        node_e(node_num) = node_ex
        fill_node(node_num, node_id + 1)
    End Sub
    Public Function com_area(ByVal first_str As String, ByVal second_str As String) As Boolean
        If first_str <> second_str Then
            Dim Temp_str As String = ""
            For Each s_Temp As Char In second_str
                If first_str.Contains(s_Temp) Then
                    Return True
                End If
            Next
            Return False
        Else
            Return True
        End If
    End Function
    Public Function connect_area(ByVal first_str As String, ByVal second_str As String) As String()
        Dim restr(1) As String
        restr(1) = first_str
        restr(0) = ""
        If first_str = "" Then
            restr(1) = second_str
            restr(0) = ""
            Return restr
        End If
        For Each s_Temp As Char In second_str
            If first_str.Contains(s_Temp) = False Then
                restr(1) += s_Temp.ToString
            Else
                restr(0) += s_Temp.ToString
            End If
        Next
        Return restr
    End Function
    Public Function pailie(ByVal n() As Integer, ByVal postion As Integer, ByVal a_num As Integer, ByVal Temp_range_num As Integer) As Boolean
        If n(postion) <= Temp_range_num - (a_num - postion) And n(postion) <= n(postion + 1) - 2 Then
            n(postion) = n(postion) + 1
            Return True
        Else
            If postion > 1 Then
                If n(postion - 1) + 2 <= Temp_range_num - (a_num - postion) Then
                    n(postion) = n(postion - 1) + 2
                End If
                For i As Integer = postion + 1 To a_num
                    If n(i - 1) + 1 <= Temp_range_num - (a_num - (i - 1)) Then
                        n(i) = n(i - 1) + 1
                    End If
                Next
                For i As Integer = 1 To a_num
                    If n(i) > Temp_range_num Then
                        Return False
                    End If
                Next
                Return pailie(n, postion - 1, a_num, Temp_range_num)
            Else
                Return False
            End If
        End If
    End Function
    Public Sub fill_node(ByVal node_num As Integer, ByVal node_id As Integer)
        node_num = node9(node_id)
        If node_num = 0 Then
            Dim sum As Integer
            sum = sum_dis()
            'If second_run Then
            If min_dis > sum Then
                min_dis = Temp_min_dis
                optinal_num = 1
                add_list(optinal_num)
            ElseIf min_dis = sum Then
                min_dis = Temp_min_dis
                optinal_num += 1
                add_list(optinal_num)
            End If
            'Else
            '    If min_dis > sum Then
            '        min_dis = Temp_min_dis
            '    End If
            'End If
            Exit Sub
        End If

        Dim l_area, r_area As String
        If node3_0(node_num) Then
            l_area = distrubition(node1(node_num) - 1)
        Else
            l_area = node4(node1(node_num))
        End If
        If node3_1(node_num) Then
            r_area = distrubition(node2(node_num) - 1)
        Else
            r_area = node4(node2(node_num))
        End If

        Dim ji_he() As String = connect_area(l_area, r_area)
        Dim Tempchar() As Char = ji_he(1)

        'Array.Sort(Tempchar)
        Dim con_length As Integer = min(area_num, Tempchar.Length)

        If node_num > 0 Then
            If check(node_num) = False Then
                Exit Sub
            End If
        End If
        '开始计算该节点的可能
        Dim set_node_e As Integer = 0
        If node5(node_num) = "" Then
            If con_length = 1 Then
                If ji_he(0) = Tempchar Then
                    do_with_area(ji_he(1).Length, ji_he(0).Length, 1, set_node_e, Tempchar, node_num, node_id)
                Else
                    do_with_area(ji_he(1).Length, ji_he(0).Length, 0, set_node_e, Tempchar, node_num, node_id)
                End If
            Else
                For Each Integerarea As Char In Tempchar
                    If ji_he(0).Contains(Integerarea) Then
                        do_with_area(ji_he(1).Length, ji_he(0).Length, 1, set_node_e, Integerarea, node_num, node_id)
                    Else
                        do_with_area(ji_he(1).Length, ji_he(0).Length, 0, set_node_e, Integerarea, node_num, node_id)
                    End If
                Next
                For Each Tempstr As String In range_list
                    If Tempstr.Length > con_length Then
                        Exit For
                    Else
                        If iscontain(Tempchar, Tempstr) Then
                            do_with_area(ji_he(1).Length, ji_he(0).Length, connect_area(ji_he(0), Tempstr)(0).Length, set_node_e, Tempstr, node_num, node_id)
                        End If
                    End If
                Next
            End If
        Else
            If iscontain(Tempchar, node5(node_num)) Then
                For Each Tempstr As String In range_list
                    If Tempstr.Length > con_length Then
                        Exit For
                    Else
                        If iscontain(Tempchar, Tempstr) And iscontain(Tempstr, node5(node_num)) Then
                            do_with_area(ji_he(1).Length, ji_he(0).Length, connect_area(ji_he(0), Tempstr)(0).Length, set_node_e, Tempstr, node_num, node_id)
                        End If
                    End If
                Next
            Else
                Dim commen As String = ""
                Dim no_commen As String = ""
                For Each i As Char In node5(node_num)
                    If Array.IndexOf(Tempchar, i) < 0 Then
                        set_node_e += 1
                        no_commen += i
                    Else
                        commen += i
                    End If
                Next
                If set_node_e = node5(node_num).Length Then
                    do_with_area(ji_he(1).Length + set_node_e, ji_he(0).Length, 0, set_node_e, node5(node_num), node_num, node_id)
                End If
                If node5(node_num).Length < area_num Then
                    For Each Integerarea As Char In Tempchar
                        If node5(node_num).Contains(Integerarea) = False Then
                            If iscontain(ji_he(0), commen + Integerarea) Then
                                do_with_area(ji_he(1).Length + set_node_e, ji_he(0).Length, 1, set_node_e, no_commen + Integerarea, node_num, node_id)
                            Else
                                do_with_area(ji_he(1).Length + set_node_e, ji_he(0).Length, 0, set_node_e, no_commen + Integerarea, node_num, node_id)
                            End If
                        End If
                    Next
                    For Each Tempstr As String In range_list
                        If Tempstr.Length > con_length - set_node_e Then
                            Exit For
                        Else
                            If iscontain(Tempchar, Tempstr) And iscontain(Tempstr, commen) Then
                                do_with_area(ji_he(1).Length + set_node_e, ji_he(0).Length, connect_area(ji_he(0), Tempstr)(0).Length, set_node_e, Tempstr, node_num, node_id)
                            End If
                        End If
                    Next
                ElseIf set_node_e <> node5(node_num).Length Then
                    do_with_area(ji_he(1).Length + set_node_e, ji_he(0).Length, connect_area(ji_he(0), commen)(0).Length, set_node_e, node5(node_num), node_num, node_id)
                End If

            End If
        End If

    End Sub
    Public Function iscontain(ByVal firststr As String, ByVal secondstr As String) As Boolean
        For Each i As Char In secondstr
            If firststr.Contains(i) = False Then
                Return False
            End If
        Next
        Return True
    End Function
    Private Function check_exist(ByVal checkstr As String) As Boolean
        Dim Tempchar() As Char = DIVA_range.ToUpper
        Array.Sort(Tempchar)
        For i As Integer = 0 To DIVA_range.Length - 1
            For j As Integer = i To DIVA_range.Length - 2
                If DataGridView1.Rows(i).Cells(j + 1).Value = False Then
                    If checkstr.Contains(Tempchar(i)) And checkstr.Contains(Tempchar(j + 1)) Then
                        Return False
                    End If
                End If
            Next
        Next
        Return True
    End Function

    Private Sub DIVA_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Private Sub RunAnaToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RunAnaToolStripMenuItem.Click
        If RichTextBox2.Text <> "" And RichTextBox3.Text <> "" Then
            date1 = System.DateTime.Now.Minute * 60 + DateTime.Now.Second + System.DateTime.Now.Millisecond / 1000
            p1 = Parameter1.Value
            p2 = Parameter2.Value
            p3 = Parameter3.Value

            area_num = NumericUpDown1.Value
            read_node(RichTextBox2.Text.Replace(";", ""), tax_num)

            clean_run()
            optinal_num = 1
            ReDim node_clade(tax_num - 1)
            For n As Integer = 1 To tax_num - 1
                node_clade(n) = node_contain(n)
            Next
            ReDim range_list(ListBox1.Items.Count - 1)
            For i As Integer = 0 To ListBox1.Items.Count - 1
                range_list(i) = ListBox1.Items.Item(i)
            Next

            CheckForIllegalCrossThreadCalls = False
            Dim diva As New Thread(AddressOf Run_diva)
            diva.Start()
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex >= 0 Then
            ListBox2.Items.Add(ListBox1.SelectedItem)
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex >= 0 Then
            Dim i As Integer = 0
            Do While ListBox1.Items(i).ToString.Length <> ListBox2.SelectedItem.ToString.Length
                i += 1
            Loop
            ListBox1.Items.Insert(i, ListBox2.SelectedItem)
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim Tempchar() As Char = DIVA_range.ToUpper
        Array.Sort(Tempchar)
        Dim list_num As Integer = ListBox1.Items.Count
        If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
            For i As Integer = 1 To list_num
                If ListBox1.Items(list_num - i).ToString.Contains(Tempchar(e.RowIndex)) And ListBox1.Items(list_num - i).ToString.Contains(Tempchar(e.ColumnIndex)) Then
                    ListBox2.Items.Add(ListBox1.Items(list_num - i))
                    ListBox1.Items.RemoveAt(list_num - i)
                End If
            Next
        End If
    End Sub
    Private Sub MakeRangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MakeRangeToolStripMenuItem.Click
        If RichTextBox2.Text <> "" And RichTextBox3.Text <> "" Then
            tax_num = RichTextBox2.Text.Length - RichTextBox2.Text.Replace(",", "").Length + 1
            Do While RichTextBox3.Text.Length <> RichTextBox3.Text.Replace("  ", " ").Length
                RichTextBox3.Text = RichTextBox3.Text.Replace("  ", " ")
            Loop
            distrubition = ("$" + RichTextBox3.Text + "$").Replace(";", "").Replace("$ ", "").Replace(" $", "").Replace("$", "").ToUpper.Split(New Char() {" "c})
            DataGridView2.Columns.Clear()
            DataGridView2.Columns.Add("ID", "Node")
            DataGridView2.Columns.Item(0).Width = 48
            DataGridView2.Columns.Add("DIS", "DIS.")
            For j As Integer = 0 To distrubition.Length - 2
                DataGridView2.Rows.Add()
                DataGridView2.Rows(j).Cells(0).Value = j + 1
                DataGridView2.Rows(j).Cells(0).ReadOnly = True
            Next
            DataGridView2.AllowUserToAddRows = False
            DataGridView2.AllowUserToDeleteRows = False
            DataGridView2.AllowUserToOrderColumns = False

            If tax_num <> distrubition.Length Then
                MsgBox("distrubition/tree error!")
                Exit Sub
            End If

            For Each i As String In RichTextBox3.Text.ToUpper.Replace(";", "").Replace(" ", "")
                If DIVA_range.Contains(i) = False Then
                    DIVA_range = DIVA_range + i
                End If
            Next
            Dim Tempchar() As Char = DIVA_range

            If distrubition.Length < NumericUpDown1.Value Then
                NumericUpDown1.Value = distrubition.Length
            End If

            Array.Sort(Tempchar)

            DataGridView1.Columns.Clear()
            DataGridView1.Rows.Clear()

            Dim r As Integer = 0
            For Each i As Char In Tempchar
                Dim Column As New DataGridViewCheckBoxColumn
                Column.HeaderText = i.ToString
                Column.Width = 32
                Column.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns.Add(Column)
                DataGridView1.Rows.Add()
                DataGridView1.Rows(r).HeaderCell.Value = i.ToString
                For j As Integer = 0 To r
                    DataGridView1.Rows(r).Cells(j).ReadOnly = True
                    DataGridView1.Rows(r).Cells(j).Style.SelectionBackColor = Color.Gray
                    DataGridView1.Rows(r).Cells(j).Style.SelectionForeColor = Color.Gray
                    DataGridView1.Rows(r).Cells(j).Style.BackColor = Color.Gray
                    DataGridView1.Rows(r).Cells(j).Style.ForeColor = Color.Gray
                Next
                r = r + 1
            Next
            For i As Integer = 0 To Tempchar.Length - 1
                For j As Integer = i To Tempchar.Length - 2
                    DataGridView1.Rows(i).Cells(j + 1).Value = True
                Next
            Next

            DataGridView1.BackgroundColor = Color.LightGray
            DataGridView1.AllowUserToAddRows = False
            DataGridView1.AllowUserToDeleteRows = False
            DataGridView1.AllowUserToOrderColumns = False
            DataGridView1.AllowUserToResizeColumns = False
            DataGridView1.AllowUserToResizeRows = False
            RefreshTheRangeToolStripMenuItem_Click(sender, e)
            RunAnaToolStripMenuItem.Enabled = True
        End If
    End Sub
    Private Sub RefreshTheRangeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshTheRangeToolStripMenuItem.Click
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        DataGridView1.EndEdit()
        rang_num = DataGridView1.Rows.Count
        Dim Tempchar() As Char = DIVA_range.ToUpper
        If NumericUpDown1.Value > rang_num Then
            NumericUpDown1.Value = rang_num
        End If
        area_num = CInt(NumericUpDown1.Value)
        Array.Sort(Tempchar)
        For j As Integer = 2 To min(area_num, rang_num)
            Dim n() As Integer
            ReDim n(j + 1)
            For x As Integer = 1 To j
                n(x) = x
            Next
            n(j + 1) = rang_num + 1
            Dim isend As Boolean = True
            Do
                Dim Tempstr As String = ""

                For x As Integer = 1 To j
                    Tempstr = Tempstr + Tempchar(n(x) - 1)
                Next
                If check_exist(Tempstr) Then
                    ListBox1.Items.Add(Tempstr)
                Else
                    ListBox2.Items.Add(Tempstr)
                End If
                isend = pailie(n, j, j, rang_num)
            Loop Until isend = False
        Next
    End Sub
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        RunAnaToolStripMenuItem.Enabled = False
    End Sub
    Private Sub ClearTreeAndDistributionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ClearTreeAndDistributionToolStripMenuItem.Click
        RichTextBox2.Text = ""
        RichTextBox3.Text = ""

        RunAnaToolStripMenuItem.Enabled = False
    End Sub
    Private Sub RestParametersToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RestParametersToolStripMenuItem.Click
        Parameter1.Value = 1
        Parameter2.Value = 1
        Parameter3.Value = 1
    End Sub
End Class