using MonoCraft.Net;
using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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

        public PerfClient()
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
            while (IsConnected)
            {
                if (Available > 10)
                {
                    PacketHandler.HandlePacket(new MemoryStream(ReceiveData(_networkStream.ReadVarInt())));
                }
                else
                {
                    await Task.Delay(1);
                }
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
