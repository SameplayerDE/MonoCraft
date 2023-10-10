namespace MonoCraft.Net.Predefined.Serverbound.Play;

public class ChatMessagePacket : Packet
{

    public string Message;
    
    public ChatMessagePacket() : base(0x03)
    {
    }

    public override void Decode(Stream stream)
    {
        throw new NotImplementedException();
    }

    public override void Encode(Stream stream)
    {
        stream.WriteString(Message);
    }
}