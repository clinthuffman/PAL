@setlocal enableextensions
@cd /d "%~dp0"
Powershell.exe -Version 2 -ExecutionPolicy ByPass -File PalCollector.ps1 -OutputDirectory "%1"