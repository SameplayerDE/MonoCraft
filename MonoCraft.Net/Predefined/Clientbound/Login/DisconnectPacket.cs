using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Login
{
    public class DisconnectPacket : Packet
    {

        public string Reason;

        public DisconnectPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            if (version == MinecraftVersion.Ver_1_16_4)
            {
                Reason = stream.ReadChat();
            }
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
