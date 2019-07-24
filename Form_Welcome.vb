Imports System.IO
Imports System.Threading
Imports System.Text.RegularExpressions
Public Class Form_Welcome
    Private Sub Welcome_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
        Timer1.Enabled = True
        change_lang(local_lang)
        MainWindow.Show()
        format_path()
        Dim th1 As New Thread(AddressOf load_main)
        th1.Start()
    End Sub
    Public Sub load_main()
        My.Computer.FileSystem.CreateDirectory(root_path + "temp")
        DeleteTemp(root_path + "temp")
        current_file = total_file
    End Sub
    Private Sub Welcome_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If current_file < total_file Then
			ProgressBar1.Value = Math.Min(current_file / total_file * 100, 100)
		Else
            Timer1.Enabled = False
            Dim RegCHZN As New Regex("[\u4e00-\u9fa5]")
            Dim m As Match = RegCHZN.Match(root_path)
            If m.Success Then
                MsgBox("安装路径不得含有中文（亚洲语言字符）！" + Chr(13) + "RASP install path should not contain Asia language character!")
                End
            End If
            If File.Exists(root_path + "Plug-ins\R_path.txt") Then
                Dim sr As New StreamReader(root_path + "Plug-ins\R_path.txt")
                rscript = sr.ReadLine
                sr.Close()
            End If


            If File.Exists(rscript) = False Then
				If File.Exists(root_path + "Plug-ins\R\bin\i386\Rscript.exe") Then
					rscript = root_path + "Plug-ins\R\bin\i386\Rscript.exe"
					Dim sw As New StreamWriter(root_path + "Plug-ins\R_path.txt")
					sw.WriteLine(rscript)
					sw.Close()
				Else
					Dim Key1 As Microsoft.Win32.RegistryKey
                    Key1 = My.Computer.Registry.LocalMachine
                    Dim Key2 As Microsoft.Win32.RegistryKey
                    Key2 = Key1.OpenSubKey("SOFTWARE\R-core\R", False)
                    If Key2 Is Nothing Then
                        MsgBox("You need to install R to free all functions of RASP." + vbCrLf + "Select [Tools-> Install 3rd Party] for more information")
                    ElseIf (Key2.GetValue("InstallPath") Is Nothing) = False Then
                        If File.Exists(Key2.GetValue("InstallPath") + "\bin\Rscript.exe") Then
                            Dim sw As New StreamWriter(root_path + "Plug-ins\R_path.txt")
                            sw.WriteLine(rscript)
                            sw.Close()
                        Else
                            MsgBox("You need to install R to free all functions of RASP." + vbCrLf + "Select [Tools-> Install 3rd Party] for more information")
                        End If
                    End If
                End If
            End If
            Me.Hide()
        End If
    End Sub
    Dim total_file As Integer = 2
    Dim current_file As Single = 0

    Public Sub DeleteTemp(ByVal aimPath As String)
        If (aimPath(aimPath.Length - 1) <> Path.DirectorySeparatorChar) Then
            aimPath += Path.DirectorySeparatorChar
        End If  '判断待删除的目录是否存在,不存在则退出.  
        If (Not Directory.Exists(aimPath)) Then Exit Sub ' 
        Dim fileList() As String = Directory.GetFileSystemEntries(aimPath)  ' 遍历所有的文件和目录  
		total_file = Math.Max(fileList.Length, total_file)
		For Each FileName As String In fileList
            If (Directory.Exists(FileName)) Then  ' 先当作目录处理如果存在这个目录就递归
                DeleteDir(aimPath + Path.GetFileName(FileName))
            Else  ' 否则直接Delete文件  
                Try
                    File.Delete(aimPath + Path.GetFileName(FileName))
                    current_file += 1
                Catch ex As Exception
                End Try
            End If
        Next  '删除文件夹  
    End Sub
    Private Sub ProgressBar1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ProgressBar1.Click

    End Sub
End Class