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
            if (version == MinecraftVersion.Ver_1_16_4)
            {
                stream.WriteVarInt(PacketIdentifier.Instance.Identify(version, MinecraftPacketType.SB_Play_KeepAlive));
                stream.WriteLong(KeepAliveId);
            }
            if (version == MinecraftVersion.Ver_1_20_2)
            {
                stream.WriteVarInt(PacketIdentifier.Instance.Identify(version, MinecraftPacketType.SB_Play_KeepAlive));
                stream.WriteLong(KeepAliveId);
            }
        }
    }
}
