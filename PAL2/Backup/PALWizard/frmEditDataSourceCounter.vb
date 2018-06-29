Imports System.Xml

Public Class frmEditDataSourceCounter
    Friend ofrmMain As frmPALEditor
    Public oXMLCounterNode As XmlNode

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
        bRetVal = ofrmMain.UpdateCounter(oXMLCounterNode, txtCounterName.Text, txtCollectionVarName.Text, ComboBoxDataSourceCounterDataType.Text, aExclusions)
        If bRetVal = True Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmAddDataSourceCounter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtCounterName.ReadOnly = True
        PopulateFormData()
    End Sub

    Private Sub btnCounterInstanceExclusionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionAdd.Click
        Dim sExclusion As String
        sExclusion = InputBox("Enter a performance counter instance to exclude.", "Performance Counter Instance Exclusion")
        ListBoxCounterInstanceExclusions.Items.Add(sExclusion)
    End Sub

    Private Sub btnCounterInstanceExclusionRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionRemove.Click
        ListBoxCounterInstanceExclusions.Items.Remove(ListBoxCounterInstanceExclusions.SelectedItem)
    End Sub

    Sub PopulateFormData()
        Dim oXMLNode As XmlNode

        txtCounterName.Text = oXMLCounterNode.Attributes("NAME").Value
        ComboBoxDataSourceCounterDataType.Text = oXMLCounterNode.Attributes("DATATYPE").Value
        txtCollectionVarName.Text = oXMLCounterNode.Attributes("COLLECTIONVARNAME").Value
        'txtAvgVarName.Text = oXMLCounterNode.Attributes("AVGVARNAME").Value
        'txtMaxVarName.Text = oXMLCounterNode.Attributes("MAXVARNAME").Value
        'txtTrendVarName.Text = oXMLCounterNode.Attributes("TRENDVARNAME").Value

        For Each oXMLNode In oXMLCounterNode.SelectNodes("./EXCLUDE")
            ListBoxCounterInstanceExclusions.Items.Add(oXMLNode.Attributes("INSTANCE").Value)
        Next
    End Sub

End Class