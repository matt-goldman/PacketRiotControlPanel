namespace PacketRiotConfigurator.Models;

public class Config
{
    public string version { get; set; }
    public string key { get; set; }
    public string hostname { get; set; }
    public string serverKey { get; set; }
    public string serverHost { get; set; }
    public List<Tunnel> https { get; set; }
}
