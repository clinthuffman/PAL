<#
    .SYNOPSIS
    Looks for a PAL tool threshold file that best matches your system and creates a data collector set based on the threshold file.
    .DESCRIPTION
    Looks for a PAL tool threshold file that best matches your system and creates a data collector set based on the threshold file.
    .EXAMPLE
    .\PalCollector.ps1
    .Notes
    Name: PalCollector.ps1
    Author: Clint Huffman (clinth@microsoft.com)
    Created: 2013-01-08
    Keywords: PowerShell, PAL
#>
param($OutputDirectory='%systemdrive%\Perflogs')

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

Function Test-Property 
{
    #// Function provided by Jeffrey Snover
    #// Tests if a property is a memory of an object.
	param ([Parameter(Position=0,Mandatory=1)]$InputObject,[Parameter(Position=1,Mandatory=1)]$Name)
	[Bool](Get-Member -InputObject $InputObject -Name $Name -MemberType *Property)
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

Function ReadThresholdFileIntoMemory
{
	param($sThresholdFilePath)
	
	[xml] (Get-Content $sThresholdFilePath)	
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

Function GetCounterComputer
{
    param($sCounterPath)
    
	#'\\IDCWEB1\Processor(_Total)\% Processor Time"
	[string] $sComputer = ""
	
	If ($sCounterPath.substring(0,2) -ne "\\")
	{
		Return ""
	}
	$sComputer = $sCounterPath.substring(2)
	$iLocThirdBackSlash = $sComputer.IndexOf("\")
	$sComputer = $sComputer.substring(0,$iLocThirdBackSlash)
	Return $sComputer
}

Function GetCounterObject
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
		Return $sCounterObject
	}	
	$iLocOfCounterInstance = 0
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

Function GetCounterName
{
    param($sCounterPath)
    
	$aCounterPath = $sCounterPath.Split("\")
	Return $aCounterPath[$aCounterPath.GetUpperBound(0)]
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

Function Get-GUID()
{
	Return "{" + [System.GUID]::NewGUID() + "}"
}

Function ConvertCounterExpressionToVarName($sCounterExpression)
{
	$sCounterObject = GetCounterObject $sCounterExpression
	$sCounterName = GetCounterName $sCounterExpression
	$sCounterInstance = GetCounterInstance $sCounterExpression	
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

Function InheritFromThresholdFiles
{
    param($sThresholdFilePath)
    
    $XmlThresholdFile = [xml] (Get-Content $sThresholdFilePath)
    #// Add it to the threshold file load history, so that we don't get into an endless loop of inheritance.
    If ($global:alThresholdFilePathLoadHistory.Contains($sThresholdFilePath) -eq $False)
    {
        [void] $global:alThresholdFilePathLoadHistory.Add($sThresholdFilePath)
    }
    
    #// Inherit from other threshold files.
    ForEach ($XmlInheritance in $XmlThresholdFile.SelectNodes("//INHERITANCE"))
    {
        If ($(Test-FileExists $XmlInheritance.FilePath) -eq $True)
        {
            $XmlInherited = [xml] (Get-Content $XmlInheritance.FilePath)
            ForEach ($XmlInheritedAnalysisNode in $XmlInherited.selectNodes("//ANALYSIS"))
            {
                $bFound = $False            
                ForEach ($XmlAnalysisNode in $global:XmlAnalysis.selectNodes("//ANALYSIS"))
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
                    [void] $global:XmlAnalysis.PAL.AppendChild($global:XmlAnalysis.ImportNode($XmlInheritedAnalysisNode, $True))                
                }
            }
    		If ($global:alThresholdFilePathLoadHistory.Contains($XmlInheritance.FilePath) -eq $False)
    		{
    			InheritFromThresholdFiles $XmlInheritance.FilePath
    		}
        }
    }	 
}

Function Get-UserTempDirectory()
{
	$DirectoryPath = Get-ChildItem env:temp	
	Return $DirectoryPath.Value
}

Function Test-LogicalDiskFreeSpace
{
    param([string] $DriveLetterOrPath, [Int] $FreeSpaceInMB)
    If ($DriveLetterOrPath.Length -ne 1)
    {
        $DriveLetterOrPath = $DriveLetterOrPath.Substring(0,1) + ':'
    }
    Else
    {
        $DriveLetterOrPath = $DriveLetterOrPath + ':'
    }
    $DriveLetterOrPath = $DriveLetterOrPath.ToUpper()
    
    $sWql = 'SELECT DeviceID, Size, FreeSpace FROM Win32_LogicalDisk WHERE DriveType = 3 AND DeviceID = "' + $DriveLetterOrPath + '"'
    
    $ColOfLogicalDisk = Get-WmiObject -Query $sWql | SELECT DeviceID, Size, FreeSpace

    ForEach ($oLogicalDisk in $ColOfLogicalDisk)
    {
        $LogicalDiskSizeInMB = $oLogicalDisk.Size / 1MB
        $LogicalDiskFreeSpaceInMB = $oLogicalDisk.FreeSpace / 1MB
    }

    [Int] $iLeftOverFreeSpace = $LogicalDiskFreeSpaceInMB - $FreeSpaceInMB
    [Int] $iPercentageOfFreeSpaceLeftOver = "{0:N0}" -f (($iLeftOverFreeSpace / $LogicalDiskSizeInMB) * 100)


    If ($FreeSpaceInMB -le $LogicalDiskFreeSpaceInMB)
    {
        If ($iPercentageOfFreeSpaceLeftOver -le 10)
        {
            Write-Warning "$iPercentageOfFreeSpaceLeftOver% of free space will be left over on $DriveLetterOrPath drive!"
        }
        
        Return $true
    }
    Else
    {
        Return $false
    }
}

Function IsNumeric
{
    param($Value)
    [double]$number = 0
    $result = [double]::TryParse($Value, [REF]$number)
    $result
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

#//////////
#// Main //
#//////////

[string] $WorkingDirectory = $PSCommandPath.Replace('\PalCollector.ps1','')

#// Test is output directory exists.
$OutputDirectory = [System.Environment]::ExpandEnvironmentVariables($OutputDirectory)

If ((Test-Path -Path $OutputDirectory) -eq $False)
{
    Write-Error "Output directory does not exist: $OutputDirectory"
    Break;
}

$OutputDriveLetter = $OutputDirectory.Substring(0,1) + ':'
$OutputDriveLetter = $OutputDriveLetter.ToUpper()

#// Test if output logical drive had enough free disk space.
If ((Test-LogicalDiskFreeSpace -DriveLetterOrPath $OutputDirectory -FreeSpaceInMB 200) -eq $False)
{
    Write-Warning "Not enough free space on $OutputDriveLetter drive! Select another location with more free space."
    #Break;
}

$aBestThresholdFileCounterList = @()
$aBestMatchThresholdFiles = @{}

$oCounterObjects = Get-Counter -ListSet * | SELECT CounterSetName | Sort-Object CounterSetName

$oFiles = Get-ChildItem -Path $WorkingDirectory\*.xml

$sBestThresholdFileMatch = 'None'
$iBestThresholdMatches = 0
$global:alCounterList = New-Object System.Collections.ArrayList

ForEach ($oFile in $oFiles)
{
    Write-Output $oFile.Name
    $iMatches = 0
    $iCounters = 0

    [xml] $global:XmlAnalysis = "<PAL></PAL>"
    $global:alThresholdFilePathLoadHistory = New-Object System.Collections.ArrayList
    Write-Host $($oFile.FullName)
    $global:XmlAnalysis = ReadThresholdFileIntoMemory -sThresholdFilePath $($oFile.FullName)
    #InheritFromThresholdFiles -sThresholdFilePath $oFile.FullName

    ForEach ($XmlPalAnalysisInstance in $global:XmlAnalysis.SelectNodes('//ANALYSIS'))
    {
        $bMatch = $False
        #// DATASOURCE
	    ForEach ($XmlCounter in $XmlPalAnalysisInstance.SelectNodes('./DATASOURCE'))
	    {
            $sCounterDataSourceCategory = GetCounterObject $XmlCounter.NAME
            ForEach ($sCounterObjectOnComputer in $oCounterObjects.CounterSetName)
            {
	            If ($sCounterDataSourceCategory -eq $sCounterObjectOnComputer)
                {
                    #//Write-Output $sCounterDataSourceCategory
                    If ($global:alCounterList.Contains($XmlCounter.NAME) -eq $False)
                    {
                        $bMatch = $True
                        [void] $global:alCounterList.Add($XmlCounter.NAME)
                        $iMatches++
                    }
                }
            }
            $iCounters++
	    }

        If ($bMatch = $True)
        {
            #// THRESHOLD
	        ForEach ($XmlCounter in $XmlPalAnalysisInstance.SelectNodes('./THRESHOLD'))
	        {
                $iMatches++
                $iCounters++
	        }
        }
    }

    If ($iMatches -eq 0)
    {
        $iPercentage = 0
    }
    Else
    {
        $iPercentage = ConvertToDataType $(($iMatches / $iCounters) * 100) 'integer'
    }

    If ([Int] $iPercentage -eq [Int] 100)
    {
        [void] $aBestMatchThresholdFiles.Add($oFile.Name, $iPercentage)
        $iBestThresholdMatches = $iMatches
        Write-Output "Matches: $iMatches ($iPercentage%) [INCLUDED]"
    }
    Else
    {
        Write-Output "Matches: $iMatches ($iPercentage%)"
    }
    
    Write-Output '' 

}
Write-Output "Best matched PAL threshold files:"
$aBestMatchThresholdFiles
Write-Output ''

$global:alCounterList = $global:alCounterList.GetEnumerator() | Sort-Object

$aCounters = @()
ForEach ($sCounterPath in $global:alCounterList)
{
    $oObject = New-Object pscustomobject
    $oCtr = CounterPathToObject -sCounterPath $sCounterPath

    If ($oCtr.Instance -eq '')
    {
        $sCounterInstance = ''
        $sNewCounterPath = '\\' + $($oCtr.Object) + '\' + $($oCtr.Name)
    }
    Else
    {
        $sCounterInstance = '*'
        $sNewCounterPath = '\\' + $($oCtr.Object) + '(*)\' + $($oCtr.Name)
    }

    Add-Member -InputObject $oObject -MemberType NoteProperty -Name 'CounterObject' -Value $($oCtr.Object)
    Add-Member -InputObject $oObject -MemberType NoteProperty -Name 'CounterInstance' -Value $sCounterInstance
    Add-Member -InputObject $oObject -MemberType NoteProperty -Name 'CounterName' -Value $($oCtr.Name)
    Add-Member -InputObject $oObject -MemberType NoteProperty -Name 'CounterPath' -Value $sNewCounterPath
    $aCounters += @($oObject)
}

$aCounters = $aCounters | Sort-Object CounterPath -Unique
$UserTempDirectory = Get-UserTempDirectory
$aCounters.CounterPath | Out-File -FilePath "$UserTempDirectory\counterlist.txt" -Force

Write-Output 'logman stop PalCollector'
logman stop PalCollector
Write-Output 'An error is normal if this is the first time you have ran this script.'
Write-Output ''

Write-Output 'logman delete PalCollector'
logman delete PalCollector
Write-Output 'An error is normal if this is the first time you have ran this script.'
Write-Output ''

Write-Output "logman create counter PalCollector -cf `"$UserTempDirectory\counterlist.txt`" -f bincirc -max 200 -ow"
logman create counter PalCollector -cf "$UserTempDirectory\counterlist.txt" -f bincirc -max 200 -ow
Write-Output ''
Write-Output 'logman start PalCollector'
logman start PalCollector

Remove-Item -Path "$UserTempDirectory\counterlist.txt"

Write-Output 'schtasks /delete /tn \Microsoft\Windows\PerfCollector\PalCollectorOnStart /F'
schtasks /delete /tn PalCollectorOnStart /F
Write-Output 'An error is normal if this is the first time you have ran this script.'
Write-Output ''

Write-Output 'schtasks /create /tn \Microsoft\Windows\PerfCollector\PalCollectorOnStart /sc onstart /tr "logman start PalCollector" /ru system'
schtasks /create /tn PalCollectorOnStart /sc onstart /tr "logman start PalCollector" /ru system

Write-Output ''
Write-Output 'A Performance Monitor data collector called "PalCollector" has been created in a binary circular log and is scheduled to start upon reboot. Go to Start, Run, "Perfmon", <Enter>, then navigate User Defined data collector sets to see the data collector.'
Write-Output 'Done!'