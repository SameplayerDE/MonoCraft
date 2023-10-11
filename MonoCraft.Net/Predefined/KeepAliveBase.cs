using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoCraft.Net.Predefined
{
    public abstract class KeepAliveBase : Packet
    {
        public long KeepAliveId;

        public KeepAliveBase(int identifier) : base()
        {
        }
    }
}
