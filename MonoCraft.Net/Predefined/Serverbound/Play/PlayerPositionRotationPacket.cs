using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerPositionRotationPacket : Packet
{

    public double X;
    public double FeetY;
    public double Z;
    public float Yaw;
    public float Pitch;
    public bool OnGround;
    
    public PlayerPositionRotationPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        stream.WriteDouble(X);
        stream.WriteDouble(FeetY);
        stream.WriteDouble(Z);
        stream.WriteFloat(Yaw);
        stream.WriteFloat(Pitch);
        stream.WriteBool(OnGround);
    }
}