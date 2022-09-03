Write-Host "[+] Starting the docker daemon" -ForegroundColor DarkBlue
Start-Process "C:\Program Files\Docker\Docker\Docker Desktop.exe"

Write-Host "[+] Starting all containers..." -ForegroundColor DarkBlue

cd rabbitmq
docker compose up -d
cd ..

cd mssqlserver
docker compose up -d
cd ..

