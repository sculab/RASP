Imports System.IO
Module Module_BBM
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
        If line.Split(New Char() {"	"c})(2).ToLower = "lnpr" Then
            withG += 1
        End If
        If line.Split(New Char() {"	"c})(5 + withG).ToLower = "alpha" Then
            withG += 1
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
End Module
