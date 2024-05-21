Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices
Public Class Config_BBM

    <DllImport("BAYESDLL.dll")> Public Shared Function runbayes(ByVal nexpath As String, ByRef genno As Integer) As Integer
    End Function
    Dim commandlines(5) As String
    Dim cmd_lines As Integer
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
        Loop Until File.Exists(root_path + "temp" + path_char + "Clade" + k.ToString + ".r") = False
        Dim usertree As Boolean = False
        'If final_tree.Replace(",", "").Length = final_tree.Replace("(", "").Length Then
        '    usertree = True
        'End If
        Dim node_count As Integer = final_tree.Length - final_tree.Replace("(", "").Length
        bayesIsrun = True
        MainWindow.CmdBox.AppendText(vbCrLf + "**************************" + vbCrLf)
        MainWindow.CmdBox.AppendText("*Bayesian MCMC Analysis*" + vbCrLf)
        MainWindow.CmdBox.AppendText("**************************" + vbCrLf)
        MainWindow.CmdBox.AppendText("Process begin at " + Date.Now.ToString + vbCrLf)

        Read_Poly_Node(final_tree.Replace(";", ""))

        'If node_count < 512 Then
        commandlines(0) = "outgroup " + (taxon_num + 2).ToString
        Select Case ComboBox1.Text
            Case "Fixed (JC)" 'JC model
                commandlines(1) = "lset nst=1 rates=equal"
            Case "Estimated (F81)" 'F81 model
                commandlines(1) = "lset nst=1 rates=equal"
                commandlines(2) = "prset statefreqpr=dirichlet(" + TextBox5.Text + "," + TextBox6.Text + ")"
        End Select

        Select Case ComboBox2.Text
            Case "Equal"
            Case "Gamma (+G)" '+G
                commandlines(3) = "lset nst=1 rates=gamma"
                commandlines(4) = "prset Shapepr=Uniform(" + TextBox7.Text + "," + TextBox8.Text + ")"
        End Select
        Select Case ComboBox1.Text + ComboBox2.Text
            Case "Fixed (JC)" + "Equal"
                MainWindow.CmdBox.AppendText("Using Model: JC" + vbCrLf)
            Case "Fixed (JC)" + "Gamma (+G)"
                MainWindow.CmdBox.AppendText("Using Model: JC+G" + vbCrLf)
            Case "Estimated (F81)" + "Equal"
                MainWindow.CmdBox.AppendText("Using Model: F81" + vbCrLf)
            Case Else
                MainWindow.CmdBox.AppendText("Using Model: F81+G" + vbCrLf)
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
        cmd_lines = 5 + nodeView.Count
        ReDim Preserve commandlines(cmd_lines + 8)
        commandlines(cmd_lines) = "prset topologypr=constraints("
        For i As Integer = 1 To nodeView.Count
            If DataGridView2.Rows(i - 1).Cells(2).FormattedValue.ToString = "True" Then
                commandlines(4 + i) = "constraint c" + i.ToString + " -1 = " + Poly_Node(i - 1, 3).Replace(",", " ")
                commandlines(cmd_lines) += "c" + i.ToString + ","
            End If
        Next

        commandlines(cmd_lines) = (commandlines(cmd_lines) + ")").Replace(",)", ")")

        commandlines(cmd_lines + 1) = "lset coding=variable"
        commandlines(cmd_lines + 2) = "set autoclose=yes nowarn=yes"
        commandlines(cmd_lines + 4) = "report ancstates=yes"
        If usertree Then
            commandlines(cmd_lines + 5) = "usertree = (" + final_tree.Replace(";", "") + "," + (taxon_num + 1).ToString + "," + (taxon_num + 2).ToString + ")"
            commandlines(cmd_lines + 6) = "mcmc" + " startingtree=user Ordertaxa=Yes printfreq=1000 diagnfreq=1000 Samplefreq=" + TextBox1.Text + " ngen=" + TextBox3.Text + " nchains=" + NumericUpDown2.Text + " Temp=" + TextBox2.Text
        Else
            commandlines(cmd_lines + 5) = ""
            commandlines(cmd_lines + 6) = "mcmc" + " printfreq=1000 diagnfreq=1000 Ordertaxa=Yes Samplefreq=" + TextBox1.Text + " ngen=" + TextBox3.Text + " nchains=" + NumericUpDown2.Text + " Temp=" + TextBox2.Text
        End If
        commandlines(cmd_lines + 7) = "[burnin=" + TextBox4.Text + ",taxonnum=" + RangeStr.Length.ToString + ",node_num=" + node_count.ToString + "]"
        If Write_Bayes_nex("clade1.nex", cmd_lines + 8) = 0 Then
            show_pie = "bayes"
            current_dir = Directory.GetCurrentDirectory
            Directory.SetCurrentDirectory(root_path)
            MainWindow.CmdBox.AppendText("Using command: " + commandlines(cmd_lines + 6) + vbCrLf)
            MainWindow.ProgressBar1.Maximum = CInt(TextBox3.Text)
            MainWindow.Bayes_Timer.Enabled = True
            Me.Hide()
            StartTreeView = False
            If CheckBox2.Checked Then
                If File.Exists(root_path + "temp" + path_char + "clade_b.log") Then
                    File.Delete(root_path + "temp" + path_char + "clade_b.log")
                End If
                File.Create(root_path + "temp" + path_char + "clade_b.log").Close()
                Dim dw2 As New StreamWriter(root_path + "temp\run_bbm.bat", False)
                dw2.WriteLine("""" + root_path + "Plug-ins\mb.3.2.7-win32" + """" + " clade1.nex")
                dw2.WriteLine("cls>endbbm")
                dw2.Close()
                bayes_gen = 0
                current_dir = Directory.GetCurrentDirectory
                Directory.SetCurrentDirectory(root_path + "temp\")
                Dim startInfo As New ProcessStartInfo
                startInfo.FileName = "run_bbm.bat"
                startInfo.WorkingDirectory = root_path + "temp"
                startInfo.UseShellExecute = False
                startInfo.CreateNoWindow = False
                Process.Start(startInfo)
                Directory.SetCurrentDirectory(current_dir)
            Else
                Dim lb As New Thread(AddressOf loadbayes)
                lb.CurrentCulture = ci
                lb.Start()
            End If
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

    Private Sub BAYES_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Hide
    End Sub

    Public Function Write_Bayes_nex(ByVal nex_name As String, ByVal command_num As Integer) As Integer
        Dim sr As New StreamWriter(root_path + "temp" + path_char + nex_name, False)
        sr.WriteLine("#NEXUS")
        sr.WriteLine("Begin data;")
        If CheckBox2.Checked Then
            sr.WriteLine("Dimensions ntax=" + (taxon_num + 3).ToString + " nchar=" + RangeStr.Length.ToString + ";")
        Else
            sr.WriteLine("Dimensions ntax=" + (taxon_num + 2).ToString + " nchar=" + RangeStr.Length.ToString + ";")
        End If
        sr.WriteLine("Format datatype=restriction;")
        sr.WriteLine("Matrix")
        For i As Integer = 1 To dtView.Count
            sr.WriteLine("TID" + dtView.Item(i - 1).Item(0).ToString + "    " + Distributiton_to_Binary(dtView.Item(i - 1).Item(state_index).ToString, RangeStr.Length))
        Next
        If CheckBox2.Checked Then
            Select Case ComboBox3.Text
                Case "Null"
                    sr.WriteLine("OG0" + "    " + Distributiton_to_Binary("", RangeStr.Length))
                Case "Wide"
                    sr.WriteLine("OG0" + "    " + Distributiton_to_Binary(RangeStr, RangeStr.Length))
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
                    sr.WriteLine("OG0" + "    " + Distributiton_to_Binary(TextBox9.Text, RangeStr.Length))
            End Select
        End If
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
        If taxon_num > 256 Then
            CheckBox2.Checked = True
        End If
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

    Private Sub Bayesian_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
        If Visible Then
            NumericUpDown1.Maximum = RangeStr.Length
            NumericUpDown1.Value = Math.Min(4, RangeStr.Length)
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
                For i = 1 To DataGridView2.Rows.Count
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

    Private Sub BayesTimer_Tick(sender As Object, e As EventArgs) Handles BayesTimer.Tick

    End Sub
End Class