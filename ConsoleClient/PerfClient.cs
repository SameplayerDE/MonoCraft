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

        private TcpClient _client;
        private Thread _readThread;
        private string _address;
        private ushort _port;

        public bool IsConnected => _client.Connected;

        public PerfClient()
        {
            _client = new TcpClient();
        }

        public void Connect(string address, ushort port)
        {
            _address = address;
            _port = port;
            try
            {
                _client.Connect(address, port);

                if (_client.Connected)
                {

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
            while (_client.Connected)
            {
                if (_client.Available > 10)
                {
                    int available = _client.Available;
                    byte[] buffer = new byte[available];
                    _client.GetStream().Read(buffer, 0, available);
                    foreach (byte b in buffer)
                    {
                        Console.WriteLine($"{Convert.ToString(b, 2)}");
                    }
                }
                else
                {
                    await Task.Delay(1);
                }
            }
        }

        public void Handshake(int protocolVersion, int nextStep = 1)
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteVarInt(protocolVersion);
            stream.WriteString(_address);
            stream.WriteUnsignedShort(_port);
            stream.WriteVarInt(nextStep);
            _client.GetStream().Write(stream.ToPacket().ToArray());
        }

        public void Login(string name = "Deus")
        {
            var stream = new MemoryStream();
            stream.WriteVarInt(0x00);
            stream.WriteString(name);
            _client.GetStream().Write(stream.ToPacket().ToArray());
        }

    }
}
