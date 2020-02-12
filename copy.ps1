
[CmdletBinding(SupportsShouldProcess = $true)]
param(

	[string] $s,
    [string] $e
	

)
#(.net\/)(\?o=(\S+))(&)(v=(\d+))(&)(e=(\d+))&(t=(\S+))
$folder = "C:\Users\jtully\Downloads";
$fileName = "e$e.html"
$source = "$folder\download"
$target = "$folder\$fileName"
$d = "C:\Development\ShitLeopard\ClosedCaptions\s$s\$fileName"
Write-Host "Renaming File $source to $target"
Rename-Item -Path $source -NewName $target
Write-Host "Moving File to $d"
Move-Item -Path $target -Destination $d