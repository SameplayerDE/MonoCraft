using fNbt;
using MonoCraft.Net.Predefined.Datatypes;
using System.Security.Cryptography;
using System.Text;

namespace MonoCraft.Net
{
    public static class StreamExtension
    {
        public static void WriteBool(this Stream stream, bool value)
        {
            stream.WriteByte(value ? (byte)0x01 : (byte)0x00);
        }
        public static void WriteSignedByte(this Stream stream, sbyte value) { throw new NotImplementedException(); }
        public static void WriteByte(this Stream stream, byte value)
        {
            stream.WriteByte(value);
        }
        public static void WriteShort(this Stream stream, short value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteUnsignedShort(this Stream stream, ushort value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteInt(this Stream stream, int value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteFloat(this Stream stream, float value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteDouble(this Stream stream, double value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteLong(this Stream stream, long value)
        {
            var data = BitConverter.GetBytes(value);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            stream.WriteBytes(data);
        }
        public static void WriteString(this Stream stream, string value)
        {
            stream.WriteVarInt(value.Length);
            //for (int i = 0; i < value.Length; i++)
            //{
            //    char @char = value[i];
            //    WriteByte(stream, Convert.ToByte(@char));
            //}
            stream.WriteBytes(Encoding.Default.GetBytes(value));
        }
        public static void WriteChat(this Stream stream, string value) { throw new NotImplementedException(); }
        public static void WriteIdentifier(this Stream stream, string value) { throw new NotImplementedException(); }
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
                WriteByte(stream, temp);
            } while (value != 0);
        }
        public static void WriteVarLong(this Stream stream, long value) { throw new NotImplementedException(); }
        public static void WriteEntityMetadata(this Stream stream, long value) { throw new NotImplementedException(); }
        public static void WriteSlot(this Stream stream, long value) { throw new NotImplementedException(); }
        public static void WriteNBTag(this Stream stream, long value) { throw new NotImplementedException(); }
        public static void WritePosition(this Stream stream, int x, int y, int z)
        {
            long result = ((x & 0x3FFFFFFL) << 38) | ((z & 0x3FFFFFFL) << 12) | (y & 0xFFF);
            byte[] byteArray = BitConverter.GetBytes(result);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(byteArray);
            }
        }
        public static void WritePosition(this Stream stream, Position position)
        {
            stream.WritePosition(position.X, position.Y, position.Z);
        }
        public static void WriteAngle(this Stream stream, sbyte value) { throw new NotImplementedException(); }
        public static void WriteUUID(this Stream stream, Guid value) { throw new NotImplementedException(); }

        public static bool ReadBool(this Stream stream)
        {
            var data = stream.ReadBytes(1);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToBoolean(data, 0);
        }
        public static sbyte ReadSignedByte(this Stream stream)
        {
            return (sbyte)stream.ReadByte();
        }
        public static byte ReadUByte(this Stream stream)
        {
            return (byte)stream.ReadByte();
        }
        public static short ReadShort(this Stream stream)
        {
            var data = stream.ReadBytes(2);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToInt16(data, 0);
        }
        public static ushort ReadUnsignedShort(this Stream stream) { throw new NotImplementedException(); }
        public static int ReadInt(this Stream stream)
        {
            var data = stream.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToInt32(data, 0);
        }
        public static float ReadFloat(this Stream stream)
        {
            var data = stream.ReadBytes(4);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToSingle(data, 0);
        }
        public static double ReadDouble(this Stream stream)
        {
            var data = stream.ReadBytes(8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToDouble(data, 0);
        }
        public static long ReadLong(this Stream stream)
        {
            var data = stream.ReadBytes(8);
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToInt64(data, 0);
        }
        public static string ReadString(this Stream stream)
        {
            int length = stream.ReadVarInt();
            byte[] chars = new byte[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = stream.ReadUByte();
            }
            return Encoding.Default.GetString(chars);
        }
        public static string ReadChat(this Stream stream) { throw new NotImplementedException(); }
        public static string ReadIdentifier(this Stream stream) { throw new NotImplementedException(); }
        public static int ReadVarInt(this Stream stream) 
        {
            int numRead = 0;
            int result = 0;
            byte read;
            do
            {
                read = stream.ReadUByte();
                int value = (read & 0b01111111);
                result |= (value << (7 * numRead));

                numRead++;
                if (numRead > 5)
                {
                    throw new Exception("VarInt is too big");
                }
            } while ((read & 0b10000000) != 0);
            return result;
        }
        public static long ReadVarLong(this Stream stream) { throw new NotImplementedException(); }
        public static long ReadEntityMetadata(this Stream stream) { throw new NotImplementedException(); }
        public static long ReadSlot(this Stream stream) { throw new NotImplementedException(); }
        public static NbtCompound ReadNBTag(this Stream stream)
        {
            return null;
        }
        public static (int, int, int) ReadPositionTuple(this Stream stream)
        {
            byte[] data = stream.ReadBytes(8);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                builder.Append($"{data[i]:x2}");
            }

            ulong var = Convert.ToUInt64(builder.ToString(), 16);

            ulong x = var & 0xFFFFFFC000000000;
            ulong y = var & 0x3FFFFFF000;
            ulong z = var & 0xFFF;

            int o_x;
            int o_y;
            int o_z;

            x = x >> 38;
            if ((x & 0x2000000) > 0)
            {
                x = x ^ 0x3FFFFFF;
                o_x = -Convert.ToInt32(x);
            }
            else
            {
                o_x = Convert.ToInt32(x);
            }


            y = y >> 12;
            if ((y & 0x2000000) > 0)
            {
                y = y ^ 0x3FFFFFF;
                o_y = -Convert.ToInt32(y);
            }
            else
            {
                o_y = Convert.ToInt32(y);
            }

            if ((z & 0x800) > 0)
            {
                z = z ^ 0xFFF;
                o_z = -Convert.ToInt32(z);
            }
            else
            {
                o_z = Convert.ToInt32(z);
            }

            //Console.WriteLine($"x: {o_x}/ y: {o_z} / z: {o_y}");
            return (o_x, o_z, o_y);
        }
        public static Position ReadPosition(this Stream stream)
        {
            (int, int, int) positionTuple = stream.ReadPositionTuple();
            (int x, int y, int z) = positionTuple;
            return new Position() {X = x, Y = y, Z = z};
        }
        public static sbyte ReadAngle(this Stream stream)
        {
            return stream.ReadSignedByte();
        }
        public static Guid ReadUUID(this Stream stream) { throw new NotImplementedException(); }
        public static byte[] ReadBytes(this Stream stream, int amount)
        {
            var buffer = new byte[amount];
            stream.Read(buffer, 0, amount);
            return buffer;
        }
        public static void WriteBytes(this Stream stream, byte[] value)
        {
            stream.Write(value, 0, value.Length);
        }
        public static sbyte[] ReadSignedBytes(this Stream stream, int amount) { throw new NotImplementedException(); }
        public static void WriteSignedBytes(this Stream stream, sbyte[] value) { throw new NotImplementedException(); }
        public static MemoryStream ToPacket(this MemoryStream stream)
        {
            byte[] content = stream.ToArray();
            var packet = new MemoryStream();
            packet.WriteVarInt(content.Length);
            packet.Write(content, 0, content.Length);
            return packet;
        }
    }
}
