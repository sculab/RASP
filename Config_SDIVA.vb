Imports System.IO
Public Class Config_SDIVA
    Dim range_dataset As New DataSet
    Dim rgView As New DataView


    Private Sub Range_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'RangeMade = True
        e.Cancel = True
        Me.Hide()

    End Sub

    Private Sub CheckBox4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox4.CheckedChanged
        NumericUpDown2.ReadOnly = CheckBox4.Checked Xor True
    End Sub

    Private Sub CheckBox7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox7.CheckedChanged
        TextBox4.ReadOnly = CheckBox7.Checked Xor True
    End Sub
    Private Sub CheckBox9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox9.CheckedChanged
        TextBox1.ReadOnly = CheckBox9.Checked Xor True
    End Sub
    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged
        TextBox5.ReadOnly = CheckBox8.Checked Xor True
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
    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Dim area_num As Integer
    Dim rang_num As Integer
    Private Sub RefreshTheRangeListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshTheRangeListToolStripMenuItem.Click
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
            ListBox2.Items.Add(ListBox1.SelectedItem)
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
        End If
        If ListBox2.Items.Count > 0 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If ListBox2.SelectedIndex >= 0 Then
            ListBox1.Items.Add(ListBox2.SelectedItem)
            ListBox2.Items.RemoveAt(ListBox2.SelectedIndex)
        End If
        If ListBox2.Items.Count > 0 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub


    Private Sub DataGridView1_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellEndEdit
        Dim Tempchar() As Char = RangeStr.ToUpper
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
        If ListBox2.Items.Count > 0 Then
            CheckBox1.Checked = True
        Else
            CheckBox1.Checked = False
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        muti_threads_DIVA = NumericUpDown3.Value
        '[去除不需要的节点
        excludeline = ""

        If DIVAForm.ListBox2.Items.Count > 0 Then
            CheckBox1.Checked = True
            excludeline = "exclude"
            For i As Integer = 1 To DIVAForm.ListBox2.Items.Count
                excludeline += " " + DIVAForm.ListBox2.Items(i - 1)
            Next
            excludeline += ";"
        End If

        '去除不需要的节点]
        fossilline = "Fossil"
        CheckBox12.Checked = False
        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString.Replace(" ", "") <> "" Then
                CheckBox1.Checked = True
                CheckBox12.Checked = True
                fossilline += " " + DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString.ToUpper
            Else
                fossilline += " 0"
            End If
        Next
        fossilline += ";"
        config_SDIVA_node = TextBox6.Text
        config_SDIVA_omitted = TextBox7.Text
        StartTreeView = False
        RangeMade = True
        DeleteFiles(root_path + "temp", ".diva")
        For i As Integer = 0 To muti_threads_DIVA - 1
            File.Delete(root_path + "temp\SDIVA-" + i.ToString + ".txt")
        Next
        Me.Hide()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Hide()
    End Sub

    Private Sub RangeForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            If RangeStr.Length <> DataGridView1.Columns.Count Then
                If taxon_num > 10 Then
                    NumericUpDown1.Value = max(2, CInt(2 ^ (taxon_num / 60)))
                End If
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
            End If
            If DataGridView2.ColumnCount = 0 Then
                DataGridView2.RowHeadersVisible = False
                Dim id_node As New DataGridViewTextBoxColumn
                id_node.HeaderText = "Node ID"
                Dim Member_node As New DataGridViewTextBoxColumn
                Member_node.HeaderText = "Member"
                Dim Fossil_node As New DataGridViewTextBoxColumn
                Fossil_node.HeaderText = "Fossil"
                DataGridView2.Columns.Add(id_node)
                DataGridView2.Columns.Add(Member_node)
                DataGridView2.Columns.Add(Fossil_node)
                DataGridView2.Columns(0).Width = 75
                DataGridView2.Columns(1).Width = 125
                DataGridView2.Columns(2).Width = 75
                DataGridView2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(2).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(0).ReadOnly = True
                DataGridView2.Columns(1).ReadOnly = True
                For i As Integer = 1 To nodeView.Count
                    Dim temp_row(2) As String
                    temp_row(0) = nodeView.Item(i - 1).Item(0).ToString()
                    temp_row(1) = nodeView.Item(i - 1).Item(1).ToString()
                    temp_row(2) = ""
                    DataGridView2.Rows.Add(temp_row)
                Next
                DataGridView2.AllowUserToAddRows = False
                DataGridView2.AllowUserToDeleteRows = False
                DataGridView2.AllowUserToOrderColumns = False
            End If
        End If
    End Sub

    Private Sub RangeForm_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci

    End Sub

    Private Sub CheckBox11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox11.CheckedChanged
        If CheckBox11.Checked Then
            CheckBox10.Checked = False
        End If
    End Sub

    Private Sub CheckBox10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox10.CheckedChanged
        If CheckBox10.Checked Then
            CheckBox11.Checked = False
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        If CheckBox3.Checked = False Then
            TextBox6.Enabled = False
            TextBox7.ReadOnly = True
            CheckBox10.Enabled = False
            CheckBox11.Enabled = False
            CheckBox12.Enabled = True
        Else

            TextBox6.Enabled = True
            TextBox7.ReadOnly = False
            CheckBox10.Enabled = True
            CheckBox11.Enabled = True
            CheckBox12.Enabled = False
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Try
            CheckBox3.Checked = True
            TextBox6.Text = DIVAForm.ComboBox1.Text.Split(New Char() {"|"c})(1)
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        particular_node_num = "node of " + TextBox6.Text
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        If Me.Visible Then
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
        End If
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        TextBox3.ReadOnly = CheckBox2.Checked Xor True
        NumericUpDown1.Enabled = CheckBox2.Checked
        CheckBox5.Enabled = CheckBox2.Checked
        CheckBox8.Enabled = CheckBox2.Checked
    End Sub

    Private Sub CheckBox5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox5.CheckedChanged
        NumericUpDown1.ReadOnly = CheckBox5.Checked Xor True
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

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
            If CheckBox4.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            sw.Write(NumericUpDown2.Value.ToString + ",")
            If CheckBox1.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            If CheckBox2.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            sw.Write(TextBox3.Text + ",")
            If CheckBox5.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            sw.Write(NumericUpDown1.Value.ToString + ",")
            If CheckBox8.Checked Then
                sw.Write("1,")
            Else
                sw.Write("0,")
            End If
            sw.Write(TextBox5.Text + ",")
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

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

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
                            CheckBox4.Checked = True
                        Else
                            CheckBox4.Checked = False
                        End If
                        NumericUpDown2.Value = CInt(range_list(1))
                        If range_list(2) = 1 Then
                            CheckBox1.Checked = True
                        Else
                            CheckBox1.Checked = False
                        End If
                        If range_list(3) = 1 Then
                            CheckBox2.Checked = True
                        Else
                            CheckBox2.Checked = False
                        End If
                        TextBox3.Text = range_list(4)
                        If range_list(5) = 1 Then
                            CheckBox5.Checked = True
                        Else
                            CheckBox5.Checked = False
                        End If
                        NumericUpDown1.Value = CInt(range_list(6))
                        If range_list(7) = 1 Then
                            CheckBox8.Checked = True
                        Else
                            CheckBox8.Checked = False
                        End If
                        TextBox5.Text = range_list(8)
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