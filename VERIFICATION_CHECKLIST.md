# Verification Checklist

This checklist should be used when testing the WinUI3 C# version on Windows.

## ✅ Build Verification

- [ ] Open `AudioPlaybackConnectorWinUI3.sln` in Visual Studio 2022
- [ ] Restore NuGet packages successfully
- [ ] Build project without errors
- [ ] No warnings (or only acceptable warnings)
- [ ] All platforms build (x64, x86, ARM64)

## ✅ Application Launch

- [ ] Application starts without errors
- [ ] No crash on startup
- [ ] Application checks OS version correctly
- [ ] Error message shown on unsupported OS (if tested on old Windows)

## ✅ System Tray Icon

- [ ] Icon appears in system notification area
- [ ] Icon is visible and clear
- [ ] Tooltip shows "AudioPlaybackConnector"
- [ ] Icon responds to mouse clicks

## ✅ Device Picker

- [ ] Left-click on tray icon opens device picker
- [ ] Device picker positioned correctly near icon
- [ ] Previously paired Bluetooth devices shown
- [ ] Device picker shows "A2DP" or audio devices
- [ ] Device picker can be dismissed

## ✅ Device Connection

- [ ] Selecting a device starts connection
- [ ] "Connecting" status displayed
- [ ] Connection succeeds (or shows appropriate error)
- [ ] "Connected" status displayed on success
- [ ] Disconnect button appears when connected

## ✅ Connection Management

- [ ] Can connect to multiple devices
- [ ] Each device shows correct status
- [ ] Disconnect button works
- [ ] Connection state changes reflected in UI
- [ ] Disconnected devices removed from picker

## ✅ Context Menu

- [ ] Right-click on tray icon opens context menu
- [ ] "Bluetooth Settings" menu item present
- [ ] "Exit" menu item present
- [ ] Icons shown for menu items
- [ ] Menu positioned correctly

## ✅ Bluetooth Settings

- [ ] Clicking "Bluetooth Settings" opens Windows Bluetooth settings
- [ ] Settings app launches correctly
- [ ] Menu closes after action

## ✅ Exit Functionality

### No Active Connections:
- [ ] "Exit" immediately closes application
- [ ] Application exits cleanly
- [ ] Tray icon removed

### With Active Connections:
- [ ] "Exit" shows confirmation dialog
- [ ] Dialog shows connection warning
- [ ] "Reconnect on next start" checkbox present
- [ ] "Exit" button present in dialog
- [ ] Clicking "Exit" closes all connections
- [ ] Clicking "Exit" closes application

## ✅ Settings Persistence

- [ ] Settings file created: `AudioPlaybackConnector.json`
- [ ] File in application directory
- [ ] Reconnect preference saved
- [ ] Connected devices saved when reconnect enabled

## ✅ Auto-Reconnect

### When Enabled:
- [ ] Enable "Reconnect on next start"
- [ ] Exit application
- [ ] Restart application
- [ ] Previously connected devices auto-connect
- [ ] Connection status shown

### When Disabled:
- [ ] Disable "Reconnect on next start"  
- [ ] Exit application
- [ ] Restart application
- [ ] No auto-connect occurs
- [ ] Device picker starts empty

## ✅ Error Handling

- [ ] Connection timeout shown correctly
- [ ] System denied error shown correctly
- [ ] Unknown errors show error code
- [ ] Retry button appears on errors
- [ ] Retry button works

## ✅ UI/UX

- [ ] All text readable
- [ ] Fonts appropriate size
- [ ] Icons clear and visible
- [ ] No UI glitches
- [ ] Animations smooth
- [ ] DPI scaling works correctly
- [ ] Dark/Light theme support (if applicable)

## ✅ Performance

- [ ] Application starts quickly
- [ ] No lag when opening menus
- [ ] Device picker responsive
- [ ] Connection process smooth
- [ ] No memory leaks (after extended use)
- [ ] Low CPU usage when idle

## ✅ Stability

- [ ] No crashes during normal use
- [ ] No crashes during error scenarios
- [ ] Graceful handling of Bluetooth disconnects
- [ ] Stable when devices turned off
- [ ] Stable when multiple quick actions

## ✅ Documentation Accuracy

- [ ] README instructions work
- [ ] Build instructions accurate
- [ ] Usage guide matches actual behavior
- [ ] Troubleshooting section helpful

## ✅ Comparison with C++ Version

If you have the C++ version available:

- [ ] Same features available
- [ ] Same UI behavior
- [ ] Same settings format
- [ ] Compatible configuration files
- [ ] Same or better performance
- [ ] Same or better stability

## 📝 Notes Section

Use this section to record any issues found:

### Issues Found:
```
1. 
2. 
3. 
```

### Performance Notes:
```
- Startup time: 
- Memory usage: 
- CPU usage: 
```

### Additional Observations:
```
1. 
2. 
3. 
```

## ✅ Final Approval

- [ ] All critical features working
- [ ] No major bugs found
- [ ] Performance acceptable
- [ ] Documentation accurate
- [ ] Ready for release

---

**Tested By:** _________________  
**Date:** _________________  
**Windows Version:** _________________  
**Build Number:** _________________  
**Test Duration:** _________________  

**Overall Result:** ⬜ PASS / ⬜ FAIL / ⬜ PARTIAL

**Recommendation:**
```


```
