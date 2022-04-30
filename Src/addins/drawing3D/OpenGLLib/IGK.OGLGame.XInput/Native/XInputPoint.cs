using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame.XInput.Native
{
    [StructLayout(LayoutKind.Sequential)]
    struct XInputPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}
