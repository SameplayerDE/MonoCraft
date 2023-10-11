using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class ChatMessagePacket : Packet
{

    public string JsonData;
    public byte Position;
    public Guid Sender;
    
    public ChatMessagePacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        JsonData = stream.ReadChat();
        Position = stream.ReadUByte();
        Sender = stream.ReadUUID();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}