using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.IO
{
    public static class CoreByteArrayExtension
    {
        public static T ToStructure<T>(this byte[] tab, int offset = 0) where T : struct
        {

            var t = default(T);
            var s = Marshal.SizeOf(typeof(T));

            if (tab.Length >= s)
            {
                var halloc = Marshal.AllocCoTaskMem(s);
                Marshal.Copy(tab, offset, halloc, s);
                t = Marshal.PtrToStructure<T>(halloc);
            }
            return t;
        }
        internal static void WriteTo(object o, BinaryWriter binw) {
            if (o == null)
                return;
            var t = o.GetType();
            var s = Marshal.SizeOf(t);
            byte[] tab = new byte[s];

            IntPtr alloc = Marshal.AllocCoTaskMem(s);
            Marshal.StructureToPtr(o, alloc, true);
            Marshal.Copy(alloc, tab, 0, tab.Length);
            binw.Write(tab, 0, tab.Length);
            Marshal.FreeCoTaskMem(alloc);

        }
        public static void WriteTo<T>(this T o, BinaryWriter mem) where T : struct
        {

            var s = Marshal.SizeOf(typeof(T));
            byte[] tab = new byte[s];
            IntPtr alloc = Marshal.AllocCoTaskMem(s);
            Marshal.StructureToPtr(o, alloc, true);
            Marshal.Copy(alloc, tab, 0, tab.Length);
            mem.Write(tab, 0, tab.Length);
            Marshal.FreeCoTaskMem(alloc);
        }
        public static void WriteTo<T>(this T[] o, BinaryWriter mem) where T : struct
        {
            if (o == null) {
                return;
            }
            var t = typeof(T);
            var s = Marshal.SizeOf(t);
            byte[] tab = new byte[s * o.Length];

            if (t.IsPrimitive)
            {
                var meth = mem.GetType().GetMethod("Write", new Type[] {
                        t
                    });
                for (int i = 0; i < o.Length; i++)
                {
                    meth.Invoke(mem, new object[] { o[i] });
                }
            }
            else
            {
                IntPtr alloc = Marshal.AllocCoTaskMem(s);
                Marshal.StructureToPtr(o, alloc, true);
                Marshal.Copy(alloc, tab, 0, tab.Length);
                mem.Write(tab, 0, tab.Length);
                Marshal.FreeCoTaskMem(alloc);
            }
        }
        internal static T Read<T>(this BinaryReader binR) where T : struct
        {
            var s = Marshal.SizeOf(typeof(T));
            //var o = default(T);
            byte[] t = new byte[s];
            binR.Read(t, 0, t.Length);
            return ToStructure<T>(t);

        }

        public static byte[] Slice(this Byte[] tab, int offset=0, int length=0) {
            var r = new Byte[length==0?tab.Length - offset:length];
            Array.Copy(tab, offset, r, 0, r.Length);
            return r;
        }
    }

}
