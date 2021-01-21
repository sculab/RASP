Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Math
Imports System.Threading

Public Class View_Tree
    Public show_my_tree As String = ""
    Dim data_type As Integer = 0
    Dim current_state_mode = 0
    Dim Poly_Node(,) As String
    Dim Node_Relationship(,) As String
    Dim Poly_Node_row(,) As Single
    Dim Poly_terminal_xy(,) As Single
    Dim Poly_Node_col(,) As Single
    Dim Distribution() As String
    Dim TaxonName() As String
    Dim TaxonTime(,) As String
    Dim Tree_Export_Char() As String
    Dim NumofNode As Integer
    Dim NumofTaxon As Integer
    Dim RangeLength As Integer
    Dim max_level As Integer
    Dim max_taxon_name As Integer
    Dim begin_draw As Boolean = False
    Dim draw_result As Boolean = False
    '结果列表
    Dim Global_Text As String = ""
    Dim Result_list() As String
    Dim result_ID As Integer = -1
    Dim selected_nodes(0) As Integer

    Dim D_nodes(0) As Integer
    Dim E_nodes(0) As Integer
    Dim G_nodes(0) As Integer
    Dim R_nodes(0) As Integer
    Dim V_nodes(0) As Integer
    '区域饼图
    Dim Current_AreaS(,) As String
    Dim Current_AreaP(,) As Single
    Dim max_dis_value As Single = -1 / 0
    Dim min_dis_value As Single = 1 / 0
    '色卡
    Dim Color_S() As String
    Dim Color_B() As Brush

    Dim Color_S_node() As String
    Dim Color_B_node() As Brush
    '圆参数
    Dim pie_step As Single = 0.01
    Dim taxon_array() As String
    Dim has_length As Boolean = False
    '平滑度
    Dim smooth_x As Integer = 1

    Public temp_result_file As String

    Dim time_view As New DataView
    Dim time_Dataset As New DataSet
    Dim Loading As Boolean = True
    Dim TimeView As Integer = 0
    Public tree_view_limit As Boolean = False
    Public current_state As Integer
    Public Function Swap_tree(ByVal Treeline As String, ByVal gotnode As Integer) As String
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
        Dim start_index, end_index As Integer

        For i As Integer = 1 To char_id
            If tree_char(i) = ")" And gotnode <> 0 Then
                gotnode -= 1
                If gotnode = 0 Then
                    end_index = i
                End If
            End If
        Next
        gotnode = Selected_node + 1
        l_c = 0
        r_c = 0
        For i As Integer = 1 To end_index
            Select Case tree_char(end_index - i + 1)
                Case "("
                    l_c += 1
                Case ")"
                    r_c += 1
                Case Else
            End Select
            If l_c = r_c Then
                start_index = end_index - i + 1
                Exit For
            End If
        Next
        l_c = 0
        r_c = 0
        For i As Integer = 1 To start_index
            Select Case tree_char(i)
                Case ")"
                    r_c += 1
                Case Else
            End Select
        Next



        l_c = 0
        r_c = 0

        Dim new_clade As String = ""
        Dim new_tree As String = ""
        Dim has_value As Boolean = False
        Dim tree_value(0) As String
        Dim tree_index(0) As Integer
        For i As Integer = 1 To end_index - start_index - 1
            Select Case tree_char(end_index - i)
                Case "("
                    l_c += 1
                    new_clade = new_clade + ")"
                    If UBound(tree_value) > 0 Then
                        new_clade = new_clade + tree_value(UBound(tree_value))
                        ReDim Preserve tree_value(tree_value.Length - 2)
                    End If
                    ReDim Preserve tree_index(tree_index.Length - 2)
                Case ")"
                    r_c += 1
                    new_clade = new_clade + "("
                    ReDim Preserve tree_index(tree_index.Length)
                    tree_index(UBound(tree_index)) = Selected_node - r_c

                Case ","
                    new_clade = new_clade + ","
                Case Else
                    If tree_char(end_index - i - 1) = ")" Then
                        ReDim Preserve tree_value(tree_value.Length)
                        tree_value(UBound(tree_value)) = tree_char(end_index - i)
                    Else
                        new_clade = new_clade + tree_char(end_index - i)
                    End If
            End Select
        Next
        For i As Integer = 1 To start_index
            new_tree = new_tree + tree_char(i)
        Next
        new_tree = new_tree + new_clade
        For i As Integer = end_index To char_id
            new_tree = new_tree + tree_char(i)
        Next

        Dim node_line_old() As String
        ReDim node_line_old(NumofNode - 1)
        For i As Integer = 0 To NumofNode - 1
            Dim temp_arry() As String = Poly_Node(i, 3).Split(",")
            Array.Sort(temp_arry)
            Poly_Node(i, 3) = temp_arry(0)
            For j As Integer = 1 To UBound(temp_arry)
                Poly_Node(i, 3) += "," + temp_arry(j)
            Next
            node_line_old(i) = Poly_Node(i, 3)
        Next
        Treeline = new_tree
        Read_Poly_Tree(Treeline)
        Dim node_line_new() As String
        ReDim node_line_new(NumofNode - 1)
        For i As Integer = 0 To NumofNode - 1
            Dim temp_arry() As String = Poly_Node(i, 3).Split(",")
            Array.Sort(temp_arry)
            Poly_Node(i, 3) = temp_arry(0)
            For j As Integer = 1 To UBound(temp_arry)
                Poly_Node(i, 3) += "," + temp_arry(j)
            Next
            node_line_new(i) = Poly_Node(i, 3)
        Next
        If draw_result = True Then
            Dim temp_array As Integer = 0
            Dim temp_string(UBound(node_line_old)) As String
            Do
                For i As Integer = 0 To UBound(node_line_new)
                    temp_string(i) = Result_list(Array.IndexOf(node_line_old, node_line_new(i)) + NumofNode * temp_array)
                Next
                For i As Integer = 0 To UBound(node_line_new)
                    Result_list(i + NumofNode * temp_array) = temp_string(i)
                Next
                temp_array += 1
            Loop Until (NumofNode + 1) * temp_array + 1 >= ListBox1.Items.Count

            If RadioButton2.Enabled Then
                Dim temp_PROB(UBound(node_line_new) + 1, RangeLength) As String
                For i As Integer = 1 To UBound(node_line_new) + 1
                    For j As Integer = 1 To RangeLength
                        temp_PROB(i, j) = PROB_list(Array.IndexOf(node_line_old, node_line_new(i - 1)), j)
                    Next
                Next

                For i As Integer = 1 To UBound(node_line_new) + 1
                    For j As Integer = 1 To RangeLength
                        PROB_list(Array.IndexOf(node_line_old, node_line_new(i - 1)), j) = temp_PROB(i, j)
                    Next
                Next
            End If
        End If
        Return new_tree
    End Function
    Public Sub Read_Poly_Tree(ByVal Treeline As String)
        If Treeline.EndsWith(";") = False Then
            Treeline += ";"
        End If
        ReDim Poly_Node(NumofNode - 1, 10) '0 root,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点, 10 级别
        ReDim Poly_Node_row(NumofNode - 1, 2)
        ReDim Poly_Node_col(NumofNode - 1, 2)
        ReDim Poly_terminal_xy(NumofTaxon - 1, 2)
        ReDim TaxonTime(NumofTaxon - 1, 2)
        has_length = False
        If Treeline.Contains(":") Then
            has_length = True
        End If
        ReDim taxon_array(NumofTaxon - 1)
        For i As Integer = 0 To NumofNode - 1
            Poly_Node(i, 0) = 0
            Poly_Node(i, 1) = ""
            Poly_Node(i, 2) = ""
            Poly_Node(i, 3) = ""
            Poly_Node(i, 6) = "0.00"
            Poly_Node(i, 7) = "0"
            Poly_Node(i, 8) = "0"
            Poly_Node(i, 10) = "0"
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
        If has_length Then
            For i As Integer = 1 To char_id
                If Tree_Export_Char(i).Contains(":") Then
                    If Tree_Export_Char(i - 1) <> ")" Then
                        '物种
                        TaxonTime(CInt(tree_char(i).Split(New Char() {":"c})(0)) - 1, 0) = tree_char(i).Split(New Char() {":"c})(1)
                        tree_char(i) = tree_char(i).Split(New Char() {":"c})(0)
                    End If
                End If
            Next
        End If

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
                    Poly_Node(point_2, 1) = Temp_node(point_1 - 1, 1)
                    Poly_Node(point_2, 2) = Temp_node(point_1 - 1, 2)
                    Poly_Node(point_2, 4) = Temp_node(point_1 - 1, 4)
                    Poly_Node(point_2, 5) = Temp_node(point_1 - 1, 5)
                    If Poly_Node(point_2, 2) = "" Then
                        Poly_Node(point_2, 10) = 1
                    Else
                        For Each k As String In Poly_Node(point_2, 2).Split(",")
                            If k <> "" Then
                                If CInt(Poly_Node(CInt(k), 10)) >= CInt(Poly_Node(point_2, 10)) Then
                                    Poly_Node(point_2, 10) = CInt(Poly_Node(k, 10)) + 1
                                End If
                            End If
                        Next
                    End If
                    For j As Integer = Temp_node(point_1 - 1, 0) To i
                        If tree_char(j) <> "(" And tree_char(j) <> ")" Then
                            If tree_char(j) <> "," Then
                                If tree_char(j - 1) <> ")" Then
                                    Poly_Node(point_2, 3) += tree_char(j)
                                End If
                            Else
                                Poly_Node(point_2, 3) += tree_char(j)
                            End If
                        End If
                    Next
                    If point_1 > 1 Then
                        Temp_node(point_1 - 2, 2) = point_2.ToString + "," + Temp_node(point_1 - 2, 2)
                        Temp_node(point_1 - 2, 4) = Min(Val(Temp_node(point_1 - 2, 4)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
                        Temp_node(point_1 - 2, 5) = Max(Val(Temp_node(point_1 - 2, 5)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
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
                        If has_length And tree_char(i).Contains(":") Then
                            If Val(tree_char(i).Split(New Char() {":"c})(0)) > 1 Then
                                Poly_Node(point_2 - 1, 6) = (Val(tree_char(i)) / 100).ToString("F2")
                            Else
                                Poly_Node(point_2 - 1, 6) = Val(tree_char(i)).ToString("F2")
                            End If
                            Poly_Node(point_2 - 1, 7) = tree_char(i).Split(New Char() {":"c})(1)
                        Else
                            If Val(tree_char(i)) > 1 Then
                                Poly_Node(point_2 - 1, 6) = (Val(tree_char(i)) / 100).ToString("F2")
                            Else
                                Poly_Node(point_2 - 1, 6) = Val(tree_char(i)).ToString("F2")
                            End If
                        End If

                    Else
                        taxon_array(tx) = tree_char(i)
                        tx += 1
                        Temp_node(point_1 - 1, 1) += tree_char(i) + ","
                        Temp_node(point_1 - 1, 4) = Min(Val(Temp_node(point_1 - 1, 4)), tx)
                        Temp_node(point_1 - 1, 5) = Max(Val(Temp_node(point_1 - 1, 4)), tx)
                    End If
            End Select
        Next
        If has_length Then
            make_chain(NumofNode - 1)
        End If
        time_Dataset.Tables("Time Table").Clear()
        time_view.AllowNew = True
        maxtime = 0
        For i As Integer = 0 To NumofTaxon - 1
            If maxtime < Val(TaxonTime(i, 1)) Then
                maxtime = Val(TaxonTime(i, 1))
            End If
        Next
        TimeBox.Text = maxtime.ToString
        Dim temp_Item_Array As Integer = 0

        For i As Integer = 0 To NumofNode - 1
            If (temp_Item_Array = 0 And time_view.Count = 1) = False Then
                time_view.AddNew()
            End If
            Dim newrow(2) As String
            newrow(0) = time_view.Count + NumofTaxon
            newrow(1) = Poly_Node(i, 8)
            If CheckBox8.Checked Then
                newrow(2) = ((maxtime - Val(Poly_Node(i, 8))) * root_time).ToString("F8")
                TaxonTime(i, 2) = ((maxtime - Val(TaxonTime(i, 1))) * root_time).ToString("F8")
            Else
                newrow(2) = (Val(Poly_Node(i, 8)) * root_time).ToString("F8")
                TaxonTime(i, 2) = (Val(TaxonTime(i, 1)) * root_time).ToString("F8")
            End If
            time_view.Item(temp_Item_Array).Row.ItemArray = newrow
            temp_Item_Array += 1

            If Poly_Node(i, 2) <> "" Then
                Dim anc_node() As String = Poly_Node(i, 2).Split(New Char() {","c})

                For Each j As String In anc_node
                    If j <> "" Then
                        Poly_Node(CInt(j), 9) = i.ToString
                        Poly_Node(i, 0) = Math.Max(Val(Poly_Node(i, 0)), Poly_Node(CInt(j), 0) + 1)
                    End If
                Next
            Else
                Poly_Node(i, 0) = 1
            End If
        Next
        time_view.AllowNew = False
        'make_tree_xy()
        NumericUpDown3.Maximum = NumofTaxon * 2 - 1
        NumericUpDown3.Value = NumofTaxon * 2 - 1
        NumericUpDown3.Minimum = NumofTaxon + 1
        If swaping = False Then
            NumericUpDown1.Value = Math.Truncate(Max((maxtime / 5), 1))
        End If
        If maxtime <= 0 Then
            has_length = False
        End If
    End Sub
    Public Sub make_tree_xy()
        For i As Integer = 0 To NumofNode - 1
            Dim anc_node() As String = Poly_Node(i, 2).Split(New Char() {","c})
            If Poly_Node(i, 2) <> "" Then
                For Each j As String In anc_node
                    If j <> "" Then
                        max_level = Math.Max(Val(Poly_Node(i, 0)), max_level)
                    End If
                Next
                For Each j As String In anc_node
                    If j <> "" Then
                        '竖线及节点中心坐标
                        Poly_Node_col(CInt(j), 0) = (Val(Poly_Node(CInt(j), 4)) + Val(Poly_Node(CInt(j), 5))) / 2
                        If ToplogyToolStripMenuItem.Checked = False And has_length Then
                            Poly_Node_col(CInt(j), 1) = (maxtime - Val(Poly_Node(CInt(j), 8))) * max_level / maxtime
                            Poly_Node_col(CInt(j), 2) = (maxtime - Val(Poly_Node(i, 8))) * max_level / maxtime

                        Else
                            Poly_Node_col(CInt(j), 1) = Val(Poly_Node(CInt(j), 0))
                            Poly_Node_col(CInt(j), 2) = Val(Poly_Node(i, 0))
                        End If

                    End If
                Next
            End If
            '横线
            If ToplogyToolStripMenuItem.Checked = False And has_length Then
                Poly_Node_row(i, 0) = (maxtime - Val(Poly_Node(i, 8))) * max_level / maxtime
            Else
                Poly_Node_row(i, 0) = Val(Poly_Node(i, 0))
            End If
            Poly_Node_row(i, 1) = Val(Poly_Node(i, 4))
            Poly_Node_row(i, 2) = Val(Poly_Node(i, 5))
            If Poly_Node(i, 1) <> "" Then
                Dim anc_terminal() As String = Poly_Node(i, 1).Split(New Char() {","c})
                For Each j As String In anc_terminal
                    If j <> "" Then
                        '末端分支横线
                        Poly_terminal_xy(CInt(j) - 1, 0) = (Array.IndexOf(taxon_array, j) + 1)
                        If ToplogyToolStripMenuItem.Checked = False And has_length Then
                            Poly_terminal_xy(CInt(j) - 1, 1) = (maxtime - TaxonTime(CInt(j) - 1, 1)) * max_level / maxtime
                            Poly_terminal_xy(CInt(j) - 1, 2) = (maxtime - Val(Poly_Node(i, 8))) * max_level / maxtime
                        Else
                            Poly_terminal_xy(CInt(j) - 1, 1) = 0
                            Poly_terminal_xy(CInt(j) - 1, 2) = Val(Poly_Node(i, 0))
                        End If

                    End If
                Next
            End If
        Next
        '根节点位置
        Poly_Node_col(NumofNode - 1, 0) = (Val(Poly_Node(NumofNode - 1, 4)) + Val(Poly_Node(NumofNode - 1, 5))) / 2
        If ToplogyToolStripMenuItem.Checked = False And has_length Then
            Poly_Node_col(NumofNode - 1, 1) = (maxtime - Val(Poly_Node(NumofNode - 1, 8))) * max_level / maxtime
            Poly_Node_col(NumofNode - 1, 2) = (maxtime - Val(Poly_Node(NumofNode - 1, 8))) * max_level / maxtime
        Else
            Poly_Node_col(NumofNode - 1, 1) = Val(Poly_Node(NumofNode - 1, 0))
            Poly_Node_col(NumofNode - 1, 2) = Val(Poly_Node(NumofNode - 1, 0))
        End If
    End Sub
    Public Sub make_chain(ByVal n As Integer)
        If Poly_Node(n, 2) <> "" Then
            Dim anc_node() As String = Poly_Node(n, 2).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    Poly_Node(CInt(j), 8) = (Val(Poly_Node(CInt(j), 7)) + Val(Poly_Node(n, 8))).ToString
                    make_chain(CInt(j))
                End If
            Next
        End If
        If Poly_Node(n, 1) <> "" Then
            Dim anc_node() As String = Poly_Node(n, 1).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    TaxonTime(CInt(j) - 1, 1) = (Val(TaxonTime(CInt(j) - 1, 0)) + Val(Poly_Node(n, 8))).ToString
                End If
            Next
        End If
    End Sub
    Private Sub TreeView_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' e.Cancel = True
        ' Me.Hide()
    End Sub

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        data_type = 0
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        CheckForIllegalCrossThreadCalls = False
        Loading = True
        Me.MinimizeBox = enableMin
        selected_nodes(0) = -1
        D_nodes(0) = -1
        R_nodes(0) = -1
        E_nodes(0) = -1
        G_nodes(0) = -1
        V_nodes(0) = -1
        Dim taxon_table As New System.Data.DataTable
        taxon_table.TableName = "Time Table"
        Dim Column_ID As New System.Data.DataColumn("Node")
        Dim Column_Length As New System.Data.DataColumn("Length")
        Dim Column_Time As New System.Data.DataColumn("Time")
        taxon_table.Columns.Add(Column_ID)
        taxon_table.Columns.Add(Column_Length)
        taxon_table.Columns.Add(Column_Time)

        time_Dataset.Tables.Add(taxon_table)

        time_view = time_Dataset.Tables("Time Table").DefaultView
        time_view.AllowNew = False
        time_view.AllowDelete = False
        time_view.AllowEdit = True
        DataGridView1.DataSource = time_view
        DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridView1.Columns(0).ReadOnly = True
        DataGridView1.Columns(0).Width = 30
        DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridView1.Columns(1).Width = 85
        DataGridView1.Columns(1).ReadOnly = True
        DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
        DataGridView1.Columns(2).Width = 55
        DataGridView1.Columns(2).ReadOnly = True

        Tree_font = OptionForm.FontDialog1.Font
        Label_font = OptionForm.FontDialog2.Font
        ID_font = OptionForm.FontDialog3.Font
        ID_color = OptionForm.FontDialog3.Color

        ListView1.Columns.Add("Range", 50, HorizontalAlignment.Left)
        ListView1.Columns.Add("Color", 100, HorizontalAlignment.Center)

        If StartTreeView Then
            'Try
            If Me.Visible = True And StartTreeView Then
                If NumofTaxon > 128 Then
                    File_zoom = 1
                End If
                draw_result = File.Exists(root_path + "temp" + path_char + "analysis_result.log")
                If show_my_tree <> "" Then
                    draw_result = False
                End If
                If draw_result And StartTreeView Then
                    Dim k As Integer = 0
                    Do
                        k += 1
                        If File.Exists(root_path + "temp" + path_char + "analysis_result_" + k.ToString + ".tmp") = False Then
                            File.Copy(root_path + "temp" + path_char + "analysis_result.log", root_path + "temp" + path_char + "analysis_result_" + k.ToString + ".tmp", True)
                            temp_result_file = root_path + "temp" + path_char + "analysis_result_" + k.ToString + ".tmp"
                            Me.Text = tree_view_title
                            k = -1
                        End If
                    Loop Until k = -1
                    read_results(temp_result_file)

                ElseIf StartTreeView = True Then
                    If show_my_tree = "" Then
                        If tree_show_with_value <> "" Then
                            show_my_tree = tree_show_with_value.Replace(";", "")
                            load_my_tree()
                        Else
                            StartTreeView = False
                            draw_result = False
                        End If
                    Else
                        load_my_tree()
                    End If

                End If
            End If
            For i As Integer = 0 To NumofTaxon - 1
                If IsNumeric(Distribution(i)) Then
                    current_state_mode = 1
                    Exit For
                End If
            Next
            If current_state_mode = 1 Then
                For i As Integer = 0 To NumofTaxon - 1
                    If IsNumeric(Distribution(i)) Then
                        If CSng(Distribution(i)) > max_dis_value Then
                            max_dis_value = CSng(Distribution(i))
                        End If
                        If CSng(Distribution(i)) < min_dis_value Then
                            min_dis_value = CSng(Distribution(i))
                        End If
                    End If
                Next
            End If
            If StartTreeView Then
                Loading = False
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
                If tree_view_limit Then
                    tree_view_limit = False
                    TabControl1.Visible = False
                    TabControl2.Dock = DockStyle.Fill
                    TabControl2.TabPages.RemoveAt(2)
                    TabControl2.TabPages.RemoveAt(1)
                    IncreaseTreeToolStripMenuItem_Click(sender, e)
                End If
            End If
        End If
    End Sub
    Public Sub load_my_tree()
        NumofTaxon = show_my_tree.Length - show_my_tree.Replace(",", "").Length + 1
        NumofNode = show_my_tree.Length - show_my_tree.Replace("(", "").Length

        ReDim TaxonName(NumofTaxon - 1)
        ReDim Distribution(NumofTaxon - 1)
        For i As Integer = 0 To NumofTaxon - 1
            TaxonName(i) = dtView.Item(i).Item(1).ToString
            Distribution(i) = dtView.Item(i).Item(current_state).ToString
            max_taxon_name = Math.Max(max_taxon_name, TaxonName(i).Length)
        Next

        ReDim Current_AreaS(NumofNode - 1, 32)
        ReDim Current_AreaP(NumofNode - 1, 32)
        ReDim Color_S(0)
        ReDim Color_B(0)
        Color_S(0) = "*"
        Color_B(0) = Brushes.Black

        For i As Integer = 0 To RangeLength - 1
            ReDim Preserve Color_S(UBound(Color_S) + 1)
            ReDim Preserve Color_B(UBound(Color_S))
            Color_S(UBound(Color_S)) = ChrW(65 + i)
        Next
        Color_S_node = Color_S.Clone
        Color_B_node = Color_B.Clone
        For Each i As String In Distribution
            If Array.IndexOf(Color_S, sort_area(i)) < 0 Then
                ReDim Preserve Color_S(UBound(Color_S) + 1)
                ReDim Preserve Color_B(UBound(Color_S))
                Color_S(UBound(Color_S)) = sort_area(i)
            End If
        Next
        load_color()
        Read_Poly_Tree(show_my_tree)
    End Sub
    Public Sub SortNum(ByRef input_array() As Object)
        For i As Integer = 0 To UBound(input_array)
            For j As Integer = i + 1 To UBound(input_array)
                If IsNumeric(input_array(i)) And IsNumeric(input_array(j)) Then
                    If CInt(input_array(i)) > CInt(input_array(j)) Then
                        Dim t As Integer = CInt(input_array(i))
                        input_array(i) = input_array(j)
                        input_array(j) = t.ToString
                    End If
                End If
                If IsNumeric(input_array(j)) And IsNumeric(input_array(i)) = False Then
                    If input_array(i) <> "*" And input_array(i) <> "" And input_array(i) <> "\" Then
                        Dim t As String = input_array(i)
                        input_array(i) = input_array(j)
                        input_array(j) = t.ToString
                    End If
                End If
                If IsNumeric(input_array(j)) = False And IsNumeric(input_array(i)) = False Then
                    If input_array(i) > input_array(j) Then
                        Dim t As String = input_array(i)
                        input_array(i) = input_array(j)
                        input_array(j) = t
                    End If
                End If
            Next
        Next
    End Sub
    Public Sub load_color()

        If data_type = 1 Then
            SortNum(Color_S)
        End If
        For i As Integer = 0 To Color_S.Length - 1
            Color_B(i) = Int2Brushes(Distributiton_to_Integer(Color_S(i)))
        Next
        If data_type = 1 Then
            SortNum(Color_S_node)
        End If
        For i As Integer = 0 To Color_S_node.Length - 1
            Color_B_node(i) = Int2Brushes(Distributiton_to_Integer(Color_S_node(i)))
        Next
        ListView1.Items.Clear()
        If ShowPieOnTerminalToolStripMenuItem.Checked Then
            If Color_S_node.Length > 1 Then
                For i As Integer = 0 To Color_S_node.Length - 1
                    ListView1.Items.Add(Color_S_node(i)).UseItemStyleForSubItems = False
                    With ListView1.Items(i).SubItems
                        With .Add("Double Click")
                            .BackColor = New Pen(Color_B_node(i)).Color
                            .ForeColor = Color.WhiteSmoke
                        End With
                    End With
                Next
            End If
        Else
            If Color_S.Length > 1 Then
                For i As Integer = 0 To Color_S.Length - 1
                    ListView1.Items.Add(Color_S(i)).UseItemStyleForSubItems = False
                    With ListView1.Items(i).SubItems
                        With .Add("Double Click")
                            .BackColor = New Pen(Color_B(i)).Color
                            .ForeColor = Color.WhiteSmoke
                        End With
                    End With
                Next
            End If
        End If
        Array.Sort(Color_S, Color_B)
        Array.Sort(Color_S_node, Color_B_node)
    End Sub
    Dim PROB_list(,) As Single
    Dim reconstr_path As String
    Public Sub read_results(ByVal path As String)
        ComboBox2.Items.Clear()
        ComboBox2.Items.Add("Event")
        RadioButton1.Checked = True
        RadioButton2.Enabled = False
        begin_draw = False
        Dim has_rec As Boolean = False
        Dim sr As StreamReader
        sr = New StreamReader(path)
        Dim line As String = ""
        data_type = 0
        Do
            line = sr.ReadLine
            If line.Contains("(Number)") Then
                data_type = 1
            End If
        Loop Until line.ToUpper.StartsWith("[TAXON]")
        line = sr.ReadLine
        Dim Temparray As Integer = 1
        ReDim Distribution(1)
        ReDim TaxonName(1)
        TaxonName(0) = line.Split(New Char() {"	"c})(1)
        max_taxon_name = TaxonName(0).Length
        Distribution(0) = line.Split(New Char() {"	"c})(2).ToUpper
        Dim temp_range As String = ""
        line = sr.ReadLine
        Do
            ReDim Preserve Distribution(Temparray)
            ReDim Preserve TaxonName(Temparray)
            TaxonName(Temparray) = line.Split(New Char() {"	"c})(1)
            max_taxon_name = Max(max_taxon_name, TaxonName(Temparray).Length)
            Distribution(Temparray) = line.Split(New Char() {"	"c})(2).ToUpper

            Temparray += 1
            line = sr.ReadLine
        Loop Until line.ToUpper.StartsWith("[TREE]")
        For i As Integer = 0 To Temparray - 1
            For Each c As Char In Distribution(i)
                If temp_range.Contains(c) = False And c <> "/" Then
                    temp_range += c
                End If
            Next
        Next
        RangeLength = temp_range.Length
        NumofTaxon = Temparray
        show_my_tree = sr.ReadLine.Split(New Char() {"="c})(1).Replace(";", "")
        NumofNode = show_my_tree.Length - show_my_tree.Replace("(", "").Length
        Read_Poly_Tree(show_my_tree)
        Temparray = 1

        ListBox1.Items.Clear()
        line = sr.ReadLine
        line = sr.ReadLine
        ListBox1.Items.Add(line)
        line = sr.ReadLine
        ListBox1.Items.Add(line.Split(New Char() {"("c})(0))
        ReDim Preserve Result_list(1) 'Result_list 
        Result_list(0) = line.Split(New Char() {":"c})(1)
        line = sr.ReadLine
        Dim reading_result As Boolean = True
        Do
            If line.StartsWith("reconstruction") Then
                has_rec = True
                reconstr_path = path
                Exit Do
            End If
            If line.StartsWith("[") Then
                reading_result = False
            End If
            If reading_result Then
                If line.StartsWith("node") Then
                    ReDim Preserve Result_list(Temparray)
                    Result_list(Temparray) = line.Split(New Char() {":"c})(1)
                    Temparray += 1
                    ListBox1.Items.Add(line.Split(New Char() {"("c})(0))
                ElseIf line.StartsWith("[END]") = False Then
                    ListBox1.Items.Add(line)
                End If
            End If
            line = sr.ReadLine
            If line <> "" Then
                If line.ToUpper.StartsWith("[PROBABILITY]") Then
                    ComboBox2.Items.Add("Area")
                    RadioButton2.Enabled = True
                    line = sr.ReadLine
                    ReDim PROB_list(NumofNode, RangeLength)
                    line = sr.ReadLine
                    Temparray = 0
                    Do
                        If line.ToUpper.StartsWith("[END]") Then
                            Exit Do
                        End If
                        For i As Integer = 1 To RangeLength
                            PROB_list(Temparray, i) = line.Split(New Char() {"	"c})(i * 2)
                        Next
                        Temparray += 1
                        line = sr.ReadLine
                    Loop Until line = ""
                End If
                If line.ToUpper.StartsWith("[TREEVIEW]") Then
                    line = sr.ReadLine
                    Do
                        If line.ToUpper.StartsWith("[END]") Then
                            dorefresh = True
                            Exit Do
                        End If
                        If line.Contains("=") Then
                            Dim var_name As String = line.Substring(0, line.IndexOf("="))
                            Dim var_value As String = line.Substring(line.IndexOf("=") + 1)
                            If var_name = "ShowScale" Then
                                ShowScale = str_to_bool(var_value)
                            End If
                            If var_name = "TransparentBG" Then
                                TransparentBG = str_to_bool(var_value)
                            End If
                            If var_name = "area_lower" Then
                                area_lower = var_value
                            End If
                            If var_name = "keep_at_least" Then
                                keep_at_least = var_value
                            End If
                            If var_name = "Show_area_names " Then
                                Show_area_names = str_to_bool(var_value)
                            End If
                            If var_name = "Show_area_pies" Then
                                Show_area_pies = str_to_bool(var_value)
                            End If
                            If var_name = "pie_radii" Then
                                pie_radii = var_value
                            End If
                            If var_name = "Display_taxon_names" Then
                                Display_taxon_names = str_to_bool(var_value)
                            End If
                            If var_name = "Display_Null_distribution" Then
                                Display_Null_distribution = str_to_bool(var_value)
                            End If
                            If var_name = "Display_taxon_dis " Then
                                Display_taxon_dis = str_to_bool(var_value)
                            End If
                            If var_name = "Display_taxon_pie" Then
                                Display_taxon_pie = str_to_bool(var_value)
                            End If
                            If var_name = "Display_circle" Then
                                Display_circle = str_to_bool(var_value)
                            End If
                            If var_name = "Circle_size" Then
                                Circle_size = var_value
                            End If
                            If var_name = "Circle_color" Then
                                Circle_color = str_to_color(var_value)
                            End If
                            If var_name = "Tree_font" Then
                                Tree_font = str_to_font(var_value)
                            End If
                            If var_name = "Label_font" Then
                                Label_font = str_to_font(var_value)
                            End If
                            If var_name = "ID_font" Then
                                ID_font = str_to_font(var_value)
                            End If
                            If var_name = "ID_color" Then
                                ID_color = str_to_color(var_value)
                            End If
                            If var_name = "Display_node_frequency" Then
                                Display_node_frequency = str_to_bool(var_value)
                            End If
                            If var_name = "Low_frequency" Then
                                Low_frequency = CSng(var_value)
                            End If
                            If var_name = "Hide_pie" Then
                                Hide_pie = var_value
                            End If
                            If var_name = "Display_lines" Then
                                Display_lines = str_to_bool(var_value)
                            End If
                            If var_name = "frequency_h" Then
                                frequency_h = var_value
                            End If
                            If var_name = "frequency_v" Then
                                frequency_v = var_value
                            End If
                            If var_name = "Display_node_ID" Then
                                Display_node_ID = str_to_bool(var_value)
                            End If
                            If var_name = "node_h" Then
                                node_h = var_value
                            End If
                            If var_name = "node_v" Then
                                node_v = var_value
                            End If
                            If var_name = "Taxon_separation" Then
                                Taxon_separation = var_value
                            End If
                            If var_name = "Branch_length" Then
                                Branch_length = var_value
                            End If
                            If var_name = "Border_separation" Then
                                Border_separation = var_value
                            End If
                            If var_name = "Line_width" Then
                                Line_width = var_value
                            End If
                            If var_name = "File_zoom" Then
                                File_zoom = var_value
                            End If
                            If var_name = "taxon_pie_radii" Then
                                taxon_pie_radii = var_value
                            End If

                        End If
                        line = sr.ReadLine
                    Loop Until line = ""
                End If
                If line <> "" Then
                    If line.ToUpper.StartsWith("[SUPPLEMENT]") Then
                        Dim info_text As String = ""

                        Do
                            line = sr.ReadLine
                            If line.ToUpper.StartsWith("[END]") Then
                                Exit Do
                            End If
                            If line <> "" Then
                                info_text += line + vbCrLf
                            End If
                        Loop Until line.ToUpper.StartsWith("[END]")
                        RichTextBox1.Text = info_text
                    End If
                End If
            End If
        Loop Until line = ""
        sr.Close()
        '[加载结果r1]
        result_ID = 1
        Load_result()
        cale_relation()
        Global_Info()
        TextBox1.Text = Global_Text
        begin_draw = True
        ListBox1.SelectedIndex = (result_ID - 1) * (NumofNode + 1)
        ComboBox2.SelectedIndex = 0
        If has_rec Then
            Dim result1 As DialogResult = MessageBox.Show("Load reconstructions with maximal S-DIVA value only?",
            "Load reconstructions",
            MessageBoxButtons.YesNo)
            If result1 = MsgBoxResult.Yes Then
                Dim th1 As New Thread(AddressOf read_max_reconstruction)
                th1.Start()
            Else
                Dim th1 As New Thread(AddressOf read_all_reconstruction)
                th1.Start()
            End If

        End If
    End Sub

    Public Sub read_all_reconstruction()
        Dim sr As StreamReader
        sr = New StreamReader(reconstr_path)
        Dim line As String = ""
        Do
            line = sr.ReadLine
        Loop Until line.StartsWith("reconstruction")
        Do
            If line.StartsWith("node") Then
                ReDim Preserve Result_list(UBound(Result_list) + 1)
                Result_list(UBound(Result_list)) = line.Split(New Char() {":"c})(1)
                ListBox1.Items.Add(line.Split(New Char() {"("c})(0))
            Else
                ListBox1.Items.Add(line)
            End If
            line = sr.ReadLine
        Loop Until line = ""
        sr.Close()
        '[加载结果r1]
    End Sub
    Public Sub read_max_reconstruction()
        Dim temp_value As Single = 0
        Dim max_value As Single = -1
        Dim max_array(0) As String

        Dim sr As StreamReader
        sr = New StreamReader(reconstr_path)
        Dim line As String = ""
        Do
            line = sr.ReadLine
        Loop Until line.StartsWith("reconstruction")

        Do
            If line.StartsWith("reconstruction") Then
                temp_value = CSng(line.Split(New Char() {"="c})(1))
                If temp_value = max_value Then
                    Dim temp As Integer = UBound(max_array)
                    ReDim Preserve max_array(temp + NumofTaxon)
                    max_array(temp + 1) = line
                    For i As Integer = (temp + 2) To (temp + NumofTaxon)
                        line = sr.ReadLine
                        max_array(i) = line
                    Next
                End If
                If temp_value > max_value Then
                    max_value = temp_value
                    ReDim max_array(NumofNode)
                    max_array(0) = line
                    For i As Integer = 1 To NumofNode
                        line = sr.ReadLine
                        max_array(i) = line
                    Next
                End If
            End If
            line = sr.ReadLine
        Loop Until line = ""
        sr.Close()
        For Each str_line As String In max_array
            If str_line.StartsWith("node") Then
                ReDim Preserve Result_list(UBound(Result_list) + 1)
                Result_list(UBound(Result_list)) = str_line.Split(New Char() {":"c})(1)
                ListBox1.Items.Add(str_line.Split(New Char() {"("c})(0))
            Else
                ListBox1.Items.Add(str_line)
            End If
        Next

        '[加载结果r1]
    End Sub
    Public Sub cale_relation()
        ReDim Node_Relationship(NumofNode, 2) '0,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率
        For i As Integer = 0 To NumofNode - 1
            Node_Relationship(i, 0) = Get_node(i)
            Node_Relationship(i, 1) = Poly_Node(i, 1)
            For Each j As String In Node_Relationship(i, 0).Split(New Char() {","c})
                If j <> "" Then
                    If Result_list(CInt(j)) = "" Then
                        If Poly_Node(CInt(j), 1) <> "" Then
                            Node_Relationship(i, 1) = Node_Relationship(i, 1) + Poly_Node(CInt(j), 1)
                        End If
                    ElseIf Result_list(CInt(j)).Split(New Char() {" "c})(1) = "" Or Result_list(CInt(j)).Split(New Char() {" "c})(1) = "/" Then
                        If Poly_Node(CInt(j), 1) <> "" Then
                            Node_Relationship(i, 1) = Node_Relationship(i, 1) + Poly_Node(CInt(j), 1)
                        End If
                    Else
                        Node_Relationship(i, 2) = Node_Relationship(i, 2) + j + ","
                    End If
                End If
            Next
        Next
    End Sub
    Public Function Get_node(ByVal n As Integer) As String
        Dim node As String = ""
        If Poly_Node(n, 2) <> "" Then
            Dim temp_ter() As String = Poly_Node(n, 2).Split(New Char() {","c})
            For Each k As String In temp_ter
                If k <> "" Then
                    If Result_list(CInt(k)) = "" Then
                        node = node + k + "," + Get_node(CInt(k))
                    ElseIf Result_list(CInt(k)).Split(New Char() {" "c})(1) = "" Or Result_list(CInt(k)).Split(New Char() {" "c})(1) = "/" Then
                        node = node + k + "," + Get_node(CInt(k))
                    Else
                        node = node + k + ","
                    End If
                End If
            Next
        End If
        Return node
    End Function
    Public Sub Load_result()
        ReDim Current_AreaS(NumofNode - 1, 32)
        ReDim Current_AreaP(NumofNode - 1, 32)
        ReDim Color_S(0)
        ReDim Color_B(0)
        Color_S(0) = "*"
        Color_B(0) = Brushes.Black
        If data_type = 0 Then
            For i As Integer = 0 To RangeLength - 1
                ReDim Preserve Color_S(UBound(Color_S) + 1)
                ReDim Preserve Color_B(UBound(Color_S))
                Color_S(UBound(Color_S)) = ChrW(65 + i)
            Next
            'For Each i As String In Distribution
            '    If Array.IndexOf(Color_S, sort_area(i)) < 0 Then
            '        ReDim Preserve Color_S(UBound(Color_S) + 1)
            '        ReDim Preserve Color_B(UBound(Color_S))
            '        Color_S(UBound(Color_S)) = sort_area(i)
            '    End If
            'Next
        End If

        For i As Integer = NumofNode * (result_ID - 1) To NumofNode * result_ID - 1
            Dim Templist() As String = Result_list(i).Split(New Char() {" "c})
            Dim Temp_array As Integer = 0
            Dim Temp_sum As Single = 0
            For j As Integer = 1 To UBound(Templist) Step 2
                If j <= 64 Then
                    If Templist(j + 1) >= area_lower Or Temp_array < keep_at_least Then
                        If Templist(j) = "" Then
                            Templist(j) = "/"
                        End If
                        Current_AreaS(CInt(i Mod NumofNode), CInt((j - 1) / 2)) = Templist(j)
                        Current_AreaP(CInt(i Mod NumofNode), CInt((j - 1) / 2)) = Templist(j + 1)
                        Temp_array += 1
                        Temp_sum += Templist(j + 1)
                        If Array.IndexOf(Color_S, Templist(j)) < 0 Then
                            ReDim Preserve Color_S(UBound(Color_S) + 1)
                            ReDim Preserve Color_B(UBound(Color_S))
                            Color_S(UBound(Color_S)) = Templist(j)
                        End If
                    End If
                Else
                    Exit For
                End If

            Next
            Current_AreaP(i Mod NumofNode, Temp_array) = 100 - Temp_sum
            Current_AreaS(i Mod NumofNode, Temp_array) = "*"
        Next
        Color_S_node = Color_S.Clone
        Color_B_node = Color_B.Clone
        For Each i As String In Distribution
            If Array.IndexOf(Color_S, sort_area(i)) < 0 Then
                ReDim Preserve Color_S(UBound(Color_S) + 1)
                ReDim Preserve Color_B(UBound(Color_S))
                Color_S(UBound(Color_S)) = sort_area(i)
            End If
        Next
        load_color()

    End Sub

    Private Sub TreeView_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If Loading = False Then
            If dorefresh Then
                dorefresh = False
                If ListBox1.Items.Count > 0 And begin_draw Then
                    Load_result()
                End If
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))

                PictureBox1.Refresh()
                PictureBox2.Height = PicBox2_High()
                Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
                draw_col(Graphics.FromImage(Bitmap_Legend), 1)
                PictureBox2.Refresh()
            Else
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
            End If
        End If

    End Sub
    Public Sub draw_tree(ByVal TempGrap As Object)
        make_tree_xy()
        Dim Poly_terminal_xy_draw(,) As Single
        Dim Poly_Node_row_draw(,) As Single
        Dim Poly_Node_col_draw(,) As Single
        ReDim Poly_Node_row_draw(NumofNode - 1, 2)
        ReDim Poly_Node_col_draw(NumofNode - 1, 2)
        ReDim Poly_terminal_xy_draw(NumofTaxon - 1, 2)
        Dim Tree_pen As New System.Drawing.Pen(System.Drawing.Color.Black, Line_width)
        Dim Select_pen As New System.Drawing.Pen(System.Drawing.Color.Red, Line_width * 2)
        Dim Line_pen As New System.Drawing.Pen(System.Drawing.Color.LightGray, Max(1, CInt(Line_width / 2)))
        Dim startpoint As Integer = (max_level + 2) * Branch_length + Border_separation 'x

        For i As Integer = 0 To NumofTaxon - 1
            Poly_terminal_xy_draw(i, 0) = Poly_terminal_xy(i, 0) * Taxon_separation + Taxon_separation + Label_font.Height 'y
            Poly_terminal_xy_draw(i, 1) = Poly_terminal_xy(i, 1) * Branch_length 'x1
            Poly_terminal_xy_draw(i, 2) = Poly_terminal_xy(i, 2) * Branch_length 'x2
        Next
        For i As Integer = 0 To NumofNode - 1
            Poly_Node_row_draw(i, 0) = Poly_Node_row(i, 0) * Branch_length 'x
            Poly_Node_row_draw(i, 1) = Poly_Node_row(i, 1) * Taxon_separation + Taxon_separation + Label_font.Height 'y1
            Poly_Node_row_draw(i, 2) = Poly_Node_row(i, 2) * Taxon_separation + Taxon_separation + Label_font.Height  'y2
            Poly_Node_col_draw(i, 0) = Poly_Node_col(i, 0) * Taxon_separation + Taxon_separation + Label_font.Height  'y
            Poly_Node_col_draw(i, 1) = Poly_Node_col(i, 1) * Branch_length 'x1
            Poly_Node_col_draw(i, 2) = Poly_Node_col(i, 2) * Branch_length 'x2

        Next
        '背景透明
        If TransparentBG = False And savingpic Then
            TempGrap.FillRectangle(Brushes.White, 0, 0, PictureBox1.Width * File_zoom, PictureBox1.Height * File_zoom)
        ElseIf savingpic = False Then
            TempGrap.FillRectangle(Brushes.White, 0, 0, PictureBox1.Width * File_zoom, PictureBox1.Height * File_zoom)
        End If
        '时间轴
        If has_length And ToplogyToolStripMenuItem.Checked = False And ShowScale Then
            TempGrap.DrawLine(Line_pen, startpoint - Poly_Node_col_draw(NumofNode - 1, 1), (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height, startpoint, (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height)
            TempGrap.DrawLine(Line_pen, startpoint - Poly_Node_col_draw(NumofNode - 1, 1), Border_separation + Label_font.Height, startpoint, Border_separation + Label_font.Height)

            TempGrap.DrawString((maxtime * root_time).ToString("F0"), Label_font, Brushes.Black, startpoint - Poly_Node_col_draw(NumofNode - 1, 1), (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height)
            TempGrap.DrawString("0", Label_font, Brushes.Black, startpoint, (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height)
            For i As Integer = 0 To maxtime * root_time * smooth_x Step NumericUpDown1.Value * smooth_x
                Dim steptime As Integer = i * Poly_Node_col_draw(NumofNode - 1, 1) / maxtime / root_time / smooth_x
                TempGrap.DrawLine(Line_pen, startpoint - steptime, (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height, startpoint - steptime, Border_separation + Label_font.Height)
                TempGrap.DrawString((i / smooth_x).ToString("F0"), Label_font, Brushes.Black, startpoint - steptime, (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height)
            Next
            TempGrap.DrawLine(Line_pen, startpoint - Poly_Node_col_draw(NumofNode - 1, 1), (NumofTaxon + 2) * Taxon_separation + Border_separation + Label_font.Height, startpoint - Poly_Node_col_draw(NumofNode - 1, 1), Border_separation + Label_font.Height)
        End If
        '绘制树干
        If Display_lines Then
            For i As Integer = 0 To NumofNode - 1
                If i <> Selected_node Then
                    TempGrap.DrawLine(Tree_pen, startpoint - CInt(Poly_Node_row_draw(i, 0)), CInt(Poly_Node_row_draw(i, 1)), startpoint - CInt(Poly_Node_row_draw(i, 0)), CInt(Poly_Node_row_draw(i, 2)))
                    TempGrap.DrawLine(Tree_pen, startpoint - CInt(Poly_Node_col_draw(i, 1)), CInt(Poly_Node_col_draw(i, 0)), startpoint - CInt(Poly_Node_col_draw(i, 2)), CInt(Poly_Node_col_draw(i, 0)))
                Else
                    TempGrap.DrawLine(Select_pen, startpoint - CInt(Poly_Node_row_draw(i, 0)), CInt(Poly_Node_row_draw(i, 1)), startpoint - CInt(Poly_Node_row_draw(i, 0)), CInt(Poly_Node_row_draw(i, 2)))
                    TempGrap.DrawLine(Select_pen, startpoint - CInt(Poly_Node_col_draw(i, 1)), CInt(Poly_Node_col_draw(i, 0)), startpoint - CInt(Poly_Node_col_draw(i, 2)), CInt(Poly_Node_col_draw(i, 0)))
                End If
            Next

            For i As Integer = 0 To NumofTaxon - 1
                TempGrap.DrawLine(Tree_pen, startpoint - CInt(Poly_terminal_xy_draw(i, 1)), CInt(Poly_terminal_xy_draw(i, 0)), startpoint - CInt(Poly_terminal_xy_draw(i, 2)), CInt(Poly_terminal_xy_draw(i, 0)))
            Next
        End If

        TempGrap.SmoothingMode = SmoothingMode.AntiAlias

        If draw_result Then
            TempGrap.DrawString(ListBox1.Items((result_ID - 1) * (NumofNode + 1)), Label_font, Brushes.Black, Border_separation, 1)
            For j As Integer = 0 To NumofNode - 1
                If RadioButton2.Checked Then
                    If Show_area_pies Then
                        '填充饼图
                        Dim pie_x As Integer = startpoint - CInt(Poly_Node_col_draw(j, 1))
                        Dim pie_y As Integer = CInt(Poly_Node_col_draw(j, 0))
                        Dim sRectPer() As Single
                        ReDim sRectPer(RangeLength)
                        For i As Integer = 0 To RangeLength
                            sRectPer(i) = CSng(359.99) / RangeLength * i
                        Next
                        TempGrap.FillPie(Brushes.LightGray, pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2, 0, CSng(359.99))
                        For n As Integer = 1 To RangeLength
                            If PROB_list(j, n) > 0 Then
                                TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(Chr(64 + n))), pie_x - pie_radii * PROB_list(j, n), pie_y - pie_radii * PROB_list(j, n), pie_radii * 2 * PROB_list(j, n), pie_radii * 2 * PROB_list(j, n), sRectPer(n - 1), sRectPer(n) - sRectPer(n - 1))
                            End If
                        Next
                        TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                    End If
                Else
                    Dim sRectPer() As Single, pec() As Single
                    Dim pie_x As Integer = startpoint - CInt(Poly_Node_col_draw(j, 1))
                    Dim pie_y As Integer = CInt(Poly_Node_col_draw(j, 0))
                    ReDim sRectPer(32)
                    ReDim pec(32)

                    pec(0) = Current_AreaP(j, 0)
                    For i As Integer = 1 To 32
                        pec(i) = pec(i - 1) + Current_AreaP(j, i)
                    Next
                    For i As Integer = 0 To 32
                        sRectPer(i) = CSng(359.99) * pec(i) / 100
                    Next
                    Dim x2() As Single, y2() As Single
                    ReDim x2(32)
                    ReDim y2(32)

                    If Show_area_pies Then
                        If CSng(Poly_Node(j, 6)) >= Hide_pie Then
                            If MostLikelyStatesOnlyToolStripMenuItem.Checked Then '只显示最有可能的分布
                                '填充饼图
                                If Display_Null_distribution = False Or Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))) IsNot Brushes.Black Then
                                    If KeyStatesOnlyToolStripMenuItem.Checked = False And EventNodesOnlyToolStripMenuItem.Checked = False Then
                                        TempGrap.FillEllipse(Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                        '边缘
                                        TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                    End If
                                    If EventNodesOnlyToolStripMenuItem.Checked Then
                                        Dim draw_pie As Boolean = False
                                        If CheckBox5.Checked Then
                                            If Array.IndexOf(V_nodes, j + 1) >= 0 Then
                                                draw_pie = True
                                            End If
                                        End If
                                        If CheckBox7.Checked Then
                                            If Array.IndexOf(E_nodes, j + 1) >= 0 Then
                                                draw_pie = True
                                            End If
                                        End If
                                        If CheckBox6.Checked Then
                                            If Array.IndexOf(G_nodes, j + 1) >= 0 Then
                                                draw_pie = True
                                            End If
                                        End If
                                        If draw_pie Then
                                            TempGrap.FillEllipse(Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                            '边缘
                                            TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                        End If
                                    End If

                                    If KeyStatesOnlyToolStripMenuItem.Checked Then
                                        Dim draw_pie As Boolean = False
                                        If Poly_Node(j, 9) <> "" Then
                                            If Current_AreaS(j, 0) <> Current_AreaS(CInt(Poly_Node(j, 9)), 0) Then
                                                draw_pie = True
                                            End If
                                        Else
                                            draw_pie = True
                                        End If

                                        If Poly_Node(j, 1) <> "" Then
                                            Dim anc_node() As String = Poly_Node(j, 1).Split(New Char() {","c})
                                            For Each k As String In anc_node
                                                If k <> "" Then
                                                    If Current_AreaS(j, 0) <> Distribution(CInt(k) - 1) Then
                                                        draw_pie = True
                                                    End If
                                                End If
                                            Next
                                        End If

                                        If Poly_Node(j, 2) <> "" Then
                                            Dim anc_node() As String = Poly_Node(j, 2).Split(New Char() {","c})
                                            For Each k As String In anc_node
                                                If k <> "" Then
                                                    If Current_AreaS(j, 0) <> Current_AreaS(CInt(k), 0) Then
                                                        draw_pie = True
                                                    End If
                                                End If
                                            Next
                                        End If
                                        If draw_pie Then
                                            TempGrap.FillEllipse(Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                            '边缘
                                            TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                        End If
                                    End If
                                End If

                            Else '显示所有分布
                                '填充饼图
                                If Display_Null_distribution = True Or (Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))) IsNot Brushes.Black) Then
                                    TempGrap.FillPie(Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2, 0, sRectPer(0))
                                    For n As Integer = 1 To 32
                                        If Current_AreaS(j, n) <> "" Then
                                            If (CInt(sRectPer(n) - sRectPer(n - 1)) > 0 Or Current_AreaS(j, n) <> "*") And CInt(sRectPer(n - 1)) <> CSng(359.99) Then
                                                TempGrap.FillPie(Color_B(Array.IndexOf(Color_S, Current_AreaS(j, n))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2, CInt(sRectPer(n - 1)), CInt(sRectPer(n) - sRectPer(n - 1)))
                                            End If
                                        Else
                                            Exit For
                                        End If
                                    Next
                                    '边缘
                                    TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)
                                End If
                            End If
                        End If
                    End If
                    If Display_circle Then
                        TempGrap.FillEllipse(New SolidBrush(Circle_color), pie_x - Circle_size, pie_y - Circle_size, Circle_size * 2, Circle_size * 2)
                        '边缘
                        TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - Circle_size, pie_y - Circle_size, Circle_size * 2, Circle_size * 2)
                    End If
                    '显示分布区
                    Dim node_font As New System.Drawing.Font(Label_font.FontFamily, Label_font.Size, FontStyle.Regular)
                    If Show_area_names Or DisplayMostMLSInCenterToolStripMenuItem.Checked Then
                        If MostLikelyStatesOnlyToolStripMenuItem.Checked Or DisplayMostMLSInCenterToolStripMenuItem.Checked Then
                            x2(0) = pie_x - Current_AreaS(j, 0).Length * node_font.SizeInPoints / 2
                            y2(0) = pie_y - node_font.Height / 2
                            TempGrap.DrawString(Current_AreaS(j, 0), node_font, Brushes.Black, x2(0), y2(0))
                        Else
                            x2(0) = CInt(Math.Cos(3.14 * pec(0) / 100 + 3.14) * pie_radii)
                            y2(0) = CInt(Math.Sin(3.14 * pec(0) / 100 + 3.14) * pie_radii)
                            If x2(0) >= 0 And y2(0) >= 0 Then
                                x2(0) = (pie_x - x2(0)) - Current_AreaS(j, 0).Length * node_font.SizeInPoints / 1.7
                                y2(0) = (pie_y - y2(0)) - node_font.Height
                            ElseIf x2(0) >= 0 And y2(0) < 0 Then
                                x2(0) = (pie_x - x2(0)) - Current_AreaS(j, 0).Length * node_font.SizeInPoints / 1.7
                                y2(0) = (pie_y - y2(0))
                            ElseIf x2(0) < 0 And y2(0) >= 0 Then
                                x2(0) = (pie_x - x2(0))
                                y2(0) = (pie_y - y2(0)) - node_font.Height
                            ElseIf x2(0) < 0 And y2(0) < 0 Then
                                x2(0) = (pie_x - x2(0))
                                y2(0) = (pie_y - y2(0))
                            End If

                            ' TempGrap.DrawString(Current_AreaS(j, 0), node_font, Color_B(Array.IndexOf(Color_S, Current_AreaS(j, 0))), x2(0), y2(0))
                            TempGrap.DrawString(Current_AreaS(j, 0), node_font, Brushes.Black, x2(0), y2(0))

                            For n As Integer = 1 To 32
                                If Current_AreaS(j, n) <> "" Then
                                    If (sRectPer(n) - sRectPer(n - 1)) > 0 Or Current_AreaS(j, n) <> "*" Then
                                        x2(n) = CInt((Math.Cos(3.14 * pec(n) / 100 + 3.14 * pec(n - 1) / 100 + 3.14)) * pie_radii)
                                        y2(n) = CInt((Math.Sin(3.14 * pec(n) / 100 + 3.14 * pec(n - 1) / 100 + 3.14)) * pie_radii)
                                        If x2(n) >= 0 And y2(n) >= 0 Then
                                            x2(n) = (pie_x - x2(n)) - Current_AreaS(j, n).Length * node_font.SizeInPoints / 1.7
                                            y2(n) = (pie_y - y2(n)) - node_font.Height
                                        ElseIf x2(n) >= 0 And y2(n) < 0 Then
                                            x2(n) = (pie_x - x2(n)) - Current_AreaS(j, n).Length * node_font.SizeInPoints / 1.7
                                            y2(n) = (pie_y - y2(n))
                                        ElseIf x2(n) < 0 And y2(n) >= 0 Then
                                            If x2(n) / pie_radii > -0.8 Then
                                                y2(n) = (pie_y - y2(n)) - node_font.Height
                                            Else
                                                y2(n) = (pie_y - y2(n)) - 0.5 * node_font.Height
                                            End If
                                            x2(n) = (pie_x - x2(n))
                                        ElseIf x2(n) < 0 And y2(n) < 0 Then
                                            x2(n) = (pie_x - x2(n))
                                            y2(n) = (pie_y - y2(n))
                                        End If

                                        If y2(n) - y2(n - 1) < 0 And y2(n) - y2(n - 1) > -node_font.Height Then
                                            y2(n) = y2(n - 1) - node_font.Height / 2
                                        End If
                                        If y2(n) - y2(n - 1) >= 0 And y2(n) - y2(n - 1) < node_font.Height Then
                                            y2(n) = y2(n - 1) + node_font.Height / 2
                                        End If

                                        If Current_AreaS(j, n) <> "*" Then
                                            If CSng(Poly_Node(j, 6)) >= Hide_pie Then
                                                '与区域色彩一致
                                                'TempGrap.DrawString(Current_AreaS(j, n), node_font, Color_B(Array.IndexOf(Color_S, Current_AreaS(j, n))), x2(n), y2(n))
                                                TempGrap.DrawString(Current_AreaS(j, n), node_font, Brushes.Black, x2(n), y2(n))
                                            End If
                                        End If
                                    Else
                                        Exit For
                                    End If
                                Else
                                    Exit For
                                End If
                            Next
                        End If

                    End If
                End If
            Next
            '被选择的描红
            For Each i As Integer In selected_nodes
                If i > 0 Then
                    TempGrap.DrawEllipse(New Pen(Color.Red, 2), startpoint - CInt(Poly_Node_col_draw(i - 1, 1)) - pie_radii, CInt(Poly_Node_col_draw(i - 1, 0)) - pie_radii, pie_radii * 2, pie_radii * 2)
                End If
            Next

            'DREG and DIVA
            If EventNodesOnlyToolStripMenuItem.Checked = False Then

                If CheckBox5.Checked Then
                    For Each i As Integer In V_nodes
                        If i > 0 Then
                            TempGrap.DrawEllipse(New Pen(Color.Blue, 2), startpoint - CInt(Poly_Node_col_draw(i - 1, 1)) - pie_radii - 2, CInt(Poly_Node_col_draw(i - 1, 0)) - pie_radii - 2, pie_radii * 2 + 4, pie_radii * 2 + 4)
                        End If
                    Next
                End If
                If CheckBox7.Checked Then
                    For Each i As Integer In E_nodes
                        If i > 0 Then
                            TempGrap.DrawEllipse(New Pen(Color.Gold, 2), startpoint - CInt(Poly_Node_col_draw(i - 1, 1)) - pie_radii - 6, CInt(Poly_Node_col_draw(i - 1, 0)) - pie_radii - 6, pie_radii * 2 + 12, pie_radii * 2 + 12)
                        End If
                    Next
                End If
                If CheckBox6.Checked Then
                    For Each i As Integer In G_nodes
                        If i > 0 Then
                            TempGrap.DrawEllipse(New Pen(Color.Green, 2), startpoint - CInt(Poly_Node_col_draw(i - 1, 1)) - pie_radii - 4, CInt(Poly_Node_col_draw(i - 1, 0)) - pie_radii - 4, pie_radii * 2 + 8, pie_radii * 2 + 8)
                        End If
                    Next
                End If
            End If
        End If
        For i As Integer = 0 To NumofTaxon - 1
            '物种名
            Dim Temp_str As String = ""
            If Display_taxon_names Then
                Temp_str = TaxonName(i).Replace("_", " ")
            End If
            If Display_taxon_dis Then
                Temp_str = "(" + Distribution(i) + ") " + Temp_str
            End If
            If Display_taxon_pie Then
                Select Case current_state_mode
                    Case 0
                        If RadioButton2.Checked Or ShowPieOnTerminalToolStripMenuItem.Checked Then
                            TempGrap.FillPie(Brushes.LightGray, startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2, 0, CSng(359.99))
                            For Each c As Char In Distribution(i)
                                TempGrap.FillPie(Color_B(Array.IndexOf(Color_S, sort_area(c))), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2, CSng(CSng(359.99) * (Asc(c) - 65) / RangeLength), CSng(CSng(359.99) / RangeLength))
                            Next
                            TempGrap.DrawEllipse(New Pen(Color.Black), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2)
                        Else
                            TempGrap.FillPie(Color_B(Array.IndexOf(Color_S, sort_area(Distribution(i)))), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2, 0, CSng(359.99))
                            TempGrap.DrawEllipse(New Pen(Color.Black), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2)
                        End If
                    Case 1
                        If IsNumeric(Distribution(i)) Then
                            Dim n As Integer = 255 * (max_dis_value - CSng(Distribution(i))) / (max_dis_value - min_dis_value)
                            Dim mycolor As Color = Color.FromArgb(n, n, n)
                            TempGrap.FillPie(New SolidBrush(mycolor), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2, 0, CSng(359.99))
                        End If
                        TempGrap.DrawEllipse(New Pen(Color.Black), startpoint - CInt(Poly_terminal_xy_draw(i, 1)) - taxon_pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - taxon_pie_radii, taxon_pie_radii * 2, taxon_pie_radii * 2)
                End Select
            End If
            TempGrap.DrawString(Temp_str, Tree_font, Brushes.Black, startpoint - CInt(Poly_terminal_xy_draw(i, 1)) + pie_radii, CInt(Poly_terminal_xy_draw(i, 0)) - Tree_font.GetHeight / 2)
        Next
        For i As Integer = 0 To NumofNode - 1
            '支持率
            If Display_node_frequency Then
                If CSng(Poly_Node(i, 6)) >= Low_frequency Then
                    TempGrap.DrawString((CSng(Poly_Node(i, 6)) * 100).ToString("F0"), Label_font, Brushes.Black, startpoint - CInt(Poly_Node_col_draw(i, 1)) + frequency_h, CInt(Poly_Node_col_draw(i, 0)) + frequency_v)
                End If
            End If
            'node ID
            If Display_node_ID Then
                If CSng(Poly_Node(i, 6)) >= Hide_pie Then
                    TempGrap.DrawString((i + NumofTaxon + 1).ToString, ID_font, New SolidBrush(ID_color), startpoint - CInt(Poly_Node_col_draw(i, 1)) + node_h, CInt(Poly_Node_col_draw(i, 0)) + node_v)
                End If
            End If
        Next

    End Sub
    Dim Selected_node As Integer = -1
    Private Sub PictureBox1_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseDoubleClick
        Dim click_node As Boolean = False
        Selected_node = -1
        If draw_result Then
            For i As Integer = 0 To NumofNode - 1
                If (Poly_Node_col(i, 0) + 1) * Taxon_separation + Label_font.Height + pie_radii > e.Y And e.Y > (Poly_Node_col(i, 0) + 1) * Taxon_separation + Label_font.Height - pie_radii Then
                    If (max_level + 2) * Branch_length + Border_separation - Poly_Node_col(i, 1) * Branch_length + pie_radii > e.X And
                    e.X > (max_level + 2) * Branch_length + Border_separation - Poly_Node_col(i, 1) * Branch_length - pie_radii Then
                        click_node = True
                        Selected_node = i
                        Exit For
                    End If
                End If
            Next
            If click_node Then
                If ListBox1.SelectedIndices.Contains((result_ID - 1) * (NumofNode + 1) + Selected_node + 1) Then
                    ListBox1.SelectedIndices.Remove((result_ID - 1) * (NumofNode + 1) + Selected_node + 1)
                    If ListBox1.SelectedIndices.Count = 0 Then
                        ListBox1.SelectedIndex = (result_ID - 1) * (NumofNode + 1)
                    End If
                Else
                    ListBox1.SelectedIndices.Add((result_ID - 1) * (NumofNode + 1) + Selected_node + 1)
                End If
            Else
                Global_Info()
                TextBox1.Text = Global_Text
                ListBox1.ClearSelected()
                ListBox1.SelectedIndex = (result_ID - 1) * (NumofNode + 1)
            End If
        ElseIf StartTreeView Then
            For i As Integer = 0 To NumofNode - 1
                If (Poly_Node_col(i, 0) + 1) * Taxon_separation + Label_font.Height + pie_radii > e.Y And e.Y > (Poly_Node_col(i, 0) + 1) * Taxon_separation + Label_font.Height - pie_radii Then
                    If (max_level + 2) * Branch_length + Border_separation - Poly_Node_col(i, 1) * Branch_length + pie_radii > e.X And
                    e.X > (max_level + 2) * Branch_length + Border_separation - Poly_Node_col(i, 1) * Branch_length - pie_radii Then
                        click_node = True
                        Selected_node = i
                        Exit For
                    End If
                End If
            Next
            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
        End If
    End Sub
    Dim Bitmap_Tree As Bitmap
    Dim Bitmap_Legend As Bitmap

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If (StartTreeView Or draw_result) And Bitmap_Tree IsNot Nothing Then
            e.Graphics.DrawImage(Bitmap_Tree, 0, 0)
        End If
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndices.Count > 0 And begin_draw Then
            ReDim selected_nodes(ListBox1.SelectedIndices.Count - 1)
            Dim ori_result_ID As Integer = result_ID
            result_ID = (ListBox1.SelectedIndices(0) - (ListBox1.SelectedIndices(0) Mod (NumofNode + 1))) / (NumofNode + 1) + 1
            For i As Integer = 0 To ListBox1.SelectedIndices.Count - 1
                If result_ID = -1 Or result_ID = (ListBox1.SelectedIndices(i) - (ListBox1.SelectedIndices(i) Mod (NumofNode + 1))) / (NumofNode + 1) + 1 Then
                    selected_nodes(i) = ListBox1.SelectedIndices(i) Mod (NumofNode + 1)
                Else
                    MsgBox("Select one result!")
                    Exit For
                End If
            Next
            Load_result()
            Get_Info()
            If Loading = False Then
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
            End If
            PictureBox2.Height = PicBox2_High()
            Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
            draw_col(Graphics.FromImage(Bitmap_Legend), 1)
            PictureBox2.Refresh()

        End If
    End Sub
    Public Sub gray_bmp(ByVal MyBitmap As Bitmap)
        Dim t, tt As Integer
        Dim b As Integer, c As Color
        With MyBitmap
            For t = 0 To .Width - 1
                For tt = 0 To .Height - 1
                    c = .GetPixel(t, tt)
                    b = c.R * 0.3 + c.G * 0.5 + c.B * 0.2
                    .SetPixel(t, tt, Color.FromArgb(b, b, b))
                Next
            Next
        End With
    End Sub

    Dim RASP_Event(,) As Integer
    Public Function sort_area(ByVal input_area As String) As String
        If data_type = 1 Then
            Return input_area
        End If
        Dim temp() As Char = input_area
        Array.Sort(temp)
        Return temp
    End Function
    Public Sub Global_Info()
        Dim Global_RASP(3), Global_DIVA(2) As Integer
        ReDim RASP_Event(NumofNode, 3)
        ReDim D_nodes(0)
        ReDim R_nodes(0)
        ReDim E_nodes(0)
        ReDim G_nodes(0)
        ReDim V_nodes(0)
        Global_Text = ""
        Dim dispersal_type() As String
        ReDim dispersal_type(0)
        Dim dispersal_times() As Single
        ReDim dispersal_times(0)

        Dim speciation_type() As String
        ReDim speciation_type(0)
        Dim speciation_times() As Single
        ReDim speciation_times(0)

        For i As Integer = 1 To NumofNode
            Global_Text += "NODE" + (i + NumofTaxon).ToString + ":" + vbCrLf
            Dim A_AREA(1), C_AREA(1, 1) As String
            Dim C_COUNT As Integer = 0
            Dim Templist() As String = Result_list(NumofNode * (result_ID - 1) + i - 1).Split(New Char() {" "c})
            If Templist.Length > 1 Then
                If Templist(1) <> "" And Templist(1) <> "/" Then
                    A_AREA(0) = Templist(1)
                    A_AREA(1) = Templist(2)
                Else
                    If Templist.Length > 4 Then
                        A_AREA(0) = Templist(3)
                        A_AREA(1) = Templist(4)
                    Else
                        A_AREA(0) = ""
                        A_AREA(1) = 0
                    End If
                End If
            Else
                A_AREA(0) = ""
                A_AREA(1) = 0
            End If
            If Node_Relationship(i - 1, 1) <> "" Then
                For Each n As String In Node_Relationship(i - 1, 1).Split(New Char() {","c})
                    If n <> "" Then
                        C_COUNT += 1
                        ReDim Preserve C_AREA(1, C_COUNT)
                        C_AREA(0, C_COUNT - 1) = Distribution(CInt(n) - 1)
                        C_AREA(1, C_COUNT - 1) = "100"
                    End If
                Next
            End If

            If Node_Relationship(i - 1, 2) <> "" Then
                For Each n As String In Node_Relationship(i - 1, 2).Split(New Char() {","c})
                    If n <> "" Then
                        C_COUNT += 1
                        ReDim Preserve C_AREA(1, C_COUNT)
                        Templist = Result_list(NumofNode * (result_ID - 1) + CInt(n)).Split(New Char() {" "c})
                        If Templist(1) <> "" And Templist(1) <> "/" Then
                            C_AREA(0, C_COUNT - 1) = Templist(1)
                            C_AREA(1, C_COUNT - 1) = Templist(2)
                        Else
                            If Templist.Length > 4 Then
                                C_AREA(0, C_COUNT - 1) = Templist(3)
                                C_AREA(1, C_COUNT - 1) = Templist(4)
                            Else
                                C_AREA(0, C_COUNT - 1) = ""
                                C_AREA(1, C_COUNT - 1) = 0
                            End If

                        End If
                    End If
                Next
            End If

            Dim Nu, Ns, Na, NuANa, NsANa, NuUNa, Nt As String
            Nt = ""
            NuANa = ""
            NsANa = ""
            Nu = ""
            Ns = C_AREA(0, 0)
            Na = A_AREA(0)
            For j As Integer = 0 To C_COUNT - 1
                Dim Ns_temp As String = ""
                For Each c As Char In C_AREA(0, j)
                    If Nu.Contains(c) = False Then
                        Nu += c
                    End If
                    If Ns.Contains(c) = True Then
                        Ns_temp += c
                    End If
                    For k As Integer = j + 1 To C_COUNT - 1
                        If C_AREA(0, k).Contains(c) = True Then
                            Nt = Nt + "^" + c
                        End If
                    Next
                Next
                Ns = Ns_temp
            Next
            NuUNa = Nu
            Nu = sort_area(Nu)
            Na = sort_area(Na)



            Dim DREG(3) As Integer
            Dim DIVA(2) As Integer

            For Each c As Char In Na
                If Nu.Contains(c) = False Then
                    DREG(2) += 1
                    NuUNa += c
                End If
            Next
            For Each c As Char In Ns
                If Na.Contains(c) Then
                    NsANa += c
                End If
            Next
            DREG(0) = Nu.Length
            For Each c As Char In Nu
                If Na.Contains(c) Then
                    NuANa += c
                    DREG(0) = DREG(0) - 1
                End If
            Next

            If Na.Length > 1 Or UseSingleAreaModelToolStripMenuItem.Checked = False Then
                DIVA(0) = DREG(0)
            Else
                Dim local_e As Integer = 0 '本地灭绝
                For j As Integer = 0 To C_COUNT - 1
                    If C_AREA(0, j).Contains(Na) = False Then
                        local_e += 1
                    End If
                Next
                DIVA(0) = NuUNa.Length - C_COUNT * NsANa.Length - local_e
            End If

            DREG(1) = Nt.Length / 2

            If NsANa.Length = 0 Then
                DREG(3) = C_COUNT - 1
            Else
                DREG(3) = 0
            End If
            DIVA(0) = DIVA(0) + DREG(1)
            DIVA(1) = DREG(3)
            DIVA(2) = DREG(2)
            For j As Integer = 0 To C_COUNT - 1
                If Na <> C_AREA(0, j) Then
                    Dim N_Temp As String = C_AREA(0, j)
                    For Each c As Char In Na
                        N_Temp = N_Temp.Replace(c, "")
                    Next
                    For Each m As Char In Na
                        For Each n As Char In N_Temp
                            If Array.IndexOf(dispersal_type, m + "->" + n) < 0 Then
                                ReDim Preserve dispersal_type(UBound(dispersal_type) + 1)
                                ReDim Preserve dispersal_times(UBound(dispersal_times) + 1)
                                dispersal_type(UBound(dispersal_type)) = m + "->" + n
                                dispersal_times(UBound(dispersal_times)) = 1 / Na.Length
                            Else
                                dispersal_times(Array.IndexOf(dispersal_type, m + "->" + n)) += 1 / Na.Length
                            End If
                        Next
                    Next
                End If
                For Each c As Char In C_AREA(0, j)
                    For k As Integer = j + 1 To C_COUNT - 1
                        If C_AREA(0, k).Contains(c) = True Then
                            If Array.IndexOf(speciation_type, c.ToString) < 0 Then
                                ReDim Preserve speciation_type(UBound(speciation_type) + 1)
                                ReDim Preserve speciation_times(UBound(speciation_times) + 1)
                                speciation_type(UBound(speciation_type)) = c
                                speciation_times(UBound(speciation_times)) = 1
                            Else
                                speciation_times(Array.IndexOf(speciation_type, c.ToString)) += 1
                            End If
                        End If
                    Next
                Next
            Next
            'Global_Text += "DREGMATRIX:" + vbCrLf
            'Global_Text += " Duplication:" + DREG(0).ToString + vbCrLf
            'Global_Text += " Reproductive isolation:" + DREG(1).ToString + vbCrLf
            'Global_Text += " Extinction:" + DREG(2).ToString + vbCrLf
            'Global_Text += " Geographical isolation:" + DREG(3).ToString + vbCrLf

            Global_Text += "EVENT MATRIX:" + vbCrLf
            Global_Text += " Dispersal:" + DIVA(0).ToString + vbCrLf
            Global_Text += " Vicariance:" + DIVA(1).ToString + vbCrLf
            Global_Text += " Extinction:" + DIVA(2).ToString + vbCrLf

            RASP_Event(i, 0) = DIVA(0)
            RASP_Event(i, 1) = DIVA(1)
            RASP_Event(i, 2) = DIVA(2)
            RASP_Event(i, 3) = DIVA(0) + DIVA(1) + DIVA(2)

            Global_RASP(0) += DREG(0)
            Global_RASP(1) += DREG(1)
            Global_RASP(2) += DREG(2)
            Global_RASP(3) += DREG(3)

            Global_DIVA(1) += DIVA(1)
            Global_DIVA(2) += DIVA(2)
            Global_DIVA(0) += DIVA(0)

            Global_Text += "Event Route:" + vbCrLf
            Dim RANGE_TEMP As String = Na
            Global_Text += " " + Na
            If DREG(2) > 0 Then
                Global_Text += "->" + NuANa
                RANGE_TEMP = NuANa
            End If
            If DREG(1) > 0 Then
                Global_Text += "->"
                Global_Text += RANGE_TEMP + Nt
            End If
            If DREG(0) > 0 Then
                Global_Text += "->" + Nu + Nt
            End If
            'If DREG(3) > 0 Then
            Global_Text += "->"
            For j As Integer = 0 To C_COUNT - 2
                Global_Text += C_AREA(0, j) + "|"
            Next
            Global_Text += C_AREA(0, C_COUNT - 1) + vbCrLf
            'End If
            Global_Text += "PROBABILITY:" + vbCrLf
            Dim pro_temp As Single = CSng(A_AREA(1)) / 100
            For t As Integer = 0 To C_COUNT - 1
                pro_temp = pro_temp * CSng(C_AREA(1, t)) / 100
            Next
            Global_Text += " " + pro_temp.ToString("F4") + vbCrLf + vbCrLf
            If DREG(0) > 0 Then
                ReDim Preserve D_nodes(D_nodes.Length)
                D_nodes(D_nodes.Length - 1) = i
            End If
            If DREG(1) > 0 Then
                ReDim Preserve R_nodes(R_nodes.Length)
                R_nodes(R_nodes.Length - 1) = i
            End If
            If DREG(2) > 0 Then
                ReDim Preserve E_nodes(E_nodes.Length)
                E_nodes(E_nodes.Length - 1) = i
            End If
            If DREG(3) > 0 Then
                ReDim Preserve G_nodes(G_nodes.Length)
                G_nodes(G_nodes.Length - 1) = i
            End If
            If DIVA(0) > 0 Then
                ReDim Preserve V_nodes(V_nodes.Length)
                V_nodes(V_nodes.Length - 1) = i
            End If

        Next
        Array.Sort(dispersal_type, dispersal_times)
        Array.Sort(speciation_type, speciation_times)
        Global_Text += "===================" + vbCrLf
        Global_Text += "Dispersal Between Areas:" + vbCrLf
        For i As Integer = 1 To dispersal_times.Length - 1
            Global_Text += dispersal_type(i) + ":" + dispersal_times(i).ToString + vbCrLf
        Next
        Global_Text += "Speciation Within Areas:" + vbCrLf
        For i As Integer = 1 To speciation_times.Length - 1
            Global_Text += speciation_type(i) + ":" + speciation_times(i).ToString + vbCrLf
        Next
        Dim Dispersal_sum(,) As Single
        ReDim Dispersal_sum(RangeLength, 2)
        For i As Integer = 1 To RangeLength
            Dim temp As String = ChrW(64 + i)
            For j As Integer = 1 To dispersal_times.Length - 1
                If dispersal_type(j).StartsWith(temp) Then
                    Dispersal_sum(i, 0) += dispersal_times(j)
                End If
                If dispersal_type(j).EndsWith(temp) Then
                    Dispersal_sum(i, 1) += dispersal_times(j)
                End If
            Next
            If Array.IndexOf(speciation_type, temp) > 0 Then
                Dispersal_sum(i, 2) = speciation_times(Array.IndexOf(speciation_type, temp))
            Else
                Dispersal_sum(i, 2) = 0
            End If
        Next
        Global_Text += "Dispersal Table:" + vbCrLf
        Global_Text += "	from	to	within" + vbCrLf
        For i As Integer = 1 To RangeLength
            Dim temp As String = ChrW(64 + i)
            Global_Text += temp + "	" + Dispersal_sum(i, 0).ToString("F2") + "	" + Dispersal_sum(i, 1).ToString("F2") + "	" + Dispersal_sum(i, 2).ToString + vbCrLf
        Next

        Global_Text += "===================" + vbCrLf
        Global_Text += "Global Cost:" + vbCrLf
        Global_Text += " Global Dispersal: " + Global_DIVA(0).ToString + vbCrLf
        Global_Text += " Global Vicariance: " + Global_DIVA(1).ToString + vbCrLf
        Global_Text += " Global Extinction: " + Global_DIVA(2).ToString + vbCrLf
    End Sub
    Public Sub Get_Info()
        Dim BoxText As String = ""
        For Each i As Integer In selected_nodes
            If i > 0 Then
                If Result_list(i - 1) <> "" Then
                    BoxText += ListBox1.Items((result_ID - 1) * (NumofNode + 1) + i).ToString.ToUpper + vbCrLf + vbCrLf
                    Dim A_AREA(1), C_AREA(1, 1) As String
                    Dim C_COUNT As Integer = 0
                    Dim Templist() As String = Result_list((result_ID - 1) * NumofNode + i - 1).Split(New Char() {" "c})
                    If Templist(1) <> "" And Templist(1) <> "/" Then
                        A_AREA(0) = Templist(1)
                        A_AREA(1) = Templist(2)
                    Else
                        If Templist.Length > 4 Then
                            A_AREA(0) = Templist(3)
                            A_AREA(1) = Templist(4)
                        Else
                            A_AREA(0) = ""
                            A_AREA(1) = 0
                        End If

                    End If

                    BoxText += "ANCESTRAL AREA:" + vbCrLf + " "
                    BoxText += Result_list((result_ID - 1) * NumofNode + i - 1) + vbCrLf + vbCrLf

                    If Node_Relationship(i - 1, 1) <> "" Then
                        BoxText += "CHILD TAXON: " + vbCrLf + " "
                        For Each n As String In Node_Relationship(i - 1, 1).Split(New Char() {","c})
                            If n <> "" Then
                                C_COUNT += 1
                                ReDim Preserve C_AREA(1, C_COUNT)
                                C_AREA(0, C_COUNT - 1) = Distribution(CInt(n) - 1)
                                C_AREA(1, C_COUNT - 1) = "100"
                                BoxText += TaxonName(CInt(n) - 1) + ","
                            End If
                        Next
                        BoxText += " " + vbCrLf + vbCrLf
                    End If

                    If Node_Relationship(i - 1, 2) <> "" Then
                        BoxText += "CHILD NODES: " + vbCrLf

                        For Each n As String In Node_Relationship(i - 1, 2).Split(New Char() {","c})
                            If n <> "" Then
                                C_COUNT += 1
                                ReDim Preserve C_AREA(1, C_COUNT)
                                Templist = Result_list((result_ID - 1) * NumofNode + CInt(n)).Split(New Char() {" "c})
                                If Templist(1) <> "" And Templist(1) <> "/" Then
                                    C_AREA(0, C_COUNT - 1) = Templist(1)
                                    C_AREA(1, C_COUNT - 1) = Templist(2)
                                Else
                                    If Templist.Length > 4 Then
                                        C_AREA(0, C_COUNT - 1) = Templist(3)
                                        C_AREA(1, C_COUNT - 1) = Templist(4)
                                    Else
                                        C_AREA(0, C_COUNT - 1) = ""
                                        C_AREA(1, C_COUNT - 1) = 0
                                    End If

                                End If
                                BoxText += " " + "Node" + (CInt(n) + 1 + NumofTaxon).ToString + ": "
                                BoxText += Result_list((result_ID - 1) * (NumofNode) + CInt(n)) + vbCrLf
                            End If
                        Next
                        BoxText += " " + vbCrLf
                    End If

                    Dim Nu, Ns, Na, NuANa, NsANa, NuUNa, Nt As String
                    Nt = ""
                    NuANa = ""
                    NsANa = ""
                    Nu = ""
                    Ns = C_AREA(0, 0)
                    Na = A_AREA(0)
                    For j As Integer = 0 To C_COUNT - 1
                        Dim Ns_temp As String = ""

                        For Each c As Char In C_AREA(0, j)
                            If Nu.Contains(c) = False Then
                                Nu += c
                            End If
                            If Ns.Contains(c) = True Then
                                Ns_temp += c
                            End If
                            For k As Integer = j + 1 To C_COUNT - 1
                                If C_AREA(0, k).Contains(c) = True Then
                                    Nt = Nt + "^" + c
                                End If
                            Next
                        Next

                        Ns = Ns_temp
                    Next
                    NuUNa = Nu

                    Dim DREG(3) As Integer '0:扩散 1: 2:灭绝 3:
                    Dim DIVA(2) As Integer

                    For Each c As Char In Na
                        If Nu.Contains(c) = False Then
                            DREG(2) += 1
                            NuUNa += c
                        End If


                    Next
                            For Each c As Char In Ns
                        If Na.Contains(c) Then
                            NsANa += c
                        End If
                    Next
                    DREG(0) = Nu.Length
                    For Each c As Char In Nu
                        If Na.Contains(c) Then
                            NuANa += c
                            DREG(0) = DREG(0) - 1
                        End If
                    Next

                    If Na.Length > 1 Or UseSingleAreaModelToolStripMenuItem.Checked = False Then
                        DIVA(0) = DREG(0)
                    Else
                        Dim local_e As Integer = 0 '本地灭绝
                        For j As Integer = 0 To C_COUNT - 1
                            If C_AREA(0, j).Contains(Na) = False Then
                                local_e += 1
                            End If
                        Next
                        DIVA(0) = NuUNa.Length - C_COUNT * NsANa.Length - local_e
                    End If


                    DREG(1) = Nt.Length / 2

                    If NsANa.Length = 0 Then
                        DREG(3) = C_COUNT - 1
                    Else
                        DREG(3) = 0
                    End If
                    DIVA(0) = DIVA(0) + DREG(1)
                    DIVA(1) = DREG(3)
                    DIVA(2) = DREG(2)

                    'BoxText += "DREGMATRIX:" + vbCrLf
                    'BoxText += " Duplication:" + DREG(0).ToString + vbCrLf
                    'BoxText += " Reproductive isolation:" + DREG(1).ToString + vbCrLf
                    'BoxText += " Extinction:" + DREG(2).ToString + vbCrLf
                    'BoxText += " Geographical isolation:" + DREG(3).ToString + vbCrLf

                    BoxText += "EVENT MATRIX:" + vbCrLf
                    BoxText += " Dispersal :" + DIVA(0).ToString + vbCrLf
                    BoxText += " Vicariance:" + DIVA(1).ToString + vbCrLf
                    BoxText += " Extinction:" + DIVA(2).ToString + vbCrLf

                    BoxText += "RASP ROUTE:" + vbCrLf
                    Dim RANGE_TEMP As String = Na
                    BoxText += " " + Na
                    If DREG(2) > 0 Then
                        BoxText += "->" + NuANa
                        RANGE_TEMP = NuANa
                    End If
                    If DREG(1) > 0 Then
                        BoxText += "->"
                        BoxText += RANGE_TEMP + Nt
                    End If
                    If DREG(0) > 0 Then
                        BoxText += "->" + Nu + Nt
                    End If
                    'If DREG(3) > 0 Then
                    BoxText += "->"
                    For j As Integer = 0 To C_COUNT - 2
                        BoxText += C_AREA(0, j) + "|"
                    Next
                    BoxText += C_AREA(0, C_COUNT - 1) + vbCrLf
                    'End If
                    BoxText += "PROBABILITY:" + vbCrLf
                    Dim pro_temp As Single = CSng(A_AREA(1)) / 100
                    For t As Integer = 0 To C_COUNT - 1
                        pro_temp = pro_temp * CSng(C_AREA(1, t)) / 100
                    Next
                    BoxText += " " + pro_temp.ToString("F4") + vbCrLf
                    BoxText += "=================" + vbCrLf + vbCrLf
                End If
            End If
        Next
        If BoxText <> "" Then
            TextBox1.Text = BoxText
        End If
    End Sub
    Public Function PicBox2_High() As Integer
        Dim startpoint As Integer = 150 + (max_level + 2) * 32
        Dim start_Y As Integer = 5
        Dim L_border As Integer = 70
        Dim font2left As Integer = 10
        Dim font2top As Integer = -16
        Dim Temp_Y As Integer
        '柱子离上端距离
        Dim col2top As Integer = 0
        '柱子间隔
        Dim colborder As Integer = 40
        '柱子宽度
        Dim colwidth As Integer = 16
        Dim temparray As Integer = 0
        If RadioButton2.Checked Then
            For Each i As Integer In selected_nodes
                If i > 0 Then
                    For n As Integer = 1 To RangeLength
                        Temp_Y = start_Y + n * colborder + col2top
                    Next
                    start_Y = Temp_Y + L_border
                ElseIf i = 0 And selected_nodes.Length = 1 Then
                    Temp_Y = (RangeLength + 3) * colborder + col2top
                    start_Y = Temp_Y + L_border
                End If
            Next
        Else
            For Each i As Integer In selected_nodes
                If i > 0 Then
                    For n As Integer = 0 To 32
                        If Current_AreaS(i - 1, n) <> "" Then
                            If CInt(Current_AreaP(i - 1, n) * 100) > 0 Or Current_AreaS(i - 1, n) <> "*" Then
                            Else
                                Temp_Y = start_Y + (n - 1) * colborder + col2top
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                        Temp_Y = start_Y + n * colborder + col2top
                    Next
                    start_Y = Temp_Y + L_border
                ElseIf i = 0 And selected_nodes.Length = 1 Then
                    For j As Integer = 0 To UBound(Color_B)
                    Next
                    Temp_Y = (UBound(Color_B) + 3) * colborder + col2top
                    start_Y = Temp_Y + L_border
                End If
            Next
        End If
        Return start_Y + colborder + 2 * col2top + L_border
    End Function
    Public Sub draw_col(ByVal TempGrap As Object, ByVal zoom As Integer)
        Dim col_font As New System.Drawing.Font("Tahoma", 10 * zoom, FontStyle.Regular)
        Dim title_font As New System.Drawing.Font("Tahoma", 10 * zoom, FontStyle.Bold)
        Dim Tree_pen As New System.Drawing.Pen(System.Drawing.Color.Gray, 2 * zoom)
        Dim startpoint As Integer = 150 + (max_level + 2) * 32
        '背景透明
        If TransparentBG = False And savingpic Then
            TempGrap.FillRectangle(Brushes.White, 0, 0, PictureBox2.Width, PictureBox2.Height)
        ElseIf savingpic = False Then
            TempGrap.FillRectangle(Brushes.White, 0, 0, PictureBox2.Width, PictureBox2.Height)
        End If
        '被选择的描红,添加图注
        '图注开始坐标
        Dim start_Y As Integer = 5
        '图注间的距离
        Dim L_border As Integer = 70
        '边距
        Dim font2left As Integer = 10
        Dim font2top As Integer = -16
        Dim Temp_Y As Integer
        '柱子离上端距离
        Dim col2top As Integer = 0
        '柱子间隔
        Dim colborder As Integer = 40
        '柱子宽度
        Dim colwidth As Integer = 16
        '灭绝率
        'Dim rate() As Single
        'ReDim rate(UBound(selected_nodes))
        'Dim ex_rate As Single = 1
        'Dim su_rate As Single = 1
        '指针
        Dim temparray As Integer = 0
        If RadioButton2.Checked Then
            For Each i As Integer In selected_nodes
                If i > 0 Then
                    TempGrap.DrawString(ListBox1.Items((result_ID - 1) * (NumofNode + 1) + i).ToString.ToUpper, title_font, Brushes.Black, 10, start_Y + 2)
                    For n As Integer = 1 To RangeLength

                        Dim cur_brush As Brush = Int2Brushes(Distributiton_to_Integer(Chr(64 + n)))
                        TempGrap.FillRectangle(cur_brush, font2left, start_Y + (n + 1) * colborder + col2top, CInt(PROB_list(i - 1, n) * 100), colwidth)
                        TempGrap.DrawRectangle(Pens.Black, font2left, start_Y + (n + 1) * colborder + col2top, CInt(PROB_list(i - 1, n) * 100), colwidth)
                        TempGrap.DrawString(Chr(64 + n), col_font, Brushes.Black, font2left, start_Y + (n + 1) * colborder + font2top)
                        TempGrap.DrawString(Val(PROB_list(i - 1, n)).ToString("F4"), col_font, Brushes.Black, CInt(PROB_list(i - 1, n) * 100 + font2left), CInt(start_Y + (n + 1) * colborder + col2top))
                        Temp_Y = start_Y + n * colborder + col2top
                    Next
                    start_Y = Temp_Y + L_border
                ElseIf i = 0 And selected_nodes.Length = 1 Then
                    TempGrap.DrawString("LEGEND", title_font, Brushes.Black, 10, start_Y)
                    For n As Integer = 1 To RangeLength
                        TempGrap.FillRectangle(Int2Brushes(Distributiton_to_Integer(Chr(64 + n))), font2left, (n) * colborder + col2top, 80, colwidth)
                        TempGrap.DrawRectangle(Pens.Black, font2left, (n) * colborder + col2top, 80, colwidth)
                        TempGrap.DrawString(Chr(64 + n), col_font, Brushes.Black, font2left, (n) * colborder + font2top)
                    Next
                    Temp_Y = (RangeLength + 3) * colborder + col2top
                    start_Y = Temp_Y + L_border
                End If
            Next
        Else
            For Each i As Integer In selected_nodes
                If i > 0 Then
                    TempGrap.DrawString(ListBox1.Items((result_ID - 1) * (NumofNode + 1) + i).ToString.ToUpper, title_font, Brushes.Black, 10, start_Y + 2)
                    For n As Integer = 0 To 32
                        If Current_AreaS(i - 1, n) <> "" Then
                            If CInt(Current_AreaP(i - 1, n) * 100) > 0 Or Current_AreaS(i - 1, n) <> "*" Then
                                Dim cur_brush As Brush = Color_B(Array.IndexOf(Color_S, Current_AreaS(i - 1, n)))
                                TempGrap.FillRectangle(cur_brush, font2left, start_Y + (n + 1) * colborder + col2top, Current_AreaP(i - 1, n), colwidth)
                                TempGrap.DrawRectangle(Pens.Black, font2left, start_Y + (n + 1) * colborder + col2top, Current_AreaP(i - 1, n), colwidth)
                                TempGrap.DrawString(Current_AreaS(i - 1, n), col_font, Brushes.Black, font2left, start_Y + (n + 1) * colborder + font2top)
                                TempGrap.DrawString(((Current_AreaP(i - 1, n)) / 100).ToString("F4"), col_font, Brushes.Black, Current_AreaP(i - 1, n) + font2left, start_Y + (n + 1) * colborder + col2top)
                            Else
                                Temp_Y = start_Y + (n - 1) * colborder + col2top
                                Exit For
                            End If
                        Else
                            Exit For
                        End If
                        Temp_Y = start_Y + n * colborder + col2top
                    Next
                    start_Y = Temp_Y + L_border
                ElseIf i = 0 And selected_nodes.Length = 1 Then
                    If ShowPieOnTerminalToolStripMenuItem.Checked Then
                        TempGrap.DrawString("LEGEND", title_font, Brushes.Black, 10, start_Y)
                        'Array.Sort(Color_S, Color_B)
                        For j As Integer = 0 To UBound(Color_B_node)
                            TempGrap.FillRectangle(Color_B_node(j), font2left, (j + 1) * colborder + col2top, 80, colwidth)
                            TempGrap.DrawRectangle(Pens.Black, font2left, (j + 1) * colborder + col2top, 80, colwidth)
                            TempGrap.DrawString(Color_S_node(j), col_font, Brushes.Black, font2left, (j + 1) * colborder + font2top)
                        Next
                        Temp_Y = (UBound(Color_B_node) + 3) * colborder + col2top
                        start_Y = Temp_Y + L_border
                    Else
                        TempGrap.DrawString("LEGEND", title_font, Brushes.Black, 10, start_Y)
                        'Array.Sort(Color_S, Color_B)
                        For j As Integer = 0 To UBound(Color_B)
                            TempGrap.FillRectangle(Color_B(j), font2left, (j + 1) * colborder + col2top, 80, colwidth)
                            TempGrap.DrawRectangle(Pens.Black, font2left, (j + 1) * colborder + col2top, 80, colwidth)
                            TempGrap.DrawString(Color_S(j), col_font, Brushes.Black, font2left, (j + 1) * colborder + font2top)
                        Next
                        Temp_Y = (UBound(Color_B) + 3) * colborder + col2top
                        start_Y = Temp_Y + L_border
                    End If

                End If
            Next
        End If
        PictureBox2.Height = start_Y + colborder + 2 * col2top + L_border
    End Sub
    Private Sub PictureBox2_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox2.Paint
        If draw_result Then
            e.Graphics.DrawImage(Bitmap_Legend, 0, 0)
        End If
    End Sub

    Private Sub OptionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OptionToolStripMenuItem.Click
        Dim OptionWindow As New View_OptionForm
        OptionWindow.Show()
    End Sub

    Private Sub LoadResultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadResultToolStripMenuItem.Click
        If File.Exists(temp_result_file) Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".txt"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                File.Copy(temp_result_file, opendialog.FileName, True)
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("No result to save!", MsgBoxStyle.Information)
        End If

    End Sub
    Dim savingpic As Boolean = False
    Private Sub SaveTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveTreeToolStripMenuItem.Click
        Try
            Dim sfd As New SaveFileDialog
            sfd.Filter = "PNG Files(*.png)|*.png;*.PNG|SVG (Adobe Illustrator)|*.svg;*.SVG|Windows Metafile(*.emf)|*.emf;*.EMF|ALL Files(*.*)|*.*"
            sfd.FileName = ""
            sfd.DefaultExt = ".png"
            sfd.CheckPathExists = True
            Dim resultdialog As DialogResult = sfd.ShowDialog()

            If resultdialog = DialogResult.OK Then
                If sfd.FileName.ToLower.EndsWith(".emf") Then
                    Dim bitmap As New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                    Dim g As Graphics = Graphics.FromImage(bitmap)
                    Dim wmf As New Drawing.Imaging.Metafile(sfd.FileName, g.GetHdc())
                    Dim ig As Graphics = Graphics.FromImage(wmf)
                    savingpic = True
                    Dim temp_bool As Boolean = TransparentBG
                    TransparentBG = True
                    draw_tree(ig)
                    TransparentBG = temp_bool
                    savingpic = False
                    ig.Dispose()
                    wmf.Dispose()
                    g.ReleaseHdc()
                    g.Dispose()
                    bitmap.Dispose()
                    bitmap = Nothing
                ElseIf sfd.FileName.ToLower.EndsWith(".svg") Then
                    savingpic = True
                    Dim temp_bool As Boolean = TransparentBG
                    TransparentBG = True
                    Dim svg_gra As New SvgNet.SvgGdi.SvgGraphics
                    draw_tree(svg_gra)
                    Dim svg_writer As New StreamWriter(sfd.FileName, False)
                    svg_writer.Write(svg_gra.WriteSVGString)
                    svg_writer.Close()
                    TransparentBG = temp_bool
                    savingpic = False
                    svg_gra.Flush()
                    svg_gra = Nothing
                ElseIf sfd.FileName.ToLower.EndsWith(".png") Then
                    If CInt(PictureBox1.Width) * File_zoom > 32768 / 4 Or CInt(PictureBox1.Height) * File_zoom > 32768 Then
                        MsgBox("The picture is very large! Please decrease the value of zoom in option if you meet error.")
                    End If

                    Dim bitmap As New Bitmap(CInt(PictureBox1.Width) * File_zoom, CInt(PictureBox1.Height) * File_zoom)
                        Dim TempGrap As Graphics = Graphics.FromImage(bitmap)
                        pie_radii = pie_radii * File_zoom
                        Tree_font = New Font(Tree_font.FontFamily, Tree_font.Size * File_zoom, Tree_font.Style)
                        Label_font = New Font(Label_font.FontFamily, Label_font.Size * File_zoom, Label_font.Style)
                        ID_font = New Font(ID_font.FontFamily, ID_font.Size * File_zoom, ID_font.Style)
                        frequency_h = frequency_h * File_zoom
                        frequency_v = frequency_v * File_zoom
                        node_h = node_h * File_zoom
                        node_v = node_v * File_zoom
                        Taxon_separation = Taxon_separation * File_zoom
                        Branch_length = Branch_length * File_zoom
                        Border_separation = Border_separation * File_zoom
                        Line_width = Line_width * File_zoom
                        taxon_pie_radii = taxon_pie_radii * File_zoom
                        Circle_size = Circle_size * File_zoom
                        savingpic = True
                        draw_tree(TempGrap)
                        savingpic = False
                        bitmap.Save(sfd.FileName)
                        pie_radii = pie_radii / File_zoom
                        Tree_font = New Font(Tree_font.FontFamily, Tree_font.Size / File_zoom, Tree_font.Style)
                        Label_font = New Font(Label_font.FontFamily, Label_font.Size / File_zoom, Label_font.Style)
                        ID_font = New Font(ID_font.FontFamily, ID_font.Size / File_zoom, ID_font.Style)
                        frequency_h = frequency_h / File_zoom
                        frequency_v = frequency_v / File_zoom
                        node_h = node_h / File_zoom
                        node_v = node_v / File_zoom
                        Taxon_separation = Taxon_separation / File_zoom
                        Branch_length = Branch_length / File_zoom
                        Border_separation = Border_separation / File_zoom
                        Line_width = Line_width / File_zoom
                        Circle_size = Circle_size / File_zoom
                        taxon_pie_radii = taxon_pie_radii / File_zoom
                        bitmap.Dispose()
                        bitmap = Nothing
                    End If

                    MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub SaveLegendToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveLegendToolStripMenuItem.Click
        Try
            Dim bitmap As New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
            Dim sfd As New SaveFileDialog
            sfd.Filter = "PNG Files(*.png)|*.png;*.PNG|SVG (Adobe Illustrator)|*.svg;*.SVG|Windows Metafile(*.emf)|*.emf;*.EMF|ALL Files(*.*)|*.*"
            sfd.FileName = ""
            sfd.DefaultExt = ".png"
            sfd.CheckPathExists = True
            Dim resultdialog As DialogResult = sfd.ShowDialog()
            If resultdialog = DialogResult.OK Then
                If sfd.FileName.ToLower.EndsWith(".emf") Then
                    Dim g As Graphics = Graphics.FromImage(bitmap)
                    Dim wmf As New Drawing.Imaging.Metafile(sfd.FileName, g.GetHdc())
                    Dim ig As Graphics = Graphics.FromImage(wmf)
                    savingpic = True
                    draw_col(ig, 1)
                    savingpic = False
                    ig.Dispose()
                    wmf.Dispose()
                    g.ReleaseHdc()
                    g.Dispose()
                ElseIf sfd.FileName.ToLower.EndsWith(".svg") Then
                    Dim svg_gra As New SvgNet.SvgGdi.SvgGraphics
                    savingpic = True
                    draw_col(svg_gra, 1)
                    savingpic = False
                    Dim svg_writer As New StreamWriter(sfd.FileName, False)
                    svg_writer.Write(svg_gra.WriteSVGString)
                    svg_writer.Close()
                    svg_gra.Flush()
                Else
                    Dim TempGrap As Graphics = Graphics.FromImage(bitmap)
                    savingpic = True
                    draw_col(TempGrap, 1)
                    savingpic = False
                    bitmap.Save(sfd.FileName)
                End If
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub LoadResultToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadResultToolStripMenuItem1.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                draw_result = True
                StartTreeView = True
                Try
                    Loading = True
                    time_Dataset.Tables("Time Table").Rows.Clear()
                    read_results(opendialog.FileName)
                    Me.Text = opendialog.FileName
                    Loading = False
                    PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                    PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                    Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                    draw_tree(Graphics.FromImage(Bitmap_Tree))
                    PictureBox1.Refresh()
                Catch ex As Exception
                    draw_result = False
                    StartTreeView = False
                    MsgBox(ex.ToString)
                    MsgBox("Error of reading result file!", MsgBoxStyle.Information)
                    Me.Hide()
                    Exit Sub
                End Try
                MsgBox("Load Successfully!", MsgBoxStyle.Information)
            End If

        End If

    End Sub

    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        If ListBox1.Items.Count > 0 And begin_draw Then
            Load_result()
        End If
        If result_ID > 0 Then
            Global_Info()
        End If
        TextBox1.Text = Global_Text
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()

        PictureBox2.Height = PicBox2_High()
        Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
        draw_col(Graphics.FromImage(Bitmap_Legend), 1)
        PictureBox2.Refresh()
    End Sub
    Private Sub SaveInfoToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveInfoToolStripMenuItem.Click

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged, CheckBox6.CheckedChanged, CheckBox7.CheckedChanged
        If Loading = False Then
            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
        End If

    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            RadioButton2.Checked = False
            If Loading = False Then
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
                PictureBox2.Height = PicBox2_High()
                Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
                draw_col(Graphics.FromImage(Bitmap_Legend), 1)
                PictureBox2.Refresh()
            End If

        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            RadioButton1.Checked = False
            If Loading = False Then
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
                PictureBox2.Height = PicBox2_High()
                Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
                draw_col(Graphics.FromImage(Bitmap_Legend), 1)
                PictureBox2.Refresh()
            End If

        End If
    End Sub
    Dim maxtime As Single = 0
    Dim root_time As Single = 1
    Dim unit_time As Single = 1
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If IsNumeric(TextBox3.Text) = False Then
            MsgBox("Unit time error!")
            Exit Sub
        End If
        If CSng(TextBox3.Text) <= 0 Then
            MsgBox("Unit time error!")
            Exit Sub
        End If
        If IsNumeric(TimeBox.Text) = False Or maxtime <= 0 Then
            MsgBox("Root time error!")
            Exit Sub
        End If
        If CSng(TimeBox.Text) <= 0 Then
            MsgBox("Root time error!")
            Exit Sub
        End If
        root_time = CSng(TimeBox.Text) / maxtime
        unit_time = CSng(TextBox3.Text) * CSng(TimeBox.Text) / NumofNode
        For i As Integer = 1 To time_view.Count
            If CheckBox8.Checked Then
                time_view.Item(i - 1).Item(2) = ((maxtime - Val(time_view.Item(i - 1).Item(1))) * root_time).ToString("F8")
                TaxonTime(i, 2) = ((maxtime - Val(TaxonTime(i - 1, 1))) * root_time).ToString("F8")
            Else
                time_view.Item(i - 1).Item(2) = (Val(time_view.Item(i - 1).Item(1)) * root_time).ToString("F8")
                TaxonTime(i, 2) = (Val(TaxonTime(i - 1, 1)) * root_time).ToString("F8")
            End If
        Next
        DataGridView1.EndEdit()
        DataGridView1.Refresh()


        If maxtime > 0 Then
            smooth_x = 1 + PictureBox3.Width / maxtime / root_time
        Else
            smooth_x = 1
        End If

        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()
        Select Case ComboBox2.SelectedIndex
            Case 0
                If draw_result And StartTreeView Then
                    start_draw_area = False
                    TimeView = 2
                    draw_area_para(0) = 0
                    TabControl2.SelectedIndex = 1

                    draw_area_para(1) = CInt(maxtime * root_time * smooth_x)
                    draw_area_para(2) = 1
                    draw_area_para(3) = 4
                    ComboBox1.Items.Clear()
                    ComboBox1.Items.Add("All")
                    ComboBox1.Items.Add("Dispersal")
                    ComboBox1.Items.Add("Vicariance")
                    ComboBox1.Items.Add("Extinction")
                    ComboBox1.Items.Add("Standard")
                    ComboBox1.SelectedIndex = 0
                    Global_Info()
                    TextBox1.Text = Global_Text
                    Dim th1 As New Thread(AddressOf EventLine)
                    th1.Start()
                End If
            Case 1
                Try
                    If draw_result And StartTreeView Then

                        TimeView = 1
                        draw_area_para(0) = 0
                        start_draw_area = False
                        TabControl2.SelectedIndex = 1

                        ReDim draw_area(CInt(maxtime * root_time * smooth_x), RangeLength)
                        ReDim draw_pie(CInt(maxtime * root_time * smooth_x), RangeLength)
                        draw_area_para(1) = CInt(maxtime * root_time * smooth_x)
                        draw_area_para(2) = 1 'NumericUpDown3.Value
                        draw_area_para(3) = RangeLength
                        ComboBox1.Items.Clear()
                        ComboBox1.Items.Add("All")
                        For i As Integer = 1 To draw_area_para(3)
                            ComboBox1.Items.Add(Chr(64 + i))
                        Next
                        ComboBox1.SelectedIndex = 0
                        Dim th1 As New Thread(AddressOf AreaLine)
                        th1.Start()
                        'CheckForIllegalCrossThreadCalls = True
                    End If
                Catch ex As Exception
                    TextBox2.Text = "Only for Bayesian Result!"
                End Try
        End Select
    End Sub
    Dim draw_area(,) As Single
    Dim draw_pie(,) As String
    Dim draw_area_para(3) As Single
    Dim start_draw_area As Boolean = False
    Public Sub AreaLine()
        Dim info_text As String = ""
        For n As Integer = 1 To RangeLength
            info_text = info_text + "	" + Chr(n + 64)
        Next
        For i As Integer = 0 To CInt(maxtime * root_time * smooth_x) 'Step NumericUpDown3.Value
            ProgressBar1.Value = i * 100 / CInt(maxtime * root_time * smooth_x)
            Dim temp_time_area() As Single
            ReDim temp_time_area(RangeLength)
            temp_time_area(0) = i
            draw_area(i, 0) = i
            If SingleClade.Checked Then
                Dim temp_array() As String = node_line(NumericUpDown3.Value).Split(New Char() {","c})
                For Each k As String In temp_array
                    If k <> "" Then
                        Dim j As Integer = CInt(k) - NumofTaxon - 1
                        For n As Integer = 1 To RangeLength
                            temp_time_area(n) += PROB_list(j, n) * Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        Next
                    End If
                Next
            Else
                For j As Integer = 0 To time_view.Count - 1
                    'If (i + NumericUpDown3.Value) >= Val(time_view.Item(j).Item(2)) And i <= Val(time_view.Item(j).Item(2)) Then
                    For n As Integer = 1 To RangeLength
                        temp_time_area(n) += PROB_list(j, n) * Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                    Next
                    ' End If
                Next
            End If


            'For j As Integer = 0 To NumofTaxon - 1
            '    'If (i + NumericUpDown3.Value) >= Val(TaxonTime(j, 2)) And i <= Val(TaxonTime(j, 2)) Then
            '    For Each c As Char In Distribution(j).ToUpper
            '        temp_time_area(Asc(c) - 64) += 1 * Exp(-(i / root_time) ^ 2 / 2)
            '    Next
            '    ' End If
            'Next
            info_text = info_text + vbCrLf + temp_time_area(0).ToString
            For n As Integer = 1 To RangeLength
                draw_area(i, n) = temp_time_area(n)
                draw_pie(i, n) = ""
                If draw_area_para(0) < temp_time_area(n) Then
                    draw_area_para(0) = temp_time_area(n)
                End If
                info_text = info_text + "	" + temp_time_area(n).ToString
            Next
        Next
        If SingleClade.Checked Then
            Dim temp_array() As String = node_line(NumericUpDown3.Value).Split(New Char() {","c})
            For Each k As String In temp_array
                If k <> "" Then
                    Dim j As Integer = CInt(k) - NumofTaxon - 1
                    For n As Integer = 1 To RangeLength
                        draw_pie(CInt(Val(time_view.Item(j).Item(2)) * smooth_x), n) += "," + (PROB_list(j, n)).ToString
                    Next
                End If
            Next
        Else
            For j As Integer = 0 To time_view.Count - 1
                For n As Integer = 1 To RangeLength
                    draw_pie(CInt(Val(time_view.Item(j).Item(2)) * smooth_x), n) += "," + (PROB_list(j, n)).ToString
                Next
            Next
        End If

        TextBox2.Text = info_text
        start_draw_area = True
        diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
        draw_diagram(Graphics.FromImage(diagram_pic))
        PictureBox3.Refresh()
        ProgressBar1.Value = 0
    End Sub
    Public Function node_line(ByVal n As Integer) As String
        Dim line As String = n.ToString + ","
        Dim anc_node() As String = Poly_Node(n - NumofTaxon - 1, 2).Split(New Char() {","c})
        For Each j As String In anc_node
            If j <> "" Then
                line = line + node_line(j + NumofTaxon + 1)
            End If

        Next
        Return line
    End Function
    Private Sub AreaLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If draw_result And StartTreeView Then

                TimeView = 1
                draw_area_para(0) = 0
                start_draw_area = False
                TabControl2.SelectedIndex = 1

                ReDim draw_area(CInt(maxtime * root_time), RangeLength)
                ReDim draw_pie(CInt(maxtime * root_time), RangeLength)
                draw_area_para(1) = CInt(maxtime * root_time)
                draw_area_para(2) = 1 'NumericUpDown3.Value
                draw_area_para(3) = RangeLength
                ComboBox1.Items.Clear()
                ComboBox1.Items.Add("All")
                For i As Integer = 1 To draw_area_para(3)
                    ComboBox1.Items.Add(Chr(64 + i))
                Next
                ComboBox1.SelectedIndex = 0
                Dim th1 As New Thread(AddressOf AreaLine)
                th1.Start()
                'CheckForIllegalCrossThreadCalls = True
            End If
        Catch ex As Exception
            TextBox2.Text = "Only for Bayesian Result!"
        End Try
    End Sub
    Dim average_value As Single
    Public Sub EventLine()
        average_value = 0
        Dim info_text As String = ""
        ReDim draw_area(CInt(maxtime * root_time * smooth_x), 4)
        ReDim draw_pie(CInt(maxtime * root_time * smooth_x), 4)
        info_text = "Dis. = Dispersal; Vic. = Vicariance; Ext. = Extinction" + vbCrLf
        info_text = info_text + "TIME	" + "Dis." + "	" + "Vic." + "	" + "Ext." + "	" + "Standard"
        Dim temp_node As Integer
        Dim temp_num As Integer = 0
        For i As Integer = 0 To CInt(maxtime * root_time * smooth_x)
            ProgressBar1.Value = i * 100 / CInt(maxtime * root_time * smooth_x)
            Dim temp_time_area() As Single

            ReDim temp_time_area(4)
            temp_time_area(0) = i
            If SingleClade.Checked Then
                Dim temp_array() As String = node_line(NumericUpDown3.Value).Split(New Char() {","c})
                For Each k As String In temp_array
                    If k <> "" Then
                        Dim j As Integer = CInt(k) - NumofTaxon - 1
                        If FromParents.Checked Then
                            If j <> time_view.Count - 1 Then
                                temp_node = CInt(Poly_Node(j, 9))
                                temp_time_area(1) += RASP_Event(j + 1, 0) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                                temp_time_area(2) += RASP_Event(j + 1, 1) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                                temp_time_area(3) += RASP_Event(j + 1, 2) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                                temp_time_area(4) += Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            End If
                        Else
                            temp_node = j
                            temp_time_area(1) += RASP_Event(j + 1, 0) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(2) += RASP_Event(j + 1, 1) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(3) += RASP_Event(j + 1, 2) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(4) += Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        End If

                    End If
                Next
                If FromParents.Checked And NumericUpDown3.Value <> NumericUpDown3.Maximum Then
                    temp_node = CInt(Poly_Node(CInt(NumericUpDown3.Value - NumofTaxon - 1), 9))
                    temp_time_area(4) += Exp(-((i - Val(time_view.Item(temp_node).Item(2))) / root_time) ^ 2 / 2)
                End If
            Else
                For j As Integer = 0 To time_view.Count - 1
                    If FromParents.Checked Then
                        If j <> time_view.Count - 1 Then
                            temp_node = CInt(Poly_Node(j, 9))
                            temp_time_area(1) += RASP_Event(j + 1, 0) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(2) += RASP_Event(j + 1, 1) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(3) += RASP_Event(j + 1, 2) * Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                            temp_time_area(4) += Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        End If
                    Else
                        temp_node = j
                        Dim e1, e2, e3, e4 As Single
                        e1 = RASP_Event(j + 1, 0) * Math.Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        e2 = RASP_Event(j + 1, 1) * Math.Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        e3 = RASP_Event(j + 1, 2) * Math.Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        'e4 = RASP_Event(j + 1, 3) * Math.Exp(-((i - Val(time_view.Item(temp_node).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)
                        e4 = Exp(-((i - Val(time_view.Item(j).Item(2)) * smooth_x) / smooth_x / unit_time) ^ 2 / 2)

                        temp_time_area(1) += e1
                        temp_time_area(2) += e2
                        temp_time_area(3) += e3
                        temp_time_area(4) += e4
                    End If
                Next
            End If
            draw_area(i, 1) = temp_time_area(1)
            draw_area(i, 2) = temp_time_area(2)
            draw_area(i, 3) = temp_time_area(3)
            draw_area(i, 4) = temp_time_area(4)
            draw_pie(i, 1) = ""
            draw_pie(i, 2) = ""
            draw_pie(i, 3) = ""
            draw_pie(i, 4) = ""
            info_text = info_text + vbCrLf + (temp_time_area(0) / smooth_x).ToString("F2")
            For n As Integer = 1 To 4
                info_text = info_text + "	" + temp_time_area(n).ToString
                If draw_area_para(0) < temp_time_area(n) Then
                    draw_area_para(0) = temp_time_area(n)
                End If
            Next
        Next

        If SingleClade.Checked Then
            Dim temp_array() As String = node_line(NumericUpDown3.Value).Split(New Char() {","c})
            For Each k As String In temp_array
                If k <> "" Then
                    Dim j As Integer = CInt(k) - NumofTaxon - 1
                    If FromParents.Checked Then
                        temp_node = CInt(Poly_Node(j, 9))
                    Else
                        temp_node = j
                    End If
                    draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 1) += "," + (RASP_Event(j + 1, 0)).ToString
                    draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 2) += "," + (RASP_Event(j + 1, 1)).ToString
                    draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 3) += "," + (RASP_Event(j + 1, 2)).ToString
                    draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 4) = "1"

                End If
            Next
            If FromParents.Checked And NumericUpDown3.Value <> NumericUpDown3.Maximum Then
                temp_node = CInt(Poly_Node(CInt(NumericUpDown3.Value - NumofTaxon - 1), 9))
                draw_pie(CInt(time_view.Item(temp_node).Item(2)), 4) = "1"
            End If
        Else
            For j As Integer = 0 To time_view.Count - 1
                If FromParents.Checked Then
                    temp_node = CInt(Poly_Node(j, 9))
                Else
                    temp_node = j
                End If
                draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 1) += "," + (RASP_Event(j + 1, 0)).ToString
                draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 2) += "," + (RASP_Event(j + 1, 1)).ToString
                draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 3) += "," + (RASP_Event(j + 1, 2)).ToString
                draw_pie(CInt(Val(time_view.Item(temp_node).Item(2)) * smooth_x), 4) = "1"
            Next
        End If

        If SingleClade.Checked Then
            Dim temp_array() As String = node_line(NumericUpDown3.Value).Split(New Char() {","c})
            For Each k As String In temp_array
                If k <> "" Then
                    Dim j As Integer = CInt(k) - NumofTaxon - 1
                    If RASP_Event(j + 1, 3) >= 1 Then
                        average_value += RASP_Event(j + 1, 3)
                        temp_num += 1
                    End If
                End If
            Next
        Else
            For j As Integer = 0 To time_view.Count - 1
                If RASP_Event(j + 1, 3) >= 1 Then
                    average_value += RASP_Event(j + 1, 3)
                    temp_num += 1
                End If
            Next
        End If
        average_value = average_value / temp_num
        TextBox2.Text = info_text
        start_draw_area = True
        diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
        draw_diagram(Graphics.FromImage(diagram_pic))
        PictureBox3.Refresh()
        ProgressBar1.Value = 0
    End Sub
    Private Sub EventLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)


    End Sub

    Private Sub TreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Dim Table_font As Font = New Font("Tahoma", 10, FontStyle.Regular)

    Public Sub draw_diagram(ByVal TempGrap As Object)
        If start_draw_area And draw_area_para(1) > 0 Then
            TempGrap.FillRectangle(Brushes.White, 0, 0, PictureBox3.Width, PictureBox3.Height)
            TempGrap.SmoothingMode = SmoothingMode.AntiAlias
            Dim x1, x2, y1, y2, left, top As Integer
            left = 20
            top = 40
            Dim grid_pen As New System.Drawing.Pen(System.Drawing.Color.LightGray, 1)
            For x As Integer = 0 To draw_area_para(1) Step NumericUpDown1.Value * smooth_x
                TempGrap.DrawLine(grid_pen, PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * x + left), top, PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * x + left), PictureBox3.Height - top)
                TempGrap.DrawString((x / smooth_x).ToString, Table_font, Brushes.Black, CInt(PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * x + left) - x.ToString.Length * Table_font.SizeInPoints / 2), CInt(PictureBox3.Height - top + Table_font.Height / 2))
            Next
            Dim temp_array As Integer = 0
            For y As Integer = top To PictureBox3.Height - top Step (PictureBox3.Height - 2 * top) / draw_area_para(0) * NumericUpDown4.Value
                TempGrap.DrawLine(grid_pen, left, PictureBox3.Height - y, PictureBox3.Width - left, PictureBox3.Height - y)
                TempGrap.DrawString((temp_array * NumericUpDown4.Value).ToString, Table_font, Brushes.Black, 0, CInt(PictureBox3.Height - y - Table_font.Height / 2))
                temp_array += 1
            Next
            For i As Integer = 0 To draw_area_para(1) Step draw_area_para(2)
                If ComboBox1.SelectedIndex <> 0 Then
                    For n As Integer = ComboBox1.SelectedIndex To ComboBox1.SelectedIndex
                        x1 = PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * i + left)
                        y1 = PictureBox3.Height - draw_area(CInt(i / draw_area_para(2)), n) / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                        If Showline.Checked Then
                            If i <= draw_area_para(1) - draw_area_para(2) Then
                                x2 = PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * (i + draw_area_para(2)) + left)
                                y2 = PictureBox3.Height - draw_area(CInt((i + draw_area_para(2)) / draw_area_para(2)), n) / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                                Dim new_pen As New System.Drawing.Pen(Int2Color(Distributiton_to_Integer(Chr(n + 64))), 2)
                                TempGrap.DrawLine(new_pen, x1, y1, x2, y2)
                            End If
                        End If

                        If ShowPoint.Checked And draw_pie(CInt(i / draw_area_para(2)), n) <> "" Then
                            For Each temp_pie As String In draw_pie(CInt(i / draw_area_para(2)), n).Split(New Char() {","c})
                                If temp_pie <> "" Then
                                    y1 = PictureBox3.Height - temp_pie / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                                    TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(Chr(n + 64))), x1 - 4, y1 - 4, 8, 8, 0, CSng(359.99))
                                    TempGrap.DrawEllipse(Pens.Black, x1 - 4, y1 - 4, 8, 8)
                                End If
                            Next
                        End If
                    Next
                Else
                    For n As Integer = 1 To draw_area_para(3)
                        x1 = PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * i + left)
                        y1 = PictureBox3.Height - draw_area(CInt(i / draw_area_para(2)), n) / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                        If Showline.Checked Then
                            If i <= draw_area_para(1) - draw_area_para(2) Then
                                x2 = PictureBox3.Width - ((PictureBox3.Width - 2 * left) / draw_area_para(1) * (i + draw_area_para(2)) + left)
                                y2 = PictureBox3.Height - draw_area(CInt((i + draw_area_para(2)) / draw_area_para(2)), n) / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                                Dim new_pen As New System.Drawing.Pen(Int2Color(Distributiton_to_Integer(Chr(n + 64))), 2)
                                TempGrap.DrawLine(new_pen, x1, y1, x2, y2)
                            End If
                        End If

                        If ShowPoint.Checked And draw_pie(CInt(i / draw_area_para(2)), n) <> "" Then
                            For Each temp_pie As String In draw_pie(CInt(i / draw_area_para(2)), n).Split(New Char() {","c})
                                If temp_pie <> "" Then
                                    y1 = PictureBox3.Height - Val(temp_pie) / draw_area_para(0) * (PictureBox3.Height - 2 * top) - top
                                    TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(Chr(n + 64))), x1 - 4, y1 - 4, 8, 8, 0, CSng(359.99))
                                    TempGrap.DrawEllipse(Pens.Black, x1 - 4, y1 - 4, 8, 8)
                                End If
                            Next

                        End If
                    Next
                End If

            Next
            'If ShowDivider.Checked And TimeView = 2 Then
            '    TempGrap.DrawLine(New System.Drawing.Pen(Color.Black, 3), left, PictureBox3.Height - top - average_value / draw_area_para(0) * (PictureBox3.Height - 2 * top), PictureBox3.Width - left, PictureBox3.Height - top - average_value / draw_area_para(0) * (PictureBox3.Height - 2 * top))
            '    TempGrap.DrawString(average_value.ToString("F2"), Table_font, Brushes.Black, PictureBox3.Width - left, PictureBox3.Height - top - average_value / draw_area_para(0) * (PictureBox3.Height - 2 * top))
            'End If
            If Showlegend.Checked Then
                Dim dis As Integer = 100
                If ComboBox1.Items(1).ToString.Length < 2 Then
                    dis = 60
                End If
                If ComboBox1.SelectedIndex > 0 Then
                    For n As Integer = ComboBox1.SelectedIndex To ComboBox1.SelectedIndex '
                        Dim new_pen As New System.Drawing.Pen(Int2Color(Distributiton_to_Integer(Chr(n + 64))), 2)

                        If Showline.Checked Then
                            TempGrap.DrawLine(new_pen, -left + dis + 5, CInt(top + 20 + Table_font.Height / 2), -left + dis + 35, CInt(top + 20 + Table_font.Height / 2))
                        End If
                        TempGrap.DrawString(ComboBox1.Text, Table_font, Brushes.Black, 20, top + 20)
                        If ShowPoint.Checked Then
                            TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(Chr(n + 64))), -left + dis + 44, CInt(top + 20 + Table_font.Height / 4), 8, 8, 0, CSng(359.99))
                            TempGrap.DrawEllipse(Pens.Black, -left + dis + 44, CInt(top + 20 + Table_font.Height / 4), 8, 8)
                        End If

                    Next
                Else
                    For n As Integer = 1 To ComboBox1.Items.Count - 1
                        Dim new_pen As New System.Drawing.Pen(Int2Color(Distributiton_to_Integer(Chr(n + 64))), 2)
                        If Showline.Checked Then
                            TempGrap.DrawLine(new_pen, -left + dis + 5, CInt(top + 20 + Table_font.Height / 2) + (n - 1) * (Table_font.Height + 4), -left + dis + 35, CInt(top + 20 + +Table_font.Height / 2) + (n - 1) * (Table_font.Height + 4))
                        End If
                        TempGrap.DrawString(ComboBox1.Items(n).ToString, Table_font, Brushes.Black, 20, top + 20 + (n - 1) * (Table_font.Height + 4))

                        If ShowPoint.Checked Then
                            TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(Chr(n + 64))), -left + dis + 44, CInt(top + 20 + Table_font.Height / 4) + (n - 1) * (Table_font.Height + 4), 8, 8, 0, CSng(359.99))
                            TempGrap.DrawEllipse(Pens.Black, -left + dis + 44, CInt(top + 20 + Table_font.Height / 4) + (n - 1) * (Table_font.Height + 4), 8, 8)
                        End If
                    Next
                End If




            End If
        End If
    End Sub

    Private Sub PictureBox3_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox3.Paint
        e.Graphics.DrawImage(diagram_pic, 0, 0)
    End Sub



    Private Sub ToplogyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ToplogyToolStripMenuItem.Click
        If StartTreeView Then
            'make_tree_xy()
            If Loading = False Then
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
            End If

        End If
    End Sub

    Private Sub MostLikelyStatesOnlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MostLikelyStatesOnlyToolStripMenuItem.Click
        If Loading = False Then
            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
        End If

        KeyStatesOnlyToolStripMenuItem.Enabled = MostLikelyStatesOnlyToolStripMenuItem.Checked
        EventNodesOnlyToolStripMenuItem.Enabled = MostLikelyStatesOnlyToolStripMenuItem.Checked

    End Sub

    Private Sub KeyStatesOnlyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles KeyStatesOnlyToolStripMenuItem.Click

        If KeyStatesOnlyToolStripMenuItem.Checked Then
            EventNodesOnlyToolStripMenuItem.Checked = False
        End If
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()


    End Sub

    Private Sub ExcludeTerminalNodesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventNodesOnlyToolStripMenuItem.Click

        If EventNodesOnlyToolStripMenuItem.Checked Then
            KeyStatesOnlyToolStripMenuItem.Checked = False
        End If
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If start_draw_area Then
            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If
    End Sub

    Private Sub SaveDiagramToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveDiagramToolStripMenuItem.Click
        Try
            Dim bitmap As New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))

            Dim sfd As New SaveFileDialog
            sfd.Filter = "PNG Files(*.png)|*.png;*.PNG|SVG (Adobe Illustrator)|*.svg;*.SVG|Windows Metafile(*.emf)|*.emf;*.EMF|ALL Files(*.*)|*.*"
            sfd.FileName = ""
            sfd.DefaultExt = ".png"
            sfd.CheckPathExists = True
            Dim resultdialog As DialogResult = sfd.ShowDialog()
            If resultdialog = DialogResult.OK Then
                If sfd.FileName.ToLower.EndsWith(".emf") Then
                    Dim g As Graphics = Graphics.FromImage(bitmap)
                    Dim wmf As New Drawing.Imaging.Metafile(sfd.FileName, g.GetHdc())
                    Dim ig As Graphics = Graphics.FromImage(wmf)
                    savingpic = True
                    draw_diagram(ig)
                    savingpic = False
                    ig.Dispose()
                    wmf.Dispose()
                    g.ReleaseHdc()
                    g.Dispose()
                ElseIf sfd.FileName.ToLower.EndsWith(".svg") Then
                    Dim svg_gra As New SvgNet.SvgGdi.SvgGraphics
                    savingpic = True
                    draw_diagram(svg_gra)
                    savingpic = False
                    Dim svg_writer As New StreamWriter(sfd.FileName, False)
                    svg_writer.Write(svg_gra.WriteSVGString)
                    svg_writer.Close()
                    svg_gra.Flush()
                Else
                    Dim TempGrap As Graphics = Graphics.FromImage(bitmap)
                    savingpic = True
                    draw_diagram(TempGrap)
                    savingpic = False
                    bitmap.Save(sfd.FileName)
                End If
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox3.Click

    End Sub
    Dim diagram_pic As Bitmap
    Private Sub PictureBox3_SizeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox3.SizeChanged
        If PictureBox3.Width > 1 And PictureBox3.Height > 1 Then
            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If

    End Sub
    Private Sub ShowPoint_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ShowPoint.CheckedChanged, Showline.CheckedChanged
        If start_draw_area Then
            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        If IsNumeric(NumericUpDown1.Value) Then
            If ListBox1.Items.Count > 0 And begin_draw Then
                Load_result()
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()

                PictureBox2.Height = PicBox2_High()
                Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
                draw_col(Graphics.FromImage(Bitmap_Legend), 1)
                PictureBox2.Refresh()
                PictureBox1.Focus()
            ElseIf StartTreeView And Loading = False Then
                PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                draw_tree(Graphics.FromImage(Bitmap_Tree))
                PictureBox1.Refresh()
                PictureBox1.Focus()
            End If

            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If
    End Sub

    Private Sub NumericUpDown4_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown4.ValueChanged
        If IsNumeric(NumericUpDown4.Value) Then
            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If
    End Sub

    Private Sub ReadLagrangeFileToolStripMenuItem_DropDownClosed(ByVal sender As Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UseSingleAreaModelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UseSingleAreaModelToolStripMenuItem.Click
        UseSingleAreaModelToolStripMenuItem.Checked = UseSingleAreaModelToolStripMenuItem.Checked Xor True
        Global_Info()
        TextBox1.Text = Global_Text

    End Sub
    Dim swaping As Boolean = False
    Private Sub SwapSubtreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwapSubtreeToolStripMenuItem.Click
        If Selected_node >= 0 Then
            swaping = True
            show_my_tree = Swap_tree(show_my_tree, Selected_node + 1)
            Load_result()
            cale_relation()
            Global_Info()
            TextBox1.Text = Global_Text

            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
            swaping = False
        End If

    End Sub


    Private Sub Showlegend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Showlegend.CheckedChanged
        If start_draw_area Then
            diagram_pic = New Bitmap(CInt(PictureBox3.Width), CInt(PictureBox3.Height))
            draw_diagram(Graphics.FromImage(diagram_pic))
            PictureBox3.Refresh()
        End If
    End Sub

    Private Sub MenuStrip1_ItemClicked(ByVal sender As Object, ByVal e As ToolStripItemClickedEventArgs) Handles MenuStrip1.ItemClicked

    End Sub

    Private Sub TreeWithTimeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles TreeWithTimeToolStripMenuItem.Click
        If StartTreeView Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Tree File (*.tree)|*.tree|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".tree"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim temp_range As String = ""
                For i As Integer = 0 To NumofTaxon - 1
                    For Each c As Char In Distribution(i)
                        If temp_range.Contains(c) = False Then
                            temp_range += c
                        End If
                    Next
                Next
                RangeLength = temp_range.Length
                Dim wr As New StreamWriter(opendialog.FileName, False)
                Dim temp As String = "Tree="
                For i As Integer = 1 To Tree_Export_Char.Length - 1
                    If Tree_Export_Char(i).Contains(":") Then
                        If Tree_Export_Char(i - 1) <> ")" Then
                            temp += TaxonName(CInt(Tree_Export_Char(i).Split(New Char() {":"c})(0)) - 1) + ":" + (Val(Tree_Export_Char(i).Split(New Char() {":"c})(1)) * root_time).ToString("F8")
                        Else
                            temp += Val(Tree_Export_Char(i).Split(New Char() {":"c})(0)).ToString + ":" + (Val(Tree_Export_Char(i).Split(New Char() {":"c})(1)) * root_time).ToString("F8")
                        End If
                        If Tree_Export_Char(i).Contains(";") Then
                            temp += ";"
                        End If
                    ElseIf Tree_Export_Char(i).Contains(";") Then
                        temp += ";"
                    Else
                        temp += Tree_Export_Char(i)
                    End If
                Next
                If temp.EndsWith(";") = False Then
                    temp += ";"
                End If
                wr.WriteLine(temp)
                wr.Close()
                MsgBox("Export Successfully!")
            End If
        End If
    End Sub

    Private Sub CurrentTreeToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CurrentTreeToolStripMenuItem.Click
        If show_my_tree <> "" Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Phylip Tree File (*.tre)|*.tre;*.TRE|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".tre"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim wr As New StreamWriter(opendialog.FileName, False)
                If opendialog.FileName.ToUpper.EndsWith(".TRE") Then
                    Dim temp_tree As String = show_my_tree + ";"
                    For i As Integer = 1 To NumofTaxon
                        temp_tree = temp_tree.Replace("(" + i.ToString + ",", "($%*" + i.ToString + "$%*,")
                        temp_tree = temp_tree.Replace("," + i.ToString + ")", ",$%*" + i.ToString + "$%*)")
                        temp_tree = temp_tree.Replace("," + i.ToString + ",", ",$%*" + i.ToString + "$%*,")
                        temp_tree = temp_tree.Replace("," + i.ToString + ":", ",$%*" + i.ToString + "$%*:")
                        temp_tree = temp_tree.Replace("(" + i.ToString + ":", "($%*" + i.ToString + "$%*:")
                    Next
                    For i As Integer = 1 To NumofTaxon
                        temp_tree = temp_tree.Replace("($%*" + i.ToString + "$%*,", "(" + TaxonName(i - 1) + ",")
                        temp_tree = temp_tree.Replace(",$%*" + i.ToString + "$%*)", "," + TaxonName(i - 1) + ")")
                        temp_tree = temp_tree.Replace(",$%*" + i.ToString + "$%*,", "," + TaxonName(i - 1) + ",")
                        temp_tree = temp_tree.Replace(",$%*" + i.ToString + "$%*:", "," + TaxonName(i - 1) + ":")
                        temp_tree = temp_tree.Replace("($%*" + i.ToString + "$%*:", "(" + TaxonName(i - 1) + ":")
                    Next
                    wr.WriteLine(temp_tree)
                End If
                wr.Close()
            End If
        Else
            MsgBox("No tree in memory!")
        End If
    End Sub

    Private Sub ExportDistributionToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExportDistributionToolStripMenuItem.Click
        If Distribution.Length > 1 Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".csv"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim dw As New StreamWriter(opendialog.FileName, False)
                For i As Integer = 0 To UBound(Distribution)
                    dw.WriteLine((i + 1).ToString + "," + TaxonName(i) + "," + Distribution(i))
                Next
                dw.Close()
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        End If

    End Sub

    Private Sub SwapWholeTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SwapWholeTreeToolStripMenuItem.Click
        swaping = True
        show_my_tree = Swap_Whole(show_my_tree)
        If draw_result = True Then
            '[加载结果r1]
            Load_result()
            cale_relation()
            Global_Info()
            TextBox1.Text = Global_Text
        End If
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()
        swaping = False
    End Sub
    Public Function Swap_Whole(ByVal Treeline As String) As String
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
        Dim tree_last_char As Integer = char_id
        Dim new_tree_char() As String
        ReDim new_tree_char(tree_last_char)

        Dim new_tree_node() As String
        ReDim new_tree_node(tree_last_char)
        Dim node_array As Integer = NumofNode - 1
        For i As Integer = 0 To tree_last_char - 1
            Dim temp_char As String = tree_char(tree_last_char - i)
            If new_tree_char(i) = "" Then
                Select Case temp_char
                    Case "("
                        new_tree_char(i) = ")"
                    Case ")"
                        new_tree_char(i) = "("
                    Case ","
                        new_tree_char(i) = ","
                    Case Else
                        If tree_char(tree_last_char - i - 1) = ")" Then
                            r_c = 0
                            l_c = 0
                            Dim change_index As Integer = 0
                            For k As Integer = 1 To tree_last_char - i - 1
                                If tree_char(tree_last_char - i - k) = ")" Then
                                    r_c += 1
                                End If
                                If tree_char(tree_last_char - i - k) = "(" Then
                                    l_c += 1
                                End If
                                If r_c = l_c Then
                                    change_index = tree_last_char - i - k
                                    Exit For
                                End If
                            Next
                            new_tree_char(tree_last_char - change_index) = ")" + tree_char(tree_last_char - i)
                            new_tree_node(tree_last_char - change_index) = node_array
                            node_array = node_array - 1
                            new_tree_char(i) = ""
                        Else
                            new_tree_char(i) = temp_char
                        End If

                End Select
            End If
        Next
        Dim new_tree As String = ""
        For i As Integer = 0 To tree_last_char
            new_tree = new_tree + new_tree_char(i)
        Next

        Treeline = new_tree
        Dim node_line_old() As String
        ReDim node_line_old(NumofNode - 1)
        For i As Integer = 0 To NumofNode - 1
            Dim temp_arry() As String = Poly_Node(i, 3).Split(",")
            Array.Sort(temp_arry)
            Poly_Node(i, 3) = temp_arry(0)
            For j As Integer = 1 To UBound(temp_arry)
                Poly_Node(i, 3) += "," + temp_arry(j)
            Next
            node_line_old(i) = Poly_Node(i, 3)
        Next
        Read_Poly_Tree(Treeline)
        Dim node_line_new() As String
        ReDim node_line_new(NumofNode - 1)
        For i As Integer = 0 To NumofNode - 1
            Dim temp_arry() As String = Poly_Node(i, 3).Split(",")
            Array.Sort(temp_arry)
            Poly_Node(i, 3) = temp_arry(0)
            For j As Integer = 1 To UBound(temp_arry)
                Poly_Node(i, 3) += "," + temp_arry(j)
            Next
            node_line_new(i) = Poly_Node(i, 3)
        Next

        node_array = 0

        If draw_result = True Then
            Dim temp_array As Integer = 0
            Dim temp_string(UBound(node_line_old)) As String
            Do
                For i As Integer = 0 To UBound(node_line_new)
                    temp_string(i) = Result_list(Array.IndexOf(node_line_old, node_line_new(i)) + NumofNode * temp_array)
                Next
                For i As Integer = 0 To UBound(node_line_new)
                    Result_list(i + NumofNode * temp_array) = temp_string(i)
                Next
                temp_array += 1
            Loop Until (NumofNode + 1) * temp_array + 1 > ListBox1.Items.Count

            If RadioButton2.Enabled Then
                Dim temp_PROB(UBound(node_line_new) + 1, RangeLength) As String
                For i As Integer = 1 To UBound(node_line_new) + 1
                    For j As Integer = 1 To RangeLength
                        temp_PROB(i, j) = PROB_list(Array.IndexOf(node_line_old, node_line_new(i - 1)), j)
                    Next
                Next

                For i As Integer = 1 To UBound(node_line_new) + 1
                    For j As Integer = 1 To RangeLength
                        PROB_list(Array.IndexOf(node_line_old, node_line_new(i - 1)), j) = temp_PROB(i, j)
                    Next
                Next
            End If
        End If
        Return Treeline
    End Function

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub TestToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        For i As Integer = 0 To NumofNode - 1
            Dim temp_arry() As String = Poly_Node(i, 3).Split(",")
            Array.Sort(temp_arry)
            Poly_Node(i, 3) = temp_arry(0)
            For j As Integer = 1 To UBound(temp_arry)
                Poly_Node(i, 3) += "," + temp_arry(j)
            Next
            MsgBox(Poly_Node(i, 3))
        Next
    End Sub

    Private Sub GoToToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GoToToolStripMenuItem.Click
        Dim select_id As Integer = -1
        Dim max_sv As Single = 0
        If ListBox1.Items.Count > 1 Then
            For i As Integer = 0 To ListBox1.Items.Count - 1
                If ListBox1.Items(i).ToString.Contains("Value") Then
                    If CSng(ListBox1.Items(i).ToString.Split("=")(1)) > max_sv Then
                        max_sv = CSng(ListBox1.Items(i).ToString.Split("=")(1))
                        select_id = i
                    End If
                End If
            Next
        End If
        If select_id > 0 Then
            ListBox1.SelectedItems.Clear()
            ListBox1.SelectedIndex = select_id
            Global_Info()
            TextBox1.Text = Global_Text
        End If
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox2.Click

    End Sub

    Private Sub ListView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListView1.DoubleClick
        If ListView1.Items.Count > 0 Then
            If ListView1.SelectedIndices(0) >= 0 Then
                Dim current_select As Integer = ListView1.SelectedIndices(0)
                ColorDialog1.Color = ListView1.Items(current_select).SubItems(1).BackColor
                If ColorDialog1.ShowDialog() = DialogResult.OK Then
                    ListView1.Items(current_select).SubItems(1).BackColor = ColorDialog1.Color
                    SetBrushes(Distributiton_to_Integer(Color_S(current_select)), New SolidBrush(ColorDialog1.Color))
                    SetColor(Distributiton_to_Integer(Color_S(current_select)), ColorDialog1.Color)
                    Color_B(current_select) = New SolidBrush(ColorDialog1.Color)
                    ListView1.SelectedItems.Clear()
                    PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
                    PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
                    Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
                    draw_tree(Graphics.FromImage(Bitmap_Tree))
                    PictureBox1.Refresh()

                    PictureBox2.Height = PicBox2_High()
                    Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
                    draw_col(Graphics.FromImage(Bitmap_Legend), 1)
                    PictureBox2.Refresh()
                End If
            End If
        End If
    End Sub

    Private Sub ListView1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListView1.SelectedIndexChanged

    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Global_Info()
        TextBox1.Text = Global_Text
    End Sub

    Private Sub DisplayMostMLSInCenterToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DisplayMostMLSInCenterToolStripMenuItem.Click
        If Loading = False Then
            Display_node_ID = False
            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
        End If
    End Sub

    Private Sub DecreasingTreeToolStripMenuItem_Click(sender As Object, e As EventArgs)
        swaping = True
        For i As Integer = 0 To 0 ' NumofNode - 1
            If Poly_Node(i, 4) > Poly_Node(i, 5) Then
                Selected_node = i - 1
                show_my_tree = Swap_tree(show_my_tree, Selected_node + 1)
                Read_Poly_Tree(show_my_tree)
            End If

        Next


        If draw_result = True Then
            '[加载结果r1]
            Load_result()
            cale_relation()
            Global_Info()
            TextBox1.Text = Global_Text
        End If
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()
        swaping = False
    End Sub

    Private Sub IncreaseTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles IncreaseTreeToolStripMenuItem.Click
        swaping = True
        For i As Integer = 0 To NumofNode - 1
            If Poly_Node(NumofNode - 1 - i, 2) <> "" Then
                If Poly_Node(NumofNode - 1 - i, 2).Split(",").Length > 2 Then
                    Dim l As Integer = Poly_Node(NumofNode - 1 - i, 2).Split(",")(0)
                    Dim r As Integer = Poly_Node(NumofNode - 1 - i, 2).Split(",")(1)
                    If Poly_Node(l, 3).Split(",").Length > Poly_Node(r, 3).Split(",").Length Then
                        show_my_tree = Swap_tree(show_my_tree, NumofNode - i)
                        Read_Poly_Tree(show_my_tree)
                    End If
                ElseIf Poly_Node(NumofNode - 1 - i, 2).Split(",").Length = 2 Then
                    If Poly_Node(NumofNode - 1 - i, 3).StartsWith(Poly_Node(NumofNode - 1 - i, 1)) Then
                        show_my_tree = Swap_tree(show_my_tree, NumofNode - i)
                        Read_Poly_Tree(show_my_tree)
                    End If
                End If
            End If

        Next
        If draw_result = True Then
            '[加载结果r1]
            Load_result()
            cale_relation()
            Global_Info()
            TextBox1.Text = Global_Text
        End If
        PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
        PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
        Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
        draw_tree(Graphics.FromImage(Bitmap_Tree))
        PictureBox1.Refresh()
        swaping = False
    End Sub

    Private Sub SaveCurrentViewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveCurrentViewToolStripMenuItem.Click
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then


            Dim w_f_t As New StreamWriter(opendialog.FileName, False)
            w_f_t.WriteLine("Result View savaed at " + System.DateTime.Now.ToString)


            w_f_t.WriteLine("[TAXON]")
            For i As Integer = 1 To NumofTaxon
                w_f_t.WriteLine(i.ToString + "	" + TaxonName(i - 1) + "	" + Distribution(i - 1))
            Next
            w_f_t.WriteLine("[TREE]")
            w_f_t.WriteLine("Tree=" + show_my_tree)
            w_f_t.WriteLine("[RESULT]")
            For i As Integer = 0 To UBound(Result_list)
                If (i Mod Result_list.Length) = 0 Then
                    w_f_t.WriteLine("Distributions at each node:")
                End If
                w_f_t.WriteLine("node " + (NumofTaxon + i + 1).ToString + ":" + Result_list(i))
            Next
            w_f_t.WriteLine("[TREEVIEW]")
            w_f_t.WriteLine("ShowScale=" + ShowScale.ToString)
            w_f_t.WriteLine("TransparentBG=" + TransparentBG.ToString)
            w_f_t.WriteLine("area_lower=" + area_lower.ToString)
            w_f_t.WriteLine("keep_at_least=" + keep_at_least.ToString)
            w_f_t.WriteLine("Show_area_names=" + Show_area_names.ToString)
            w_f_t.WriteLine("Show_area_pies=" + Show_area_pies.ToString)
            w_f_t.WriteLine("pie_radii=" + pie_radii.ToString)
            w_f_t.WriteLine("Display_taxon_names=" + Display_taxon_names.ToString)
            w_f_t.WriteLine("Display_Null_distribution=" + Display_Null_distribution.ToString)
            w_f_t.WriteLine("Display_taxon_dis=" + Display_taxon_dis.ToString)
            w_f_t.WriteLine("Display_taxon_pie=" + Display_taxon_pie.ToString)
            w_f_t.WriteLine("Display_circle=" + Display_circle.ToString)
            w_f_t.WriteLine("Circle_size=" + Circle_size.ToString)
            w_f_t.WriteLine("Circle_color=" + Circle_color.ToString)
            w_f_t.WriteLine("Tree_font=" + Tree_font.ToString)
            w_f_t.WriteLine("Label_font=" + Label_font.ToString)
            w_f_t.WriteLine("ID_font=" + ID_font.ToString)
            w_f_t.WriteLine("ID_color=" + ID_color.ToString)
            w_f_t.WriteLine("Display_node_frequency=" + Display_node_frequency.ToString)
            w_f_t.WriteLine("Low_frequency=" + Low_frequency.ToString)
            w_f_t.WriteLine("Hide_pie=" + Hide_pie.ToString)
            w_f_t.WriteLine("Display_lines=" + Display_lines.ToString)
            w_f_t.WriteLine("frequency_h=" + frequency_h.ToString)
            w_f_t.WriteLine("frequency_v=" + frequency_v.ToString)
            w_f_t.WriteLine("Display_node_ID=" + Display_node_ID.ToString)
            w_f_t.WriteLine("node_h=" + node_h.ToString)
            w_f_t.WriteLine("node_v=" + node_v.ToString)
            w_f_t.WriteLine("Taxon_separation=" + Taxon_separation.ToString)
            w_f_t.WriteLine("Branch_length=" + Branch_length.ToString)
            w_f_t.WriteLine("Border_separation=" + Border_separation.ToString)
            w_f_t.WriteLine("Line_width=" + Line_width.ToString)
            w_f_t.WriteLine("File_zoom=" + File_zoom.ToString)
            w_f_t.WriteLine("taxon_pie_radii=" + taxon_pie_radii.ToString)
            w_f_t.WriteLine("[END]")
            w_f_t.Close()
            MsgBox("Save Successfully!", MsgBoxStyle.Information)
        Else
            MsgBox("No result to save!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub DispersalInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DispersalInformationToolStripMenuItem.Click
        If TextBox1.Text <> "" Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".txt"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim sw As New StreamWriter(opendialog.FileName)
                sw.Write(TextBox1.Text)
                sw.Close()
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("No information to save!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub SupplementInformationToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SupplementInformationToolStripMenuItem.Click
        If RichTextBox1.Text <> "" Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Excel File (*.xls)|*.xls;*.XLS|Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".xls"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim sw As New StreamWriter(opendialog.FileName)
                sw.Write(RichTextBox1.Text)
                sw.Close()
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Else
            MsgBox("No information to save!", MsgBoxStyle.Information)
        End If
    End Sub

    Private Sub ShowPieOnTerminalToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowPieOnTerminalToolStripMenuItem.Click
        load_color()

        If Loading = False Then
            PictureBox1.Width = (max_level + 2) * Branch_length + 2 * Border_separation + (max_taxon_name + RangeStr.Length + 8) * Label_font.SizeInPoints
            PictureBox1.Height = (NumofTaxon + 5) * Taxon_separation + Border_separation + Label_font.Height + Tree_font.Height 'y
            Bitmap_Tree = New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))
            draw_tree(Graphics.FromImage(Bitmap_Tree))
            PictureBox1.Refresh()
        End If
        PictureBox2.Height = PicBox2_High()
        Bitmap_Legend = New Bitmap(CInt(PictureBox2.Width), CInt(PictureBox2.Height))
        draw_col(Graphics.FromImage(Bitmap_Legend), 1)
        PictureBox2.Refresh()
    End Sub
End Class
