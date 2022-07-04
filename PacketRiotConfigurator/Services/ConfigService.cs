using System.Text.Json;

namespace PacketRiotConfigurator.Services;

public class ConfigService : IConfigService
{
    string ConfigFilePath;

    public ConfigService()
    {
        ConfigFilePath = Preferences.Get(nameof(ConfigFilePath), null);
    }

    public async Task<Config> LoadConfig()
    {
        if (string.IsNullOrWhiteSpace(ConfigFilePath))
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    {DevicePlatform.WinUI, new[] {".json"} },
                    {DevicePlatform.macOS, new[] {".json"} }
                });

            PickOptions options = new PickOptions
            {
                PickerTitle = "Locate your config file",
                FileTypes = customFileType
            };

            var result = await FilePicker.Default.PickAsync(options);

            if (result is null)
                return new Config();

            Preferences.Set(nameof(ConfigFilePath), result.FullPath);

            ConfigFilePath = result.FullPath;
        }

        using (var sr = new StreamReader(ConfigFilePath))
        {
            return JsonSerializer.Deserialize<Config>(sr.ReadToEnd());
        }
    }

    public async Task SaveConfig(Config config)
    {
        if (string.IsNullOrWhiteSpace(ConfigFilePath))
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    {DevicePlatform.WinUI, new[] {".json"} },
                    {DevicePlatform.macOS, new[] {".json"} }
                });

            PickOptions options = new PickOptions
            {
                PickerTitle = "Choose where to save your config",
                FileTypes = customFileType
            };

            var result = await FilePicker.Default.PickAsync(options);

            if (result is null)
                return;

            Preferences.Set(nameof(ConfigFilePath), result.FullPath);

            ConfigFilePath = result.FullPath;
        }

        var serialisedConfig = JsonSerializer.Serialize(config);

        await File.WriteAllTextAsync(ConfigFilePath, serialisedConfig);
    }
}
