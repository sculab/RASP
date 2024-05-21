<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config_BayArea
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
        TextBox3 = New TextBox()
        Label4 = New Label()
        Label1 = New Label()
        TextBox1 = New TextBox()
        ComboBox1 = New ComboBox()
        Label7 = New Label()
        DataGridView1 = New DataGridView()
        Label2 = New Label()
        Label3 = New Label()
        Label5 = New Label()
        ComboBox4 = New ComboBox()
        ComboBox2 = New ComboBox()
        ComboBox3 = New ComboBox()
        TextBox2 = New TextBox()
        GroupBox1 = New GroupBox()
        Button5 = New Button()
        Button2 = New Button()
        Button1 = New Button()
        Button3 = New Button()
        Button4 = New Button()
        CheckBox1 = New CheckBox()
        TextBox5 = New TextBox()
        GroupBox2 = New GroupBox()
        CType(DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' TextBox3
        ' 
        TextBox3.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox3.Location = New Point(517, 16)
        TextBox3.Margin = New Padding(3, 4, 3, 4)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(124, 21)
        TextBox3.TabIndex = 58
        TextBox3.Text = "5000000"
        ' 
        ' Label4
        ' 
        Label4.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label4.Location = New Point(343, 36)
        Label4.Name = "Label4"
        Label4.Size = New Size(154, 38)
        Label4.TabIndex = 54
        Label4.Text = "Frequent of samples"
        Label4.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label1
        ' 
        Label1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(343, 7)
        Label1.Name = "Label1"
        Label1.Size = New Size(191, 38)
        Label1.TabIndex = 55
        Label1.Text = "Chain Length"
        Label1.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' TextBox1
        ' 
        TextBox1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox1.Location = New Point(517, 45)
        TextBox1.Margin = New Padding(3, 4, 3, 4)
        TextBox1.Name = "TextBox1"
        TextBox1.Size = New Size(124, 21)
        TextBox1.TabIndex = 57
        TextBox1.Text = "1000"
        ' 
        ' ComboBox1
        ' 
        ComboBox1.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox1.Enabled = False
        ComboBox1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox1.FormattingEnabled = True
        ComboBox1.Items.AddRange(New Object() {"INDEPENDENCE", "DISTANCE NORM"})
        ComboBox1.Location = New Point(517, 73)
        ComboBox1.Margin = New Padding(3, 4, 3, 4)
        ComboBox1.Name = "ComboBox1"
        ComboBox1.Size = New Size(124, 23)
        ComboBox1.TabIndex = 61
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label7.Location = New Point(343, 76)
        Label7.Name = "Label7"
        Label7.Size = New Size(68, 15)
        Label7.TabIndex = 62
        Label7.Text = "Model Type"
        ' 
        ' DataGridView1
        ' 
        DataGridView1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        DataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridView1.Location = New Point(7, 17)
        DataGridView1.Margin = New Padding(3, 4, 3, 4)
        DataGridView1.Name = "DataGridView1"
        DataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single
        DataGridView1.RowTemplate.Height = 23
        DataGridView1.Size = New Size(316, 313)
        DataGridView1.TabIndex = 63
        ' 
        ' Label2
        ' 
        Label2.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.Location = New Point(343, 126)
        Label2.Name = "Label2"
        Label2.Size = New Size(229, 38)
        Label2.TabIndex = 64
        Label2.Text = "Geo Distance Power"
        Label2.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label3
        ' 
        Label3.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.Location = New Point(343, 95)
        Label3.Name = "Label3"
        Label3.Size = New Size(191, 38)
        Label3.TabIndex = 65
        Label3.Text = "Guess Initial Rates"
        Label3.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' Label5
        ' 
        Label5.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Label5.Location = New Point(343, 157)
        Label5.Name = "Label5"
        Label5.Size = New Size(191, 38)
        Label5.TabIndex = 68
        Label5.Text = "Geo Distance Truncate"
        Label5.TextAlign = ContentAlignment.MiddleLeft
        ' 
        ' ComboBox4
        ' 
        ComboBox4.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox4.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox4.FormattingEnabled = True
        ComboBox4.Items.AddRange(New Object() {"T", "F"})
        ComboBox4.Location = New Point(517, 104)
        ComboBox4.Margin = New Padding(3, 4, 3, 4)
        ComboBox4.Name = "ComboBox4"
        ComboBox4.Size = New Size(124, 23)
        ComboBox4.TabIndex = 71
        ' 
        ' ComboBox2
        ' 
        ComboBox2.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox2.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox2.FormattingEnabled = True
        ComboBox2.Items.AddRange(New Object() {"T", "F"})
        ComboBox2.Location = New Point(517, 135)
        ComboBox2.Margin = New Padding(3, 4, 3, 4)
        ComboBox2.Name = "ComboBox2"
        ComboBox2.Size = New Size(124, 23)
        ComboBox2.TabIndex = 72
        ' 
        ' ComboBox3
        ' 
        ComboBox3.DropDownStyle = ComboBoxStyle.DropDownList
        ComboBox3.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        ComboBox3.FormattingEnabled = True
        ComboBox3.Items.AddRange(New Object() {"T", "F"})
        ComboBox3.Location = New Point(518, 166)
        ComboBox3.Margin = New Padding(3, 4, 3, 4)
        ComboBox3.Name = "ComboBox3"
        ComboBox3.Size = New Size(124, 23)
        ComboBox3.TabIndex = 73
        ' 
        ' TextBox2
        ' 
        TextBox2.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left Or AnchorStyles.Right
        TextBox2.Location = New Point(7, 20)
        TextBox2.Margin = New Padding(3, 4, 3, 4)
        TextBox2.Multiline = True
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(292, 81)
        TextBox2.TabIndex = 74
        TextBox2.Text = "-gainPrior=1.0" & vbCrLf & "-lossPrior=1.0" & vbCrLf & "-distancePowerPrior=1.0" & vbCrLf & "-areaProposalTuner=0.2"
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Anchor = AnchorStyles.Top Or AnchorStyles.Bottom Or AnchorStyles.Left
        GroupBox1.Controls.Add(Button5)
        GroupBox1.Controls.Add(Button2)
        GroupBox1.Controls.Add(Button1)
        GroupBox1.Controls.Add(DataGridView1)
        GroupBox1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        GroupBox1.ForeColor = Color.Black
        GroupBox1.Location = New Point(3, 5)
        GroupBox1.Margin = New Padding(3, 4, 3, 4)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Padding = New Padding(3, 4, 3, 4)
        GroupBox1.Size = New Size(330, 372)
        GroupBox1.TabIndex = 75
        GroupBox1.TabStop = False
        GroupBox1.Text = "Geographic data"
        ' 
        ' Button5
        ' 
        Button5.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button5.Location = New Point(248, 338)
        Button5.Margin = New Padding(3, 4, 3, 4)
        Button5.Name = "Button5"
        Button5.Size = New Size(75, 26)
        Button5.TabIndex = 65
        Button5.Text = "Clear"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' Button2
        ' 
        Button2.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button2.Location = New Point(87, 338)
        Button2.Margin = New Padding(3, 4, 3, 4)
        Button2.Name = "Button2"
        Button2.Size = New Size(75, 26)
        Button2.TabIndex = 64
        Button2.Text = "Save"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Button1
        ' 
        Button1.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button1.Location = New Point(6, 338)
        Button1.Margin = New Padding(3, 4, 3, 4)
        Button1.Name = "Button1"
        Button1.Size = New Size(75, 26)
        Button1.TabIndex = 64
        Button1.Text = "Load"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Button3
        ' 
        Button3.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button3.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Button3.Location = New Point(346, 343)
        Button3.Margin = New Padding(3, 4, 3, 4)
        Button3.Name = "Button3"
        Button3.Size = New Size(75, 26)
        Button3.TabIndex = 76
        Button3.Text = "OK"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Button4
        ' 
        Button4.Anchor = AnchorStyles.Bottom Or AnchorStyles.Right
        Button4.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        Button4.Location = New Point(563, 343)
        Button4.Margin = New Padding(3, 4, 3, 4)
        Button4.Name = "Button4"
        Button4.Size = New Size(75, 26)
        Button4.TabIndex = 77
        Button4.Text = "Cancel"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' CheckBox1
        ' 
        CheckBox1.AutoSize = True
        CheckBox1.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        CheckBox1.Location = New Point(346, 199)
        CheckBox1.Margin = New Padding(3, 4, 3, 4)
        CheckBox1.Name = "CheckBox1"
        CheckBox1.Size = New Size(151, 19)
        CheckBox1.TabIndex = 82
        CheckBox1.Text = "Save original results to"
        CheckBox1.UseVisualStyleBackColor = True
        ' 
        ' TextBox5
        ' 
        TextBox5.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TextBox5.Location = New Point(518, 197)
        TextBox5.Margin = New Padding(3, 4, 3, 4)
        TextBox5.Name = "TextBox5"
        TextBox5.ReadOnly = True
        TextBox5.Size = New Size(123, 21)
        TextBox5.TabIndex = 83
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(TextBox2)
        GroupBox2.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        GroupBox2.ForeColor = Color.Black
        GroupBox2.Location = New Point(339, 226)
        GroupBox2.Margin = New Padding(3, 4, 3, 4)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Padding = New Padding(3, 4, 3, 4)
        GroupBox2.Size = New Size(307, 109)
        GroupBox2.TabIndex = 84
        GroupBox2.TabStop = False
        GroupBox2.Text = "Other Options"
        ' 
        ' Config_BayArea
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(646, 378)
        ControlBox = False
        Controls.Add(GroupBox2)
        Controls.Add(TextBox5)
        Controls.Add(CheckBox1)
        Controls.Add(TextBox1)
        Controls.Add(Button4)
        Controls.Add(Button3)
        Controls.Add(GroupBox1)
        Controls.Add(ComboBox3)
        Controls.Add(ComboBox2)
        Controls.Add(ComboBox4)
        Controls.Add(Label5)
        Controls.Add(Label2)
        Controls.Add(ComboBox1)
        Controls.Add(Label7)
        Controls.Add(TextBox3)
        Controls.Add(Label4)
        Controls.Add(Label1)
        Controls.Add(Label3)
        Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(3, 4, 3, 4)
        MaximizeBox = False
        MinimizeBox = False
        Name = "Config_BayArea"
        StartPosition = FormStartPosition.CenterScreen
        Text = "BayArea"
        TopMost = True
        CType(DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
        ResumeLayout(False)
        PerformLayout()

    End Sub
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBox4 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox3 As System.Windows.Forms.ComboBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox5 As System.Windows.Forms.TextBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
End Class
