Imports System.Windows.Forms

Public Class AboutDialog
    Friend sPALVersion As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub AboutDialog_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If sPALVersion = "" Then
            sPALVersion = "v1.1 (RTM)"
        End If
        lblAboutText.Text = "PAL " & sPALVersion & vbNewLine & "Written by Clint Huffman (clinth@microsoft.com)"
    End Sub
End Class
