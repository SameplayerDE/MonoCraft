using MonoCraft.Net.Predefined.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net
{
    public static class MinecraftProtocolUtils
    {

        private static readonly Dictionary<MinecraftVersion, int> VersionToProtocol = new Dictionary<MinecraftVersion, int>
        {
            { MinecraftVersion.Ver_1_20_2, 764 },
            { MinecraftVersion.Ver_1_20_1, 763 },
            { MinecraftVersion.Ver_1_20, 763 },
            { MinecraftVersion.Ver_1_19_4, 762 },
            { MinecraftVersion.Ver_1_19_3, 761 },
            { MinecraftVersion.Ver_1_19_2, 760 },
            { MinecraftVersion.Ver_1_19_1, 760 },
            { MinecraftVersion.Ver_1_19, 759 },
            { MinecraftVersion.Ver_1_18_2, 758 },
            { MinecraftVersion.Ver_1_18_1, 757 },
            { MinecraftVersion.Ver_1_18, 757 },
            { MinecraftVersion.Ver_1_17_1, 756 },
            { MinecraftVersion.Ver_1_17, 755 },
            { MinecraftVersion.Ver_1_16_5, 754 },
            { MinecraftVersion.Ver_1_16_4, 754 },
            { MinecraftVersion.Ver_1_16_3, 753 },
            { MinecraftVersion.Ver_1_16_2, 751 },
            { MinecraftVersion.Ver_1_16_1, 736 },
            { MinecraftVersion.Ver_1_16, 735 },
            { MinecraftVersion.Ver_1_15_2, 578 },
            { MinecraftVersion.Ver_1_15_1, 575 },
            { MinecraftVersion.Ver_1_15, 573 },
            { MinecraftVersion.Ver_1_14_4, 498 },
            { MinecraftVersion.Ver_1_14_3, 490 },
            { MinecraftVersion.Ver_1_14_2, 485 },
            { MinecraftVersion.Ver_1_14_1, 480 },
            { MinecraftVersion.Ver_1_14, 477 },
            { MinecraftVersion.Ver_1_13_2, 404 },
            { MinecraftVersion.Ver_1_13_1, 401 },
            { MinecraftVersion.Ver_1_13, 393 },
            { MinecraftVersion.Ver_1_12_2, 340 },
            { MinecraftVersion.Ver_1_12_1, 338 },
            { MinecraftVersion.Ver_1_12, 335 },
        };

        public static int GetProtocolVersion(MinecraftVersion version)
        {
            if (VersionToProtocol.TryGetValue(version, out int protocolVersion))
            {
                return protocolVersion;
            }

            throw new ArgumentException("invalid version", nameof(version));
        }

    }
}
