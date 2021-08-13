Write-Host ' pscp -unsafe -r .\ubuntu\*.* tulde23@tully.world:/home/tulde23/ubuntu';
pscp -P 22 -unsafe -r sl-deploy tulde23@192.168.86.32:/home/tulde23/ 