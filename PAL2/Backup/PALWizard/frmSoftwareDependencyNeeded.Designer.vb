<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSoftwareDependencyNeeded
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
        Me.lblText = New System.Windows.Forms.Label
        Me.lblLink = New System.Windows.Forms.LinkLabel
        Me.lblLinkDescription = New System.Windows.Forms.Label
        Me.btnClose = New System.Windows.Forms.Button
        Me.txtLink = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'lblText
        '
        Me.lblText.AutoSize = True
        Me.lblText.Location = New System.Drawing.Point(13, 13)
        Me.lblText.Name = "lblText"
        Me.lblText.Size = New System.Drawing.Size(59, 13)
        Me.lblText.TabIndex = 0
        Me.lblText.Text = "[Insert text]"
        '
        'lblLink
        '
        Me.lblLink.AutoSize = True
        Me.lblLink.Location = New System.Drawing.Point(13, 101)
        Me.lblLink.Name = "lblLink"
        Me.lblLink.Size = New System.Drawing.Size(59, 13)
        Me.lblLink.TabIndex = 1
        Me.lblLink.TabStop = True
        Me.lblLink.Text = "LinkLabel1"
        '
        'lblLinkDescription
        '
        Me.lblLinkDescription.AutoSize = True
        Me.lblLinkDescription.Location = New System.Drawing.Point(13, 85)
        Me.lblLinkDescription.Name = "lblLinkDescription"
        Me.lblLinkDescription.Size = New System.Drawing.Size(90, 13)
        Me.lblLinkDescription.TabIndex = 2
        Me.lblLinkDescription.Text = "lblLinkDescription"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(100, 198)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 3
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'txtLink
        '
        Me.txtLink.Location = New System.Drawing.Point(16, 147)
        Me.txtLink.Name = "txtLink"
        Me.txtLink.Size = New System.Drawing.Size(256, 20)
        Me.txtLink.TabIndex = 4
        '
        'frmSoftwareDependencyNeeded
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(284, 264)
        Me.Controls.Add(Me.txtLink)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblLinkDescription)
        Me.Controls.Add(Me.lblLink)
        Me.Controls.Add(Me.lblText)
        Me.Name = "frmSoftwareDependencyNeeded"
        Me.Text = "PAL Tool: Software Dependency Needed"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblText As System.Windows.Forms.Label
    Friend WithEvents lblLink As System.Windows.Forms.LinkLabel
    Friend WithEvents lblLinkDescription As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtLink As System.Windows.Forms.TextBox
End Class
