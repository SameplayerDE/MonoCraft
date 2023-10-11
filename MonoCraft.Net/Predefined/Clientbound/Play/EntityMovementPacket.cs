using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityMovementPacket : Packet
{

    public int EntityId;
    
    public EntityMovementPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        EntityId = stream.ReadVarInt();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}