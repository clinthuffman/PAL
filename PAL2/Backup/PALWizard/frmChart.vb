Imports System.Xml

Public Class frmChart
    Friend ofrmMain As frmPALEditor
    Public oXMLChartNode As XmlNode
    Public oXMLAnalysisNode As XmlNode
    Public XMLRoot As XmlNode
    Dim sIsThresholdsAdded As String
    Dim oXMLSelectedVariable As XmlNode
    Dim sXMLSelectedAttribute As String

    Sub ResetChartControl()
        'txtChartOTSFormat.Text = "MM/dd hh:mm"
        'txtChartGroupSize.Text = "640x480"
        'ComboBoxChartLegend.SelectedItem = "ON"
        'ComboBoxDataSource.SelectedItem = "Line"
    End Sub

    Private Sub frmChart_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtWarningThresholdCode.Enabled = False
        txtCriticalThresholdCode.Enabled = False
        PopulateForm()
    End Sub

    Sub PopulateForm()
        Dim oXMLNode, oXmlSeriesNode, oXmlCodeNode As XmlNode
        Dim bFound As Boolean
        Dim sCollectionVarName As String = ""
        Dim sCode As String = ""
        Dim sPrimaryDataSourceCollectionVarName As String = ""

        sIsThresholdsAdded = "False"
        txtWarningThresholdCode.Text = ""
        txtCriticalThresholdCode.Text = ""
        txtWarningThresholdCode.Enabled = False
        CheckBoxWarningThresholdCode.Checked = False
        txtCriticalThresholdCode.Enabled = False
        CheckBoxCriticalThresholdCode.Checked = False

        For Each oXmlSeriesNode In oXMLChartNode.SelectNodes("./SERIES")
            If oXmlSeriesNode.Attributes("NAME").Value = "Warning" Then
                For Each oXmlCodeNode In oXmlSeriesNode.SelectNodes("./CODE")
                    txtWarningThresholdCode.Text = oXmlCodeNode.InnerText
                Next
                txtWarningThresholdCode.Enabled = True
                CheckBoxWarningThresholdCode.Checked = True
                sIsThresholdsAdded = "True"
            End If
            If oXmlSeriesNode.Attributes("NAME").Value = "Critical" Then
                For Each oXmlCodeNode In oXmlSeriesNode.SelectNodes("./CODE")
                    txtCriticalThresholdCode.Text = oXmlCodeNode.InnerText
                Next
                txtCriticalThresholdCode.Enabled = True
                CheckBoxCriticalThresholdCode.Checked = True
                sIsThresholdsAdded = "True"
            End If
        Next

        txtChartTitle.Text = oXMLChartNode.Attributes("CHARTTITLE").Value
        bFound = False
        For Each oXMLNode In oXMLAnalysisNode.SelectNodes("./DATASOURCE")
            ComboBoxDataSource.Items.Add(oXMLNode.Attributes("NAME").Value)
            If oXMLNode.Attributes("NAME").Value = oXMLChartNode.Attributes("DATASOURCE").Value Then
                bFound = True            
            End If
        Next

        If bFound = True Then
            ComboBoxDataSource.SelectedItem = oXMLChartNode.Attributes("DATASOURCE").Value
        Else
            ComboBoxDataSource.SelectedItem = oXMLAnalysisNode.Attributes("PRIMARYDATASOURCE").Value
        End If

        For Each oXMLNode In oXMLChartNode.SelectNodes("./EXCLUDE")
            ListBoxCounterInstanceExclusions.Items.Add(oXMLNode.Attributes("INSTANCE").Value)
        Next

        '//ListView
        '<QUESTION TITLE="Number of Processors" VARNAME="NumberOfProcessors">How many processors (physical and virtual) does the server have?</QUESTION>
        ListBoxThresholdVariables.Items.Clear()
        For Each oXMLNode In XMLRoot.SelectNodes("//QUESTION")
            ListBoxThresholdVariables.Items.Add("$" & oXMLNode.Attributes("QUESTIONVARNAME").Value)
        Next
        For Each oXMLNode In oXMLAnalysisNode.SelectNodes("./DATASOURCE")
            sCollectionVarName = oXMLNode.Attributes("COLLECTIONVARNAME").Value
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName)
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].CounterPath")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].CounterComputer")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].CounterObject")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].CounterName")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].CounterInstance")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Time[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Value[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].QuantizedTime[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].QuantizedMin[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].QuantizedAvg[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].QuantizedMax[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].QuantizedTrend[]")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Min")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Avg")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Max")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].Trend")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].StdDev")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].PercentileSeventyth")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].PercentileEightyth")
            ListBoxThresholdVariables.Items.Add("$" & sCollectionVarName & "[0].PercentileNinetyth")
            If oXMLNode.Attributes("NAME").Value = oXMLAnalysisNode.Attributes("PRIMARYDATASOURCE").Value Then
                sPrimaryDataSourceCollectionVarName = oXMLNode.Attributes("COLLECTIONVARNAME").Value
            End If
        Next
        ListBoxThresholdVariables.Sorted = True
        ListBoxThresholdVariables.SetSelected(0, True)
        BindSelectedListBoxVariableItemToXML()

        sCode = "StaticChartThreshold -CollectionOfCounterInstances $" & sPrimaryDataSourceCollectionVarName & " -MinThreshold 10 -MaxThreshold 20 -IsOperatorGreaterThan $True -UseMaxValue $False"
        If txtWarningThresholdCode.Text = "" Then
            txtWarningThresholdCode.Text = sCode
        End If
        sCode = "StaticChartThreshold -CollectionOfCounterInstances $" & sPrimaryDataSourceCollectionVarName & " -MinThreshold 20 -MaxThreshold 29.999 -IsOperatorGreaterThan $True -UseMaxValue $True"
        If txtCriticalThresholdCode.Text = "" Then
            txtCriticalThresholdCode.Text = sCode
        End If
    End Sub

    Sub BindSelectedListBoxVariableItemToXML()
        Dim oXMLNode As XmlNode
        Dim sCollectionVarName As String = ""
        For Each oXMLNode In XMLRoot.SelectNodes("//QUESTION")
            If LCase("$" & oXMLNode.Attributes("QUESTIONVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
                oXMLSelectedVariable = oXMLNode
                sXMLSelectedAttribute = "QUESTIONVARNAME"
                Exit Sub
            End If
        Next
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

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

        If CheckBoxCriticalThresholdCode.Checked = True Or CheckBoxWarningThresholdCode.Checked = True Then
            sIsThresholdsAdded = "True"
        Else
            sIsThresholdsAdded = "False"
        End If

        bRetVal = ofrmMain.UpdateChart(oXMLChartNode, txtChartTitle.Text, ComboBoxDataSource.Text, sIsThresholdsAdded, aExclusions, CheckBoxWarningThresholdCode.Checked, CheckBoxCriticalThresholdCode.Checked, txtWarningThresholdCode.Text, txtCriticalThresholdCode.Text)
        ofrmMain.btnEditChart.Text = "Edit Chart..."
        If bRetVal = True Then
            Me.Close()
        End If
    End Sub

    Private Sub btnCounterInstanceExclusionAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionAdd.Click
        Dim sExclusion As String
        sExclusion = InputBox("Enter a performance counter instance to exclude.", "Performance Counter Instance Exclusion")
        ListBoxCounterInstanceExclusions.Items.Add(sExclusion)
    End Sub

    Private Sub btnCounterInstanceExclusionRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCounterInstanceExclusionRemove.Click
        ListBoxCounterInstanceExclusions.Items.Remove(ListBoxCounterInstanceExclusions.SelectedItem)
    End Sub

    Private Sub CheckBoxWarningThresholdCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxWarningThresholdCode.CheckedChanged
        If CheckBoxWarningThresholdCode.Checked = True Then
            txtWarningThresholdCode.Enabled = True
        Else
            txtWarningThresholdCode.Enabled = False
        End If
    End Sub

    Private Sub CheckBoxCriticalThresholdCode_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxCriticalThresholdCode.CheckedChanged
        If CheckBoxCriticalThresholdCode.Checked = True Then
            txtCriticalThresholdCode.Enabled = True
        Else
            txtCriticalThresholdCode.Enabled = False
        End If
    End Sub

    Private Sub ListBoxThresholdVariables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxThresholdVariables.SelectedIndexChanged
        Dim sSelectedItem, sCompareString As String
        Dim sCollectionVarName As String
        Dim bFound As Boolean = False
        sSelectedItem = LCase(ListBoxThresholdVariables.SelectedItem)
        bFound = False

        BindSelectedListBoxVariableItemToXML()

        For Each oXMLNode In XMLRoot.SelectNodes("//QUESTION")
            sCompareString = LCase("$" & oXMLNode.Attributes("QUESTIONVARNAME").Value)
            If sCompareString = sSelectedItem Then
                txtVarName.Text = "$" & oXMLSelectedVariable.Attributes("QUESTIONVARNAME").Value
                txtVarDescription.Text = "Type QUESTION. The following question is asked of the user: " & Chr(34) & oXMLNode.InnerText & Chr(34) & "."
                bFound = True
                Exit For
            End If
        Next

        If bFound = False Then
            For Each oXMLNode In oXMLAnalysisNode.SelectNodes("./DATASOURCE")
                sCollectionVarName = "$" & oXMLNode.Attributes("COLLECTIONVARNAME").Value
                If sSelectedItem = LCase(sCollectionVarName) Then
                    txtVarName.Text = sCollectionVarName
                    txtVarDescription.Text = "Type COLLECTION of counter instances. A collection of counters instances that match the counter expression path. For example, if the data source expression path is '\Processor(*)\% Processor Time', then this object will contain a collection of Processor counter instances."
                    bFound = True
                    Exit For
                End If

                If sSelectedItem.IndexOf(".counterpath") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].CounterPath"
                    txtVarDescription.Text = "Type COUNTER Schema. The full counter path as it would be seen in performance monitor. Example: This entire path would be the value: \\demoserver\Processor(0)\% Processor Time"
                End If

                If sSelectedItem.IndexOf(".countercomputer") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].CounterComputer"
                    txtVarDescription.Text = "Type COUNTER Schema. The counter computer. Example: " & Chr(34) & "demoserver" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
                End If

                If sSelectedItem.IndexOf(".counterobject") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].CounterObject"
                    txtVarDescription.Text = "Type COUNTER Schema. The counter object/category. Example: " & Chr(34) & "Processor" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
                End If

                If sSelectedItem.IndexOf(".countername") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].CounterName"
                    txtVarDescription.Text = "Type COUNTER Schema. The counter name. Example: " & Chr(34) & "% Processor Time" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
                End If

                If sSelectedItem.IndexOf(".counterinstance") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].CounterInstance"
                    txtVarDescription.Text = "Type COUNTER Schema. The counter instance. Example: " & Chr(34) & "0" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
                End If

                If sSelectedItem.IndexOf(".time") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Time[]"
                    txtVarDescription.Text = "Type Array of Time. This array contains all of the date time values of this counter of all of the data points in the counter log."
                End If
                If sSelectedItem.IndexOf(".value") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Value[]"
                    txtVarDescription.Text = "Type Array of Values. This array contains all of the data points of this counter in the counter log."
                End If
                If sSelectedItem.IndexOf(".quantizedtime") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].QuantizedTime[]"
                    txtVarDescription.Text = "Type Array of Time. This array contains date time values of all of the time slices of this counter. Quantize means to approximate or bucket-ize the data into equal parts."
                End If
                If sSelectedItem.IndexOf(".quantizedmin") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].QuantizedMin[]"
                    txtVarDescription.Text = "Type Array of Values. This array contains the minimum value of this counter found in each time slice. Quantize means to approximate or bucket-ize the data into equal parts."
                End If
                If sSelectedItem.IndexOf(".quantizedavg") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].QuantizedAvg[]"
                    txtVarDescription.Text = "Type Array of Values. This array contains the average value of this counter found in each time slice. Quantize means to approximate or bucket-ize the data into equal parts."
                End If
                If sSelectedItem.IndexOf(".quantizedmax") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].QuantizedMax[]"
                    txtVarDescription.Text = "Type Array of Values. This array contains the maximum value of this counter found in each time slice. Quantize means to approximate or bucket-ize the data into equal parts."
                End If
                If sSelectedItem.IndexOf(".quantizedtrend") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].QuantizedTrend[]"
                    txtVarDescription.Text = "Type Array of Values. This array contains the trend value found in each time slice. The trend value is the average difference between the QuantizedAvg values of this counter from this time slice to the beginning of the counter log. Quantize means to approximate or bucket-ize the data into equal parts."
                End If
                If sSelectedItem.IndexOf(".min") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Min"
                    txtVarDescription.Text = "Type Integer. This property contains the minimum value of all of the data points of this counter in the counter log."
                End If
                If sSelectedItem.IndexOf(".avg") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Avg"
                    txtVarDescription.Text = "Type Integer. This property contains the average value of all of the data points of this counter in the counter log."
                End If
                If sSelectedItem.IndexOf(".max") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Max"
                    txtVarDescription.Text = "Type Integer. This property contains the maximum value of all of the data points of this counter in the counter log."
                End If
                If sSelectedItem.IndexOf(".trend") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].Trend"
                    txtVarDescription.Text = "Type Integer. This property contains the trend value of all of the data points in the counter log. The trend value is the average difference between the average values from this data point to the beginning of the counter log."
                End If
                If sSelectedItem.IndexOf(".stddev") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].StdDev"
                    txtVarDescription.Text = "Type Integer. This property contains the standard deviation of all of the data points of this counter found in the counter log."
                End If
                If sSelectedItem.IndexOf(".percentileseventyth") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].PercentileSeventyth"
                    txtVarDescription.Text = "Type Integer. This property contains the average of the data points within a times slice of this counter instance which 30% of the outliers removed."
                End If
                If sSelectedItem.IndexOf(".percentileeightyth") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].PercentileEightyth"
                    txtVarDescription.Text = "Type Integer. This property contains the average of the data points within a times slice of this counter instance which 20% of the outliers removed."
                End If
                If sSelectedItem.IndexOf(".percentileninetyth") > 0 Then
                    txtVarName.Text = sCollectionVarName & "[0].PercentileNinetyth"
                    txtVarDescription.Text = "Type Integer. This property contains the average of the data points within a times slice of this counter instance which 10% of the outliers removed."
                End If
            Next
        End If
    End Sub
End Class