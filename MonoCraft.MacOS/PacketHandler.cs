using MonoCraft.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class BlockChangeEventArgs : EventArgs
    {
        public int X; public int Y; public int Z;
        public int Id;
    }

    internal static class PacketHandler
    {

        public static event EventHandler<BlockChangeEventArgs> OnBlockChange;

        public static void HandlePacket(MemoryStream packet, NetClient client)
        {
            int packetId = packet.ReadVarInt();
            Console.WriteLine("packet from server [{0:x2}]", packetId);

            if (packetId == 0x03)
            {
                int threshold = packet.ReadVarInt();
                client.CompressionThreshold = threshold;
                Console.WriteLine("set-compression from server [{0}]", threshold);
            }

            if (packetId == 0x1F)
            {
                long keepAliveId = packet.ReadLong();
                Console.WriteLine("keep-alive from server [{0}]", keepAliveId);
                client.KeepAlive(keepAliveId);
            }

            if (packetId == 0x19)
            {
                string reason = packet.ReadString();
                Console.WriteLine("disconnect from server [{0}]", reason);
            }

            if (packetId == 0x0B)
            {
                (int, int, int) position = packet.ReadPosition();
                int id = packet.ReadVarInt();
                Console.WriteLine("BLOCK-CHANGE from server [{0}]", id);

                OnBlockChange?.Invoke(null, new BlockChangeEventArgs()
                {
                    X = position.Item1,
                    Y = position.Item2,
                    Z = position.Item3,
                    Id = id
                });
            }

            if (packetId == 0x0E)
            {
                string message = packet.ReadString();
                Console.WriteLine("chat-message from server [{0}]", message);
            }

            //if (packetId == 0x34)
            //{
            //    double x = packet.ReadDouble();
            //    double y = packet.ReadDouble();
            //    double z = packet.ReadDouble();
            //    float yaw = packet.ReadFloat();
            //    float pitch = packet.ReadFloat();
            //    byte flags = packet.ReadUByte();
            //    int teleportId = packet.ReadVarInt();
            //
            //    if (Player == null)
            //    {
            //        Player = new Player();
            //        Player.VelX = 0.6;
            //        Player.VelZ = 0.2;
            //    }
            //
            //    SetPosition(x, y, z, yaw, pitch);
            //
            //    //Console.WriteLine("player-position-rotation from server [{0}]", teleportId);
            //
            //    TeleportConfirm(teleportId);
            //}

            if (packetId == 0x4E)
            {
                long worldAge = packet.ReadLong();
                long timeOfDay = packet.ReadLong();
                Console.WriteLine("time-update from server [{0}, {1}]", worldAge, timeOfDay);

                //SendPosition(Player);
                //OnServerTick?.Invoke();
            }

            packet.Dispose();
        }

    }
}
