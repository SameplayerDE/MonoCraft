using System;
using System.IO;
using ConsoleClient;

string address = "MC.HYPIXEL.NET";
ushort port = 25565;

StatusChecker statusChecker = new StatusChecker();
statusChecker.Connect(address, port);
statusChecker.Disconnect();
statusChecker.Close();

byte[] imageBytes = Convert.FromBase64String(statusChecker.Response.FaviconBase64.Replace("data:image/png;base64,", ""));
File.WriteAllBytes("image.png", imageBytes);

using var game = new MonoCraft.App.Game1();
game.Run();