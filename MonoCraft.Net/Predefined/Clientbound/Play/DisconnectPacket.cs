namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class DisconnectPacket : Packet
{

    public string Reason;
    
    public DisconnectPacket() : base(0x19)
    {
    }

    public override void Decode(Stream stream)
    {
        Reason = stream.ReadChat();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}