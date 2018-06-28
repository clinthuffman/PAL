Imports System.Xml

Public Class frmQuestions
    Public oXMLRoot As XmlNode
    Public oXMLThresholdFile As XmlDocument
    Dim oSelectedXMLQuestion As XmlNode

    Private Sub frmQuestions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshForm()
    End Sub

    Sub RefreshForm()
        Dim oXmlNode As XmlNode

        ListBoxQuestions.Items.Clear()
        For Each oXmlNode In oXMLRoot.SelectNodes("//QUESTION")
            ListBoxQuestions.Items.Add(oXmlNode.Attributes("QUESTIONVARNAME").Value)
        Next
    End Sub

    Private Sub btnAddQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddQuestion.Click
        ListBoxQuestions.Items.Add("--Needs Updated--")
        AddQuestion("--Needs Updated--", "--Needs Updated--", "boolean", "False")
    End Sub

    Private Sub ListBoxQuestions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxQuestions.SelectedIndexChanged
        BindListBoxQuestinosToSelectedXMLQuestion()
        txtQuestion.Text = oSelectedXMLQuestion.InnerText
        txtQuestionVarName.Text = oSelectedXMLQuestion.Attributes("QUESTIONVARNAME").Value
        ComboBoxQuestionDataType.Text = oSelectedXMLQuestion.Attributes("DATATYPE").Value
        Select Case oSelectedXMLQuestion.Attributes("DATATYPE").Value
            Case "boolean"
                ComboBoxDefaultValueBoolean.Text = oSelectedXMLQuestion.Attributes("DEFAULTVALUE").Value
                txtQuestionDefaultValueString.Text = ""
            Case "string"
                txtQuestionDefaultValueString.Text = oSelectedXMLQuestion.Attributes("DEFAULTVALUE").Value
                ComboBoxDefaultValueBoolean.Text = ""
        End Select
    End Sub

    Sub BindListBoxQuestinosToSelectedXMLQuestion()
        Dim oXmlNode As XmlNode

        For Each oXmlNode In oXMLRoot.SelectNodes("//QUESTION")
            If LCase(oXmlNode.Attributes("QUESTIONVARNAME").Value) = LCase(ListBoxQuestions.SelectedItem) Then
                oSelectedXMLQuestion = oXmlNode
                Exit Sub
            End If
        Next
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.Close()
    End Sub

    Function AddQuestion(ByVal sQuestion As String, ByVal sQuestionVarName As String, ByVal sQuestionDataType As String, ByVal sQuestionDefaultValue As String) As Boolean
        Dim oXMLQuestionNode As XmlNode
        Dim oXMLNewAttribute As XmlAttribute

        oXMLQuestionNode = oXMLThresholdFile.CreateNode(XmlNodeType.Element, "QUESTION", "")

        oXMLNewAttribute = oXMLThresholdFile.CreateAttribute("QUESTIONVARNAME")
        oXMLNewAttribute.Value = sQuestionVarName
        oXMLQuestionNode.Attributes.Append(oXMLNewAttribute)

        oXMLNewAttribute = oXMLThresholdFile.CreateAttribute("DATATYPE")
        oXMLNewAttribute.Value = sQuestionDataType
        oXMLQuestionNode.Attributes.Append(oXMLNewAttribute)

        oXMLNewAttribute = oXMLThresholdFile.CreateAttribute("DEFAULTVALUE")
        oXMLNewAttribute.Value = sQuestionDefaultValue
        oXMLQuestionNode.Attributes.Append(oXMLNewAttribute)

        oXMLQuestionNode.InnerText = sQuestion
        oXMLRoot.AppendChild(oXMLQuestionNode)

        RefreshForm()
        Return True
    End Function

    Function UpdateQuestion(ByVal oXmlQuestionNode As XmlNode, ByVal sQuestion As String, ByVal sQuestionVarName As String, ByVal sQuestionDataType As String, ByVal sQuestionDefaultValue As String) As Boolean
        oXmlQuestionNode.Attributes("QUESTIONVARNAME").Value = txtQuestionVarName.Text
        oXmlQuestionNode.Attributes("DATATYPE").Value = sQuestionDataType
        oXmlQuestionNode.Attributes("DEFAULTVALUE").Value = sQuestionDefaultValue
        oXmlQuestionNode.InnerText = txtQuestion.Text
        RefreshForm()
        Return True
    End Function

    Private Sub btnUpdateQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateQuestion.Click
        Dim sDefaultValue As String
        sDefaultValue = ""
        Select Case ComboBoxQuestionDataType.Text
            Case "boolean"
                sDefaultValue = ComboBoxDefaultValueBoolean.Text
            Case "string"
                sDefaultValue = txtQuestionDefaultValueString.Text
        End Select
        UpdateQuestion(oSelectedXMLQuestion, txtQuestion.Text, txtQuestionVarName.Text, ComboBoxQuestionDataType.Text, sDefaultValue)
    End Sub

    Private Sub btnRemoveQuestion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveQuestion.Click
        BindListBoxQuestinosToSelectedXMLQuestion()
        oXMLRoot.RemoveChild(oSelectedXMLQuestion)
        RefreshForm()
        txtQuestion.Text = ""
        txtQuestionVarName.Text = ""
    End Sub

    Private Sub ComboBoxQuestionDataType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxQuestionDataType.SelectedIndexChanged
        If ComboBoxQuestionDataType.Text = "boolean" Then
            ComboBoxDefaultValueBoolean.Enabled = True
            ComboBoxDefaultValueBoolean.Text = "False"
            txtQuestionDefaultValueString.Enabled = False
        Else
            ComboBoxDefaultValueBoolean.Enabled = False
            txtQuestionDefaultValueString.Enabled = True
        End If
    End Sub
End Class