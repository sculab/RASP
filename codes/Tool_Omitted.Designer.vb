<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tool_Omitted
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tool_Omitted))
        ComboBox1 = New ComboBox()
        DataGridView1 = New DataGridView()
        Button2 = New Button()
        Button3 = New Button()
        TextBox1 = New TextBox()
        Label3 = New Label()
        Label5 = New Label()
        Label7 = New Label()
        TreeBox = New TextBox()
        Label8 = New Label()
        BurninBox = New TextBox()
        GroupBox2 = New GroupBox()
        Button1 = New Button()
        Button4 = New Button()
        Button5 = New Button()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.FormattingEnabled = True
        ComboBox1.Location = New Point(53, 17)
        ComboBox1.Margin = New Padding(3, 4, 3, 4)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(352, 23)
        ComboBox1.TabIndex = 40
        ' 
        ' DataGridView1
        ' 
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(3, 3)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowTemplate.Height = 23
        DataGridView1.Size = New Size(411, 114)
        DataGridView1.TabIndex = 42
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(3, 245)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(56, 27)
        Button2.TabIndex = 43
        Button2.Text = "Add"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(65, 245)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(56, 27)
        Button3.TabIndex = 44
        Button3.Text = "Delete"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(53, 51)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(352, 21)
        TextBox1.TabIndex = 45
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(6, 20)
        Label3.Name = "Label3"
        Label3.Size = New Size(46, 15)
        Label3.TabIndex = 46
        Label3.Text = "Clade: "
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(6, 54)
        Label5.Name = "Label5"
        Label5.Size = New Size(47, 15)
        Label5.TabIndex = 47
        Label5.Text = "Name: "
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(6, 86)
        Label7.Name = "Label7"
        Label7.Size = New Size(96, 15)
        Label7.TabIndex = 50
        Label7.Text = "Amount of trees:"
        ' 
        ' TreeBox
        ' 
        TreeBox.Location = New Point(108, 83)
        TreeBox.Margin = New Padding(3, 4, 3, 4)
        TreeBox.Name = "TreeBox"
        TreeBox.ReadOnly = True
        TreeBox.Size = New Size(79, 21)
        TreeBox.TabIndex = 49
        TreeBox.Text = "0"
        TreeBox.TextAlign = HorizontalAlignment.Right
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(196, 86)
        Label8.Name = "Label8"
        Label8.Size = New Size(50, 15)
        Label8.TabIndex = 51
        Label8.Text = "Burn-in:"
        ' 
        ' BurninBox
        ' 
        BurninBox.Location = New Point(249, 83)
        BurninBox.Margin = New Padding(3, 4, 3, 4)
        BurninBox.Name = "BurninBox"
        BurninBox.Size = New Size(95, 21)
        BurninBox.TabIndex = 52
        BurninBox.Text = "0"
        BurninBox.TextAlign = HorizontalAlignment.Right
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(BurninBox)
        GroupBox2.Controls.Add(Label7)
        GroupBox2.Controls.Add(Label8)
        GroupBox2.Controls.Add(TextBox1)
        GroupBox2.Controls.Add(Label5)
        GroupBox2.Controls.Add(TreeBox)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(ComboBox1)
        GroupBox2.Location = New Point(3, 125)
        GroupBox2.Margin = New Padding(3, 4, 3, 4)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 4, 3, 4)
        GroupBox2.Size = New Size(411, 112)
        GroupBox2.TabIndex = 53
        GroupBox2.TabStop = False
        GroupBox2.Text = "Trees data set"
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(141, 245)
        Button1.Name = "Button1"
        Button1.Size = New Size(97, 27)
        Button1.TabIndex = 54
        Button1.Text = "Export dataset"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(244, 245)
        Button4.Name = "Button4"
        Button4.Size = New Size(97, 27)
        Button4.TabIndex = 54
        Button4.Text = "Export clade"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(358, 245)
        Button5.Margin = New Padding(3, 4, 3, 4)
        Button5.Name = "Button5"
        Button5.Size = New Size(56, 27)
        Button5.TabIndex = 55
        Button5.Text = "Close"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Tool_Omitted
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(417, 278)
        ControlBox = False
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(Button1)
        Controls.Add(GroupBox2)
        Controls.Add(Button3)
        Controls.Add(Button2)
        Controls.Add(DataGridView1)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(3, 4, 3, 4)
        Name = "Tool_Omitted"
        Text = "Omitted Groups"
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)

    End Sub
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents TreeBox As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents BurninBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As Button
End Class
