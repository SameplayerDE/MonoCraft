namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerPositionRotationPacket : Packet
{

    public double X;
    public double FeetY;
    public double Z;
    public float Yaw;
    public float Pitch;
    public bool OnGround;
    
    public PlayerPositionRotationPacket() : base(0x13)
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
        stream.WriteFloat(Yaw);
        stream.WriteFloat(Pitch);
        stream.WriteBool(OnGround);
    }
}