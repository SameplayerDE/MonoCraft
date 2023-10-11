using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class EntityRotationPacket : Packet
    {

        public int EntityId;
        public sbyte Yaw;
        public sbyte Pitch;
        public bool OnGround;

        public EntityRotationPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            EntityId = stream.ReadVarInt();
            Yaw = stream.ReadAngle();
            Pitch = stream.ReadAngle();
            OnGround = stream.ReadBool();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
