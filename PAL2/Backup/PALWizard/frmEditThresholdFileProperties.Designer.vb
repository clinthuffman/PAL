<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditThresholdFileProperties
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OK_Button = New System.Windows.Forms.Button
        Me.Cancel_Button = New System.Windows.Forms.Button
        Me.lblAnalysisCollectionName = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtThresholdTitle = New System.Windows.Forms.TextBox
        Me.txtThresholdFileVersion = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.CheckBox1 = New System.Windows.Forms.CheckBox
        Me.txtThresholdFileDescription = New System.Windows.Forms.TextBox
        Me.lblContentOwners = New System.Windows.Forms.Label
        Me.lblFeedBackEmailAddresses = New System.Windows.Forms.Label
        Me.txtThresholdFileContentOwners = New System.Windows.Forms.TextBox
        Me.txtThresholdFileFeedbackEmailAddresses = New System.Windows.Forms.TextBox
        Me.lblThresholdFilePropertiesDescription = New System.Windows.Forms.Label
        Me.ListBoxOfThresholdFileInheritance = New System.Windows.Forms.ListBox
        Me.btnThresholdFileInheritanceAdd = New System.Windows.Forms.Button
        Me.btnThresholdFileInheritanceRemove = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.btnUp = New System.Windows.Forms.Button
        Me.btnDown = New System.Windows.Forms.Button
        Me.lblInheritsFrom = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(208, 432)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 7
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 8
        Me.Cancel_Button.Text = "Cancel"
        '
        'lblAnalysisCollectionName
        '
        Me.lblAnalysisCollectionName.AutoSize = True
        Me.lblAnalysisCollectionName.Location = New System.Drawing.Point(15, 54)
        Me.lblAnalysisCollectionName.Name = "lblAnalysisCollectionName"
        Me.lblAnalysisCollectionName.Size = New System.Drawing.Size(30, 13)
        Me.lblAnalysisCollectionName.TabIndex = 35
        Me.lblAnalysisCollectionName.Text = "Title:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 41
        Me.Label2.Text = "Version:"
        '
        'txtThresholdTitle
        '
        Me.txtThresholdTitle.Location = New System.Drawing.Point(156, 51)
        Me.txtThresholdTitle.Name = "txtThresholdTitle"
        Me.txtThresholdTitle.Size = New System.Drawing.Size(203, 20)
        Me.txtThresholdTitle.TabIndex = 1
        '
        'txtThresholdFileVersion
        '
        Me.txtThresholdFileVersion.Location = New System.Drawing.Point(156, 77)
        Me.txtThresholdFileVersion.Name = "txtThresholdFileVersion"
        Me.txtThresholdFileVersion.Size = New System.Drawing.Size(80, 20)
        Me.txtThresholdFileVersion.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 175)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 37
        Me.Label1.Text = "Threshold file description:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(282, 168)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(78, 17)
        Me.CheckBox1.TabIndex = 6
        Me.CheckBox1.Text = "WordWrap"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'txtThresholdFileDescription
        '
        Me.txtThresholdFileDescription.Location = New System.Drawing.Point(12, 191)
        Me.txtThresholdFileDescription.Multiline = True
        Me.txtThresholdFileDescription.Name = "txtThresholdFileDescription"
        Me.txtThresholdFileDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtThresholdFileDescription.Size = New System.Drawing.Size(347, 77)
        Me.txtThresholdFileDescription.TabIndex = 5
        '
        'lblContentOwners
        '
        Me.lblContentOwners.AutoSize = True
        Me.lblContentOwners.Location = New System.Drawing.Point(16, 102)
        Me.lblContentOwners.Name = "lblContentOwners"
        Me.lblContentOwners.Size = New System.Drawing.Size(90, 13)
        Me.lblContentOwners.TabIndex = 42
        Me.lblContentOwners.Text = "Content owner(s):"
        '
        'lblFeedBackEmailAddresses
        '
        Me.lblFeedBackEmailAddresses.Location = New System.Drawing.Point(16, 127)
        Me.lblFeedBackEmailAddresses.Name = "lblFeedBackEmailAddresses"
        Me.lblFeedBackEmailAddresses.Size = New System.Drawing.Size(133, 31)
        Me.lblFeedBackEmailAddresses.TabIndex = 43
        Me.lblFeedBackEmailAddresses.Text = "Feeback email addresses (separated by semicolons):"
        '
        'txtThresholdFileContentOwners
        '
        Me.txtThresholdFileContentOwners.Location = New System.Drawing.Point(156, 103)
        Me.txtThresholdFileContentOwners.Name = "txtThresholdFileContentOwners"
        Me.txtThresholdFileContentOwners.Size = New System.Drawing.Size(203, 20)
        Me.txtThresholdFileContentOwners.TabIndex = 3
        '
        'txtThresholdFileFeedbackEmailAddresses
        '
        Me.txtThresholdFileFeedbackEmailAddresses.Location = New System.Drawing.Point(156, 129)
        Me.txtThresholdFileFeedbackEmailAddresses.Name = "txtThresholdFileFeedbackEmailAddresses"
        Me.txtThresholdFileFeedbackEmailAddresses.Size = New System.Drawing.Size(203, 20)
        Me.txtThresholdFileFeedbackEmailAddresses.TabIndex = 4
        Me.txtThresholdFileFeedbackEmailAddresses.Text = "paltool@microsoft.com;"
        '
        'lblThresholdFilePropertiesDescription
        '
        Me.lblThresholdFilePropertiesDescription.Location = New System.Drawing.Point(16, 9)
        Me.lblThresholdFilePropertiesDescription.Name = "lblThresholdFilePropertiesDescription"
        Me.lblThresholdFilePropertiesDescription.Size = New System.Drawing.Size(344, 39)
        Me.lblThresholdFilePropertiesDescription.TabIndex = 46
        Me.lblThresholdFilePropertiesDescription.Text = "The fields on this form describe the PAL tool threshold file. This information is" & _
            " shown in reports generated by this threshold file."
        '
        'ListBoxOfThresholdFileInheritance
        '
        Me.ListBoxOfThresholdFileInheritance.FormattingEnabled = True
        Me.ListBoxOfThresholdFileInheritance.Location = New System.Drawing.Point(12, 297)
        Me.ListBoxOfThresholdFileInheritance.Name = "ListBoxOfThresholdFileInheritance"
        Me.ListBoxOfThresholdFileInheritance.Size = New System.Drawing.Size(224, 108)
        Me.ListBoxOfThresholdFileInheritance.TabIndex = 47
        '
        'btnThresholdFileInheritanceAdd
        '
        Me.btnThresholdFileInheritanceAdd.Location = New System.Drawing.Point(243, 298)
        Me.btnThresholdFileInheritanceAdd.Name = "btnThresholdFileInheritanceAdd"
        Me.btnThresholdFileInheritanceAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdFileInheritanceAdd.TabIndex = 48
        Me.btnThresholdFileInheritanceAdd.Text = "Add..."
        Me.btnThresholdFileInheritanceAdd.UseVisualStyleBackColor = True
        '
        'btnThresholdFileInheritanceRemove
        '
        Me.btnThresholdFileInheritanceRemove.Location = New System.Drawing.Point(243, 327)
        Me.btnThresholdFileInheritanceRemove.Name = "btnThresholdFileInheritanceRemove"
        Me.btnThresholdFileInheritanceRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdFileInheritanceRemove.TabIndex = 49
        Me.btnThresholdFileInheritanceRemove.Text = "Remove"
        Me.btnThresholdFileInheritanceRemove.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnUp
        '
        Me.btnUp.Location = New System.Drawing.Point(243, 357)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(75, 23)
        Me.btnUp.TabIndex = 50
        Me.btnUp.Text = "Move up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'btnDown
        '
        Me.btnDown.Location = New System.Drawing.Point(243, 387)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(75, 23)
        Me.btnDown.TabIndex = 51
        Me.btnDown.Text = "Move down"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'lblInheritsFrom
        '
        Me.lblInheritsFrom.AutoSize = True
        Me.lblInheritsFrom.Location = New System.Drawing.Point(15, 281)
        Me.lblInheritsFrom.Name = "lblInheritsFrom"
        Me.lblInheritsFrom.Size = New System.Drawing.Size(155, 13)
        Me.lblInheritsFrom.TabIndex = 52
        Me.lblInheritsFrom.Text = "Threshold file inheritance order:"
        '
        'frmEditThresholdFileProperties
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(366, 473)
        Me.Controls.Add(Me.lblInheritsFrom)
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnThresholdFileInheritanceRemove)
        Me.Controls.Add(Me.btnThresholdFileInheritanceAdd)
        Me.Controls.Add(Me.ListBoxOfThresholdFileInheritance)
        Me.Controls.Add(Me.lblThresholdFilePropertiesDescription)
        Me.Controls.Add(Me.txtThresholdFileFeedbackEmailAddresses)
        Me.Controls.Add(Me.txtThresholdFileContentOwners)
        Me.Controls.Add(Me.lblFeedBackEmailAddresses)
        Me.Controls.Add(Me.lblContentOwners)
        Me.Controls.Add(Me.lblAnalysisCollectionName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtThresholdTitle)
        Me.Controls.Add(Me.txtThresholdFileVersion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.txtThresholdFileDescription)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditThresholdFileProperties"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Threshold File Properties"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lblAnalysisCollectionName As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtThresholdTitle As System.Windows.Forms.TextBox
    Friend WithEvents txtThresholdFileVersion As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents txtThresholdFileDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblContentOwners As System.Windows.Forms.Label
    Friend WithEvents lblFeedBackEmailAddresses As System.Windows.Forms.Label
    Friend WithEvents txtThresholdFileContentOwners As System.Windows.Forms.TextBox
    Friend WithEvents txtThresholdFileFeedbackEmailAddresses As System.Windows.Forms.TextBox
    Friend WithEvents lblThresholdFilePropertiesDescription As System.Windows.Forms.Label
    Friend WithEvents ListBoxOfThresholdFileInheritance As System.Windows.Forms.ListBox
    Friend WithEvents btnThresholdFileInheritanceAdd As System.Windows.Forms.Button
    Friend WithEvents btnThresholdFileInheritanceRemove As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents lblInheritsFrom As System.Windows.Forms.Label

End Class
