using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    public struct WOFFFileCharMapRange
    {
        public ushort OffsetRange { get; set; }
        public ushort MinChar{ get; set; }
        public ushort MaxChar { get; set; }
        public ushort ShiftRange { get; set; }
    }
}
