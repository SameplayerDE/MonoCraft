using System.Net;
using System.Net.Sockets;

namespace ConsoleClient
{
    internal class Client
    {

        private Thread _receivingThread;
        private Thread _sendingThread;
        private Thread _processThread;

        private Socket _client;
        private NetworkStream _stream;
        private BufferedStream _buffer;
        private string _adrr;
        private ushort _port;

        public EventHandler<NetworkReceivedEventArgs> OnDataReceived;
        public bool IsConnected = false;
        public int Available => _client.Available;
        public BufferedStream Stream => GetStream();

        public Queue<byte[]> OutQueue = new();

        public Client(string adress, ushort port)
        {
            _adrr = adress;
            _port = port;
            _client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public async Task SendDataAsync(byte[] data)
        {
            await GetStream().WriteAsync(data, 0, data.Length);
        }

        public async Task<byte[]> ReceiveDataAsync(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead = await GetStream().ReadAsync(buffer, 0, bufferSize);
            if (bytesRead > 0)
            {
                byte[] receivedData = new byte[bytesRead];
                Array.Copy(buffer, receivedData, bytesRead);
                return receivedData;
            }
            return null;
        }

        public void Connect()
        {
            Task connectionTask = _client.ConnectAsync(_adrr, _port);
            while (!connectionTask.IsCompleted)
            {
                Console.Write(".");
                Thread.Sleep(500);
            }
            if (connectionTask.IsFaulted)
            {
                Console.WriteLine("\nCould not connect to server.");
                return;
            }
            else
            {
                Console.WriteLine("\nConnected to server.");
                IsConnected = true;
            }
            _client.ReceiveBufferSize = short.MaxValue;
            _client.SendBufferSize = 256;

            _stream = new NetworkStream(_client, true);
            _buffer = new BufferedStream(_stream);

            Handshake(_buffer);
            Login(_buffer);


            //
            //byte[] networkBuffer = new byte[4096 * 2];
            //
            //while (IsConnected)
            //{
            //    if (stream.DataAvailable)
            //    {
            //        int availableBytes = stream.Read(networkBuffer, 0, networkBuffer.Length);
            //        Console.WriteLine("Read {0} bytes", availableBytes);
            //        OnDataReceived?.Invoke(this, new NetworkReceivedEventArgs(networkBuffer, availableBytes));
            //    }
            //}
        }

        static void Handshake(Stream ns)
        {
            byte[] byteArray = new byte[] { 0x10, 0x00, 0xF2, 0x05, 0x09, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x68, 0x6F, 0x73, 0x74, 0x00, 0x00, 0x02 };
            ns.Write(byteArray, 0, byteArray.Length);
            ns.Flush();
        }

        static void Login(Stream ns)
        {
            byte[] byteArray = new byte[] { 0x06, 0x00, 0x04, 0x44, 0x65, 0x75, 0x73 };
            ns.Write(byteArray, 0, byteArray.Length);
            ns.Flush();
        }

        private void SendingMethod(object? obj)
        {

        }

        private void ReceivingMethod(object? obj)
        {
        }

        public void WriteFloat(Stream stream, float data)
        {
            stream.Write(HostToNetworkOrder(data));
        }

        public void WriteBoolean(Stream stream, bool value)
        {
            stream.WriteByte((value) ? (byte)0x01 : (byte)0x00);
        }

        public void WriteAngle(Stream stream, byte value)
        {
            stream.WriteByte(value);
        }

        private double NetworkToHostOrder(byte[] data)
        {
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }
            return BitConverter.ToDouble(data, 0);
        }

        public double ReadDouble(Stream stream)
        {
            return NetworkToHostOrder(ReadUnsignedBytes(stream, 8));
        }

        public float ReadFloat(Stream stream)
        {
            float value = BitConverter.ToSingle(ReadUnsignedBytes(stream, 4), 0);
            return NetworkToHostOrder(value);
        }

        public byte ReadUnsignedByte(Stream stream)
        {
            byte value = (byte)stream.ReadByte();
            return value;
        }

        private byte[] ReadUnsignedBytes(Stream stream, int count)
        {
            byte[] bytes = new byte[count];
            for (int i = 0; i < count; i++)
            {
                bytes[i] = ReadUnsignedByte(stream);
            }
            return bytes;
        }

        private float NetworkToHostOrder(float network)
        {
            var bytes = BitConverter.GetBytes(network);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToSingle(bytes, 0);
        }

        public void WriteDouble(Stream stream, double value)
        {
            WriteBytes(stream, BitConverter.GetBytes(value));
        }

        private byte[] HostToNetworkOrder(float host)
        {
            var bytes = BitConverter.GetBytes(host);

            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return bytes;
        }

        public long ReadLong(Stream stream)
        {
            byte[] data = new byte[8]; // 8 bytes because 64 bit value
            stream.Read(data, 0, data.Length);
            long value = BitConverter.ToInt64(data, 0);
            return IPAddress.NetworkToHostOrder(value);
        }

        public int ReadVarInt(byte[] buffer)
        {
            int bytesRead = 0;
            return ReadVarInt(buffer, out bytesRead);
        }

        public int ReadVarInt(byte[] buffer, out int bytesRead)
        {
            int numRead = 0;
            int result = 0;
            byte read;
            do
            {
                read = (byte)buffer[numRead];
                int value = (read & 0x7f);
                result |= (value << (7 * numRead));

                numRead++;
                if (numRead > 5)
                {
                    throw new Exception("VarInt is too big");
                }
            } while ((read & 0x80) != 0);
            bytesRead = numRead;
            return result;
        }

        public int ReadVarInt(Stream stream)
        {
            int bytesRead = 0;
            return ReadVarInt(stream, out bytesRead);
        }

        public byte[] ReadBytes(Stream stream, int count)
        {
            int read = 0;
            byte[] buffer = new byte[count];
            while (read < buffer.Length)
            {
                int readBytes = stream.Read(buffer, read, count - read);
                if (readBytes < 0) //No data read?
                {
                    break;
                }
                read += readBytes;
            }

            return buffer;
        }

        public int ReadVarInt(Stream stream, out int bytesRead)
        {
            int numRead = 0;
            int result = 0;
            byte read;
            do
            {
                read = (byte)stream.ReadByte();
                int value = (read & 0x7f);
                result |= (value << (7 * numRead));

                numRead++;
                if (numRead > 5)
                {
                    throw new Exception("VarInt is too big");
                }
            } while ((read & 0x80) != 0);
            bytesRead = numRead;
            return result;
        }

        public void WriteVarInt(Stream stream, int value)
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

        public void WriteBytes(Stream stream, byte[] value)
        {
            foreach (byte @byte in value)
            {
                stream.WriteByte(@byte);
            }
        }

        public void WriteLong(Stream stream, long value)
        {
            value = IPAddress.HostToNetworkOrder(value);
            byte[] data = BitConverter.GetBytes(value);
            WriteBytes(stream, data);
        }

        internal BufferedStream GetStream()
        {
            return _buffer;
        }
    }
}
