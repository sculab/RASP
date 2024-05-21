<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_Traits
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
        TextBox3 = New TextBox()
        ComboBox1 = New ComboBox()
        Button1 = New Button()
        GroupBox1 = New GroupBox()
        TextBox6 = New TextBox()
        Label9 = New Label()
        Label6 = New Label()
        Label4 = New Label()
        Label1 = New Label()
        TextBox1 = New TextBox()
        TextBox4 = New TextBox()
        DataGridView2 = New DataGridView()
        Button2 = New Button()
        ComboBox2 = New ComboBox()
        ComboBox3 = New ComboBox()
        ComboBox4 = New ComboBox()
        ComboBox5 = New ComboBox()
        Label2 = New Label()
        Label3 = New Label()
        Label5 = New Label()
        Label7 = New Label()
        ComboBox6 = New ComboBox()
        TimerTraits = New Timer(components)
        TextBox2 = New TextBox()
        ProgressBar1 = New ProgressBar()
        GroupBox1.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(76, 25)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(63, 21)
        TextBox3.TabIndex = 52
        TextBox3.Text = "5050000"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Maximum Likelihood", "MCMC"})
        ComboBox1.Location = New Point(463, 33)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(145, 23)
        ComboBox1.TabIndex = 85
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(463, 413)
        Button1.Name = "Button1"
        Button1.Size = New Size(64, 23)
        Button1.TabIndex = 84
        Button1.Text = "Run"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        GroupBox1.Controls.Add(TextBox6)
        GroupBox1.Controls.Add(Label9)
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(TextBox4)
        GroupBox1.Location = New Point(463, 62)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(148, 132)
        GroupBox1.TabIndex = 83
        GroupBox1.TabStop = False
        GroupBox1.Text = "MCMC && ML"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(76, 102)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(63, 21)
        TextBox6.TabIndex = 54
        TextBox6.Text = "100"
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(6, 105)
        Label9.Name = "Label9"
        Label9.Size = New Size(50, 15)
        Label9.TabIndex = 53
        Label9.Text = "MLTries"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(6, 80)
        Label6.Name = "Label6"
        Label6.Size = New Size(43, 15)
        Label6.TabIndex = 42
        Label6.Text = "BurnIn"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(6, 53)
        Label4.Name = "Label4"
        Label4.Size = New Size(50, 15)
        Label4.TabIndex = 44
        Label4.Text = "Sample"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(6, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(58, 15)
        Label1.TabIndex = 46
        Label1.Text = "Iterations"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(76, 50)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(63, 21)
        TextBox1.TabIndex = 50
        TextBox1.Text = "10000"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(76, 77)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(63, 21)
        TextBox4.TabIndex = 41
        TextBox4.Text = "50000"
        ' 
        ' DataGridView2
        ' 
        DataGridView2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(0, 3)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridView2.RowTemplate.Height = 23
        DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView2.Size = New Size(457, 404)
        DataGridView2.TabIndex = 66
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button2.Location = New Point(547, 413)
        Button2.Name = "Button2"
        Button2.Size = New Size(64, 23)
        Button2.TabIndex = 88
        Button2.Text = "Close"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"", "HyperPriorAll gamma 0 10 0 10", "HyperPriorAll exponential 0 10", "HyperPriorAll beta 0 100 0 50", "HyperPriorAll uniform 0 100 0 100"})
        ComboBox2.Location = New Point(310, 196)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(104, 23)
        ComboBox2.TabIndex = 92
        ' 
        ' ComboBox3
        ' 
        ComboBox3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox3.FormattingEnabled = True
        ComboBox3.Items.AddRange(New Object() {"", "RevJumpHP gamma 0 10 0 10", "RevJumpHP exponential 0 10", "RevJumpHP beta 0 100 0 50", "RevJumpHP uniform 0 100 0 100"})
        ComboBox3.Location = New Point(310, 225)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(104, 23)
        ComboBox3.TabIndex = 93
        ' 
        ' ComboBox4
        ' 
        ComboBox4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox4.FormattingEnabled = True
        ComboBox4.Items.AddRange(New Object() {"", "RestrictAll 1"})
        ComboBox4.Location = New Point(310, 254)
        ComboBox4.Name = "ComboBox4"
        ComboBox4.Size = New Size(104, 23)
        ComboBox4.TabIndex = 94
        ' 
        ' ComboBox5
        ' 
        ComboBox5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox5.FormattingEnabled = True
        ComboBox5.Items.AddRange(New Object() {"", "stones 100 10000"})
        ComboBox5.Location = New Point(310, 283)
        ComboBox5.Name = "ComboBox5"
        ComboBox5.Size = New Size(104, 23)
        ComboBox5.TabIndex = 95
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Location = New Point(266, 199)
        Label2.Name = "Label2"
        Label2.Size = New Size(36, 15)
        Label2.TabIndex = 96
        Label2.Text = "HPAll"
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label3.AutoSize = True
        Label3.Location = New Point(266, 257)
        Label3.Name = "Label3"
        Label3.Size = New Size(43, 15)
        Label3.TabIndex = 97
        Label3.Text = "ResAll"
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label5.AutoSize = True
        Label5.Location = New Point(266, 228)
        Label5.Name = "Label5"
        Label5.Size = New Size(39, 15)
        Label5.TabIndex = 98
        Label5.Text = "RJHP"
        ' 
        ' Label7
        ' 
        Label7.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label7.AutoSize = True
        Label7.Location = New Point(266, 286)
        Label7.Name = "Label7"
        Label7.Size = New Size(39, 15)
        Label7.TabIndex = 99
        Label7.Text = "Stone"
        ' 
        ' ComboBox6
        ' 
        ComboBox6.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        ComboBox6.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox6.Enabled = False
        ComboBox6.FormattingEnabled = True
        ComboBox6.Items.AddRange(New Object() {"1) MultiState", "2) Discrete: Independent", "3) Discrete: Dependant", "4) Continuous: Random Walk (Model A)", "5) Continuous: Directional (Model B)", "6) Continuous: Regression", "7) Independent Contrast", "8) Independent Contrast: Correlation", "9) Independent Contrast: Regression", "10)Discrete: Covarion"})
        ComboBox6.Location = New Point(463, 4)
        ComboBox6.Name = "ComboBox6"
        ComboBox6.Size = New Size(145, 23)
        ComboBox6.TabIndex = 101
        ' 
        ' TimerTraits
        ' 
        TimerTraits.Interval = 3000
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        TextBox2.Location = New Point(463, 200)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(148, 207)
        TextBox2.TabIndex = 105
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ProgressBar1.Location = New Point(0, 413)
        ProgressBar1.Maximum = 10000
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(457, 23)
        ProgressBar1.TabIndex = 106
        ' 
        ' Config_Traits
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(615, 443)
        ControlBox = False
        Controls.Add(ProgressBar1)
        Controls.Add(TextBox2)
        Controls.Add(DataGridView2)
        Controls.Add(ComboBox6)
        Controls.Add(ComboBox5)
        Controls.Add(ComboBox4)
        Controls.Add(ComboBox3)
        Controls.Add(ComboBox2)
        Controls.Add(Label7)
        Controls.Add(Label5)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Button1)
        Controls.Add(GroupBox1)
        Controls.Add(Button2)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Config_Traits"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Bayestraits"
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox5 As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ComboBox6 As System.Windows.Forms.ComboBox
    Friend WithEvents TimerTraits As System.Windows.Forms.Timer
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ProgressBar1 As ProgressBar
End Class
