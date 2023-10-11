using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class BossBarPacket : Packet
{

    public Guid UUID;
    public int Action;
    
    public BossBarPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        UUID = stream.ReadUUID();
        Action = stream.ReadVarInt();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}