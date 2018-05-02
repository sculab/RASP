<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DIVA
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
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.RichTextBox2 = New System.Windows.Forms.RichTextBox
        Me.RichTextBox3 = New System.Windows.Forms.RichTextBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.NumericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Parameter3 = New System.Windows.Forms.NumericUpDown
        Me.Parameter2 = New System.Windows.Forms.NumericUpDown
        Me.Parameter1 = New System.Windows.Forms.NumericUpDown
        Me.ListBox1 = New System.Windows.Forms.ListBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.AnalysisToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MakeRangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RunAnaToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshTheRangeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ClearTreeAndDistributionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RestParametersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ListBox2 = New System.Windows.Forms.ListBox
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.DataGridView2 = New System.Windows.Forms.DataGridView
        Me.GroupBox1.SuspendLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Parameter3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Parameter2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Parameter1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RichTextBox1.Location = New System.Drawing.Point(0, 208)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(341, 245)
        Me.RichTextBox1.TabIndex = 4
        Me.RichTextBox1.Text = ""
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(310, 25)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(61, 14)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Max Area:"
        '
        'RichTextBox2
        '
        Me.RichTextBox2.Location = New System.Drawing.Point(7, 17)
        Me.RichTextBox2.Name = "RichTextBox2"
        Me.RichTextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RichTextBox2.Size = New System.Drawing.Size(518, 37)
        Me.RichTextBox2.TabIndex = 15
        Me.RichTextBox2.Text = "((1,(2,(3,(4,5)))),(6,(7,(8,9))));"
        '
        'RichTextBox3
        '
        Me.RichTextBox3.Location = New System.Drawing.Point(7, 16)
        Me.RichTextBox3.Name = "RichTextBox3"
        Me.RichTextBox3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.RichTextBox3.Size = New System.Drawing.Size(518, 42)
        Me.RichTextBox3.TabIndex = 16
        Me.RichTextBox3.Text = "c c ab a b a a a cd;"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.NumericUpDown1)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Parameter3)
        Me.GroupBox1.Controls.Add(Me.Parameter2)
        Me.GroupBox1.Controls.Add(Me.Parameter1)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 154)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox1.Size = New System.Drawing.Size(531, 48)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(158, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(25, 14)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "P3:"
        '
        'NumericUpDown1
        '
        Me.NumericUpDown1.Location = New System.Drawing.Point(374, 20)
        Me.NumericUpDown1.Maximum = New Decimal(New Integer() {15, 0, 0, 0})
        Me.NumericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.NumericUpDown1.Name = "NumericUpDown1"
        Me.NumericUpDown1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.NumericUpDown1.Size = New System.Drawing.Size(39, 22)
        Me.NumericUpDown1.TabIndex = 24
        Me.NumericUpDown1.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(82, 25)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(25, 14)
        Me.Label7.TabIndex = 4
        Me.Label7.Text = "P2:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(5, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(25, 14)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "P1:"
        '
        'Parameter3
        '
        Me.Parameter3.Location = New System.Drawing.Point(189, 20)
        Me.Parameter3.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Parameter3.Name = "Parameter3"
        Me.Parameter3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Parameter3.Size = New System.Drawing.Size(39, 22)
        Me.Parameter3.TabIndex = 2
        Me.Parameter3.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'Parameter2
        '
        Me.Parameter2.Location = New System.Drawing.Point(110, 20)
        Me.Parameter2.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Parameter2.Name = "Parameter2"
        Me.Parameter2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Parameter2.Size = New System.Drawing.Size(39, 22)
        Me.Parameter2.TabIndex = 0
        '
        'Parameter1
        '
        Me.Parameter1.Location = New System.Drawing.Point(33, 20)
        Me.Parameter1.Maximum = New Decimal(New Integer() {10, 0, 0, 0})
        Me.Parameter1.Name = "Parameter1"
        Me.Parameter1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Parameter1.Size = New System.Drawing.Size(39, 22)
        Me.Parameter1.TabIndex = 0
        Me.Parameter1.Value = New Decimal(New Integer() {1, 0, 0, 0})
        '
        'ListBox1
        '
        Me.ListBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 14
        Me.ListBox1.Location = New System.Drawing.Point(347, 241)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(96, 212)
        Me.ListBox1.TabIndex = 18
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AnalysisToolStripMenuItem, Me.RangeToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(733, 24)
        Me.MenuStrip1.TabIndex = 19
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'AnalysisToolStripMenuItem
        '
        Me.AnalysisToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MakeRangeToolStripMenuItem, Me.RunAnaToolStripMenuItem})
        Me.AnalysisToolStripMenuItem.Name = "AnalysisToolStripMenuItem"
        Me.AnalysisToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.AnalysisToolStripMenuItem.Text = "Analysis"
        '
        'MakeRangeToolStripMenuItem
        '
        Me.MakeRangeToolStripMenuItem.Name = "MakeRangeToolStripMenuItem"
        Me.MakeRangeToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.MakeRangeToolStripMenuItem.Text = "Make Range"
        '
        'RunAnaToolStripMenuItem
        '
        Me.RunAnaToolStripMenuItem.Enabled = False
        Me.RunAnaToolStripMenuItem.Name = "RunAnaToolStripMenuItem"
        Me.RunAnaToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.RunAnaToolStripMenuItem.Text = "Run Analysis"
        '
        'RangeToolStripMenuItem
        '
        Me.RangeToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshTheRangeToolStripMenuItem, Me.ClearTreeAndDistributionToolStripMenuItem, Me.RestParametersToolStripMenuItem})
        Me.RangeToolStripMenuItem.Name = "RangeToolStripMenuItem"
        Me.RangeToolStripMenuItem.Size = New System.Drawing.Size(60, 20)
        Me.RangeToolStripMenuItem.Text = "Refresh"
        '
        'RefreshTheRangeToolStripMenuItem
        '
        Me.RefreshTheRangeToolStripMenuItem.Name = "RefreshTheRangeToolStripMenuItem"
        Me.RefreshTheRangeToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.RefreshTheRangeToolStripMenuItem.Text = "Refresh The Range List"
        '
        'ClearTreeAndDistributionToolStripMenuItem
        '
        Me.ClearTreeAndDistributionToolStripMenuItem.Name = "ClearTreeAndDistributionToolStripMenuItem"
        Me.ClearTreeAndDistributionToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.ClearTreeAndDistributionToolStripMenuItem.Text = "Clear Tree and Distribution"
        '
        'RestParametersToolStripMenuItem
        '
        Me.RestParametersToolStripMenuItem.Name = "RestParametersToolStripMenuItem"
        Me.RestParametersToolStripMenuItem.Size = New System.Drawing.Size(219, 22)
        Me.RestParametersToolStripMenuItem.Text = "Rest Parameters"
        '
        'ListBox2
        '
        Me.ListBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ListBox2.FormattingEnabled = True
        Me.ListBox2.ItemHeight = 14
        Me.ListBox2.Location = New System.Drawing.Point(449, 241)
        Me.ListBox2.Name = "ListBox2"
        Me.ListBox2.Size = New System.Drawing.Size(87, 212)
        Me.ListBox2.Sorted = True
        Me.ListBox2.TabIndex = 20
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(544, 27)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DataGridView1.RowTemplate.Height = 23
        Me.DataGridView1.Size = New System.Drawing.Size(189, 175)
        Me.DataGridView1.TabIndex = 21
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(347, 208)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(96, 23)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Exclude ->"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.Location = New System.Drawing.Point(449, 208)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(87, 23)
        Me.Button2.TabIndex = 23
        Me.Button2.Text = "<- Include"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RichTextBox2)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox2.Size = New System.Drawing.Size(531, 59)
        Me.GroupBox2.TabIndex = 25
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Tree"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RichTextBox3)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 87)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.GroupBox3.Size = New System.Drawing.Size(531, 67)
        Me.GroupBox3.TabIndex = 26
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Distribution"
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(544, 208)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 23
        Me.DataGridView2.Size = New System.Drawing.Size(189, 245)
        Me.DataGridView2.TabIndex = 27
        '
        'DIVA
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 14.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(733, 453)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ListBox2)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Font = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "DIVA"
        Me.Text = "3 Parameters DIVA"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.NumericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Parameter3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Parameter2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Parameter1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RichTextBox2 As System.Windows.Forms.RichTextBox
    Friend WithEvents RichTextBox3 As System.Windows.Forms.RichTextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Parameter2 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Parameter1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Parameter3 As System.Windows.Forms.NumericUpDown
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents AnalysisToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RunAnaToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents NumericUpDown1 As System.Windows.Forms.NumericUpDown
    Friend WithEvents MakeRangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshTheRangeToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ClearTreeAndDistributionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RestParametersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridView2 As System.Windows.Forms.DataGridView
End Class
