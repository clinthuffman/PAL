Imports System.Xml
Public Class frmAddDataSourceCounter
    Friend ofrmMain As frmPALEditor
    Public oXmlAnalysisNode As XmlNode
    Public XMLRoot As XmlNode
    Friend LastUsedComputerName As String

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Dim i As Integer
        Dim aExclusions() As String
        Dim bRetVal As Boolean

        ReDim aExclusions(0)
        aExclusions(0) = ""
        For i = 0 To ListBoxCounterInstanceExclusions.Items.Count - 1
            ReDim Preserve aExclusions(i)
            aExclusions(i) = ListBoxCounterInstanceExclusions.Items(i).ToString
        Next
        bRetVal = ofrmMain.AddCounter(ofrmMain.g_oXMLSelectedAnalysis.Attributes("NAME").Value, txtCounterName.Text, txtCollectionVarName.Text, ComboBoxDataSourceCounterDataType.Text, aExclusions)
        If bRetVal = True Then
            ofrmMain.ListBoxDataSourceCounters.Items.Add(txtCounterName.Text)
            Me.Close()
        End If
        ofrmMain.LastUsedComputerName = LastUsedComputerName
    End Sub

    Private Sub btnBrowseCounters_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowseCounters.Click
        Dim ofrmAddCounter As New frmAddCounters

        ofrmAddCounter.radAllPerfCounters.Enabled = False
        ofrmAddCounter.ListBoxPerfCounters.SelectionMode = SelectionMode.One
        ofrmAddCounter.ListBoxPerfInstances.SelectionMode = SelectionMode.One
        ofrmAddCounter.radInstancesAll.Checked = True
        ofrmAddCounter.ofrmAddDataSourceCounter = Me
        ofrmAddCounter.LastUsedComputerName = LastUsedComputerName
        ofrmAddCounter.Show(Me)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddDataSourceCounter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnCounterInstanceExclusionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionAdd.Click
        Dim sExclusion As String
        sExclusion = InputBox("Enter a performance counter instance to exclude.", "Performance Counter Instance Exclusion")
        ListBoxCounterInstanceExclusions.Items.Add(sExclusion)
    End Sub

    Private Sub btnCounterInstanceExclusionRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionRemove.Click
        ListBoxCounterInstanceExclusions.Items.Remove(ListBoxCounterInstanceExclusions.SelectedItem)
    End Sub

    Private Sub btnAddGeneratedCounter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddGeneratedCounter.Click
        Dim ofrmDataSourceGeneratedCounters As New frmAddDataSourceGeneratedCounter
        ofrmDataSourceGeneratedCounters.ofrmMain = ofrmMain
        ofrmDataSourceGeneratedCounters.LastUsedComputerName = LastUsedComputerName
        ofrmDataSourceGeneratedCounters.XMLRoot = XMLRoot
        ofrmDataSourceGeneratedCounters.oXmlAnalysisNode = oXmlAnalysisNode
        ofrmDataSourceGeneratedCounters.Show(ofrmMain)
    End Sub
End Class