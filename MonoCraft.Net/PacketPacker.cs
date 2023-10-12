using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net
{
    public static class PacketPacker
    {

        public static byte[] Pack(Packet packet, bool compress = false, int threshold = 256)
        {
            if (compress)
            {
                return Compress(packet, threshold);
            }
            else
            {
                return null;
            }
        }

        public static byte[] Compress(Packet packet, int threshold)
        {
            return null;
        }

        public static byte[] Decompress(Packet packet, int threshold)
        {
            if (threshold >= 0)
            {
                int compressionLength;
                int isCompressed = packetStream.ReadVarInt(out compressionLength);

            }
        }

    }
}
