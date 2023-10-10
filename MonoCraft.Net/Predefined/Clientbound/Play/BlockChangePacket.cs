using MonoCraft.Net.Predefined.Datatypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class BlockChangePacket : Packet
    {
        public Position Location;
        public int BlockId;

        public BlockChangePacket() : base(0x0B)
        {
        }

        public override void Decode(Stream stream)
        {
            Location = stream.ReadPosition();
            BlockId = stream.ReadVarInt();
        }

        public override void Encode(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
