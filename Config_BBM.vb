Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Config_BBM
#Const TargetOS = "win32"
#If TargetOS = "linux" Then
    <DllImport("./libmrBayes.so")> Public Function runbayes(ByVal nexpath As String, ByRef genno As Integer) As Integer
    End Function
#ElseIf TargetOS = "win32" Or TargetOS = "macos" Then
    <DllImport("BAYESDLL.dll")> Public Shared Function runbayes(ByVal nexpath As String, ByRef genno As Integer) As Integer
    End Function
#End If
    Dim commandlines(1024) As String
    Dim Select_Node_Num As Integer
    Dim Tree_Node_Num As Integer
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
        If CInt(TextBox4.Text) >= CInt(TextBox3.Text) / CInt(TextBox1.Text) - 1 Then
            MsgBox("Number of discard samples is too large!")
            Exit Sub
        End If
        If CInt(TextBox4.Text) < 10 Then
            MsgBox("Number of discard samples is too small!")
            Exit Sub
        End If
        If NumericUpDown1.Value > RangeStr.Length Then
            NumericUpDown1.Value = RangeStr.Length
        End If
        Dim k As Integer = 1
        Do
            If File.Exists(root_path + "temp" + path_char + "Clade" + k.ToString + ".r") Then
                Try
                    File.Delete(root_path + "temp" + path_char + "Clade" + k.ToString + ".r")
                Catch ex As Exception

                End Try
            End If
            k += 1
        Loop Until k = 512
        Dim usertree As Boolean = False
        If final_tree.Replace(",", "").Length = final_tree.Replace("(", "").Length Then
            usertree = True
        End If
        Dim node_count As Integer = final_tree.Length - final_tree.Replace("(", "").Length
        bayesIsrun = True
        MainWindow.CmdBox.AppendText(Chr(10) + "**************************" + Chr(10))
        MainWindow.CmdBox.AppendText("*Bayesian MCMC Analysis*" + Chr(10))
        MainWindow.CmdBox.AppendText("**************************" + Chr(10))
        MainWindow.CmdBox.AppendText("Process begin at " + Date.Now.ToString + Chr(10))

        Read_Poly_Node(final_tree.Replace(";", ""))
        For i As Integer = 0 To 1024
            commandlines(i) = ""
        Next
        'If node_count < 512 Then
        commandlines(0) = "outgroup " + (taxon_num + 2).ToString
        Select Case ComboBox1.Text
            Case "Fixed (JC)" 'JC model
                commandlines(100) = "lset nst=1 rates=equal"
            Case "Estimated (F81)" 'F81 model
                commandlines(100) = "lset nst=1 rates=equal"
                commandlines(101) = "prset statefreqpr=dirichlet(" + TextBox5.Text + "," + TextBox6.Text + ")"
        End Select

        Select Case ComboBox2.Text
            Case "Equal"
            Case "Gamma (+G)" '+G
                commandlines(102) = "lset nst=1 rates=gamma"
                commandlines(103) = "prset Shapepr=Uniform(" + TextBox7.Text + "," + TextBox8.Text + ")"
        End Select
        Select Case ComboBox1.Text + ComboBox2.Text
            Case "Fixed (JC)" + "Equal"
                MainWindow.CmdBox.AppendText("Using Model: JC" + Chr(10))
            Case "Fixed (JC)" + "Gamma (+G)"
                MainWindow.CmdBox.AppendText("Using Model: JC+G" + Chr(10))
            Case "Estimated (F81)" + "Equal"
                MainWindow.CmdBox.AppendText("Using Model: F81" + Chr(10))
            Case Else
                MainWindow.CmdBox.AppendText("Using Model: F81+G" + Chr(10))
        End Select

        Select_Node_Num = 0
        ReDim Select_Node_list(0)
        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                Select_Node_Num += 1
                ReDim Preserve Select_Node_list(Select_Node_Num)
                Select_Node_list(Select_Node_Num) = i
            End If
        Next
            commandlines(1019) = "prset topologypr=constraints("
            For i As Integer = 1 To nodeView.Count
                If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                commandlines(200 + i) = "constraint c" + i.ToString + " -1 = " + Poly_Node(i - 1, 3).Replace(",", " ")
                    commandlines(1019) = commandlines(1019) + "c" + i.ToString + ","
                End If
            Next
       

        commandlines(1017) = "lset coding=variable"
        commandlines(1018) = "set autoclose=yes nowarn=yes"
        commandlines(1019) = (commandlines(1019) + ")").Replace(",)", ")")
        commandlines(1020) = "report ancstates=yes"
        If usertree Then
            commandlines(1021) = "usertree = (" + final_tree.Replace(";", "") + "," + (taxon_num + 1).ToString + "," + (taxon_num + 2).ToString + ")"
            commandlines(1022) = "mcmc" + " startingtree=user Ordertaxa=Yes Samplefreq=" + TextBox1.Text + " ngen=" + TextBox3.Text + " nchains=" + NumericUpDown2.Text + " Temp=" + TextBox2.Text
        Else
            commandlines(1021) = ""
            commandlines(1022) = "mcmc" + " Ordertaxa=Yes Samplefreq=" + TextBox1.Text + " ngen=" + TextBox3.Text + " nchains=" + NumericUpDown2.Text + " Temp=" + TextBox2.Text
        End If
        commandlines(1023) = "[burnin=" + TextBox4.Text + ",taxonnum=" + RangeStr.Length.ToString + ",node_num=" + node_count.ToString + "]"
        If Write_Bayes_nex("clade1.nex", 1024) = 0 Then
            show_pie = "bayes"
            current_dir = Directory.GetCurrentDirectory
            Directory.SetCurrentDirectory(root_path)
            MainWindow.CmdBox.AppendText("Using command: " + commandlines(1022) + Chr(10))
            MainWindow.ProgressBar1.Maximum = CInt(TextBox3.Text)
            MainWindow.Bayes_Timer.Enabled = True
            Me.Hide()
            StartTreeView = False
            Dim lb As New Thread(AddressOf loadbayes)
            lb.CurrentCulture = ci
            lb.Start()
        Else
            MainWindow.CmdBox.AppendText("Process Canceled!")
        End If
    End Sub
    Public Sub loadbayes()
        If File.Exists(root_path + "temp" + path_char + "clade_b.log") Then
            File.Delete(root_path + "temp" + path_char + "clade_b.log")
        End If
        File.Create(root_path + "temp" + path_char + "clade_b.log").Close()
        bayes_gen = 0
        runbayes("exe temp" + path_char + "clade1.nex", bayes_gen)
        build_result()
        bayes_gen = -1
    End Sub
    Public Sub build_result()
        Dim burnin, char_num, node_num As Integer
        Dim sr As StreamReader
        Dim line As String = ""
        sr = New StreamReader(root_path + "temp" + path_char + "clade1.nex")
        Do
            line = sr.ReadLine
        Loop Until line.StartsWith("[")
        sr.Close()
        line = line.Replace("[", "").Replace("]", "")
        burnin = CInt(line.Split(New Char() {","c})(0).Split(New Char() {"="c})(1))
        char_num = CInt(line.Split(New Char() {","c})(1).Split(New Char() {"="c})(1))
        Tree_Node_Num = CInt(line.Split(New Char() {","c})(2).Split(New Char() {"="c})(1))
        node_num = Select_Node_Num
        read_result("clade1.nex.run1.p", burnin, char_num, node_num)
        read_result("clade1.nex.run2.p", burnin, char_num, node_num)
        build_final(char_num, Tree_Node_Num)
    End Sub
    Public Sub read_result(ByVal file As String, ByVal burnin As Integer, ByVal char_num As Integer, ByVal node_num As Integer)
        Dim P_result(,,) As Single
        Dim withG As Integer = 0
        ReDim P_result(char_num, 1, node_num)
        Dim sr As StreamReader
        Dim line_count(,,) As Integer
        ReDim line_count(char_num, 1, node_num)
        sr = New StreamReader(root_path + "temp" + path_char + file)
        Dim line As String = ""
        line = sr.ReadLine()
        line = sr.ReadLine()
        If line.Split(New Char() {"	"c})(5).ToLower = "alpha" Then
            withG = 1
        End If
        For i As Integer = 1 To burnin + 1
            sr.ReadLine()
        Next
        line = sr.ReadLine()
        Do
            Dim P_char() As String = line.Split(New Char() {"	"c})
            bayes_gen = CInt(P_char(0))
            For i As Integer = 1 To char_num
                For j As Integer = 1 To node_num
                    If IsNumeric(P_char((i - 1) * 2 + 5 + withG + (j - 1) * char_num * 2)) Then
                        P_result(i, 0, j) += Val(P_char((i - 1) * 2 + 5 + withG + (j - 1) * char_num * 2))
                        line_count(i, 0, j) += 1
                    End If
                    If IsNumeric(P_char((i - 1) * 2 + 6 + withG + (j - 1) * char_num * 2)) Then
                        P_result(i, 1, j) += Val(P_char((i - 1) * 2 + 6 + withG + (j - 1) * char_num * 2))
                        line_count(i, 1, j) += 1
                    End If
                Next
            Next
            line = sr.ReadLine()

        Loop Until line = ""
        sr.Close()
        Dim sw As StreamWriter
        sw = New StreamWriter(root_path + "temp" + path_char + "clade_b.log", True)
            For j As Integer = 1 To Tree_Node_Num
                Dim temp_index As Integer = Array.IndexOf(Select_Node_list, j.ToString)
                If temp_index > 0 Then
                    sw.Write("clade" + j.ToString + file.Replace("clade1.nex", "") + " =")
                    For i As Integer = 1 To char_num
                        sw.Write("	" + (P_result(i, 0, temp_index) / line_count(i, 0, temp_index)).ToString("F6") + "	" + (P_result(i, 1, temp_index) / line_count(i, 1, temp_index)).ToString("F6"))
                    Next
                Else
                    sw.Write("clade" + j.ToString + file.Replace("clade1.nex", "") + " =")
                    For i As Integer = 1 To char_num
                        sw.Write("	1	0")
                    Next
                End If
                sw.WriteLine("")
            Next
      
        sw.WriteLine("------------------")
        sw.Close()
    End Sub
    Public Sub build_final(ByVal char_num As Integer, ByVal node_num As Integer)
        Dim run1(,) As Single
        Dim run2(,) As Single
        ReDim run1(node_num, char_num * 2)
        ReDim run2(node_num, char_num * 2)
        Dim sr As StreamReader
        sr = New StreamReader(root_path + "temp" + path_char + "clade_b.log", True)
        Dim line As String
        line = sr.ReadLine
        Dim l As Integer = 1
        Do
            Dim plist() As String = line.Split(New Char() {"	"c})
            For i As Integer = 1 To UBound(plist)
                run1(l, i) = Val(plist(i))
            Next
            l += 1
            line = sr.ReadLine
        Loop Until line.StartsWith("-")

        line = sr.ReadLine
        l = 1
        Do
            Dim plist() As String = line.Split(New Char() {"	"c})
            For i As Integer = 1 To UBound(plist)
                run2(l, i) = Val(plist(i))
            Next
            l += 1
            line = sr.ReadLine
        Loop Until line.StartsWith("-")
        sr.Close()

        Dim sw As StreamWriter
        sw = New StreamWriter(root_path + "temp" + path_char + "clade_b.log", True)

        For j As Integer = 1 To Tree_Node_Num

            sw.Write("clade" + j.ToString + " =")

            For i As Integer = 1 To char_num * 2
                sw.Write("	" + ((run1(j, i) + run2(j, i)) / 2).ToString("F6"))
            Next
            sw.WriteLine("")
        Next
        sw.WriteLine("------------------")
        sw.Close()
    End Sub
    Private Sub BAYES_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Public Function Write_Bayes_nex(ByVal nex_name As String, ByVal command_num As Integer) As Integer
        Dim sr As New StreamWriter(root_path + "temp" + path_char + nex_name, False)
        sr.WriteLine("#NEXUS")
        sr.WriteLine("Begin data;")
        sr.WriteLine("Dimensions ntax=" + (taxon_num + 2).ToString + " nchar=" + RangeStr.Length.ToString + ";")
        sr.WriteLine("Format datatype=restriction;")
        sr.WriteLine("Matrix")
        For i As Integer = 1 To dtView.Count
            sr.WriteLine("TID" + MainWindow.DataGridView1.Rows(i - 1).Cells(0).Value.ToString + "    " + Distributiton_to_Binary(MainWindow.DataGridView1.Rows(i - 1).Cells(2).Value.ToString, RangeStr.Length))
        Next
        Select Case ComboBox3.Text
            Case "Null"
                sr.WriteLine("OG1" + "    " + Distributiton_to_Binary("", RangeStr.Length))
                sr.WriteLine("OG2" + "    " + Distributiton_to_Binary("", RangeStr.Length))
            Case "Wide"
                sr.WriteLine("OG1" + "    " + Distributiton_to_Binary(RangeStr, RangeStr.Length))
                sr.WriteLine("OG2" + "    " + Distributiton_to_Binary(RangeStr, RangeStr.Length))
            Case Else
                If TextBox9.Text <> "" Then
                    TextBox9.Text = TextBox9.Text.ToUpper
                    For Each i As Char In TextBox9.Text
                        If RangeStr.Contains(i.ToString) = False Then
                            sr.Close()
                            MsgBox("Error of custom distributiton!")
                            Return -1
                        End If
                    Next
                End If
                sr.WriteLine("OG1" + "    " + Distributiton_to_Binary(TextBox9.Text, RangeStr.Length))
                sr.WriteLine("OG2" + "    " + Distributiton_to_Binary(TextBox9.Text, RangeStr.Length))
        End Select

        sr.WriteLine(";")
        sr.WriteLine("End;")
        sr.WriteLine("begin mrbayes;")

        For i As Integer = 0 To command_num
            If commandlines(i) <> "" Then
                If commandlines(i).EndsWith("]") Then
                    sr.WriteLine(commandlines(i))
                Else
                    sr.WriteLine(commandlines(i) + ";")
                End If
            End If
        Next
        sr.WriteLine("End;")
        sr.Close()
        Return 0
    End Function

    Private Sub BAYES_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        ComboBox1.SelectedIndex = 0
        ComboBox2.SelectedIndex = 0
        ComboBox3.SelectedIndex = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
    End Sub

    Private Sub ComboBox3_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox3.SelectedIndexChanged
        If ComboBox3.SelectedIndex = 2 Then
            TextBox9.Enabled = True
        Else
            TextBox9.Enabled = False
        End If
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedIndex = 1 Then
            TextBox5.Enabled = True
            TextBox6.Enabled = True
        Else
            TextBox5.Enabled = False
            TextBox6.Enabled = False
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox2.SelectedIndexChanged
        If ComboBox2.SelectedIndex = 1 Then
            TextBox7.Enabled = True
            TextBox8.Enabled = True
        Else
            TextBox7.Enabled = False
            TextBox8.Enabled = False
        End If
    End Sub

    Private Sub Bayesian_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then
            NumericUpDown1.Maximum = RangeStr.Length
            NumericUpDown1.Value = min(4, RangeStr.Length)
            If DataGridView2.ColumnCount = 0 Then
                DataGridView2.DataSource = nodeView
                DataGridView2.Columns(0).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(1).SortMode = DataGridViewColumnSortMode.NotSortable
                DataGridView2.Columns(0).Width = 20
                DataGridView2.Columns(1).Width = 20
                DataGridView2.Columns(0).ReadOnly = True
                DataGridView2.Columns(1).ReadOnly = True
                DataGridView2.RowHeadersVisible = False
                Dim select_node As New DataGridViewCheckBoxColumn
                select_node.HeaderText = "Select"
                DataGridView2.Columns.Add(select_node)
                DataGridView2.Columns(0).Width = 75
                DataGridView2.Columns(1).Width = 75
                DataGridView2.Columns(2).Width = 50
                For i As Integer = 1 To DataGridView2.Rows.Count
                    DataGridView2.Rows(i - 1).Cells(2).Value = True
                Next
            End If
        End If
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        For i As Integer = 1 To DataGridView2.Rows.Count
            DataGridView2.Rows(i - 1).Cells(2).Value = True
        Next
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For i As Integer = 1 To DataGridView2.Rows.Count
            DataGridView2.Rows(i - 1).Cells(2).Value = False
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For i As Integer = 1 To DataGridView2.Rows.Count
            If Val(DataGridView2.Rows(i - 1).Cells(0).Value.ToString.Split(New Char() {":"c})(1)) * 100 < NumericUpDown3.Value Then
                DataGridView2.Rows(i - 1).Cells(2).Value = False
            Else
                DataGridView2.Rows(i - 1).Cells(2).Value = True
            End If
        Next
    End Sub
End Class