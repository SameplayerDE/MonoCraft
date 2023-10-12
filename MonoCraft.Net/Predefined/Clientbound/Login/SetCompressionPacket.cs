using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Login
{
    public class SetCompressionPacket : Packet
    {

        public int Threshold;

        public SetCompressionPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            if (version == MinecraftVersion.Ver_1_16_4)
            {
;                Threshold = stream.ReadVarInt();
            }
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
