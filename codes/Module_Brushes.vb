Imports System.Drawing
Module Module_Brushes
    Public Function Distributiton_to_Integer(ByVal D_str As String) As Integer
        If IsNumeric(D_str) Then
            Return D_str
        End If
        Dim D_Integer As Integer = 0
        If D_str.Contains("_") Then
            Return 0
        End If
        For Each i As Char In D_str.ToUpper
            D_Integer += 2 ^ (AscW(i) - AscW("A"))
        Next
        If D_str = "*" Then
            Return -1
        End If
        Return D_Integer
    End Function
    Public Globe_Brush As Brush() = {Brushes.White, Brushes.Blue, Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, Brushes.CadetBlue, Brushes.Chartreuse, Brushes.Chocolate, Brushes.OrangeRed, Brushes.CornflowerBlue, Brushes.Crimson, Brushes.Cyan, Brushes.DarkBlue, Brushes.DarkCyan, Brushes.DarkGoldenrod, Brushes.DarkGray, Brushes.DarkGreen, Brushes.DarkKhaki, Brushes.DarkMagenta, Brushes.DarkOliveGreen, Brushes.DarkOrange, Brushes.DarkOrchid, Brushes.DarkRed, Brushes.DarkSalmon, Brushes.DarkSeaGreen, Brushes.DarkSlateBlue, Brushes.DarkSlateGray, Brushes.DarkTurquoise, Brushes.DarkViolet, Brushes.DeepPink, Brushes.DeepSkyBlue, Brushes.DimGray, Brushes.DodgerBlue, Brushes.Firebrick, Brushes.ForestGreen, Brushes.Fuchsia, Brushes.Gainsboro, Brushes.Gold, Brushes.Goldenrod, Brushes.Gray, Brushes.Green, Brushes.GreenYellow, Brushes.HotPink, Brushes.IndianRed, Brushes.Indigo, Brushes.Khaki, Brushes.Lavender, Brushes.LawnGreen, Brushes.LightBlue, Brushes.LightCoral, Brushes.LightCyan, Brushes.LightGreen, Brushes.Moccasin, Brushes.LightPink, Brushes.LightSalmon, Brushes.LightSeaGreen, Brushes.LightSkyBlue, Brushes.LightSlateGray, Brushes.LightSteelBlue, Brushes.Lime, Brushes.LimeGreen, Brushes.Magenta, Brushes.Maroon, Brushes.MediumAquamarine, Brushes.MediumBlue, Brushes.MediumOrchid, Brushes.MediumPurple, Brushes.MediumSeaGreen, Brushes.MediumSlateBlue, Brushes.MediumSpringGreen, Brushes.MediumTurquoise, Brushes.MediumVioletRed, Brushes.MidnightBlue, Brushes.MistyRose, Brushes.NavajoWhite, Brushes.Navy, Brushes.Olive, Brushes.OliveDrab, Brushes.Orange, Brushes.Coral, Brushes.Orchid, Brushes.PaleGreen, Brushes.PaleTurquoise, Brushes.PaleVioletRed, Brushes.PeachPuff, Brushes.Peru, Brushes.Pink, Brushes.Plum, Brushes.PowderBlue, Brushes.Purple, Brushes.Red, Brushes.RosyBrown, Brushes.RoyalBlue, Brushes.SaddleBrown, Brushes.Salmon, Brushes.SeaGreen, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue, Brushes.SlateBlue, Brushes.SlateGray, Brushes.SpringGreen, Brushes.SteelBlue, Brushes.Tan, Brushes.Teal, Brushes.Thistle, Brushes.Tomato, Brushes.Turquoise, Brushes.Violet, Brushes.Wheat, Brushes.Yellow, Brushes.Black}
    Public Globe_Color As Color() = {Color.White, Color.Blue, Color.BlueViolet, Color.Brown, Color.BurlyWood, Color.CadetBlue, Color.Chartreuse, Color.Chocolate, Color.OrangeRed, Color.CornflowerBlue, Color.Crimson, Color.Cyan, Color.DarkBlue, Color.DarkCyan, Color.DarkGoldenrod, Color.DarkGray, Color.DarkGreen, Color.DarkKhaki, Color.DarkMagenta, Color.DarkOliveGreen, Color.DarkOrange, Color.DarkOrchid, Color.DarkRed, Color.DarkSalmon, Color.DarkSeaGreen, Color.DarkSlateBlue, Color.DarkSlateGray, Color.DarkTurquoise, Color.DarkViolet, Color.DeepPink, Color.DeepSkyBlue, Color.DimGray, Color.DodgerBlue, Color.Firebrick, Color.ForestGreen, Color.Fuchsia, Color.Gainsboro, Color.Gold, Color.Goldenrod, Color.Gray, Color.Green, Color.GreenYellow, Color.HotPink, Color.IndianRed, Color.Indigo, Color.Khaki, Color.Lavender, Color.LawnGreen, Color.LightBlue, Color.LightCoral, Color.LightCyan, Color.LightGreen, Color.Moccasin, Color.LightPink, Color.LightSalmon, Color.LightSeaGreen, Color.LightSkyBlue, Color.LightSlateGray, Color.LightSteelBlue, Color.Lime, Color.LimeGreen, Color.Magenta, Color.Maroon, Color.MediumAquamarine, Color.MediumBlue, Color.MediumOrchid, Color.MediumPurple, Color.MediumSeaGreen, Color.MediumSlateBlue, Color.MediumSpringGreen, Color.MediumTurquoise, Color.MediumVioletRed, Color.MidnightBlue, Color.MistyRose, Color.NavajoWhite, Color.Navy, Color.Olive, Color.OliveDrab, Color.Orange, Color.Coral, Color.Orchid, Color.PaleGreen, Color.PaleTurquoise, Color.PaleVioletRed, Color.PeachPuff, Color.Peru, Color.Pink, Color.Plum, Color.PowderBlue, Color.Purple, Color.Red, Color.RosyBrown, Color.RoyalBlue, Color.SaddleBrown, Color.Salmon, Color.SeaGreen, Color.Sienna, Color.Silver, Color.SkyBlue, Color.SlateBlue, Color.SlateGray, Color.SpringGreen, Color.SteelBlue, Color.Tan, Color.Teal, Color.Thistle, Color.Tomato, Color.Turquoise, Color.Violet, Color.Wheat, Color.Yellow, Color.Black}
    Public Function str_to_bool(ByVal font_str As String) As Boolean
        If font_str.ToUpper.StartsWith("T") Then
            Return True
        End If
        Return False
    End Function
    Public Function str_to_font(ByVal font_str As String) As Font
        Try
            font_str = font_str.Split(":")(1)
            Dim font_arr() As String = font_str.Split(",")
            Return New Font(font_arr(0).Split("=")(1), CInt(font_arr(1).Split("=")(1)), FontStyle.Regular, 3, 0, False)
        Catch ex As Exception
            Return Label_font
        End Try
    End Function
    Public Function str_to_color(ByVal color_str As String) As Color
        If color_str = "Color [White]" Then
            Return Color.White
        End If
        If color_str = "Color [Blue]" Then
            Return Color.Blue
        End If
        If color_str = "Color [BlueViolet]" Then
            Return Color.BlueViolet
        End If
        If color_str = "Color [Brown]" Then
            Return Color.Brown
        End If
        If color_str = "Color [BurlyWood]" Then
            Return Color.BurlyWood
        End If
        If color_str = "Color [CadetBlue]" Then
            Return Color.CadetBlue
        End If
        If color_str = "Color [Chartreuse]" Then
            Return Color.Chartreuse
        End If
        If color_str = "Color [Chocolate]" Then
            Return Color.Chocolate
        End If
        If color_str = "Color [OrangeRed]" Then
            Return Color.OrangeRed
        End If
        If color_str = "Color [CornflowerBlue]" Then
            Return Color.CornflowerBlue
        End If
        If color_str = "Color [Crimson]" Then
            Return Color.Crimson
        End If
        If color_str = "Color [Cyan]" Then
            Return Color.Cyan
        End If
        If color_str = "Color [DarkBlue]" Then
            Return Color.DarkBlue
        End If
        If color_str = "Color [DarkCyan]" Then
            Return Color.DarkCyan
        End If
        If color_str = "Color [DarkGoldenrod]" Then
            Return Color.DarkGoldenrod
        End If
        If color_str = "Color [DarkGray]" Then
            Return Color.DarkGray
        End If
        If color_str = "Color [DarkGreen]" Then
            Return Color.DarkGreen
        End If
        If color_str = "Color [DarkKhaki]" Then
            Return Color.DarkKhaki
        End If
        If color_str = "Color [DarkMagenta]" Then
            Return Color.DarkMagenta
        End If
        If color_str = "Color [DarkOliveGreen]" Then
            Return Color.DarkOliveGreen
        End If
        If color_str = "Color [DarkOrange]" Then
            Return Color.DarkOrange
        End If
        If color_str = "Color [DarkOrchid]" Then
            Return Color.DarkOrchid
        End If
        If color_str = "Color [DarkRed]" Then
            Return Color.DarkRed
        End If
        If color_str = "Color [DarkSalmon]" Then
            Return Color.DarkSalmon
        End If
        If color_str = "Color [DarkSeaGreen]" Then
            Return Color.DarkSeaGreen
        End If
        If color_str = "Color [DarkSlateBlue]" Then
            Return Color.DarkSlateBlue
        End If
        If color_str = "Color [DarkSlateGray]" Then
            Return Color.DarkSlateGray
        End If
        If color_str = "Color [DarkTurquoise]" Then
            Return Color.DarkTurquoise
        End If
        If color_str = "Color [DarkViolet]" Then
            Return Color.DarkViolet
        End If
        If color_str = "Color [DeepPink]" Then
            Return Color.DeepPink
        End If
        If color_str = "Color [DeepSkyBlue]" Then
            Return Color.DeepSkyBlue
        End If
        If color_str = "Color [DimGray]" Then
            Return Color.DimGray
        End If
        If color_str = "Color [DodgerBlue]" Then
            Return Color.DodgerBlue
        End If
        If color_str = "Color [Firebrick]" Then
            Return Color.Firebrick
        End If
        If color_str = "Color [ForestGreen]" Then
            Return Color.ForestGreen
        End If
        If color_str = "Color [Fuchsia]" Then
            Return Color.Fuchsia
        End If
        If color_str = "Color [Gainsboro]" Then
            Return Color.Gainsboro
        End If
        If color_str = "Color [Gold]" Then
            Return Color.Gold
        End If
        If color_str = "Color [Goldenrod]" Then
            Return Color.Goldenrod
        End If
        If color_str = "Color [Gray]" Then
            Return Color.Gray
        End If
        If color_str = "Color [Green]" Then
            Return Color.Green
        End If
        If color_str = "Color [GreenYellow]" Then
            Return Color.GreenYellow
        End If
        If color_str = "Color [HotPink]" Then
            Return Color.HotPink
        End If
        If color_str = "Color [IndianRed]" Then
            Return Color.IndianRed
        End If
        If color_str = "Color [Indigo]" Then
            Return Color.Indigo
        End If
        If color_str = "Color [Khaki]" Then
            Return Color.Khaki
        End If
        If color_str = "Color [Lavender]" Then
            Return Color.Lavender
        End If
        If color_str = "Color [LawnGreen]" Then
            Return Color.LawnGreen
        End If
        If color_str = "Color [LightBlue]" Then
            Return Color.LightBlue
        End If
        If color_str = "Color [LightCoral]" Then
            Return Color.LightCoral
        End If
        If color_str = "Color [LightCyan]" Then
            Return Color.LightCyan
        End If
        If color_str = "Color [LightGreen]" Then
            Return Color.LightGreen
        End If
        If color_str = "Color [Moccasin]" Then
            Return Color.Moccasin
        End If
        If color_str = "Color [LightPink]" Then
            Return Color.LightPink
        End If
        If color_str = "Color [LightSalmon]" Then
            Return Color.LightSalmon
        End If
        If color_str = "Color [LightSeaGreen]" Then
            Return Color.LightSeaGreen
        End If
        If color_str = "Color [LightSkyBlue]" Then
            Return Color.LightSkyBlue
        End If
        If color_str = "Color [LightSlateGray]" Then
            Return Color.LightSlateGray
        End If
        If color_str = "Color [LightSteelBlue]" Then
            Return Color.LightSteelBlue
        End If
        If color_str = "Color [Lime]" Then
            Return Color.Lime
        End If
        If color_str = "Color [LimeGreen]" Then
            Return Color.LimeGreen
        End If
        If color_str = "Color [Magenta]" Then
            Return Color.Magenta
        End If
        If color_str = "Color [Maroon]" Then
            Return Color.Maroon
        End If
        If color_str = "Color [MediumAquamarine]" Then
            Return Color.MediumAquamarine
        End If
        If color_str = "Color [MediumBlue]" Then
            Return Color.MediumBlue
        End If
        If color_str = "Color [MediumOrchid]" Then
            Return Color.MediumOrchid
        End If
        If color_str = "Color [MediumPurple]" Then
            Return Color.MediumPurple
        End If
        If color_str = "Color [MediumSeaGreen]" Then
            Return Color.MediumSeaGreen
        End If
        If color_str = "Color [MediumSlateBlue]" Then
            Return Color.MediumSlateBlue
        End If
        If color_str = "Color [MediumSpringGreen]" Then
            Return Color.MediumSpringGreen
        End If
        If color_str = "Color [MediumTurquoise]" Then
            Return Color.MediumTurquoise
        End If
        If color_str = "Color [MediumVioletRed]" Then
            Return Color.MediumVioletRed
        End If
        If color_str = "Color [MidnightBlue]" Then
            Return Color.MidnightBlue
        End If
        If color_str = "Color [MistyRose]" Then
            Return Color.MistyRose
        End If
        If color_str = "Color [NavajoWhite]" Then
            Return Color.NavajoWhite
        End If
        If color_str = "Color [Navy]" Then
            Return Color.Navy
        End If
        If color_str = "Color [Olive]" Then
            Return Color.Olive
        End If
        If color_str = "Color [OliveDrab]" Then
            Return Color.OliveDrab
        End If
        If color_str = "Color [Orange]" Then
            Return Color.Orange
        End If
        If color_str = "Color [Coral]" Then
            Return Color.Coral
        End If
        If color_str = "Color [Orchid]" Then
            Return Color.Orchid
        End If
        If color_str = "Color [PaleGreen]" Then
            Return Color.PaleGreen
        End If
        If color_str = "Color [PaleTurquoise]" Then
            Return Color.PaleTurquoise
        End If
        If color_str = "Color [PaleVioletRed]" Then
            Return Color.PaleVioletRed
        End If
        If color_str = "Color [PeachPuff]" Then
            Return Color.PeachPuff
        End If
        If color_str = "Color [Peru]" Then
            Return Color.Peru
        End If
        If color_str = "Color [Pink]" Then
            Return Color.Pink
        End If
        If color_str = "Color [Plum]" Then
            Return Color.Plum
        End If
        If color_str = "Color [PowderBlue]" Then
            Return Color.PowderBlue
        End If
        If color_str = "Color [Purple]" Then
            Return Color.Purple
        End If
        If color_str = "Color [Red]" Then
            Return Color.Red
        End If
        If color_str = "Color [RosyBrown]" Then
            Return Color.RosyBrown
        End If
        If color_str = "Color [RoyalBlue]" Then
            Return Color.RoyalBlue
        End If
        If color_str = "Color [SaddleBrown]" Then
            Return Color.SaddleBrown
        End If
        If color_str = "Color [Salmon]" Then
            Return Color.Salmon
        End If
        If color_str = "Color [SeaGreen]" Then
            Return Color.SeaGreen
        End If
        If color_str = "Color [Sienna]" Then
            Return Color.Sienna
        End If
        If color_str = "Color [Silver]" Then
            Return Color.Silver
        End If
        If color_str = "Color [SkyBlue]" Then
            Return Color.SkyBlue
        End If
        If color_str = "Color [SlateBlue]" Then
            Return Color.SlateBlue
        End If
        If color_str = "Color [SlateGray]" Then
            Return Color.SlateGray
        End If
        If color_str = "Color [SpringGreen]" Then
            Return Color.SpringGreen
        End If
        If color_str = "Color [SteelBlue]" Then
            Return Color.SteelBlue
        End If
        If color_str = "Color [Tan]" Then
            Return Color.Tan
        End If
        If color_str = "Color [Teal]" Then
            Return Color.Teal
        End If
        If color_str = "Color [Thistle]" Then
            Return Color.Thistle
        End If
        If color_str = "Color [Tomato]" Then
            Return Color.Tomato
        End If
        If color_str = "Color [Turquoise]" Then
            Return Color.Turquoise
        End If
        If color_str = "Color [Violet]" Then
            Return Color.Violet
        End If
        If color_str = "Color [Wheat]" Then
            Return Color.Wheat
        End If
        If color_str = "Color [Yellow]" Then
            Return Color.Yellow
        End If
        If color_str = "Color [Black]" Then
            Return Color.Black
        End If
        Return Color.White
    End Function
    Public Function Int2Brushes(ByVal x As Integer) As Brush
        If x = -1 Then
            Return Globe_Brush(111)
        End If
        Return Globe_Brush(x Mod 111)
    End Function
    Public Sub SetBrushes(ByVal x As Integer, ByVal b As Brush)
        If x = -1 Then
            Globe_Brush(111) = b
        Else
            Globe_Brush(x Mod 111) = b
        End If
    End Sub
    Public Sub SetColor(ByVal x As Integer, ByVal b As Color)
        If x = -1 Then
            Globe_Color(111) = b
        Else
            Globe_Color(x Mod 111) = b
        End If
    End Sub
    Public Function Int2Color(ByVal x As Integer) As Color
        If x = -1 Then
            Return Globe_Color(111)
        End If
        Return Globe_Color(x Mod 111)
    End Function
End Module
