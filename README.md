# Performance Analysis of Logs (PAL) Tool
[![licence badge]][licence]
[![stars badge]][stars]
[![forks badge]][forks]
[![issues badge]][issues]

[licence badge]:https://img.shields.io/badge/license-MIT-blue.svg
[stars badge]:https://img.shields.io/github/stars/clinthuffman/PAL.svg
[forks badge]:https://img.shields.io/github/forks/clinthuffman/PAL.svg
[issues badge]:https://img.shields.io/github/issues/clinthuffman/PAL.svg

[licence]:https://github.com/clinthuffman/PAL/blob/master/LICENSE.md
[stars]:https://github.com/clinthuffman/PAL/stargazers
[forks]:https://github.com/clinthuffman/PAL/network
[issues]:https://github.com/clinthuffman/PAL/issues

## Project Description
Ever have a performance problem, but don't know what performance counters to collect or how to analyze them?
The PAL (Performance Analysis of Logs) tool is a powerful tool that reads in a performance monitor counter log and analyzes it using known thresholds.

## Features
 - Thresholds files for most of the major Microsoft products such as IIS, MOSS, SQL Server, BizTalk, Exchange, and Active Directory.
 - An easy to use GUI interface which makes creating batch files for the PAL.ps1 script.
 - A GUI editor for creating or editing your own threshold files.
 - Creates an HTML based report for ease of copy/pasting into other applications.
 - Analyzes performance counter logs for thresholds using thresholds that change their criteria based on the computer's role or hardware specs.

## How to download
If you wish to install the PAL tool, then download `PAL_Setup`. It contains the Microsoft installer files.
If you just want to run it without installation, then download `PAL_FlatFile`.

Both files can be downloaded from the [releases section](https://github.com/clinthuffman/PAL/releases)

## How to use
Run PALWizard.exe to use the PAL Wizard tool. Otherwise, use PAL.ps1 directly.

## Feedback
Your feedback is welcome via the [issues section](https://github.com/clinthuffman/PAL/issues)

## Contributing
Your contributions are very welcome by submitting a Pull Request.
