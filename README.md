# AudioPlaybackConnector WinUI3

This repository contains a **WinUI3 C# conversion** of the original [AudioPlaybackConnector](https://github.com/ysc3839/AudioPlaybackConnector) by ysc3839.

## 🎯 About

AudioPlaybackConnector is a Bluetooth audio playback (A2DP Sink) connector for Windows 10 2004+ and Windows 11. It provides a simple, modern interface to manage Bluetooth audio connections through a system tray application.

## 📦 What's New in This Fork

This fork converts the original C++/WinRT implementation to a modern **WinUI3 Windows App SDK C# application**, making it:
- ✨ Easier to maintain and extend
- 🚀 More accessible to C# developers
- 🔧 Simpler to build and deploy
- 📚 Better documented

## 📂 Repository Contents

- **Root directory**: Original C++/WinRT version (preserved for reference)
- **AudioPlaybackConnectorWinUI3/**: New WinUI3 C# version

## 🎨 Features

Both versions provide identical functionality:

- ✅ Connect to Bluetooth A2DP Sink devices
- ✅ System tray icon with device picker
- ✅ Connection status monitoring
- ✅ Auto-reconnect on startup (optional)
- ✅ Settings persistence
- ✅ Clean, modern interface

## 🚀 Quick Start

### For Users

1. **Download** the latest release from the [Releases](../../releases) page
2. **Run** the application - it will appear in your system tray
3. **Click** the tray icon to open the device picker
4. **Select** your Bluetooth audio device
5. **Enjoy** your Bluetooth audio connection!

### For Developers

See the [WinUI3 C# version README](AudioPlaybackConnectorWinUI3/README.md) for build instructions.

## 📖 Documentation

- [WinUI3 README](AudioPlaybackConnectorWinUI3/README.md) - Build and usage guide
- [Migration Guide](MIGRATION.md) - Detailed conversion documentation
- [Original README](README_ORIGINAL.md) - Original C++ version documentation

## 🔧 Building

### WinUI3 C# Version (Recommended)

**Requirements:**
- Windows 10/11
- Visual Studio 2022
- .NET 8 SDK
- Windows App SDK

**Build:**
```powershell
cd AudioPlaybackConnectorWinUI3
dotnet restore
dotnet build
```

See [detailed build instructions](AudioPlaybackConnectorWinUI3/README.md) for more information.

### Original C++ Version

**Requirements:**
- Visual Studio 2019/2022 with C++ workload
- Windows SDK
- C++/WinRT packages

**Build:**
Open `AudioPlaybackConnector.sln` in Visual Studio and build.

## 🌍 Language Support

The C++ version includes internationalization support. The C# version currently uses English text only, but can be extended to support multiple languages using .NET resource files (RESX).

## ⚠️ System Requirements

- Windows 10 version 2004 (build 19041) or later
- Windows 11 recommended
- Bluetooth adapter with A2DP Sink support
- Paired Bluetooth audio device

## 🤝 Contributing

Contributions are welcome! Please feel free to:

- Report bugs or issues
- Suggest new features
- Submit pull requests
- Improve documentation

## 📄 License

This project maintains the same license as the original AudioPlaybackConnector project. See [LICENSE](LICENSE) for details.

## 🙏 Credits

- **Original Author**: [ysc3839](https://github.com/ysc3839)
- **Original Project**: [AudioPlaybackConnector](https://github.com/ysc3839/AudioPlaybackConnector)
- **WinUI3 Conversion**: This fork

## 🔗 Links

- [Original Project](https://github.com/ysc3839/AudioPlaybackConnector)
- [WinUI3 Documentation](https://docs.microsoft.com/windows/apps/winui/)
- [Windows App SDK](https://docs.microsoft.com/windows/apps/windows-app-sdk/)

## 💬 Support

If you encounter issues:

1. Check the [documentation](AudioPlaybackConnectorWinUI3/README.md)
2. Review existing [issues](../../issues)
3. Create a new issue with:
   - Windows version
   - Application version
   - Steps to reproduce
   - Expected vs actual behavior

## 🎯 Why WinUI3 C#?

The conversion to WinUI3 C# offers several advantages:

| Aspect | C++ Version | C# Version |
|--------|------------|------------|
| Build Setup | Complex | Simple |
| Code Clarity | Low-level | High-level |
| Memory Management | Manual | Automatic |
| Async Programming | Coroutines | Async/await |
| IDE Support | Good | Excellent |
| Learning Curve | Steep | Gentle |
| Maintenance | Complex | Easy |

Both versions work identically - choose based on your preference and expertise!
