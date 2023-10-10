namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class ChatMessagePacket : Packet
{

    public string JsonData;
    public byte Position;
    public Guid Sender;
    
    public ChatMessagePacket() : base(0x0E)
    {
    }

    public override void Decode(Stream stream)
    {
        JsonData = stream.ReadChat();
        Position = stream.ReadUByte();
        Sender = stream.ReadUUID();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}