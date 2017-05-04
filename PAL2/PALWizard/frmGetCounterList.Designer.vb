<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogPreProcessor
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
        Me.lblPleaseWait = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'lblPleaseWait
        '
        Me.lblPleaseWait.AutoSize = True
        Me.lblPleaseWait.Location = New System.Drawing.Point(12, 54)
        Me.lblPleaseWait.Name = "lblPleaseWait"
        Me.lblPleaseWait.Size = New System.Drawing.Size(231, 13)
        Me.lblPleaseWait.TabIndex = 0
        Me.lblPleaseWait.Text = "Please wait while the log(s) are pre-processed..."
        '
        'frmLogPreProcessor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 177)
        Me.Controls.Add(Me.lblPleaseWait)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLogPreProcessor"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Log Pre-Processor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPleaseWait As System.Windows.Forms.Label

End Class
