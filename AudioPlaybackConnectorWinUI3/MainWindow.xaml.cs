using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Media.Audio;
using Windows.UI.Popups;
using WinRT.Interop;

namespace AudioPlaybackConnectorWinUI3;

public sealed partial class MainWindow : Window
{
    private DevicePicker? _devicePicker;
    private Dictionary<string, (DeviceInformation Device, AudioPlaybackConnection Connection)> _audioConnections = new();
    private MenuFlyout? _contextMenu;
    private Flyout? _exitFlyout;
    private NotifyIconHelper? _notifyIcon;
    private SettingsManager? _settings;
    private bool _reconnect = false;
    private List<string> _lastDevices = new();

    public MainWindow()
    {
        this.InitializeComponent();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        // Hide the window initially - we'll use notification area
        this.AppWindow.Hide();

        _settings = new SettingsManager();
        await _settings.LoadSettingsAsync();
        _reconnect = _settings.Reconnect;
        _lastDevices = _settings.LastDevices.ToList();

        SetupDevicePicker();
        SetupContextMenu();
        SetupExitFlyout();
        SetupNotifyIcon();

        // Reconnect devices if needed
        if (_reconnect)
        {
            foreach (var deviceId in _lastDevices)
            {
                await ConnectDeviceAsync(deviceId);
            }
            _lastDevices.Clear();
        }
    }

    private void SetupDevicePicker()
    {
        _devicePicker = new DevicePicker();
        
        // Initialize with window handle
        var hwnd = WindowNative.GetWindowHandle(this);
        WinRT.Interop.InitializeWithWindow.Initialize(_devicePicker, hwnd);

        _devicePicker.Filter.SupportedDeviceSelectors.Add(AudioPlaybackConnection.GetDeviceSelector());
        
        _devicePicker.DevicePickerDismissed += (sender, args) =>
        {
            this.AppWindow.Hide();
        };

        _devicePicker.DeviceSelected += async (sender, args) =>
        {
            await ConnectDeviceAsync(args.SelectedDevice);
        };

        _devicePicker.DisconnectButtonClicked += (sender, args) =>
        {
            DisconnectDevice(args.Device);
        };
    }

    private void SetupContextMenu()
    {
        _contextMenu = new MenuFlyout();

        var settingsItem = new MenuFlyoutItem
        {
            Text = GetLocalizedString("Bluetooth Settings"),
            Icon = new FontIcon { Glyph = "\xE713" }
        };
        settingsItem.Click += async (s, e) =>
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:bluetooth"));
        };

        var exitItem = new MenuFlyoutItem
        {
            Text = GetLocalizedString("Exit"),
            Icon = new FontIcon { Glyph = "\xE8BB" }
        };
        exitItem.Click += (s, e) =>
        {
            if (_audioConnections.Count == 0)
            {
                Application.Current.Exit();
            }
            else
            {
                ShowExitConfirmation();
            }
        };

        _contextMenu.Items.Add(settingsItem);
        _contextMenu.Items.Add(exitItem);
    }

    private void SetupExitFlyout()
    {
        var textBlock = new TextBlock
        {
            Text = GetLocalizedString("All connections will be closed.\nExit anyway?"),
            Margin = new Thickness(0, 0, 0, 12)
        };

        var checkbox = new CheckBox
        {
            Content = GetLocalizedString("Reconnect on next start"),
            IsChecked = _reconnect
        };

        var button = new Button
        {
            Content = GetLocalizedString("Exit"),
            HorizontalAlignment = HorizontalAlignment.Right
        };
        button.Click += async (s, e) =>
        {
            _reconnect = checkbox.IsChecked ?? false;
            await SaveAndExitAsync();
        };

        var stackPanel = new StackPanel();
        stackPanel.Children.Add(textBlock);
        stackPanel.Children.Add(checkbox);
        stackPanel.Children.Add(button);

        _exitFlyout = new Flyout
        {
            Content = stackPanel,
            ShouldConstrainToRootBounds = false
        };
    }

    private void SetupNotifyIcon()
    {
        var hwnd = WindowNative.GetWindowHandle(this);
        _notifyIcon = new NotifyIconHelper(hwnd);
        _notifyIcon.IconClicked += OnNotifyIconClicked;
        _notifyIcon.IconRightClicked += OnNotifyIconRightClicked;
        _notifyIcon.Initialize();
    }

    private void OnNotifyIconClicked(object? sender, EventArgs e)
    {
        ShowDevicePicker();
    }

    private void OnNotifyIconRightClicked(object? sender, Point location)
    {
        this.AppWindow.Show();
        _contextMenu?.ShowAt(RootCanvas, location);
    }

    private void ShowDevicePicker()
    {
        if (_devicePicker != null && _notifyIcon != null)
        {
            var iconRect = _notifyIcon.GetIconRect();
            var rect = new Rect(iconRect.Left, iconRect.Top, iconRect.Width, iconRect.Height);
            
            this.AppWindow.Show();
            _devicePicker.Show(rect, Placement.Above);
        }
    }

    private void ShowExitConfirmation()
    {
        if (_exitFlyout != null && _notifyIcon != null)
        {
            var iconRect = _notifyIcon.GetIconRect();
            RootCanvas.Width = iconRect.Width;
            RootCanvas.Height = iconRect.Height;
            
            this.AppWindow.Show();
            _exitFlyout.ShowAt(RootCanvas);
        }
    }

    private async Task ConnectDeviceAsync(DeviceInformation device)
    {
        if (_devicePicker == null) return;

        _devicePicker.SetDisplayStatus(device, 
            GetLocalizedString("Connecting"), 
            DevicePickerDisplayStatusOptions.ShowProgress | DevicePickerDisplayStatusOptions.ShowDisconnectButton);

        try
        {
            var connection = AudioPlaybackConnection.TryCreateFromId(device.Id);
            if (connection == null)
            {
                _devicePicker.SetDisplayStatus(device, 
                    GetLocalizedString("Unknown error"), 
                    DevicePickerDisplayStatusOptions.ShowRetryButton);
                return;
            }

            _audioConnections[device.Id] = (device, connection);

            connection.StateChanged += (sender, args) =>
            {
                if (sender.State == AudioPlaybackConnectionState.Closed)
                {
                    if (_audioConnections.ContainsKey(sender.DeviceId))
                    {
                        _devicePicker?.SetDisplayStatus(_audioConnections[sender.DeviceId].Device, 
                            string.Empty, 
                            DevicePickerDisplayStatusOptions.None);
                        _audioConnections.Remove(sender.DeviceId);
                    }
                    sender.Close();
                }
            };

            await connection.StartAsync();
            var result = await connection.OpenAsync();

            switch (result.Status)
            {
                case AudioPlaybackConnectionOpenResultStatus.Success:
                    _devicePicker.SetDisplayStatus(device, 
                        GetLocalizedString("Connected"), 
                        DevicePickerDisplayStatusOptions.ShowDisconnectButton);
                    break;
                
                case AudioPlaybackConnectionOpenResultStatus.RequestTimedOut:
                    _audioConnections.Remove(device.Id);
                    _devicePicker.SetDisplayStatus(device, 
                        GetLocalizedString("The request timed out"), 
                        DevicePickerDisplayStatusOptions.ShowRetryButton);
                    break;
                
                case AudioPlaybackConnectionOpenResultStatus.DeniedBySystem:
                    _audioConnections.Remove(device.Id);
                    _devicePicker.SetDisplayStatus(device, 
                        GetLocalizedString("The operation was denied by the system"), 
                        DevicePickerDisplayStatusOptions.ShowRetryButton);
                    break;
                
                case AudioPlaybackConnectionOpenResultStatus.UnknownFailure:
                    _audioConnections.Remove(device.Id);
                    var errorMsg = $"Unknown failure (0x{result.ExtendedError.HResult:X8})";
                    _devicePicker.SetDisplayStatus(device, errorMsg, 
                        DevicePickerDisplayStatusOptions.ShowRetryButton);
                    break;
            }
        }
        catch (Exception ex)
        {
            _audioConnections.Remove(device.Id);
            var errorMsg = $"{ex.Message} (0x{ex.HResult:X8})";
            _devicePicker?.SetDisplayStatus(device, errorMsg, 
                DevicePickerDisplayStatusOptions.ShowRetryButton);
        }
    }

    private async Task ConnectDeviceAsync(string deviceId)
    {
        try
        {
            var device = await DeviceInformation.CreateFromIdAsync(deviceId);
            await ConnectDeviceAsync(device);
        }
        catch
        {
            // Ignore errors when reconnecting
        }
    }

    private void DisconnectDevice(DeviceInformation device)
    {
        if (_audioConnections.TryGetValue(device.Id, out var connection))
        {
            connection.Connection.Close();
            _audioConnections.Remove(device.Id);
        }
        _devicePicker?.SetDisplayStatus(device, string.Empty, DevicePickerDisplayStatusOptions.None);
    }

    private async Task SaveAndExitAsync()
    {
        // Save settings
        if (_settings != null)
        {
            _settings.Reconnect = _reconnect;
            _settings.LastDevices = _reconnect ? _audioConnections.Keys.ToList() : new List<string>();
            await _settings.SaveSettingsAsync();
        }

        // Close all connections
        foreach (var connection in _audioConnections.Values)
        {
            connection.Connection.Close();
        }
        _audioConnections.Clear();

        // Cleanup
        _notifyIcon?.Dispose();

        Application.Current.Exit();
    }

    private string GetLocalizedString(string key)
    {
        // Simple localization - can be extended
        return key;
    }
}
