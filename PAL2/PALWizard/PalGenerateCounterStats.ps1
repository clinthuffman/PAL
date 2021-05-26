Param
(
	[Parameter(Position=0)] $aValue,
    [Parameter(Position=1)] $alQuantizedIndex,
    [Parameter(Position=2)] $DataType,
    [Parameter(Position=3)] $AnalysisInterval,
	[Parameter(Position=4)] $IsLowPriority
)
Set-StrictMode -Version 2

<#
$aValue = 23836.973587674111,14742.94049656583,19525.322677833501,32158.036528081728,17802.707943444526,18336.923885260159,18649.800681775498,17820.629793292177,17757.080988853231,17792.584507828204
$IsLowPriority = $True
$DataType = 'integer'
$alQuantizedIndex = 0,1,2,3,4,5,6,7,8,9
#>

#// Run as low priority
If (($IsLowPriority -eq $True) -or ($IsLowPriority -eq 'True') -or ($IsLowPriority -eq '$True'))
{
    [System.Threading.Thread]::CurrentThread.Priority = 'Lowest'
}

#///////////////////
#// Functions
#//////////////////

Function IsNumeric
{
    param($Value)
    [double]$number = 0
    $result = [double]::TryParse($Value, [REF]$number)
    $result
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

Function ConvertToDataType
{
	param($ValueAsDouble, $DataTypeAsString="integer")
	$sDateType = $DataTypeAsString.ToLower()

    If ($(IsNumeric -Value $ValueAsDouble) -eq $True)
    {
    	switch ($sDateType)
    	{
    		"integer" {[Math]::Round($ValueAsDouble,0)}
    		"round1" {[Math]::Round($ValueAsDouble,1)}
    		"round2" {[Math]::Round($ValueAsDouble,2)}
    		"round3" {[Math]::Round($ValueAsDouble,3)}
    		"round4" {[Math]::Round($ValueAsDouble,4)}
    		"round5" {[Math]::Round($ValueAsDouble,5)}
    		"round6" {[Math]::Round($ValueAsDouble,6)}
    		default {$ValueAsDouble}
    	}
    }
    Else
    {
        $ValueAsDouble
    }
}

Function GenerateQuantizedAvgValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aAvgQuantizedValues = New-Object System.Collections.ArrayList
    If ($ArrayOfValues -is [System.Collections.ArrayList])
    {
        [boolean] $IsValueNumeric = $false
    	For ($a=0;$a -lt $ArrayOfQuantizedIndexes.Count;$a++)
    	{
    		[double] $iSum = 0.0
            [int] $iCount = 0
    		[System.Object[]] $aSubArray = $ArrayOfQuantizedIndexes[$a]
    		For ($b=0;$b -le $aSubArray.GetUpperBound(0);$b++)
    		{
    			$i = $aSubArray[$b]
                $IsValueNumeric = IsNumeric -Value $ArrayOfValues[$i]
                If ($IsValueNumeric)
                {
                    $iSum += $ArrayOfValues[$i]
                    $iCount++
                }			
    		}
            If ($iCount -gt 0)
            {
                $iValue = ConvertToDataType -ValueAsDouble $($iSum / $iCount) -DataTypeAsString $DataTypeAsString
                [Void] $aAvgQuantizedValues.Add($iValue)
            }
            Else
            {
                [Void] $aAvgQuantizedValues.Add('-')
            }
    	}
    }
    Else
    {
        Return $ArrayOfValues
    }
	$aAvgQuantizedValues
}

Function GenerateQuantizedMinValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aMinQuantizedValues = New-Object System.Collections.ArrayList
    If ($ArrayOfValues -is [System.Collections.ArrayList])
    {
    	For ($a=0;$a -lt $ArrayOfQuantizedIndexes.Count;$a++)
    	{
            [int] $iCount = 0
    		[System.Object[]] $aSubArray = $ArrayOfQuantizedIndexes[$a]
    		$iMin = $ArrayOfValues[$aSubArray[0]]
    		For ($b=0;$b -le $aSubArray.GetUpperBound(0);$b++)
    		{
    			$i = $aSubArray[$b]
    			If ($ArrayOfValues[$i] -lt $iMin)
    			{
    				$iMin = $ArrayOfValues[$i]
    			}
    		}
        	$iValue = ConvertToDataType -ValueAsDouble $iMin -DataTypeAsString $DataTypeAsString
        	[Void] $aMinQuantizedValues.Add($iValue)
    	}
    }
    Else
    {
        Return $ArrayOfValues
    }
	$aMinQuantizedValues
}

Function GenerateQuantizedMaxValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aMaxQuantizedValues = New-Object System.Collections.ArrayList
    If ($ArrayOfValues -is [System.Collections.ArrayList])
    {
    	For ($a=0;$a -lt $ArrayOfQuantizedIndexes.Count;$a++)
    	{
            [int] $iCount = 0
    		[System.Object[]] $aSubArray = $ArrayOfQuantizedIndexes[$a]
    		$iMax = $ArrayOfValues[$aSubArray[0]]
    		For ($b=0;$b -le $aSubArray.GetUpperBound(0);$b++)
    		{
    			$i = $aSubArray[$b]
    			If ($ArrayOfValues[$i] -gt $iMax)
    			{
    				$iMax = $ArrayOfValues[$i]
    			}
    		}
            $iValue = ConvertToDataType -ValueAsDouble $iMax -DataTypeAsString $DataTypeAsString
            [Void] $aMaxQuantizedValues.Add($iValue)
    	}
    }
    Else
    {
        Return $ArrayOfValues
    }
	$aMaxQuantizedValues
}

Function CalculateHourlyTrend
{
	param($Value,$AnalysisIntervalInSeconds,$DataTypeAsString)
    	
    If ($AnalysisIntervalInSeconds -lt 3600)
	{
        $IntervalAdjustment = 3600 / $AnalysisIntervalInSeconds 
        Return ConvertToDataType -ValueAsDouble $($Value * $IntervalAdjustment) -DataTypeAsString $DataTypeAsString
    }

    If ($AnalysisIntervalInSeconds -gt 3600)
	{
        $IntervalAdjustment = $AnalysisIntervalInSeconds / 3600
        Return ConvertToDataType -ValueAsDouble $($Value / $IntervalAdjustment) -DataTypeAsString $DataTypeAsString
    }

    If ($AnalysisIntervalInSeconds -eq 3600)
	{
        Return ConvertToDataType -ValueAsDouble $Value -DataTypeAsString $DataTypeAsString
	}
}

Function RemoveDashesFromArray
{
    param($Array)
    $Array | Where-Object {$_ -notlike '-'}
}

Function CalculateTrend
{
	param($ArrayOfQuantizedAvgs,$AnalysisIntervalInSeconds,$DataTypeAsString)
    $iSum = 0
    If (($ArrayOfQuantizedAvgs -is [System.Collections.ArrayList]) -or ($ArrayOfQuantizedAvgs -is [System.object[]]))
    {
    	If ($ArrayOfQuantizedAvgs -is [System.object[]])
    	{
    		$alDiff = New-Object System.Collections.ArrayList
    		$iUb = $ArrayOfQuantizedAvgs.GetUpperBound(0)
    		If ($iUb -gt 0)
    		{
    			For ($a = 1;$a -le $iUb;$a++)
    			{
                    $ArrayA = RemoveDashesFromArray -Array $ArrayOfQuantizedAvgs[$a]
                    $ArrayB = RemoveDashesFromArray -Array $ArrayOfQuantizedAvgs[$($a-1)]
                    If (($ArrayA -eq $null) -or ($ArrayB -eq $null))
                    {
                        $iDiff = 0
                    }
                    Else
                    {
    				    $iDiff = $ArrayA - $ArrayB
                    }
    				[void] $alDiff.Add($iDiff)
    			}
    		}
    		Else
    		{
    			Return $ArrayOfQuantizedAvgs[0]
    		}
    		
    		ForEach ($a in $alDiff)
    		{
    			$iSum = $iSum + $a
    		}
    		$iAvg = $iSum / $alDiff.Count
    		CalculateHourlyTrend -Value $iAvg -AnalysisIntervalInSeconds $AnalysisIntervalInSeconds -DataTypeAsString $DataTypeAsString
    	}
    	Else
    	{
    		$ArrayOfQuantizedAvgs
    	}
    }
    Else
    {
        Return $ArrayOfQuantizedAvgs
    }
}

Function GenerateQuantizedTrendValueArray
{
	param($ArrayOfQuantizedAvgs,$AnalysisIntervalInSeconds,$DataTypeAsString)
    If (($ArrayOfQuantizedAvgs -is [System.Collections.ArrayList]) -or ($ArrayOfQuantizedAvgs -is [System.object[]]))
    {
    	$alQuantizedValues = New-Object System.Collections.ArrayList
    	[void] $alQuantizedValues.Add(0)
    	For ($i = 1; $i -le $ArrayOfQuantizedAvgs.GetUpperBound(0);$i++)
    	{
    		$iTrendValue = CalculateTrend -ArrayOfQuantizedAvgs $ArrayOfQuantizedAvgs[0..$i] -AnalysisIntervalInSeconds $AnalysisInterval -DataTypeAsString "Integer"
    		[void] $alQuantizedValues.Add($iTrendValue)
    	}
    	$alQuantizedValues
    }
    Else
    {
        Return $ArrayOfQuantizedAvgs
    }
}

Function CalculateStdDev
{
	param($Values)
    $SumSquared = 0
	For ($i=0;$i -lt $Values.Count;$i++)
	{
		$SumSquared = $SumSquared + ($Values[$i] * $Values[$i])
	}	
	$oStats = $Values | Measure-Object -Sum
	
	If ($oStats.Sum -gt 0)
	{
		If ($oStats.Count -gt 1)
		{
			$StdDev = [Math]::Sqrt([Math]::Abs(($SumSquared - ($oStats.Sum * $oStats.Sum / $oStats.Count)) / ($oStats.Count -1)))
		}
		Else
		{
			$StdDev = [Math]::Sqrt([Math]::Abs(($SumSquared - ($oStats.Sum * $oStats.Sum / $oStats.Count)) / $oStats.Count))
		}
	}
	Else
	{
		$StdDev = 0
	}
	$StdDev
}

Function CalculatePercentile
{
	param($Values,$Percentile)
    If ($Values -eq $null)
    {Return $Values}
    If ($Values -is [System.Collections.ArrayList])
    {
    	$oStats = $Values | Measure-Object -Average -Minimum -Maximum -Sum
    	$iDeviation = $oStats.Average * ($Percentile / 100)
    	$iLBound = $Values.Count - [int]$(($Percentile / 100) * $Values.Count)
        $iUBound = [int]$(($Percentile / 100) * $Values.Count)
        [System.Object[]] $aSortedNumbers = $Values | Sort-Object
        If ($aSortedNumbers -isnot [System.Object[]])
        {
            Write-Error 'ERROR: $aSortedNumbers -isnot [System.Object[]]. This is most likely due to no counters in the threshold file matching to counters in the counter log.'
        }        
        $iIndex = 0
        If ($iUBound -gt $aSortedNumbers.GetUpperBound(0))
    	{
            $iUBound = $aSortedNumbers.GetUpperBound(0)
    	}
        If ($iLBound -eq $iUBound)
    	{
            Return $aSortedNumbers[$iLBound]
        }
    	$aNonDeviatedNumbers = New-Object System.Collections.ArrayList
        For ($i=0;$i -lt $iUBound;$i++)
    	{
            [void] $aNonDeviatedNumbers.Add($iIndex)
            $aNonDeviatedNumbers[$iIndex] = $aSortedNumbers[$i]
            $iIndex++
        }
        If ($iIndex -gt 0)
    	{
    		$oStats = $aNonDeviatedNumbers | Measure-Object -Average
            Return $oStats.Average
    	}
        Else
    	{
            Return "-"
        }
    }
    Else
    {
        Return $Values
    }
}

#///////////////////
#// Main
#//////////////////

$oData = New-Object pscustomobject


$MightBeArrayListOrDouble = $(MakeNumeric -Values $aValue)

#// 290 ms

$alAllNumeric = New-Object System.Collections.ArrayList
If (($MightBeArrayListOrDouble -is [System.Collections.ArrayList]) -or ($MightBeArrayListOrDouble -is [Array]))
{
    [System.Collections.ArrayList] $alAllNumeric = $MightBeArrayListOrDouble
}
Else
{        
    [void] $AlAllNumeric.Add($MightBeArrayListOrDouble)
}

#// 290 ms

$alQuantizedAvgValues = @(GenerateQuantizedAvgValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
$alQuantizedMinValues = @(GenerateQuantizedMinValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
$alQuantizedMaxValues = @(GenerateQuantizedMaxValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
$alQuantizedTrendValues = @(GenerateQuantizedTrendValueArray -ArrayOfQuantizedAvgs $alQuantizedAvgValues -AnalysisIntervalInSeconds $AnalysisInterval -DataTypeAsString $DataType)

Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedAvgValues' -Value $alQuantizedAvgValues
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedMinValues' -Value $alQuantizedMinValues
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedMaxValues' -Value $alQuantizedMaxValues
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedTrendValues' -Value $alQuantizedTrendValues
        
#// 950 ms
            
$oStats = $alAllNumeric | Measure-Object -Average -Minimum -Maximum
$Min = $(ConvertToDataType -ValueAsDouble $oStats.Minimum -DataTypeAsString $DataType)
$Avg = $(ConvertToDataType -ValueAsDouble $oStats.Average -DataTypeAsString $DataType)
$Max = $(ConvertToDataType -ValueAsDouble $oStats.Maximum -DataTypeAsString $DataType)
$Trend = $(ConvertToDataType -ValueAsDouble $alQuantizedTrendValues[$($alQuantizedTrendValues.GetUpperBound(0))] -DataTypeAsString $DataType)    
$StdDev = $(CalculateStdDev -Values $alAllNumeric)
$StdDev = $(ConvertToDataType -ValueAsDouble $StdDev -DataTypeAsString $DataType)    
$PercentileSeventyth = $(CalculatePercentile -Values $alAllNumeric -Percentile 70)
$PercentileSeventyth = $(ConvertToDataType -ValueAsDouble $PercentileSeventyth -DataTypeAsString $DataType)
$PercentileEightyth = $(CalculatePercentile -Values $alAllNumeric -Percentile 80)
$PercentileEightyth = $(ConvertToDataType -ValueAsDouble $PercentileEightyth -DataTypeAsString $DataType)
$PercentileNinetyth = $(CalculatePercentile -Values $alAllNumeric -Percentile 90)
$PercentileNinetyth = $(ConvertToDataType -ValueAsDouble $PercentileNinetyth -DataTypeAsString $DataType)

Add-Member -InputObject $oData -MemberType NoteProperty -Name 'Min' -Value $Min
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'Avg' -Value $Avg
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'Max' -Value $Max
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'Trend' -Value $Trend
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'StdDev' -Value $StdDev
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'PercentileSeventyth' -Value $PercentileSeventyth
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'PercentileEightyth' -Value $PercentileEightyth
Add-Member -InputObject $oData -MemberType NoteProperty -Name 'PercentileNinetyth' -Value $PercentileNinetyth

#// 1300 ms

$oData