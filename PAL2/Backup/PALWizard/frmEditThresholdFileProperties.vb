Imports System.Windows.Forms
Imports System.Xml

Public Class frmEditThresholdFileProperties
    Friend sTitle As String
    Friend sVersion As String
    Friend sContentOwners As String
    Friend sFeedbackEmailAddresses As String
    Friend sDescription As String
    Friend ofrmMain As frmPALEditor
    Friend sThresholdInheritanceFilePaths As String
    Dim sSelectedThresholdFileInheritanceItem As String

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Dim i, y As Integer
        sTitle = txtThresholdTitle.Text
        sVersion = txtThresholdFileVersion.Text
        sDescription = txtThresholdFileDescription.Text
        sContentOwners = txtThresholdFileContentOwners.Text
        sFeedbackEmailAddresses = txtThresholdFileFeedbackEmailAddresses.Text

        ofrmMain.g_sThresholdFileTitle = sTitle
        ofrmMain.g_sThresholdFileVersion = sVersion
        ofrmMain.g_sThresholdFileDescription = sDescription
        ofrmMain.g_sThresholdFileContentOwners = sContentOwners
        ofrmMain.g_sThresholdFileFeedbackEmailAddresses = sFeedbackEmailAddresses

        ofrmMain.g_ThresholdFileInheritanceFileNames = ""
        y = ListBoxOfThresholdFileInheritance.Items.Count - 1
        For i = 0 To y
            If i = 0 Then
                ofrmMain.g_ThresholdFileInheritanceFileNames = ListBoxOfThresholdFileInheritance.Items(i)
            Else
                ofrmMain.g_ThresholdFileInheritanceFileNames = ofrmMain.g_ThresholdFileInheritanceFileNames & "," & ListBoxOfThresholdFileInheritance.Items(i)
            End If
        Next

        ofrmMain.UpdateThresholdFile()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub frmEditThresholdFileProperties_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtThresholdTitle.Text = sTitle
        txtThresholdFileVersion.Text = sVersion
        txtThresholdFileDescription.Text = sDescription
        txtThresholdFileContentOwners.Text = sContentOwners
        txtThresholdFileFeedbackEmailAddresses.Text = sFeedbackEmailAddresses

        Dim oXmlNode As XmlNode
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot As XmlNode
        ListBoxOfThresholdFileInheritance.Items.Clear()
        If System.IO.File.Exists(ofrmMain.g_ThresholdFilePath) = True Then
            oXmlDoc.Load(ofrmMain.g_ThresholdFilePath)
            oXmlRoot = oXmlDoc.DocumentElement
            For Each oXmlNode In oXmlRoot.SelectNodes("//INHERITANCE")
                ListBoxOfThresholdFileInheritance.Items.Add(oXmlNode.Attributes("FILEPATH").Value)
            Next
        End If
    End Sub

    Private Sub ListBoxOfThresholdFileInheritance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxOfThresholdFileInheritance.SelectedIndexChanged
        sSelectedThresholdFileInheritanceItem = ListBoxOfThresholdFileInheritance.SelectedItem
    End Sub

    Private Function GetFileNameFromPath(ByVal sFilePath As String)
        Dim aFilePath As String()
        Dim sFileName As String
        aFilePath = sFilePath.Split("\")
        sFileName = aFilePath(aFilePath.GetUpperBound(0))
        Return sFileName
    End Function

    Private Sub btnThresholdFileInheritanceAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdFileInheritanceAdd.Click
        Dim sFileName As String
        OpenFileDialog1.Filter = "XML files (*.xml)|*.xml|All Files (*.*)|*.*"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            sFileName = GetFileNameFromPath(OpenFileDialog1.FileName)
            ListBoxOfThresholdFileInheritance.Items.Add(sFileName)
        End If
    End Sub

    Private Sub btnThresholdFileInheritanceRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdFileInheritanceRemove.Click
        ListBoxOfThresholdFileInheritance.Items.Remove(ListBoxOfThresholdFileInheritance.SelectedItem)
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        If ListBoxOfThresholdFileInheritance.SelectedIndex > 0 And ListBoxOfThresholdFileInheritance.SelectedIndex < ListBoxOfThresholdFileInheritance.Items.Count Then
            ListBoxOfThresholdFileInheritance.Items.Insert(ListBoxOfThresholdFileInheritance.SelectedIndex + 1, ListBoxOfThresholdFileInheritance.Items(ListBoxOfThresholdFileInheritance.SelectedIndex - 1))
            ListBoxOfThresholdFileInheritance.Items.RemoveAt(ListBoxOfThresholdFileInheritance.SelectedIndex - 1)
        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        If ListBoxOfThresholdFileInheritance.SelectedIndex >= 0 And ListBoxOfThresholdFileInheritance.SelectedIndex < ListBoxOfThresholdFileInheritance.Items.Count - 1 Then
            ListBoxOfThresholdFileInheritance.Items.Insert(ListBoxOfThresholdFileInheritance.SelectedIndex, ListBoxOfThresholdFileInheritance.Items(ListBoxOfThresholdFileInheritance.SelectedIndex + 1))
            ListBoxOfThresholdFileInheritance.Items.RemoveAt(ListBoxOfThresholdFileInheritance.SelectedIndex + 1)
        End If
    End Sub
End Class
