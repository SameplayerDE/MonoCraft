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

        public PingResponsePacket() : base(0x01)
        {
        }

        public override void Decode(Stream stream)
        {
            Payload = stream.ReadLong();
        }

        public override void Encode(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
