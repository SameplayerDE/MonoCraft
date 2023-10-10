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

        public EntityRotationPacket() : base(0x29)
        {
        }

        public override void Decode(Stream stream)
        {
            EntityId = stream.ReadVarInt();
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
