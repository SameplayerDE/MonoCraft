namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityMovementPacket : Packet
{

    public int EntityId;
    
    public EntityMovementPacket() : base(0x2A)
    {
    }

    public override void Decode(Stream stream)
    {
        EntityId = stream.ReadVarInt();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}