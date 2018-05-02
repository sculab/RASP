Imports System.IO
Imports System.Drawing.Drawing2D

Public Class View_Pie
    Dim begin_draw As Boolean = False
    Dim area_dis() As String
    Dim area_p() As Single
    Dim pec() As Single
    Dim distrubition() As String
    Dim particular_node_info As String
    Dim node_line, node_line1 As String
    Dim pie_radii As Integer = 120
    Dim pie_x As Integer
    Dim pie_y As Integer
    Dim zoom As Integer = 5
    Dim pie_show As String
    Dim node_list() As String
    Private Sub pie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        Me.MinimizeBox = enableMin
        pie_show = show_pie
        If File.Exists(root_path + "temp" + path_char + pie_show) = False Then
            Dim k As Integer = 1
            Select Case pie_show
                Case "bayes"
                    Do
                        If File.Exists(root_path + "temp" + path_char + "clade" + k.ToString + ".r") Then
                            Dim Temp_read_node As New StreamReader(root_path + "temp" + path_char + "clade" + k.ToString + ".r")
                            particular_node_info = Temp_read_node.ReadLine
                            ComboBox1.Items.Add(particular_node_info)
                            ReDim Preserve node_list(ComboBox1.Items.Count)
                            node_list(ComboBox1.Items.Count) = k
                            Temp_read_node.Close()
                        End If
                        k += 1
                    Loop Until k = 512
                Case Else
                    Do While File.Exists(root_path + "temp" + path_char + "clade_P" + k.ToString + ".r") = True
                        If File.Exists(root_path + "temp" + path_char + "clade_P" + k.ToString + ".r") Then
                            Dim Temp_read_node As New StreamReader(root_path + "temp" + path_char + "clade_P" + k.ToString + ".r")
                            particular_node_info = Temp_read_node.ReadLine
                            ComboBox1.Items.Add(particular_node_info)
                            Temp_read_node.Close()
                        End If
                        k += 1
                    Loop
            End Select

            begin_draw = False
            If k >= 2 Then
                ComboBox1.SelectedIndex = 0
            End If
            Exit Sub
        End If
        Dim read_node As New StreamReader(root_path + "temp" + path_char + pie_show)
        particular_node_info = read_node.ReadLine
        Dim line As String = read_node.ReadLine
        ReDim area_p(0)
        ReDim distrubition(0)
        If line = "" Then
            begin_draw = False
            Exit Sub
        End If
        Do
            area_p(UBound(area_p)) = Val(line.Split(New Char() {"	"c})(1))
            distrubition(UBound(distrubition)) = line.Split(New Char() {"	"c})(0)
            line = read_node.ReadLine
            If line <> "" Then
                ReDim Preserve distrubition(UBound(distrubition) + 1)
                ReDim Preserve area_p(UBound(area_p) + 1)
            End If
        Loop Until line = ""
        Array.Sort(area_p, distrubition, New scomparer)
        read_node.Close()
        Dim sum As Single = 0
        For i As Integer = 0 To area_p.Length - 1
            If IsNumeric(area_p(i)) Then
                sum = sum + area_p(i)
            End If
        Next
        If sum > 0 Then
            For i As Integer = 0 To area_p.Length - 1
                area_p(i) = area_p(i) / sum * 100
            Next
            begin_draw = True
            PicShow.Refresh()
        End If


    End Sub
    Private Sub Mypaint(ByVal TempGrap As Graphics, ByVal zoom As Integer)
        Dim sRectPer() As Single
        ReDim sRectPer(UBound(pec))
        For i As Integer = 0 To UBound(pec)
            sRectPer(i) = 360 * pec(i) / 100
        Next
        PicShow.Width = 100 + area_dis.Length * 80 + 2 * pie_radii
        Dim x2() As Single, y2() As Single
        ReDim x2(UBound(pec))
        ReDim y2(UBound(pec))
        '填充饼图
        TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(area_dis(0))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2, 0, sRectPer(0))
        For n As Integer = 1 To sRectPer.Length - 1
            TempGrap.FillPie(Int2Brushes(Distributiton_to_Integer(area_dis(n))), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2, sRectPer(n - 1), sRectPer(n) - sRectPer(n - 1))
        Next
        '边缘
        TempGrap.DrawEllipse(New Pen(Color.Black), pie_x - pie_radii, pie_y - pie_radii, pie_radii * 2, pie_radii * 2)


        '显示分布区
        Dim node_font As New System.Drawing.Font("Tahoma", 10 * zoom, FontStyle.Regular)

        x2(0) = CInt(Math.Cos(3.14 * pec(0) / 100 + 3.14) * pie_radii)
        y2(0) = CInt(Math.Sin(3.14 * pec(0) / 100 + 3.14) * pie_radii)
        If x2(0) >= 0 And y2(0) >= 0 Then
            x2(0) = (pie_x - x2(0)) - area_dis(0).Length * node_font.SizeInPoints / 2
            y2(0) = (pie_y - y2(0)) - node_font.Height
        ElseIf x2(0) >= 0 And y2(0) < 0 Then
            x2(0) = (pie_x - x2(0)) - area_dis(0).Length * node_font.SizeInPoints / 2
            y2(0) = (pie_y - y2(0))
        ElseIf x2(0) < 0 And y2(0) >= 0 Then
            x2(0) = (pie_x - x2(0))
            y2(0) = (pie_y - y2(0)) - node_font.Height
        ElseIf x2(0) < 0 And y2(0) < 0 Then
            x2(0) = (pie_x - x2(0))
            y2(0) = (pie_y - y2(0))
        End If
        TempGrap.DrawString(area_dis(0), node_font, Int2Brushes(Distributiton_to_Integer(area_dis(0))), x2(0), y2(0))
        For n As Integer = 1 To sRectPer.Length - 1
            If area_dis(n) <> "" Then
                If (sRectPer(n) - sRectPer(n - 1)) > 0 Or area_dis(n) <> "*" Then
                    x2(n) = CInt((Math.Cos(3.14 * pec(n) / 100 + 3.14 * pec(n - 1) / 100 + 3.14)) * pie_radii)
                    y2(n) = CInt((Math.Sin(3.14 * pec(n) / 100 + 3.14 * pec(n - 1) / 100 + 3.14)) * pie_radii)
                    If x2(n) >= 0 And y2(n) >= 0 Then
                        x2(n) = (pie_x - x2(n)) - area_dis(n).Length * node_font.SizeInPoints / 2
                        y2(n) = (pie_y - y2(n)) - node_font.Height
                    ElseIf x2(n) >= 0 And y2(n) < 0 Then
                        x2(n) = (pie_x - x2(n)) - area_dis(n).Length * node_font.SizeInPoints / 2
                        y2(n) = (pie_y - y2(n))
                    ElseIf x2(n) < 0 And y2(n) >= 0 Then

                        y2(n) = (pie_y - y2(n)) - node_font.Height

                        x2(n) = (pie_x - x2(n))
                    ElseIf x2(n) < 0 And y2(n) < 0 Then
                        x2(n) = (pie_x - x2(n))
                        y2(n) = (pie_y - y2(n))
                    End If
                    TempGrap.DrawString(area_dis(n), node_font, Int2Brushes(Distributiton_to_Integer(area_dis(n))), x2(n), y2(n))
                Else
                    Exit For
                End If
            Else
                Exit For
            End If
        Next
    End Sub
    Private Sub PicShow_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PicShow.Paint
        Dim Myline As Graphics = e.Graphics
        draw_pic(Myline, 1)
    End Sub
    Public Sub draw_pic(ByVal Myline As Object, ByVal zoom As Integer)
        If begin_draw Then
            Myline.SmoothingMode = SmoothingMode.AntiAlias

            Dim new_rect As System.Drawing.Rectangle
            new_rect.X = 0
            new_rect.Y = 0
            new_rect.Width = CInt(PicShow.Width) * zoom
            new_rect.Height = CInt(PicShow.Height) * zoom
            Dim node_font As New System.Drawing.Font("Tahoma", 10 * zoom, FontStyle.Regular)
            '划分区间

            ReDim pec(0)
            ReDim area_dis(0)
            pec(0) = area_p(0)
            area_dis(0) = distrubition(0)
            For i As Integer = 1 To area_p.Length - 1
                If area_p(i) > NumericUpDown1.Value Or i < NumericUpDown2.Value Then
                    ReDim Preserve pec(UBound(pec) + 1)
                    ReDim Preserve area_dis(UBound(area_dis) + 1)
                    pec(i) = pec(i - 1) + area_p(i)
                    area_dis(i) = distrubition(i)
                End If
            Next
            ReDim Preserve pec(UBound(pec) + 1)
            pec(UBound(pec)) = 100
            ReDim Preserve area_dis(UBound(area_dis) + 1)
            area_dis(UBound(area_dis)) = "*"

            Dim MyPen As Pen
            MyPen = New Pen(Int2Brushes(Distributiton_to_Integer(area_dis(0))), 32 * zoom)
            Myline.DrawLine(MyPen, (50) * zoom, 300 * zoom, (50) * zoom, (300 - CInt(pec(0)) * 2 - 1) * zoom)
            Myline.DrawString(area_dis(0) + ":" + CSng(pec(0)).ToString("F2") + "%", node_font, Brushes.Black, 25 * zoom, (300 - CInt(pec(0)) * 2 - 1) * zoom - node_font.Height)
            MyPen.Dispose()
            For n As Integer = 1 To pec.Length - 1
                If pec(n) - pec(n - 1) > 0 Then
                    MyPen = New Pen(Int2Brushes(Distributiton_to_Integer(area_dis(n))), 32 * zoom)
                    Myline.DrawLine(MyPen, (50 + n * 80) * zoom, 300 * zoom, (50 + n * 80) * zoom, (300 - CInt(pec(n) - pec(n - 1)) * 2 - 1) * zoom)
                    Myline.DrawString(area_dis(n) + ":" + CSng(pec(n) - pec(n - 1)).ToString("F2") + "%", node_font, Brushes.Black, (25 + n * 80) * zoom, (300 - CInt(pec(n) - pec(n - 1)) * 2 - 1) * zoom - node_font.Height)
                    MyPen.Dispose()
                End If
            Next
            Myline.DrawString(particular_node_info, node_font, Brushes.Black, 10 * zoom, 10 * zoom)
            pie_x = 50 + pec.Length * 80 + pie_radii
            pie_y = 300 - pie_radii
            Mypaint(Myline, zoom)
        End If
    End Sub

    Private Function percentage(ByVal rect As Integer, ByVal ParamArray rectall() As Integer) As Single
        Dim sum As Int64
        For i As Integer = 0 To UBound(rectall, 1)
            sum += rectall(i)
        Next
        percentage = rect / sum
    End Function


    Private Sub SavePictureToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SavePictureToolStripMenuItem.Click
        Try
            Dim bitmap As New Bitmap(CInt(PicShow.Width), CInt(PicShow.Height))
            Dim TempGrap As Graphics
            TempGrap = Graphics.FromImage(bitmap)
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
                    draw_pic(ig, 1)
                    ig.Dispose()
                    wmf.Dispose()
                    g.ReleaseHdc()
                    g.Dispose()
                ElseIf sfd.FileName.ToLower.EndsWith(".svg") Then
                    Dim svg_gra As New SvgNet.SvgGdi.SvgGraphics
                    draw_pic(svg_gra, 1)
                    Dim svg_writer As New StreamWriter(sfd.FileName, False)
                    svg_writer.Write(svg_gra.WriteSVGString)
                    svg_writer.Close()
                    svg_gra.Flush()
                Else
                    draw_pic(TempGrap, 1)
                    bitmap.Save(sfd.FileName)
                End If
                MsgBox("Save Successfully!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Dim read_node As StreamReader
        Select Case pie_show
            Case "bayes"
                read_node = New StreamReader(root_path + "temp" + path_char + "clade" + node_list(ComboBox1.SelectedIndex + 1) + ".r")
            Case Else
                read_node = New StreamReader(root_path + "temp" + path_char + "clade_P" + (ComboBox1.SelectedIndex + 1).ToString + ".r")
        End Select


        particular_node_info = read_node.ReadLine
        Dim line As String = read_node.ReadLine
        ReDim area_p(0)
        ReDim distrubition(0)
        If line = "" Then
            begin_draw = False
            Exit Sub
        End If
        Do
            area_p(UBound(area_p)) = line.Split(New Char() {"	"c})(1)
            distrubition(UBound(distrubition)) = line.Split(New Char() {"	"c})(0)
            line = read_node.ReadLine
            If line <> "" Then
                ReDim Preserve distrubition(UBound(distrubition) + 1)
                ReDim Preserve area_p(UBound(area_p) + 1)
            End If
        Loop Until line = ""
        read_node.Close()
        Array.Sort(area_p, distrubition, New scomparer)

        Dim sum As Single = 0
        For i As Integer = 0 To area_p.Length - 1
            If IsNumeric(area_p(i)) Then
                sum = sum + area_p(i)
            End If
        Next
        If sum > 0 Then
            For i As Integer = 0 To area_p.Length - 1
                area_p(i) = area_p(i) / sum * 100
            Next
            begin_draw = True
            PicShow.Refresh()
        End If
    End Sub

    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        PicShow.Refresh()
    End Sub

    Private Sub NumericUpDown2_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown2.ValueChanged
        PicShow.Refresh()
    End Sub

    Private Sub PicShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PicShow.Click

    End Sub
End Class