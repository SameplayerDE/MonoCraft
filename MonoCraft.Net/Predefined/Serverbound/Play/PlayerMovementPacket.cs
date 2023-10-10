namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerMovementPacket : Packet
{

    public bool OnGround;
    
    public PlayerMovementPacket() : base(0x15)
    {
    }

    public override void Decode(Stream stream)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream)
    {
        stream.WriteBool(OnGround);
    }
}