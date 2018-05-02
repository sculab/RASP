Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Config_BayArea
    <DllImport("BAYAREA.dll")> Public Shared Function runbayarea(ByVal pam1 As String, ByRef genno As Integer) As Integer
    End Function
    Private Sub BayArea_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If TargetOS = "macos" Then
            Me.TopMost = False
        End If
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        ComboBox1.SelectedIndex = 1
        ComboBox4.SelectedIndex = 0
        ComboBox3.SelectedIndex = 1
        ComboBox2.SelectedIndex = 1
    End Sub

    Private Sub BayArea_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            If DataGridView1.RowCount <> RangeStr.Length Then
                Dim Tempchar() As Char = RangeStr.ToUpper
                Array.Sort(Tempchar)
                DataGridView1.Columns.Clear()
                DataGridView1.Rows.Clear()
                DataGridView1.AllowUserToAddRows = True
                DataGridView1.AllowUserToDeleteRows = True
                DataGridView1.AllowUserToOrderColumns = True
                DataGridView1.AllowUserToResizeColumns = True
                DataGridView1.AllowUserToResizeRows = True
                Dim Column0 As New DataGridViewTextBoxColumn
                Column0.HeaderText = "Area"
                Column0.Width = 40
                Column0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                Column0.ReadOnly = True
                DataGridView1.Columns.Add(Column0)
                Dim Column1 As New DataGridViewTextBoxColumn
                Column1.HeaderText = "Latitude"
                Column1.Width = 80
                Column1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns.Add(Column1)
                Dim Column2 As New DataGridViewTextBoxColumn
                Column2.HeaderText = "Longitude"
                Column2.Width = 80
                Column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
                DataGridView1.Columns.Add(Column2)
                For i As Integer = 0 To Tempchar.Length - 1
                    DataGridView1.Rows.Add()
                    DataGridView1.Rows(i).Cells(0).Value = Tempchar(i)
                    For j As Integer = 1 To 2
                        DataGridView1.Rows(i).Cells(j).Value = "0.0"
                    Next
                Next
                DataGridView1.AllowUserToAddRows = False
                DataGridView1.AllowUserToDeleteRows = False
                DataGridView1.AllowUserToOrderColumns = False
            End If


            Read_Poly_Tree(tree_show_with_value)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|Phylip File (*.txt)|*.txt;*.TXT|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".csv"
        opendialog.CheckFileExists = True
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Try
                Dim Tempchar() As Char = RangeStr.ToUpper
                Array.Sort(Tempchar)
                Dim dr As New StreamReader(opendialog.FileName)
                Dim line As String = ""
                If opendialog.FileName.ToLower.EndsWith(".txt") Then
                    line = dr.ReadLine
                    For i As Integer = 0 To Tempchar.Length - 1
                        line = dr.ReadLine
                        DataGridView1.Rows(i).Cells(1).Value = line.Split(New Char() {" "c})(0)
                        DataGridView1.Rows(i).Cells(2).Value = line.Split(New Char() {" "c})(1)
                    Next
                Else
                    For i As Integer = 0 To Tempchar.Length - 1
                        line = dr.ReadLine
                        DataGridView1.Rows(i).Cells(1).Value = line.Split(New Char() {","c})(1)
                        DataGridView1.Rows(i).Cells(2).Value = line.Split(New Char() {","c})(2)
                    Next
                End If
                dr.Close()
            Catch ex As Exception
                MsgBox("Please check your geographic data before analysis!")
            End Try
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".csv"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Dim dw As New StreamWriter(opendialog.FileName, False)
            Dim Tempchar() As Char = RangeStr.ToUpper
            Array.Sort(Tempchar)
            For i As Integer = 0 To Tempchar.Length - 1
                dw.WriteLine(DataGridView1.Rows(i).Cells(0).Value + "," + DataGridView1.Rows(i).Cells(1).Value + "," + DataGridView1.Rows(i).Cells(2).Value)
            Next
            dw.Close()
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Hide()
    End Sub
    Dim bay_tree As String
    Dim TaxonName() As String
    Dim TaxonTime(,) As String
    Dim taxon_array() As String
    Dim has_length As Boolean
    Dim root_time As Single
    Dim Tree_Export_Char() As String
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
            Next
        End If
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
                    If IsNumeric(tree_char(i + 1)) Then
                        tree_char(i + 1) = ""
                    End If
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
                        Temp_node(point_1 - 2, 4) = min(Val(Temp_node(point_1 - 2, 4)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
                        Temp_node(point_1 - 2, 5) = max(Val(Temp_node(point_1 - 2, 5)), (Val(Poly_Node(point_2, 5)) + Val(Poly_Node(point_2, 4))) / 2)
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
                    If tree_char(i - 1) <> ")" Then

                        taxon_array(tx) = tree_char(i)
                        tx += 1
                        Temp_node(point_1 - 1, 1) += tree_char(i) + ","
                        Temp_node(point_1 - 1, 4) = min(Val(Temp_node(point_1 - 1, 4)), tx)
                        Temp_node(point_1 - 1, 5) = max(Val(Temp_node(point_1 - 1, 4)), tx)
                    End If
            End Select
        Next
        If has_length Then
            make_chain(taxon_num - 1 - 1)

            root_time = 0
            For i As Integer = 0 To taxon_num - 1
                If root_time < Val(TaxonTime(i, 1)) Then
                    root_time = Val(TaxonTime(i, 1))
                End If
            Next
            For i As Integer = 1 To char_id
                If Tree_Export_Char(i).Contains(":") And Tree_Export_Char(i).Contains(";") = False Then
                    tree_char(i) = tree_char(i).Split(New Char() {":"c})(0) + ":" + tree_char(i).Split(New Char() {":"c})(1)
                End If
                If Tree_Export_Char(i).Contains(";") And Tree_Export_Char(i).Contains(":") = False Then
                    tree_char(i) = ":" + (Int(root_time * 2)).ToString + ";"
                End If
            Next
        End If
        bay_tree = ""
        For i As Integer = 1 To char_id
            bay_tree += tree_char(i)
        Next
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
    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If CInt(TextBox3.Text) Mod CInt(TextBox1.Text) <> 0 Then
            MsgBox("Chain length should be an integer multiple of frequent of samples.")
            Exit Sub
        End If
            delete_temp_file("bayArea.areas.txt")
            delete_temp_file("bayArea.geo.txt")
            delete_temp_file("bayArea.tree.txt")
            delete_temp_file("bayArea.config.txt")
            Dim range_file As New StreamWriter(root_path + "temp\bayarea.areas.txt", False)
            range_file.WriteLine(dtView.Count.ToString + " " + RangeStr.Length.ToString)
            For i As Integer = 1 To dtView.Count
                range_file.WriteLine(MainWindow.DataGridView1.Rows(i - 1).Cells(0).Value.ToString + " " + Distributiton_to_Binary(MainWindow.DataGridView1.Rows(i - 1).Cells(2).Value.ToString, RangeStr.Length))
            Next
            range_file.Close()

            Dim geo_file As New StreamWriter(root_path + "temp\bayarea.geo.txt", False)
            geo_file.WriteLine("# 0.0")
        For i As Integer = 0 To RangeStr.Length - 1
            If CSng(DataGridView1.Rows(i).Cells(1).Value) = 0 Then
                DataGridView1.Rows(i).Cells(1).Value = "0.000001"
            End If
            If CSng(DataGridView1.Rows(i).Cells(2).Value) = 0 Then
                DataGridView1.Rows(i).Cells(2).Value = "0.000001"
            End If
            geo_file.WriteLine(DataGridView1.Rows(i).Cells(1).Value.ToString + " " + DataGridView1.Rows(i).Cells(2).Value.ToString)
        Next
            geo_file.Close()

            Dim tree_file As New StreamWriter(root_path + "temp\bayarea.tree.txt", False)
            tree_file.WriteLine(bay_tree)
            tree_file.Close()

            Dim config_file As New StreamWriter(root_path + "temp\bayarea.config.txt", False)
            config_file.WriteLine("-areaFileName=bayarea.areas.txt")
            config_file.WriteLine("-geoFileName=bayarea.geo.txt")
            config_file.WriteLine("-treeFileName=bayarea.tree.txt")
            config_file.WriteLine("-chainLength=" + TextBox3.Text)
            config_file.WriteLine("-parameterSampleFrequency=" + TextBox1.Text)
            config_file.WriteLine("-historySampleFrequency=" + TextBox1.Text)
            config_file.WriteLine("-chainBurnIn=0")
            config_file.WriteLine("-probBurnIn=0")
            config_file.WriteLine("-printFrequency=" + TextBox1.Text)
            Select Case ComboBox1.SelectedIndex
                Case 0
                    config_file.WriteLine("-modelType=1")
                Case Else
                    config_file.WriteLine("-modelType=3")
            End Select
            config_file.WriteLine("-guessInitialRates=" + ComboBox4.Text)
            config_file.WriteLine("-geoDistancePowerPositive=" + ComboBox2.Text)
            config_file.WriteLine("-geoDistanceTruncate=" + ComboBox3.Text)
            config_file.WriteLine(TextBox2.Text)
            config_file.Close()

            StartTreeView = False
            Process_ID = 7
            bayarea_gen = 0
            MainWindow.FDTimer.Enabled = True
            bayesIsrun = True
            Process_Text += Chr(10) + "**********BAYAREA ANALYSIS**********"
        Process_Text += Chr(10) + "Analysis start at " + Date.Now.ToString + Chr(10)

        config_BayArea_cycle = CSng(TextBox3.Text)
        config_BayArea_fre = CSng(TextBox1.Text)

            Dim lb As New Thread(AddressOf loadbayes)
            lb.CurrentCulture = ci
            lb.Start()
            Me.Hide()
    End Sub
    Public Sub loadbayes()
        current_dir = Directory.GetCurrentDirectory
        Directory.SetCurrentDirectory(root_path)
        runbayarea("AB", bayarea_gen)
        Directory.SetCurrentDirectory(current_dir)
        bayarea_gen = -2
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked Then
            Dim fl As New FolderBrowserDialog
            Dim se As DialogResult
            se = fl.ShowDialog()
            If se = DialogResult.OK Then
                TextBox5.Text = fl.SelectedPath
            Else
                CheckBox1.Checked = False
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Dim Tempchar() As Char = RangeStr.ToUpper
        Array.Sort(Tempchar)
        DataGridView1.Columns.Clear()
        DataGridView1.Rows.Clear()
        DataGridView1.AllowUserToAddRows = True
        DataGridView1.AllowUserToDeleteRows = True
        DataGridView1.AllowUserToOrderColumns = True
        DataGridView1.AllowUserToResizeColumns = True
        DataGridView1.AllowUserToResizeRows = True
        Dim Column0 As New DataGridViewTextBoxColumn
        Column0.HeaderText = "Area"
        Column0.Width = 40
        Column0.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        Column0.ReadOnly = True
        DataGridView1.Columns.Add(Column0)
        Dim Column1 As New DataGridViewTextBoxColumn
        Column1.HeaderText = "Latitude"
        Column1.Width = 80
        Column1.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns.Add(Column1)
        Dim Column2 As New DataGridViewTextBoxColumn
        Column2.HeaderText = "Longitude"
        Column2.Width = 80
        Column2.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter
        DataGridView1.Columns.Add(Column2)
        For i As Integer = 0 To Tempchar.Length - 1
            DataGridView1.Rows.Add()
            DataGridView1.Rows(i).Cells(0).Value = Tempchar(i)
            For j As Integer = 1 To 2
                DataGridView1.Rows(i).Cells(j).Value = "0.0"
            Next
        Next
        DataGridView1.AllowUserToAddRows = False
        DataGridView1.AllowUserToDeleteRows = False
        DataGridView1.AllowUserToOrderColumns = False
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        MainWindow.TracerViewToolStripMenuItem.Enabled = False
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        MainWindow.TracerViewToolStripMenuItem.Enabled = False
    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class