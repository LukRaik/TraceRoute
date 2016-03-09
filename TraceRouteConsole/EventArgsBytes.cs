using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraceRouteConsole
{
    class EventArgsBytes:EventArgs
    {
        public byte[] _byte { get; private set; }
        public EventArgsBytes(byte[] BYTE)
        {
            _byte = BYTE;
        }
    }
}
