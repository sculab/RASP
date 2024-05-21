<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_Chrom
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows 窗体设计器所必需的
    Private components As System.ComponentModel.IContainer

    '注意: 以下过程是 Windows 窗体设计器所必需的
    '可以使用 Windows 窗体设计器修改它。
    '不要使用代码编辑器修改它。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config_Chrom))
        Button1 = New Button()
        Label1 = New Label()
        ComboBox1 = New ComboBox()
        Label2 = New Label()
        TextBox1 = New TextBox()
        TextBox2 = New TextBox()
        Label3 = New Label()
        Label4 = New Label()
        TextBox3 = New TextBox()
        TextBox4 = New TextBox()
        Button2 = New Button()
        TextBox5 = New TextBox()
        GroupBox1 = New GroupBox()
        CheckBox1 = New CheckBox()
        TextBox8 = New TextBox()
        Label8 = New Label()
        TextBox6 = New TextBox()
        TextBox7 = New TextBox()
        Label6 = New Label()
        Label7 = New Label()
        MenuStrip1 = New MenuStrip()
        FileToolStripMenuItem = New ToolStripMenuItem()
        TranslateResultToolStripMenuItem = New ToolStripMenuItem()
        FormatTreeToolStripMenuItem = New ToolStripMenuItem()
        Button3 = New Button()
        TimerChromEvol = New Timer(components)
        GroupBox1.SuspendLayout()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(378, 308)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 23)
        Button1.TabIndex = 0
        Button1.Text = "Build Files"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(4, 16)
        Label1.Name = "Label1"
        Label1.Size = New Size(93, 15)
        Label1.TabIndex = 1
        Label1.Text = "Model selection"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"All_Models", "Optimize_Model"})
        ComboBox1.Location = New Point(115, 11)
        ComboBox1.Margin = New Padding(3, 4, 3, 4)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(140, 23)
        ComboBox1.TabIndex = 2
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(4, 42)
        Label2.Name = "Label2"
        Label2.Size = New Size(175, 15)
        Label2.TabIndex = 3
        Label2.Text = "Maximal chromosome number"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(187, 39)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(68, 21)
        TextBox1.TabIndex = 4
        TextBox1.Text = "-10"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(187, 64)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(68, 21)
        TextBox2.TabIndex = 5
        TextBox2.Text = "1"
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(4, 68)
        Label3.Name = "Label3"
        Label3.Size = New Size(173, 15)
        Label3.TabIndex = 6
        Label3.Text = "Minimal chromosome number"
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(4, 93)
        Label4.Name = "Label4"
        Label4.Size = New Size(133, 15)
        Label4.TabIndex = 7
        Label4.Text = "Number of simulations"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(187, 89)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(68, 21)
        TextBox3.TabIndex = 8
        TextBox3.Text = "10000"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(6, 213)
        TextBox4.Multiline = True
        TextBox4.Name = "TextBox4"
        TextBox4.ReadOnly = True
        TextBox4.Size = New Size(248, 86)
        TextBox4.TabIndex = 9
        TextBox4.Text = "_branchMul 999" & vbCrLf & "_simulationsNum 0" & vbCrLf & "_logValue 6" & vbCrLf & "_maxOptimizationIterations 5" & vbCrLf & "_epsilonLLimprovement 0.01" & vbCrLf & "_optimizePointsNum 10,3,1" & vbCrLf & "_optimizeIterNum 0,2,5" & vbCrLf & "_gainConstR 1" & vbCrLf & "_lossConstR 1" & vbCrLf & "_duplConstR 1"
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(459, 308)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 23)
        Button2.TabIndex = 11
        Button2.Text = "Close"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(271, 33)
        TextBox5.Multiline = True
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(263, 269)
        TextBox5.TabIndex = 12
        TextBox5.Text = resources.GetString("TextBox5.Text")
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(CheckBox1)
        GroupBox1.Controls.Add(TextBox8)
        GroupBox1.Controls.Add(Label8)
        GroupBox1.Controls.Add(TextBox6)
        GroupBox1.Controls.Add(TextBox7)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label7)
        GroupBox1.Controls.Add(TextBox4)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Location = New Point(2, 26)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(263, 305)
        GroupBox1.TabIndex = 13
        GroupBox1.TabStop = False
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(9, 192)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(115, 19)
        CheckBox1.TabIndex = 17
        CheckBox1.Text = "More command:"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(187, 164)
        TextBox8.Name = "TextBox8"
        TextBox8.Size = New Size(68, 21)
        TextBox8.TabIndex = 16
        TextBox8.Text = "0"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(4, 168)
        Label8.Name = "Label8"
        Label8.Size = New Size(132, 15)
        Label8.TabIndex = 15
        Label8.Text = "Optimize base number"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(187, 139)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(68, 21)
        TextBox6.TabIndex = 14
        TextBox6.Text = "0.5"
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(187, 114)
        TextBox7.Name = "TextBox7"
        TextBox7.Size = New Size(68, 21)
        TextBox7.TabIndex = 11
        TextBox7.Text = "9"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(4, 143)
        Label6.Name = "Label6"
        Label6.Size = New Size(127, 15)
        Label6.TabIndex = 13
        Label6.Text = "Rate for base number"
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(4, 118)
        Label7.Name = "Label7"
        Label7.Size = New Size(82, 15)
        Label7.TabIndex = 12
        Label7.Text = "Base number"
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        MenuStrip1.Items.AddRange(New ToolStripItem() {FileToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(536, 24)
        MenuStrip1.TabIndex = 14
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' FileToolStripMenuItem
        ' 
        FileToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {TranslateResultToolStripMenuItem, FormatTreeToolStripMenuItem})
        FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        FileToolStripMenuItem.Size = New Size(39, 20)
        FileToolStripMenuItem.Text = "File"
        ' 
        ' TranslateResultToolStripMenuItem
        ' 
        TranslateResultToolStripMenuItem.Name = "TranslateResultToolStripMenuItem"
        TranslateResultToolStripMenuItem.Size = New Size(165, 22)
        TranslateResultToolStripMenuItem.Text = "Translate Result"
        ' 
        ' FormatTreeToolStripMenuItem
        ' 
        FormatTreeToolStripMenuItem.Name = "FormatTreeToolStripMenuItem"
        FormatTreeToolStripMenuItem.Size = New Size(165, 22)
        FormatTreeToolStripMenuItem.Text = "Translate Tree"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(271, 308)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 23)
        Button3.TabIndex = 15
        Button3.Text = "Auto Run"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TimerChromEvol
        ' 
        TimerChromEvol.Interval = 1000
        ' 
        ' Config_Chrom
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(536, 332)
        ControlBox = False
        Controls.Add(Button3)
        Controls.Add(GroupBox1)
        Controls.Add(TextBox5)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(MenuStrip1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        MainMenuStrip = MenuStrip1
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Config_Chrom"
        Text = "ChromEvol"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FormatTreeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TranslateResultToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TimerChromEvol As System.Windows.Forms.Timer
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
