Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices

Public Class Tool_DPP
    <DllImport("DPPDLL.dll")> Public Shared Function rundppdiv(ByVal a As String, ByRef genno As Double) As Integer
    End Function
    Dim TaxonName() As String
    Dim TaxonTime(,) As String
    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        If TextBox2.Text <> "" Then
            If ComboBox1.SelectedIndex > 0 Then
                Dim temp_taxon() As String = ComboBox1.Text.Split("|")(1).Replace("(", "").Replace(")", "").Split(",")
                Dim clade_taxon(1) As Integer
                clade_taxon(0) = CInt(temp_taxon(0))
                clade_taxon(1) = CInt(temp_taxon(UBound(temp_taxon)))
                DataGridView5.Rows.Add()
                DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(0).Value = clade_taxon(0).ToString + "." + TaxonName(clade_taxon(0) - 1)
                DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(1).Value = clade_taxon(1).ToString + "." + TaxonName(clade_taxon(1) - 1)
                DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(2).Value = TextBox2.Text
            ElseIf ComboBox1.SelectedIndex = 0 Then
                DataGridView5.Rows.Add()
                DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(0).Value = "root"
                DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(2).Value = TextBox2.Text
            Else
                MsgBox("Please select the clade!")
            End If
        Else
            MsgBox("Please give the age of fossil!")
        End If

    End Sub
    Private Sub DPP_Config_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            ReDim TaxonName(taxon_num - 1)
            For i As Integer = 0 To taxon_num - 1
                TaxonName(i) = MainWindow.DataGridView1.Rows(i).Cells(1).Value
            Next
            If DataGridView5.Rows.Count <= 0 Then
                ComboBox2.SelectedIndex = 0
                ComboBox3.SelectedIndex = 0
                DataGridView5.Columns.Clear()
                DataGridView5.Rows.Clear()
                Dim Column_Taxon1 As New DataGridViewTextBoxColumn
                Dim Column_Taxon2 As New DataGridViewTextBoxColumn
                Dim Column_Dis As New DataGridViewTextBoxColumn
                Column_Taxon1.HeaderText = "Taxon1"
                Column_Taxon2.HeaderText = "Taxon2"
                Column_Dis.HeaderText = "Time"
                DataGridView5.Columns.Add(Column_Taxon1)
                DataGridView5.Columns.Add(Column_Taxon2)
                DataGridView5.Columns.Add(Column_Dis)

                DataGridView5.Columns(0).ReadOnly = True
                DataGridView5.Columns(1).ReadOnly = True
                DataGridView5.Columns(2).ReadOnly = False

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
            End If
            If File.Exists(root_path + path_char + "temp\dppresult.ant.tre") Then
                File.Delete(root_path + path_char + "temp\dppresult.ant.tre")
            End If
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

    Private Sub DPP_Config_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        If TargetOS = "macos" Then
            Me.TopMost = False
        End If
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Dim wr As New StreamWriter(root_path + "temp" + path_char + "dppdiv.tre")
        wr.Write(tree_show_with_value + vbCrLf)
        wr.Close()
        Dim wr1 As New StreamWriter(root_path + "temp" + path_char + "dppdiv.cal")

        If ComboBox2.SelectedIndex <= 3 Then
            Dim count As Integer = 0
            Dim cal_file As String = ""
            For i As Integer = 0 To DataGridView5.Rows.Count - 1
                Dim has_muti As Boolean = False
                Dim min_age As Single = CSng(DataGridView5.Rows(i).Cells(2).Value)
                Dim max_age As String = CSng(DataGridView5.Rows(i).Cells(2).Value)
                For j As Integer = 0 To DataGridView5.Rows.Count - 1
                    If DataGridView5.Rows(i).Cells(0).Value = DataGridView5.Rows(j).Cells(0).Value Then
                        If DataGridView5.Rows(i).Cells(1).Value = DataGridView5.Rows(j).Cells(1).Value Then
                            If j > i Then
                                has_muti = True
                                If CSng(DataGridView5.Rows(j).Cells(2).Value) < min_age Then
                                    min_age = DataGridView5.Rows(j).Cells(2).Value
                                End If
                                If CSng(DataGridView5.Rows(j).Cells(2).Value) > max_age Then
                                    max_age = DataGridView5.Rows(j).Cells(2).Value
                                End If
                            ElseIf j < i Then
                                GoTo next01
                            End If
                        End If
                    End If
                Next
                If has_muti Then
                    If DataGridView5.Rows(i).Cells(0).Value.ToString <> "root" Then
                        cal_file += "-U	" + DataGridView5.Rows(i).Cells(0).Value.ToString.Split(".")(0) + "	" + DataGridView5.Rows(i).Cells(1).Value.ToString.Split(".")(0) + "	" + min_age.ToString + "	" + max_age.ToString + vbCrLf
                    Else
                        cal_file += "-U	root	" + min_age.ToString + "	" + max_age.ToString + vbCrLf
                    End If
                Else
                    If DataGridView5.Rows(i).Cells(0).Value.ToString <> "root" Then
                        cal_file += "-E	" + DataGridView5.Rows(i).Cells(0).Value.ToString.Split(".")(0) + "	" + DataGridView5.Rows(i).Cells(1).Value.ToString.Split(".")(0) + "	" + min_age.ToString + "	" + max_age.ToString + vbCrLf
                    Else
                        cal_file += "-E	root	" + min_age.ToString + vbCrLf
                    End If
                End If
                count += 1
next01:     Next
            wr1.WriteLine(count.ToString)
            wr1.WriteLine(cal_file)
        Else
            wr1.WriteLine(DataGridView5.Rows.Count)
            For i As Integer = 0 To DataGridView5.Rows.Count - 1

                If DataGridView5.Rows(i).Cells(0).Value.ToString <> "root" Then
                    wr1.WriteLine("-t	" + DataGridView5.Rows(i).Cells(0).Value.ToString.Split(".")(0) + "	" + DataGridView5.Rows(i).Cells(1).Value.ToString.Split(".")(0) + "	" + DataGridView5.Rows(i).Cells(2).Value.ToString)
                Else
                    wr1.WriteLine("-t	root	" + DataGridView5.Rows(i).Cells(2).Value.ToString)
                End If
            Next
        End If

        wr1.Close()
        MainWindow.ProgressBar1.Maximum = 1000
        MainWindow.DPP_Timer.Enabled = True
        Me.Hide()
        StartTreeView = False
        CheckForIllegalCrossThreadCalls = False
        Directory.SetCurrentDirectory(root_path)
        Dim lb As New Thread(AddressOf loaddpp)
        lb.CurrentCulture = ci
        lb.Start()

    End Sub
    Public Sub format_tree()
        If File.Exists(root_path + path_char + "temp\dppresult.ant.tre") Then
            Dim sr As New StreamReader(root_path + path_char + "temp\dppresult.ant.tre")
            Dim sw As New StreamWriter(root_path + path_char + "temp\dppresult.format.tre")
            Dim line As String = sr.ReadLine
            Do
                If line <> "begin trees;" Then
                    sw.WriteLine(line)
                Else
                    sw.WriteLine(line)
                    sw.WriteLine("   Translate")
                    For i As Integer = 1 To dtView.Count - 1
                        sw.WriteLine(dtView.Item(i - 1).Item(0).ToString + " " + dtView.Item(i - 1).Item(1).ToString + ",")
                    Next
                    sw.WriteLine(dtView.Item(dtView.Count - 1).Item(0).ToString + " " + dtView.Item(dtView.Count - 1).Item(1).ToString)
                    sw.WriteLine("		;")
                End If
                line = sr.ReadLine
            Loop Until line Is Nothing
            sw.Close()
            sr.Close()
        End If

    End Sub
    Public Sub loaddpp()
        dpp_gen = 0
        rundppdiv(makecmd(), dpp_gen)
        format_tree()
        dpp_gen = -1
        CheckForIllegalCrossThreadCalls = True
    End Sub
    Public Function makecmd() As String
        Dim command_line As String = ""
        command_line = "-in temp\dppdiv.dat -out temp\dppresult -tre temp\dppdiv.tre -cal temp\dppdiv.cal"
        command_line += " -pm " + TextBox1.Text
        command_line += " -ra " + TextBox4.Text
        command_line += " -rb " + TextBox5.Text
        command_line += " -hsh " + TextBox6.Text
        command_line += " -n " + TextBox3.Text
        command_line += " -sf " + TextBox8.Text
        Select Case ComboBox3.Text
            Case "strict clock"
                command_line += " -clok"
            Case "DPP"
            Case Else
                command_line += " -urg"
        End Select
        If ComboBox2.SelectedIndex <= 3 Then
            command_line += " -npr " + (ComboBox2.SelectedIndex + 1).ToString
        Else
            command_line += " -npr " + (ComboBox2.SelectedIndex + 2).ToString
        End If

        If CheckBox1.Checked Then
            command_line += " -ubl"
        End If
        If CheckBox2.Checked Then
            command_line += " -snm"
        End If
        If CheckBox3.Checked Then
            command_line += " -soft"
        End If
        Return command_line
    End Function

    Private Sub TextBox1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.GotFocus
        TextBox10.Text = "Prior mean of number of rate categories"
    End Sub
    Private Sub TextBox4_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox4.GotFocus
        TextBox10.Text = "Shape for gamma of rates"
    End Sub
    Private Sub TextBox5_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox5.GotFocus
        TextBox10.Text = "Scale for gamma of rates"
    End Sub
    Private Sub TextBox6_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox6.GotFocus
        TextBox10.Text = "Shape for gamma hyper prior on alpha concentration parameter"
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim opendialog As New SaveFileDialog
        opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".csv"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            Dim dw As New StreamWriter(opendialog.FileName, False)
            For i As Integer = 0 To DataGridView5.Rows.Count - 1
                dw.WriteLine(DataGridView5.Rows(i).Cells(0).Value.ToString + "," + DataGridView5.Rows(i).Cells(1).Value.ToString + "," + DataGridView5.Rows(i).Cells(2).Value.ToString)
            Next
            dw.Close()
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "CSV File (*.csv)|*.csv;*.CSV|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.Multiselect = False
        opendialog.DefaultExt = ".csv"
        opendialog.CheckFileExists = True
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        Try
            If resultdialog = DialogResult.OK Then
                DataGridView5.Rows.Clear()
                Dim dr As New StreamReader(opendialog.FileName)
                Dim line As String
                Dim temp_t() As String
                line = dr.ReadLine
                If line <> "" Then
                    Do
                        temp_t = line.Split(New Char() {","c})
                        DataGridView5.Rows.Add()
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(0).Value = temp_t(0)
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(1).Value = temp_t(1)
                        DataGridView5.Rows(DataGridView5.Rows.Count - 1).Cells(2).Value = temp_t(2)
                        line = dr.ReadLine
                    Loop Until line Is Nothing
                End If
                dr.Close()

            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Sub
End Class