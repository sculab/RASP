Module Module_Option
    Public ShowScale As Boolean = True
    Public StartTreeView As Boolean = False
    Public TransparentBG As Boolean = False
    Public area_lower As Integer = 5
    Public keep_at_least As Integer = 1
    Public Show_area_names As Boolean = False
    Public Show_area_pies As Boolean = True
    Public pie_radii As Single = 16
    Public Display_taxon_names As Boolean = True
    Public Display_Null_distribution As Boolean = True
    Public Display_taxon_dis As Boolean = True
    Public Display_taxon_pie As Boolean = True
    Public Display_circle As Boolean = True
    Public Circle_size As Integer = 8
    Public Circle_color As System.Drawing.Color = Color.White
    Public Tree_font As System.Drawing.Font
    Public Label_font As System.Drawing.Font
    Public ID_font As System.Drawing.Font
    Public ID_color As System.Drawing.Color = Color.Black
    Public Display_node_frequency As Boolean = True
    Public Low_frequency As Single = 0.01
    Public Hide_pie As Single = 0
    Public Display_lines As Boolean = True
    Public frequency_h As Integer = 10
    Public frequency_v As Integer = 10
    Public Display_node_ID As Boolean = True
    Public node_h As Integer = -8
    Public node_v As Integer = -8
    Public Taxon_separation As Integer = 32
    Public Branch_length As Integer = 32
    Public Border_separation As Integer = 10
    Public Line_width As Integer = 1
    Public File_zoom As Integer = 4
    Public taxon_pie_radii As Integer = 5
    Public dorefresh As Boolean = False
End Module
