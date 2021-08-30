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
	[switch] $pscp,
	[Parameter(HelpMessage="run remote copy")]
	[switch] $remoteCopy,
	[Parameter(HelpMessage="deploy script")]
	[switch] $deployScript




)
 
 
 
 

 
 
 $publish = "$PSScriptRoot\ubuntu"

 $source="$PSScriptRoot\ubuntu"
  $destination = $source


  if( $build -eq $True){

  Write-Host "Building...."
 dotnet publish -c Release -o $source -r linux-x64
 }

 if ($deployScript){
	 bash -c "rsync -avzr -e \`"ssh -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null\`" --progress /mnt/c/development/shitleopard/sl-deploy tulde23@camaro:/home/tulde23/sl-deploy"
 }

 if( $pscp){

	bash -c "rsync -avzr -e \`"ssh -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null\`" --progress /mnt/c/development/shitleopard/src/ubuntu/ tulde23@camaro:/home/tulde23/shitleopard.com/"
 
 }


if( $remoteCopy){
	ssh tulde23@camaro "~/sl-deploy" -S

}
 #sudo cp -Rf /home/tulde23/ubuntu/ubuntu /var/www/aspnetcore/shit_leopard/
 #sudo cp -Rf ubuntu/ /var/www/aspnetcore/shit_leopard/ubuntu/
  #sudo systemctl restart kestrel-shitleopard.service
  #sudo systemctl restart kestrel-shitleopard.service
  #$ journalctl --since "1 minute ago"

