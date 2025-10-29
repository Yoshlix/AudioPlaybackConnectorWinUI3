#!/bin/bash
# Build script for AudioPlaybackConnector WinUI3 C# version
# Note: This can only check the project structure on Linux.
# Actual build requires Windows with .NET SDK and Windows App SDK.

echo "===================================="
echo "AudioPlaybackConnector WinUI3 Check"
echo "===================================="
echo ""

cd "$(dirname "$0")/AudioPlaybackConnectorWinUI3"

echo "Checking .NET SDK..."
if ! command -v dotnet &> /dev/null; then
    echo "ERROR: .NET SDK not found."
    exit 1
fi

echo ".NET SDK found:"
dotnet --version
echo ""

echo "Checking project files..."
if [ ! -f "AudioPlaybackConnectorWinUI3.csproj" ]; then
    echo "ERROR: Project file not found"
    exit 1
fi

echo "✓ Project file found"
echo ""

echo "Attempting to restore packages..."
dotnet restore
if [ $? -eq 0 ]; then
    echo "✓ Package restore successful"
else
    echo "⚠ Package restore failed (expected on non-Windows systems)"
fi
echo ""

echo "===================================="
echo "Project structure validated!"
echo "===================================="
echo ""
echo "To build this project:"
echo "1. Use Windows 10/11 with Visual Studio 2022"
echo "2. Run: dotnet build"
echo "   or use build.bat on Windows"
echo ""
