<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_SDIVA
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config_SDIVA))
        DataGridView1 = New DataGridView()
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        Button1 = New Button()
        Button2 = New Button()
        MenuStrip1 = New MenuStrip()
        OperationToolStripMenuItem = New ToolStripMenuItem()
        RefreshTheRangeListToolStripMenuItem = New ToolStripMenuItem()
        SaveSettingsToolStripMenuItem = New ToolStripMenuItem()
        LoadSettingToolStripMenuItem = New ToolStripMenuItem()
        Button3 = New Button()
        Button4 = New Button()
        Label2 = New Label()
        Label3 = New Label()
        GroupBox2 = New GroupBox()
        CheckBox6 = New CheckBox()
        CheckBox5 = New CheckBox()
        Label1 = New Label()
        CheckBox1 = New CheckBox()
        TextBox5 = New TextBox()
        NumericUpDown2 = New NumericUpDown()
        CheckBox4 = New CheckBox()
        CheckBox8 = New CheckBox()
        CheckBox2 = New CheckBox()
        TextBox3 = New TextBox()
        NumericUpDown1 = New NumericUpDown()
        CheckBox12 = New CheckBox()
        TextBox1 = New TextBox()
        CheckBox9 = New CheckBox()
        TextBox4 = New TextBox()
        CheckBox7 = New CheckBox()
        Label4 = New Label()
        ComboBox1 = New ComboBox()
        TextBox6 = New TextBox()
        CheckBox11 = New CheckBox()
        TextBox7 = New TextBox()
        CheckBox10 = New CheckBox()
        GroupBox1 = New GroupBox()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        TabPage2 = New TabPage()
        DataGridView2 = New DataGridView()
        CheckBox13 = New CheckBox()
        Label5 = New Label()
        NumericUpDown3 = New NumericUpDown()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        GroupBox2.SuspendLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        TabPage2.SuspendLayout()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(2, 2)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.RowTemplate.Height = 23
        DataGridView1.Size = New Size(389, 253)
        DataGridView1.TabIndex = 0
        ' 
        ' ListBox1
        ' 
        ListBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        ListBox1.BorderStyle = BorderStyle.FixedSingle
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(407, 59)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(120, 377)
        ListBox1.Sorted = True
        ListBox1.TabIndex = 2
        ' 
        ' ListBox2
        ' 
        ListBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        ListBox2.BorderStyle = BorderStyle.FixedSingle
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(538, 59)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(120, 377)
        ListBox2.Sorted = True
        ListBox2.TabIndex = 3
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button1.Location = New Point(489, 28)
        Button1.Name = "Button1"
        Button1.Size = New Size(38, 26)
        Button1.TabIndex = 4
        Button1.Text = ">>"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button2.Location = New Point(538, 28)
        Button2.Name = "Button2"
        Button2.Size = New Size(38, 26)
        Button2.TabIndex = 5
        Button2.Text = "<<"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        MenuStrip1.Items.AddRange(New ToolStripItem() {OperationToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(662, 24)
        MenuStrip1.TabIndex = 8
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' OperationToolStripMenuItem
        ' 
        OperationToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {RefreshTheRangeListToolStripMenuItem, SaveSettingsToolStripMenuItem, LoadSettingToolStripMenuItem})
        OperationToolStripMenuItem.Name = "OperationToolStripMenuItem"
        OperationToolStripMenuItem.Size = New Size(73, 20)
        OperationToolStripMenuItem.Text = "Operation"
        ' 
        ' RefreshTheRangeListToolStripMenuItem
        ' 
        RefreshTheRangeListToolStripMenuItem.Name = "RefreshTheRangeListToolStripMenuItem"
        RefreshTheRangeListToolStripMenuItem.Size = New Size(201, 22)
        RefreshTheRangeListToolStripMenuItem.Text = "Refresh the Range List"
        ' 
        ' SaveSettingsToolStripMenuItem
        ' 
        SaveSettingsToolStripMenuItem.Name = "SaveSettingsToolStripMenuItem"
        SaveSettingsToolStripMenuItem.Size = New Size(201, 22)
        SaveSettingsToolStripMenuItem.Text = "Save Setting"
        ' 
        ' LoadSettingToolStripMenuItem
        ' 
        LoadSettingToolStripMenuItem.Name = "LoadSettingToolStripMenuItem"
        LoadSettingToolStripMenuItem.Size = New Size(201, 22)
        LoadSettingToolStripMenuItem.Text = "Load Setting"
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button3.Location = New Point(407, 442)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 26)
        Button3.TabIndex = 9
        Button3.Text = "OK"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button4.Location = New Point(583, 442)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 26)
        Button4.TabIndex = 10
        Button4.Text = "Cancel"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Location = New Point(407, 32)
        Label2.Name = "Label2"
        Label2.Size = New Size(47, 15)
        Label2.TabIndex = 11
        Label2.Text = "Include"
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label3.AutoSize = True
        Label3.Location = New Point(608, 32)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 15)
        Label3.TabIndex = 12
        Label3.Text = "Exclude"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        GroupBox2.Controls.Add(CheckBox6)
        GroupBox2.Controls.Add(CheckBox5)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(CheckBox1)
        GroupBox2.Controls.Add(TextBox5)
        GroupBox2.Controls.Add(NumericUpDown2)
        GroupBox2.Controls.Add(CheckBox4)
        GroupBox2.Controls.Add(CheckBox8)
        GroupBox2.Controls.Add(CheckBox2)
        GroupBox2.Controls.Add(TextBox3)
        GroupBox2.Controls.Add(NumericUpDown1)
        GroupBox2.Location = New Point(4, 316)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(397, 120)
        GroupBox2.TabIndex = 23
        GroupBox2.TabStop = False
        GroupBox2.Text = "Optimize"
        ' 
        ' CheckBox6
        ' 
        CheckBox6.AutoSize = True
        CheckBox6.Location = New Point(6, 94)
        CheckBox6.Name = "CheckBox6"
        CheckBox6.Size = New Size(103, 19)
        CheckBox6.TabIndex = 49
        CheckBox6.Text = "Use Final tree"
        CheckBox6.UseVisualStyleBackColor = True
        ' 
        ' CheckBox5
        ' 
        CheckBox5.AutoSize = True
        CheckBox5.Enabled = False
        CheckBox5.Location = New Point(228, 67)
        CheckBox5.Name = "CheckBox5"
        CheckBox5.Size = New Size(102, 19)
        CheckBox5.TabIndex = 47
        CheckBox5.Text = "Random Step"
        CheckBox5.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(25, 70)
        Label1.Name = "Label1"
        Label1.Size = New Size(124, 15)
        Label1.TabIndex = 46
        Label1.Text = "Max Reconstructions "
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(228, 44)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(148, 19)
        CheckBox1.TabIndex = 42
        CheckBox1.Text = "Allow Extinction (Slow)"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox5
        ' 
        TextBox5.BackColor = Color.White
        TextBox5.Location = New Point(331, 92)
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(60, 21)
        TextBox5.TabIndex = 23
        TextBox5.Text = "1000"
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.BackColor = Color.White
        NumericUpDown2.Location = New Point(158, 19)
        NumericUpDown2.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        NumericUpDown2.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.ReadOnly = True
        NumericUpDown2.Size = New Size(56, 21)
        NumericUpDown2.TabIndex = 24
        NumericUpDown2.TextAlign = HorizontalAlignment.Right
        NumericUpDown2.Value = New Decimal(New Integer() {4, 0, 0, 0})
        ' 
        ' CheckBox4
        ' 
        CheckBox4.AutoSize = True
        CheckBox4.Checked = True
        CheckBox4.CheckState = CheckState.Checked
        CheckBox4.Location = New Point(6, 20)
        CheckBox4.Name = "CheckBox4"
        CheckBox4.Size = New Size(156, 19)
        CheckBox4.TabIndex = 21
        CheckBox4.Text = "Max areas at each node"
        CheckBox4.UseVisualStyleBackColor = True
        ' 
        ' CheckBox8
        ' 
        CheckBox8.AutoSize = True
        CheckBox8.Location = New Point(123, 94)
        CheckBox8.Name = "CheckBox8"
        CheckBox8.Size = New Size(210, 19)
        CheckBox8.TabIndex = 21
        CheckBox8.Text = "Max Reconstructions for final tree:"
        CheckBox8.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(6, 44)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(179, 19)
        CheckBox2.TabIndex = 20
        CheckBox2.Text = "Allow Reconstruction (Slow)"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(158, 65)
        TextBox3.Name = "TextBox3"
        TextBox3.ReadOnly = True
        TextBox3.Size = New Size(56, 21)
        TextBox3.TabIndex = 22
        TextBox3.Text = "100"
        TextBox3.TextAlign = HorizontalAlignment.Right
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Enabled = False
        NumericUpDown1.Location = New Point(331, 65)
        NumericUpDown1.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.Size = New Size(59, 21)
        NumericUpDown1.TabIndex = 48
        NumericUpDown1.Value = New Decimal(New Integer() {2, 0, 0, 0})
        ' 
        ' CheckBox12
        ' 
        CheckBox12.AutoSize = True
        CheckBox12.Location = New Point(6, 241)
        CheckBox12.Name = "CheckBox12"
        CheckBox12.Size = New Size(254, 19)
        CheckBox12.TabIndex = 41
        CheckBox12.Text = "Use ancestral ranges of condensed trees"
        CheckBox12.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(186, 161)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(56, 21)
        TextBox1.TabIndex = 22
        TextBox1.Text = "65536"
        TextBox1.TextAlign = HorizontalAlignment.Right
        ' 
        ' CheckBox9
        ' 
        CheckBox9.AutoSize = True
        CheckBox9.Checked = True
        CheckBox9.CheckState = CheckState.Checked
        CheckBox9.Location = New Point(137, 119)
        CheckBox9.Name = "CheckBox9"
        CheckBox9.Size = New Size(53, 19)
        CheckBox9.TabIndex = 25
        CheckBox9.Text = "keep"
        CheckBox9.UseVisualStyleBackColor = True
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(186, 78)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(56, 21)
        TextBox4.TabIndex = 22
        TextBox4.Text = "32767"
        TextBox4.TextAlign = HorizontalAlignment.Right
        ' 
        ' CheckBox7
        ' 
        CheckBox7.AutoSize = True
        CheckBox7.Checked = True
        CheckBox7.CheckState = CheckState.Checked
        CheckBox7.Location = New Point(125, 81)
        CheckBox7.Name = "CheckBox7"
        CheckBox7.Size = New Size(62, 19)
        CheckBox7.TabIndex = 21
        CheckBox7.Text = "Bound"
        CheckBox7.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(114, 18)
        Label4.Name = "Label4"
        Label4.Size = New Size(0, 15)
        Label4.TabIndex = 42
        ' 
        ' ComboBox1
        ' 
        ComboBox1.BackColor = Color.WhiteSmoke
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {""})
        ComboBox1.Location = New Point(140, 15)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(90, 23)
        ComboBox1.TabIndex = 40
        ' 
        ' TextBox6
        ' 
        TextBox6.BackColor = Color.White
        TextBox6.Location = New Point(6, 15)
        TextBox6.Name = "TextBox6"
        TextBox6.Size = New Size(105, 21)
        TextBox6.TabIndex = 39
        ' 
        ' CheckBox11
        ' 
        CheckBox11.AutoSize = True
        CheckBox11.Enabled = False
        CheckBox11.Location = New Point(6, 41)
        CheckBox11.Name = "CheckBox11"
        CheckBox11.Size = New Size(167, 19)
        CheckBox11.TabIndex = 38
        CheckBox11.Text = "With an undefined sister x"
        CheckBox11.UseVisualStyleBackColor = True
        ' 
        ' TextBox7
        ' 
        TextBox7.Location = New Point(150, 65)
        TextBox7.Name = "TextBox7"
        TextBox7.ReadOnly = True
        TextBox7.Size = New Size(86, 21)
        TextBox7.TabIndex = 37
        ' 
        ' CheckBox10
        ' 
        CheckBox10.AutoSize = True
        CheckBox10.Enabled = False
        CheckBox10.Location = New Point(6, 67)
        CheckBox10.Name = "CheckBox10"
        CheckBox10.Size = New Size(107, 19)
        CheckBox10.TabIndex = 36
        CheckBox10.Text = "With omitted in"
        CheckBox10.UseVisualStyleBackColor = True
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(TextBox6)
        GroupBox1.Controls.Add(TextBox7)
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(CheckBox11)
        GroupBox1.Controls.Add(CheckBox10)
        GroupBox1.Controls.Add(ComboBox1)
        GroupBox1.Location = New Point(6, 144)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(255, 91)
        GroupBox1.TabIndex = 44
        GroupBox1.TabStop = False
        GroupBox1.Visible = False
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Location = New Point(2, 28)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(401, 286)
        TabControl1.TabIndex = 49
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(DataGridView1)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(393, 258)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Range constraints"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(DataGridView2)
        TabPage2.Location = New Point(4, 26)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3)
        TabPage2.Size = New Size(393, 256)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Fossils"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' DataGridView2
        ' 
        DataGridView2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(2, 2)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
        DataGridView2.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing
        DataGridView2.RowTemplate.Height = 23
        DataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DataGridView2.Size = New Size(389, 253)
        DataGridView2.TabIndex = 67
        ' 
        ' CheckBox13
        ' 
        CheckBox13.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CheckBox13.AutoSize = True
        CheckBox13.Location = New Point(131, 447)
        CheckBox13.Name = "CheckBox13"
        CheckBox13.Size = New Size(129, 19)
        CheckBox13.TabIndex = 52
        CheckBox13.Text = "Hide CMD Window"
        CheckBox13.UseVisualStyleBackColor = True
        CheckBox13.Visible = False
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Label5.AutoSize = True
        Label5.Location = New Point(6, 448)
        Label5.Name = "Label5"
        Label5.Size = New Size(56, 15)
        Label5.TabIndex = 51
        Label5.Text = "Threads:"
        ' 
        ' NumericUpDown3
        ' 
        NumericUpDown3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        NumericUpDown3.BackColor = Color.White
        NumericUpDown3.Location = New Point(63, 446)
        NumericUpDown3.Maximum = New Decimal(New Integer() {1024, 0, 0, 0})
        NumericUpDown3.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown3.Name = "NumericUpDown3"
        NumericUpDown3.Size = New Size(57, 21)
        NumericUpDown3.TabIndex = 50
        NumericUpDown3.TextAlign = HorizontalAlignment.Right
        NumericUpDown3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Config_SDIVA
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(662, 472)
        ControlBox = False
        Controls.Add(CheckBox13)
        Controls.Add(Label5)
        Controls.Add(NumericUpDown3)
        Controls.Add(TabControl1)
        Controls.Add(GroupBox1)
        Controls.Add(CheckBox9)
        Controls.Add(TextBox1)
        Controls.Add(TextBox4)
        Controls.Add(CheckBox12)
        Controls.Add(CheckBox7)
        Controls.Add(GroupBox2)
        Controls.Add(Label3)
        Controls.Add(Label2)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Controls.Add(ListBox2)
        Controls.Add(ListBox1)
        Controls.Add(MenuStrip1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        MainMenuStrip = MenuStrip1
        MaximizeBox = False
        MinimizeBox = False
        Name = "Config_SDIVA"
        StartPosition = FormStartPosition.CenterScreen
        Text = "S-DIVA"
        TopMost = True
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage2.ResumeLayout(False)
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown3, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents OperationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshTheRangeListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox9 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox4 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox8 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox7 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox4 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox2 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox12 As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox6 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox11 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox7 As System.Windows.Forms.TextBox
    Friend WithEvents CheckBox10 As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox5 As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents SaveSettingsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LoadSettingToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CheckBox6 As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox13 As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents NumericUpDown3 As System.Windows.Forms.NumericUpDown
End Class
