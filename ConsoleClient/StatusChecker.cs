using MonoCraft.Core.Net;
using MonoCraft.Net;
using MonoCraft.Net.Predefined;
using MonoCraft.Net.Predefined.Enums;
using Newtonsoft.Json;

namespace ConsoleClient;

public class StatusChecker : Client
{

    public ServerStatusResponse Response;
    public string JsonString;
    
    public StatusChecker()
    {
        ConnectionEstablished += () =>
        {
            Handshake((int)MinecraftVersion.Ver_1_16_4);
            SendRequest();
            var task = ReadResponse();
            Response = task.Result;
        };
    }
    
    public void Handshake(int protocolVersion)
    {
        var stream = new MemoryStream();
        stream.WriteVarInt(0x00);
        stream.WriteVarInt(protocolVersion);
        stream.WriteString(Address);
        stream.WriteUnsignedShort(Port);
        stream.WriteVarInt(0X01);
        GetStream().Write(stream.ToPacket().ToArray());
    }
    
    public void SendRequest()
    {
        var stream = new MemoryStream();
        stream.WriteVarInt(0x00);
        GetStream().Write(stream.ToPacket().ToArray());
    }

    public async Task<ServerStatusResponse> ReadResponse()
    {
        try
        {
            while (IsConnected)
            {

                if (GetStream() == null)
                {
                    break;
                }

                if (Available > 10)
                {
                    try
                    {
                        int packetLength = GetStream().ReadVarInt();
                        var stream = new MemoryStream(ReceiveData(packetLength));
                        int id = stream.ReadVarInt();
                        JsonString = stream.ReadString();
                        var response = JsonConvert.DeserializeObject<ServerStatusResponse>(JsonString);
                        return response;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"{ex.Message}");
                    }
                }
                else
                {
                    await Task.Delay(10);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }
    
    public byte[] ReceiveData(int bufferSize)
    {
        byte[] buffer = new byte[bufferSize];
        int totalBytesRead = 0;

        while (totalBytesRead < bufferSize)
        {
            int bytesRead = GetStream().Read(buffer, totalBytesRead, bufferSize - totalBytesRead);
            if (bytesRead == 0)
            {
                throw new IOException("Verbindung geschlossen, bevor genügend Daten empfangen wurden.");
            }
            totalBytesRead += bytesRead;
        }
        return buffer;
    }
    
}