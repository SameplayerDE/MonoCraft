namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerPositionPacket : Packet
{

    public double X;
    public double FeetY;
    public double Z;
    public bool OnGround;
    
    public PlayerPositionPacket() : base(0x12)
    {
    }

    public override void Decode(Stream stream)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream)
    {
        stream.WriteDouble(X);
        stream.WriteDouble(FeetY);
        stream.WriteDouble(Z);
        stream.WriteBool(OnGround);
    }
}