using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.Native
{
    internal class User32
    {
        [DllImport("shell32.dll")]
        internal extern static IntPtr ExtractIcon(IntPtr phandle, string filename, int index);
      
    }
}
