using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class SpawnPlayerPacket : Packet
    {

        public int EntityId;
        public Guid PlayerUUID;
        public double X;
        public double Y;
        public double Z;
        public sbyte Yaw;
        public sbyte Pitch;

        public SpawnPlayerPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            EntityId = stream.ReadVarInt();
            PlayerUUID = stream.ReadUUID();
            X = stream.ReadDouble();
            Y = stream.ReadDouble();
            Z = stream.ReadDouble();
            Yaw = stream.ReadAngle();
            Pitch = stream.ReadAngle();
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
