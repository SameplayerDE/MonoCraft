namespace MonoCraft.Net
{
    public interface IStreamExtension
    {
        void WriteBool(Stream stream, bool value);
        void WriteSignedByte(Stream stream, sbyte value);
        void WriteByte(Stream stream, byte value);
        void WriteShort(Stream stream, short value);
        void WriteUnsignedShort(Stream stream, ushort value);
        void WriteInt(Stream stream, int value);
        void WriteFloat(Stream stream, float value);
        void WriteDouble(Stream stream, double value);
        void WriteLong(Stream stream, long value);
        void WriteString(Stream stream, string value);
        void WriteChat(Stream stream, string value);
        void WriteIdentifier(Stream stream, string value);
        void WriteVarInt(Stream stream, int value);
        void WriteVarLong(Stream stream, long value);
        void WriteEntityMetadata(Stream stream, long value);
        void WriteSlot(Stream stream, long value);
        void WriteNBTag(Stream stream, long value);
        void WritePosition(Stream stream, long value);
        void WriteAngle(Stream stream, long value);
        void WriteUUID(Stream stream, long value);

        bool ReadBool(Stream stream);
        sbyte ReadSignedByte(Stream stream);
        byte ReadByte(Stream stream);
        short ReadShort(Stream stream);
        ushort ReadUnsignedShort(Stream stream);
        int ReadInt(Stream stream);
        float ReadFloat(Stream stream);
        double ReadDouble(Stream stream);
        long ReadLong(Stream stream);
        string ReadString(Stream stream);
        string ReadChat(Stream stream);
        string ReadIdentifier(Stream stream);
        int ReadVarInt(Stream stream);
        long ReadVarLong(Stream stream);
        long ReadEntityMetadata(Stream stream);
        long ReadSlot(Stream stream);
        long ReadNBTag(Stream stream);
        long ReadPosition(Stream stream);
        long ReadAngle(Stream stream);
        long ReadUUID(Stream stream);

        byte[] ReadBytes(Stream stream, int amount);
        void WriteBytes(Stream stream, byte[] value);
        sbyte[] ReadSignedBytes(Stream stream, int amount);
        void WriteSignedBytes(Stream stream, sbyte[] value);
    }
}