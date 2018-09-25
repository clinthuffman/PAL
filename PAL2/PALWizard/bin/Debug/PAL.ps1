#requires -Version 2.0
param($Log='SamplePerfmonLog.blg',$ThresholdFile="QuickSystemOverview.xml",$AnalysisInterval='AUTO',$IsOutputHtml=$True,$IsOutputXml=$False,$HtmlOutputFileName="[LogFileName]_PAL_[DateTimeStamp].htm",$XmlOutputFileName="[LogFileName]_PAL_[DateTimeStamp].xml",$OutputDir="[My Documents]\PAL Reports",$AllCounterStats=$False,$BeginTime=$null,$EndTime=$null,[System.Int32] $NumberOfThreads=1,$IsLowPriority=$False,$DisplayReport=$True)
Set-StrictMode -Version 2
cls
#//
#// PAL v2.8
#// Written by Clint Huffman (clinthuffman@hotmail.com)
#// This tool is not supported by Microsoft. 
#// Please post all of your support questions to the PAL web site at http://github.com/clinthuffman/PAL

#////////////////////////////////
#// Global changable variables
#///////////////////////////////

$Error.Clear()
$Version = '2.8.1'
$AutoAnalysisIntervalNumberOfTimeSlices = 30

#// Chart Settings
$CHART_LINE_THICKNESS = 3 #// 2 is thin, 3 is normal, 4 is thick
$CHART_WIDTH = 620        #// Width in pixels
$CHART_HEIGHT = 620       #// height in pixels
$CHART_MAX_INSTANCES = 12 #// the maximum number of counter instance in a chart
$CHART_MAX_NUMBER_OF_AXIS_X_INTERVALS = 30 #// The maximum allowed X axis labels in the chart.

#//////////////////////////////////////////////////////////////////
#// Global unchangable variables - DO NOT CHANGE THESE VARIABLES
#/////////////////////////////////////////////////////////////////

$global:oPal = New-Object System.Object
$global:sPerfLogFilePath = ''
$global:ChartSettings = New-Object System.Object
$global:Jobs = @()
$global:htVariables = @{}
$global:htCodeReplacements = @{}
$global:htCounterInstanceStats = @{}
$global:sDateTimePattern = 'yyyy.MM.dd-HH:mm:ss'
$global:ScriptExecutionBeginTime = (Get-Date)
$global:oOverallProgress = ''
$global:OverallActiveAnalysis = ''
$global:ErrorNumber = 0

#///////////////
#// Functions
#//////////////

Function Test-Property 
{
    #// Provided by Jeffrey Snover
	param ([Parameter(Position=0,Mandatory=1)]$InputObject,[Parameter(Position=1,Mandatory=1)]$Name)
	[Bool](Get-Member -InputObject $InputObject -Name $Name -MemberType *Property)
}

function Decode-XmlEscapeValues
{
    param([string] $Value)
    $Value = $Value -Replace '&amp;','&'
    $Value = $Value -Replace '&quot;','"'
    $Value = $Value -Replace '&lt;','<'
    $Value = $Value -Replace '&gt;','>'
    Return $Value
}

Function OpenHtmlReport
{
    param([string] $HtmlOutputFileName = '')

    $HtmlOutputFileName
    If ($(Test-Property $global:oPal -Name 'ArgsProcessed') -eq $True)
    {
        If (($global:oPal.ArgsProcessed.DisplayReport -eq $false) -or ($global:oPal.ArgsProcessed.IsOutputHtml -eq $False))
        {
            #// Don't automatically open the report or error.
            Return
        }
    }
    #// The ambersand is needed because there might be spaces in the file path to the HTML report.
    Invoke-Expression -Command "&'$HtmlOutputFileName'"
}

Function WriteErrorToHtmlAndShow
{
    param([string] $sError,[boolean] $IsFatal = $False)

    [bool] $IsArgsProcessed = $False
    If ($(Test-Property -InputObject $global:oPal -Name ArgsProcessed) -eq $True)
    {
        If ($(Test-Property -InputObject $global:oPal.ArgsProcessed -Name HtmlOutputFileName) -eq $True)
        {
            $h = $global:oPal.ArgsProcessed.HtmlOutputFileName
            $IsArgsProcessed = $True
        }        
    }
    
    If ($IsArgsProcessed -eq $False)
    {
        $sHtmlReportFile = "[LogFileName]_PAL.htm"
        $UsersMyDocumentsFolder = [environment]::GetFolderPath("MyDocuments")
        $PalReportsDefaultFolderPath = "$UsersMyDocumentsFolder\PAL Reports"

	    $sLogFileName = $Log
	    If ($sLogFileName.Contains(";"))
	    {
		    $aStrings = $sLogFileName.Split(";")
		    $sLogFileName = $aStrings[0]
	    }
	    If ($sLogFileName.Contains("\"))
	    {
		    $aStrings = $sLogFileName.Split("\")
		    $sLogFileName = $aStrings[$aStrings.GetUpperBound(0)]
	    }
	    $sLogFileName = $sLogFileName.SubString(0,$sLogFileName.Length - 4)
        $sLogFileName = "$sLogFileName" + 'Error' + "$global:ErrorNumber"
        $sLogFileName = ConvertStringToFileName $sLogFileName

        $SessionGuid = [System.GUID]::NewGUID()
	    $sHtmlReportFile =  $sHtmlReportFile -replace("\[LogFileName\]",$sLogFileName)
        $sHtmlReportFile =  $sHtmlReportFile -replace("\[GUID\]",$SessionGuid)
        
        $h = "$PalReportsDefaultFolderPath\$sHtmlReportFile"
    }
    
    
    #///////////////////////
    #// Header
    #///////////////////////    
    '<HTML>' > $h
    '<HEAD>' >> $h
    '<STYLE TYPE="text/css" TITLE="currentStyle" MEDIA="screen">' >> $h
    'body {' >> $h
    '   font: normal 8pt/16pt Verdana;' >> $h
    '   color: #000000;' >> $h
    '   margin: 10px;' >> $h
    '   }' >> $h
    'p {font: 8pt/16pt Verdana;margin-top: 0px;}' >> $h
    'h1 {font: 20pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'h2 {font: 15pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'h3 {font: 13pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'td {font: normal 8pt Verdana;}' >> $h
    'th {font: bold 8pt Verdana;}' >> $h
    'blockquote {font: normal 8pt Verdana;}' >> $h
    '</STYLE>' >> $h
    '</HEAD>' >> $h
    '<BODY LINK="Black" VLINK="Black">' >> $h
    '<TABLE CELLPADDING=10 WIDTH="100%"><TR><TD BGCOLOR="#000000">' >> $h
    '<FONT COLOR="#FFFFFF" FACE="Tahoma" SIZE="5"><STRONG>Error processing "' + $($Log) + '"</STRONG></FONT><BR><BR>' >> $h
    '<FONT COLOR="#FFFFFF" FACE="Tahoma" SIZE="2"><STRONG>Report Generated at: ' + "$((get-date).tostring($global:sDateTimePattern))" + '</STRONG></FONT>' >> $h
    '</TD><TD><A HREF="http://github.com/clinthuffman/PAL"><FONT COLOR="#000000" FACE="Tahoma" SIZE="10">PAL</FONT><FONT COLOR="#000000" FACE="Tahoma" SIZE="5">v2</FONT></A></FONT>' >> $h
    '</TD></TR></TABLE>' >> $h
    '<BR>' >> $h
    "$global:oOverallProgress, $global:OverallActiveAnalysis" >> $h
    '<BR><BR>' >> $h
    $sError >> $h
    '<BR><BR>' >> $h

    'SCRIPT ARGUMENTS:<BR>' >> $h
    "Log: $Log<BR>" >> $h
    "ThresholdFile: $ThresholdFile<BR>" >> $h
    "AnalysisInterval: $AnalysisInterval<BR>" >> $h
    "IsOutputHtml: $IsOutputHtml<BR>" >> $h
    "IsOutputXml: $IsOutputXml<BR>" >> $h
    "HtmlOutputFileName: $HtmlOutputFileName<BR>" >> $h
    "XmlOutputFileName: $XmlOutputFileName<BR>" >> $h
    "OutputDir: $OutputDir<BR>" >> $h
    "AllCounterStats: $AllCounterStats<BR>" >> $h
    "NumberOfThreads: $NumberOfThreads<BR>" >> $h
    "IsLowPriority: $IsLowPriority<BR>" >> $h
    "DisplayReport: $DisplayReport<BR>" >> $h
    '<BR>' >> $h
    '<BR>Please contact the PAL tool team with this error by posting it to <A HREF="http://github.com/clinthuffman/PAL">GitHub.com/clinthuffman/PAL</A>. Thank you!<BR><BR>For detailed information, please look at the script execution log at <A HREF="file://' + $($global:oPal.Session.DebugLog) + '">' + $($global:oPal.Session.DebugLog) + '</A><BR>' >> $h
    '</HTML>' >> $h
    OpenHtmlReport -HtmlOutputFileName $h

    If ($IsFatal -eq $True)
    {
        Break Main;
    }
}

Trap
{
    [string] $sError = 'An error occurred on...<BR>'
    If ($(Test-Property -InputObject $Error[0] -Name 'CommandInvocation') -eq $True)
    {
        $sError = "$sError" + "$($Error[0].CommandInvocation.Line)<BR>"
        $sError = "$sError" + "$($Error[0].CommandInvocation.PositionMessage)<BR>"
        $sError = "$sError" + "$($Error[0].Exception.Message)<BR>"
    }

    If ($(Test-Property -InputObject $Error[0] -Name 'InvocationInfo') -eq $True)
    {
        $sError = "$sError" + "$($Error[0].InvocationInfo.Line)<BR>"
        $sError = "$sError" + "$($Error[0].InvocationInfo.PositionMessage)<BR>"
        $sError = "$sError" + "$($Error[0].Exception.Message)<BR>"
    }

    WriteErrorToHtmlAndShow -sError $sError
    #Break Main;
}

Function SetThreadPriority
{
    If ($IsLowPriority -eq $True)
    {
        [System.Threading.Thread]::CurrentThread.Priority = 'Lowest'
    }
}

Function FillNullsWithDashesAndIsAllNull
{
    param($Values)
    For ($i=0;$i -le $Values.GetUpperBound(0);$i++)
    {
        If (($Values[$i] -eq ' ') -or ($Values[$i] -eq $null))
        {
            $Values[$i] = '-'
        }
        Else
        {
            $global:IsValuesAllNull = $False
        }
    }
    $Values
}

Function Get-UserTempDirectory()
{
	$DirectoryPath = Get-ChildItem env:temp	
	Return $DirectoryPath.Value
}

Function InitializeGlobalVariables()
{
    #// $global:oPal
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'Version' -Value $Version
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'aTime' -Value @()
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'Culture' -Value (New-Object System.Globalization.CultureInfo($(Get-Culture).Name))
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'RelogedLogFilePath' -Value ''
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'LogCounterList' -Value @()
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'LogCounterSortedList' -Value @()
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'LogCounterListFilePath' -Value ''
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'LogCounterTimeZone' -Value ''
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'LogCounterData' -Value (New-Object System.Collections.ArrayList)
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'NumberOfCounterInstancesInPerfmonLog' -Value 0
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'ArgsOriginal' -Value @{}
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'ArgsProcessed' -Value @{}
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'QuantizedIndex' -Value (New-Object System.Collections.ArrayList)
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'QuantizedTime' -Value (New-Object System.Collections.ArrayList)
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'NumberOfValuesPerTimeSlice' -Value -1

    $oSession = New-Object System.Object
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'SessionGuid' -Value ([System.GUID]::NewGUID())
    
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'SessionDateTimeStamp' -Value (Get-Date -Format 'yyyy-MM-dd HH:mm:ss')
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'ScriptExecutionBeginTime' -Value (Get-Date)
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'ScriptExecutionEndTime' -Value (Get-Date)
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'DebugLog' -Value ''
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'LocalizedDecimal' -Value ($oPal.Culture.NumberFormat.NumberDecimalSeparator)
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'LocalizedThousandsSeparator' -Value ($oPal.Culture.NumberFormat.NumberGroupSeparator)
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'UserTempDirectory' -Value (Get-UserTempDirectory)
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'SessionWorkingDirectory' -Value ''
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'ResourceDirectoryPath' -Value ''
    Add-Member -InputObject $oSession -MemberType NoteProperty -Name 'CounterListFilterFilePath' -Value ''

    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'Session' -Value $oSession

    $global:oPal.Session.SessionWorkingDirectory = $oPal.Session.UserTempDirectory + '\' + $oPal.Session.SessionGuid

    $oScriptFileObject = Get-Item -Path $MyInvocation.ScriptName
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'ScriptFileLastModified' -Value $oScriptFileObject.LastWriteTime
    $Legal = 'The information and actions by this tool is provided "as is" and is intended for information purposes only. The authors and contributors of this tool take no responsibility for damages or losses incurred by use of this tool.'
    $sTempString = "PAL " + $global:oPal.Version + " (http://github.com/clinthuffman/PAL)`nWritten by: Clint Huffman (clinthuffman@hotmail.com) and other contributors.`nLast Modified: " + $global:oPal.ScriptFileLastModified + "`n$Legal`n"
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'MainHeader' -Value $sTempString
    Add-Member -InputObject $global:oPal -MemberType NoteProperty -Name 'QuestionVariables' -Value @{}

    #// $global:ChartSettings
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'XInterval' -Value -1
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'XIntervalMax' -Value $CHART_MAX_NUMBER_OF_AXIS_X_INTERVALS
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'LineThickness' -Value $CHART_LINE_THICKNESS
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'Width' -Value $CHART_WIDTH
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'Height' -Value $CHART_HEIGHT
    Add-Member -InputObject $global:ChartSettings -MemberType NoteProperty -Name 'InstancesMax' -Value $CHART_MAX_INSTANCES
}

Function ShowMainHeader()
{
	Write-Host $global:oPal.MainHeader
}

Function Set-EnglishLocales
{
    param()
    $global:originalCulture = (Get-Culture)
    $usenglishLocales = new-object System.Globalization.CultureInfo "en-US"   
    $global:currentThread.CurrentCulture = $usenglishLocales
    $global:currentThread.CurrentUICulture = $usenglishLocales
}

Function GlobalizationCheck
{
    $sDisplayName = (Get-Culture).DisplayName
	Write-Host "Your locale is set to: $sDisplayName"
	#If ($($sDisplayName.Contains('English')) -eq $false)
	#{
    #    $global:bEnglishLocale = $false
    #    Write-Host 'Your locale is not English. PAL unfortunately must be running under an English locale. Setting it to English-US.'
    #    Set-EnglishLocales
	#}
    #Else
    #{
    #    $global:bEnglishLocale = $true
    #}
}

Function ConvertTextTrueFalse($str)
{
	If ($str -eq $null)
	{Return $False}
    If ($str -is [System.String])
    {
        $strLower = $str.ToLower()
        If (($strLower -eq 'true') -or ($strLower -eq '$true'))
        {
            Return $True
        }
    	Else 
        {
            Return $False
        }
    }
    Else
    {
        If ($str -is [System.Boolean])
        {
            Return $str
        }
    }
}

Function ProcessArgs
{
    param([System.Object[]] $MyArgs)

    $OriginalArgs = @{}
    $ProcessedArgs = @{}
        
    If ($MyArgs.Count -ne 0)
    {
        #// Add the extra arguments into a hash table
        For ($i=0;$i -lt $MyArgs.Count;$i++)
        {
            If ($MyArgs[$i].SubString(0,1) -eq '-')
            {
                $sName = $MyArgs[$i].SubString(1);$i++;$sValue = $MyArgs[$i]
                If (($sValue -eq 'True') -or ($sValue -eq 'False'))
                {
                    $IsTrueOrFalse = ConvertTextTrueFalse $sValue
                    [void] $global:oPal.QuestionVariables.Add($sName,$IsTrueOrFalse)                    
                }
                Else
                {
                    [void] $global:oPal.QuestionVariables.Add($sName,$sValue)
                }
                [void] $OriginalArgs.Add($sName,$sValue)
                [void] $ProcessedArgs.Add($sName,$sValue)
            }
        }
    }

    [void] $OriginalArgs.Add('Log',$Log)
	If (-not $Log)
	{
		Write-Warning 'Missing the Log parameter.'
        Write-Host ''
        WriteErrorToHtmlAndShow -sError 'Missing the Log parameter.'
		Break Main
	}
    If ($($Log.IndexOf("'", [StringComparison]::OrdinalIgnoreCase)) -gt 0)
    {
		Write-Warning 'The file path to the counter log cannot contain any single quotes.'
		Write-Host ""
        WriteErrorToHtmlAndShow -sError 'The file path to the counter log cannot contain any single quotes.'
		Break Main
    }
    [void] $ProcessedArgs.Add('Log',$Log)
    
    [void] $OriginalArgs.Add('ThresholdFile',$ThresholdFile)
	If (-not $ThresholdFile)
	{
		Write-Warning 'Missing the ThresholdFile parameter.'
		Write-Host ''
        WriteErrorToHtmlAndShow -sError 'Missing the ThresholdFile parameter.'
		Break Main
	}
    [void] $ProcessedArgs.Add('ThresholdFile',$ThresholdFile)

    [void] $OriginalArgs.Add('AnalysisInterval',$AnalysisInterval)
	If (-not $AnalysisInterval)
	{
		Write-Warning 'Missing the Interval parameter.'
		Write-Host ''
        WriteErrorToHtmlAndShow -sError 'Missing the Interval parameter.'
		Break Main
	}
    If ($AnalysisInterval -is [System.String])
    {
		If ($($AnalysisInterval.IndexOf("AUTO", [StringComparison]::OrdinalIgnoreCase)) -gt 0)
		{
			$AnalysisInterval = 'AUTO'
		}
    }
    [void] $ProcessedArgs.Add('AnalysisInterval',$AnalysisInterval)

    [void] $OriginalArgs.Add('HtmlOutputFileName',$HtmlOutputFileName)
	If (-not $HtmlOutputFileName)
	{
		Write-Warning 'Missing the HtmlOutputFileName parameter.'
		Write-Host ''
        WriteErrorToHtmlAndShow -sError 'Missing the HtmlOutputFileName parameter.'
		Break Main
	}
    [void] $ProcessedArgs.Add('HtmlOutputFileName',$HtmlOutputFileName)

    [void] $OriginalArgs.Add('XmlOutputFileName',$XmlOutputFileName)
    [void] $ProcessedArgs.Add('XmlOutputFileName',$XmlOutputFileName)

    [void] $OriginalArgs.Add('OutputDir',$OutputDir)
	If (-not $OutputDir)
	{
		Write-Warning 'Missing the OutputDir parameter.'
		Write-Host ''
        WriteErrorToHtmlAndShow -sError 'Missing the OutputDir parameter.'
		Break Main
	}
    [void] $ProcessedArgs.Add('OutputDir',$OutputDir)    
	
	#// Check if the files exist
    If ($Log.Contains(';'))
    {
    	$aArgPerfmonLogFilePath = $Log.Split(';')
    	For ($sFile = 0;$sFile -lt $aArgPerfmonLogFilePath.length;$sFile++)
    	{
    		If ((Test-Path $aArgPerfmonLogFilePath[$sFile]) -eq $False)
    		{
    			$sText = "[ProcessArgs] The file ""$($aArgPerfmonLogFilePath[$sFile])"" does not exist."
                Write-Warning $sText
                WriteErrorToHtmlAndShow -sError $sText
    			Break Main
    		}
    	}
    }
    Else
    {
    	If ((Test-Path $Log) -eq $False)
    	{
    		$sText = "[ProcessArgs] The file ""$Log"" does not exist."
            Write-Warning $sText
            WriteErrorToHtmlAndShow -sError $sText
    		Break Main
    	}    		
    }

    If ((Test-Path $ThresholdFile) -eq $False) 
    {
    	$sText = "[ProcessArgs] The file ""$ThresholdFile"" does not exist."
        Write-Warning $sText
        WriteErrorToHtmlAndShow -sError $sText
    	Break Main
    }
        
    If ($BeginTime -ne $null)
    {
        [void] $OriginalArgs.Add('BeginTime',$BeginTime)
        $BeginTime = $BeginTime -Replace ' AM','AM'
        $BeginTime = $BeginTime -Replace ' PM','PM'
        [void] $ProcessedArgs.Add('BeginTime',$BeginTime)
    }
    Else
    {
        [void] $OriginalArgs.Add('BeginTime',$null)
        [void] $ProcessedArgs.Add('BeginTime',$null)
    }
    
    
    If ($EndTime -ne $null)
    {
        [void] $OriginalArgs.Add('EndTime',$EndTime)
        $EndTime = $EndTime -Replace ' AM','AM'
        $EndTime = $EndTime -Replace ' PM','PM'
        [void] $ProcessedArgs.Add('EndTime',$EndTime)
    }
    Else
    {
        [void] $OriginalArgs.Add('EndTime',$null)
        [void] $ProcessedArgs.Add('EndTime',$null)
    }

    [void] $OriginalArgs.Add('IsOutputHtml',$IsOutputHtml)
    $IsOutputHtml = ConvertTextTrueFalse $IsOutputHtml
    [void] $ProcessedArgs.Add('IsOutputHtml',$IsOutputHtml)

    [void] $OriginalArgs.Add('IsOutputXml',$IsOutputXml)
    $IsOutputXml = ConvertTextTrueFalse $IsOutputXml
    [void] $ProcessedArgs.Add('IsOutputXml',$IsOutputXml)

    [void] $OriginalArgs.Add('AllCounterStats',$AllCounterStats)
    $AllCounterStats = ConvertTextTrueFalse $AllCounterStats
    [void] $ProcessedArgs.Add('AllCounterStats',$AllCounterStats)

    [void] $OriginalArgs.Add('NumberOfThreads',$NumberOfThreads)
    If ([Int] $NumberOfThreads -lt 1)
    {
        $NumberOfThreads = 1
    }
    [void] $ProcessedArgs.Add('NumberOfThreads',$NumberOfThreads)

    [void] $OriginalArgs.Add('IsLowPriority',$IsLowPriority)
    $IsLowPriority = ConvertTextTrueFalse $IsLowPriority
    [void] $ProcessedArgs.Add('IsLowPriority',$IsLowPriority)
    
    [void] $OriginalArgs.Add('DisplayReport',$DisplayReport)
    $DisplayReport = ConvertTextTrueFalse $DisplayReport
    [void] $ProcessedArgs.Add('DisplayReport',$DisplayReport)

    'SCRIPT ARGUMENTS:'
    $ProcessedArgs.GetEnumerator() | Sort-Object Name
    Write-Host ''

    $global:oPal.ArgsOriginal = $OriginalArgs
    $global:oPal.ArgsProcessed = $ProcessedArgs
}

Function StartDebugLogFile
{
    param($sDirectoryPath, $iAttempt)
	If ($iAttempt -eq 0) 
	{
        $sFilePath = $sDirectoryPath + "\PAL.log"
    }
	Else
	{
        $sFilePath = $sDirectoryPath + "\PAL" + $iAttempt + ".log"
    }
	$erroractionpreference = "SilentlyContinue"
    
	Trap
	{
		$iAttempt++
		If ($iAttempt -le 5)	
		{
			StartDebugLogFile $sDirectoryPath $iAttempt
		}
        Else
        {
            Write-Host 'Unable to start a transcript.'
        }
	}

	If (($Host.Name -eq 'ConsoleHost') -or (Get-Command -Name Start-Transcript | Where-Object {$_.PSSnapin -ne 'Microsoft.PowerShell.Host'})) 
    {
		Start-Transcript -Path $sFilePath -Force
        $global:oPal.Session.DebugLog = $sFilePath
	} 
    ElseIf ($Host.Name -eq 'PowerGUIScriptEditorHost') 
    {
		Write-Warning 'You must install the Transcription add-on for the PowerGUI Script Editor if you want to use transcription.'
	} 
    Else
    {
		Write-Warning 'This host does not support transcription.'
	}
	$erroractionpreference = "Continue"
}

Function StopDebugLogFile()
{
	If (($Host.Name -eq 'ConsoleHost') -or (Get-Command -Name Stop-Transcript | Where-Object {$_.PSSnapin -ne 'Microsoft.PowerShell.Host'})) 
    {
        Stop-Transcript
	}
}

Function RemoveCounterComputer
{
    param($sCounterPath)
    
	#'\\IDCWEB1\Processor(_Total)\% Processor Time"
	[string] $sString = ""
	#// Remove the double backslash if exists
	If ($sCounterPath.substring(0,2) -eq "\\")
	{		
		$sComputer = $sCounterPath.substring(2)
		$iLocThirdBackSlash = $sComputer.IndexOf("\")
		$sString = $sComputer.substring($iLocThirdBackSlash)
	}
	Else
	{
		$sString = $sCounterPath
	}		
		Return $sString	
}

Function RemoveCounterNameAndComputerName
{
    param($sCounterPath)
    
    If ($sCounterPath.substring(0,2) -eq "\\")
    {
    	$sCounterObject = RemoveCounterComputer $sCounterPath
    }
    Else
    {
        $sCounterObject = $sCounterPath
    }
	# \Paging File(\??\C:\pagefile.sys)\% Usage Peak
	# \(MSSQL|SQLServer).*:Memory Manager\Total Server Memory (KB)
	$aCounterObject = $sCounterObject.split("\")
	$iLenOfCounterName = $aCounterObject[$aCounterObject.GetUpperBound(0)].length
	$sCounterObject = $sCounterObject.substring(0,$sCounterObject.length - $iLenOfCounterName)
	$sCounterObject = $sCounterObject.Trim("\")
    Return $sCounterObject 	    
}

Function GetCounterName
{
    param($sCounterPath)
    
	$aCounterPath = @($sCounterPath.Split("\"))
	Return $aCounterPath[$aCounterPath.GetUpperBound(0)]
}

Function GetCounterInstance
{
    param($sCounterPath)
    
	$sCounterObject = RemoveCounterNameAndComputerName $sCounterPath	
	#// "Paging File(\??\C:\pagefile.sys)"
	$Char = $sCounterObject.Substring(0,1)	
	If ($Char -eq "`\")
	{
		$sCounterObject = $sCounterObject.SubString(1)
	}
	$Char = $sCounterObject.Substring($sCounterObject.Length-1,1)	
	If ($Char -ne "`)")
	{
		Return ""
	}	
	$iLocOfCounterInstance = 0
	$iRightParenCount = 0
	For ($a=$sCounterObject.Length-1;$a -gt 0;$a = $a - 1)
	{			
		$Char = $sCounterObject.Substring($a,1)
		If ($Char -eq "`)")
		{
			$iRightParenCount = $iRightParenCount + 1
		}
		If ($Char -eq "`(")
		{
			$iRightParenCount = $iRightParenCount - 1
		}
		$iLocOfCounterInstance = $a
		If ($iRightParenCount -eq 0){break}
	}
	$iLenOfInstance = $sCounterObject.Length - $iLocOfCounterInstance - 2
	Return $sCounterObject.Substring($iLocOfCounterInstance+1, $iLenOfInstance)
}

Function GetCounterObject
{
    param($sCounterPath)
	$sCounterObject = RemoveCounterNameAndComputerName $sCounterPath
	#// "Paging File(\??\C:\pagefile.sys)"
    
    If ($sCounterObject -ne '')
    {
    	$Char = $sCounterObject.Substring(0,1)
    	If ($Char -eq "`\")
    	{
    		$sCounterObject = $sCounterObject.SubString(1)
    	}	
    	
    	$Char = $sCounterObject.Substring($sCounterObject.Length-1,1)	
    	If ($Char -ne "`)")
    	{
    		Return $sCounterObject
    	}	
    	$iLocOfCounterInstance = 0
    	$iRightParenCount = 0
    	For ($a=$sCounterObject.Length-1;$a -gt 0;$a = $a - 1)
    	{			
    		$Char = $sCounterObject.Substring($a,1)
    		If ($Char -eq "`)")
    		{
    			$iRightParenCount = $iRightParenCount + 1
    		}
    		If ($Char -eq "`(")
    		{
    			$iRightParenCount = $iRightParenCount - 1
    		}
    		$iLocOfCounterInstance = $a
    		If ($iRightParenCount -eq 0){break}
    	}
	   Return $sCounterObject.Substring(0,$iLocOfCounterInstance)    
    }
    Else
    {
        Return ""
    }
}

Function CreateDirectory
{
    param($DirectoryPath,$DirectoryName=$null)
        
	If ($DirectoryName -eq $null)
	{
		If ((Test-Path -Path $DirectoryPath) -eq $False)
		{
			Write-Host "Creating directory `"$DirectoryPath`""
			Return New-Item -Path $DirectoryPath -type directory
		}
	}
	Else
	{
		If ((Test-Path -Path $DirectoryPath\$DirectoryName) -eq $False)
		{
			Write-Host "Creating directory `"$DirectoryPath\$DirectoryName`""
			Return New-Item -Path $DirectoryPath -Name $DirectoryName -type directory	
		}
	}
}

Function CreateSessionWorkingDirectory()
{	
	Write-Host "Creating session working directory..."
	$Temp = CreateDirectory -DirectoryPath $global:oPal.Session.SessionWorkingDirectory
    $Temp = $null
}

Function AddBackSlashToEndIfNotThereAlready
{
    param($sString)
    
	$LastChar = $sString.SubString($sString.Length-1)
	If ($LastChar -ne "\")
	{
		$sString = $sString + "\"
	}
	Return $sString
}

Function GetFirstPerfmonLogFileName()
{
	$sString = $global:oPal.ArgsProcessed.Log
	If ($sString.Contains(";"))
	{
		$aStrings = $sString.Split(";")
		$sString = $aStrings[0]
	}
	If ($sString.Contains("\"))
	{
		$aStrings = $sString.Split("\")
		$sString = $aStrings[$aStrings.GetUpperBound(0)]
	}
	# Remove the file extension
	$sString = $sString.SubString(0,$sString.Length - 4)
	Return $sString
}

Function ConvertStringToFileName
{
    param($sString)
    
	$sResult = $sString
	$sResult = $sResult -replace "\\", "_"
	$sResult = $sResult -replace "/", "_"
	$sResult = $sResult -replace " ", "_"
	$sResult = $sResult -replace "\?", ""
	$sResult = $sResult -replace ":", ""
	$sResult = $sResult -replace ">", ""
	$sResult = $sResult -replace "<", ""
	$sResult = $sResult -replace "\(", "_"
	$sResult = $sResult -replace "\)", "_"
	$sResult = $sResult -replace "\*", ""
	$sResult = $sResult -replace "\|", "_"
	$sResult = $sResult -replace "{", ""
	$sResult = $sResult -replace "}", ""
    $sResult = $sResult -replace "#", ""
	Return $sResult
}

Function ResolvePALStringVariablesForPALArguments()
{
	$UsersMyDocumentsFolder = [environment]::GetFolderPath("MyDocuments")
    
    $OutputDir = AddBackSlashToEndIfNotThereAlready $global:oPal.ArgsProcessed.OutputDir

	$sDateTimeStampForFile = $global:oPal.Session.SessionDateTimeStamp -replace(" ", "")
	$sDateTimeStampForFile = $sDateTimeStampForFile -replace(":", "")
    $sDateTimeStampForFile = $sDateTimeStampForFile -replace("-", "")

    $sLogFileName = GetFirstPerfmonLogFileName
    $sLogFileName = ConvertStringToFileName $sLogFileName
	
	$OutputDir = $OutputDir -replace("\[DateTimeStamp\]",$global:oPal.Session.SessionDateTimeStamp)
	$OutputDir = $OutputDir -replace("\[LogFileName\]",$sLogFileName)
    $OutputDir = $OutputDir -replace("\[GUID\]",$global:oPal.Session.SessionGuid)
	$OutputDir = $OutputDir -replace("\[My Documents\]",$UsersMyDocumentsFolder)
	$global:oPal.ArgsProcessed.OutputDir = $OutputDir
    
	$sHtmlReportFile = $HtmlOutputFileName -replace("\[DateTimeStamp\]",$sDateTimeStampForFile)
	$sHtmlReportFile =  $sHtmlReportFile -replace("\[LogFileName\]",$sLogFileName)
    $sHtmlReportFile =  $sHtmlReportFile -replace("\[GUID\]",$global:oPal.session.SessionGuid)
	$global:oPal.ArgsProcessed.HtmlOutputFileName = $global:oPal.ArgsProcessed.OutputDir + $sHtmlReportFile
    
    If ($global:oPal.ArgsProcessed.IsOutputXml -eq $True)
    {
    	$sXmlReportFile = $XmlOutputFileName -replace("\[DateTimeStamp\]",$sDateTimeStampForFile)
    	$sXmlReportFile =  $sXmlReportFile -replace("\[LogFileName\]",$sLogFileName)
        $sXmlReportFile =  $sXmlReportFile -replace("\[GUID\]",$global:oPal.Session.SessionGuid)
    	$global:oPal.ArgsProcessed.XmlOutputFileName = $global:oPal.ArgsProcessed.OutputDir + $sXmlReportFile
    }	
	
	$sDirectoryName = $sHtmlReportFile.SubString(0,$sHtmlReportFile.Length - 4)
	$sDirectoryName = ConvertStringToFileName $sDirectoryName
	$global:oPal.Session.ResourceDirectoryPath = $global:oPal.ArgsProcessed.OutputDir + $sDirectoryName + "\"
}

Function CreateFile
{
    param($FilePath)
    
	If ((Test-Path -Path $FilePath) -eq $False)
	{
		Write-Host "Creating file `"$FilePath`""
		Return New-Item -Path $FilePath -type file		
	}
}

Function CreateFileSystemResources()
{
	$Temp = CreateDirectory $global:oPal.ArgsProcessed.OutputDir
    $Temp = CreateFile $global:oPal.ArgsProcessed.HtmlOutputFileName
	$Temp = CreateDirectory $global:oPal.Session.ResourceDirectoryPath
    $Temp = $null
}

Function ReadThresholdFileIntoMemory
{
	param($sThresholdFilePath)	
	[xml] (Get-Content $sThresholdFilePath -Encoding UTF8)	
}

Function CreateXmlObject
{
    $XmlAnalysesDocument = ReadThresholdFileIntoMemory -sThresholdFilePath $global:oPal.ArgsProcessed.ThresholdFile
    $global:oXml = New-Object System.Object
    Add-Member -InputObject $global:oXml -MemberType NoteProperty -Name 'XmlRoot' -Value $XmlAnalysesDocument
    Add-Member -InputObject $global:oXml -MemberType NoteProperty -Name 'XmlAnalyses' -Value $XmlAnalysesDocument.PAL
    Add-Member -InputObject $global:oXml -MemberType NoteProperty -Name 'ThresholdFilePathLoadHistory' -Value (New-Object System.Collections.ArrayList)
    Add-Member -InputObject $global:oXml -MemberType NoteProperty -Name 'XmlCounterLogCounterInstanceList' -Value ''
}

Function CheckPalXmlThresholdFileVersion
{
    param($XmlThresholdFile)
    [string] $sVersion = ''
    ForEach ($XmlPal in $XmlThresholdFile.SelectNodes("//PAL"))
    {    
        If ($(Test-property -InputObject $XmlPal -Name 'PALVERSION') -eq $True)
        {
            $sVersion = $XmlPal.PALVERSION
            If ($sVersion.SubString(0,1) -ne '2')
            {
                $sText = 'The threshold file specified is not compatible with PAL v2.0.'
                Write-Error $sText
                WriteErrorToHtmlAndShow -sError $sText
                Break Main;
            }
        }
        Else
        {
            $sText = 'The threshold file specified is not compatible with PAL v2.0.'
            Write-Error $sText
            WriteErrorToHtmlAndShow -sError $sText
            Break Main;    
        } 
    }  
}

Function Test-FileExists
{
    param($Path)
    If ($Path -eq '')
    {
        Return $false
    }
    Else
    {
        Return Test-Path -Path $Path
    }
}

Function InheritFromThresholdFiles
{
    param($sThresholdFilePath)
    
    $XmlThresholdFile = [xml] (Get-Content $sThresholdFilePath -Encoding UTF8)
    CheckPalXmlThresholdFileVersion -XmlThresholdFile $XmlThresholdFile
    #// Add it to the threshold file load history, so that we don't get into an endless loop of inheritance.
    If ($global:oXml.ThresholdFilePathLoadHistory.Contains($sThresholdFilePath) -eq $False)
    {
        [void] $global:oXml.ThresholdFilePathLoadHistory.Add($sThresholdFilePath)
    }
    
    #// Inherit from other threshold files.
    ForEach ($XmlInheritance in $XmlThresholdFile.SelectNodes('//INHERITANCE'))
    {
        If ($(Test-FileExists $XmlInheritance.FilePath) -eq $True)
        {
            $XmlInherited = [xml] (Get-Content $XmlInheritance.FilePath -Encoding UTF8)
            ForEach ($XmlInheritedAnalysisNode in $XmlInherited.selectNodes('//ANALYSIS'))
            {
                $bFound = $False            
                ForEach ($XmlAnalysisNode in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
                {
                    If ($XmlInheritedAnalysisNode.ID -eq $XmlAnalysisNode.ID)
                    {
                        $bFound = $True
                        Break
                    }
                    If ($XmlInheritedAnalysisNode.NAME -eq $XmlAnalysisNode.NAME)
                    {
                        $bFound = $True
                        Break
                    }
                }
                If ($bFound -eq $False)
                {
                    [void] $global:oXml.XmlAnalyses.AppendChild($global:oXml.XmlRoot.ImportNode($XmlInheritedAnalysisNode, $True))
                }
            }
            ForEach ($XmlInheritedQuestionNode in $XmlInherited.selectNodes("//QUESTION"))
            {
                $bFound = $False
                ForEach ($XmlQuestionNode in $global:oXml.XmlAnalyses.selectNodes("//QUESTION"))
                {
                    If ($XmlInheritedQuestionNode.QUESTIONVARNAME -eq $XmlQuestionNode.QUESTIONVARNAME)
                    {
                        $bFound = $True
                        Break
                    }
                }
                If ($bFound -eq $False)
                {            
                    [void] $global:oXml.XmlAnalyses.AppendChild($global:oXml.XmlRoot.ImportNode($XmlInheritedQuestionNode, $True))
                }
            }
            
    		If ($global:oXml.ThresholdFilePathLoadHistory.Contains($XmlInheritance.FilePath) -eq $False)
    		{
    			InheritFromThresholdFiles $XmlInheritance.FilePath
    		}
        }
    }
}

Function CounterPathToObject
{
    param($sCounterPath)

    $pattern = '(?<srv>\\\\[^\\]*)?\\(?<obj>[^\(^\)]*)(\((?<inst>.*(\(.*\))?)\))?\\(?<ctr>.*\s?(\(.*\))?)'

    $oCtr = New-Object System.Object

    If ($sCounterPath -match $pattern)
    {
        [string] $sComputer = $matches["srv"]
        If ($sComputer -ne '')
        {$sComputer = $sComputer.Substring(2)}
        Add-Member -InputObject $oCtr -MemberType NoteProperty -Name 'Computer' -Value $sComputer
        Add-Member -InputObject $oCtr -MemberType NoteProperty -Name 'Object' -Value $matches["obj"]
        Add-Member -InputObject $oCtr -MemberType NoteProperty -Name 'Instance' -Value $matches["inst"]
        Add-Member -InputObject $oCtr -MemberType NoteProperty -Name 'Name' -Value $matches["ctr"]
    }
    Return $oCtr
}

Function GenerateThresholdFileCounterList
{
    Write-Host 'Generating the counter list to filter on...' -NoNewline
    $p = $global:oPal.Session.SessionWorkingDirectory + '\CounterListFilter.txt'
    $c = New-Object System.Collections.ArrayList
    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        If ($(ConvertTextTrueFalse $XmlAnalysisInstance.ENABLED) -eq $True)
        {
            ForEach ($XmlAnalysisDataSourceInstance in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
            {
                If ($XmlAnalysisDataSourceInstance.TYPE -eq 'CounterLog')
                {
                    If ($(Test-property -InputObject $XmlAnalysisDataSourceInstance -Name 'ISCOUNTEROBJECTREGULAREXPRESSION') -eq $True)
                    {
                        If ($(ConvertTextTrueFalse $XmlAnalysisDataSourceInstance.ISCOUNTEROBJECTREGULAREXPRESSION) -eq $True)
                        {
                            $oCtr = CounterPathToObject -sCounterPath $XmlAnalysisDataSourceInstance.EXPRESSIONPATH
                            If ($($oCtr.Instance) -eq $null)
                            {
                                $sNewExpressionPath = '\' + '*' + '\' + "$($oCtr.Name)"
                            }
                            Else
                            {
                                $sNewExpressionPath = '\' + '*' + '(' + "$($oCtr.Instance)" + ')\' + "$($oCtr.Name)"
                            }
                            $c += $sNewExpressionPath
                        }
                        Else
                        {
                            $c += $XmlAnalysisDataSourceInstance.EXPRESSIONPATH
                        }
                    }
                    Else
                    {
                        $c += $XmlAnalysisDataSourceInstance.EXPRESSIONPATH
                    }
                }
            }
        }
    }
    Write-Host 'Done'
    Write-Host 'Removing duplicate counter expressions from counter list...' -NoNewline
    #// Remove duplicate counter expression paths
    $c = $c | select -uniq
    $c | Out-File -FilePath $p -Encoding 'ASCII'
    $global:oPal.Session.CounterListFilterFilePath = $p
    Write-Host 'Done'
    Write-Host ''
}

Function CheckTheFileExtension
{
    param($FilePath, $ThreeLetterExtension)
    
	$ExtractedExtension = $FilePath.SubString($FilePath.Length-3)
	If ($ExtractedExtension.ToLower() -eq $ThreeLetterExtension) {Return $True}
	Else {Return $False}
}

Function CheckIsSingleCsvFile
{
    param($sPerfmonLogPaths)
    $NumberOfCsvFiles = 0
    If ($sPerfmonLogPaths.Contains(';'))
    {
        $aPerfmonLogPaths = $sPerfmonLogPaths.Split(';')
        For ($f=0;$f -lt $aPerfmonLogPaths.length;$f++)
        {
            If ($(CheckTheFileExtension -FilePath $aPerfmonLogPaths[$f] -ThreeLetterExtension 'csv') -eq $True)
            {
                $sText = 'PAL is unable to merge CSV perfmon log files. Run PAL again, but analyze only one perfmon log at a time. PAL uses Relog.exe (part of the operating system) to merge the log files together.'
                Write-Warning $sText
                WriteErrorToHtmlAndShow -sError $sText
                Break Main;
            }
        }
        Return $False
    }
    Else
    {
        If (CheckTheFileExtension $sPerfmonLogPaths "csv")
        {
            Return $True
        }
        Else
        {
            Return $False
        }
    }
}

Function IsSamplesInPerfmonLog
{
    param($RelogOutput)
    $u = $RelogOutput.GetUpperBound(0)

    :OutputOfRelogLoop For ($i=$u;$i -gt 0;$i = $i - 1)
    {
        If ($($RelogOutput[$i].Contains('----------------')) -eq $True)
        {
            $a = $i + 5
            $SamplesLine = $RelogOutput[$a]
            break OutputOfRelogLoop;
        }
    }
    $aSamples = $SamplesLine.Split(' ')
    $NumOfSamples = $aSamples[$aSamples.GetUpperBound(0)]
    If ($NumOfSamples -gt 0)
    {$True}
    Else
    {$False}    
}

Function GetNumberOfSamplesFromRelogOutput
{
    param($sRelogOutput)
    [System.String] $sLine = ''
    [System.Int32] $u = 0
    [System.Int32] $iResult = 0
    ForEach ($sLine in $sRelogOutput)
    {
        If ($sLine.IndexOf('Samples:') -ge 0)
        {
            $aLine = $sLine.Split(' ')
            $u = $aLine.GetUpperBound(0)
            $iResult = $aLine[$u]
            Return $iResult
        }
    }
}

Function GetFileNameFromFilePath
{
    param($FilePath)    
    $ArrayOfStrings = $FilePath.Split('\')
    $ArrayOfStrings[$ArrayOfStrings.GetUpperBound(0)]    
}

Function GetLogNameFromLogParameter
{
    $aSplitBySemiColon = $global:oPal.ArgsProcessed.Log.Split(';')
    GetFileNameFromFilePath -FilePath $aSplitBySemiColon[0]
}

Function MergeConvertFilterPerfmonLogs
{
    param($sPerfmonLogPaths, $BeginTime=$null, $EndTime=$null)
    $sCommand = ''
    $RelogOutput = ''
    $IsSingleCsvFile = CheckIsSingleCsvFile -sPerfmonLogPaths $sPerfmonLogPaths
	$global:oPal.RelogedLogFilePath = $global:oPal.Session.SessionWorkingDirectory + "\_FilteredPerfmonLog.csv"
    $global:sFirstCounterLogFilePath = $sPerfmonLogPaths

	If ($IsSingleCsvFile -eq $False)
	{
		$sTemp = ''
		If ($sPerfmonLogPaths.Contains(';'))
		{
			$aPerfmonLogPaths = $sPerfmonLogPaths.Split(';')
            $global:sFirstCounterLogFilePath = $aPerfmonLogPaths[0]
			For ($f=0;$f -lt $aPerfmonLogPaths.length;$f++)
			{
				$sTemp = $sTemp + " " + "`"" + $aPerfmonLogPaths[$f] + "`""
			}
			$sTemp = $sTemp.Trim()

            #// Dont filter anymore. Filtering causes problems with counter language translation
            $sCommand = $('relog.exe ' + "`"$sTemp`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"")

            #If ($global:oPal.ArgsProcessed.AllCounterStats -eq $True)
            #{
            #    $sCommand = $('relog.exe ' + "`"$sTemp`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"")
            #}
            #Else
            #{
            #    $sCommand = $('relog.exe ' + "`"$sTemp`"" + ' -cf ' + "`"$($global:oPal.Session.CounterListFilePath)`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"")
            #}
		}
		Else
		{
            $global:sFirstCounterLogFilePath = $sPerfmonLogPaths

            #// Dont filter anymore. Filtering causes problems with counter language translation
            $sCommand = $('relog.exe ' + "`"$sPerfmonLogPaths`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"" + ' -y')

            #If ($global:oPal.ArgsProcessed.AllCounterStats -eq $True)
            #{
            #    $sCommand = $('relog.exe ' + "`"$sPerfmonLogPaths`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"" + ' -y')
            #}
            #Else
            #{
            #    $sCommand = 'relog.exe ' + "`"$sPerfmonLogPaths`"" + ' -cf ' + "`"$($global:oPal.Session.CounterListFilterFilePath)`"" + ' -f csv -o ' + "`"$($global:oPal.RelogedLogFilePath)`"" + ' -y'
            #}
		}
	}
	Else
	{
        #// Just use the original CSV perfmon log.
        $global:oPal.RelogedLogFilePath = $sPerfmonLogPaths
        $global:sFirstCounterLogFilePath = $sPerfmonLogPaths
	}
    
    If (($global:oPal.ArgsProcessed.BeginTime -ne $null) -and ($global:oPal.ArgsProcessed.EndTime -ne $null))
    {
        #// Fix provided by kwomba
        $sCommand = "$sCommand" + ' -b ' + "`"$($global:oPal.ArgsProcessed.BeginTime)`"" + ' -e ' + "`"$($global:oPal.ArgsProcessed.EndTime)`""
    }
    
    If ($IsSingleCsvFile -eq $False)
    {
        Write-Host $sCommand
        Write-Host ''
        $RelogOutput = Invoke-Expression -Command $sCommand
        #// Remove the extra blank lines and relog progress bar.
        $RelogOutput | ForEach-Object {If (($_ -ne '') -and ($_.SubString(0,1) -ne 0)) {$_}}
    }
    
    $sRelogOutputAsSingleString = [string]::join("", $RelogOutput)
    If ($sRelogOutputAsSingleString.contains('No data to return.') -eq $True)
    {
        
        $sError = "Relog.exe failed to process the log. This commonly occurs when a BLG file from a Windows Vista or newer operating system is attempting to be analyze on Windows XP or Windows Server 2003, or due to log corruption. If you see this message on Windows XP or Server 2003, then try analyzing the log on Windows Vista/Server 2008 or later. Review the results above this line. If relog.exe continues to fail, then try running Relog.exe manually and/or contact Microsoft Customer Support Servers for support on Relog.exe only. PAL is not supported by Microsoft."
        WriteErrorToHtmlAndShow -sError $sError
        Break Main
    }
    
	$NewLogExists = Test-Path -Path $global:oPal.RelogedLogFilePath
	If ($NewLogExists -eq $False)
	{	
		$sError = $('[MergeConvertFilterPerfmonLogs] ERROR: Unable to find the converted log file: ' + "$($global:oPal.RelogedLogFilePath). " + "Relog.exe failed to process the log. Review the results above this line. If relog.exe continues to fail, then try running Relog.exe manually and/or contact Microsoft Customer Support Servers for support on Relog.exe only. PAL is not supported by Microsoft.")
        WriteErrorToHtmlAndShow -sError $sError
        Write-Error $sError
		Break Main
	}

    If (($IsSingleCsvFile -eq $False) -and ($RelogOutput -ne $null))
    {
        If ($(IsSamplesInPerfmonLog -RelogOutput $RelogOutput) -eq $False)
        {
    		$sError = $("[MergeConvertFilterPerfmonLogs] ERROR: Unable to use the log file(s): " + "$($global:oPal.ArgsOriginal.Log). " + "The counters in the log(s) do not contain any useable samples.")
            WriteErrorToHtmlAndShow -sError $sError
            Write-Error $sError
    		Break Main
        }
    }
    If ($RelogOutput -ne $null)
    {
        $NumberOfSamples = GetNumberOfSamplesFromRelogOutput -sRelogOutput $RelogOutput
        If ($NumberOfSamples -is [System.Int32])
        {
            If ($NumberOfSamples -lt 10)
            {
                $sText = $("ERROR: Not enough samples in the counter log to properly process. Create another performance counter log with more samples in it and try again. Number of samples is: " + "$NumberOfSamples")
                Write-Error $sText
                WriteErrorToHtmlAndShow -sError $sText
                Break Main
            }
        }
    }
}

Function GetCounterList
{
    $u = $global:oPal.LogCounterData[0].GetUpperBound(0)
    $global:oPal.LogCounterList = $global:oPal.LogCounterData[0][1..$u]
}

Function ConvertCounterNameToExpressionPath($sCounterPath)
{
    $oCtr = CounterPathToObject -sCounterPath $sCounterPath
	$sCounterObject = $oCtr.Object
	$sCounterName = $oCtr.Name
	$sCounterInstance = $oCtr.Instance	
	If ($sCounterInstance -eq $null)
	{
		"\$sCounterObject\$sCounterName"
	}
	Else
	{
		"\$sCounterObject(*)\$sCounterName"
	}	
}

Function ConvertCounterExpressionToVarName($sCounterExpression)
{

    $oCtr = CounterPathToObject -sCounterPath $sCounterExpression
	$sCounterObject = $oCtr.Object
	$sCounterName = $oCtr.Name
	$sCounterInstance = $oCtr.Instance	

	If ($sCounterInstance -ne "*")
	{
		$sResult = $sCounterObject + $sCounterName + $sCounterInstance
	}
	Else
	{
		$sResult = $sCounterObject + $sCounterName + "ALL"
	}
	$sResult = $sResult -replace "/", ""
	$sResult = $sResult -replace "\.", ""
	$sResult = $sResult -replace "%", "Percent"
	$sResult = $sResult -replace " ", ""
	$sResult = $sResult -replace "\.", ""
	$sResult = $sResult -replace ":", ""
	$sResult = $sResult -replace "\(", ""
	$sResult = $sResult -replace "\)", ""
	$sResult = $sResult -replace "-", ""
	$sResult
}

Function CreateXmlAnalysisNodeFromCounterPath
{
    param($sCounterExpressionPath)
    

    $oCtr = CounterPathToObject -sCounterPath $sCounterExpressionPath

    $sAnalysisCategory = $oCtr.Object
    $sAnalysisName = $oCtr.Name
    $sGUID = [System.GUID]::NewGUID()
    $VarName = ConvertCounterExpressionToVarName $sCounterExpressionPath

	#// ANALYSIS Attributes
    $XmlNewAnalysisNode = $global:oXml.XmlRoot.CreateElement("ANALYSIS")
    $XmlNewAnalysisNode.SetAttribute("NAME", $sAnalysisName)
    $XmlNewAnalysisNode.SetAttribute("ENABLED", $True)
    $XmlNewAnalysisNode.SetAttribute("CATEGORY", $sAnalysisCategory)
    $XmlNewAnalysisNode.SetAttribute("ID", $sGUID)
	$XmlNewAnalysisNode.SetAttribute("FROMALLCOUNTERSTATS", 'True')    
    
    #// DATASOURCE
    $XmlNewDataSourceNode = $global:oXml.XmlRoot.CreateElement("DATASOURCE")
    $XmlNewDataSourceNode.SetAttribute("TYPE", "CounterLog")
    $XmlNewDataSourceNode.SetAttribute("NAME", $sCounterExpressionPath)
    $XmlNewDataSourceNode.SetAttribute("COLLECTIONVARNAME", "CollectionOf$VarName")
    $XmlNewDataSourceNode.SetAttribute("EXPRESSIONPATH", $sCounterExpressionPath)
    $XmlNewDataSourceNode.SetAttribute("NUMBEROFSAMPLESVARNAME", "NumberOfSamples$VarName")
    $XmlNewDataSourceNode.SetAttribute("MINVARNAME", "Min$VarName")
    $XmlNewDataSourceNode.SetAttribute("AVGVARNAME", "Avg$VarName")
    $XmlNewDataSourceNode.SetAttribute("MAXVARNAME", "Max$VarName")
    $XmlNewDataSourceNode.SetAttribute("TRENDVARNAME", "Trend$VarName")
    $XmlNewDataSourceNode.SetAttribute("DATATYPE", "round3")
    [void] $XmlNewAnalysisNode.AppendChild($XmlNewDataSourceNode)
    
    #// CHART
    $XmlNewDataSourceNode = $global:oXml.XmlRoot.CreateElement("CHART")
    $XmlNewDataSourceNode.SetAttribute("CHARTTITLE", $sCounterExpressionPath)
    $XmlNewDataSourceNode.SetAttribute("ISTHRESHOLDSADDED", "False")
    $XmlNewDataSourceNode.SetAttribute("DATASOURCE", $sCounterExpressionPath)
    $XmlNewDataSourceNode.SetAttribute("CHARTLABELS", "instance")
    [void] $XmlNewAnalysisNode.AppendChild($XmlNewDataSourceNode)    
    
    [void] $global:oXml.XmlAnalyses.AppendChild($XmlNewAnalysisNode)
}

Function CalculatePercentage
{
    param($Number,$Total)
    If ($Total -eq 0)
    {
        Return 100
    }
    $Result = ($Number * 100) / $Total
    $Result
}

Function AddAllCountersFromPerfmonLog
{
    param($XmlAnalysis, $sPerfLogFilePath)
    Write-Host 'All counter stats is set to true. Loading all counters in perfmon log into the threshold file as new analyses. This may take several minutes.'
    Write-Host ''
    $htCounterExpressions = @{}

    Write-Host 'Importing the counter list as new threshold analyses...' -NoNewline
    #// Add Primary data source counters that are already in the threshold file.
    $PercentComplete = 0
    ForEach ($XmlAnalysisNode in $XmlAnalysis.SelectNodes('//ANALYSIS'))
    {
        If (($($htCounterExpressions.ContainsKey($($XmlAnalysisNode.PRIMARYDATASOURCE))) -eq $False) -or ($htCounterExpressions.Count -eq 0))
        {
            [void] $htCounterExpressions.Add($XmlAnalysisNode.PRIMARYDATASOURCE,"")
        }
    }
    
    For ($i=0;$i -lt $global:oPal.LogCounterList.GetUpperBound(0);$i++)
    {
        $sCounterExpression = ConvertCounterNameToExpressionPath $global:oPal.LogCounterList[$i]
        If ($htCounterExpressions.ContainsKey($sCounterExpression) -eq $False)
        {
            CreateXmlAnalysisNodeFromCounterPath $sCounterExpression
            [void] $htCounterExpressions.Add($sCounterExpression,"")
        }
        $PercentComplete = CalculatePercentage -Number $i -Total $global:oPal.LogCounterList.GetUpperBound(0)
        Write-Progress -activity 'Importing the counter list as new analyses...' -status '% Complete:' -percentcomplete $PercentComplete -id 2;
    }
    Write-Progress -activity 'Importing the counter list as new threshold analyses' -status '% Complete:' -Completed -id 2
    Write-Host 'Done!'
	$XmlAnalysis
}

Function PrepareCounterLogs
{
    If (($global:oPal.ArgsProcessed.BeginTime -ne $null) -and ($global:oPal.ArgsProcessed.EndTime -ne $null))
    {
        MergeConvertFilterPerfmonLogs -sPerfmonLogPaths $global:oPal.ArgsProcessed.Log -BeginTime $global:oPal.ArgsProcessed.BeginTime -EndTime $global:oPal.ArgsProcessed.EndTime
    }
    Else
    {
        MergeConvertFilterPerfmonLogs -sPerfmonLogPaths $global:oPal.ArgsProcessed.Log
    }
    Write-Host ''

    If ($global:oPal.ArgsProcessed.AllCounterStats -eq $True)
    {
        $global:oXml.XmlAnalyses = AddAllCountersFromPerfmonLog -XmlAnalysis $global:oXml.XmlAnalyses -sPerfLogFilePath $global:sFirstCounterLogFilePath
    }

    ConstructCounterDataArray
    GetCounterList

    #// Sort the counter list
    $c = $global:oPal.LogCounterList.GetEnumerator() | Sort-Object
    $global:oPal.LogCounterSortedList = @($c)
}

Function IsNumeric
{
    param($Value)
    [double]$number = 0
    $result = [double]::TryParse($Value, [REF]$number)
    $result
}

Function IsGreaterThanZero
{
    param($Value)
    If (IsNumeric $Value)
    {
        If ($Value -gt 0)
        {
            Return $True
        }
        Else
        {
            Return $False
        }
    }
    Else
    {
        Return $False
    }
}

Function ConvertToDataType
{
	param($ValueAsDouble, $DataTypeAsString="integer")
	$sDateType = $DataTypeAsString.ToLower()

    If ($(IsNumeric -Value $ValueAsDouble) -eq $True)
    {
    	switch ($sDateType)
    	{
    		#'absolute' {[Math]::Abs($ValueAsDouble)}
    		#'double' {[double]$ValueAsDouble}
    		'integer' {[Math]::Round($ValueAsDouble,0)}
    		#'long' {[long]$ValueAsDouble}
    		#'single' {[single]$ValueAsDouble}
    		'round1' {[Math]::Round($ValueAsDouble,1)}
    		'round2' {[Math]::Round($ValueAsDouble,2)}
    		'round3' {[Math]::Round($ValueAsDouble,3)}
    		'round4' {[Math]::Round($ValueAsDouble,4)}
    		'round5' {[Math]::Round($ValueAsDouble,5)}
    		'round6' {[Math]::Round($ValueAsDouble,6)}
    		default {$ValueAsDouble}
    	}
    }
    Else
    {
        $ValueAsDouble
    }
}

Function GenerateXmlCounterList
{
    #// This function converts the raw text based counter list into an XML document organized by counter properties for better performance.

    $c = $global:oPal.LogCounterList
    
    [xml] $global:oXml.XmlCounterLogCounterInstanceList = "<PAL></PAL>"
    For ($i=0;$i -le $c.GetUpperBound(0);$i++)
    {
        $PercentComplete = CalculatePercentage -Number $i -Total $c.GetUpperBound(0)
        $sComplete = "Progress: $(ConvertToDataType $PercentComplete 'integer')% (Counter $i of $($c.GetUpperBound(0)))"
        write-progress -activity 'Generating counter index to improve performance...' -status $sComplete -percentcomplete $PercentComplete -id 2;
        
        $sCounterPath = $c[$i]
        $oCtr = CounterPathToObject -sCounterPath $sCounterPath
    	$sCounterComputer = $oCtr.Computer
        $sCounterObject = $oCtr.Object
        $sCounterName = $oCtr.Name
        $sCounterInstance = $oCtr.Instance

        If ($sCounterObject -ne $null)
        {
            $IsCounterComputerFound = $False
            $IsCounterObjectFound = $False
            $IsCounterNameFound = $False
            $IsCounterInstanceFound = $False
            $IsCounterObjectSqlInstance = $False
            $sCounterObjectSqlInstance = ""
            $sCounterObjectSqlRegularExpression = ''        
            #// Counter Computers
            ForEach ($XmlCounterComputerNode in $global:oXml.XmlCounterLogCounterInstanceList.SelectNodes('//COUNTERCOMPUTER'))
            {
                If ($XmlCounterComputerNode.NAME -eq $sCounterComputer)
                {
                    $IsCounterComputerFound = $True
                    #// Counter Objects
                    ForEach ($XmlCounterObjectNode in $XmlCounterComputerNode.ChildNodes)
                    {                    
                        If (($XmlCounterObjectNode.NAME -eq $sCounterObject) -or ($XmlCounterObjectNode.NAME -eq $sCounterObjectSqlInstance))
                        {
                            $IsCounterObjectFound = $True
                            #// Counter Names
                            ForEach ($XmlCounterNameNode in $XmlCounterObjectNode.ChildNodes)
                            {
                                If ($XmlCounterNameNode.NAME -eq $sCounterName)
                                {
                                    $IsCounterNameFound = $True
                                    #// Counter Instances
                                    ForEach ($XmlCounterInstanceNode in $XmlCounterNameNode.ChildNodes)
                                    {
                                        If ($XmlCounterInstanceNode.NAME -ne '')
                                        {
                                            If ($XmlCounterInstanceNode.NAME.ToLower() -eq $sCounterInstance.ToLower())
                                            {
                                                $IsCounterInstanceFound = $True
                                            }
                                        }
                                    }
                                    #// Create the counter Instance if it does not exist.
                                    If (($IsCounterInstanceFound -eq $False) -or ($IsCounterObjectSqlInstance -eq $True))
                                    {
                                        $XmlNewCounterInstanceNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERINSTANCE")
                                        $XmlNewCounterInstanceNode.SetAttribute("NAME", $sCounterInstance)
                                        $XmlNewCounterInstanceNode.SetAttribute("COUNTERPATH", $sCounterPath)
                                        $XmlNewCounterInstanceNode.SetAttribute("COUNTERDATAINDEX", $($i+1)) #// The +1 is to compensate for the removal of the time zone in the original CSV file.
                                        [void] $XmlCounterNameNode.AppendChild($XmlNewCounterInstanceNode)
                                    }
                                }
                            }
                            #// Create the counter Name if it does not exist.
                            If ($IsCounterNameFound -eq $False)
                            {                            
                                $XmlNewCounterNameNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERNAME")
                                $XmlNewCounterNameNode.SetAttribute("NAME", $sCounterName)
                                
                                $XmlNewCounterInstanceNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERINSTANCE")
                                $XmlNewCounterInstanceNode.SetAttribute("NAME", $sCounterInstance)
                                $XmlNewCounterInstanceNode.SetAttribute("COUNTERPATH", $sCounterPath)
                                $XmlNewCounterInstanceNode.SetAttribute("COUNTERDATAINDEX", $($i+1)) #// The +1 is to compensate for the removal of the time zone in the original CSV file.
                                
                                [void] $XmlNewCounterNameNode.AppendChild($XmlNewCounterInstanceNode)
                                [void] $XmlCounterObjectNode.AppendChild($XmlNewCounterNameNode)
                            }
                        }
                    }
                    #// Create the counter object if it does not exist.
                    If ($IsCounterObjectFound -eq $False)
                    {
                        $XmlNewCounterObjectNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTEROBJECT")
                        $XmlNewCounterObjectNode.SetAttribute("NAME", $sCounterObject)
                        
                        $XmlNewCounterNameNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERNAME")
                        $XmlNewCounterNameNode.SetAttribute("NAME", $sCounterName)
                        
                        $XmlNewCounterInstanceNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERINSTANCE")
                        $XmlNewCounterInstanceNode.SetAttribute("NAME", $sCounterInstance)
                        $XmlNewCounterInstanceNode.SetAttribute("COUNTERPATH", $sCounterPath)
                        $XmlNewCounterInstanceNode.SetAttribute("COUNTERDATAINDEX", $($i+1)) #// The +1 is to compensate for the removal of the time zone in the original CSV file.
                        
                        [void] $XmlNewCounterNameNode.AppendChild($XmlNewCounterInstanceNode)
                        [void] $XmlNewCounterObjectNode.AppendChild($XmlNewCounterNameNode)
                        [void] $XmlCounterComputerNode.AppendChild($XmlNewCounterObjectNode)
                    }
                }            
            }
            #// Create the counter computer if it does not exist
            If ($IsCounterComputerFound -eq $False)
            {
                $XmlNewCounterComputerNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERCOMPUTER")
                $XmlNewCounterComputerNode.SetAttribute("NAME", $sCounterComputer)
                
                $XmlNewCounterObjectNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTEROBJECT")
                $XmlNewCounterObjectNode.SetAttribute("NAME", $sCounterObject)
                
                $XmlNewCounterNameNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERNAME")
                $XmlNewCounterNameNode.SetAttribute("NAME", $sCounterName)
                
                $XmlNewCounterInstanceNode = $global:oXml.XmlCounterLogCounterInstanceList.CreateElement("COUNTERINSTANCE")
                $XmlNewCounterInstanceNode.SetAttribute("NAME", $sCounterInstance)
                $XmlNewCounterInstanceNode.SetAttribute("COUNTERPATH", $sCounterPath)
                $XmlNewCounterInstanceNode.SetAttribute("COUNTERDATAINDEX", $($i+1)) #// The +1 is to compensate for the removal of the time zone in the original CSV file.
                
                [void] $XmlNewCounterNameNode.AppendChild($XmlNewCounterInstanceNode)
                [void] $XmlNewCounterObjectNode.AppendChild($XmlNewCounterNameNode)
                [void] $XmlNewCounterComputerNode.AppendChild($XmlNewCounterObjectNode)
                [void] $global:oXml.XmlCounterLogCounterInstanceList.DocumentElement.AppendChild($XmlNewCounterComputerNode)            
            }            
        }
    }
    $sComplete = "Progress: 100% (Counter $($c.GetUpperBound(0)) of $($c.GetUpperBound(0)))"
    write-progress -activity 'Generating counter index to improve performance...' -status $sComplete -Completed -id 2
    $global:oPal.NumberOfCounterInstancesInPerfmonLog = $c.GetUpperBound(0) + 1    
    Write-Host "Number Of Counter Instances In Perfmon Log: $($global:oPal.NumberOfCounterInstancesInPerfmonLog)"
    Write-Host ''
}

Function SetDefaultQuestionVariables
{
    param($XmlAnalysis)
    #// Add all of the Question Variable defaults
    ForEach ($XmlQuestion in $XmlAnalysis.SelectNodes('//QUESTION'))
    {
        If ($(Test-property -InputObject $XmlQuestion -Name 'QUESTIONVARNAME') -eq $True)
        {
            If ($($global:oPal.QuestionVariables.Contains($($XmlQuestion.QUESTIONVARNAME))) -eq $False)
            {
                If ($(Test-property -InputObject $XmlQuestion -Name 'DEFAULTVALUE') -eq $True)
                {                
                    If (($($XmlQuestion.DEFAULTVALUE) -eq 'True') -or ($($XmlQuestion.DEFAULTVALUE) -eq 'False'))
                    {
                        $IsTrueOrFalse = ConvertTextTrueFalse $XmlQuestion.DEFAULTVALUE
                        $global:oPal.QuestionVariables.Add($($XmlQuestion.QUESTIONVARNAME),$IsTrueOrFalse)
                    }
                    Else
                    {
                        #// Cast the question variables to their appropriate type.
                        If ($(Test-property -InputObject $XmlQuestion -Name 'DATATYPE') -eq $True)
                        {
                            $sDataType = $XmlQuestion.DATATYPE
                            switch ($sDataType)
                            {
                                'boolean'
                                {
                                    #// Already taken care of from above.
                                }
                                'integer'
                                {
                                    [int] $DefaultValue = $($XmlQuestion.DEFAULTVALUE)
                                    $global:oPal.QuestionVariables.Add($($XmlQuestion.QUESTIONVARNAME),$DefaultValue)
                                }
                                'int'
                                {
                                    [int] $DefaultValue = $($XmlQuestion.DEFAULTVALUE)
                                    $global:oPal.QuestionVariables.Add($($XmlQuestion.QUESTIONVARNAME),$DefaultValue)
                                }                                
                                'string'
                                {
                                    [string] $DefaultValue = $($XmlQuestion.DEFAULTVALUE)
                                    $global:oPal.QuestionVariables.Add($($XmlQuestion.QUESTIONVARNAME),$DefaultValue)
                                }
                            }
                        }
                        Else
                        {
                            #// Assume string
                            [string] $DefaultValue = $($XmlQuestion.DEFAULTVALUE)
                            $global:oPal.QuestionVariables.Add($($XmlQuestion.QUESTIONVARNAME),$DefaultValue)
                        }
                    }
                }
            }
        }
    }
}

Function Test-XmlBoolAttribute
{
    param ([Parameter(Position=0,Mandatory=1)]$InputObject,[Parameter(Position=1,Mandatory=1)]$Name)
    If ($(Test-property -InputObject $InputObject -Name $Name) -eq $True)
    {
        If ($(ConvertTextTrueFalse $InputObject.$Name) -eq $True)
        {
            $True        
        }
        Else
        {
            $False
        }
    }
    Else
    {
        $False
    }
}

function IsCounterObjectLangMatch
{
    param([xml] $xmlCounterLang, [string] $CounterObjectEnUs, [string] $CounterObject)

    [bool] $IsCounterObjectMatch = $false
    foreach ($CounterLangObject in $xmlCounterLang.CounterLang.ChildNodes)
    {
        if ($CounterLangObject.enus -eq $CounterObjectEnUs)
        {
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.csCZ) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.deDE) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.esES) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.frFR) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.huHU) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.itIT) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.jaJP) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.koKR) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.nlNL) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.plPL) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.ptBR) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.ptPT) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.ruRU) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.svSE) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.trTR) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            if ((Decode-XmlEscapeValues -Value $CounterLangObject.zhCN) -eq $CounterObject) {$IsCounterObjectMatch = $true}
            Return $IsCounterObjectMatch
        }
    }
    Return $IsCounterObjectMatch
}

function IsCounterNameLangMatch
{
    param([xml] $xmlCounterLang, [string] $CounterObjectEnUs, [string] $CounterNameEnUs, [string] $CounterName)
    [bool] $IsMatch = $false
    foreach ($CounterLangObject in $xmlCounterLang.CounterLang.ChildNodes)
    {
        if ($CounterLangObject.enus -eq $CounterObjectEnUs)
        {
            foreach ($CounterLangName in $CounterLangObject.ChildNodes)
            {
                if ($CounterLangName.enus -eq $CounterNameEnUs)
                {
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.csCZ) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.deDE) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.esES) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.frFR) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.huHU) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.itIT) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.jaJP) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.koKR) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.nlNL) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.plPL) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.ptBR) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.ptPT) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.ruRU) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.svSE) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.trTR) -eq $CounterName) {$IsMatch = $true}
                    if ((Decode-XmlEscapeValues -Value $CounterLangName.zhCN) -eq $CounterName) {$IsMatch = $true}
                    Return $IsMatch
                }
            }
        }
    }
    Return $IsMatch
}

function Get-CounterObjectLanguage
{
    param([xml] $XmlCounterLang, [string] $CounterObject)

    [string] $DetectedLanguage = 'enUS'
    [bool] $IsFound = $False

    foreach ($XmlCounterLangObject in $XmlCounterLang.CounterLang.ChildNodes)
    {
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.csCZ) -eq $CounterObject) {$DetectedLanguage = 'csCZ';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.deDE) -eq $CounterObject) {$DetectedLanguage = 'deDE';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.esES) -eq $CounterObject) {$DetectedLanguage = 'esES';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.frFR) -eq $CounterObject) {$DetectedLanguage = 'frFR';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.huHU) -eq $CounterObject) {$DetectedLanguage = 'huHU';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.itIT) -eq $CounterObject) {$DetectedLanguage = 'itIT';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.jaJP) -eq $CounterObject) {$DetectedLanguage = 'jaJP';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.koKR) -eq $CounterObject) {$DetectedLanguage = 'koKR';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.nlNL) -eq $CounterObject) {$DetectedLanguage = 'nlNL';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.plPL) -eq $CounterObject) {$DetectedLanguage = 'plPL';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.ptBR) -eq $CounterObject) {$DetectedLanguage = 'ptBR';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.ptPT) -eq $CounterObject) {$DetectedLanguage = 'ptPT';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.ruRU) -eq $CounterObject) {$DetectedLanguage = 'ruRU';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.svSE) -eq $CounterObject) {$DetectedLanguage = 'svSE';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.trTR) -eq $CounterObject) {$DetectedLanguage = 'trTR';$IsFound = $True}
        if ((Decode-XmlEscapeValues -Value $XmlCounterLangObject.zhCN) -eq $CounterObject) {$DetectedLanguage = 'zhCN';$IsFound = $True}
        if ($IsFound -eq $True) {Return $DetectedLanguage}
    }
    Return $DetectedLanguage
}

function Get-CounterTranslations
{
    param([xml] $xmlCounterLang, [string] $CounterObjectEnUs, [string] $CounterNameEnUs)
    $oTranslations = New-Object System.Object

    foreach ($CounterLangObject in $xmlCounterLang.CounterLang.SelectNodes('//CounterObject'))
    {
        if ($CounterLangObject.enus -eq $CounterObjectEnUs)
        {
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'csCZ' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.csCZ)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'deDE' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.deDE)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'enUS' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.enUS)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'esES' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.esES)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'frFR' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.frFR)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'huHU' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.huHU)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'itIT' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.itIT)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'jaJP' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.jaJP)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'koKR' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.koKR)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'nlNL' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.nlNL)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'plPL' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.plPL)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'ptBR' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.ptBR)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'ptPT' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.ptPT)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'ruRU' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.ruRU)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'svSE' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.svSE)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'trTR' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.trTR)
            Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'zhCN' -Value (Decode-XmlEscapeValues -Value $CounterLangObject.zhCN)
            
            foreach ($XmlCounterNames in $CounterLangObject.SelectNodes('./CounterName'))
            {
                if ($CounterNameEnUs -eq $XmlCounterNames.enUS)
                {
                    #$oCounterNameTranslation = New-Object pscustomobject
                    $oCounterNameTranslation = New-Object System.Object
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'csCZ' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.csCZ)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'deDE' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.deDE)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'enUS' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.enUS)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'esES' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.esES)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'frFR' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.frFR)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'huHU' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.huHU)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'itIT' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.itIT)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'jaJP' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.jaJP)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'koKR' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.koKR)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'nlNL' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.nlNL)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'plPL' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.plPL)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'ptBR' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.ptBR)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'ptPT' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.ptPT)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'ruRU' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.ruRU)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'svSE' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.svSE)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'trTR' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.trTR)
                    Add-Member -InputObject $oCounterNameTranslation -MemberType NoteProperty -Name 'zhCN' -Value (Decode-XmlEscapeValues -Value $XmlCounterNames.zhCN)
                    Add-Member -InputObject $oTranslations -MemberType NoteProperty -Name 'CounterName' -Value $oCounterNameTranslation
                    Return $oTranslations
                }
            }            
        }
    }
    Return $oTranslations
}

Function GetRawDataSourceData($XmlDataSource)
{
    [bool] $IsAtLeastOneCounterInstanceFound = $False
    $htCounterIndexes = @()

    if ((Test-Path -Path 'CounterLang.xml') -eq $True)
    {
        $xmlCounterLang = [xml] (Get-Content -Path 'CounterLang.xml' -Encoding UTF8)
    }    

    $oCtr = CounterPathToObject -sCounterPath $XmlDataSource.EXPRESSIONPATH
    $oReCtr = $oCtr
    $sDsCounterObject = $oCtr.Object
    $sDsCounterName = $oCtr.Name
    $sDsCounterInstance = $oCtr.Instance
	$iCounterIndexInCsv = 0

    $oDsCounterTranslations = Get-CounterTranslations -xmlCounterLang $xmlCounterLang -CounterObjectEnUs $sDsCounterObject -CounterNameEnUs $sDsCounterName

    If ($(Test-XmlBoolAttribute -InputObject $XmlDataSource -Name 'ISCOUNTEROBJECTREGULAREXPRESSION') -eq $True)
    {
        $IsCounterObjectRegularExpression = $True
    }
    Else
    {
        $IsCounterObjectRegularExpression = $False
    }

    If ($(Test-XmlBoolAttribute -InputObject $XmlDataSource -Name 'ISCOUNTERNAMEREGULAREXPRESSION') -eq $True)
    {
        $IsCounterNameRegularExpression = $True
    }
    Else
    {
        $IsCounterNameRegularExpression = $False
    } 
    
    If ($(Test-XmlBoolAttribute -InputObject $XmlDataSource -Name 'ISCOUNTERINSTANCEREGULAREXPRESSION') -eq $True)
    {
        $IsCounterInstanceRegularExpression = $True
    }
    Else
    {
        $IsCounterInstanceRegularExpression = $False
    }

    If (($IsCounterObjectRegularExpression -eq $True) -or ($IsCounterNameRegularExpression -eq $True) -or ($IsCounterInstanceRegularExpression -eq $True))
    {
        $sDsCounterObject = GetCounterObject -sCounterPath $XmlDataSource.REGULAREXPRESSIONCOUNTERPATH
        $sDsCounterName = GetCounterName -sCounterPath $XmlDataSource.REGULAREXPRESSIONCOUNTERPATH
        $sDsCounterInstance = GetCounterInstance -sCounterPath $XmlDataSource.REGULAREXPRESSIONCOUNTERPATH
    }    
    
    :CounterComputerLoop ForEach ($XmlCounterComputerNode in $global:oXml.XmlCounterLogCounterInstanceList.SelectNodes('//COUNTERCOMPUTER'))
    {
        :CounterObjectLoop ForEach ($XmlCounterObjectNode in $XmlCounterComputerNode.ChildNodes)
        {            
            $IsCounterObjectMatch = $False
            If ($IsCounterObjectRegularExpression -eq $True)
            {
                If ($XmlCounterObjectNode.NAME -match $sDsCounterObject)
                {
                    $IsCounterObjectMatch = $True
                }
            }
            Else
            {
                If ($XmlCounterObjectNode.NAME -eq $sDsCounterObject)
                {
                    $IsCounterObjectMatch = $True
                }
            }

            #// Try counter object language match

            if (($sDsCounterObject -eq 'LogicalDisk') -and ($XmlCounterObjectNode.NAME -eq 'LogicalDisk'))
            {
                $blah = $True
            }

            if ($IsCounterObjectMatch -eq $False)
            {
                if (Test-Property -InputObject $oDsCounterTranslations -Name 'enUS')
                {
                    If ($oDsCounterTranslations.enUS -ne '')
                    {
                        Switch ($XmlCounterObjectNode.NAME)
                        {
                            $oDsCounterTranslations.csCZ {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.deDE {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.enUS {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.esES {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.frFR {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.huHU {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.itIT {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.jaJP {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.koKR {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.nlNL {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.plPL {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.ptBR {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.ptPT {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.ruRU {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.svSE {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.trTR {$IsCounterObjectMatch = $True}
                            $oDsCounterTranslations.zhCN {$IsCounterObjectMatch = $True}
                        }
                    }
                }
            }

            If ($IsCounterObjectMatch -eq $True)
            {
                :CounterNameLoop ForEach ($XmlCounterNameNode in $XmlCounterObjectNode.ChildNodes)
                {
                    $IsCounterNameMatch = $False
                    If ($IsCounterNameRegularExpression -eq $True)
                    {
                        If ($XmlCounterNameNode.NAME -match $sDsCounterName)
                        {
                            $IsCounterNameMatch = $True
                        }
                    }
                    Else
                    {
                        If ($XmlCounterNameNode.NAME -eq $sDsCounterName)
                        {
                            $IsCounterNameMatch = $True
                        }
                    }

                    if ($IsCounterNameMatch -eq $False)
                    {
                        if (Test-Property -InputObject $oDsCounterTranslations -Name 'CounterName')
                        {
                            if (Test-Property -InputObject $oDsCounterTranslations.CounterName -Name 'enUS')
                            {
                                If ($oDsCounterTranslations.CounterName.enUS -ne '')
                                {
                                    Switch ($XmlCounterNameNode.NAME)
                                    {
                                        $oDsCounterTranslations.CounterName.csCZ {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.deDE {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.enUS {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.esES {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.frFR {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.huHU {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.itIT {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.jaJP {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.koKR {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.nlNL {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.plPL {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.ptBR {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.ptPT {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.ruRU {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.svSE {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.trTR {$IsCounterNameMatch = $True}
                                        $oDsCounterTranslations.CounterName.zhCN {$IsCounterNameMatch = $True}
                                    }
                                }
                            }
                        }
                    }

                    If ($IsCounterNameMatch -eq $True)
                    {
                        :CounterInstanceLoop ForEach ($XmlCounterInstanceNode in $XmlCounterNameNode.ChildNodes)
                        {
                            $IsCounterInstanceMatch = $False
                            If (($sDsCounterInstance -eq '') -or ($sDsCounterInstance -eq '*') -or ($sDsCounterInstance -eq $null))
                            {
                                $IsCounterInstanceMatch = $True
                            }
                            Else
                            {
                                If ($IsCounterInstanceRegularExpression -eq $True)
                                {
                                    If ($XmlCounterInstanceNode.NAME -match $sDsCounterInstance)
                                    {
                                        $IsCounterInstanceMatch = $True
                                    }
                                }
                                Else
                                {
                                    If ($sDsCounterInstance -eq $XmlCounterInstanceNode.NAME)
                                    {
                                        $IsCounterInstanceMatch = $True
                                    }
                                }
                            
                            }
                            If ($IsCounterInstanceMatch -eq $True)
                            {
                                ForEach ($XmlExcludeNode in $XmlDataSource.SelectNodes('./EXCLUDE'))
                                {
                                    If ($XmlExcludeNode.INSTANCE -eq $XmlCounterInstanceNode.NAME)
                                    {
                                        $IsCounterInstanceMatch = $False
                                    }
                                }
                            }                            
                            If ($IsCounterInstanceMatch -eq $True)
                            {
                                $IsAtLeastOneCounterInstanceFound = $True
                                
                                $XmlNewCounterInstance = $global:oXml.XmlRoot.CreateElement("COUNTERINSTANCE")
                                $sCounterPath = $XmlCounterInstanceNode.COUNTERPATH
                                $XmlNewCounterInstance.SetAttribute("NAME", $XmlCounterInstanceNode.COUNTERPATH)
                                $XmlNewCounterInstance.SetAttribute("COUNTERPATH", $XmlCounterInstanceNode.COUNTERPATH)
                                $XmlNewCounterInstance.SetAttribute("COUNTERCOMPUTER", $XmlCounterComputerNode.NAME)
                                $XmlNewCounterInstance.SetAttribute("COUNTEROBJECT", $XmlCounterObjectNode.NAME)
                                $XmlNewCounterInstance.SetAttribute("COUNTERNAME", $XmlCounterNameNode.NAME)
                                $XmlNewCounterInstance.SetAttribute("COUNTERINSTANCE", $XmlCounterInstanceNode.NAME)
                                $XmlNewCounterInstance.SetAttribute("COUNTERDATAINDEX", $XmlCounterInstanceNode.COUNTERDATAINDEX)
                                [void] $XmlDataSource.AppendChild($XmlNewCounterInstance)
                            }
                        }
                    }
                }
            }
        }        
    }
    $IsAtLeastOneCounterInstanceFound
}

Function LocateCounterInstancesInCsv
{
    Write-Host 'Matching counter instances to threshold data sources...' -NoNewline
    $dtStartTime = (Get-Date)    
    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        If ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'ENABLED') -eq $True)
        {
            $XmlAnalysisInstance.SetAttribute("AllCountersFound",'True')
            ForEach ($XmlDataSource in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
            {
                If ($XmlDataSource.TYPE -eq 'CounterLog')
                {
                    $IsAtLeastOneCounterInstanceFound = GetRawDataSourceData $XmlDataSource
                    If ($IsAtLeastOneCounterInstanceFound -eq $False)
                    {
                        $XmlAnalysisInstance.SetAttribute("AllCountersFound",'False')
                    }
                }
            }
        }
    }
    $dtDuration = ConvertToDataType (New-TimeSpan -Start $dtStartTime -End (Get-Date)).TotalSeconds 'round3'
    Write-Host "Done [$dtDuration seconds]"
}

Function ConstructCounterDataArray
{
    $PercentComplete = 0
    $sComplete = "Progress: 0% (Counter ?? of ??)"
    write-progress -activity 'Importing counter data into memory...' -status $sComplete -percentcomplete $PercentComplete -id 2
    
	$oCSVFile = Get-Content -Path $global:oPal.RelogedLogFilePath
	#// Get the width and height of the CSV file as indexes.
	$aLine = $oCSVFile[0].Trim('"') -split '","'
    $iPerfmonCsvIndexWidth = $aLine.GetUpperBound(0)
	$iPerfmonCsvIndexHeight = $oCSVFile.GetUpperBound(0)

	If ($($oCSVFile[$iPerfmonCsvIndexHeight].Contains(',')) -eq $False)
	{
		do 
		{
			$iPerfmonCsvIndexHeight = $iPerfmonCsvIndexHeight - 1
		} until ($($oCSVFile[$iPerfmonCsvIndexHeight].Contains(',')) -eq $true)	
	}
	For ($i=0;$i -le $iPerfmonCsvIndexHeight;$i++)
	{
		$aLine = $oCSVFile[$i].Trim('"') -split '","'
		[void] $global:oPal.LogCounterData.Add($aLine)
        $PercentComplete = CalculatePercentage -Number $i -Total $iPerfmonCsvIndexHeight
        $sComplete = "Progress: $(ConvertToDataType $PercentComplete 'integer')% (Counter $i of $iPerfmonCsvIndexHeight)"
        write-progress -activity 'Importing counter data into memory...' -status $sComplete -percentcomplete $PercentComplete -id 2
	}
    $sComplete = "Progress: 100% (Counter $iPerfmonCsvIndexHeight of $iPerfmonCsvIndexHeight)"
    write-progress -activity 'Importing counter data into memory...' -status $sComplete -Completed -id 2
}

Function GetCounterDataFromPerfmonLog($iCounterIndexInCsv)
{    
    $aValues = New-Object System.Collections.ArrayList
    If ($global:oPal.LogCounterData.Count -eq 0)
    {
        ConstructCounterDataArray
    }
    
    For ($i=1;$i -lt $global:oPal.LogCounterData.Count;$i++)
    {
        [void] $aValues.Add($($global:oPal.LogCounterData[$i][$iCounterIndexInCsv]))
    }
	$aValues
}

Function GetTimeZoneFromCsvFile
{
    param($CsvFilePath)
    
	$oCSVFile = Get-Content $CsvFilePath
	$aRawCounterList = $oCSVFile[0].Split(",")
	Return $aRawCounterList[0].Trim("`"")
}

Function GetTimeDataFromPerfmonLog()
{
	If ($global:oPal.LogCounterTimeZone -eq '')
	{
		$global:oPal.LogCounterTimeZone = GetTimeZoneFromCsvFile $global:oPal.RelogedLogFilePath
	}
    $global:oPal.aTime = GetCounterDataFromPerfmonLog -sCounterPath $global:oPal.LogCounterTimeZone -iCounterIndexInCsv 0
}

Function GenerateAutoAnalysisInterval
{
	param($ArrayOfTimes,$NumberOfTimeSlices=30)
    $dtBeginDateTime = $ArrayOfTimes[0]
    $dtEndDateTime = $ArrayOfTimes[$ArrayOfTimes.GetUpperBound(0)]
    
    If ($dtBeginDateTime -isnot [datetime])
    {
        [datetime] $dtBeginDateTime = $dtBeginDateTime
    }
    
    If ($dtEndDateTime -isnot [datetime])
    {
        [datetime] $dtEndDateTime = $dtEndDateTime
    }
        
    $iTimeSpanInSeconds = [int] $(New-TimeSpan -Start ($dtBeginDateTime) -End ($dtEndDateTime)).TotalSeconds
	[int] $AutoAnalysisIntervalInSeconds = $iTimeSpanInSeconds / $NumberOfTimeSlices
	$AutoAnalysisIntervalInSeconds
}

Function ProcessAnalysisInterval
{
    If ($global:oPal.ArgsProcessed.AnalysisInterval -eq 'AUTO')
    {
        Write-Host "Auto analysis interval (one time only)..." -NoNewline
        $global:oPal.ArgsProcessed.AnalysisInterval = GenerateAutoAnalysisInterval -ArrayOfTimes $global:oPal.aTime -NumberOfTimeSlices $AutoAnalysisIntervalNumberOfTimeSlices
        Write-Host 'Done'
    }
    Else
    {
        $global:AnalysisInterval = $Interval
    }
}

Function AddCounterStatsToXmlCounterInstances
{
    param($oCompletedAnalysis)

    If ($oCompletedAnalysis -eq $null)
    {
        Return $null
    }

    $XmlAnalysis = $global:oXml.XmlAnalyses.ANALYSIS[$oCompletedAnalysis.XmlNodeIndex]

    ForEach ($oDataSource in @($oCompletedAnalysis.DataSources))
    {
        If (@($XmlAnalysis.DATASOURCE).Count -gt 1)
        {
            $XmlDataSource = $XmlAnalysis.DATASOURCE[$oDataSource.XmlNodeIndex]
        }
        Else
        {
            $XmlDataSource = $XmlAnalysis.DATASOURCE
        }

        If ($XmlDataSource.TYPE -eq 'CounterLog')
        {
            ForEach ($oCounterInstance in @($oDataSource.CounterInstances))
            {            
                If (@($XmlDataSource.COUNTERINSTANCE).Count -gt 1)
                {
                    $XmlCounterInstance = $XmlDataSource.COUNTERINSTANCE[$oCounterInstance.XmlNodeIndex]
                }
                Else
                {
                    $XmlCounterInstance = $XmlDataSource.COUNTERINSTANCE
                }

                If ($oCounterInstance.oStats.Min -is [System.Double])
                {$XmlCounterInstance.SetAttribute("MIN", $($oCounterInstance.oStats.Min))}
                Else {$XmlCounterInstance.SetAttribute("MIN","")}
        
                If ($oCounterInstance.oStats.Avg -is [System.Double])
                {$XmlCounterInstance.SetAttribute("AVG", $($oCounterInstance.oStats.Avg))}
                Else {$XmlCounterInstance.SetAttribute("AVG","")}
        
                If ($oCounterInstance.oStats.Max -is [System.Double])
                {$XmlCounterInstance.SetAttribute("MAX", $($oCounterInstance.oStats.Max))}
                Else {$XmlCounterInstance.SetAttribute("MAX","")}

                If ($oCounterInstance.oStats.Trend -is [System.Double])
                {$XmlCounterInstance.SetAttribute("TREND", $($oCounterInstance.oStats.Trend))}
                Else {$XmlCounterInstance.SetAttribute("TREND","")}
        
                If ($oCounterInstance.oStats.StdDev -is [System.Double])
                {$XmlCounterInstance.SetAttribute("STDDEV", $($oCounterInstance.oStats.StdDev))}
                Else {$XmlCounterInstance.SetAttribute("STDDEV","")}
        
                If ($oCounterInstance.oStats.PercentileSeventyth -is [System.Double])
                {$XmlCounterInstance.SetAttribute("PERCENTILESEVENTYTH", $($oCounterInstance.oStats.PercentileSeventyth))}
                Else {$XmlCounterInstance.SetAttribute("PERCENTILESEVENTYTH","")}
        
                If ($oCounterInstance.oStats.PercentileEightyth -is [System.Double])
                {$XmlCounterInstance.SetAttribute("PERCENTILEEIGHTYTH", $($oCounterInstance.oStats.PercentileEightyth))}
                Else {$XmlCounterInstance.SetAttribute("PERCENTILEEIGHTYTH","")}
        
                If ($oCounterInstance.oStats.PercentileNinetyth -is [System.Double])
                {$XmlCounterInstance.SetAttribute("PERCENTILENINETYTH", $($oCounterInstance.oStats.PercentileNinetyth))}
                Else {$XmlCounterInstance.SetAttribute("PERCENTILENINETYTH","")}
        
                If (($oCounterInstance.oStats.QuantizedMinValues -is [System.Collections.ArrayList]) -and ($oCounterInstance.oStats.QuantizedMinValues -ne $null))
                {
                    [string] $sStringValues = [string]::Join(',',$($oCounterInstance.oStats.QuantizedMinValues))
                    $XmlCounterInstance.SetAttribute("QUANTIZEDMIN", $sStringValues)
                }
                Else {$XmlCounterInstance.SetAttribute("QUANTIZEDMIN","")}
        
                If (($oCounterInstance.oStats.QuantizedAvgValues -is [System.Collections.ArrayList]) -and ($oCounterInstance.oStats.QuantizedAvgValues -ne $null))
                {
                    [string] $sStringValues = [string]::Join(',',$($oCounterInstance.oStats.QuantizedAvgValues))
                    $XmlCounterInstance.SetAttribute("QUANTIZEDAVG", $sStringValues)
                }
                Else {$XmlCounterInstance.SetAttribute("QUANTIZEDAVG","")}
        
                If (($oCounterInstance.oStats.QuantizedMaxValues -is [System.Collections.ArrayList]) -and ($oCounterInstance.oStats.QuantizedMaxValues -ne $null))
                {
                    [string] $sStringValues = [string]::Join(',',$($oCounterInstance.oStats.QuantizedMaxValues))
                    $XmlCounterInstance.SetAttribute("QUANTIZEDMAX", $sStringValues)
                }
                Else {$XmlCounterInstance.SetAttribute("QUANTIZEDMAX","")}
        
                If (($oCounterInstance.oStats.QuantizedTrendValues -is [System.Collections.ArrayList]) -and ($oCounterInstance.oStats.QuantizedTrendValues -ne $null))
                {
                    [string] $sStringValues = [string]::Join(',',$($oCounterInstance.oStats.QuantizedTrendValues))
                    $XmlCounterInstance.SetAttribute("QUANTIZEDTREND", $sStringValues)
                }
                Else {$XmlCounterInstance.SetAttribute("QUANTIZEDTREND","")}

                $oCtr = CounterPathToObject -sCounterPath $oCounterInstance.Path
                AddToCounterInstanceStatsArrayList $oCounterInstance.Path $oPal.aTime $oCounterInstance.aValues $oPal.QuantizedTime $($oCounterInstance.oStats.QuantizedMinValues) $($oCounterInstance.oStats.QuantizedAvgValues) $($oCounterInstance.oStats.QuantizedMaxValues) $($oCounterInstance.oStats.QuantizedTrendValues) $oCtr.Computer $oCtr.Object $oCtr.Name $oCtr.Instance $($oCounterInstance.oStats.Min) $($oCounterInstance.oStats.Avg) $($oCounterInstance.oStats.Max) $($oCounterInstance.oStats.Trend) $($oCounterInstance.oStats.StdDev) $($oCounterInstance.oStats.PercentileSeventyth) $($oCounterInstance.oStats.PercentileEightyth) $($oCounterInstance.oStats.PercentileNinetyth)
            }
        }
    }
}

Function DistributeJobs
{
    $iNumOfThreads = $global:oPal.ArgsProcessed.NumberOfThreads
    $iNumOfJobs = @($global:oCollectionOfAnalyses).Count

    $BatchesOfThreadJobs = New-Object System.Collections.ArrayList
    For ($t = 0; $t -lt $iNumOfThreads; $t++)
    {
        $aThreadJobs = New-Object System.Collections.ArrayList
        [void] $BatchesOfThreadJobs.Add($aThreadJobs)
    }

    $j = 0
    For ($i = 0; $i -lt $iNumOfJobs;$i = $i + $iNumOfThreads)
    {
        For ($t = 0; $t -lt $iNumOfThreads;$t++)
        {
            If ($j -lt $iNumOfJobs)
            {
                $oJob = $global:oCollectionOfAnalyses[$j]
                [void] $BatchesOfThreadJobs[$t].Add($oJob)
                $j++
            }
        }
    }
    $BatchesOfThreadJobs
}

Function AddCompletedJobBatchToXmlCounterInstances
{
    param($oCompletedBatchJob)

    ForEach ($oCompletedJob in @($oCompletedBatchJob))
    {
        If ($oCompletedJob -is [System.Management.Automation.PSCustomObject])
        {
            AddCounterStatsToXmlCounterInstances -oCompletedAnalysis $oCompletedJob
        }
    }
}

Function UpdateOverallProgress
{
    param([string] $Status='')
    $global:iOverallCompletion++
    $iPercentComplete = ConvertToDataType $(($global:iOverallCompletion / 17) * 100) 'integer'
    If ($iPercentComplete -gt 100){$iPercentComplete = 100}
    $sComplete = "PAL $Version Progress: $iPercentComplete%... $Status"
    Write-Progress -activity 'Overall progress...' -status $sComplete -percentcomplete $iPercentComplete -id 1;
    $global:oOverallProgress = 'Overall progress... Status: ' + "$($Status)" + ', ' + "$($sComplete)"
}

Function LoadCounterDataIntoXml
{
    LocateCounterInstancesInCsv
    
    UpdateOverallProgress -Status 'Preparing the thread engine...'
    #// Prep counter stats generation
    $global:Jobs = New-Object System.Collections.ArrayList
    $global:oCollectionOfAnalyses = New-Object System.Collections.ArrayList
    $iAnalysisIndex = 0

    $iNumberOfAnalyses = 0
    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes("//ANALYSIS"))
    {   
        If (($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'ENABLED') -eq $True) -and ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'AllCountersFound') -eq $True))
        {
            $iNumberOfAnalyses++
        }
    }

    $iPercentComplete = 0
    $sComplete = '(Analysis: 0 of ' + $iNumberOfAnalyses + ')'
    Write-Progress -activity 'Preparing the thread engine...' -status $sComplete -percentcomplete $iPercentComplete -id 2;
    $iAnalysisNumber = 0

    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes("//ANALYSIS"))
    {   
        If (($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'ENABLED') -eq $True) -and ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'AllCountersFound') -eq $True))
        {
            $iAnalysisNumber++
            $iPercentComplete = ConvertToDataType $(($iAnalysisNumber / $iNumberOfAnalyses) * 100) 'integer'
            $sComplete = '(Analysis: '  + $iAnalysisNumber  + ' of ' + $iNumberOfAnalyses + ')'
            Write-Progress -activity 'Preparing the thread engine...' -status $sComplete -percentcomplete $iPercentComplete -id 2;

            $oCollectionOfDataSources = New-Object System.Collections.ArrayList

            $oAnalysis = New-Object pscustomobject
            Add-Member -InputObject $oAnalysis -MemberType NoteProperty -Name 'Name' -Value $XmlAnalysisInstance.NAME
            Add-Member -InputObject $oAnalysis -MemberType NoteProperty -Name 'XmlNodeIndex' -Value $iAnalysisIndex

            $iDataSourceIndex = 0
            ForEach ($XmlDataSource in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
            {
                $oCollectionOfCounterInstances = New-Object System.Collections.ArrayList

                $oDataSource = New-Object pscustomobject
                Add-Member -InputObject $oDataSource -MemberType NoteProperty -Name 'Name' -Value $XmlDataSource.NAME
                Add-Member -InputObject $oDataSource -MemberType NoteProperty -Name 'XmlNodeIndex' -Value $iDataSourceIndex
                Add-Member -InputObject $oDataSource -MemberType NoteProperty -Name 'Type' -Value $XmlDataSource.TYPE
                Add-Member -InputObject $oDataSource -MemberType NoteProperty -Name 'DataType' -Value $XmlDataSource.DATATYPE

                If ($XmlDataSource.TYPE -eq 'CounterLog')
                {
                    $iCounterInstanceIndex = 0
                    ForEach ($XmlCounterInstance in $XmlDataSource.SelectNodes('./COUNTERINSTANCE'))
                    {
                        $oCounterInstance = New-Object pscustomobject
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'Path' -Value $XmlCounterInstance.COUNTERPATH
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'XmlNodeIndex' -Value $iCounterInstanceIndex
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'CounterListIndexInCsv' -Value $XmlCounterInstance.COUNTERDATAINDEX
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'IsAllNull' -Value $True
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'oStats' -Value @()
                        Add-Member -InputObject $oCounterInstance -MemberType NoteProperty -Name 'aValues' -Value @()

                        $ic = $XmlCounterInstance.COUNTERDATAINDEX
                        $oCounterInstance.aValues = GetCounterDataFromPerfmonLog -iCounterIndexInCsv $ic

                        $global:IsValuesAllNull = $True
                        $oCounterInstance.aValues = FillNullsWithDashesAndIsAllNull -Values $oCounterInstance.aValues
                        $oCounterInstance.IsAllNull = $global:IsValuesAllNull

                        If ($global:IsValuesAllNull -eq $True)
                        {
                            $XmlCounterInstance.SetAttribute("ISALLNULL", "True")
                        }
                        Else
                        {
                            $XmlCounterInstance.SetAttribute("ISALLNULL", "False")
                        }

                        [void] $oCollectionOfCounterInstances.Add($oCounterInstance)
                        $iCounterInstanceIndex++
                    }
                }

                Add-Member -InputObject $oDataSource -MemberType NoteProperty -Name 'CounterInstances' -Value $oCollectionOfCounterInstances
                [void] $oCollectionOfDataSources.Add($oDataSource)
                $iDataSourceIndex++
            }
            Add-Member -InputObject $oAnalysis -MemberType NoteProperty -Name 'DataSources' -Value $oCollectionOfDataSources
            [void] $global:oCollectionOfAnalyses.Add($oAnalysis)
        }
        $iAnalysisIndex++
    }

    $LoadCounterStartTime = (Get-Date)
    $i = 0
    
    $iNumOfAnalyses = @($global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS')).Count
    $iTotalJobs = $global:oCollectionOfAnalyses.Count
    $iIndexOfJobs = 0
    $iNumOfThreadsRunning = 0

    #// Distribute to each thread.
    $BatchesOfThreadJobs = @(DistributeJobs)

    #// Remove all of the jobs that might be running previously to this session.
    If (@(Get-Job).Count -gt 0)
    {
        Remove-Job -Name * -Force
    }
    UpdateOverallProgress -Status 'Calculating counter statistics [very CPU intensive]...'
    Write-Host 'Calculating counter statistics [very CPU intensive]...'

    $dtStartTime = (Get-Date)
    For ($t=0;$t -lt @($BatchesOfThreadJobs).Count;$t++)
    {
        $oBatch = $BatchesOfThreadJobs[$t]
        $sText = "Number of jobs passed into Thread($t): $(@($oBatch).Count)"
        Write-Host `t$sText
        $oJobReturn = Start-Job -FilePath .\PalGenerateMultiCounterStats.ps1 -ArgumentList $oBatch, $global:oPal.QuantizedIndex, $global:oPal.ArgsProcessed.AnalysisInterval, $global:oPal.ArgsProcessed.IsLowPriority, $t
    }

    $iTesting = 0

    $BatchesOfCompletedJobs = New-Object System.Collections.ArrayList
    $IsDone = $False    
    While ($IsDone -eq $False)
    {        
        foreach ($Job in Get-Job)
        {
            Switch ($Job.State)
            {
                'Completed'
                {
                    $Returned = @(Receive-Job -Job $Job)
                    If ($Returned -ne $null)
                    {
                        ForEach ($Result in @($Returned))
                        {
                            If ($Result -ne $null)
                            {
                                Write-Output `t"$($Result.Name)"
                                $global:OverallActiveAnalysis = $($Result.Name)
                                $iIndexOfJobs++
                                AddCounterStatsToXmlCounterInstances -oCompletedAnalysis $Result
                            }
                        }
                    }
                    Remove-Job $job.id
                }

                'Running'
                {

                    $Returned = Receive-Job -Job $Job
                    If ($Returned -ne $null)
                    {
                        ForEach ($Result in @($Returned))
                        {
                            If ($Result -ne $null)
                            {
                                If ($(Test-Property -InputObject $Result -Name 'Name') -eq $True)
                                {
                                    Write-Output `t"$($Result.Name)"
                                    $global:OverallActiveAnalysis = $($Result.Name)
                                }
                                Else
                                {
                                    Write-Output `t"$($Result)"
                                }
                                $iIndexOfJobs++
                                AddCounterStatsToXmlCounterInstances -oCompletedAnalysis $Result
                            }
                        }
                    }
                }
            }
        }
        $iJobCount = @(Get-Job).Count
        $iPercentComplete = ConvertToDataType $(($iIndexOfJobs / $iTotalJobs) * 100) 'integer'
        If ($iPercentComplete -gt 100)
        {
            $iPercentComplete = 100
        }
        $sComplete = "Progress: $iPercentComplete% (Analysis $iIndexOfJobs of $iTotalJobs) RunningThreads: $iJobCount/$(@($BatchesOfThreadJobs).Count)"
        Write-Progress -activity 'Calculating counter statistics...' -status $sComplete -percentcomplete $iPercentComplete -id 2;
        If ($iJobCount -eq 0)
        {
            $IsDone = $True
        }
        Else
        {
            Start-Sleep -Milliseconds 1000
        }
    }

    $sComplete = '(RunningThreads: 0 of ' + $(@($BatchesOfThreadJobs).Count) + ')'
    Write-Progress -activity 'Calculating counter statistics...' -status $sComplete -Completed -id 2;

    $dtDuration = ConvertToDataType (New-TimeSpan -Start $dtStartTime -End (Get-Date)).TotalSeconds 'round3'
    Write-Host "Calculating counter statistics... Done! [$dtDuration seconds]"
}

Function GenerateQuantizedIndexArray
{
	param($ArrayOfTimes,$AnalysisIntervalInSeconds=60)

    Write-Host "Quantized index (one time only)..." -NoNewline
	$alIndexArray = New-Object System.Collections.ArrayList
	$alSubIndexArray = New-Object System.Collections.ArrayList
	[datetime] $dTimeCursor = [datetime] $ArrayOfTimes[0]
	$dTimeCursor = $dTimeCursor.AddSeconds($AnalysisIntervalInSeconds)
    $u = $ArrayOfTimes.GetUpperBound(0)
    $dEndTime = [datetime] $ArrayOfTimes[$u]
    
    #// If the analysis interval is larger than the entire time range of the log, then just use the one time slice.
    If ($dTimeCursor -gt $dEndTime)
    {
        $dDurationTime = New-TimeSpan -Start $ArrayOfTimes[0] -End $dEndTime
        Write-Warning $('The analysis interval is larger than the time range of the entire log. Please use an analysis interval that is smaller than ' + "$($dDurationTime.TotalSeconds)" + ' seconds.')
        Write-Warning $("Log Start Time: $($ArrayOfTimes[0])")
        Write-Warning $("Log Stop Time: $($ArrayOfTimes[$u])")
        Write-Warning $("Log Length: $($dDurationTime)")

        $sText = $('The analysis interval is larger than the time range of the entire log. Please use an analysis interval that is smaller than ' + "$($dDurationTime.TotalSeconds)" + ' seconds.' ) + $(". Log Start Time: $($ArrayOfTimes[0])") + $(". Log Stop Time: $($ArrayOfTimes[$u])") + $(". Log Length: $($dDurationTime)")
        WriteErrorToHtmlAndShow -sError $sText
        Break Main;
    }
    
    #// Set the Chart X Axis interval
    
    If ($global:oPal.NumberOfValuesPerTimeSlice -eq -1)
    {
    	:ValuesPerTimeSliceLoop For ($i=0;$i -le $ArrayOfTimes.GetUpperBound(0);$i++)
    	{
    		If ($ArrayOfTimes[$i] -le $dTimeCursor)
    		{
    			[Void] $alSubIndexArray.Add($i)
                $global:oPal.NumberOfValuesPerTimeSlice = $alSubIndexArray.Count
    		}
    		Else
    		{
                [Void] $alSubIndexArray.Add($i)
                $global:oPal.NumberOfValuesPerTimeSlice = $alSubIndexArray.Count
    			$alSubIndexArray.Clear()
                Break ValuesPerTimeSliceLoop;
    		}
    	}

        $global:ChartSettings.XInterval = $global:oPal.NumberOfValuesPerTimeSlice
        $iNumberOfValuesPerTimeSliceInChart = $global:oPal.NumberOfValuesPerTimeSlice
        $iNumberOfIntervals = $ArrayOfTimes.Count / $global:oPal.NumberOfValuesPerTimeSlice
        $iNumberOfIntervals = [Math]::Round($iNumberOfIntervals,0)
        
        If ($iNumberOfIntervals -gt $global:ChartSettings.XIntervalMax)
        {
            $iNumberOfValuesPerTimeSliceInChart = $ArrayOfTimes.Count / $global:ChartSettings.XIntervalMax
            $iNumberOfValuesPerTimeSliceInChart = [Math]::Round($iNumberOfValuesPerTimeSliceInChart,0)
            $global:ChartSettings.XInterval = $iNumberOfValuesPerTimeSliceInChart
        }
    }    
    
    #// Quantize the time array.
	For ($i=0;$i -le $ArrayOfTimes.GetUpperBound(0);$i++)
	{
		If ($ArrayOfTimes[$i] -le $dTimeCursor)
		{
			[Void] $alSubIndexArray.Add($i)
		}
		Else
		{
			[Void] $alIndexArray.Add([System.Object[]] $alSubIndexArray)
			$alSubIndexArray.Clear()
			[Void] $alSubIndexArray.Add($i)
			$dTimeCursor = $dTimeCursor.AddSeconds($AnalysisIntervalInSeconds)
		}
	}
    Write-Host 'Done!'
	$alIndexArray
}

Function AddToCounterInstanceStatsArrayList
{
    param($sCounterPath,$aTime,$aValue,$alQuantizedTime,$alQuantizedMinValues,$alQuantizedAvgValues,$alQuantizedMaxValues,$alQuantizedTrendValues,$sCounterComputer,$sCounterObject,$sCounterName,$sCounterInstance, $Min='-', $Avg='-', $Max='-', $Trend='-', $StdDev='-', $PercentileSeventyth='-', $PercentileEightyth='-', $PercentileNinetyth='-')
        
    If ($global:htCounterInstanceStats.Contains($sCounterPath) -eq $False)
    {
    	$quantizedResultsObject = New-Object pscustomobject
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name CounterPath -Value $sCounterPath
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name CounterComputer -Value $sCounterComputer
        #// Check if this is a SQL Named instance.
        If (($($sCounterPath.Contains('MSSQL$')) -eq $True) -or ($($sCounterPath.Contains('MSOLAP$')) -eq $True))
        {
            $sCounterObject = GetCounterObject $sCounterPath
        }
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Name -Value $sCounterPath
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name CounterObject -Value $sCounterObject
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name CounterName -Value $sCounterName
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name CounterInstance -Value $sCounterInstance
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Time -Value $aTime
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Value -Value $aValue
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name QuantizedTime -Value $alQuantizedTime
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name QuantizedMin -Value $alQuantizedMinValues
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name QuantizedAvg -Value $alQuantizedAvgValues
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name QuantizedMax -Value $alQuantizedMaxValues
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name QuantizedTrend -Value $alQuantizedTrendValues
    	Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Min -Value $Min
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Avg -Value $Avg
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Max -Value $Max
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name Trend -Value $Trend
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name StdDev -Value $StdDev
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name PercentileSeventyth -Value $PercentileSeventyth
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name PercentileEightyth -Value $PercentileEightyth
        Add-Member -InputObject $quantizedResultsObject -MemberType NoteProperty -Name PercentileNinetyth -Value $PercentileNinetyth
    	[void] $global:htCounterInstanceStats.Add($sCounterPath,$quantizedResultsObject)
    }
}

Function GenerateQuantizedTimeArray
{
	param($ArrayOfTimes,$QuantizedIndexArray = $(GenerateQuantizedIndexArray -ArrayOfTimes $ArrayOfTimes -AnalysisIntervalInSeconds $global:AnalysisInterval))
    #Write-Host "Quantized time (one time only)..." -NoNewline
	$alQuantizedTimeArray = New-Object System.Collections.ArrayList
	For ($i=0;$i -lt $QuantizedIndexArray.Count;$i++)
	{
		$iFirstIndex = $QuantizedIndexArray[$i][0]
		[void] $alQuantizedTimeArray.Add([datetime]$ArrayOfTimes[$iFirstIndex])	
	}
    #Write-Host 'Done!'
	$alQuantizedTimeArray
    $global:oPal.QuantizedTime = $alQuantizedTimeArray
    
    #// For backward compatibility in threshold files.
    $global:alQuantizedTime = $alQuantizedTimeArray
}

Function ExecuteCodeForGeneratedDataSource
{
    param($Code,$Name,$ExpressionPath,$htVariables,$htQuestionVariables)
    #// This needs to be in its own function call due to how local variables are used.
    Invoke-Expression -Command $sCode
}

Function PrepareGeneratedCodeReplacements
{
    param($XmlAnalysisInstance)
    	
    #// Generated data source, charts, and thresholds assume that all of the counterlog counters are available to it.
	ForEach ($XmlCounterDataSource in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
	{       
		If ($XmlCounterDataSource.TYPE -eq "CounterLog")
		{
			$global:alCounterDataSourceCollection = New-Object System.Collections.ArrayList
            ForEach ($XmlCounterDataSourceInstance in $XmlCounterDataSource.SelectNodes("./COUNTERINSTANCE"))
            {
                If ($(Test-XmlBoolAttribute -InputObject $XmlCounterDataSourceInstance -Name 'ISALLNULL') -eq $True)
                {
                    $IsAllNull = $True
                }
                Else
                {
                    $IsAllNull = $False
                }
                
                If ($IsAllNull -eq $False)
                {
                    [void] $alCounterDataSourceCollection.Add($global:htCounterInstanceStats[$XmlCounterDataSourceInstance.NAME])
                }
            }
            $IsKeyExist = $False
            $IsKeyExist = $global:htVariables.ContainsKey($XmlCounterDataSource.COLLECTIONVARNAME)
            If ($IsKeyExist -eq $False)
            {
                [void] $global:htVariables.Add($XmlCounterDataSource.COLLECTIONVARNAME,$alCounterDataSourceCollection)
            }

            $sCollectionName = $XmlCounterDataSource.COLLECTIONVARNAME
            $sCollectionNameWithBackslash = "\`$$sCollectionName"
            $sCollectionNameWithDoubleQuotes = "`"$sCollectionName`""
            $sCollectionVarName = "`$htVariables[$sCollectionNameWithDoubleQuotes]"

            $IsKeyExist = $False
            $IsKeyExist = $htCodeReplacements.ContainsKey($sCollectionNameWithBackslash)
            If ($IsKeyExist -eq $False)
            {
                [void] $htCodeReplacements.Add($sCollectionNameWithBackslash,$sCollectionVarName)
            }
		}
	}
                    
    #// Add the code replacements for the question variables
    ForEach ($sKey in $oPal.QuestionVariables.Keys)
    {
        $sModifiedKey = "\`$$sKey"
        $sKeyWithDoubleQuotes = "`"$sKey`""
        $sModifiedVarName = "`$oPal.QuestionVariables[$sKeyWithDoubleQuotes]"
        $IsInHashTable = $oPal.QuestionVariables.Contains($sModifiedKey)
        $IsKeyExist = $htCodeReplacements.Contains($sModifiedKey)
        If (($IsInHashTable -eq $false) -and ($IsKeyExist -eq $False))
        {
            [void] $htCodeReplacements.Add($sModifiedKey,$sModifiedVarName)
        }
    }
}

Function GenerateDataSourceData
{
    param($XmlGeneratedDataSource)

	#// Add a code replacement for the generated data source collection
	$alGeneratedDataSourceCollection = New-Object System.Collections.ArrayList
	[void] $global:htVariables.Add($XmlGeneratedDataSource.COLLECTIONVARNAME,$alGeneratedDataSourceCollection)
	$sCollectionName = $XmlGeneratedDataSource.COLLECTIONVARNAME
	$sCollectionNameWithBackslash = "\`$$sCollectionName"
	$sCollectionNameWithDoubleQuotes = "`"$sCollectionName`""
	$sCollectionVarName = "`$global:htVariables[$sCollectionNameWithDoubleQuotes]"
	[void] $global:htCodeReplacements.Add($sCollectionNameWithBackslash,$sCollectionVarName)

	$ExpressionPath = $XmlGeneratedDataSource.EXPRESSIONPATH
        
	$Name = $XmlGeneratedDataSource.NAME

    $XmlCode = $XmlGeneratedDataSource.CODE
	$sCode = $XmlCode.get_innertext()
	#// Replace all of the variables with their hash table version.
	ForEach ($sKey in $global:htCodeReplacements.Keys)
	{
		$sCode = $sCode -Replace $sKey,$global:htCodeReplacements[$sKey]
	}
	#// Execute the code
	ExecuteCodeForGeneratedDataSource -Code $sCode -Name $Name -ExpressionPath $ExpressionPath -htVariables $global:htVariables -htQuestionVariables $global:oPal.QuestionVariables

    $alNewGeneratedCounters = New-Object System.Collections.ArrayList

    $aKeys = $global:htVariables[$XmlGeneratedDataSource.COLLECTIONVARNAME].Keys | Sort

   ForEach ($sKey in $aKeys)
   {                    
		$aValue = $global:htVariables[$XmlGeneratedDataSource.COLLECTIONVARNAME][$sKey]

        $oStats = .\PalGenerateCounterStats.ps1 $aValue $global:oPal.QuantizedIndex $($XmlGeneratedDataSource.DATATYPE) $global:oPal.ArgsProcessed.AnalysisInterval $global:oPal.ArgsProcessed.IsLowPriority

        $oCtr = CounterPathToObject -sCounterPath $sKey
        AddToCounterInstanceStatsArrayList $sKey $oPal.aTime $aValue $oPal.QuantizedTime $oStats.QuantizedMinValues $oStats.QuantizedAvgValues $oStats.QuantizedMaxValues $oStats.QuantizedTrendValues $oCtr.Computer $oCtr.Object $oCtr.Name $oCtr.Instance $oStats.Min $oStats.Avg $oStats.Max $oStats.Trend $oStats.StdDev $oStats.PercentileSeventyth $oStats.PercentileEightyth $oStats.PercentileNinetyth    
        
		$XmlNewCounterInstance = $global:oXml.XmlRoot.CreateElement("COUNTERINSTANCE")
		$XmlNewCounterInstance.SetAttribute("NAME", $sKey)
        [string] $sStringValues = $oStats.Min
		$XmlNewCounterInstance.SetAttribute("MIN", $sStringValues)
        [string] $sStringValues = $oStats.Avg
		$XmlNewCounterInstance.SetAttribute("AVG", $sStringValues)
        [string] $sStringValues = $oStats.Max
		$XmlNewCounterInstance.SetAttribute("MAX", $sStringValues)
        [string] $sStringValues = $oStats.Trend
		$XmlNewCounterInstance.SetAttribute("TREND", $sStringValues)
        [string] $sStringValues = $oStats.PercentileSeventyth
		$XmlNewCounterInstance.SetAttribute("PERCENTILESEVENTYTH", $sStringValues)
        [string] $sStringValues = $oStats.PercentileEightyth
		$XmlNewCounterInstance.SetAttribute("PERCENTILEEIGHTYTH", $sStringValues)
        [string] $sStringValues = $oStats.PercentileNinetyth
		$XmlNewCounterInstance.SetAttribute("PERCENTILENINETYTH", $sStringValues)
        [string] $sStringValues = [string]::Join(',',($oStats.QuantizedMinValues))
		$XmlNewCounterInstance.SetAttribute("QUANTIZEDMIN", $([string]::Join(',',$sStringValues)))
        [string] $sStringValues = [string]::Join(',',($oStats.QuantizedAvgValues))
		$XmlNewCounterInstance.SetAttribute("QUANTIZEDAVG", $([string]::Join(',',$sStringValues)))
        [string] $sStringValues = [string]::Join(',',($oStats.QuantizedMaxValues))
		$XmlNewCounterInstance.SetAttribute("QUANTIZEDMAX", $([string]::Join(',',$sStringValues)))
        [string] $sStringValues = [string]::Join(',',($oStats.QuantizedTrendValues))
		$XmlNewCounterInstance.SetAttribute("QUANTIZEDTREND", $([string]::Join(',',$sStringValues)))
        [string] $sStringValues = [string]::Join(',',($oStats.StdDev))
		$XmlNewCounterInstance.SetAttribute("STDDEV", $([string]::Join(',',$sStringValues)))
		$XmlNewCounterInstance.SetAttribute("COUNTERPATH", $sKey)
		$XmlNewCounterInstance.SetAttribute("COUNTERCOMPUTER", $oCtr.Computer)
		$XmlNewCounterInstance.SetAttribute("COUNTEROBJECT", $oCtr.Object)
		$XmlNewCounterInstance.SetAttribute("COUNTERNAME", $oCtr.Name)
		$XmlNewCounterInstance.SetAttribute("COUNTERINSTANCE", $oCtr.Instance)
		[void] $XmlGeneratedDataSource.AppendChild($XmlNewCounterInstance)
        [void] $alNewGeneratedCounters.Add($global:htCounterInstanceStats[$sKey])      
   }
   #// Replace the collection made from the generation code so that it is the same as other counters.
   $global:htVariables[$XmlGeneratedDataSource.COLLECTIONVARNAME] = $alNewGeneratedCounters   
}

Function GenerateDataSources
{
    $dtStartTime = (Get-Date)
    Write-Host 'Creating generated counter data [This is PAL working with multiple counters to produce a single result]...'
    $iNumOfGeneratedCounters = 0

    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        If ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'ENABLED') -eq $True)
        {
            $IsEnabled = $True
        }
        Else
        {
            $IsEnabled = $False
        }

        If ($IsEnabled -eq $True)
        {
        	ForEach ($XmlDataSource in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
        	{
                If ($XmlDataSource.TYPE -eq 'Generated')
                {
                    $iNumOfGeneratedCounters++
                }
        	}
        }
    }

    $iDsNum = 0

    $iPercentComplete = 0
    $sComplete = "Progress: $iPercentComplete% (Analysis $iDsNum of $iNumOfGeneratedCounters)"
    Write-Progress -activity "Creating generated counter data..." -status $sComplete -percentcomplete $iPercentComplete -id 2;

    ForEach ($XmlAnalysisInstance in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        If ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'ENABLED') -eq $True)
        {
            $IsEnabled = $True
        }
        Else
        {
            $IsEnabled = $False
        }

        If ($IsEnabled -eq $True)
        {
            If ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisInstance -Name 'AllCountersFound') -eq $True)
            {
                ForEach ($XmlDataSource in $XmlAnalysisInstance.SelectNodes('./DATASOURCE'))
            	{
                    If ($XmlDataSource.TYPE -eq 'Generated')
                    {
                        Write-Output `t"$($XmlAnalysisInstance.Name)"
                        $global:OverallActiveAnalysis = $($XmlAnalysisInstance.Name)
        			    #//$global:htVariables = @{}
        			    #//$global:htCodeReplacements = @{}
                        PrepareGeneratedCodeReplacements -XmlAnalysisInstance $XmlAnalysisInstance
            			GenerateDataSourceData -XmlGeneratedDataSource $XmlDataSource
                        $iPercentComplete = ConvertToDataType $(($iDsNum / $iNumOfGeneratedCounters) * 100) 'integer'
                        $sComplete = "Progress: $iPercentComplete% (Datasource $iDsNum of $iNumOfGeneratedCounters)"
                        Write-Progress -activity "Creating generated counter data..." -status $sComplete -percentcomplete $iPercentComplete -id 2;
                        $iDsNum++
                    }
            	}
            }
        }
    }
    $sComplete = "Progress: $iPercentComplete% (Analysis $iDsNum of $iNumOfGeneratedCounters)" 
    Write-Progress -activity "Creating generated counter data..." -Status $sComplete -Completed -id 2

    #// Sort htCounterInstanceStats
    $htCounterInstanceStats = $htCounterInstanceStats.GetEnumerator() | Sort-Object -Property Name
    $dtDuration = ConvertToDataType (New-TimeSpan -Start $dtStartTime -End (Get-Date)).TotalSeconds 'round3'
    Write-Host "Creating generated counter data... Done! [$dtDuration seconds]"
}

Function GenerateMSChart
{
    param($sChartTitle, $sSaveFilePath, $htOfSeriesObjects)
    
	#// GAC the Microsoft Chart Controls just in case it is not GAC'd.
	#// Requires the .NET Framework v3.5 Service Pack 1 or greater.
	$oPALPosition= New-Object System.Windows.Forms.DataVisualization.Charting.ElementPosition
	$oPALChart = New-Object System.Windows.Forms.DataVisualization.Charting.Chart
	$oPALChartArea = New-Object System.Windows.Forms.DataVisualization.Charting.ChartArea
	$fontNormal = new-object System.Drawing.Font("Tahoma",10,[Drawing.FontStyle]'Regular')

	$sFormat = "#" + $global:oPal.Session.LocalizedThousandsSeparator + "###" + $global:oPal.Session.LocalizedDecimal + "###"		
	$oPALChartArea.AxisY.LabelStyle.Format = $sFormat
	$oPALChartArea.AxisY.LabelStyle.Font = $fontNormal
   	$oPALChartArea.AxisX.LabelStyle.Angle = 90
 
    $oPALChartArea.AxisX.Interval = $global:ChartSettings.XInterval
	$oPALChart.ChartAreas["Default"] = $oPALChartArea
	
    #// Add each of the Series objects to the chart.
	ForEach ($Series in $htOfSeriesObjects)
	{
		$oPALChart.Series[$Series.Name] = $Series
	}
	
	#// Chart size
	$oChartSize = New-Object System.Drawing.Size
	$oChartSize.Width = $global:ChartSettings.Width
	$oChartSize.Height = $global:ChartSettings.Height
	$oPALChart.Size = $oChartSize
	
	#// Chart Title
    $oChartTitle = $oPALChart.Titles.Add($sChartTitle)
    $oFontSize = $oChartTitle.Font.Size
    $oFontSize = 12
    $oChartTitle.Font = New-Object System.Drawing.Font($oChartTitle.Font.Name, $oFontSize, $oChartTitle.Font.Style, $oChartTitle.Font.Unit)
	
	#// Chart Legend
	$oLegend = New-Object System.Windows.Forms.DataVisualization.Charting.Legend
    $oLegend.Docking = "Bottom"
    $oLegend.LegendStyle = "Table"
	[Void] $oPALChart.Legends.Add($oLegend)

	#// Save the chart image to a PNG file. PNG files are better quality images.
	$oPALChartImageFormat = [System.Windows.Forms.DataVisualization.Charting.ChartImageFormat]"Png"
    $sSaveFilePath
	[Void] $oPALChart.SaveImage($sSaveFilePath, $oPALChartImageFormat)	
}

Function CreatePalCharts
{
    param([System.Collections.ArrayList] $ArrayListOfCounterSeries, [System.Collections.ArrayList] $ArrayListOfThresholdSeries, [System.Int32] $MaxNumberOfItemsInChart = 10, [System.String] $OutputDirectoryPath = '', [System.String] $ImageFileName = '', [System.String] $ChartTitle = '')
    $alOfChartFilesPaths = New-Object System.Collections.ArrayList
    
    If ($ArrayListOfCounterSeries.Count -eq 0)
    {
        Return $alOfChartFilesPaths
    }
    
    [System.Int32] $a = 0
    [System.Int32] $iChartNumber = 0
    [System.String] $sFilePath = ''
    [System.Boolean] $bFileExists = $False
    Do 
    {
        $alNewChartSeries = New-Object System.Collections.ArrayList
        #// Add thresholds
        For ($t=0;$t -lt $ArrayListOfThresholdSeries.Count;$t++)
        {
            $alNewChartSeries += $ArrayListOfThresholdSeries[$t]
        }        
        #// Add counter instances
        If (($ArrayListOfCounterSeries.Count - 1 - $a) -gt $MaxNumberOfItemsInChart)
        {
            $b = 0
            Do
            {
                $alNewChartSeries += $ArrayListOfCounterSeries[$a]
                $a++
                $b++
            } until ($b -ge $MaxNumberOfItemsInChart)
        }
        Else
        {
            Do
            {
                $alNewChartSeries += $ArrayListOfCounterSeries[$a]
                $a++
            } until ($a -ge $ArrayListOfCounterSeries.Count)
        }

        #// Write chart
        $iChartNumber = 0
        Do
        {
            $sFilePath = $OutputDirectoryPath + $ImageFileName + "$iChartNumber" + '.png'
            $bFileExists = Test-Path -Path $sFilePath
            $iChartNumber++
        } until ($bFileExists -eq $False)
        $sFilePath = GenerateMSChart $ChartTitle $sFilePath $alNewChartSeries
        $alOfChartFilesPaths += $sFilePath        
    } until ($a -ge $ArrayListOfCounterSeries.Count)
    $alOfChartFilesPaths    
}

Function MakeNumeric
{
	param($Values)
	#// Make an array all numeric
    $alNewArray = New-Object System.Collections.ArrayList
    If (($Values -is [System.Collections.ArrayList]) -or ($Values -is [Array]))
    {    	
    	For ($i=0;$i -lt $Values.Count;$i++)
    	{
    		If ($(IsNumeric -Value $Values[$i]) -eq $True)
    		{
    			[Void] $alNewArray.Add([System.Double]$Values[$i])
    		}
    	}    	
    }
    Else
    {
        [Void] $alNewArray.Add([System.Double]$Values)
    }
    $alNewArray
}

Function ReduceArrayValuesToMax
{
    param($aValues,$ChartMaxLimit)

    $ChartMaxLimit = MakeNumeric -Values $ChartMaxLimit
    $iValues = MakeNumeric -Values $aValues
    For ($i=0;$i -le $iValues.GetUpperBound(0);$i++)
    {
        
        If ($iValues[$i] -gt $ChartMaxLimit)
        {
            $iValues[$i] = $ChartMaxLimit
        }
    }
    Return $iValues
}

Function ExtractSqlNamedInstanceFromCounterObjectPath
{
    param($sCounterObjectPath)
    $sSqlNamedInstance = ''
    $iLocOfSqlInstance = $sCounterObjectPath.IndexOf('$')
    If ($iLocOfSqlInstance -eq -1)
    {
        Return $sSqlNamedInstance
    }
    $iLocOfSqlInstance++
    $iLocOfColon = $sCounterObjectPath.IndexOf(':',$iLocOfSqlInstance)
    $iLenOfSqlInstance = $iLocOfColon - $iLocOfSqlInstance
    $sSqlNamedInstance = $sCounterObjectPath.SubString($iLocOfSqlInstance,$iLenOfSqlInstance)
    Return $sSqlNamedInstance
}

Function AddWarningCriticalThresholdRangesToXml
{
	param($XmlChartInstance,$WarningMinValues=$null,$WarningMaxValues=$null,$CriticalMinValues=$null,$CriticalMaxValues=$null)
	
    If (($WarningMinValues -ne $null) -or ($WarningMaxValues -ne $null))
    {
    	$oMinWarningStats = $WarningMinValues | Measure-Object -Minimum
    	$oMaxWarningStats = $WarningMaxValues | Measure-Object -Maximum
    	$XmlChartInstance.SetAttribute("MINWARNINGVALUE",$($oMinWarningStats.Minimum))
    	$XmlChartInstance.SetAttribute("MAXWARNINGVALUE",$($oMaxWarningStats.Maximum))        
    }
    
    If (($CriticalMinValues -ne $null) -or ($CriticalMaxValues -ne $null))
    {
    	$oMinCriticalStats = $CriticalMinValues | Measure-Object -Minimum
    	$oMaxCriticalStats = $CriticalMaxValues | Measure-Object -Maximum
    	$XmlChartInstance.SetAttribute("MINCRITICALVALUE",$($oMinCriticalStats.Minimum))
    	$XmlChartInstance.SetAttribute("MAXCRITICALVALUE",$($oMaxCriticalStats.Maximum))
    }
}

Function StaticChartThreshold
{
    param($CollectionOfCounterInstances,$MinThreshold,$MaxThreshold,$UseMaxValue=$True,$IsOperatorGreaterThan=$True)
    
    If ($IsOperatorGreaterThan -eq $True)
    {
        ForEach ($CounterInstanceOfCollection in $CollectionOfCounterInstances)
        {
            If (($CounterInstanceOfCollection.Max -gt $MaxThreshold) -and ($UseMaxValue -eq $True))
            {
                $MaxThreshold = $CounterInstanceOfCollection.Max
            }
        }
    }
    Else
    {
        ForEach ($CounterInstanceOfCollection in $CollectionOfCounterInstances)
        {
            If (($CounterInstanceOfCollection.Min -lt $MinThreshold) -and ($UseMaxValue -eq $True))
            {
                $MinThreshold = $CounterInstanceOfCollection.Min
            }
        }    
    }
    
    :ChartCodeLoop ForEach ($CounterInstanceOfCollection in $CollectionOfCounterInstances)
    {
        ForEach ($iValue in $CounterInstanceOfCollection.Value)
        {
            [void] $MinSeriesCollection.Add($MinThreshold)
            [void] $MaxSeriesCollection.Add($MaxThreshold)
        }
        Break ChartCodeLoop
    }
}

Function ConvertCounterToFileName
{
    param($sCounterPath)
    
    $oCtr = CounterPathToObject -sCounterPath $sCounterPath
	$sCounterObject = $oCtr.Object
	$sCounterName = $oCtr.Name
	$sResult = $sCounterObject + "_" + $sCounterName
	$sResult = $sResult -replace "/", "_"
	$sResult = $sResult -replace "%", "Percent"
	$sResult = $sResult -replace " ", "_"
	$sResult = $sResult -replace "\.", ""
	$sResult = $sResult -replace ":", "_"
	$sResult = $sResult -replace ">", "_"
	$sResult = $sResult -replace "<", "_"
	$sResult = $sResult -replace "\(", "_"
	$sResult = $sResult -replace "\)", "_"
	$sResult = $sResult -replace "\*", "x"
	$sResult = $sResult -replace "\|", "_"
    $sResult = $sResult -replace "#", "Num"
   	$sResult = $sResult -replace "\\", "_"
	$sResult = $sResult -replace "\?", ""
	$sResult = $sResult -replace "\*", ""
	$sResult = $sResult -replace "\|", "_"
	$sResult = $sResult -replace "{", ""
	$sResult = $sResult -replace "}", ""    
	Return $sResult
}

Function AddADashStyle
{
    param($Series,$DashStyleNumber)
    
    If ($DashStyleNumber -gt 3)
    {
        do 
        {$DashStyleNumber = $DashStyleNumber - 4} until ($DashStyleNumber -le 3)
    }
    
    switch ($DashStyleNumber)
    {
    	0 {$Series.BorderDashStyle = [System.Windows.Forms.DataVisualization.Charting.ChartDashStyle]"Solid"}
        1 {$Series.BorderDashStyle = [System.Windows.Forms.DataVisualization.Charting.ChartDashStyle]"Dash"}
    	2 {$Series.BorderDashStyle = [System.Windows.Forms.DataVisualization.Charting.ChartDashStyle]"DashDot"}
    	3 {$Series.BorderDashStyle = [System.Windows.Forms.DataVisualization.Charting.ChartDashStyle]"Dot"}
		default {$Series.BorderDashStyle = [System.Windows.Forms.DataVisualization.Charting.ChartDashStyle]"Solid"}
    }
	$Series
}

Function ConvertCounterArraysToSeriesHashTable
{
    param($alSeries, $aDateTimes, $htOfCounterValues, $IsThresholdsEnabled, $dWarningMin, $dWarningMax, $dCriticalMin, $dCriticalMax, $sBackGradientStyle="TopBottom")

	If ($IsThresholdsEnabled -eq $True)
	{
        If ($dWarningMax -ne $null)
        {
    		#// Add Warning Threshold values
    		$SeriesWarningThreshold = New-Object System.Windows.Forms.DataVisualization.Charting.Series
    		For ($a=0; $a -lt $aDateTimes.length; $a++)
    		{
                If ($sBackGradientStyle -eq "BottomTop")
                {
                    [Void] $SeriesWarningThreshold.Points.Add($dWarningMax[$a], $dWarningMin[$a])
                }
                Else
                {
                    [Void] $SeriesWarningThreshold.Points.Add($dWarningMin[$a], $dWarningMax[$a])
                }
    		}
    		$SeriesWarningThreshold.ChartType = [System.Windows.Forms.DataVisualization.Charting.SeriesChartType]"Range"
    		$SeriesWarningThreshold.Name = "Warning"
            If ($sBackGradientStyle -eq "BottomTop")
            {
        		$SeriesWarningThreshold.Color = [System.Drawing.Color]"Transparent"
                $SeriesWarningThreshold.BackImageTransparentColor = [System.Drawing.Color]"White"
                $SeriesWarningThreshold.BackSecondaryColor = [System.Drawing.Color]"PaleGoldenrod"        
                $SeriesWarningThreshold.BackGradientStyle = [System.Windows.Forms.DataVisualization.Charting.GradientStyle]"TopBottom"
            }
            Else
            {
                $SeriesWarningThreshold.Color = [System.Drawing.Color]"PaleGoldenrod"
                $SeriesWarningThreshold.BackGradientStyle = [System.Windows.Forms.DataVisualization.Charting.GradientStyle]"TopBottom"
            }
    		[Void] $alSeries.Add($SeriesWarningThreshold)        
        }
        
        If ($dCriticalMin -ne $null)
        {
    		#// Add Critical Threshold values
    		$SeriesCriticalThreshold = New-Object System.Windows.Forms.DataVisualization.Charting.Series
    		For ($a=0; $a -lt $aDateTimes.length; $a++)
    		{
    			[Void] $SeriesCriticalThreshold.Points.Add($dCriticalMin[$a], $dCriticalMax[$a])
    		}
    		$SeriesCriticalThreshold.ChartType = [System.Windows.Forms.DataVisualization.Charting.SeriesChartType]"Range"
    		$SeriesCriticalThreshold.Name = "Critical"
            If ($sBackGradientStyle -eq "BottomTop")
            {
        		$SeriesCriticalThreshold.Color = [System.Drawing.Color]"Transparent"
                $SeriesCriticalThreshold.BackImageTransparentColor = [System.Drawing.Color]"White"
                $SeriesCriticalThreshold.BackSecondaryColor = [System.Drawing.Color]"Tomato"        
                $SeriesCriticalThreshold.BackGradientStyle = [System.Windows.Forms.DataVisualization.Charting.GradientStyle]"TopBottom"
            }
            Else
            {
                $SeriesCriticalThreshold.Color = [System.Drawing.Color]"Tomato"
                $SeriesCriticalThreshold.BackGradientStyle = [System.Windows.Forms.DataVisualization.Charting.GradientStyle]"TopBottom"
            }        
            [Void] $alSeries.Add($SeriesCriticalThreshold)
        }
	}
	#// Sort the hast table and return an array of dictionary objects
	[System.Object[]] $aDictionariesOfCounterValues = $htOfCounterValues.GetEnumerator() | Sort-Object Name

	#// Add the counter instance values
	For ($a=0; $a -lt $aDictionariesOfCounterValues.Count; $a++)
	{
		$SeriesOfCounterValues = New-Object System.Windows.Forms.DataVisualization.Charting.Series
        $aValues = $aDictionariesOfCounterValues[$a].Value
		For ($b=0;$b -lt $aValues.Count; $b++)
		{
			If (($aDateTimes[$b] -ne $null) -and ($aValues[$b] -ne $null))
			{
                #// Skips corrupted datetime fields
                $dtDateTime = $aDateTimes[$b]
                If ($dtDateTime -isnot [datetime])
                {
                    [datetime] $dtDateTime = $dtDateTime
                }
                [Void] $SeriesOfCounterValues.Points.AddXY(($dtDateTime).tostring($global:sDateTimePattern), $aValues[$b])
			}
		}
		$SeriesOfCounterValues.ChartType = [System.Windows.Forms.DataVisualization.Charting.SeriesChartType]"Line"
		$SeriesOfCounterValues.Name = $aDictionariesOfCounterValues[$a].Name
        $SeriesOfCounterValues = AddADashStyle -Series $SeriesOfCounterValues -DashStyleNumber $a
        #// Line thickness
        $SeriesOfCounterValues.BorderWidth = $CHART_LINE_THICKNESS

		[Void] $alSeries.Add($SeriesOfCounterValues)
	}
}

Function SetXmlChartIsThresholdAddedAttribute
{
    param($XmlChart)
    [Int] $iNumOfSeries = 0
    
    ForEach ($XmlChartSeries in $XmlChart.SelectNodes("./SERIES"))
    {
        $iNumOfSeries++
    }
    
    If ($iNumOfSeries -eq 0)
    {
        $XmlChart.SetAttribute("ISTHRESHOLDSADDED", "False")
    }
    Else
    {
        $XmlChart.SetAttribute("ISTHRESHOLDSADDED", "True")
    }
}

Function GeneratePalChart
{
	param($XmlChart,$XmlAnalysisInstance)

    #//$global:htVariables = @{}
    #//$global:htCodeReplacements = @{}

    PrepareGeneratedCodeReplacements -XmlAnalysisInstance $XmlAnalysisInstance

    $alChartFilePaths = New-Object System.Collections.ArrayList
    $aDateTimes = $global:oPal.aTime
    $htCounterValues = @{}
    $alOfSeries = New-Object System.Collections.ArrayList    
    
    If ($(Test-property -InputObject $XmlChart -Name 'ISTHRESHOLDSADDED') -eq $False)
    {
        SetXmlChartIsThresholdAddedAttribute -XmlChart $XmlChart
    }

    If ($XmlChart.ISTHRESHOLDSADDED -eq "True")
    {
        $alOfChartThresholdSeries = New-Object System.Collections.ArrayList

        ForEach ($XmlChartSeries in $XmlChart.SelectNodes("./SERIES"))
        {            
            $global:MinSeriesCollection = New-Object System.Collections.ArrayList
            $global:MaxSeriesCollection = New-Object System.Collections.ArrayList
            
            $ExpressionPath = $XmlChartSeries.NAME
            $Name = $XmlChartSeries.NAME

        	ForEach ($XmlCode in $XmlChartSeries.SelectNodes("./CODE"))
        	{
                $sCode = $XmlCode.get_innertext()
                #// Replace all of the variables with their hash table version.
                ForEach ($sKey in $htCodeReplacements.Keys)
                {
                    $sCode = $sCode -Replace $sKey,$htCodeReplacements[$sKey]
                }

                #// Execute the code
                ExecuteCodeForGeneratedDataSource -Code $sCode -Name $Name -ExpressionPath $ExpressionPath -htVariables $global:htVariables -htQuestionVariables $global:oPal.QuestionVariables       
                Break #// Only execute one block of code, so breaking out.
        	}

        	$oSeriesData = New-Object pscustomobject
        	Add-Member -InputObject $oSeriesData -MemberType NoteProperty -Name Name -Value $XmlChartSeries.NAME
            Add-Member -InputObject $oSeriesData -MemberType NoteProperty -Name MinValues -Value $MinSeriesCollection
            Add-Member -InputObject $oSeriesData -MemberType NoteProperty -Name MaxValues -Value $MaxSeriesCollection
            [void] $alOfChartThresholdSeries.Add($oSeriesData)        
        }
    
        $IsWarningThresholds = $False
        $IsCriticalThresholds = $False
        ForEach ($oChartThresholdSeriesInstance in $alOfChartThresholdSeries)
        {
            If ($oChartThresholdSeriesInstance.Name -eq "Warning")
            {
                $IsWarningThresholds = $True
                $MinWarningThresholdValues = $oChartThresholdSeriesInstance.MinValues
                $MaxWarningThresholdValues = $oChartThresholdSeriesInstance.MaxValues
            }
            If ($oChartThresholdSeriesInstance.Name -eq "Critical")
            {
                $IsCriticalThresholds = $True
                $MinCriticalThresholdValues = $oChartThresholdSeriesInstance.MinValues
                $MaxCriticalThresholdValues = $oChartThresholdSeriesInstance.MaxValues
            }
        }
		
        If (($IsCriticalThresholds -eq $True) -and ($IsWarningThresholds -eq $True))
        {
            AddWarningCriticalThresholdRangesToXml -XmlChartInstance $XmlChart -WarningMinValues $MinWarningThresholdValues -WarningMaxValues $MaxWarningThresholdValues -CriticalMinValues $MinCriticalThresholdValues -CriticalMaxValues $MaxCriticalThresholdValues
        }
        Else
        {
            If ($IsCriticalThresholds -eq $True)
            {
                AddWarningCriticalThresholdRangesToXml -XmlChartInstance $XmlChart -CriticalMinValues $MinCriticalThresholdValues -CriticalMaxValues $MaxCriticalThresholdValues
            }
            Else
            {
                AddWarningCriticalThresholdRangesToXml -XmlChartInstance $XmlChart -WarningMinValues $MinWarningThresholdValues -WarningMaxValues $MaxWarningThresholdValues
            }
        }		
		
        #// Populate $htCounterValues
        ForEach ($XmlCounterDataSource in $XmlAnalysisInstance.SelectNodes("./DATASOURCE"))
        {
            If ($XmlChart.DATASOURCE -eq $XmlCounterDataSource.EXPRESSIONPATH)
            {
                ForEach ($XmlDataSourceCounterInstance in $XmlCounterDataSource.SelectNodes("./COUNTERINSTANCE"))
                {
                    If ($(Test-XmlBoolAttribute -InputObject $XmlDataSourceCounterInstance -Name 'ISALLNULL') -eq $True)
                    {
                        $IsAllNull = $True
                    }
                    Else
                    {
                        $IsAllNull = $False
                    }
                    
                    If ($IsAllNull -eq $False)
                    {
                        If ($(Test-property -InputObject $XmlChart -Name 'MAXLIMIT') -eq $True)
                        {
                            $aValues = ReduceArrayValuesToMax -aValues $($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].Value) -ChartMaxLimit $($XmlChart.MAXLIMIT)
                        }
                        Else
                        {
                            $aValues = $global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].Value
                        }

                        #// Check if this is a named instance of SQL Server
                        If (($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterObject.Contains('MSSQL$') -eq $True) -or ($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterObject.Contains('MSOLAP$') -eq $True))
                        {
                            $sSqlNamedInstance = ExtractSqlNamedInstanceFromCounterObjectPath -sCounterObjectPath $global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterObject
                            If ($XmlDataSourceCounterInstance.COUNTERINSTANCE -eq '')
                            {
        						$CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance
                            }
                            Else
                            {
                                $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance + "/" + $XmlDataSourceCounterInstance.COUNTERINSTANCE
                            }
                        }
                        Else
                        {
                            If ($XmlDataSourceCounterInstance.COUNTERINSTANCE -eq '')
                            {
        						$CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER
                            }
                            Else
                            {
                                $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $XmlDataSourceCounterInstance.COUNTERINSTANCE
                            }
                        }
                        [void] $htCounterValues.Add($CounterLabel,$aValues)
                    }
                }
            }
        }
        If ($htCounterValues.Count -gt 0)
        {
            If ($(Test-property -InputObject $XmlChart -Name 'BACKGRADIENTSTYLE') -eq $True)
            {
                If (($IsCriticalThresholds -eq $True) -and ($IsWarningThresholds -eq $True))
                {
                    ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $MinWarningThresholdValues $MaxWarningThresholdValues $MinCriticalThresholdValues $MaxCriticalThresholdValues $XmlChart.BACKGRADIENTSTYLE
                }
                Else
                {
                    If ($IsCriticalThresholds -eq $True)
                    {
                        ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $null $null $MinCriticalThresholdValues $MaxCriticalThresholdValues $XmlChart.BACKGRADIENTSTYLE
                    }
                    Else
                    {
                        ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $MinWarningThresholdValues $MaxWarningThresholdValues $null $null $XmlChart.BACKGRADIENTSTYLE
                    }
                }        
                
            }
            Else
            {
                If (($IsCriticalThresholds -eq $True) -and ($IsWarningThresholds -eq $True))
                {
                    ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $MinWarningThresholdValues $MaxWarningThresholdValues $MinCriticalThresholdValues $MaxCriticalThresholdValues
                }
                Else
                {
                    If ($IsCriticalThresholds -eq $True)
                    {
                        ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $null $null $MinCriticalThresholdValues $MaxCriticalThresholdValues
                    }
                    Else
                    {
                        ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $true $MinWarningThresholdValues $MaxWarningThresholdValues $null $null
                    }
                }
            }
        }
        Else
        {
            Write-Warning "`t[GeneratePalChart] No data to chart."
        }        
    }
    Else
    {
        #// Populate $htCounterValues
        ForEach ($XmlCounterDataSource in $XmlAnalysisInstance.SelectNodes("./DATASOURCE"))
        {
            If ($XmlChart.DATASOURCE -eq $XmlCounterDataSource.EXPRESSIONPATH)
            {
                ForEach ($XmlDataSourceCounterInstance in $XmlCounterDataSource.SelectNodes("./COUNTERINSTANCE"))
                {
                    If ($(Test-property -InputObject $XmlChart -Name 'MAXLIMIT') -eq $True)
                    {
                        $aValues = ReduceArrayValuesToMax -aValues $($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].Value) -ChartMaxLimit $($XmlChart.MAXLIMIT)
                    }
                    Else
                    {
                        $aValues = $global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].Value
                    }
                    
                    #// Check if this is a named instance of SQL Server
                    If (($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterObject.Contains('MSSQL$') -eq $True) -or ($global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterObject.Contains('MSOLAP$') -eq $True))
                    {
                        $sSqlNamedInstance = ExtractSqlNamedInstanceFromCounterObjectPath -sCounterObjectPath $global:htCounterInstanceStats[$XmlDataSourceCounterInstance.NAME].CounterPath
                        If ($XmlDataSourceCounterInstance.COUNTERINSTANCE -eq '')
                        {
                            $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance
                        }
                        Else
                        {
                            $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance + "/" + $XmlDataSourceCounterInstance.COUNTERINSTANCE
                        }
                    }
                    Else
                    {
                        If ($XmlDataSourceCounterInstance.COUNTERINSTANCE -eq '')
                        {
                            $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER
                        }
                        Else
                        {
                            $CounterLabel = $XmlDataSourceCounterInstance.COUNTERCOMPUTER + "/" + $XmlDataSourceCounterInstance.COUNTERINSTANCE
                        }
                    }
                    [void] $htCounterValues.Add($CounterLabel,$aValues)
                }
            }   
        }
        If ($htCounterValues.Count -gt 0)
        {
            ConvertCounterArraysToSeriesHashTable $alOfSeries $aDateTimes $htCounterValues $False
        }
        Else
        {
            Write-Warning "`t[GeneratePalChart] No data to chart."
        }
    }    
    
    #// If there are too many counter instances in a data source for one chart, then need to do multiple charts.
    $ImageFileName = ConvertCounterToFileName -sCounterPath $XmlChart.DATASOURCE
    $OutputDirectoryPath = $global:oPal.Session.ResourceDirectoryPath
	$sChartTitle = $XmlChart.CHARTTITLE
	$MaxNumberOfItemsInChart = $CHART_MAX_INSTANCES
    
    $alThresholdsOfSeries = New-Object System.Collections.ArrayList
    $alNonThresholdsOfSeries = New-Object System.Collections.ArrayList
    For ($t=0;$t -lt $alOfSeries.Count;$t++)
    {
        If (($($alOfSeries[$t].Name.Contains('Warning')) -eq $True) -or ($($alOfSeries[$t].Name.Contains('Critical')) -eq $True))
        {
            $alThresholdsOfSeries += $alOfSeries[$t]
        }
        Else
        {
            $alNonThresholdsOfSeries += $alOfSeries[$t]
        }        
    }
    
    #// Put _Total and _Global_ instances in their own chart series
    $alTotalInstancesSeries = New-Object System.Collections.ArrayList
    $alAllOthersOfSeries = New-Object System.Collections.ArrayList
    For ($t=0;$t -lt $alNonThresholdsOfSeries.Count;$t++)
    {
        If (($alNonThresholdsOfSeries[$t].Name.Contains('_Total') -eq $True) -or ($alNonThresholdsOfSeries[$t].Name.Contains('_Global_') -eq $True))
        {
            $alTotalInstancesSeries += $alNonThresholdsOfSeries[$t]
        }
        Else
        {
            $alAllOthersOfSeries += $alNonThresholdsOfSeries[$t]
        }
    }

    #// Chart all of the _Total instances
    If ($alTotalInstancesSeries.Count -gt 0)
    {
        $alFilesPaths = CreatePalCharts -ArrayListOfCounterSeries $alTotalInstancesSeries -ArrayListOfThresholdSeries $alThresholdsOfSeries -MaxNumberOfItemsInChart $MaxNumberOfItemsInChart -OutputDirectoryPath $OutputDirectoryPath -ImageFileName $ImageFileName -ChartTitle $sChartTitle
        $alFilesPaths | ForEach-Object {[void] $alChartFilePaths.Add($_)}
    }
    
    #// Chart all non-_Total instances
    If ($alAllOthersOfSeries.Count -gt 0)
    {
        $alFilesPaths = CreatePalCharts -ArrayListOfCounterSeries $alAllOthersOfSeries -ArrayListOfThresholdSeries $alThresholdsOfSeries -MaxNumberOfItemsInChart $MaxNumberOfItemsInChart -OutputDirectoryPath $OutputDirectoryPath -ImageFileName $ImageFileName -ChartTitle $sChartTitle
        $alFilesPaths | ForEach-Object {[void] $alChartFilePaths.Add($_)}
    }   
    $alChartFilePaths
}

Function ConvertToRelativeFilePaths
{
    param($RootPath,$TargetPath)
    $Result = $TargetPath.Replace($RootPath,'')
    $Result
}

Function GenerateCharts
{
    Write-Host "Generating Charts..."
    $alOfChartFilePaths = New-Object System.Collections.ArrayList
    $alTempFilePaths = New-Object System.Collections.ArrayList

    ForEach ($XmlAnalysis in @($global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS')))
    {
        If (($(Test-XmlBoolAttribute -InputObject $XmlAnalysis -Name 'ENABLED') -eq $True) -and ($(Test-XmlBoolAttribute -InputObject $XmlAnalysis -Name 'AllCountersFound') -eq $True))
        {
            Write-Host `t($XmlAnalysis.NAME)
            $global:OverallActiveAnalysis = $XmlAnalysis.NAME
            ForEach ($XmlChart in $XmlAnalysis.SelectNodes("./CHART"))
            {
                $alTempFilePaths = GeneratePalChart -XmlChart $XmlChart -XmlAnalysisInstance $XmlAnalysis
                [System.Object[]] $alTempFilePaths = @($alTempFilePaths | Where-Object {$_ -ne $null})
                For ($i=0;$i -lt $alTempFilePaths.Count;$i++)
                {
                    $alTempFilePaths[$i] = ConvertToRelativeFilePaths -RootPath $global:oPal.ArgsProcessed.OutputDir -TargetPath $alTempFilePaths[$i]
                }
                        
                If ($alTempFilePaths -ne $null) #// Added by Andy from Codeplex.com
                {
                    $result = [string]::Join(',',$alTempFilePaths)
                    $XmlChart.SetAttribute("FILEPATHS", $result)
                }
            }
        }
    }
}

Function AddToVariablesAndCodeReplacements
{
    param([string] $sCollectionVarName, $sCounterInstancePath)

    #// Add to htVariables
    If ($global:htVariables.ContainsKey($sCollectionVarName) -eq $False)
    {
        [void] $global:htVariables.Add($sCollectionVarName, $(New-Object System.Collections.ArrayList))
    }

    
    If ($global:htCounterInstanceStats.ContainsKey($sCounterInstancePath) -eq $True)
    {
        $oCounterInstance = $global:htCounterInstanceStats[$sCounterInstancePath]
        $IsFound = $False
        :VariableSearchLoop ForEach ($oCounterPath in $global:htVariables[$sCollectionVarName])
        {
            ForEach ($oCounter in $oCounterPath)
            {
                If ($oCounter.CounterPath -eq $oCounterInstance.CounterPath)
                {
                    $IsFound = $True
                    Break VariableSearchLoop;
                }
            }
        }
        If ($IsFound -eq $False)
        {
            [void] $global:htVariables[$sCollectionVarName].Add($oCounterInstance)
        }
    }

    #// Add to htCodeReplacements
    $sCollectionNameWithBackslash = "\`$$sCollectionVarName"
    $sCollectionNameWithDoubleQuotes = "`"$sCollectionVarName`""
    $sCollectionVarName = "`$htVariables[$sCollectionNameWithDoubleQuotes]"

    $IsKeyExist = $False
    $IsKeyExist = $htCodeReplacements.ContainsKey($sCollectionNameWithBackslash)
    If ($IsKeyExist -eq $False)
    {
        [void] $global:htCodeReplacements.Add($sCollectionNameWithBackslash,$sCollectionVarName)
    }

}

Function PrepareEnvironmentForThresholdProcessing
{
    param($CurrentAnalysisInstance)
    
    If ($global:oPal.QuantizedIndex -eq $null)
    {
        If (($global:oPal.aTime -eq $null) -or ($global:oPal.aTime -eq ''))
        {
            $global:oPal.aTime = GetTimeDataFromPerfmonLog
        }        
        $global:oPal.QuantizedIndex = GenerateQuantizedIndexArray -ArrayOfTimes $global:oPal.aTime -AnalysisIntervalInSeconds $global:oPal.ArgsProcessed.AnalysisInterval
    }
    
    If ($global:oPal.QuantizedTime -eq $null)
    {
        $global:oPal.QuantizedTime = GenerateQuantizedTimeArray -ArrayOfTimes $global:oPal.aTime -QuantizedIndexArray $global:oPal.QuantizedIndex
    }
    
    #// Create an internal overall counter stat for each counter instance for each counter stat.
    ForEach ($XmlDataSource in $CurrentAnalysisInstance.SelectNodes('./DATASOURCE'))
    {
        ForEach ($XmlCounterInstance in $XmlDataSource.SelectNodes('./COUNTERINSTANCE'))
        {
            If ($(Test-XmlBoolAttribute -InputObject $XmlCounterInstance -Name 'ISINTERNALONLY') -eq $True)
            {
                $IsInternalOnly = $True
            }
            Else
            {
                $IsInternalOnly = $False
            }

            If ($IsInternalOnly -eq $False)
            {
                $XmlNewCounterInstance = $global:oXml.XmlRoot.CreateElement("COUNTERINSTANCE")
                $InternalCounterInstanceName = 'INTERNAL_OVERALL_COUNTER_STATS_' + $($XmlCounterInstance.NAME)
                $XmlNewCounterInstance.SetAttribute("NAME", $InternalCounterInstanceName)
                $XmlNewCounterInstance.SetAttribute("MIN", $($XmlCounterInstance.MIN))
                $XmlNewCounterInstance.SetAttribute("AVG", $($XmlCounterInstance.AVG))
                $XmlNewCounterInstance.SetAttribute("MAX", $($XmlCounterInstance.MAX))
                $XmlNewCounterInstance.SetAttribute("TREND", $($XmlCounterInstance.TREND))
                $XmlNewCounterInstance.SetAttribute("STDDEV", $($XmlCounterInstance.STDDEV))
                $XmlNewCounterInstance.SetAttribute("PERCENTILESEVENTYTH", $($XmlCounterInstance.PERCENTILESEVENTYTH))
                $XmlNewCounterInstance.SetAttribute("PERCENTILEEIGHTYTH", $($XmlCounterInstance.PERCENTILEEIGHTYTH))
                $XmlNewCounterInstance.SetAttribute("PERCENTILENINETYTH", $($XmlCounterInstance.PERCENTILENINETYTH))
                $XmlNewCounterInstance.SetAttribute("QUANTIZEDMIN", $([string]::Join(',',$XmlCounterInstance.QUANTIZEDMIN)))
                $XmlNewCounterInstance.SetAttribute("QUANTIZEDAVG", $([string]::Join(',',$XmlCounterInstance.QUANTIZEDAVG)))
                $XmlNewCounterInstance.SetAttribute("QUANTIZEDMAX", $([string]::Join(',',$XmlCounterInstance.QUANTIZEDMAX)))
                $XmlNewCounterInstance.SetAttribute("QUANTIZEDTREND", $([string]::Join(',',$XmlCounterInstance.QUANTIZEDTREND)))
                $XmlNewCounterInstance.SetAttribute("COUNTERPATH", $($XmlCounterInstance.COUNTERPATH))
                $XmlNewCounterInstance.SetAttribute("COUNTERCOMPUTER", $($XmlCounterInstance.COUNTERCOMPUTER))
                $XmlNewCounterInstance.SetAttribute("COUNTEROBJECT", $($XmlCounterInstance.COUNTEROBJECT))
                $XmlNewCounterInstance.SetAttribute("COUNTERNAME", $($XmlCounterInstance.COUNTERNAME))

                If ($(Test-property -InputObject $XmlCounterInstance -Name 'ISALLNULL') -eq $True)
                {
                    $XmlNewCounterInstance.SetAttribute("ISALLNULL", 'True')
                }
                Else
                {
                    $XmlNewCounterInstance.SetAttribute("ISALLNULL", 'False')
                }                    
                $XmlNewCounterInstance.SetAttribute("ISINTERNALONLY", 'True')
                $XmlNewCounterInstance.SetAttribute("ORIGINALNAME", $($XmlCounterInstance.NAME))
                [void] $XmlDataSource.AppendChild($XmlNewCounterInstance)
                $oCounter = $global:htCounterInstanceStats[$XmlCounterInstance.NAME]
                AddToCounterInstanceStatsArrayList $InternalCounterInstanceName $oCounter.Time $oCounter.Value $oCounter.QuantizedTime $XmlCounterInstance.QUANTIZEDMIN $XmlCounterInstance.QUANTIZEDAVG $XmlCounterInstance.QUANTIZEDMAX $XmlCounterInstance.QUANTIZEDTREND $oCounter.CounterComputer $oCounter.CounterObject $oCounter.CounterName $oCounter.CounterInstance $oCounter.Min $oCounter.Avg $oCounter.Max $oCounter.Trend $oCounter.StdDev $oCounter.PercentileSeventyth $oCounter.PercentileEightyth $oCounter.PercentileNinetyth
                
                $sCollectionVarName = $XmlDataSource.COLLECTIONVARNAME
                AddToVariablesAndCodeReplacements -sCollectionVarName $sCollectionVarName -sCounterInstancePath $InternalCounterInstanceName
            }
        }
    }
}

Function GetQuantizedTimeSliceTimeRange
{
    param($TimeSliceIndex)

    $u = $global:oPal.QuantizedTime.Count - 1
    If ($TimeSliceIndex -ge $u)
    {
    	$LastTimeSlice = $global:oPal.QuantizedTime[$u]
    	[datetime] $EndTime = $global:oPal.QuantizedTime[$u].AddSeconds($global:oPal.ArgsProcessed.AnalysisInterval)
        $Date1 = Get-Date $([datetime] $global:oPal.QuantizedTime[$u]) -format $global:sDateTimePattern
        $Date2 = Get-Date $([datetime]$EndTime) -format $global:sDateTimePattern
        [string] $ResultTimeRange = "$Date1" + ' - ' + "$Date2"
    }
    Else
    {
        $Date1 = Get-Date $([datetime]$global:oPal.QuantizedTime[$TimeSliceIndex]) -format $global:sDateTimePattern
        $Date2 = Get-Date $([datetime]$global:oPal.QuantizedTime[$TimeSliceIndex+1]) -format $global:sDateTimePattern
        [string] $ResultTimeRange = "$Date1" + ' - ' + "$Date2"
    }
    $ResultTimeRange
}

Function CreateAlert
{
    param($TimeSliceIndex,$CounterInstanceObject,$IsMinThresholdBroken=$False,$IsAvgThresholdBroken=$False,$IsMaxThresholdBroken=$False,$IsTrendThresholdBroken=$False,$IsMinEvaluated=$False,$IsAvgEvaluated=$False,$IsMaxEvaluated=$False,$IsTrendEvaluated=$False)
    #// The following are provided via global variables to make it simple for users to use.
    #$global:CurrentXmlAnalysisInstance = $XmlAnalysisInstance
    #$global:ThresholdName = $XmlThreshold.NAME
    #$global:ThresholdCondition = $XmlThreshold.CONDITION
    #$global:ThresholdColor = $XmlThreshold.COLOR
    #$global:ThresholdPriority = $XmlThreshold.PRIORITY
    #$global:ThresholdAnalysisID = $XmlAnalysisInstance.ID
    #$global:IsMinEvaluated = $False
    #$global:IsAvgEvaluated = $False
    #$global:IsMaxEvaluated = $False
    #$global:IsTrendEvaluated = $False    
    
    [string] $sCounterInstanceName = $CounterInstanceObject.CounterPath
    If ($($sCounterInstanceName.Contains('INTERNAL_OVERALL_COUNTER_STATS')) -eq $True)
    {
        $IsInternalOnly = $True
    }
    Else
    {
        $IsInternalOnly = $False
    }
    
    $IsSameCounterAlertFound = $False
    :XmlAlertLoop ForEach ($XmlAlert in $CurrentXmlAnalysisInstance.SelectNodes('./ALERT'))
    {
        If (($XmlAlert.TIMESLICEINDEX -eq $TimeSliceIndex) -and ($XmlAlert.COUNTER -eq $CounterInstanceObject.CounterPath))
        {
            #// Update alert
            If ($([int] $ThresholdPriority) -ge $([int] $XmlAlert.CONDITIONPRIORITY))
            {
                $XmlAlert.CONDITIONCOLOR = $ThresholdColor
                $XmlAlert.CONDITION = $ThresholdCondition
                $XmlAlert.CONDITIONNAME = $ThresholdName
                $XmlAlert.CONDITIONPRIORITY = $ThresholdPriority
            }

            If ($IsMinThresholdBroken -eq $True)
            {
                If ($([int] $ThresholdPriority) -ge $([int] $XmlAlert.MINPRIORITY))
                {
                    $XmlAlert.MINCOLOR = $ThresholdColor
                    $XmlAlert.MINPRIORITY = $ThresholdPriority
                    #// $XmlAlert.MINEVALUATED = 'True'
                }
            }
            
            If ($IsAvgThresholdBroken -eq $True)
            {
                If ($([int] $ThresholdPriority) -ge $([int] $XmlAlert.AVGPRIORITY))
                {
                    $XmlAlert.AVGCOLOR = $ThresholdColor
                    $XmlAlert.AVGPRIORITY = $ThresholdPriority
                    #// $XmlAlert.AVGEVALUATED = 'True'
                }
            }
            
            If ($IsMaxThresholdBroken -eq $True)
            {
                If ($([int] $ThresholdPriority) -ge $([int] $XmlAlert.MAXPRIORITY))
                {
                    $XmlAlert.MAXCOLOR = $ThresholdColor
                    $XmlAlert.MAXPRIORITY = $ThresholdPriority
                    #// $XmlAlert.MAXEVALUATED = 'True'
                }
            }
            
            If ($IsTrendThresholdBroken -eq $True)
            {
                If ($([int] $ThresholdPriority) -ge $([int] $XmlAlert.TRENDPRIORITY))
                {
                    $XmlAlert.TRENDCOLOR = $ThresholdColor
                    $XmlAlert.TRENDPRIORITY = $ThresholdPriority
                    #// $XmlAlert.TRENDEVALUATED = 'True'
                }
            }
            $IsSameCounterAlertFound = $True
            Break XmlAlertLoop
        }
    }
    
    If ($IsSameCounterAlertFound -eq $False)
    {        
        #// Add the alert
        
        $XmlNewAlert = $global:oXml.XmlRoot.CreateElement("ALERT")
        $XmlNewAlert.SetAttribute("TIMESLICEINDEX", $TimeSliceIndex)
        $XmlNewAlert.SetAttribute("TIMESLICERANGE", $(GetQuantizedTimeSliceTimeRange -TimeSliceIndex $TimeSliceIndex))
        $XmlNewAlert.SetAttribute("CONDITIONCOLOR", $ThresholdColor)
        $XmlNewAlert.SetAttribute("CONDITION", $ThresholdCondition)
        $XmlNewAlert.SetAttribute("CONDITIONNAME", $ThresholdName)
        $XmlNewAlert.SetAttribute("CONDITIONPRIORITY", $ThresholdPriority)
        $XmlNewAlert.SetAttribute("COUNTER", $CounterInstanceObject.CounterPath)
        $XmlNewAlert.SetAttribute("PARENTANALYSIS", $($CurrentXmlAnalysisInstance.NAME))
        $XmlNewAlert.SetAttribute("ISINTERNALONLY", $IsInternalOnly)
        
        If ($IsMinThresholdBroken -eq $True)
        {
            $XmlNewAlert.SetAttribute("MINCOLOR", $ThresholdColor)
            $XmlNewAlert.SetAttribute("MINPRIORITY", $ThresholdPriority)
            #// $XmlNewAlert.SetAttribute("MINEVALUATED", 'True')
        }
        Else
        {
            If ($IsMinEvaluated -eq $True)
            {
                #// 00FF00 is a light green
                #$XmlNewAlert.SetAttribute("MINCOLOR", '#00FF00')
                $XmlNewAlert.SetAttribute("MINCOLOR", 'White')
                $XmlNewAlert.SetAttribute("MINPRIORITY", '0')
                #// $XmlNewAlert.SetAttribute("MINEVALUATED", 'True')
            }
            Else
            {
                $XmlNewAlert.SetAttribute("MINCOLOR", 'White')
                $XmlNewAlert.SetAttribute("MINPRIORITY", '0')
            }
        }
        
        If ($IsAvgThresholdBroken -eq $True)
        {
            $XmlNewAlert.SetAttribute("AVGCOLOR", $ThresholdColor)
            $XmlNewAlert.SetAttribute("AVGPRIORITY", $ThresholdPriority)
            #// $XmlNewAlert.SetAttribute("AVGEVALUATED", 'True')
        }
        Else
        {
            If ($IsAvgEvaluated -eq $True)
            {
                #// 00FF00 is a light green
                #$XmlNewAlert.SetAttribute("AVGCOLOR", '#00FF00')
                $XmlNewAlert.SetAttribute("AVGCOLOR", 'White')
                $XmlNewAlert.SetAttribute("AVGPRIORITY", '0')
                #// $XmlNewAlert.SetAttribute("AVGEVALUATED", 'True')
            }
            Else
            {
                $XmlNewAlert.SetAttribute("AVGCOLOR", 'White')
                $XmlNewAlert.SetAttribute("AVGPRIORITY", '0')
            }
        }
        
        If ($IsMaxThresholdBroken -eq $True)
        {
            $XmlNewAlert.SetAttribute("MAXCOLOR", $ThresholdColor)
            $XmlNewAlert.SetAttribute("MAXPRIORITY", $ThresholdPriority)
            #// $XmlNewAlert.SetAttribute("MAXEVALUATED", 'True')
        }
        Else
        {
            If ($IsMaxEvaluated -eq $True)
            {
                #// 00FF00 is a light green
                #$XmlNewAlert.SetAttribute("MAXCOLOR", '#00FF00')
                $XmlNewAlert.SetAttribute("MAXCOLOR", 'White')
                $XmlNewAlert.SetAttribute("MAXPRIORITY", '0')
                #// $XmlNewAlert.SetAttribute("MAXEVALUATED", 'True')
            }
            Else
            {
                $XmlNewAlert.SetAttribute("MAXCOLOR", 'White')
                $XmlNewAlert.SetAttribute("MAXPRIORITY", '0')
            }
        }
        
        If ($IsTrendThresholdBroken -eq $True)
        {
            $XmlNewAlert.SetAttribute("TRENDCOLOR", $ThresholdColor)
            $XmlNewAlert.SetAttribute("TRENDPRIORITY", $ThresholdPriority)
            #// $XmlNewAlert.SetAttribute("TRENDEVALUATED", 'True')
        }
        Else
        {
            If ($IsTrendEvaluated -eq $True)
            {
                #// 00FF00 is a light green
                #$XmlNewAlert.SetAttribute("TRENDCOLOR", '#00FF00')
                $XmlNewAlert.SetAttribute("TRENDCOLOR", 'White')
                $XmlNewAlert.SetAttribute("TRENDPRIORITY", '0')
                #// $XmlNewAlert.SetAttribute("TRENDEVALUATED", 'True')
            }
            Else
            {
                $XmlNewAlert.SetAttribute("TRENDCOLOR", 'White')
                $XmlNewAlert.SetAttribute("TRENDPRIORITY", '0')
            }
        }
        $XmlNewAlert.SetAttribute("MIN", $($CounterInstanceObject.QuantizedMin[$TimeSliceIndex]))
        $XmlNewAlert.SetAttribute("AVG", $($CounterInstanceObject.QuantizedAvg[$TimeSliceIndex]))
        $XmlNewAlert.SetAttribute("MAX", $($CounterInstanceObject.QuantizedMax[$TimeSliceIndex]))
        $XmlNewAlert.SetAttribute("TREND", $($CounterInstanceObject.QuantizedTrend[$TimeSliceIndex]))
        [void] $CurrentXmlAnalysisInstance.AppendChild($XmlNewAlert)
    }
}

Function OverallStaticThreshold
{
    param($oCounterInstance,$Operator,$Threshold,$IsTrendOnly=$False)

    $IsMinThresholdBroken = $False
    $IsAvgThresholdBroken = $False
    $IsMaxThresholdBroken = $False
    $IsTrendThresholdBroken = $False
    $IsMinEvaluated = $False
    $IsAvgEvaluated = $False
    $IsMaxEvaluated = $False
    $IsTrendEvaluated = $False
            
    If ($IsTrendOnly -eq $False)
    {
        #/////////////////////////
        #// IsMinThresholdBroken
        #/////////////////////////
        If (($oCounterInstance.Min -ne '-') -and ($oCounterInstance.Min -ne $null))
        {
            If ($oCounterInstance.Min -is [System.Char])
            {
                [System.Int32] $iMin = $oCounterInstance.Min
                [System.Double] $iMin = $iMin
            }
            Else
            {
                [System.Double] $iMin = $oCounterInstance.Min
            }

    		switch ($Operator)
            {
                'gt'
                {
                    If ([System.Double] $iMin -gt [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }
                }
                'ge'
                {
                    If ([System.Double] $iMin -ge [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }
                }
                'lt'
                {
                    If ([System.Double] $iMin -lt [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }
                }
                'le'
                {
                    If ([System.Double] $iMin -le [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }
                }
                'eq'
                {
                    If ([System.Double] $iMin -eq [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }
                }
                default
                {
                    If ([System.Double] $iMin -gt [System.Double] $Threshold)
                    {
                        $IsMinThresholdBroken = $True
                    }                    
                }
            }
        }
        #/////////////////////////
        #// IsAvgThresholdBroken
        #/////////////////////////
        If (($oCounterInstance.Avg -ne '-') -and ($oCounterInstance.Avg -ne $null))
        {
            If ($oCounterInstance.Avg -is [System.Char])
            {
                [System.Int32] $iAvg = $oCounterInstance.Avg
                [System.Double] $iAvg = $iAvg
            }
            Else
            {
                [System.Double] $iAvg = $oCounterInstance.Avg
            }

    		switch ($Operator)
            {
                'gt'
                {
                    If ([System.Double] $iAvg -gt [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }
                }
                'ge'
                {
                    If ([System.Double] $iAvg -ge [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }
                }
                'lt'
                {
                    If ([System.Double] $iAvg -lt [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }
                }
                'le'
                {
                    If ([System.Double] $iAvg -le [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }
                }
                'eq'
                {
                    If ([System.Double] $iAvg -eq [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }
                }
                default
                {
                    If ([System.Double] $iAvg -gt [System.Double] $Threshold)
                    {
                        $IsAvgThresholdBroken = $True
                    }                    
                }
            }
        }            
        #/////////////////////////
        #// IsMaxThresholdBroken
        #/////////////////////////
        If (($oCounterInstance.Max -ne '-') -and ($oCounterInstance.Max -ne $null))
        {
            If ($oCounterInstance.Max -is [System.Char])
            {
                [System.Int32] $iMax = $oCounterInstance.Max
                [System.Double] $iMax = $iMax
            }
            Else
            {
                [System.Double] $iMax = $oCounterInstance.Max
            }

    		switch ($Operator)
            {
                'gt'
                {
                    If ([System.Double] $iMax -gt [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }
                }
                'ge'
                {
                    If ([System.Double] $iMax -ge [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }
                }
                'lt'
                {
                    If ([System.Double] $iMax -lt [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }
                }
                'le'
                {
                    If ([System.Double] $iMax -le [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }
                }
                'eq'
                {
                    If ([System.Double] $iMax -eq [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }
                }
                default
                {
                    If ([System.Double] $iMax -gt [System.Double] $Threshold)
                    {
                        $IsMaxThresholdBroken = $True
                    }                    
                }
            }
        }
    }
    Else
    {
        #/////////////////////////
        #// IsTrendThresholdBroken
        #/////////////////////////

                
        If ($oCounterInstance.Trend -is [System.String])
        {
            [System.Double] $iTrend = $oCounterInstance.Trend
        }
        Else
        {
            [System.Double] $iTrend = $oCounterInstance.Trend
        }
                
        If (($iTrend -ne '-') -and ($iTrend -ne $null))
        {
    		switch ($Operator)
            {
                'gt'
                {
                    If ([System.Double] $iTrend -gt [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }
                }
                'ge'
                {
                    If ([System.Double] $iTrend -ge [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }
                }
                'lt'
                {
                    If ([System.Double] $iTrend -lt [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }
                }
                'le'
                {
                    If ([System.Double] $iTrend -le [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }
                }
                'eq'
                {
                    If ([System.Double] $iTrend -eq [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }
                }
                default
                {
                    If ([System.Double] $iTrend -gt [System.Double] $Threshold)
                    {
                        $IsTrendThresholdBroken = $True
                    }                    
                }
            }
        }
    }
    If (($IsMinThresholdBroken -eq $True) -or ($IsAvgThresholdBroken -eq $True) -or ($IsMaxThresholdBroken -eq $True) -or ($IsTrendThresholdBroken -eq $True))
    {
        CreateAlert -TimeSliceIndex 0 -CounterInstanceObject $oCounterInstance -IsMinThresholdBroken $IsMinThresholdBroken -IsAvgThresholdBroken $IsAvgThresholdBroken -IsMaxThresholdBroken $IsMaxThresholdBroken -IsTrendThresholdBroken $IsTrendThresholdBroken -IsMinEvaluated $IsMinEvaluated -IsAvgEvaluated $IsAvgEvaluated -IsMaxEvaluated $IsMaxEvaluated -IsTrendEvaluated $IsTrendEvaluated
    }
}

Function StaticThreshold
{
    param($CollectionOfCounterInstances,$Operator,$Threshold,$IsTrendOnly=$False)
    
    For ($i=0;$i -lt $CollectionOfCounterInstances.Count;$i++)
    {
        $oCounterInstance = $CollectionOfCounterInstances[$i]

        If ($oCounterInstance.Name.Contains('INTERNAL_OVERALL_COUNTER_STATS_') -eq $True)
        {
            OverallStaticThreshold -oCounterInstance $oCounterInstance -Operator $Operator -Threshold $Threshold -IsTrendOnly $IsTrendOnly
        }
        Else
        {
            For ($t=0;$t -lt $alQuantizedTime.Count;$t++)
            {
                $IsMinThresholdBroken = $False
                $IsAvgThresholdBroken = $False
                $IsMaxThresholdBroken = $False
                $IsTrendThresholdBroken = $False
                $IsMinEvaluated = $False
                $IsAvgEvaluated = $False
                $IsMaxEvaluated = $False
                $IsTrendEvaluated = $False
            
                If ($IsTrendOnly -eq $False)
                {
                    #/////////////////////////
                    #// IsMinThresholdBroken
                    #/////////////////////////
                    If (($oCounterInstance.QuantizedMin[$t] -ne '-') -and ($oCounterInstance.QuantizedMin[$t] -ne $null))
                    {
                        If ($oCounterInstance.QuantizedMin[$t] -is [System.Char])
                        {
                            [System.Int32] $iQuantizedMin = $oCounterInstance.QuantizedMin[$t]
                            [System.Double] $iQuantizedMin = $iQuantizedMin
                        }
                        Else
                        {
                            [System.Double] $iQuantizedMin = $oCounterInstance.QuantizedMin[$t]
                        }

    				    switch ($Operator)
                        {
                            'gt'
                            {
                                If ([System.Double] $iQuantizedMin -gt [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }
                    	    }
                            'ge'
                            {
                                If ([System.Double] $iQuantizedMin -ge [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }
                    	    }
                    	    'lt'
                            {
                                If ([System.Double] $iQuantizedMin -lt [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }
                    	    }
                            'le'
                            {
                                If ([System.Double] $iQuantizedMin -le [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }
                    	    }
                    	    'eq'
                            {
                                If ([System.Double] $iQuantizedMin -eq [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }
                    	    }
                    	    default
                            {
                                If ([System.Double] $iQuantizedMin -gt [System.Double] $Threshold)
                                {
                                    $IsMinThresholdBroken = $True
                                }                    
                    	    }
                        }
                    }
                    #/////////////////////////
                    #// IsAvgThresholdBroken
                    #/////////////////////////
                    If (($oCounterInstance.QuantizedAvg[$t] -ne '-') -and ($oCounterInstance.QuantizedAvg[$t] -ne $null))
                    {
                        If ($oCounterInstance.QuantizedAvg[$t] -is [System.Char])
                        {
                            [System.Int32] $iQuantizedAvg = $oCounterInstance.QuantizedAvg[$t]
                            [System.Double] $iQuantizedAvg = $iQuantizedAvg
                        }
                        Else
                        {
                            [System.Double] $iQuantizedAvg = $oCounterInstance.QuantizedAvg[$t]
                        }

    				    switch ($Operator)
                        {
                            'gt'
                            {
                                If ([System.Double] $iQuantizedAvg -gt [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }
                    	    }
                            'ge'
                            {
                                If ([System.Double] $iQuantizedAvg -ge [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }
                    	    }
                    	    'lt'
                            {
                                If ([System.Double] $iQuantizedAvg -lt [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }
                    	    }
                            'le'
                            {
                                If ([System.Double] $iQuantizedAvg -le [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }
                    	    }
                    	    'eq'
                            {
                                If ([System.Double] $iQuantizedAvg -eq [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }
                    	    }
                    	    default
                            {
                                If ([System.Double] $iQuantizedAvg -gt [System.Double] $Threshold)
                                {
                                    $IsAvgThresholdBroken = $True
                                }                    
                    	    }
                        }
                    }            
                    #/////////////////////////
                    #// IsMaxThresholdBroken
                    #/////////////////////////
                    If (($oCounterInstance.QuantizedMax[$t] -ne '-') -and ($oCounterInstance.QuantizedMax[$t] -ne $null))
                    {
                        If ($oCounterInstance.QuantizedMax[$t] -is [System.Char])
                        {
                            [System.Int32] $iQuantizedMax = $oCounterInstance.QuantizedMax[$t]
                            [System.Double] $iQuantizedMax = $iQuantizedMax
                        }
                        Else
                        {
                            [System.Double] $iQuantizedMax = $oCounterInstance.QuantizedMax[$t]
                        }

    				    switch ($Operator)
                        {
                            'gt'
                            {
                                If ([System.Double] $iQuantizedMax -gt [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }
                    	    }
                            'ge'
                            {
                                If ([System.Double] $iQuantizedMax -ge [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }
                    	    }
                    	    'lt'
                            {
                                If ([System.Double] $iQuantizedMax -lt [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }
                    	    }
                            'le'
                            {
                                If ([System.Double] $iQuantizedMax -le [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }
                    	    }
                    	    'eq'
                            {
                                If ([System.Double] $iQuantizedMax -eq [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }
                    	    }
                    	    default
                            {
                                If ([System.Double] $iQuantizedMax -gt [System.Double] $Threshold)
                                {
                                    $IsMaxThresholdBroken = $True
                                }                    
                    	    }
                        }
                    }
                }
                Else
                {
                    #/////////////////////////
                    #// IsTrendThresholdBroken
                    #/////////////////////////

                
                    If ($oCounterInstance.QuantizedTrend -is [System.String])
                    {
                        [System.Double[]] $aQuantizedTrend = @($oCounterInstance.QuantizedTrend.Split(','))
                    }
                    Else
                    {
                        [System.Double[]] $aQuantizedTrend = @($oCounterInstance.QuantizedTrend)
                    }

                    $iQuantizedTrend = $aQuantizedTrend[$t]
                
                    If (($iQuantizedTrend -ne '-') -and ($iQuantizedTrend -ne $null))
                    {
 

    				    switch ($Operator)
                        {
                            'gt'
                            {
                                If ([System.Double] $iQuantizedTrend -gt [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }
                    	    }
                            'ge'
                            {
                                If ([System.Double] $iQuantizedTrend -ge [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }
                    	    }
                    	    'lt'
                            {
                                If ([System.Double] $iQuantizedTrend -lt [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }
                    	    }
                            'le'
                            {
                                If ([System.Double] $iQuantizedTrend -le [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }
                    	    }
                    	    'eq'
                            {
                                If ([System.Double] $iQuantizedTrend -eq [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }
                    	    }
                    	    default
                            {
                                If ([System.Double] $iQuantizedTrend -gt [System.Double] $Threshold)
                                {
                                    $IsTrendThresholdBroken = $True
                                }                    
                    	    }
                        }
                    }
                }
                If (($IsMinThresholdBroken -eq $True) -or ($IsAvgThresholdBroken -eq $True) -or ($IsMaxThresholdBroken -eq $True) -or ($IsTrendThresholdBroken -eq $True))
                {
                    CreateAlert -TimeSliceIndex $t -CounterInstanceObject $oCounterInstance -IsMinThresholdBroken $IsMinThresholdBroken -IsAvgThresholdBroken $IsAvgThresholdBroken -IsMaxThresholdBroken $IsMaxThresholdBroken -IsTrendThresholdBroken $IsTrendThresholdBroken -IsMinEvaluated $IsMinEvaluated -IsAvgEvaluated $IsAvgEvaluated -IsMaxEvaluated $IsMaxEvaluated -IsTrendEvaluated $IsTrendEvaluated
                }
            }
        }
    }
}

Function ExecuteCodeForThreshold
{
    param($Code,$Name,$htVariables,$htQuestionVariables)
    $global:IsMinThresholdBroken = $False
    $global:IsAvgThresholdBroken = $False
    $global:IsMaxThresholdBroken = $False
    $global:IsTrendThresholdBroken = $False
    $global:IsMinEvaluated = $False
    $global:IsAvgEvaluated = $False
    $global:IsMaxEvaluated = $False
    $global:IsTrendEvaluated = $False
    #'Code after changes:' >> CodeDebug.txt
    #'===================' >> CodeDebug.txt
    #$sCode >> CodeDebug.txt
    If ($sCode -is [System.String])
    {
        Invoke-Expression -Command $sCode
    }
}

Function ProcessThreshold
{
    param($XmlAnalysisInstance,$XmlThreshold)
    
    $global:CurrentXmlAnalysisInstance = $XmlAnalysisInstance
    $global:ThresholdName = $XmlThreshold.NAME
    $global:ThresholdCondition = $XmlThreshold.CONDITION
    $global:ThresholdColor = $XmlThreshold.COLOR
    $global:ThresholdPriority = $XmlThreshold.PRIORITY
    If ($(Test-property -InputObject $XmlAnalysisInstance -Name 'ID') -eq $True)
    {
        $global:ThresholdAnalysisID = $XmlAnalysisInstance.ID
    }
    Else
    {
        $global:ThresholdAnalysisID = Get-GUID
    }
    
    ForEach ($XmlCode in $XmlThreshold.SelectNodes("./CODE"))
    {
		$sCode = $XmlCode.get_innertext()
		#'Code before changes:' >> CodeDebug.txt
		#'====================' >> CodeDebug.txt
		#$sCode >> CodeDebug.txt            
		#// Replace all of the variables with their hash table version.
        
		ForEach ($sKey in $global:htCodeReplacements.Keys)
		{
			$sCode = $sCode -Replace $sKey,$global:htCodeReplacements[$sKey]
		}
        
		#// Execute the code
		ExecuteCodeForThreshold -Code $sCode -Name $ThresholdName -htVariables $global:htVariables -htQuestionVariables $global:oPal.QuestionVariables        
		Break #// Only execute one block of code, so breaking out.
    }
}

Function ProcessThresholds
{
    Write-Host 'Processing Thresholds...'

    ForEach ($XmlAnalysis in @($global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS')))
    {
        If (($(Test-XmlBoolAttribute -InputObject $XmlAnalysis -Name 'ENABLED') -eq $True) -and ($(Test-XmlBoolAttribute -InputObject $XmlAnalysis -Name 'AllCountersFound') -eq $True))
        {
            Write-Host `t($XmlAnalysis.NAME)
            $global:OverallActiveAnalysis = $XmlAnalysis.NAME
            PrepareEnvironmentForThresholdProcessing -CurrentAnalysisInstance $XmlAnalysis
            ForEach ($XmlThreshold in $XmlAnalysis.SelectNodes("./THRESHOLD"))
            {
                ProcessThreshold -XmlAnalysisInstance $XmlAnalysis -XmlThreshold $XmlThreshold
            }
        }
    }
}

Function GetCategoryList
{
    ForEach ($XmlAnalysisNode in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        If ($(Test-XmlBoolAttribute -InputObject $XmlAnalysisNode -Name 'ENABLED') -eq $True)
        {
            $IsEnabled = $True
        }
        Else
        {
            $IsEnabled = $False
        }    

        If ($IsEnabled -eq $True)
        {
            If ($(Test-property -InputObject $XmlAnalysisNode -Name 'CATEGORY') -eq $True) 
            {
                If ($alCategoryList.Contains($XmlAnalysisNode.CATEGORY) -eq $False)
                {
                    [void] $alCategoryList.Add($XmlAnalysisNode.CATEGORY)
                }            
            }
        }
    }
}

Function PrepareDataForReport
{
    $global:alCategoryList = New-Object System.Collections.ArrayList
    GetCategoryList
    $global:alCategoryList.Sort()
}

Function SeparateWarningCritical
{
    param([Int] $Warnings = 0,[Int] $Criticals = 0)
    [String] $sResult = ''
    If (($Warnings -gt 0) -and ($Criticals -gt 0))
    {
        $sResult = '(Alerts: <FONT COLOR="Red"><B>' + $Criticals + '</B></FONT>|<FONT COLOR="#FF6600"><B>' + $Warnings + '</B></FONT>)'
    }
    Else
    {
        If ($Criticals -gt 0)
        {
            $sResult = '(Alerts: <FONT COLOR="Red"><B>' + $Criticals + '</B></FONT>| ' + $Warnings + ')'
        }
        Else
        {
            If ($Warnings -gt 0)
            {
                $sResult = '(Alerts: ' + $Criticals + '|<FONT COLOR="#FF6600"><B>' + $Warnings + '</B></FONT>)'
            }
            Else
            {
                $sResult = '(Alerts: ' + $Criticals + '|' + $Warnings + ')'
            }
        }
    }
    Return $sResult
}

Function ConvertStringForHref($str)
{
	$RetVal = $str
    $RetVal = $RetVal.Replace('/','')
	$RetVal = $RetVal.Replace('%','Percent')
	$RetVal = $RetVal.Replace(' ','')
	$RetVal = $RetVal.Replace('.','')
	$RetVal = $RetVal.Replace('(','')
	$RetVal = $RetVal.Replace(')','')
	$RetVal = $RetVal.Replace('*','All')
	$RetVal = $RetVal.Replace('\','')
    $RetVal = $RetVal.Replace(':','')
    $RetVal = $RetVal.Replace('-','')
	#// Remove first char if it is an underscore.
	$FirstChar = $RetVal.SubString(0,1)
	If ($FirstChar -eq '_')
	{		
		$RetVal = $RetVal.SubString(1)
	}
	#// Remove last char if it is an underscore.
	$iLenMinusOne = $RetVal.Length - 1
	$LastChar = $RetVal.SubString($iLenMinusOne)
	If ($LastChar -eq '_')
	{
		$RetVal = $RetVal.SubString(0,$iLenMinusOne)
	}
    $RetVal
}

Function IsThresholdsInAnalysis
{
    param($XmlAnalysis)
    ForEach ($XmlNode in $XmlAnalysis.SelectNodes('./THRESHOLD'))
    {
        Return $True
    }
    $False    
}

Function GetLogTimeRange
{	
	$u = $global:oPal.aTime.GetUpperBound(0)
    
    $Date1 = Get-Date $([datetime]$global:oPal.aTime[0]) -format $global:sDateTimePattern
    $Date2 = Get-Date $([datetime]$global:oPal.aTime[$u]) -format $global:sDateTimePattern
    [string] $ResulTimeRange = "$Date1" + ' - ' + "$Date2"
    $ResulTimeRange
}

Function ConvertAnalysisIntervalIntoHumanReadableTime
{
    [string] $sInterval = ''

    $TimeInSeconds = $global:oPal.ArgsProcessed.AnalysisInterval
    
	$Global:BeginTime = Get-Date
	$Global:EndTime = $Global:BeginTime.AddSeconds($TimeInSeconds)
	$DateDifference = New-TimeSpan -Start ([DateTime]$Global:BeginTime) -End ([DateTime]$Global:EndTime)
	If ($DateDifference.Seconds -gt 0)
	{
		$sInterval = "$($DateDifference.Seconds) second(s)"
	}
	If ($DateDifference.Minutes -gt 0)
	{
		$sInterval = "$($DateDifference.Minutes) Minute(s) $sInterval"
	}
	If ($DateDifference.Hours -gt 0)
	{
		$sInterval = "$($DateDifference.Hours) Hours(s) $sInterval"
	}
	If ($DateDifference.Days -gt 0)
	{
		$sInterval = "$($DateDifference.Days) Days(s) $sInterval"
	}
    $sInterval
}

Function AddThousandsSeparator
{
    param($Value)
    If ($Value -eq '-')
    {Return $Value}
    [double] $Value = $Value
	If ($Value -eq 0)
	{ 0 }
	Else
	{ $Value.ToString("#,#.########") }
}

Function Add-WhiteFont
{
    param([string] $Text,[string] $Color)
    $Color = $Color.ToLower()
    If (($Color -eq 'red') -or ($Color -eq '#FF0000'))
    {
        Return '<FONT COLOR="#FFFFFF">' + $Text + '</FONT>'
    }
    Else
    {
        Return $Text
    }
}

Function GenerateHtml
{
    param()
    If ($IsOutputHtml -eq $False)
    {
        Return
    }
    Write-Host 'Generating the HTML Report...' -NoNewline

    $iPercentComplete = ConvertToDataType $((0 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Header]' -status $sComplete -percentcomplete $iPercentComplete -id 2;
    
    $iNumberOfAnalyses = 0
    $iTableCounter = 0
    ForEach ($XmlAnalysisNode in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
    {
        $iNumberOfAnalyses++
    }
    
    $h = $global:oPal.ArgsProcessed.HtmlOutputFileName
    
    #///////////////////////
    #// Header
    #///////////////////////    
    '<HTML>' > $h
    '<HEAD>' >> $h
    '<STYLE TYPE="text/css" TITLE="currentStyle" MEDIA="screen">' >> $h
    'body {' >> $h
    '   font: normal 8pt/16pt Verdana;' >> $h
    '   color: #000000;' >> $h
    '   margin: 10px;' >> $h
    '   }' >> $h
    'p {font: 8pt/16pt Verdana;margin-top: 0px;}' >> $h
    'h1 {font: 20pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'h2 {font: 15pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'h3 {font: 13pt Verdana;margin-bottom: 0px;color: #000000;}' >> $h
    'td {font: normal 8pt Verdana;}' >> $h
    'th {font: bold 8pt Verdana;}' >> $h
    'blockquote {font: normal 8pt Verdana;}' >> $h
    '</STYLE>' >> $h
    '</HEAD>' >> $h
    '<BODY LINK="Black" VLINK="Black">' >> $h
    '<TABLE CELLPADDING=10 WIDTH="100%"><TR><TD BGCOLOR="#000000">' >> $h
    '<FONT COLOR="#FFFFFF" FACE="Tahoma" SIZE="5"><STRONG>Analysis of "' + $(GetLogNameFromLogParameter) + '"</STRONG></FONT><BR><BR>' >> $h
    ## Updated to format with globalised date time JonnyG 2010-06-11
    '<FONT COLOR="#FFFFFF" FACE="Tahoma" SIZE="2"><STRONG>Report Generated at: ' + "$((get-date).tostring($global:sDateTimePattern))" + '</STRONG></FONT>' >> $h
    '</TD><TD><A HREF="http://github.com/clinthuffman/PAL"><FONT COLOR="#000000" FACE="Tahoma" SIZE="10">PAL</FONT><FONT COLOR="#000000" FACE="Tahoma" SIZE="5">v2</FONT></A></FONT>' >> $h
    '</TD></TR></TABLE>' >> $h
    '<BR>' >> $h

    $iPercentComplete = ConvertToDataType $((1 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Table of Contents]' -status $sComplete -percentcomplete $iPercentComplete -id 2;

    #///////////////////////
    #// Table of Contents
    #///////////////////////
    '<H4>On This Page</H4>' >> $h
    '<UL>' >> $h
        '<LI><A HREF="#ToolParameters">Tool Parameters</A></LI>' >> $h
        '<LI><A HREF="#AlertsbyChronologicalOrder">Alerts by Chronological Order</A></LI>' >> $h
        '<UL>' >> $h
        If ($alQuantizedTime -eq $null)
        {
            Write-Error 'None of the counters in the counter log match up to the threshold file. The counter log is either missing counters or is corrupted. Try opening this counter log in Performance Monitor to confirm the counters. Collect another counter log using the counters defined in the threshold file. Consider using the Export to Perfmon log template feature to collect the proper counters.'
            break;
        }
        For ($t=0;$t -lt $alQuantizedTime.Count;$t++)
        {
            $TimeRange = GetQuantizedTimeSliceTimeRange -TimeSliceIndex $t
            $HrefLink = "TimeRange_" + "$(ConvertStringForHref $TimeRange)"
            $NumOfAlerts = 0
            $NumOfCriticalAlerts = 0
            $NumOfWarningAlerts = 0
            
            ForEach ($XmlAlert in $global:oXml.XmlAnalyses.SelectNodes('//ALERT'))
            {
                If (($XmlAlert.TIMESLICEINDEX -eq $t) -and ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False))
                {
                    If ($(Test-property -InputObject $XmlAlert -Name 'CONDITION') -eq $True)
                    {
                        Switch ($XmlAlert.CONDITION)
                        {
                            "Warning" {$NumOfWarningAlerts++}
                            "Critical" {$NumOfCriticalAlerts++}
                        }
                    }
                    $NumOfAlerts++
                }
            }
            $sAlertsText = SeparateWarningCritical -Warnings $NumOfWarningAlerts -Criticals $NumOfCriticalAlerts
            '<LI><A HREF="#' + $HrefLink + '">' + $TimeRange + ' ' + $sAlertsText + '</A></LI>' >> $h
        }
        '</UL>' >> $h
        ForEach ($Category in $global:alCategoryList)
        {
            $HrefLink = ConvertStringForHref $Category
            $HtmlCategory = '<LI><A HREF="#' + $HrefLink + '">' + "$Category" + '</A></LI>'
            $bHasHtmlCategoryBeenWritten = $False
            $IsCategoryEmpty = $True
            ForEach ($XmlAnalysisNode in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
            {
                If ($(ConvertTextTrueFalse $XmlAnalysisNode.ENABLED) -eq $True)
                {
                    $bThresholdsInAnalysis = $False
                    $bThresholdsInAnalysis = IsThresholdsInAnalysis -XmlAnalysis $XmlAnalysisNode
                    If ($(Test-property -InputObject $XmlAnalysisNode -Name 'AllCountersFound') -eq $True)
                    {
                        $IsAllCountersFound = ConvertTextTrueFalse $XmlAnalysisNode.AllCountersFound
                    }
                    Else
                    {
                        $IsAllCountersFound = $False
                    }
                    
                    $AnalysisCategory = $XmlAnalysisNode.CATEGORY
                    
                    If ($bThresholdsInAnalysis -eq $True)
                    {
                        If ($IsAllCountersFound -eq $True)
                        {
                            #// Count the number of alerts in each of the analyses for the TOC.
                            $NumOfAlerts = 0
                            $NumOfWarningAlerts = 0
                            $NumOfCriticalAlerts = 0
                            ForEach ($XmlAlert in $XmlAnalysisNode.SelectNodes('./ALERT'))
                            {
                                If ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False)
                                {
                                    If ($(Test-property -InputObject $XmlAlert -Name 'CONDITION') -eq $True)
                                    {
                                        Switch ($XmlAlert.CONDITION)
                                        {
                                            "Warning" {$NumOfWarningAlerts++}
                                            "Critical" {$NumOfCriticalAlerts++}
                                        }
                                    }
                                    $NumOfAlerts++
                                }
                            }
                            $sAlertsText = SeparateWarningCritical -Warnings $NumOfWarningAlerts -Criticals $NumOfCriticalAlerts
                            If ($AnalysisCategory.ToLower() -eq $Category.ToLower())                    
                            {
                                If ($bHasHtmlCategoryBeenWritten -eq $False)
                                {
                                    $HtmlCategory >> $h
                                    '<UL>' >> $h
                                    $bHasHtmlCategoryBeenWritten = $True
                                    $IsCategoryEmpty = $False
                                }
                                $HrefLink = ConvertStringForHref $XmlAnalysisNode.NAME

                                '<LI><A HREF="#' + $HrefLink + '">' + $XmlAnalysisNode.NAME + ' ' + $sAlertsText + '</A></LI>' >> $h
                            }
                        }
                    }
                    Else
                    {
                        If ($IsAllCountersFound -eq $True)
                        {
                            If ($AnalysisCategory.ToLower() -eq $Category.ToLower())
                            {
                                If ($bHasHtmlCategoryBeenWritten -eq $False)
                                {
                                    $HtmlCategory >> $h
                                    '<UL>' >> $h
                                    $bHasHtmlCategoryBeenWritten = $True
                                    $IsCategoryEmpty = $False
                                }
                                $HrefLink = ConvertStringForHref $XmlAnalysisNode.NAME
                                '<LI><A HREF="#' + $HrefLink + '">' + $XmlAnalysisNode.NAME + ' (Stats only)</A></LI>' >> $h
                            }
                        }
                    }
                }
            }
            If ($IsCategoryEmpty -eq $False)
            {
                '</UL>' >> $h
            }
        }
        '<LI><A HREF="#IndexOfJobs">Incomplete analyses</A></LI>' >> $h
        '<LI><A HREF="#Disclaimer">Disclaimer</A></LI>' >> $h
    '</UL>' >> $h
    '<BR>' >> $h
    '<A HREF="#top">Back to the top</A><BR>' >> $h

    $iPercentComplete = ConvertToDataType $((2 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Tool Parameters]' -status $sComplete -percentcomplete $iPercentComplete -id 2;

    #///////////////////////
    #// Tool Parameters
    #///////////////////////
    '<TABLE BORDER=0 WIDTH=50%>' >> $h
    '<TR><TD>' >> $h
    '<H1><A NAME="ToolParameters">Tool Parameters:</A></H1>' >> $h
    '<HR>' >> $h
    '</TD></TR>' >> $h
    '</TABLE>' >> $h
    '<TABLE BORDER=0 CELLPADDING=5>' >> $h
    '<TR><TH WIDTH=300 BGCOLOR="#000000"><FONT COLOR="#FFFFFF">Name</FONT></TH><TH BGCOLOR="#000000"><FONT COLOR="#FFFFFF">Value</FONT></TH></TR>' >> $h
    '<TR><TD WIDTH=300><B>Log Time Range: </B></TD><TD>' + $(GetLogTimeRange) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>Log(s): </B></TD><TD>' + $Log + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>AnalysisInterval: </B></TD><TD>' + $(ConvertAnalysisIntervalIntoHumanReadableTime) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>Threshold File: </B></TD><TD>' + $($ThresholdFile) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>AllCounterStats: </B></TD><TD>' + $($AllCounterStats) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>NumberOfThreads: </B></TD><TD>' + $($NumberOfThreads) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>IsLowPriority: </B></TD><TD>' + $($IsLowPriority) + '</TD></TR>' >> $h
    '<TR><TD WIDTH=300><B>DisplayReport: </B></TD><TD>' + $($DisplayReport) + '</TD></TR>' >> $h
    $dEndTime = Get-Date
	$Global:dDurationTime = New-TimeSpan -Start $global:ScriptExecutionBeginTime -End $dEndTime
	"`nScript Execution Duration: " + $Global:dDurationTime + "`n"	
    '<TR><TD WIDTH=300><B>Script Execution Duration: </B></TD><TD>' + $($Global:dDurationTime) + '</TD></TR>' >> $h
    
    ForEach ($sKey in $global:oPal.QuestionVariables.Keys)
    {
        '<TR><TD WIDTH=300><B>' + $sKey + ':</B></TD><TD>' + $($global:oPal.QuestionVariables[$sKey]) + '</TD></TR>' >> $h
    }
    '</TABLE>' >> $h
    '<BR>' >> $h
    '<A HREF="#top">Back to the top</A><BR>' >> $h

    $iPercentComplete = ConvertToDataType $((3 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Alerts in Chronological Order]' -status $sComplete -percentcomplete $iPercentComplete -id 2;
    
    #///////////////////////
    #// Alerts in Chronological Order
    #///////////////////////
    '<TABLE BORDER=0 WIDTH=50%>' >> $h
    '<TR><TD>' >> $h
    '<H1><A NAME="AlertsbyChronologicalOrder">Alerts by Chronological Order</A></H1>' >> $h
    '<HR>' >> $h
    '</TD></TR>' >> $h
    '</TABLE>' >> $h
    '<BLOCKQUOTE><B>Description: </B> This section displays all of the alerts in chronological order.</BLOCKQUOTE>' >> $h
    '<BR>' >> $h
    '<CENTER>' >> $h
    '<H3>Alerts</H3>' >> $h
    '<TABLE BORDER=0 WIDTH=60%><TR><TD>' >> $h
    'An alert is generated if any of the thresholds were broken during one of the time ranges analyzed. The background of each of the values represents the highest priority threshold that the value broke. See each of the counter' + "'" + 's respective analysis section for more details about what the threshold means.' >> $h
    '</TD></TR></TABLE>' >> $h
    '<BR>' >> $h
    $IsAlerts = $False
    :IsAlerts ForEach ($XmlAlert in $global:oXml.XmlAnalyses.SelectNodes('//ALERT'))
    {
        $IsAlerts = $True
        break IsAlerts
    }
    
    If ($IsAlerts -eq $False)
    {
        '<TABLE BORDER=1 CELLPADDING=5>' >> $h
        '<TR><TH>No Alerts Found</TH></TR>' >> $h
        '</TABLE>' >> $h
    }
    Else
    {
        '<TABLE BORDER=1 CELLPADDING=2>' >> $h
        '<TR><TH>Time Range</TH><TH></TH><TH></TH><TH></TH><TH></TH><TH></TH><TH></TH></TR>' >> $h
        For ($t=0;$t -lt $alQuantizedTime.Count;$t++)
        {
            $IsAnyAlertsInQuantizedTimeSlice = $False
            $TimeRange = GetQuantizedTimeSliceTimeRange -TimeSliceIndex $t
            $HrefLink = "TimeRange_" + "$(ConvertStringForHref $TimeRange)"
            :AlertInQuantizedTimeSliceLoopCheck ForEach ($XmlAlert in $global:oXml.XmlAnalyses.SelectNodes('//ALERT'))
            {
                If (($XmlAlert.TIMESLICEINDEX -eq $t) -and ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False))
                {
                    $IsAnyAlertsInQuantizedTimeSlice = $True
                    Break AlertInQuantizedTimeSliceLoopCheck
                }
            }            
            
            If ($IsAnyAlertsInQuantizedTimeSlice -eq $True)
            {
                '<TR><TH><A NAME="' + $HrefLink + '">' + $TimeRange + '</A></TH><TH>Condition</TH><TH>Counter</TH><TH>Min</TH><TH>Avg</TH><TH>Max</TH><TH>Hourly Trend</TH></TR>' >> $h
                ForEach ($XmlAlert in $global:oXml.XmlAnalyses.SelectNodes('//ALERT'))
                {
                    $HrefLink = ConvertStringForHref $XmlAlert.PARENTANALYSIS
                    If (($XmlAlert.TIMESLICEINDEX -eq $t) -and ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False))
                    {
                        [string] $sPart00 = '<TR><TD></TD><TD BGCOLOR="' + $($XmlAlert.CONDITIONCOLOR) + '"><A HREF="#' + $HrefLink + '">'
                        [string] $sPart01 = Add-WhiteFont -Text $($XmlAlert.CONDITIONNAME) -Color $($XmlAlert.CONDITIONCOLOR)
                        [string] $sPart02 = '</A></TD><TD>' + $($XmlAlert.COUNTER) + '</TD><TD BGCOLOR="' + $($XmlAlert.MINCOLOR) + '">'
                        [string] $sPart03 = Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.MIN) -Color $($XmlAlert.MINCOLOR)
                        [string] $sPart04 = '</TD><TD BGCOLOR="' + $($XmlAlert.AVGCOLOR) + '">'
                        [string] $sPart05 = Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.AVG) -Color $($XmlAlert.AVGCOLOR)
                        [string] $sPart06 = '</TD><TD BGCOLOR="' + $($XmlAlert.MAXCOLOR) + '">'
                        [string] $sPart07 = Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.MAX) -Color $($XmlAlert.MAXCOLOR)
                        [string] $sPart08 = '</TD><TD BGCOLOR="' + $($XmlAlert.TRENDCOLOR) + '">'
                        [string] $sPart09 = Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.TREND) -Color $($XmlAlert.TRENDCOLOR)
                        [string] $sPart10 = '</TD></TR>'
                        $sPart00 + $sPart01 + $sPart02 + $sPart03 + $sPart04 + $sPart05 + $sPart06 + $sPart07 + $sPart08 + $sPart09 + $sPart10 >> $h
                    }
                }
            }
        }
        '</TABLE>' >> $h
    }
    '</CENTER>' >> $h
    
    $iPercentComplete = ConvertToDataType $((4 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Analyses]' -status $sComplete -percentcomplete $iPercentComplete -id 2;

    ForEach ($Category in $global:alCategoryList)
    {
        #///////////////////////
        #// Category
        #///////////////////////
        #'<A HREF="#top">Back to the top</A><BR>' >> $h
        $HrefLink = ConvertStringForHref $Category
        $bHasHtmlCategoryBeenWritten = $False
        $HtmlCategoryHeader = '<TABLE BORDER=0 WIDTH=50%><TR><TD>' + '<H1><A NAME="' + $HrefLink + '">' + "$Category" + '</A></H1><HR></TD></TR></TABLE>'
        ForEach ($XmlAnalysisNode in $global:oXml.XmlAnalyses.SelectNodes('//ANALYSIS'))
        {
            #///////////////////////
            #// Analysis
            #///////////////////////            
            If ($(ConvertTextTrueFalse $XmlAnalysisNode.ENABLED) -eq $True)
            {
                If ($(Test-property -InputObject $XmlAnalysisNode -Name 'AllCountersFound') -eq $True)
                {
                    $IsAllCountersFound = ConvertTextTrueFalse $XmlAnalysisNode.AllCountersFound
                }
                Else
                {
                    $IsAllCountersFound = $False
                }
                
                $AnalysisCategory = $XmlAnalysisNode.CATEGORY
                If ($IsAllCountersFound -eq $True)
                {
                    If ($AnalysisCategory.ToLower() -eq $Category.ToLower())
                    {
                        If ($bHasHtmlCategoryBeenWritten -eq $False)
                        {
                            $HtmlCategoryHeader >> $h
                            $bHasHtmlCategoryBeenWritten = $True
                        }
                        $HrefLink = ConvertStringForHref $XmlAnalysisNode.NAME
                        '<H2><A NAME="' + $HrefLink + '">' + $XmlAnalysisNode.NAME + '</A></H2>' >> $h
                        #/////////////////
                        #// Description
                        #/////////////////
                        $IsAllCountersFound = ConvertTextTrueFalse $XmlAnalysisNode.AllCountersFound
                        If (($(Test-property -InputObject $XmlAnalysisNode -Name 'DESCRIPTION') -eq $True) -and ($IsAllCountersFound -eq $True))
                        {
                            $sDescription = $XmlAnalysisNode.DESCRIPTION.get_innertext()
                            '<BLOCKQUOTE><B>Description:</B> ' + $sDescription + '</BLOCKQUOTE>' >> $h
                            '<BR>' >> $h                        
                        }                        
                        
                        #///////////////////////
                        #// Chart
                        #///////////////////////
                        ForEach ($XmlChart in $XmlAnalysisNode.SelectNodes('./CHART'))
                        {
                            If ($(Test-property -InputObject $XmlChart -Name 'FILEPATHS') -eq $True)
                            {
                                If ($XmlChart.FILEPATHS -ne $null) #// Added by Andy from Codeplex.com
                                {
                                    $aFilePaths = $XmlChart.FILEPATHS.Split(',')

                                    If ($(Test-property -InputObject $XmlChart -Name 'ISTHRESHOLDSADDED') -eq $False)
                                    {
                                        SetXmlChartIsThresholdAddedAttribute -XmlChart $XmlChart
                                    }                                    
                                    
        							If ($(ConvertTextTrueFalse $XmlChart.ISTHRESHOLDSADDED) -eq $True)
        							{
                                        If (($(Test-property -InputObject $XmlChart -Name 'MINWARNINGVALUE') -eq $True) -and ($(Test-property -InputObject $XmlChart -Name 'MAXWARNINGVALUE') -eq $True))
                                        {
                                            $IsWarningValuesExist = $True
                                        }
                                        Else
                                        {
                                            $IsWarningValuesExist = $False
                                        }
                                        If (($(Test-property -InputObject $XmlChart -Name 'MINCRITICALVALUE') -eq $True) -and ($(Test-property -InputObject $XmlChart -Name 'MAXCRITICALVALUE') -eq $True))
                                        {
                                            $IsCriticalValuesExist = $True
                                        }
                                        Else
                                        {
                                            $IsCriticalValuesExist = $False
                                        }
                                        If (($IsWarningValuesExist -eq $True) -and ($IsCriticalValuesExist -eq $True))
                                        {
        								    $sAltText = "$($XmlChart.CHARTTITLE)`n" + $(If ($($XmlChart.MINWARNINGVALUE) -ne ''){'Warning Range: ' + "$(AddThousandsSeparator -Value $XmlChart.MINWARNINGVALUE)" + ' to ' + "$(AddThousandsSeparator -Value $XmlChart.MAXWARNINGVALUE)`n"}) + $(If ($($XmlChart.MINCRITICALVALUE) -ne ''){'Critical Range: ' + "$(AddThousandsSeparator -Value $XmlChart.MINCRITICALVALUE)" + ' to ' + "$(AddThousandsSeparator -Value $XmlChart.MAXCRITICALVALUE)"})
                                        }
                                        Else
                                        {
                                            If ($IsWarningValuesExist -eq $True)
                                            {
                                                $sAltText = "$($XmlChart.CHARTTITLE)`n" + $(If ($($XmlChart.MINWARNINGVALUE) -ne ''){'Warning Range: ' + "$(AddThousandsSeparator -Value $XmlChart.MINWARNINGVALUE)" + ' to ' + "$(AddThousandsSeparator -Value $XmlChart.MAXWARNINGVALUE)"})
                                            }
                                            Else
                                            {
                                                $sAltText = "$($XmlChart.CHARTTITLE)`n" + $(If ($($XmlChart.MINCRITICALVALUE) -ne ''){'Critical Range: ' + "$(AddThousandsSeparator -Value $XmlChart.MINCRITICALVALUE)" + ' to ' + "$(AddThousandsSeparator -Value $XmlChart.MAXCRITICALVALUE)"})
                                            }
                                        }
        							}
        							Else
        							{
        								$sAltText = "$($XmlChart.CHARTTITLE)"
        							}
                                    ForEach ($sFilePath in $aFilePaths)
                                    {
                                        '<CENTER><IMG SRC=' + '"' + "$sFilePath" + '"' + ' ALT="' + $sAltText + '"></CENTER><BR>' >> $h
                                    }
                                }
                                Else
                                {
                                    '<CENTER><table border=1 cellpadding=10><tr><td><FONT COLOR="#000000" FACE="Tahoma" SIZE="4">No data to chart</font></td></tr></table></CENTER><BR>' >> $h
                                }
                            }
                            Else
                            {
                                '<CENTER><table border=1 cellpadding=10><tr><td><FONT COLOR="#000000" FACE="Tahoma" SIZE="4">No data to chart</font></td></tr></table></CENTER><BR>' >> $h
                            }                            
                        }
                        #///////////////////////
                        #// Counter Stats
                        #///////////////////////
                        '<CENTER>' >> $h
                        '<H3>Overall Counter Instance Statistics</H3>' >> $h
                        #'<TABLE BORDER=0 WIDTH=60%><TR><TD>' >> $h
                        #'Overall statistics of each of the counter instances. Min, Avg, and Max are the minimum, average, and Maximum values in the entire log. Hourly Trend is the calculated hourly slope of the entire log. 10%, 20%, and 30% of Outliers Removed is the average of the values after the percentage of outliers furthest away from the average have been removed. This is to help determine if a small percentage of the values are extreme which can skew the average.' >> $h
                        #'</TD></TR></TABLE><BR>' >> $h
                        #// Get the number of thresholds to determine if the counter stat condition is OK or never checked.
                        $iNumberOfThresholds = 0
                        ForEach ($XmlThreshold in $XmlAnalysisNode.SelectNodes('./THRESHOLD'))
                        {
                            $iNumberOfThresholds++
                        }                        
                        ForEach ($XmlChart in $XmlAnalysisNode.SelectNodes('./CHART'))
                        {
                            ForEach ($XmlDataSource in $XmlAnalysisNode.SelectNodes('./DATASOURCE'))
                            {
                                If ($XmlDataSource.EXPRESSIONPATH -eq $XmlChart.DATASOURCE)
                                {
                                    '<TABLE ID="table' + "$iTableCounter" + '" BORDER=1 CELLPADDING=2>' >> $h
                                    $iTableCounter++
                                    '<TR><TH><B>Condition</B></TH><TH><B>' + "$($XmlChart.DATASOURCE)" + '</B></TH><TH><B>Min</B></TH><TH><B>Avg</B></TH><TH><B>Max</B></TH><TH><B>Hourly Trend</B></TH><TH><B>Std Deviation</B></TH><TH><B>10% of Outliers Removed</B></TH><TH><B>20% of Outliers Removed</B></TH><TH><B>30% of Outliers Removed</B></TH></TR>' >> $h                                
                                    ForEach ($XmlCounterInstance in $XmlDataSource.SelectNodes('./COUNTERINSTANCE'))
                                    {
                                        If ($(Test-XmlBoolAttribute -InputObject $XmlCounterInstance -Name 'ISINTERNALONLY') -eq $True)
                                        {
                                            $IsInternalOnly = $True
                                        }
                                        Else
                                        {
                                            $IsInternalOnly = $False
                                        }
                                        If ($IsInternalOnly -eq $False)
                                        {
                                            $IsAlertOnOverallCounterStatInstance = $False
                                            #// Search for the INTERNAL ONLY COUNTER instance that matches this one.
                                            :InternalOnlyAlertLoop ForEach ($XmlAlert in $XmlAnalysisNode.SelectNodes('./ALERT'))
                                            {
                                                If ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $True)
                                                {
                                                    [string] $InternalCounterPath = $XmlAlert.COUNTER
                                                    $InternalCounterPath = $InternalCounterPath.Replace('INTERNAL_OVERALL_COUNTER_STATS_','')
                                                    If ($InternalCounterPath -eq $XmlCounterInstance.COUNTERPATH)
                                                    {
                                                        $IsAlertOnOverallCounterStatInstance = $True
                                                        #// Check if this is a named instance of SQL Server
                                                        If (($XmlCounterInstance.COUNTEROBJECT.Contains('MSSQL$') -eq $True) -or ($XmlCounterInstance.COUNTEROBJECT.Contains('MSOLAP$') -eq $True))
                                                        {
                                                            $sSqlNamedInstance = ExtractSqlNamedInstanceFromCounterObjectPath -sCounterObjectPath $XmlCounterInstance.COUNTEROBJECT
                                                            If ($XmlCounterInstance.COUNTERINSTANCE -eq "")
                                                            {
                                                                $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance
                                                            }
                                                            Else
                                                            {
                                                                $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance + '/' + "$($XmlCounterInstance.COUNTERINSTANCE)"
                                                            }                                                            
                                                        }
                                                        Else
                                                        {
                                                            If ($XmlCounterInstance.COUNTERINSTANCE -eq "")
                                                            {
                                                                $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER
                                                            }
                                                            Else
                                                            {
                                                                $sCounterInstance = "$($XmlCounterInstance.COUNTERCOMPUTER)" + '/' + "$($XmlCounterInstance.COUNTERINSTANCE)"
                                                            }
                                                        }
                                                        '<TR><TD BGCOLOR="' + $XmlAlert.CONDITIONCOLOR + '">' + $(Add-WhiteFont -Text $XmlAlert.CONDITIONNAME -Color $XmlAlert.CONDITIONCOLOR) + '</TD><TD>' + $sCounterInstance + '</TD><TD BGCOLOR="' + $XmlAlert.MINCOLOR + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlCounterInstance.MIN) -Color $XmlAlert.MINCOLOR) + '</TD><TD BGCOLOR="' + $XmlAlert.AVGCOLOR + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlCounterInstance.AVG) -Color $XmlAlert.AVGCOLOR) + '</TD><TD BGCOLOR="' + $XmlAlert.MAXCOLOR + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlCounterInstance.MAX) -Color $XmlAlert.MAXCOLOR) + '</TD><TD BGCOLOR="' + $XmlAlert.TRENDCOLOR + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlCounterInstance.TREND) -Color $XmlAlert.TRENDCOLOR) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.STDDEV) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILENINETYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILEEIGHTYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILESEVENTYTH) + '</TD></TR>' >> $h
                                                        Break InternalOnlyAlertLoop                                                        
                                                    }
                                                }
                                            }
                                            If ($IsAlertOnOverallCounterStatInstance -eq $False)
                                            {
                                                #// Check if this is a named instance of SQL Server
                                                If (($XmlCounterInstance.COUNTEROBJECT.Contains('MSSQL$') -eq $True) -or ($XmlCounterInstance.COUNTEROBJECT.Contains('MSOLAP$') -eq $True))
                                                {
                                                    $sSqlNamedInstance = ExtractSqlNamedInstanceFromCounterObjectPath -sCounterObjectPath $XmlCounterInstance.COUNTEROBJECT
                                                    If ($XmlCounterInstance.COUNTERINSTANCE -eq "")
                                                    {
                                                        $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance
                                                    }
                                                    Else
                                                    {
                                                        $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER + "/" + $sSqlNamedInstance + '/' + "$($XmlCounterInstance.COUNTERINSTANCE)"
                                                    }                                                    
                                                }
                                                Else
                                                {
                                                    If ($XmlCounterInstance.COUNTERINSTANCE -eq "")
                                                    {
                                                        $sCounterInstance = $XmlCounterInstance.COUNTERCOMPUTER
                                                    }
                                                    Else
                                                    {
                                                        $sCounterInstance = "$($XmlCounterInstance.COUNTERCOMPUTER)" + '/' + "$($XmlCounterInstance.COUNTERINSTANCE)"
                                                    }
                                                }
                                                #// If the number of thresholds is zero, then do not put in OK.
                                                If ($iNumberOfThresholds -gt 0)
                                                {
                                                    '<TR><TD BGCOLOR="#00FF00">OK</TD><TD>' + $sCounterInstance + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.MIN) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.AVG) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.MAX) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.TREND) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.STDDEV) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILENINETYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILEEIGHTYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILESEVENTYTH) + '</TD></TR>' >> $h
                                                }
                                                Else
                                                {
                                                    '<TR><TD>No Thresholds</TD><TD>' + $sCounterInstance + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.MIN) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.AVG) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.MAX) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.TREND) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.STDDEV) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILENINETYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILEEIGHTYTH) + '</TD><TD>' + $(AddThousandsSeparator -Value $XmlCounterInstance.PERCENTILESEVENTYTH) + '</TD></TR>' >> $h
                                                }                                                
                                            }
                                        }
                                    }
                                    '</TABLE>' >> $h
                                    '<BR>' >> $h
                                }
                            }
                        }
                        '</CENTER>' >> $h
                        '<BR>' >> $h
                        
                        #///////////////////////
                        #// Alerts
                        #///////////////////////
                        '<CENTER>' >> $h
                        '<H3>Alerts</H3>' >> $h
                        #'<TABLE BORDER=0 WIDTH=60%><TR><TD>' >> $h
                        #'An alert is generated if any of the thresholds were broken during one of the time ranges analyzed. The background of each of the values represents the highest priority threshold that the value broke. See each of the counter' + "'" + 's respective analysis section for more details about what the threshold means.' >> $h
                        #'</TD></TR></TABLE>' >> $h
                        
                        #// Check if no alerts are found.
                        $IsAlertFound = $False
                        :IsAlertsLoop ForEach ($XmlAlert in $XmlAnalysisNode.SelectNodes('./ALERT'))
                        {
                            $IsAlertFound = $True
                            Break IsAlertsLoop
                        }
                        
                        If ($IsAlertFound -eq $False)
                        {
                            '<TABLE BORDER=1 CELLPADDING=5>' >> $h
                            '<TR><TH>No Alerts Found</TH></TR>' >> $h
                            '</TABLE>' >> $h
                            '<BR>' >> $h
                        }
                        Else
                        {
                            '<TABLE BORDER=1 CELLPADDING=2>' >> $h
                            '<TR><TH>Time Range</TH><TH></TH><TH></TH><TH></TH><TH></TH><TH></TH><TH></TH></TR>' >> $h
                            For ($t=0;$t -lt $alQuantizedTime.Count;$t++)
                            {
                                $IsAnyAlertsInQuantizedTimeSlice = $False
                                $TimeRange = GetQuantizedTimeSliceTimeRange -TimeSliceIndex $t
                                $HrefLink = "TimeRange_" + "$(ConvertStringForHref $TimeRange)"
                                :AlertInQuantizedTimeSliceLoopCheck ForEach ($XmlAlert in $XmlAnalysisNode.SelectNodes('./ALERT'))
                                {                                
                                    If (($XmlAlert.TIMESLICEINDEX -eq $t) -and ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False))
                                    {
                                        $IsAnyAlertsInQuantizedTimeSlice = $True
                                        Break AlertInQuantizedTimeSliceLoopCheck
                                    }
                                }
                                If ($IsAnyAlertsInQuantizedTimeSlice -eq $True)
                                {
                                    '<TR><TH><A HREF="#' + $HrefLink + '">' + $TimeRange + '</A></TH><TH>Condition</TH><TH>Counter</TH><TH>Min</TH><TH>Avg</TH><TH>Max</TH><TH>Hourly Trend</TH></TR>' >> $h
                                    ForEach ($XmlAlert in $XmlAnalysisNode.SelectNodes('./ALERT'))
                                    {                                
                                        If (($XmlAlert.TIMESLICEINDEX -eq $t) -and ($(ConvertTextTrueFalse $XmlAlert.ISINTERNALONLY) -eq $False))
                                        {
                                            '<TR><TD></TD><TD BGCOLOR="' + $($XmlAlert.CONDITIONCOLOR) + '">' + $(Add-WhiteFont -Text $($XmlAlert.CONDITIONNAME) -Color $($XmlAlert.CONDITIONCOLOR)) + '</TD><TD>' + $($XmlAlert.COUNTER) + '</TD><TD BGCOLOR="' + $($XmlAlert.MINCOLOR) + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.MIN) -Color $($XmlAlert.MINCOLOR)) + '</TD><TD BGCOLOR="' + $($XmlAlert.AVGCOLOR) + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.AVG) -Color $($XmlAlert.AVGCOLOR)) + '</TD><TD BGCOLOR="' + $($XmlAlert.MAXCOLOR) + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.MAX) -Color $($XmlAlert.MAXCOLOR)) + '</TD><TD BGCOLOR="' + $($XmlAlert.TRENDCOLOR) + '">' + $(Add-WhiteFont -Text $(AddThousandsSeparator -Value $XmlAlert.TREND) -Color $($XmlAlert.TRENDCOLOR)) + '</TD></TR>' >> $h
                                        }
                                    }
                                }
                            }                        
                            '</TABLE>' >> $h
                        }
                    '</CENTER>' >> $h                    
                    '<A HREF="#top">Back to the top</A><BR>' >> $h
                    }
                }                
            }
        }
        '<BR>' >> $h
    }
    '<A HREF="#top">Back to the top</A><BR>' >> $h

    $iPercentComplete = ConvertToDataType $((6 / 7) * 100) 'integer'
    $sComplete = "Progress: $iPercentComplete%"
    Write-Progress -activity 'Generating HTML Report... [Incomplete Analyses]' -status $sComplete -percentcomplete $iPercentComplete -id 2;

    <#
    '<H1><A NAME="IndexOfJobs">Incomplete Analyses</A></H1>' >> $h
    '<BR>' >> $h
    'The following analyses could not be completed.<BR>' >> $h
    '<BR>' >> $h
    '<TABLE BORDER=1>' >> $h
    '<TR><TH WIDTH=300 BGCOLOR="#000000"><FONT COLOR="#FFFFFF">Name</FONT></TH><TH BGCOLOR="#000000"><FONT COLOR="#FFFFFF">Status</FONT></TH></TR>' >> $h
    $IsAllCompleted = $True
    ForEach ($oJob in $global:aIndexOfJobs)
    {
        If ($oJob.Status -ne 'Completed')
        {
            $IsAllCompleted = $False
            $IsFirstCounter = $True
            [string] $sStatus = $oJob.Status + ': '
            If ($oJob.Status -eq 'Incomplete. Missing counter(s) in log.')
            {
                $oXmlAnalysis = $global:XmlAnalysis.PAL.ANALYSIS[$oJob.AnalysisIndex]                
                ForEach ($oXmlDatasource in $oXmlAnalysis.SelectNodes('./DATASOURCE'))
                {
                    If ($oXmlDatasource.TYPE -eq 'CounterLog')
                    {
                        If ($IsFirstCounter -eq $True)
                        {
                            $sStatus = $sStatus + $oXmlDatasource.NAME
                            $IsFirstCounter = $False
                        }
                        Else
                        {
                            $sStatus = $sStatus + ', ' + $oXmlDatasource.NAME
                        }
                    }
                }
            }
            $sLine = '<TR><TD>' + $oJob.Name + '</TD><TD>' + $sStatus + '</TD></TR>' >> $h
        }
    }
    If ($IsAllCompleted -eq $True)
    {
        $sLine = '<TR><TD>All analyses</TD><TD>Completed</TD></TR>' >> $h
    }
    '</TABLE>' >> $h
    '<BR>' >> $h
    
    '<A HREF="#top">Back to the top</A><BR>' >> $h
    #>
    '<BR><BR><TABLE BORDER="1" CELLPADDING="5"><TR><TD BGCOLOR="Silver"><A NAME="Disclaimer"><B>Disclaimer:</B></A> This report was generated using the Performance Analysis of Logs (PAL) tool. The information provided in this report is provided "as-is" and is intended for information purposes only. The software is licensed "as-is". You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.</TD></TR></TABLE>' >> $h
    '</BODY>' >> $h
    '</HTML>' >> $h
    Write-Progress -activity 'Generating HTML Report...' -status $sComplete -Completed -id 2
}

Function SaveXmlReport
{    
    If ($global:oPal.ArgsProcessed.IsOutputXml -eq $True)
    {
        $global:oXml.XmlRoot.Save($global:oPal.ArgsProcessed.XmlOutputFileName)
    }    
}

#//////////////
#// Main
#/////////////

#// The following block of code was contributed by Carl Knox.
if([Reflection.Assembly]::LoadWithPartialName("System.Windows.Forms.DataVisualization") -eq $null)
{
    #// ... then the Microsoft Chart Controls are not installed.
    [void][reflection.assembly]::Load("System.Windows.Forms, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")
    [void][System.Windows.Forms.MessageBox]::Show("Microsoft Chart Controls for Microsoft .NET 3.5 Framework is required", "Microsoft Chart Controls Required")
    #Open the URL
    WriteErrorToHtmlAndShow -sError 'Microsoft Chart Controls for Microsoft .NET 3.5 Framework is required. Download and install for free at http://www.microsoft.com/downloads/en/details.aspx?familyid=130F7986-BF49-4FE5-9CA8-910AE6EA442C&displaylang=en'
    [System.Diagnostics.Process]::Start("http://www.microsoft.com/downloads/en/details.aspx?familyid=130F7986-BF49-4FE5-9CA8-910AE6EA442C&displaylang=en");
    Break;
}

$global:iOverallCompletion = 0
Write-Progress -activity 'Overall progress...' -status 'Progress: 0%... SetThreadPriority' -percentcomplete 0 -id 1
    SetThreadPriority
    InitializeGlobalVariables
    #StartDebugLogFile $global:oPal.Session.UserTempDirectory 0
    ShowMainHeader
    GlobalizationCheck
UpdateOverallProgress -Status 'ProcessArgs'
    ProcessArgs -MyArgs $args
    CreateSessionWorkingDirectory
    ResolvePALStringVariablesForPALArguments
    CreateFileSystemResources
    CreateXmlObject
UpdateOverallProgress -Status 'InheritFromThresholdFiles' 
    InheritFromThresholdFiles -sThresholdFilePath $global:oPal.ArgsProcessed.ThresholdFile
    Write-Host ''
    Write-Host 'Threshold File Load History (in order of priority):'
    $global:oXml.ThresholdFilePathLoadHistory
    Write-Host ''
UpdateOverallProgress -Status 'Creating a threshold file counter list' 
    GenerateThresholdFileCounterList
UpdateOverallProgress -Status 'Preparing counter log(s)' 
    PrepareCounterLogs
UpdateOverallProgress -Status 'Getting time data from counter log(s)' 
    GetTimeDataFromPerfmonLog
UpdateOverallProgress -Status 'Determining the analysis interval' 
    ProcessAnalysisInterval
UpdateOverallProgress -Status 'Creating a counter index'
    GenerateXmlCounterList
UpdateOverallProgress -Status 'Processing question variables' 
    SetDefaultQuestionVariables -XmlAnalysis $global:oXml.XmlAnalyses
UpdateOverallProgress -Status 'Quantizing the analysis interval' 
    $global:oPal.QuantizedIndex = @(GenerateQuantizedIndexArray -ArrayOfTimes $global:oPal.aTime -AnalysisIntervalInSeconds $global:oPal.ArgsProcessed.AnalysisInterval)
    $global:oPal.QuantizedTime = @(GenerateQuantizedTimeArray -ArrayOfTimes $global:oPal.aTime -QuantizedIndexArray $global:oPal.QuantizedIndex)
#// Updates to overall progress are within the following function.
    LoadCounterDataIntoXml
UpdateOverallProgress -Status 'Generating analysis counters' 
    GenerateDataSources
UpdateOverallProgress -Status 'Creating charts' 
    GenerateCharts
UpdateOverallProgress -Status 'Applying thresholds' 
    ProcessThresholds
UpdateOverallProgress -Status 'Preparing report' 
    PrepareDataForReport
UpdateOverallProgress -Status 'Creating the HTML report' 
    GenerateHtml
UpdateOverallProgress -Status 'Saving the XML report' 
    SaveXmlReport
Write-Progress -activity 'Overall progress...' -Completed -id 1 -Status 'Progress: 100%'
    OpenHtmlReport -HtmlOutputFileName $($global:oPal.ArgsProcessed.HtmlOutputFileName)
    #StopDebugLogFile