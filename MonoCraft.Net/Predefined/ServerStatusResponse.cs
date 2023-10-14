using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MonoCraft.Net.Predefined;

public class ServerStatusResponse
{
    [JsonProperty("version")]
    public ServerStatusResponseVersion Version;
    [JsonProperty("players")]
    public ServerStatusResponsePlayerList PlayerList;
    //[JsonProperty("description")]
    //public ServerStatusResponseDescription Description;
    [JsonProperty("favicon")]
    public string FaviconBase64;
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
    public string Text;
    [JsonProperty("extra")]
    public List<ServerStatusResponseDescriptionElement> Extra;
}

public class ServerStatusResponseDescriptionElement
{
    [JsonProperty("text")]
    public string Text;
    [JsonProperty("bold")]
    public bool Bold;
    [JsonProperty("color")]
    public string Color;
}