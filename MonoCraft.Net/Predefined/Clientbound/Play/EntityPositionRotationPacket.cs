using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class EntityPositionRotationPacket : Packet
    {

        public int EntityId;
        public short DeltaX;
        public short DeltaY;
        public short DeltaZ;
        public sbyte Yaw;
        public sbyte Pitch;
        public bool OnGround;

        public EntityPositionRotationPacket() : base(0x27)
        {
        }

        public override void Decode(Stream stream)
        {
            EntityId = stream.ReadVarInt();
            DeltaX = stream.ReadShort();
            DeltaY = stream.ReadShort();
            DeltaZ = stream.ReadShort();
            Yaw = stream.ReadAngle();
            Pitch = stream.ReadAngle();
            OnGround = stream.ReadBool();
        }

        public override void Encode(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
