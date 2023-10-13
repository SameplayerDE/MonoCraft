using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Serverbound.Handshake
{
    public class HandshakePacket : Packet
    {

        public enum State
        {
            Status = 0x01,
            Login = 0x02
        }

        public int ProtocolVersion;
        public string ServerAddress;
        public ushort ServerPort;
        public State NextState;

        public HandshakePacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            stream.WriteVarInt(ProtocolVersion);
            stream.WriteString(ServerAddress);
            stream.WriteUnsignedShort(ServerPort);
            stream.WriteVarInt((int)NextState);
        }
    }
}
