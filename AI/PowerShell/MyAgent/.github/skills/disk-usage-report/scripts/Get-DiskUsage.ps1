# Get disk usage information for all Windows drives
$disks = Get-Volume | Where-Object {$_.DriveLetter} | Select-Object @(
    @{Name='Drive'; Expression={$_.DriveLetter + ':'}},
    @{Name='FileSystem'; Expression={$_.FileSystem}},
    @{Name='Capacity (GB)'; Expression={[math]::Round($_.Size/1GB, 2)}},
    @{Name='FreeSpace (GB)'; Expression={[math]::Round($_.SizeRemaining/1GB, 2)}},
    @{Name='Used (GB)'; Expression={[math]::Round(($_.Size - $_.SizeRemaining)/1GB, 2)}},
    @{Name='Usage %'; Expression={[math]::Round((($_.Size - $_.SizeRemaining)/$_.Size)*100, 2)}}
)

$disks | ConvertTo-Csv -NoTypeInformation