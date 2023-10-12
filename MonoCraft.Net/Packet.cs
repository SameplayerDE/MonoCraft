using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net;

public abstract class Packet : IPacket
{
    public Packet()
    {
    }
    public abstract void Decode(Stream stream, MinecraftVersion version);
    public abstract void Encode(Stream stream, MinecraftVersion version);
}