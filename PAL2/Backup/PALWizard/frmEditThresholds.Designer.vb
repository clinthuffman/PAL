<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditThresholds
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditThresholds))
        Me.txtThresholdCode = New System.Windows.Forms.TextBox
        Me.LabelThresholdPriority = New System.Windows.Forms.Label
        Me.txtThresholdPriority = New System.Windows.Forms.TextBox
        Me.LabelThresholdCondition = New System.Windows.Forms.Label
        Me.ComboBoxThresholdCondition = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBoxThresholdCode = New System.Windows.Forms.GroupBox
        Me.btnThresholdCancel = New System.Windows.Forms.Button
        Me.btnThresholdOK = New System.Windows.Forms.Button
        Me.GroupBoxThresholdVariables = New System.Windows.Forms.GroupBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtVarName = New System.Windows.Forms.TextBox
        Me.lblVarName = New System.Windows.Forms.Label
        Me.txtVarDescription = New System.Windows.Forms.TextBox
        Me.lblTVarDescription = New System.Windows.Forms.Label
        Me.ListBoxThresholdVariables = New System.Windows.Forms.ListBox
        Me.ColorDialogThresholdColor = New System.Windows.Forms.ColorDialog
        Me.lblThresholdColorDisplay = New System.Windows.Forms.Label
        Me.txtThresholdName = New System.Windows.Forms.TextBox
        Me.GroupBoxThresholdCode.SuspendLayout()
        Me.GroupBoxThresholdVariables.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtThresholdCode
        '
        Me.txtThresholdCode.AcceptsReturn = True
        Me.txtThresholdCode.AcceptsTab = True
        Me.txtThresholdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtThresholdCode.ImeMode = System.Windows.Forms.ImeMode.Off
        Me.txtThresholdCode.Location = New System.Drawing.Point(6, 13)
        Me.txtThresholdCode.Multiline = True
        Me.txtThresholdCode.Name = "txtThresholdCode"
        Me.txtThresholdCode.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtThresholdCode.Size = New System.Drawing.Size(739, 186)
        Me.txtThresholdCode.TabIndex = 27
        Me.txtThresholdCode.WordWrap = False
        '
        'LabelThresholdPriority
        '
        Me.LabelThresholdPriority.AutoSize = True
        Me.LabelThresholdPriority.Location = New System.Drawing.Point(9, 190)
        Me.LabelThresholdPriority.Name = "LabelThresholdPriority"
        Me.LabelThresholdPriority.Size = New System.Drawing.Size(41, 13)
        Me.LabelThresholdPriority.TabIndex = 26
        Me.LabelThresholdPriority.Text = "Priority:"
        '
        'txtThresholdPriority
        '
        Me.txtThresholdPriority.Location = New System.Drawing.Point(94, 183)
        Me.txtThresholdPriority.Name = "txtThresholdPriority"
        Me.txtThresholdPriority.Size = New System.Drawing.Size(116, 20)
        Me.txtThresholdPriority.TabIndex = 25
        '
        'LabelThresholdCondition
        '
        Me.LabelThresholdCondition.AutoSize = True
        Me.LabelThresholdCondition.Location = New System.Drawing.Point(9, 165)
        Me.LabelThresholdCondition.Name = "LabelThresholdCondition"
        Me.LabelThresholdCondition.Size = New System.Drawing.Size(54, 13)
        Me.LabelThresholdCondition.TabIndex = 21
        Me.LabelThresholdCondition.Text = "Condition:"
        '
        'ComboBoxThresholdCondition
        '
        Me.ComboBoxThresholdCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxThresholdCondition.FormattingEnabled = True
        Me.ComboBoxThresholdCondition.Items.AddRange(New Object() {"Warning", "Critical"})
        Me.ComboBoxThresholdCondition.Location = New System.Drawing.Point(94, 157)
        Me.ComboBoxThresholdCondition.Name = "ComboBoxThresholdCondition"
        Me.ComboBoxThresholdCondition.Size = New System.Drawing.Size(116, 21)
        Me.ComboBoxThresholdCondition.TabIndex = 20
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Name:"
        '
        'GroupBoxThresholdCode
        '
        Me.GroupBoxThresholdCode.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxThresholdCode.Controls.Add(Me.txtThresholdCode)
        Me.GroupBoxThresholdCode.Location = New System.Drawing.Point(15, 219)
        Me.GroupBoxThresholdCode.Name = "GroupBoxThresholdCode"
        Me.GroupBoxThresholdCode.Size = New System.Drawing.Size(751, 205)
        Me.GroupBoxThresholdCode.TabIndex = 32
        Me.GroupBoxThresholdCode.TabStop = False
        Me.GroupBoxThresholdCode.Text = "PowerShell Threshold Code"
        '
        'btnThresholdCancel
        '
        Me.btnThresholdCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnThresholdCancel.Location = New System.Drawing.Point(582, 429)
        Me.btnThresholdCancel.Name = "btnThresholdCancel"
        Me.btnThresholdCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdCancel.TabIndex = 33
        Me.btnThresholdCancel.Text = "Cancel"
        Me.btnThresholdCancel.UseVisualStyleBackColor = True
        '
        'btnThresholdOK
        '
        Me.btnThresholdOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnThresholdOK.Location = New System.Drawing.Point(664, 429)
        Me.btnThresholdOK.Name = "btnThresholdOK"
        Me.btnThresholdOK.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdOK.TabIndex = 34
        Me.btnThresholdOK.Text = "OK"
        Me.btnThresholdOK.UseVisualStyleBackColor = True
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
        Me.GroupBoxThresholdVariables.Location = New System.Drawing.Point(220, 13)
        Me.GroupBoxThresholdVariables.Name = "GroupBoxThresholdVariables"
        Me.GroupBoxThresholdVariables.Size = New System.Drawing.Size(543, 190)
        Me.GroupBoxThresholdVariables.TabIndex = 37
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
        'ColorDialogThresholdColor
        '
        Me.ColorDialogThresholdColor.SolidColorOnly = True
        '
        'lblThresholdColorDisplay
        '
        Me.lblThresholdColorDisplay.Location = New System.Drawing.Point(63, 160)
        Me.lblThresholdColorDisplay.Name = "lblThresholdColorDisplay"
        Me.lblThresholdColorDisplay.Size = New System.Drawing.Size(25, 17)
        Me.lblThresholdColorDisplay.TabIndex = 39
        Me.lblThresholdColorDisplay.Text = "   "
        '
        'txtThresholdName
        '
        Me.txtThresholdName.Location = New System.Drawing.Point(15, 30)
        Me.txtThresholdName.Multiline = True
        Me.txtThresholdName.Name = "txtThresholdName"
        Me.txtThresholdName.Size = New System.Drawing.Size(199, 121)
        Me.txtThresholdName.TabIndex = 40
        '
        'frmEditThresholds
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(775, 464)
        Me.Controls.Add(Me.txtThresholdName)
        Me.Controls.Add(Me.lblThresholdColorDisplay)
        Me.Controls.Add(Me.GroupBoxThresholdVariables)
        Me.Controls.Add(Me.btnThresholdOK)
        Me.Controls.Add(Me.btnThresholdCancel)
        Me.Controls.Add(Me.GroupBoxThresholdCode)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LabelThresholdPriority)
        Me.Controls.Add(Me.txtThresholdPriority)
        Me.Controls.Add(Me.LabelThresholdCondition)
        Me.Controls.Add(Me.ComboBoxThresholdCondition)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(791, 502)
        Me.Name = "frmEditThresholds"
        Me.Text = "Edit Threshold Properties"
        Me.GroupBoxThresholdCode.ResumeLayout(False)
        Me.GroupBoxThresholdCode.PerformLayout()
        Me.GroupBoxThresholdVariables.ResumeLayout(False)
        Me.GroupBoxThresholdVariables.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtThresholdCode As System.Windows.Forms.TextBox
    Friend WithEvents LabelThresholdPriority As System.Windows.Forms.Label
    Friend WithEvents txtThresholdPriority As System.Windows.Forms.TextBox
    Friend WithEvents LabelThresholdCondition As System.Windows.Forms.Label
    Friend WithEvents ComboBoxThresholdCondition As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxThresholdCode As System.Windows.Forms.GroupBox
    Friend WithEvents btnThresholdCancel As System.Windows.Forms.Button
    Friend WithEvents btnThresholdOK As System.Windows.Forms.Button
    Friend WithEvents GroupBoxThresholdVariables As System.Windows.Forms.GroupBox
    Friend WithEvents ListBoxThresholdVariables As System.Windows.Forms.ListBox
    Friend WithEvents txtVarDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblTVarDescription As System.Windows.Forms.Label
    Friend WithEvents txtVarName As System.Windows.Forms.TextBox
    Friend WithEvents lblVarName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ColorDialogThresholdColor As System.Windows.Forms.ColorDialog
    Friend WithEvents lblThresholdColorDisplay As System.Windows.Forms.Label
    Friend WithEvents txtThresholdName As System.Windows.Forms.TextBox
End Class
