using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net;

public class PacketIdentifier
{
    public static PacketIdentifier Instance { get; } = new();

    private Dictionary<(MinecraftVersion, ConnectionState, int), MinecraftPacketType> _map = new();
    
    static PacketIdentifier()
    {
        
    }

    private PacketIdentifier()
    {
        Map(MinecraftVersion.Ver_1_20_2, ConnectionState.Login, 0x03, MinecraftPacketType.CB_Login_SetCompression);
    }

    public void Map(MinecraftVersion version, ConnectionState connectionState, int packetId, MinecraftPacketType type)
    {
        _map[(version, connectionState, packetId)] = type;
    }

    public MinecraftPacketType Identify(MinecraftVersion version, ConnectionState connectionState, int packetId)
    {
        if (_map.TryGetValue((version, connectionState, packetId), out var packetType))
        {
            return packetType;
        }
        return MinecraftPacketType.NotImplemented;
        throw new Exception("packet with this id does not exist for this minecraft version");
    }
    
}