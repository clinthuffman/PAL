<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditDataSourceCounter
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
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.btnCounterInstanceExclusionRemove = New System.Windows.Forms.Button
        Me.btnCounterInstanceExclusionAdd = New System.Windows.Forms.Button
        Me.ListBoxCounterInstanceExclusions = New System.Windows.Forms.ListBox
        Me.txtCollectionVarName = New System.Windows.Forms.TextBox
        Me.LabelCollectionVarName = New System.Windows.Forms.Label
        Me.LabelDataSourceCounterDataType = New System.Windows.Forms.Label
        Me.ComboBoxDataSourceCounterDataType = New System.Windows.Forms.ComboBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtCounterName = New System.Windows.Forms.TextBox
        Me.lblAnalysisName = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionRemove)
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionAdd)
        Me.GroupBox2.Controls.Add(Me.ListBoxCounterInstanceExclusions)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 149)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(376, 144)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Instance Exclusions"
        '
        'btnCounterInstanceExclusionRemove
        '
        Me.btnCounterInstanceExclusionRemove.Location = New System.Drawing.Point(71, 116)
        Me.btnCounterInstanceExclusionRemove.Name = "btnCounterInstanceExclusionRemove"
        Me.btnCounterInstanceExclusionRemove.Size = New System.Drawing.Size(62, 23)
        Me.btnCounterInstanceExclusionRemove.TabIndex = 8
        Me.btnCounterInstanceExclusionRemove.Text = "Remove"
        Me.btnCounterInstanceExclusionRemove.UseVisualStyleBackColor = True
        '
        'btnCounterInstanceExclusionAdd
        '
        Me.btnCounterInstanceExclusionAdd.Location = New System.Drawing.Point(8, 116)
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
        Me.ListBoxCounterInstanceExclusions.Size = New System.Drawing.Size(363, 95)
        Me.ListBoxCounterInstanceExclusions.TabIndex = 6
        '
        'txtCollectionVarName
        '
        Me.txtCollectionVarName.Location = New System.Drawing.Point(119, 116)
        Me.txtCollectionVarName.Name = "txtCollectionVarName"
        Me.txtCollectionVarName.Size = New System.Drawing.Size(258, 20)
        Me.txtCollectionVarName.TabIndex = 2
        '
        'LabelCollectionVarName
        '
        Me.LabelCollectionVarName.AutoSize = True
        Me.LabelCollectionVarName.Location = New System.Drawing.Point(13, 119)
        Me.LabelCollectionVarName.Name = "LabelCollectionVarName"
        Me.LabelCollectionVarName.Size = New System.Drawing.Size(100, 13)
        Me.LabelCollectionVarName.TabIndex = 10
        Me.LabelCollectionVarName.Text = "CollectionVarName:"
        '
        'LabelDataSourceCounterDataType
        '
        Me.LabelDataSourceCounterDataType.AutoSize = True
        Me.LabelDataSourceCounterDataType.Location = New System.Drawing.Point(12, 93)
        Me.LabelDataSourceCounterDataType.Name = "LabelDataSourceCounterDataType"
        Me.LabelDataSourceCounterDataType.Size = New System.Drawing.Size(60, 13)
        Me.LabelDataSourceCounterDataType.TabIndex = 20
        Me.LabelDataSourceCounterDataType.Text = "Data Type:"
        '
        'ComboBoxDataSourceCounterDataType
        '
        Me.ComboBoxDataSourceCounterDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDataSourceCounterDataType.FormattingEnabled = True
        Me.ComboBoxDataSourceCounterDataType.Items.AddRange(New Object() {"absolute", "byte", "double", "integer", "long", "round", "round1", "round2", "round3", "round4", "round5", "round6", "single"})
        Me.ComboBoxDataSourceCounterDataType.Location = New System.Drawing.Point(75, 85)
        Me.ComboBoxDataSourceCounterDataType.Name = "ComboBoxDataSourceCounterDataType"
        Me.ComboBoxDataSourceCounterDataType.Size = New System.Drawing.Size(162, 21)
        Me.ComboBoxDataSourceCounterDataType.Sorted = True
        Me.ComboBoxDataSourceCounterDataType.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(12, 299)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 9
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(94, 299)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 10
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtCounterName
        '
        Me.txtCounterName.Location = New System.Drawing.Point(75, 59)
        Me.txtCounterName.Name = "txtCounterName"
        Me.txtCounterName.Size = New System.Drawing.Size(302, 20)
        Me.txtCounterName.TabIndex = 0
        '
        'lblAnalysisName
        '
        Me.lblAnalysisName.AutoSize = True
        Me.lblAnalysisName.Location = New System.Drawing.Point(12, 59)
        Me.lblAnalysisName.Name = "lblAnalysisName"
        Me.lblAnalysisName.Size = New System.Drawing.Size(38, 13)
        Me.lblAnalysisName.TabIndex = 28
        Me.lblAnalysisName.Text = "Name:"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(13, 13)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(365, 33)
        Me.lblDescription.TabIndex = 29
        Me.lblDescription.Text = "Use this form to edit a performance counter being used as an additional datasourc" & _
            "e used by the analysis thresholds."
        '
        'frmEditDataSourceCounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 330)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.txtCollectionVarName)
        Me.Controls.Add(Me.lblAnalysisName)
        Me.Controls.Add(Me.LabelCollectionVarName)
        Me.Controls.Add(Me.txtCounterName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LabelDataSourceCounterDataType)
        Me.Controls.Add(Me.ComboBoxDataSourceCounterDataType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmEditDataSourceCounter"
        Me.Text = "Edit DataSource Counter"
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCounterInstanceExclusionRemove As System.Windows.Forms.Button
    Friend WithEvents btnCounterInstanceExclusionAdd As System.Windows.Forms.Button
    Friend WithEvents ListBoxCounterInstanceExclusions As System.Windows.Forms.ListBox
    Friend WithEvents txtCollectionVarName As System.Windows.Forms.TextBox
    Friend WithEvents LabelCollectionVarName As System.Windows.Forms.Label
    Friend WithEvents LabelDataSourceCounterDataType As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDataSourceCounterDataType As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtCounterName As System.Windows.Forms.TextBox
    Friend WithEvents lblAnalysisName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
End Class
