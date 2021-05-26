Param
(
	[Parameter(Position=0)] $CollectionOfAnalyses,
    [Parameter(Position=1)] $alQuantizedIndex,
    [Parameter(Position=2)] $AnalysisInterval,
	[Parameter(Position=3)] $IsLowPriority,
    [Parameter(Position=4)] $iThread
)
Set-StrictMode -Version 2

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
        If ($(IsNumeric -Value $Values) -eq $True)
        {
            [Void] $alNewArray.Add([System.Double]$Values)
        }        
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
    		"integer" {[System.Double] [Math]::Round($ValueAsDouble,0)}
    		"round1" {[System.Double] [Math]::Round($ValueAsDouble,1)}
    		"round2" {[System.Double] [Math]::Round($ValueAsDouble,2)}
    		"round3" {[System.Double] [Math]::Round($ValueAsDouble,3)}
    		"round4" {[System.Double] [Math]::Round($ValueAsDouble,4)}
    		"round5" {[System.Double] [Math]::Round($ValueAsDouble,5)}
    		"round6" {[System.Double] [Math]::Round($ValueAsDouble,6)}
    		default {[System.Double] $ValueAsDouble}
    	}
    }
    Else
    {
        [System.Double] $ValueAsDouble
    }
}

Function GenerateQuantizedAvgValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aAvgQuantizedValues = New-Object System.Collections.ArrayList
    If (($ArrayOfValues -is [System.Collections.ArrayList]) -or ($ArrayOfValues -is [System.object[]]))
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
                [System.Double] $iValue = ConvertToDataType -ValueAsDouble $($iSum / $iCount) -DataTypeAsString $DataTypeAsString
                [Void] $aAvgQuantizedValues.Add($iValue)
            }
            Else
            {
                [System.Double] $d = -1
                [Void] $aAvgQuantizedValues.Add($d)
            }
    	}
    }
    Else
    {
        Return [System.Double] $ArrayOfValues
    }
	$aAvgQuantizedValues
}

Function GenerateQuantizedMinValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aMinQuantizedValues = New-Object System.Collections.ArrayList
    If (($ArrayOfValues -is [System.Collections.ArrayList]) -or ($ArrayOfValues -is [System.object[]]))
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
        	[System.Double] $iValue = ConvertToDataType -ValueAsDouble $iMin -DataTypeAsString $DataTypeAsString
        	[Void] $aMinQuantizedValues.Add($iValue)
    	}
    }
    Else
    {
        Return [System.Double] $ArrayOfValues
    }
	$aMinQuantizedValues
}

Function GenerateQuantizedMaxValueArray
{
	param($ArrayOfValues, $ArrayOfQuantizedIndexes, $DataTypeAsString="double")
	$aMaxQuantizedValues = New-Object System.Collections.ArrayList
    If (($ArrayOfValues -is [System.Collections.ArrayList]) -or ($ArrayOfValues -is [System.object[]]))
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
            [System.Double] $iValue = ConvertToDataType -ValueAsDouble $iMax -DataTypeAsString $DataTypeAsString
            [Void] $aMaxQuantizedValues.Add($iValue)
    	}
    }
    Else
    {
        Return [System.Double] $ArrayOfValues
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
        Return [System.Double] $ArrayOfQuantizedAvgs
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
    		[System.Double] $iTrendValue = CalculateTrend -ArrayOfQuantizedAvgs $ArrayOfQuantizedAvgs[0..$i] -AnalysisIntervalInSeconds $AnalysisInterval -DataTypeAsString "Integer"
    		[void] $alQuantizedValues.Add($iTrendValue)
    	}
    	$alQuantizedValues
    }
    Else
    {
        Return [System.Double] $ArrayOfQuantizedAvgs
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
            Return $Values
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
            Return '-'
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

ForEach ($oAnalysis in @($CollectionOfAnalyses))
{
    ForEach ($oDataSource in @($oAnalysis.DataSources))
    {
        $DataType = $oDataSource.DataType
        ForEach ($oCounterInstance in @($oDataSource.CounterInstances))
        {
            $aValues = $oCounterInstance.aValues
            $oData = New-Object pscustomobject
            $MightBeArrayListOrDouble = $(MakeNumeric -Values $aValues)
            $alAllNumeric = New-Object System.Collections.ArrayList

            If (($MightBeArrayListOrDouble -is [System.Collections.ArrayList]) -or ($MightBeArrayListOrDouble -is [Array]))
            {
                [System.Collections.ArrayList] $alAllNumeric = $MightBeArrayListOrDouble
            }
            Else
            {
                $AlAllNumeric = @($MightBeArrayListOrDouble)
            }

            $alQuantizedAvgValues = @(GenerateQuantizedAvgValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
            $alQuantizedMinValues = @(GenerateQuantizedMinValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
            $alQuantizedMaxValues = @(GenerateQuantizedMaxValueArray -ArrayOfValues $alAllNumeric -ArrayOfQuantizedIndexes $alQuantizedIndex -DataTypeAsString $DataType)
            $alQuantizedTrendValues = @(GenerateQuantizedTrendValueArray -ArrayOfQuantizedAvgs $alQuantizedAvgValues -AnalysisIntervalInSeconds $AnalysisInterval -DataTypeAsString $DataType)

            Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedAvgValues' -Value $alQuantizedAvgValues
            Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedMinValues' -Value $alQuantizedMinValues
            Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedMaxValues' -Value $alQuantizedMaxValues
            Add-Member -InputObject $oData -MemberType NoteProperty -Name 'QuantizedTrendValues' -Value $alQuantizedTrendValues
        
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

            $oCounterInstance.oStats = $oData
        }
    }
$oAnalysis
}