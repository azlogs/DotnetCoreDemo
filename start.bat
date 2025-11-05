@echo off
REM Blog Engine Quick Start Script for Windows
REM This script helps you get the Blog Engine API up and running quickly
REM
REM IMPORTANT: This script requires SQL Server to be running.
REM For the easiest setup, use Docker Compose instead:
REM   cd DemoDotnetCore && docker-compose up -d
REM
REM If you have SQL Server installed locally, update the connection string
REM in DemoDotnetCore\BlogEngine\appsettings.json before running this script.

echo.
echo ======================
echo Blog Engine Quick Start
echo ======================
echo.
echo WARNING: This requires SQL Server to be running.
echo For easy setup with SQL Server, use: docker-compose up -d
echo.

REM Check if .NET is installed
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] .NET SDK not found!
    echo Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0
    exit /b 1
)

REM Check .NET version
echo [OK] Found .NET SDK
dotnet --version
echo.

REM Navigate to project directory
cd /d "%~dp0\DemoDotnetCore"

REM Restore packages
echo [INFO] Restoring NuGet packages...
dotnet restore
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Failed to restore packages
    exit /b 1
)
echo.

REM Build the solution
echo [INFO] Building solution...
dotnet build --no-restore
if %ERRORLEVEL% NEQ 0 (
    echo [ERROR] Build failed
    exit /b 1
)
echo.

REM Check if database exists
if not exist "BlogEngine.DataModels\blogengine.db" (
    echo [INFO] Creating database...
    cd BlogEngine.DataModels
    
    REM Check if EF tools are installed
    dotnet ef --version >nul 2>nul
    if %ERRORLEVEL% NEQ 0 (
        echo [INFO] Installing Entity Framework Core tools...
        dotnet tool install --global dotnet-ef --version 9.0.6
    )
    
    REM Apply migrations
    dotnet ef database update
    if %ERRORLEVEL% NEQ 0 (
        echo [ERROR] Failed to create database
        exit /b 1
    )
    echo [OK] Database created successfully
    cd ..
) else (
    echo [OK] Database already exists
)
echo.

REM Run the application
echo [SUCCESS] Starting Blog Engine API...
echo.
echo The API will be available at:
echo   - HTTP:  http://localhost:5000
echo   - HTTPS: https://localhost:5001
echo   - Swagger: http://localhost:5000/swagger
echo.
echo Press Ctrl+C to stop the server
echo.

cd BlogEngine
dotnet run
