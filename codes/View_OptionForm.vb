Public Class View_OptionForm

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        area_lower = NumericUpDown1.Value
        keep_at_least = NumericUpDown2.Value
        Show_area_names = CheckBox1.Checked
        Show_area_pies = CheckBox2.Checked
        pie_radii = NumericUpDown7.Value
        Display_taxon_names = CheckBox3.Checked
        Tree_font = FontDialog1.Font
        Label_font = FontDialog2.Font
        ID_font = FontDialog3.Font
        ID_color = FontDialog3.Color
        Display_node_frequency = CheckBox4.Checked
        Low_frequency = NumericUpDown14.Value / 100
        Hide_pie = NumericUpDown15.Value / 100
        frequency_h = NumericUpDown3.Value
        frequency_v = NumericUpDown4.Value
        Display_node_ID = CheckBox5.Checked
        node_h = NumericUpDown5.Value
        node_v = NumericUpDown6.Value
        Taxon_separation = NumericUpDown8.Value
        Branch_length = NumericUpDown9.Value
        Border_separation = NumericUpDown10.Value
        Line_width = NumericUpDown11.Value
        Display_taxon_dis = CheckBox6.Checked
        ShowScale = CheckBox10.Checked
        TransparentBG = CheckBox7.Checked
        Display_taxon_pie = CheckBox8.Checked
        taxon_pie_radii = NumericUpDown12.Value
        File_zoom = NumericUpDown13.Value
        Display_lines = CheckBox9.Checked
        Display_Null_distribution = CheckBox11.Checked
        Display_circle = CheckBox12.Checked
        Circle_size = NumericUpDown16.Value
        Circle_color = Label17.BackColor
        dorefresh = True
        Me.Close()
    End Sub

    Private Sub OptionForm_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible = True Then
            NumericUpDown12.Value = taxon_pie_radii
            CheckBox8.Checked = Display_taxon_pie
            CheckBox9.Checked = Display_lines
            NumericUpDown1.Value = area_lower
            NumericUpDown2.Value = keep_at_least
            CheckBox1.Checked = Show_area_names
            CheckBox2.Checked = Show_area_pies
            NumericUpDown7.Value = pie_radii
            CheckBox3.Checked = Display_taxon_names
            FontDialog1.Font = Tree_font
            FontDialog2.Font = Label_font
            FontDialog3.Font = ID_font
            FontDialog3.Color = ID_color
            CheckBox4.Checked = Display_node_frequency
            NumericUpDown3.Value = frequency_h
            NumericUpDown4.Value = frequency_v
            CheckBox5.Checked = Display_node_ID
            CheckBox6.Checked = Display_taxon_dis
            NumericUpDown5.Value = node_h
            NumericUpDown6.Value = node_v
            NumericUpDown8.Value = Taxon_separation
            NumericUpDown9.Value = Branch_length
            NumericUpDown10.Value = Border_separation
            NumericUpDown11.Value = Line_width
            CheckBox7.Checked = TransparentBG
            CheckBox8.Checked = Display_taxon_pie
            NumericUpDown13.Value = File_zoom
            CheckBox10.Checked = ShowScale
            CheckBox11.Checked = Display_Null_distribution
            NumericUpDown14.Value = Low_frequency * 100
            NumericUpDown15.Value = Hide_pie * 100
            CheckBox12.Checked = Display_circle
            NumericUpDown16.Value = Circle_size
            Label17.BackColor = Circle_color
        End If
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        FontDialog1.ShowDialog()  '开启字体对话框
    End Sub

    Private Sub OptionForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        ComboBox1.SelectedIndex = 0
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBox1.SelectedIndexChanged
        Select Case ComboBox1.SelectedIndex
            Case 0
                NumericUpDown7.Value = 16
                NumericUpDown3.Value = 10
                NumericUpDown4.Value = 10
                NumericUpDown8.Value = 32
                NumericUpDown9.Value = 32
                NumericUpDown12.Value = 5
            Case 1
                NumericUpDown7.Value = 12
                NumericUpDown3.Value = 8
                NumericUpDown4.Value = 8
                NumericUpDown8.Value = 24
                NumericUpDown9.Value = 24
                NumericUpDown12.Value = 3
            Case 2
                NumericUpDown7.Value = 8
                NumericUpDown3.Value = 5
                NumericUpDown4.Value = 5
                NumericUpDown8.Value = 16
                NumericUpDown9.Value = 16
                NumericUpDown12.Value = 2
            Case 3
                NumericUpDown1.Value = 100
                NumericUpDown2.Value = 2
                NumericUpDown7.Value = 14
                NumericUpDown3.Value = 8
                NumericUpDown4.Value = 8
                NumericUpDown8.Value = 12
                NumericUpDown9.Value = 28
                NumericUpDown12.Value = 3
                CheckBox1.Checked = True
                CheckBox4.Checked = False
                CheckBox6.Checked = False
        End Select
    End Sub

    Private Sub CheckBox8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox8.CheckedChanged

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        FontDialog2.ShowDialog()
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        FontDialog3.ShowDialog()
    End Sub

    Private Sub NumericUpDown8_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown8.ValueChanged

    End Sub
End Class