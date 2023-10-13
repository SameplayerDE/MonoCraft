using MonoCraft.Net;
using Login = MonoCraft.Net.Predefined.Clientbound.Login;
using Play = MonoCraft.Net.Predefined.Clientbound.Play;
using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    internal static class PacketHandler
    {

        private static PacketIdentifier Identifier = PacketIdentifier.Instance;
        
        public static async void HandlePacket(MemoryStream packet, NetClient client)
        {
            int packetId = packet.ReadVarInt();

            var packetType = Identifier.Identify(client.Version, PacketDirection.Clientbound, client.ConnectionState, packetId);
            
            if (packetType != MinecraftPacketType.NotImplemented)
            {
                //Console.WriteLine(packetType.ToString());
            }

            if (packetType == MinecraftPacketType.CB_Login_EncryptionRequest)
            {
                Login.EncryptionRequestPacket data = new Login.EncryptionRequestPacket();
                data.Decode(packet, client.Version);

                Console.WriteLine(data.ServerId);
                Console.WriteLine(BitConverter.ToString(data.PublicKey));
                Console.WriteLine(BitConverter.ToString(data.VerifyToken));
            }
            if (packetType == MinecraftPacketType.CB_Login_SetCompression)
            {
                Login.SetCompressionPacket data = new Login.SetCompressionPacket();
                data.Decode(packet, client.Version);

                Console.WriteLine("threshold: {0}", data.Threshold);
            }
            if (packetType == MinecraftPacketType.CB_Login_Disconnect)
            {
                Login.DisconnectPacket data = new Login.DisconnectPacket();
                data.Decode(packet, client.Version);

                Console.WriteLine("reason: {0}", data.Reason);
            }
            if (packetType == MinecraftPacketType.CB_Login_SetCompression)
            {
                Login.SetCompressionPacket data = new Login.SetCompressionPacket();
                data.Decode(packet, client.Version);

                Console.WriteLine("threshold: {0}", data.Threshold);
            }

            if (packetType == MinecraftPacketType.CB_Login_LoginSuccess)
            {
                client.ConnectionState = ConnectionState.Play;
            }

            if (packetId == 0x03)
            {
                int threshold = packet.ReadVarInt();
                client.CompressionThreshold = threshold;
                //Console.WriteLine("set-compression from server [{0}]", threshold);
            }

            if (packetId == 0x27)
            {
                Play.EntityPositionPacket data = new Play.EntityPositionPacket();
                data.Decode(packet, client.Version);

                //Console.WriteLine("entity-postion from server");

            }

            if (packetId == 0x28)
            {
                Play.EntityPositionRotationPacket data = new Play.EntityPositionRotationPacket();
                data.Decode(packet, client.Version);

                //Console.WriteLine("entity-postion-rotation from server");

            }

            if (packetId == 0x29)
            {
                Play.EntityRotationPacket data = new Play.EntityRotationPacket();
                data.Decode(packet, client.Version);

                //Console.WriteLine("entity-rotation from server [{0},{1}]", data.Yaw, data.Pitch);

            }

            if (packetId == 0x20)
            {
                Play.ChunkDataPacket data = new Play.ChunkDataPacket();
                data.Decode(packet, client.Version);

                //Console.WriteLine("chunk-data from server [{0}, {1}]", data.ChunkX, data.ChunkY);

                var chunkDataStream = new MemoryStream(data.Data);
                chunkDataStream.Dispose();

                //for (int chunkSectionIndex = 0; chunkSectionIndex < 16; chunkSectionIndex++)
                //{
                //    int index = 0;
                //    if ((data.PrimaryBitMask & (1 << chunkSectionIndex)) != 0)
                //    {
                //        short blockCount = chunkDataStream.ReadShort();
                //        byte bitsPerBlock = chunkDataStream.ReadUByte();
                //        int paletteLength = chunkDataStream.ReadVarInt();
                //        int[] palette = new int[paletteLength];
                //        for (int i = 0; i < paletteLength; i++)
                //        {
                //            palette[i] = chunkDataStream.ReadVarInt();
                //        }
                //        int dataArrayLength = chunkDataStream.ReadVarInt();
                //        long[] dataArray = new long[dataArrayLength];
                //        for (int i = 0; i < dataArrayLength; i++)
                //        {
                //            dataArray[i] = chunkDataStream.ReadLong();
                //        }
                //    }
                //}
            }

            if (packetId == 0x1F)
            {
                long keepAliveId = packet.ReadLong();
                //Console.WriteLine("keep-alive from server [{0}]", keepAliveId);
                client.KeepAlive(keepAliveId);
            }

            if (packetId == 0x19)
            {
                string reason = packet.ReadString();
                //Console.WriteLine("disconnect from server [{0}]", reason);
            }

            if (packetId == 0x0B)
            {
                (int, int, int) position = packet.ReadPositionTuple();
                int id = packet.ReadVarInt();
                //Console.WriteLine("BLOCK-CHANGE from server [{0}]", id);
            }

            if (packetId == 0x0E)
            {
                string message = packet.ReadString();
                //Console.WriteLine("chat-message from server [{0}]", message);
            }

            if (packetId == 0x34)
            {
                double x = packet.ReadDouble();
                double y = packet.ReadDouble();
                double z = packet.ReadDouble();
                float yaw = packet.ReadFloat();
                float pitch = packet.ReadFloat();
                byte flags = packet.ReadUByte();
                int teleportId = packet.ReadVarInt();
            
                if (client.Player == null)
                {
                    client.Player = new Player();
                    client.Player.VelX = 0.6;
                    client.Player.VelZ = 0.2;
                }
            
                client.SetPosition(x, y, z, yaw, pitch);

                //Console.WriteLine("player-position-rotation from server [{0}]", teleportId);

                client.TeleportConfirm(teleportId);
            }

            if (packetId == 0x4E)
            {
                long worldAge = packet.ReadLong();
                long timeOfDay = packet.ReadLong();
                //Console.WriteLine("time-update from server [{0}, {1}]", worldAge, timeOfDay);

                //SendPosition(Player);
                //OnServerTick?.Invoke();
            }

            packet.Dispose();
        }
    }
}
