Public Class frmNewAnalysis
    Friend ofrmMain As frmPALEditor
    Friend LastUsedComputerName As String

    Private Sub btnNewAnalysisCancelbtnNewAnalysisCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAnalysisCancelbtnNewAnalysisCancel.Click
        Me.Close()
    End Sub

    Private Sub btnNewAnalysisOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAnalysisOK.Click
        ofrmMain.AddAnalysis(txtNewAnalysisName.Text.ToString, True, txtNewAnalysisCounter.Text.ToString, ComboBoxCategory.Text.ToString, txtNewAnalysisDescription.Text)
        ofrmMain.RefreshTreeViewAnalysis()
        ofrmMain.LastUsedComputerName = LastUsedComputerName

        Dim oTreeNodeCategory As TreeNode
        Dim oTreeNode As TreeNode
        For Each oTreeNodeCategory In ofrmMain.TreeViewAnalysis.Nodes
            For Each oTreeNode In oTreeNodeCategory.Nodes
                If LCase(oTreeNode.Name) = LCase(txtNewAnalysisName.Text) Then
                    ofrmMain.TreeViewAnalysis.SelectedNode = oTreeNode
                End If
            Next
        Next
        Me.Close()
    End Sub

    Private Sub btnNewAnalysisCounterPicker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewAnalysisCounterPicker.Click
        Dim ofrmAddCounter As New frmAddCounters
        ofrmAddCounter.ofrmNewAnalysis = Me
        ofrmAddCounter.radAllPerfCounters.Enabled = False        
        ofrmAddCounter.ListBoxPerfCounters.SelectionMode = SelectionMode.One
        ofrmAddCounter.ListBoxPerfInstances.SelectionMode = SelectionMode.One
        ofrmAddCounter.radInstancesAll.Checked = True
        ofrmAddCounter.LastUsedComputerName = LastUsedComputerName
        ofrmAddCounter.Show(Me)
    End Sub
End Class