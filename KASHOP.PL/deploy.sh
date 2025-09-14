#!/bin/bash
set -e

# Build & Publish
dotnet publish -c Release -o ./publish

# Upload to server (FTP)
lftp -u site36550,'h#2EDb!93=Mn' ftp://site36550.siteasp.net <<EOF
set ssl:verify-certificate no
cd wwwroot
mirror -R ./publish .
quit
EOF

