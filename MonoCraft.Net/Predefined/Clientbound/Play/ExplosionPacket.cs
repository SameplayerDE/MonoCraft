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
    
    public ExplosionPacket() : base(0x1B)
    {
    }

    public override void Decode(Stream stream)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}