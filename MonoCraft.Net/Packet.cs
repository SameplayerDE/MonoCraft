namespace MonoCraft.Net;

public abstract class Packet : IPacket
{

    public int Identifier;

    public Packet(int identifier)
    {
        Identifier = identifier;
    }

    public abstract void Decode(Stream stream);
    public abstract void Encode(Stream stream);
}