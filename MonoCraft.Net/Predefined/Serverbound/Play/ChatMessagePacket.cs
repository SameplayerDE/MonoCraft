using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class ChatMessagePacket : Packet
{

    public string Message;
    
    public ChatMessagePacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        stream.WriteString(Message);
    }
}