﻿using ConsoleClient;
using System;

NetClient client = new NetClient();
client.OnConnectionEstablished += Init;
//client.OnServerTick += () => client.Chat("Tick");

await client.ConnectAsync("localhost", 25565);

Player Player = client.Player;
Random random = new Random();
while (client.IsConnected)
{
    //var input = Console.ReadLine();
    //if (string.IsNullOrEmpty(input))
    //{
    //    continue;
    //}
    //var parts = input.Split(' ');
    //var command = parts[0];
    //if (string.Equals(command, "dc"))
    //{
    //    await client.DisconnectAsync();
    //}
    //if (string.Equals(command, "sw0"))
    //{
    //    client.SwingArm(0);
    //}
    //if (string.Equals(command, "sw1"))
    //{
    //    client.SwingArm(1);
    //}
    //if (string.Equals(command, "chat"))
    //{
    //    if (parts.Length > 1)
    //    {
    //        client.Chat(string.Join(" ", parts[1..]));
    //    }
    //}
    
    if (Player == null)
    {
        Player = client.Player;
        continue;
    }

    Thread.Sleep(100);

    Player.X += Player.VelX;
    Player.Z += Player.VelZ;

    if (Player.X + Player.VelX >= 24 || Player.X + Player.VelX <= -7)
    {
        Player.VelX *= -1;
    }
    if (Player.Z + Player.VelZ >= 24 || Player.Z + Player.VelZ <= -7)
    {
        Player.VelZ *= -1;
    }
    client.SendPosition(Player);

}

void Init()
{
    Console.WriteLine("connected to the server");
}

