using MonoCraft.Net;
using System.Collections.Concurrent;
using System.Data;
using System.IO.Compression;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MonoCraft.Net.Predefined.Enums;
using ConnectionState = MonoCraft.Net.Predefined.Enums.ConnectionState;

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

        public MinecraftVersion Version;
        public ConnectionState ConnectionState;
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
        public ConcurrentQueue<MemoryStream> InQueue { get; private set; } = new();

        public Player Player;

        private Semaphore _packetsToProcess = new Semaphore(0, 1);
        public object penis;

        public NetClient(MinecraftVersion version)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            OnConnectionEstablished += Init;
            Version = version;
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

        public void Connect(string address, ushort port)
        {
            _address = address;
            _port = port;
            //_ipAddress = IPAddress.Parse(_address);
            //_ipEndPoint = new IPEndPoint(_ipAddress, _port);

            _socket.Connect(_address, _port);

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

            Handshake((int)Version, 2);
            Login("Nicolas");
        }

        private void Process(object? obj)
        {
            while (IsConnected)
            {
                if (InQueue.Count > 0)
                {
                    try
                    {
                        if (InQueue.TryDequeue(out var packet))
                        {
                            if (packet == null)
                            {
                                continue;
                            }
                            PacketHandler.HandlePacket(packet, this);
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

        private async void Send(object? obj)
        {
            Console.WriteLine("Called Send");
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
                        await SendDataAsync(packet);
                        //packet.Dispose();
                    }
                }
                else
                {
                    Console.WriteLine("Nothing to send");
                    Thread.Sleep(10);
                }
            }
        }

        private async void Read(object? obj)
        {
            Console.WriteLine("Called Read");
            while (IsConnected)
            {
               //if (_networkStream != null)
               //{
               //    if (_networkStream.DataAvailable)
               //    {
               //        // Create a byte array to store the data that is read from the stream.
               //        byte[] data = new byte[_socket.Available];
               //
               //        // Read the data from the stream into the byte array.
               //        _networkStream.Read(data, 0, data.Length);
               //
               //        // Print the bytes in binary format to the console.
               //        foreach (byte b in data)
               //        {
               //            Console.WriteLine($"{b:X2}");
               //        }
               //    }
               //}
                if (_networkStream.DataAvailable)
                {

                    if (_socket.Available >= 10)
                    {
                        int packetLength = _networkStream.ReadVarInt();

                        if (CompressionThreshold >= 0)
                        {
                            if (packetLength >= CompressionThreshold)
                            {
                                // packet is compressed
                            }
                        }
                        await ReceiveDataAsync(packetLength);
                    }
                }
            }
        }

        public void SetPosition(double x, double y, double z, float yaw, float pitch)
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
                Console.WriteLine("Send Data To Server");
            }
        }

        public byte[] ReceiveData(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int totalBytesRead = 0;

            while (totalBytesRead < bufferSize)
            {
                int bytesRead = _networkStream.Read(buffer, totalBytesRead, bufferSize - totalBytesRead);
                if (bytesRead == 0)
                {
                    // Wenn Read() 0 Bytes zurückgibt, bedeutet dies, dass die Verbindung geschlossen wurde.
                    throw new IOException("Verbindung geschlossen, bevor genügend Daten empfangen wurden.");
                }
                totalBytesRead += bytesRead;
            }

            return buffer;
        }


        public async Task<byte[]> ReceiveDataAsync(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int totalBytesRead = 0;

            while (totalBytesRead < bufferSize)
            {
                int bytesRead = await _networkStream.ReadAsync(buffer, totalBytesRead, bufferSize - totalBytesRead);
                if (bytesRead == 0)
                {
                    // Wenn Read() 0 Bytes zurückgibt, bedeutet dies, dass die Verbindung geschlossen wurde.
                    throw new IOException("Verbindung geschlossen, bevor genügend Daten empfangen wurden.");
                }
                totalBytesRead += bytesRead;
                Console.WriteLine("Read {0} / {1}", totalBytesRead, bufferSize);
            }

            InQueue.Enqueue(new MemoryStream(buffer));
            return buffer;
        }

        public void Handshake(int protocolVersion, int nextStep = 1)
        {
            Console.WriteLine("Called Handshake");
            ConnectionState = ConnectionState.Handshake;
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(protocolVersion);
            stream.WriteString(_address);
            stream.WriteUnsignedShort(_port);
            stream.WriteVarInt(nextStep);
            OutQueue.Enqueue(stream.ToPacket());
            ConnectionState = ConnectionState.Login;
        }

        public void Login(string name = "Deus")
        {
            Console.WriteLine("Called Login");
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

        public void TeleportConfirm(int id)
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
