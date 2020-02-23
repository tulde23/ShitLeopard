 <#
.Synopsis
	Builds a the Gumshoe Solution

.Description
	Builds the Gumshoe solution, runs unit and integration tests, performs code coverage analysis and generates a report.

.Example
	Run-Build -projectName "Gumshoe" -incrementalBuildNumber 10
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(

	
    [Parameter(HelpMessage="Builds VueJS App")]
	[switch] $buildVue,
    [Parameter(HelpMessage="Publish Local")]
	[switch] $publishLocal



)
 
 
 
 
 if( $buildVue -eq $True){
 Write-Host 'npm run build'
  npm run build
 }
 
 
 $publish = "ubuntu"

 $source="ubuntu"
  $destination = $source



 dotnet publish -c Release -o $source -r linux-x64

 if( $publishLocal -eq $True){
 Write-Host ' pscp -unsafe -r .\ubuntu\*.* tulde23@192.168.1.96:/home/tulde23/sl';
 pscp -unsafe -r $destination tulde23@192.168.1.96:/home/tulde23/sl
 }
 else{
 Write-Host ' pscp -unsafe -r .\ubuntu\*.* tulde23@tully.world:/home/tulde23/sl';
 pscp -unsafe -r $destination tulde23@tully.world:/home/tulde23/sl
 }
 #sudo cp -RT sl/ubuntu /var/aspnetcore/shit_leopard
 #sudo systemctl restart kestrel-shit-leopard.service
