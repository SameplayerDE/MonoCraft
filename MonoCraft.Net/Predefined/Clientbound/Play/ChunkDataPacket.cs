using fNbt;
using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined.Clientbound.Play
{
    public class ChunkDataPacket : Packet
    {

        public int ChunkX;
        public int ChunkY;
        public bool FullChunk;
        public int PrimaryBitMask;
        public NbtCompound HeightMaps;
        public int[] Biomes;
        public byte[] Data;
        public List<NbtCompound> TileEntities;

        public ChunkDataPacket() : base()
        {
        }

        public override void Decode(Stream stream, MinecraftVersion version)
        {
            ChunkX = stream.ReadInt();
            ChunkY = stream.ReadInt();
            FullChunk = stream.ReadBool();
            PrimaryBitMask = stream.ReadVarInt();
            HeightMaps = stream.ReadNBTag();

            if (FullChunk)
            {
                int biomeCount = stream.ReadVarInt();

                int[] biomeIds = new int[biomeCount];
                for (int idx = 0; idx < biomeIds.Length; idx++)
                {
                    biomeIds[idx] = stream.ReadVarInt();
                }

                Biomes = biomeIds;
            }

           int i = stream.ReadVarInt();
           Data = new byte[i];
           stream.Read(Data, 0, i);
           //int tileEntities = stream.ReadVarInt();
           //for (int k = 0; k < tileEntities; k++)
           //{
           //    TileEntities.Add(stream.ReadNBTag());
           //}
        }

        public override void Encode(Stream stream, MinecraftVersion version)
        {
            throw new NotImplementedException();
        }
    }
}
