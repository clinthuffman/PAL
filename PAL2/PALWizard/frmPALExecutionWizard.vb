Imports System.Xml
Imports System.IO
Imports System.Collections.ObjectModel
Imports PALFunctions.PALFunctions

Public Class frmPALExecutionWizard
    Public CounterLogPaths, ThresholdFilePath, AnalysisInterval As String
    Public IsOutputHTML, IsOutputXML, AllCounterStats, IsLowPriority As Boolean
    Public iNumberOfThreads As Integer
    Public PALScriptInstallDirectory, PALBatchFile As String
    Dim dctQuestionCollection As New SortedDictionary(Of String, PALFunctions.QuestionObject)
    Dim dctThresholdFileInheritanceHistory As New Dictionary(Of String, String)
    Dim dctAnalysisCollection As New Dictionary(Of String, PALFunctions.AnalysisCollectionObject)
    Dim bInitialFormLoad, bLowPriorityExecution As Boolean
    Dim SelectedThresholdFile As String
    Dim sSelectedAnalysisCollectionKey As String
    Dim sSelectedQuestion As String
    Dim BatchFileText As String
    Dim oCommandsQueue As New PALFunctions.PALCommandQueueObject
    Const DEFAULT_OUTPUT_DIRECTORY As String = "[My Documents]\PAL Reports"
    Dim iUB As Integer = -1 'Upper bound of oCommandQueue.aCommandQueues 


    Private Function AutoDetectMatchCounterObjectsInLogToThresholdFiles(ByRef ListOfCounterObjectsFromCounterLog As List(Of String))
        Dim sArgs As String
        Dim sCmd As String
        Dim sLine, sListOfCounterObjectsFromCounterLogFilePath As String
        Dim sCounterObject, sSessionGuid, sTempFolder As String
        Dim ListOfThresholdFiles As New List(Of String)

        '// Write counter object list to a temp file.
        sSessionGuid = Guid.NewGuid().ToString
        sTempFolder = My.Application.GetEnvironmentVariable("TEMP")
        sListOfCounterObjectsFromCounterLogFilePath = sTempFolder & "\PalAutoDetectObjectsFromCounterLogFile_" & sSessionGuid & ".txt"
        Using writer As StreamWriter = New StreamWriter(sListOfCounterObjectsFromCounterLogFilePath, True)
            For Each sCounterObject In ListOfCounterObjectsFromCounterLog
                writer.WriteLine(sCounterObject)
            Next
            writer.Close()
        End Using

        Dim oCounterLogFile As New PALFunctions.PALCounterLogFileObject
        sCmd = "cmd"
        sArgs = " /C " & Chr(34) & "powershell -ExecutionPolicy ByPass -File .\AutoDetect.ps1 -InputFile " & sListOfCounterObjectsFromCounterLogFilePath
        Dim consoleApp As New Process
        With consoleApp
            .StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            .StartInfo.CreateNoWindow = True
            .StartInfo.UseShellExecute = False
            .StartInfo.RedirectStandardOutput = True
            .StartInfo.FileName = sCmd
            .StartInfo.Arguments = sArgs
            .Start()
        End With
        Do Until consoleApp.StandardOutput.EndOfStream = True
            sLine = consoleApp.StandardOutput.ReadLine
            ListOfThresholdFiles.Add(sLine)
        Loop

        ListOfThresholdFiles.Sort()
        Return ListOfThresholdFiles
    End Function


    Private Function GetCounterLogCounterList(ByVal sCounterLogFilePath) As List(Of String)
        Dim sArgs As String
        Dim sCmd As String
        Dim sLine As String
        Dim aLine As String()
        Dim sCounterObject As String
        Dim ListOfCounterObjects As New List(Of String)
        Dim iIndexOfParan As Integer

        Dim oCounterLogFile As New PALFunctions.PALCounterLogFileObject
        sCmd = "cmd"
        sArgs = " /C " & Chr(34) & "relog " & Chr(34) & sCounterLogFilePath & Chr(34) & " -q" & Chr(34)
        Dim consoleApp As New Process
        With consoleApp
            .StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            .StartInfo.CreateNoWindow = True
            .StartInfo.UseShellExecute = False
            .StartInfo.RedirectStandardOutput = True
            .StartInfo.FileName = sCmd
            .StartInfo.Arguments = sArgs
            .Start()
        End With
        Do Until consoleApp.StandardOutput.EndOfStream = True
            sLine = consoleApp.StandardOutput.ReadLine
            If sLine.IndexOf("\\") = 0 Then
                aLine = Split(sLine, "\")
                sCounterObject = aLine(3)
                If sCounterObject.Contains("(") = True Then
                    iIndexOfParan = sCounterObject.IndexOf("(")
                    sCounterObject = sCounterObject.Substring(0, iIndexOfParan)
                End If
                If ListOfCounterObjects.Contains(sCounterObject) = False Then
                    ListOfCounterObjects.Add(sCounterObject)
                End If
            End If
        Loop
        ListOfCounterObjects.Sort()
        Return ListOfCounterObjects
    End Function



    Private Function GetCounterLogInformation(ByVal sCounterLogFilePath) As PALFunctions.PALCounterLogFileObject
        Dim sArgs As String
        Dim sCmd As String
        Dim sLine As String
        Dim aLine As String()
        Dim u As Integer
        Dim oCounterLogFile As New PALFunctions.PALCounterLogFileObject
        sCmd = "cmd"
        sArgs = " /C " & Chr(34) & "relog " & Chr(34) & sCounterLogFilePath & Chr(34) & Chr(34)
        Dim consoleApp As New Process
        With consoleApp
            .StartInfo.WindowStyle = ProcessWindowStyle.Hidden
            .StartInfo.CreateNoWindow = True
            .StartInfo.UseShellExecute = False
            .StartInfo.RedirectStandardOutput = True
            .StartInfo.FileName = sCmd
            .StartInfo.Arguments = sArgs
            .Start()
        End With
        Do Until consoleApp.StandardOutput.EndOfStream = True
            sLine = consoleApp.StandardOutput.ReadLine
            If sLine.IndexOf("Begin:") >= 0 Then
                aLine = Split(sLine)
                u = aLine.GetUpperBound(0)
                oCounterLogFile.dBeginTime = aLine(u - 1) & " " & aLine(u)
            End If
            If sLine.IndexOf("End:") >= 0 Then
                aLine = Split(sLine)
                u = aLine.GetUpperBound(0)
                oCounterLogFile.dEndTime = aLine(u - 1) & " " & aLine(u)
            End If
            If sLine.IndexOf("Samples:") >= 0 Then
                aLine = Split(sLine)
                u = aLine.GetUpperBound(0)
                oCounterLogFile.iNumberOfSamples = aLine(u)
            End If
        Loop
        Return oCounterLogFile
    End Function

    Private Function IsFileFoundInDirectorySearch(ByVal sDirectoryPath As String, ByVal sFileName As String)
        Dim sFilePath, sFile As String
        Dim oFiles As ReadOnlyCollection(Of String)
        Dim aStrings As String()
        oFiles = My.Computer.FileSystem.GetFiles(sDirectoryPath, FileIO.SearchOption.SearchTopLevelOnly, sFileName)
        For Each sFilePath In oFiles
            aStrings = Split(sFilePath, "\")
            sFile = aStrings(aStrings.GetUpperBound(0))
            If LCase(sFile) = LCase(sFileName) Then
                Return True
            End If
        Next
        Return False
    End Function

    Private Function CheckForPalPs1(ByVal sDirectoryPath As String)
        Dim bFound As Boolean
        bFound = False
        bFound = IsFileFoundInDirectorySearch(sDirectoryPath, "pal.ps1")
        If bFound = False Then
            bFound = IsFileFoundInDirectorySearch("C:\Program Files\PAL", "pal.ps1")
        End If
        If bFound = False Then
            bFound = IsFileFoundInDirectorySearch("C:\Program Files (x86)\PAL", "pal.ps1")
        End If
        Return bFound
    End Function


    Private Sub frmPALExecutionWizard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim bFoundPalPs1 As Boolean
        bFoundPalPs1 = False
        Me.Text = "PAL Wizard " & Me.GetType.Assembly.GetName.Version.ToString()
        'PALScriptInstallDirectory = My.Computer.FileSystem.CurrentDirectory
        PALScriptInstallDirectory = Path.GetDirectoryName(Application.ExecutablePath)
        bFoundPalPs1 = CheckForPalPs1(PALScriptInstallDirectory)
        Do Until bFoundPalPs1 = True
            PALScriptInstallDirectory = InputBox("Please provide the location of the PAL.ps1 script")
            bFoundPalPs1 = CheckForPalPs1(PALScriptInstallDirectory)
        Loop
        iNumberOfThreads = GetNumberOfLogicalProcessors()
        TextBoxExecNumberOfThreads.Text = iNumberOfThreads
        AddNewToCommandQueue(True)
        ThresholdFilePageRefresh()
        OutputPageRefresh()
        BuildQueueCommand()
        TabControl.SelectTab(TabPageWelcome)
        IsOutputHTML = True
        IsOutputXML = False
        AllCounterStats = False
        IsLowPriority = True
        LinkLabelURL.Links.Remove(LinkLabelURL.Links(0))
        LinkLabelURL.Links.Add(0, LinkLabelURL.Text.Length, "https://github.com/clinthuffman/PAL")
        LinkLabelLicense.Links.Remove(LinkLabelLicense.Links(0))
        LinkLabelLicense.Links.Add(0, LinkLabelLicense.Text.Length, "https://github.com/clinthuffman/PAL/blob/master/LICENSE")
        LinkLabelEmailClint.Links.Remove(LinkLabelEmailClint.Links(0))
        LinkLabelEmailClint.Links.Add(0, LinkLabelEmailClint.Text.Length, "mailto:clinth@microsoft.com")
        LinkLabelClinthBlog.Links.Remove(LinkLabelClinthBlog.Links(0))
        LinkLabelClinthBlog.Links.Add(0, LinkLabelClinthBlog.Text.Length, "https://blogs.technet.microsoft.com/clinth/")
        LinkLabelAboutTheAuthorClintH.Links.Remove(LinkLabelAboutTheAuthorClintH.Links(0))
        LinkLabelAboutTheAuthorClintH.Links.Add(0, LinkLabelAboutTheAuthorClintH.Text.Length, "https://blogs.technet.microsoft.com/clinth/2009/12/03/about-the-author-clint-huffman/")
        LinkLabelSupport.Links.Remove(LinkLabelSupport.Links(0))
        LinkLabelSupport.Links.Add(0, LinkLabelSupport.Text.Length, "https://github.com/clinthuffman/PAL/issues")
        bLowPriorityExecution = True
    End Sub

    Function GetNumberOfLogicalProcessors() As String
        Return System.Environment.ExpandEnvironmentVariables("%NUMBER_OF_PROCESSORS%")
    End Function

    Sub RefreshQueuePage()
        Dim i As Integer
        Dim sText As String = ""

        i = oCommandsQueue.aCommandQueue.GetUpperBound(0)
        oCommandsQueue.aCommandQueue(i).CounterLogPaths = CounterLogPaths
        oCommandsQueue.aCommandQueue(i).ThresholdFilePath = ThresholdFilePath
        oCommandsQueue.aCommandQueue(i).IsOutputHTML = IsOutputHTML
        oCommandsQueue.aCommandQueue(i).IsOutputXML = IsOutputXML
        oCommandsQueue.aCommandQueue(i).AllCounterStats = AllCounterStats
        oCommandsQueue.aCommandQueue(i).AnalysisInterval = AnalysisInterval
        oCommandsQueue.aCommandQueue(i).NumberOfThreads = iNumberOfThreads
        oCommandsQueue.aCommandQueue(i).IsLowPriority = IsLowPriority
        Dim dctTempQuestionCollection As New SortedDictionary(Of String, PALFunctions.QuestionObject)

        For Each sKey In dctQuestionCollection.Keys
            Dim oNewQuestion As New PALFunctions.QuestionObject
            oNewQuestion.bAnswer = dctQuestionCollection(sKey).bAnswer
            oNewQuestion.DefaultValue = dctQuestionCollection(sKey).DefaultValue
            oNewQuestion.Question = dctQuestionCollection(sKey).Question
            oNewQuestion.QuestionDataType = dctQuestionCollection(sKey).QuestionDataType
            oNewQuestion.QuestionVarName = dctQuestionCollection(sKey).QuestionVarName
            oNewQuestion.sAnswer = dctQuestionCollection(sKey).sAnswer
            oNewQuestion.Options = dctQuestionCollection(sKey).Options
            dctTempQuestionCollection.Add(sKey, oNewQuestion)
            sText = sText & sKey & ": " & dctQuestionCollection(sKey).sAnswer & vbNewLine
        Next
        oCommandsQueue.aCommandQueue(i).dctQuestionCollection = dctTempQuestionCollection
        BatchFileText = GenerateTextForCommandQueueTextBox(oCommandsQueue)
        txtBatchText.Text = BatchFileText
        txtQuestionAnswerResults.Text = sText

    End Sub

    Private Function AddDoubleQuotesOrNot(ByVal sText) As String
        Dim DQ As String
        DQ = Chr(34) ' double quote
        If InStr(sText, Chr(34)) > 0 Then
            Return sText
        Else
            Return DQ & sText & DQ
        End If
    End Function

    Private Sub btnFileBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileBrowse.Click
        OpenLogFile()
    End Sub

    Sub OpenLogFile()
        Dim sDir As String
        Dim aFileParts() As String
        Dim i As Integer
        OpenFileDialog1.Filter = "Log files (*.blg, *.csv, *.tsv)|*.blg;*.csv;*.tsv|All Files (*.*)|*.*"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            ComboBoxRunLogFile.Text = Join(OpenFileDialog1.FileNames, ";")
        End If
        aFileParts = Split(OpenFileDialog1.FileNames(0), "\")
        sDir = "."
        For i = 0 To UBound(aFileParts)
            If i = 0 Then
                sDir = aFileParts(i)
            Else
                If i < UBound(aFileParts) Then
                    sDir = sDir & "\" & aFileParts(i)
                End If
            End If
        Next
        ReadBlgFilesIntoMemory(sDir)
    End Sub

    Sub ReadBlgFilesIntoMemory(ByVal sDir)
        Dim sFile As String
        Dim oFiles As ReadOnlyCollection(Of String)
        If sDir = "" Then
            sDir = "."
        End If
        ComboBoxRunLogFile.Items.Clear()
        oFiles = My.Computer.FileSystem.GetFiles(sDir, FileIO.SearchOption.SearchTopLevelOnly, "*.blg")
        For Each sFile In oFiles
            ComboBoxRunLogFile.Items.Add(sFile)
        Next
    End Sub

    Sub ReadAnalysisCollectionFilesIntoMemory()
        Dim sFile As String
        Dim sFileContents As String
        Dim oXmlRoot As XmlNode
        Dim oFiles As ReadOnlyCollection(Of String)
        Dim sKey, sText As String

        dctQuestionCollection.Clear()
        oFiles = My.Computer.FileSystem.GetFiles(".", FileIO.SearchOption.SearchTopLevelOnly, "*.xml")
        For Each sFile In oFiles
            sFileContents = My.Computer.FileSystem.ReadAllText(sFile)
            Dim oXmlDoc As New XmlDocument
            oXmlDoc.Load(sFile)
            oXmlRoot = oXmlDoc.DocumentElement
            Dim oAnalysisCollection As New PALFunctions.AnalysisCollectionObject
            Try
                If oXmlRoot.Attributes("NAME").Value <> "" Then
                    oAnalysisCollection.Name = oXmlRoot.Attributes("NAME").Value
                    oAnalysisCollection.Version = oXmlRoot.Attributes("VERSION").Value
                    oAnalysisCollection.Description = oXmlRoot.Attributes("DESCRIPTION").Value
                    oAnalysisCollection.ContentOwners = oXmlRoot.Attributes("CONTENTOWNERS").Value
                    oAnalysisCollection.FeedbackEmailAddresses = oXmlRoot.Attributes("FEEDBACKEMAILADDRESS").Value
                    oAnalysisCollection.FilePath = sFile
                    oAnalysisCollection.XmlRoot = oXmlRoot
                    sKey = oAnalysisCollection.Name
                    '// Check for duplicates
                    If dctAnalysisCollection.ContainsKey(sKey) = False Then
                        dctAnalysisCollection.Add(sKey, oAnalysisCollection)
                    Else
                        sText = "Two or more threshold files contain the same title. Only one of them will be listed." & vbNewLine & _
                        "The following threshold files are in conflict: " & vbNewLine & _
                        vbNewLine & _
                        "Threshold File Title: " & dctAnalysisCollection(sKey).Name & vbNewLine & _
                        "Threshold File Version: " & dctAnalysisCollection(sKey).Version & vbNewLine & _
                        "Threshold Fiel Path: " & dctAnalysisCollection(sKey).FilePath & vbNewLine & _
                        vbNewLine & _
                        "Threshold File Title: " & oAnalysisCollection.Name & vbNewLine & _
                        "Threshold File Version: " & oAnalysisCollection.Version & vbNewLine & _
                        "Threshold Fiel Path: " & oAnalysisCollection.FilePath
                        MsgBox(sText, MsgBoxStyle.Information, "Threshold File Conflict")
                    End If
                End If
            Catch ex As Exception

            End Try
        Next
    End Sub

    Sub ThresholdFilePageRefresh()
        ReadAnalysisCollectionFilesIntoMemory()
        ComboBoxAnalysisCollection.Items.Clear()
        For Each sKey In dctAnalysisCollection.Keys
            ComboBoxAnalysisCollection.Items.Add(sKey)
        Next
        For Each oItem In ComboBoxAnalysisCollection.Items
            If oItem = "System Overview" Then
                ComboBoxAnalysisCollection.SelectedItem = "System Overview"
                Exit For
            End If
        Next
    End Sub

    Sub RefreshCounterLogPage()
        ReadBlgFilesIntoMemory(".")
    End Sub

    Private Sub TabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabControl.SelectedIndexChanged
        Select Case TabControl.SelectedTab.Text
            Case "Counter Log"
                RefreshCounterLogPage()
            Case "Threshold File"
                ThresholdFilePageFocus()
            Case "Questions"
                RefreshQueuePage()
            Case "Output Options"
                RefreshOutputOptionsPage()
            Case "Queue"
                RefreshQueuePage()
            Case "Execute"
        End Select
    End Sub

    Private Sub ThresholdFilePageFocus()

    End Sub

    Private Sub ComboBoxAnalysisCollection_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxAnalysisCollection.SelectedIndexChanged
        sSelectedAnalysisCollectionKey = ComboBoxAnalysisCollection.SelectedItem
        RefreshAnalysisCollectionSection(False)
        lblThresholdFileInheritanceNote.Text = ""
    End Sub

    Sub ProcessThresholdFileInheritance(ByVal sFilePath As String)
        '// Inherit questions from other threshold files if specified in the threshold file.
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot As XmlNode
        Dim sPalThresholdFilePath As String
        If System.IO.File.Exists(sFilePath) = True Then
            oXmlDoc.Load(sFilePath)
            oXmlRoot = oXmlDoc.DocumentElement
            If dctThresholdFileInheritanceHistory.ContainsKey(sFilePath) = False Then
                dctThresholdFileInheritanceHistory.Add(sFilePath, sFilePath)
            End If
            For Each oXmlNode In oXmlRoot.SelectNodes("//INHERITANCE")
                sPalThresholdFilePath = PALScriptInstallDirectory & "\" & oXmlNode.Attributes("FILEPATH").Value
                If dctThresholdFileInheritanceHistory.ContainsKey(sPalThresholdFilePath) = False Then
                    dctThresholdFileInheritanceHistory.Add(sPalThresholdFilePath, sFilePath)
                End If
            Next

            For Each oXmlNode In oXmlRoot.SelectNodes("//INHERITANCE")
                sPalThresholdFilePath = PALScriptInstallDirectory & "\" & oXmlNode.Attributes("FILEPATH").Value
                ProcessThresholdFileInheritanceRecursion(sPalThresholdFilePath)
            Next
        End If
    End Sub

    Sub ProcessThresholdFileInheritanceRecursion(ByVal sFilePath As String)
        '// Inherit questions from other threshold files if specified in the threshold file.
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot As XmlNode
        Dim sPalThresholdFilePath As String
        If System.IO.File.Exists(sFilePath) = True Then
            oXmlDoc.Load(sFilePath)
            oXmlRoot = oXmlDoc.DocumentElement
            'If dctThresholdFileInheritanceHistory.ContainsKey(sFilePath) = False Then
            '    dctThresholdFileInheritanceHistory.Add(sFilePath, "")
            'End If
            For Each oXmlNode In oXmlRoot.SelectNodes("//INHERITANCE")
                sPalThresholdFilePath = PALScriptInstallDirectory & "\" & oXmlNode.Attributes("FILEPATH").Value
                If dctThresholdFileInheritanceHistory.ContainsKey(sPalThresholdFilePath) = False Then
                    dctThresholdFileInheritanceHistory.Add(sPalThresholdFilePath, sFilePath)
                    ProcessThresholdFileInheritanceRecursion(sPalThresholdFilePath)
                End If
            Next
        End If
    End Sub

    Sub RefreshAnalysisCollectionSection(ByVal bForce As Boolean)
        Dim oXmlNode As XmlNode
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot As XmlNode
        Dim sKey, sPalThresholdFilePath, sText As String
        Dim bFound As Boolean

        txtQuestion.Text = ""
        txtQuestion.Enabled = False
        txtQuestionAnswer.Text = ""
        txtQuestionAnswer.Enabled = False
        ComboBoxQuestionAnswer.Text = ""
        ComboBoxQuestionAnswer.Enabled = False

        sText = ""

        If String.IsNullOrEmpty(sSelectedAnalysisCollectionKey) = True Then
            MsgBox("No threshold file found or no threshold files are located in the local directory. Try placing or creating new PAL v1.1 threshold files in the local directory and try again.")
            Exit Sub
        End If

        '// Check to see if the Analysis Collection selection has really changed or not unless forced to refresh.
        If bForce = False Then
            If ThresholdFilePath = dctAnalysisCollection(sSelectedAnalysisCollectionKey).FilePath Then
                Exit Sub
            End If
        End If
        ThresholdFilePath = dctAnalysisCollection(sSelectedAnalysisCollectionKey).FilePath
        txtThresholdFileName.Text = ExtractFileNameFromFilePath(ThresholdFilePath)
        txtAnalysisCollectionDescription.Text = dctAnalysisCollection(sSelectedAnalysisCollectionKey).Description
        txtThresholdFileContentOwners.Text = dctAnalysisCollection(sSelectedAnalysisCollectionKey).ContentOwners & " (" & dctAnalysisCollection(sSelectedAnalysisCollectionKey).FeedbackEmailAddresses & ")"

        dctThresholdFileInheritanceHistory.Clear()
        ProcessThresholdFileInheritance(ThresholdFilePath)

        ListBoxQuestions.Items.Clear()
        dctQuestionCollection.Clear()
        ListBoxOfThresholdFileInheritance.Items.Clear()
        ListBoxOfThresholdFileInheritanceRecursion.Items.Clear()

        Dim aKeys
        Dim sExt As String
        '// Add the threshold file inheritance that is immediately in this threshold file.
        Dim sPalThresholdFileName, sInheritedFromFileName, sThresholdFileName As String
        sThresholdFileName = GetFileNameFromPath(ThresholdFilePath)
        aKeys = dctThresholdFileInheritanceHistory.Keys

        '// Remove blank entries and non-files
        For Each sPalThresholdFilePath In dctThresholdFileInheritanceHistory.Keys
            sExt = LCase(Mid(sPalThresholdFilePath, Len(sPalThresholdFilePath) - 3))
            If sExt = ".xml" And System.IO.File.Exists(sPalThresholdFilePath) = True Then
                sPalThresholdFileName = GetFileNameFromPath(sPalThresholdFilePath)

                'sKey = PALScriptInstallDirectory & "\" & sPalThresholdFileName
                'sKey = dctThresholdFileInheritanceHistory(sPalThresholdFilePath)

                sInheritedFromFileName = GetFileNameFromPath(dctThresholdFileInheritanceHistory(sPalThresholdFilePath))
                If sInheritedFromFileName = sThresholdFileName And sThresholdFileName <> sPalThresholdFileName Then
                    ListBoxOfThresholdFileInheritance.Items.Add(sPalThresholdFileName)
                End If
            End If
        Next

        '// Add the threshold file inheritance that is recursively inherited.
        For Each sPalThresholdFilePath In dctThresholdFileInheritanceHistory.Keys
            sExt = LCase(Mid(sPalThresholdFilePath, Len(sPalThresholdFilePath) - 3))
            If sExt = ".xml" And System.IO.File.Exists(sPalThresholdFilePath) = True Then
                sPalThresholdFileName = GetFileNameFromPath(sPalThresholdFilePath)
                'sKey = PALScriptInstallDirectory & "\" & sPalThresholdFileName
                sInheritedFromFileName = GetFileNameFromPath(dctThresholdFileInheritanceHistory(sPalThresholdFilePath))
                If sInheritedFromFileName <> sThresholdFileName And sThresholdFileName <> sPalThresholdFileName Then
                    ListBoxOfThresholdFileInheritanceRecursion.Items.Add(sPalThresholdFileName)
                End If
            End If
        Next

        For Each sPalThresholdFilePath In dctThresholdFileInheritanceHistory.Keys
            If System.IO.File.Exists(sPalThresholdFilePath) = True Then
                oXmlDoc.Load(sPalThresholdFilePath)
                oXmlRoot = oXmlDoc.DocumentElement

                For Each oXmlNode In oXmlRoot.SelectNodes("//QUESTION")
                    bFound = False
                    For Each sKey In ListBoxQuestions.Items
                        If oXmlNode.Attributes("QUESTIONVARNAME").Value = sKey Then
                            bFound = True
                            Exit For
                        End If
                    Next
                    If bFound = False Then
                        ListBoxQuestions.Items.Add(oXmlNode.Attributes("QUESTIONVARNAME").Value)
                        Dim oQuestion As New PALFunctions.QuestionObject
                        oQuestion.QuestionVarName = oXmlNode.Attributes("QUESTIONVARNAME").Value
                        oQuestion.QuestionDataType = oXmlNode.Attributes("DATATYPE").Value
                        oQuestion.DefaultValue = oXmlNode.Attributes("DEFAULTVALUE").Value
                        Try
                            oQuestion.Options = oXmlNode.Attributes("OPTIONS").Value
                        Catch ex As Exception

                        End Try
                        oQuestion.Question = oXmlNode.InnerText
                        If oQuestion.QuestionDataType = "boolean" Then
                            If oQuestion.DefaultValue = "True" Then
                                oQuestion.bAnswer = True
                                oQuestion.sAnswer = "True"
                            Else
                                oQuestion.bAnswer = False
                                oQuestion.sAnswer = "False"
                            End If
                        Else
                            oQuestion.sAnswer = oQuestion.DefaultValue
                        End If
                        dctQuestionCollection.Add(oQuestion.QuestionVarName, oQuestion)
                        sText = sText & oQuestion.QuestionVarName & ": " & oQuestion.sAnswer & vbNewLine
                        If ListBoxQuestions.SelectedItem = Nothing Then
                            ListBoxQuestions.SelectedItem = oXmlNode.Attributes("QUESTIONVARNAME").Value
                        End If
                    End If
                Next
            End If
        Next
        txtQuestionAnswerResults.Text = sText
    End Sub

    Function ExtractFileNameFromFilePath(ByVal sFilePath) As String
        Dim aString() As String
        Dim sFileName As String
        aString = Split(sFilePath, "\")
        sFileName = aString(aString.GetUpperBound(0))
        Return sFileName
    End Function

    Private Sub btnOutputDirectoryBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutputDirectoryBrowse.Click
        BrowseForOutputDirectory()
    End Sub

    Sub BrowseForOutputDirectory()
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK And iUB >= 0 Then
            oCommandsQueue.aCommandQueue(iUB).OutputDirectory = FolderBrowserDialog1.SelectedPath
            txtOutputDirectoryPath.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory
        End If
    End Sub

    Sub RefreshOutputOptionsPage()
        If iUB >= 0 Then
            txtOutputDirectoryPath.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory
        End If
    End Sub

    Private Sub txtOutputDirectoryPath_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOutputDirectoryPath.TextChanged
        If iUB >= 0 Then
            oCommandsQueue.aCommandQueue(iUB).OutputDirectory = txtOutputDirectoryPath.Text
            txtFullHTMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).HTMLOutputFileName
            txtFullXMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).XMLOutputFileName
        End If
    End Sub

    Sub ExecuteBatchFile(ByVal sBatchFilePath As String)
        Dim sArgs As String
        Dim sCmd As String

        sCmd = "cmd"
        sArgs = " /C " & Chr(34) & sBatchFilePath & Chr(34)
        Dim consoleApp As New Process
        With consoleApp
            .StartInfo.UseShellExecute = True
            .StartInfo.RedirectStandardOutput = False
            .StartInfo.FileName = sCmd
            .StartInfo.Arguments = sArgs
            .Start()
        End With

    End Sub

    Private Sub txtBatchText_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBatchText.TextChanged
        BatchFileText = txtBatchText.Text
    End Sub

    Public Function GenerateNewGUID()
        Return "{" & Guid.NewGuid().ToString & "}"
    End Function

    Function IsCounterLogDataValid()
        If ComboBoxRunLogFile.Text = "" Then
            MsgBox("Please specify a valid counter log path.")
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub ComboBoxRunLogFile_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxRunLogFile.TextChanged
        Dim i As Integer
        CounterLogPaths = ComboBoxRunLogFile.Text
        i = oCommandsQueue.aCommandQueue.GetUpperBound(0)
        oCommandsQueue.aCommandQueue(i).CounterLogPaths = CounterLogPaths

        If CheckBoxRestrictToADateTimeRange.Checked = True Then
            UpdateCheckBoxRestrictToDateTimeRange()
        End If
    End Sub

    Private Sub ComboBoxInterval_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ComboBoxInterval.TextChanged
        AnalysisInterval = ComboBoxInterval.Text
    End Sub

    Private Sub CheckBoxXMLOutput_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxXMLOutput.CheckedChanged
        If CheckBoxXMLOutput.Checked = True Then
            IsOutputXML = "True"
            lblXMLFileNameLabel.Enabled = True
            txtXMLOutputFileName.Enabled = True
            lblFullXMLOutputPathLabel.Enabled = True
            txtFullXMLOutputFileName.Enabled = True
        Else
            IsOutputXML = "False"
            lblXMLFileNameLabel.Enabled = False
            txtXMLOutputFileName.Enabled = False
            lblFullXMLOutputPathLabel.Enabled = False
            txtFullXMLOutputFileName.Enabled = False
        End If
        IsBothOutputOptionsUnchecked()
    End Sub

    Private Sub CheckBoxHTMLOutput_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckBoxHTMLOutput.CheckedChanged
        If CheckBoxHTMLOutput.Checked = True Then
            IsOutputHTML = "True"
            lblHTMLReportFileName.Enabled = True
            txtHTMLOutputFileName.Enabled = True
            lblFullHTMLOutputDirectoryLabel.Enabled = True
            txtFullHTMLOutputFileName.Enabled = True
        Else
            IsOutputHTML = "False"
            lblHTMLReportFileName.Enabled = False
            txtHTMLOutputFileName.Enabled = False
            lblFullHTMLOutputDirectoryLabel.Enabled = False
            txtFullHTMLOutputFileName.Enabled = False
        End If
        IsBothOutputOptionsUnchecked()
    End Sub

    Sub IsBothOutputOptionsUnchecked()
        If CheckBoxHTMLOutput.Checked = False And CheckBoxXMLOutput.Checked = False Then
            MsgBox("At least one output type must be checked.")
            CheckBoxHTMLOutput.Checked = True
        End If
    End Sub

    Private Sub btnEditThresholdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditThresholdFile.Click
        Dim ofrmPALEditor As New frmPALEditor
        If ComboBoxAnalysisCollection.Text <> "" Then
            ofrmPALEditor.sThresholdFilePath = ThresholdFilePath
        End If
        ofrmPALEditor.Text = "PAL Editor - " & ComboBoxAnalysisCollection.Text
        ofrmPALEditor.ofrmPALExecutionWizard = Me
        ofrmPALEditor.Show()
    End Sub

    Private Sub btnExportThresholdFileToPerfmonTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportThresholdFileToPerfmonTemplate.Click
        MsgBox("I highly recommend using the PAL Collector script (powershell) to create data collector sets instead of using templates. Go to http://aka.ms/PalCollector.", MsgBoxStyle.OkOnly, "PAL Collector Notification")
        ExportToThresholdFileToPerfmonTemplate()
    End Sub

    Private Sub LinkLabelURL_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelURL.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub LinkLabelLicense_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelLicense.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub CheckBoxAddMoreToThisQueue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If CheckBoxAddMoreToThisQueue.Checked = True Then
        '    btnNextOnPage4.Text = ""
        'End If
    End Sub

    Private Sub CheckBoxRunAsLowPriority_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRunAsLowPriority.CheckedChanged
        If CheckBoxRunAsLowPriority.Checked = True Then
            bLowPriorityExecution = True
            IsLowPriority = True
        Else
            bLowPriorityExecution = False
            IsLowPriority = False
        End If
        oCommandsQueue.bLowPriorityExecution = bLowPriorityExecution
    End Sub

    Private Sub btnNextOnPageExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageExecute.Click
        Dim sGUID As String
        Dim BATCH_FILE_NAME As String = "_RunPAL.bat"

        UpdateNumberOfThreads()
        RefreshQueuePage()

        Dim i As Integer
        i = oCommandsQueue.aCommandQueue.GetUpperBound(0)
        oCommandsQueue.aCommandQueue(i).NumberOfThreads = iNumberOfThreads

        If RadExecuteAndRunWizardAgain.Checked = True Then
            oCommandsQueue.SaveTextAsBatchFile(oCommandsQueue.sBatchFilePath, oCommandsQueue.BuildBatchFileText(oCommandsQueue), PALScriptInstallDirectory)
            ExecuteBatchFile(oCommandsQueue.sBatchFilePath)

            sGUID = GenerateNewGUID()
            oCommandsQueue.sBatchFilePath = oCommandsQueue.sWorkingDirectory & "\" & sGUID & BATCH_FILE_NAME

            AddNewToCommandQueue(True)
            TabControl.SelectTab(TabPageCounterLog)
        Else
            If RadAddMoreToQueue.Checked = True Then
                AddNewToCommandQueue(False)
                TabControl.SelectTab(TabPageCounterLog)
            Else
                oCommandsQueue.SaveTextAsBatchFile(oCommandsQueue.sBatchFilePath, oCommandsQueue.BuildBatchFileText(oCommandsQueue), PALScriptInstallDirectory)
                ExecuteBatchFile(oCommandsQueue.sBatchFilePath)
                sGUID = GenerateNewGUID()
                oCommandsQueue.sBatchFilePath = oCommandsQueue.sWorkingDirectory & "\" & sGUID & BATCH_FILE_NAME
                Me.Close()
            End If
        End If
    End Sub

    Sub AddNewToCommandQueue(ByVal bClear As Boolean)
        Dim a, b, c As Integer
        Dim aPALCommands() As PALFunctions.PALCommandObject
        Dim oPALCommand As New PALFunctions.PALCommandObject
        Dim sPreviousOutputDirectory, sPreviousThresholdFilePath, sPreviousAnalysisInterval As String

        If iUB >= 0 Then
            sPreviousOutputDirectory = oCommandsQueue.aCommandQueue(iUB).OutputDirectory
            sPreviousThresholdFilePath = oCommandsQueue.aCommandQueue(iUB).ThresholdFilePath
            sPreviousAnalysisInterval = oCommandsQueue.aCommandQueue(iUB).AnalysisInterval
        Else
            sPreviousOutputDirectory = ""
            sPreviousThresholdFilePath = ""
            sPreviousAnalysisInterval = ""
        End If

        If bClear = True Then
            ReDim aPALCommands(0)
            aPALCommands(0) = oPALCommand
            oCommandsQueue.aCommandQueue = aPALCommands
            iUB = 0
        Else
            a = oCommandsQueue.aCommandQueue.GetUpperBound(0)
            ReDim aPALCommands(a + 1)
            For b = 0 To a
                aPALCommands(b) = oCommandsQueue.aCommandQueue(b)
            Next
            c = aPALCommands.GetUpperBound(0)
            aPALCommands(c) = oPALCommand
            oCommandsQueue.aCommandQueue = aPALCommands
            iUB = c
        End If
        '// Set the new command queue item properties to previous settings.
        If sPreviousOutputDirectory <> "" Then
            oCommandsQueue.aCommandQueue(iUB).OutputDirectory = sPreviousOutputDirectory
        End If
        If sPreviousThresholdFilePath <> "" Then
            oCommandsQueue.aCommandQueue(iUB).ThresholdFilePath = sPreviousThresholdFilePath
        End If
        If sPreviousAnalysisInterval <> "" Then
            oCommandsQueue.aCommandQueue(iUB).AnalysisInterval = sPreviousAnalysisInterval
        End If
        oCommandsQueue.aCommandQueue(iUB).PALScriptInstallDirectory = PALScriptInstallDirectory
        IsCheckBoxRestrictToADateTimeRangeCheckedUnchecked()
    End Sub

    Sub RemoveLastFromCommandQueue()
        Dim a As Integer
        Dim aPALCommands() As PALFunctions.PALCommandObject
        Dim oPALCommand As New PALFunctions.PALCommandObject

        If iUB < 1 Then
            MsgBox("The last item in the queue cannot be removed, but can be edited by the rest of the wizard.")
            Exit Sub
        End If

        ReDim aPALCommands(iUB - 1)
        For a = 0 To iUB - 1
            aPALCommands(a) = oCommandsQueue.aCommandQueue(a)
        Next
        oCommandsQueue.aCommandQueue = aPALCommands
        iUB = oCommandsQueue.aCommandQueue.GetUpperBound(0)
    End Sub

    Sub BuildQueueCommand()
        BatchFileText = GenerateTextForCommandQueueTextBox(oCommandsQueue)
    End Sub

    Private Sub RadAddMoreToQueue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadAddMoreToQueue.CheckedChanged
        If RadAddMoreToQueue.Checked = True Then
            btnNextOnPageExecute.Text = "Add"
        Else
            btnNextOnPageExecute.Text = "Finish"
        End If
    End Sub

    Function GenerateTextForCommandQueueTextBox(ByVal oCommandQueue As PALFunctions.PALCommandQueueObject)
        Dim sText As String
        'Const sCommandDelimitor As String = "========COMMAND_DELIMITOR========"
        Dim oCommand As PALFunctions.PALCommandObject
        sText = ""
        For Each oCommand In oCommandQueue.aCommandQueue
            sText = sText & oCommand.BuildQueryStyleCommandLine() & vbNewLine & vbNewLine
        Next
        Return sText
    End Function

    Sub OutputPageRefresh()
        If iUB >= 0 Then
            txtHTMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).HTMLOutputFileName
            txtXMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).XMLOutputFileName
            txtFullHTMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).HTMLOutputFileName
            txtFullXMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).XMLOutputFileName
        End If
    End Sub

    Private Sub txtHTMLReportFileName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtHTMLOutputFileName.TextChanged
        If iUB >= 0 Then
            oCommandsQueue.aCommandQueue(iUB).HTMLOutputFileName = txtHTMLOutputFileName.Text
            txtFullHTMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).HTMLOutputFileName
        End If
    End Sub

    Private Sub txtXMLOutputFileName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtXMLOutputFileName.TextChanged
        If iUB >= 0 Then
            oCommandsQueue.aCommandQueue(iUB).XMLOutputFileName = txtXMLOutputFileName.Text
            txtFullXMLOutputFileName.Text = oCommandsQueue.aCommandQueue(iUB).OutputDirectory & "\" & oCommandsQueue.aCommandQueue(iUB).XMLOutputFileName
        End If
    End Sub

    Private Sub btnRemoveLastFromQueue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveLastFromQueue.Click
        RemoveLastFromCommandQueue()
        RefreshQueuePage()
    End Sub

    Private Sub UpdateCheckBoxRestrictToDateTimeRange()
        Dim oCounterLogFile As PALFunctions.PALCounterLogFileObject
        lblDateTimeRangeNote.Text = "Status: Please wait. Retrieving time range from counter log using Relog.exe from command line..."
        lblDateTimeRangeNote.Update()
        oCounterLogFile = GetCounterLogInformation(ComboBoxRunLogFile.Text)
        If oCounterLogFile.dBeginTime <> "#12:00:00 AM#" Then
            DateTimePickerBeginTime.Value = oCounterLogFile.dBeginTime
            DateTimePickerEndTime.Value = oCounterLogFile.dEndTime
            lblDateTimeRangeNote.Text = "Status: Done"
            lblDateTimeRangeNote.Update()
        Else
            lblDateTimeRangeNote.Text = "Status: An error occurred retrieving time range from counter log using Relog.exe from command line."
            lblDateTimeRangeNote.Update()
        End If
    End Sub

    Private Sub CheckBoxRestrictToADateTimeRange_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxRestrictToADateTimeRange.CheckedChanged
        If CheckBoxRestrictToADateTimeRange.Checked = True Then
            lblBeginTime.Enabled = True
            lblEndTime.Enabled = True
            DateTimePickerBeginTime.Enabled = True
            DateTimePickerEndTime.Enabled = True
            lblDateTimeRangeNote.Enabled = True
            UpdateCheckBoxRestrictToDateTimeRange()
        Else
            lblBeginTime.Enabled = False
            lblEndTime.Enabled = False
            DateTimePickerBeginTime.Enabled = False
            DateTimePickerEndTime.Enabled = False
            lblDateTimeRangeNote.Enabled = False
        End If
        IsCheckBoxRestrictToADateTimeRangeCheckedUnchecked()
    End Sub

    Sub IsCheckBoxRestrictToADateTimeRangeCheckedUnchecked()
        If CheckBoxRestrictToADateTimeRange.Checked = True Then '
            oCommandsQueue.aCommandQueue(iUB).TimeRangeRestriction = True
        Else
            oCommandsQueue.aCommandQueue(iUB).TimeRangeRestriction = False
        End If
    End Sub

    Private Sub DateTimePickerBeginTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerBeginTime.ValueChanged
        oCommandsQueue.aCommandQueue(iUB).BeginTime = DateTimePickerBeginTime.Value
    End Sub

    Private Sub DateTimePickerEndTime_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DateTimePickerEndTime.ValueChanged
        oCommandsQueue.aCommandQueue(iUB).EndTime = DateTimePickerEndTime.Value
    End Sub

    Private Sub btnNextOnPageWelcome_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageWelcome.Click
        TabControl.SelectTab(TabPageCounterLog)
    End Sub

    Private Sub btnPreviousOnPageCounterLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageCounterLog.Click
        TabControl.SelectTab(TabPageWelcome)
    End Sub

    Private Sub btnNextOnPageCounterLog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageCounterLog.Click
        TabControl.SelectTab(TabPageThresholdFile)
    End Sub

    Private Sub btnPreviousOnPageThresholdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageThresholdFile.Click
        TabControl.SelectTab(TabPageCounterLog)
    End Sub

    Private Sub btnNextOnPageThresholdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageThresholdFile.Click
        TabControl.SelectTab(TabPageQuestions)
    End Sub

    Private Sub btnPreviousOnPageQuestions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageQuestions.Click
        TabControl.SelectTab(TabPageThresholdFile)
    End Sub

    Private Sub btnNextOnPageQuestions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageQuestions.Click
        TabControl.SelectTab(TabPageAnalysisInterval)
    End Sub

    Private Sub btnPreviousOnPageAnalysisInterval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageAnalysisInterval.Click
        TabControl.SelectTab(TabPageQuestions)
    End Sub

    Private Sub btnNextOnPageAnalysisInterval_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageAnalysisInterval.Click
        TabControl.SelectTab(TabPageOutputOptions)
    End Sub

    Private Sub btnPreviousOnPageOutputOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageOutputOptions.Click
        TabControl.SelectTab(TabPageAnalysisInterval)
    End Sub

    Private Sub btnNextOnPageOutputOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageOutputOptions.Click
        TabControl.SelectTab(TabPageQueue)
    End Sub

    Private Sub btnPreviousOnPageQueue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageQueue.Click
        TabControl.SelectTab(TabPageOutputOptions)
    End Sub

    Private Sub btnNextOnPageQueue_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNextOnPageQueue.Click
        TabControl.SelectTab(TabPageExecute)
    End Sub

    Private Sub btnPreviousOnPageExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreviousOnPageExecute.Click
        TabControl.SelectTab(TabPageQueue)
    End Sub

    Private Sub ListBoxQuestions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxQuestions.SelectedIndexChanged
        Dim AofOptions As String()
        sSelectedQuestion = ListBoxQuestions.SelectedItem
        If String.IsNullOrEmpty(sSelectedQuestion) = True Then
            Exit Sub
        End If
        txtQuestion.Enabled = True
        txtQuestionAnswer.Enabled = True
        ComboBoxQuestionAnswer.Enabled = True
        If dctQuestionCollection.Count = 0 Then
            Exit Sub
        End If
        txtQuestion.Text = dctQuestionCollection(sSelectedQuestion).Question
        Select Case dctQuestionCollection(sSelectedQuestion).QuestionDataType
            Case "boolean"
                txtQuestionAnswer.Enabled = False
                txtQuestionAnswer.Visible = False
                lblQuestionAnswerValueString.Visible = False
                lblQuestionAnswerValueBoolean.Visible = True
                ComboBoxQuestionAnswer.Enabled = True
                ComboBoxQuestionAnswer.Visible = True
                ComboBoxQuestionAnswer.Items.Clear()
                ComboBoxQuestionAnswer.Items.Add("True")
                ComboBoxQuestionAnswer.Items.Add("False")
                If dctQuestionCollection(sSelectedQuestion).bAnswer = True Then
                    ComboBoxQuestionAnswer.Text = "True"
                Else
                    ComboBoxQuestionAnswer.Text = "False"
                End If

            Case "string"
                lblQuestionAnswerValueString.Visible = True
                lblQuestionAnswerValueBoolean.Visible = False
                txtQuestionAnswer.Enabled = True
                txtQuestionAnswer.Visible = True
                ComboBoxQuestionAnswer.Enabled = False
                ComboBoxQuestionAnswer.Visible = False
                txtQuestionAnswer.Text = dctQuestionCollection(sSelectedQuestion).sAnswer

            Case "options"
                txtQuestionAnswer.Enabled = False
                txtQuestionAnswer.Visible = False
                lblQuestionAnswerValueString.Visible = False
                lblQuestionAnswerValueBoolean.Visible = True
                ComboBoxQuestionAnswer.Enabled = True
                ComboBoxQuestionAnswer.Visible = True
                ComboBoxQuestionAnswer.Items.Clear()
                AofOptions = dctQuestionCollection(sSelectedQuestion).Options.Split(",")
                For Each sOption In AofOptions
                    ComboBoxQuestionAnswer.Items.Add(sOption)
                Next
                ComboBoxQuestionAnswer.Text = dctQuestionCollection(sSelectedQuestion).sAnswer

            Case Else
                lblQuestionAnswerValueString.Visible = True
                lblQuestionAnswerValueBoolean.Visible = False
                txtQuestionAnswer.Enabled = True
                txtQuestionAnswer.Visible = True
                ComboBoxQuestionAnswer.Enabled = False
                ComboBoxQuestionAnswer.Visible = False
                txtQuestionAnswer.Text = dctQuestionCollection(sSelectedQuestion).sAnswer
        End Select
    End Sub

    Private Sub ComboBoxQuestionAnswer_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxQuestionAnswer.SelectedIndexChanged

        Select Case ComboBoxQuestionAnswer.Text
            Case "True"
                dctQuestionCollection(sSelectedQuestion).bAnswer = True
                dctQuestionCollection(sSelectedQuestion).sAnswer = "True"
            Case "False"
                dctQuestionCollection(sSelectedQuestion).bAnswer = False
                dctQuestionCollection(sSelectedQuestion).sAnswer = "False"
            Case Else
                dctQuestionCollection(sSelectedQuestion).sAnswer = ComboBoxQuestionAnswer.Text
        End Select
        RefreshQueuePage()
    End Sub

    Private Sub txtQuestionAnswer_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQuestionAnswer.TextChanged
        If txtQuestionAnswer.Text <> "" Then
            dctQuestionCollection(sSelectedQuestion).sAnswer = txtQuestionAnswer.Text
            RefreshQueuePage()
        End If
    End Sub

    Private Sub CheckBoxAllCounterstats_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAllCounterstats.CheckedChanged
        AllCounterStats = CheckBoxAllCounterstats.Checked
        RefreshQueuePage()
    End Sub

    Private Sub LinkLabelClinthBlog_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelClinthBlog.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub LinkLabelEmailClint_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelEmailClint.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub LinkLabelAboutTheAuthorClintH_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelAboutTheAuthorClintH.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub SaveFileDialog1_FileOk(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles SaveFileDialog1.FileOk
        Dim oPALFunctions As New PALFunctions.PALFunctions
        Dim sPerfmonLogTemplateBody As String
        Const WINSEVEN = 1, WINSIX = 2, LOGMAN = 3

        Select Case SaveFileDialog1.FilterIndex
            Case WINSIX
                sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToPerfmonTemplate(ThresholdFilePath)
                If sPerfmonLogTemplateBody <> "" Then
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False)
                End If
            Case WINSEVEN
                sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToDataCollectorTemplate(ThresholdFilePath)
                If sPerfmonLogTemplateBody <> "" Then
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False)
                End If
            Case LOGMAN
                sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToLogmanCounterListFile(ThresholdFilePath)
                If sPerfmonLogTemplateBody <> "" Then
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False, System.Text.Encoding.ASCII)
                End If
            Case Else
                sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToDataCollectorTemplate(ThresholdFilePath)
                If sPerfmonLogTemplateBody <> "" Then
                    My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False)
                End If
        End Select

        '// Special logic about SQL named instances
        'If InStr(1, ThresholdFilePath, "SQL", CompareMethod.Text) > 0 Then
        '    MsgBox("The PAL tool is able to analyze Microsoft SQL Server named instance counters, but this Performance Monitor log template does not contain counters for Microsoft SQL Server named instances. When importing this template into Microsoft Performance Monitor, manually add Microsoft SQL Server named instance counters. This issue is due to the naming convention used by Microsoft SQL Server named instance performance counter paths.", MsgBoxStyle.Information, "Information About SQL Server Named Instances")
        'End If
        '// End Special logic about SQL named instances
    End Sub

    Private Sub ExportToThresholdFileToPerfmonTemplate()
        If ThresholdFilePath = "" Then
            Exit Sub
        End If
        SaveFileDialog1.Filter = "Windows 7/Windows Server 2008 files (*.xml)|*.xml|Windows XP/Windows Server 2003 files (*.htm)|*.htm|Logman files (*.txt)|*.txt"
        SaveFileDialog1.ShowDialog()
    End Sub

    Private Sub ExportToWinSevenThresholdFileToPerfmonTemplate()
        Dim sPerfmonLogTemplateBody As String
        Dim oPALFunctions As New PALFunctions.PALFunctions
        If ThresholdFilePath = "" Then
            Exit Sub
        End If
        sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToPerfmonTemplate(ThresholdFilePath)
        SaveFileDialog1.Filter = "Windows 7/Windows Server 2008 files (*.xml)|*.xml|All Files (*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False, System.Text.Encoding.Unicode)
        End If
        '// Special logic about SQL named instances
        If InStr(1, ThresholdFilePath, "SQL", CompareMethod.Text) > 0 Then
            MsgBox("The PAL tool is able to analyze Microsoft SQL Server named instance counters, but this Performance Monitor log template does not contain counters for Microsoft SQL Server named instances. When importing this template into Microsoft Performance Monitor, manually add Microsoft SQL Server named instance counters. This issue is due to the naming convention used by Microsoft SQL Server named instance performance counter paths.", MsgBoxStyle.Information, "Information About SQL Server Named Instances")
        End If
        '// End Special logic about SQL named instances
    End Sub

    Private Sub ExportToWinSixThresholdFileToPerfmonTemplate()
        Dim sPerfmonLogTemplateBody As String
        Dim oPALFunctions As New PALFunctions.PALFunctions
        If ThresholdFilePath = "" Then
            Exit Sub
        End If
        sPerfmonLogTemplateBody = oPALFunctions.ExportThresholdFileToPerfmonTemplate(ThresholdFilePath)
        SaveFileDialog1.Filter = "Windows XP/Windows Server 2003 files (*.htm)|*.htm|All Files (*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            My.Computer.FileSystem.WriteAllText(SaveFileDialog1.FileName, sPerfmonLogTemplateBody, False)
        End If
        '// Special logic about SQL named instances
        If InStr(1, ThresholdFilePath, "SQL", CompareMethod.Text) > 0 Then
            MsgBox("The PAL tool is able to analyze Microsoft SQL Server named instance counters, but this Performance Monitor log template does not contain counters for Microsoft SQL Server named instances. When importing this template into Microsoft Performance Monitor, manually add Microsoft SQL Server named instance counters. This issue is due to the naming convention used by Microsoft SQL Server named instance performance counter paths.", MsgBoxStyle.Information, "Information About SQL Server Named Instances")
        End If
        '// End Special logic about SQL named instances
    End Sub

    Private Sub LinkLabelSupport_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabelSupport.LinkClicked
        Dim sInfo As New ProcessStartInfo(e.Link.LinkData.ToString())
        Process.Start(sInfo)
    End Sub

    Private Sub ListBoxOfThresholdFileInheritance_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxOfThresholdFileInheritance.SelectedIndexChanged
        Dim sNote As String = ""
        Dim sThresholdFileName, sKey, sSelectedItemInheritedFromFileName As String
        If ListBoxOfThresholdFileInheritance.SelectedItem = Nothing Then
            Exit Sub
        End If

        If String.IsNullOrEmpty(sSelectedAnalysisCollectionKey) = True Then
            Exit Sub
        End If

        sThresholdFileName = GetFileNameFromPath(ThresholdFilePath)

        sKey = PALScriptInstallDirectory & "\" & ListBoxOfThresholdFileInheritance.SelectedItem
        sSelectedItemInheritedFromFileName = GetFileNameFromPath(dctThresholdFileInheritanceHistory(sKey))

        If sSelectedItemInheritedFromFileName <> sThresholdFileName Then
            sNote = "Note: Inherits from " & sSelectedItemInheritedFromFileName & ". If you want to remove this item, then you must go to the " & sSelectedItemInheritedFromFileName & " threshold file."
            btnThresholdFileInheritanceRemove.Enabled = False
            btnListBoxOfThresholdFileInheritanceUp.Enabled = False
            btnListBoxOfThresholdFileInheritanceDown.Enabled = False
        Else
            sNote = "Note: Inherits from " & sSelectedItemInheritedFromFileName & "."
            btnThresholdFileInheritanceAdd.Enabled = True
            btnThresholdFileInheritanceRemove.Enabled = True
            btnListBoxOfThresholdFileInheritanceUp.Enabled = True
            btnListBoxOfThresholdFileInheritanceDown.Enabled = True
        End If
        lblThresholdFileInheritanceNote.Text = sNote

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
            For Each sItem In OpenFileDialog1.FileNames
                sFileName = GetFileNameFromPath(sItem)
                If sFileName <> "" Then
                    ListBoxOfThresholdFileInheritance.Items.Add(sFileName)
                End If
            Next
        End If
        UpdateThresholdFile()
        RefreshAnalysisCollectionSection(True)
    End Sub

    Private Sub btnThresholdFileInheritanceRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdFileInheritanceRemove.Click
        Dim iResult As MsgBoxResult
        If ListBoxOfThresholdFileInheritance.SelectedItem <> Nothing Then
            iResult = MsgBox("Are you sure you want to permanently remove this inheritance?", MsgBoxStyle.YesNo, "Are you sure?")
            If iResult = vbYes Then
                ListBoxOfThresholdFileInheritance.Items.Remove(ListBoxOfThresholdFileInheritance.SelectedItem)
                UpdateThresholdFile()
                RefreshAnalysisCollectionSection(True)
            End If
        End If
    End Sub

    Private Sub UpdateThresholdFile()
        Dim sThresholdFilePath As String
        Dim aFileNames As String()
        Dim oXmlNode As XmlNode
        Dim oXmlDoc As New XmlDocument
        Dim oXmlRoot As XmlNode
        Dim oXmlNewNode As XmlNode
        Dim oXmlNewAttribute As XmlAttribute
        Dim sThresholdFileInheritanceFileNames As String
        Dim i, y As Integer

        sThresholdFileInheritanceFileNames = ""
        y = ListBoxOfThresholdFileInheritance.Items.Count - 1
        For i = 0 To y
            If i = 0 Then
                sThresholdFileInheritanceFileNames = ListBoxOfThresholdFileInheritance.Items(i)
            Else
                sThresholdFileInheritanceFileNames = sThresholdFileInheritanceFileNames & "," & ListBoxOfThresholdFileInheritance.Items(i)
            End If
        Next

        sThresholdFilePath = dctAnalysisCollection(sSelectedAnalysisCollectionKey).FilePath
        If System.IO.File.Exists(sThresholdFilePath) = True Then
            oXmlDoc.Load(sThresholdFilePath)
            oXmlRoot = oXmlDoc.DocumentElement

            '// Remove all of the INHERITANCE nodes.
            For Each oXmlNode In oXmlRoot.SelectNodes("//INHERITANCE")
                oXmlRoot.RemoveChild(oXmlNode)
            Next

            '// Add the new INHERITANCE nodes.
            aFileNames = Split(sThresholdFileInheritanceFileNames, ",")
            For Each sFileName In aFileNames
                If sFileName <> "" Then
                    oXmlNewNode = oXmlDoc.CreateNode(XmlNodeType.Element, "INHERITANCE", "")
                    oXmlNewAttribute = oXmlDoc.CreateAttribute("FILEPATH")
                    oXmlNewAttribute.Value = sFileName
                    oXmlNewNode.Attributes.Append(oXmlNewAttribute)
                    oXmlRoot.AppendChild(oXmlNewNode)
                End If
            Next
            oXmlDoc.Save(sThresholdFilePath)
        End If
    End Sub

    Private Sub btnListBoxOfThresholdFileInheritanceUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListBoxOfThresholdFileInheritanceUp.Click
        If ListBoxOfThresholdFileInheritance.SelectedItem <> Nothing Then
            If ListBoxOfThresholdFileInheritance.SelectedIndex > 0 And ListBoxOfThresholdFileInheritance.SelectedIndex < ListBoxOfThresholdFileInheritance.Items.Count Then
                ListBoxOfThresholdFileInheritance.Items.Insert(ListBoxOfThresholdFileInheritance.SelectedIndex + 1, ListBoxOfThresholdFileInheritance.Items(ListBoxOfThresholdFileInheritance.SelectedIndex - 1))
                ListBoxOfThresholdFileInheritance.Items.RemoveAt(ListBoxOfThresholdFileInheritance.SelectedIndex - 1)
                UpdateThresholdFile()
            End If
        End If
    End Sub

    Private Sub btnListBoxOfThresholdFileInheritanceDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListBoxOfThresholdFileInheritanceDown.Click
        If ListBoxOfThresholdFileInheritance.SelectedItem <> Nothing Then
            If ListBoxOfThresholdFileInheritance.SelectedIndex >= 0 And ListBoxOfThresholdFileInheritance.SelectedIndex < ListBoxOfThresholdFileInheritance.Items.Count - 1 Then
                ListBoxOfThresholdFileInheritance.Items.Insert(ListBoxOfThresholdFileInheritance.SelectedIndex, ListBoxOfThresholdFileInheritance.Items(ListBoxOfThresholdFileInheritance.SelectedIndex + 1))
                ListBoxOfThresholdFileInheritance.Items.RemoveAt(ListBoxOfThresholdFileInheritance.SelectedIndex + 1)
                UpdateThresholdFile()
            End If
        End If
    End Sub

    Private Sub ListBoxOfThresholdFileInheritanceRecursion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxOfThresholdFileInheritanceRecursion.GotFocus
        ListBoxOfThresholdFileInheritance.SelectedItem = Nothing
        btnThresholdFileInheritanceAdd.Enabled = False
        btnThresholdFileInheritanceRemove.Enabled = False
        btnListBoxOfThresholdFileInheritanceUp.Enabled = False
        btnListBoxOfThresholdFileInheritanceDown.Enabled = False
    End Sub

    Private Sub ListBoxOfThresholdFileInheritanceRecursion_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxOfThresholdFileInheritanceRecursion.LostFocus
        ListBoxOfThresholdFileInheritanceRecursion.SelectedItem = Nothing
    End Sub

    Private Sub ListBoxOfThresholdFileInheritanceRecursion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxOfThresholdFileInheritanceRecursion.SelectedIndexChanged
        Dim sNote As String = ""
        Dim sThresholdFileName, sKey, sSelectedItemInheritedFromFileName As String
        If ListBoxOfThresholdFileInheritanceRecursion.SelectedItem = Nothing Then
            Exit Sub
        End If

        If String.IsNullOrEmpty(sSelectedAnalysisCollectionKey) = True Then
            Exit Sub
        End If

        sThresholdFileName = GetFileNameFromPath(ThresholdFilePath)

        sKey = PALScriptInstallDirectory & "\" & ListBoxOfThresholdFileInheritanceRecursion.SelectedItem
        sSelectedItemInheritedFromFileName = GetFileNameFromPath(dctThresholdFileInheritanceHistory(sKey))

        If sSelectedItemInheritedFromFileName <> sThresholdFileName Then
            sNote = "Note: Inherits from " & sSelectedItemInheritedFromFileName & " . If you want to remove this item, then you must go to the " & sSelectedItemInheritedFromFileName & " threshold file."
            btnThresholdFileInheritanceRemove.Enabled = False
            btnListBoxOfThresholdFileInheritanceUp.Enabled = False
            btnListBoxOfThresholdFileInheritanceDown.Enabled = False
        Else
            sNote = ""
            btnThresholdFileInheritanceRemove.Enabled = True
            btnListBoxOfThresholdFileInheritanceUp.Enabled = True
            btnListBoxOfThresholdFileInheritanceDown.Enabled = True
        End If
        lblThresholdFileInheritanceNote.Text = sNote
    End Sub

    Private Sub TextBoxExecNumberOfThreads_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBoxExecNumberOfThreads.TextChanged
        UpdateNumberOfThreads()
    End Sub

    Private Sub UpdateNumberOfThreads()
        If iNumberOfThreads < 1 Or iNumberOfThreads > 100 Then
            iNumberOfThreads = 1
        End If

        If TextBoxExecNumberOfThreads.Text = "" Then
            Exit Sub
        End If

        If IsNumeric(TextBoxExecNumberOfThreads.Text) = True Then
            If TextBoxExecNumberOfThreads.Text > 0 And TextBoxExecNumberOfThreads.Text < 100 Then
                iNumberOfThreads = TextBoxExecNumberOfThreads.Text
            Else
                MsgBox("The number of threads must be a value between 1 and 100.")
                TextBoxExecNumberOfThreads.Text = iNumberOfThreads
            End If
        Else
            MsgBox("The number of threads must be a value between 1 and 100.")
            TextBoxExecNumberOfThreads.Text = iNumberOfThreads
        End If
    End Sub

    Private Sub btnAutoDetectThresholdFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoDetectThresholdFile.Click
        Dim sTempFolder, sTempAutoDetectThresholdFilePath, sText, sAutoDetectThresholdFileName As String
        Dim ListOfCounterObjectsFromCounterLog, ListOfThresholdFiles As List(Of String)

        If CounterLogPaths <> Nothing Then
            frmPleaseWait.Show()
            frmPleaseWait.Update()
            ListOfCounterObjectsFromCounterLog = GetCounterLogCounterList(CounterLogPaths)
            ListOfThresholdFiles = AutoDetectMatchCounterObjectsInLogToThresholdFiles(ListOfCounterObjectsFromCounterLog)

            sTempFolder = oCommandsQueue.sWorkingDirectory
            sAutoDetectThresholdFileName = "PalAutoDetectThresholdFile_" & Guid.NewGuid().ToString() & ".xml"
            sTempAutoDetectThresholdFilePath = sTempFolder & "\" & sAutoDetectThresholdFileName

            sText = "<PAL PALVERSION=" & Chr(34) & "2.0" & Chr(34) & " NAME=" & Chr(34) & "Auto-Detect" & Chr(34) & " DESCRIPTION=" & Chr(34) & "This setting automatically detects the threshold files relavent to the counter log." & Chr(34) & " CONTENTOWNERS=" & Chr(34) & "Clint Huffman" & Chr(34) & " FEEDBACKEMAILADDRESS=" & Chr(34) & "clinth@microsoft.com" & Chr(34) & " VERSION=" & Chr(34) & "0.1" & Chr(34) & " LANGUAGE=" & Chr(34) & "English" & Chr(34) & " LANGUAGECODE=" & Chr(34) & "en" & Chr(34) & ">" & vbNewLine
            For Each sThresholdFile In ListOfThresholdFiles
                sText = sText & vbTab & "<INHERITANCE FILEPATH=" & Chr(34) & sThresholdFile & Chr(34) & " />" & vbNewLine
            Next
            sText = sText & "</PAL>"
            My.Computer.FileSystem.WriteAllText(sTempAutoDetectThresholdFilePath, sText, False, System.Text.Encoding.ASCII)

            Dim oAnalysisCollection As New PALFunctions.AnalysisCollectionObject
            oAnalysisCollection.Name = sAutoDetectThresholdFileName
            oAnalysisCollection.Version = "1.0"
            oAnalysisCollection.Description = "This theshold file was generated by the Auto-detect feature. This is a *temporary* file in the %temp% directory."
            oAnalysisCollection.ContentOwners = ""
            oAnalysisCollection.FeedbackEmailAddresses = ""
            oAnalysisCollection.FilePath = sTempAutoDetectThresholdFilePath
            If dctAnalysisCollection.ContainsKey(oAnalysisCollection.Name) = False Then
                dctAnalysisCollection.Add(oAnalysisCollection.Name, oAnalysisCollection)
            End If

            ThresholdFilePath = sTempAutoDetectThresholdFilePath
            ComboBoxAnalysisCollection.Items.Add(sAutoDetectThresholdFileName)
            ComboBoxAnalysisCollection.SelectedItem = sAutoDetectThresholdFileName
            RefreshAnalysisCollectionSection(True)
            frmPleaseWait.Close()
        End If
    End Sub
End Class






