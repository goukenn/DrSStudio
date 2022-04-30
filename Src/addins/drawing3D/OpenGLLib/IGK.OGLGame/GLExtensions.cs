using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.OGLGame
{
    public static class GLExtensions
    {

        public static IntPtr ToPinnedPtr(this float[] tab)
        {
            //The array must be pinned using a GCHandle before it is passed to this method.
            //For maximum performance, this method does not validate the array passed to it; this can result in unexpected behavior.

            GCHandle.Alloc(tab, GCHandleType.Pinned);
            IntPtr c =// GLMarshal.CreateTaskMemoryPointer (typeof(float), tab.Length);
            Marshal.UnsafeAddrOfPinnedArrayElement(tab, 0);
            //c = GLMarshal.GetObjectPointer (tab);
            return c;
        }

        public static IntPtr ToPtr(this float[] tab) {
            GCHandle.Alloc(tab, GCHandleType.Pinned);
            IntPtr c =// GLMarshal.CreateTaskMemoryPointer (typeof(float), tab.Length);
            Marshal.UnsafeAddrOfPinnedArrayElement(tab, 0);
            //c = GLMarshal.GetObjectPointer (tab);
            return c;
        }
        public static T ConvertTo<T>(this IntPtr data)
        {
            T obj = default(T);
            obj = (T)Marshal.PtrToStructure(data, obj.GetType());
            return obj;
        }
        public static Rectanglef GetBound(this Vector3f startPoint, Vector3f point)
        {
            return new Rectanglef(
                  global::System.Math.Min(startPoint.X, point.X),
                  global::System.Math.Min(startPoint.Y, point.Y),
                  global::System.Math.Abs(startPoint.X - point.X),
                  global::System.Math.Abs(startPoint.Y - point.Y)
                  );
        }
    }
}
