using Microsoft.UI.Xaml;
using System;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.Media.Audio;

namespace AudioPlaybackConnectorWinUI3;

public partial class App : Application
{
    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        // Check if the OS supports AudioPlaybackConnection
        if (!IsSupported())
        {
            ShowUnsupportedOSMessage();
            Exit();
            return;
        }

        // Create and activate the main window
        m_window = new MainWindow();
        m_window.Activate();
    }

    private bool IsSupported()
    {
        try
        {
            return ApiInformation.IsTypePresent("Windows.Media.Audio.AudioPlaybackConnection");
        }
        catch
        {
            return false;
        }
    }

    private async void ShowUnsupportedOSMessage()
    {
        var dialog = new Windows.UI.Popups.MessageDialog(
            "AudioPlaybackConnector is not supported on this operating system version.",
            "Unsupported Operating System");
        await dialog.ShowAsync();
    }

    private Window? m_window;
}
