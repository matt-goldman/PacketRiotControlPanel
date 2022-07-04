namespace PacketRiotConfigurator.Services;

public interface IConfigService
{
    Task<Config> LoadConfig();

    Task SaveConfig(Config config);
}
