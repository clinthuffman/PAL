PAL Value Added Perfmon Scripts

Usage: This scripts are provided as value added only to help with data gathering. Assume all scripts are low-overhead and non-invasive unless otherwise specified.

Requirements:
•	You must be logged in with Administrator rights on all servers involved for the scripts to work. Alternate credentials are possible, but not in this version of the scripts.
•	WMI is used in most cases which uses DCOM between servers.
•	The scripts have only been tested on Windows Server 2003 servers, but should work on other Windows operating systems unless otherwise specified.

CreateAndStartPerfmonLogs.vbs: Creates and starts perfmon logs on any number of servers remotely. Must be Windows 2003 servers. The counter list text file contains the list of counters that will be added to the perfmon logs, so run this script against servers that are the same such as all of the BizTalk servers – SQL Servers would be a separate run of this script. Writes it output to output.xml.
Syntax:
CScript CreateAndStartPerfmonLogs.vbs <computer[;computer]> <ServerType> <CounterListFilePath>
 <computer[;computer]>  List of computers to create and start the perfmon log.
 <ServerType>           Text description to be added to the log name. The computer name is added to the end.
 <CounterListFilePath>  File containing the list of perfmon counters.
Example:
CScript CreateAndStartPerfmonLogs.vbs BizTalk01;BizTalk02 BizTalk CounterList_BizTalk2006.txt

StartPerfmonLogs.vbs: Starts perfmon logs on any number of servers remotely. Must be Windows 2003 Servers or greater. This script is helpful if you need to manually start a lot of perfmon logs remotely. The PerfmonLogNamePrefix is used as a wildcard, so if you use “HealthCheck” all of the perfmon logs on the target servers that start with HealthCheck will be effected. Writes it output to output.xml.
SynTax:
CScript StartPerfmonLogs.vbs <computer[;computer]> <PerfmonLogNamePrefix>
 <computer[;computer]>  List of computers to create and start the perfmon log.
 <PerfmonLogNamePrefix> Perfmon log prefix.
Example:
CScript StartPerfmonLogs.vbs BizTalk01;BizTalk02 HealthCheck

StopPerfmonLogs.vbs: Stops perfmon logs on any number of servers remotely. Must be Windows 2003 Servers or greater. This script is helpful if you need to manually stop a lot of perfmon logs remotely. The PerfmonLogNamePrefix is used as a wildcard, so if you use “HealthCheck” all of the perfmon logs on the target servers that start with HealthCheck will be effected. Writes it output to output.xml.
SynTax:
CScript StopPerfmonLogs.vbs <computer[;computer]> <PerfmonLogNamePrefix>
 <computer[;computer]>  List of computers to create and stop the perfmon log.
 <PerfmonLogNamePrefix> Perfmon log prefix. 
Example:
CScript StartPerfmonLogs.vbs BizTalk01;BizTalk02 HealthCheck


MovePerfmonLogs.vbs: Moves perfmon logs from any number of servers remotely to a central folder location. Uses the c$ admin shares. Writes it output to output.xml.
Syntax:
CScript MovePerfmonLogs.vbs <computer[;computer]> <PerfmonLogNamePrefix> <DestinationFolder>
 <computer[;computer]>  List of computers to create and start the perfmon log.
 <PerfmonLogNamePrefix> Perfmon log prefix. The computer name is added to the end.
 <DestinationFolder>    Destination folder where to copy perfmon logs to.
