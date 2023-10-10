using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class BossBarPacket : Packet
{

    public Guid UUID;
    public int Action;
    
    public BossBarPacket() : base(0x0C)
    {
    }

    public override void Decode(Stream stream)
    {
        UUID = stream.ReadUUID();
        Action = stream.ReadVarInt();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}