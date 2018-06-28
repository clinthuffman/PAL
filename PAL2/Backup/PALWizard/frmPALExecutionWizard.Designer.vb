<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPALExecutionWizard
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPALExecutionWizard))
        Me.TabControl = New System.Windows.Forms.TabControl
        Me.TabPageWelcome = New System.Windows.Forms.TabPage
        Me.LinkLabelSupport = New System.Windows.Forms.LinkLabel
        Me.LinkLabelAboutTheAuthorClintH = New System.Windows.Forms.LinkLabel
        Me.LinkLabelEmailClint = New System.Windows.Forms.LinkLabel
        Me.lblAboutTheAuthor = New System.Windows.Forms.Label
        Me.LinkLabelClinthBlog = New System.Windows.Forms.LinkLabel
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.lblv2 = New System.Windows.Forms.Label
        Me.LinkLabelLicense = New System.Windows.Forms.LinkLabel
        Me.LinkLabelURL = New System.Windows.Forms.LinkLabel
        Me.lblQuestionsOrProblems = New System.Windows.Forms.Label
        Me.lblPAL = New System.Windows.Forms.Label
        Me.btnNextOnPageWelcome = New System.Windows.Forms.Button
        Me.lblWelcome = New System.Windows.Forms.Label
        Me.TabPageCounterLog = New System.Windows.Forms.TabPage
        Me.GroupBoxBeginTimeEndTime = New System.Windows.Forms.GroupBox
        Me.lblDateTimeRangeNote = New System.Windows.Forms.Label
        Me.lblEndTime = New System.Windows.Forms.Label
        Me.lblBeginTime = New System.Windows.Forms.Label
        Me.DateTimePickerEndTime = New System.Windows.Forms.DateTimePicker
        Me.DateTimePickerBeginTime = New System.Windows.Forms.DateTimePicker
        Me.CheckBoxRestrictToADateTimeRange = New System.Windows.Forms.CheckBox
        Me.ComboBoxRunLogFile = New System.Windows.Forms.ComboBox
        Me.lblStep1 = New System.Windows.Forms.Label
        Me.lblRunLogPath = New System.Windows.Forms.Label
        Me.btnPreviousOnPageCounterLog = New System.Windows.Forms.Button
        Me.btnFileBrowse = New System.Windows.Forms.Button
        Me.btnNextOnPageCounterLog = New System.Windows.Forms.Button
        Me.TabPageThresholdFile = New System.Windows.Forms.TabPage
        Me.Label6 = New System.Windows.Forms.Label
        Me.ListBoxOfThresholdFileInheritanceRecursion = New System.Windows.Forms.ListBox
        Me.lblThresholdFileInheritanceNote = New System.Windows.Forms.Label
        Me.btnListBoxOfThresholdFileInheritanceDown = New System.Windows.Forms.Button
        Me.btnListBoxOfThresholdFileInheritanceUp = New System.Windows.Forms.Button
        Me.btnThresholdFileInheritanceRemove = New System.Windows.Forms.Button
        Me.btnThresholdFileInheritanceAdd = New System.Windows.Forms.Button
        Me.lblThresholdFileInheritance = New System.Windows.Forms.Label
        Me.ListBoxOfThresholdFileInheritance = New System.Windows.Forms.ListBox
        Me.lblStep3 = New System.Windows.Forms.Label
        Me.lblThresholdFileContentOwners = New System.Windows.Forms.Label
        Me.txtThresholdFileContentOwners = New System.Windows.Forms.TextBox
        Me.btnPreviousOnPageThresholdFile = New System.Windows.Forms.Button
        Me.btnExportThresholdFileToPerfmonTemplate = New System.Windows.Forms.Button
        Me.btnNextOnPageThresholdFile = New System.Windows.Forms.Button
        Me.Label2 = New System.Windows.Forms.Label
        Me.ComboBoxAnalysisCollection = New System.Windows.Forms.ComboBox
        Me.txtAnalysisCollectionDescription = New System.Windows.Forms.TextBox
        Me.lblThresholdFileName = New System.Windows.Forms.Label
        Me.btnEditThresholdFile = New System.Windows.Forms.Button
        Me.lblAnalysisCollectionName = New System.Windows.Forms.Label
        Me.txtThresholdFileName = New System.Windows.Forms.TextBox
        Me.TabPageQuestions = New System.Windows.Forms.TabPage
        Me.txtQuestionAnswerResults = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnPreviousOnPageQuestions = New System.Windows.Forms.Button
        Me.btnNextOnPageQuestions = New System.Windows.Forms.Button
        Me.GroupBoxQuestionVariableNames = New System.Windows.Forms.GroupBox
        Me.ListBoxQuestions = New System.Windows.Forms.ListBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtQuestion = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.ComboBoxQuestionAnswer = New System.Windows.Forms.ComboBox
        Me.txtQuestionAnswer = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TabPageAnalysisInterval = New System.Windows.Forms.TabPage
        Me.GroupBoxAllCounterStats = New System.Windows.Forms.GroupBox
        Me.lblAllCounterStats = New System.Windows.Forms.Label
        Me.CheckBoxAllCounterstats = New System.Windows.Forms.CheckBox
        Me.GroupBoxAnalysisInterval = New System.Windows.Forms.GroupBox
        Me.lblStep2 = New System.Windows.Forms.Label
        Me.lblSecs = New System.Windows.Forms.Label
        Me.ComboBoxInterval = New System.Windows.Forms.ComboBox
        Me.lblRunInterval = New System.Windows.Forms.Label
        Me.btnPreviousOnPageAnalysisInterval = New System.Windows.Forms.Button
        Me.btnNextOnPageAnalysisInterval = New System.Windows.Forms.Button
        Me.TabPageOutputOptions = New System.Windows.Forms.TabPage
        Me.GroupBoxXMLOutput = New System.Windows.Forms.GroupBox
        Me.txtFullXMLOutputFileName = New System.Windows.Forms.TextBox
        Me.lblFullXMLOutputPathLabel = New System.Windows.Forms.Label
        Me.lblXMLFileNameLabel = New System.Windows.Forms.Label
        Me.txtXMLOutputFileName = New System.Windows.Forms.TextBox
        Me.CheckBoxXMLOutput = New System.Windows.Forms.CheckBox
        Me.txtOutputDirectoryPath = New System.Windows.Forms.TextBox
        Me.btnOutputDirectoryBrowse = New System.Windows.Forms.Button
        Me.GroupBoxHTMLOutput = New System.Windows.Forms.GroupBox
        Me.txtFullHTMLOutputFileName = New System.Windows.Forms.TextBox
        Me.lblFullHTMLOutputDirectoryLabel = New System.Windows.Forms.Label
        Me.lblHTMLReportFileName = New System.Windows.Forms.Label
        Me.txtHTMLOutputFileName = New System.Windows.Forms.TextBox
        Me.CheckBoxHTMLOutput = New System.Windows.Forms.CheckBox
        Me.lblOutputDirectory = New System.Windows.Forms.Label
        Me.lblOutputDirectoryDescription = New System.Windows.Forms.Label
        Me.btnPreviousOnPageOutputOptions = New System.Windows.Forms.Button
        Me.btnNextOnPageOutputOptions = New System.Windows.Forms.Button
        Me.TabPageQueue = New System.Windows.Forms.TabPage
        Me.btnRemoveLastFromQueue = New System.Windows.Forms.Button
        Me.GroupBoxQueue = New System.Windows.Forms.GroupBox
        Me.txtBatchText = New System.Windows.Forms.TextBox
        Me.btnPreviousOnPageQueue = New System.Windows.Forms.Button
        Me.lblStep5 = New System.Windows.Forms.Label
        Me.btnNextOnPageQueue = New System.Windows.Forms.Button
        Me.TabPageExecute = New System.Windows.Forms.TabPage
        Me.GroupBoxNumberOfThreads = New System.Windows.Forms.GroupBox
        Me.lblNumberOfThreads = New System.Windows.Forms.Label
        Me.CheckBoxRunAsLowPriority = New System.Windows.Forms.CheckBox
        Me.lblThreadingHelp = New System.Windows.Forms.Label
        Me.TextBoxExecNumberOfThreads = New System.Windows.Forms.TextBox
        Me.RadExecuteQueue = New System.Windows.Forms.RadioButton
        Me.RadExecuteAndRunWizardAgain = New System.Windows.Forms.RadioButton
        Me.RadAddMoreToQueue = New System.Windows.Forms.RadioButton
        Me.lblExecute = New System.Windows.Forms.Label
        Me.btnPreviousOnPageExecute = New System.Windows.Forms.Button
        Me.btnNextOnPageExecute = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.ListBoxTemp = New System.Windows.Forms.ListBox
        Me.lblQuestion = New System.Windows.Forms.Label
        Me.txtQuestionDELETE = New System.Windows.Forms.TextBox
        Me.lblQuestionAnswerValueString = New System.Windows.Forms.Label
        Me.ComboBoxQuestionAnswerDELETE = New System.Windows.Forms.ComboBox
        Me.txtQuestionAnswerDELETE = New System.Windows.Forms.TextBox
        Me.lblQuestionAnswerValueBoolean = New System.Windows.Forms.Label
        Me.btnAutoDetectThresholdFile = New System.Windows.Forms.Button
        Me.TabControl.SuspendLayout()
        Me.TabPageWelcome.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPageCounterLog.SuspendLayout()
        Me.GroupBoxBeginTimeEndTime.SuspendLayout()
        Me.TabPageThresholdFile.SuspendLayout()
        Me.TabPageQuestions.SuspendLayout()
        Me.GroupBoxQuestionVariableNames.SuspendLayout()
        Me.TabPageAnalysisInterval.SuspendLayout()
        Me.GroupBoxAllCounterStats.SuspendLayout()
        Me.GroupBoxAnalysisInterval.SuspendLayout()
        Me.TabPageOutputOptions.SuspendLayout()
        Me.GroupBoxXMLOutput.SuspendLayout()
        Me.GroupBoxHTMLOutput.SuspendLayout()
        Me.TabPageQueue.SuspendLayout()
        Me.GroupBoxQueue.SuspendLayout()
        Me.TabPageExecute.SuspendLayout()
        Me.GroupBoxNumberOfThreads.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl
        '
        Me.TabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl.Controls.Add(Me.TabPageWelcome)
        Me.TabControl.Controls.Add(Me.TabPageCounterLog)
        Me.TabControl.Controls.Add(Me.TabPageThresholdFile)
        Me.TabControl.Controls.Add(Me.TabPageQuestions)
        Me.TabControl.Controls.Add(Me.TabPageAnalysisInterval)
        Me.TabControl.Controls.Add(Me.TabPageOutputOptions)
        Me.TabControl.Controls.Add(Me.TabPageQueue)
        Me.TabControl.Controls.Add(Me.TabPageExecute)
        Me.TabControl.Location = New System.Drawing.Point(12, 12)
        Me.TabControl.Name = "TabControl"
        Me.TabControl.SelectedIndex = 0
        Me.TabControl.Size = New System.Drawing.Size(613, 459)
        Me.TabControl.TabIndex = 0
        '
        'TabPageWelcome
        '
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelSupport)
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelAboutTheAuthorClintH)
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelEmailClint)
        Me.TabPageWelcome.Controls.Add(Me.lblAboutTheAuthor)
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelClinthBlog)
        Me.TabPageWelcome.Controls.Add(Me.PictureBox1)
        Me.TabPageWelcome.Controls.Add(Me.lblv2)
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelLicense)
        Me.TabPageWelcome.Controls.Add(Me.LinkLabelURL)
        Me.TabPageWelcome.Controls.Add(Me.lblQuestionsOrProblems)
        Me.TabPageWelcome.Controls.Add(Me.lblPAL)
        Me.TabPageWelcome.Controls.Add(Me.btnNextOnPageWelcome)
        Me.TabPageWelcome.Controls.Add(Me.lblWelcome)
        Me.TabPageWelcome.Location = New System.Drawing.Point(4, 25)
        Me.TabPageWelcome.Name = "TabPageWelcome"
        Me.TabPageWelcome.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageWelcome.Size = New System.Drawing.Size(605, 430)
        Me.TabPageWelcome.TabIndex = 0
        Me.TabPageWelcome.Text = "Welcome"
        Me.TabPageWelcome.UseVisualStyleBackColor = True
        '
        'LinkLabelSupport
        '
        Me.LinkLabelSupport.Location = New System.Drawing.Point(432, 218)
        Me.LinkLabelSupport.Name = "LinkLabelSupport"
        Me.LinkLabelSupport.Size = New System.Drawing.Size(170, 17)
        Me.LinkLabelSupport.TabIndex = 12
        Me.LinkLabelSupport.TabStop = True
        Me.LinkLabelSupport.Text = "Support"
        '
        'LinkLabelAboutTheAuthorClintH
        '
        Me.LinkLabelAboutTheAuthorClintH.AutoSize = True
        Me.LinkLabelAboutTheAuthorClintH.Location = New System.Drawing.Point(432, 178)
        Me.LinkLabelAboutTheAuthorClintH.Name = "LinkLabelAboutTheAuthorClintH"
        Me.LinkLabelAboutTheAuthorClintH.Size = New System.Drawing.Size(87, 13)
        Me.LinkLabelAboutTheAuthorClintH.TabIndex = 11
        Me.LinkLabelAboutTheAuthorClintH.TabStop = True
        Me.LinkLabelAboutTheAuthorClintH.Text = "About the Author"
        '
        'LinkLabelEmailClint
        '
        Me.LinkLabelEmailClint.AutoSize = True
        Me.LinkLabelEmailClint.Location = New System.Drawing.Point(432, 191)
        Me.LinkLabelEmailClint.Name = "LinkLabelEmailClint"
        Me.LinkLabelEmailClint.Size = New System.Drawing.Size(139, 13)
        Me.LinkLabelEmailClint.TabIndex = 10
        Me.LinkLabelEmailClint.TabStop = True
        Me.LinkLabelEmailClint.Text = "Email: clinth@microsoft.com"
        '
        'lblAboutTheAuthor
        '
        Me.lblAboutTheAuthor.AutoSize = True
        Me.lblAboutTheAuthor.Location = New System.Drawing.Point(429, 165)
        Me.lblAboutTheAuthor.Name = "lblAboutTheAuthor"
        Me.lblAboutTheAuthor.Size = New System.Drawing.Size(107, 13)
        Me.lblAboutTheAuthor.TabIndex = 9
        Me.lblAboutTheAuthor.Text = "Author: Clint Huffman"
        '
        'LinkLabelClinthBlog
        '
        Me.LinkLabelClinthBlog.Location = New System.Drawing.Point(432, 204)
        Me.LinkLabelClinthBlog.Name = "LinkLabelClinthBlog"
        Me.LinkLabelClinthBlog.Size = New System.Drawing.Size(170, 19)
        Me.LinkLabelClinthBlog.TabIndex = 8
        Me.LinkLabelClinthBlog.TabStop = True
        Me.LinkLabelClinthBlog.Text = "Blog"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.PALWizard.My.Resources.Resources.ClintHuffmanGamerPic
        Me.PictureBox1.Location = New System.Drawing.Point(435, 92)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(63, 66)
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'lblv2
        '
        Me.lblv2.AutoSize = True
        Me.lblv2.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblv2.Location = New System.Drawing.Point(118, 234)
        Me.lblv2.Name = "lblv2"
        Me.lblv2.Size = New System.Drawing.Size(43, 31)
        Me.lblv2.TabIndex = 6
        Me.lblv2.Text = "v2"
        '
        'LinkLabelLicense
        '
        Me.LinkLabelLicense.AutoSize = True
        Me.LinkLabelLicense.Location = New System.Drawing.Point(14, 371)
        Me.LinkLabelLicense.Name = "LinkLabelLicense"
        Me.LinkLabelLicense.Size = New System.Drawing.Size(99, 13)
        Me.LinkLabelLicense.TabIndex = 5
        Me.LinkLabelLicense.TabStop = True
        Me.LinkLabelLicense.Text = "License Information"
        '
        'LinkLabelURL
        '
        Me.LinkLabelURL.AutoSize = True
        Me.LinkLabelURL.Location = New System.Drawing.Point(14, 274)
        Me.LinkLabelURL.Name = "LinkLabelURL"
        Me.LinkLabelURL.Size = New System.Drawing.Size(126, 13)
        Me.LinkLabelURL.TabIndex = 4
        Me.LinkLabelURL.TabStop = True
        Me.LinkLabelURL.Text = "http://pal.codeplex.com/"
        '
        'lblQuestionsOrProblems
        '
        Me.lblQuestionsOrProblems.Location = New System.Drawing.Point(14, 297)
        Me.lblQuestionsOrProblems.Name = "lblQuestionsOrProblems"
        Me.lblQuestionsOrProblems.Size = New System.Drawing.Size(423, 74)
        Me.lblQuestionsOrProblems.TabIndex = 3
        Me.lblQuestionsOrProblems.Text = resources.GetString("lblQuestionsOrProblems.Text")
        '
        'lblPAL
        '
        Me.lblPAL.AutoSize = True
        Me.lblPAL.Font = New System.Drawing.Font("Microsoft Sans Serif", 40.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPAL.Location = New System.Drawing.Point(6, 211)
        Me.lblPAL.Name = "lblPAL"
        Me.lblPAL.Size = New System.Drawing.Size(129, 63)
        Me.lblPAL.TabIndex = 2
        Me.lblPAL.Text = "PAL"
        '
        'btnNextOnPageWelcome
        '
        Me.btnNextOnPageWelcome.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageWelcome.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageWelcome.Name = "btnNextOnPageWelcome"
        Me.btnNextOnPageWelcome.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageWelcome.TabIndex = 1
        Me.btnNextOnPageWelcome.Text = "Next"
        Me.btnNextOnPageWelcome.UseVisualStyleBackColor = True
        '
        'lblWelcome
        '
        Me.lblWelcome.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWelcome.Location = New System.Drawing.Point(3, 17)
        Me.lblWelcome.Name = "lblWelcome"
        Me.lblWelcome.Size = New System.Drawing.Size(420, 141)
        Me.lblWelcome.TabIndex = 0
        Me.lblWelcome.Text = "Welcome to the Performance Analysis of Logs (PAL) tool analysis wizard. This wiza" & _
            "rd will guide you through the process of analyzing your log file(s)."
        '
        'TabPageCounterLog
        '
        Me.TabPageCounterLog.Controls.Add(Me.GroupBoxBeginTimeEndTime)
        Me.TabPageCounterLog.Controls.Add(Me.ComboBoxRunLogFile)
        Me.TabPageCounterLog.Controls.Add(Me.lblStep1)
        Me.TabPageCounterLog.Controls.Add(Me.lblRunLogPath)
        Me.TabPageCounterLog.Controls.Add(Me.btnPreviousOnPageCounterLog)
        Me.TabPageCounterLog.Controls.Add(Me.btnFileBrowse)
        Me.TabPageCounterLog.Controls.Add(Me.btnNextOnPageCounterLog)
        Me.TabPageCounterLog.Location = New System.Drawing.Point(4, 25)
        Me.TabPageCounterLog.Name = "TabPageCounterLog"
        Me.TabPageCounterLog.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCounterLog.Size = New System.Drawing.Size(605, 430)
        Me.TabPageCounterLog.TabIndex = 1
        Me.TabPageCounterLog.Text = "Counter Log"
        Me.TabPageCounterLog.UseVisualStyleBackColor = True
        '
        'GroupBoxBeginTimeEndTime
        '
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.lblDateTimeRangeNote)
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.lblEndTime)
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.lblBeginTime)
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.DateTimePickerEndTime)
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.DateTimePickerBeginTime)
        Me.GroupBoxBeginTimeEndTime.Controls.Add(Me.CheckBoxRestrictToADateTimeRange)
        Me.GroupBoxBeginTimeEndTime.Location = New System.Drawing.Point(10, 175)
        Me.GroupBoxBeginTimeEndTime.Name = "GroupBoxBeginTimeEndTime"
        Me.GroupBoxBeginTimeEndTime.Size = New System.Drawing.Size(409, 154)
        Me.GroupBoxBeginTimeEndTime.TabIndex = 19
        Me.GroupBoxBeginTimeEndTime.TabStop = False
        Me.GroupBoxBeginTimeEndTime.Text = "Date/Time Range"
        '
        'lblDateTimeRangeNote
        '
        Me.lblDateTimeRangeNote.Enabled = False
        Me.lblDateTimeRangeNote.Location = New System.Drawing.Point(9, 98)
        Me.lblDateTimeRangeNote.Name = "lblDateTimeRangeNote"
        Me.lblDateTimeRangeNote.Size = New System.Drawing.Size(373, 39)
        Me.lblDateTimeRangeNote.TabIndex = 5
        Me.lblDateTimeRangeNote.Text = "Status: "
        '
        'lblEndTime
        '
        Me.lblEndTime.AutoSize = True
        Me.lblEndTime.Enabled = False
        Me.lblEndTime.Location = New System.Drawing.Point(21, 69)
        Me.lblEndTime.Name = "lblEndTime"
        Me.lblEndTime.Size = New System.Drawing.Size(55, 13)
        Me.lblEndTime.TabIndex = 4
        Me.lblEndTime.Text = "End Time:"
        '
        'lblBeginTime
        '
        Me.lblBeginTime.AutoSize = True
        Me.lblBeginTime.Enabled = False
        Me.lblBeginTime.Location = New System.Drawing.Point(18, 42)
        Me.lblBeginTime.Name = "lblBeginTime"
        Me.lblBeginTime.Size = New System.Drawing.Size(63, 13)
        Me.lblBeginTime.TabIndex = 3
        Me.lblBeginTime.Text = "Begin Time:"
        '
        'DateTimePickerEndTime
        '
        Me.DateTimePickerEndTime.CustomFormat = "dddd, MMMM dd, yyyy hh:mm tt"
        Me.DateTimePickerEndTime.Enabled = False
        Me.DateTimePickerEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerEndTime.Location = New System.Drawing.Point(84, 69)
        Me.DateTimePickerEndTime.Name = "DateTimePickerEndTime"
        Me.DateTimePickerEndTime.Size = New System.Drawing.Size(298, 20)
        Me.DateTimePickerEndTime.TabIndex = 2
        '
        'DateTimePickerBeginTime
        '
        Me.DateTimePickerBeginTime.CustomFormat = "dddd, MMMM dd, yyyy hh:mm tt"
        Me.DateTimePickerBeginTime.Enabled = False
        Me.DateTimePickerBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePickerBeginTime.Location = New System.Drawing.Point(84, 42)
        Me.DateTimePickerBeginTime.Name = "DateTimePickerBeginTime"
        Me.DateTimePickerBeginTime.Size = New System.Drawing.Size(298, 20)
        Me.DateTimePickerBeginTime.TabIndex = 1
        '
        'CheckBoxRestrictToADateTimeRange
        '
        Me.CheckBoxRestrictToADateTimeRange.AutoSize = True
        Me.CheckBoxRestrictToADateTimeRange.Location = New System.Drawing.Point(9, 19)
        Me.CheckBoxRestrictToADateTimeRange.Name = "CheckBoxRestrictToADateTimeRange"
        Me.CheckBoxRestrictToADateTimeRange.Size = New System.Drawing.Size(167, 17)
        Me.CheckBoxRestrictToADateTimeRange.TabIndex = 0
        Me.CheckBoxRestrictToADateTimeRange.Text = "Restrict to a DateTime Range"
        Me.CheckBoxRestrictToADateTimeRange.UseVisualStyleBackColor = True
        '
        'ComboBoxRunLogFile
        '
        Me.ComboBoxRunLogFile.DropDownWidth = 750
        Me.ComboBoxRunLogFile.FormattingEnabled = True
        Me.ComboBoxRunLogFile.Location = New System.Drawing.Point(10, 148)
        Me.ComboBoxRunLogFile.Name = "ComboBoxRunLogFile"
        Me.ComboBoxRunLogFile.Size = New System.Drawing.Size(382, 21)
        Me.ComboBoxRunLogFile.TabIndex = 17
        '
        'lblStep1
        '
        Me.lblStep1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStep1.Location = New System.Drawing.Point(7, 18)
        Me.lblStep1.Name = "lblStep1"
        Me.lblStep1.Size = New System.Drawing.Size(412, 102)
        Me.lblStep1.TabIndex = 18
        Me.lblStep1.Text = resources.GetString("lblStep1.Text")
        '
        'lblRunLogPath
        '
        Me.lblRunLogPath.AutoSize = True
        Me.lblRunLogPath.Location = New System.Drawing.Point(7, 132)
        Me.lblRunLogPath.Name = "lblRunLogPath"
        Me.lblRunLogPath.Size = New System.Drawing.Size(93, 13)
        Me.lblRunLogPath.TabIndex = 6
        Me.lblRunLogPath.Text = "Counter Log Path:"
        '
        'btnPreviousOnPageCounterLog
        '
        Me.btnPreviousOnPageCounterLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageCounterLog.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageCounterLog.Name = "btnPreviousOnPageCounterLog"
        Me.btnPreviousOnPageCounterLog.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageCounterLog.TabIndex = 2
        Me.btnPreviousOnPageCounterLog.Text = "Previous"
        Me.btnPreviousOnPageCounterLog.UseVisualStyleBackColor = True
        '
        'btnFileBrowse
        '
        Me.btnFileBrowse.Location = New System.Drawing.Point(394, 146)
        Me.btnFileBrowse.Name = "btnFileBrowse"
        Me.btnFileBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnFileBrowse.TabIndex = 7
        Me.btnFileBrowse.Text = "..."
        Me.btnFileBrowse.UseVisualStyleBackColor = True
        '
        'btnNextOnPageCounterLog
        '
        Me.btnNextOnPageCounterLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageCounterLog.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageCounterLog.Name = "btnNextOnPageCounterLog"
        Me.btnNextOnPageCounterLog.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageCounterLog.TabIndex = 1
        Me.btnNextOnPageCounterLog.Text = "Next"
        Me.btnNextOnPageCounterLog.UseVisualStyleBackColor = True
        '
        'TabPageThresholdFile
        '
        Me.TabPageThresholdFile.Controls.Add(Me.btnAutoDetectThresholdFile)
        Me.TabPageThresholdFile.Controls.Add(Me.Label6)
        Me.TabPageThresholdFile.Controls.Add(Me.ListBoxOfThresholdFileInheritanceRecursion)
        Me.TabPageThresholdFile.Controls.Add(Me.lblThresholdFileInheritanceNote)
        Me.TabPageThresholdFile.Controls.Add(Me.btnListBoxOfThresholdFileInheritanceDown)
        Me.TabPageThresholdFile.Controls.Add(Me.btnListBoxOfThresholdFileInheritanceUp)
        Me.TabPageThresholdFile.Controls.Add(Me.btnThresholdFileInheritanceRemove)
        Me.TabPageThresholdFile.Controls.Add(Me.btnThresholdFileInheritanceAdd)
        Me.TabPageThresholdFile.Controls.Add(Me.lblThresholdFileInheritance)
        Me.TabPageThresholdFile.Controls.Add(Me.ListBoxOfThresholdFileInheritance)
        Me.TabPageThresholdFile.Controls.Add(Me.lblStep3)
        Me.TabPageThresholdFile.Controls.Add(Me.lblThresholdFileContentOwners)
        Me.TabPageThresholdFile.Controls.Add(Me.txtThresholdFileContentOwners)
        Me.TabPageThresholdFile.Controls.Add(Me.btnPreviousOnPageThresholdFile)
        Me.TabPageThresholdFile.Controls.Add(Me.btnExportThresholdFileToPerfmonTemplate)
        Me.TabPageThresholdFile.Controls.Add(Me.btnNextOnPageThresholdFile)
        Me.TabPageThresholdFile.Controls.Add(Me.Label2)
        Me.TabPageThresholdFile.Controls.Add(Me.ComboBoxAnalysisCollection)
        Me.TabPageThresholdFile.Controls.Add(Me.txtAnalysisCollectionDescription)
        Me.TabPageThresholdFile.Controls.Add(Me.lblThresholdFileName)
        Me.TabPageThresholdFile.Controls.Add(Me.btnEditThresholdFile)
        Me.TabPageThresholdFile.Controls.Add(Me.lblAnalysisCollectionName)
        Me.TabPageThresholdFile.Controls.Add(Me.txtThresholdFileName)
        Me.TabPageThresholdFile.Location = New System.Drawing.Point(4, 25)
        Me.TabPageThresholdFile.Name = "TabPageThresholdFile"
        Me.TabPageThresholdFile.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageThresholdFile.Size = New System.Drawing.Size(605, 430)
        Me.TabPageThresholdFile.TabIndex = 2
        Me.TabPageThresholdFile.Text = "Threshold File"
        Me.TabPageThresholdFile.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(172, 312)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(189, 14)
        Me.Label6.TabIndex = 56
        Me.Label6.Text = "Recursively inherited:"
        '
        'ListBoxOfThresholdFileInheritanceRecursion
        '
        Me.ListBoxOfThresholdFileInheritanceRecursion.FormattingEnabled = True
        Me.ListBoxOfThresholdFileInheritanceRecursion.HorizontalScrollbar = True
        Me.ListBoxOfThresholdFileInheritanceRecursion.Location = New System.Drawing.Point(175, 329)
        Me.ListBoxOfThresholdFileInheritanceRecursion.Name = "ListBoxOfThresholdFileInheritanceRecursion"
        Me.ListBoxOfThresholdFileInheritanceRecursion.Size = New System.Drawing.Size(245, 56)
        Me.ListBoxOfThresholdFileInheritanceRecursion.TabIndex = 55
        '
        'lblThresholdFileInheritanceNote
        '
        Me.lblThresholdFileInheritanceNote.Location = New System.Drawing.Point(136, 388)
        Me.lblThresholdFileInheritanceNote.Name = "lblThresholdFileInheritanceNote"
        Me.lblThresholdFileInheritanceNote.Size = New System.Drawing.Size(284, 39)
        Me.lblThresholdFileInheritanceNote.TabIndex = 54
        '
        'btnListBoxOfThresholdFileInheritanceDown
        '
        Me.btnListBoxOfThresholdFileInheritanceDown.Location = New System.Drawing.Point(426, 282)
        Me.btnListBoxOfThresholdFileInheritanceDown.Name = "btnListBoxOfThresholdFileInheritanceDown"
        Me.btnListBoxOfThresholdFileInheritanceDown.Size = New System.Drawing.Size(27, 23)
        Me.btnListBoxOfThresholdFileInheritanceDown.TabIndex = 53
        Me.btnListBoxOfThresholdFileInheritanceDown.Text = "dn"
        Me.btnListBoxOfThresholdFileInheritanceDown.UseVisualStyleBackColor = True
        '
        'btnListBoxOfThresholdFileInheritanceUp
        '
        Me.btnListBoxOfThresholdFileInheritanceUp.Location = New System.Drawing.Point(426, 252)
        Me.btnListBoxOfThresholdFileInheritanceUp.Name = "btnListBoxOfThresholdFileInheritanceUp"
        Me.btnListBoxOfThresholdFileInheritanceUp.Size = New System.Drawing.Size(27, 23)
        Me.btnListBoxOfThresholdFileInheritanceUp.TabIndex = 52
        Me.btnListBoxOfThresholdFileInheritanceUp.Text = "up"
        Me.btnListBoxOfThresholdFileInheritanceUp.UseVisualStyleBackColor = True
        '
        'btnThresholdFileInheritanceRemove
        '
        Me.btnThresholdFileInheritanceRemove.Location = New System.Drawing.Point(459, 282)
        Me.btnThresholdFileInheritanceRemove.Name = "btnThresholdFileInheritanceRemove"
        Me.btnThresholdFileInheritanceRemove.Size = New System.Drawing.Size(58, 23)
        Me.btnThresholdFileInheritanceRemove.TabIndex = 51
        Me.btnThresholdFileInheritanceRemove.Text = "Remove"
        Me.btnThresholdFileInheritanceRemove.UseVisualStyleBackColor = True
        '
        'btnThresholdFileInheritanceAdd
        '
        Me.btnThresholdFileInheritanceAdd.Location = New System.Drawing.Point(459, 252)
        Me.btnThresholdFileInheritanceAdd.Name = "btnThresholdFileInheritanceAdd"
        Me.btnThresholdFileInheritanceAdd.Size = New System.Drawing.Size(58, 23)
        Me.btnThresholdFileInheritanceAdd.TabIndex = 50
        Me.btnThresholdFileInheritanceAdd.Text = "Add..."
        Me.btnThresholdFileInheritanceAdd.UseVisualStyleBackColor = True
        '
        'lblThresholdFileInheritance
        '
        Me.lblThresholdFileInheritance.Location = New System.Drawing.Point(22, 252)
        Me.lblThresholdFileInheritance.Name = "lblThresholdFileInheritance"
        Me.lblThresholdFileInheritance.Size = New System.Drawing.Size(100, 56)
        Me.lblThresholdFileInheritance.TabIndex = 34
        Me.lblThresholdFileInheritance.Text = "Inherited by the threshold file:"
        '
        'ListBoxOfThresholdFileInheritance
        '
        Me.ListBoxOfThresholdFileInheritance.FormattingEnabled = True
        Me.ListBoxOfThresholdFileInheritance.HorizontalScrollbar = True
        Me.ListBoxOfThresholdFileInheritance.Location = New System.Drawing.Point(136, 252)
        Me.ListBoxOfThresholdFileInheritance.Name = "ListBoxOfThresholdFileInheritance"
        Me.ListBoxOfThresholdFileInheritance.Size = New System.Drawing.Size(284, 56)
        Me.ListBoxOfThresholdFileInheritance.TabIndex = 33
        '
        'lblStep3
        '
        Me.lblStep3.Location = New System.Drawing.Point(6, 6)
        Me.lblStep3.Name = "lblStep3"
        Me.lblStep3.Size = New System.Drawing.Size(414, 72)
        Me.lblStep3.TabIndex = 32
        Me.lblStep3.Text = "Choose a threshold file that is appropriate for the computer(s) on which the log " & _
            "was captured. If the computer(s) have multiple products or roles, then change th" & _
            "e inheritance to include all of them."
        '
        'lblThresholdFileContentOwners
        '
        Me.lblThresholdFileContentOwners.AutoSize = True
        Me.lblThresholdFileContentOwners.Location = New System.Drawing.Point(23, 229)
        Me.lblThresholdFileContentOwners.Name = "lblThresholdFileContentOwners"
        Me.lblThresholdFileContentOwners.Size = New System.Drawing.Size(90, 13)
        Me.lblThresholdFileContentOwners.TabIndex = 31
        Me.lblThresholdFileContentOwners.Text = "Content owner(s):"
        '
        'txtThresholdFileContentOwners
        '
        Me.txtThresholdFileContentOwners.Location = New System.Drawing.Point(136, 226)
        Me.txtThresholdFileContentOwners.Name = "txtThresholdFileContentOwners"
        Me.txtThresholdFileContentOwners.ReadOnly = True
        Me.txtThresholdFileContentOwners.Size = New System.Drawing.Size(284, 20)
        Me.txtThresholdFileContentOwners.TabIndex = 30
        '
        'btnPreviousOnPageThresholdFile
        '
        Me.btnPreviousOnPageThresholdFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageThresholdFile.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageThresholdFile.Name = "btnPreviousOnPageThresholdFile"
        Me.btnPreviousOnPageThresholdFile.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageThresholdFile.TabIndex = 1
        Me.btnPreviousOnPageThresholdFile.Text = "Previous"
        Me.btnPreviousOnPageThresholdFile.UseVisualStyleBackColor = True
        '
        'btnExportThresholdFileToPerfmonTemplate
        '
        Me.btnExportThresholdFileToPerfmonTemplate.Location = New System.Drawing.Point(138, 109)
        Me.btnExportThresholdFileToPerfmonTemplate.Name = "btnExportThresholdFileToPerfmonTemplate"
        Me.btnExportThresholdFileToPerfmonTemplate.Size = New System.Drawing.Size(183, 21)
        Me.btnExportThresholdFileToPerfmonTemplate.TabIndex = 29
        Me.btnExportThresholdFileToPerfmonTemplate.Text = "Export to Perfmon template file..."
        Me.btnExportThresholdFileToPerfmonTemplate.UseVisualStyleBackColor = True
        '
        'btnNextOnPageThresholdFile
        '
        Me.btnNextOnPageThresholdFile.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageThresholdFile.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageThresholdFile.Name = "btnNextOnPageThresholdFile"
        Me.btnNextOnPageThresholdFile.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageThresholdFile.TabIndex = 0
        Me.btnNextOnPageThresholdFile.Text = "Next"
        Me.btnNextOnPageThresholdFile.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(22, 162)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(100, 34)
        Me.Label2.TabIndex = 28
        Me.Label2.Text = "Threshold file description:"
        '
        'ComboBoxAnalysisCollection
        '
        Me.ComboBoxAnalysisCollection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxAnalysisCollection.FormattingEnabled = True
        Me.ComboBoxAnalysisCollection.Location = New System.Drawing.Point(136, 81)
        Me.ComboBoxAnalysisCollection.Name = "ComboBoxAnalysisCollection"
        Me.ComboBoxAnalysisCollection.Size = New System.Drawing.Size(284, 21)
        Me.ComboBoxAnalysisCollection.Sorted = True
        Me.ComboBoxAnalysisCollection.TabIndex = 10
        '
        'txtAnalysisCollectionDescription
        '
        Me.txtAnalysisCollectionDescription.Location = New System.Drawing.Point(138, 162)
        Me.txtAnalysisCollectionDescription.Multiline = True
        Me.txtAnalysisCollectionDescription.Name = "txtAnalysisCollectionDescription"
        Me.txtAnalysisCollectionDescription.ReadOnly = True
        Me.txtAnalysisCollectionDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtAnalysisCollectionDescription.Size = New System.Drawing.Size(282, 58)
        Me.txtAnalysisCollectionDescription.TabIndex = 27
        '
        'lblThresholdFileName
        '
        Me.lblThresholdFileName.AutoSize = True
        Me.lblThresholdFileName.Location = New System.Drawing.Point(22, 138)
        Me.lblThresholdFileName.Name = "lblThresholdFileName"
        Me.lblThresholdFileName.Size = New System.Drawing.Size(102, 13)
        Me.lblThresholdFileName.TabIndex = 11
        Me.lblThresholdFileName.Text = "Threshold file name:"
        '
        'btnEditThresholdFile
        '
        Me.btnEditThresholdFile.Location = New System.Drawing.Point(326, 109)
        Me.btnEditThresholdFile.Name = "btnEditThresholdFile"
        Me.btnEditThresholdFile.Size = New System.Drawing.Size(94, 21)
        Me.btnEditThresholdFile.TabIndex = 23
        Me.btnEditThresholdFile.Text = "Edit..."
        Me.btnEditThresholdFile.UseVisualStyleBackColor = True
        '
        'lblAnalysisCollectionName
        '
        Me.lblAnalysisCollectionName.AutoSize = True
        Me.lblAnalysisCollectionName.Location = New System.Drawing.Point(22, 81)
        Me.lblAnalysisCollectionName.Name = "lblAnalysisCollectionName"
        Me.lblAnalysisCollectionName.Size = New System.Drawing.Size(89, 13)
        Me.lblAnalysisCollectionName.TabIndex = 24
        Me.lblAnalysisCollectionName.Text = "Threshold file title"
        '
        'txtThresholdFileName
        '
        Me.txtThresholdFileName.Location = New System.Drawing.Point(138, 136)
        Me.txtThresholdFileName.Name = "txtThresholdFileName"
        Me.txtThresholdFileName.ReadOnly = True
        Me.txtThresholdFileName.Size = New System.Drawing.Size(282, 20)
        Me.txtThresholdFileName.TabIndex = 25
        '
        'TabPageQuestions
        '
        Me.TabPageQuestions.Controls.Add(Me.txtQuestionAnswerResults)
        Me.TabPageQuestions.Controls.Add(Me.Label5)
        Me.TabPageQuestions.Controls.Add(Me.btnPreviousOnPageQuestions)
        Me.TabPageQuestions.Controls.Add(Me.btnNextOnPageQuestions)
        Me.TabPageQuestions.Controls.Add(Me.GroupBoxQuestionVariableNames)
        Me.TabPageQuestions.Location = New System.Drawing.Point(4, 25)
        Me.TabPageQuestions.Name = "TabPageQuestions"
        Me.TabPageQuestions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageQuestions.Size = New System.Drawing.Size(605, 430)
        Me.TabPageQuestions.TabIndex = 7
        Me.TabPageQuestions.Text = "Questions"
        Me.TabPageQuestions.UseVisualStyleBackColor = True
        '
        'txtQuestionAnswerResults
        '
        Me.txtQuestionAnswerResults.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtQuestionAnswerResults.Location = New System.Drawing.Point(15, 55)
        Me.txtQuestionAnswerResults.Multiline = True
        Me.txtQuestionAnswerResults.Name = "txtQuestionAnswerResults"
        Me.txtQuestionAnswerResults.ReadOnly = True
        Me.txtQuestionAnswerResults.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtQuestionAnswerResults.Size = New System.Drawing.Size(422, 143)
        Me.txtQuestionAnswerResults.TabIndex = 34
        Me.txtQuestionAnswerResults.Text = "Results:"
        Me.txtQuestionAnswerResults.WordWrap = False
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(6, 7)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(431, 60)
        Me.Label5.TabIndex = 33
        Me.Label5.Text = resources.GetString("Label5.Text")
        '
        'btnPreviousOnPageQuestions
        '
        Me.btnPreviousOnPageQuestions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageQuestions.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageQuestions.Name = "btnPreviousOnPageQuestions"
        Me.btnPreviousOnPageQuestions.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageQuestions.TabIndex = 18
        Me.btnPreviousOnPageQuestions.Text = "Previous"
        Me.btnPreviousOnPageQuestions.UseVisualStyleBackColor = True
        '
        'btnNextOnPageQuestions
        '
        Me.btnNextOnPageQuestions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageQuestions.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageQuestions.Name = "btnNextOnPageQuestions"
        Me.btnNextOnPageQuestions.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageQuestions.TabIndex = 17
        Me.btnNextOnPageQuestions.Text = "Next"
        Me.btnNextOnPageQuestions.UseVisualStyleBackColor = True
        '
        'GroupBoxQuestionVariableNames
        '
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.ListBoxQuestions)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.Label1)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.txtQuestion)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.Label3)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.ComboBoxQuestionAnswer)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.txtQuestionAnswer)
        Me.GroupBoxQuestionVariableNames.Controls.Add(Me.Label4)
        Me.GroupBoxQuestionVariableNames.Location = New System.Drawing.Point(9, 204)
        Me.GroupBoxQuestionVariableNames.Name = "GroupBoxQuestionVariableNames"
        Me.GroupBoxQuestionVariableNames.Size = New System.Drawing.Size(428, 220)
        Me.GroupBoxQuestionVariableNames.TabIndex = 16
        Me.GroupBoxQuestionVariableNames.TabStop = False
        Me.GroupBoxQuestionVariableNames.Text = "Questions:"
        '
        'ListBoxQuestions
        '
        Me.ListBoxQuestions.FormattingEnabled = True
        Me.ListBoxQuestions.Location = New System.Drawing.Point(6, 16)
        Me.ListBoxQuestions.Name = "ListBoxQuestions"
        Me.ListBoxQuestions.Size = New System.Drawing.Size(120, 186)
        Me.ListBoxQuestions.Sorted = True
        Me.ListBoxQuestions.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(132, 49)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Question:"
        '
        'txtQuestion
        '
        Me.txtQuestion.Location = New System.Drawing.Point(132, 65)
        Me.txtQuestion.Multiline = True
        Me.txtQuestion.Name = "txtQuestion"
        Me.txtQuestion.ReadOnly = True
        Me.txtQuestion.Size = New System.Drawing.Size(282, 137)
        Me.txtQuestion.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(132, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Answer:"
        '
        'ComboBoxQuestionAnswer
        '
        Me.ComboBoxQuestionAnswer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxQuestionAnswer.Enabled = False
        Me.ComboBoxQuestionAnswer.FormattingEnabled = True
        Me.ComboBoxQuestionAnswer.Items.AddRange(New Object() {"True", "False"})
        Me.ComboBoxQuestionAnswer.Location = New System.Drawing.Point(183, 15)
        Me.ComboBoxQuestionAnswer.Name = "ComboBoxQuestionAnswer"
        Me.ComboBoxQuestionAnswer.Size = New System.Drawing.Size(231, 21)
        Me.ComboBoxQuestionAnswer.TabIndex = 21
        Me.ComboBoxQuestionAnswer.Visible = False
        '
        'txtQuestionAnswer
        '
        Me.txtQuestionAnswer.Enabled = False
        Me.txtQuestionAnswer.Location = New System.Drawing.Point(183, 16)
        Me.txtQuestionAnswer.Name = "txtQuestionAnswer"
        Me.txtQuestionAnswer.Size = New System.Drawing.Size(231, 20)
        Me.txtQuestionAnswer.TabIndex = 19
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(132, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 13)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Answer:"
        '
        'TabPageAnalysisInterval
        '
        Me.TabPageAnalysisInterval.Controls.Add(Me.GroupBoxAllCounterStats)
        Me.TabPageAnalysisInterval.Controls.Add(Me.GroupBoxAnalysisInterval)
        Me.TabPageAnalysisInterval.Controls.Add(Me.btnPreviousOnPageAnalysisInterval)
        Me.TabPageAnalysisInterval.Controls.Add(Me.btnNextOnPageAnalysisInterval)
        Me.TabPageAnalysisInterval.Location = New System.Drawing.Point(4, 25)
        Me.TabPageAnalysisInterval.Name = "TabPageAnalysisInterval"
        Me.TabPageAnalysisInterval.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageAnalysisInterval.Size = New System.Drawing.Size(605, 430)
        Me.TabPageAnalysisInterval.TabIndex = 3
        Me.TabPageAnalysisInterval.Text = "Output Options"
        Me.TabPageAnalysisInterval.UseVisualStyleBackColor = True
        '
        'GroupBoxAllCounterStats
        '
        Me.GroupBoxAllCounterStats.Controls.Add(Me.lblAllCounterStats)
        Me.GroupBoxAllCounterStats.Controls.Add(Me.CheckBoxAllCounterstats)
        Me.GroupBoxAllCounterStats.Location = New System.Drawing.Point(6, 159)
        Me.GroupBoxAllCounterStats.Name = "GroupBoxAllCounterStats"
        Me.GroupBoxAllCounterStats.Size = New System.Drawing.Size(391, 108)
        Me.GroupBoxAllCounterStats.TabIndex = 19
        Me.GroupBoxAllCounterStats.TabStop = False
        Me.GroupBoxAllCounterStats.Text = "All Counter Stats"
        '
        'lblAllCounterStats
        '
        Me.lblAllCounterStats.Location = New System.Drawing.Point(6, 16)
        Me.lblAllCounterStats.Name = "lblAllCounterStats"
        Me.lblAllCounterStats.Size = New System.Drawing.Size(379, 59)
        Me.lblAllCounterStats.TabIndex = 1
        Me.lblAllCounterStats.Text = resources.GetString("lblAllCounterStats.Text")
        '
        'CheckBoxAllCounterstats
        '
        Me.CheckBoxAllCounterstats.AutoSize = True
        Me.CheckBoxAllCounterstats.Location = New System.Drawing.Point(9, 78)
        Me.CheckBoxAllCounterstats.Name = "CheckBoxAllCounterstats"
        Me.CheckBoxAllCounterstats.Size = New System.Drawing.Size(247, 17)
        Me.CheckBoxAllCounterstats.TabIndex = 0
        Me.CheckBoxAllCounterstats.Text = "Process all of the counters in the counter log(s)"
        Me.CheckBoxAllCounterstats.UseVisualStyleBackColor = True
        '
        'GroupBoxAnalysisInterval
        '
        Me.GroupBoxAnalysisInterval.Controls.Add(Me.lblStep2)
        Me.GroupBoxAnalysisInterval.Controls.Add(Me.lblSecs)
        Me.GroupBoxAnalysisInterval.Controls.Add(Me.ComboBoxInterval)
        Me.GroupBoxAnalysisInterval.Controls.Add(Me.lblRunInterval)
        Me.GroupBoxAnalysisInterval.Location = New System.Drawing.Point(6, 6)
        Me.GroupBoxAnalysisInterval.Name = "GroupBoxAnalysisInterval"
        Me.GroupBoxAnalysisInterval.Size = New System.Drawing.Size(391, 147)
        Me.GroupBoxAnalysisInterval.TabIndex = 18
        Me.GroupBoxAnalysisInterval.TabStop = False
        Me.GroupBoxAnalysisInterval.Text = "Analysis Interval"
        '
        'lblStep2
        '
        Me.lblStep2.Location = New System.Drawing.Point(6, 16)
        Me.lblStep2.Name = "lblStep2"
        Me.lblStep2.Size = New System.Drawing.Size(379, 100)
        Me.lblStep2.TabIndex = 17
        Me.lblStep2.Text = resources.GetString("lblStep2.Text")
        '
        'lblSecs
        '
        Me.lblSecs.AutoSize = True
        Me.lblSecs.Location = New System.Drawing.Point(338, 122)
        Me.lblSecs.Name = "lblSecs"
        Me.lblSecs.Size = New System.Drawing.Size(47, 13)
        Me.lblSecs.TabIndex = 15
        Me.lblSecs.Text = "seconds"
        '
        'ComboBoxInterval
        '
        Me.ComboBoxInterval.FormattingEnabled = True
        Me.ComboBoxInterval.Items.AddRange(New Object() {"AUTO", "10 minutes", "20 minutes", "30 minutes", "40 minutes", "50 minutes", "1 hour", "2 hours", "3 hours", "4 hours", "5 hours", "10 hours", "11 hours", "12 hours", "13 hours", "14 hours", "15 hours", "16 hours", "17 hours", "18 hours", "19 hours", "20 hours", "21 hours", "22 hours", "23 hours", "1 day", "2 days", "3 days", "4 days", "5 days", "6 days", "1 week"})
        Me.ComboBoxInterval.Location = New System.Drawing.Point(98, 119)
        Me.ComboBoxInterval.Name = "ComboBoxInterval"
        Me.ComboBoxInterval.Size = New System.Drawing.Size(234, 21)
        Me.ComboBoxInterval.TabIndex = 16
        Me.ComboBoxInterval.Text = "AUTO"
        '
        'lblRunInterval
        '
        Me.lblRunInterval.AutoSize = True
        Me.lblRunInterval.Location = New System.Drawing.Point(6, 122)
        Me.lblRunInterval.Name = "lblRunInterval"
        Me.lblRunInterval.Size = New System.Drawing.Size(86, 13)
        Me.lblRunInterval.TabIndex = 9
        Me.lblRunInterval.Text = "Analysis Interval:"
        '
        'btnPreviousOnPageAnalysisInterval
        '
        Me.btnPreviousOnPageAnalysisInterval.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageAnalysisInterval.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageAnalysisInterval.Name = "btnPreviousOnPageAnalysisInterval"
        Me.btnPreviousOnPageAnalysisInterval.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageAnalysisInterval.TabIndex = 1
        Me.btnPreviousOnPageAnalysisInterval.Text = "Previous"
        Me.btnPreviousOnPageAnalysisInterval.UseVisualStyleBackColor = True
        '
        'btnNextOnPageAnalysisInterval
        '
        Me.btnNextOnPageAnalysisInterval.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageAnalysisInterval.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageAnalysisInterval.Name = "btnNextOnPageAnalysisInterval"
        Me.btnNextOnPageAnalysisInterval.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageAnalysisInterval.TabIndex = 0
        Me.btnNextOnPageAnalysisInterval.Text = "Next"
        Me.btnNextOnPageAnalysisInterval.UseVisualStyleBackColor = True
        '
        'TabPageOutputOptions
        '
        Me.TabPageOutputOptions.Controls.Add(Me.GroupBoxXMLOutput)
        Me.TabPageOutputOptions.Controls.Add(Me.txtOutputDirectoryPath)
        Me.TabPageOutputOptions.Controls.Add(Me.btnOutputDirectoryBrowse)
        Me.TabPageOutputOptions.Controls.Add(Me.GroupBoxHTMLOutput)
        Me.TabPageOutputOptions.Controls.Add(Me.lblOutputDirectory)
        Me.TabPageOutputOptions.Controls.Add(Me.lblOutputDirectoryDescription)
        Me.TabPageOutputOptions.Controls.Add(Me.btnPreviousOnPageOutputOptions)
        Me.TabPageOutputOptions.Controls.Add(Me.btnNextOnPageOutputOptions)
        Me.TabPageOutputOptions.Location = New System.Drawing.Point(4, 25)
        Me.TabPageOutputOptions.Name = "TabPageOutputOptions"
        Me.TabPageOutputOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageOutputOptions.Size = New System.Drawing.Size(605, 430)
        Me.TabPageOutputOptions.TabIndex = 4
        Me.TabPageOutputOptions.Text = "File Output"
        Me.TabPageOutputOptions.UseVisualStyleBackColor = True
        '
        'GroupBoxXMLOutput
        '
        Me.GroupBoxXMLOutput.Controls.Add(Me.txtFullXMLOutputFileName)
        Me.GroupBoxXMLOutput.Controls.Add(Me.lblFullXMLOutputPathLabel)
        Me.GroupBoxXMLOutput.Controls.Add(Me.lblXMLFileNameLabel)
        Me.GroupBoxXMLOutput.Controls.Add(Me.txtXMLOutputFileName)
        Me.GroupBoxXMLOutput.Controls.Add(Me.CheckBoxXMLOutput)
        Me.GroupBoxXMLOutput.Location = New System.Drawing.Point(12, 250)
        Me.GroupBoxXMLOutput.Name = "GroupBoxXMLOutput"
        Me.GroupBoxXMLOutput.Size = New System.Drawing.Size(408, 109)
        Me.GroupBoxXMLOutput.TabIndex = 14
        Me.GroupBoxXMLOutput.TabStop = False
        Me.GroupBoxXMLOutput.Text = "XML Output"
        '
        'txtFullXMLOutputFileName
        '
        Me.txtFullXMLOutputFileName.Enabled = False
        Me.txtFullXMLOutputFileName.Location = New System.Drawing.Point(47, 81)
        Me.txtFullXMLOutputFileName.Name = "txtFullXMLOutputFileName"
        Me.txtFullXMLOutputFileName.ReadOnly = True
        Me.txtFullXMLOutputFileName.Size = New System.Drawing.Size(355, 20)
        Me.txtFullXMLOutputFileName.TabIndex = 17
        '
        'lblFullXMLOutputPathLabel
        '
        Me.lblFullXMLOutputPathLabel.AutoSize = True
        Me.lblFullXMLOutputPathLabel.Enabled = False
        Me.lblFullXMLOutputPathLabel.Location = New System.Drawing.Point(6, 84)
        Me.lblFullXMLOutputPathLabel.Name = "lblFullXMLOutputPathLabel"
        Me.lblFullXMLOutputPathLabel.Size = New System.Drawing.Size(35, 13)
        Me.lblFullXMLOutputPathLabel.TabIndex = 16
        Me.lblFullXMLOutputPathLabel.Text = "Path: "
        '
        'lblXMLFileNameLabel
        '
        Me.lblXMLFileNameLabel.AutoSize = True
        Me.lblXMLFileNameLabel.Enabled = False
        Me.lblXMLFileNameLabel.Location = New System.Drawing.Point(3, 39)
        Me.lblXMLFileNameLabel.Name = "lblXMLFileNameLabel"
        Me.lblXMLFileNameLabel.Size = New System.Drawing.Size(117, 13)
        Me.lblXMLFileNameLabel.TabIndex = 14
        Me.lblXMLFileNameLabel.Text = "XML Report File Name:"
        '
        'txtXMLOutputFileName
        '
        Me.txtXMLOutputFileName.Enabled = False
        Me.txtXMLOutputFileName.Location = New System.Drawing.Point(6, 55)
        Me.txtXMLOutputFileName.Name = "txtXMLOutputFileName"
        Me.txtXMLOutputFileName.Size = New System.Drawing.Size(396, 20)
        Me.txtXMLOutputFileName.TabIndex = 13
        '
        'CheckBoxXMLOutput
        '
        Me.CheckBoxXMLOutput.AutoSize = True
        Me.CheckBoxXMLOutput.Location = New System.Drawing.Point(9, 19)
        Me.CheckBoxXMLOutput.Name = "CheckBoxXMLOutput"
        Me.CheckBoxXMLOutput.Size = New System.Drawing.Size(100, 17)
        Me.CheckBoxXMLOutput.TabIndex = 12
        Me.CheckBoxXMLOutput.Text = "XML Document"
        Me.CheckBoxXMLOutput.UseVisualStyleBackColor = True
        '
        'txtOutputDirectoryPath
        '
        Me.txtOutputDirectoryPath.Location = New System.Drawing.Point(99, 108)
        Me.txtOutputDirectoryPath.Name = "txtOutputDirectoryPath"
        Me.txtOutputDirectoryPath.Size = New System.Drawing.Size(224, 20)
        Me.txtOutputDirectoryPath.TabIndex = 3
        Me.txtOutputDirectoryPath.Text = "[My Documents]\PAL Reports"
        '
        'btnOutputDirectoryBrowse
        '
        Me.btnOutputDirectoryBrowse.Location = New System.Drawing.Point(329, 106)
        Me.btnOutputDirectoryBrowse.Name = "btnOutputDirectoryBrowse"
        Me.btnOutputDirectoryBrowse.Size = New System.Drawing.Size(25, 23)
        Me.btnOutputDirectoryBrowse.TabIndex = 8
        Me.btnOutputDirectoryBrowse.Text = "..."
        Me.btnOutputDirectoryBrowse.UseVisualStyleBackColor = True
        '
        'GroupBoxHTMLOutput
        '
        Me.GroupBoxHTMLOutput.Controls.Add(Me.txtFullHTMLOutputFileName)
        Me.GroupBoxHTMLOutput.Controls.Add(Me.lblFullHTMLOutputDirectoryLabel)
        Me.GroupBoxHTMLOutput.Controls.Add(Me.lblHTMLReportFileName)
        Me.GroupBoxHTMLOutput.Controls.Add(Me.txtHTMLOutputFileName)
        Me.GroupBoxHTMLOutput.Controls.Add(Me.CheckBoxHTMLOutput)
        Me.GroupBoxHTMLOutput.Location = New System.Drawing.Point(12, 135)
        Me.GroupBoxHTMLOutput.Name = "GroupBoxHTMLOutput"
        Me.GroupBoxHTMLOutput.Size = New System.Drawing.Size(408, 109)
        Me.GroupBoxHTMLOutput.TabIndex = 13
        Me.GroupBoxHTMLOutput.TabStop = False
        Me.GroupBoxHTMLOutput.Text = "HTML Output"
        '
        'txtFullHTMLOutputFileName
        '
        Me.txtFullHTMLOutputFileName.Location = New System.Drawing.Point(44, 81)
        Me.txtFullHTMLOutputFileName.Name = "txtFullHTMLOutputFileName"
        Me.txtFullHTMLOutputFileName.ReadOnly = True
        Me.txtFullHTMLOutputFileName.Size = New System.Drawing.Size(355, 20)
        Me.txtFullHTMLOutputFileName.TabIndex = 15
        '
        'lblFullHTMLOutputDirectoryLabel
        '
        Me.lblFullHTMLOutputDirectoryLabel.AutoSize = True
        Me.lblFullHTMLOutputDirectoryLabel.Location = New System.Drawing.Point(3, 84)
        Me.lblFullHTMLOutputDirectoryLabel.Name = "lblFullHTMLOutputDirectoryLabel"
        Me.lblFullHTMLOutputDirectoryLabel.Size = New System.Drawing.Size(35, 13)
        Me.lblFullHTMLOutputDirectoryLabel.TabIndex = 14
        Me.lblFullHTMLOutputDirectoryLabel.Text = "Path: "
        '
        'lblHTMLReportFileName
        '
        Me.lblHTMLReportFileName.AutoSize = True
        Me.lblHTMLReportFileName.Location = New System.Drawing.Point(3, 39)
        Me.lblHTMLReportFileName.Name = "lblHTMLReportFileName"
        Me.lblHTMLReportFileName.Size = New System.Drawing.Size(128, 13)
        Me.lblHTMLReportFileName.TabIndex = 13
        Me.lblHTMLReportFileName.Text = " HTML Report File Name:"
        '
        'txtHTMLOutputFileName
        '
        Me.txtHTMLOutputFileName.Location = New System.Drawing.Point(3, 55)
        Me.txtHTMLOutputFileName.Name = "txtHTMLOutputFileName"
        Me.txtHTMLOutputFileName.Size = New System.Drawing.Size(396, 20)
        Me.txtHTMLOutputFileName.TabIndex = 12
        '
        'CheckBoxHTMLOutput
        '
        Me.CheckBoxHTMLOutput.AutoSize = True
        Me.CheckBoxHTMLOutput.Checked = True
        Me.CheckBoxHTMLOutput.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxHTMLOutput.Location = New System.Drawing.Point(6, 19)
        Me.CheckBoxHTMLOutput.Name = "CheckBoxHTMLOutput"
        Me.CheckBoxHTMLOutput.Size = New System.Drawing.Size(91, 17)
        Me.CheckBoxHTMLOutput.TabIndex = 11
        Me.CheckBoxHTMLOutput.Text = "HTML Report"
        Me.CheckBoxHTMLOutput.UseVisualStyleBackColor = True
        '
        'lblOutputDirectory
        '
        Me.lblOutputDirectory.AutoSize = True
        Me.lblOutputDirectory.Location = New System.Drawing.Point(6, 111)
        Me.lblOutputDirectory.Name = "lblOutputDirectory"
        Me.lblOutputDirectory.Size = New System.Drawing.Size(87, 13)
        Me.lblOutputDirectory.TabIndex = 9
        Me.lblOutputDirectory.Text = "Output Directory:"
        '
        'lblOutputDirectoryDescription
        '
        Me.lblOutputDirectoryDescription.Location = New System.Drawing.Point(6, 12)
        Me.lblOutputDirectoryDescription.Name = "lblOutputDirectoryDescription"
        Me.lblOutputDirectoryDescription.Size = New System.Drawing.Size(414, 91)
        Me.lblOutputDirectoryDescription.TabIndex = 10
        Me.lblOutputDirectoryDescription.Text = resources.GetString("lblOutputDirectoryDescription.Text")
        '
        'btnPreviousOnPageOutputOptions
        '
        Me.btnPreviousOnPageOutputOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageOutputOptions.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageOutputOptions.Name = "btnPreviousOnPageOutputOptions"
        Me.btnPreviousOnPageOutputOptions.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageOutputOptions.TabIndex = 1
        Me.btnPreviousOnPageOutputOptions.Text = "Previous"
        Me.btnPreviousOnPageOutputOptions.UseVisualStyleBackColor = True
        '
        'btnNextOnPageOutputOptions
        '
        Me.btnNextOnPageOutputOptions.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageOutputOptions.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageOutputOptions.Name = "btnNextOnPageOutputOptions"
        Me.btnNextOnPageOutputOptions.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageOutputOptions.TabIndex = 0
        Me.btnNextOnPageOutputOptions.Text = "Next"
        Me.btnNextOnPageOutputOptions.UseVisualStyleBackColor = True
        '
        'TabPageQueue
        '
        Me.TabPageQueue.Controls.Add(Me.btnRemoveLastFromQueue)
        Me.TabPageQueue.Controls.Add(Me.GroupBoxQueue)
        Me.TabPageQueue.Controls.Add(Me.btnPreviousOnPageQueue)
        Me.TabPageQueue.Controls.Add(Me.lblStep5)
        Me.TabPageQueue.Controls.Add(Me.btnNextOnPageQueue)
        Me.TabPageQueue.Location = New System.Drawing.Point(4, 25)
        Me.TabPageQueue.Name = "TabPageQueue"
        Me.TabPageQueue.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageQueue.Size = New System.Drawing.Size(605, 430)
        Me.TabPageQueue.TabIndex = 5
        Me.TabPageQueue.Text = "Queue"
        Me.TabPageQueue.UseVisualStyleBackColor = True
        '
        'btnRemoveLastFromQueue
        '
        Me.btnRemoveLastFromQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRemoveLastFromQueue.Location = New System.Drawing.Point(6, 380)
        Me.btnRemoveLastFromQueue.Name = "btnRemoveLastFromQueue"
        Me.btnRemoveLastFromQueue.Size = New System.Drawing.Size(142, 23)
        Me.btnRemoveLastFromQueue.TabIndex = 24
        Me.btnRemoveLastFromQueue.Text = "Remove Item From Queue"
        Me.btnRemoveLastFromQueue.UseVisualStyleBackColor = True
        '
        'GroupBoxQueue
        '
        Me.GroupBoxQueue.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxQueue.Controls.Add(Me.txtBatchText)
        Me.GroupBoxQueue.Location = New System.Drawing.Point(9, 60)
        Me.GroupBoxQueue.Name = "GroupBoxQueue"
        Me.GroupBoxQueue.Size = New System.Drawing.Size(590, 314)
        Me.GroupBoxQueue.TabIndex = 23
        Me.GroupBoxQueue.TabStop = False
        Me.GroupBoxQueue.Text = "Queue"
        '
        'txtBatchText
        '
        Me.txtBatchText.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtBatchText.Location = New System.Drawing.Point(6, 19)
        Me.txtBatchText.Multiline = True
        Me.txtBatchText.Name = "txtBatchText"
        Me.txtBatchText.ReadOnly = True
        Me.txtBatchText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtBatchText.Size = New System.Drawing.Size(578, 289)
        Me.txtBatchText.TabIndex = 18
        Me.txtBatchText.WordWrap = False
        '
        'btnPreviousOnPageQueue
        '
        Me.btnPreviousOnPageQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageQueue.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageQueue.Name = "btnPreviousOnPageQueue"
        Me.btnPreviousOnPageQueue.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageQueue.TabIndex = 1
        Me.btnPreviousOnPageQueue.Text = "Previous"
        Me.btnPreviousOnPageQueue.UseVisualStyleBackColor = True
        '
        'lblStep5
        '
        Me.lblStep5.Location = New System.Drawing.Point(6, 12)
        Me.lblStep5.Name = "lblStep5"
        Me.lblStep5.Size = New System.Drawing.Size(593, 35)
        Me.lblStep5.TabIndex = 20
        Me.lblStep5.Text = "The queue (batch file) below will execute the PAL.ps1 (Powershell) script which w" & _
            "ill analyze your log(s). The batch file will be created at %temp%\PAL_{GUID}.bat" & _
            "."
        '
        'btnNextOnPageQueue
        '
        Me.btnNextOnPageQueue.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageQueue.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageQueue.Name = "btnNextOnPageQueue"
        Me.btnNextOnPageQueue.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageQueue.TabIndex = 0
        Me.btnNextOnPageQueue.Text = "Next"
        Me.btnNextOnPageQueue.UseVisualStyleBackColor = True
        '
        'TabPageExecute
        '
        Me.TabPageExecute.Controls.Add(Me.GroupBoxNumberOfThreads)
        Me.TabPageExecute.Controls.Add(Me.RadExecuteQueue)
        Me.TabPageExecute.Controls.Add(Me.RadExecuteAndRunWizardAgain)
        Me.TabPageExecute.Controls.Add(Me.RadAddMoreToQueue)
        Me.TabPageExecute.Controls.Add(Me.lblExecute)
        Me.TabPageExecute.Controls.Add(Me.btnPreviousOnPageExecute)
        Me.TabPageExecute.Controls.Add(Me.btnNextOnPageExecute)
        Me.TabPageExecute.Location = New System.Drawing.Point(4, 25)
        Me.TabPageExecute.Name = "TabPageExecute"
        Me.TabPageExecute.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExecute.Size = New System.Drawing.Size(605, 430)
        Me.TabPageExecute.TabIndex = 6
        Me.TabPageExecute.Text = "Execute"
        Me.TabPageExecute.UseVisualStyleBackColor = True
        '
        'GroupBoxNumberOfThreads
        '
        Me.GroupBoxNumberOfThreads.Controls.Add(Me.lblNumberOfThreads)
        Me.GroupBoxNumberOfThreads.Controls.Add(Me.CheckBoxRunAsLowPriority)
        Me.GroupBoxNumberOfThreads.Controls.Add(Me.lblThreadingHelp)
        Me.GroupBoxNumberOfThreads.Controls.Add(Me.TextBoxExecNumberOfThreads)
        Me.GroupBoxNumberOfThreads.Location = New System.Drawing.Point(6, 165)
        Me.GroupBoxNumberOfThreads.Name = "GroupBoxNumberOfThreads"
        Me.GroupBoxNumberOfThreads.Size = New System.Drawing.Size(447, 112)
        Me.GroupBoxNumberOfThreads.TabIndex = 27
        Me.GroupBoxNumberOfThreads.TabStop = False
        Me.GroupBoxNumberOfThreads.Text = "Threading"
        '
        'lblNumberOfThreads
        '
        Me.lblNumberOfThreads.AutoSize = True
        Me.lblNumberOfThreads.Location = New System.Drawing.Point(6, 27)
        Me.lblNumberOfThreads.Name = "lblNumberOfThreads"
        Me.lblNumberOfThreads.Size = New System.Drawing.Size(151, 13)
        Me.lblNumberOfThreads.TabIndex = 25
        Me.lblNumberOfThreads.Text = "Number of processing threads:"
        '
        'CheckBoxRunAsLowPriority
        '
        Me.CheckBoxRunAsLowPriority.AutoSize = True
        Me.CheckBoxRunAsLowPriority.Checked = True
        Me.CheckBoxRunAsLowPriority.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxRunAsLowPriority.Location = New System.Drawing.Point(258, 27)
        Me.CheckBoxRunAsLowPriority.Name = "CheckBoxRunAsLowPriority"
        Me.CheckBoxRunAsLowPriority.Size = New System.Drawing.Size(183, 17)
        Me.CheckBoxRunAsLowPriority.TabIndex = 23
        Me.CheckBoxRunAsLowPriority.Text = "Execute as a low priority process."
        Me.CheckBoxRunAsLowPriority.UseVisualStyleBackColor = True
        '
        'lblThreadingHelp
        '
        Me.lblThreadingHelp.Location = New System.Drawing.Point(6, 47)
        Me.lblThreadingHelp.Name = "lblThreadingHelp"
        Me.lblThreadingHelp.Size = New System.Drawing.Size(435, 54)
        Me.lblThreadingHelp.TabIndex = 26
        Me.lblThreadingHelp.Text = resources.GetString("lblThreadingHelp.Text")
        '
        'TextBoxExecNumberOfThreads
        '
        Me.TextBoxExecNumberOfThreads.Location = New System.Drawing.Point(163, 24)
        Me.TextBoxExecNumberOfThreads.Name = "TextBoxExecNumberOfThreads"
        Me.TextBoxExecNumberOfThreads.Size = New System.Drawing.Size(29, 20)
        Me.TextBoxExecNumberOfThreads.TabIndex = 24
        Me.TextBoxExecNumberOfThreads.Text = "1"
        '
        'RadExecuteQueue
        '
        Me.RadExecuteQueue.AutoSize = True
        Me.RadExecuteQueue.Checked = True
        Me.RadExecuteQueue.Location = New System.Drawing.Point(9, 73)
        Me.RadExecuteQueue.Name = "RadExecuteQueue"
        Me.RadExecuteQueue.Size = New System.Drawing.Size(301, 17)
        Me.RadExecuteQueue.TabIndex = 10
        Me.RadExecuteQueue.TabStop = True
        Me.RadExecuteQueue.Text = "Execute: Execute what is currently in the queue/batch file."
        Me.RadExecuteQueue.UseVisualStyleBackColor = True
        '
        'RadExecuteAndRunWizardAgain
        '
        Me.RadExecuteAndRunWizardAgain.Location = New System.Drawing.Point(9, 113)
        Me.RadExecuteAndRunWizardAgain.Name = "RadExecuteAndRunWizardAgain"
        Me.RadExecuteAndRunWizardAgain.Size = New System.Drawing.Size(433, 46)
        Me.RadExecuteAndRunWizardAgain.TabIndex = 9
        Me.RadExecuteAndRunWizardAgain.Text = "Execute and Restart: Execute the current queue/batch file and create a new one. M" & _
            "ore than one instance of PAL running at the same time is supported."
        Me.RadExecuteAndRunWizardAgain.UseVisualStyleBackColor = True
        '
        'RadAddMoreToQueue
        '
        Me.RadAddMoreToQueue.AutoSize = True
        Me.RadAddMoreToQueue.Location = New System.Drawing.Point(9, 96)
        Me.RadAddMoreToQueue.Name = "RadAddMoreToQueue"
        Me.RadAddMoreToQueue.Size = New System.Drawing.Size(425, 17)
        Me.RadAddMoreToQueue.TabIndex = 8
        Me.RadAddMoreToQueue.Text = "Add to Queue: Do not execute yet. I want to add more items to the queue/batch fil" & _
            "e. "
        Me.RadAddMoreToQueue.UseVisualStyleBackColor = True
        '
        'lblExecute
        '
        Me.lblExecute.Location = New System.Drawing.Point(6, 11)
        Me.lblExecute.Name = "lblExecute"
        Me.lblExecute.Size = New System.Drawing.Size(440, 59)
        Me.lblExecute.TabIndex = 6
        Me.lblExecute.Text = "Thank you. You have provided enough information to process the log. Click Finish " & _
            "to begin executing the analysis. Optionally, you can run this wizard again to re" & _
            "use some of the same settings." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnPreviousOnPageExecute
        '
        Me.btnPreviousOnPageExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreviousOnPageExecute.Location = New System.Drawing.Point(443, 401)
        Me.btnPreviousOnPageExecute.Name = "btnPreviousOnPageExecute"
        Me.btnPreviousOnPageExecute.Size = New System.Drawing.Size(75, 23)
        Me.btnPreviousOnPageExecute.TabIndex = 5
        Me.btnPreviousOnPageExecute.Text = "Previous"
        Me.btnPreviousOnPageExecute.UseVisualStyleBackColor = True
        '
        'btnNextOnPageExecute
        '
        Me.btnNextOnPageExecute.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNextOnPageExecute.Location = New System.Drawing.Point(524, 401)
        Me.btnNextOnPageExecute.Name = "btnNextOnPageExecute"
        Me.btnNextOnPageExecute.Size = New System.Drawing.Size(75, 23)
        Me.btnNextOnPageExecute.TabIndex = 4
        Me.btnNextOnPageExecute.Text = "Finish"
        Me.btnNextOnPageExecute.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        '
        'SaveFileDialog1
        '
        '
        'ListBoxTemp
        '
        Me.ListBoxTemp.FormattingEnabled = True
        Me.ListBoxTemp.Location = New System.Drawing.Point(6, 16)
        Me.ListBoxTemp.Name = "ListBoxTemp"
        Me.ListBoxTemp.Size = New System.Drawing.Size(120, 121)
        Me.ListBoxTemp.TabIndex = 12
        '
        'lblQuestion
        '
        Me.lblQuestion.AutoSize = True
        Me.lblQuestion.Location = New System.Drawing.Point(132, 16)
        Me.lblQuestion.Name = "lblQuestion"
        Me.lblQuestion.Size = New System.Drawing.Size(52, 13)
        Me.lblQuestion.TabIndex = 22
        Me.lblQuestion.Text = "Question:"
        '
        'txtQuestionDELETE
        '
        Me.txtQuestionDELETE.Location = New System.Drawing.Point(132, 32)
        Me.txtQuestionDELETE.Multiline = True
        Me.txtQuestionDELETE.Name = "txtQuestionDELETE"
        Me.txtQuestionDELETE.Size = New System.Drawing.Size(282, 77)
        Me.txtQuestionDELETE.TabIndex = 17
        '
        'lblQuestionAnswerValueString
        '
        Me.lblQuestionAnswerValueString.AutoSize = True
        Me.lblQuestionAnswerValueString.Location = New System.Drawing.Point(132, 123)
        Me.lblQuestionAnswerValueString.Name = "lblQuestionAnswerValueString"
        Me.lblQuestionAnswerValueString.Size = New System.Drawing.Size(45, 13)
        Me.lblQuestionAnswerValueString.TabIndex = 18
        Me.lblQuestionAnswerValueString.Text = "Answer:"
        '
        'ComboBoxQuestionAnswerDELETE
        '
        Me.ComboBoxQuestionAnswerDELETE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBoxQuestionAnswerDELETE.FormattingEnabled = True
        Me.ComboBoxQuestionAnswerDELETE.Items.AddRange(New Object() {"True", "False"})
        Me.ComboBoxQuestionAnswerDELETE.Location = New System.Drawing.Point(183, 115)
        Me.ComboBoxQuestionAnswerDELETE.Name = "ComboBoxQuestionAnswerDELETE"
        Me.ComboBoxQuestionAnswerDELETE.Size = New System.Drawing.Size(231, 21)
        Me.ComboBoxQuestionAnswerDELETE.TabIndex = 21
        '
        'txtQuestionAnswerDELETE
        '
        Me.txtQuestionAnswerDELETE.Enabled = False
        Me.txtQuestionAnswerDELETE.Location = New System.Drawing.Point(183, 116)
        Me.txtQuestionAnswerDELETE.Name = "txtQuestionAnswerDELETE"
        Me.txtQuestionAnswerDELETE.Size = New System.Drawing.Size(171, 20)
        Me.txtQuestionAnswerDELETE.TabIndex = 19
        '
        'lblQuestionAnswerValueBoolean
        '
        Me.lblQuestionAnswerValueBoolean.AutoSize = True
        Me.lblQuestionAnswerValueBoolean.Location = New System.Drawing.Point(132, 123)
        Me.lblQuestionAnswerValueBoolean.Name = "lblQuestionAnswerValueBoolean"
        Me.lblQuestionAnswerValueBoolean.Size = New System.Drawing.Size(45, 13)
        Me.lblQuestionAnswerValueBoolean.TabIndex = 20
        Me.lblQuestionAnswerValueBoolean.Text = "Answer:"
        '
        'btnAutoDetectThresholdFile
        '
        Me.btnAutoDetectThresholdFile.Location = New System.Drawing.Point(426, 79)
        Me.btnAutoDetectThresholdFile.Name = "btnAutoDetectThresholdFile"
        Me.btnAutoDetectThresholdFile.Size = New System.Drawing.Size(92, 23)
        Me.btnAutoDetectThresholdFile.TabIndex = 57
        Me.btnAutoDetectThresholdFile.Text = "Auto-detect"
        Me.btnAutoDetectThresholdFile.UseVisualStyleBackColor = True
        '
        'frmPALExecutionWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(637, 483)
        Me.Controls.Add(Me.TabControl)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(529, 498)
        Me.Name = "frmPALExecutionWizard"
        Me.Text = "PAL Wizard"
        Me.TabControl.ResumeLayout(False)
        Me.TabPageWelcome.ResumeLayout(False)
        Me.TabPageWelcome.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPageCounterLog.ResumeLayout(False)
        Me.TabPageCounterLog.PerformLayout()
        Me.GroupBoxBeginTimeEndTime.ResumeLayout(False)
        Me.GroupBoxBeginTimeEndTime.PerformLayout()
        Me.TabPageThresholdFile.ResumeLayout(False)
        Me.TabPageThresholdFile.PerformLayout()
        Me.TabPageQuestions.ResumeLayout(False)
        Me.TabPageQuestions.PerformLayout()
        Me.GroupBoxQuestionVariableNames.ResumeLayout(False)
        Me.GroupBoxQuestionVariableNames.PerformLayout()
        Me.TabPageAnalysisInterval.ResumeLayout(False)
        Me.GroupBoxAllCounterStats.ResumeLayout(False)
        Me.GroupBoxAllCounterStats.PerformLayout()
        Me.GroupBoxAnalysisInterval.ResumeLayout(False)
        Me.GroupBoxAnalysisInterval.PerformLayout()
        Me.TabPageOutputOptions.ResumeLayout(False)
        Me.TabPageOutputOptions.PerformLayout()
        Me.GroupBoxXMLOutput.ResumeLayout(False)
        Me.GroupBoxXMLOutput.PerformLayout()
        Me.GroupBoxHTMLOutput.ResumeLayout(False)
        Me.GroupBoxHTMLOutput.PerformLayout()
        Me.TabPageQueue.ResumeLayout(False)
        Me.GroupBoxQueue.ResumeLayout(False)
        Me.GroupBoxQueue.PerformLayout()
        Me.TabPageExecute.ResumeLayout(False)
        Me.TabPageExecute.PerformLayout()
        Me.GroupBoxNumberOfThreads.ResumeLayout(False)
        Me.GroupBoxNumberOfThreads.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl As System.Windows.Forms.TabControl
    Friend WithEvents TabPageWelcome As System.Windows.Forms.TabPage
    Friend WithEvents TabPageCounterLog As System.Windows.Forms.TabPage
    Friend WithEvents lblWelcome As System.Windows.Forms.Label
    Friend WithEvents TabPageThresholdFile As System.Windows.Forms.TabPage
    Friend WithEvents TabPageAnalysisInterval As System.Windows.Forms.TabPage
    Friend WithEvents TabPageOutputOptions As System.Windows.Forms.TabPage
    Friend WithEvents TabPageQueue As System.Windows.Forms.TabPage
    Friend WithEvents btnNextOnPageWelcome As System.Windows.Forms.Button
    Friend WithEvents btnPreviousOnPageCounterLog As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageCounterLog As System.Windows.Forms.Button
    Friend WithEvents btnPreviousOnPageThresholdFile As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageThresholdFile As System.Windows.Forms.Button
    Friend WithEvents btnPreviousOnPageAnalysisInterval As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageAnalysisInterval As System.Windows.Forms.Button
    Friend WithEvents btnPreviousOnPageOutputOptions As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageOutputOptions As System.Windows.Forms.Button
    Friend WithEvents ComboBoxRunLogFile As System.Windows.Forms.ComboBox
    Friend WithEvents lblStep1 As System.Windows.Forms.Label
    Friend WithEvents lblRunLogPath As System.Windows.Forms.Label
    Friend WithEvents btnFileBrowse As System.Windows.Forms.Button
    Friend WithEvents lblStep3 As System.Windows.Forms.Label
    Friend WithEvents lblThresholdFileContentOwners As System.Windows.Forms.Label
    Friend WithEvents txtThresholdFileContentOwners As System.Windows.Forms.TextBox
    Friend WithEvents btnExportThresholdFileToPerfmonTemplate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtAnalysisCollectionDescription As System.Windows.Forms.TextBox
    Friend WithEvents txtThresholdFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblAnalysisCollectionName As System.Windows.Forms.Label
    Friend WithEvents btnEditThresholdFile As System.Windows.Forms.Button
    Friend WithEvents lblThresholdFileName As System.Windows.Forms.Label
    Friend WithEvents ComboBoxAnalysisCollection As System.Windows.Forms.ComboBox
    Friend WithEvents lblStep2 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxInterval As System.Windows.Forms.ComboBox
    Friend WithEvents lblRunInterval As System.Windows.Forms.Label
    Friend WithEvents lblSecs As System.Windows.Forms.Label
    Friend WithEvents btnPreviousOnPageQueue As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageQueue As System.Windows.Forms.Button
    Friend WithEvents TabPageExecute As System.Windows.Forms.TabPage
    Friend WithEvents lblExecute As System.Windows.Forms.Label
    Friend WithEvents btnPreviousOnPageExecute As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageExecute As System.Windows.Forms.Button
    Friend WithEvents txtOutputDirectoryPath As System.Windows.Forms.TextBox
    Friend WithEvents lblStep5 As System.Windows.Forms.Label
    Friend WithEvents txtBatchText As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnOutputDirectoryBrowse As System.Windows.Forms.Button
    Friend WithEvents lblOutputDirectoryDescription As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents CheckBoxXMLOutput As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBoxHTMLOutput As System.Windows.Forms.CheckBox
    Friend WithEvents lblPAL As System.Windows.Forms.Label
    Friend WithEvents LinkLabelURL As System.Windows.Forms.LinkLabel
    Friend WithEvents lblQuestionsOrProblems As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents LinkLabelLicense As System.Windows.Forms.LinkLabel
    Friend WithEvents GroupBoxQueue As System.Windows.Forms.GroupBox
    Friend WithEvents RadExecuteQueue As System.Windows.Forms.RadioButton
    Friend WithEvents RadExecuteAndRunWizardAgain As System.Windows.Forms.RadioButton
    Friend WithEvents RadAddMoreToQueue As System.Windows.Forms.RadioButton
    Friend WithEvents CheckBoxRunAsLowPriority As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxHTMLOutput As System.Windows.Forms.GroupBox
    Friend WithEvents txtHTMLOutputFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblHTMLReportFileName As System.Windows.Forms.Label
    Friend WithEvents lblOutputDirectory As System.Windows.Forms.Label
    Friend WithEvents GroupBoxXMLOutput As System.Windows.Forms.GroupBox
    Friend WithEvents txtXMLOutputFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblFullHTMLOutputDirectoryLabel As System.Windows.Forms.Label
    Friend WithEvents lblFullXMLOutputPathLabel As System.Windows.Forms.Label
    Friend WithEvents lblXMLFileNameLabel As System.Windows.Forms.Label
    Friend WithEvents txtFullXMLOutputFileName As System.Windows.Forms.TextBox
    Friend WithEvents txtFullHTMLOutputFileName As System.Windows.Forms.TextBox
    Friend WithEvents btnRemoveLastFromQueue As System.Windows.Forms.Button
    Friend WithEvents GroupBoxBeginTimeEndTime As System.Windows.Forms.GroupBox
    Friend WithEvents CheckBoxRestrictToADateTimeRange As System.Windows.Forms.CheckBox
    Friend WithEvents lblEndTime As System.Windows.Forms.Label
    Friend WithEvents lblBeginTime As System.Windows.Forms.Label
    Friend WithEvents DateTimePickerEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePickerBeginTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDateTimeRangeNote As System.Windows.Forms.Label
    Friend WithEvents lblv2 As System.Windows.Forms.Label
    Friend WithEvents TabPageQuestions As System.Windows.Forms.TabPage
    Friend WithEvents GroupBoxQuestionVariableNames As System.Windows.Forms.GroupBox
    Friend WithEvents ListBoxQuestions As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtQuestion As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBoxQuestionAnswer As System.Windows.Forms.ComboBox
    Friend WithEvents txtQuestionAnswer As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ListBoxTemp As System.Windows.Forms.ListBox
    Friend WithEvents lblQuestion As System.Windows.Forms.Label
    Friend WithEvents txtQuestionDELETE As System.Windows.Forms.TextBox
    Friend WithEvents lblQuestionAnswerValueString As System.Windows.Forms.Label
    Friend WithEvents ComboBoxQuestionAnswerDELETE As System.Windows.Forms.ComboBox
    Friend WithEvents txtQuestionAnswerDELETE As System.Windows.Forms.TextBox
    Friend WithEvents lblQuestionAnswerValueBoolean As System.Windows.Forms.Label
    Friend WithEvents btnPreviousOnPageQuestions As System.Windows.Forms.Button
    Friend WithEvents btnNextOnPageQuestions As System.Windows.Forms.Button
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBoxAnalysisInterval As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxAllCounterStats As System.Windows.Forms.GroupBox
    Friend WithEvents lblAllCounterStats As System.Windows.Forms.Label
    Friend WithEvents CheckBoxAllCounterstats As System.Windows.Forms.CheckBox
    Friend WithEvents lblAboutTheAuthor As System.Windows.Forms.Label
    Friend WithEvents LinkLabelClinthBlog As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabelEmailClint As System.Windows.Forms.LinkLabel
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents LinkLabelAboutTheAuthorClintH As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabelSupport As System.Windows.Forms.LinkLabel
    Friend WithEvents lblThresholdFileInheritance As System.Windows.Forms.Label
    Friend WithEvents ListBoxOfThresholdFileInheritance As System.Windows.Forms.ListBox
    Friend WithEvents btnThresholdFileInheritanceRemove As System.Windows.Forms.Button
    Friend WithEvents btnThresholdFileInheritanceAdd As System.Windows.Forms.Button
    Friend WithEvents btnListBoxOfThresholdFileInheritanceDown As System.Windows.Forms.Button
    Friend WithEvents btnListBoxOfThresholdFileInheritanceUp As System.Windows.Forms.Button
    Friend WithEvents lblThresholdFileInheritanceNote As System.Windows.Forms.Label
    Friend WithEvents ListBoxOfThresholdFileInheritanceRecursion As System.Windows.Forms.ListBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblNumberOfThreads As System.Windows.Forms.Label
    Friend WithEvents TextBoxExecNumberOfThreads As System.Windows.Forms.TextBox
    Friend WithEvents lblThreadingHelp As System.Windows.Forms.Label
    Friend WithEvents GroupBoxNumberOfThreads As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuestionAnswerResults As System.Windows.Forms.TextBox
    Friend WithEvents btnAutoDetectThresholdFile As System.Windows.Forms.Button

End Class
