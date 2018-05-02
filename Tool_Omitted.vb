Imports System.IO
Imports System.Threading
Public Class Tool_Omitted
    Dim tree_file As String = "((1,3):20,(2,4):40):60;"
    Dim node(,) As String
    Dim node_xy(,) As Single
    Dim node_lr(,) As Single
    Dim root_node As Integer = -1
    Dim clade_height As Integer = 20
    Dim clade_weight As Integer = 20
    Dim clade_broad As Integer = 250
    Dim root_x As Integer = 700
    Dim root_y As Integer = 100
    Dim max_x As Integer
    Dim max_y As Integer
    Dim Start_draw As Boolean = True
    Dim tree_char() As String
    Dim taxon_ID() As String
    Dim node_clade() As String
    Dim node_support() As String
    Dim node_select() As String
    Dim clicked_node As Integer = -1

    Private Const Pi As Single = 3.1415926
    Dim pie_step As Single = 0.01
    Dim begin_draw As Boolean = False
    Dim pie_set() As Integer
    Dim DIVA_node() As Integer
    Dim area() As String
    Dim distrubiton() As String
    Dim node_line As String
    Dim pie_radii As Integer = 10
    Dim pie_x As Integer = 200
    Dim pie_y As Integer = 200

    Dim omitted_dataset As New DataSet
    Dim odView As New DataView
    Private Sub Omitted_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            System.Threading.Thread.CurrentThread.CurrentCulture = ci
            If tree_show <> "" Then
                Dim Temp_taxon As Integer = 0
                ComboBox1.SelectedIndex = 0

                Dim taxon_table As New System.Data.DataTable
                taxon_table.TableName = "Taxon Table"
                Dim Column_ID As New System.Data.DataColumn("ID")
                Dim Column_Taxon As New System.Data.DataColumn("Name")
                Dim Column_Distrution As New System.Data.DataColumn("Clade")
                taxon_table.Columns.Add(Column_ID)
                taxon_table.Columns.Add(Column_Taxon)
                taxon_table.Columns.Add(Column_Distrution)
                omitted_dataset.Tables.Add(taxon_table)

                odView = omitted_dataset.Tables("Taxon Table").DefaultView

                odView.AllowNew = False
                odView.AllowDelete = False
                odView.AllowEdit = True
                DataGridView1.DataSource = odView
                DataGridView1.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView1.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView1.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView1.Columns(0).ReadOnly = True
                DataGridView1.Columns(2).ReadOnly = True
                odView.AllowNew = True
                odView.AddNew()
                odView.AllowNew = False
            Else
                Me.Close()
            End If
        Catch ex As Exception
            Me.Close()
        End Try


    End Sub
    Private Function percentage(ByVal rect As Integer, ByVal ParamArray rectall() As Integer) As Single
        Dim sum As Int64
        For i As Integer = 0 To UBound(rectall, 1)
            sum += rectall(i)
        Next
        percentage = rect / sum
    End Function



    Public Sub Fill_clade()
        Dim Temp_array() As String
        ReDim Temp_array(taxon_num * 2 - 1)
        ReDim taxon_ID(taxon_num)
        Dim Temp_num As Integer = 0

        For i As Integer = 1 To taxon_num * 4 - 3
            If tree_char(i) <> "," And tree_char(i) <> "(" And tree_char(i) <> ")" Then
                Temp_num = Temp_num + 1
                Temp_array(Temp_num) = "$" + tree_char(i) + "$"
                taxon_ID(Temp_num) = "$" + tree_char(i) + "$"
            End If
        Next
        Do
            For n As Integer = 1 To taxon_num - 1
                If Temp_array(taxon_num + n) = "" Then
                    Dim left_clade As String = node(n, 1)
                    Dim right_clade As String = node(n, 2)
                    If Array.IndexOf(Temp_array, left_clade) >= 0 And Array.IndexOf(Temp_array, right_clade) >= 0 Then
                        Dim l1 As Single, l2 As Single
                        Dim n1 As Single, n2 As Single
                        If left_clade.StartsWith("$") Then
                            l1 = Array.IndexOf(Temp_array, left_clade)
                            n1 = 1
                        Else
                            l1 = node_xy(left_clade, 1)
                            n1 = node_xy(left_clade, 0) + 1
                        End If
                        If right_clade.StartsWith("$") Then
                            l2 = Array.IndexOf(Temp_array, right_clade)
                            n2 = 1
                        Else
                            l2 = node_xy(right_clade, 1)
                            n2 = node_xy(right_clade, 0) + 1
                        End If
                        If n1 > n2 Then
                            node_xy(n, 0) = n1
                        Else
                            node_xy(n, 0) = n2
                        End If

                        node_xy(n, 1) = (l1 + l2) / 2
                        Temp_array(taxon_num + n) = n.ToString
                    End If
                End If
            Next
        Loop Until Temp_array(taxon_num + root_node) <> ""

    End Sub
    Public Sub exchange_clade(ByVal i As Integer)
        Dim Temp_node_left_arry() As String
        Dim Temp_node_right_arry() As String
        ReDim Temp_node_left_arry(taxon_num - 1)
        ReDim Temp_node_right_arry(taxon_num - 1)
        For j As Integer = 1 To taxon_num - 1
            Temp_node_left_arry(j) = node(j, 1)
            Temp_node_right_arry(j) = node(j, 2)
        Next

        Dim left_length As Integer = 0
        Dim right_length As Integer = 0
        Dim left_length_p As Integer = 0
        Dim right_length_p As Integer = 0

        If node(i, 1).StartsWith("$") = False Then
            left_length = node(node(i, 1), 3)
        Else
            left_length = 1
        End If
        If node(i, 2).StartsWith("$") = False Then
            right_length = node(node(i, 2), 3)
        Else
            right_length = 1
        End If

        If Array.IndexOf(Temp_node_left_arry, i.ToString) >= 0 Then
            If node(Array.IndexOf(Temp_node_left_arry, i.ToString), 1).StartsWith("$") = False Then
                left_length_p = node(node(Array.IndexOf(Temp_node_left_arry, i.ToString), 1), 3)
            Else
                left_length_p = 1
            End If
            If node(Array.IndexOf(Temp_node_left_arry, i.ToString), 2).StartsWith("$") = False Then
                right_length_p = node(node(Array.IndexOf(Temp_node_left_arry, i.ToString), 2), 3)
            Else
                right_length_p = 1
            End If
        End If
        If Array.IndexOf(Temp_node_right_arry, i.ToString) >= 0 Then
            If node(Array.IndexOf(Temp_node_right_arry, i.ToString), 1).StartsWith("$") = False Then
                left_length_p = node(node(Array.IndexOf(Temp_node_right_arry, i.ToString), 1), 3)
            Else
                left_length_p = 1
            End If
            If node(Array.IndexOf(Temp_node_right_arry, i.ToString), 2).StartsWith("$") = False Then
                right_length_p = node(node(Array.IndexOf(Temp_node_right_arry, i.ToString), 2), 3)
            Else
                right_length_p = 1
            End If
        End If




        If left_length_p > right_length_p Then
            If left_length > right_length Then
                Dim Temp_str As String
                Temp_str = node(i, 1)
                node(i, 1) = node(i, 2)
                node(i, 2) = Temp_str
            End If
        Else
            If left_length < right_length Then
                Dim Temp_str As String
                Temp_str = node(i, 1)
                node(i, 1) = node(i, 2)
                node(i, 2) = Temp_str
            End If
        End If

        If node(i, 1).StartsWith("$") = False Then
            exchange_clade(node(i, 1))
        End If
        If node(i, 2).StartsWith("$") = False Then
            exchange_clade(node(i, 2))
        End If
    End Sub
    Public Function left_clade_length(ByVal node_num As Integer) As Single

        Dim Temp_num As Integer = 0
        Dim length As Integer = 0
        If node(node_num, 1).StartsWith("$") Then
            Return 1
        Else
            Do
                node_num = node(node_num, 1)
                Temp_num = Temp_num + node(node_num, 3) / 2
            Loop Until node(node_num, 1).StartsWith("$")
            Return Temp_num + 1
        End If
    End Function
    Public Function right_clade_length(ByVal node_num As Integer) As Single
        Dim Temp_num As Integer = 0
        Dim length As Integer = 0
        If node(node_num, 2).StartsWith("$") Then
            Return 1
        Else
            Do
                node_num = node(node_num, 2)
                Temp_num = Temp_num + node(node_num, 3) / 2
            Loop Until node(node_num, 2).StartsWith("$")
            Return Temp_num + 1
        End If
    End Function
    Public Sub clade_xy(ByVal node_number As Integer)
        Dim left_clade As String = node(node_number, 1)
        Dim right_clade As String = node(node_number, 2)
        If left_clade.StartsWith("$") = False Then
            node_xy(left_clade, 1) = node_xy(node_number, 1) + node_lr(node_number, 0)
            node_xy(left_clade, 0) = (node(left_clade, 0) - 1)
            clade_xy(CInt(left_clade))
        End If

        If right_clade.StartsWith("$") = False Then
            node_xy(right_clade, 1) = node_xy(node_number, 1) - node_lr(node_number, 1)
            node_xy(right_clade, 0) = (node(right_clade, 0) - 1)
            clade_xy(CInt(right_clade))
        End If
    End Sub
    Public Function clade_length(ByVal node_number As Integer) As Integer
        Dim node_length As Integer = 0
        Dim left_clade As String = node(node_number, 1)
        Dim right_clade As String = node(node_number, 2)

        If left_clade.StartsWith("$") And left_clade.EndsWith("$") Then
            node_length = node_length + 1
        Else
            node_length = node_length + clade_length(CInt(left_clade))
        End If


        If right_clade.StartsWith("$") And right_clade.EndsWith("$") Then
            node_length = node_length + 1
        Else
            node_length = node_length + clade_length(CInt(right_clade))
        End If
        node(node_number, 3) = node_length
        Return node_length
    End Function

    Private Function node_contain(ByVal num As Integer) As String
        Dim contain As String = ""
        Dim left_clade As String = node(num, 1)
        Dim right_clade As String = node(num, 2)
        If left_clade.StartsWith("$") Then
            contain = contain + left_clade
        Else
            contain = contain + node_contain(left_clade)
        End If
        If right_clade.StartsWith("$") Then
            contain = contain + right_clade
        Else
            contain = contain + node_contain(right_clade)
        End If
        Return contain
    End Function


    Private Sub SaveResultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "Text File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            File.Copy(root_path + "temp" + path_char + "result.txt", opendialog.FileName, True)
            MsgBox("Save Successfully!", MsgBoxStyle.Information)
        End If
    End Sub

    Public Function f_node(ByVal c_tree As String, ByVal clade As String, ByVal ID As String) As String
        Dim particular_node_clade As String = ""
        Dim particular_node_area As String = "#"
        Dim Tempnodearry() As String = (clade.Replace(" ", "").Replace(",,", ",") + ",").Replace(",,", "").Split(New Char() {","c})

        For Each Tempstr As String In Tempnodearry
            If Tempstr <> "" Then
                If Tempstr.Contains("-") Then
                    Dim Tempnum() As String = Tempstr.Split(New Char() {"-"c})
                    For i As Integer = CInt(Tempnum(0)) To CInt(Tempnum(1))
                        particular_node_clade = particular_node_clade + "#" + i.ToString
                    Next
                Else
                    particular_node_clade = particular_node_clade + "#" + Tempstr
                End If
            End If
        Next
        particular_node_clade = particular_node_clade.Replace("##", "#")
        Dim Tempnodearry1() As String = particular_node_clade.Split(New Char() {"#"c})
        Array.Sort(Tempnodearry1)

        Dim Temp_tree As String
        Temp_tree = c_tree.Replace("(", "#").Replace(")", "#").Replace(",", "#")
        Dim t_count As Integer = 0
        For Each Tempstr As String In Tempnodearry1
            If Tempstr <> "" Then
                t_count = t_count + 1
                Temp_tree = Temp_tree.Replace("#" + Tempstr + "#", "#" + "x".PadRight(Tempstr.Length, "x") + "#")
            End If
        Next
        Dim s_num As Integer = Temp_tree.IndexOf("x")
        Dim e_num As Integer = Temp_tree.LastIndexOf("x")
        Temp_tree = Temp_tree.Substring(s_num, e_num - s_num + 1).Replace("#", "").Replace("x", "")
        If Temp_tree = "" Then
            Do While c_tree.Substring(s_num - 1, 1) = "("
                s_num = s_num - 1
                If s_num = 0 Then
                    Exit Do
                End If
            Loop
            Do
                e_num = e_num + 1
            Loop Until c_tree.Substring(e_num, 1) <> ")"

            Dim Temp_char() As Char = c_tree
            Dim l_c As Integer = 0
            Dim r_c As Integer = 0
            For i As Integer = s_num To e_num
                If Temp_char(i) = "(" Then
                    l_c = l_c + 1
                End If
                If Temp_char(i) = ")" Then
                    r_c = r_c + 1
                End If
            Next
            Dim Temp_tree1 As String
            If l_c >= r_c Then
                If c_tree.Substring(s_num, l_c - r_c + 1).Replace("(", "") <> "" Then
                    If Tempnodearry1.Length > 2 Then
                        Return c_tree
                    End If

                End If
                Temp_tree1 = c_tree.Substring(s_num + l_c - r_c, e_num - s_num - l_c + r_c)
            Else
                If c_tree.Substring(e_num - (r_c - l_c) - 1, (r_c - l_c) + 1).Replace(")", "") <> "" Then
                    If Tempnodearry1.Length > 2 Then
                        Return c_tree
                    End If
                End If
                Temp_tree1 = c_tree.Substring(s_num, e_num - s_num + l_c - r_c)
            End If
            Dim Temp_char1() As Char = Temp_tree1
            Dim l_c1 As Integer = 0
            Dim r_c1 As Integer = 0
            For i As Integer = 0 To Temp_tree1.Length - 1
                If Temp_char1(i) = "(" Then
                    l_c1 = l_c1 + 1
                End If
                If Temp_char1(i) = ")" Then
                    r_c1 = r_c1 + 1
                End If
                If r_c1 > l_c1 Then
                    Return c_tree
                End If
            Next
            If l_c >= r_c Then
                c_tree = c_tree.Insert(s_num, "(")
                c_tree = c_tree.Insert(e_num + 1, "," + ID + ")")
            Else
                c_tree = c_tree.Insert(s_num, "(")
                c_tree = c_tree.Insert(e_num + 1 - r_c + l_c, "," + ID + ")")
            End If

        End If
        Return c_tree
    End Function
    Public Function e_node(ByVal c_tree As String, ByVal clade As String) As String
        Dim particular_node_clade As String = ""
        Dim particular_node_area As String = "#"
        Dim Tempnodearry() As String = (clade.Replace(" ", "").Replace(",,", ",") + ",").Replace(",,", "").Split(New Char() {","c})

        For Each Tempstr As String In Tempnodearry
            If Tempstr <> "" Then
                If Tempstr.Contains("-") Then
                    Dim Tempnum() As String = Tempstr.Split(New Char() {"-"c})
                    For i As Integer = CInt(Tempnum(0)) To CInt(Tempnum(1))
                        particular_node_clade = particular_node_clade + "#" + i.ToString
                    Next
                Else
                    particular_node_clade = particular_node_clade + "#" + Tempstr
                End If
            End If
        Next
        particular_node_clade = particular_node_clade.Replace("##", "#")
        Dim Tempnodearry1() As String = particular_node_clade.Split(New Char() {"#"c})
        Array.Sort(Tempnodearry1)

        Dim Temp_tree As String
        Temp_tree = c_tree.Replace("(", "#").Replace(")", "#").Replace(",", "#")
        Dim t_count As Integer = 0
        For Each Tempstr As String In Tempnodearry1
            If Tempstr <> "" Then
                t_count = t_count + 1
                Temp_tree = Temp_tree.Replace("#" + Tempstr + "#", "#" + "x".PadRight(Tempstr.Length, "x") + "#")
            End If
        Next
        Dim s_num As Integer = Temp_tree.IndexOf("x")
        Dim e_num As Integer = Temp_tree.LastIndexOf("x")
        Temp_tree = Temp_tree.Substring(s_num, e_num - s_num + 1).Replace("#", "").Replace("x", "")
        If Temp_tree = "" Then
            Do While c_tree.Substring(s_num - 1, 1) = "("
                s_num = s_num - 1
                If s_num = 0 Then
                    Exit Do
                End If
            Loop
            Do
                e_num = e_num + 1
            Loop Until c_tree.Substring(e_num, 1) <> ")"

            Dim Temp_char() As Char = c_tree
            Dim l_c As Integer = 0
            Dim r_c As Integer = 0
            For i As Integer = s_num To e_num
                If Temp_char(i) = "(" Then
                    l_c = l_c + 1
                End If
                If Temp_char(i) = ")" Then
                    r_c = r_c + 1
                End If
            Next
            If l_c >= r_c Then
                If c_tree.Substring(s_num, l_c - r_c + 1).Replace("(", "") <> "" Then
                    If Tempnodearry1.Length > 2 Then
                        Return c_tree
                    End If

                End If
                c_tree = c_tree.Substring(s_num + l_c - r_c, e_num - s_num - l_c + r_c)
            Else
                If c_tree.Substring(e_num - (r_c - l_c) - 1, (r_c - l_c) + 1).Replace(")", "") <> "" Then
                    If Tempnodearry1.Length > 2 Then
                        Return c_tree
                    End If
                End If
                c_tree = c_tree.Substring(s_num, e_num - s_num + l_c - r_c)
            End If
        End If
        Return c_tree
    End Function

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If TextBox1.Text <> "" Then
            For i As Integer = 0 To dtView.Count - 1
                If TextBox1.Text = dtView.Item(i)(1).ToString Then
                    MsgBox("Two species cannot have the same name!")
                    Exit Sub
                End If
            Next
            For i As Integer = 0 To odView.Count - 1
                If TextBox1.Text = odView.Item(i)(1).ToString Then
                    MsgBox("Two species cannot have the same name!")
                    Exit Sub
                End If
            Next
        Else
            MsgBox("The name of species could not be NULL!")
            Exit Sub
        End If
        odView.AllowNew = True
        odView.AddNew()
        If odView.Count > 2 Then
            odView.Item(odView.Count - 2).Item(0) = dtView.Count + odView.Count - 1
            odView.Item(odView.Count - 2).Item(1) = TextBox1.Text
            odView.Item(odView.Count - 2).Item(2) = ComboBox1.Text.Split(New Char() {"|"c})(1)
        Else
            odView.Item(0).Item(0) = dtView.Count + 1
            odView.Item(0).Item(1) = TextBox1.Text
            odView.Item(0).Item(2) = ComboBox1.Text.Split(New Char() {"|"c})(1)
        End If

        odView.AllowNew = False
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If DataGridView1.SelectedRows.Count > 0 Then
            If odView.Count - 1 <> DataGridView1.SelectedRows.Item(0).Index Then
                odView.AllowDelete = True
                odView.AllowNew = True
                'odView.AddNew()
                odView.Table.Rows(DataGridView1.SelectedRows.Item(0).Index).Delete()
                'DataGridView1.Rows.RemoveAt(DataGridView1.SelectedRows.Item(0).Index)

                For i As Integer = 0 To odView.Count - 2
                    odView.Item(i).Item(0) = dtView.Count + 1 + i
                Next
                odView.AllowNew = False
                odView.AllowDelete = False
            End If
        Else
            MsgBox("Please select a row to delete!")
        End If
    End Sub
    Public Sub export_omitted()
        Process_ID = 1
        Dim wt As New StreamWriter(export_file_name, False)
        Dim winfo As New StreamWriter(export_file_name.Replace(".trees", "_info.txt"), False)
        wt.WriteLine("#NEXUS")
        wt.WriteLine("")
        wt.WriteLine("Begin taxa;")
        wt.WriteLine("	Dimensions ntax=" + (odView.Count - 1 + dtView.Count).ToString + ";")
        wt.WriteLine("	Taxlabels")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(1))
        Next
        For i As Integer = 0 To odView.Count - 2
            wt.WriteLine("		" + odView.Item(i)(1))
        Next

        wt.WriteLine("		;")
        wt.WriteLine("End;")
        wt.WriteLine("")
        wt.WriteLine("Begin trees;")
        wt.WriteLine("	Translate")
        For i As Integer = 0 To dtView.Count - 1
            wt.WriteLine("		" + dtView.Item(i)(0) + " " + dtView.Item(i)(1) + ",")
        Next
        For i As Integer = 0 To odView.Count - 2
            wt.WriteLine("		" + odView.Item(i)(0) + " " + odView.Item(i)(1) + ",")
        Next
        If odView.Count <> 2 Then
            wt.WriteLine("		" + odView.Item(odView.Count - 2)(0) + " " + odView.Item(odView.Count - 2)(1) + ",")
        End If
        wt.WriteLine(";")
        'Wait.Show()
        Dim rt As New StreamReader(root_path + omittedtree)
        Dim line As String
        Dim o_num As Integer = 0
        Dim tree_num As Integer = 0
        For i As Integer = 1 To CInt(BurninBox.Text)
            line = rt.ReadLine
            tree_num = tree_num + 1
        Next
        line = rt.ReadLine
        tree_num = tree_num + 1
        Dim c_tree As String = line
        Dim new_id() As Integer
        Dim new_length() As Integer
        ReDim new_id(odView.Count - 2)
        ReDim new_length(odView.Count - 2)
        For i As Integer = 0 To odView.Count - 2
            new_id(i) = i
            new_length(i) = (odView.Item(i)(2).ToString.Replace(",", "").Length - odView.Item(i)(2).ToString.Length)
        Next
        Array.Sort(new_length, new_id)
        Do
            Process_Int = 10000 * tree_num / CInt(TreeBox.Text)
            c_tree = line
            For i As Integer = 0 To odView.Count - 2
                line = c_tree
                c_tree = f_node(c_tree, odView.Item(new_id(i))(2), odView.Item(new_id(i))(0))
                If c_tree = line Then
                    Exit For
                End If
            Next
            If c_tree <> line Then
                o_num = o_num + 1
                wt.WriteLine(("tree NEWTREE_" + o_num.ToString + " = " + c_tree + ";").Replace(";;", ";"))
            End If
            line = rt.ReadLine
            tree_num = tree_num + 1
        Loop Until line Is Nothing
        wt.WriteLine("End;")
        rt.Close()
        wt.Close()
        winfo.WriteLine("ID		Name		Clade")

        For i As Integer = 0 To odView.Count - 2
            winfo.WriteLine(odView.Item(i)(0) + "		" + odView.Item(i)(1) + "		" + odView.Item(i)(2))
        Next

        winfo.WriteLine("Amount of trees: " + TreeBox.Text)
        winfo.WriteLine("Burn-in: " + BurninBox.Text)
        winfo.WriteLine("Export trees: " + o_num.ToString + " (" + CSng(o_num / (CInt(TreeBox.Text) - CInt(BurninBox.Text)) * 100).ToString("F2") + "%)")
        winfo.Close()
        Process_ID = -1
        Process_Int = 0
        Process_Text = "Export Successfully!" + Chr(10)
    End Sub

    Public Sub export_clade()
        Dim e_clade() As String = ComboBox_text.Split(New Char() {"|"c})(1).Split(New Char() {","c})
        Array.Sort(e_clade)
        Dim wt As New StreamWriter(export_file_name, False)
        wt.WriteLine("#NEXUS")
        wt.WriteLine("")
        wt.WriteLine("Begin taxa;")
        wt.WriteLine("	Dimensions ntax=" + e_clade.Length.ToString + ";")
        wt.WriteLine("	Taxlabels")
        For i As Integer = 0 To dtView.Count - 1
            If Array.IndexOf(e_clade, dtView.Item(i)(0).ToString) >= 0 Then
                wt.WriteLine("		" + dtView.Item(i)(1))
            End If
        Next

        wt.WriteLine("		;")
        wt.WriteLine("End;")
        wt.WriteLine("")
        wt.WriteLine("Begin trees;")
        wt.WriteLine("	Translate")
        Dim sp_name() As Integer
        ReDim sp_name(e_clade.Length - 1)
        Dim sp_count As Integer = 0
        For i As Integer = 0 To dtView.Count - 1
            If Array.IndexOf(e_clade, dtView.Item(i)(0).ToString) >= 0 Then
                wt.WriteLine("		" + (sp_count + 1).ToString + " " + dtView.Item(i)(1) + ",")
                sp_name(sp_count) = i + 1
                sp_count += 1
            End If
        Next
        wt.WriteLine("		;")
        'Wait.Show()
        Dim rt As New StreamReader(root_path + omittedtree)
        Dim line As String
        Dim o_num As Integer = 0
        Dim tree_num As Integer = 0
        For i As Integer = 1 To CInt(BurninBox.Text)
            line = rt.ReadLine
            tree_num = tree_num + 1
            'Wait.Info.Text = "Processing tree " + tree_num.ToString
        Next
        line = rt.ReadLine
        tree_num = tree_num + 1
        Dim c_tree As String = line
        Do
            c_tree = line
            line = c_tree
            c_tree = e_node(c_tree, ComboBox_text.Split(New Char() {"|"c})(1))
            If c_tree <> line Then
                o_num = o_num + 1
                For i As Integer = 0 To sp_name.Length - 1
                    c_tree = c_tree.Replace("(" + sp_name(i).ToString + ",", "(" + (i + 1).ToString + ",") _
                    .Replace(")" + sp_name(i).ToString + ",", ")" + (i + 1).ToString + ",") _
                    .Replace("," + sp_name(i).ToString + ")", "," + (i + 1).ToString + ")") _
                    .Replace("," + sp_name(i).ToString + "(", "," + (i + 1).ToString + "(")
                Next

                wt.WriteLine(("tree NEWTREE_" + o_num.ToString + " = " + c_tree + ";").Replace(";;", ";"))
            End If
            line = rt.ReadLine
            tree_num = tree_num + 1
        Loop Until line Is Nothing
        wt.WriteLine("End;")
        rt.Close()
        wt.Close()
        Process_Text = "Export Successfully!" + Chr(10)
    End Sub
    Dim export_file_name As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        DataGridView1.EndEdit()
        If odView.Count > 1 Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Tree dataset  (*.trees)|*.trees;*.TREES|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".trees"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                If File.Exists(opendialog.FileName) Then
                    File.Delete(opendialog.FileName)
                End If
                export_file_name = opendialog.FileName
                Dim thead1 As New Thread(AddressOf export_omitted)
                thead1.CurrentCulture = ci
                thead1.Start()
            End If
        Else
            MsgBox("No omitted species!")
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        If ComboBox1.SelectedIndex > taxon_num - 1 Then
            Dim opendialog As New SaveFileDialog
            opendialog.Filter = "Tree dataset  (*.trees)|*.trees;*.TREES|ALL Files(*.*)|*.*"
            opendialog.FileName = ""
            opendialog.DefaultExt = ".trees"
            opendialog.CheckFileExists = False
            opendialog.CheckPathExists = True
            Dim resultdialog As DialogResult = opendialog.ShowDialog()
            If resultdialog = DialogResult.OK Then
                If File.Exists(opendialog.FileName) Then
                    File.Delete(opendialog.FileName)
                End If
                export_file_name = opendialog.FileName
                Dim thead1 As New Thread(AddressOf export_clade)
                thead1.CurrentCulture = ci
                thead1.Start()
            End If

        Else
            MsgBox("No clade available!")
        End If
    End Sub
    Dim ComboBox_text As String = ""
    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        ComboBox_text = ComboBox1.Text
    End Sub
End Class