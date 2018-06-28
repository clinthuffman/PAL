<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDataSourceCounter
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
        Me.LabelAvgVarName = New System.Windows.Forms.Label
        Me.LabelDataSourceCounterDataType = New System.Windows.Forms.Label
        Me.ComboBoxDataSourceCounterDataType = New System.Windows.Forms.ComboBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtCounterName = New System.Windows.Forms.TextBox
        Me.lblAnalysisName = New System.Windows.Forms.Label
        Me.lblDescription = New System.Windows.Forms.Label
        Me.btnBrowseCounters = New System.Windows.Forms.Button
        Me.btnAddGeneratedCounter = New System.Windows.Forms.Button
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionRemove)
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionAdd)
        Me.GroupBox2.Controls.Add(Me.ListBoxCounterInstanceExclusions)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 170)
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
        Me.btnCounterInstanceExclusionRemove.TabIndex = 9
        Me.btnCounterInstanceExclusionRemove.Text = "Remove"
        Me.btnCounterInstanceExclusionRemove.UseVisualStyleBackColor = True
        '
        'btnCounterInstanceExclusionAdd
        '
        Me.btnCounterInstanceExclusionAdd.Location = New System.Drawing.Point(8, 116)
        Me.btnCounterInstanceExclusionAdd.Name = "btnCounterInstanceExclusionAdd"
        Me.btnCounterInstanceExclusionAdd.Size = New System.Drawing.Size(62, 23)
        Me.btnCounterInstanceExclusionAdd.TabIndex = 8
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
        Me.ListBoxCounterInstanceExclusions.TabIndex = 7
        '
        'txtCollectionVarName
        '
        Me.txtCollectionVarName.Location = New System.Drawing.Point(119, 144)
        Me.txtCollectionVarName.Name = "txtCollectionVarName"
        Me.txtCollectionVarName.Size = New System.Drawing.Size(259, 20)
        Me.txtCollectionVarName.TabIndex = 4
        '
        'LabelAvgVarName
        '
        Me.LabelAvgVarName.AutoSize = True
        Me.LabelAvgVarName.Location = New System.Drawing.Point(13, 147)
        Me.LabelAvgVarName.Name = "LabelAvgVarName"
        Me.LabelAvgVarName.Size = New System.Drawing.Size(100, 13)
        Me.LabelAvgVarName.TabIndex = 12
        Me.LabelAvgVarName.Text = "CollectionVarName:"
        '
        'LabelDataSourceCounterDataType
        '
        Me.LabelDataSourceCounterDataType.AutoSize = True
        Me.LabelDataSourceCounterDataType.Location = New System.Drawing.Point(13, 121)
        Me.LabelDataSourceCounterDataType.Name = "LabelDataSourceCounterDataType"
        Me.LabelDataSourceCounterDataType.Size = New System.Drawing.Size(60, 13)
        Me.LabelDataSourceCounterDataType.TabIndex = 20
        Me.LabelDataSourceCounterDataType.Text = "Data Type:"
        '
        'ComboBoxDataSourceCounterDataType
        '
        Me.ComboBoxDataSourceCounterDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDataSourceCounterDataType.FormattingEnabled = True
        Me.ComboBoxDataSourceCounterDataType.Items.AddRange(New Object() {"integer", "round1", "round2", "round3", "round4", "round5", "round6"})
        Me.ComboBoxDataSourceCounterDataType.Location = New System.Drawing.Point(76, 113)
        Me.ComboBoxDataSourceCounterDataType.Name = "ComboBoxDataSourceCounterDataType"
        Me.ComboBoxDataSourceCounterDataType.Size = New System.Drawing.Size(162, 21)
        Me.ComboBoxDataSourceCounterDataType.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(8, 320)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 10
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(90, 320)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 11
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtCounterName
        '
        Me.txtCounterName.Location = New System.Drawing.Point(76, 87)
        Me.txtCounterName.Name = "txtCounterName"
        Me.txtCounterName.Size = New System.Drawing.Size(302, 20)
        Me.txtCounterName.TabIndex = 1
        '
        'lblAnalysisName
        '
        Me.lblAnalysisName.AutoSize = True
        Me.lblAnalysisName.Location = New System.Drawing.Point(13, 87)
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
        Me.lblDescription.Text = "Use this form to add performance counters as additional datasources used by the a" & _
            "nalysis thresholds."
        '
        'btnBrowseCounters
        '
        Me.btnBrowseCounters.Location = New System.Drawing.Point(76, 58)
        Me.btnBrowseCounters.Name = "btnBrowseCounters"
        Me.btnBrowseCounters.Size = New System.Drawing.Size(131, 23)
        Me.btnBrowseCounters.TabIndex = 0
        Me.btnBrowseCounters.Text = "Browse Counters..."
        Me.btnBrowseCounters.UseVisualStyleBackColor = True
        '
        'btnAddGeneratedCounter
        '
        Me.btnAddGeneratedCounter.Location = New System.Drawing.Point(247, 58)
        Me.btnAddGeneratedCounter.Name = "btnAddGeneratedCounter"
        Me.btnAddGeneratedCounter.Size = New System.Drawing.Size(131, 23)
        Me.btnAddGeneratedCounter.TabIndex = 30
        Me.btnAddGeneratedCounter.Text = "Add Generated..."
        Me.btnAddGeneratedCounter.UseVisualStyleBackColor = True
        '
        'frmAddDataSourceCounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 350)
        Me.Controls.Add(Me.btnAddGeneratedCounter)
        Me.Controls.Add(Me.btnBrowseCounters)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.lblAnalysisName)
        Me.Controls.Add(Me.txtCounterName)
        Me.Controls.Add(Me.txtCollectionVarName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.LabelAvgVarName)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.LabelDataSourceCounterDataType)
        Me.Controls.Add(Me.ComboBoxDataSourceCounterDataType)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmAddDataSourceCounter"
        Me.Text = "Add a DataSource Counter"
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnCounterInstanceExclusionRemove As System.Windows.Forms.Button
    Friend WithEvents btnCounterInstanceExclusionAdd As System.Windows.Forms.Button
    Friend WithEvents ListBoxCounterInstanceExclusions As System.Windows.Forms.ListBox
    Friend WithEvents txtCollectionVarName As System.Windows.Forms.TextBox
    Friend WithEvents LabelAvgVarName As System.Windows.Forms.Label
    Friend WithEvents LabelDataSourceCounterDataType As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDataSourceCounterDataType As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtCounterName As System.Windows.Forms.TextBox
    Friend WithEvents lblAnalysisName As System.Windows.Forms.Label
    Friend WithEvents lblDescription As System.Windows.Forms.Label
    Friend WithEvents btnBrowseCounters As System.Windows.Forms.Button
    Friend WithEvents btnAddGeneratedCounter As System.Windows.Forms.Button
End Class
