using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Status
{
    public class PingResponsePacket : Packet
    {

        public long Payload;

        public PingResponsePacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            Payload = stream.ReadLong();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
