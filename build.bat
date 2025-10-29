@echo off
REM Build script for AudioPlaybackConnector WinUI3 C# version
REM This script must be run on Windows with .NET 8 SDK and Windows App SDK installed

echo ====================================
echo AudioPlaybackConnector WinUI3 Build
echo ====================================
echo.

cd /d "%~dp0AudioPlaybackConnectorWinUI3"

echo Checking .NET SDK...
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: .NET SDK not found. Please install .NET 8 SDK.
    echo Download from: https://dotnet.microsoft.com/download
    pause
    exit /b 1
)

echo .NET SDK found: 
dotnet --version
echo.

echo Restoring NuGet packages...
dotnet restore
if errorlevel 1 (
    echo ERROR: Failed to restore packages
    pause
    exit /b 1
)
echo.

echo Building project (Debug x64)...
dotnet build -c Debug
if errorlevel 1 (
    echo ERROR: Build failed
    pause
    exit /b 1
)
echo.

echo ====================================
echo Build completed successfully!
echo ====================================
echo.
echo The application is built in:
echo AudioPlaybackConnectorWinUI3\bin\Debug\net8.0-windows10.0.19041.0\
echo.

pause
