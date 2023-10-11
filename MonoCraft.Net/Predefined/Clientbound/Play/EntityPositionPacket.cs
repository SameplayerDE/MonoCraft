using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class EntityPositionPacket : Packet
    {

        public int EntityId;
        public short DeltaX;
        public short DeltaY;
        public short DeltaZ;
        public bool OnGround;

        public EntityPositionPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            EntityId = stream.ReadVarInt();
            DeltaX = stream.ReadShort();
            DeltaY = stream.ReadShort();
            DeltaZ = stream.ReadShort();
            OnGround = stream.ReadBool();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
