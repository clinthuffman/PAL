'// CreateAndStartPerfmonLogs.vbs
'// Purpose: This script is designed for use in Health Checks.
'//  It creates and starts perfmon logs on multiple servers.
'//
'// Syntax:
'// CScript CreateAndStartPerfmonLogs.vbs <computer[;computer]> <ServerType> <CounterListFilePath>
'//  <computer[;computer]>  List of computers to create and start the perfmon log.
'//  <ServerType>           Text description to be added to the log name.
'//  <CounterListFilePath>  File containing the list of perfmon counters.
'// 
'// Written by: Clint Huffman (clinth@microsoft.com
'// Last Modified: 3/6/2007
'// 

Const PERFMON_COLLECTION_INTERVAL = "00:01:00" ' This must be in HH:MM:SS format.
' The following DURATION constants determine when to stop the perfmon log in relation to the current time. For example, "d" and 1 mean to stop the log after one day.
Const DURATION_OF_COLLECTION_INTERVAL = "d" ' Interval to collect data. See the "interval" argument of the DateAdd() VBScript function.
Const DURATION_OF_COLLECTION_NUMBER = 1 ' Numeric express that is the number of internal you want to collect data. See the "number" argument of the DateAdd() VBScript function.
' XML Constants
Const OUTPUT_XML_FILE = "output.xml"
' General Constants
Const PERFMON_LOG_FOLDER = "C:\Perflogs"
' Global variables
Dim g_aComputers
Dim g_sCounterFilePath
Dim g_sServerType

Main

Sub Main()
    ProcessArguments
    CreatePerfmonLogs
End Sub

Sub CreatePerfmonLogs
    Set oXMLDoc = OpenOrCreateOutputXML(OUTPUT_XML_FILE)
    Set oXMLRoot = oXMLDoc.documentElement
    Set oFSO = CreateObject("Scripting.FileSystemObject")
    
    For i = 0 to UBound(g_aComputers)
        sComputer = g_aComputers(i)
        sRemotePerfmonLogFolder = "\\" & sComputer & "\" & Replace(PERFMON_LOG_FOLDER, ":", "$")
        bGoodFolderPath = False
        ' Create the remote perfmon log folder. An error will occur if it already exists.
        ON ERROR RESUME NEXT
        Set oFolder = oFSO.CreateFolder(sRemotePerfmonLogFolder)
        If oFolder.Path <> "" Then
            ' The folder path is good.
            bGoodFolderPath = True
        Else
            WScript.Echo "Unable to create the folder " & chr(34) & sRemotePerfmonLogFolder & chr(34)
        End If
        ON ERROR GOTO 0
        
        If bGoodFolderPath = True Then
            If Instr(1, Now(), "M", 1) > 0 Then
                sStart = RemoveSecondSpaceFromTime(Now())
                sEnd = RemoveSecondSpaceFromTime(DateAdd(DURATION_OF_COLLECTION_INTERVAL, DURATION_OF_COLLECTION_NUMBER, sStart))
            Else
                sStart = Now()
                sEnd = DateAdd(DURATION_OF_COLLECTION_INTERVAL, DURATION_OF_COLLECTION_NUMBER, sStart)
            End If
            sPerfmonLogName = "HealthCheck_" & g_sServerType & "_" & sComputer
            sPerfmonLogFolder = PERFMON_LOG_FOLDER
            
            sPerfmonLogFilePath = "c:\perflogs\HealthCheck_" & g_sServerType & "_" & sComputer & ".blg"
            sCommand = "logman create counter " & sPerfmonLogName & " -s " & sComputer & " -o " & sPerfmonLogFilePath & " -f bin -v nnnnnn -cf " & g_sCounterFilePath & " -si " & PERFMON_COLLECTION_INTERVAL & " -b " & sStart & " -e " & sEnd
            WScript.echo sCommand
            Set objShell = CreateObject("WScript.Shell")
            Set objExecObject = objShell.Exec(sCommand)
            sStdOut = objExecObject.StdOut.ReadAll()
            WScript.Echo sStdOut
            ' Write the result to the output xml file.
            If Instr(1, sStdOut, "command completed successfully", 1) > 0 Then
                Set oXMLServerNode = LocateOrCreateServerNode(oXMLDoc, sComputer)
                Set newClassNode = oXMLDoc.createNode(1, "RESULT", "")
                newClassNode.SetAttribute "Name", "CreateAndStartPerfmonLogs.vbs"
                newClassNode.SetAttribute "PerfmonLogName", sPerfmonLogName
                newClassNode.SetAttribute "PerfmonLogFilePath", sPerfmonLogFilePath
                newClassNode.SetAttribute "CounterFilePath", g_sCounterFilePath
                newClassNode.SetAttribute "CollectionIntervalInHoursMinutesDaysSeconds", PERFMON_COLLECTION_INTERVAL
                newClassNode.SetAttribute "StartTime", sStart
                newClassNode.SetAttribute "EndTime", sEnd
                newClassNode.SetAttribute "CreationTime", sStart
                newClassNode.SetAttribute "Result", sStdOut
                oXMLServerNode.appendChild newClassNode
                oXMLDoc.Save OUTPUT_XML_FILE
            Else
                Set oXMLServerNode = LocateOrCreateServerNode(oXMLDoc, sComputer)
                Set newClassNode = oXMLDoc.createNode(1, "RESULT", "")
                newClassNode.SetAttribute "Name", "CreateAndStartPerfmonLogs.vbs"
                newClassNode.SetAttribute "PerfmonLogName", sPerfmonLogName
                newClassNode.SetAttribute "PerfmonLogFilePath", sPerfmonLogFilePath
                newClassNode.SetAttribute "CounterFilePath", g_sCounterFilePath
                newClassNode.SetAttribute "CollectionIntervalInHoursMinutesDaysSeconds", PERFMON_COLLECTION_INTERVAL
                newClassNode.SetAttribute "StartTime", ""
                newClassNode.SetAttribute "EndTime", ""
                newClassNode.SetAttribute "CreationTime", sStart
                newClassNode.SetAttribute "Result", sStdOut
                oXMLServerNode.appendChild newClassNode
                oXMLDoc.Save OUTPUT_XML_FILE
            End If
        End If
    Next
End Sub

Sub ProcessArguments()
    sSyntax = "" &_
    "CScript CreateAndStartPerfmonLogs.vbs <computer[;computer]> <ServerType> <CounterListFilePath>" & vbNewLine &_
    " <computer[;computer]>  List of computers to create and start the perfmon log." & vbNewLine &_
    " <ServerType>           Text description to be added to the log name." & vbNewLine &_
    " <CounterListFilePath>  File containing the list of perfmon counters." & vbNewLine    
    
    Set oArgs = WScript.Arguments
    
    SELECT CASE oArgs.Count
        CASE 3
            sComputers = oArgs(0)
            g_sServerType = oArgs(1)
            g_sCounterFilePath = oArgs(2)
        CASE Else
            WScript.Echo sSyntax
            WScript.Quit
    END SELECT
    g_aComputers = Split(sComputers, ";")
End Sub

Function RemoveSecondSpaceFromTime(sTime)
    Dim sNewTime, iFirstSpace, iSecondSpace
    Dim sNewTimeLeft, sNewTimeRight    
    sNewTime = CStr(sTime)
    iFirstSpace = Instr(1, sNewTime, " ")
    iSecondSpace = Instr(iFirstSpace + 1, sNewTime, " ")
    sNewTimeLeft = Mid(sNewTime, 1, iSecondSpace - 1)
    sNewTimeRight = Right(sNewTime, 2)
    sNewTime = sNewTimeLeft & sNewTimeRight
    RemoveSecondSpaceFromTime = sNewTime
End Function

Function OpenOrCreateOutputXML(sOutputXMLFile)
    Set oFSO = CreateObject("Scripting.FileSystemObject")
    Set xmldoc = CreateObject("Msxml2.DOMDocument")
    xmldoc.async = False
    
    ' Check to see if the XML output file exists yet. If so, open it. Otherwise, create a new one.
    If oFSO.FileExists(sOutputXMLFile) Then
        xmldoc.load sOutputXMLFile
    Else
        xmldoc.loadXML "<HealthCheck></HealthCheck>"
        Set XMLRoot = xmldoc.documentElement
        XMLRoot.SetAttribute "CreationDate", Now()
        xmldoc.save sOutputXMLFile
    End If
    Set OpenOrCreateOutputXML = xmldoc
End Function

Function LocateOrCreateServerNode(oXMLDoc, sServerName)
    ' Locates or creates the respective server node in the output xml document.
    Set oXMLRoot = oXMLDoc.documentElement
    Set oNodes = oXMLRoot.SelectNodes("//SERVER")
    bFound = False
    For Each oNode in oNodes
        sNodeName = oNode.GetAttribute("Name")
        If LCase(sNodeName) = LCase(sServerName) Then
            bFound = True
            Set LocateOrCreateServerNode = oNode
        End If
    Next    
    If bFound = False Then
        Set newProcessNode = oXMLDoc.createNode(1, "SERVER", "")
        newProcessNode.SetAttribute "Name", sServerName
        oXMLRoot.appendChild newProcessNode
        oXMLDoc.save OUTPUT_XML_FILE
        
        Set oNodes = oXMLRoot.SelectNodes("//SERVER")
        bFound = False
        For Each oNode in oNodes
            sNodeServerName = oNode.GetAttribute("Name")
            If LCase(sNodeServerName) = LCase(sServerName) Then
                bFound = True
                Set LocateOrCreateServerNode = oNode
                oXmlDoc.save OUTPUT_XML_FILE
            End If
        Next
    End If    
End Function