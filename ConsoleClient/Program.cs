using ConsoleClient;
using System.Net.Sockets;

Client client = new Client("localhost", 25565);
client.Connect();

int packetLength = 0;
int packetId = -1;
int count = 0;

var receiveTask = Task.Run(async () =>
{
    try
    {
        while (client.IsConnected)
        {
            if (client.Available > 0)
            {
                packetLength = client.ReadVarInt(client.Stream);
                byte[] receivedData = await client.ReceiveDataAsync(packetLength);

                MemoryStream memoryStream = new MemoryStream(receivedData);
                packetId = client.ReadVarInt(memoryStream);
                count++;

                if (packetId == 0x1F)
                {
                    long keepAliveId = client.ReadLong(memoryStream);
                    Console.WriteLine("keep-alive from server [{0}]", keepAliveId);

                    var stream = new MemoryStream();
                    client.WriteVarInt(stream, 0x09);
                    client.WriteVarInt(stream, 0x10);
                    client.WriteLong(stream, keepAliveId);

                    //client.OutQueue.Enqueue(stream.ToArray());

                    var task = client.SendDataAsync(stream.ToArray());
                    if (task.IsCompleted)
                    {
                        client.GetStream().Flush();
                    }
                }

                if (packetId == 0x0E)
                {
                    Console.WriteLine("chat-message from server");


                }

                if (packetId == 0x34)
                {
                    Console.WriteLine("position-look from server");

                    double x = client.ReadDouble(memoryStream); // x
                    double y = client.ReadDouble(memoryStream); // y
                    double z = client.ReadDouble(memoryStream); // z
                    float yaw = client.ReadFloat(memoryStream); // yaw
                    float pitch = client.ReadFloat(memoryStream); // pitch
                    byte flags = client.ReadUnsignedByte(memoryStream); // flags
                    
                    var stream = new MemoryStream();
                    client.WriteVarInt(stream, 2);
                    client.WriteVarInt(stream, 0x00);
                    client.WriteVarInt(stream, client.ReadVarInt(memoryStream));

                    //client.OutQueue.Enqueue(stream.ToArray());
                    var task = client.SendDataAsync(stream.ToArray());
                    if (task.IsCompleted)
                    {
                        client.GetStream().Flush();
                    }

                }
            }
        }
    }catch(Exception e)
    {
        Console.WriteLine($"Error: {e}");
    }
});

await Task.WhenAll(receiveTask);