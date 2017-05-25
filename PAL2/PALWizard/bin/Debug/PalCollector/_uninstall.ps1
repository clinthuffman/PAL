param([string] $IsSilentInstallation = 'false', [string] $Log = '.\Setup.log')
#Requires -Version 2.0
# This code is Copyright (c) 2016 Microsoft Corporation.
#
# All rights reserved.
#
# THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR IMPLIED, 
#  INCLUDING BUT NOT LIMITED To THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
#  PARTICULAR PURPOSE.'
#
# IN NO EVENT SHALL MICROSOFT AND/OR ITS RESPECTIVE SUPPLIERS BE LIABLE FOR ANY SPECIAL, INDIRECT OR 
#  CONSEQUENTIAL DAMAGES OR ANY DAMAGES WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS,
#  WHETHER IN AN ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF OR IN CONNECTION 
#  WITH THE USE OR PERFORMANCE OF THIS CODE OR INFORMATION.

[string] $SetupLog = ((PWD).Path + '\Setup.log')

[bool] $IsSilentInstallation = [System.Convert]::ToBoolean($IsSilentInstallation)

#////////////////
#// Functions //
#//////////////

Function Write-Log
{
    param($Output, [string] $Log = '.\Setup.log')
    #// Writes to the log file.
    $TimeStamp = "$(Get-Date -format yyyyMMdd-HHmmss)"    
    if ($Output -eq $null) {Add-content $Log -value ('[' + $TimeStamp + '] NULL') -Encoding Unicode;Return}
    switch ($Output.GetType().FullName)
    {
        'System.String'                {Add-content -Path $Log -value ('[' + $TimeStamp + '] ' + $Output) -Encoding Unicode}
        'System.Collections.ArrayList' {Add-content -Path $Log -value ('[' + $TimeStamp + ']') -Encoding Unicode; $Output >> $Log}
        default                        {Add-content -Path $Log -value ('[' + $TimeStamp + '] ' + $Output) -Encoding Unicode}
    }
}

Function Test-Error
{
    param($Err)
    #// Tests if an error condition exists and writes it to the log.
    if ($Err.Count -gt 0)
    {
        Write-Log ('[Test-Error] Error(s) found: ' + $Err.Count) -Log $Log
        Write-Log ($Err) -Log $Log
        $Err.Clear()
    }
}

Function Invoke-MyCmd
{
    param([string] $Cmd, [string] $Log = '.\Setup.log')
    Write-Log ($Cmd) -Log $Log
    $Output = Invoke-Expression -Command $Cmd
    Write-Log ($Output) -Log $Log
    Test-Error -Err $Error -Log $Log
}

#///////////
#// Main //
#/////////

$Error.Clear()
Write-Log ('[Uninstall]: Start') -Log $Log
Invoke-MyCmd -Cmd 'schtasks /Delete /TN \Microsoft\Windows\PLA\PalCollector-OnWindowsStart /F' -Log $Log
Invoke-MyCmd -Cmd 'logman stop PalCollector' -Log $Log
Start-Sleep -Seconds 2
Invoke-MyCmd -Cmd 'logman delete PalCollector' -Log $Log
Write-Log ('[Uninstall]: End')