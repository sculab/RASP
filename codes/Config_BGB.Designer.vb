<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_BGB
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
        Button1 = New Button()
        Button2 = New Button()
        Button11 = New Button()
        TabControl1 = New TabControl()
        TabPage1 = New TabPage()
        NumericUpDown2 = New NumericUpDown()
        Label1 = New Label()
        DataGridView1 = New DataGridView()
        Label2 = New Label()
        ListBox1 = New ListBox()
        ListBox2 = New ListBox()
        Label3 = New Label()
        Button3 = New Button()
        Button4 = New Button()
        TabPage2 = New TabPage()
        Label6 = New Label()
        Button14 = New Button()
        ComboBox2 = New ComboBox()
        Button10 = New Button()
        DataGridView3 = New DataGridView()
        Button6 = New Button()
        Button9 = New Button()
        Button5 = New Button()
        TextBox1 = New TextBox()
        Label4 = New Label()
        DataGridView2 = New DataGridView()
        TabPage3 = New TabPage()
        Label7 = New Label()
        DataGridView5 = New DataGridView()
        Button8 = New Button()
        Button7 = New Button()
        DataGridView4 = New DataGridView()
        ListBox4 = New ListBox()
        ListBox3 = New ListBox()
        Button12 = New Button()
        Button13 = New Button()
        ComboBox1 = New ComboBox()
        CheckBox1 = New CheckBox()
        NumericUpDown1 = New NumericUpDown()
        Label5 = New Label()
        ComboBox3 = New ComboBox()
        CheckBox2 = New CheckBox()
        MenuStrip1 = New MenuStrip()
        OperationToolStripMenuItem = New ToolStripMenuItem()
        RefreshTheRangeListToolStripMenuItem = New ToolStripMenuItem()
        SaveSettingsToolStripMenuItem = New ToolStripMenuItem()
        LoadSettingToolStripMenuItem = New ToolStripMenuItem()
        TabControl1.SuspendLayout()
        TabPage1.SuspendLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        TabPage2.SuspendLayout()
        CType(DataGridView3, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).BeginInit()
        TabPage3.SuspendLayout()
        CType(DataGridView5, ComponentModel.ISupportInitialize).BeginInit()
        CType(DataGridView4, ComponentModel.ISupportInitialize).BeginInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).BeginInit()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(400, 317)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(0, 0)
        Button1.TabIndex = 0
        Button1.Text = "Button1"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(391, 225)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(0, 0)
        Button2.TabIndex = 1
        Button2.Text = "Button2"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button11
        ' 
        Button11.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button11.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Button11.Location = New Point(446, 400)
        Button11.Name = "Button11"
        Button11.Size = New Size(68, 28)
        Button11.TabIndex = 40
        Button11.Text = "Reset"
        Button11.UseVisualStyleBackColor = True
        ' 
        ' TabControl1
        ' 
        TabControl1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(TabPage2)
        TabControl1.Controls.Add(TabPage3)
        TabControl1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TabControl1.Location = New Point(0, 28)
        TabControl1.Margin = New Padding(3, 4, 3, 4)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(660, 368)
        TabControl1.TabIndex = 39
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(NumericUpDown2)
        TabPage1.Controls.Add(Label1)
        TabPage1.Controls.Add(DataGridView1)
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(ListBox1)
        TabPage1.Controls.Add(ListBox2)
        TabPage1.Controls.Add(Label3)
        TabPage1.Controls.Add(Button3)
        TabPage1.Controls.Add(Button4)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Margin = New Padding(3, 4, 3, 4)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3, 4, 3, 4)
        TabPage1.Size = New Size(652, 340)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Range constraints"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' NumericUpDown2
        ' 
        NumericUpDown2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        NumericUpDown2.BackColor = Color.White
        NumericUpDown2.Location = New Point(511, 311)
        NumericUpDown2.Margin = New Padding(3, 4, 3, 4)
        NumericUpDown2.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        NumericUpDown2.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown2.Name = "NumericUpDown2"
        NumericUpDown2.ReadOnly = True
        NumericUpDown2.Size = New Size(51, 21)
        NumericUpDown2.TabIndex = 24
        NumericUpDown2.TextAlign = HorizontalAlignment.Right
        NumericUpDown2.Value = New Decimal(New Integer() {2, 0, 0, 0})
        ' 
        ' Label1
        ' 
        Label1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label1.AutoSize = True
        Label1.Location = New Point(439, 314)
        Label1.Name = "Label1"
        Label1.Size = New Size(66, 15)
        Label1.TabIndex = 25
        Label1.Text = "Max areas:"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(0, 0)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.RowTemplate.Height = 23
        DataGridView1.Size = New Size(436, 337)
        DataGridView1.TabIndex = 24
        ' 
        ' Label2
        ' 
        Label2.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label2.AutoSize = True
        Label2.Location = New Point(442, 8)
        Label2.Name = "Label2"
        Label2.Size = New Size(47, 15)
        Label2.TabIndex = 32
        Label2.Text = "Include"
        ' 
        ' ListBox1
        ' 
        ListBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        ListBox1.BorderStyle = BorderStyle.FixedSingle
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(442, 30)
        ListBox1.Margin = New Padding(3, 4, 3, 4)
        ListBox1.Name = "ListBox1"
        ListBox1.Size = New Size(100, 272)
        ListBox1.Sorted = True
        ListBox1.TabIndex = 25
        ' 
        ' ListBox2
        ' 
        ListBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        ListBox2.BorderStyle = BorderStyle.FixedSingle
        ListBox2.FormattingEnabled = True
        ListBox2.ItemHeight = 15
        ListBox2.Location = New Point(549, 30)
        ListBox2.Margin = New Padding(3, 4, 3, 4)
        ListBox2.Name = "ListBox2"
        ListBox2.Size = New Size(100, 272)
        ListBox2.Sorted = True
        ListBox2.TabIndex = 26
        ' 
        ' Label3
        ' 
        Label3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label3.AutoSize = True
        Label3.Location = New Point(602, 8)
        Label3.Name = "Label3"
        Label3.Size = New Size(50, 15)
        Label3.TabIndex = 33
        Label3.Text = "Exclude"
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button3.Location = New Point(498, 4)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(44, 23)
        Button3.TabIndex = 27
        Button3.Text = ">>"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button4.Location = New Point(549, 4)
        Button4.Margin = New Padding(3, 4, 3, 4)
        Button4.Name = "Button4"
        Button4.Size = New Size(44, 23)
        Button4.TabIndex = 28
        Button4.Text = "<<"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' TabPage2
        ' 
        TabPage2.Controls.Add(Label6)
        TabPage2.Controls.Add(Button14)
        TabPage2.Controls.Add(ComboBox2)
        TabPage2.Controls.Add(Button10)
        TabPage2.Controls.Add(DataGridView3)
        TabPage2.Controls.Add(Button6)
        TabPage2.Controls.Add(Button9)
        TabPage2.Controls.Add(Button5)
        TabPage2.Controls.Add(TextBox1)
        TabPage2.Controls.Add(Label4)
        TabPage2.Controls.Add(DataGridView2)
        TabPage2.Location = New Point(4, 24)
        TabPage2.Margin = New Padding(3, 4, 3, 4)
        TabPage2.Name = "TabPage2"
        TabPage2.Padding = New Padding(3, 4, 3, 4)
        TabPage2.Size = New Size(652, 340)
        TabPage2.TabIndex = 1
        TabPage2.Text = "Time-Stratified"
        TabPage2.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label6.AutoSize = True
        Label6.Location = New Point(228, 315)
        Label6.Name = "Label6"
        Label6.Size = New Size(10, 15)
        Label6.TabIndex = 17
        Label6.Text = " "
        ' 
        ' Button14
        ' 
        Button14.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button14.BackColor = Color.Transparent
        Button14.Location = New Point(387, 311)
        Button14.Name = "Button14"
        Button14.Size = New Size(107, 23)
        Button14.TabIndex = 16
        Button14.Text = "Save && Apply"
        Button14.UseVisualStyleBackColor = False
        ' 
        ' ComboBox2
        ' 
        ComboBox2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"dispersal multipliers", "areas allowed", "areas adjacency", "distances"})
        ComboBox2.Location = New Point(500, 310)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(152, 23)
        ComboBox2.TabIndex = 15
        ' 
        ' Button10
        ' 
        Button10.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button10.Location = New Point(81, 310)
        Button10.Margin = New Padding(3, 4, 3, 4)
        Button10.Name = "Button10"
        Button10.Size = New Size(72, 26)
        Button10.TabIndex = 14
        Button10.Text = "Import"
        Button10.UseVisualStyleBackColor = True
        ' 
        ' DataGridView3
        ' 
        DataGridView3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Right
        DataGridView3.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView3.Location = New Point(500, 66)
        DataGridView3.Margin = New Padding(3, 4, 3, 4)
        DataGridView3.Name = "DataGridView3"
        DataGridView3.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView3.RowTemplate.Height = 23
        DataGridView3.Size = New Size(152, 238)
        DataGridView3.TabIndex = 1
        ' 
        ' Button6
        ' 
        Button6.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button6.Location = New Point(580, 32)
        Button6.Margin = New Padding(3, 4, 3, 4)
        Button6.Name = "Button6"
        Button6.Size = New Size(72, 26)
        Button6.TabIndex = 12
        Button6.Text = "Remove"
        Button6.UseVisualStyleBackColor = True
        ' 
        ' Button9
        ' 
        Button9.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        Button9.Location = New Point(3, 310)
        Button9.Margin = New Padding(3, 4, 3, 4)
        Button9.Name = "Button9"
        Button9.Size = New Size(72, 26)
        Button9.TabIndex = 13
        Button9.Text = "Export"
        Button9.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Button5.Location = New Point(500, 32)
        Button5.Margin = New Padding(3, 4, 3, 4)
        Button5.Name = "Button5"
        Button5.Size = New Size(72, 26)
        Button5.TabIndex = 11
        Button5.Text = "Add"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        TextBox1.BackColor = Color.White
        TextBox1.Location = New Point(580, 3)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.ReadOnly = True
        TextBox1.Size = New Size(68, 21)
        TextBox1.TabIndex = 7
        TextBox1.Text = "100"
        ' 
        ' Label4
        ' 
        Label4.Anchor = AnchorStyles.Top Or AnchorStyles.Right
        Label4.AutoSize = True
        Label4.Location = New Point(504, 7)
        Label4.Name = "Label4"
        Label4.Size = New Size(60, 15)
        Label4.TabIndex = 6
        Label4.Text = "Root age:"
        ' 
        ' DataGridView2
        ' 
        DataGridView2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView2.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView2.Location = New Point(0, 0)
        DataGridView2.Margin = New Padding(3, 4, 3, 4)
        DataGridView2.Name = "DataGridView2"
        DataGridView2.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView2.RowTemplate.Height = 23
        DataGridView2.Size = New Size(494, 304)
        DataGridView2.TabIndex = 1
        ' 
        ' TabPage3
        ' 
        TabPage3.Controls.Add(Label7)
        TabPage3.Controls.Add(DataGridView5)
        TabPage3.Controls.Add(Button8)
        TabPage3.Controls.Add(Button7)
        TabPage3.Controls.Add(DataGridView4)
        TabPage3.Controls.Add(ListBox4)
        TabPage3.Controls.Add(ListBox3)
        TabPage3.Location = New Point(4, 24)
        TabPage3.Margin = New Padding(3, 4, 3, 4)
        TabPage3.Name = "TabPage3"
        TabPage3.Size = New Size(652, 340)
        TabPage3.TabIndex = 2
        TabPage3.Text = "Fossil & MRCA"
        TabPage3.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(3, 20)
        Label7.Name = "Label7"
        Label7.Size = New Size(209, 15)
        Label7.TabIndex = 12
        Label7.Text = "Select two taxon to define their MRCA"
        ' 
        ' DataGridView5
        ' 
        DataGridView5.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView5.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView5.Location = New Point(357, 0)
        DataGridView5.Margin = New Padding(3, 4, 3, 4)
        DataGridView5.Name = "DataGridView5"
        DataGridView5.RowTemplate.Height = 23
        DataGridView5.Size = New Size(295, 337)
        DataGridView5.TabIndex = 8
        ' 
        ' Button8
        ' 
        Button8.Location = New Point(218, 16)
        Button8.Margin = New Padding(3, 4, 3, 4)
        Button8.Name = "Button8"
        Button8.Size = New Size(62, 26)
        Button8.TabIndex = 10
        Button8.Text = "Remove"
        Button8.UseVisualStyleBackColor = True
        ' 
        ' Button7
        ' 
        Button7.Location = New Point(286, 16)
        Button7.Margin = New Padding(3, 4, 3, 4)
        Button7.Name = "Button7"
        Button7.Size = New Size(62, 26)
        Button7.TabIndex = 9
        Button7.Text = "Add"
        Button7.UseVisualStyleBackColor = True
        ' 
        ' DataGridView4
        ' 
        DataGridView4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        DataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView4.Location = New Point(249, 50)
        DataGridView4.Margin = New Padding(3, 4, 3, 4)
        DataGridView4.Name = "DataGridView4"
        DataGridView4.RowTemplate.Height = 23
        DataGridView4.Size = New Size(99, 287)
        DataGridView4.TabIndex = 7
        ' 
        ' ListBox4
        ' 
        ListBox4.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ListBox4.FormattingEnabled = True
        ListBox4.ItemHeight = 15
        ListBox4.Location = New Point(125, 50)
        ListBox4.Margin = New Padding(3, 4, 3, 4)
        ListBox4.Name = "ListBox4"
        ListBox4.Size = New Size(116, 274)
        ListBox4.TabIndex = 6
        ' 
        ' ListBox3
        ' 
        ListBox3.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ListBox3.FormattingEnabled = True
        ListBox3.ItemHeight = 15
        ListBox3.Location = New Point(3, 50)
        ListBox3.Margin = New Padding(3, 4, 3, 4)
        ListBox3.Name = "ListBox3"
        ListBox3.Size = New Size(116, 274)
        ListBox3.TabIndex = 5
        ' 
        ' Button12
        ' 
        Button12.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button12.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Button12.Location = New Point(588, 400)
        Button12.Margin = New Padding(3, 4, 3, 4)
        Button12.Name = "Button12"
        Button12.Size = New Size(68, 28)
        Button12.TabIndex = 38
        Button12.Text = "Cancel"
        Button12.UseVisualStyleBackColor = True
        ' 
        ' Button13
        ' 
        Button13.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button13.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Button13.Location = New Point(517, 400)
        Button13.Margin = New Padding(3, 4, 3, 4)
        Button13.Name = "Button13"
        Button13.Size = New Size(68, 28)
        Button13.TabIndex = 37
        Button13.Text = "OK"
        Button13.UseVisualStyleBackColor = True
        ' 
        ' ComboBox1
        ' 
        ComboBox1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox1.BackColor = SystemColors.Window
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"DEC", "DEC+j", "DIVALIKE", "DIVALIKE+j", "BAYAREALIKE", "BAYAREALIKE+j"})
        ComboBox1.Location = New Point(4, 403)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(143, 23)
        ComboBox1.TabIndex = 41
        ' 
        ' CheckBox1
        ' 
        CheckBox1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        CheckBox1.AutoSize = True
        CheckBox1.Location = New Point(153, 406)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(82, 19)
        CheckBox1.TabIndex = 42
        CheckBox1.Text = "Hide CMD"
        CheckBox1.UseVisualStyleBackColor = True
        CheckBox1.Visible = False
        ' 
        ' NumericUpDown1
        ' 
        NumericUpDown1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        NumericUpDown1.BackColor = Color.White
        NumericUpDown1.Location = New Point(387, 404)
        NumericUpDown1.Margin = New Padding(3, 4, 3, 4)
        NumericUpDown1.Maximum = New Decimal(New Integer() {1024, 0, 0, 0})
        NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        NumericUpDown1.Name = "NumericUpDown1"
        NumericUpDown1.ReadOnly = True
        NumericUpDown1.Size = New Size(52, 21)
        NumericUpDown1.TabIndex = 43
        NumericUpDown1.TextAlign = HorizontalAlignment.Right
        NumericUpDown1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        ' 
        ' Label5
        ' 
        Label5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label5.AutoSize = True
        Label5.Location = New Point(331, 406)
        Label5.Name = "Label5"
        Label5.Size = New Size(56, 15)
        Label5.TabIndex = 44
        Label5.Text = "Threads:"
        ' 
        ' ComboBox3
        ' 
        ComboBox3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        ComboBox3.BackColor = SystemColors.Window
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox3.FormattingEnabled = True
        ComboBox3.Items.AddRange(New Object() {"optimx", "GenSA"})
        ComboBox3.Location = New Point(241, 403)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(84, 23)
        ComboBox3.TabIndex = 45
        ComboBox3.Visible = False
        ' 
        ' CheckBox2
        ' 
        CheckBox2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left
        CheckBox2.AutoSize = True
        CheckBox2.Checked = True
        CheckBox2.CheckState = CheckState.Checked
        CheckBox2.Location = New Point(4, 406)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(110, 19)
        CheckBox2.TabIndex = 46
        CheckBox2.Text = "Test +J models"
        CheckBox2.UseVisualStyleBackColor = True
        CheckBox2.Visible = False
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        MenuStrip1.Items.AddRange(New ToolStripItem() {OperationToolStripMenuItem})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(660, 24)
        MenuStrip1.TabIndex = 47
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
        ' Config_BGB
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(660, 432)
        ControlBox = False
        Controls.Add(MenuStrip1)
        Controls.Add(CheckBox2)
        Controls.Add(ComboBox3)
        Controls.Add(NumericUpDown1)
        Controls.Add(Label5)
        Controls.Add(CheckBox1)
        Controls.Add(ComboBox1)
        Controls.Add(Button11)
        Controls.Add(TabControl1)
        Controls.Add(Button12)
        Controls.Add(Button13)
        Controls.Add(Button2)
        Controls.Add(Button1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(3, 4, 3, 4)
        Name = "Config_BGB"
        Text = "BioGeoBEARS"
        TopMost = True
        TabControl1.ResumeLayout(False)
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(NumericUpDown2, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        TabPage2.ResumeLayout(False)
        TabPage2.PerformLayout()
        CType(DataGridView3, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView2, ComponentModel.ISupportInitialize).EndInit()
        TabPage3.ResumeLayout(False)
        TabPage3.PerformLayout()
        CType(DataGridView5, ComponentModel.ISupportInitialize).EndInit()
        CType(DataGridView4, ComponentModel.ISupportInitialize).EndInit()
        CType(NumericUpDown1, ComponentModel.ISupportInitialize).EndInit()
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents NumericUpDown2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents DataGridView3 As System.Windows.Forms.DataGridView
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DataGridView5 As System.Windows.Forms.DataGridView
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents ListBox4 As System.Windows.Forms.ListBox
    Friend WithEvents ListBox3 As System.Windows.Forms.ListBox
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents Button13 As System.Windows.Forms.Button
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents Button14 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ComboBox3 As ComboBox
    Friend WithEvents CheckBox2 As CheckBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents OperationToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshTheRangeListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveSettingsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadSettingToolStripMenuItem As ToolStripMenuItem
End Class
