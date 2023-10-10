namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class UnloadChunkPacket : Packet
{

    public int ChunkX;
    public int ChunkZ;
    
    public UnloadChunkPacket() : base(0x1C)
    {
    }

    public override void Decode(Stream stream)
    {
        ChunkX = stream.ReadInt();
        ChunkZ = stream.ReadInt();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}