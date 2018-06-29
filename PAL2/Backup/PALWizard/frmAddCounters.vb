Imports System.Diagnostics

Public Class frmAddCounters
    Inherits System.Windows.Forms.Form
    Public ofrmNewAnalysis As frmNewAnalysis
    Public ofrmAddDataSourceCounter As frmAddDataSourceCounter
    Friend LastUsedComputerName As String
    Friend ofrmMain As frmPALEditor
    Dim arrPerfObjects() As PerfCounter
    Public aCounters() As String
    Public strDestination As String
    Dim arrCounterExplains(0, 1) As String

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents ListBoxPerfCounters As System.Windows.Forms.ListBox
    Friend WithEvents ListBoxPerfInstances As System.Windows.Forms.ListBox
    Friend WithEvents cbPerfObjects As System.Windows.Forms.ComboBox
    Friend WithEvents txtComputerName As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents radAllPerfCounters As System.Windows.Forms.RadioButton
    Friend WithEvents radPerfCountersSelect As System.Windows.Forms.RadioButton
    Friend WithEvents radInstancesAll As System.Windows.Forms.RadioButton
    Friend WithEvents radInstancesSelect As System.Windows.Forms.RadioButton
    Friend WithEvents gbInstances As System.Windows.Forms.GroupBox
    Friend WithEvents gbCounters As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtExplain As System.Windows.Forms.TextBox
    Friend WithEvents btnConnect As System.Windows.Forms.Button
    Friend WithEvents StatusBar1 As System.Windows.Forms.StatusBar
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmAddCounters))
        Me.ListBoxPerfCounters = New System.Windows.Forms.ListBox
        Me.ListBoxPerfInstances = New System.Windows.Forms.ListBox
        Me.cbPerfObjects = New System.Windows.Forms.ComboBox
        Me.txtComputerName = New System.Windows.Forms.TextBox
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.radAllPerfCounters = New System.Windows.Forms.RadioButton
        Me.radPerfCountersSelect = New System.Windows.Forms.RadioButton
        Me.radInstancesAll = New System.Windows.Forms.RadioButton
        Me.radInstancesSelect = New System.Windows.Forms.RadioButton
        Me.gbInstances = New System.Windows.Forms.GroupBox
        Me.gbCounters = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtExplain = New System.Windows.Forms.TextBox
        Me.btnConnect = New System.Windows.Forms.Button
        Me.StatusBar1 = New System.Windows.Forms.StatusBar
        Me.gbInstances.SuspendLayout()
        Me.gbCounters.SuspendLayout()
        Me.SuspendLayout()
        '
        'ListBoxPerfCounters
        '
        Me.ListBoxPerfCounters.Location = New System.Drawing.Point(8, 64)
        Me.ListBoxPerfCounters.Name = "ListBoxPerfCounters"
        Me.ListBoxPerfCounters.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxPerfCounters.Size = New System.Drawing.Size(168, 95)
        Me.ListBoxPerfCounters.TabIndex = 1
        '
        'ListBoxPerfInstances
        '
        Me.ListBoxPerfInstances.Location = New System.Drawing.Point(8, 64)
        Me.ListBoxPerfInstances.Name = "ListBoxPerfInstances"
        Me.ListBoxPerfInstances.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBoxPerfInstances.Size = New System.Drawing.Size(168, 95)
        Me.ListBoxPerfInstances.TabIndex = 2
        '
        'cbPerfObjects
        '
        Me.cbPerfObjects.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPerfObjects.DropDownWidth = 184
        Me.cbPerfObjects.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.cbPerfObjects.IntegralHeight = False
        Me.cbPerfObjects.ItemHeight = 13
        Me.cbPerfObjects.Location = New System.Drawing.Point(8, 64)
        Me.cbPerfObjects.MaxDropDownItems = 30
        Me.cbPerfObjects.Name = "cbPerfObjects"
        Me.cbPerfObjects.Size = New System.Drawing.Size(184, 21)
        Me.cbPerfObjects.Sorted = True
        Me.cbPerfObjects.TabIndex = 3
        '
        'txtComputerName
        '
        Me.txtComputerName.AcceptsReturn = True
        Me.txtComputerName.Location = New System.Drawing.Point(8, 24)
        Me.txtComputerName.Name = "txtComputerName"
        Me.txtComputerName.Size = New System.Drawing.Size(128, 20)
        Me.txtComputerName.TabIndex = 4
        Me.txtComputerName.Text = "txtComputerName"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(16, 272)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.TabIndex = 5
        Me.btnAdd.Text = "Add"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(296, 280)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 8)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(176, 16)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Select counters from computer:"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 16)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Performance object:"
        '
        'radAllPerfCounters
        '
        Me.radAllPerfCounters.Location = New System.Drawing.Point(8, 16)
        Me.radAllPerfCounters.Name = "radAllPerfCounters"
        Me.radAllPerfCounters.TabIndex = 10
        Me.radAllPerfCounters.Text = "All counters"
        '
        'radPerfCountersSelect
        '
        Me.radPerfCountersSelect.Location = New System.Drawing.Point(8, 40)
        Me.radPerfCountersSelect.Name = "radPerfCountersSelect"
        Me.radPerfCountersSelect.Size = New System.Drawing.Size(152, 24)
        Me.radPerfCountersSelect.TabIndex = 11
        Me.radPerfCountersSelect.Text = "Select counters from list"
        '
        'radInstancesAll
        '
        Me.radInstancesAll.Location = New System.Drawing.Point(8, 16)
        Me.radInstancesAll.Name = "radInstancesAll"
        Me.radInstancesAll.TabIndex = 12
        Me.radInstancesAll.Text = "All instances"
        '
        'radInstancesSelect
        '
        Me.radInstancesSelect.Location = New System.Drawing.Point(8, 40)
        Me.radInstancesSelect.Name = "radInstancesSelect"
        Me.radInstancesSelect.Size = New System.Drawing.Size(152, 24)
        Me.radInstancesSelect.TabIndex = 13
        Me.radInstancesSelect.Text = "Select instances from list"
        '
        'gbInstances
        '
        Me.gbInstances.Controls.Add(Me.radInstancesSelect)
        Me.gbInstances.Controls.Add(Me.radInstancesAll)
        Me.gbInstances.Controls.Add(Me.ListBoxPerfInstances)
        Me.gbInstances.Location = New System.Drawing.Point(192, 96)
        Me.gbInstances.Name = "gbInstances"
        Me.gbInstances.Size = New System.Drawing.Size(184, 168)
        Me.gbInstances.TabIndex = 14
        Me.gbInstances.TabStop = False
        Me.gbInstances.Text = "Instances"
        '
        'gbCounters
        '
        Me.gbCounters.Controls.Add(Me.radPerfCountersSelect)
        Me.gbCounters.Controls.Add(Me.radAllPerfCounters)
        Me.gbCounters.Controls.Add(Me.ListBoxPerfCounters)
        Me.gbCounters.Location = New System.Drawing.Point(8, 96)
        Me.gbCounters.Name = "gbCounters"
        Me.gbCounters.Size = New System.Drawing.Size(184, 168)
        Me.gbCounters.TabIndex = 14
        Me.gbCounters.TabStop = False
        Me.gbCounters.Text = "Counters"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(200, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(112, 16)
        Me.Label3.TabIndex = 15
        Me.Label3.Text = "Counter description:"
        '
        'txtExplain
        '
        Me.txtExplain.Location = New System.Drawing.Point(200, 16)
        Me.txtExplain.Multiline = True
        Me.txtExplain.Name = "txtExplain"
        Me.txtExplain.ReadOnly = True
        Me.txtExplain.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtExplain.Size = New System.Drawing.Size(176, 72)
        Me.txtExplain.TabIndex = 16
        Me.txtExplain.Text = ""
        '
        'btnConnect
        '
        Me.btnConnect.Location = New System.Drawing.Point(136, 24)
        Me.btnConnect.Name = "btnConnect"
        Me.btnConnect.Size = New System.Drawing.Size(56, 20)
        Me.btnConnect.TabIndex = 1
        Me.btnConnect.Text = "Connect"
        '
        'StatusBar1
        '
        Me.StatusBar1.Location = New System.Drawing.Point(0, 307)
        Me.StatusBar1.Name = "StatusBar1"
        Me.StatusBar1.Size = New System.Drawing.Size(384, 16)
        Me.StatusBar1.TabIndex = 18
        '
        'frmAddCounters
        '
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.ClientSize = New System.Drawing.Size(384, 323)
        Me.Controls.Add(Me.StatusBar1)
        Me.Controls.Add(Me.btnConnect)
        Me.Controls.Add(Me.txtExplain)
        Me.Controls.Add(Me.txtComputerName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.cbPerfObjects)
        Me.Controls.Add(Me.gbInstances)
        Me.Controls.Add(Me.gbCounters)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "frmAddCounters"
        Me.Text = "Add Counters"
        Me.gbInstances.ResumeLayout(False)
        Me.gbCounters.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private Sub frmAddCounters_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '<Populate txtComputerName with the local machine name>
        If LastUsedComputerName = "" Then
            txtComputerName.Text = System.Environment.MachineName
        Else
            txtComputerName.Text = LastUsedComputerName
        End If

        '</Populate txtComputerName with the local machine name>
        EnumeratePerfObjects(txtComputerName.Text)
    End Sub

    Private Sub EnumeratePerfObjects(ByVal strComputerName As String)
        StatusBar1.Text = "Connecting to " & strComputerName & "..."

        Dim arrPerfObjectNames() As String
        Dim o, i, c As Integer
        Dim objCategories() As PerformanceCounterCategory

        On Error Resume Next
        objCategories = PerformanceCounterCategory.GetCategories(strComputerName)
        If Err.Number <> 0 Then
            MsgBox("Unable to connect to " & strComputerName)
            Exit Sub
        End If

        radPerfCountersSelect.Checked = True
        radInstancesSelect.Checked = True
        ListBoxPerfCounters.Items.Clear()
        ListBoxPerfInstances.Items.Clear()
        cbPerfObjects.Items.Clear()

        ReDim arrPerfObjectNames(UBound(objCategories))
        For o = 0 To UBound(objCategories)
            arrPerfObjectNames(o) = objCategories(o).CategoryName.ToString
        Next

        Array.Sort(arrPerfObjectNames)

        For o = 0 To UBound(arrPerfObjectNames)
            cbPerfObjects.Items.Add(arrPerfObjectNames(o))
        Next
        cbPerfObjects.SelectedItem = "Processor"
    End Sub

    Private Sub ListBoxPerfObjects_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        EnumPerfCountersAndInstances(txtComputerName.Text)
    End Sub

    Private Structure PerfCounter
        Public Name As String
        Public Instances() As String
    End Structure

    Private Structure PerfObject
        Public Name As String
        Public Counters() As PerfCounter
    End Structure

    Private Sub EnumPerfCountersAndInstances(ByVal strMachineName As String)
        Dim i As Integer
        ListBoxPerfCounters.Items.Clear()
        ListBoxPerfInstances.Items.Clear()
        'gbInstances.Enabled = True
        gbCounters.Enabled = True
        radInstancesAll.Checked = False
        radInstancesSelect.Checked = True

        btnAdd.Enabled = True

        Dim arrPerfObjectNames() As String
        Dim o, c As Integer
        Dim arrTemp() As String

        Dim objPerfCategory As New PerformanceCounterCategory(cbPerfObjects.SelectedItem, strMachineName)
        Dim objPerfCounters() As PerformanceCounter
        Dim strPerfInstanceNames() As String

        On Error Resume Next
        strPerfInstanceNames = objPerfCategory.GetInstanceNames()

        If IsNothing(strPerfInstanceNames) = False Then
            If strPerfInstanceNames.Length > 0 Then
                objPerfCounters = objPerfCategory.GetCounters(strPerfInstanceNames(0).ToString)
            Else
                objPerfCounters = objPerfCategory.GetCounters()
                'gbInstances.Enabled = False
            End If
        Else
            objPerfCounters = objPerfCategory.GetCounters()
            'gbInstances.Enabled = False
        End If

        If objPerfCounters.Length = 0 Then
            gbCounters.Enabled = False
        End If

        If IsNothing(objPerfCounters) = True And IsNothing(strPerfInstanceNames) = True Then
            btnAdd.Enabled = False
            Exit Sub
        End If

        If objPerfCounters.Length > 0 Or strPerfInstanceNames.Length > 0 Then
            ReDim arrTemp(objPerfCounters.GetUpperBound(0))
            ReDim arrCounterExplains(objPerfCounters.GetUpperBound(0), 1)
            For c = 0 To objPerfCounters.GetUpperBound(0)
                arrTemp(c) = objPerfCounters(c).CounterName
                arrCounterExplains(c, 0) = objPerfCounters(c).CounterName
                arrCounterExplains(c, 1) = objPerfCounters(c).CounterHelp
            Next
            Array.Sort(arrTemp)
            For c = 0 To UBound(arrTemp)
                ListBoxPerfCounters.Items.Add(arrTemp(c))
            Next

            For i = 0 To strPerfInstanceNames.GetUpperBound(0)
                Array.Sort(strPerfInstanceNames)
                ListBoxPerfInstances.Items.Add(strPerfInstanceNames(i).ToString)
            Next
            On Error GoTo 0
            If ListBoxPerfInstances.Items.Count > 0 Then
                ListBoxPerfInstances.SetSelected(0, True)
            End If
            If ListBoxPerfCounters.Items.Count > 0 Then
                ListBoxPerfCounters.SetSelected(0, True)
            End If
        Else
            btnAdd.Enabled = False
            Exit Sub
        End If
        StatusBar1.Text = "Connected to " & txtComputerName.Text
        LastUsedComputerName = txtComputerName.Text
    End Sub

    Private Sub cbPerfObjects_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPerfObjects.SelectedIndexChanged
        txtExplain.Text = ""
        EnumPerfCountersAndInstances(txtComputerName.Text)
    End Sub

    Private Sub radAllPerfCounters_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radAllPerfCounters.CheckedChanged
        If radAllPerfCounters.Checked = True Then
            Dim i As Integer
            For i = 0 To ListBoxPerfCounters.Items.Count - 1
                ListBoxPerfCounters.SetSelected(i, True)
                ListBoxPerfCounters.Enabled = False
            Next
        End If
    End Sub

    Private Sub radPerfCountersSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radPerfCountersSelect.CheckedChanged
        ListBoxPerfCounters.Enabled = True
    End Sub

    Private Sub radInstancesAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radInstancesAll.CheckedChanged
        If radInstancesAll.Checked = True Then
            Dim i As Integer
            For i = 0 To ListBoxPerfInstances.Items.Count - 1
                ListBoxPerfInstances.SetSelected(i, True)
                ListBoxPerfInstances.Enabled = False
            Next
        End If
    End Sub

    Private Sub radInstancesSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radInstancesSelect.CheckedChanged
        ListBoxPerfInstances.Enabled = True
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim sCounterPath, sPerfObject, sCounter, sInstance, sVarName As String

        Select Case Me.Owner.Name
            Case "frmNewAnalysis"
                sPerfObject = cbPerfObjects.SelectedItem.ToString
                sCounter = ListBoxPerfCounters.SelectedItem.ToString
                If radInstancesAll.Checked = True Then
                    If ListBoxPerfInstances.Items.Count > 0 Then
                        sInstance = "*"
                    Else
                        sInstance = ""
                    End If
                Else
                    If ListBoxPerfInstances.SelectedItems.Count > 0 Then
                        sInstance = ListBoxPerfInstances.SelectedItem.ToString
                    Else
                        If ListBoxPerfInstances.Items.Count > 0 Then
                            sInstance = "*"
                        Else
                            sInstance = ""
                        End If
                    End If
                End If
                If sInstance = "" Then
                    sCounterPath = "\" & sPerfObject & "\" & sCounter
                Else
                    sCounterPath = "\" & sPerfObject & "(" & sInstance & ")\" & sCounter
                End If

                ofrmNewAnalysis.txtNewAnalysisName.Text = GetCounterObject(sCounterPath) & " " & GetCounterName(sCounterPath)
                ofrmNewAnalysis.txtNewAnalysisCounter.Text = sCounterPath
                ofrmNewAnalysis.ComboBoxCategory.Text = cbPerfObjects.Text
                ofrmNewAnalysis.txtNewAnalysisDescription.Text = txtExplain.Text
                ofrmNewAnalysis.LastUsedComputerName = LastUsedComputerName
            Case "frmAddDataSourceCounter"
                sPerfObject = cbPerfObjects.SelectedItem.ToString
                sCounter = ListBoxPerfCounters.SelectedItem.ToString
                If radInstancesAll.Checked = True Then
                    If ListBoxPerfInstances.Items.Count > 0 Then
                        sInstance = "*"
                    Else
                        sInstance = ""
                    End If
                Else
                    If ListBoxPerfInstances.SelectedItems.Count > 0 Then
                        sInstance = ListBoxPerfInstances.SelectedItem.ToString
                    Else
                        If ListBoxPerfInstances.Items.Count > 0 Then
                            sInstance = "*"
                        Else
                            sInstance = ""
                        End If
                    End If
                End If
                If sInstance = "" Then
                    sCounterPath = "\" & sPerfObject & "\" & sCounter
                Else
                    sCounterPath = "\" & sPerfObject & "(" & sInstance & ")\" & sCounter
                End If
                sVarName = "CollectionOf" & ConvertCounterToVarName(sCounterPath)
                ofrmAddDataSourceCounter.txtCounterName.Text = sCounterPath
                ofrmAddDataSourceCounter.ComboBoxDataSourceCounterDataType.Text = "integer"
                ofrmAddDataSourceCounter.txtCollectionVarName.Text = sVarName
                ofrmAddDataSourceCounter.LastUsedComputerName = LastUsedComputerName
        End Select
    End Sub

    Function ConvertCounterToVarName(ByVal sCounter As String)
        Dim sCounterObject, sCounterName, sCounterInstance As String
        ' \\IDCWEB1\Processor(_Total)\% Processor Time
        sCounterObject = GetCounterObject(sCounter)
        sCounterName = GetCounterName(sCounter)
        sCounterInstance = GetCounterInstance(sCounter)
        If sCounterInstance <> "*" Then
            ConvertCounterToVarName = sCounterObject & sCounterName & sCounterInstance
        Else
            ConvertCounterToVarName = sCounterObject & sCounterName & "All"
        End If

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

    'Public Function OneCounterWithInstanceWildCards()
    '    Dim sCounter As String



    '    Return sCounter

    '    'Dim strBaseCounterPath, strCounterPath, strModifiedCounterPath As String
    '    ''Dim arrTemp()
    '    ''Dim arrTemp2() As sAlert
    '    'Dim i, c, d, a, b As Integer
    '    'Dim bDuplicateFound As Boolean
    '    'Dim arrCounterPath(), arrTempCounterPath() As String

    '    'strBaseCounterPath = cbPerfObjects.Text 'Processor
    '    'strCounterPath = strBaseCounterPath

    '    'If radInstancesAll.Checked = True Then
    '    '    ReDim arrTempCounterPath(0)
    '    '    arrTempCounterPath(0) = strCounterPath & "(*)" 'Processor(*)
    '    'Else
    '    '    'Enum through all of the instances
    '    '    If ListBoxPerfInstances.SelectedItems.Count > 0 Then
    '    '        ReDim arrTempCounterPath(ListBoxPerfInstances.SelectedItems.Count - 1)
    '    '        For i = 0 To ListBoxPerfInstances.SelectedItems.Count - 1
    '    '            arrTempCounterPath(i) = strCounterPath & "(" & ListBoxPerfInstances.SelectedItems(i) & ")" 'Processor(_Total)
    '    '        Next
    '    '    Else
    '    '        ReDim arrTempCounterPath(0)
    '    '        arrTempCounterPath(0) = strCounterPath 'Processor
    '    '    End If
    '    'End If

    '    'If radAllPerfCounters.Checked = True Then
    '    '    a = 0
    '    '    For i = 0 To UBound(arrTempCounterPath)
    '    '        ReDim Preserve arrCounterPath(a)
    '    '        arrCounterPath(a) = arrTempCounterPath(i) & "\" & "*" 'Processor(*)\*
    '    '        a = a + 1
    '    '    Next
    '    'Else
    '    '    If ListBoxPerfCounters.SelectedItems.Count > 0 Then
    '    '        a = 0
    '    '        For i = 0 To UBound(arrTempCounterPath)
    '    '            For c = 0 To ListBoxPerfCounters.SelectedItems.Count - 1
    '    '                ReDim Preserve arrCounterPath(a)
    '    '                arrCounterPath(a) = arrTempCounterPath(i) & "\" & ListBoxPerfCounters.SelectedItems(c) 'Processor(*)\% Processor Time
    '    '                a = a + 1
    '    '            Next
    '    '        Next
    '    '    Else
    '    '        For i = 0 To UBound(arrTempCounterPath)
    '    '            ReDim Preserve arrCounterPath(a)
    '    '            arrCounterPath(a) = arrTempCounterPath(i)
    '    '            a = a + 1
    '    '        Next
    '    '    End If
    '    'End If

    '    'Select Case UCase(strDestination)
    '    '    Case UCase("frmEditThresholds")

    '    '        For c = 0 To ListBoxPerfCounters.SelectedItems.Count - 1
    '    '            strCounterPath = strBaseCounterPath & "\" & ListBoxPerfCounters.SelectedItems(c) 'Processor\% Processor Time

    '    '            'strModifiedCounterPath = Mid(strCounterPath, InStr(3, strCounterPath, "\") + 1)
    '    '            bDuplicateFound = False
    '    '            If IsNothing(AlertProfiles) = True Then
    '    '                Exit Sub
    '    '            End If

    '    '            '<find the selected profile>
    '    '            Dim p As Integer
    '    '            p = FindSelectedAlertProfile(frmEditThresholds.cbAlertProfiles.Text)
    '    '            If p = -1 Then
    '    '                Exit Sub
    '    '            End If
    '    '            '</find the selected profile>

    '    '            If IsNothing(AlertProfiles(p)) = True Then
    '    '                Exit Sub
    '    '            End If

    '    '            If IsNothing(AlertProfiles(p).arrAlertsInProfile) = False Then
    '    '                For d = 0 To UBound(AlertProfiles(p).arrAlertsInProfile)
    '    '                    If IsNothing(AlertProfiles(p).arrAlertsInProfile(d)) = False Then
    '    '                        If AlertProfiles(p).arrAlertsInProfile(d).Name = strCounterPath Then
    '    '                            bDuplicateFound = True
    '    '                            Exit For
    '    '                        End If
    '    '                    End If
    '    '                Next
    '    '                If bDuplicateFound = False Then
    '    '                    '<add to memory>
    '    '                    If IsNothing(AlertProfiles(p).arrAlertsInProfile) = True Then
    '    '                        ReDim AlertProfiles(p).arrAlertsInProfile(0)
    '    '                    Else
    '    '                        ReDim Preserve AlertProfiles(p).arrAlertsInProfile(UBound(AlertProfiles(p).arrAlertsInProfile) + 1)
    '    '                    End If
    '    '                    AlertProfiles(p).arrAlertsInProfile(UBound(AlertProfiles(p).arrAlertsInProfile)).Name = strCounterPath

    '    '                    Dim e As Integer
    '    '                    Dim strExplain As String
    '    '                    For e = 0 To UBound(arrCounterExplains)
    '    '                        If IsNothing(arrCounterExplains(e, 0)) = False Or IsNothing(arrCounterExplains(e, 1)) = False Then
    '    '                            If arrCounterExplains(e, 0) = ListBoxPerfCounters.SelectedItems(c) Then
    '    '                                strExplain = arrCounterExplains(e, 1)
    '    '                                Exit For
    '    '                            End If
    '    '                        End If
    '    '                    Next

    '    '                    AlertProfiles(p).arrAlertsInProfile(UBound(AlertProfiles(p).arrAlertsInProfile)).Comment = strExplain
    '    '                    '</add to memory>
    '    '                    frmEditThresholds.listCounters.Items.Add(strCounterPath)
    '    '                End If
    '    '            Else
    '    '                ReDim AlertProfiles(p).arrAlertsInProfile(0)
    '    '                AlertProfiles(p).arrAlertsInProfile(UBound(AlertProfiles(p).arrAlertsInProfile)).Name = strCounterPath
    '    '                '</add to memory>
    '    '                frmEditThresholds.listCounters.Items.Add(strCounterPath)
    '    '            End If
    '    '        Next

    '    '    Case UCase("CounterGroup")

    '    '        'frmParent.AxAddCountersSystemMonitor.Counters.Add(strCounterPath)
    '    '        frmParent.StatusBar1.Text = "Adding counters to counter group in treeview..."
    '    '        Dim iCG As Integer

    '    '        '<Find the CounterGroup we are modifiying>
    '    '        For a = 0 To UBound(CounterGroups)
    '    '            If frmParent.TreeView1.SelectedNode.Text = CounterGroups(a).Name Then
    '    '                iCG = a
    '    '                Exit For
    '    '            End If
    '    '        Next
    '    '        '<Find the CounterGroup we are modifiying>

    '    '        Dim bFound As Boolean
    '    '        Dim intTreeNode As Integer
    '    '        For a = 0 To UBound(arrCounterPath)

    '    '            '<Does this path already exist? If yes, then don't add it.>
    '    '            If IsNothing(CounterGroups(iCG).Counters) = False Then
    '    '                bFound = False
    '    '                For b = 0 To UBound(CounterGroups(iCG).Counters)
    '    '                    If arrCounterPath(a) = CounterGroups(iCG).Counters(b).Name Then
    '    '                        bFound = True
    '    '                    End If
    '    '                Next
    '    '            Else
    '    '                bFound = False
    '    '            End If

    '    '            '</Does this path already exist? If yes, then don't add it.>

    '    '            If bFound = False Then
    '    '                frmParent.CreateCounter(arrCounterPath(a), iCG)
    '    '            End If
    '    '        Next
    '    '        CounterGroups(iCG).TreeNode.ExpandAll()
    '    'End Select


    'End Function

    Private Sub ListBoxPerfCounters_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListBoxPerfCounters.SelectedIndexChanged
        Dim i As Integer

        If IsNothing(arrCounterExplains) = True Then
            txtExplain.Text = ""
            Exit Sub
        End If

        For i = 0 To UBound(arrCounterExplains)
            If IsNothing(arrCounterExplains(i, 0)) = False Or IsNothing(arrCounterExplains(i, 1)) = False Then
                If arrCounterExplains(i, 0) = ListBoxPerfCounters.Items.Item(ListBoxPerfCounters.SelectedIndex) Then
                    txtExplain.Text = arrCounterExplains(i, 1)
                    Exit Sub
                End If
            Else
                txtExplain.Text = ""
            End If
        Next
        'Find counter in arrCounterExplains, then get help.

    End Sub

    'Sub AddCounterToCounterGroup(ByVal strCounterPath As String, ByVal iCounterGroup As Integer)
    '    Dim strModifiedCounterPath As String
    '    Dim i, intTreeNode As Integer

    '    frmParent.StatusBar1.Text = "Adding counters to counter group in treeview..."
    '    'strModifiedCounterPath = Mid(strCounterPath, InStr(3, strCounterPath, "\") + 1)
    '    'MsgBox(AxAddCountersSystemMonitor.Counters.Count)

    '    '<Does this path already exist? If yes, then don't add it.>
    '    For i = 0 To frmParent.TreeView1.SelectedNode.GetNodeCount(False) - 1
    '        If frmParent.TreeView1.SelectedNode.Nodes(i).Text = strModifiedCounterPath Then
    '            'AxAddCountersSystemMonitor.Reset()
    '            frmParent.StatusBar1.Text = "Done"
    '            Exit Sub
    '        End If
    '    Next
    '    '</Does this path already exist? If yes, then don't add it.>

    '    '<Add to TreeView>
    '    Dim objNewTreeNodeCounter As New TreeNode
    '    objNewTreeNodeCounter.Text = strModifiedCounterPath
    '    objNewTreeNodeCounter.ImageIndex = frmParent.enumImage.Counter
    '    objNewTreeNodeCounter.SelectedImageIndex = frmParent.enumImage.Counter
    '    intTreeNode = frmParent.TreeView1.SelectedNode.Nodes.Add(objNewTreeNodeCounter)
    '    '</Add to TreeView>

    '    Dim a, b As Integer
    '    If IsNothing(CounterGroups) = False Then
    '        For a = 0 To UBound(CounterGroups)
    '            If IsNothing(CounterGroups(a)) = False Then
    '                If frmParent.TreeView1.SelectedNode.Text = CounterGroups(a).Name Then
    '                    If IsNothing(CounterGroups(a).Counters) = True Then
    '                        ReDim CounterGroups(a).Counters(0)
    '                    Else
    '                        ReDim Preserve CounterGroups(a).Counters(UBound(CounterGroups(a).Counters) + 1)
    '                    End If
    '                    b = UBound(CounterGroups(a).Counters)
    '                    CounterGroups(a).Counters(b).Name = strModifiedCounterPath
    '                    CounterGroups(a).Counters(b).TreeNode = frmParent.TreeView1.Nodes(0).Nodes(1).Nodes(intTreeNode)
    '                    Exit For
    '                End If
    '            End If
    '        Next
    '    End If

    '    frmParent.TreeView1.SelectedNode.Expand()
    '    'frmParent.AxAddCountersSystemMonitor.Reset()
    '    frmParent.StatusBar1.Text = "Done"
    'End Sub

    Private Sub btnConnect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConnect.Click
        StatusBar1.Text = "Connecting to " & txtComputerName.Text & "..."
        EnumeratePerfObjects(txtComputerName.Text)
    End Sub

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
        iLocFirstParen = Instr(sCounterInstance, "(")
        If iLocFirstParen = 0 Then
            GetCounterInstance = ""
            Exit Function
        End If
        iLocSecondParen = Instr(sCounterInstance, ")")
        iLen = iLocSecondParen - iLocFirstParen - 1
        GetCounterInstance = Mid(sCounterInstance, iLocFirstParen + 1, iLen)
    End Function
End Class
