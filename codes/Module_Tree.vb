Module Module_Tree
	Public Function differ_array(ByRef a() As String, ByRef b() As String) As Integer()
		Dim r() As Integer = {0, 0, 0}
		For Each i As String In a
			If Array.IndexOf(b, i) >= 0 Then
				r(1) += 1
			End If
		Next
		r(0) = a.Length - r(1)
		r(2) = b.Length - r(1)
		Return r
	End Function

	Class Ploy_Tree
		Public Node_Number As Integer
		Public Taxon_Number As Integer
		Public Tree_Line As String
		Public Node_Count() As Integer '0
		Public Node_Terminal() As String '1
		Public Node_Children() As String '2
		Public Node_Chain() As String '3
		Public Node_Left() As Integer '4
		Public Node_Right() As Integer '5
		Public Node_Support_Value() As Double '6
		Public Node_Branch_Length() As Double '7
		Public Node_Total_Length() As Double '8
		Public Node_Parent() As Integer '9
		Public Node_Level_A() As Integer '10 terminal为1
		Public Node_Level_B() As Integer '10 root为1
		Public Node_Weight_A() As Single
		Public Node_Weight_B() As Single
		Public Chain_Sum_A As Single
		Public Chain_Sum_B As Single
        Public Tree_Export_Char() As String
        Public Time_Maximum As Double
        Public Terminal_Branch_Length() As Double '0
        Public Terminal_Total_Length() As Double '1
        Public Terminal_Chain() As String '2

        Public Has_Length As Boolean = False

		Sub New(ByVal Tree_String As String)
			Try
				If Tree_String.EndsWith(";") Then
					Tree_String.Remove(Tree_String.Length - 1)
				End If
				Me.Tree_Line = Tree_String
				Me.Node_Number = Tree_String.Split("(").Length - 1
				Me.Taxon_Number = Tree_String.Split(",").Length
				ReDim Me.Node_Count(Me.Node_Number - 1) '0
				ReDim Me.Node_Terminal(Me.Node_Number - 1) '1
				ReDim Me.Node_Children(Me.Node_Number - 1) '2
				ReDim Me.Node_Chain(Me.Node_Number - 1) '3
				ReDim Me.Node_Left(Me.Node_Number - 1) '4
				ReDim Me.Node_Right(Me.Node_Number - 1) '5
				ReDim Me.Node_Support_Value(Me.Node_Number - 1) '6
				ReDim Me.Node_Branch_Length(Me.Node_Number - 1) '7
				ReDim Me.Node_Total_Length(Me.Node_Number - 1) '8
				ReDim Me.Node_Parent(Me.Node_Number - 1) '9
				ReDim Me.Node_Level_A(Me.Node_Number - 1) '10
				ReDim Me.Node_Level_B(Me.Node_Number - 1) '10
				ReDim Me.Node_Weight_A(Me.Node_Number - 1)
				ReDim Me.Node_Weight_B(Me.Node_Number - 1)

				'0 物种总数量,1 末端, 2 子节点, 3 全部链, 4 左侧个数, 5 右侧个数, 6 支持率,7 枝长, 8 总长, 9 祖先节点, 10 级别
				ReDim Terminal_Branch_Length(Me.Taxon_Number - 1) '0
				ReDim Terminal_Total_Length(Me.Taxon_Number - 1) '1
				ReDim Terminal_Chain(Me.Taxon_Number - 1) '2

				If Me.Tree_Line.Contains(":") Then
					Me.Has_Length = True
				End If

				For i As Integer = 0 To Me.Node_Number - 1
					Me.Node_Count(i) = 0
					Me.Node_Terminal(i) = ""
					Me.Node_Children(i) = ""
					Me.Node_Chain(i) = ""
					Me.Node_Left(i) = 0
					Me.Node_Right(i) = 0
					Me.Node_Support_Value(i) = 0
					Me.Node_Branch_Length(i) = 0
					Me.Node_Total_Length(i) = 0
					Me.Node_Parent(i) = -1
					Me.Node_Level_A(i) = 0
					Me.Node_Level_B(i) = 0
				Next
				Dim tree_char() As String
				ReDim tree_char(Me.Taxon_Number * 7)
				Dim char_id As Integer = 0
				Dim l_c As Integer = 0
				Dim r_c As Integer = 0
				Dim tx As Integer = 0
				Dim last_symb As Boolean = True
				For Each i As Char In Me.Tree_Line
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
				ReDim Tree_Export_Char(char_id)
				For i As Integer = 1 To char_id
					Tree_Export_Char(i) = tree_char(i)
				Next
				If Has_Length Then
					Dim count As Integer = 0
					For i As Integer = 1 To char_id
						If Tree_Export_Char(i).Contains(":") Then
							If Tree_Export_Char(i - 1) <> ")" Then
								'物种
								Terminal_Branch_Length(CInt(tree_char(i).Split(New Char() {":"c})(0)) - 1) = tree_char(i).Split(New Char() {":"c})(1)
								count += 1
								tree_char(i) = tree_char(i).Split(New Char() {":"c})(0)
							End If
						End If
					Next
				Else
					For i As Integer = 0 To Me.Taxon_Number - 1
						Terminal_Branch_Length(i) = 1
					Next
				End If


				Dim point_1, point_2 As Integer
				point_1 = 0
				point_2 = 0

				Dim temp_node_pos() As Integer '0
				Dim temp_node_terminal() As String '1
				Dim temp_node_children() As String '2
				Dim temp_node_left() As Integer '4
				Dim temp_node_right() As Integer '5

				ReDim temp_node_pos(Me.Node_Number) '1
				ReDim temp_node_terminal(Me.Node_Number) '1
				ReDim temp_node_children(Me.Node_Number) '2
				ReDim temp_node_left(Me.Node_Number) '4
				ReDim temp_node_right(Me.Node_Number) '5
				'0 节点位置,1 末端, 2 子节点, 4 左侧个数, 5 右侧个数
				For i As Integer = 0 To Me.Node_Number - 1
					temp_node_pos(i) = 0
					temp_node_terminal(i) = ""
					temp_node_children(i) = ""
					temp_node_left(i) = 32768
					temp_node_right(i) = 0
				Next
				For i As Integer = 1 To char_id
					Select Case tree_char(i)
						Case "("
							l_c += 1
							temp_node_pos(point_1) = i
							point_1 += 1
						Case ")"
							r_c += 1
							Node_Terminal(point_2) = temp_node_terminal(point_1 - 1)
							Node_Children(point_2) = temp_node_children(point_1 - 1)
							Node_Left(point_2) = temp_node_left(point_1 - 1)
							Node_Right(point_2) = temp_node_right(point_1 - 1)
							If Node_Children(point_2) = "" Then
								Node_Level_A(point_2) = 1
							Else
								For Each k As String In Node_Children(point_2).Split(",")
									If k <> "" Then
										If Node_Level_A(k) >= Node_Level_A(point_2) Then
											Node_Level_A(point_2) = Node_Level_A(k) + 1
										End If
									End If
								Next
							End If
							Node_Level_B(point_2) = l_c - r_c + 1
							For j As Integer = temp_node_pos(point_1 - 1) To i
								If tree_char(j) <> "(" And tree_char(j) <> ")" Then
									If tree_char(j) <> "," Then
										If tree_char(j - 1) <> ")" Then
											Node_Chain(point_2) += tree_char(j)
										End If
									Else
										Node_Chain(point_2) += tree_char(j)
									End If
								End If
							Next
							If point_1 > 1 Then
								temp_node_children(point_1 - 2) = point_2.ToString + "," + temp_node_children(point_1 - 2)
								temp_node_left(point_1 - 2) = Math.Min(temp_node_left(point_1 - 2), (Node_Right(point_2) + Node_Left(point_2)) / 2)
								temp_node_right(point_1 - 2) = Math.Max(temp_node_right(point_1 - 2), (Node_Right(point_2) + Node_Left(point_2)) / 2)
							End If
							point_2 += 1
							point_1 -= 1
							temp_node_pos(point_1) = 0
							temp_node_terminal(point_1) = ""
							temp_node_children(point_1) = ""
							temp_node_left(point_1) = 32768
							temp_node_right(point_1) = 0
						Case ","

						Case Else
							If tree_char(i - 1) = ")" Then
								'读取支持率
								If Has_Length And tree_char(i).Contains(":") Then
									If Val(tree_char(i).Split(New Char() {":"c})(0)) > 1 Then
										Node_Support_Value(point_2 - 1) = Val(tree_char(i).Split(New Char() {":"c})(0)) / 100
									Else
										Node_Support_Value(point_2 - 1) = Val(tree_char(i).Split(New Char() {":"c})(0))
									End If
									Node_Branch_Length(point_2 - 1) = Val(tree_char(i).Split(New Char() {":"c})(1))
								Else
									If Val(tree_char(i)) > 1 Then
										Node_Support_Value(point_2 - 1) = Val(tree_char(i)) / 100
									Else
										Node_Support_Value(point_2 - 1) = Val(tree_char(i))
									End If
								End If
							Else
								tx += 1
								temp_node_terminal(point_1 - 1) += tree_char(i) + ","
								temp_node_left(point_1 - 1) = Math.Min(temp_node_left(point_1 - 1), tx)
								temp_node_right(point_1 - 1) = Math.Max(temp_node_left(point_1 - 1), tx)
							End If
					End Select
				Next
				If Has_Length = False Then
					For i As Integer = 0 To Me.Node_Number - 1
						Node_Branch_Length(i) = 1
					Next
				End If
				make_chain(Me.Node_Number - 1, "")

				Time_Maximum = 0
				For i As Integer = 0 To Me.Taxon_Number - 1
					If Time_Maximum < Terminal_Total_Length(i) Then
						Time_Maximum = Terminal_Total_Length(i)
					End If
				Next
				For i As Integer = 0 To Me.Node_Number - 1
					If Me.Node_Children(i) <> "" Then
						Dim anc_node() As String = Me.Node_Children(i).Split(New Char() {","c})

						For Each j As String In anc_node
							If j <> "" Then
								Node_Parent(CInt(j)) = i
							End If
						Next
					End If
				Next
				If Time_Maximum <= 0 Then
					Has_Length = False
				End If
				Dim max_level_A As Integer = 0
				For i As Integer = 0 To Node_Number - 1
					If max_level_A < Node_Level_A(i) Then
						max_level_A = Node_Level_A(i)
					End If
				Next
				Dim max_level_B As Integer = 0
				For i As Integer = 0 To Node_Number - 1
					If max_level_B < Node_Level_B(i) Then
						max_level_B = Node_Level_B(i)
					End If
				Next
				For i As Integer = 0 To Node_Number - 1
					Dim temp_list() As String = Node_Chain(i).Split(",")
					Array.Sort(temp_list)
					Node_Count(i) = temp_list.Length
					Node_Chain(i) = Join(temp_list, ",")
					Node_Weight_A(i) = Node_Count(i) / taxon_num * Node_Level_A(i) / max_level_A
					Node_Weight_B(i) = Node_Count(i) / Node_Level_B(i)
					Chain_Sum_A += Node_Weight_A(i)
					Chain_Sum_B += Node_Weight_B(i)
				Next
			Catch ex As Exception
				MsgBox(ex.ToString)
			End Try

		End Sub
        Public Sub make_chain(ByVal n As Integer, ByVal node_chain As String)
            node_chain = "," + n.ToString + node_chain
            If Node_Children(n) <> "" Then
                Dim anc_node() As String = Node_Children(n).Split(New Char() {","c})
                For Each j As String In anc_node
                    If j <> "" Then
                        Node_Total_Length(CInt(j)) = Node_Branch_Length(CInt(j)) + Node_Total_Length(n)

                        make_chain(CInt(j), node_chain)
                    End If
                Next
            End If
            If Node_Terminal(n) <> "" Then
                Dim anc_node() As String = Node_Terminal(n).Split(New Char() {","c})
                For Each j As String In anc_node
                    If j <> "" Then
                        Terminal_Total_Length(CInt(j) - 1) = Terminal_Branch_Length(CInt(j) - 1) + Node_Total_Length(n)
                        Terminal_Chain(CInt(j) - 1) = node_chain
                    End If
                Next
            End If
        End Sub

    End Class

End Module
