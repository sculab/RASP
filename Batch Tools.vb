Imports System.IO
Imports System.Threading
Public Class Batch_Tools

    Private Sub Batch_Tools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim se As DialogResult
        se = Me.FolderBrowserDialog1.ShowDialog()
        If se = DialogResult.OK Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub ListFilesToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListFilesToolStripMenuItem.Click
        ListBox1.Items.Clear
        ListFiles(TextBox1.Text)
    End Sub
    Private Sub ListFiles(ByVal From_dir As String)
        Try
            If Not (From_dir Is Nothing) Then
                Dim mFileInfo As System.IO.FileInfo
                Dim mDir As System.IO.DirectoryInfo
                Dim mDirInfo As New System.IO.DirectoryInfo(From_dir)
                For Each mFileInfo In mDirInfo.GetFiles()
                    If mFileInfo.Extension.ToUpper = TextBox2.Text.ToUpper Then
                        ListBox1.Items.Add(mFileInfo.FullName)
                    End If
                Next
                If CheckBox1.Checked Then
                    For Each mDir In mDirInfo.GetDirectories
                        ListFiles(mDir.FullName)
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Sub
    Public Function load_final_trees(ByVal tree_path As String) As String
        Dim rt As StreamReader
        Dim line As String = ""
        Dim tree_complete As String = ""
        Dim name_num As Integer = 0
        Try
            rt = New StreamReader(tree_path)
            line = rt.ReadLine
            Dim f_t_name(,) As String
            Do While line Is Nothing = False

                Do
                    If line.StartsWith("	") Or line.StartsWith(" ") Then
                        line = line.Remove(0, 1)
                    Else
                        Exit Do
                    End If
                Loop


                If line.Replace("	", "").Replace(" ", "").ToUpper.StartsWith("TRANSLATE") Then
                    line = rt.ReadLine.Replace("	", " ").Replace(",", "")

                    Do

                        If line.Length > 0 Then
                            Do
                                If line.StartsWith("	") Or line.StartsWith(" ") Then
                                    line = line.Remove(0, 1)
                                Else
                                    Exit Do
                                End If
                            Loop
                            Dim TRANSLATE() As String = line.Replace(";", "").Split(New Char() {" "c})
                            ReDim Preserve f_t_name(1, name_num)
                            f_t_name(0, name_num) = TRANSLATE(0)
                            f_t_name(1, name_num) = TRANSLATE(1).Replace("'", "")
                            name_num = name_num + 1
                        End If
                        line = rt.ReadLine.Replace("	", " ").Replace(",", "")
                    Loop Until line.Contains(";")
                    If line.Replace("	", "").Replace(" ", "").Length > 1 Then
                        Do
                            If line.StartsWith("	") Or line.StartsWith(" ") Then
                                line = line.Remove(0, 1)
                            Else
                                Exit Do
                            End If
                        Loop
                        Dim TRANSLATE() As String = line.Replace(";", "").Split(New Char() {" "c})
                        ReDim Preserve f_t_name(1, name_num)
                        f_t_name(0, name_num) = TRANSLATE(0)
                        f_t_name(1, name_num) = TRANSLATE(1).Replace("'", "")
                        name_num = name_num + 1
                        line = rt.ReadLine.Replace("	", " ")
                    End If
                End If

                If line.Replace("	", "").Replace(" ", "").ToUpper.StartsWith("TREE") Or (line.Replace("	", "").Replace(" ", "").StartsWith("(") And line.Replace("	", "").Replace(" ", "").EndsWith(";")) Then
                    Do While line.Contains(";") = False
                        Dim next_tree_line As String = rt.ReadLine
                        If next_tree_line <> "" Then
                            line = line + next_tree_line
                        End If
                    Loop
                    Dim tree_Temp As String = line.Substring(line.IndexOf("("), line.Length - line.IndexOf("("))
                    Dim tree_Temp1 As String = ""
                    Dim is_sym As Boolean = False
                    For Each tree_chr As Char In tree_Temp
                        If tree_chr = "[" Then
                            is_sym = True
                        End If
                        If tree_chr = "]" Then
                            is_sym = False
                        End If
                        If is_sym = False And tree_chr <> "]" Then
                            tree_Temp1 = tree_Temp1 + tree_chr.ToString
                        End If
                    Next
                    tree_complete = tree_Temp1

                    Dim isbase_three As Boolean = True
                    If tree_complete.Replace("(", "").Length - tree_complete.Replace(",", "").Length = 1 Then
                        Dim tree_char() As String
                        Dim tree_poly() As Char = tree_complete
                        ReDim tree_char(name_num * 4)
                        tree_complete = ""
                        Dim char_id As Integer = 0
                        Dim l_c As Integer = 0
                        Dim r_c As Integer = 0
                        Dim dh As Integer = 0
                        Dim last_symb As Boolean = True
                        For i As Integer = 0 To tree_poly.Length - 1
                            Select Case tree_poly(i)
                                Case "("
                                    char_id += 1
                                    tree_char(char_id) = tree_poly(i)
                                    last_symb = True

                                Case ")"
                                    char_id += 1
                                    tree_char(char_id) = tree_poly(i)
                                    last_symb = True
                                Case ","
                                    char_id += 1
                                    tree_char(char_id) = tree_poly(i)
                                    last_symb = True
                                Case Else
                                    If last_symb Then
                                        char_id += 1
                                        tree_char(char_id) = tree_poly(i)
                                        last_symb = False
                                    Else
                                        tree_char(char_id) += tree_poly(i)
                                    End If
                            End Select
                        Next
                        Dim three_clade_id(2) As Integer
                        three_clade_id(0) = 0
                        three_clade_id(1) = 0
                        three_clade_id(2) = 0
                        For i As Integer = 1 To tree_char.Length - 1
                            If tree_char(i) = "(" Then
                                l_c = l_c + 1
                            End If
                            If tree_char(i) = ")" Then
                                three_clade_id(2) = i
                            End If
                            If tree_char(i) = "," Then
                                dh = dh + 1
                            End If
                            If dh = l_c + 1 Then
                                If three_clade_id(1) = 0 Then
                                    dh = 0
                                    l_c = 0
                                End If
                                three_clade_id(1) = i
                            End If
                        Next
                        If dh <> l_c Then
                            isbase_three = False
                        End If
                        dh = 0
                        l_c = 0
                        For i As Integer = 0 To three_clade_id(2) - 1
                            If tree_char(three_clade_id(2) - i) = ")" Then
                                r_c = r_c + 1
                            End If
                            If tree_char(three_clade_id(2) - i) = "," Then
                                dh = dh + 1
                            End If
                            If dh = r_c + 1 Then
                                If three_clade_id(0) = 0 Then
                                    dh = 0
                                    r_c = 0
                                End If
                                three_clade_id(0) = three_clade_id(2) - i
                            End If
                        Next
                        If dh <> r_c Then
                            isbase_three = False
                        End If
                        dh = 0
                        r_c = 0
                        For i As Integer = 0 To three_clade_id(2) - 1
                            Select Case tree_char(i)
                                Case "("
                                    l_c += 1
                                Case ","
                                    dh = dh + 1
                                Case ")"
                                    r_c += 1
                                Case Else

                            End Select
                            If i = three_clade_id(0) Then
                                If l_c <> dh Or dh <> r_c + 1 Then
                                    isbase_three = False
                                End If
                            End If
                            If i = three_clade_id(1) Then
                                If l_c <> dh - 1 Or dh - 1 <> r_c + 1 Then
                                    isbase_three = False
                                End If
                            End If
                        Next
                        If isbase_three Then
                            If three_clade_id(2) - three_clade_id(1) <= three_clade_id(0) - 1 Then
                                If three_clade_id(2) - three_clade_id(1) <= three_clade_id(1) - three_clade_id(0) Then
                                    tree_complete = "("
                                    For i As Integer = 1 To three_clade_id(1) - 1
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += "),"
                                    For i As Integer = three_clade_id(1) + 1 To three_clade_id(2)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ";"
                                Else
                                    tree_complete = "("
                                    For i As Integer = three_clade_id(0) To three_clade_id(1) - 1
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ","
                                    For i As Integer = 1 To three_clade_id(0)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    For i As Integer = three_clade_id(1) + 1 To three_clade_id(2)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ");"
                                End If
                            Else
                                If three_clade_id(0) - 1 <= three_clade_id(1) - three_clade_id(0) Then
                                    tree_complete = ""
                                    For i As Integer = 1 To three_clade_id(0)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += "("
                                    For i As Integer = three_clade_id(0) + 1 To three_clade_id(2)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ");"
                                Else
                                    tree_complete = "("
                                    For i As Integer = three_clade_id(0) + 1 To three_clade_id(1) - 1
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ","
                                    For i As Integer = 1 To three_clade_id(0)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    For i As Integer = three_clade_id(1) + 1 To three_clade_id(2)
                                        tree_complete = tree_complete + tree_char(i)
                                    Next
                                    tree_complete += ");"
                                End If
                            End If
                        Else
                            For i As Integer = 1 To three_clade_id(2)
                                tree_complete = tree_complete + tree_char(i)
                            Next
                        End If
                    End If

                    For i As Integer = 1 To name_num
                        If f_t_name(0, i - 1) <> "" And f_t_name(1, i - 1) <> "" Then
                            tree_complete = tree_complete.Replace("(" + f_t_name(0, i - 1) + ",", "($%*" + f_t_name(1, i - 1) + "$%*,")
                            tree_complete = tree_complete.Replace("," + f_t_name(0, i - 1) + ")", ",$%*" + f_t_name(1, i - 1) + "$%*)")
                            tree_complete = tree_complete.Replace("," + f_t_name(0, i - 1) + ",", ",$%*" + f_t_name(1, i - 1) + "$%*,")
                            tree_complete = tree_complete.Replace("(" + f_t_name(0, i - 1) + ":", "($%*" + f_t_name(1, i - 1) + "$%*:")
                            tree_complete = tree_complete.Replace("," + f_t_name(0, i - 1) + ":", ",$%*" + f_t_name(1, i - 1) + "$%*:")
                        End If
                    Next
                    tree_complete = tree_complete.Replace("$%*", "")
                    tree_complete = tree_complete.Replace(" ", "")
                End If
                line = rt.ReadLine()
            Loop

        Catch ex As Exception
            rt.Close()
            MsgBox(ex.ToString)
            Return ""
        End Try
       
        Return tree_complete
    End Function

    Private Sub ConvertTreeToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ConvertTreeToolStripMenuItem.Click
        For i As Integer = 0 To ListBox1.Items.Count - 1
            Dim tree_line As String = load_final_trees(ListBox1.Items(i).ToString())
            If tree_line <> "" Then
                Dim ws As New StreamWriter(ListBox1.Items(i).ToString().Substring(0, ListBox1.Items(i).ToString().LastIndexOf(".")) & ".tree")

                ws.Write(tree_line)
                ws.Close()
            End If
        Next
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        For i As Integer = 0 To ListBox1.Items.Count - 1
            File.Delete(ListBox1.Items(i).ToString())
        Next
    End Sub
End Class