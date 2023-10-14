using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;
using MonoCraft.Core.Net;
using MonoCraft.Net;
using MonoCraft.Net.Predefined;
using MonoCraft.Net.Predefined.Enums;
using Newtonsoft.Json;

namespace ConsoleClient
{
    public class StatusChecker : Client
    {
        public event Action<ServerStatusResponse> StatusUpdated;

        public string JsonString;
        public ServerStatusResponse Response;

        public StatusChecker()
        {
            ConnectionEstablished += () =>
            {
                Handshake((int)MinecraftVersion.Ver_1_16_4);
                SendRequestAsync();
                ReadResponseAsync();
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

        public void SendRequestAsync()
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            GetStream().Write(stream.ToPacket().ToArray());
        }

        public async void ReadResponseAsync()
        {
            while (IsConnected)
            {
                if (IsConnected && Available > 10)
                {
                    int packetLength = GetStream().ReadVarInt();
                    var buffer = new byte[packetLength];
                    GetStream().Read(buffer, 0, packetLength);
                    var stream = new MemoryStream(buffer);
                    int id = stream.ReadVarInt();
                    JsonString = stream.ReadString();
                    Response = JsonConvert.DeserializeObject<ServerStatusResponse>(JsonString);
                }
                else
                {
                    await Task.Delay(1000);
                }
            }
            
        }
    }
}
