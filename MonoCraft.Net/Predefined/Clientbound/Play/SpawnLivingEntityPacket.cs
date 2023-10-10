using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class SpawnLivingEntityPacket : Packet
    {
        public SpawnLivingEntityPacket() : base(0x02)
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
