using MonoCraft.Net.Predefined.Datatypes;
using MonoCraft.Net.Predefined.Enums;
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

        public BlockChangePacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            Location = stream.ReadPosition();
            BlockId = stream.ReadVarInt();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
