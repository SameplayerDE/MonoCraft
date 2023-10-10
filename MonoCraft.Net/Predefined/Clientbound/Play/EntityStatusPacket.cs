namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityStatusPacket : Packet
{

    public int EntityId;
    public byte EntityStatus;
    
    public EntityStatusPacket() : base(0x1A)
    {
    }

    public override void Decode(Stream stream)
    {
        EntityId = stream.ReadInt();
        EntityStatus = stream.ReadUByte();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}