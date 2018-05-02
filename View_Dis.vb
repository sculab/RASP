Imports System.IO
Imports System.Threading
Imports System.Math
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class View_Dis

    Private Sub LoadResultToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadResultToolStripMenuItem.Click
        Dim opendialog As New OpenFileDialog
        opendialog.Filter = "BayesTraits Log File (*.log.txt)|*.log.txt|Beast Log File (*.log)|*.log|mrBayes Log File (*.p)|*.p|ALL Files(*.*)|*.*"
        opendialog.FileName = ""
        opendialog.DefaultExt = ".log.txt"
        opendialog.CheckFileExists = False
        opendialog.CheckPathExists = True
        Dim resultdialog As DialogResult = opendialog.ShowDialog()
        If resultdialog = DialogResult.OK Then
            If File.Exists(opendialog.FileName) Then
                If opendialog.FileName.ToLower.EndsWith(".log.txt") Then
                    TextBox2.Text = "Root P"
                ElseIf opendialog.FileName.ToLower.EndsWith(".log") Then
                    TextBox2.Text = "posterior"
                ElseIf opendialog.FileName.ToLower.EndsWith(".p") Then
                    TextBox2.Text = "LnL"
                Else
                    TextBox2.Text = InputBox("Input one of the columns' header:", "Header")
                End If
                If TextBox2.Text = "" Then
                    TextBox2.Text = "No valid header"
                End If
                read_traits_file(opendialog.FileName)
                traits_view_file = opendialog.FileName
            End If
        End If
    End Sub
    Public Sub read_traits_file(ByVal infile As String)
        Dim sw As New StreamReader(infile)
        Dim line As String = ""
        Do
            line = sw.ReadLine
            If line Is Nothing Then
                MsgBox("Could not load this file!")
                Exit Sub
            End If
        Loop Until line.Contains(TextBox2.Text)
        Dim head_str() As String = line.Split("	")
        ListBox1.Items.Clear()
        For Each i As String In head_str
            If i <> "" Then
                ListBox1.Items.Add(i)
            End If
        Next
        sw.Close()

    End Sub
    Dim traits_view_file As String
    Dim prob_dist() As Integer
    Dim prob_para(6) As Single
    Dim split_count As Integer = 20
    Dim value_array() As Single
    Dim curren_select As Integer = -1
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedIndex > 0 Then
            cal_column(ListBox1.SelectedIndex)
            If prob_para(3) > 0 Then
                TextBox1.Text = "Total:" & prob_para(3).ToString & vbCrLf _
           & "Mean:" & prob_para(4).ToString("F4") & vbCrLf _
           & "Var:" & (prob_para(5) * prob_para(5)).ToString("F4") & vbCrLf _
           & "Median:" & prob_para(6).ToString("F4") & vbCrLf _
           & "SD:" & prob_para(5).ToString("F4") & vbCrLf _
           & "Max:" & prob_para(1).ToString("F4") & vbCrLf _
           & "Min:" & prob_para(0).ToString("F4")
                PictureBox1.Refresh()
            End If
        End If
    End Sub
    Public Sub cal_column(ByVal select_index As Integer)
        If curren_select <> select_index Then
            CheckBox4.Checked = False
            curren_select = select_index
        End If
        Dim sw As New StreamReader(traits_view_file)
        Dim line As String = ""
        Do
            line = sw.ReadLine
            If line Is Nothing Then
                MsgBox("Could not load this file!")
                Exit Sub
            End If
        Loop Until line.Contains(TextBox2.Text)
        line = sw.ReadLine
        prob_para(0) = 1.0E+16 '最小值
        prob_para(1) = -1.0E+16 '最大值
        prob_para(2) = 0 '总和
        prob_para(3) = 0 '总数量
        prob_para(4) = 0 '平均
        prob_para(5) = 0 '标准差
        prob_para(6) = 0 '中值
        ReDim value_array(0)
        value_array(0) = -12345
        Do
            If line <> "" Then
                Dim temp_array() As String = line.Split("	")
                If temp_array.Length > select_index Then
                    Dim temp As String = temp_array(select_index)
                    If IsNumeric(temp) Then
                        If CheckBox4.Checked Then
                            If CSng(temp) <= CSng(TextBox3.Text) And CSng(temp) >= CSng(TextBox4.Text) Then
                                Dim temp_num As Single = CSng(temp)
                                If temp_num < prob_para(0) Then
                                    prob_para(0) = temp_num
                                End If
                                If temp_num > prob_para(1) Then
                                    prob_para(1) = CSng(temp)
                                End If
                                prob_para(2) += temp_num
                                prob_para(3) += 1
                                If value_array(0) <> -12345 Then
                                    ReDim Preserve value_array(UBound(value_array) + 1)
                                End If
                                value_array(UBound(value_array)) = temp_num
                            End If
                        Else
                            Dim temp_num As Single = CSng(temp)
                            If temp_num < prob_para(0) Then
                                prob_para(0) = temp_num
                            End If
                            If temp_num > prob_para(1) Then
                                prob_para(1) = CSng(temp)
                            End If
                            prob_para(2) += temp_num
                            prob_para(3) += 1
                            If value_array(0) <> -12345 Then
                                ReDim Preserve value_array(UBound(value_array) + 1)
                            End If
                            value_array(UBound(value_array)) = temp_num
                        End If

                    End If
                End If
            End If
            line = sw.ReadLine
        Loop Until line Is Nothing
        If value_array(0) <> -12345 Then
            prob_para(5) = cal_sd(value_array)
        End If
        Array.Sort(value_array)
        If (value_array.Length Mod 2) = 1 Then
            prob_para(6) = value_array((value_array.Length - 1) / 2)
        Else
            prob_para(6) = (value_array(value_array.Length / 2) + value_array(value_array.Length / 2 - 1)) / 2
        End If
        sw.Close()
        If prob_para(3) > 0 Then
            prob_para(4) = prob_para(2) / prob_para(3)
            Dim sw1 As New StreamReader(traits_view_file)
            Do
                line = sw1.ReadLine
            Loop Until line.Contains(TextBox2.Text)
            line = sw1.ReadLine
            ReDim prob_dist(split_count)
            For i As Integer = 0 To split_count
                prob_dist(i) = 0
            Next

            Do
                Dim temp_array() As String = line.Split("	")
                If temp_array.Length > select_index Then
                    Dim temp As String = temp_array(select_index)
                    If IsNumeric(temp) Then
                        If CheckBox4.Checked Then
                            If CSng(temp) <= CSng(TextBox3.Text) And CSng(temp) >= CSng(TextBox4.Text) Then
                                Dim temp_num As Single = CSng(temp)

                                Dim dis_count As Integer
                                If prob_para(1) > prob_para(0) Then
                                    dis_count = Math.Truncate((temp_num - prob_para(0)) / (prob_para(1) - prob_para(0)) * split_count)
                                Else
                                    dis_count = split_count - 1
                                End If
                                If dis_count = split_count Then
                                    dis_count -= 1
                                End If
                                prob_dist(dis_count + 1) += 1
                            End If
                        Else
                            Dim temp_num As Single = CSng(temp)

                            Dim dis_count As Integer
                            If prob_para(1) > prob_para(0) Then
                                dis_count = Math.Truncate((temp_num - prob_para(0)) / (prob_para(1) - prob_para(0)) * split_count)
                            Else
                                dis_count = split_count - 1
                            End If
                            If dis_count = split_count Then
                                dis_count -= 1
                            End If
                            prob_dist(dis_count + 1) += 1
                        End If

                    End If
                End If
                line = sw1.ReadLine
            Loop Until line Is Nothing
            sw1.Close()

            If CheckBox4.Checked = False Then
                TextBox3.Text = prob_para(1)
                TextBox4.Text = prob_para(0)
            End If

        End If
    End Sub
    Dim savingpic As Boolean = False
    Public Sub drawfig1(ByVal drawg As Object)
        If ListBox1.SelectedIndex > 0 And prob_para(3) > 0 Then
            If PictureBox1.Height > 100 And PictureBox1.Width > 100 Then
                drawg.SmoothingMode = SmoothingMode.AntiAlias
                drawg.FillRectangle(Brushes.White, 0, 0, PictureBox1.Width, PictureBox1.Height)
                Dim newfont As New Font("Times New Roman", NumericUpDown1.Value, FontStyle.Regular, GraphicsUnit.Point)
                drawg.DrawLine(Pens.Black, 40, 0, 40, PictureBox1.Height)
                drawg.DrawLine(Pens.Black, 0, PictureBox1.Height - 40, PictureBox1.Width, PictureBox1.Height - 40)
                For i As Integer = 0 To CInt(NumericUpDown4.Value / 5)
                    drawg.DrawString(((CInt(NumericUpDown4.Value / 5) - i) / 20).ToString("F2"), newfont, Brushes.Black, 0, CInt(40 + (PictureBox1.Height - 80) * i / CInt(NumericUpDown4.Value / 5)))
                    drawg.DrawLine(Pens.Black, 35, CInt(40 + (PictureBox1.Height - 80) * i / CInt(NumericUpDown4.Value / 5)), 40, CInt(40 + (PictureBox1.Height - 80) * i / CInt(NumericUpDown4.Value / 5)))
                Next
                For i As Integer = 0 To split_count
                    drawg.DrawString((i / split_count * (prob_para(1) - prob_para(0)) + prob_para(0)).ToString("F2"), newfont, Brushes.Black, CInt(40 + i / split_count * (PictureBox1.Width - 80)), PictureBox1.Height - 40)
                    If i < split_count Then
                        Dim r_height As Integer = CInt((prob_dist(i + 1) / prob_para(3)) / NumericUpDown4.Value * 100 * (PictureBox1.Height - 80))
                        Dim r_weight As Integer = CInt((PictureBox1.Width - 80) / split_count - NumericUpDown2.Value)
                        drawg.FillRectangle(New SolidBrush(Label4.BackColor), CInt(40 + i / split_count * (PictureBox1.Width - 80)) + NumericUpDown2.Value, PictureBox1.Height - 40 - r_height, r_weight, r_height)
                    End If
                Next
                Dim mean_x As Integer
                If prob_para(1) > prob_para(0) Then
                    mean_x = ((prob_para(4) - prob_para(0)) / (prob_para(1) - prob_para(0)) * (PictureBox1.Width - 80) + 40).ToString("F2")
                Else
                    mean_x = (PictureBox1.Width - 40).ToString("F2")
                End If

                Dim media_x As Integer
                If prob_para(1) > prob_para(0) Then
                    media_x = ((prob_para(6) - prob_para(0)) / (prob_para(1) - prob_para(0)) * (PictureBox1.Width - 80) + 40).ToString("F2")
                Else
                    media_x = (PictureBox1.Width - 40).ToString("F2")
                End If

                If CheckBox1.Checked Then
                    drawg.DrawLine(New Pen(Brushes.SteelBlue, 2), mean_x, 40, mean_x, PictureBox1.Height - 40)
                    drawg.DrawString("mean=" & prob_para(4).ToString("F4") & "±" & prob_para(5).ToString("F4"), newfont, Brushes.SteelBlue, 42, 4)
                End If
                If CheckBox2.Checked Then
                    drawg.DrawString("median=" & prob_para(6).ToString("F4"), newfont, Brushes.PaleVioletRed, 42, 24)
                    drawg.DrawLine(New Pen(Brushes.PaleVioletRed, 2), media_x, 40, media_x, PictureBox1.Height - 40)
                End If
            End If

        End If
    End Sub

    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If savingpic = False Then
            drawfig1(e.Graphics)
        End If
    End Sub

    Private Sub View_Traits_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub View_Traits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        split_count = NumericUpDown3.Value
        ListBox1_SelectedIndexChanged(sender, e)
        PictureBox1.Refresh()
    End Sub

    Private Sub PictureBox1_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles PictureBox1.Resize
        PictureBox1.Refresh()
    End Sub

    Private Sub SaveGraphicToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveGraphicToolStripMenuItem.Click
        Try
            Dim bitmap As New Bitmap(CInt(PictureBox1.Width), CInt(PictureBox1.Height))

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
                    drawfig1(ig)
                    savingpic = False
                    ig.Dispose()
                    wmf.Dispose()
                    g.ReleaseHdc()
                    g.Dispose()
                ElseIf sfd.FileName.ToLower.EndsWith(".svg") Then
                    Dim svg_gra As New SvgNet.SvgGdi.SvgGraphics
                    savingpic = True
                    drawfig1(svg_gra)
                    savingpic = False
                    Dim svg_writer As New StreamWriter(sfd.FileName, False)
                    svg_writer.Write(svg_gra.WriteSVGString)
                    svg_writer.Close()
                    svg_gra.Flush()
                Else
                    Dim TempGrap As Graphics = Graphics.FromImage(bitmap)
                    savingpic = True
                    drawfig1(TempGrap)
                    savingpic = False
                    bitmap.Save(sfd.FileName)
                End If
                MsgBox("Save Successfully!", MsgBoxStyle.Information)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label4.Click
        ColorDialog1.Color = Label4.BackColor
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Label4.BackColor = ColorDialog1.Color
            PictureBox1.Refresh()
        End If
    End Sub

    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged
        TextBox2.ReadOnly = CheckBox3.Checked Xor True
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        PictureBox1.Refresh()
    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged
        PictureBox1.Refresh()
    End Sub

    Private Sub NumericUpDown3_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles NumericUpDown3.Validated
        split_count = NumericUpDown3.Value
    End Sub

    Private Sub ExpToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub UniformMaxMinToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UniformMaxMinToolStripMenuItem.Click
        Config_Traits.TextBox2.Text = ""
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i).ToString.StartsWith("q") Then
                cal_column(i)
                Config_Traits.TextBox2.Text = Config_Traits.TextBox2.Text & "pr " & ListBox1.Items(i).ToString & " uniform " & prob_para(0).ToString & " " & prob_para(1).ToString & vbCrLf
            End If
        Next
        Config_Traits.ComboBox1.SelectedIndex = 1
        Config_Traits.ComboBox2.SelectedIndex = 0
        Config_Traits.ComboBox3.SelectedIndex = 0
        Config_Traits.ComboBox4.SelectedIndex = 0
        Config_Traits.ComboBox5.SelectedIndex = 1
        Config_Traits.ComboBox6.SelectedIndex = 0
        Config_Traits.Show()
    End Sub

    Private Sub UniformMeanToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles UniformMeanToolStripMenuItem1.Click
        Config_Traits.TextBox2.Text = ""
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i).ToString.StartsWith("q") Then
                cal_column(i)
                Config_Traits.TextBox2.Text = Config_Traits.TextBox2.Text & "pr " & ListBox1.Items(i).ToString & " uniform " & max((prob_para(4) - prob_para(5)), prob_para(0)).ToString & " " & min((prob_para(4) + prob_para(5)), prob_para(1)).ToString & vbCrLf
            End If
        Next
        Config_Traits.ComboBox1.SelectedIndex = 1
        Config_Traits.ComboBox2.SelectedIndex = 0
        Config_Traits.ComboBox3.SelectedIndex = 0
        Config_Traits.ComboBox4.SelectedIndex = 0
        Config_Traits.ComboBox5.SelectedIndex = 1
        Config_Traits.ComboBox6.SelectedIndex = 0
        Config_Traits.Show()
    End Sub

    Private Sub GammaToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GammaToolStripMenuItem1.Click
        Config_Traits.TextBox2.Text = ""
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i).ToString.StartsWith("q") Then
                cal_column(i)
                Config_Traits.TextBox2.Text = Config_Traits.TextBox2.Text & "pr " & ListBox1.Items(i).ToString & " gamma " & prob_para(4).ToString & " " & (prob_para(5) * prob_para(5)).ToString & vbCrLf
            End If
        Next
        Config_Traits.ComboBox1.SelectedIndex = 1
        Config_Traits.ComboBox2.SelectedIndex = 0
        Config_Traits.ComboBox3.SelectedIndex = 0
        Config_Traits.ComboBox4.SelectedIndex = 0
        Config_Traits.ComboBox5.SelectedIndex = 1
        Config_Traits.ComboBox6.SelectedIndex = 0
        Config_Traits.Show()
    End Sub

    Private Sub ExpontialToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExpontialToolStripMenuItem.Click
        Config_Traits.TextBox2.Text = ""
        For i As Integer = 0 To ListBox1.Items.Count - 1
            If ListBox1.Items(i).ToString.StartsWith("q") Then
                cal_column(i)
                Config_Traits.TextBox2.Text = Config_Traits.TextBox2.Text & "pr " & ListBox1.Items(i).ToString & " exp " & prob_para(4).ToString & vbCrLf
            End If
        Next
        Config_Traits.ComboBox1.SelectedIndex = 1
        Config_Traits.ComboBox2.SelectedIndex = 0
        Config_Traits.ComboBox3.SelectedIndex = 0
        Config_Traits.ComboBox4.SelectedIndex = 0
        Config_Traits.ComboBox5.SelectedIndex = 1
        Config_Traits.ComboBox6.SelectedIndex = 0
        Config_Traits.Show()
    End Sub
End Class