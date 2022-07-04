namespace PacketRiotConfigurator.Models;

public class Tunnel
{
    public string domain { get; set; }
    public bool secure { get; set; }
    public bool redirect { get; set; }
    public string upstreamURL { get; set; }
    public bool? useLetsEnc { get; set; }
}
