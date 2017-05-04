=================
PAL v2.0
=================

!! Introduction
*PAL v2.0* is an easy to use tool which simplifies the analysis of Microsoft Performance Monitor Logs (.blg | .csv). It generates an HTML report containing graphical charts and alerts of the performance counters using known thresholds.

!! Usage
Execute the the PAL icon in your Start Programs menu or run the PAL.ps1 script from a PowerShell.

*PowerShell Syntax:*
.\PAL.ps1 -Log <Path to the Perfmon Log> -ThresholdFile <Path to the PAL xml threshold file> -NumberOfProcessors Integer -TotalMemory Integer -SixtyFourBit $True|$False -AllCounterStats $True|$False

-Log: (Required) System.String This is the file path to a Microsoft Performance Monitor Log in either binary (.blg) or text (.csv). Multiple counter logs can be specified by separating each file path with a semicolon (;). PAL will merge these files togeher using the Relog.exe command line tool built into the operating system.

-ThresholdFile: (Optional) System.String This is the file path to the PAL threshold file (*.xml). These files exist in the installation directory of the PAL tool. If omitted, the QuickSystemOverview.xml threshold file is used.

-AnalysisInterval: (Optional) System.Int32 or System.String This is the time interval in seconds that determines how the PAL tool will "slice" the counter log and create statistics for each slice. Each slice is analyzed against thresholds. The default is 'AUTO' which will automatically slice up the log into 30 equal parts.

-IsOutputHtml

-IsOutputXml

-HtmlOutputFileName

-XmlOutputFileName

-NumberOfThreads

-IsLowPriority

-DisplayReport

-AllCounterStats: (Optional) System.Boolean This is a new feature in PAL v2.0 which tells PAL to chart all performance counters found in the log. *Warning:* This can be resource intensive. If omitted, False is assumed - meaning PAL will only chart and analyze counters found in the PAL threshold file specified in the ThresholdFile argument.

-OutputDir: (Optional) System.String This is the directory path of the directory location create the output of the tool.

Some threshold files might require more parameters. For example, Quick System Overview uses the following parameters:

-NumberOfProcessors: (Optional) System.Int32 This is the number of logical processors (including mult-core processors) running on the computer where the Perfmon log was captured. Do not count Hyper-Threaded processors.

-TotalMemory: (Optional) System.Int32 This is the amount of physical RAM in gigabytes installed on the computer where the Perfmon log was captured. If omitted, 4GBs is assumed.

-SixtyFourBit: (Optional) System.Boolean This a whether or not the computer where the perfmon log was captured is a 32-bit (x86) or 64-bit (x64) computer. If omitted, False (32-bit) is assumed.



PAL threshold files might need more arguments passed into the script for proper analysis. Consult the documentation of the threshold file for more information on special arguments.

*Basic Example:*
.\PAL.ps1 -Log SamplePerfmonLog.blg -ThresholdFile QuickSystemOverview.xml -NumberOfProcessors 1 -TotalMemory 1 -SixyFourBit $False -AllCounterStats $False

*Process all counters in the log Example:*
.\PAL.ps1 -Log SamplePerfmonLog.blg -ThresholdFile QuickSystemOverview.xml -NumberOfProcessors 1 -TotalMemory 1 -SixtyFourBit $False -AllCounterStats $True

!! Installation

Run the PAL setup MSI file that ships in the zip file at http://pal.codeplex.com.

*Required Products (free and public):*
 - PowerShell v2.0 or greater.
 - Microsoft .NET Framework 3.5 Service Pack 1
 - Microsoft Chart Controls for Microsoft .NET Framework 3.5

The MSI installer will install the Microsoft Chart Controls for Microsoft .NET Framework 3.5 and .NET Framework v3.5 if needed.

*Warning:* The PAL installer (MSI) will set the PowerShell execution policy to unrestricted. This will allow the execution of PowerShell scripts.

*Download locations:*

Microsoft .NET Framework 3.5 Service Pack 1 (Partial package - internet access required)
http://www.microsoft.com/downloads/details.aspx?familyid=AB99342F-5D1A-413D-8319-81DA479AB0D7&displaylang=en

Microsoft .NET Framework 3.5 Service Pack 1 (full package - no internet access required)
http://download.microsoft.com/download/2/0/e/20e90413-712f-438c-988e-fdaa79a8ac3d/dotnetfx35.exe

Microsoft Chart Controls for Microsoft .NET Framework 3.5
http://www.microsoft.com/downloads/details.aspx?FamilyID=130f7986-bf49-4fe5-9ca8-910ae6ea442c&DisplayLang=en

*PowerShell Execution Policy*
Installation of PAL v2.0 will PowerShell to unrestricted script usage. The Microsoft installer (MSI) package will automatically set the machine level execution policy to unrestricted. 

To this manually, open an elevated (run as administrator) PowerShell prompt by clicking Start, All Programs, PowerShell, then right click on Windows PowerShell, and run as Administrator. Next, type the following command:
Set-ExecutionPolicy unrestricted

*Warning:* The PAL installer (MSI) will set the PowerShell execution policy to unrestricted. This will allow the execution of PowerShell scripts.

!! Support

This tool is not supported by Microsoft. Please post all of your support questions to the PAL web site at http://pal.codeplex.com/workitem/list/basic?ProjectName=pal