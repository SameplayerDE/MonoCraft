using MonoCraft.Net;
using MonoCraft.Net.Predefined.Enums;
using Clientbound = MonoCraft.Net.Predefined.Clientbound;
using Serverbound = MonoCraft.Net.Predefined.Serverbound;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;

namespace ConsoleClient
{
    public class PerfClient
    {

        private Socket _socket;
        private NetworkStream _networkStream;
        private Thread _readThread;

        private string _address;
        private ushort _port;

        public bool IsConnected => _socket.Connected;
        public int Available => _socket.Available;
        public ConnectionState ConnectionState;

        public PerfClient()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ConnectionState = ConnectionState.Handshake;
        }

        public void Connect(string address, ushort port)
        {
            _address = address;
            _port = port;
            try
            {
                _socket.Connect(address, port);

                if (IsConnected)
                {
                    _networkStream = new NetworkStream(_socket, true);
                    Handshake((int)MinecraftVersion.Ver_1_16_4, 2);
                    Login("Oktay");

                    _readThread = new Thread(Read);
                    _readThread.IsBackground = true;
                    _readThread.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private async void Read(object? obj)
        {
            try
            {
                while (IsConnected)
                {

                    if (_networkStream == null)
                    {
                        break;
                    }

                    if (Available > 10)
                    {
                        try
                        {
                            await ProcessPacket(new MemoryStream(ReceiveData(_networkStream.ReadVarInt())));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.Message}");
                        }
                    }
                    else
                    {
                        await Task.Delay(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private Task ProcessPacket(MemoryStream stream)
        {
            int packetId = stream.ReadVarInt();

            var packetType = PacketIdentifier.Instance.Identify(MinecraftVersion.Ver_1_16_4, PacketDirection.Clientbound, ConnectionState, packetId);

            if (packetType != MinecraftPacketType.NotImplemented)
            {
                try
                {
                    Console.WriteLine(packetType);
                    // Create a new instance of the packet class based on the packet type.
                    var type = PacketIdentifier.Instance.GetTypeByType(packetType);
                   
                    if (type != null)
                    {
                        var packet = (Packet)Activator.CreateInstance(type);
                        try
                        {
                            packet?.Decode(stream, MinecraftVersion.Ver_1_16_4);
                        }catch(Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }




            if (packetType == MinecraftPacketType.CB_Login_LoginSuccess)
            {
                ConnectionState = ConnectionState.Play;
            }

            if (packetType == MinecraftPacketType.CB_Play_KeepAlive)
            {
                Clientbound.Play.KeepAlivePacket request = new Clientbound.Play.KeepAlivePacket();
                request.Decode(stream, MinecraftVersion.Ver_1_16_4);

                MemoryStream responseStream = new MemoryStream();
                Serverbound.Play.KeepAlivePacket response = new Serverbound.Play.KeepAlivePacket();
                response.KeepAliveId = request.KeepAliveId;
                response.Encode(responseStream, MinecraftVersion.Ver_1_16_4);
                _networkStream.Write(responseStream.ToPacket().ToArray());
            }

            stream.Dispose();

            return Task.CompletedTask;
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
                    throw new IOException("Verbindung geschlossen, bevor genügend Daten empfangen wurden.");
                }
                totalBytesRead += bytesRead;
            }
            return buffer;
        }

        public void Handshake(int protocolVersion, int nextStep = 1)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(protocolVersion);
            stream.WriteString(_address);
            stream.WriteUnsignedShort(_port);
            stream.WriteVarInt(nextStep);

            if (nextStep == 1)
            {
                ConnectionState = ConnectionState.Status;
            }
            if (nextStep == 2)
            {
                ConnectionState = ConnectionState.Login;
            }

            _networkStream.Write(stream.ToPacket().ToArray());

            

        }

        public void Login(string name = "Deus")
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteString(name);
            _networkStream.Write(stream.ToPacket().ToArray());
        }

    }
}
