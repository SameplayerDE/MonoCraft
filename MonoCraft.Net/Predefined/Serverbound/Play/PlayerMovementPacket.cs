using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class PlayerMovementPacket : Packet
{

    public bool OnGround;
    
    public PlayerMovementPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        stream.WriteBool(OnGround);
    }
}