using MonoCraft.Net.Predefined.Enums;
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

        public TeleportConfirmPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            stream.WriteVarInt(TeleportId);
        }
    }
}
