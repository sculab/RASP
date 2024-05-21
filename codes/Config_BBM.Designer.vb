<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_BBM
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        components = New ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config_BBM))
        TextBox4 = New TextBox()
        TextBox3 = New TextBox()
        TextBox1 = New TextBox()
        Button1 = New Button()
        Label1 = New Label()
        TextBox2 = New TextBox()
        NumericUpDown2 = New NumericUpDown()
        Label2 = New Label()
        Label3 = New Label()
        Label4 = New Label()
        Label6 = New Label()
        NumericUpDown1 = New NumericUpDown()
        Label5 = New Label()
        Button2 = New Button()
        ComboBox1 = New ComboBox()
        Label7 = New Label()
        TextBox5 = New TextBox()
        TextBox6 = New TextBox()
        Label8 = New Label()
        Label9 = New Label()
        ComboBox2 = New ComboBox()
        Label10 = New Label()
        TextBox7 = New TextBox()
        TextBox8 = New TextBox()
        GroupBox1 = New GroupBox()
        GroupBox2 = New GroupBox()
        CheckBox1 = New CheckBox()
        NumericUpDown3 = New NumericUpDown()
        Label13 = New Label()
        Label14 = New Label()
        Button3 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        DataGridView2 = New DataGridView()
        GroupBox3 = New GroupBox()
        GroupBox4 = New GroupBox()
        Label11 = New Label()
        Label12 = New Label()
        ComboBox3 = New ComboBox()
        TextBox9 = New TextBox()
        BayesTimer = New Timer(components)
        CheckBox2 = New CheckBox()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox3.SuspendLayout()
        GroupBox4.SuspendLayout()
        SuspendLayout()
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(175, 102)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(89, 21)
        TextBox4.TabIndex = 41
        TextBox4.Text = "100"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(175, 25)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(89, 21)
        TextBox3.TabIndex = 52
        TextBox3.Text = "50000"
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(175, 77)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(89, 21)
        TextBox1.TabIndex = 50
        TextBox1.Text = "100"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(409, 310)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 24)
        Button1.TabIndex = 48
        Button1.Text = "OK"
        ' 
        ' Label1
        ' 
        Label1.Location = New Point(7, 20)
        Label1.Name = "Label1"
        Label1.Size = New Size(164, 30)
        Label1.TabIndex = 46
        Label1.Text = "Number of cycles"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(176, 128)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(89, 21)
        TextBox2.TabIndex = 40
        TextBox2.Text = "0.1"
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Location = New Point(175, 50)
        NumericUpDown2.Maximum = New Decimal(New Integer() {100000000, 0, 0, 0})
        NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.Size = New Size(89, 21)
        NumericUpDown2.TabIndex = 39
        NumericUpDown2.Value = New Decimal(New Integer() {10, 0, 0, 0})
        ' 
        ' Label2
        ' 
        Label2.Location = New Point(7, 44)
        Label2.Name = "Label2"
        Label2.Size = New Size(164, 30)
        Label2.TabIndex = 47
        Label2.Text = "Number of chains"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.Location = New Point(8, 124)
        Label3.Name = "Label3"
        Label3.Size = New Size(166, 30)
        Label3.TabIndex = 43
        Label3.Text = "Temperature"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label4
        ' 
        Label4.Location = New Point(7, 70)
        Label4.Name = "Label4"
        Label4.Size = New Size(164, 30)
        Label4.TabIndex = 44
        Label4.Text = "Frequent of samples"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label6
        ' 
        Label6.Location = New Point(7, 97)
        Label6.Name = "Label6"
        Label6.Size = New Size(164, 30)
        Label6.TabIndex = 42
        Label6.Text = "Discard samples"
        Label6.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Location = New Point(205, 25)
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(59, 21)
        NumericUpDown1.TabIndex = 39
        NumericUpDown1.Value = New Decimal(New Integer() {4, 0, 0, 0})
        ' 
        ' Label5
        ' 
        Label5.Location = New Point(6, 20)
        Label5.Name = "Label5"
        Label5.Size = New Size(166, 30)
        Label5.TabIndex = 43
        Label5.Text = "Maximum number of areas"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(490, 310)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 24)
        Button2.TabIndex = 48
        Button2.Text = "Cancel"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"Fixed (JC)", "Estimated (F81)"})
        ComboBox1.Location = New Point(158, 23)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(108, 23)
        ComboBox1.TabIndex = 54
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(7, 25)
        Label7.Name = "Label7"
        Label7.Size = New Size(103, 15)
        Label7.TabIndex = 55
        Label7.Text = "State frequencies"
        ' 
        ' TextBox5
        ' 
        TextBox5.Location = New Point(158, 49)
        TextBox5.Name = "TextBox5"
        TextBox5.Size = New Size(53, 21)
        TextBox5.TabIndex = 56
        TextBox5.Text = "0.5"
        ' 
        ' TextBox6
        ' 
        TextBox6.Location = New Point(213, 49)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(53, 21)
        TextBox6.TabIndex = 57
        TextBox6.Text = "0.5"
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(22, 52)
        Label8.Name = "Label8"
        Label8.Size = New Size(126, 15)
        Label8.TabIndex = 58
        Label8.Text = "|-Dirichlet distribution "
        ' 
        ' Label9
        ' 
        Label9.AutoSize = True
        Label9.Location = New Point(7, 77)
        Label9.Name = "Label9"
        Label9.Size = New Size(144, 15)
        Label9.TabIndex = 59
        Label9.Text = "Among-Site rate variation"
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"Equal", "Gamma (+G)"})
        ComboBox2.Location = New Point(158, 75)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(108, 23)
        ComboBox2.TabIndex = 60
        ' 
        ' Label10
        ' 
        Label10.AutoSize = True
        Label10.Location = New Point(23, 105)
        Label10.Name = "Label10"
        Label10.Size = New Size(123, 15)
        Label10.TabIndex = 61
        Label10.Text = "|-Gamma distribution"
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(158, 102)
        TextBox7.Name = "TextBox7"
        TextBox7.Size = New Size(55, 21)
        TextBox7.TabIndex = 62
        TextBox7.Text = "0.001"
        ' 
        ' TextBox8
        ' 
        TextBox8.Location = New Point(213, 102)
        TextBox8.Name = "TextBox8"
        TextBox8.Size = New Size(52, 21)
        TextBox8.TabIndex = 62
        TextBox8.Text = "100"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox3)
        GroupBox1.Controls.Add(Label6)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label3)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(NumericUpDown2)
        GroupBox1.Controls.Add(TextBox2)
        GroupBox1.Controls.Add(Label1)
        GroupBox1.Controls.Add(TextBox1)
        GroupBox1.Controls.Add(TextBox4)
        GroupBox1.Location = New Point(290, 3)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(276, 158)
        GroupBox1.TabIndex = 63
        GroupBox1.TabStop = False
        GroupBox1.Text = "Markov Chain Monte Carlo analysis"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(ComboBox1)
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Controls.Add(TextBox8)
        GroupBox2.Controls.Add(TextBox5)
        GroupBox2.Controls.Add(TextBox7)
        GroupBox2.Controls.Add(TextBox6)
        GroupBox2.Controls.Add(Label10)
        GroupBox2.Controls.Add(Label8)
        GroupBox2.Controls.Add(ComboBox2)
        GroupBox2.Controls.Add(Label9)
        GroupBox2.Location = New Point(290, 167)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(276, 132)
        GroupBox2.TabIndex = 64
        GroupBox2.TabStop = False
        GroupBox2.Text = "Model"
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(9, 50)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(204, 19)
        CheckBox1.TabIndex = 65
        CheckBox1.Text = "Allow null distribution in analysis"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' NumericUpDown3
        ' 
        NumericUpDown3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown3.Location = New Point(205, 220)
        NumericUpDown3.Name = "NumericUpDown3"
        NumericUpDown3.Size = New Size(46, 21)
        NumericUpDown3.TabIndex = 71
        NumericUpDown3.TextAlign = HorizontalAlignment.Right
        NumericUpDown3.Value = New Decimal(New Integer() {90, 0, 0, 0})
        ' 
        ' Label13
        ' 
        Label13.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label13.AutoSize = True
        Label13.Location = New Point(185, 222)
        Label13.Name = "Label13"
        Label13.Size = New Size(21, 15)
        Label13.TabIndex = 73
        Label13.Text = ">="
        ' 
        ' Label14
        ' 
        Label14.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label14.AutoSize = True
        Label14.Location = New Point(252, 222)
        Label14.Name = "Label14"
        Label14.Size = New Size(18, 15)
        Label14.TabIndex = 72
        Label14.Text = "%"
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button3.Location = New Point(133, 218)
        Button3.Name = "Button3"
        Button3.Size = New Size(51, 23)
        Button3.TabIndex = 70
        Button3.Text = "Select "
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button4.Location = New Point(59, 218)
        Button4.Name = "Button4"
        Button4.Size = New Size(51, 23)
        Button4.TabIndex = 69
        Button4.Text = "Clear"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button5.Location = New Point(8, 218)
        Button5.Name = "Button5"
        Button5.Size = New Size(51, 23)
        Button5.TabIndex = 68
        Button5.Text = "All"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(6, 20)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridView2.RowTemplate.Height = 23
        DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView2.Size = New Size(258, 192)
        DataGridView2.TabIndex = 66
        ' 
        ' GroupBox3
        ' 
        GroupBox3.Controls.Add(DataGridView2)
        GroupBox3.Controls.Add(NumericUpDown3)
        GroupBox3.Controls.Add(Button5)
        GroupBox3.Controls.Add(Label13)
        GroupBox3.Controls.Add(Button4)
        GroupBox3.Controls.Add(Label14)
        GroupBox3.Controls.Add(Button3)
        GroupBox3.Location = New Point(8, 87)
        GroupBox3.Name = "GroupBox3"
        GroupBox3.Size = New Size(276, 247)
        GroupBox3.TabIndex = 74
        GroupBox3.TabStop = False
        GroupBox3.Text = "Node list"
        ' 
        ' GroupBox4
        ' 
        GroupBox4.Controls.Add(Label5)
        GroupBox4.Controls.Add(NumericUpDown1)
        GroupBox4.Controls.Add(CheckBox1)
        GroupBox4.Controls.Add(Label11)
        GroupBox4.Controls.Add(Label12)
        GroupBox4.Controls.Add(ComboBox3)
        GroupBox4.Controls.Add(TextBox9)
        GroupBox4.Location = New Point(8, 3)
        GroupBox4.Name = "GroupBox4"
        GroupBox4.Size = New Size(276, 78)
        GroupBox4.TabIndex = 75
        GroupBox4.TabStop = False
        GroupBox4.Text = "Area"
        ' 
        ' Label11
        ' 
        Label11.AutoSize = True
        Label11.Location = New Point(6, 84)
        Label11.Name = "Label11"
        Label11.Size = New Size(100, 15)
        Label11.TabIndex = 44
        Label11.Text = "Root distribution "
        Label11.Visible = False
        ' 
        ' Label12
        ' 
        Label12.AutoSize = True
        Label12.Location = New Point(28, 110)
        Label12.Name = "Label12"
        Label12.Size = New Size(122, 15)
        Label12.TabIndex = 47
        Label12.Text = "|-Custom distribution"
        Label12.Visible = False
        ' 
        ' ComboBox3
        ' 
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox3.FormattingEnabled = True
        ComboBox3.Items.AddRange(New Object() {"Null", "Wide", "Custom"})
        ComboBox3.Location = New Point(178, 79)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(89, 23)
        ComboBox3.TabIndex = 45
        ComboBox3.Visible = False
        ' 
        ' TextBox9
        ' 
        TextBox9.Location = New Point(178, 106)
        TextBox9.Name = "TextBox9"
        TextBox9.Size = New Size(89, 21)
        TextBox9.TabIndex = 46
        TextBox9.Visible = False
        ' 
        ' BayesTimer
        ' 
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(290, 314)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(102, 19)
        CheckBox2.TabIndex = 76
        CheckBox2.Text = "Large dataset"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' Config_BBM
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(571, 341)
        ControlBox = False
        Controls.Add(CheckBox2)
        Controls.Add(GroupBox4)
        Controls.Add(GroupBox3)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Config_BBM"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Bayesian Analysis"
        TopMost = True
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        GroupBox3.ResumeLayout(False)
        GroupBox3.PerformLayout()
        GroupBox4.ResumeLayout(False)
        GroupBox4.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox8 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox9 As System.Windows.Forms.TextBox
    Friend WithEvents BayesTimer As Timer
    Friend WithEvents CheckBox2 As CheckBox
End Class
