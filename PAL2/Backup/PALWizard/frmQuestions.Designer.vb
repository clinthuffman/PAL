<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmQuestions
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
        Me.ListBoxQuestions = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblQuestion = New System.Windows.Forms.Label
        Me.txtQuestion = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtQuestionVarName = New System.Windows.Forms.TextBox
        Me.btnAddQuestion = New System.Windows.Forms.Button
        Me.btnRemoveQuestion = New System.Windows.Forms.Button
        Me.btnUpdateQuestion = New System.Windows.Forms.Button
        Me.lblQuestionDataType = New System.Windows.Forms.Label
        Me.ComboBoxQuestionDataType = New System.Windows.Forms.ComboBox
        Me.lblQuestionDefaultValueString = New System.Windows.Forms.Label
        Me.txtQuestionDefaultValueString = New System.Windows.Forms.TextBox
        Me.lblQuestionDefaultValueBoolean = New System.Windows.Forms.Label
        Me.ComboBoxDefaultValueBoolean = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'ListBoxQuestions
        '
        Me.ListBoxQuestions.FormattingEnabled = True
        Me.ListBoxQuestions.Location = New System.Drawing.Point(12, 49)
        Me.ListBoxQuestions.Name = "ListBoxQuestions"
        Me.ListBoxQuestions.Size = New System.Drawing.Size(120, 173)
        Me.ListBoxQuestions.Sorted = True
        Me.ListBoxQuestions.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 33)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "These are the questions that will be asked of the user for this threshold file."
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(298, 371)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblQuestion
        '
        Me.lblQuestion.Location = New System.Drawing.Point(138, 112)
        Me.lblQuestion.Name = "lblQuestion"
        Me.lblQuestion.Size = New System.Drawing.Size(235, 31)
        Me.lblQuestion.TabIndex = 4
        Me.lblQuestion.Text = "Question: This is the question presented to the user."
        '
        'txtQuestion
        '
        Me.txtQuestion.Location = New System.Drawing.Point(141, 146)
        Me.txtQuestion.Multiline = True
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.Size = New System.Drawing.Size(232, 77)
        Me.txtQuestion.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(138, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(232, 29)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Question Variable Name: This is the variable name referenced in the code of thres" & _
            "holds."
        '
        'txtQuestionVarName
        '
        Me.txtQuestionVarName.Location = New System.Drawing.Point(141, 81)
        Me.txtQuestionVarName.Name = "txtQuestionVarName"
        Me.txtQuestionVarName.Size = New System.Drawing.Size(232, 20)
        Me.txtQuestionVarName.TabIndex = 7
        '
        'btnAddQuestion
        '
        Me.btnAddQuestion.Location = New System.Drawing.Point(12, 229)
        Me.btnAddQuestion.Name = "btnAddQuestion"
        Me.btnAddQuestion.Size = New System.Drawing.Size(57, 23)
        Me.btnAddQuestion.TabIndex = 8
        Me.btnAddQuestion.Text = "Add"
        Me.btnAddQuestion.UseVisualStyleBackColor = True
        '
        'btnRemoveQuestion
        '
        Me.btnRemoveQuestion.Location = New System.Drawing.Point(75, 228)
        Me.btnRemoveQuestion.Name = "btnRemoveQuestion"
        Me.btnRemoveQuestion.Size = New System.Drawing.Size(57, 23)
        Me.btnRemoveQuestion.TabIndex = 9
        Me.btnRemoveQuestion.Text = "Remove"
        Me.btnRemoveQuestion.UseVisualStyleBackColor = True
        '
        'btnUpdateQuestion
        '
        Me.btnUpdateQuestion.Location = New System.Drawing.Point(298, 310)
        Me.btnUpdateQuestion.Name = "btnUpdateQuestion"
        Me.btnUpdateQuestion.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdateQuestion.TabIndex = 10
        Me.btnUpdateQuestion.Text = "Update"
        Me.btnUpdateQuestion.UseVisualStyleBackColor = True
        '
        'lblQuestionDataType
        '
        Me.lblQuestionDataType.AutoSize = True
        Me.lblQuestionDataType.Location = New System.Drawing.Point(138, 238)
        Me.lblQuestionDataType.Name = "lblQuestionDataType"
        Me.lblQuestionDataType.Size = New System.Drawing.Size(57, 13)
        Me.lblQuestionDataType.TabIndex = 11
        Me.lblQuestionDataType.Text = "DataType:"
        '
        'ComboBoxQuestionDataType
        '
        Me.ComboBoxQuestionDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxQuestionDataType.FormattingEnabled = True
        Me.ComboBoxQuestionDataType.Items.AddRange(New Object() {"boolean", "string"})
        Me.ComboBoxQuestionDataType.Location = New System.Drawing.Point(203, 230)
        Me.ComboBoxQuestionDataType.Name = "ComboBoxQuestionDataType"
        Me.ComboBoxQuestionDataType.Size = New System.Drawing.Size(170, 21)
        Me.ComboBoxQuestionDataType.TabIndex = 12
        '
        'lblQuestionDefaultValueString
        '
        Me.lblQuestionDefaultValueString.AutoSize = True
        Me.lblQuestionDefaultValueString.Location = New System.Drawing.Point(138, 265)
        Me.lblQuestionDefaultValueString.Name = "lblQuestionDefaultValueString"
        Me.lblQuestionDefaultValueString.Size = New System.Drawing.Size(82, 13)
        Me.lblQuestionDefaultValueString.TabIndex = 13
        Me.lblQuestionDefaultValueString.Text = "Default Answer:"
        '
        'txtQuestionDefaultValueString
        '
        Me.txtQuestionDefaultValueString.Enabled = False
        Me.txtQuestionDefaultValueString.Location = New System.Drawing.Point(222, 258)
        Me.txtQuestionDefaultValueString.Name = "txtQuestionDefaultValueString"
        Me.txtQuestionDefaultValueString.Size = New System.Drawing.Size(151, 20)
        Me.txtQuestionDefaultValueString.TabIndex = 14
        '
        'lblQuestionDefaultValueBoolean
        '
        Me.lblQuestionDefaultValueBoolean.AutoSize = True
        Me.lblQuestionDefaultValueBoolean.Location = New System.Drawing.Point(138, 292)
        Me.lblQuestionDefaultValueBoolean.Name = "lblQuestionDefaultValueBoolean"
        Me.lblQuestionDefaultValueBoolean.Size = New System.Drawing.Size(82, 13)
        Me.lblQuestionDefaultValueBoolean.TabIndex = 15
        Me.lblQuestionDefaultValueBoolean.Text = "Default Answer:"
        '
        'ComboBoxDefaultValueBoolean
        '
        Me.ComboBoxDefaultValueBoolean.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxDefaultValueBoolean.FormattingEnabled = True
        Me.ComboBoxDefaultValueBoolean.Items.AddRange(New Object() {"True", "False"})
        Me.ComboBoxDefaultValueBoolean.Location = New System.Drawing.Point(222, 283)
        Me.ComboBoxDefaultValueBoolean.Name = "ComboBoxDefaultValueBoolean"
        Me.ComboBoxDefaultValueBoolean.Size = New System.Drawing.Size(151, 21)
        Me.ComboBoxDefaultValueBoolean.TabIndex = 16
        '
        'frmQuestions
        '
        Me.AcceptButton = Me.btnOK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(380, 402)
        Me.Controls.Add(Me.ComboBoxDefaultValueBoolean)
        Me.Controls.Add(Me.lblQuestionDefaultValueBoolean)
        Me.Controls.Add(Me.txtQuestionDefaultValueString)
        Me.Controls.Add(Me.lblQuestionDefaultValueString)
        Me.Controls.Add(Me.ComboBoxQuestionDataType)
        Me.Controls.Add(Me.lblQuestionDataType)
        Me.Controls.Add(Me.btnUpdateQuestion)
        Me.Controls.Add(Me.btnRemoveQuestion)
        Me.Controls.Add(Me.btnAddQuestion)
        Me.Controls.Add(Me.txtQuestionVarName)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtQuestion)
        Me.Controls.Add(Me.lblQuestion)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ListBoxQuestions)
        Me.Name = "frmQuestions"
        Me.Text = "Edit Questions"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ListBoxQuestions As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents lblQuestion As System.Windows.Forms.Label
    Friend WithEvents txtQuestion As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtQuestionVarName As System.Windows.Forms.TextBox
    Friend WithEvents btnAddQuestion As System.Windows.Forms.Button
    Friend WithEvents btnRemoveQuestion As System.Windows.Forms.Button
    Friend WithEvents btnUpdateQuestion As System.Windows.Forms.Button
    Friend WithEvents lblQuestionDataType As System.Windows.Forms.Label
    Friend WithEvents ComboBoxQuestionDataType As System.Windows.Forms.ComboBox
    Friend WithEvents lblQuestionDefaultValueString As System.Windows.Forms.Label
    Friend WithEvents txtQuestionDefaultValueString As System.Windows.Forms.TextBox
    Friend WithEvents lblQuestionDefaultValueBoolean As System.Windows.Forms.Label
    Friend WithEvents ComboBoxDefaultValueBoolean As System.Windows.Forms.ComboBox
End Class
