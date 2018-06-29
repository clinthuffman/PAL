Imports System.Xml
Public Class frmAddThreshold
    Friend ofrmMain As frmPALEditor
    Public oXmlAnalysisNode As XmlNode
    Public XMLRoot As XmlNode
    Dim oXMLSelectedVariable As XmlNode
    Dim sXMLSelectedAttribute As String
    Dim sSelectedThresholdColor As String

    Private Sub frmAddThreshold_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshForm()
    End Sub

    Private Sub btnThresholdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdCancel.Click
        Me.Close()
    End Sub

    Private Sub btnThresholdOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdOK.Click
        Dim sRetVal As String
        sRetVal = ValidationCheck()
        If sRetVal = "" Then
            ofrmMain.AddThreshold(txtThresholdName.Text, ComboBoxThresholdCondition.Text, txtThresholdColor.Text, txtThresholdPriority.Text, txtThresholdCode.Text, txtThresholdDescription.Text)
            Me.Close()
        Else
            MsgBox(sRetVal & " needs to contain text.")
        End If
    End Sub

    Function ValidationCheck() As String

        If txtThresholdCode.Text = "" Then
            Return txtThresholdCode.Name
            Exit Function
        End If

        If txtThresholdColor.Text = "" Then
            Return txtThresholdColor.Name
            Exit Function
        End If

        'If txtThresholdDescription.Text <> "" Then
        '    Return txtThresholdDescription.Name
        '    Exit Function
        'End If

        If txtThresholdName.Text = "" Then
            Return txtThresholdName.Name
            Exit Function
        End If

        If txtThresholdPriority.Text = "" Then
            Return txtThresholdPriority.Name
            Exit Function
        End If

        If ComboBoxThresholdCondition.Text = "" Then
            Return ComboBoxThresholdCondition.Name
            Exit Function
        End If

        Return ""
    End Function
    Sub RefreshForm()
        Dim oXMLNode, oXmlPrimaryDataSource As XmlNode
        Dim sPrimaryDataSource, sDataSourceName, sPrimaryCollectionVarName, sCollectionVarName As String

        sPrimaryDataSource = oXmlAnalysisNode.Attributes("PRIMARYDATASOURCE").Value
        oXmlPrimaryDataSource = oXmlAnalysisNode '// To appease the Null exception warning.
        For Each oXMLNode In oXmlAnalysisNode.SelectNodes("./DATASOURCE")
            sDataSourceName = oXMLNode.Attributes("NAME").Value
            If sPrimaryDataSource = sDataSourceName Then
                oXmlPrimaryDataSource = oXMLNode
                Exit For
            End If
        Next
        sPrimaryCollectionVarName = oXmlPrimaryDataSource.Attributes("COLLECTIONVARNAME").Value

        txtThresholdCode.Text = "#// Use PowerShell code to create alerts when the conditions for this threshold are met." & vbNewLine & _
        "#// Optionally use the variables listed above in the Variables list box." & vbNewLine & _
        "#// If the condition for this threshold is a static value, then use the StaticThreshold() function." & vbNewLine & _
        "#// Otherwise, you will need to manually loop through the counter instance collection object." & vbNewLine & _
        "#// The counter instance collection object has a unique name for each counter data source in this analysis." & vbNewLine & _
        "#// See the variables in the Variables list box above for more information on the counter instance collection object." & vbNewLine & _
        vbNewLine & _
        "#// Here is a ready to use example on how to use the StaticThreshold() function to define a threshold:" & vbNewLine & _
        "StaticThreshold -CollectionOfCounterInstances $" & sPrimaryCollectionVarName & " -Operator 'gt' -Threshold 10" & vbNewLine & _
        vbNewLine & _
        "#// The -Operator parameter accepts gt for greater than, ge for greater than or equal to, lt for less than," & vbNewLine & _
        "#//  and le for less than or equal to. -Threshold is the static value for the threshold."

        txtThresholdName.Text = "Sample Threshold Name"

        '//ListView
        ListBoxThresholdVariables.Items.Clear()
        For Each oXMLNode In XMLRoot.SelectNodes("//QUESTION")
            ListBoxThresholdVariables.Items.Add("$" & oXMLNode.Attributes("QUESTIONVARNAME").Value)
        Next
        For Each oXMLNode In oXmlAnalysisNode.SelectNodes("./DATASOURCE")
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
        Next

        ListBoxThresholdVariables.Sorted = True
        ListBoxThresholdVariables.SetSelected(0, True)
        BindSelectedListBoxVariableItemToXML()

        ComboBoxThresholdCondition.SelectedItem = "Critical"
        ListBoxThresholdVariables.SelectedItem = sPrimaryCollectionVarName

    End Sub

    Sub BindSelectedListBoxVariableItemToXML()
        Dim oXMLNode As XmlNode
        For Each oXMLNode In XMLRoot.SelectNodes("//QUESTION")
            If LCase("$" & oXMLNode.Attributes("QUESTIONVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
                oXMLSelectedVariable = oXMLNode
                sXMLSelectedAttribute = "QUESTIONVARNAME"
                Exit Sub
            End If
        Next
        For Each oXMLNode In oXmlAnalysisNode.SelectNodes("./DATASOURCE")
            If LCase(oXMLNode.Attributes("COLLECTIONVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
                oXMLSelectedVariable = oXMLNode
                sXMLSelectedAttribute = oXMLNode.Attributes("COLLECTIONVARNAME").Value
                Exit Sub
            End If
            'If LCase(oXMLNode.Attributes("AVGVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
            '    oXMLSelectedVariable = oXMLNode
            '    sXMLSelectedAttribute = "AVGVARNAME"
            '    Exit Sub
            'End If
            'If LCase(oXMLNode.Attributes("MAXVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
            '    oXMLSelectedVariable = oXMLNode
            '    sXMLSelectedAttribute = "MAXVARNAME"
            '    Exit Sub
            'End If
            'If LCase(oXMLNode.Attributes("TRENDVARNAME").Value) = LCase(ListBoxThresholdVariables.SelectedItem) Then
            '    oXMLSelectedVariable = oXMLNode
            '    sXMLSelectedAttribute = "TRENDVARNAME"
            '    Exit Sub
            'End If
        Next
    End Sub

    Private Sub ListBoxThresholdVariables_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxThresholdVariables.SelectedIndexChanged
        Dim sSelectedItem, sCompareString As String
        Dim sCollectionVarName As String
        Dim bFound As Boolean = False
        'If ListBoxThresholdVariables.SelectedItem = "CounterComputer" Or ListBoxThresholdVariables.SelectedItem = "CounterObject" Or ListBoxThresholdVariables.SelectedItem = "CounterName" Or ListBoxThresholdVariables.SelectedItem = "CounterInstance" Or ListBoxThresholdVariables.SelectedItem = "CounterPath" Then
        '    sCompareString = ListBoxThresholdVariables.SelectedItem
        'Else
        '    BindSelectedListBoxVariableItemToXML()
        '    sCompareString = sXMLSelectedAttribute
        'End If
        sSelectedItem = LCase(ListBoxThresholdVariables.SelectedItem)
        bFound = False

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
            For Each oXMLNode In oXmlAnalysisNode.SelectNodes("./DATASOURCE")
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

        'Select Case sCompareString
        '    Case "QUESTIONVARNAME"
        '        txtVarName.Text = oXMLSelectedVariable.Attributes("QUESTIONVARNAME").Value
        '        txtVarDescription.Text = "Type QUESTION. The following question is asked of the user: " & Chr(34) & oXMLSelectedVariable.InnerText & Chr(34) & "."
        '        'Case "MINVARNAME"
        '        '    txtVarName.Text = oXMLSelectedVariable.Attributes("MINVARNAME").Value
        '        '    txtVarDescription.Text = "Type COUNTER. The minimum value in each analysis interval of " & Chr(34) & oXMLSelectedVariable.Attributes("NAME").Value & Chr(34) & " which is a counter datasource in this analysis."
        '        'Case "AVGVARNAME"
        '        '    txtVarName.Text = oXMLSelectedVariable.Attributes("AVGVARNAME").Value
        '        '    txtVarDescription.Text = "Type COUNTER. The average value in each analysis interval of " & Chr(34) & oXMLSelectedVariable.Attributes("NAME").Value & Chr(34) & " which is a counter datasource in this analysis."
        '        'Case "MAXVARNAME"
        '        '    txtVarName.Text = oXMLSelectedVariable.Attributes("MAXVARNAME").Value
        '        '    txtVarDescription.Text = "Type COUNTER. The maximum value in each analysis interval of " & Chr(34) & oXMLSelectedVariable.Attributes("NAME").Value & Chr(34) & " which is a counter datasource in this analysis."
        '        'Case "TRENDVARNAME"
        '        '    txtVarName.Text = oXMLSelectedVariable.Attributes("TRENDVARNAME").Value
        '        '    txtVarDescription.Text = "Type COUNTER. The hourly trend value in each analysis interval of " & Chr(34) & oXMLSelectedVariable.Attributes("NAME").Value & Chr(34) & " which is a counter datasource in this analysis."
        '    Case "CounterComputer"
        '        txtVarName.Text = "CounterComputer"
        '        txtVarDescription.Text = "Type COUNTER Schema. The counter computer. Example: " & Chr(34) & "demoserver" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
        '    Case "CounterPath"
        '        txtVarName.Text = "CounterPath"
        '        txtVarDescription.Text = "Type COUNTER Schema. The full counter path as it would be seen in performance monitor. Example: This entire path would be the value: \\demoserver\Processor(0)\% Processor Time"
        '    Case "CounterObject"
        '        txtVarName.Text = "CounterObject"
        '        txtVarDescription.Text = "Type COUNTER Schema. The counter object/category. Example: " & Chr(34) & "Processor" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
        '    Case "CounterName"
        '        txtVarName.Text = "CounterName"
        '        txtVarDescription.Text = "Type COUNTER Schema. The counter name. Example: " & Chr(34) & "% Processor Time" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
        '    Case "CounterInstance"
        '        txtVarName.Text = "CounterInstance"
        '        txtVarDescription.Text = "Type COUNTER Schema. The counter instance. Example: " & Chr(34) & "0" & Chr(34) & " would be the value of this variable in the case of \\demoserver\Processor(0)\% Processor Time"
        '    Case Else
        '        '// A CollectionVarName was most likely selected.
        '        txtVarName.Text = sCompareString
        '        txtVarDescription.Text = "Type COLLECTION of counter instances. A collection of counters instances that match the counter expression path. For example, if the data source expression path is '\Processor(*)\% Processor Time', then this object will contain a collection of Processor counter instances."
        'End Select
    End Sub

    Private Sub btnThresholdColorPicker_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdColorPicker.Click
        Dim sColor As String
        Dim r As String
        Dim g As String
        Dim b As String

        ColorDialogThresholdColor.ShowDialog()
        r = ConvertToHexAndZeroFill(ColorDialogThresholdColor.Color.R)
        g = ConvertToHexAndZeroFill(ColorDialogThresholdColor.Color.G)
        b = ConvertToHexAndZeroFill(ColorDialogThresholdColor.Color.B)
        sColor = "#" & r & g & b
        txtThresholdColor.Text = sColor
        sSelectedThresholdColor = sColor
        lblThresholdColorDisplay.BackColor = ColorDialogThresholdColor.Color
    End Sub

    Function ConvertToHexAndZeroFill(ByVal s As String) As String
        s = Hex(s)
        If Len(s) = 1 Then
            s = "0" & s
        End If
        Return s
    End Function

    Private Sub ComboBoxThresholdCondition_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxThresholdCondition.SelectedIndexChanged
        Dim sCondition
        sCondition = LCase(ComboBoxThresholdCondition.SelectedItem)
        Select Case sCondition
            Case "warning"
                txtThresholdColor.Text = "Yellow"
                lblThresholdColorDisplay.BackColor = Color.Yellow
                txtThresholdPriority.Text = "50"
            Case "critical"
                txtThresholdColor.Text = "Red"
                lblThresholdColorDisplay.BackColor = Color.Red
                txtThresholdPriority.Text = "100"
            Case Else
                txtThresholdColor.Text = "Red"
                lblThresholdColorDisplay.BackColor = Color.Red
                txtThresholdPriority.Text = "100"
        End Select
    End Sub

End Class