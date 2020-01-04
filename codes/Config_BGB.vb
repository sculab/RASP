
Imports System.IO
Public Class Config_BGB
    Dim range_dataset As New DataSet
    Dim rgView As New DataView
    Dim maxtime As Single
    Dim area_num As Integer
    Dim rang_num As Integer
    Dim Distribution() As String
    Dim TaxonName() As String
    Dim TaxonTime(,) As String
    Dim taxon_array() As String
    Dim has_length As Boolean
    Dim Tree_Export_Char() As String
    Dim root_time As Single
    Dim lag_tree As String
    Dim area_dispersal As String
    Dim excluded_ranges As String
    Dim included_ranges As String
    Dim area_labels As String
    Dim base_rates As String
    Dim dispersal_durations As String
    Dim lag_taxa As String
    Dim taxon_range_data As String
    Dim BGB_Body As String
    Dim BGB_Header As String
    Dim time_period_num As Integer = 1

    Private Sub Config_BGB_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'RangeMade = True
        e.Cancel = True
        Me.Hide()
    End Sub
    Private Function check_exist(ByVal checkstr As String) As Boolean
        Dim Tempchar() As Char = RangeStr.ToUpper
        Array.Sort(Tempchar)
        For i As Integer = 0 To RangeStr.Length - 1
            For j As Integer = i To RangeStr.Length - 2
                If DataGridView1.Rows(i).Cells(j + 1).Value = False Then
                    If checkstr.Contains(Tempchar(i)) And checkstr.Contains(Tempchar(j + 1)) Then
                        Return False
                    End If
                End If
            Next
        Next
        Return True
    End Function
    Private Sub RefreshTheRangeListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        DataGridView1.EndEdit()
        rang_num = DataGridView1.Rows.Count
        Dim Tempchar() As Char = RangeStr.ToUpper
        area_num = CInt(NumericUpDown2.Value)
        Array.Sort(Tempchar)
        For j As Integer = 2 To area_num
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

                If check_exist(Tempstr) Or Array.IndexOf(Distribution, Tempstr) >= 0 Then
                    ListBox1.Items.Add(Tempstr)
                Else
                    ListBox2.Items.Add(Tempstr)
                End If
                isend = pailie(n, j, j)
            Loop Until isend = False
        Next

        'MsgBox(ListBox1.Items.Count)
    End Sub
    Public Function pailie(ByVal n() As Integer, ByVal postion As Integer, ByVal a_num As Integer) As Boolean
        If n(postion) <= rang_num - (a_num - postion) And n(postion) <= n(postion + 1) - 2 Then
            n(postion) = n(postion) + 1
            Return True
        Else
            If postion > 1 Then
                If n(postion - 1) + 2 <= rang_num - (a_num - postion) Then
                    n(postion) = n(postion - 1) + 2
                End If
                For i As Integer = postion + 1 To a_num
                    If n(i - 1) + 1 <= rang_num - (a_num - (i - 1)) Then
                        n(i) = n(i - 1) + 1
                    End If
                Next
                For i As Integer = 1 To a_num
                    If n(i) > rang_num Then
                        Return False
                    End If
                Next
                Return pailie(n, postion - 1, a_num)
            Else
                Return False
            End If
        End If
    End Function
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If ListBox1.SelectedIndex >= 0 Then
            If Array.IndexOf(Distribution, ListBox1.SelectedItem) < 0 Or ListBox1.SelectedItem.ToString.Length = 1 Then
                ListBox2.Items.Add(ListBox1.SelectedItem)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            Else
                MsgBox(ListBox1.SelectedItem + " is the distribution of taxon, you should not exclude it!")
            End If
        End If

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex >= 0 Then
            ListBox1.Items.Add(ListBox2.SelectedItem)
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
    End Sub
    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim Tempchar() As Char = RangeStr.ToUpper
        Array.Sort(Tempchar)
        Dim list_num As Integer = ListBox1.Items.Count
        If DataGridView1.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = False Then
            For i As Integer = 1 To list_num

                If ListBox1.Items(list_num - i).ToString.Contains(Tempchar(e.RowIndex)) And ListBox1.Items(list_num - i).ToString.Contains(Tempchar(e.ColumnIndex)) Then
                    If Array.IndexOf(Distribution, ListBox1.Items(list_num - i).ToString) < 0 Then
                        ListBox2.Items.Add(ListBox1.Items(list_num - i))
                        ListBox1.Items.RemoveAt(list_num - i)
                    End If
                End If
            Next
        End If
    End Sub
    Public Sub Read_LG_Tree(ByVal Treeline As String)
        ReDim Poly_Node(taxon_num - 1 - 1, 9) '0 root,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点
        ReDim TaxonTime(taxon_num - 1, 2)
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
        ReDim Temp_node(taxon_num - 1, 6) '0 节点位置,1 末端, 2 子节点, 4 左侧个数, 5 右侧个数, 6 支持率
        For i As Integer = 0 To taxon_num - 1 - 1
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
						Temp_node(point_1 - 2, 4) = Math.Min(Val(Temp_node(point_1 - 2, 4)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
						Temp_node(point_1 - 2, 5) = Math.Max(Val(Temp_node(point_1 - 2, 5)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
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
						Temp_node(point_1 - 1, 4) = Math.Min(Val(Temp_node(point_1 - 1, 4)), tx)
						Temp_node(point_1 - 1, 5) = Math.Max(Val(Temp_node(point_1 - 1, 4)), tx)
					End If
            End Select
        Next
        make_chain(taxon_num - 1 - 1)
        maxtime = 0
        For i As Integer = 0 To taxon_num - 1
            If maxtime < Val(TaxonTime(i, 1)) Then
                maxtime = Val(TaxonTime(i, 1))
            End If
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If ListBox1.SelectedIndex >= 0 Then
            If Array.IndexOf(Distribution, ListBox1.SelectedItem) < 0 Or ListBox1.SelectedItem.ToString.Length = 1 Then
                ListBox2.Items.Add(ListBox1.SelectedItem)
                ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            Else
                MsgBox(ListBox1.SelectedItem + " is the distribution of taxon, you should not exclude it!")
            End If
        End If
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ListBox2.SelectedIndex >= 0 Then
            ListBox1.Items.Add(ListBox2.SelectedItem)
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
    End Sub
    Private Sub Config_BGB_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            If RangeStr.Length <> DataGridView1.Rows.Count Then
                time_period_num = 1
                ListBox3.Items.Clear()
                ListBox4.Items.Clear()
                ReDim TaxonName(taxon_num - 1)
                ReDim Distribution(taxon_num - 1)
                For i As Integer = 0 To taxon_num - 1
                    TaxonName(i) = dtView.Item(i).Item(1).ToString
                    Distribution(i) = dtView.Item(i).Item(state_index).ToString
                    ListBox3.Items.Add(TaxonName(i))
                    ListBox4.Items.Add(TaxonName(i))
                Next
                Dim Tempchar() As Char = RangeStr.ToUpper
                NumericUpDown2.Maximum = RangeStr.Length
                Array.Sort(Tempchar)
                DataGridView1.Columns.Clear()
                DataGridView1.Rows.Clear()
                DataGridView1.AllowUserToAddRows = True
                DataGridView1.AllowUserToDeleteRows = True
                DataGridView1.AllowUserToOrderColumns = True
                DataGridView1.AllowUserToResizeColumns = True
                DataGridView1.AllowUserToResizeRows = True
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
                RefreshTheRangeListToolStripMenuItem_Click(sender, e)

                DataGridView2.Columns.Clear()
                DataGridView2.Rows.Clear()
                DataGridView2.AllowUserToAddRows = True
                DataGridView2.AllowUserToDeleteRows = True
                DataGridView2.AllowUserToOrderColumns = True
                DataGridView2.AllowUserToResizeColumns = True
                DataGridView2.AllowUserToResizeRows = True
                r = 0
                For Each i As Char In Tempchar
                    Dim Column2 As New DataGridViewTextBoxColumn
                    Column2.HeaderText = i.ToString
                    Column2.Width = 32
                    Column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                    DataGridView2.Columns.Add(Column2)
                    DataGridView2.Rows.Add()
                    DataGridView2.Rows(r).HeaderCell.Value = "0-1 " + i.ToString
                    r = r + 1
                Next
                DataGridView2.RowHeadersWidth = 70
                For i As Integer = 0 To Tempchar.Length - 1
                    For j As Integer = 0 To Tempchar.Length - 1
                        DataGridView2.Rows(i).Cells(j).Value = "1"
                    Next
                Next
                DataGridView2.BackgroundColor = Color.LightGray
                DataGridView2.AllowUserToAddRows = False
                DataGridView2.AllowUserToDeleteRows = False
                DataGridView2.AllowUserToOrderColumns = False
                DataGridView2.AllowUserToResizeColumns = False
                DataGridView2.AllowUserToResizeRows = False
                RefreshTheRangeListToolStripMenuItem_Click(sender, e)

                DataGridView3.Columns.Clear()
                DataGridView3.Rows.Clear()
                Dim Column3 As New DataGridViewTextBoxColumn
                Column3.HeaderText = "Time"
                Column3.Width = 80
                Column3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView3.Columns.Add(Column3)
                DataGridView3.Rows.Add()
                DataGridView3.Rows(0).HeaderCell.Value = "0"
                DataGridView3.Rows(0).Cells(0).Value = "0.0"
                DataGridView3.Rows(0).Cells(0).ReadOnly = True
                DataGridView3.Rows(0).Cells(0).Style.SelectionBackColor = Color.Gray
                DataGridView3.Rows(0).Cells(0).Style.BackColor = Color.Gray
                DataGridView3.BackgroundColor = Color.LightGray
                DataGridView3.AllowUserToAddRows = False
                DataGridView3.AllowUserToDeleteRows = False
                DataGridView3.AllowUserToOrderColumns = False
                DataGridView3.AllowUserToResizeColumns = False
                DataGridView3.AllowUserToResizeRows = False

                DataGridView4.Columns.Clear()
                DataGridView4.Rows.Clear()
                Dim Column4 As New DataGridViewCheckBoxColumn
                Column4.HeaderText = ""
                Column4.Width = 30
                Column4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView4.Columns.Add(Column4)
                For i As Integer = 0 To Tempchar.Length - 1
                    DataGridView4.Rows.Add()
                    DataGridView4.Rows(i).HeaderCell.Value = Tempchar(i).ToString
                Next
                DataGridView4.AllowUserToAddRows = False
                DataGridView4.AllowUserToDeleteRows = False
                DataGridView4.AllowUserToOrderColumns = False
                DataGridView4.AllowUserToResizeColumns = False
                DataGridView4.AllowUserToResizeRows = False

                DataGridView5.Columns.Clear()
                DataGridView5.Rows.Clear()
                Dim Column_Taxon1 As New DataGridViewTextBoxColumn
                Dim Column_Taxon2 As New DataGridViewTextBoxColumn
                Dim Column_Dis As New DataGridViewTextBoxColumn
                Column_Taxon1.HeaderText = "Taxon1"
                Column_Taxon2.HeaderText = "Taxon2"
                Column_Dis.HeaderText = "Range"
                DataGridView5.Columns.Add(Column_Taxon1)
                DataGridView5.Columns.Add(Column_Taxon2)
                DataGridView5.Columns.Add(Column_Dis)

                DataGridView5.Columns(0).ReadOnly = True
                DataGridView5.Columns(1).ReadOnly = True
                DataGridView5.Columns(2).ReadOnly = True

                Dim Column_Check As New DataGridViewCheckBoxColumn
                Column_Check.HeaderText = ""
                DataGridView5.Columns.Add(Column_Check)
                DataGridView5.Columns(0).Width = 85
                DataGridView5.Columns(1).Width = 85
                DataGridView5.Columns(2).Width = 85
                DataGridView5.Columns(3).Width = 40
                DataGridView5.AllowUserToAddRows = False
                DataGridView5.AllowUserToDeleteRows = False
                DataGridView5.AllowUserToOrderColumns = False
                DataGridView5.AllowUserToResizeColumns = False
                DataGridView5.AllowUserToResizeRows = False
                Read_Poly_Tree(tree_show_with_value)
                NumericUpDown2.Minimum = 0
                For Each i As String In Distribution
                    If NumericUpDown2.Minimum < i.Length Then
                        NumericUpDown2.Minimum = i.Length
                    End If
                Next
                If has_length = False Then
                    Me.Visible = False
                End If
            End If
        End If
    End Sub
    Public Sub Read_Poly_Tree(ByVal Treeline As String)
        Dim node_number As Integer = Treeline.Length - Treeline.Replace(")", "").Length
        ReDim Poly_Node(node_number - 1, 9) '0 root,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点
        ReDim TaxonTime(taxon_num - 1, 2)
        has_length = False
        If Treeline.Contains(":") Then
            has_length = True
        End If
        ReDim taxon_array(taxon_num - 1)
        For i As Integer = 0 To node_number - 1
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
                'If Tree_Export_Char(i) = "," Then
                'tree_node += 1
                'End If
                If Tree_Export_Char(i).Contains(":") Then
                    If Tree_Export_Char(i - 1) <> ")" Then
                        TaxonTime(CInt(tree_char(i).Split(New Char() {":"c})(0)) - 1, 0) = tree_char(i).Split(New Char() {":"c})(1)
                    Else
                        tree_node += 1
                        tree_char(i) = ":" + tree_char(i).Split(New Char() {":"c})(1)
                    End If
                End If
                If Tree_Export_Char(i).Contains(";") Then
                    tree_node += 1
                    tree_char(i) = ";"
                End If
            Next
        End If

        Dim point_1, point_2 As Integer
        point_1 = 0
        point_2 = 0
        Dim Temp_node(,) As String
        ReDim Temp_node(node_number, 6) '0 节点位置,1 末端, 2 子节点, 4 左侧个数, 5 右侧个数, 6 支持率
        For i As Integer = 0 To node_number - 1
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
                        Temp_node(point_1 - 2, 4) = Math.Min(Val(Temp_node(point_1 - 2, 4)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
                        Temp_node(point_1 - 2, 5) = Math.Max(Val(Temp_node(point_1 - 2, 5)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
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
                        Temp_node(point_1 - 1, 4) = Math.Min(Val(Temp_node(point_1 - 1, 4)), tx)
                        Temp_node(point_1 - 1, 5) = Math.Max(Val(Temp_node(point_1 - 1, 4)), tx)
                    End If
            End Select
        Next
        If has_length Then
            make_chain(node_number - 1)
        End If
        root_time = 0
        For i As Integer = 0 To taxon_num - 1
            If root_time < Val(TaxonTime(i, 1)) Then
                root_time = Val(TaxonTime(i, 1))
            End If
        Next
        TextBox1.Text = root_time.ToString
        root_time = CSng(root_time.ToString("F3")) + 0.01
        config_tree_time = root_time - 0.01
        If DataGridView3.Rows.Count = 1 Then
            DataGridView3.Rows.Add()
            DataGridView3.Rows(1).HeaderCell.Value = "1"
            DataGridView3.Rows(1).Cells(0).Value = root_time.ToString
            DataGridView3.Rows(1).Cells(0).ReadOnly = True
            DataGridView3.Rows(1).Cells(0).Style.SelectionBackColor = Color.Gray
            DataGridView3.Rows(1).Cells(0).Style.BackColor = Color.Gray
        End If
        lag_tree = ""
        For i As Integer = 1 To char_id
            lag_tree += tree_char(i)
        Next
        For i As Integer = 1 To taxon_num
            lag_tree = lag_tree.Replace("(" + i.ToString + ",", "($%*" + TaxonName(i - 1) + "$%*,")
            lag_tree = lag_tree.Replace("," + i.ToString + ")", ",$%*" + TaxonName(i - 1) + "$%*)")
            lag_tree = lag_tree.Replace("," + i.ToString + ",", ",$%*" + TaxonName(i - 1) + "$%*,")
            lag_tree = lag_tree.Replace("(" + i.ToString + ":", "($%*" + TaxonName(i - 1) + "$%*:")
            lag_tree = lag_tree.Replace("," + i.ToString + ":", ",$%*" + TaxonName(i - 1) + "$%*:")
            lag_tree = lag_tree.Replace("," + i.ToString + ":", ",$%*" + TaxonName(i - 1) + "$%*:")
        Next
        lag_tree = lag_tree.Replace("$%*", "")
    End Sub
    Public Sub make_chain(ByVal n As Integer)
        If Poly_Node(n, 2) <> "" Then
            Dim anc_node() As String = Poly_Node(n, 2).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    If j.Contains(":") Then
                        j = j.Split(New Char() {":"c})(0)
                    End If
                    Poly_Node(CInt(j), 8) = (Val(Poly_Node(CInt(j), 7)) + Val(Poly_Node(n, 8))).ToString
                    make_chain(CInt(j))
                End If
            Next
        End If
        If Poly_Node(n, 1) <> "" Then
            Dim anc_node() As String = Poly_Node(n, 1).Split(New Char() {","c})
            For Each j As String In anc_node
                If j <> "" Then
                    If j.Contains(":") Then
                        j = j.Split(New Char() {":"c})(0)
                    End If
                    TaxonTime(CInt(j) - 1, 1) = (Val(TaxonTime(CInt(j) - 1, 0)) + Val(Poly_Node(n, 8))).ToString
                End If
            Next
        End If
    End Sub
    Public Function range_to_num(ByVal range_str As String) As String
        Dim num_str As String = "("
        For Each i As Char In range_str.ToUpper
            num_str += (Asc(i) - Asc("A")).ToString + ","
        Next
        num_str += ")"
        Return num_str
    End Function
    Private Sub DataGridView1_CellContentClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub
    Private Sub Button5_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button5.Click
        time_period_num += 1
        Button14.BackColor = Color.IndianRed
        Label6.Text = "Click to apply rate matrix ->"
        Dim Tempchar() As Char = RangeStr.ToUpper
        NumericUpDown2.Maximum = RangeStr.Length
        Array.Sort(Tempchar)

        DataGridView2.AllowUserToAddRows = True
        DataGridView2.AllowUserToDeleteRows = True
        DataGridView2.AllowUserToOrderColumns = True
        DataGridView2.AllowUserToResizeColumns = True
        DataGridView2.AllowUserToResizeRows = True

        Dim r As Integer = 0
        DataGridView2.Rows.Insert(0, RangeStr.Length)
        For j As Integer = 1 To time_period_num
            For Each i As Char In Tempchar
                DataGridView2.Rows(r).HeaderCell.Value = (j - 1).ToString + "-" + (j).ToString + " " + i.ToString
                r += 1
            Next
        Next
        For i As Integer = 0 To Tempchar.Length - 1
            For j As Integer = 0 To Tempchar.Length - 1
                DataGridView2.Rows(i).Cells(j).Value = "1"
                If (time_period_num Mod 2) = 0 Then
                    DataGridView2.Rows(i).Cells(j).Style.BackColor = Color.LightYellow
                End If
            Next
        Next
        DataGridView2.BackgroundColor = Color.LightGray
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToDeleteRows = False
        DataGridView2.AllowUserToOrderColumns = False
        DataGridView2.AllowUserToResizeColumns = False
        DataGridView2.AllowUserToResizeRows = False
        For i As Integer = 1 To DataGridView2.ColumnCount
            DataGridView2.Columns(i - 1).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        DataGridView3.AllowUserToAddRows = True
        DataGridView3.AllowUserToDeleteRows = True
        DataGridView3.AllowUserToOrderColumns = True
        DataGridView3.AllowUserToResizeColumns = True
        DataGridView3.AllowUserToResizeRows = True
        DataGridView3.Rows.Insert(1, 1)
        For i As Integer = 1 To time_period_num
            DataGridView3.Rows(i).HeaderCell.Value = (i).ToString
        Next
        DataGridView3.Rows(1).Cells(0).Value = (Val(DataGridView3.Rows(2).Cells(0).Value) / 2).ToString("F1")

        DataGridView3.BackgroundColor = Color.LightGray
        DataGridView3.AllowUserToAddRows = False
        DataGridView3.AllowUserToDeleteRows = False
        DataGridView3.AllowUserToOrderColumns = False
        DataGridView3.AllowUserToResizeColumns = False
        DataGridView3.AllowUserToResizeRows = False
    End Sub
    Private Sub Button6_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button6.Click
        If time_period_num > 1 Then
            Button14.BackColor = Color.IndianRed
            Label6.Text = "Click to apply rate matrix ->"
            Dim Tempchar() As Char = RangeStr.ToUpper
            NumericUpDown2.Maximum = RangeStr.Length
            Array.Sort(Tempchar)
            DataGridView2.AllowUserToAddRows = True
            DataGridView2.AllowUserToDeleteRows = True
            DataGridView2.AllowUserToOrderColumns = True
            DataGridView2.AllowUserToResizeColumns = True
            DataGridView2.AllowUserToResizeRows = True
            For i As Integer = 0 To Tempchar.Length - 1
                DataGridView2.Rows.RemoveAt(Tempchar.Length - 1 - i)
            Next

            DataGridView2.BackgroundColor = Color.LightGray
            DataGridView2.AllowUserToAddRows = False
            DataGridView2.AllowUserToDeleteRows = False
            DataGridView2.AllowUserToOrderColumns = False
            DataGridView2.AllowUserToResizeColumns = False
            DataGridView2.AllowUserToResizeRows = False

            DataGridView3.AllowUserToAddRows = True
            DataGridView3.AllowUserToDeleteRows = True
            DataGridView3.AllowUserToOrderColumns = True
            DataGridView3.AllowUserToResizeColumns = True
            DataGridView3.AllowUserToResizeRows = True
            DataGridView3.Rows.RemoveAt(1)
            DataGridView3.AllowUserToAddRows = False
            DataGridView3.AllowUserToDeleteRows = False
            DataGridView3.AllowUserToOrderColumns = False
            DataGridView3.AllowUserToResizeColumns = False
            DataGridView3.AllowUserToResizeRows = False
            time_period_num -= 1
            Dim r As Integer = 0
            For j As Integer = 1 To time_period_num
                For Each i As Char In Tempchar

                    DataGridView2.Rows(r).HeaderCell.Value = (j - 1).ToString + "-" + (j).ToString + " " + i.ToString
                    r += 1
                Next
            Next

            DataGridView2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
            For i As Integer = 1 To time_period_num
                DataGridView3.Rows(i).HeaderCell.Value = (i).ToString
            Next
        End If
    End Sub
    Private Sub NumericUpDown2_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles NumericUpDown2.ValueChanged
        If DataGridView1.Rows.Count > 1 Then
            RefreshTheRangeListToolStripMenuItem_Click(sender, e)
        End If
    End Sub

    Private Sub Config_BGB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TargetOS = "macos" Then
            Me.TopMost = False
            NumericUpDown1.Value = 1
            'NumericUpDown1.Enabled = False
            CheckBox1.Visible = False
        End If
        'CheckBox1.Checked = True Xor isDebug

        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
        Me.TabControl1.TabPages.RemoveAt(2)
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
    End Sub
    Dim mrca As String
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If ListBox3.SelectedIndex >= 0 And ListBox4.SelectedIndex >= 0 Then
            If ListBox3.SelectedIndex <> ListBox4.SelectedIndex Then
                Dim dis_str As String = ""
                For i As Integer = 0 To DataGridView4.Rows.Count - 1
                    If DataGridView4.Rows(i).Cells(0).Value = True Then
                        dis_str += DataGridView4.Rows(i).HeaderCell.Value
                    End If
                Next
                If dis_str <> "" Then
                    Dim temp As Boolean = True
                    For i As Integer = 0 To DataGridView5.Rows.Count - 1
                        If (DataGridView5.Rows(i).Cells(0).Value.ToString = ListBox3.Items(ListBox3.SelectedIndex) Or
                            DataGridView5.Rows(i).Cells(0).Value.ToString = ListBox4.Items(ListBox4.SelectedIndex)) And
                        (DataGridView5.Rows(i).Cells(1).Value.ToString = ListBox3.Items(ListBox3.SelectedIndex) Or
                            DataGridView5.Rows(i).Cells(1).Value.ToString = ListBox4.Items(ListBox4.SelectedIndex)) Then
                            temp = False
                        End If
                    Next
                    If temp Then
                        DataGridView5.Rows.Add()
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(0).Value = ListBox3.Items(ListBox3.SelectedIndex)
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(1).Value = ListBox4.Items(ListBox4.SelectedIndex)
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(2).Value = dis_str
                    Else
                        MsgBox("Repeated taxon group!")
                    End If
                Else
                    MsgBox("Please select the one area at least!")
                End If
            Else
                MsgBox("Please select the different taxon!")
            End If
        Else
            MsgBox("Please select the taxon!")
        End If
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Dim temp As Integer = DataGridView5.Rows.Count - 1
        For i As Integer = 0 To temp
            If DataGridView5.Rows(temp - i).Cells(3).Value = True Then
                DataGridView5.Rows.RemoveAt(temp - i)
            End If
        Next
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            write_time_period(opendialog.FileName)
        End If
    End Sub
    Public Sub write_time_period(ByVal file_path As String)
        area_dispersal = ""
        For k As Integer = 1 To time_period_num
            For i As Integer = (k - 1) * RangeStr.Length To k * RangeStr.Length - 1
                For j As Integer = 0 To RangeStr.Length - 1
                    area_dispersal += DataGridView2.Rows(i).Cells(j).Value.ToString + " "
                Next
                area_dispersal = area_dispersal.Remove(area_dispersal.Length - 1)
                area_dispersal += vbCrLf
            Next
            area_dispersal += vbCrLf + vbCrLf
        Next
        dispersal_durations = ""
        For i As Integer = 1 To time_period_num
            dispersal_durations += DataGridView3.Rows(i).Cells(0).Value + " "
        Next
        Dim wr3 As New StreamWriter(file_path, False, System.Text.Encoding.Default)
        wr3.Write(area_dispersal)
        wr3.Write("#periods=" + dispersal_durations)
        wr3.Close()
    End Sub
    Public Sub read_time_period(ByVal file_path As String)
        Dim sr As New StreamReader(file_path, System.Text.Encoding.Default)
        Dim temp_number As Integer = 1
        Dim line As String = ""
        line = sr.ReadLine()

        If line.Split(" ").Length = RangeStr.Length Then
            Dim peroid As String = ""
            Do
                line = sr.ReadLine()
                If line <> "" Then
                    temp_number += 1
                    If line.StartsWith("#") Then
                        temp_number -= 1
                        peroid = line.Split("=")(1)
                        Exit Do
                    End If
                End If
            Loop Until line Is Nothing
            sr.Close()
            time_period_num = temp_number / (RangeStr.Length)
            If time_period_num <> temp_number / (RangeStr.Length) Then
                MsgBox("Wrong number of periods!")
                Exit Sub
            End If
            Dim sr1 As New StreamReader(file_path, System.Text.Encoding.Default)
            Dim Tempchar() As Char = RangeStr.ToUpper
            Array.Sort(Tempchar)
            Dim r As Integer
            DataGridView3.AllowUserToAddRows = True
            DataGridView3.AllowUserToDeleteRows = True
            DataGridView3.AllowUserToOrderColumns = True
            DataGridView3.AllowUserToResizeColumns = True
            DataGridView3.AllowUserToResizeRows = True

            DataGridView2.AllowUserToAddRows = True
            DataGridView2.AllowUserToDeleteRows = True
            DataGridView2.AllowUserToOrderColumns = True
            DataGridView2.AllowUserToResizeColumns = True
            DataGridView2.AllowUserToResizeRows = True

            DataGridView2.Columns.Clear()
            DataGridView2.Rows.Clear()
            DataGridView2.Rows.Clear()
            DataGridView2.AllowUserToAddRows = True
            DataGridView2.AllowUserToDeleteRows = True
            DataGridView2.AllowUserToOrderColumns = True
            DataGridView2.AllowUserToResizeColumns = True
            DataGridView2.AllowUserToResizeRows = True
            r = 0
            For Each i As Char In Tempchar
                Dim Column2 As New DataGridViewTextBoxColumn
                Column2.HeaderText = i.ToString

                Column2.Width = 32
                Column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter

                DataGridView2.Columns.Add(Column2)
            Next
            DataGridView2.BackgroundColor = Color.LightGray

            DataGridView3.Columns.Clear()
            DataGridView3.Rows.Clear()
            Dim Column3 As New DataGridViewTextBoxColumn
            Column3.HeaderText = "Time"
            Column3.Width = 80
            Column3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView3.Columns.Add(Column3)
            DataGridView3.Rows.Add()
            DataGridView3.Rows(0).Cells(0).Value = "0.0"
            DataGridView3.Rows(0).Cells(0).ReadOnly = True
            DataGridView3.Rows(0).Cells(0).Style.SelectionBackColor = Color.Gray
            DataGridView3.Rows(0).Cells(0).Style.BackColor = Color.Gray
            DataGridView3.BackgroundColor = Color.LightGray


            For t As Integer = 1 To time_period_num
                r = (t - 1) * Tempchar.Length
                For Each i As Char In Tempchar
                    DataGridView2.Rows.Add()
                    DataGridView2.Rows(r).HeaderCell.Value = (t - 1).ToString + "-" + (t).ToString + " " + i.ToString
                    r = r + 1
                Next
                For i As Integer = (t - 1) * Tempchar.Length To t * Tempchar.Length - 1
                    Dim temp_list() As String = sr1.ReadLine().Split(" ")
                    For j As Integer = 0 To Tempchar.Length - 1
                        DataGridView2.Rows(i).Cells(j).Value = temp_list(j)
                    Next
                Next
                Do While (sr1.Peek() = "13" Or sr1.Peek() = "10")
                    sr1.ReadLine()
                Loop
                If peroid <> "" Then
                    DataGridView3.Rows.Add()
                    DataGridView3.Rows(t - 1).HeaderCell.Value = (t - 1).ToString
                    DataGridView3.Rows(t).HeaderCell.Value = (t).ToString
                    DataGridView3.Rows(t).Cells(0).Value = peroid.Split(" ")(t - 1)
                    DataGridView3.BackgroundColor = Color.LightGray
                Else
                    DataGridView3.Rows.Add()
                    DataGridView3.Rows(t - 1).HeaderCell.Value = (t - 1).ToString
                    DataGridView3.Rows(t).HeaderCell.Value = (t).ToString
                    DataGridView3.Rows(t).Cells(0).Value = "0.0"
                    DataGridView3.BackgroundColor = Color.LightGray
                End If
            Next
            DataGridView2.BackgroundColor = Color.LightGray
            DataGridView2.AllowUserToAddRows = False
            DataGridView2.AllowUserToDeleteRows = False
            DataGridView2.AllowUserToOrderColumns = False
            DataGridView2.AllowUserToResizeColumns = False
            DataGridView2.AllowUserToResizeRows = False
            DataGridView3.AllowUserToAddRows = False
            DataGridView3.AllowUserToDeleteRows = False
            DataGridView3.AllowUserToOrderColumns = False
            DataGridView3.AllowUserToResizeColumns = False
            DataGridView3.AllowUserToResizeRows = False
            sr1.Close()
        Else
            sr.Close()
            MsgBox("Areas are not fit!")
            Exit Sub
        End If
    End Sub
    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                read_time_period(opendialog.FileName)
                Label6.Text = "Click to apply rate matrix ->"
            End If
        End If
    End Sub

    Private Sub Button11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button11.Click
        Button11.BackColor = Color.Transparent
        DeleteFiles(root_path, ".rm")
        DeleteFiles(root_path, ".tp")
        time_period_num = 1
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        ReDim TaxonName(taxon_num - 1)
        ReDim Distribution(taxon_num - 1)
        For i As Integer = 0 To taxon_num - 1
            TaxonName(i) = dtView.Item(i).Item(1).ToString
            Distribution(i) = dtView.Item(i).Item(state_index).ToString
            ListBox3.Items.Add(TaxonName(i))
            ListBox4.Items.Add(TaxonName(i))
        Next
        Dim Tempchar() As Char = RangeStr.ToUpper
        Array.Sort(Tempchar)
        NumericUpDown2.Maximum = RangeStr.Length

        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.AllowUserToDeleteRows = True
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.AllowUserToResizeColumns = True
        DataGridView1.AllowUserToResizeRows = True
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
        RefreshTheRangeListToolStripMenuItem_Click(sender, e)

        DataGridView2.Columns.Clear()
        DataGridView2.Rows.Clear()
        DataGridView2.AllowUserToAddRows = True
        DataGridView2.AllowUserToDeleteRows = True
        DataGridView2.AllowUserToOrderColumns = True
        DataGridView2.AllowUserToResizeColumns = True
        DataGridView2.AllowUserToResizeRows = True
        r = 0
        For Each i As Char In Tempchar
            Dim Column2 As New DataGridViewTextBoxColumn
            Column2.HeaderText = i.ToString
            Column2.Width = 32
            Column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
            DataGridView2.Columns.Add(Column2)
            DataGridView2.Rows.Add()
            DataGridView2.Rows(r).HeaderCell.Value = "0-1 " + i.ToString
            r = r + 1
        Next
        DataGridView2.RowHeadersWidth = 70
        For i As Integer = 0 To Tempchar.Length - 1
            For j As Integer = 0 To Tempchar.Length - 1
                DataGridView2.Rows(i).Cells(j).Value = "1"
            Next
        Next
        DataGridView2.BackgroundColor = Color.LightGray
        DataGridView2.AllowUserToAddRows = False
        DataGridView2.AllowUserToDeleteRows = False
        DataGridView2.AllowUserToOrderColumns = False
        DataGridView2.AllowUserToResizeColumns = False
        DataGridView2.AllowUserToResizeRows = False
        RefreshTheRangeListToolStripMenuItem_Click(sender, e)

        DataGridView3.Columns.Clear()
        DataGridView3.Rows.Clear()
        Dim Column3 As New DataGridViewTextBoxColumn
        Column3.HeaderText = "Time"
        Column3.Width = 80
        Column3.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView3.Columns.Add(Column3)
        DataGridView3.Rows.Add()
        DataGridView3.Rows(0).HeaderCell.Value = "0"
        DataGridView3.Rows(0).Cells(0).Value = "0.0"
        DataGridView3.Rows(0).Cells(0).ReadOnly = True
        DataGridView3.Rows(0).Cells(0).Style.SelectionBackColor = Color.Gray
        DataGridView3.Rows(0).Cells(0).Style.BackColor = Color.Gray
        DataGridView3.BackgroundColor = Color.LightGray
        DataGridView3.AllowUserToAddRows = False
        DataGridView3.AllowUserToDeleteRows = False
        DataGridView3.AllowUserToOrderColumns = False
        DataGridView3.AllowUserToResizeColumns = False
        DataGridView3.AllowUserToResizeRows = False

        DataGridView4.Columns.Clear()
        DataGridView4.Rows.Clear()
        Dim Column4 As New DataGridViewCheckBoxColumn
        Column4.HeaderText = ""
        Column4.Width = 30
        Column4.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView4.Columns.Add(Column4)
        For i As Integer = 0 To Tempchar.Length - 1
            DataGridView4.Rows.Add()
            DataGridView4.Rows(i).HeaderCell.Value = Tempchar(i).ToString
        Next
        DataGridView4.AllowUserToAddRows = False
        DataGridView4.AllowUserToDeleteRows = False
        DataGridView4.AllowUserToOrderColumns = False
        DataGridView4.AllowUserToResizeColumns = False
        DataGridView4.AllowUserToResizeRows = False

        DataGridView5.Columns.Clear()
        DataGridView5.Rows.Clear()
        Dim Column_Taxon1 As New DataGridViewTextBoxColumn
        Dim Column_Taxon2 As New DataGridViewTextBoxColumn
        Dim Column_Dis As New DataGridViewTextBoxColumn
        Column_Taxon1.HeaderText = "Taxon1"
        Column_Taxon2.HeaderText = "Taxon2"
        Column_Dis.HeaderText = "Range"
        DataGridView5.Columns.Add(Column_Taxon1)
        DataGridView5.Columns.Add(Column_Taxon2)
        DataGridView5.Columns.Add(Column_Dis)

        DataGridView5.Columns(0).ReadOnly = True
        DataGridView5.Columns(1).ReadOnly = True
        DataGridView5.Columns(2).ReadOnly = True

        Dim Column_Check As New DataGridViewCheckBoxColumn
        Column_Check.HeaderText = ""
        DataGridView5.Columns.Add(Column_Check)
        DataGridView5.Columns(0).Width = 85
        DataGridView5.Columns(1).Width = 85
        DataGridView5.Columns(2).Width = 85
        DataGridView5.Columns(3).Width = 40
        DataGridView5.AllowUserToAddRows = False
        DataGridView5.AllowUserToDeleteRows = False
        DataGridView5.AllowUserToOrderColumns = False
        DataGridView5.AllowUserToResizeColumns = False
        DataGridView5.AllowUserToResizeRows = False
        Read_Poly_Tree(tree_show_with_value)
    End Sub

    Private Sub Button13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button13.Click
        RangeStr = sort_str(RangeStr)
        muti_threads_BGB = NumericUpDown1.Value
        If File.Exists(root_path + "temp\BGB_err.txt") Then
            File.Delete(root_path + "temp\BGB_err.txt")
        End If
        If File.Exists(root_path + "temp\BGB_result.txt") Then
            File.Delete(root_path + "temp\BGB_result.txt")
        End If
        If File.Exists(root_path + "temp\restable_AICc_rellike_formatted.txt") Then
            File.Delete(root_path + "temp\restable_AICc_rellike_formatted.txt")
        End If
        'If File.Exists(root_path + "temp\teststable.txt") Then
        '    File.Delete(root_path + "temp\teststable.txt")
        'End If
        DeleteFiles(root_path + "temp", ".BGB.txt")
        DeleteFiles(root_path + "temp", ".r")
        DeleteFiles(root_path + "temp", ".end")
        DeleteFiles(root_path + "temp", ".tab")

        If File.Exists(root_path + "temp\err.log") Then
            File.Delete(root_path + "temp\err.log")
        End If
        If ListBox1.Items.Count * taxon_num > 4096 Then
            Dim dr As DialogResult = MsgBox("There are too many ranges, you may need hours or days to run the analysis. Continue?", MsgBoxStyle.YesNo)
            If dr = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If



        area_labels = ""
        Dim temp_areas() As Char = RangeStr.ToUpper
        Array.Sort(temp_areas)
        For Each i As Char In temp_areas
            area_labels += i.ToString + " "
        Next

        excluded_ranges = ""
        Dim temp_range(0) As String
        temp_range(0) = ""
        For i As Integer = 0 To ListBox2.Items.Count - 1
            ReDim Preserve temp_range(temp_range.Length)
            temp_range(UBound(temp_range)) = ListBox2.Items(i)
        Next

        For i As Integer = 0 To UBound(temp_range)
            If temp_range(i) <> "" Then
                excluded_ranges += """" + temp_range(i).ToUpper + """" + ","
            End If
        Next
        If excluded_ranges <> "" Then
            excluded_ranges = excluded_ranges.Remove(excluded_ranges.Length - 1)
        End If
        excluded_ranges = "c(" + excluded_ranges + ")"

        included_ranges = """" + "_" + """"
        ReDim temp_range(0)
        temp_range(0) = ""
        For i As Integer = 0 To ListBox1.Items.Count - 1
            ReDim Preserve temp_range(temp_range.Length)
            temp_range(UBound(temp_range)) = ListBox1.Items(i)
        Next
        For i As Integer = 1 To RangeStr.Length
            ReDim Preserve temp_range(temp_range.Length)
            temp_range(UBound(temp_range)) = RangeStr.ToCharArray()(i - 1).ToString
        Next
        For i As Integer = 0 To UBound(Distribution)
            If Array.IndexOf(temp_range, Distribution(i)) < 0 Then
                ReDim Preserve temp_range(temp_range.Length)
                temp_range(UBound(temp_range)) = Distribution(i)
            End If
        Next

        For i As Integer = 0 To UBound(temp_range)
            If temp_range(i) <> "" Then
                included_ranges += "," + """" + temp_range(i).ToUpper + """"
            End If
        Next
        included_ranges = "c(" + included_ranges + ")"


        dispersal_durations = ""
        Dim check_num As Integer = 0
        For i As Integer = 1 To time_period_num
            dispersal_durations += DataGridView3.Rows(i).Cells(0).Value + vbCrLf
            If CSng(DataGridView3.Rows(i).Cells(0).Value) >= root_time Then
                check_num += 1
            End If
        Next
        If check_num > 1 Then
            MsgBox("The timeperiods has to have just only one oldest time that is older than the root age of the tree")
            Exit Sub
        End If
        taxon_range_data = taxon_num.ToString + "	" + RangeStr.Length.ToString + " (" + area_labels.Remove(area_labels.Length - 1) + ")" + vbCrLf
        For i As Integer = 0 To taxon_num - 1
            taxon_range_data += TaxonName(i) + "	" + Distributiton_to_Binary(Distribution(i), RangeStr.Length) + vbCrLf
        Next

        mrca = ""
        For i As Integer = 0 To DataGridView5.Rows.Count - 1
            mrca += "mrca = " + "ag" + i.ToString + " " + DataGridView5.Rows(i).Cells(0).Value + " " + DataGridView5.Rows(i).Cells(1).Value + vbCrLf
            mrca += "fixnode = " + "ag" + i.ToString + " " + Distributiton_to_Binary(DataGridView5.Rows(i).Cells(2).Value, RangeStr.Length) + vbCrLf
        Next
        Dim wr1 As New StreamWriter(root_path + "temp\final.tre", False, System.Text.Encoding.Default)
        wr1.Write(lag_tree)
        wr1.Close()
        Dim wr2 As New StreamWriter(root_path + "temp\final.data", False, System.Text.Encoding.Default)
        wr2.Write(taxon_range_data)
        wr2.Close()

        Dim wr4 As New StreamWriter(root_path + "temp\final.timeperiod", False, System.Text.Encoding.Default)
        wr4.Write(dispersal_durations)
        wr4.Close()
        If BGB_mode = 0 Then
            If CheckBox2.Checked = False Then
                BGB_mode = 3
            End If
        End If
        Select Case BGB_mode
            Case 1
                Dim sr1 As New StreamReader(root_path + "Plug-ins\BGB\" + ComboBox1.Text + ".r")
                BGB_Body = sr1.ReadToEnd
                sr1.Close()
                BGB_Body = BGB_Body.Replace("#excluded_ranges#", excluded_ranges)
                BGB_Body = BGB_Body.Replace("#included_ranges#", included_ranges)
                BGB_Body = BGB_Body.Replace("#max_range_size#", NumericUpDown2.Value)
                '每个线程单核心以提高稳定性
                BGB_Body = BGB_Body.Replace("#cores_to_use#", 1)
				BGB_Body = BGB_Body.Replace("#timeperiod#", lib_path + "temp/final.timeperiod")
                If ComboBox3.SelectedIndex = 0 Then
                    BGB_Body = BGB_Body.Replace("#optimx#", "TRUE")
                Else
                    BGB_Body = BGB_Body.Replace("#optimx#", "'GenSA'")
                End If
                For i As Integer = 0 To 3
                    If File.Exists(root_path + "temp\" + i.ToString + ".tp") Then
						BGB_Body = BGB_Body.Replace("#ratematrix" + i.ToString + "#", lib_path + "temp/" + i.ToString + ".rm")
						Select Case i
                            Case 0
                                BGB_Body = BGB_Body.Replace("#dispersal_multipliers_fn#", "dispersal_multipliers_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 1
                                BGB_Body = BGB_Body.Replace("#areas_allowed_fn#", "areas_allowed_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 2
                                BGB_Body = BGB_Body.Replace("#areas_adjacency_fn#", "areas_adjacency_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 3
                                BGB_Body = BGB_Body.Replace("#distsfn#", "distsfn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                        End Select
                    End If
                Next

                If time_period_num > 1 Then
                    BGB_Body = BGB_Body.Replace("#time_period#", "")
                End If
                Dim wr As New StreamWriter(root_path + "temp\BGB_model.r", False)
                wr.Write(BGB_Body)
                wr.Close()
            Case 0, 2, 3
                Dim sr0 As New StreamReader(root_path + "Plug-ins\BGB\header.r")
				BGB_Header = sr0.ReadToEnd
				If rscript = root_path + "Plug-ins\R\bin\i386\Rscript.exe" Then
					BGB_Header = BGB_Header.Replace("#r_lib#", "")
				End If
				sr0.Close()
                BGB_Header += "source(" + """" + "BGB.r" + """" + ")" + vbCrLf
                BGB_Header += "})"

                BGB_Body = "tryCatch({" + vbCrLf

                If BGB_mode = 0 Then
                    Dim sr1 As New StreamReader(root_path + "Plug-ins\BGB\model_test+j.r")
                    BGB_Body += sr1.ReadToEnd
                    sr1.Close()
                End If
                If BGB_mode = 3 Then
                    Dim sr1 As New StreamReader(root_path + "Plug-ins\BGB\model_test.r")
                    BGB_Body += sr1.ReadToEnd
                    sr1.Close()
                End If
                If BGB_mode = 2 Then
                    Dim sr1 As New StreamReader(root_path + "Plug-ins\BGB\" + ComboBox1.Text + ".r")
                    BGB_Body += sr1.ReadToEnd
                    BGB_Body = BGB_Body.Replace("#model_tab#", "tab" + ComboBox1.Text + ".txt")
                    sr1.Close()
                End If

                Dim sr2 As New StreamReader(root_path + "Plug-ins\BGB\footer.r")
                BGB_Body += sr2.ReadToEnd
                sr2.Close()

                BGB_Header = BGB_Header.Replace("#lib_path#", lib_path)

                BGB_Body = BGB_Body.Replace("#lib_path#", lib_path)
                BGB_Body = BGB_Body.Replace("#max_range_size#", NumericUpDown2.Value)
                BGB_Body = BGB_Body.Replace("#cores_to_use#", NumericUpDown1.Value)
                BGB_Body = BGB_Body.Replace("#excluded_ranges#", excluded_ranges)
                BGB_Body = BGB_Body.Replace("#included_ranges#", included_ranges)
                BGB_Body = BGB_Body.Replace("#timeperiod#", lib_path + "temp/final.timeperiod")

                If ComboBox3.SelectedIndex = 0 Then
                    BGB_Body = BGB_Body.Replace("#optimx#", "TRUE")
                Else
                    BGB_Body = BGB_Body.Replace("#optimx#", "'GenSA'")
                End If

                For i As Integer = 0 To 3
                    If File.Exists(root_path + "temp\" + i.ToString + ".tp") Then
						BGB_Body = BGB_Body.Replace("#ratematrix" + i.ToString + "#", lib_path + "temp/" + i.ToString + ".rm")
						Select Case i
                            Case 0
                                BGB_Body = BGB_Body.Replace("#dispersal_multipliers_fn#", "dispersal_multipliers_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 1
                                BGB_Body = BGB_Body.Replace("#areas_allowed_fn#", "areas_allowed_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 2
                                BGB_Body = BGB_Body.Replace("#areas_adjacency_fn#", "areas_adjacency_fn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                            Case 3
                                BGB_Body = BGB_Body.Replace("#distsfn#", "distsfn")
                                BGB_Body = BGB_Body.Replace("#" + i.ToString + "#", "")
                        End Select
                    End If
                Next

				BGB_Body = BGB_Body.Replace("#treefile#", lib_path + "temp/final.tre")
				BGB_Body = BGB_Body.Replace("#datafile#", lib_path + "temp/final.data")
				If time_period_num > 1 Then
                    BGB_Body = BGB_Body.Replace("#time_period#", "")
                End If
                Dim sw_header As New StreamWriter(root_path + "temp\LOAD_BGB.r", False)
                sw_header.Write(BGB_Header)
                sw_header.Close()

                Dim sw_body As New StreamWriter(root_path + "temp\BGB.r", False)
                sw_body.Write(BGB_Body)
                sw_body.Close()

                If File.Exists(root_path + "temp\BGB_result.txt") Then
                    File.Delete(root_path + "temp\BGB_result.txt")
                End If

                Dim wr5 As New StreamWriter(root_path + "temp\LOAD_BGB.bat", False, System.Text.Encoding.Default)
                wr5.WriteLine("""" + rscript + """" + " LOAD_BGB.r>BGB.log")
                wr5.WriteLine("echo end>BGB.end")
                wr5.Close()

        End Select
        If ComboBox1.Visible Then
            
        Else

        End If

        If File.Exists(root_path + "temp\BGB.log") Then
            File.Delete(root_path + "temp\BGB.log")
        End If
        If File.Exists(root_path + "temp\BGB.end") Then
            File.Delete(root_path + "temp\BGB.end")
        End If

        StartTreeView = False
        BGB_con_made = True

        Me.Hide()
    End Sub

    Private Sub Button12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button12.Click
        Me.Hide()
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        RangeStr = sort_str(RangeStr)
        Dim Tempchar() As Char = RangeStr.ToUpper
        If File.Exists(root_path + "temp/" + ComboBox2.SelectedIndex.ToString + ".tp") Then
            read_time_period(root_path + "temp/" + ComboBox2.SelectedIndex.ToString + ".tp")
            For i As Integer = 0 To Tempchar.Length - 1
                For j As Integer = 0 To Tempchar.Length - 1
                    If (time_period_num Mod 2) = 0 Then
                        DataGridView2.Rows(i).Cells(j).Style.BackColor = Color.LightYellow
                    End If
                Next
            Next
        Else
            If DataGridView2.Rows.Count > 0 Then
                For i As Integer = 0 To Tempchar.Length - 1
                    For j As Integer = 0 To Tempchar.Length - 1
                        DataGridView2.Rows(i).Cells(j).Value = "1"
                        If (time_period_num Mod 2) = 0 Then
                            DataGridView2.Rows(i).Cells(j).Style.BackColor = Color.LightYellow
                        End If
                    Next
                Next
            End If

        End If
    End Sub

    Private Sub Button14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button14.Click
        RangeStr = sort_str(RangeStr)
        Button14.BackColor = Color.Transparent
        Label6.Text = ""
        write_time_period(root_path + "temp/" + ComboBox2.SelectedIndex.ToString + ".tp")
        area_dispersal = ""
        For k As Integer = 1 To time_period_num
            For Each i As Char In RangeStr
                area_dispersal += i.ToString + Chr(9)
            Next
            area_dispersal.Remove(area_dispersal.Length - 1)
            area_dispersal += vbCrLf
            For i As Integer = (k - 1) * RangeStr.Length To k * RangeStr.Length - 1
                For j As Integer = 0 To RangeStr.Length - 1
                    area_dispersal += DataGridView2.Rows(i).Cells(j).Value.ToString + Chr(9)
                Next
                area_dispersal = area_dispersal.Remove(area_dispersal.Length - 1)
                area_dispersal += vbCrLf
            Next
            area_dispersal += vbCrLf
        Next
        area_dispersal += "END" + vbCrLf
        Dim wr3 As New StreamWriter(root_path + "temp\" + ComboBox2.SelectedIndex.ToString + ".rm", False, System.Text.Encoding.Default)
        wr3.Write(area_dispersal)
        wr3.Close()
    End Sub

    Private Sub DataGridView3_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellEndEdit
        Button14.BackColor = Color.IndianRed
        Label6.Text = "Click to apply rate matrix ->"
    End Sub

    Private Sub DataGridView2_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Button14.BackColor = Color.IndianRed
        Label6.Text = "Click to apply rate matrix ->"
    End Sub

    Private Sub RefreshTheRangeListToolStripMenuItem_Click_1(sender As Object, e As EventArgs) Handles RefreshTheRangeListToolStripMenuItem.Click
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        DataGridView1.EndEdit()
        rang_num = DataGridView1.Rows.Count
        Dim Tempchar() As Char = RangeStr.ToUpper
        area_num = CInt(NumericUpDown2.Value)
        Array.Sort(Tempchar)
        For j As Integer = 2 To area_num
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
                isend = pailie(n, j, j)
            Loop Until isend = False
        Next
    End Sub

    Private Sub SaveSettingsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SaveSettingsToolStripMenuItem.Click
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Dim sw As New StreamWriter(opendialog.FileName)
            sw.WriteLine("[Range list]")
            Dim list_num As Integer = DataGridView1.Rows.Count
            For i As Integer = 0 To list_num - 1
                For j As Integer = 0 To list_num - 1
                    If DataGridView1.Rows(i).Cells(j).Value Then
                        sw.Write("1,")
                    Else
                        sw.Write("0,")
                    End If
                Next
            Next
            sw.Write(vbCrLf)
            sw.WriteLine("[Optimize]")
            If CheckBox2.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            sw.Write(NumericUpDown2.Value.ToString + ",")

            sw.Write(vbCrLf)
            sw.WriteLine("[Fossils]")
            For i As Integer = 0 To DataGridView2.Rows.Count - 1
                sw.Write(DataGridView2.Rows(i).Cells(2).Value.ToString + ",")
            Next
            sw.Write(vbCrLf)
            sw.WriteLine("[Include]")
            For i As Integer = 0 To ListBox1.Items.Count - 1
                sw.Write(ListBox1.Items(i).ToString + ",")
            Next
            sw.Write(vbCrLf)
            sw.WriteLine("[Exclude]")
            For i As Integer = 0 To ListBox2.Items.Count - 1
                sw.Write(ListBox2.Items(i).ToString + ",")
            Next
            sw.Close()
        End If
    End Sub

    Private Sub LoadSettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadSettingToolStripMenuItem.Click
        Try
            Dim opendialog As New OpenFileDialog
            opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".txt"
            opendialog.CheckFileExists = True
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                Dim sr As New StreamReader(opendialog.FileName)
                Dim line As String = sr.ReadLine
                Do While line <> ""
                    If line = "[Range list]" Then
                        Dim range_list() As String = sr.ReadLine.Split(",")
                        Dim list_num As Integer = DataGridView1.Rows.Count
                        For i As Integer = 0 To list_num - 1
                            For j As Integer = 0 To list_num - 1
                                If range_list(i * list_num + j) = "1" Then
                                    DataGridView1.Rows(i).Cells(j).Value = True
                                Else
                                    DataGridView1.Rows(i).Cells(j).Value = False
                                End If
                            Next
                        Next
                    End If
                    If line = "[Optimize]" Then
                        Dim range_list() As String = sr.ReadLine.Split(",")
                        If range_list(0) = 1 Then
                            CheckBox2.Checked = True
                        Else
                            CheckBox2.Checked = False
                        End If
                        NumericUpDown2.Value = CInt(range_list(1))

                    End If
                    If line = "[Fossils]" Then
                        Dim range_list() As String = sr.ReadLine.Split(",")
                        For i As Integer = 0 To DataGridView2.Rows.Count - 1
                            DataGridView2.Rows(i).Cells(2).Value = range_list(i)
                        Next
                    End If
                    If line = "[Include]" Then
                        ListBox1.Items.Clear()
                        line = sr.ReadLine
                        If line <> "" Then
                            Dim range_list() As String = line.Split(",")
                            For Each i As String In range_list
                                If i <> "" Then
                                    ListBox1.Items.Add(i)
                                End If
                            Next
                        End If
                    End If
                    If line = "[Exclude]" Then
                        ListBox2.Items.Clear()
                        line = sr.ReadLine
                        If line <> "" Then
                            Dim range_list() As String = line.Split(",")
                            For Each i As String In range_list
                                If i <> "" Then
                                    ListBox2.Items.Add(i)
                                End If
                            Next
                        End If
                    End If
                    line = sr.ReadLine
                Loop
                sr.Close()
            End If
        Catch ex As Exception
            MsgBox("Could not load setting!")
        End Try

    End Sub
End Class