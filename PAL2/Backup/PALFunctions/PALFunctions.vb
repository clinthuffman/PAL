Imports System.Xml

Public Class PALFunctions
    Private dctThresholdFileInteritanceHistory As Dictionary(Of String, String)
    Private oXmlGlobalThresholdDoc As XmlDocument

    Private Function FolderOfFile(ByVal sFilePath As String)
        Dim aFilePath As Array
        Dim sFolderOfFile As String = ""
        Dim i, u As Integer
        aFilePath = sFilePath.Split("\")

        If aFilePath.GetUpperBound(0) = 0 Then
            FolderOfFile = sFilePath
            Exit Function
        End If

        u = aFilePath.GetUpperBound(0) - 1
        For i = 0 To u
            sFolderOfFile = sFolderOfFile & aFilePath(i) & "\"
        Next

        FolderOfFile = sFolderOfFile
    End Function

    Private Function InheritFromThresholdFiles(ByVal sThresholdFilePath As String) As XmlDocument
        Dim oXmlThresholdDoc, oXmlInheritedDoc, oXmlReturned As New XmlDocument
        Dim oXmlThresholdRoot, oXmlInheritanceNode, oXmlInheritedAnalysisNode, oXmlAnalysisNode, oXmlReturnedRoot, oXmlReturnedAnalysisNode As XmlNode
        Dim sXmlInherited, sFolderPathOfThresholdFilePath As String
        Dim bFound As Boolean = True

        oXmlThresholdDoc.Load(sThresholdFilePath)
        oXmlThresholdRoot = oXmlThresholdDoc.DocumentElement
        If dctThresholdFileInteritanceHistory.ContainsKey(sThresholdFilePath) = False Then
            dctThresholdFileInteritanceHistory.Add(sThresholdFilePath, "")
        End If
        sFolderPathOfThresholdFilePath = FolderOfFile(sThresholdFilePath)
        For Each oXmlInheritanceNode In oXmlThresholdDoc.SelectNodes("//INHERITANCE")
            If oXmlInheritanceNode.Attributes("FILEPATH").Value <> "" Or oXmlInheritanceNode.Attributes("FILEPATH").Value <> Nothing Then
                sXmlInherited = sFolderPathOfThresholdFilePath & oXmlInheritanceNode.Attributes("FILEPATH").Value
                oXmlInheritedDoc.Load(sXmlInherited)
                For Each oXmlInheritedAnalysisNode In oXmlInheritedDoc.SelectNodes("//ANALYSIS")
                    bFound = False
                    For Each oXmlAnalysisNode In oXmlThresholdRoot.SelectNodes("//ANALYSIS")
                        If oXmlInheritedAnalysisNode.Attributes("ID").Value = oXmlAnalysisNode.Attributes("ID").Value Then
                            bFound = True
                            Exit For
                        End If
                        If oXmlInheritedAnalysisNode.Attributes("NAME").Value = oXmlAnalysisNode.Attributes("NAME").Value Then
                            bFound = True
                            Exit For
                        End If
                    Next
                    If bFound = False Then
                        oXmlThresholdRoot.AppendChild(oXmlThresholdDoc.ImportNode(oXmlInheritedAnalysisNode, True))
                    End If
                Next
                If dctThresholdFileInteritanceHistory.ContainsKey(sXmlInherited) = False Then
                    oXmlReturned = InheritFromThresholdFiles(sXmlInherited)
                    oXmlReturnedRoot = oXmlReturned.DocumentElement
                    For Each oXmlReturnedAnalysisNode In oXmlReturnedRoot.SelectNodes("//ANALYSIS")
                        bFound = False
                        For Each oXmlAnalysisNode In oXmlThresholdRoot.SelectNodes("//ANALYSIS")
                            If oXmlReturnedAnalysisNode.Attributes("ID").Value = oXmlAnalysisNode.Attributes("ID").Value Then
                                bFound = True
                                Exit For
                            End If
                            If oXmlReturnedAnalysisNode.Attributes("NAME").Value = oXmlAnalysisNode.Attributes("NAME").Value Then
                                bFound = True
                                Exit For
                            End If
                        Next
                        If bFound = False Then
                            oXmlThresholdRoot.AppendChild(oXmlThresholdDoc.ImportNode(oXmlReturnedAnalysisNode, True))
                        End If
                    Next
                End If
            End If
        Next
        Return oXmlThresholdDoc
    End Function

    Public Function ExportThresholdFileToPerfmonTemplate(ByVal sThresholdFilePath As String) As String
        'Returns the body of the perfmon template file as a string.
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot, oXmlNode As XmlNode
        Dim sCounter, sOutput, sCounterObject, sCounterName, sCounterInstance As String
        Dim sExistingCounterExpression, sExistingCounterObject, sExistingCounterName, sExistingCounterInstance, sLogName, sString As String
        Dim dctCounters As New Dictionary(Of String, String)
        Dim i As Integer
        Dim bFound As Boolean
        Dim ListOfCounters As New List(Of String)

        Dim sSqlServerNamedInstances, sText As String
        Dim aOfSqlNamedInstances As String()

        sString = "replaceme1,replaceme2"
        aOfSqlNamedInstances = sString.Split(",")

        oXmlGlobalThresholdDoc = New XmlDocument
        dctThresholdFileInteritanceHistory = New Dictionary(Of String, String)
        oXmlDoc = InheritFromThresholdFiles(sThresholdFilePath)
        oXmlRoot = oXmlDoc.DocumentElement

        Dim bSqlCounterObjectFound As Boolean = False
        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If sCounter.IndexOf("SQLServer:") > 0 Then
                    bSqlCounterObjectFound = True
                    Exit For
                End If
            End If
        Next

        If bSqlCounterObjectFound = True Then
            sSqlServerNamedInstances = ""
            sText = "In addition to the SQL Server default instance, do you wish to specify any SQL Server named instances (separated by semi-colons (;))?" & Chr(10) & "If none, then leave blank." & Chr(10) & "Ex: NamedInstance01;NamedInstance02"
            sSqlServerNamedInstances = InputBox(sText, "Specify Microsoft SQL Server named instances", "")
            If sSqlServerNamedInstances = "" Then
                bSqlCounterObjectFound = False
            Else
                aOfSqlNamedInstances = sSqlServerNamedInstances.Split(";")
                bSqlCounterObjectFound = True
            End If
        End If

        oXmlGlobalThresholdDoc = New XmlDocument
        dctThresholdFileInteritanceHistory = New Dictionary(Of String, String)
        oXmlDoc = InheritFromThresholdFiles(sThresholdFilePath)
        oXmlRoot = oXmlDoc.DocumentElement
        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If ListOfCounters.Count > 0 Then
                    For i = 0 To ListOfCounters.Count - 1
                        sExistingCounterExpression = ListOfCounters(i)
                        sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                        sExistingCounterName = GetCounterName(sExistingCounterExpression)
                        sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                        sCounterObject = GetCounterObject(sCounter)
                        sCounterName = GetCounterName(sCounter)
                        sCounterInstance = GetCounterInstance(sCounter)
                        If sExistingCounterObject = sCounterObject Then
                            If sExistingCounterName = sCounterName Then
                                If sExistingCounterInstance <> "" Then
                                    If sExistingCounterInstance <> "*" Then
                                        sExistingCounterInstance = "*"
                                        ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                        bFound = True
                                        Exit For
                                    Else
                                        bFound = True
                                        Exit For
                                    End If
                                Else
                                    bFound = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Else
                    bFound = False
                End If
                If bFound = False Then
                    ListOfCounters.Add(sCounter)
                End If
            End If
        Next

        Dim c As Integer
        Dim sSqlNamedInstance As String
        '// Add the SQL Named instances counters
        If bSqlCounterObjectFound = True Then
            Dim aOfString As String()
            If ListOfCounters.Count > 0 Then
                For c = 0 To ListOfCounters.Count - 1
                    sCounter = ListOfCounters(c)
                    sCounterObject = GetCounterObject(sCounter)
                    sCounterName = GetCounterName(sCounter)
                    sCounterInstance = GetCounterInstance(sCounter)
                    If sCounterObject.IndexOf("SQLServer:") = 0 Then
                        For Each sSqlNamedInstance In aOfSqlNamedInstances
                            sText = "MSSQL$" & sSqlNamedInstance.ToUpper() & ":"
                            aOfString = sCounterObject.Split(":")
                            sCounterObject = sText & aOfString(1)
                            sCounter = "\" & sCounterObject & "(" & sCounterInstance & ")" & "\" & sCounterName
                            bFound = False
                            For i = 0 To ListOfCounters.Count - 1
                                sExistingCounterExpression = ListOfCounters(i)
                                sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                                sExistingCounterName = GetCounterName(sExistingCounterExpression)
                                sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                                If sExistingCounterObject = sCounterObject Then
                                    If sExistingCounterName = sCounterName Then
                                        If sExistingCounterInstance <> "" Then
                                            If sExistingCounterInstance <> "*" Then
                                                sExistingCounterInstance = "*"
                                                ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                                bFound = True
                                                Exit For
                                            Else
                                                bFound = True
                                                Exit For
                                            End If
                                        Else
                                            bFound = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If bFound = False Then
                                ListOfCounters.Add(sCounter)
                            End If
                        Next
                    End If
                Next
            End If
        End If

        '// Sort the counters
        ListOfCounters.Sort()

        Dim sPerfLogName, sThresholdFileVersion, sThresholdFileDescription As String
        sPerfLogName = oXmlRoot.Attributes("NAME").Value
        sThresholdFileVersion = oXmlRoot.Attributes("VERSION").Value
        sThresholdFileDescription = oXmlRoot.Attributes("DESCRIPTION").Value
        sLogName = "PAL_" & sPerfLogName
        sLogName = sLogName.Replace(" ", "_")
        sLogName = sLogName.Replace("/", "_")
        sLogName = sLogName.Replace("\", "_")
        sLogName = sLogName.Replace("(", "")
        sLogName = sLogName.Replace(")", "")

        sOutput = "" & _
        "<HTML>" & vbNewLine & _
        "<HEAD>" & vbNewLine & _
        "<META HTTP-EQUIV=" & Chr(34) & "Content-Type" & Chr(34) & " CONTENT=" & Chr(34) & "text/html;" & Chr(34) & " />" & vbNewLine & _
        "<META NAME=" & Chr(34) & "GENERATOR" & Chr(34) & " Content=" & Chr(34) & "Microsoft System Monitor" & Chr(34) & " />" & vbNewLine & _
        "</HEAD>" & vbNewLine & _
        "<BODY>" & vbNewLine & _
        "<OBJECT ID=" & Chr(34) & "DISystemMonitor1" & Chr(34) & " WIDTH=" & Chr(34) & "100%" & Chr(34) & " HEIGHT=" & Chr(34) & "100%" & Chr(34) & " CLASSID=" & Chr(34) & "CLSID:C4D2D8E0-D1DD-11CE-940F-008029004347" & Chr(34) & ">" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "_Version" & Chr(34) & " VALUE=" & Chr(34) & "196611" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogName" & Chr(34) & " VALUE=" & Chr(34) & sLogName & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "Comment" & Chr(34) & " VALUE=" & Chr(34) & sThresholdFileDescription & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogType" & Chr(34) & " VALUE=" & Chr(34) & "0" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "CurrentState" & Chr(34) & " VALUE=" & Chr(34) & "1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "RealTimeDataSource" & Chr(34) & " VALUE=" & Chr(34) & "1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileMaxSize" & Chr(34) & " VALUE=" & Chr(34) & "-1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "DataStoreAttributes" & Chr(34) & " VALUE=" & Chr(34) & "34" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileBaseName" & Chr(34) & " VALUE=" & Chr(34) & sLogName & " v" & sThresholdFileVersion & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileSerialNumber" & Chr(34) & " VALUE=" & Chr(34) & "1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileFolder" & Chr(34) & " VALUE=" & Chr(34) & "C:\PerfLogs" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "Sql Log Base Name" & Chr(34) & " VALUE=" & Chr(34) & "SQL:!" & sLogName & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileAutoFormat" & Chr(34) & " VALUE=" & Chr(34) & "1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileType" & Chr(34) & " VALUE=" & Chr(34) & "2" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "StartMode" & Chr(34) & " VALUE=" & Chr(34) & "0" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "StopMode" & Chr(34) & " VALUE=" & Chr(34) & "0" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "RestartMode" & Chr(34) & " VALUE=" & Chr(34) & "0" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "LogFileName" & Chr(34) & " VALUE=" & Chr(34) & "C:\PerfLogs\" & sLogName & "_000001.blg" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "EOFCommandFile" & Chr(34) & " VALUE=" & Chr(34) & "" & Chr(34) & "/>" & vbNewLine

        For i = 0 To ListOfCounters.Count - 1
            ListOfCounters(i) = ListOfCounters(i).Replace("&", "&amp;")
            sOutput = sOutput & "	<PARAM NAME=" & Chr(34) & "Counter" & ZeroFill(i, 5) & ".Path" & Chr(34) & " VALUE=" & Chr(34) & ListOfCounters(i) & Chr(34) & "/>" & vbNewLine
        Next

        sOutput = sOutput & _
        "	<PARAM NAME=" & Chr(34) & "CounterCount" & Chr(34) & " VALUE=" & Chr(34) & ListOfCounters.Count & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "UpdateInterval" & Chr(34) & " VALUE=" & Chr(34) & "15" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "SampleIntervalUnitType" & Chr(34) & " VALUE=" & Chr(34) & "1" & Chr(34) & "/>" & vbNewLine & _
        "	<PARAM NAME=" & Chr(34) & "SampleIntervalValue" & Chr(34) & " VALUE=" & Chr(34) & "15" & Chr(34) & "/>" & vbNewLine & _
        "</OBJECT>" & vbNewLine & _
        "</BODY>" & vbNewLine & _
        "</HTML>"

        Return sOutput
    End Function

    Public Function ExportThresholdFileToLogmanCounterListFile(ByVal sThresholdFilePath As String) As String
        'Returns the body of the perfmon template file as a string.
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot, oXmlNode As XmlNode
        Dim sCounter, sOutput, sCounterObject, sCounterName, sCounterInstance As String
        Dim sExistingCounterExpression, sExistingCounterObject, sExistingCounterName, sExistingCounterInstance, sString As String
        Dim ListOfCounters As New List(Of String)
        Dim i As Integer

        Dim bFound As Boolean
        Dim sSqlServerNamedInstances, sText As String
        Dim aOfSqlNamedInstances As String()

        sString = "replaceme1,replaceme2"
        aOfSqlNamedInstances = sString.Split(",")

        oXmlGlobalThresholdDoc = New XmlDocument
        dctThresholdFileInteritanceHistory = New Dictionary(Of String, String)
        oXmlDoc = InheritFromThresholdFiles(sThresholdFilePath)
        oXmlRoot = oXmlDoc.DocumentElement

        Dim bSqlCounterObjectFound As Boolean = False
        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If sCounter.IndexOf("SQLServer:") > 0 Then
                    bSqlCounterObjectFound = True
                    Exit For
                End If
            End If
        Next

        If bSqlCounterObjectFound = True Then
            sSqlServerNamedInstances = ""
            sText = "In addition to the SQL Server default instance, do you wish to specify any SQL Server named instances (separated by semi-colons (;))?" & Chr(10) & "If none, then leave blank." & Chr(10) & "Ex: NamedInstance01;NamedInstance02"
            sSqlServerNamedInstances = InputBox(sText, "Specify Microsoft SQL Server named instances", "")
            If sSqlServerNamedInstances = "" Then
                bSqlCounterObjectFound = False
            Else
                aOfSqlNamedInstances = sSqlServerNamedInstances.Split(";")
                bSqlCounterObjectFound = True
            End If
        End If

        oXmlGlobalThresholdDoc = New XmlDocument
        dctThresholdFileInteritanceHistory = New Dictionary(Of String, String)
        oXmlDoc = InheritFromThresholdFiles(sThresholdFilePath)
        oXmlRoot = oXmlDoc.DocumentElement
        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If ListOfCounters.Count > 0 Then
                    For i = 0 To ListOfCounters.Count - 1
                        sExistingCounterExpression = ListOfCounters(i)
                        sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                        sExistingCounterName = GetCounterName(sExistingCounterExpression)
                        sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                        sCounterObject = GetCounterObject(sCounter)
                        sCounterName = GetCounterName(sCounter)
                        sCounterInstance = GetCounterInstance(sCounter)
                        If sExistingCounterObject = sCounterObject Then
                            If sExistingCounterName = sCounterName Then
                                If sExistingCounterInstance <> "" Then
                                    If sExistingCounterInstance <> "*" Then
                                        sExistingCounterInstance = "*"
                                        ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                        bFound = True
                                        Exit For
                                    Else
                                        bFound = True
                                        Exit For
                                    End If
                                Else
                                    bFound = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Else
                    bFound = False
                End If
                If bFound = False Then
                    ListOfCounters.Add(sCounter)
                End If
            End If
        Next

        '// Add the SQL Named instances counters
        Dim c As Integer
        Dim sSqlNamedInstance As String
        If bSqlCounterObjectFound = True Then
            Dim aOfString As String()
            If ListOfCounters.Count > 0 Then
                For c = 0 To ListOfCounters.Count - 1
                    sCounter = ListOfCounters(c)
                    sCounterObject = GetCounterObject(sCounter)
                    sCounterName = GetCounterName(sCounter)
                    sCounterInstance = GetCounterInstance(sCounter)
                    If sCounterObject.IndexOf("SQLServer:") = 0 Then
                        For Each sSqlNamedInstance In aOfSqlNamedInstances
                            sText = "MSSQL$" & sSqlNamedInstance.ToUpper() & ":"
                            aOfString = sCounterObject.Split(":")
                            sCounterObject = sText & aOfString(1)
                            sCounter = "\" & sCounterObject & "(" & sCounterInstance & ")" & "\" & sCounterName
                            bFound = False
                            For i = 0 To ListOfCounters.Count - 1
                                sExistingCounterExpression = ListOfCounters(i)
                                sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                                sExistingCounterName = GetCounterName(sExistingCounterExpression)
                                sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                                If sExistingCounterObject = sCounterObject Then
                                    If sExistingCounterName = sCounterName Then
                                        If sExistingCounterInstance <> "" Then
                                            If sExistingCounterInstance <> "*" Then
                                                sExistingCounterInstance = "*"
                                                ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                                bFound = True
                                                Exit For
                                            Else
                                                bFound = True
                                                Exit For
                                            End If
                                        Else
                                            bFound = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If bFound = False Then
                                ListOfCounters.Add(sCounter)
                            End If
                        Next
                    End If
                Next
            End If
        End If

        '// Sort the counters
        ListOfCounters.Sort()

        sOutput = ""
        For i = 0 To ListOfCounters.Count - 1
            sOutput = sOutput & ListOfCounters(i) & vbNewLine
        Next

        Return sOutput
    End Function

    Public Function ExportThresholdFileToDataCollectorTemplate(ByVal sThresholdFilePath As String) As String
        'Returns the body of the perfmon template file as a string.
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot, oXmlNode As XmlNode
        Dim sCounter, sOutput, sCounterObject, sCounterName, sCounterInstance, sSqlServerNamedInstances, sSqlNamedInstance As String
        Dim sExistingCounterExpression, sExistingCounterObject, sExistingCounterName, sExistingCounterInstance, sText, sString As String
        Dim ListOfCounters As New List(Of String)
        Dim i, c As Integer
        Dim aOfSqlNamedInstances As String()

        sString = "replaceme1,replaceme2"
        aOfSqlNamedInstances = sString.Split(",")

        Dim bFound As Boolean
        oXmlGlobalThresholdDoc = New XmlDocument
        dctThresholdFileInteritanceHistory = New Dictionary(Of String, String)
        oXmlDoc = InheritFromThresholdFiles(sThresholdFilePath)
        oXmlRoot = oXmlDoc.DocumentElement

        Dim bSqlCounterObjectFound As Boolean = False
        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If sCounter.IndexOf("SQLServer:") > 0 Then
                    bSqlCounterObjectFound = True
                    Exit For
                End If
            End If
        Next

        If bSqlCounterObjectFound = True Then
            sSqlServerNamedInstances = ""
            sText = "In addition to the SQL Server default instance, do you wish to specify any SQL Server named instances (separated by semi-colons (;))?" & Chr(10) & "If none, then leave blank." & Chr(10) & "Ex: NamedInstance01;NamedInstance02"
            sSqlServerNamedInstances = InputBox(sText, "Specify Microsoft SQL Server named instances", "")
            If sSqlServerNamedInstances = "" Then
                bSqlCounterObjectFound = False
                aOfSqlNamedInstances = Nothing
            Else
                aOfSqlNamedInstances = sSqlServerNamedInstances.Split(";")
                bSqlCounterObjectFound = True
            End If
        End If

        For Each oXmlNode In oXmlRoot.SelectNodes("//DATASOURCE")
            If oXmlNode.Attributes("TYPE").Value = "CounterLog" Then
                sCounter = oXmlNode.Attributes("EXPRESSIONPATH").Value
                bFound = False
                If ListOfCounters.Count > 0 Then
                    For i = 0 To ListOfCounters.Count - 1
                        sExistingCounterExpression = ListOfCounters(i)
                        sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                        sExistingCounterName = GetCounterName(sExistingCounterExpression)
                        sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                        sCounterObject = GetCounterObject(sCounter)
                        sCounterName = GetCounterName(sCounter)
                        sCounterInstance = GetCounterInstance(sCounter)
                        If sExistingCounterObject = sCounterObject Then
                            If sExistingCounterName = sCounterName Then
                                If sExistingCounterInstance <> "" Then
                                    If sExistingCounterInstance <> "*" Then
                                        sExistingCounterInstance = "*"
                                        ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                        bFound = True
                                        Exit For
                                    Else
                                        bFound = True
                                        Exit For
                                    End If
                                Else
                                    bFound = True
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                Else
                    bFound = False
                End If
                If bFound = False Then
                    ListOfCounters.Add(sCounter)
                End If
            End If
        Next

        '// Add the SQL Named instances counters
        If bSqlCounterObjectFound = True Then
            Dim aOfString As String()
            If ListOfCounters.Count > 0 Then
                For c = 0 To ListOfCounters.Count - 1
                    sCounter = ListOfCounters(c)
                    sCounterObject = GetCounterObject(sCounter)
                    sCounterName = GetCounterName(sCounter)
                    sCounterInstance = GetCounterInstance(sCounter)
                    If sCounterObject.IndexOf("SQLServer:") = 0 Then
                        For Each sSqlNamedInstance In aOfSqlNamedInstances
                            sText = "MSSQL$" & sSqlNamedInstance.ToUpper() & ":"
                            aOfString = sCounterObject.Split(":")
                            sCounterObject = sText & aOfString(1)
                            sCounter = "\" & sCounterObject & "(" & sCounterInstance & ")" & "\" & sCounterName
                            bFound = False
                            For i = 0 To ListOfCounters.Count - 1
                                sExistingCounterExpression = ListOfCounters(i)
                                sExistingCounterObject = GetCounterObject(sExistingCounterExpression)
                                sExistingCounterName = GetCounterName(sExistingCounterExpression)
                                sExistingCounterInstance = GetCounterInstance(sExistingCounterExpression)
                                If sExistingCounterObject = sCounterObject Then
                                    If sExistingCounterName = sCounterName Then
                                        If sExistingCounterInstance <> "" Then
                                            If sExistingCounterInstance <> "*" Then
                                                sExistingCounterInstance = "*"
                                                ListOfCounters(i) = "\" & sExistingCounterObject & "(" & sExistingCounterInstance & ")" & "\" & sExistingCounterName
                                                bFound = True
                                                Exit For
                                            Else
                                                bFound = True
                                                Exit For
                                            End If
                                        Else
                                            bFound = True
                                            Exit For
                                        End If
                                    End If
                                End If
                            Next
                            If bFound = False Then
                                ListOfCounters.Add(sCounter)
                            End If
                        Next
                    End If
                Next
            End If
        End If

        '// Sort the counters
        ListOfCounters.Sort()

        Dim sPerfLogName, sThresholdFileVersion, sThresholdFileDescription, sLogName As String
        sPerfLogName = oXmlRoot.Attributes("NAME").Value
        sThresholdFileVersion = oXmlRoot.Attributes("VERSION").Value
        sThresholdFileDescription = oXmlRoot.Attributes("DESCRIPTION").Value
        sLogName = "PAL_" & sPerfLogName
        sLogName = sLogName.Replace(" ", "_")
        sLogName = sLogName.Replace("/", "_")
        sLogName = sLogName.Replace("\", "_")
        sLogName = sLogName.Replace("(", "")
        sLogName = sLogName.Replace(")", "")

        sOutput = "" & _
        "<?xml version=" & Chr(34) & "1.0" & Chr(34) & " encoding=" & Chr(34) & "UTF-8" & Chr(34) & "?>" & vbNewLine & _
        "<?Copyright (c) Microsoft Corporation. All rights reserved.?>" & vbNewLine & _
        "<DataCollectorSet>" & vbNewLine & _
        "<Name>" & sLogName & "</Name>" & vbNewLine & _
        "<DisplayName>@%systemroot%\system32\wdc.dll,#10026</DisplayName>" & vbNewLine & _
        "<Description>@%systemroot%\system32\wdc.dll,#10027</Description>" & vbNewLine & _
        "<Keyword>CPU</Keyword>" & vbNewLine & _
        "<Keyword>Memory</Keyword>" & vbNewLine & _
        "<Keyword>Disk</Keyword>" & vbNewLine & _
        "<Keyword>Network</Keyword>" & vbNewLine & _
        "<Keyword>Performance</Keyword>" & vbNewLine & _
        "<RootPath>%systemdrive%\perflogs\System\Performance</RootPath>" & vbNewLine & _
        "<SubdirectoryFormat>3</SubdirectoryFormat>" & vbNewLine & _
        "<SubdirectoryFormatPattern>yyyyMMdd\-NNNNNN</SubdirectoryFormatPattern>" & vbNewLine & _
        "<PerformanceCounterDataCollector>" & vbNewLine & _
        "    <Name>" & sLogName & "</Name>" & vbNewLine & _
        "    <SampleInterval>15</SampleInterval>" & vbNewLine
        For i = 0 To ListOfCounters.Count - 1
            ListOfCounters(i) = ListOfCounters(i).Replace("&", "&amp;")
            sOutput = sOutput & "    <Counter>" & ListOfCounters(i) & "</Counter>" & vbNewLine
        Next
        sOutput = sOutput & _
        "</PerformanceCounterDataCollector>" & vbNewLine & _
        "</DataCollectorSet>"

        Return sOutput
    End Function

    Private Function RemoveCounterComputer(ByVal sCounter As String)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        '\\IDCWEB1\Processor\% Processor Time"
        '\Processor(_Total)\% Processor Time"
        '\Processor\% Processor Time"
        'Processor(_Total)\% Processor Time"
        'Processor\% Processor Time"
        Dim iLocThirdBackSlash As Integer
        ' Removes the counter computer name
        If sCounter.Substring(0, 2) <> "\\" Then
            RemoveCounterComputer = sCounter
            Exit Function
        End If
        iLocThirdBackSlash = InStr(3, sCounter, "\")
        RemoveCounterComputer = Mid(sCounter, iLocThirdBackSlash)
    End Function

    Private Function RemoveCounterInstance(ByVal sCounter As String)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        ' Removes the counter instance name
        Dim sNewCounter, sLeftPart, sRightPart As String
        Dim iLocFirstParen, iLocSecondParen, iLen As Integer
        sNewCounter = sCounter
        iLocFirstParen = InStr(sNewCounter, "(")
        If iLocFirstParen = 0 Then
            RemoveCounterInstance = sNewCounter
            Exit Function
        End If
        iLocSecondParen = InStr(sNewCounter, ")")
        iLen = iLocSecondParen - iLocFirstParen - 1
        sLeftPart = sNewCounter(iLocFirstParen - 1)
        'sLeftPart = Left(sNewCounter, iLocFirstParen - 1)
        sRightPart = Mid(sNewCounter, iLocSecondParen + 1)
        Return sLeftPart & sRightPart
    End Function

    Private Function GetCounterComputer(ByVal sCounter As String)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        ' Returns the counter computer name
        Dim sCounterComputer As String
        Dim iLocThirdBackSlash As Integer
        sCounterComputer = sCounter
        If sCounterComputer.Substring(0, 2) <> "\\" Then
            GetCounterComputer = ""
            Exit Function
        End If
        iLocThirdBackSlash = InStr(3, sCounterComputer, "\")
        Return Trim(Mid(sCounterComputer, 3, iLocThirdBackSlash - 3))
    End Function

    Private Function ConvertStringToArray(ByVal sText As String)
        Dim aStringArray()
        Dim iLen, i As Integer
        iLen = Len(sText)
        ReDim aStringArray(iLen - 1)
        For i = 0 To iLen - 1
            aStringArray(i) = Mid(sText, i + 1, 1)
        Next
        Return aStringArray
    End Function

    Private Function GetCounterObject(ByVal sCounter As String)
        '"\\demoserver\SQLServer:Latches\Latch Waits/sec (ms)"
        'WScript.Echo GetCounterObject("\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec") & " = (MSSQL|SQLServer).*:Locks"
        'WScript.Echo GetCounterObject("\\IDCWEB1\Processor(_Total)\% Processor Time") & " = Processor"
        'WScript.Echo GetCounterObject("\Processor(_Total)\% Processor Time") & " = Processor"
        'WScript.Echo GetCounterObject("\Category\Counter") & " = Category"
        'WScript.Echo GetCounterObject("\Category\Counter(x)") & " = Category"
        'WScript.Echo GetCounterObject("\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak") & " = Paging File"
        'WScript.Echo GetCounterObject("\Category(Instance(x))\Counter (x)") & " = Category"
        'WScript.Echo GetCounterObject("\(MSSQL|SQLServer).*:Memory Manager\Total Server Memory (KB)") & " = (MSSQL|SQLServer).*:Memory Manager"
        Dim sCounterObject, sOriginalCounterPath As String
        Dim iLocLeftParen, iLocThirdBackSlash, iLocBackSlash As Integer
        sOriginalCounterPath = sCounter
        sCounterObject = sCounter
        ' Returns the counter object           
        If sCounterObject.Substring(0, 2) = "\\" Then
            '\\IDCWEB1\Processor(_Total)\% Processor Time
            '\\IDCWEB1\Processor\% Processor Time
            iLocThirdBackSlash = InStr(3, sCounterObject, "\")
            sCounterObject = Mid(sCounterObject, iLocThirdBackSlash + 1)
            'Processor(_Total)\% Processor Time
            'Processor\% Processor Time
        ElseIf sCounterObject.Substring(0, 1) = "\" Then
            '\Processor\% Processor Time
            '\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec
            sCounterObject = sCounterObject.Substring(1)
            'Processor\% Processor Time
        Else
            GetCounterObject = ""
            Exit Function
        End If
        'SQLServer:Latches\Latch Waits/sec (ms)
        iLocBackSlash = InStr(sCounterObject, "\")
        sCounterObject = StripCounterNameFromCounterString(sCounterObject)
        'If Right(sCounterObject, 1) = ")" Then
        If sCounterObject.Substring(sCounterObject.Length - 1) = ")" Then
            Dim aChar
            Dim iRightParenCount, i As Integer
            aChar = ConvertStringToArray(sCounterObject)
            iRightParenCount = 0
            For i = UBound(aChar) To 0 Step -1
                If aChar(i) = ")" Then
                    iRightParenCount = iRightParenCount + 1
                End If
                If aChar(i) = "(" Then
                    iLocLeftParen = i + 1
                    If iRightParenCount = 1 Then
                        Exit For
                    Else
                        iRightParenCount = iRightParenCount - 1
                    End If
                End If
            Next
            iLocLeftParen = iLocLeftParen - 1
            Return sCounterObject.Substring(0, iLocLeftParen)
        Else
            ' Category\Counter
            Return sCounterObject
        End If
    End Function

    Private Function GetCounterName(ByVal sCounter As String)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        '\Processor(_Total)\% Processor Time"
        '\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak
        '\Category(Instance(x))\Counter (x)
        ' Returns the counter name
        Dim sCounterName As String
        Dim aBackSlashes
        sCounterName = sCounter
        aBackSlashes = Split(sCounterName, "\")
        Return aBackSlashes(UBound(aBackSlashes))
    End Function

    Private Function StripCounterNameFromCounterString(ByVal sCounter As String)
        Dim sCounterName, iLenCounterName
        If InStr(sCounter, "\") = 0 Then
            StripCounterNameFromCounterString = sCounter
            Exit Function
        End If
        sCounterName = sCounter
        iLenCounterName = Len(GetCounterName(sCounterName)) + 1
        If iLenCounterName > 0 Then
            Return Mid(sCounterName, 1, Len(sCounterName) - iLenCounterName)
        Else
            Return sCounter
        End If
    End Function

    Private Function StripComputerNameFromCounterString(ByVal sCounter As String)
        Dim sCounterName As String
        Dim iLocThirdBackSlash As Integer
        sCounterName = sCounter
        If sCounterName.Substring(0, 2) = "\\" Then
            iLocThirdBackSlash = InStr(3, sCounterName, "\")
            sCounterName = Mid(sCounterName, iLocThirdBackSlash)
        End If
        Return sCounterName
    End Function

    Private Function GetCounterInstance(ByVal sCounter As String)
        '\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak
        '\Category(Instance(x))\Counter (x)
        '\SQLServer:Latches\Latch Waits/sec (ms)
        '\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec
        '\(MSSQL|SQLServer).*:Memory Manager\Total Server Memory (KB)
        Dim iLocLeftParen, iRightParenCount, i As Integer
        Dim aChar
        Dim sCounterName, sInstanceLength As String
        Dim bInstanceFound As Boolean
        sCounterName = sCounter
        sCounterName = StripComputerNameFromCounterString(sCounterName)
        sCounterName = StripCounterNameFromCounterString(sCounterName)
        If InStr(sCounter, ")\") = 0 Then
            GetCounterInstance = ""
            Exit Function
        End If
        aChar = ConvertStringToArray(sCounterName)
        iRightParenCount = 0
        bInstanceFound = False
        For i = UBound(aChar) To 0 Step -1
            If aChar(i) = ")" Then
                iRightParenCount = iRightParenCount + 1
                bInstanceFound = True
            End If
            If aChar(i) = "(" Then
                iLocLeftParen = i + 1
                If iRightParenCount = 1 Then
                    Exit For
                Else
                    iRightParenCount = iRightParenCount - 1
                End If
            End If
        Next
        If bInstanceFound = False Then
            Return ""
            Exit Function
        Else
            sInstanceLength = Len(sCounterName) - iLocLeftParen - 1
            Return Mid(sCounterName, iLocLeftParen + 1, sInstanceLength)
        End If
    End Function

    Private Function ZeroFill(ByVal iNum As Integer, ByVal iLength As Integer) As String
        Dim sOutput As String
        sOutput = iNum.ToString
        If Len(sOutput) >= iLength Then
            Return sOutput
            Exit Function
        End If
        Do
            sOutput = "0" & sOutput
        Loop Until Len(sOutput) = iLength
        Return sOutput
    End Function

    Public Function IsLogParserInstalled()
        Dim oRegKey As Object
        Dim sKey As String
        sKey = "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{4A1AAA95-FD08-449B-BD16-E87083D8F087}"
        oRegKey = Microsoft.Win32.Registry.GetValue(sKey, "", "")
        If IsNothing(oRegKey) = False Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IsOWC11Installed()
        Dim oRegKey As Object
        Dim sKey As String
        sKey = "HKEY_LOCAL_MACHINE\SOFTWARE\Classes\CLSID\{0002E559-0000-0000-C000-000000000046}"
        oRegKey = Microsoft.Win32.Registry.GetValue(sKey, "", "")
        If IsNothing(oRegKey) = False Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GenerateNewGUID()
        Return "{" & Guid.NewGuid().ToString & "}"
    End Function

    Function SaveTextFile(ByVal sFileName As String, ByVal sText As String) As Boolean
        Try
            My.Computer.FileSystem.WriteAllText(sFileName, sText, False, System.Text.Encoding.ASCII)
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function


End Class

Public Class LogFileDataObject
    Public Status As LogFileStatus
    Public RemoteFilePath As String
    Public LocalFilePath As String
    Public IsCopiedLocally As Boolean
    Public AnalysisInterval As Integer

    Public Function Analyze()
        Return 0
    End Function

    Public Function StartLog()
        Return 0
    End Function

    Public Function StopLog()
        Return 0
    End Function
End Class

Public Enum LogFileStatus
    Started
    Stopped
    Unknown
    ManuallyAdded
End Enum

Public Class PALCounterObject
    Public CounterComputer As String
    Public CounterObject As String
    Public CounterName As String
    Public CounterInstance As String
    Public CounterPath As String

    Public Sub ProcessCounterPath()
        Me.CounterComputer = GetCounterComputer(Me.CounterPath)
        Me.CounterObject = GetCounterObject(Me.CounterPath)
        Me.CounterName = GetCounterName(Me.CounterPath)
        Me.CounterInstance = GetCounterInstance(Me.CounterPath)
    End Sub

    Public Function GetCounterComputer(ByVal sCounter As String) As String
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        ' Returns the counter computer name
        Dim sCounterComputer As String
        Dim iLocThirdBackSlash As Integer
        sCounterComputer = sCounter
        If Left(sCounterComputer, 2) <> "\\" Then
            GetCounterComputer = ""
            Exit Function
        End If
        iLocThirdBackSlash = InStr(3, sCounterComputer, "\")
        Return Trim(Mid(sCounterComputer, 3, iLocThirdBackSlash - 3))
    End Function

    Public Function GetCounterObject(ByVal sCounter As String) As String
        '"\\demoserver\SQLServer:Latches\Latch Waits/sec (ms)"
        'WScript.Echo GetCounterObject("\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec") & " = (MSSQL|SQLServer).*:Locks"
        'WScript.Echo GetCounterObject("\\IDCWEB1\Processor(_Total)\% Processor Time") & " = Processor"
        'WScript.Echo GetCounterObject("\Processor(_Total)\% Processor Time") & " = Processor"
        'WScript.Echo GetCounterObject("\Category\Counter") & " = Category"
        'WScript.Echo GetCounterObject("\Category\Counter(x)") & " = Category"
        'WScript.Echo GetCounterObject("\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak") & " = Paging File"
        'WScript.Echo GetCounterObject("\Category(Instance(x))\Counter (x)") & " = Category"
        'WScript.Echo GetCounterObject("\(MSSQL|SQLServer).*:Memory Manager\Total Server Memory (KB)") & " = (MSSQL|SQLServer).*:Memory Manager"
        Dim sCounterObject As String
        Dim iLocThirdBackSlash, iLocBackSlash As Integer
        sCounterObject = sCounter
        ' Returns the counter object           
        If Left(sCounterObject, 2) = "\\" Then
            '\\IDCWEB1\Processor(_Total)\% Processor Time
            '\\IDCWEB1\Processor\% Processor Time
            iLocThirdBackSlash = InStr(3, sCounterObject, "\")
            sCounterObject = Mid(sCounterObject, iLocThirdBackSlash + 1)
            'Processor(_Total)\% Processor Time
            'Processor\% Processor Time
        ElseIf Left(sCounterObject, 1) = "\" Then
            '\Processor\% Processor Time
            '\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec
            sCounterObject = Mid(sCounterObject, 2)
            'Processor\% Processor Time
        End If
        'SQLServer:Latches\Latch Waits/sec (ms)
        iLocBackSlash = InStr(sCounterObject, "\")
        sCounterObject = StripCounterNameFromCounterString(sCounterObject)
        If Right(sCounterObject, 1) = ")" Then
            Dim aChar, iRightParenCount, i, iLocLeftParen
            iLocLeftParen = 0
            aChar = ConvertStringToCharArray(sCounterObject)
            iRightParenCount = 0
            For i = UBound(aChar) To 0 Step -1
                If aChar(i) = ")" Then
                    iRightParenCount = iRightParenCount + 1
                End If
                If aChar(i) = "(" Then
                    iLocLeftParen = i + 1
                    If iRightParenCount = 1 Then
                        Exit For
                    Else
                        iRightParenCount = iRightParenCount - 1
                    End If
                End If
            Next
            iLocLeftParen = iLocLeftParen - 1
            GetCounterObject = Left(sCounterObject, iLocLeftParen)
        Else
            ' Category\Counter
            GetCounterObject = sCounterObject
        End If
    End Function

    Public Function GetCounterName(ByVal sCounter) As String
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        '\Processor(_Total)\% Processor Time"
        '\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak
        '\Category(Instance(x))\Counter (x)
        ' Returns the counter name
        Dim sCounterName, aBackSlashes() As String
        sCounterName = sCounter
        aBackSlashes = Split(sCounterName, "\")
        Return aBackSlashes(UBound(aBackSlashes))
    End Function

    Function StripCounterNameFromCounterString(ByVal sCounter As String) As String
        Dim sCounterName As String
        Dim iLenCounterName As Integer
        If InStr(sCounter, "\") = 0 Then
            Return sCounter
            Exit Function
        End If
        sCounterName = sCounter
        iLenCounterName = Len(GetCounterName(sCounterName)) + 1
        If iLenCounterName > 0 Then
            Return Mid(sCounterName, 1, Len(sCounterName) - iLenCounterName)
        Else
            Return sCounter
        End If
    End Function

    Function ConvertStringToCharArray(ByVal sText As String) As String()
        Dim aStringArray() As String
        Dim iLen, i As Integer
        iLen = Len(sText)
        ReDim aStringArray(iLen - 1)
        For i = 0 To iLen - 1
            aStringArray(i) = Mid(sText, i + 1, 1)
        Next
        Return aStringArray
    End Function

    Public Function GetCounterInstance(ByVal sCounter As String) As String
        '\\BLACKVISE\Paging File(\??\C:\pagefile.sys)\% Usage Peak
        '\Category(Instance(x))\Counter (x)
        '\SQLServer:Latches\Latch Waits/sec (ms)
        '\(MSSQL|SQLServer).*:Locks(_Total)\Lock Requests/sec
        '\(MSSQL|SQLServer).*:Memory Manager\Total Server Memory (KB)
        Dim iLocLeftParen, iRightParenCount, i As Integer
        Dim sInstanceLength, sCounterName, aChar() As String
        Dim bInstanceFound As Boolean
        sCounterName = sCounter
        sCounterName = StripComputerNameFromCounterString(sCounterName)
        sCounterName = StripCounterNameFromCounterString(sCounterName)
        If InStr(sCounter, ")\") = 0 Then
            Return ""
            Exit Function
        End If
        aChar = ConvertStringToCharArray(sCounterName)
        iRightParenCount = 0
        bInstanceFound = False
        For i = UBound(aChar) To 0 Step -1
            If aChar(i) = ")" Then
                iRightParenCount = iRightParenCount + 1
                bInstanceFound = True
            End If
            If aChar(i) = "(" Then
                iLocLeftParen = i + 1
                If iRightParenCount = 1 Then
                    Exit For
                Else
                    iRightParenCount = iRightParenCount - 1
                End If
            End If
        Next
        If bInstanceFound = False Then
            Return ""
            Exit Function
        Else
            sInstanceLength = Len(sCounterName) - iLocLeftParen - 1
            Return Mid(sCounterName, iLocLeftParen + 1, sInstanceLength)
        End If
    End Function

    Function StripComputerNameFromCounterString(ByVal sCounter As String) As String
        Dim sCounterName As String
        Dim iLocThirdBackSlash As Integer
        sCounterName = sCounter
        If Left(sCounterName, 2) = "\\" Then
            iLocThirdBackSlash = InStr(3, sCounterName, "\")
            sCounterName = Mid(sCounterName, iLocThirdBackSlash)
        End If
        Return sCounterName
    End Function
End Class

Public Class ComputerCounterLogsDataObject
    Public aLogFiles() As LogFileDataObject
    Public PerfLogsDirectory As String
    Public PollingInterval As Integer

    Public Function AutoDiscover()
        Return 0
    End Function
End Class

Public Class ComputerProfileDataObject
    Public aComputerCounterLogs() As ComputerCounterLogsDataObject
    Public dctQuestionAnswers As Dictionary(Of String, String)
    Public ComputerName As String
End Class

Public Class ComputerGroupDataObject
    Public aComputersProfiles() As ComputerProfileDataObject
End Class

Public Class ThresholdFileDataObject
    Public aComputerGroups() As ComputerGroupDataObject
    Public ThresholdFileName As String
    Public ThresholdFilePath As String
End Class

Public Class PALCounterLogFileObject
    Public sOriginalLogFilePath As String
    Public sCSVLogFilePath As String
    Public sFilteredLogFilePath As String
    Public sCounterList() As String
    Public dBeginTime As DateTime
    Public dEndTime As DateTime
    Public iNumberOfSamples As Long

    Public Function GetCounterListFromPerfCounterCSVFile(ByVal sSourcePerfCounterCSVFilePath As String) As String()
        Dim sLine, aLines() As String
        Dim oFile As System.IO.TextReader = My.Computer.FileSystem.OpenTextFileReader(sSourcePerfCounterCSVFilePath)
        sLine = oFile.ReadLine
        aLines = Split(sLine, ",")
        Return aLines
    End Function

    Public Function ConvertCounterLogBLGToCSV(ByVal sSourceBLGFilePath As String, ByVal sOutputCSVFilePath As String) As Boolean
        '// Returns the file path to the CSV produced
        Dim sArgs As String
        Dim sCmd As String
        sCmd = "ReLog.exe"
        If InStr(1, sSourceBLGFilePath, Chr(34), 1) > 0 Then
            sArgs = " " & sSourceBLGFilePath & " -f CSV -y -o " & sOutputCSVFilePath
        Else
            sArgs = " " & Chr(34) & sSourceBLGFilePath & Chr(34) & " -f CSV -y -o " & sOutputCSVFilePath
        End If
        Dim consoleApp As New Process
        With consoleApp
            .StartInfo.UseShellExecute = False
            .StartInfo.RedirectStandardOutput = False
            .StartInfo.FileName = sCmd
            .StartInfo.Arguments = sArgs
            .StartInfo.CreateNoWindow = True
            .Start()
        End With
        consoleApp.WaitForExit()
        '// Wait for the output file to be created.
        Return True
    End Function

End Class

Public Class PALCommandObject
    Public PALScriptInstallDirectory As String = "."
    Public CounterLogPaths As String = ""
    Public ThresholdFilePath As String = ""
    Public AnalysisInterval As String = "AUTO"
    Public OutputDirectory As String = "[My Documents]\PAL Reports"
    Public IsOutputHTML As Boolean = True
    Public IsOutputXML As Boolean = False
    Public AllCounterStats As Boolean = False
    Public dctQuestionCollection As New SortedDictionary(Of String, QuestionObject)
    Public CommandLine As String = ""
    Public HTMLOutputFileName As String = "[LogFileName]_PAL_ANALYSIS_[DateTimeStamp].htm"
    Public XMLOutputFileName As String = "[LogFileName]_PAL_ANALYSIS_[DateTimeStamp].xml"
    Public TimeRangeRestriction As Boolean = False
    Public BeginTime As Date
    Public EndTime As Date
    Public NumberOfThreads As Integer = 1
    Public IsLowPriority As Boolean

    Public Function BuildCommandLine() As String
        Dim sKey As String
        Const DEFAULT_OUTPUT_DIRECTORY As String = "[My Documents]\PAL Reports"
        Dim sBatchLine, sLogFile, sThresholdFile, sInterval, sOutputDirectory, sIsOutputHTML, sIsOutputXML, sHTMLOutputFileName, sXMLOutputFileName, sAllCounterStats, sBeginTime, sEndTime, sNumberOfThreads, sIsLowPriority As String

        sLogFile = "-Log " & AddDoubleQuotesOrNot(CounterLogPaths)
        sThresholdFile = "-ThresholdFile " & AddDoubleQuotesOrNot(ThresholdFilePath)
        sInterval = "-Interval " & AddDoubleQuotesOrNot(ConvertIntervalToSeconds(AnalysisInterval))

        If IsLowPriority = True Then
            sIsLowPriority = "-IsLowPriority $True"
        Else
            sIsLowPriority = "-IsLowPriority $False"
        End If
        If IsOutputHTML = True Then
            sIsOutputHTML = "-IsOutputHtml $True"
        Else
            sIsOutputHTML = "-IsOutputHtml $False"
        End If
        If IsOutputXML = True Then
            sIsOutputXML = "-IsOutputXml $True"
        Else
            sIsOutputXML = "-IsOutputXml $False"
        End If
        If AllCounterStats = True Then
            sAllCounterStats = "-AllCounterStats $True"
        Else
            sAllCounterStats = "-AllCounterStats $False"
        End If

        sHTMLOutputFileName = "-HtmlOutputFileName " & AddDoubleQuotesOrNot(HTMLOutputFileName)
        sXMLOutputFileName = "-XmlOutputFileName " & AddDoubleQuotesOrNot(XMLOutputFileName)

        If TimeRangeRestriction = True Then
            sBeginTime = "-BeginTime " & Chr(34) & BeginTime.ToString & Chr(34)
            sEndTime = "-EndTime " & Chr(34) & EndTime.ToString & Chr(34)
        Else
            sBeginTime = ""
            sEndTime = ""
        End If

        If OutputDirectory = DEFAULT_OUTPUT_DIRECTORY Then
            '// If set to the default of PAL Reports, then omit the output directory setting.
            sOutputDirectory = ""
        Else
            sOutputDirectory = "-OutputDir " & AddDoubleQuotesOrNot(OutputDirectory)
        End If
        sNumberOfThreads = "-NumberOfThreads " & NumberOfThreads
        If sOutputDirectory = "" Then
            sBatchLine = "Powershell -ExecutionPolicy ByPass -NoProfile -File " & Chr(34) & ".\PAL.ps1" & Chr(34) & " " & sLogFile & " " & sThresholdFile & " " & sInterval & " " & sIsOutputHTML & " " & sHTMLOutputFileName & " " & sIsOutputXML & " " & sXMLOutputFileName & " " & sAllCounterStats & " " & sNumberOfThreads & " " & sIsLowPriority
        Else
            sBatchLine = "Powershell -ExecutionPolicy ByPass -NoProfile -File " & Chr(34) & ".\PAL.ps1" & Chr(34) & " " & sLogFile & " " & sThresholdFile & " " & sInterval & " " & sIsOutputHTML & " " & sHTMLOutputFileName & " " & sIsOutputXML & " " & sXMLOutputFileName & " " & sAllCounterStats & " " & sOutputDirectory & " " & sNumberOfThreads & " " & sIsLowPriority
        End If
        If LCase(IsOutputHTML) = "$true" Then
            sBatchLine = sBatchLine & " " & sHTMLOutputFileName
        End If
        If dctQuestionCollection.Count > 0 Then
            For Each sKey In dctQuestionCollection.Keys
                If dctQuestionCollection(sKey).QuestionDataType = "boolean" Then
                    If dctQuestionCollection(sKey).bAnswer = True Then
                        sBatchLine = sBatchLine & " " & "-" & sKey & " $True"
                    Else
                        sBatchLine = sBatchLine & " " & "-" & sKey & " $False"
                    End If
                Else
                    sBatchLine = sBatchLine & " " & "-" & sKey & " " & Chr(34) & dctQuestionCollection(sKey).sAnswer & Chr(34)
                End If
            Next
        End If
        If TimeRangeRestriction = True Then
            sBatchLine = sBatchLine & " " & sBeginTime & " " & sEndTime
        End If
        CommandLine = sBatchLine
        Return sBatchLine
    End Function

    Public Function BuildQueryStyleCommandLine() As String
        Dim sKey As String
        Const DEFAULT_OUTPUT_DIRECTORY As String = "[My Documents]\PAL Reports"
        Dim sBatchLine, sLogFile, sThresholdFile, sInterval, sOutputDirectory, sIsOutputHTML, sIsOutputXML, sHTMLOutputFileName, sXMLOutputFileName, sAllCounterstats, sBeginTime, sEndTime, sNumberOfThreads, sIsLowPriority As String

        sLogFile = "-Log " & AddDoubleQuotesOrNot(CounterLogPaths)
        sThresholdFile = "-ThresholdFile " & AddDoubleQuotesOrNot(ThresholdFilePath)
        sInterval = "-Interval " & AddDoubleQuotesOrNot(ConvertIntervalToSeconds(AnalysisInterval))
        If IsLowPriority = True Then
            sIsLowPriority = "-IsLowPriority $True"
        Else
            sIsLowPriority = "-IsLowPriority $False"
        End If
        If IsOutputHTML = True Then
            sIsOutputHTML = "-IsOutputHtml $True"
        Else
            sIsOutputHTML = "-IsOutputHtml $False"
        End If
        If IsOutputXML = True Then
            sIsOutputXML = "-IsOutputXml $True"
        Else
            sIsOutputXML = "-IsOutputXml $False"
        End If
        If AllCounterStats = True Then
            sAllCounterstats = "-AllCounterStats $True"
        Else
            sAllCounterstats = "-AllCounterStats $False"
        End If
        sHTMLOutputFileName = "-HtmlOutputFileName " & AddDoubleQuotesOrNot(HTMLOutputFileName)
        sXMLOutputFileName = "-XmlOutputFileName " & AddDoubleQuotesOrNot(XMLOutputFileName)
        If TimeRangeRestriction = True Then
            sBeginTime = "-BeginTime " & Chr(34) & BeginTime.ToString & Chr(34)
            sEndTime = "-EndTime " & Chr(34) & EndTime.ToString & Chr(34)
        Else
            sBeginTime = ""
            sEndTime = ""
        End If
        If OutputDirectory = DEFAULT_OUTPUT_DIRECTORY Then
            '// If set to the default of PAL Reports, then omit the output directory setting.
            sOutputDirectory = ""
        Else
            sOutputDirectory = "-OutputDir " & AddDoubleQuotesOrNot(OutputDirectory)
        End If
        sNumberOfThreads = "-NumberOfThreads " & NumberOfThreads
        If sOutputDirectory = "" Then
            sBatchLine = "Powershell -ExecutionPolicy ByPass -NoProfile -File " & Chr(34) & ".\PAL.ps1" & Chr(34) & vbNewLine & sLogFile & vbNewLine & sThresholdFile & vbNewLine & sInterval & vbNewLine & sIsOutputHTML & vbNewLine & sHTMLOutputFileName & vbNewLine & sIsOutputXML & vbNewLine & sXMLOutputFileName & vbNewLine & sAllCounterstats & vbNewLine & sNumberOfThreads & vbNewLine & sIsLowPriority
        Else
            sBatchLine = "Powershell -ExecutionPolicy ByPass -NoProfile -File " & Chr(34) & ".\PAL.ps1" & Chr(34) & vbNewLine & sLogFile & vbNewLine & sThresholdFile & vbNewLine & sInterval & vbNewLine & sIsOutputHTML & vbNewLine & sHTMLOutputFileName & vbNewLine & sIsOutputXML & vbNewLine & sXMLOutputFileName & vbNewLine & sOutputDirectory & vbNewLine & sAllCounterstats & vbNewLine & sNumberOfThreads & vbNewLine & sIsLowPriority
        End If
        If LCase(IsOutputHTML) = "$true" Then
            sBatchLine = sBatchLine & vbNewLine & sHTMLOutputFileName
        End If

        If dctQuestionCollection.Count > 0 Then
            For Each sKey In dctQuestionCollection.Keys
                If dctQuestionCollection(sKey).QuestionDataType = "boolean" Then
                    If dctQuestionCollection(sKey).bAnswer = True Then
                        sBatchLine = sBatchLine & vbNewLine & "-" & sKey & " $True"
                    Else
                        sBatchLine = sBatchLine & vbNewLine & "-" & sKey & " $False"
                    End If
                Else
                    sBatchLine = sBatchLine & vbNewLine & "-" & sKey & " " & Chr(34) & dctQuestionCollection(sKey).sAnswer & Chr(34)
                End If
            Next
        End If
        If TimeRangeRestriction = True Then
            sBatchLine = sBatchLine & vbNewLine & sBeginTime & vbNewLine & sEndTime
        End If
        CommandLine = sBatchLine
        Return sBatchLine
    End Function

    Private Function RemoveQuotesFromString(ByVal sText) As String
        Dim iEndOfString As Integer
        iEndOfString = Len(sText) - 2
        sText = Mid(sText, 1, iEndOfString)
        Return sText
    End Function

    Private Function AddSingleQuotesOrNot(ByVal sText) As String
        Dim SQ, DQ As String
        SQ = Chr(39) ' single quote
        DQ = Chr(34) ' double quote
        If InStr(sText, SQ) > 0 Then
            Return sText
        Else
            If InStr(sText, DQ) > 0 Then
                ' Strip the double quotes.
                sText = RemoveQuotesFromString(sText)
            End If
            Return SQ & sText & SQ
        End If
    End Function

    Private Function AddDoubleQuotesOrNot(ByVal sText) As String
        Dim DQ As String
        DQ = Chr(34) ' double quote
        If InStr(sText, DQ) > 0 Then
            Return sText
        Else
            Return DQ & sText & DQ
        End If
    End Function

    Private Function ConvertIntervalToSeconds(ByVal sInterval) As String
        Select Case sInterval
            Case "AUTO"
                Return "AUTO"
            Case "10 minutes"
                Return "600"
            Case "20 minutes"
                Return "1200"
            Case "30 minutes"
                Return "1800"
            Case "40 minutes"
                Return "2400"
            Case "50 minutes"
                Return "3000"
            Case "1 hour"
                Return "3600"
            Case "2 hours"
                Return "7200"
            Case "3 hours"
                Return "10800"
            Case "4 hours"
                Return "14400"
            Case "5 hours"
                Return "18000"
            Case "6 hours"
                Return "21600"
            Case "7 hours"
                Return "25200"
            Case "8 hours"
                Return "28800"
            Case "9 hours"
                Return "32400"
            Case "10 hours"
                Return "36000"
            Case "11 hours"
                Return "39600"
            Case "12 hours"
                Return "43200"
            Case "13 hours"
                Return "46800"
            Case "14 hours"
                Return "50400"
            Case "15 hours"
                Return "54000"
            Case "16 hours"
                Return "57600"
            Case "17 hours"
                Return "61200"
            Case "18 hours"
                Return "64800"
            Case "19 hours"
                Return "68400"
            Case "20 hours"
                Return "72000"
            Case "21 hours"
                Return "75600"
            Case "22 hours"
                Return "79200"
            Case "23 hours"
                Return "82800"
            Case "1 day"
                Return "86400"
            Case "2 days"
                Return "172800"
            Case "3 days"
                Return "259200"
            Case "4 days"
                Return "345600"
            Case "5 days"
                Return "432000"
            Case "6 days"
                Return "518400"
            Case "1 week"
                Return "604800"
            Case Else
                Return sInterval
        End Select
    End Function
End Class

Public Class QuestionObject
    Public Question As String
    Public QuestionVarName As String
    Public QuestionDataType As String
    Public DefaultValue As String
    Public sAnswer As String
    Public bAnswer As Boolean
    Public Options As String

End Class

Public Class AnalysisCollectionObject
    Public Name As String
    Public Version As String
    Public Description As String
    Public ContentOwners As String
    Public FeedbackEmailAddresses As String
    Public FilePath As String
    Public XmlRoot As XmlElement
End Class

Public Class PALCommandQueueObject
    Public aCommandQueue() As PALCommandObject
    Public sBatchFilePath As String
    Public sWorkingDirectory As String
    Public bLowPriorityExecution As Boolean

    Sub New()
        Dim sGUID As String
        Const BATCH_FILE_NAME = "_RunPAL.bat"
        sGUID = GenerateNewGUID()
        sWorkingDirectory = My.Application.GetEnvironmentVariable("TEMP")
        sBatchFilePath = sWorkingDirectory & "\" & sGUID & BATCH_FILE_NAME
    End Sub

    Public Function BuildBatchFileText(ByVal oPALCommandQueueObject As PALCommandQueueObject) As String
        Dim oCommand As PALCommandObject
        Const LOW_PRIORITY = "start /LOW /WAIT "
        Dim sBatchText As String = ""
        For Each oCommand In oPALCommandQueueObject.aCommandQueue
            If bLowPriorityExecution = True Then
                sBatchText = sBatchText & LOW_PRIORITY & oCommand.BuildCommandLine() & vbNewLine
            Else
                sBatchText = sBatchText & oCommand.BuildCommandLine() & vbNewLine
            End If
        Next
        Return sBatchText
    End Function

    Sub SaveTextAsBatchFile(ByVal sFilePath As String, ByVal sText As String, ByVal sPalInstallationDirectory As String)
        Dim sPalInstallDriveLetter As String
        sPalInstallDriveLetter = Left(sPalInstallationDirectory, 2)
        sText = "cd " & Chr(34) & sPalInstallationDirectory & Chr(34) & vbNewLine & sText
        sText = sPalInstallDriveLetter & vbNewLine & sText & vbNewLine & "REM This Windows can be closed."
        My.Computer.FileSystem.WriteAllText(sFilePath, sText, False, System.Text.Encoding.ASCII)
    End Sub

    Public Function GenerateNewGUID()
        Return "{" & Guid.NewGuid().ToString & "}"
    End Function
End Class