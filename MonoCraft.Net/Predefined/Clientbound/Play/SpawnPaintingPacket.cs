using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class SpawnPaintingPacket : Packet
    {
        public SpawnPaintingPacket() : base(0x03)
        {
        }

        public override void Decode(Stream stream)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
