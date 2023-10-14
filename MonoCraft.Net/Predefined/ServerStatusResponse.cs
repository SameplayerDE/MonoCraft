using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MonoCraft.Net.Predefined;

public class ServerStatusResponse
{
    [JsonProperty("version")]
    public ServerStatusResponseVersion Version;
    [JsonProperty("players")]
    public ServerStatusResponsePlayerList PlayerList;
    [JsonProperty("description")]
    public ServerStatusResponseDescription Description;
}

public class ServerStatusResponseVersion
{
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("protocol")]
    public int ProtocolVersion;
}

public class ServerStatusResponsePlayerList
{
    [JsonProperty("max")]
    public int Max;
    [JsonProperty("online")]
    public int Online;
}
    
public class ServerStatusResponseDescription
{
    [JsonProperty("text")]
    public string Content;
}