<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tool_SvT
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
        MenuStrip1 = New MenuStrip()
        AnalysisToolStripMenuItem = New ToolStripMenuItem()
        ExportToolStripMenuItem = New ToolStripMenuItem()
        FullTableToolStripMenuItem = New ToolStripMenuItem()
        TreesToolStripMenuItem = New ToolStripMenuItem()
        AnalysisToolStripMenuItem1 = New ToolStripMenuItem()
        FindBestFitTreeToolStripMenuItem = New ToolStripMenuItem()
        ListView1 = New ListView()
        ColumnHeader1 = New ColumnHeader()
        ColumnHeader2 = New ColumnHeader()
        ColumnHeader3 = New ColumnHeader()
        ProgressBar1 = New ProgressBar()
        Timer_R = New Timer(components)
        Label6 = New Label()
        ListBox1 = New ListBox()
        MenuStrip1.SuspendLayout()
        SuspendLayout()
        ' 
        ' MenuStrip1
        ' 
        MenuStrip1.Items.AddRange(New ToolStripItem() {AnalysisToolStripMenuItem, AnalysisToolStripMenuItem1})
        MenuStrip1.Location = New Point(0, 0)
        MenuStrip1.Name = "MenuStrip1"
        MenuStrip1.Size = New Size(560, 25)
        MenuStrip1.TabIndex = 0
        MenuStrip1.Text = "MenuStrip1"
        ' 
        ' AnalysisToolStripMenuItem
        ' 
        AnalysisToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {ExportToolStripMenuItem})
        AnalysisToolStripMenuItem.Name = "AnalysisToolStripMenuItem"
        AnalysisToolStripMenuItem.Size = New Size(39, 21)
        AnalysisToolStripMenuItem.Text = "File"
        ' 
        ' ExportToolStripMenuItem
        ' 
        ExportToolStripMenuItem.DropDownItems.AddRange(New ToolStripItem() {FullTableToolStripMenuItem, TreesToolStripMenuItem})
        ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        ExportToolStripMenuItem.Size = New Size(114, 22)
        ExportToolStripMenuItem.Text = "Export"
        ' 
        ' FullTableToolStripMenuItem
        ' 
        FullTableToolStripMenuItem.Name = "FullTableToolStripMenuItem"
        FullTableToolStripMenuItem.Size = New Size(131, 22)
        FullTableToolStripMenuItem.Text = "Full Table"
        ' 
        ' TreesToolStripMenuItem
        ' 
        TreesToolStripMenuItem.Name = "TreesToolStripMenuItem"
        TreesToolStripMenuItem.Size = New Size(131, 22)
        TreesToolStripMenuItem.Text = "Trees"
        ' 
        ' AnalysisToolStripMenuItem1
        ' 
        AnalysisToolStripMenuItem1.DropDownItems.AddRange(New ToolStripItem() {FindBestFitTreeToolStripMenuItem})
        AnalysisToolStripMenuItem1.Name = "AnalysisToolStripMenuItem1"
        AnalysisToolStripMenuItem1.Size = New Size(66, 21)
        AnalysisToolStripMenuItem1.Text = "Analysis"
        ' 
        ' FindBestFitTreeToolStripMenuItem
        ' 
        FindBestFitTreeToolStripMenuItem.Name = "FindBestFitTreeToolStripMenuItem"
        FindBestFitTreeToolStripMenuItem.Size = New Size(160, 22)
        FindBestFitTreeToolStripMenuItem.Text = "State vs. Trees"
        ' 
        ' ListView1
        ' 
        ListView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ListView1.Columns.AddRange(New ColumnHeader() {ColumnHeader1, ColumnHeader2, ColumnHeader3})
        ListView1.FullRowSelect = True
        ListView1.Location = New Point(105, 27)
        ListView1.Name = "ListView1"
        ListView1.Size = New Size(454, 333)
        ListView1.TabIndex = 3
        ListView1.UseCompatibleStateImageBehavior = False
        ListView1.View = View.Details
        ' 
        ' ColumnHeader1
        ' 
        ColumnHeader1.Text = "ID"
        ' 
        ' ColumnHeader2
        ' 
        ColumnHeader2.Text = "Score"
        ' 
        ' ColumnHeader3
        ' 
        ColumnHeader3.Text = "Tree"
        ColumnHeader3.Width = 300
        ' 
        ' ProgressBar1
        ' 
        ProgressBar1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        ProgressBar1.Location = New Point(0, 366)
        ProgressBar1.Maximum = 10000
        ProgressBar1.Name = "ProgressBar1"
        ProgressBar1.Size = New Size(485, 23)
        ProgressBar1.TabIndex = 20
        ' 
        ' Timer_R
        ' 
        Timer_R.Interval = 1000
        ' 
        ' Label6
        ' 
        Label6.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Label6.AutoSize = True
        Label6.Location = New Point(491, 369)
        Label6.Name = "Label6"
        Label6.Size = New Size(57, 15)
        Label6.TabIndex = 21
        Label6.Text = "Waiting..."
        ' 
        ' ListBox1
        ' 
        ListBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        ListBox1.FormattingEnabled = True
        ListBox1.ItemHeight = 15
        ListBox1.Location = New Point(0, 27)
        ListBox1.Name = "ListBox1"
        ListBox1.SelectionMode = SelectionMode.MultiExtended
        ListBox1.Size = New Size(100, 334)
        ListBox1.TabIndex = 1
        ' 
        ' Tool_SvT
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(560, 389)
        Controls.Add(Label6)
        Controls.Add(ProgressBar1)
        Controls.Add(ListView1)
        Controls.Add(ListBox1)
        Controls.Add(MenuStrip1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        MainMenuStrip = MenuStrip1
        Margin = New Padding(3, 4, 3, 4)
        Name = "Tool_SvT"
        Text = "State vs. Trees"
        MenuStrip1.ResumeLayout(False)
        MenuStrip1.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents AnalysisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnalysisToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents FindBestFitTreeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FullTableToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents TreesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Timer_R As Timer
    Friend WithEvents Label6 As Label
    Friend WithEvents ListBox1 As ListBox
End Class
