using MonoCraft.Net.Predefined.Enums;

namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class DisconnectPacket : Packet
{

    public string Reason;
    
    public DisconnectPacket() : base()
    {
    }

    public override void Decode(Stream stream, MinecraftVersion version)
    {
        Reason = stream.ReadChat();
    }

    public override void Encode(Stream stream, MinecraftVersion version)
    {
        throw new NotImplementedException();
    }
}