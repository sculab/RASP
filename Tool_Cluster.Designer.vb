<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tool_Cluster
    Inherits System.Windows.Forms.Form

    'Form 重写 Dispose，以清理组件列表。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tool_Cluster))
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveMatrixToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveGraphicToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveTreesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SaveALLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadMatrixToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadIntoRASPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LoadGeneNamesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CalculateMatrixToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SubcladesDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RFDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LeveledRFDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.TripleDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.SPRDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.RFDistanceToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalizedRFDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NormalizedWeightedRFDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.PathDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.WeightedPathDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.KFDistanceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ClusterTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HierarchicalClusterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TestKToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.MCLToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.StatisticsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.HideCMDWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ZoomOutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ZoomInToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.ConsensusTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer_R = New System.Windows.Forms.Timer(Me.components)
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Label5 = New System.Windows.Forms.Label()
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.CheckBox3 = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.NumericUpDown2 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown3 = New System.Windows.Forms.NumericUpDown()
        Me.NumericUpDown4 = New System.Windows.Forms.NumericUpDown()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(590, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(92, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Cluster method"
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(3, 3)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(720, 720)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.AnalysisToolStripMenuItem, Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(690, 25)
        Me.MenuStrip1.TabIndex = 11
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveToolStripMenuItem, Me.LoadToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(39, 21)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveMatrixToolStripMenuItem, Me.SaveGraphicToolStripMenuItem, Me.SaveTreesToolStripMenuItem, Me.SaveALLToolStripMenuItem})
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveMatrixToolStripMenuItem
        '
        Me.SaveMatrixToolStripMenuItem.Name = "SaveMatrixToolStripMenuItem"
        Me.SaveMatrixToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.SaveMatrixToolStripMenuItem.Text = "Save Matrix"
        '
        'SaveGraphicToolStripMenuItem
        '
        Me.SaveGraphicToolStripMenuItem.Name = "SaveGraphicToolStripMenuItem"
        Me.SaveGraphicToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.SaveGraphicToolStripMenuItem.Text = "Save Graphic"
        '
        'SaveTreesToolStripMenuItem
        '
        Me.SaveTreesToolStripMenuItem.Name = "SaveTreesToolStripMenuItem"
        Me.SaveTreesToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.SaveTreesToolStripMenuItem.Text = "Save Selected"
        '
        'SaveALLToolStripMenuItem
        '
        Me.SaveALLToolStripMenuItem.Name = "SaveALLToolStripMenuItem"
        Me.SaveALLToolStripMenuItem.Size = New System.Drawing.Size(156, 22)
        Me.SaveALLToolStripMenuItem.Text = "Save ALL"
        '
        'LoadToolStripMenuItem
        '
        Me.LoadToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LoadMatrixToolStripMenuItem, Me.LoadIntoRASPToolStripMenuItem, Me.LoadGeneNamesToolStripMenuItem})
        Me.LoadToolStripMenuItem.Name = "LoadToolStripMenuItem"
        Me.LoadToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.LoadToolStripMenuItem.Text = "Load"
        '
        'LoadMatrixToolStripMenuItem
        '
        Me.LoadMatrixToolStripMenuItem.Name = "LoadMatrixToolStripMenuItem"
        Me.LoadMatrixToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.LoadMatrixToolStripMenuItem.Text = "Load Matrix From File"
        '
        'LoadIntoRASPToolStripMenuItem
        '
        Me.LoadIntoRASPToolStripMenuItem.Name = "LoadIntoRASPToolStripMenuItem"
        Me.LoadIntoRASPToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.LoadIntoRASPToolStripMenuItem.Text = "Load Into RASP"
        Me.LoadIntoRASPToolStripMenuItem.Visible = False
        '
        'LoadGeneNamesToolStripMenuItem
        '
        Me.LoadGeneNamesToolStripMenuItem.Name = "LoadGeneNamesToolStripMenuItem"
        Me.LoadGeneNamesToolStripMenuItem.Size = New System.Drawing.Size(203, 22)
        Me.LoadGeneNamesToolStripMenuItem.Text = "Load Gene Names"
        Me.LoadGeneNamesToolStripMenuItem.Visible = False
        '
        'AnalysisToolStripMenuItem
        '
        Me.AnalysisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CalculateMatrixToolStripMenuItem, Me.ClusterTreeToolStripMenuItem, Me.ToolStripSeparator2, Me.StatisticsToolStripMenuItem})
        Me.AnalysisToolStripMenuItem.Name = "AnalysisToolStripMenuItem"
        Me.AnalysisToolStripMenuItem.Size = New System.Drawing.Size(66, 21)
        Me.AnalysisToolStripMenuItem.Text = "Analysis"
        '
        'CalculateMatrixToolStripMenuItem
        '
        Me.CalculateMatrixToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SubcladesDistanceToolStripMenuItem, Me.RFDistanceToolStripMenuItem, Me.LeveledRFDistanceToolStripMenuItem, Me.ToolStripSeparator4, Me.TripleDistanceToolStripMenuItem, Me.ToolStripSeparator5, Me.SPRDistanceToolStripMenuItem, Me.ToolStripSeparator6, Me.RFDistanceToolStripMenuItem1, Me.NormalizedRFDistanceToolStripMenuItem, Me.NormalizedWeightedRFDistanceToolStripMenuItem, Me.ToolStripSeparator7, Me.PathDistanceToolStripMenuItem, Me.WeightedPathDistanceToolStripMenuItem, Me.ToolStripSeparator8, Me.KFDistanceToolStripMenuItem})
        Me.CalculateMatrixToolStripMenuItem.Name = "CalculateMatrixToolStripMenuItem"
        Me.CalculateMatrixToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.CalculateMatrixToolStripMenuItem.Text = "Calculate Matrix"
        '
        'SubcladesDistanceToolStripMenuItem
        '
        Me.SubcladesDistanceToolStripMenuItem.Name = "SubcladesDistanceToolStripMenuItem"
        Me.SubcladesDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.SubcladesDistanceToolStripMenuItem.Text = "Sub-nodes (SN) Distance"
        Me.SubcladesDistanceToolStripMenuItem.Visible = False
        '
        'RFDistanceToolStripMenuItem
        '
        Me.RFDistanceToolStripMenuItem.Name = "RFDistanceToolStripMenuItem"
        Me.RFDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.RFDistanceToolStripMenuItem.Text = "Normalized SN Distance"
        Me.RFDistanceToolStripMenuItem.Visible = False
        '
        'LeveledRFDistanceToolStripMenuItem
        '
        Me.LeveledRFDistanceToolStripMenuItem.Name = "LeveledRFDistanceToolStripMenuItem"
        Me.LeveledRFDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.LeveledRFDistanceToolStripMenuItem.Text = "Weighted SN Distance"
        Me.LeveledRFDistanceToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(272, 6)
        Me.ToolStripSeparator4.Visible = False
        '
        'TripleDistanceToolStripMenuItem
        '
        Me.TripleDistanceToolStripMenuItem.Name = "TripleDistanceToolStripMenuItem"
        Me.TripleDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.TripleDistanceToolStripMenuItem.Text = "Triple Distance"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(272, 6)
        '
        'SPRDistanceToolStripMenuItem
        '
        Me.SPRDistanceToolStripMenuItem.Name = "SPRDistanceToolStripMenuItem"
        Me.SPRDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.SPRDistanceToolStripMenuItem.Text = "SPR Distance"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(272, 6)
        '
        'RFDistanceToolStripMenuItem1
        '
        Me.RFDistanceToolStripMenuItem1.Name = "RFDistanceToolStripMenuItem1"
        Me.RFDistanceToolStripMenuItem1.Size = New System.Drawing.Size(275, 22)
        Me.RFDistanceToolStripMenuItem1.Text = "RF Distance"
        '
        'NormalizedRFDistanceToolStripMenuItem
        '
        Me.NormalizedRFDistanceToolStripMenuItem.Name = "NormalizedRFDistanceToolStripMenuItem"
        Me.NormalizedRFDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.NormalizedRFDistanceToolStripMenuItem.Text = "Normalized RF Distance"
        '
        'NormalizedWeightedRFDistanceToolStripMenuItem
        '
        Me.NormalizedWeightedRFDistanceToolStripMenuItem.Name = "NormalizedWeightedRFDistanceToolStripMenuItem"
        Me.NormalizedWeightedRFDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.NormalizedWeightedRFDistanceToolStripMenuItem.Text = "Normalized Weighted RF Distance"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(272, 6)
        '
        'PathDistanceToolStripMenuItem
        '
        Me.PathDistanceToolStripMenuItem.Name = "PathDistanceToolStripMenuItem"
        Me.PathDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.PathDistanceToolStripMenuItem.Text = "Path Distance"
        '
        'WeightedPathDistanceToolStripMenuItem
        '
        Me.WeightedPathDistanceToolStripMenuItem.Name = "WeightedPathDistanceToolStripMenuItem"
        Me.WeightedPathDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.WeightedPathDistanceToolStripMenuItem.Text = "Weighted Path Distance"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(272, 6)
        '
        'KFDistanceToolStripMenuItem
        '
        Me.KFDistanceToolStripMenuItem.Name = "KFDistanceToolStripMenuItem"
        Me.KFDistanceToolStripMenuItem.Size = New System.Drawing.Size(275, 22)
        Me.KFDistanceToolStripMenuItem.Text = "KF Distance"
        '
        'ClusterTreeToolStripMenuItem
        '
        Me.ClusterTreeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HierarchicalClusterToolStripMenuItem, Me.TestKToolStripMenuItem1, Me.MCLToolStripMenuItem})
        Me.ClusterTreeToolStripMenuItem.Name = "ClusterTreeToolStripMenuItem"
        Me.ClusterTreeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.ClusterTreeToolStripMenuItem.Text = "Cluster Trees"
        '
        'HierarchicalClusterToolStripMenuItem
        '
        Me.HierarchicalClusterToolStripMenuItem.Name = "HierarchicalClusterToolStripMenuItem"
        Me.HierarchicalClusterToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.HierarchicalClusterToolStripMenuItem.Text = "Hierarchical Cluster "
        '
        'TestKToolStripMenuItem1
        '
        Me.TestKToolStripMenuItem1.Name = "TestKToolStripMenuItem1"
        Me.TestKToolStripMenuItem1.Size = New System.Drawing.Size(226, 22)
        Me.TestKToolStripMenuItem1.Text = "Test k"
        '
        'MCLToolStripMenuItem
        '
        Me.MCLToolStripMenuItem.Name = "MCLToolStripMenuItem"
        Me.MCLToolStripMenuItem.Size = New System.Drawing.Size(226, 22)
        Me.MCLToolStripMenuItem.Text = "Markov Cluster Algorithm"
        Me.MCLToolStripMenuItem.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(177, 6)
        Me.ToolStripSeparator2.Visible = False
        '
        'StatisticsToolStripMenuItem
        '
        Me.StatisticsToolStripMenuItem.Name = "StatisticsToolStripMenuItem"
        Me.StatisticsToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.StatisticsToolStripMenuItem.Text = "Statistics"
        Me.StatisticsToolStripMenuItem.Visible = False
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.HideCMDWindowToolStripMenuItem, Me.ToolStripSeparator1, Me.ZoomOutToolStripMenuItem, Me.ZoomInToolStripMenuItem, Me.ToolStripSeparator3, Me.ConsensusTreeToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(47, 21)
        Me.OptionsToolStripMenuItem.Text = "View"
        Me.OptionsToolStripMenuItem.Visible = False
        '
        'HideCMDWindowToolStripMenuItem
        '
        Me.HideCMDWindowToolStripMenuItem.CheckOnClick = True
        Me.HideCMDWindowToolStripMenuItem.Name = "HideCMDWindowToolStripMenuItem"
        Me.HideCMDWindowToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.HideCMDWindowToolStripMenuItem.Text = "Hide CMD Window"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(216, 6)
        '
        'ZoomOutToolStripMenuItem
        '
        Me.ZoomOutToolStripMenuItem.Name = "ZoomOutToolStripMenuItem"
        Me.ZoomOutToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ZoomOutToolStripMenuItem.Text = "Zoom Out"
        '
        'ZoomInToolStripMenuItem
        '
        Me.ZoomInToolStripMenuItem.Name = "ZoomInToolStripMenuItem"
        Me.ZoomInToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ZoomInToolStripMenuItem.Text = "Zoom In"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(216, 6)
        '
        'ConsensusTreeToolStripMenuItem
        '
        Me.ConsensusTreeToolStripMenuItem.Name = "ConsensusTreeToolStripMenuItem"
        Me.ConsensusTreeToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ConsensusTreeToolStripMenuItem.Text = "Rooted Condensed Tree"
        '
        'Timer_R
        '
        Me.Timer_R.Interval = 1000
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader1})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.Location = New System.Drawing.Point(0, 28)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(185, 396)
        Me.ListView1.TabIndex = 14
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "ID"
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "COUNT"
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "DISTANCE"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(593, 242)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 15)
        Me.Label5.TabIndex = 15
        Me.Label5.Text = "Threads"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown1.Location = New System.Drawing.Point(614, 260)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {128, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.Size = New System.Drawing.Size(62, 21)
        Me.NumericUpDown1.TabIndex = 16
        Me.NumericUpDown1.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Location = New System.Drawing.Point(185, 28)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(407, 375)
        Me.Panel1.TabIndex = 18
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(1, 425)
        Me.ProgressBar1.Maximum = 10000
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(689, 23)
        Me.ProgressBar1.TabIndex = 19
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Label6.Location = New System.Drawing.Point(191, 406)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 15)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "Waiting..."
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ComboBox1
        '
        Me.ComboBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"ward.D", "ward.D2", "single", "complete", "average", "mcquitty", "median", "centroid"})
        Me.ComboBox1.Location = New System.Drawing.Point(593, 50)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(83, 23)
        Me.ComboBox1.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(593, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 15)
        Me.Label1.TabIndex = 24
        Me.Label1.Text = "Group by k"
        '
        'TextBox4
        '
        Me.TextBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox4.Location = New System.Drawing.Point(614, 209)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(62, 21)
        Me.TextBox4.TabIndex = 28
        Me.TextBox4.Text = "0.2"
        '
        'CheckBox3
        '
        Me.CheckBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CheckBox3.AutoSize = True
        Me.CheckBox3.Location = New System.Drawing.Point(595, 185)
        Me.CheckBox3.Name = "CheckBox3"
        Me.CheckBox3.Size = New System.Drawing.Size(95, 19)
        Me.CheckBox3.TabIndex = 29
        Me.CheckBox3.Text = "Cut distance"
        Me.CheckBox3.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(596, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(32, 15)
        Me.Label4.TabIndex = 30
        Me.Label4.Text = "from"
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(611, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(17, 15)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "to"
        '
        'NumericUpDown2
        '
        Me.NumericUpDown2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown2.Location = New System.Drawing.Point(634, 131)
        Me.NumericUpDown2.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDown2.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NumericUpDown2.Name = "NumericUpDown2"
        Me.NumericUpDown2.Size = New System.Drawing.Size(42, 21)
        Me.NumericUpDown2.TabIndex = 32
        Me.NumericUpDown2.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'NumericUpDown3
        '
        Me.NumericUpDown3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown3.Location = New System.Drawing.Point(634, 158)
        Me.NumericUpDown3.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDown3.Minimum = New Decimal(New Integer() {4, 0, 0, 0})
        Me.NumericUpDown3.Name = "NumericUpDown3"
        Me.NumericUpDown3.Size = New System.Drawing.Size(42, 21)
        Me.NumericUpDown3.TabIndex = 33
        Me.NumericUpDown3.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'NumericUpDown4
        '
        Me.NumericUpDown4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NumericUpDown4.Location = New System.Drawing.Point(614, 103)
        Me.NumericUpDown4.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        Me.NumericUpDown4.Minimum = New Decimal(New Integer() {2, 0, 0, 0})
        Me.NumericUpDown4.Name = "NumericUpDown4"
        Me.NumericUpDown4.Size = New System.Drawing.Size(62, 21)
        Me.NumericUpDown4.TabIndex = 34
        Me.NumericUpDown4.Value = New Decimal(New Integer() {3, 0, 0, 0})
        '
        'Tool_Cluster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(690, 449)
        Me.Controls.Add(Me.NumericUpDown4)
        Me.Controls.Add(Me.NumericUpDown3)
        Me.Controls.Add(Me.NumericUpDown2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.CheckBox3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.NumericUpDown1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Tool_Cluster"
        Me.Text = "Trees vs. Trees"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.NumericUpDown2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.NumericUpDown4, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnalysisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents CalculateMatrixToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ClusterTreeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer_R As Timer
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents SaveToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveMatrixToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveTreesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveALLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadMatrixToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Label5 As Label
    Friend WithEvents NumericUpDown1 As NumericUpDown
    Friend WithEvents StatisticsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadIntoRASPToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HideCMDWindowToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZoomOutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ZoomInToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Panel1 As Panel
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Label6 As Label
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents RFDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LeveledRFDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SPRDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents LoadGeneNamesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents MCLToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents HierarchicalClusterToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents CheckBox3 As CheckBox
    Friend WithEvents ConsensusTreeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents Label4 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents NumericUpDown2 As NumericUpDown
    Friend WithEvents NumericUpDown3 As NumericUpDown
    Friend WithEvents NumericUpDown4 As NumericUpDown
    Friend WithEvents TestKToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents TripleDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SubcladesDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator5 As ToolStripSeparator
    Friend WithEvents ToolStripSeparator6 As ToolStripSeparator
    Friend WithEvents RFDistanceToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents NormalizedRFDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents NormalizedWeightedRFDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As ToolStripSeparator
    Friend WithEvents PathDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents WeightedPathDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As ToolStripSeparator
    Friend WithEvents KFDistanceToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents SaveGraphicToolStripMenuItem As ToolStripMenuItem
End Class
