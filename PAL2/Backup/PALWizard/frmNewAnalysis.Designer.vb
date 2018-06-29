<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewAnalysis
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
        Me.txtNewAnalysisName = New System.Windows.Forms.TextBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.ComboBoxCategory = New System.Windows.Forms.ComboBox
        Me.lblNewAnalysisName = New System.Windows.Forms.Label
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel = New System.Windows.Forms.Button
        Me.btnNewAnalysisOK = New System.Windows.Forms.Button
        Me.lblNewAnalysisCounter = New System.Windows.Forms.Label
        Me.txtNewAnalysisCounter = New System.Windows.Forms.TextBox
        Me.btnNewAnalysisCounterPicker = New System.Windows.Forms.Button
        Me.txtNewAnalysisDescription = New System.Windows.Forms.TextBox
        Me.lblNewAnalysisDescription = New System.Windows.Forms.Label
        Me.lblNewAnalysisInstructions = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtNewAnalysisName
        '
        Me.txtNewAnalysisName.Location = New System.Drawing.Point(69, 78)
        Me.txtNewAnalysisName.Name = "txtNewAnalysisName"
        Me.txtNewAnalysisName.Size = New System.Drawing.Size(185, 20)
        Me.txtNewAnalysisName.TabIndex = 0
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(11, 112)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 13)
        Me.lblCategory.TabIndex = 9
        Me.lblCategory.Text = "Category:"
        '
        'ComboBoxCategory
        '
        Me.ComboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBoxCategory.FormattingEnabled = True
        Me.ComboBoxCategory.Location = New System.Drawing.Point(69, 104)
        Me.ComboBoxCategory.Name = "ComboBoxCategory"
        Me.ComboBoxCategory.Size = New System.Drawing.Size(185, 21)
        Me.ComboBoxCategory.TabIndex = 1
        '
        'lblNewAnalysisName
        '
        Me.lblNewAnalysisName.AutoSize = True
        Me.lblNewAnalysisName.Location = New System.Drawing.Point(12, 84)
        Me.lblNewAnalysisName.Name = "lblNewAnalysisName"
        Me.lblNewAnalysisName.Size = New System.Drawing.Size(38, 13)
        Me.lblNewAnalysisName.TabIndex = 10
        Me.lblNewAnalysisName.Text = "Name:"
        '
        'btnNewAnalysisCancelbtnNewAnalysisCancel
        '
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.Location = New System.Drawing.Point(100, 284)
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.Name = "btnNewAnalysisCancelbtnNewAnalysisCancel"
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.TabIndex = 3
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.Text = "Cancel"
        Me.btnNewAnalysisCancelbtnNewAnalysisCancel.UseVisualStyleBackColor = True
        '
        'btnNewAnalysisOK
        '
        Me.btnNewAnalysisOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnNewAnalysisOK.Location = New System.Drawing.Point(19, 284)
        Me.btnNewAnalysisOK.Name = "btnNewAnalysisOK"
        Me.btnNewAnalysisOK.Size = New System.Drawing.Size(75, 23)
        Me.btnNewAnalysisOK.TabIndex = 3
        Me.btnNewAnalysisOK.Text = "OK"
        Me.btnNewAnalysisOK.UseVisualStyleBackColor = True
        '
        'lblNewAnalysisCounter
        '
        Me.lblNewAnalysisCounter.AutoSize = True
        Me.lblNewAnalysisCounter.Location = New System.Drawing.Point(11, 135)
        Me.lblNewAnalysisCounter.Name = "lblNewAnalysisCounter"
        Me.lblNewAnalysisCounter.Size = New System.Drawing.Size(47, 13)
        Me.lblNewAnalysisCounter.TabIndex = 12
        Me.lblNewAnalysisCounter.Text = "Counter:"
        '
        'txtNewAnalysisCounter
        '
        Me.txtNewAnalysisCounter.Location = New System.Drawing.Point(68, 129)
        Me.txtNewAnalysisCounter.Name = "txtNewAnalysisCounter"
        Me.txtNewAnalysisCounter.Size = New System.Drawing.Size(186, 20)
        Me.txtNewAnalysisCounter.TabIndex = 2
        '
        'btnNewAnalysisCounterPicker
        '
        Me.btnNewAnalysisCounterPicker.Location = New System.Drawing.Point(69, 49)
        Me.btnNewAnalysisCounterPicker.Name = "btnNewAnalysisCounterPicker"
        Me.btnNewAnalysisCounterPicker.Size = New System.Drawing.Size(185, 23)
        Me.btnNewAnalysisCounterPicker.TabIndex = 13
        Me.btnNewAnalysisCounterPicker.Text = "Browse Counters..."
        Me.btnNewAnalysisCounterPicker.UseVisualStyleBackColor = True
        '
        'txtNewAnalysisDescription
        '
        Me.txtNewAnalysisDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNewAnalysisDescription.Location = New System.Drawing.Point(15, 180)
        Me.txtNewAnalysisDescription.Multiline = True
        Me.txtNewAnalysisDescription.Name = "txtNewAnalysisDescription"
        Me.txtNewAnalysisDescription.Size = New System.Drawing.Size(278, 96)
        Me.txtNewAnalysisDescription.TabIndex = 14
        '
        'lblNewAnalysisDescription
        '
        Me.lblNewAnalysisDescription.AutoSize = True
        Me.lblNewAnalysisDescription.Location = New System.Drawing.Point(12, 164)
        Me.lblNewAnalysisDescription.Name = "lblNewAnalysisDescription"
        Me.lblNewAnalysisDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblNewAnalysisDescription.TabIndex = 15
        Me.lblNewAnalysisDescription.Text = "Description:"
        '
        'lblNewAnalysisInstructions
        '
        Me.lblNewAnalysisInstructions.Location = New System.Drawing.Point(12, 10)
        Me.lblNewAnalysisInstructions.Name = "lblNewAnalysisInstructions"
        Me.lblNewAnalysisInstructions.Size = New System.Drawing.Size(291, 39)
        Me.lblNewAnalysisInstructions.TabIndex = 16
        Me.lblNewAnalysisInstructions.Text = "Create a new analysis. To begin, click the Browse Counters button or fill out the" & _
            " form manually."
        '
        'frmNewAnalysis
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(305, 317)
        Me.Controls.Add(Me.lblNewAnalysisInstructions)
        Me.Controls.Add(Me.lblNewAnalysisDescription)
        Me.Controls.Add(Me.txtNewAnalysisDescription)
        Me.Controls.Add(Me.btnNewAnalysisCounterPicker)
        Me.Controls.Add(Me.lblNewAnalysisCounter)
        Me.Controls.Add(Me.txtNewAnalysisCounter)
        Me.Controls.Add(Me.btnNewAnalysisOK)
        Me.Controls.Add(Me.btnNewAnalysisCancelbtnNewAnalysisCancel)
        Me.Controls.Add(Me.lblNewAnalysisName)
        Me.Controls.Add(Me.lblCategory)
        Me.Controls.Add(Me.ComboBoxCategory)
        Me.Controls.Add(Me.txtNewAnalysisName)
        Me.Name = "frmNewAnalysis"
        Me.Text = "New Analysis"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNewAnalysisName As System.Windows.Forms.TextBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents ComboBoxCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewAnalysisName As System.Windows.Forms.Label
    Friend WithEvents btnNewAnalysisCancelbtnNewAnalysisCancel As System.Windows.Forms.Button
    Friend WithEvents btnNewAnalysisOK As System.Windows.Forms.Button
    Friend WithEvents lblNewAnalysisCounter As System.Windows.Forms.Label
    Friend WithEvents txtNewAnalysisCounter As System.Windows.Forms.TextBox
    Friend WithEvents btnNewAnalysisCounterPicker As System.Windows.Forms.Button
    Friend WithEvents txtNewAnalysisDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblNewAnalysisDescription As System.Windows.Forms.Label
    Friend WithEvents lblNewAnalysisInstructions As System.Windows.Forms.Label
End Class
