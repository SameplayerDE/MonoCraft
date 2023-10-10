namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class PlayerPositionLookPacket : Packet
{

    public double X;
    public double Y;
    public double Z;
    public float Yaw;
    public float Pitch;
    public byte Flags;
    public int TeleportId;
    
    public PlayerPositionLookPacket() : base(0x34)
    {
    }

    public override void Decode(Stream stream)
    {
        X = stream.ReadDouble();
        Y = stream.ReadDouble();
        Z = stream.ReadDouble();
        Yaw = stream.ReadFloat();
        Pitch = stream.ReadFloat();
        Flags = stream.ReadUByte();
        TeleportId = stream.ReadVarInt();
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}