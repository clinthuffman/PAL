Imports System.Windows.Forms

Public Class frmLogPreProcessor
    Friend ofrmParent As frmPALExecutionWizard
    Friend sSourceLogForRelog As String
    Friend sDestinationLogForRelog As String

    Private Sub frmLogPreProcessor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim aCounterList(1) As String
        'aCounterList(0) = "Computer1"
        'aCounterList(1) = "Computer2"

        'Dim oPALFunctions As New PALFunctions.PALFunctions
        'oPALFunctions.ConvertCounterLogBLGToCSV(sSourceLogForRelog, sDestinationLogForRelog)

        'ofrmParent.aCounterList = aCounterList
        'ofrmParent.lblRelogStatus.Text = "Done"
    End Sub


End Class
