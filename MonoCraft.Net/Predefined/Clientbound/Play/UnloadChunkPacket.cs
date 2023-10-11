using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class UnloadChunkPacket : Packet
{

    public int ChunkX;
    public int ChunkZ;
    
    public UnloadChunkPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        ChunkX = stream.ReadInt();
        ChunkZ = stream.ReadInt();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}