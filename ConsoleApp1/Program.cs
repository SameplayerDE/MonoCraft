using System;
using System.Formats.Asn1;
using System.Net;
using System.Net.Sockets;
using System.Text;
using MonoCraft.Net;
using MonoCraft.Net.Predefined;
using MonoCraft.Net.Predefined.Enums;
using Newtonsoft.Json;
using CsvHelper;
using Spectre.Console;

namespace MinecraftServerStatus
{
    class Program
    {

        public static byte[] ReceiveData(Stream stream, int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            int totalBytesRead = 0;

            while (totalBytesRead < bufferSize)
            {
                int bytesRead = stream.Read(buffer, totalBytesRead, bufferSize - totalBytesRead);
                if (bytesRead == 0)
                {
                    throw new IOException("Verbindung geschlossen, bevor genügend Daten empfangen wurden.");
                }
                totalBytesRead += bytesRead;
            }
            return buffer;
        }

        static async Task Main(string[] args)
        {

            int totalDurationInSeconds = 60 * 10; // Gesamtdauer in Sekunden
            int intervalInSeconds = 100; // Intervall zwischen den Ausführungen in Sekunden

            // Startzeitpunkt
            DateTime startTime = DateTime.Now;

            // Endzeitpunkt
            DateTime endTime = startTime.AddSeconds(totalDurationInSeconds);

            while (DateTime.Now < endTime)
            {
                // Führen Sie Ihre Methode hier aus.
                PingAndQuery(new Server("gommehd.net", 25565));

                Console.WriteLine("ping");

                // Warten für das Intervall
                Thread.Sleep(intervalInSeconds);
            }

        }

        static void PingAndQuery(object state)
        {
            Server server = (Server)state;

            // Verbindung zum Server herstellen
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(server.IPAddress, server.Port);

            ServerStatusResponse r = null;

            NetworkStream stream = new NetworkStream(socket, true);

            var handshake = new MemoryStream();
            handshake.WriteVarInt(0x00);
            handshake.WriteVarInt((int)MinecraftVersion.Ver_1_16_4);
            handshake.WriteString(server.IPAddress.ToString());
            handshake.WriteUnsignedShort((ushort)server.Port);
            handshake.WriteVarInt(0X01);
            stream.Write(handshake.ToPacket().ToArray());

            var request = new MemoryStream();
            request.WriteVarInt(0x00);
            stream.Write(request.ToPacket().ToArray());

            bool readResponse = false;

            while (!readResponse)
            {
                if (socket.Available >= 4)
                {
                    int packetLength = stream.ReadVarInt();
                    var buffer = ReceiveData(stream, packetLength);

                    var response = new MemoryStream(buffer);
                    int id = response.ReadVarInt();
                    string json = response.ReadString();

                    r = JsonConvert.DeserializeObject<ServerStatusResponse>(json);

                    readResponse = true;
                }
            }

            long payloadSent = 10;

            var ping = new MemoryStream();
            ping.WriteVarInt(0x01);
            ping.WriteLong(payloadSent);
            stream.Write(ping.ToPacket().ToArray());

            bool readPong = false;

            while (!readPong)
            {
                if (socket.Available >= 4)
                {
                    int packetLength = stream.ReadVarInt();
                    var buffer = ReceiveData(stream, packetLength);

                    var response = new MemoryStream(buffer);
                    int id = response.ReadVarInt();
                    long payloadReceived = response.ReadLong();

                    if (payloadReceived == payloadSent)
                    {
                        socket.Close();
                    }
                    readPong = true;
                }
            }

            if (!File.Exists(server.Name + ".txt"))
            {
                using (File.Create(server.Name + ".txt")) { }
            }

            using (StreamWriter writer = File.AppendText(server.Name + ".txt"))
            {
                writer.WriteLine(r.PlayerList.Online);
            }
        }

        class Server
        {
            public string Name { get; set; }
            public IPAddress IPAddress { get; set; }
            public int Port { get; set; }

            public Server(string name, int port)
            {
                Name = name;

                if (IPAddress.TryParse(name, out var address))
                {
                    IPAddress = address;
                }
                else
                {
                    IPAddress = Dns.GetHostAddresses(name)[0];
                }

                Port = port;
            }
        }
    }
}
