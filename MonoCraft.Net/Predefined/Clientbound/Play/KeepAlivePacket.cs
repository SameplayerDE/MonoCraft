using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class KeepAlivePacket : KeepAliveBase
    {
        public KeepAlivePacket() : base(0x1F)
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            KeepAliveId = stream.ReadLong();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            stream.WriteLong(KeepAliveId);
        }
    }
}
