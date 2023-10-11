using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Serverbound.Status
{
    public class PingRequestPacket : Packet
    {

        public long Payload;

        public PingRequestPacket() : base(0x00)
        {
        }

        public override void Decode(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream)
        {
            stream.WriteLong(Payload);
        }
    }
}
