//using System;
//using System.IO;
//using ConsoleClient;
//
//
//
//string address = "MC.HYPIXEL.NET";
//ushort port = 25565;
//
//var _checker = new StatusChecker();
//
//_checker.Connect(address, port);
//while (_checker.Response == null)
//{
//
//}
//byte[] imageBytes = Convert.FromBase64String(_checker.Response.FaviconBase64.Replace("data:image/png;base64,", ""));
//File.WriteAllBytes("image.png", imageBytes);
//
//
//using var game = new MonoCraft.App.Game1(_checker.Response);
//game.Run();