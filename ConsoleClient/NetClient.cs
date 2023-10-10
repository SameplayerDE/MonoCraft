using MonoCraft.Net;
using System.Collections.Concurrent;
using System.Data;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Xml.Linq;

namespace ConsoleClient
{
    class Player
    {
        public double X, Y, Z;
        public double VelX, VelZ;
        public float Yaw, Pitch;
    }

    class PacketStream
    {
        public int PacketLength;
        public byte[] Data;
    }

    internal class NetClient
    {
        private Thread _readThread;
        private Thread _sendThread;
        private Thread _processThread;

        private Socket _socket;
        private NetworkStream _networkStream;

        private ushort _port;
        private string _address;

        private IPAddress _ipAddress;
        private IPEndPoint _ipEndPoint;

        public bool IsConnected { get; private set; }
        public Action OnConnectionEstablished;
        public Action OnServerTick;
        public int CompressionThreshold = -1;

        public ConcurrentQueue<MemoryStream> OutQueue { get; private set; } = new();
        public ConcurrentQueue<PacketStream> InQueue { get; private set; } = new();

        public Player Player;

        private Semaphore _packetsToProcess = new Semaphore(0, 1);
        public object penis;

        public NetClient()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            OnConnectionEstablished += Init;
        }

        public async Task ConnectAsync(string address, ushort port)
        {
            _address = address;
            _port = port;
            //_ipAddress = IPAddress.Parse(_address);
            //_ipEndPoint = new IPEndPoint(_ipAddress, _port);

            await _socket.ConnectAsync(_address, _port);

            if (_socket.Connected)
            {
                IsConnected = true;
                _networkStream = new NetworkStream(_socket, true);
                OnConnectionEstablished?.Invoke();
            }
        }

        public async Task DisconnectAsync()
        {
            await _socket.DisconnectAsync(false);
            IsConnected = false;
        }

        private void Init()
        {
            _readThread = new Thread(Read);
            _readThread.IsBackground = true;
            _readThread.Start();

            _sendThread = new Thread(Send);
            _sendThread.IsBackground = true;
            _sendThread.Start();

            _processThread = new Thread(Process);
            _processThread.IsBackground = true;
            _processThread.Start();

            var name = Console.ReadLine();
            Handshake(754, 2);
            Login(name);
        }

        private void Process(object? obj)
        {
            while (IsConnected)
            {
                if (InQueue.Count > 0)
                {
                    try
                    {
                        if (InQueue.TryDequeue(out var data))
                        {
                            if (data == null)
                            {
                                continue;
                            }

                            var packet = new MemoryStream(data.Data);
                            bool isCompressed = false;
                            int dataLength = 0;

                            if (CompressionThreshold >= 0)
                            {
                                if (data.PacketLength >= CompressionThreshold)
                                {
                                    dataLength = packet.ReadVarInt();
                                    isCompressed = dataLength > 0;
                                }
                            }

                            if (isCompressed)
                            {
                                //// Create a ZLibStream with the MemoryStream as the source stream.
                                //ZLibStream zlibStream = new ZLibStream(new MemoryStream(data.Data), CompressionMode.Decompress);
                                //
                                //// Read the decompressed data from the ZLibStream.
                                //byte[] decompressedData = new byte[zlibStream.Length];
                                //zlibStream.Read(decompressedData, 0, decompressedData.Length);

                                //using (MemoryStream compressedStream = new MemoryStream(packet.ReadBytes(10)))
                                //{
                                //    using (MemoryStream decompressedStream = new MemoryStream())
                                //    {
                                //        using (DeflateStream deflateStream = new DeflateStream(compressedStream, CompressionMode.Decompress))
                                //        {
                                //            deflateStream.CopyTo(decompressedStream);
                                //        }
                                //        Console.WriteLine("{0:x2}", decompressedStream.ReadVarInt());
                                //    }
                                //}
                            }
                            else
                            {
                                PacketHandler.HandlePacket(packet, this); //Uncompressed
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        IsConnected = false;
                        _socket.Disconnect(false);
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void Send(object? obj)
        {
            while (IsConnected)
            {
                if (OutQueue.Count > 0)
                {
                    if (OutQueue.TryDequeue(out var packet))
                    {
                        if (CompressionThreshold >= 0)
                        {
                            if (packet.Length >= CompressionThreshold)
                            {
                                // packet needs to be compressed
                            }
                        }
                        SendDataAsync(packet);
                        //packet.Dispose();
                    }
                }
                else
                {
                    Thread.Sleep(1);
                }
            }
        }

        private void Read(object? obj)
        {
            while (IsConnected)
            {
                if (_networkStream.DataAvailable)
                {
                    int packetLength = _networkStream.ReadVarInt();

                    if (CompressionThreshold >= 0)
                    {
                        if (packetLength >= CompressionThreshold)
                        {
                            // packet is compressed
                        }
                    }

                    ReceiveDataAsync2(packetLength);
                }
            }
        }

        private void SetPosition(double x, double y, double z, float yaw, float pitch)
        {
            Player.X = x;
            Player.Y = y;
            Player.Z = z;
            Player.Yaw = yaw;
            Player.Pitch = pitch;
        }


        public async Task SendDataAsync(byte[] data)
        {
            await _networkStream.WriteAsync(data, 0, data.Length);
        }

        public async Task SendDataAsync(MemoryStream memoryStream)
        {
            if (memoryStream != null)
            {
                byte[] data = memoryStream.ToArray();
                await _networkStream.WriteAsync(data, 0, data.Length);
            }
        }

        public async Task<byte[]> ReceiveDataAsync(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int bytesRead = await _networkStream.ReadAsync(buffer, 0, bufferSize);
            if (bytesRead > 0)
            {
                byte[] receivedData = new byte[bytesRead];
                Array.Copy(buffer, receivedData, bytesRead);
                InQueue.Enqueue(new PacketStream()
                {
                    Data = receivedData,
                    PacketLength = bufferSize
                });
                return receivedData;
            }
            return null;
        }

        public async Task<byte[]> ReceiveDataAsync2(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int totalBytesRead = 0;

            while (totalBytesRead < bufferSize)
            {
                int bytesRead = await _networkStream.ReadAsync(buffer, totalBytesRead, bufferSize - totalBytesRead);

                if (bytesRead <= 0)
                {
                    // Wenn keine Daten mehr gelesen werden können, breche ab.
                    break;
                }

                totalBytesRead += bytesRead;
            }

            if (totalBytesRead > 0)
            {
                byte[] receivedData = new byte[totalBytesRead];
                Array.Copy(buffer, receivedData, totalBytesRead);
                InQueue.Enqueue(new PacketStream()
                {
                    Data = receivedData,
                    PacketLength = totalBytesRead
                });
                return receivedData;
            }
            return null;
        }


        private void Handshake(int protocolVersion, int nextStep = 1)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(protocolVersion);
            stream.WriteString(_address);
            stream.WriteUnsignedShort(_port);
            stream.WriteVarInt(nextStep);
            OutQueue.Enqueue(stream.ToPacket());
        }

        private void Login(string name = "Deus")
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteString(name);
            OutQueue.Enqueue(stream.ToPacket());
        }

       public void KeepAlive(long id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x10);
            stream.WriteLong(id);
            OutQueue.Enqueue(stream.ToPacket());
        }

        private void TeleportConfirm(int id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(id);

            OutQueue.Enqueue(stream.ToPacket());
        }

        public void SendPosition(Player player)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x12);
            stream.WriteDouble(player.X);
            stream.WriteDouble(player.Y);
            stream.WriteDouble(player.Z);
            stream.WriteBool(true);
            OutQueue.Enqueue(stream.ToPacket());
        }

        public void SwingArm(int id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x2C);
            stream.WriteVarInt(id);
            OutQueue.Enqueue(stream.ToPacket());
        }

        public void Chat(string message)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x03);
            stream.WriteString(message);
            OutQueue.Enqueue(stream.ToPacket());
        }

    }
}
