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
            STATUS = 0x01,
            LOGIN = 0x02
        }

        public int ProtocolVersion;
        public string ServerAddress;
        public ushort ServerPort;
        public State NextState;

        public HandshakePacket() : base(0x00)
        {
        }

        public override void Decode(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream)
        {
            stream.WriteVarInt(ProtocolVersion);
            stream.WriteString(ServerAddress);
            stream.WriteUnsignedShort(ServerPort);
            stream.WriteVarInt((int)NextState);
        }
    }
}
