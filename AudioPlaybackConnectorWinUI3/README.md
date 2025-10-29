# AudioPlaybackConnector - WinUI3 C# Version

This is a WinUI3 Windows App SDK C# conversion of the original C++/WinRT AudioPlaybackConnector application.

## What Was Converted

The project has been successfully converted from C++/WinRT to WinUI3 C# with the following components:

### Original C++ Components → New C# Components

1. **Main Application Logic** (`AudioPlaybackConnector.cpp`) → `App.xaml.cs` and `MainWindow.xaml.cs`
   - Application initialization and Windows version checking
   - Notification icon management
   - Device picker integration
   - Audio playback connection handling

2. **Settings Management** (`SettingsUtil.hpp`) → `SettingsManager.cs`
   - JSON-based configuration storage
   - Reconnection preferences
   - Last connected devices tracking

3. **Notification Icon** (Win32 API calls) → `NotifyIconHelper.cs`
   - System tray icon management
   - Click and context menu handling
   - Icon positioning and display

4. **UI Components**
   - Context menu with Bluetooth settings and exit options
   - Device picker for selecting Bluetooth audio devices
   - Exit confirmation dialog with reconnection option

## Project Structure

```
AudioPlaybackConnectorWinUI3/
├── AudioPlaybackConnectorWinUI3.csproj    # Project file
├── Package.appxmanifest                    # App manifest
├── app.manifest                            # Application manifest
├── App.xaml                                # Application XAML
├── App.xaml.cs                             # Application code-behind
├── MainWindow.xaml                         # Main window XAML
├── MainWindow.xaml.cs                      # Main window code-behind
├── SettingsManager.cs                      # Settings management
├── NotifyIconHelper.cs                     # Notification icon helper
├── Assets/
│   └── AudioPlaybackConnector.ico         # Application icon
└── Properties/
```

## Key Features Preserved

✅ Bluetooth A2DP Sink device connection  
✅ System tray notification icon  
✅ Device picker for selecting Bluetooth devices  
✅ Connection status display  
✅ Reconnection on startup option  
✅ Settings persistence (JSON format)  
✅ Context menu with Bluetooth settings and exit  
✅ Exit confirmation when active connections exist  

## Building the Project

### Requirements

- Windows 10 version 2004 (build 19041) or later
- Windows 11 recommended
- Visual Studio 2022 version 17.0 or later with:
  - .NET Desktop Development workload
  - Windows App SDK C# Templates
  - Windows 10/11 SDK (10.0.19041.0 or later)

### Build Instructions

1. **Open the project in Visual Studio 2022**
   ```
   Open AudioPlaybackConnectorWinUI3.sln in Visual Studio
   ```

2. **Restore NuGet packages**
   ```
   Right-click solution → Restore NuGet Packages
   ```
   Or via command line:
   ```powershell
   dotnet restore
   ```

3. **Build the project**
   - Select your target platform (x86, x64, or ARM64)
   - Build → Build Solution (or press Ctrl+Shift+B)
   
   Or via command line:
   ```powershell
   dotnet build -c Release
   ```

4. **Run the application**
   - Press F5 to debug or Ctrl+F5 to run without debugging
   
   Or via command line:
   ```powershell
   dotnet run
   ```

### Publishing

To create a distributable package:

```powershell
dotnet publish -c Release -r win-x64 --self-contained
```

## Differences from C++ Version

### Improvements

1. **Simplified Code**: C# provides cleaner async/await patterns and LINQ
2. **Better Memory Management**: Automatic garbage collection
3. **Modern .NET APIs**: Usage of System.Text.Json for configuration
4. **Easier Maintenance**: More readable code with less boilerplate

### Known Limitations

1. **SVG Icon Support**: The C++ version used Direct2D for SVG rendering. The C# version currently uses a static ICO file. SVG support could be added using third-party libraries like SkiaSharp if needed.

2. **Internationalization**: The C++ version had a custom translation system. This can be implemented in C# using standard .NET resource files (RESX) if needed.

3. **Build Platform**: Can only be built on Windows (the original C++ version had the same limitation)

## Configuration

The application stores its configuration in `AudioPlaybackConnector.json` in the application directory:

```json
{
  "Reconnect": false,
  "LastDevices": []
}
```

- `Reconnect`: Whether to automatically reconnect to devices on startup
- `LastDevices`: List of device IDs to reconnect to

## Usage

1. **First Run**: The application minimizes to the system tray
2. **Connect a Device**: 
   - Click the tray icon to open the device picker
   - Select a Bluetooth audio device
   - Wait for connection confirmation
3. **Context Menu**: Right-click the tray icon for options:
   - Bluetooth Settings: Opens Windows Bluetooth settings
   - Exit: Closes the application (with confirmation if devices are connected)
4. **Disconnect**: Click "Disconnect" in the device picker for a connected device

## Troubleshooting

### Application doesn't start
- Ensure you're running Windows 10 version 2004 or later
- Check that Bluetooth is enabled in Windows settings

### Can't find Bluetooth devices
- Pair the device in Windows Bluetooth settings first
- The device must support A2DP Sink profile
- Ensure the device is in pairing/discoverable mode

### Build errors
- Ensure all required Visual Studio components are installed
- Update Windows App SDK to the latest version
- Clean and rebuild the solution

## License

Same as the original project - see LICENSE file.

## Credits

Converted from the original C++/WinRT project by ysc3839:
https://github.com/ysc3839/AudioPlaybackConnector
