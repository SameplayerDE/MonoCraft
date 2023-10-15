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

            int totalDurationInSeconds = 60 * 60 * 10; // Gesamtdauer in Sekunden
            int intervalInSeconds = 1 * 1000; // Intervall zwischen den Ausführungen in Sekunden

            List<int> playerCount = new();

            // Startzeitpunkt
            DateTime startTime = DateTime.Now;

            // Endzeitpunkt
            DateTime endTime = startTime.AddSeconds(totalDurationInSeconds);

            Server server = new Server("gommehd.net", 25565);

            while (DateTime.Now < endTime)
            {
                try
                {
                    PingAndQuery(server, playerCount);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                // Warten für das Intervall
                Thread.Sleep(intervalInSeconds);
            }

        }

        static void PingAndQuery(object state, List<int> playercount)
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

            if (!File.Exists(server.Name + ".csv"))
            {
                using (StreamWriter writer = new StreamWriter(File.Create(server.Name + ".csv")))
                {
                    writer.WriteLine("online,max,time,day");
                }
            }

            using (StreamWriter writer = File.AppendText(server.Name + ".csv"))
            {
                writer.WriteLine(r.PlayerList.Online + "," + r.PlayerList.Max + "," + DateTime.Now.ToString("HH:mm:ss.fff") + "," + DateTime.Now.ToString("dd.MM.yyyy"));
            }

            Console.Write($"[{r.PlayerList.Online} -> {DateTime.Now.ToString("HH:mm:ss.fff")}]: ".PadRight(30));


            //int starGroups = (int)Math.Ceiling((double)r.PlayerList.Online / 50);
            //
            //for (int i = 0; i < starGroups; i++)
            //{
            //    Console.Write("*");
            //}

            int stars = MapRange(r.PlayerList.Online, 0, r.PlayerList.Max, 0, 10);
            
            for (int i = 0; i < stars; i++)
            {
                Console.Write("*");
            }
            
            Console.WriteLine();

            playercount.Add(r.PlayerList.Online);
        }

        static int MapRange(int value, int fromMin, int fromMax, int toMin, int toMax)
        {
            // Sicherstellen, dass der Wert im ursprünglichen Bereich (fromMin bis fromMax) liegt
            value = Math.Max(value, fromMin);
            value = Math.Min(value, fromMax);

            // Lineare Abbildung des Werts auf den neuen Bereich (toMin bis toMax)
            return (int)Math.Round((double)(value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin);
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
