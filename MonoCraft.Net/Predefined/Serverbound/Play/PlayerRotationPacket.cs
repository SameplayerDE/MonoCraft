using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerRotationPacket : Packet
{
    
    public float Yaw;
    public float Pitch;
    public bool OnGround;
    
    public PlayerRotationPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        stream.WriteFloat(Yaw);
        stream.WriteFloat(Pitch);
        stream.WriteBool(OnGround);
    }
}