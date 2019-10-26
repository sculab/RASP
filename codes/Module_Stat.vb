Module Module_Stat
	Function COUNT(ByVal n() As Single) As Integer
		COUNT = UBound(n) - LBound(n) + 1
	End Function

	'对n()求和
	Function SUM_DOUBLE(ByVal n() As Single) As Single
		Dim S As Single, I As Integer
		S = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		SUM_DOUBLE = S
	End Function

    '对n()求平均值
    Function AVERAGE(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single, I As Integer
        S = 0
        For I = lb To UBound(n)
            S = S + n(I)
        Next
        AVERAGE = S / (UBound(n) - lb + 1)
    End Function

    Function AVERAGE_positive(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single, I As Integer, C As Integer
        S = 0
        C = 0
        For I = lb To UBound(n)
            If n(I) <> 1 / 0 Then
                C += 1
                S = S + n(I)
            End If

        Next
        Return S / C
    End Function


    '对n()剔除异常后求平均值
    Function TRIMMEAN(ByVal n() As Single) As Single
		Dim Min, Max, S As Single, I As Integer
		Min = n(0)
		Max = Min
		S = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
			If n(I) > Max Then
				Max = n(I)
			ElseIf n(I) < Min Then
				Min = n(I)
			End If
		Next
		TRIMMEAN = (S - Max - Min) / (UBound(n) - LBound(n) - 1)
	End Function

	'对n()求几何平均值
	Function GEOMEAN(ByVal n() As Single) As Single
		Dim S As Single, I As Integer
		S = 1
		For I = LBound(n) To UBound(n)
			S = S * n(I)
		Next
		GEOMEAN = Math.Exp(1 / ((UBound(n) - LBound(n) + 1)) * Math.Log(S))
	End Function

	'对n()求中位数
	Function MEDIAN(ByVal n() As Single) As Single
		Dim S As Single, I As Integer
		Array.Sort(n)
		I = UBound(n) - LBound(n) + 1
		If I Mod 2 = 0 Then
			S = (n(I / 2) + n(I / 2 - 1)) / 2
		Else
			S = n((I - 1) / 2)
		End If
		MEDIAN = S
	End Function

    '对n()求最大值
    Function matrix_Max(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single
        Dim I As Integer
        S = n(lb)
        For I = lb To UBound(n)
            If n(I) > S Then
                S = n(I)
            End If
        Next
        Return S
    End Function

    '对n()求最小值
    Function matrix_Min(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single
        Dim I As Integer
        S = n(lb)
        For I = lb To UBound(n)
            If n(I) < S Then
                S = n(I)
            End If
        Next
        Return S
    End Function

    Function matrix_Max_positive(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single
        Dim I As Integer
        S = n(lb)
        For I = lb To UBound(n)
            If n(I) > S And n(I) <> 1 / 0 Then
                S = n(I)
            End If
        Next
        Return S
    End Function

    '对n()求最小值
    Function matrix_Min_positive(ByVal n() As Single, Optional ByVal lb As Integer = 0) As Single
        Dim S As Single
        Dim I As Integer
        S = n(lb)
        For I = lb To UBound(n)
            If n(I) < S And n(I) <> 1 / 0 Then
                S = n(I)
            End If
        Next
        Return S
    End Function
    '对n()求方差
    Function VARP(ByVal n() As Single) As Single
		Dim S, T As Single, I As Integer, average As Single
		S = 0
		T = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		average = S / (UBound(n) - LBound(n) + 1)
		For I = LBound(n) To UBound(n)
			T = (n(I) - average) * (n(I) - average) + T
		Next
		VARP = T / (UBound(n) - LBound(n) + 1)
	End Function

	'对n()求无偏方差
	Function VAR(ByVal n() As Single) As Single
		Dim S, T As Single, I As Integer, average As Single
		S = 0
		T = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		average = S / (UBound(n) - LBound(n) + 1)
		For I = LBound(n) To UBound(n)
			T = (n(I) - average) * (n(I) - average) + T
		Next
		VAR = T / (UBound(n) - LBound(n))
	End Function

	'对n()求标准差
	Function STDEVP(ByVal n() As Single) As Single
		Dim S, T As Single, I As Integer, average As Single
		S = 0
		T = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		average = S / (UBound(n) - LBound(n) + 1)
		For I = LBound(n) To UBound(n)
			T = T + (n(I) - average) * (n(I) - average)
		Next
		STDEVP = Math.Sqrt(T / (UBound(n) - LBound(n) + 1))
	End Function

	'对n()求标准偏差
	Function STDEV(ByVal n() As Single) As Single
		Dim S, T As Single, I As Integer, average As Single
		S = 0
		T = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		average = S / (UBound(n) - LBound(n) + 1)
		For I = LBound(n) To UBound(n)
			T = T + (n(I) - average) * (n(I) - average)
		Next
		STDEV = Math.Sqrt(T / (UBound(n) - LBound(n)))
	End Function

	'对n()求均值的绝对偏差的平均值
	Function ADEDEV(ByVal n() As Single) As Single
		Dim S, T As Single, I As Integer, average As Single
		S = 0
		T = 0
		For I = LBound(n) To UBound(n)
			S = S + n(I)
		Next
		average = S / (UBound(n) - LBound(n) + 1)
		For I = LBound(n) To UBound(n)
			T = T + Math.Abs(n(I) - average)
		Next
		ADEDEV = T / (UBound(n) - LBound(n) + 1)
	End Function

	'对n()求四分位数
	Function QUTER(ByVal arr() As Single) As Single()
		Array.Sort(arr)
		Dim n As Integer = arr.Length
		Dim b As Single = (n + 1) * 0.25
		Dim c As Integer = Int(b)
		Dim d As Single = b - c
		Dim Q(2) As Single
		Q(0) = arr(c - 1) + (arr(c) - arr(c - 1)) * d

		b = (n + 1) * 0.5
		c = Int(b)
		d = b - c
		Q(1) = arr(c - 1) + (arr(c) - arr(c - 1)) * d

		b = (n + 1) * 0.75
		c = Int(b)
		d = b - c
		Q(2) = arr(c - 1) + (arr(c) - arr(c - 1)) * d

		Return Q
	End Function

End Module
