using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Serverbound.Play
{
    public class KeepAlivePacket : KeepAliveBase
    {
        public KeepAlivePacket() : base(0x10)
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
