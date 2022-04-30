using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Native
{
    class Kernell32
    {
        [DllImport("kernel32.dll")]
        internal extern static IntPtr LoadLibrary(string filename);
        [DllImport("kernel32.dll")]
        internal extern static IntPtr FreeLibrary(IntPtr phandle);
    }
}
