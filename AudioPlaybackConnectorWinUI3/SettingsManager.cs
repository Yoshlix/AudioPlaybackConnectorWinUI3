using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace AudioPlaybackConnectorWinUI3;

public class SettingsManager
{
    private const string ConfigFileName = "AudioPlaybackConnector.json";
    
    public bool Reconnect { get; set; }
    public List<string> LastDevices { get; set; } = new();

    private string GetConfigPath()
    {
        var appPath = AppContext.BaseDirectory;
        return Path.Combine(appPath, ConfigFileName);
    }

    public async Task LoadSettingsAsync()
    {
        try
        {
            var configPath = GetConfigPath();
            if (!File.Exists(configPath))
            {
                SetDefaults();
                return;
            }

            var json = await File.ReadAllTextAsync(configPath);
            var settings = JsonSerializer.Deserialize<SettingsData>(json);
            
            if (settings != null)
            {
                Reconnect = settings.Reconnect;
                LastDevices = settings.LastDevices ?? new List<string>();
            }
            else
            {
                SetDefaults();
            }
        }
        catch
        {
            SetDefaults();
        }
    }

    public async Task SaveSettingsAsync()
    {
        try
        {
            var settings = new SettingsData
            {
                Reconnect = Reconnect,
                LastDevices = LastDevices
            };

            var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions 
            { 
                WriteIndented = true 
            });
            
            var configPath = GetConfigPath();
            await File.WriteAllTextAsync(configPath, json);
        }
        catch
        {
            // Ignore errors when saving
        }
    }

    private void SetDefaults()
    {
        Reconnect = false;
        LastDevices = new List<string>();
    }

    private class SettingsData
    {
        public bool Reconnect { get; set; }
        public List<string> LastDevices { get; set; } = new();
    }
}
