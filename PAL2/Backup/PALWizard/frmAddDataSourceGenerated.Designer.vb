<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddDataSourceGeneratedCounter
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddDataSourceGeneratedCounter))
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
        Me.GroupBoxThresholdCode = New System.Windows.Forms.GroupBox
        Me.txtCode = New System.Windows.Forms.TextBox
        Me.GroupBoxThresholdVariables = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtVarName = New System.Windows.Forms.TextBox
        Me.lblVarName = New System.Windows.Forms.Label
        Me.txtVarDescription = New System.Windows.Forms.TextBox
        Me.lblTVarDescription = New System.Windows.Forms.Label
        Me.ListBoxThresholdVariables = New System.Windows.Forms.ListBox
        Me.GroupBox2.SuspendLayout()
        Me.GroupBoxThresholdCode.SuspendLayout()
        Me.GroupBoxThresholdVariables.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionRemove)
        Me.GroupBox2.Controls.Add(Me.btnCounterInstanceExclusionAdd)
        Me.GroupBox2.Controls.Add(Me.ListBoxCounterInstanceExclusions)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 133)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(310, 181)
        Me.GroupBox2.TabIndex = 23
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Instance Exclusions"
        '
        'btnCounterInstanceExclusionRemove
        '
        Me.btnCounterInstanceExclusionRemove.Location = New System.Drawing.Point(67, 152)
        Me.btnCounterInstanceExclusionRemove.Name = "btnCounterInstanceExclusionRemove"
        Me.btnCounterInstanceExclusionRemove.Size = New System.Drawing.Size(62, 23)
        Me.btnCounterInstanceExclusionRemove.TabIndex = 9
        Me.btnCounterInstanceExclusionRemove.Text = "Remove"
        Me.btnCounterInstanceExclusionRemove.UseVisualStyleBackColor = True
        '
        'btnCounterInstanceExclusionAdd
        '
        Me.btnCounterInstanceExclusionAdd.Location = New System.Drawing.Point(4, 152)
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
        Me.ListBoxCounterInstanceExclusions.Size = New System.Drawing.Size(297, 134)
        Me.ListBoxCounterInstanceExclusions.TabIndex = 7
        '
        'txtCollectionVarName
        '
        Me.txtCollectionVarName.Location = New System.Drawing.Point(118, 107)
        Me.txtCollectionVarName.Name = "txtCollectionVarName"
        Me.txtCollectionVarName.Size = New System.Drawing.Size(199, 20)
        Me.txtCollectionVarName.TabIndex = 4
        Me.txtCollectionVarName.Text = "$CollectionOfGeneratedCounterInstances"
        '
        'LabelAvgVarName
        '
        Me.LabelAvgVarName.AutoSize = True
        Me.LabelAvgVarName.Location = New System.Drawing.Point(12, 110)
        Me.LabelAvgVarName.Name = "LabelAvgVarName"
        Me.LabelAvgVarName.Size = New System.Drawing.Size(100, 13)
        Me.LabelAvgVarName.TabIndex = 12
        Me.LabelAvgVarName.Text = "CollectionVarName:"
        '
        'LabelDataSourceCounterDataType
        '
        Me.LabelDataSourceCounterDataType.AutoSize = True
        Me.LabelDataSourceCounterDataType.Location = New System.Drawing.Point(12, 84)
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
        Me.ComboBoxDataSourceCounterDataType.Location = New System.Drawing.Point(75, 76)
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
        Me.txtCounterName.Location = New System.Drawing.Point(75, 50)
        Me.txtCounterName.Name = "txtCounterName"
        Me.txtCounterName.Size = New System.Drawing.Size(243, 20)
        Me.txtCounterName.TabIndex = 1
        Me.txtCounterName.Text = "\Object(*)\Counter"
        '
        'lblAnalysisName
        '
        Me.lblAnalysisName.AutoSize = True
        Me.lblAnalysisName.Location = New System.Drawing.Point(12, 50)
        Me.lblAnalysisName.Name = "lblAnalysisName"
        Me.lblAnalysisName.Size = New System.Drawing.Size(38, 13)
        Me.lblAnalysisName.TabIndex = 28
        Me.lblAnalysisName.Text = "Name:"
        '
        'lblDescription
        '
        Me.lblDescription.Location = New System.Drawing.Point(13, 13)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(305, 33)
        Me.lblDescription.TabIndex = 29
        Me.lblDescription.Text = "Use this form to add generated counters as additional datasources used by the ana" & _
            "lysis thresholds."
        '
        'GroupBoxThresholdCode
        '
        Me.GroupBoxThresholdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxThresholdCode.Controls.Add(Me.txtCode)
        Me.GroupBoxThresholdCode.Location = New System.Drawing.Point(324, 209)
        Me.GroupBoxThresholdCode.Name = "GroupBoxThresholdCode"
        Me.GroupBoxThresholdCode.Size = New System.Drawing.Size(458, 129)
        Me.GroupBoxThresholdCode.TabIndex = 33
        Me.GroupBoxThresholdCode.TabStop = False
        Me.GroupBoxThresholdCode.Text = "PowerShell Counter Generation Code"
        '
        'txtCode
        '
        Me.txtCode.AcceptsReturn = True
        Me.txtCode.AcceptsTab = True
        Me.txtCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtCode.Location = New System.Drawing.Point(6, 13)
        Me.txtCode.Multiline = True
        Me.txtCode.Name = "txtCode"
        Me.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtCode.Size = New System.Drawing.Size(446, 110)
        Me.txtCode.TabIndex = 27
        Me.txtCode.WordWrap = False
        '
        'GroupBoxThresholdVariables
        '
        Me.GroupBoxThresholdVariables.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.Label2)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.txtVarName)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.lblVarName)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.txtVarDescription)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.lblTVarDescription)
        Me.GroupBoxThresholdVariables.Controls.Add(Me.ListBoxThresholdVariables)
        Me.GroupBoxThresholdVariables.Location = New System.Drawing.Point(324, 13)
        Me.GroupBoxThresholdVariables.Name = "GroupBoxThresholdVariables"
        Me.GroupBoxThresholdVariables.Size = New System.Drawing.Size(458, 190)
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
        Me.Label2.Text = "Variables that are available to be used in this counter generation PowerShell cod" & _
            "e."
        '
        'txtVarName
        '
        Me.txtVarName.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtVarName.Location = New System.Drawing.Point(277, 17)
        Me.txtVarName.Name = "txtVarName"
        Me.txtVarName.ReadOnly = True
        Me.txtVarName.Size = New System.Drawing.Size(173, 20)
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
        Me.txtVarDescription.Location = New System.Drawing.Point(324, 58)
        Me.txtVarDescription.Multiline = True
        Me.txtVarDescription.Name = "txtVarDescription"
        Me.txtVarDescription.ReadOnly = True
        Me.txtVarDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtVarDescription.Size = New System.Drawing.Size(128, 121)
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
        Me.ListBoxThresholdVariables.Size = New System.Drawing.Size(309, 121)
        Me.ListBoxThresholdVariables.Sorted = True
        Me.ListBoxThresholdVariables.TabIndex = 0
        '
        'frmAddDataSourceGeneratedCounter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(794, 350)
        Me.Controls.Add(Me.GroupBoxThresholdVariables)
        Me.Controls.Add(Me.GroupBoxThresholdCode)
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
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 378)
        Me.Name = "frmAddDataSourceGeneratedCounter"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "Add a Generated DataSource Counter"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBoxThresholdCode.ResumeLayout(False)
        Me.GroupBoxThresholdCode.PerformLayout()
        Me.GroupBoxThresholdVariables.ResumeLayout(False)
        Me.GroupBoxThresholdVariables.PerformLayout()
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
    Friend WithEvents GroupBoxThresholdCode As System.Windows.Forms.GroupBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents GroupBoxThresholdVariables As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtVarName As System.Windows.Forms.TextBox
    Friend WithEvents lblVarName As System.Windows.Forms.Label
    Friend WithEvents txtVarDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblTVarDescription As System.Windows.Forms.Label
    Friend WithEvents ListBoxThresholdVariables As System.Windows.Forms.ListBox
End Class
