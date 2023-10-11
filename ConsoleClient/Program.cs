using ConsoleClient;
using System;
using MonoCraft.Net.Predefined.Enums;

NetClient client = new NetClient(MinecraftVersion.Ver_1_20_1);
client.OnConnectionEstablished += Init;
//client.OnServerTick += () => client.Chat("Tick");

await client.ConnectAsync("forgeunited.org", 25565);

Player Player = client.Player;
Random random = new Random();
bool ChangedPosition = false;
while (client.IsConnected)
{
    if (Player == null)
    {
        Player = client.Player;
        continue;
    }

    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
    {
        continue;
    }
    var parts = input.Split(' ');
    var command = parts[0];
    if (string.Equals(command, "dc"))
    {
        await client.DisconnectAsync();
    }
    if (string.Equals(command, "w"))
    {
        Player.X++;
        ChangedPosition = true;
    }
    if (string.Equals(command, "a"))
    {
        Player.Z++;
        ChangedPosition = true;
    }
    if (string.Equals(command, "s"))
    {
        Player.X--;
        ChangedPosition = true;
    }
    if (string.Equals(command, "d"))
    {
        Player.Z--;
        ChangedPosition = true;
    }
    if (string.Equals(command, "chat"))
    {
        if (parts.Length > 1)
        {
            client.Chat(string.Join(" ", parts[1..]));
        }
    }
    
    if (ChangedPosition)
    {
        client.SendPosition(Player);
    }

    //
    //Thread.Sleep(50);
    //
    //Player.X += Player.VelX;
    //Player.Z += Player.VelZ;
    //
    //if (Player.X + Player.VelX >= 24 || Player.X + Player.VelX <= -7)
    //{
    //    Player.VelX *= -1;
    //}
    //if (Player.Z + Player.VelZ >= 24 || Player.Z + Player.VelZ <= -7)
    //{
    //    Player.VelZ *= -1;
    //}
    //client.SendPosition(Player);

}

void Init()
{
    Console.WriteLine("connected to the server");
}

