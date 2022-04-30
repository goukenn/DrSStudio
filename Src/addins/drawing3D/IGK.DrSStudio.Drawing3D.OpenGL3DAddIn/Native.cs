using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.OpenGL
{
    internal static class Native
    {
        #region "NATIVE METHOD"
        [DllImport("Kernel32.dll")]
        internal static extern IntPtr LoadLibrary(string file);
        [DllImport("Kernel32.dll")]
        internal static extern IntPtr GetProcAddress(
  IntPtr hModule,
  string lpProcName
);
        [DllImport("Kernel32.dll")]
        internal static extern bool FreeLibrary(IntPtr file);
        #endregion
    }
}
