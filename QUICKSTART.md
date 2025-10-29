# Quick Start Guide

## For Windows Users

### Prerequisites
- Windows 10 version 2004 (build 19041) or later
- Visual Studio 2022 (recommended) or .NET 8 SDK

### Option 1: Using Visual Studio 2022 (Recommended)

1. **Open the Solution**
   ```
   Double-click AudioPlaybackConnectorWinUI3.sln
   ```

2. **Restore Packages**
   - Visual Studio will automatically restore NuGet packages
   - Or: Right-click solution → Restore NuGet Packages

3. **Build**
   - Press `Ctrl+Shift+B` or
   - Build → Build Solution

4. **Run**
   - Press `F5` to debug or `Ctrl+F5` to run without debugging

### Option 2: Using Command Line

1. **Open PowerShell or Command Prompt**

2. **Navigate to Project**
   ```powershell
   cd AudioPlaybackConnectorWinUI3
   ```

3. **Restore and Build**
   ```powershell
   dotnet restore
   dotnet build -c Release
   ```

4. **Run**
   ```powershell
   dotnet run
   ```

### Option 3: Using Build Script

1. **Double-click `build.bat`** in Windows Explorer
2. Wait for build to complete
3. Navigate to `AudioPlaybackConnectorWinUI3\bin\Debug\net8.0-windows10.0.19041.0\`
4. Run `AudioPlaybackConnectorWinUI3.exe`

## For Linux/macOS Users

You can validate the project structure but cannot build:

```bash
chmod +x build.sh
./build.sh
```

This will:
- Check .NET SDK installation
- Validate project files
- Restore NuGet packages (if compatible)
- Confirm project structure

## First Run

1. **Application starts** and minimizes to system tray
2. **Look for icon** in Windows notification area (bottom-right)
3. **Click icon** to open Bluetooth device picker
4. **Right-click icon** for settings menu

## Connecting a Device

1. Pair Bluetooth device in Windows Settings first
2. Click the tray icon
3. Select your Bluetooth audio device
4. Wait for "Connected" status
5. Enjoy your Bluetooth audio!

## Common Issues

**Can't build:**
- Ensure you have Windows 10/11
- Install Visual Studio 2022 with .NET Desktop workload
- Or install .NET 8 SDK

**Can't find devices:**
- Pair device in Windows Bluetooth settings first
- Ensure device supports A2DP Sink
- Check device is in pairing mode

**Application won't start:**
- Check Windows version (must be 2004+)
- Check if .NET 8 runtime is installed
- Run as administrator if needed

## Next Steps

- See [README.md](README.md) for full documentation
- See [VERIFICATION_CHECKLIST.md](VERIFICATION_CHECKLIST.md) for testing
- See [AudioPlaybackConnectorWinUI3/README.md](AudioPlaybackConnectorWinUI3/README.md) for details

## Need Help?

- Check documentation in this repository
- Review [troubleshooting](AudioPlaybackConnectorWinUI3/README.md#troubleshooting)
- Open an issue on GitHub
