namespace MonoCraft.Net;

public static class StreamExtender
{
    
    public static void WriteBytes(this Stream stream, byte[] value)
    {
        stream.Write(value, 0, value.Length);
    }

    public static void WriteBool(this Stream stream, bool value)
    {
        stream.WriteByte(value ? (byte)1 : (byte)0);
    }

    public static void WriteShort(this Stream stream, short value)
    {
        byte mostSignificantByte = (byte)(value >> 8);
        byte leastSignificantByte = (byte)(value & 0xFF);
        stream.WriteByte(mostSignificantByte);
        stream.WriteByte(leastSignificantByte);
    }
    
    public static void WriteFloat(this Stream stream, float value)
    {
        stream.WriteBytes(BitConverter.GetBytes(value));
    }
    
    public static void WriteVarInt(this Stream stream, int value)
    {
        do
        {
            byte temp = (byte)(value & 0b01111111);
            value >>= 7;
            if (value != 0)
            {
                temp |= 0b10000000;
            }
            stream.WriteByte(temp);
        } while (value != 0);
    }
}