using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerPositionPacket : Packet
{

    public double X;
    public double FeetY;
    public double Z;
    public bool OnGround;
    
    public PlayerPositionPacket() : base()
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
        stream.WriteBool(OnGround);
    }
}