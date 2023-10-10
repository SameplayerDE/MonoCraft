using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Serverbound.Play
{
    public class TeleportConfirmPacket : Packet
    {

        public int TeleportId;

        public TeleportConfirmPacket() : base(0x00)
        {
        }

        public override void Decode(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream)
        {
            stream.WriteVarInt(TeleportId);
        }
    }
}
