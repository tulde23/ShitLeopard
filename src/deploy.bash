#!/bin/bash
rsync -avzr -e "ssh -o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null" --progress /mnt/c/development/shitleopard/src/ubuntu/ tulde23@camaro:/home/tulde23/shitleopard.com/
