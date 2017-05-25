'// MovePerfmonLogs.vbs
'// Purpose: This script is designed for use in Health Checks.
'//  It copies existing perfmon logs from multiple servers to a central location.
'//
'// Syntax:
'// CScript MovePerfmonLogs.vbs <computer[;computer]> <PerfmonLogNamePrefix> <DestinationFolder>
'//  <computer[;computer]>  List of computers to create and start the perfmon log.
'//  <PerfmonLogNamePrefix> Perfmon log prefix. The computer name is added to the end.
'//  <DestinationFolder>    Destination folder where to copy perfmon logs to.
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
Const PERFMON_LOG_FOLDER = "C$\Perflogs"
' Global variables
Dim g_aComputers
Dim g_sPerfmonLogPrefix
Dim g_DestinationFolder

Main

Sub Main()
    ProcessArguments
    MovePerfmonLogs
End Sub

Sub MovePerfmonLogs()
    Set oXMLDoc = OpenOrCreateOutputXML(OUTPUT_XML_FILE)
    Set oXMLRoot = oXMLDoc.documentElement
    Set oFSO = CreateObject("Scripting.FileSystemObject")
    
    For i = 0 to UBound(g_aComputers)
        sComputer = g_aComputers(i)
        sPerfmonLogFiles = "\\" & sComputer & "\" & PERFMON_LOG_FOLDER & "\" & g_sPerfmonLogPrefix & "*"
        oFSO.MoveFile sPerfmonLogFiles, g_DestinationFolder
        
        sPerfmonLogName = g_sPerfmonLogPrefix & sComputer
        
        ' Write the result to the output xml file.
            Set oXMLServerNode = LocateOrCreateServerNode(oXMLDoc, sComputer)
            Set newClassNode = oXMLDoc.createNode(1, "RESULT", "")
            newClassNode.SetAttribute "Name", "MovePerfmonLogs.vbs"
            newClassNode.SetAttribute "ExecutionTime", Now()
            newClassNode.SetAttribute "PerfmonLogName", sPerfmonLogName
            newClassNode.SetAttribute "Result", "N/A"
            oXMLServerNode.appendChild newClassNode
            oXMLDoc.Save OUTPUT_XML_FILE
    Next
End Sub

Sub ProcessArguments()
    sSyntax = "" &_
    "CScript MovePerfmonLogs.vbs <computer[;computer]> <PerfmonLogNamePrefix> <DestinationFolder>" & vbNewLine &_
    " <computer[;computer]>  List of computers to create and start the perfmon log." & vbNewLine &_
    " <PerfmonLogNamePrefix> Perfmon log prefix. The computer name is added to the end." & vbNewLine & _
    " <DestinationFolder>    Destination folder where to copy perfmon logs to." & vbNewLine
    
    Set oArgs = WScript.Arguments
    
    SELECT CASE oArgs.Count
        CASE 3
            sComputers = oArgs(0)
            g_sPerfmonLogPrefix = oArgs(1)
            g_DestinationFolder = oArgs(2)
        CASE Else
            WScript.Echo sSyntax
            WScript.Quit
    END SELECT
    g_aComputers = Split(sComputers, ";")
End Sub

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