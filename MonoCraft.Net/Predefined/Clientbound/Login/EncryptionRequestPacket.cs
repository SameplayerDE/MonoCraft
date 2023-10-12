using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Login
{
    public class EncryptionRequestPacket : Packet
    {

        public string ServerId;
        public int PublicKeyLength;
        public byte[] PublicKey;
        public int VerifyTokenLength;
        public byte[] VerifyToken;

        public EncryptionRequestPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            if (version == MinecraftVersion.Ver_1_16_4)
            {
                ServerId = stream.ReadString();
                PublicKeyLength = stream.ReadVarInt();
                PublicKey = stream.ReadBytes(PublicKeyLength);
                VerifyTokenLength = stream.ReadVarInt();
                VerifyToken = stream.ReadBytes(VerifyTokenLength);
            }
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
