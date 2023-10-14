using System.Text;
using ConsoleClient;
using MonoCraft.Net.Predefined.Enums;
using Spectre.Console;

string address = "MC.HYPIXEL.NET";
ushort port = 25565;

StatusChecker statusChecker = new StatusChecker();
statusChecker.Connect(address, port);
statusChecker.Disconnect();
statusChecker.Close();

byte[] imageBytes = Convert.FromBase64String(statusChecker.Response.FaviconBase64.Replace("data:image/png;base64,", ""));
File.WriteAllBytes("image.png", imageBytes);
var image = new CanvasImage("image.png");
File.Delete("image.png");
AnsiConsole.Render(image);

//Console.WriteLine("{0}", statusChecker.JsonString);
Console.WriteLine("Version: {0}", statusChecker.Response.Version.Name);
Console.WriteLine("Protocol: {0}", statusChecker.Response.Version.ProtocolVersion);
Console.WriteLine("Max: {0}", statusChecker.Response.PlayerList.Max);
Console.WriteLine("Online: {0}", statusChecker.Response.PlayerList.Online);

//if (statusChecker.Response.Description.Extra != null)
//{
//    foreach (var e in statusChecker.Response.Description.Extra)
//    {
//        Console.Write(e.Text);
//    }
//}
//else
//{
//    Console.Write(ConvertMinecraftColors(statusChecker.Response.Description.Text));
//}

static string ConvertMinecraftColors(string input)
{
    string result = input;

    result = result.Replace("§0", "\u001b[30m"); // Black
    result = result.Replace("§1", "\u001b[34m"); // Dark Blue
    result = result.Replace("§2", "\u001b[32m"); // Dark Green
    result = result.Replace("§3", "\u001b[36m"); // Dark Aqua
    result = result.Replace("§4", "\u001b[31m"); // Dark Red
    result = result.Replace("§5", "\u001b[35m"); // Dark Purple
    result = result.Replace("§6", "\u001b[33m"); // Gold
    result = result.Replace("§7", "\u001b[37m"); // Gray
    result = result.Replace("§8", "\u001b[90m"); // Dark Gray
    result = result.Replace("§9", "\u001b[94m"); // Blue
    result = result.Replace("§a", "\u001b[92m"); // Green
    result = result.Replace("§b", "\u001b[96m"); // Aqua
    result = result.Replace("§c", "\u001b[91m"); // Red
    result = result.Replace("§d", "\u001b[95m"); // Light Purple
    result = result.Replace("§e", "\u001b[93m"); // Yellow
    result = result.Replace("§f", "\u001b[97m"); // White
    result = result.Replace("§r", "\u001b[0m");  // Reset color

    // Entfernen nicht unterstützter Farbcodes
    result = result.Replace("§l", ""); // Fett
    result = result.Replace("§o", ""); // Kursiv
    result = result.Replace("§n", ""); // Unterstrichen
    result = result.Replace("§m", ""); // Durchgestrichen
    result = result.Replace("§k", ""); // Magischer Buchstabe
    result = result.Replace("§x", ""); // Schrift aus

    
    return result;
}

//PerfClient client = new PerfClient((MinecraftVersion)statusChecker.Response.Version.ProtocolVersion);
//client.Connect(address, port);
//
//while (client.IsConnected)
//{
//    
//}

//NetClient client = new NetClient(MinecraftVersion.Ver_1_16_4);
//client.OnConnectionEstablished += Init;
////client.OnServerTick += () => client.Chat("Tick");
//
//client.Connect("localhost", 25565);
//
//Player Player = client.Player;
//Random random = new Random();
//bool ChangedPosition = false;
//while (client.IsConnected)
//{
//    if (Player == null)
//    {
//        Player = client.Player;
//        continue;
//    }
//
//    var input = Console.ReadLine();
//    if (string.IsNullOrEmpty(input))
//    {
//        continue;
//    }
//    var parts = input.Split(' ');
//    var command = parts[0];
//    if (string.Equals(command, "dc"))
//    {
//        await client.DisconnectAsync();
//    }
//    if (string.Equals(command, "w"))
//    {
//        Player.X++;
//        ChangedPosition = true;
//    }
//    if (string.Equals(command, "a"))
//    {
//        Player.Z++;
//        ChangedPosition = true;
//    }
//    if (string.Equals(command, "s"))
//    {
//        Player.X--;
//        ChangedPosition = true;
//    }
//    if (string.Equals(command, "d"))
//    {
//        Player.Z--;
//        ChangedPosition = true;
//    }
//    if (string.Equals(command, "chat"))
//    {
//        if (parts.Length > 1)
//        {
//            client.Chat(string.Join(" ", parts[1..]));
//        }
//    }
//    
//    if (ChangedPosition)
//    {
//        client.SendPosition(Player);
//    }
//
//    //
//    //Thread.Sleep(50);
//    //
//    //Player.X += Player.VelX;
//    //Player.Z += Player.VelZ;
//    //
//    //if (Player.X + Player.VelX >= 24 || Player.X + Player.VelX <= -7)
//    //{
//    //    Player.VelX *= -1;
//    //}
//    //if (Player.Z + Player.VelZ >= 24 || Player.Z + Player.VelZ <= -7)
//    //{
//    //    Player.VelZ *= -1;
//    //}
//    //client.SendPosition(Player);
//
//}
//
//void Init()
//{
//    Console.WriteLine("connected to the server");
//}

