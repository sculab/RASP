Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class View_BBM
    Dim runfile As String = ""
    Private Sub Trace_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        Me.MinimizeBox = enableMin
        ComboBox1.SelectedIndex = 0
    End Sub
    Public Sub loadfile()
        ListBox1.Items.Clear()
        If File.Exists(runfile) = False Then
            Exit Sub
        End If
        Dim pfile1 As New StreamReader(runfile)
        Dim line As String = ""
        line = pfile1.ReadLine
        line = pfile1.ReadLine



        For Each i As String In line.Split(New Char() {"	"c})
            If i <> "" Then
                If i.StartsWith("p(") Then
                    If i.Contains("-") Then
                        Dim temp As String = i.Split(New Char() {"["c})(1).Split(New Char() {"-"c})(0)
                        ListBox1.Items.Add(i.Split(New Char() {"["c})(0) + "[" + (CInt(Select_Node_list(CInt(temp))) + taxon_num).ToString + "-" + i.Split(New Char() {"-"c})(1))
                    Else
                        ListBox1.Items.Add(i)
                    End If
                Else
                    ListBox1.Items.Add(i)
                End If

            End If
        Next
        pfile1.Close()
    End Sub
    Private Sub ListBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBox1.SelectedIndexChanged
        PictureBox1.Refresh()
    End Sub
    Public Sub drawfig1(ByVal drawg As Graphics)
        If ListBox1.SelectedIndex > 0 Then
            drawg.SmoothingMode = SmoothingMode.AntiAlias
            Dim newfont As New Font("Times New Roman", 10, FontStyle.Regular, GraphicsUnit.Point)
            Dim pfile1 As New StreamReader(runfile)
            Dim line As String = ""
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            Dim x As Integer = 0
            Dim sum_x As Single = 0
            Dim xstep As Integer = NumericUpDown1.Value
            Do
                Dim linesplit() As String = line.Split(New Char() {"	"c})
                linesplit(0) = linesplit(0).Replace(" ", "")
                If IsNumeric(linesplit(ListBox1.SelectedIndex)) Then
                    Dim tempnum As Single = Val(linesplit(ListBox1.SelectedIndex))
                    sum_x += tempnum
                    If x Mod xstep = 0 Then
                        drawg.FillEllipse(Brushes.Blue, CInt(NumericUpDown4.Value + x / xstep - NumericUpDown5.Value / 2), CInt(NumericUpDown3.Value - NumericUpDown5.Value / 2 - tempnum * NumericUpDown2.Value), NumericUpDown5.Value, NumericUpDown5.Value)
                        If (x / xstep) Mod 100 = 0 And x > 0 Then
                            Dim font_w As Integer = drawg.MeasureString(linesplit(0), newfont).Width
                            drawg.DrawString(linesplit(0), newfont, Brushes.Black, CInt(NumericUpDown4.Value + x / xstep - font_w / 2), NumericUpDown3.Value + 2)
                            drawg.DrawLine(Pens.Black, CInt(NumericUpDown4.Value + x / xstep), NumericUpDown3.Value - 4, CInt(NumericUpDown4.Value + x / xstep), NumericUpDown3.Value)
                        End If
                    End If
                End If
                line = pfile1.ReadLine
                x += 1
            Loop Until line = ""
            drawg.DrawLine(Pens.Gray, 0, CInt(NumericUpDown3.Value - NumericUpDown5.Value / 2 - sum_x / x * NumericUpDown2.Value), CInt(NumericUpDown4.Value + x / xstep), CInt(NumericUpDown3.Value - NumericUpDown5.Value / 2 - sum_x / x * NumericUpDown2.Value))
            drawg.DrawString((CInt((sum_x / x * 10000)) / 10000).ToString("F4"), newfont, Brushes.Gray, 0, CInt(NumericUpDown3.Value - NumericUpDown5.Value / 2 - sum_x / x * NumericUpDown2.Value) - newfont.Height)
            drawg.DrawLine(Pens.Black, 0, CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2), CInt(NumericUpDown4.Value + x / xstep), CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2))
            drawg.DrawLine(Pens.Black, NumericUpDown4.Value, 0, NumericUpDown4.Value, PictureBox1.Height)
            For i As Integer = 0 To 10 * NumericUpDown6.Value Step NumericUpDown7.Value
                If i > 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            For i As Integer = -10 * NumericUpDown6.Value To 0 Step NumericUpDown7.Value
                If i <> 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            pfile1.Close()
        End If
    End Sub
    Public Sub drawfig2(ByVal drawg As Graphics)
        If ListBox1.SelectedIndex > 0 Then
            If ListBox1.Items(ListBox1.SelectedIndex).ToString.StartsWith("a") Then
                drawfig3(drawg)
                Exit Sub
            ElseIf ListBox1.Items(ListBox1.SelectedIndex).ToString.StartsWith("p") = False Then
                Exit Sub
            End If
            drawg.SmoothingMode = SmoothingMode.AntiAlias
            Dim pfile1 As New StreamReader(runfile)
            Dim line As String = ""
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            Dim x As Integer = 0
            Dim dis(100) As Integer
            Do
                Dim linesplit() As String = line.Split(New Char() {"	"c})
                If IsNumeric(linesplit(ListBox1.SelectedIndex)) Then
                    Dim tempnum As Integer = CInt((Val(linesplit(ListBox1.SelectedIndex)) * 100).ToString("F0"))
                    dis(tempnum) += 1
                    x += 1
                End If
                line = pfile1.ReadLine
            Loop Until line = ""
            Dim linepen As New Pen(Color.BlueViolet, CInt(NumericUpDown2.Value / 100))
            For i As Integer = 0 To 100
                drawg.DrawLine(linepen, NumericUpDown4.Value, NumericUpDown3.Value - CInt((i + 0.5) * NumericUpDown2.Value / 100), NumericUpDown4.Value + CInt(100 * dis(i) / x * NumericUpDown1.Value), NumericUpDown3.Value - CInt((i + 0.5) * NumericUpDown2.Value / 100))
            Next
            Dim newfont As New Font("Times New Roman", 10, FontStyle.Regular, GraphicsUnit.Point)
            drawg.DrawLine(Pens.Black, 0, CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2), PictureBox1.Width, CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2))
            drawg.DrawLine(Pens.Black, NumericUpDown4.Value, 0, NumericUpDown4.Value, PictureBox1.Height)

            drawg.DrawLine(Pens.Black, NumericUpDown4.Value + NumericUpDown1.Value * 100, NumericUpDown3.Value - NumericUpDown2.Value, NumericUpDown4.Value + NumericUpDown1.Value * 100, NumericUpDown3.Value)

            For i As Integer = 0 To 10 * NumericUpDown6.Value Step NumericUpDown7.Value
                If i > 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            For i As Integer = -10 * NumericUpDown6.Value To 0 Step NumericUpDown7.Value
                If i <> 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            pfile1.Close()
        End If
    End Sub
    Public Sub drawfig3(ByVal drawg As Graphics)
        If ListBox1.SelectedIndex > 0 Then
            drawg.SmoothingMode = SmoothingMode.AntiAlias
            Dim pfile1 As New StreamReader(runfile)
            Dim line As String = ""
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            line = pfile1.ReadLine
            Dim x As Integer = 0
            Dim dis(200) As Integer
            Do
                Dim linesplit() As String = line.Split(New Char() {"	"c})
                If IsNumeric(linesplit(ListBox1.SelectedIndex)) Then
                    Dim tempnum As Integer = CInt((Val(linesplit(ListBox1.SelectedIndex))).ToString("F0"))
                    dis(tempnum) += 1
                    x += 1
                End If
                line = pfile1.ReadLine
            Loop Until line = ""
            Dim linepen As New Pen(Color.BlueViolet, CInt(NumericUpDown2.Value / 100))
            For i As Integer = 0 To 200
                drawg.DrawLine(linepen, NumericUpDown4.Value, NumericUpDown3.Value - CInt((i + 0.5) * NumericUpDown2.Value / 100), NumericUpDown4.Value + CInt(100 * dis(i) / x * NumericUpDown1.Value), NumericUpDown3.Value - CInt((i + 0.5) * NumericUpDown2.Value / 100))
            Next
            Dim newfont As New Font("Times New Roman", 10, FontStyle.Regular, GraphicsUnit.Point)
            drawg.DrawLine(Pens.Black, 0, CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2), PictureBox1.Width, CInt(NumericUpDown3.Value + NumericUpDown5.Value / 2))
            drawg.DrawLine(Pens.Black, NumericUpDown4.Value, 0, NumericUpDown4.Value, PictureBox1.Height)

            drawg.DrawLine(Pens.Black, NumericUpDown4.Value + NumericUpDown1.Value * 100, NumericUpDown3.Value - NumericUpDown2.Value * 2, NumericUpDown4.Value + NumericUpDown1.Value * 100, NumericUpDown3.Value)

            For i As Integer = 0 To 10 * NumericUpDown6.Value Step NumericUpDown7.Value
                If i > 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            For i As Integer = -10 * NumericUpDown6.Value To 0 Step NumericUpDown7.Value
                If i <> 0 Then
                    Dim font_h As Integer = drawg.MeasureString((i / 10).ToString, newfont).Height
                    Dim font_w As Integer = drawg.MeasureString((i / 10).ToString, newfont).Width
                    drawg.DrawLine(Pens.Black, NumericUpDown4.Value, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10), NumericUpDown4.Value + 4, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10))
                    drawg.DrawString((i / 10).ToString, newfont, Brushes.Black, NumericUpDown4.Value - font_w, CInt(NumericUpDown3.Value - NumericUpDown2.Value * i / 10 - font_h / 2))
                End If
            Next
            pfile1.Close()
        End If
    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If File.Exists(runfile) = False Then
            Exit Sub
        End If
        If RadioButton1.Checked Then
            drawfig1(e.Graphics)
        Else
            drawfig2(e.Graphics)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        PictureBox1.Refresh()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        RadioButton1.Checked = RadioButton2.Checked Xor True
        If RadioButton2.Checked Then
            NumericUpDown1.Value = 5
        End If
        PictureBox1.Refresh()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        RadioButton2.Checked = RadioButton1.Checked Xor True
        If RadioButton1.Checked Then
            NumericUpDown1.Value = 1
        End If
        PictureBox1.Refresh()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        NumericUpDown1.Value = 1
        NumericUpDown2.Value = 200
        NumericUpDown3.Value = 250
        NumericUpDown4.Value = 50
        NumericUpDown5.Value = 3
        NumericUpDown6.Value = 2
        NumericUpDown7.Value = 1
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                runfile = (root_path + "temp" + path_char + "clade1.nex.run1.p")
            Case 1
                runfile = (root_path + "temp" + path_char + "clade1.nex.run2.p")
        End Select
        loadfile()
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PictureBox1.Click

    End Sub
End Class