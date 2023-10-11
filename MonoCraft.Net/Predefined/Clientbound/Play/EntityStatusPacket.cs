using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class EntityStatusPacket : Packet
{

    public int EntityId;
    public byte EntityStatus;
    
    public EntityStatusPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        EntityId = stream.ReadInt();
        EntityStatus = stream.ReadUByte();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}