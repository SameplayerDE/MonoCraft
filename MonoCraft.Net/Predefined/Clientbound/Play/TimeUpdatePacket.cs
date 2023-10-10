using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class TimeUpdatePacket : Packet
    {

        public long WorldAge;
        public long TimeOfDay;

        public TimeUpdatePacket() : base(0x4E)
        {
        }

        public override void Decode(Stream stream)
        {
            WorldAge = stream.ReadLong();
            TimeOfDay = stream.ReadLong();
        }

        public override void Encode(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
