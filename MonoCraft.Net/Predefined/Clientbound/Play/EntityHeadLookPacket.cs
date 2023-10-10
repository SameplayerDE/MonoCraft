namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityHeadLookPacket : Packet
{

    public int EntityId;
    public sbyte HeadYaw;
    
    public EntityHeadLookPacket() : base(0x3A)
    {
    }

    public override void Decode(Stream stream)
    {
        EntityId = stream.ReadVarInt();
        HeadYaw = stream.ReadAngle();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}