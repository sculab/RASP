Imports System.IO
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Globalization.CultureInfo
Module Module_Var
    Public Version As String = "4.1.3"
    Public build As String = "20190624"
    Public enableMin As Boolean = True
    Public isDebug As Boolean = False
    Public ci As System.Globalization.CultureInfo = New System.Globalization.CultureInfo("en-us")
#Const TargetOS = "win32"
#If TargetOS = "linux" Then
    Public TargetOS As String = "linux"
#ElseIf TargetOS = "macos" Then
    Public TargetOS As String = "macos"
#ElseIf TargetOS = "win32" Then
    Public TargetOS As String = "win32"
#End If

    Public first_open(6) As Boolean
    Public error_no As Integer
    Public error_msg As String
    Public path_char As String
    Public root_path As String
    Public lib_path As String
    Public sdec_lg As String
    Public current_dir As String
    Public final_tree As String = ""
    Public tree_wo_value As String = ""
    Public MainWindow As New Form_Main
    Public Lagrange_Config As New Config_Lagrange
    Public BGB_Config As New Config_BGB
    Public DPPdiv_Config As New Tool_DPP
    Public DIVAForm As New Config_SDIVA
    Public RangeMade As Boolean = False
    Public Lag_con_made As Boolean = False
    Public BGB_con_made As Boolean = False
    Public RangeStr As String = ""
    Public BayesForm As New Config_BBM
    Public TracerForm As New View_Tracer
    Public CombineForm As New Tool_Combine
    Public SvTForm As New Tool_SvT
    Public BayAreaForm As New Config_BayArea
    Public TraitsForm As New Config_Traits
    Public ChromForm As New Config_Chrom
    Public OptionForm As New View_OptionForm
    Public TraitsView As New View_Dis
    Public ClusterForm As Boolean = False
    Public state_mode As Integer = 0
    Public Config_BayArea_Burnin As Integer = 0
    Public taxon_num As Integer
    Public terminal_num As Integer
    Public tree_show As String
    Public tree_show_with_value As String
    Public dtView As New DataView
    Public orgView As New DataView
    Public nodeView As New DataView
    Public particular_node_num As String = ""
    Public omittedtree As String
    Public node_probability() As Single
    Public Poly_Node(,) As String
    Public PHYLIP_node() As String
    Public bayesIsrun As Boolean = False
    Public show_pie As String
    Public pars_run As Boolean = False
    Public bayes_gen As Integer = 0
    Public bayarea_gen As Integer = 0
    Public dpp_gen As Double = 0
    Public lag_gen As Integer
    Public BGB_gen As Integer
    Public BGB_mode As Integer = 0
    Public muti_threads_BGB As Integer = 1
    Public muti_threads_DEC As Integer = 1
    Public muti_threads_DIVA As Integer = 1
    Public DIVA_mode As Integer = 0
    Public diva_gen As Integer = 0
    Public rscript As String = ""
    Public PV_SUM As Integer = 0
    Public cons_tre As Integer = 0
    Public OFD As New OpenFileDialog
    Public Process_ID As Integer = -1
    Public Process_Gen As String
    Public Process_Gen1 As String
    Public Process_Text As String
    Public Process_Int As Integer
    Public Dec_Sym As Char
    Public mrbayes_tree As Boolean = False
    Public Select_Node_list() As String
    Public tree_view_title As String
    Public config_tree_time As Single
    Public config_lagrange_cycle As Single = 100
    Public config_lagrange_presision As Single = 0.0001
    Public config_BayArea_cycle As Single
    Public config_BayArea_fre As Single
    Public config_SDIVA_node As String
    Public config_SDIVA_omitted As String
    Public CharMatrix() As String, TaxonList() As String
    Public excludeline As String
    Public fossilline As String

    Public state_index As Integer = 2
    Public state_header As String = ""

    Public Function ToVal(ByVal str As String) As Single
        Return Val(str.Replace(Dec_Sym, "."))
    End Function
    Public Function sum(ByVal input() As Integer) As Integer
        Dim r As Integer = 0
        For Each i As Integer In input
            r += i
        Next
        Return r
    End Function
    Public Sub format_path()
        Select Case TargetOS
            Case "linux"
                path_char = "/"
            Case "win32", "macos"
                path_char = "\"
            Case Else
                path_char = "\"
        End Select
        For i As Integer = 0 To 6
            first_open(i) = True
        Next
        root_path = (Application.StartupPath + path_char).Replace(path_char + path_char, path_char)
        lib_path = root_path.Replace("\", "/")
        Dec_Sym = CInt("0").ToString("F1").Replace("0", "")
        If Dec_Sym <> "." Then
            MsgBox("Notice: RASP will use dat (.) as decimal quotation instead of comma (,). We recommand to change your system's number format to English! ")
        End If
    End Sub
    Public Sub DeleteFiles(ByVal aimPath As String, ByVal ext As String)
        If (aimPath(aimPath.Length - 1) <> Path.DirectorySeparatorChar) Then
            aimPath += Path.DirectorySeparatorChar
        End If  '判断待删除的目录是否存在,不存在则退出.  
        If (Not Directory.Exists(aimPath)) Then Exit Sub ' 
        Dim fileList() As String = Directory.GetFiles(aimPath)  ' 遍历所有的文件和目录  
        For Each FileName As String In fileList
            Try
                If FileName.ToUpper.EndsWith(ext.ToUpper) Then
                    File.Delete(aimPath + Path.GetFileName(FileName))
                End If
            Catch ex As Exception
            End Try
        Next
    End Sub
    Public Function Count_Files(ByVal aimPath As String, ByVal ext As String) As Integer
        Dim count As Integer = 0
        If (aimPath(aimPath.Length - 1) <> Path.DirectorySeparatorChar) Then
            aimPath += Path.DirectorySeparatorChar
        End If  '判断待删除的目录是否存在,不存在则退出.  
        If (Not Directory.Exists(aimPath)) Then Exit Function ' 
        Dim fileList() As String = Directory.GetFiles(aimPath)  ' 遍历所有的文件和目录  
        For Each FileName As String In fileList
            Try
                If FileName.ToUpper.EndsWith(ext.ToUpper) Then
                    count += 1
                End If
            Catch ex As Exception
            End Try
        Next
        Return count
    End Function
    Public Function format_time(ByVal time As String) As String
        Dim temp_time As String = ""
        For Each i As Char In time
            If IsNumeric(i) Then
                temp_time = temp_time + i.ToString
            End If
        Next
        Return temp_time
    End Function
    Public Function Distributiton_to_Binary(ByVal D_str As String, ByVal Length As Integer) As String
        Dim B_str() As Char
        ReDim B_str(Length - 1)
        For i As Integer = 0 To Length - 1
            B_str(i) = "0"
        Next
        If D_str <> "/" Then
            For Each i As Char In D_str.ToUpper
                B_str(AscW(i) - AscW("A")) = "1"
            Next
        End If

        Return B_str
    End Function
    Public Sub DeleteDir(ByVal aimPath As String)
        If (aimPath(aimPath.Length - 1) <> Path.DirectorySeparatorChar) Then
            aimPath += Path.DirectorySeparatorChar
        End If  '判断待删除的目录是否存在,不存在则退出.  
        If (Not Directory.Exists(aimPath)) Then Exit Sub ' 
        Dim fileList() As String = Directory.GetFileSystemEntries(aimPath)  ' 遍历所有的文件和目录  
        For Each FileName As String In fileList
            If (Directory.Exists(FileName)) Then  ' 先当作目录处理如果存在这个目录就递归
                DeleteDir(aimPath + Path.GetFileName(FileName))
            Else  ' 否则直接Delete文件  
                Try
                    File.Delete(aimPath + Path.GetFileName(FileName))
                Catch ex As Exception
                End Try
            End If
        Next  '删除文件夹  
    End Sub
    Public Sub delete_temp_file(ByVal file_name As String)
        If File.Exists(root_path + "temp\" + file_name) Then
            File.Delete(root_path + "temp\" + file_name)
        End If
    End Sub
    Public Sub Read_Poly_Node(ByVal Treeline As String)
        Dim NumofNode As Integer = Treeline.Length - Treeline.Replace("(", "").Length
        ReDim Poly_Node(NumofNode, 7) '0 root,1 末端, 2 子节点, 4,枝长,6,支持率, 3 全部链, 7 PHYLIP NODE
        For i As Integer = 0 To NumofNode
            Poly_Node(i, 0) = ""
            Poly_Node(i, 1) = ""
            Poly_Node(i, 2) = ""
            Poly_Node(i, 3) = ""
            Poly_Node(i, 6) = "1.00"
        Next
        Dim tree_char() As String
        ReDim tree_char(NumofNode * 7)
        Dim char_id As Integer = 0
        Dim l_c As Integer = 0
        Dim r_c As Integer = 0
        Dim dh As Integer = 0
        Dim last_symb As Boolean = True
        For Each i As Char In Treeline
            Select Case i
                Case "("
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True

                Case ")"
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True
                Case ","
                    char_id += 1
                    tree_char(char_id) = i
                    last_symb = True
                Case Else
                    If last_symb Then
                        char_id += 1
                        tree_char(char_id) = i
                        last_symb = False
                    Else
                        tree_char(char_id) += i
                    End If
            End Select
        Next
        For i As Integer = 1 To char_id
            If tree_char(i).Contains(":") Then
                tree_char(i) = tree_char(i).Split(New Char() {":"c})(0)
            End If
        Next
        Dim point_1, point_2 As Integer
        point_1 = 0
        point_2 = 0
        Dim Temp_node(,) As String
        ReDim Temp_node(NumofNode, 7) '0 节点位置,1 末端 2, 子节点, 7 phylip 
        For i As Integer = 1 To char_id
            Select Case tree_char(i)
                Case "("
                    l_c += 1
                    Temp_node(point_1, 0) = i
                    Temp_node(point_1, 7) = l_c
                    point_1 += 1
                Case ")"
                    r_c += 1
                    Poly_Node(point_2, 1) = Temp_node(point_1 - 1, 1)
                    Poly_Node(point_2, 2) = Temp_node(point_1 - 1, 2)
                    Poly_Node(point_2, 7) = Temp_node(point_1 - 1, 7)
                    For j As Integer = Temp_node(point_1 - 1, 0) To i
                        If tree_char(j) <> "(" And tree_char(j) <> ")" Then
                            If tree_char(j) <> "," Then
                                If tree_char(j - 1) <> ")" Then
                                    Poly_Node(point_2, 3) += tree_char(j)
                                End If
                            Else
                                Poly_Node(point_2, 3) += tree_char(j)
                            End If
                        End If
                    Next
                    If point_1 > 1 Then
                        Temp_node(point_1 - 2, 2) = point_2.ToString + "," + Temp_node(point_1 - 2, 2)
                    Else
                        Poly_Node(point_2, 0) = "-1"
                    End If
                    point_2 += 1
                    point_1 -= 1
                    Temp_node(point_1, 0) = ""
                    Temp_node(point_1, 1) = ""
                    Temp_node(point_1, 2) = ""
                    Temp_node(point_1, 3) = ""
                Case ","
                Case Else
                    If tree_char(i - 1) = ")" Then
                        '读取支持率
                        If Val(tree_char(i)) > 1 Then
                            Poly_Node(point_2 - 1, 6) = (Val(tree_char(i)) / 100).ToString("F2")
                        Else
                            Poly_Node(point_2 - 1, 6) = Val(tree_char(i)).ToString("F2")
                        End If
                    Else
                        Temp_node(point_1 - 1, 1) += tree_char(i) + ","
                    End If

            End Select
        Next
        For i As Integer = 0 To NumofNode
            If Poly_Node(i, 2) <> "" Then
                Dim anc_node() As String = Poly_Node(i, 2).Split(New Char() {","c})
                For Each j As String In anc_node
                    If j <> "" Then
                        Poly_Node(CInt(j), 0) = i.ToString
                    End If
                Next
            End If
        Next
    End Sub
    Public Function binary_to_dis(ByVal binary As String) As String
        Dim dis As String = ""
        For i As Integer = 0 To binary.Length - 1
            If binary.Chars(i).ToString <> "0" Then
                dis += ChrW(65 + i)
            End If
        Next
        Return dis
    End Function
    Public Function sort_str(ByVal str As String) As String
        Dim tmp() As Char = str
        Array.Sort(tmp)
        Return tmp
    End Function
    Public Function cal_sd(ByVal single_array() As Single) As Single
        Dim u As Single = 0
        Dim m As Single = 0
        Dim n As Integer = single_array.Length
        For Each i As Single In single_array
            m += i
        Next
        m = m / n
        For Each i As Single In single_array
            u += (i - m) ^ 2
        Next
        u = (u / (n - 1)) ^ 0.5
        Return u
    End Function
    '节点编号转换

    Public Function Left_to_right(ByVal left_id As Integer, ByVal tree_line As String) As Integer
        Dim t_num As Integer = tree_line.Length - tree_line.Replace(",", "").Length + 1
        left_id = left_id - t_num
        Dim t1 As Integer = 0
        Dim t2 As Integer = 0
        Dim t3 As Integer = 0
        Dim t_c As Integer = 0
        For i As Integer = 0 To tree_line.Length - 1
            If tree_line.Chars(i) = ")" Then
                t1 += 1
            End If
            If tree_line.Chars(i) = "(" Then
                t2 += 1
            End If
            t_c += 1
            If t2 = left_id Then
                Exit For
            End If
        Next
        For i As Integer = t_c - 1 To tree_line.Length - 1
            If tree_line.Chars(i) = "(" Then
                t3 += 1
            End If
            If tree_line.Chars(i) = ")" Then
                t3 -= 1
                t1 += 1
            End If
            If t3 = 0 Then
                Exit For
            End If
        Next
        Return t_num + t1
    End Function
End Module
