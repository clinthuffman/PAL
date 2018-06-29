Imports System.Xml
Imports System.IO
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPALEditor
    Inherits System.Windows.Forms.Form
    Public sThresholdFilePath As String
    'Dim g_ThresholdFilePath As String
    Dim g_xmlThresholdFile As XmlDocument
    Dim g_XMLRoot As XmlElement
    Dim g_Analysis() As oAnalysis
    Public g_oXMLSelectedAnalysis As XmlNode
    Dim g_oXMLSelectedDataSourceCounter As XmlNode
    Dim g_oXMLSelectedThreshold As XmlNode
    Dim g_oXMLSelectedChart As XmlNode
    Dim g_LastSelectedTreeViewNode As TreeNode
    Dim g_LastSelectedTreeViewName As String
    Dim g_LastSelectedCheckBoxCategory As String
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Public ofrmPALExecutionWizard As frmPALExecutionWizard
    Friend WithEvents btnEditThresholdProperties As System.Windows.Forms.Button
    Friend LastUsedComputerName As String
    Friend g_ThresholdFilePath, g_ThresholdFileInheritanceFileNames, g_sThresholdFileTitle, g_sThresholdFileVersion, g_sThresholdFileDescription, g_sThresholdFileContentOwners, g_sThresholdFileFeedbackEmailAddresses
    Friend WithEvents lblHtmlRenderedDescription As System.Windows.Forms.Label
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPALEditor))
        Me.txtAnalysisName = New System.Windows.Forms.TextBox
        Me.lblName = New System.Windows.Forms.Label
        Me.lblAnalysisCounter = New System.Windows.Forms.Label
        Me.txtAnalysisCounter = New System.Windows.Forms.TextBox
        Me.CheckBoxAnalysisEnabled = New System.Windows.Forms.CheckBox
        Me.ComboBoxCategory = New System.Windows.Forms.ComboBox
        Me.lblCategory = New System.Windows.Forms.Label
        Me.ListBoxDataSourceCounters = New System.Windows.Forms.ListBox
        Me.GroupBoxDataSourceCounters = New System.Windows.Forms.GroupBox
        Me.btnDataSourceCountersEdit = New System.Windows.Forms.Button
        Me.btnDataSourceCountersRemove = New System.Windows.Forms.Button
        Me.btnDataSourceCountersAdd = New System.Windows.Forms.Button
        Me.LabelAnalysisNames = New System.Windows.Forms.Label
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.NewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.OpenToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SaveAsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.btnAnalysisNew = New System.Windows.Forms.Button
        Me.btnAnalysisDelete = New System.Windows.Forms.Button
        Me.TreeViewAnalysis = New System.Windows.Forms.TreeView
        Me.lblAnalysisDescription = New System.Windows.Forms.Label
        Me.txtAnalysisDescription = New System.Windows.Forms.TextBox
        Me.CheckBoxAnalysisDescriptionWordWrap = New System.Windows.Forms.CheckBox
        Me.btnUpdate = New System.Windows.Forms.Button
        Me.btnEditChart = New System.Windows.Forms.Button
        Me.GroupBoxThresholds = New System.Windows.Forms.GroupBox
        Me.btnThresholdDelete = New System.Windows.Forms.Button
        Me.btnThresholdEdit = New System.Windows.Forms.Button
        Me.btnThresholdNew = New System.Windows.Forms.Button
        Me.ListBoxThresholds = New System.Windows.Forms.ListBox
        Me.GroupBoxAnalysis = New System.Windows.Forms.GroupBox
        Me.lblHtmlRenderedDescription = New System.Windows.Forms.Label
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser
        Me.btnEditQuestions = New System.Windows.Forms.Button
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.btnEditThresholdProperties = New System.Windows.Forms.Button
        Me.GroupBoxDataSourceCounters.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.GroupBoxThresholds.SuspendLayout()
        Me.GroupBoxAnalysis.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtAnalysisName
        '
        Me.txtAnalysisName.Location = New System.Drawing.Point(108, 32)
        Me.txtAnalysisName.Name = "txtAnalysisName"
        Me.txtAnalysisName.Size = New System.Drawing.Size(242, 20)
        Me.txtAnalysisName.TabIndex = 1
        '
        'lblName
        '
        Me.lblName.AutoSize = True
        Me.lblName.Location = New System.Drawing.Point(3, 39)
        Me.lblName.Name = "lblName"
        Me.lblName.Size = New System.Drawing.Size(38, 13)
        Me.lblName.TabIndex = 2
        Me.lblName.Text = "Name:"
        '
        'lblAnalysisCounter
        '
        Me.lblAnalysisCounter.AutoSize = True
        Me.lblAnalysisCounter.Location = New System.Drawing.Point(3, 65)
        Me.lblAnalysisCounter.Name = "lblAnalysisCounter"
        Me.lblAnalysisCounter.Size = New System.Drawing.Size(99, 13)
        Me.lblAnalysisCounter.TabIndex = 4
        Me.lblAnalysisCounter.Text = "Counter to Analyze:"
        '
        'txtAnalysisCounter
        '
        Me.txtAnalysisCounter.Location = New System.Drawing.Point(108, 58)
        Me.txtAnalysisCounter.Name = "txtAnalysisCounter"
        Me.txtAnalysisCounter.ReadOnly = True
        Me.txtAnalysisCounter.Size = New System.Drawing.Size(242, 20)
        Me.txtAnalysisCounter.TabIndex = 3
        '
        'CheckBoxAnalysisEnabled
        '
        Me.CheckBoxAnalysisEnabled.AutoSize = True
        Me.CheckBoxAnalysisEnabled.Location = New System.Drawing.Point(6, 19)
        Me.CheckBoxAnalysisEnabled.Name = "CheckBoxAnalysisEnabled"
        Me.CheckBoxAnalysisEnabled.Size = New System.Drawing.Size(65, 17)
        Me.CheckBoxAnalysisEnabled.TabIndex = 5
        Me.CheckBoxAnalysisEnabled.Text = "Enabled"
        Me.CheckBoxAnalysisEnabled.UseVisualStyleBackColor = True
        '
        'ComboBoxCategory
        '
        Me.ComboBoxCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBoxCategory.FormattingEnabled = True
        Me.ComboBoxCategory.Location = New System.Drawing.Point(108, 84)
        Me.ComboBoxCategory.Name = "ComboBoxCategory"
        Me.ComboBoxCategory.Size = New System.Drawing.Size(239, 21)
        Me.ComboBoxCategory.TabIndex = 6
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.Location = New System.Drawing.Point(3, 92)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(52, 13)
        Me.lblCategory.TabIndex = 7
        Me.lblCategory.Text = "Category:"
        '
        'ListBoxDataSourceCounters
        '
        Me.ListBoxDataSourceCounters.FormattingEnabled = True
        Me.ListBoxDataSourceCounters.Location = New System.Drawing.Point(14, 15)
        Me.ListBoxDataSourceCounters.Name = "ListBoxDataSourceCounters"
        Me.ListBoxDataSourceCounters.Size = New System.Drawing.Size(330, 82)
        Me.ListBoxDataSourceCounters.TabIndex = 8
        '
        'GroupBoxDataSourceCounters
        '
        Me.GroupBoxDataSourceCounters.Controls.Add(Me.btnDataSourceCountersEdit)
        Me.GroupBoxDataSourceCounters.Controls.Add(Me.btnDataSourceCountersRemove)
        Me.GroupBoxDataSourceCounters.Controls.Add(Me.btnDataSourceCountersAdd)
        Me.GroupBoxDataSourceCounters.Controls.Add(Me.ListBoxDataSourceCounters)
        Me.GroupBoxDataSourceCounters.Location = New System.Drawing.Point(6, 111)
        Me.GroupBoxDataSourceCounters.Name = "GroupBoxDataSourceCounters"
        Me.GroupBoxDataSourceCounters.Size = New System.Drawing.Size(447, 108)
        Me.GroupBoxDataSourceCounters.TabIndex = 9
        Me.GroupBoxDataSourceCounters.TabStop = False
        Me.GroupBoxDataSourceCounters.Text = "Data Source Counters"
        '
        'btnDataSourceCountersEdit
        '
        Me.btnDataSourceCountersEdit.Location = New System.Drawing.Point(353, 45)
        Me.btnDataSourceCountersEdit.Name = "btnDataSourceCountersEdit"
        Me.btnDataSourceCountersEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnDataSourceCountersEdit.TabIndex = 22
        Me.btnDataSourceCountersEdit.Text = "Edit..."
        Me.btnDataSourceCountersEdit.UseVisualStyleBackColor = True
        '
        'btnDataSourceCountersRemove
        '
        Me.btnDataSourceCountersRemove.Location = New System.Drawing.Point(353, 74)
        Me.btnDataSourceCountersRemove.Name = "btnDataSourceCountersRemove"
        Me.btnDataSourceCountersRemove.Size = New System.Drawing.Size(75, 23)
        Me.btnDataSourceCountersRemove.TabIndex = 21
        Me.btnDataSourceCountersRemove.Text = "Delete"
        Me.btnDataSourceCountersRemove.UseVisualStyleBackColor = True
        '
        'btnDataSourceCountersAdd
        '
        Me.btnDataSourceCountersAdd.Location = New System.Drawing.Point(353, 15)
        Me.btnDataSourceCountersAdd.Name = "btnDataSourceCountersAdd"
        Me.btnDataSourceCountersAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnDataSourceCountersAdd.TabIndex = 20
        Me.btnDataSourceCountersAdd.Text = "Add..."
        Me.btnDataSourceCountersAdd.UseVisualStyleBackColor = True
        '
        'LabelAnalysisNames
        '
        Me.LabelAnalysisNames.AutoSize = True
        Me.LabelAnalysisNames.Location = New System.Drawing.Point(3, 4)
        Me.LabelAnalysisNames.Name = "LabelAnalysisNames"
        Me.LabelAnalysisNames.Size = New System.Drawing.Size(135, 13)
        Me.LabelAnalysisNames.TabIndex = 12
        Me.LabelAnalysisNames.Text = "Category - Analysis Names:"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(945, 24)
        Me.MenuStrip1.TabIndex = 14
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripMenuItem, Me.OpenToolStripMenuItem, Me.RefreshToolStripMenuItem, Me.SaveToolStripMenuItem, Me.SaveAsToolStripMenuItem})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "File"
        '
        'NewToolStripMenuItem
        '
        Me.NewToolStripMenuItem.Name = "NewToolStripMenuItem"
        Me.NewToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.NewToolStripMenuItem.Text = "New"
        '
        'OpenToolStripMenuItem
        '
        Me.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem"
        Me.OpenToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.OpenToolStripMenuItem.Text = "Open..."
        '
        'RefreshToolStripMenuItem
        '
        Me.RefreshToolStripMenuItem.Name = "RefreshToolStripMenuItem"
        Me.RefreshToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.RefreshToolStripMenuItem.Text = "Refresh"
        '
        'SaveToolStripMenuItem
        '
        Me.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem"
        Me.SaveToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.SaveToolStripMenuItem.Text = "Save"
        '
        'SaveAsToolStripMenuItem
        '
        Me.SaveAsToolStripMenuItem.Name = "SaveAsToolStripMenuItem"
        Me.SaveAsToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.SaveAsToolStripMenuItem.Text = "Save As..."
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(116, 22)
        Me.AboutToolStripMenuItem.Text = "About..."
        '
        'btnAnalysisNew
        '
        Me.btnAnalysisNew.Location = New System.Drawing.Point(12, 3)
        Me.btnAnalysisNew.Name = "btnAnalysisNew"
        Me.btnAnalysisNew.Size = New System.Drawing.Size(75, 23)
        Me.btnAnalysisNew.TabIndex = 15
        Me.btnAnalysisNew.Text = "New"
        Me.btnAnalysisNew.UseVisualStyleBackColor = True
        '
        'btnAnalysisDelete
        '
        Me.btnAnalysisDelete.Location = New System.Drawing.Point(87, 3)
        Me.btnAnalysisDelete.Name = "btnAnalysisDelete"
        Me.btnAnalysisDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnAnalysisDelete.TabIndex = 16
        Me.btnAnalysisDelete.Text = "Delete"
        Me.btnAnalysisDelete.UseVisualStyleBackColor = True
        '
        'TreeViewAnalysis
        '
        Me.TreeViewAnalysis.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeViewAnalysis.HideSelection = False
        Me.TreeViewAnalysis.Location = New System.Drawing.Point(0, 0)
        Me.TreeViewAnalysis.Name = "TreeViewAnalysis"
        Me.TreeViewAnalysis.Size = New System.Drawing.Size(255, 451)
        Me.TreeViewAnalysis.TabIndex = 17
        '
        'lblAnalysisDescription
        '
        Me.lblAnalysisDescription.AutoSize = True
        Me.lblAnalysisDescription.Location = New System.Drawing.Point(17, 338)
        Me.lblAnalysisDescription.Name = "lblAnalysisDescription"
        Me.lblAnalysisDescription.Size = New System.Drawing.Size(63, 13)
        Me.lblAnalysisDescription.TabIndex = 18
        Me.lblAnalysisDescription.Text = "Description:"
        '
        'txtAnalysisDescription
        '
        Me.txtAnalysisDescription.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAnalysisDescription.Location = New System.Drawing.Point(17, 355)
        Me.txtAnalysisDescription.Multiline = True
        Me.txtAnalysisDescription.Name = "txtAnalysisDescription"
        Me.txtAnalysisDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.txtAnalysisDescription.Size = New System.Drawing.Size(616, 97)
        Me.txtAnalysisDescription.TabIndex = 19
        '
        'CheckBoxAnalysisDescriptionWordWrap
        '
        Me.CheckBoxAnalysisDescriptionWordWrap.AutoSize = True
        Me.CheckBoxAnalysisDescriptionWordWrap.Checked = True
        Me.CheckBoxAnalysisDescriptionWordWrap.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBoxAnalysisDescriptionWordWrap.Location = New System.Drawing.Point(86, 337)
        Me.CheckBoxAnalysisDescriptionWordWrap.Name = "CheckBoxAnalysisDescriptionWordWrap"
        Me.CheckBoxAnalysisDescriptionWordWrap.Size = New System.Drawing.Size(78, 17)
        Me.CheckBoxAnalysisDescriptionWordWrap.TabIndex = 21
        Me.CheckBoxAnalysisDescriptionWordWrap.Text = "WordWrap"
        Me.CheckBoxAnalysisDescriptionWordWrap.UseVisualStyleBackColor = True
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.Location = New System.Drawing.Point(501, 458)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(132, 33)
        Me.btnUpdate.TabIndex = 22
        Me.btnUpdate.Text = "Update Analysis"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'btnEditChart
        '
        Me.btnEditChart.Location = New System.Drawing.Point(362, 32)
        Me.btnEditChart.Name = "btnEditChart"
        Me.btnEditChart.Size = New System.Drawing.Size(91, 73)
        Me.btnEditChart.TabIndex = 23
        Me.btnEditChart.Text = "Edit Chart..."
        Me.btnEditChart.UseVisualStyleBackColor = True
        '
        'GroupBoxThresholds
        '
        Me.GroupBoxThresholds.Controls.Add(Me.btnThresholdDelete)
        Me.GroupBoxThresholds.Controls.Add(Me.btnThresholdEdit)
        Me.GroupBoxThresholds.Controls.Add(Me.btnThresholdNew)
        Me.GroupBoxThresholds.Controls.Add(Me.ListBoxThresholds)
        Me.GroupBoxThresholds.Location = New System.Drawing.Point(9, 225)
        Me.GroupBoxThresholds.Name = "GroupBoxThresholds"
        Me.GroupBoxThresholds.Size = New System.Drawing.Size(444, 110)
        Me.GroupBoxThresholds.TabIndex = 25
        Me.GroupBoxThresholds.TabStop = False
        Me.GroupBoxThresholds.Text = "Thresholds"
        '
        'btnThresholdDelete
        '
        Me.btnThresholdDelete.Location = New System.Drawing.Point(348, 76)
        Me.btnThresholdDelete.Name = "btnThresholdDelete"
        Me.btnThresholdDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdDelete.TabIndex = 13
        Me.btnThresholdDelete.Text = "Delete"
        Me.btnThresholdDelete.UseVisualStyleBackColor = True
        '
        'btnThresholdEdit
        '
        Me.btnThresholdEdit.Location = New System.Drawing.Point(348, 47)
        Me.btnThresholdEdit.Name = "btnThresholdEdit"
        Me.btnThresholdEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdEdit.TabIndex = 12
        Me.btnThresholdEdit.Text = "Edit..."
        Me.btnThresholdEdit.UseVisualStyleBackColor = True
        '
        'btnThresholdNew
        '
        Me.btnThresholdNew.Location = New System.Drawing.Point(348, 19)
        Me.btnThresholdNew.Name = "btnThresholdNew"
        Me.btnThresholdNew.Size = New System.Drawing.Size(75, 23)
        Me.btnThresholdNew.TabIndex = 11
        Me.btnThresholdNew.Text = "Add..."
        Me.btnThresholdNew.UseVisualStyleBackColor = True
        '
        'ListBoxThresholds
        '
        Me.ListBoxThresholds.FormattingEnabled = True
        Me.ListBoxThresholds.Location = New System.Drawing.Point(11, 19)
        Me.ListBoxThresholds.Name = "ListBoxThresholds"
        Me.ListBoxThresholds.Size = New System.Drawing.Size(330, 82)
        Me.ListBoxThresholds.TabIndex = 10
        '
        'GroupBoxAnalysis
        '
        Me.GroupBoxAnalysis.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxAnalysis.Controls.Add(Me.lblHtmlRenderedDescription)
        Me.GroupBoxAnalysis.Controls.Add(Me.WebBrowser1)
        Me.GroupBoxAnalysis.Controls.Add(Me.btnEditChart)
        Me.GroupBoxAnalysis.Controls.Add(Me.CheckBoxAnalysisEnabled)
        Me.GroupBoxAnalysis.Controls.Add(Me.GroupBoxThresholds)
        Me.GroupBoxAnalysis.Controls.Add(Me.txtAnalysisName)
        Me.GroupBoxAnalysis.Controls.Add(Me.lblAnalysisDescription)
        Me.GroupBoxAnalysis.Controls.Add(Me.txtAnalysisDescription)
        Me.GroupBoxAnalysis.Controls.Add(Me.lblName)
        Me.GroupBoxAnalysis.Controls.Add(Me.CheckBoxAnalysisDescriptionWordWrap)
        Me.GroupBoxAnalysis.Controls.Add(Me.btnUpdate)
        Me.GroupBoxAnalysis.Controls.Add(Me.txtAnalysisCounter)
        Me.GroupBoxAnalysis.Controls.Add(Me.lblAnalysisCounter)
        Me.GroupBoxAnalysis.Controls.Add(Me.ComboBoxCategory)
        Me.GroupBoxAnalysis.Controls.Add(Me.lblCategory)
        Me.GroupBoxAnalysis.Controls.Add(Me.GroupBoxDataSourceCounters)
        Me.GroupBoxAnalysis.Location = New System.Drawing.Point(3, 33)
        Me.GroupBoxAnalysis.Name = "GroupBoxAnalysis"
        Me.GroupBoxAnalysis.Size = New System.Drawing.Size(640, 503)
        Me.GroupBoxAnalysis.TabIndex = 26
        Me.GroupBoxAnalysis.TabStop = False
        Me.GroupBoxAnalysis.Text = "Analysis"
        '
        'lblHtmlRenderedDescription
        '
        Me.lblHtmlRenderedDescription.AutoSize = True
        Me.lblHtmlRenderedDescription.Location = New System.Drawing.Point(471, 13)
        Me.lblHtmlRenderedDescription.Name = "lblHtmlRenderedDescription"
        Me.lblHtmlRenderedDescription.Size = New System.Drawing.Size(146, 13)
        Me.lblHtmlRenderedDescription.TabIndex = 27
        Me.lblHtmlRenderedDescription.Text = "HTML Rendered Description:"
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(471, 32)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(162, 303)
        Me.WebBrowser1.TabIndex = 26
        '
        'btnEditQuestions
        '
        Me.btnEditQuestions.Location = New System.Drawing.Point(12, 33)
        Me.btnEditQuestions.Name = "btnEditQuestions"
        Me.btnEditQuestions.Size = New System.Drawing.Size(150, 23)
        Me.btnEditQuestions.TabIndex = 27
        Me.btnEditQuestions.Text = "Edit Questions..."
        Me.btnEditQuestions.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 24)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.LabelAnalysisNames)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnEditThresholdProperties)
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBoxAnalysis)
        Me.SplitContainer1.Size = New System.Drawing.Size(933, 530)
        Me.SplitContainer1.SplitterDistance = 257
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 35
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.TreeViewAnalysis)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnAnalysisNew)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnEditQuestions)
        Me.SplitContainer2.Panel2.Controls.Add(Me.btnAnalysisDelete)
        Me.SplitContainer2.Panel2MinSize = 70
        Me.SplitContainer2.Size = New System.Drawing.Size(255, 528)
        Me.SplitContainer2.SplitterDistance = 451
        Me.SplitContainer2.TabIndex = 35
        '
        'btnEditThresholdProperties
        '
        Me.btnEditThresholdProperties.Location = New System.Drawing.Point(3, 4)
        Me.btnEditThresholdProperties.Name = "btnEditThresholdProperties"
        Me.btnEditThresholdProperties.Size = New System.Drawing.Size(178, 23)
        Me.btnEditThresholdProperties.TabIndex = 27
        Me.btnEditThresholdProperties.Text = "Edit Threshold File Properties..."
        Me.btnEditThresholdProperties.UseVisualStyleBackColor = True
        '
        'frmPALEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(945, 557)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(961, 595)
        Me.Name = "frmPALEditor"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "PAL Editor"
        Me.GroupBoxDataSourceCounters.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.GroupBoxThresholds.ResumeLayout(False)
        Me.GroupBoxAnalysis.ResumeLayout(False)
        Me.GroupBoxAnalysis.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtAnalysisName As System.Windows.Forms.TextBox
    Friend WithEvents lblName As System.Windows.Forms.Label
    Friend WithEvents lblAnalysisCounter As System.Windows.Forms.Label
    Friend WithEvents txtAnalysisCounter As System.Windows.Forms.TextBox
    Friend WithEvents CheckBoxAnalysisEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents ComboBoxCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Friend WithEvents ListBoxDataSourceCounters As System.Windows.Forms.ListBox
    Friend WithEvents GroupBoxDataSourceCounters As System.Windows.Forms.GroupBox
    Friend WithEvents LabelAnalysisNames As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SaveAsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        OpenFileDialog1.Filter = "XML files (*.xml)|*.xml|All Files (*.*)|*.*"
        OpenFileDialog1.FileName = ""
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            OpenThresholdFile(OpenFileDialog1.FileName)
            RefreshTreeViewAnalysis()
        End If
        SaveToolStripMenuItem.Enabled = True
    End Sub

    Private Sub frmMain_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ofrmPALExecutionWizard.RefreshAnalysisCollectionSection(True)
    End Sub

    Private Sub frmPALEditor_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.GotFocus
        UpdateThresholdFile()
        UpdateAnalysis()
        RefreshTreeViewAnalysis()
        ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''g_sThresholdFileTitle = ""
        ''g_sThresholdFileVersion = ""
        ''g_XMLRoot.Attributes("VERSION").Value = g_sThresholdFileVersion
        ''g_XMLRoot.Attributes("NAME").Value = g_sThresholdFileTitle
        ''g_XMLRoot.Attributes("DESCRIPTION").Value = g_sThresholdFileDescription
        ''g_XMLRoot.Attributes("CONTENTOWNERS").Value = g_sThresholdFileContentOwners
        ''g_XMLRoot.Attributes("FEEDBACKEMAILADDRESS").Value = g_sThresholdFileFeedbackEmailAddresses

        g_LastSelectedTreeViewName = ""
        LastUsedComputerName = ""
        If sThresholdFilePath <> "" Then
            OpenThresholdFile(sThresholdFilePath)
            RefreshTreeViewAnalysis()
        Else
            CreateAndOpenNewThresholdFile()
        End If
        btnEditChart.Enabled = False

    End Sub

    Private Sub OpenThresholdFile(ByVal sThresholdFilePath As String)
        Dim xmldoc As New XmlDocument
        g_ThresholdFilePath = sThresholdFilePath
        xmldoc.Load(sThresholdFilePath)
        g_xmlThresholdFile = xmldoc
        g_XMLRoot = g_xmlThresholdFile.DocumentElement
    End Sub

    Public Sub RefreshTreeViewAnalysis()
        Dim oXmlNode As XmlNode
        Dim oXmlAttributeCollection As XmlAttributeCollection
        Dim oXmlAnalysisNodeList As XmlNodeList
        Dim bFound As Boolean
        Dim i, s As Integer
        Dim aCategories() As String

        If IsNothing(g_XMLRoot) = True Then
            Exit Sub
        End If

        g_sThresholdFileTitle = g_XMLRoot.Attributes("NAME").Value
        g_sThresholdFileDescription = g_XMLRoot.Attributes("DESCRIPTION").Value
        g_sThresholdFileVersion = g_XMLRoot.Attributes("VERSION").Value
        g_sThresholdFileContentOwners = g_XMLRoot.Attributes("CONTENTOWNERS").Value
        g_sThresholdFileFeedbackEmailAddresses = g_XMLRoot.Attributes("FEEDBACKEMAILADDRESS").Value

        oXmlAnalysisNodeList = g_XMLRoot.SelectNodes("//ANALYSIS")
        TreeViewAnalysis.Nodes.Clear()
        ' Populate TreeView analysis with Categories
        i = 0
        ReDim Preserve aCategories(0)
        aCategories(0) = ""
        For Each oXmlNode In oXmlAnalysisNodeList
            bFound = False
            For s = 0 To UBound(aCategories)
                If LCase(aCategories(s)) = LCase(oXmlNode.Attributes("CATEGORY").Value) Then
                    bFound = True
                    Exit For
                End If
            Next
            If bFound = False Then
                ReDim Preserve aCategories(i)
                aCategories(i) = oXmlNode.Attributes("CATEGORY").Value
                i = i + 1
            End If
        Next

        For i = 0 To UBound(aCategories)
            TreeViewAnalysis.Nodes.Add(aCategories(i), aCategories(i))
        Next

        ' Add Analysis nodes to Treeview under the respective catagory
        i = 0
        For Each oXmlNode In oXmlAnalysisNodeList
            oXmlAttributeCollection = oXmlNode.Attributes
            TreeViewAnalysis.Nodes.Item(oXmlNode.Attributes("CATEGORY").Value).Nodes.Add(oXmlNode.Attributes("NAME").Value, oXmlNode.Attributes("NAME").Value)
        Next
        RefreshCategoryComboBox()
        'If g_LastSelectedTreeViewName <> "" Then
        '    TreeViewAnalysis.SelectedNode = g_LastSelectedTreeViewNode
        'End If
        TreeViewAnalysis.Sort()
    End Sub
    Friend WithEvents btnAnalysisNew As System.Windows.Forms.Button
    Friend WithEvents btnAnalysisDelete As System.Windows.Forms.Button

    Sub RefreshCategoryComboBox()
        Dim oXmlAnalysisNodeList As XmlNodeList
        Dim oXMLNode As XmlNode
        Dim aCategories()
        Dim i, s As Integer
        Dim bFound As Boolean

        oXmlAnalysisNodeList = g_XMLRoot.SelectNodes("//ANALYSIS")
        i = 0
        ReDim Preserve aCategories(0)
        aCategories(0) = "Processor"
        For Each oXMLNode In oXmlAnalysisNodeList
            bFound = False
            For s = 0 To UBound(aCategories)
                If LCase(aCategories(s)) = LCase(oXMLNode.Attributes("CATEGORY").Value) Then
                    bFound = True
                End If
            Next
            If bFound = False Then
                ReDim Preserve aCategories(i)
                aCategories(i) = oXMLNode.Attributes("CATEGORY").Value
                i = i + 1
            End If
        Next
        ComboBoxCategory.DataSource = aCategories
    End Sub

    Sub RefreshFormWithAnalysisData(ByVal sAnalysis As String)
        Dim oXmlNode As XmlNode
        Dim bFoundAnalysisNode As Boolean
        Dim i As Integer

        bFoundAnalysisNode = False
        For Each oXmlNode In g_XMLRoot.SelectNodes("//ANALYSIS")
            If LCase(oXmlNode.Attributes("NAME").Value) = LCase(sAnalysis) Then
                g_oXMLSelectedAnalysis = oXmlNode
                g_LastSelectedTreeViewName = sAnalysis
                bFoundAnalysisNode = True
                Exit For
            End If
        Next

        'For Each oXmlNode In g_xmlThresholdFile.SelectNodes("//ANALYSIS")
        '    If LCase(oXmlNode.Attributes("NAME").Value) = LCase(sAnalysis) Then
        '        g_oXMLSelectedAnalysis = oXmlNode
        '        g_LastSelectedTreeViewName = sAnalysis
        '        bFoundAnalysisNode = True
        '        Exit For
        '    End If
        'Next

        If bFoundAnalysisNode = False Then
            ResetMainForm()
            Exit Sub
        End If

        txtAnalysisName.Text = g_oXMLSelectedAnalysis.Attributes("NAME").Value
        If g_oXMLSelectedAnalysis.Attributes("ENABLED").Value = "True" Then
            CheckBoxAnalysisEnabled.Checked = True
        Else
            CheckBoxAnalysisEnabled.Checked = False
        End If
        txtAnalysisCounter.Text = g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value
        ComboBoxCategory.Text = g_oXMLSelectedAnalysis.Attributes("CATEGORY").Value

        '// Description
        txtAnalysisDescription.Text = ""
        For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./DESCRIPTION")
            txtAnalysisDescription.Text = oXmlNode.InnerText
        Next

        '// Counters
        ListBoxDataSourceCounters.Items.Clear()
        i = 0
        For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./DATASOURCE")
            ListBoxDataSourceCounters.Items.Add(oXmlNode.Attributes("NAME").Value)
        Next
        ListBoxDataSourceCounters.Sorted = True
        ListBoxDataSourceCounters.SetSelected(0, True)
        BindSelectedListBoxCounterItemToXML()

        '// Thresholds
        ListBoxThresholds.Items.Clear()
        i = 0
        For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
            ListBoxThresholds.Items.Add(oXmlNode.Attributes("NAME").Value)
        Next
        If ListBoxThresholds.Items.Count > 0 Then
            ListBoxThresholds.SetSelected(0, True)
            BindSelectedListBoxThresholdItemToXML()
        End If

        '// Chart
        btnEditChart.Text = "Create Chart..."
        btnEditChart.Enabled = True
        i = 0
        For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./CHART")
            i = i + 1
        Next
        If i > 0 Then
            btnEditChart.Text = "Edit Chart..."
        End If

    End Sub

    Sub ResetMainForm()
        txtAnalysisName.Text = ""
        txtAnalysisCounter.Text = ""
        txtAnalysisDescription.Text = ""
        CheckBoxAnalysisDescriptionWordWrap.Checked = True
        CheckBoxAnalysisEnabled.Checked = False
        ListBoxDataSourceCounters.Items.Clear()
        ListBoxThresholds.Items.Clear()
    End Sub

    Friend WithEvents TreeViewAnalysis As System.Windows.Forms.TreeView

    Private Sub TreeViewAnalysis_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeViewAnalysis.AfterSelect
        g_LastSelectedTreeViewNode = e.Node
        g_LastSelectedTreeViewName = e.Node.Name
        If TreeViewAnalysis.SelectedNode.Level = 0 Then
            g_oXMLSelectedAnalysis = Nothing
        End If
        RefreshFormWithAnalysisData(TreeViewAnalysis.SelectedNode.Name.ToString)
    End Sub

    Private Sub ComboBoxChartMaxCategoryLabels_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub LabelChartMaxCategoryLabels_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
    Friend WithEvents lblAnalysisDescription As System.Windows.Forms.Label
    Friend WithEvents txtAnalysisDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnDataSourceCountersRemove As System.Windows.Forms.Button
    Friend WithEvents btnDataSourceCountersAdd As System.Windows.Forms.Button

    Private Sub btnAnalysisNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnalysisNew.Click
        Dim oFrmNewAnalysis As New frmNewAnalysis

        Dim oXmlAnalysisNodeList As XmlNodeList
        Dim oXMLNode As XmlNode
        Dim aCategories()
        Dim i, s As Integer
        Dim bFound As Boolean

        oXmlAnalysisNodeList = g_XMLRoot.SelectNodes("//ANALYSIS")
        i = 0
        ReDim Preserve aCategories(0)
        aCategories(0) = ""
        For Each oXMLNode In oXmlAnalysisNodeList
            bFound = False
            For s = 0 To UBound(aCategories)
                If LCase(aCategories(s)) = LCase(oXMLNode.Attributes("CATEGORY").Value) Then
                    bFound = True
                End If
            Next
            If bFound = False Then
                ReDim Preserve aCategories(i)
                aCategories(i) = oXMLNode.Attributes("CATEGORY").Value
                i = i + 1
            End If
        Next

        oFrmNewAnalysis.ComboBoxCategory.DataSource = aCategories
        oFrmNewAnalysis.LastUsedComputerName = LastUsedComputerName
        oFrmNewAnalysis.ofrmMain = Me
        oFrmNewAnalysis.Show(Me)
    End Sub

    Sub AddAnalysis(ByVal sName As String, ByVal bEnabled As Boolean, ByVal sAnalysisCounter As String, ByVal sCategory As String, ByVal sDescription As String)
        Dim oXMLAnalysisNode As XmlNode
        Dim oXMLDescriptionNode As XmlNode
        Dim oXMLDescriptionCDATANode As XmlCDataSection
        Dim oXMLCounterNode As XmlNode
        Dim oXMLChartNode As XmlNode
        Dim oXMLNewAttribute As XmlAttribute
        Dim sVarName As String
        Dim iGUID As Guid
        iGUID = Guid.NewGuid

        '// Analysis
        oXMLAnalysisNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "ANALYSIS", "")
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
        oXMLNewAttribute.Value = sName
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("ENABLED")
        oXMLNewAttribute.Value = bEnabled.ToString
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CATEGORY")
        oXMLNewAttribute.Value = sCategory
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("PRIMARYDATASOURCE")
        oXMLNewAttribute.Value = sAnalysisCounter
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("ID")
        oXMLNewAttribute.Value = iGUID.ToString
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("FROMALLCOUNTERSTATS")
        oXMLNewAttribute.Value = "False"
        oXMLAnalysisNode.Attributes.Append(oXMLNewAttribute)

        '// Description
        oXMLDescriptionNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DESCRIPTION", "")
        oXMLDescriptionCDATANode = g_xmlThresholdFile.CreateCDataSection(sDescription)
        'oXMLDescriptionNode.InnerText = sDescription
        oXMLDescriptionNode.AppendChild(oXMLDescriptionCDATANode)
        oXMLAnalysisNode.AppendChild(oXMLDescriptionNode)

        '// DATASOURCE
        'sVarName = GetCounterObject(sAnalysisCounter) & GetCounterName(sAnalysisCounter)
        sVarName = ConvertCounterToVarName(sAnalysisCounter)
        oXMLCounterNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DATASOURCE", "")
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("TYPE")
        oXMLNewAttribute.Value = "CounterLog"
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
        oXMLNewAttribute.Value = sAnalysisCounter
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("COLLECTIONVARNAME")
        oXMLNewAttribute.Value = "CollectionOf" & sVarName
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("EXPRESSIONPATH")
        oXMLNewAttribute.Value = sAnalysisCounter
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("TRENDVARNAME")
        oXMLNewAttribute.Value = "Trend" & sVarName
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("DATATYPE")
        oXMLNewAttribute.Value = "integer"
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLAnalysisNode.AppendChild(oXMLCounterNode)

        '// Chart
        oXMLChartNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CHART", "")
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CHARTTITLE")
        oXMLNewAttribute.Value = sAnalysisCounter
        oXMLChartNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("ISTHRESHOLDSADDED")
        oXMLNewAttribute.Value = "False"
        oXMLChartNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("DATASOURCE")
        oXMLNewAttribute.Value = sAnalysisCounter
        oXMLChartNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CHARTLABELS")
        oXMLNewAttribute.Value = "instance"
        oXMLChartNode.Attributes.Append(oXMLNewAttribute)

        oXMLAnalysisNode.AppendChild(oXMLChartNode)

        g_XMLRoot.AppendChild(oXMLAnalysisNode)
    End Sub

    Function ConvertCounterToVarName(ByVal sCounter As String)
        Dim sCounterObject, sCounterName
        ' \\IDCWEB1\Processor(_Total)\% Processor Time
        sCounterObject = GetCounterObject(sCounter)
        sCounterName = GetCounterName(sCounter)
        ConvertCounterToVarName = sCounterObject & "_" & sCounterName
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "/", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "\", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "%", "Percent")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, " ", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, ".", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, ":", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "(", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, ")", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "-", "")
        ConvertCounterToVarName = Replace(ConvertCounterToVarName, "_", "")
    End Function

    Function GetCounterObject(ByVal sCounter As String)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        '\Processor(_Total)\% Processor Time"
        '\\IDCWEB1\Processor\% Processor Time"
        '\Processor\% Processor Time"
        Dim sCounterObject As String
        Dim iLocParen, iLocThirdBackSlash, iLocBackSlash As Integer
        sCounterObject = sCounter
        ' Returns the counter object
        iLocParen = InStr(3, sCounterObject, "(")

        If sCounterObject.Substring(0, 2) = "\\" Then
            '\\IDCWEB1\Processor(_Total)\% Processor Time
            '\\IDCWEB1\Processor\% Processor Time
            iLocThirdBackSlash = InStr(3, sCounterObject, "\")
            sCounterObject = Mid(sCounterObject, iLocThirdBackSlash + 1)
            'Processor(_Total)\% Processor Time
            'Processor\% Processor Time
        ElseIf sCounterObject.Substring(0, 1) = "\" Then
            '\Processor\% Processor Time
            sCounterObject = Mid(sCounterObject, 2)
            'Processor\% Processor Time
        End If
        If iLocParen = 0 Then
            'Processor\% Processor Time
            iLocBackSlash = InStr(sCounterObject, "\")
            GetCounterObject = Mid(sCounterObject, 1, iLocBackSlash - 1)
        Else
            'Processor(_Total)\% Processor Time
            iLocParen = InStr(sCounterObject, "(")
            GetCounterObject = Mid(sCounterObject, 1, iLocParen - 1)
        End If
    End Function

    Function GetCounterName(ByVal sCounter)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        '\Processor(_Total)\% Processor Time"
        ' Returns the counter name
        Dim sCounterName As String
        Dim iLocSecondBackSlash, iLocThirdBackSlash, iLocForthBackSlash As Integer
        sCounterName = sCounter
        If sCounterName.Substring(0, 2) = "\\" Then
            iLocThirdBackSlash = InStr(3, sCounterName, "\")
            iLocForthBackSlash = InStr(iLocThirdBackSlash + 1, sCounterName, "\")
            GetCounterName = Mid(sCounterName, iLocForthBackSlash + 1)
        Else
            iLocSecondBackSlash = InStr(3, sCounter, "\")
            GetCounterName = Mid(sCounterName, iLocSecondBackSlash + 1)
        End If
    End Function

    Function GetCounterInstance(ByVal sCounter)
        '\\IDCWEB1\Processor(_Total)\% Processor Time"
        ' Returns the counter instance name
        Dim sCounterInstance As String
        Dim iLocFirstParen, iLocSecondParen, iLen As Integer
        sCounterInstance = sCounter
        iLocFirstParen = InStr(sCounterInstance, "(")
        If iLocFirstParen = 0 Then
            GetCounterInstance = ""
            Exit Function
        End If
        iLocSecondParen = InStr(sCounterInstance, ")")
        iLen = iLocSecondParen - iLocFirstParen - 1
        GetCounterInstance = Mid(sCounterInstance, iLocFirstParen + 1, iLen)
    End Function

    Friend WithEvents RefreshToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem


    Private Sub RefreshToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshToolStripMenuItem.Click
        RefreshTreeViewAnalysis()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        UpdateThresholdFile()
        UpdateAnalysis()
        SaveXMLToFile(g_ThresholdFilePath)
    End Sub

    Sub SaveXMLToFile(ByVal sFilePath As String)
        g_xmlThresholdFile.Save(sFilePath)
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        UpdateThresholdFile()
        UpdateAnalysis()
        SaveToolStripMenuItem.Enabled = True
        SaveFileDialog1.Filter = "XML files (*.xml)|*.xml|All Files (*.*)|*.*"
        If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
            SaveXMLToFile(SaveFileDialog1.FileName)
            g_ThresholdFilePath = SaveFileDialog1.FileName
        End If
    End Sub

    Private Sub btnAnalysisDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnalysisDelete.Click
        DeleteAnalysis(TreeViewAnalysis.SelectedNode.Name.ToString)
        g_oXMLSelectedAnalysis = Nothing
        ResetMainForm()
    End Sub

    Sub DeleteAnalysis(ByVal sAnalysis As String)
        Dim bFoundAnalysisNode As Boolean
        Dim oXmlNode As XmlNode

        bFoundAnalysisNode = False
        For Each oXmlNode In g_XMLRoot.SelectNodes("//ANALYSIS")
            If LCase(oXmlNode.Attributes("NAME").Value) = LCase(sAnalysis) Then
                g_oXMLSelectedAnalysis = oXmlNode
                bFoundAnalysisNode = True
                Exit For
            End If
        Next
        If bFoundAnalysisNode = False Then
            Exit Sub
        End If
        g_XMLRoot.RemoveChild(g_oXMLSelectedAnalysis)
        RefreshTreeViewAnalysis()
    End Sub
    Friend WithEvents CheckBoxAnalysisDescriptionWordWrap As System.Windows.Forms.CheckBox

    Private Sub CheckBoxAnalysisDescriptionWordWrap_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAnalysisDescriptionWordWrap.CheckedChanged
        If CheckBoxAnalysisDescriptionWordWrap.Checked = True Then
            txtAnalysisDescription.WordWrap = True
            txtAnalysisDescription.ScrollBars = ScrollBars.Vertical
        Else
            txtAnalysisDescription.WordWrap = False
            txtAnalysisDescription.ScrollBars = ScrollBars.Both
        End If
    End Sub

    Sub UpdateThresholdFile()
        Dim oXmlNewNode As XmlNode
        Dim oXmlNewAttribute As XmlAttribute
        Dim aFileNames()
        Dim sFileName As String
        If sThresholdFilePath <> "" Then
            Me.Text = "PAL Tool - " & g_sThresholdFileTitle & " " & g_sThresholdFileVersion
            g_XMLRoot.Attributes("VERSION").Value = g_sThresholdFileVersion
            g_XMLRoot.Attributes("NAME").Value = g_sThresholdFileTitle
            g_XMLRoot.Attributes("DESCRIPTION").Value = g_sThresholdFileDescription
            g_XMLRoot.Attributes("CONTENTOWNERS").Value = g_sThresholdFileContentOwners
            g_XMLRoot.Attributes("FEEDBACKEMAILADDRESS").Value = g_sThresholdFileFeedbackEmailAddresses

            '// Remove all of the INHERITANCE nodes.
            For Each oXmlNode In g_XMLRoot.SelectNodes("//INHERITANCE")
                g_XMLRoot.RemoveChild(oXmlNode)
            Next

            '// Add the new INHERITANCE nodes.
            aFileNames = Split(g_ThresholdFileInheritanceFileNames, ",")
            For Each sFileName In aFileNames
                oXmlNewNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "INHERITANCE", "")
                oXmlNewAttribute = g_xmlThresholdFile.CreateAttribute("FILEPATH")
                oXmlNewAttribute.Value = sFileName
                oXmlNewNode.Attributes.Append(oXmlNewAttribute)
                g_XMLRoot.AppendChild(oXmlNewNode)
            Next
        End If
    End Sub

    Sub UpdateAnalysis()
        Dim oXmlNode As XmlNode
        Dim oXMLCDataNode As XmlCDataSection
        Dim oXmlNodeList As XmlNodeList

        If txtAnalysisName.Text = "" Then
            Exit Sub
        End If
        g_oXMLSelectedAnalysis.Attributes("NAME").Value = txtAnalysisName.Text
        If CheckBoxAnalysisEnabled.Checked = True Then
            g_oXMLSelectedAnalysis.Attributes("ENABLED").Value = "True"
        Else
            g_oXMLSelectedAnalysis.Attributes("ENABLED").Value = "False"
        End If
        g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value = txtAnalysisCounter.Text
        g_oXMLSelectedAnalysis.Attributes("CATEGORY").Value = ComboBoxCategory.Text

        '// Description
        '// Remove all description nodes.
        oXmlNodeList = g_oXMLSelectedAnalysis.SelectNodes("./DESCRIPTION")
        If oXmlNodeList.Count > 0 Then
            For Each oXmlNode In oXmlNodeList
                g_oXMLSelectedAnalysis.RemoveChild(oXmlNode)
            Next
        End If

        '// Create and set description field
        oXmlNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DESCRIPTION", "")
        oXMLCDataNode = g_xmlThresholdFile.CreateCDataSection(txtAnalysisDescription.Text)
        oXmlNode.AppendChild(oXMLCDataNode)
        g_oXMLSelectedAnalysis.AppendChild(oXmlNode)

        'For Each oXmlNode In g_oXMLSelectedAnalysis.ChildNodes
        '    If oXmlNode.Name = "DESCRIPTION" Then
        '        oXmlNode.InnerText = txtAnalysisDescription.Text
        '        Exit For
        '    End If
        'Next

        '// Counters
        ' Should only be updated by Add Remove controls.

        '// Chart
        'For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./CHART") ' should only retrun one node
        '    oXmlNode.Attributes("CHARTTITLE").Value = txtChartTitle.Text
        '    oXmlNode.Attributes("OTSFORMAT").Value = txtChartOTSFormat.Text
        '    oXmlNode.Attributes("GROUPSIZE").Value = txtChartGroupSize.Text
        '    oXmlNode.Attributes("CATEGORIES").Value = ComboBoxChartCategories.Text
        '    oXmlNode.Attributes("DATATYPE").Value = LCase(ComboBoxChartDataType.Text)
        '    oXmlNode.Attributes("LEGEND").Value = ComboBoxChartLegend.Text
        '    oXmlNode.Attributes("MAXCATEGORYLABELS").Value = ComboBoxChartMaxCategoryLabels.Text
        '    oXmlNode.Attributes("CHARTTYPE").Value = ComboBoxChartType.Text
        '    oXmlNode.Attributes("VALUES").Value = ComboBoxChartValues.Text
        'Next

        '// Thresholds
        '// This should be updated when thresholds are added or removed.
        'For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
        '    ListBoxThresholds.Items.Add(oXmlNode.Attributes("NAME").Value)
        '    g_oXMLSelectedThreshold = oXmlNode
        'Next
        'If ListBoxThresholds.Items.Count > 0 Then
        '    ListBoxThresholds.SelectedIndex = 0
        'End If
    End Sub

    Function AddCounter(ByVal sAnalysis As String, ByVal sName As String, ByVal sCollectionVarName As String, ByVal sDataType As String, ByVal aExclusions() As String) As Boolean
        Dim oXMLCounterNode, oXMLExclude As XmlNode
        Dim oXMLNewAttribute As XmlAttribute
        Dim i As Integer

        Dim sCounterObject As String
        Dim sCounterInstance As String
        Dim sCounterName As String
        Dim sNewCounterObject As String
        Dim sNewCounterInstance As String
        Dim sNewCounterName As String
        sNewCounterObject = LCase(GetCounterObject(sName))
        sNewCounterInstance = LCase(GetCounterInstance(sName))
        sNewCounterName = LCase(GetCounterName(sName))

        ''// Check to see if adding a wildcard instance because this only works if all of the instance names are the same as the analysis counter instances.
        'If sNewCounterInstance = "*" Then
        '    Dim oMsgBoxResult As MsgBoxResult
        '    oMsgBoxResult = MsgBox("A wildcard datasource counter only works if all of the instances are respectively the same as the primary counter to analyze. Are you sure you want to add this wildcard (*) instance counter?", MsgBoxStyle.YesNo, "Datasource Counter Add")
        '    If oMsgBoxResult = MsgBoxResult.No Then
        '        Return False
        '        Exit Function
        '    End If
        'End If

        '// Check to see if this counter already exists
        For Each oXMLCounterNode In g_oXMLSelectedAnalysis.SelectNodes("./DATASOURCE")
            sCounterObject = LCase(GetCounterObject(oXMLCounterNode.Attributes("NAME").Value))
            sCounterInstance = LCase(GetCounterInstance(oXMLCounterNode.Attributes("NAME").Value))
            sCounterName = LCase(GetCounterName(oXMLCounterNode.Attributes("NAME").Value))
            '// Check to see if a wildcard instance already exists
            If sCounterObject = sNewCounterObject And sCounterInstance = "*" And sCounterName = sNewCounterName Then
                MsgBox("All counter instances of the counter you are attemping to add already exist, therefore you cannot add an additional counter instance of this counter unless you remove the counter using the wildcard (*).")
                Return False
                Exit Function
            End If
            ''// Check to see if the counter instance being added is a wildcard instance and if an instance of this counter already exists.
            'If sCounterObject = sNewCounterObject And sNewCounterInstance = "*" And sCounterName = sNewCounterName Then
            '    MsgBox("You cannot add a new wildcard instance (*) when another instance of this counter already exists. Either add another specific instance of this counter or remove the counter that already exists.")
            '    Return False
            '    Exit Function
            'End If
            If sCounterObject = sNewCounterObject And sCounterInstance = sNewCounterInstance And sCounterName = sNewCounterName Then
                '// It exists if we get here, so do not add this counter.
                '// Inform the user that this is a duplicate.
                MsgBox("This counter already exists.")
                Return False
                Exit Function
            End If
        Next

        '// Continue with normal counter add
        oXMLCounterNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DATASOURCE", "")
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("TYPE")
        oXMLNewAttribute.Value = "CounterLog"
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
        oXMLNewAttribute.Value = sName
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("EXPRESSIONPATH")
        oXMLNewAttribute.Value = sName
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("COLLECTIONVARNAME")
        oXMLNewAttribute.Value = sCollectionVarName
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("DATATYPE")
        oXMLNewAttribute.Value = sDataType
        oXMLCounterNode.Attributes.Append(oXMLNewAttribute)

        '// Add Exclusions if any
        If aExclusions(0) <> "" Then
            For i = 0 To UBound(aExclusions)
                oXMLExclude = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "EXCLUDE", "")
                oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("INSTANCE")
                oXMLNewAttribute.Value = aExclusions(i)
                oXMLExclude.Attributes.Append(oXMLNewAttribute)
                oXMLCounterNode.AppendChild(oXMLExclude)
            Next
        End If
        g_oXMLSelectedAnalysis.AppendChild(oXMLCounterNode)
        Return True
    End Function

    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents btnEditChart As System.Windows.Forms.Button

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        UpdateThresholdFile()
        UpdateAnalysis()
        RefreshTreeViewAnalysis()
        ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Sub ReSelectTreeViewNode(ByVal sNodeName As String)
        Dim oCategoryNode As TreeNode
        Dim oAnalysisNode As TreeNode

        For Each oCategoryNode In TreeViewAnalysis.Nodes
            For Each oAnalysisNode In oCategoryNode.Nodes
                If oAnalysisNode.Name = sNodeName Then
                    TreeViewAnalysis.SelectedNode = oAnalysisNode
                    Exit Sub
                End If
            Next
        Next
    End Sub
    Friend WithEvents btnDataSourceCountersEdit As System.Windows.Forms.Button

    Private Sub btnDataSourceCountersAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataSourceCountersAdd.Click
        Dim ofrmDataSourceCounters As New frmAddDataSourceCounter
        ofrmDataSourceCounters.ofrmMain = Me
        ofrmDataSourceCounters.LastUsedComputerName = LastUsedComputerName
        ofrmDataSourceCounters.XMLRoot = g_XMLRoot
        ofrmDataSourceCounters.oXmlAnalysisNode = g_oXMLSelectedAnalysis
        ofrmDataSourceCounters.Show(Me)
    End Sub
    Friend WithEvents GroupBoxThresholds As System.Windows.Forms.GroupBox
    Friend WithEvents btnThresholdDelete As System.Windows.Forms.Button
    Friend WithEvents btnThresholdEdit As System.Windows.Forms.Button
    Friend WithEvents btnThresholdNew As System.Windows.Forms.Button
    Friend WithEvents ListBoxThresholds As System.Windows.Forms.ListBox

    Private Sub btnDataSourceCountersRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataSourceCountersRemove.Click
        Dim bRetVal As Boolean
        bRetVal = RemoveCounter(ListBoxDataSourceCounters.SelectedItem)
        If bRetVal = True Then
            ListBoxDataSourceCounters.Items.Remove(ListBoxDataSourceCounters.SelectedItem)
        End If
    End Sub

    Function RemoveCounter(ByVal sName As String) As Boolean
        Dim oXMLCounterNode As XmlNode
        Dim bFound As Boolean
        Dim sCounterObject As String
        Dim sCounterInstance As String
        Dim sCounterName As String
        Dim sNewCounterObject As String
        Dim sNewCounterInstance As String
        Dim sNewCounterName As String
        sNewCounterObject = LCase(GetCounterObject(sName))
        sNewCounterInstance = LCase(GetCounterInstance(sName))
        sNewCounterName = LCase(GetCounterName(sName))
        bFound = False

        '// If the counter being requested for removal is the analysis counter, then deny the request.
        sCounterObject = LCase(GetCounterObject(g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value))
        sCounterInstance = LCase(GetCounterInstance(g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value))
        sCounterName = LCase(GetCounterName(g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value))
        If sCounterObject = sNewCounterObject And sCounterInstance = sNewCounterInstance And sCounterName = sNewCounterName Then
            '// The counter being requested for removal is the analysis. Explain that this cannot be removed.
            MsgBox("This counter cannot be removed since it is the primary counter to analyze.")
            Return False
            Exit Function
        End If

        '// Find and remove the counter.
        For Each oXMLCounterNode In g_oXMLSelectedAnalysis.SelectNodes("./DATASOURCE")
            sCounterObject = LCase(GetCounterObject(oXMLCounterNode.Attributes("NAME").Value))
            sCounterInstance = LCase(GetCounterInstance(oXMLCounterNode.Attributes("NAME").Value))
            sCounterName = LCase(GetCounterName(oXMLCounterNode.Attributes("NAME").Value))
            If sCounterObject = sNewCounterObject And sCounterInstance = sNewCounterInstance And sCounterName = sNewCounterName Then
                g_oXMLSelectedAnalysis.RemoveChild(oXMLCounterNode)
                bFound = True
                Exit For
            End If
        Next
        If bFound = True Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub btnDataSourceCountersEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataSourceCountersEdit.Click
        Dim ofrmEditDataSourceCounter As New frmEditDataSourceCounter
        BindSelectedListBoxCounterItemToXML()
        ofrmEditDataSourceCounter.oXMLCounterNode = g_oXMLSelectedDataSourceCounter
        ofrmEditDataSourceCounter.ofrmMain = Me
        ofrmEditDataSourceCounter.Show(Me)
    End Sub

    'Private Sub ListBoxDataSourceCounters_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxDataSourceCounters.SelectedValueChanged
    '    BindSelectedListBoxCounterItemToXML()
    'End Sub

    Sub BindSelectedListBoxCounterItemToXML()
        Dim oXMLCounterNode As XmlNode
        Dim sCounterObject As String
        Dim sCounterInstance As String
        Dim sCounterName As String
        Dim sNewCounterObject As String
        Dim sNewCounterInstance As String
        Dim sNewCounterName As String
        Dim sName As String

        sName = ListBoxDataSourceCounters.SelectedItem
        sNewCounterObject = LCase(GetCounterObject(sName))
        sNewCounterInstance = LCase(GetCounterInstance(sName))
        sNewCounterName = LCase(GetCounterName(sName))

        For Each oXMLCounterNode In g_oXMLSelectedAnalysis.SelectNodes("./DATASOURCE")
            sCounterObject = LCase(GetCounterObject(oXMLCounterNode.Attributes("NAME").Value))
            sCounterInstance = LCase(GetCounterInstance(oXMLCounterNode.Attributes("NAME").Value))
            sCounterName = LCase(GetCounterName(oXMLCounterNode.Attributes("NAME").Value))
            If sCounterObject = sNewCounterObject And sCounterInstance = sNewCounterInstance And sCounterName = sNewCounterName Then
                g_oXMLSelectedDataSourceCounter = oXMLCounterNode
                Exit For
            End If
        Next
    End Sub

    Function UpdateCounter(ByVal oXMLCounterNode As XmlNode, ByVal sName As String, ByVal sCollectionVarName As String, ByVal sDataType As String, ByVal aExclusions() As String) As Boolean
        Dim oXMLExclude As XmlNode
        Dim oXMLNewAttribute As XmlAttribute
        Dim i As Integer

        '// Continue with normal counter update
        oXMLCounterNode.Attributes("NAME").Value = sName        
        oXMLCounterNode.Attributes("EXPRESSIONPATH").Value = sName
        oXMLCounterNode.Attributes("COLLECTIONVARNAME").Value = sCollectionVarName
        oXMLCounterNode.Attributes("DATATYPE").Value = sDataType
        'oXMLCounterNode.Attributes("MINVARNAME").Value = sMinVarName
        'oXMLCounterNode.Attributes("AVGVARNAME").Value = sAvgVarName
        'oXMLCounterNode.Attributes("MAXVARNAME").Value = sMaxVarName
        'oXMLCounterNode.Attributes("TRENDVARNAME").Value = sTrendVarName

        '// Remove all EXCLUDE nodes for a rebuild
        For Each oXMLExclude In oXMLCounterNode.SelectNodes("./EXCLUDE")
            oXMLCounterNode.RemoveChild(oXMLExclude)
        Next

        '// Add Exclusions if any
        If aExclusions(0) <> "" Then
            For i = 0 To UBound(aExclusions)
                oXMLExclude = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "EXCLUDE", "")
                oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("INSTANCE")
                oXMLNewAttribute.Value = aExclusions(i)
                oXMLExclude.Attributes.Append(oXMLNewAttribute)
                oXMLCounterNode.AppendChild(oXMLExclude)
            Next
        End If
        Return True
    End Function

    Function UpdateChart(ByVal oXMLChart As XmlNode, ByVal sChartTitle As String, ByVal sDataSource As String, ByVal sIsThresholdsAdded As String, ByVal aExclusions() As String, ByVal bIsChartWarningThresholdEnabled As Boolean, ByVal bIsChartCriticalThresholdEnabled As Boolean, ByVal sChartWarningThresholdCode As String, ByVal sChartCriticalThresholdCode As String) As Boolean
        Dim oXMLExclude As XmlNode
        Dim oXmlSeriesNode, oXmlCodeNode As XmlNode
        Dim oXMLNewAttribute As XmlAttribute
        Dim oXMLCDataNode As XmlCDataSection
        Dim i As Integer

        oXMLChart.Attributes("CHARTTITLE").Value = sChartTitle
        oXMLChart.Attributes("DATASOURCE").Value = sDataSource
        oXMLChart.Attributes("ISTHRESHOLDSADDED").Value = sIsThresholdsAdded
        oXMLChart.Attributes("CHARTLABELS").Value = "instance"

        '// Remove all SERIES nodes.
        'oXMLChart.RemoveAll()
        For Each oXmlSeriesNode In oXMLChart.SelectNodes("./SERIES")
            For Each oXmlCodeNode In oXmlSeriesNode.SelectNodes("./CODE")
                oXmlSeriesNode.RemoveChild(oXmlCodeNode)
            Next
            oXMLChart.RemoveChild(oXmlSeriesNode)
        Next

        If bIsChartWarningThresholdEnabled = True Then
            oXmlSeriesNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "SERIES", "")
            oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
            oXMLNewAttribute.Value = "Warning"
            oXmlSeriesNode.Attributes.Append(oXMLNewAttribute)
            oXmlCodeNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CODE", "")
            oXMLCDataNode = g_xmlThresholdFile.CreateCDataSection(sChartWarningThresholdCode)
            oXmlCodeNode.AppendChild(oXMLCDataNode)
            oXmlSeriesNode.AppendChild(oXmlCodeNode)
            oXMLChart.AppendChild(oXmlSeriesNode)
        End If

        If bIsChartCriticalThresholdEnabled = True Then
            oXmlSeriesNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "SERIES", "")
            oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
            oXMLNewAttribute.Value = "Critical"
            oXmlSeriesNode.Attributes.Append(oXMLNewAttribute)
            oXmlCodeNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CODE", "")
            oXMLCDataNode = g_xmlThresholdFile.CreateCDataSection(sChartCriticalThresholdCode)
            oXmlCodeNode.AppendChild(oXMLCDataNode)
            oXmlSeriesNode.AppendChild(oXmlCodeNode)
            oXMLChart.AppendChild(oXmlSeriesNode)
        End If

        '// Remove all EXCLUDE nodes for a rebuild
        For Each oXMLExclude In oXMLChart.SelectNodes("./EXCLUDE")
            oXMLChart.RemoveChild(oXMLExclude)
        Next

        '// Add Exclusions if any
        If aExclusions(0) <> "" Then
            For i = 0 To UBound(aExclusions)
                oXMLExclude = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "EXCLUDE", "")
                oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("INSTANCE")
                oXMLNewAttribute.Value = aExclusions(i)
                oXMLExclude.Attributes.Append(oXMLNewAttribute)
                oXMLChart.AppendChild(oXMLExclude)
            Next
        End If
        Return True
    End Function

    Function UpdateThreshold(ByVal sName As String, ByVal sCondition As String, ByVal sColor As String, ByVal sPriority As String, ByVal sCode As String) As Boolean
        Dim oXMLDescription As XmlNode
        Dim oXMLCode, oXmlNode As XmlNode
        Dim oXMLCDataNode As XmlCDataSection
        Dim iCount As Integer

        '// Check to see if more than 1 threshold exists with the same name.
        If LCase(g_oXMLSelectedThreshold.Attributes("NAME").Value) = LCase(sName) Then
            '// Name will not be changed.
            iCount = 0
            For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
                If LCase(oXmlNode.Attributes("NAME").Value) = LCase(sName) Then
                    iCount = iCount + 1
                End If
            Next
            If iCount > 1 Then
                Return False
                Exit Function
            End If
        Else
            '// Name will be changed.
            iCount = 0
            For Each oXmlNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
                If LCase(oXmlNode.Attributes("NAME").Value) = LCase(sName) Then
                    iCount = iCount + 1
                End If
            Next
            If iCount > 0 Then
                Return False
                Exit Function
            End If
        End If



        g_oXMLSelectedThreshold.Attributes("NAME").Value = sName
        g_oXMLSelectedThreshold.Attributes("COLOR").Value = sColor
        g_oXMLSelectedThreshold.Attributes("CONDITION").Value = sCondition
        g_oXMLSelectedThreshold.Attributes("PRIORITY").Value = sPriority

        '// Remove all description nodes.
        For Each oXMLDescription In g_oXMLSelectedThreshold.SelectNodes("./DESCRIPTION")
            g_oXMLSelectedThreshold.RemoveChild(oXMLDescription)
        Next
        '// Create and set description field
        'oXMLDescription = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DESCRIPTION", "")
        'oXMLCDataNode = g_xmlThresholdFile.CreateCDataSection(sDescription)
        'oXMLDescription.AppendChild(oXMLCDataNode)
        'g_oXMLSelectedThreshold.AppendChild(oXMLDescription)


        '// Remove all code nodes.
        For Each oXMLCode In g_oXMLSelectedThreshold.SelectNodes("./CODE")
            g_oXMLSelectedThreshold.RemoveChild(oXMLCode)
        Next
        oXMLCode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CODE", "")
        oXMLCDataNode = g_xmlThresholdFile.CreateCDataSection(sCode)
        oXMLCode.AppendChild(oXMLCDataNode)
        g_oXMLSelectedThreshold.AppendChild(oXMLCode)

        RefreshFormWithAnalysisData(g_oXMLSelectedAnalysis.Attributes("NAME").Value)

        Return True
    End Function

    Sub AddChart()
        Dim oNewXMLChartNode As XmlNode
        Dim oXMLNewAttribute As XmlAttribute

        oNewXMLChartNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CHART", "")

        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CHARTTITLE")
        oXMLNewAttribute.Value = g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value
        oNewXMLChartNode.Attributes.Append(oXMLNewAttribute)

        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("DATASOURCE")
        oXMLNewAttribute.Value = g_oXMLSelectedAnalysis.Attributes("PRIMARYDATASOURCE").Value
        oNewXMLChartNode.Attributes.Append(oXMLNewAttribute)

        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("ISTHRESHOLDSADDED")
        oXMLNewAttribute.Value = "False"
        oNewXMLChartNode.Attributes.Append(oXMLNewAttribute)

        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CHARTLABELS")
        oXMLNewAttribute.Value = "instance"
        oNewXMLChartNode.Attributes.Append(oXMLNewAttribute)

        g_oXMLSelectedAnalysis.AppendChild(oNewXMLChartNode)

    End Sub

    Private Sub btnEditChart_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditChart.Click
        Dim ofrmChart As New frmChart
        If BindSelectedChartToXML() = True Then
            ofrmChart.oXMLChartNode = g_oXMLSelectedChart
            ofrmChart.ofrmMain = Me
            ofrmChart.oXMLAnalysisNode = g_oXMLSelectedAnalysis
            ofrmChart.XMLRoot = g_XMLRoot
            ofrmChart.Show(Me)
        Else
            AddChart()
            If BindSelectedChartToXML() = True Then
                ofrmChart.oXMLChartNode = g_oXMLSelectedChart
                ofrmChart.ofrmMain = Me
                ofrmChart.oXMLAnalysisNode = g_oXMLSelectedAnalysis
                ofrmChart.XMLRoot = g_XMLRoot
                ofrmChart.Show(Me)
            End If
        End If
    End Sub

    Function BindSelectedChartToXML() As Boolean
        Dim oXMLChartNode As XmlNode
        If IsNothing(g_oXMLSelectedAnalysis) = False Then
            For Each oXMLChartNode In g_oXMLSelectedAnalysis.SelectNodes("./CHART")
                g_oXMLSelectedChart = oXMLChartNode
                BindSelectedChartToXML = True
                Exit Function
            Next
            BindSelectedChartToXML = False
        End If
    End Function

    Private Sub btnThresholdNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdNew.Click
        Dim ofrmAddThreshold As New frmAddThreshold
        ofrmAddThreshold.ofrmMain = Me
        ofrmAddThreshold.XMLRoot = g_XMLRoot
        ofrmAddThreshold.oXmlAnalysisNode = g_oXMLSelectedAnalysis
        ofrmAddThreshold.Show(Me)
    End Sub

    Private Sub ListBoxThresholds_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxThresholds.SelectedIndexChanged
        BindSelectedListBoxThresholdItemToXML()
    End Sub

    Private Sub btnThresholdEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdEdit.Click
        Dim ofrmEditThreshold As New frmEditThresholds
        ofrmEditThreshold.oXmlThresholdNode = g_oXMLSelectedThreshold
        ofrmEditThreshold.ofrmMain = Me
        ofrmEditThreshold.XMLRoot = g_XMLRoot
        ofrmEditThreshold.oXmlAnalysisNode = g_oXMLSelectedAnalysis
        ofrmEditThreshold.Show(Me)
    End Sub

    Sub BindSelectedListBoxThresholdItemToXML()
        Dim oXMLThresholdNode As XmlNode

        For Each oXMLThresholdNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
            If LCase(oXMLThresholdNode.Attributes("NAME").Value) = LCase(ListBoxThresholds.SelectedItem) Then
                g_oXMLSelectedThreshold = oXMLThresholdNode
                Exit For
            End If
        Next
    End Sub

    Function AddThreshold(ByVal sName As String, ByVal sCondition As String, ByVal sColor As String, ByVal sPriority As String, ByVal sCode As String, ByVal sDescription As String) As Boolean
        Dim oXMLThresholdNode, oXMLDescriptionNode, oXMLCodeNode As XmlNode
        Dim oXMLNewAttribute As XmlAttribute
        Dim oXmlCDataNode As XmlCDataSection

        '// Check to see if this theshold already exists.
        For Each oXMLThresholdNode In g_oXMLSelectedAnalysis.SelectNodes("./THRESHOLD")
            If LCase(oXMLThresholdNode.Attributes("NAME").Value) = LCase(sName) Then
                MsgBox("Threshold already exists")
                Return False
                Exit Function
            End If
        Next

        '// Continue with normal threshold add
        oXMLThresholdNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "THRESHOLD", "")
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("NAME")
        oXMLNewAttribute.Value = sName
        oXMLThresholdNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("CONDITION")
        oXMLNewAttribute.Value = sCondition
        oXMLThresholdNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("COLOR")
        oXMLNewAttribute.Value = sColor
        oXMLThresholdNode.Attributes.Append(oXMLNewAttribute)
        oXMLNewAttribute = g_xmlThresholdFile.CreateAttribute("PRIORITY")
        oXMLNewAttribute.Value = sPriority
        oXMLThresholdNode.Attributes.Append(oXMLNewAttribute)

        '// Description
        oXMLDescriptionNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "DESCRIPTION", "")
        oXmlCDataNode = g_xmlThresholdFile.CreateCDataSection(sDescription)
        oXMLDescriptionNode.AppendChild(oXmlCDataNode)
        oXMLThresholdNode.AppendChild(oXMLDescriptionNode)

        '// Code
        oXMLCodeNode = g_xmlThresholdFile.CreateNode(XmlNodeType.Element, "CODE", "")
        oXmlCDataNode = g_xmlThresholdFile.CreateCDataSection(sCode)
        oXMLCodeNode.AppendChild(oXmlCDataNode)
        oXMLThresholdNode.AppendChild(oXMLCodeNode)

        g_oXMLSelectedAnalysis.AppendChild(oXMLThresholdNode)

        RefreshFormWithAnalysisData(g_oXMLSelectedAnalysis.Attributes("NAME").Value)
        Return True
    End Function

    Function RemoveThreshold(ByVal oXMLAnalysis As XmlNode, ByVal oXMLThreshold As XmlNode) As Boolean
        oXMLAnalysis.RemoveChild(oXMLThreshold)
        RefreshFormWithAnalysisData(g_oXMLSelectedAnalysis.Attributes("NAME").Value)
    End Function

    Private Sub btnThresholdDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnThresholdDelete.Click
        RemoveThreshold(g_oXMLSelectedAnalysis, g_oXMLSelectedThreshold)
        RefreshFormWithAnalysisData(g_oXMLSelectedAnalysis.Attributes("NAME").Value)
    End Sub
    Friend WithEvents GroupBoxAnalysis As System.Windows.Forms.GroupBox
    Friend WithEvents btnEditQuestions As System.Windows.Forms.Button

    Private Sub btnEditQuestions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditQuestions.Click
        Dim ofrmQuestions As New frmQuestions
        ofrmQuestions.oXMLRoot = g_XMLRoot
        ofrmQuestions.oXMLThresholdFile = g_xmlThresholdFile
        ofrmQuestions.Show(Me)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        Dim ofrmAbout As New AboutDialog
        ofrmAbout.Show()
    End Sub
    Friend WithEvents NewToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

    Private Sub NewToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewToolStripMenuItem.Click
        CreateAndOpenNewThresholdFile()
    End Sub

    Sub CreateAndOpenNewThresholdFile()
        Dim oNewXMLDoc As New XmlDocument
        Dim sXML As String

        sXML = "<?xml version=" & Chr(34) & "1.0" & Chr(34) & "?>" & vbNewLine & "<PAL NAME=" & Chr(34) & "New Threshold File" & Chr(34) & " DESCRIPTION=" & Chr(34) & "" & Chr(34) & " VERSION=" & Chr(34) & "1.0" & Chr(34) & " CONTENTOWNERS=" & Chr(34) & "" & Chr(34) & " FEEDBACKEMAILADDRESS=" & Chr(34) & "" & Chr(34) & ">" & vbNewLine & "</PAL>"
        oNewXMLDoc.LoadXml(sXML)
        'NAME="System Overview" VERSION="1.1" DESCRIPTION="General operating system analysis."
        g_xmlThresholdFile = oNewXMLDoc
        SaveToolStripMenuItem.Enabled = False
        g_ThresholdFilePath = ""
        g_XMLRoot = g_xmlThresholdFile.DocumentElement
        RefreshTreeViewAnalysis()
    End Sub

    Private Sub btnEditThresholdProperties_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditThresholdProperties.Click
        Dim ofrmEditThresholdFileProperties As New frmEditThresholdFileProperties
        ofrmEditThresholdFileProperties.sTitle = g_sThresholdFileTitle
        ofrmEditThresholdFileProperties.sVersion = g_sThresholdFileVersion
        ofrmEditThresholdFileProperties.sDescription = g_sThresholdFileDescription
        ofrmEditThresholdFileProperties.sContentOwners = g_sThresholdFileContentOwners
        ofrmEditThresholdFileProperties.sFeedbackEmailAddresses = g_sThresholdFileFeedbackEmailAddresses
        ofrmEditThresholdFileProperties.ofrmMain = Me
        ofrmEditThresholdFileProperties.Show(Me)
    End Sub

    Private Sub CheckBoxAnalysisEnabled_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBoxAnalysisEnabled.CheckedChanged
        'UpdateThresholdFile()
        'UpdateAnalysis()
    End Sub

    Private Sub ComboBoxCategory_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ComboBoxCategory.SelectedIndexChanged
        'UpdateThresholdFile()
        'UpdateAnalysis()
        'RefreshTreeViewAnalysis()
        'ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Private Sub txtAnalysisName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAnalysisName.TextChanged
        'UpdateThresholdFile()
        'UpdateAnalysis()
        'RefreshTreeViewAnalysis()
        'ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Private Sub txtAnalysisCounter_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAnalysisCounter.TextChanged
        'UpdateThresholdFile()
        'UpdateAnalysis()
        'RefreshTreeViewAnalysis()
        'ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Private Sub txtAnalysisDescription_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtAnalysisDescription.TextChanged
        Dim sHtmlHeader, sHtmlFooter As String
        sHtmlHeader = "<HTML>" & _
        "<HEAD>" & _
        "<STYLE TYPE=" & Chr(34) & "text/css" & Chr(34) & " TITLE=" & Chr(34) & "currentStyle" & Chr(34) & " MEDIA=" & Chr(34) & "screen" & Chr(34) & ">" & _
            "body" & _
            "{" & _
                "font: normal 8pt/16pt Verdana;" & _
                "color: #000000;" & _
                "margin: 10px;" & _
            "}" & _
            "p {font: 8pt/16pt Verdana;margin-top: 0px;}" & _
            "h1 {font: 20pt Verdana;margin-bottom: 0px;color: #000000;}" & _
            "h2 {font: 15pt Verdana;margin-bottom: 0px;color: #000000;}" & _
            "h3 {font: 13pt Verdana;margin-bottom: 0px;color: #000000;}" & _
            "td {font: normal 8pt Verdana;}" & _
            "th {font: bold 8pt Verdana;}" & _
            "blockquote {font: normal 8pt Verdana;}" & _
        "</STYLE>" & _
        "</HEAD>" & _
        "<BODY LINK=" & Chr(34) & "Black" & Chr(34) & " VLINK=" & Chr(34) & "Black" & Chr(34) & ">"
        sHtmlFooter = "</BODY></HTML>"
        WebBrowser1.DocumentText = sHtmlHeader & txtAnalysisDescription.Text & sHtmlFooter
        WebBrowser1.Update()

        'UpdateThresholdFile()
        'UpdateAnalysis()
        'RefreshTreeViewAnalysis()
        'ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

    Private Sub ListBoxDataSourceCounters_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ListBoxDataSourceCounters.SelectedIndexChanged
        'UpdateThresholdFile()
        'UpdateAnalysis()
        'RefreshTreeViewAnalysis()
        'ReSelectTreeViewNode(txtAnalysisName.Text)
    End Sub

End Class

Class oAnalysis
    Public Name As String
    Public Enabled As Boolean
    Public PrimaryDataSource As String
    Public Category As String
    Public Description As String
    Public Counters() As oCounter
    Public Chart As oChart
End Class

Class oCounter
    Public Name As String
    Public MinVarName As String
    Public AvgVarName As String
    Public MaxVarName As String
    Public TrendVarName As String
    Public DataType As String
    Public Exclude() As String
End Class

Class oThreshold
    Public Name As String
    Public Condition As String
    Public Color As Color
    Public Priority As Integer
    Public Description As String
    Public Code As String
End Class

Class oChart
    '<CHART CHARTTYPE="Line" CATEGORIES="AUTO" MAXCATEGORYLABELS="0" LEGEND="ON" VALUES="AUTO" GROUPSIZE="640x480" OTSFORMAT="MM/dd hh:mm" CHARTTITLE="\Processor(*)\% Processor Time" DATASOURCE="\Processor(*)\% Processor Time" DATATYPE="Integer"/>
    Public Charttitle As String
    Public Charttype As String
    Public Categories As String
    Public MaxCategoryLabels As String
    Public Legend As String
    Public Values As String
    Public Groupsize As String
    Public OTSFormat As String
    Public Datasource As String
    Public Datatype As String

End Class