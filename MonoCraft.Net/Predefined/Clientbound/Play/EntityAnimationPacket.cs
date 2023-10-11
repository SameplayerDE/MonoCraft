using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityAnimationPacket : Packet
{

    public int EntityId;
    public sbyte Animation;
    
    public EntityAnimationPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        EntityId = stream.ReadVarInt();
        Animation = stream.ReadSignedByte();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}