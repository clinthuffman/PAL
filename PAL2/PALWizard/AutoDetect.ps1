param([string] $InputFilePath)

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

#//////////
#// Main //
#//////////


#// Test if output directory exists.
#$OutputFile = [System.Environment]::ExpandEnvironmentVariables($OutputDirectory)

$aBestThresholdFileCounterList = @()
$aBestMatchThresholdFiles = @{}

$oCounterObjects = Get-Content $InputFilePath

$oFiles = Get-ChildItem *.xml

$sBestThresholdFileMatch = 'None'
$iBestThresholdMatches = 0
$global:alCounterList = New-Object System.Collections.ArrayList

$alFileExcludeList = New-Object System.Collections.ArrayList
[void] $alFileExcludeList.Add('All.xml')
[void] $alFileExcludeList.Add('AutoDetect.xml')
[void] $alFileExcludeList.Add('CalculatedIops.xml')
[void] $alFileExcludeList.Add('Custom.xml')
[void] $alFileExcludeList.Add('ICIPThresholds.xml')
[void] $alFileExcludeList.Add('PALFunctions.xml')
[void] $alFileExcludeList.Add('PALWizard.xml')
[void] $alFileExcludeList.Add('QuickSystemOverview.xml')
[void] $alFileExcludeList.Add('Exchange2007-HUBCAS-TechNet.xml')
[void] $alFileExcludeList.Add('Exchange2007-HUBCASMBX-TechNet.xml')
[void] $alFileExcludeList.Add('Exchange2007-HUB.xml')
[void] $alFileExcludeList.Add('PrintServer.xml')

ForEach ($oFile in $oFiles)
{
    $iMatches = 0
    $iCounters = 0

    If ($($alFileExcludeList.Contains($oFile.Name)) -eq $False)
    {

        [xml] $global:XmlAnalysis = "<PAL></PAL>"
        $global:alThresholdFilePathLoadHistory = New-Object System.Collections.ArrayList
        $global:XmlAnalysis = ReadThresholdFileIntoMemory -sThresholdFilePath $oFile.Name

        ForEach ($XmlPalAnalysisInstance in $global:XmlAnalysis.SelectNodes('//ANALYSIS'))
        {
            $bMatch = $False
            #// DATASOURCE
	        ForEach ($XmlCounter in $XmlPalAnalysisInstance.SelectNodes('./DATASOURCE'))
	        {
                $sCounterDataSourceCategory = GetCounterObject $XmlCounter.NAME
                ForEach ($sCounterObjectInCounterLog in $oCounterObjects)
                {
	                If ($sCounterDataSourceCategory -eq $sCounterObjectInCounterLog)
                    {
                        $iMatches++
                        $bMatch = $True
                    }
                }
                $iCounters++
	        }

            <#
            If ($bMatch -eq $True)
            {
	            ForEach ($XmlCounter in $XmlPalAnalysisInstance.SelectNodes('./THRESHOLD'))
	            {
                    $iMatches++
                    $iCounters++
	            }
            }#>
        }

        If ($iMatches -eq 0)
        {
            $iPercentage = 0
        }
        Else
        {
            $iPercentage = ConvertToDataType $(($iMatches / $iCounters) * 100) 'integer'
        }

        If ($iPercentage -ge 75)
        {
            [void] $aBestMatchThresholdFiles.Add($oFile.Name, $iPercentage)
            $iBestThresholdMatches = $iMatches
        }        
    }
}
$aBestMatchThresholdFiles.Add('QuickSystemOverview.xml', 100)
$aBestMatchThresholdFiles.Keys