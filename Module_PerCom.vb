Public Class Module_PerCom
    Private n As Int64, m As Int64
    Private pNum As Int64
    Private used() As Int64
    Private p() As String
    Private Data() As String
    Private PData() As String
    '排列组合   
    Public Function Permute(ByVal vData As Object, ByVal iPm As Int64) As Object
        Data = vData
        n = UBound(vData) - LBound(vData) + 1
        If iPm <= n Then
            m = iPm
        Else
            m = n
        End If

        ReDim used(n - 1)
        ReDim p(m - 1)

        pNum = 0
        ReDim PData(Pnm(n, m) - 1)

        Permute0(0)

        Return PData
    End Function

    '组合   
    Public Function Combine(ByVal vData As Object, ByVal iPm As Int64) As Object

        Data = vData
        n = UBound(vData) - LBound(vData) + 1

        If iPm <= n Then
            m = iPm
        Else
            m = n
        End If

        ReDim used(n - 1)
        ReDim p(m - 1)

        pNum = 0
        ReDim PData(Cnm(n, m) - 1)
        Combine0(0, 0)
        Return PData
    End Function


    '   permute(pos   --   表示在解空间中填写数据的下标位置)   
    '{               如果解空间填写满了   打印解空间当前的排列结果   函数返回   
    '       for   (i=0;   i<n;   i++)   --   n是待排列数据总数   
    '       {   
    '               尝试在这个下标位置填写每一个待排列的数据   
    '               (但这些数据可填写的前提是数据没有被标记为已使用)   
    '   
    '       填写后,   把这个下标为i的数据标记为已使用   '   
    '       permute(pos+1);   --   填写解空间中下一个位置   '   
    '       下标为i的数据已参与了解空间下标pos处的排列   '   
    '取消已使用标记(因为该数据可以在解空间其他下标处使用)   
    '       继续for循环考察下一个待排列数据   
    '       }   
    '   }   
    '   
    '   used[i]   ==   1   -   待排列空间中下标i处的数据已被使用;   
    '   used[i]   ==   0   -   可以使用待排列空间中下标i处的数据;   

    Private Sub Permute0(ByVal pos As Int64)
        Dim i As Int64
        If pos = m Then
            For i = 0 To m - 1
                PData(pNum) = PData(pNum) & p(i)
            Next
            pNum = pNum + 1
            Exit Sub
        End If
        For i = 0 To n - 1
            If used(i) = 0 Then
                used(i) = used(i) + 1
                p(pos) = Data(i)
                Permute0(pos + 1)
                used(i) = used(i) - 1
            End If
        Next i

    End Sub

    '组合   
    'idx--记录下标pos处的i的位置   
    Private Sub Combine0(ByVal pos As Int64, ByVal idx As Int64)
        Dim i As Int64
        If pos = m Then
            For i = 0 To m - 1
                PData(pNum) = PData(pNum) & p(i)
            Next
            pNum = pNum + 1
            Exit Sub
        End If
        For i = idx To n - 1
            If used(i) = 0 Then
                used(i) = used(i) + 1
                p(pos) = Data(i)
                Combine0((pos + 1), i)
                used(i) = used(i) - 1
            End If
        Next i

    End Sub

    '计算排列的个数   
    'n*(n-1)*(n-2)*...*(n-m+1)   m个   
    Public Function Pnm(ByVal n As Int64, ByVal m As Int64) As Int64
        If m > n Then
            m = n
        End If
        If m = 0 Then
            Pnm = 1
            Exit Function
        Else
            Pnm = n * Pnm(n - 1, m - 1)
        End If

    End Function

    '计算组合的个数   
    Public Function Cnm(ByVal n As Int64, ByVal m As Int64) As Int64
        If m > n Then
            m = n
        End If
        Cnm = Pnm(n, m) / Pnm(m, m)

    End Function
End Class
Public Class scomparer
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Return New CaseInsensitiveComparer().Compare(y, x)
    End Function
End Class
Public Class lcomparer
    Implements IComparer
    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Return New CaseInsensitiveComparer().Compare(x, y)
    End Function
End Class

