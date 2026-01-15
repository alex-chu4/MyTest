---
name: disk-usage-report
description: This skill can get all disk usage info of Windows, and provide the data in CSV or Excel (.xlsx). 
---

## Disk Usage Report Skill

1. Run `scripts\Get-DiskUsage.ps1` to get CSV data of disk usage info.  
2. If user want JSON format report, run `scripts/Get-DiskUsage.ps1 | ConvertTo-Json` to convert CSV to JSON
