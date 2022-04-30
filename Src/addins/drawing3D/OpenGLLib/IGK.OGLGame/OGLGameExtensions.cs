using System;
using System.Runtime.InteropServices;

namespace IGK.OGLGame
{
    public static class OGLGameExtensions
    {
        public static IntPtr ToPointer<T>(this T[] item)
        {
            if (item == null)
                return IntPtr.Zero;
            IntPtr c = Marshal.UnsafeAddrOfPinnedArrayElement(item, 0);
            return c;
        }
    }
}
