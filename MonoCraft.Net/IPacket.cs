using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net
{
    public interface IPacket
    {
        void Encode(Stream stream, MinecraftVersion version);
        void Decode(Stream stream, MinecraftVersion version);
    }
}
