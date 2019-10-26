<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Tool_TvS
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tool_TvS))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.AnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FullTableToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.AnalysisToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.StateVsTreesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.FindBestFitTreeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.OptionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ModelTestToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader12 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader11 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader6 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader4 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader5 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader7 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader8 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader9 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader10 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Timer_R = New System.Windows.Forms.Timer(Me.components)
        Me.Label6 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalysisToolStripMenuItem, Me.AnalysisToolStripMenuItem1, Me.OptionToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(560, 25)
        Me.MenuStrip1.TabIndex = 22
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AnalysisToolStripMenuItem
        '
        Me.AnalysisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExportToolStripMenuItem})
        Me.AnalysisToolStripMenuItem.Name = "AnalysisToolStripMenuItem"
        Me.AnalysisToolStripMenuItem.Size = New System.Drawing.Size(39, 21)
        Me.AnalysisToolStripMenuItem.Text = "File"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FullTableToolStripMenuItem})
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(114, 22)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'FullTableToolStripMenuItem
        '
        Me.FullTableToolStripMenuItem.Name = "FullTableToolStripMenuItem"
        Me.FullTableToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.FullTableToolStripMenuItem.Text = "Full Table"
        '
        'AnalysisToolStripMenuItem1
        '
        Me.AnalysisToolStripMenuItem1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.StateVsTreesToolStripMenuItem, Me.FindBestFitTreeToolStripMenuItem})
        Me.AnalysisToolStripMenuItem1.Name = "AnalysisToolStripMenuItem1"
        Me.AnalysisToolStripMenuItem1.Size = New System.Drawing.Size(66, 21)
        Me.AnalysisToolStripMenuItem1.Text = "Analysis"
        '
        'StateVsTreesToolStripMenuItem
        '
        Me.StateVsTreesToolStripMenuItem.Name = "StateVsTreesToolStripMenuItem"
        Me.StateVsTreesToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.StateVsTreesToolStripMenuItem.Text = "State vs. Trees"
        '
        'FindBestFitTreeToolStripMenuItem
        '
        Me.FindBestFitTreeToolStripMenuItem.Name = "FindBestFitTreeToolStripMenuItem"
        Me.FindBestFitTreeToolStripMenuItem.Size = New System.Drawing.Size(180, 22)
        Me.FindBestFitTreeToolStripMenuItem.Text = "Tree vs. States"
        '
        'OptionToolStripMenuItem
        '
        Me.OptionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModelTestToolStripMenuItem})
        Me.OptionToolStripMenuItem.Name = "OptionToolStripMenuItem"
        Me.OptionToolStripMenuItem.Size = New System.Drawing.Size(60, 21)
        Me.OptionToolStripMenuItem.Text = "Option"
        Me.OptionToolStripMenuItem.Visible = False
        '
        'ModelTestToolStripMenuItem
        '
        Me.ModelTestToolStripMenuItem.Checked = True
        Me.ModelTestToolStripMenuItem.CheckOnClick = True
        Me.ModelTestToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ModelTestToolStripMenuItem.Name = "ModelTestToolStripMenuItem"
        Me.ModelTestToolStripMenuItem.Size = New System.Drawing.Size(139, 22)
        Me.ModelTestToolStripMenuItem.Text = "Model test"
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader12, Me.ColumnHeader11, Me.ColumnHeader6, Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5, Me.ColumnHeader7, Me.ColumnHeader8, Me.ColumnHeader9, Me.ColumnHeader10})
        Me.ListView1.FullRowSelect = True
        Me.ListView1.HideSelection = False
        Me.ListView1.Location = New System.Drawing.Point(0, 27)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(559, 333)
        Me.ListView1.TabIndex = 24
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader12
        '
        Me.ColumnHeader12.Text = "tID"
        Me.ColumnHeader12.Width = 0
        '
        'ColumnHeader11
        '
        Me.ColumnHeader11.Text = "ID"
        Me.ColumnHeader11.Width = 40
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "State"
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Tree"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Tag = ""
        Me.ColumnHeader2.Text = "Moran's I"
        Me.ColumnHeader2.Width = 75
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "P-value (I)"
        Me.ColumnHeader3.Width = 75
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "Cmean"
        Me.ColumnHeader4.Width = 75
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "P-value (C)"
        Me.ColumnHeader5.Width = 75
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Pagel's λ"
        Me.ColumnHeader7.Width = 75
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "P-value (λ)"
        Me.ColumnHeader8.Width = 75
        '
        'ColumnHeader9
        '
        Me.ColumnHeader9.Text = "K"
        Me.ColumnHeader9.Width = 75
        '
        'ColumnHeader10
        '
        Me.ColumnHeader10.Text = "P-value (K)"
        Me.ColumnHeader10.Width = 75
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(0, 366)
        Me.ProgressBar1.Maximum = 10000
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(485, 23)
        Me.ProgressBar1.TabIndex = 25
        '
        'Timer_R
        '
        Me.Timer_R.Interval = 1000
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(491, 369)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 15)
        Me.Label6.TabIndex = 26
        Me.Label6.Text = "Waiting..."
        '
        'Tool_TvS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(560, 389)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Label6)
        Me.Font = New System.Drawing.Font("Arial", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Name = "Tool_TvS"
        Me.Text = "Tree and States"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents AnalysisToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents FullTableToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents AnalysisToolStripMenuItem1 As ToolStripMenuItem
    Friend WithEvents FindBestFitTreeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ListView1 As ListView
    Friend WithEvents ColumnHeader1 As ColumnHeader
    Friend WithEvents ColumnHeader2 As ColumnHeader
    Friend WithEvents ColumnHeader3 As ColumnHeader
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents Timer_R As Timer
    Friend WithEvents Label6 As Label
    Friend WithEvents ColumnHeader4 As ColumnHeader
    Friend WithEvents ColumnHeader5 As ColumnHeader
    Friend WithEvents ColumnHeader6 As ColumnHeader
    Friend WithEvents ColumnHeader7 As ColumnHeader
    Friend WithEvents ColumnHeader8 As ColumnHeader
    Friend WithEvents ColumnHeader9 As ColumnHeader
    Friend WithEvents ColumnHeader10 As ColumnHeader
    Friend WithEvents ColumnHeader11 As ColumnHeader
    Friend WithEvents StateVsTreesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ColumnHeader12 As ColumnHeader
    Friend WithEvents OptionToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ModelTestToolStripMenuItem As ToolStripMenuItem
End Class
