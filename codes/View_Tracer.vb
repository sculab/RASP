Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class View_Tracer

    Private Sub View_Tracer_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles MyBase.FormClosing
        e.Cancel = True
        Hide
    End Sub
    Dim sample_parameters() As Integer
    Dim sample_number() As String
    Dim max_p, min_p, cyc_count As Integer

    Private Sub View_Tracer_Resize(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Resize
        PictureBox1.Refresh
    End Sub
    Private Sub View_Tracer_VisibleChanged(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.VisibleChanged
        If Visible Then
            If File.Exists(root_path + "temp" + path_char + "bayarea.areas.txt.parameters.txt") Then
                Dim sr As New StreamReader(root_path + "temp" + path_char + "bayarea.areas.txt.parameters.txt")
                Dim line = sr.ReadLine
                line = sr.ReadLine
                cyc_count = 0
                Do
                    ReDim Preserve sample_parameters(cyc_count)
                    ReDim Preserve sample_number(cyc_count)
                    sample_parameters(cyc_count) = Val(line.Split("	")(1))
                    sample_number(cyc_count) = line.Split("	")(0)
                    cyc_count += 1
                    line = sr.ReadLine
                Loop Until line = ""
                cyc_count -= 1
                max_p = sample_parameters(0)
                min_p = sample_parameters(0)
                For i = 0 To cyc_count
                    If sample_parameters(i) < min_p Then
                        min_p = sample_parameters(i)
                    End If
                    If sample_parameters(i) > max_p Then
                        max_p = sample_parameters(i)
                    End If
                Next
                sr.Close
                cyc_count += 1
            Else
                MsgBox("Could not find Bayarea parameters!")
            End If
        End If
    End Sub
    Public Sub drawfig(ByVal drawg As Graphics)
        drawg.SmoothingMode = SmoothingMode.AntiAlias
        Dim newfont As New Font("Times New Roman", 10, FontStyle.Regular, GraphicsUnit.Point)
        drawg.DrawLine(Pens.Black, 0, 16, PictureBox1.Width, 16)
        drawg.DrawLine(Pens.Black, 16, 0, 16, PictureBox1.Height)
        For i As Integer = 1 To 9
            drawg.DrawLine(Pens.Black, 16 + CInt((PictureBox1.Width - 16) / 10 * i), 0, 16 + CInt((PictureBox1.Width - 16) / 10 * i), PictureBox1.Height)
            drawg.DrawString(sample_number(CInt(i / 10 * cyc_count) - 1), newfont, Brushes.Gray, 16 + CInt((PictureBox1.Width - 16) / 10 * i), 0)
            drawg.DrawString(CInt(i / 10 * (max_p - min_p) + min_p).ToString, newfont, Brushes.Gray, 16, 16 + CInt((PictureBox1.Height - 16) / 10 * i))
        Next
        For i As Integer = 1 To cyc_count
            drawg.FillEllipse(Brushes.Blue, 16 - 2 + CInt((PictureBox1.Width - 16) / cyc_count * (i - 1)), CInt((PictureBox1.Height - 16) * (sample_parameters(i - 1) - min_p) / (max_p - min_p)) + 16 - 2, 4, 4)
        Next
    End Sub
    Private Sub PictureBox1_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles PictureBox1.Paint
        If File.Exists(root_path + "temp" + path_char + "bayarea.areas.txt.parameters.txt") = False Then
            Exit Sub
        End If
        drawfig(e.Graphics)
    End Sub

    Private Sub View_Tracer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If CInt(TextBox1.Text) Mod CInt(BayAreaForm.TextBox1.Text) <> 0 Then
            MsgBox("Burn-in should be an integer multiple of frequent of samples.")
            Exit Sub
        End If
        If CInt(TextBox1.Text) >= CInt(BayAreaForm.TextBox3.Text) Then
            MsgBox("Burn-in should be no more than chain length.")
            Exit Sub
        End If
        Process_ID = 7
        Config_BayArea_Burnin = CInt(TextBox1.Text)
        bayarea_gen = -1
        Me.Hide()
    End Sub
End Class