<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPleaseWait
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
        Me.lblAboutText = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblAboutText
        '
        Me.lblAboutText.Location = New System.Drawing.Point(13, 13)
        Me.lblAboutText.Name = "lblAboutText"
        Me.lblAboutText.Size = New System.Drawing.Size(217, 65)
        Me.lblAboutText.TabIndex = 1
        Me.lblAboutText.Text = "Auto detection in progress. Please wait..."
        '
        'AboutDialog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(242, 57)
        Me.Controls.Add(Me.lblAboutText)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AboutDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Please wait..."
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblAboutText As System.Windows.Forms.Label

End Class
