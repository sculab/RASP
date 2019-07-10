Module Module_Functions
    '连接字符串
    Public Function join_array(ByRef my_array() As String, ByVal split As String) As String
        Dim result As String = my_array(0).ToString
        For i As Integer = 1 To UBound(my_array)
            result &= split & my_array(i).ToString
        Next
        Return result
    End Function
    Public Function join_array(ByRef my_array() As Single, ByVal split As String) As String
        Dim result As String = my_array(0).ToString
        For i As Integer = 1 To UBound(my_array)
            result &= split & my_array(i).ToString
        Next
        Return result
    End Function
    Public Function make_seq(ByVal n As Integer, ByVal start As Integer) As String()
        Dim r() As String
        ReDim r(n - 1)
        For i As Integer = 0 To n - 1
            r(i) = (start + i).ToString
        Next
        Return r
    End Function
    '比较两个字符串
    Public Function comp_list(ByVal str1 As String, ByVal str2 As String) As Integer
        Dim arr1() As String = str1.Split(",")
        Dim arr2() As String = str2.Split(",")
        Dim count As Integer = 0
        For Each i As String In arr1
            If Array.IndexOf(arr2, i) >= 0 Then
                count += 1
            End If
        Next
        Return arr1.Length + arr2.Length - 2 * count
    End Function
    '字符排序
    Public Function sort_str(ByVal str As String) As String
        Dim tmp() As Char = str
        Array.Sort(tmp)
        Return tmp
    End Function
    '对n()求标准差
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
End Module
