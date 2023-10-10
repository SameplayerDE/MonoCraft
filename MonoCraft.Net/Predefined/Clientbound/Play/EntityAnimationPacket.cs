namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityAnimationPacket : Packet
{

    public int EntityId;
    public sbyte Animation;
    
    public EntityAnimationPacket() : base(0x05)
    {
    }

    public override void Decode(Stream stream)
    {
        EntityId = stream.ReadVarInt();
        Animation = stream.ReadSignedByte();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}