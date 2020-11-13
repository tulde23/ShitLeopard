 <#
.Synopsis
	Builds a the ShitLeopard Solution

.Description
	Builds the ShitLeopard solution, runs unit and integration tests, performs code coverage analysis and generates a report.

.Example
	Run-Build -projectName "ShitLeopard" -incrementalBuildNumber 10
#>
[CmdletBinding(SupportsShouldProcess = $true)]
param(

	
    [Parameter(HelpMessage="Builds VueJS App")]
	[switch] $build,
    [Parameter(HelpMessage="Publish Local")]
	[switch] $publishLocal,
    [Parameter(HelpMessage="Remote Compy")]
	[switch] $pscp



)
 
 
 
 
 if( $buildVue -eq $True){
 Write-Host 'npm run build'
  npm run build
 }
 
 
 $publish = "$PSScriptRoot\ubuntu"

 $source="$PSScriptRoot\ubuntu"
  $destination = $source


  if( $build -eq $True){

  Write-Host "Building...."
 dotnet publish -c Release -o $source -r linux-x64
 }


 if( $pscp){

 if( $publishLocal -eq $True){
		Write-Host ' pscp -unsafe -r .\ubuntu\*.* tulde23@192.168.86.38:/home/tulde23/ubuntu';
		pscp -P 22 -unsafe -r $destination tulde23@192.168.86.38:/home/tulde23/ubuntu 
	}
	else{
		Write-Host ' pscp -unsafe -r .\ubuntu\*.* tulde23@tully.world:/home/tulde23/ubuntu';
		pscp -P 22 -unsafe -r $destination tulde23@tully.world:/home/tulde23/ubuntu 
	}
 }
 #sudo cp -RT sl/ubuntu /var/aspnetcore/shit_leopard
 #sudo systemctl restart kestrel-shitleopard.service

 #sudo cp -RT ubuntu/ /var/www/aspnetcore/shit_leopard/ubuntu/

