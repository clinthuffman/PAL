@setlocal enableextensions
@cd /d "%~dp0"
Powershell.exe -ExecutionPolicy ByPass -File PalCollector.ps1 -OutputDirectory "%1"