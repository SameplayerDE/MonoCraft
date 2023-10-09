using MonoCraft.Net;
using System.Collections.Concurrent;
using System.Data;
using System.Net;
using System.Net.Sockets;

namespace ConsoleClient
{
    class Player
    {
        public double X, Y, Z;
        public double VelX, VelZ;
        public float Yaw, Pitch;
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

        public ConcurrentQueue<MemoryStream> OutQueue { get; private set; } = new();
        public ConcurrentQueue<byte[]> InQueue { get; private set; } = new();

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
            Handshake();
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

                            var packet = new MemoryStream(data);
                            int packetId = packet.ReadVarInt();

                            if (packetId == 0x1F)
                            {
                                long keepAliveId = packet.ReadLong();
                                //Console.WriteLine("keep-alive from server [{0}]", keepAliveId);
                                KeepAlive(keepAliveId);
                            }

                            if (packetId == 0x19)
                            {
                                string reason = packet.ReadString();
                                //Console.WriteLine("disconnect from server [{0}]", reason);
                            }

                            if (packetId == 0x0E)
                            {
                                string message = packet.ReadString();
                                Console.WriteLine("chat-message from server [{0}]", message);
                            }

                            if (packetId == 0x34)
                            {
                                double x = packet.ReadDouble();
                                double y = packet.ReadDouble();
                                double z = packet.ReadDouble();
                                float yaw = packet.ReadFloat();
                                float pitch = packet.ReadFloat();
                                byte flags = packet.ReadUByte();
                                int teleportId = packet.ReadVarInt();

                                if (Player == null)
                                {
                                    Player = new Player();
                                    Player.VelX = 0.6;
                                    Player.VelZ = 0.2;
                                }

                                SetPosition(x, y, z, yaw, pitch);

                                //Console.WriteLine("player-position-rotation from server [{0}]", teleportId);

                                TeleportConfirm(teleportId);
                            }

                            if (packetId == 0x4E)
                            {
                                long worldAge = packet.ReadLong();
                                long timeOfDay = packet.ReadLong();
                                //Console.WriteLine("time-update from server [{0}, {1}]", worldAge, timeOfDay);

                                SendPosition(Player);
                                OnServerTick?.Invoke();
                            }

                            packet.Dispose();
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
            while (IsConnected)
            {
                if (OutQueue.Count > 0)
                {
                    if (OutQueue.TryDequeue(out var packet))
                    {
                        await SendDataAsync(packet);
                        packet.Dispose();
                    }
                }
            }
        }

        private async void Read(object? obj)
        {
            while (IsConnected)
            {
                if (_networkStream.DataAvailable)
                {
                    int packetLength = _networkStream.ReadVarInt();
                    var receivedData = await ReceiveDataAsync(packetLength);
                    InQueue.Enqueue(receivedData);
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
                return receivedData;
            }
            return null;
        }

        private void Handshake()
        {
            var stream = new MemoryStream();
            byte[] byteArray = new byte[] { 0x10, 0x00, 0xF2, 0x05, 0x09, 0x6C, 0x6F, 0x63, 0x61, 0x6C, 0x68, 0x6F, 0x73, 0x74, 0x00, 0x00, 0x02 };
            stream.Write(byteArray, 0, byteArray.Length);
            OutQueue.Enqueue(stream);
        }

        private void Login(string name = "Deus")
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(2 + name.Length);
            stream.WriteVarInt(0x00);
            stream.WriteString(name);
            OutQueue.Enqueue(stream);
        }

        private void KeepAlive(long id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x09);
            stream.WriteVarInt(0x10);
            stream.WriteLong(id);
            OutQueue.Enqueue(stream);
        }

        private void TeleportConfirm(int id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x02);
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(id);
            OutQueue.Enqueue(stream);
        }

        public void SendPosition(Player player)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(26);
            stream.WriteVarInt(0x12);
            stream.WriteDouble(player.X);
            stream.WriteDouble(player.Y);
            stream.WriteDouble(player.Z);
            stream.WriteBool(true);
            OutQueue.Enqueue(stream);
        }

        public void SwingArm(int id)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(2);
            stream.WriteVarInt(0x2C);
            stream.WriteVarInt(id);
            OutQueue.Enqueue(stream);
        }

        public void Chat(string message)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(2 + message.Length);
            stream.WriteVarInt(0x03);
            stream.WriteString(message);
            OutQueue.Enqueue(stream);
        }

    }
}
