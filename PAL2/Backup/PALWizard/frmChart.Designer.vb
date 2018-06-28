<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChart
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmChart))
        Me.txtChartTitle = New System.Windows.Forms.TextBox
        Me.LabelChartTitle = New System.Windows.Forms.Label
        Me.LabelDataSource = New System.Windows.Forms.Label
        Me.ComboBoxDataSource = New System.Windows.Forms.ComboBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblDescription = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCounterInstanceExclusionRemove = New System.Windows.Forms.Button
        Me.btnCounterInstanceExclusionAdd = New System.Windows.Forms.Button
        Me.ListBoxCounterInstanceExclusions = New System.Windows.Forms.ListBox
        Me.GroupBoxWarningThresholdCode = New System.Windows.Forms.GroupBox
        Me.CheckBoxWarningThresholdCode = New System.Windows.Forms.CheckBox
        Me.txtWarningThresholdCode = New System.Windows.Forms.TextBox
        Me.GroupBoxCriticalThresholdCode = New System.Windows.Forms.GroupBox
        Me.CheckBoxCriticalThresholdCode = New System.Windows.Forms.CheckBox
        Me.txtCriticalThresholdCode = New System.Windows.Forms.TextBox
        Me.GroupBoxThresholdVariables = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtVarName = New System.Windows.Forms.TextBox
        Me.lblVarName = New System.Windows.Forms.Label
        Me.txtVarDescription = New System.Windows.Forms.TextBox
        Me.lblTVarDescription = New System.Windows.Forms.Label
        Me.ListBoxThresholdVariables = New System.Windows.Forms.ListBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBoxWarningThresholdCode.SuspendLayout()
        Me.GroupBoxCriticalThresholdCode.SuspendLayout()
        Me.GroupBoxThresholdVariables.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtChartTitle
        '
        Me.txtChartTitle.Location = New System.Drawing.Point(103, 40)
        Me.txtChartTitle.Name = "txtChartTitle"
        Me.txtChartTitle.Size = New System.Drawing.Size(455, 20)
        Me.txtChartTitle.TabIndex = 15
        '
        'LabelChartTitle
        '
        Me.LabelChartTitle.AutoSize = True
        Me.LabelChartTitle.Location = New System.Drawing.Point(12, 43)
        Me.LabelChartTitle.Name = "LabelChartTitle"
        Me.LabelChartTitle.Size = New System.Drawing.Size(30, 13)
        Me.LabelChartTitle.TabIndex = 14
        Me.LabelChartTitle.Text = "Title:"
        '
        'LabelDataSource
        '
        Me.LabelDataSource.AutoSize = True
        Me.LabelDataSource.Location = New System.Drawing.Point(12, 69)
        Me.LabelDataSource.Name = "LabelDataSource"
        Me.LabelDataSource.Size = New System.Drawing.Size(64, 13)
        Me.LabelDataSource.TabIndex = 1
        Me.LabelDataSource.Text = "DataSource"
        '
        'ComboBoxDataSource
        '
        Me.ComboBoxDataSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDataSource.FormattingEnabled = True
        Me.ComboBoxDataSource.Location = New System.Drawing.Point(103, 66)
        Me.ComboBoxDataSource.Name = "ComboBoxDataSource"
        Me.ComboBoxDataSource.Size = New System.Drawing.Size(455, 21)
        Me.ComboBoxDataSource.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(703, 647)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 15
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(622, 647)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 16
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(12, 9)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(453, 28)
        Me.lblDescription.TabIndex = 17
        Me.lblDescription.Text = "Use this form to edit the chart used to graph the counter(s) being analyzed."
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionRemove)
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionAdd)
        Me.GroupBox2.Controls.Add(Me.ListBoxCounterInstanceExclusions)
        Me.GroupBox2.Enabled = False
        Me.GroupBox2.Location = New System.Drawing.Point(564, 40)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(217, 243)
        Me.GroupBox2.TabIndex = 24
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Instance Exclusions"
        '
        'btnCounterInstanceExclusionRemove
        '
        Me.btnCounterInstanceExclusionRemove.Location = New System.Drawing.Point(70, 209)
        Me.btnCounterInstanceExclusionRemove.Name = "btnCounterInstanceExclusionRemove"
        Me.btnCounterInstanceExclusionRemove.Size = New System.Drawing.Size(62, 23)
        Me.btnCounterInstanceExclusionRemove.TabIndex = 8
        Me.btnCounterInstanceExclusionRemove.Text = "Remove"
        Me.btnCounterInstanceExclusionRemove.UseVisualStyleBackColor = True
        '
        'btnCounterInstanceExclusionAdd
        '
        Me.btnCounterInstanceExclusionAdd.Location = New System.Drawing.Point(7, 209)
        Me.btnCounterInstanceExclusionAdd.Name = "btnCounterInstanceExclusionAdd"
        Me.btnCounterInstanceExclusionAdd.Size = New System.Drawing.Size(62, 23)
        Me.btnCounterInstanceExclusionAdd.TabIndex = 7
        Me.btnCounterInstanceExclusionAdd.Text = "Add..."
        Me.btnCounterInstanceExclusionAdd.UseVisualStyleBackColor = True
        '
        'ListBoxCounterInstanceExclusions
        '
        Me.ListBoxCounterInstanceExclusions.FormattingEnabled = True
        Me.ListBoxCounterInstanceExclusions.HorizontalScrollbar = True
        Me.ListBoxCounterInstanceExclusions.Location = New System.Drawing.Point(7, 15)
        Me.ListBoxCounterInstanceExclusions.Name = "ListBoxCounterInstanceExclusions"
        Me.ListBoxCounterInstanceExclusions.Size = New System.Drawing.Size(203, 186)
        Me.ListBoxCounterInstanceExclusions.TabIndex = 6
        '
        'GroupBoxWarningThresholdCode
        '
        Me.GroupBoxWarningThresholdCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxWarningThresholdCode.Controls.Add(Me.CheckBoxWarningThresholdCode)
        Me.GroupBoxWarningThresholdCode.Controls.Add(Me.txtWarningThresholdCode)
        Me.GroupBoxWarningThresholdCode.Location = New System.Drawing.Point(15, 289)
        Me.GroupBoxWarningThresholdCode.Name = "GroupBoxWarningThresholdCode"
        Me.GroupBoxWarningThresholdCode.Size = New System.Drawing.Size(766, 175)
        Me.GroupBoxWarningThresholdCode.TabIndex = 33
        Me.GroupBoxWarningThresholdCode.TabStop = False
        Me.GroupBoxWarningThresholdCode.Text = "Warning Threshold PowerShell Code"
        '
        'CheckBoxWarningThresholdCode
        '
        Me.CheckBoxWarningThresholdCode.AutoSize = True
        Me.CheckBoxWarningThresholdCode.Location = New System.Drawing.Point(236, -1)
        Me.CheckBoxWarningThresholdCode.Name = "CheckBoxWarningThresholdCode"
        Me.CheckBoxWarningThresholdCode.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxWarningThresholdCode.TabIndex = 28
        Me.CheckBoxWarningThresholdCode.Text = "Enabled"
        Me.CheckBoxWarningThresholdCode.UseVisualStyleBackColor = True
        '
        'txtWarningThresholdCode
        '
        Me.txtWarningThresholdCode.AcceptsReturn = True
        Me.txtWarningThresholdCode.AcceptsTab = True
        Me.txtWarningThresholdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtWarningThresholdCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtWarningThresholdCode.Location = New System.Drawing.Point(6, 19)
        Me.txtWarningThresholdCode.Multiline = True
        Me.txtWarningThresholdCode.Name = "txtWarningThresholdCode"
        Me.txtWarningThresholdCode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtWarningThresholdCode.Size = New System.Drawing.Size(754, 150)
        Me.txtWarningThresholdCode.TabIndex = 27
        Me.txtWarningThresholdCode.WordWrap = False
        '
        'GroupBoxCriticalThresholdCode
        '
        Me.GroupBoxCriticalThresholdCode.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxCriticalThresholdCode.Controls.Add(Me.CheckBoxCriticalThresholdCode)
        Me.GroupBoxCriticalThresholdCode.Controls.Add(Me.txtCriticalThresholdCode)
        Me.GroupBoxCriticalThresholdCode.Location = New System.Drawing.Point(15, 470)
        Me.GroupBoxCriticalThresholdCode.Name = "GroupBoxCriticalThresholdCode"
        Me.GroupBoxCriticalThresholdCode.Size = New System.Drawing.Size(766, 175)
        Me.GroupBoxCriticalThresholdCode.TabIndex = 34
        Me.GroupBoxCriticalThresholdCode.TabStop = False
        Me.GroupBoxCriticalThresholdCode.Text = "Critical Threshold PowerShell Code"
        '
        'CheckBoxCriticalThresholdCode
        '
        Me.CheckBoxCriticalThresholdCode.AutoSize = True
        Me.CheckBoxCriticalThresholdCode.Location = New System.Drawing.Point(236, 0)
        Me.CheckBoxCriticalThresholdCode.Name = "CheckBoxCriticalThresholdCode"
        Me.CheckBoxCriticalThresholdCode.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxCriticalThresholdCode.TabIndex = 29
        Me.CheckBoxCriticalThresholdCode.Text = "Enabled"
        Me.CheckBoxCriticalThresholdCode.UseVisualStyleBackColor = True
        '
        'txtCriticalThresholdCode
        '
        Me.txtCriticalThresholdCode.AcceptsReturn = True
        Me.txtCriticalThresholdCode.AcceptsTab = True
        Me.txtCriticalThresholdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCriticalThresholdCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtCriticalThresholdCode.Location = New System.Drawing.Point(6, 19)
        Me.txtCriticalThresholdCode.Multiline = True
        Me.txtCriticalThresholdCode.Name = "txtCriticalThresholdCode"
        Me.txtCriticalThresholdCode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCriticalThresholdCode.Size = New System.Drawing.Size(754, 150)
        Me.txtCriticalThresholdCode.TabIndex = 27
        Me.txtCriticalThresholdCode.WordWrap = False
        '
        'GroupBoxThresholdVariables
        '
        Me.GroupBoxThresholdVariables.Controls.Add(Me.Label2)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.txtVarName)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.lblVarName)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.txtVarDescription)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.lblTVarDescription)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.ListBoxThresholdVariables)
        Me.GroupBoxThresholdVariables.Location = New System.Drawing.Point(15, 93)
        Me.GroupBoxThresholdVariables.Name = "GroupBoxThresholdVariables"
        Me.GroupBoxThresholdVariables.Size = New System.Drawing.Size(543, 190)
        Me.GroupBoxThresholdVariables.TabIndex = 38
        Me.GroupBoxThresholdVariables.TabStop = False
        Me.GroupBoxThresholdVariables.Text = "Variables"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(6, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(221, 34)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Variables that are available to be used in this threshold's PowerShell code."
        '
        'txtVarName
        '
        Me.txtVarName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVarName.Location = New System.Drawing.Point(277, 17)
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.ReadOnly = True
        Me.txtVarName.Size = New System.Drawing.Size(260, 20)
        Me.txtVarName.TabIndex = 6
        '
        'lblVarName
        '
        Me.lblVarName.AutoSize = True
        Me.lblVarName.Location = New System.Drawing.Point(233, 20)
        Me.lblVarName.Name = "lblVarName"
        Me.lblVarName.Size = New System.Drawing.Size(38, 13)
        Me.lblVarName.TabIndex = 5
        Me.lblVarName.Text = "Name:"
        '
        'txtVarDescription
        '
        Me.txtVarDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVarDescription.Location = New System.Drawing.Point(384, 58)
        Me.txtVarDescription.Multiline = True
        Me.txtVarDescription.Name = "txtVarDescription"
        Me.txtVarDescription.ReadOnly = True
        Me.txtVarDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVarDescription.Size = New System.Drawing.Size(153, 121)
        Me.txtVarDescription.TabIndex = 4
        '
        'lblTVarDescription
        '
        Me.lblTVarDescription.AutoSize = True
        Me.lblTVarDescription.Location = New System.Drawing.Point(387, 40)
        Me.lblTVarDescription.Name = "lblTVarDescription"
        Me.lblTVarDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblTVarDescription.TabIndex = 3
        Me.lblTVarDescription.Text = "Description:"
        '
        'ListBoxThresholdVariables
        '
        Me.ListBoxThresholdVariables.FormattingEnabled = True
        Me.ListBoxThresholdVariables.Location = New System.Drawing.Point(9, 58)
        Me.ListBoxThresholdVariables.Name = "ListBoxThresholdVariables"
        Me.ListBoxThresholdVariables.ScrollAlwaysVisible = True
        Me.ListBoxThresholdVariables.Size = New System.Drawing.Size(369, 121)
        Me.ListBoxThresholdVariables.Sorted = True
        Me.ListBoxThresholdVariables.TabIndex = 0
        '
        'frmChart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(785, 677)
        Me.Controls.Add(Me.GroupBoxThresholdVariables)
        Me.Controls.Add(Me.GroupBoxCriticalThresholdCode)
        Me.Controls.Add(Me.GroupBoxWarningThresholdCode)
        Me.Controls.Add(Me.LabelChartTitle)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.txtChartTitle)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.ComboBoxDataSource)
        Me.Controls.Add(Me.LabelDataSource)
        Me.Controls.Add(Me.btnCancel)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(801, 715)
        Me.Name = "frmChart"
        Me.Text = "Edit Chart"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBoxWarningThresholdCode.ResumeLayout(False)
        Me.GroupBoxWarningThresholdCode.PerformLayout()
        Me.GroupBoxCriticalThresholdCode.ResumeLayout(False)
        Me.GroupBoxCriticalThresholdCode.PerformLayout()
        Me.GroupBoxThresholdVariables.ResumeLayout(False)
        Me.GroupBoxThresholdVariables.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtChartTitle As System.Windows.Forms.TextBox
    Friend WithEvents LabelChartTitle As System.Windows.Forms.Label
    Friend WithEvents LabelDataSource As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDataSource As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCounterInstanceExclusionRemove As System.Windows.Forms.Button
    Friend WithEvents btnCounterInstanceExclusionAdd As System.Windows.Forms.Button
    Friend WithEvents ListBoxCounterInstanceExclusions As System.Windows.Forms.ListBox
    Friend WithEvents GroupBoxWarningThresholdCode As System.Windows.Forms.GroupBox
    Friend WithEvents txtWarningThresholdCode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxCriticalThresholdCode As System.Windows.Forms.GroupBox
    Friend WithEvents txtCriticalThresholdCode As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxWarningThresholdCode As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxCriticalThresholdCode As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxThresholdVariables As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVarName As System.Windows.Forms.TextBox
    Friend WithEvents lblVarName As System.Windows.Forms.Label
    Friend WithEvents txtVarDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblTVarDescription As System.Windows.Forms.Label
    Friend WithEvents ListBoxThresholdVariables As System.Windows.Forms.ListBox
End Class
