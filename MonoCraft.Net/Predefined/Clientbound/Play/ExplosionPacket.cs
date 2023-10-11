using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class ExplosionPacket : Packet
{

    public float X;
    public float Y;
    public float Z;
    public float Strength;
    public int RecordCount;
    public object Records;
    public float PlayerMotionX;
    public float PlayerMotionY;
    public float PlayerMotionZ;
    
    public ExplosionPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}