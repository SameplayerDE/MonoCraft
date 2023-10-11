using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityHeadLookPacket : Packet
{

    public int EntityId;
    public sbyte HeadYaw;
    
    public EntityHeadLookPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        EntityId = stream.ReadVarInt();
        HeadYaw = stream.ReadAngle();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}