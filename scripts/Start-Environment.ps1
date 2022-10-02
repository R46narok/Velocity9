Write-Host "[+] Starting rider" -ForegroundColor DarkBlue

Start-Process -FilePath "rider64.exe " -ArgumentList "$PSScriptRoot/../ZeroGravity.sln"

Write-Host "[+] Starting the docker daemon" -ForegroundColor DarkBlue
Start-Process "C:\Program Files\Docker\Docker\Docker Desktop.exe"

Write-Host "[+] Starting all containers..." -ForegroundColor DarkBlue

docker compose -f "$PSScriptRoot\..\tools/rabbitmq/docker-compose.yml" up -d
docker compose -f "$PSScriptRoot\..\tools/mssqlserver/docker-compose.yml" up -d

