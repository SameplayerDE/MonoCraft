namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerRotationPacket : Packet
{
    
    public float Yaw;
    public float Pitch;
    public bool OnGround;
    
    public PlayerRotationPacket() : base(0x13)
    {
    }

    public override void Decode(Stream stream)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream)
    {
        stream.WriteFloat(Yaw);
        stream.WriteFloat(Pitch);
        stream.WriteBool(OnGround);
    }
}