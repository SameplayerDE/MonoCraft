namespace MonoCraft.Net.Predefined.Clientbound.Play;

public class DestroyEntitiesPacket : Packet
{

    public int Count;
    public int[] EntityIds;
    
    public DestroyEntitiesPacket() : base(0x36)
    {
    }

    public override void Decode(Stream stream)
    {
        Count = stream.ReadVarInt();
        EntityIds = new int[Count];
        for (int i = 0; i < Count; i++)
        {
            EntityIds[i] = stream.ReadVarInt();
        }
    }

    public override void Encode(Stream stream)
    {
        throw new NotImplementedException();
    }
}