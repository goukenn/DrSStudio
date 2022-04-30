using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Extensions
{
    public static class CoreByteArrayExtension
    {
        /// <summary>
        /// convert by to structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tab"></param>
        /// <returns></returns>
        public static T ToStructure<T>(this byte[] tab) where T:struct {

            var t = default(T);
            var s = Marshal.SizeOf(typeof(T));
            var halloc = Marshal.AllocCoTaskMem(s);
            Marshal.Copy(tab, 0, halloc, s);
            t = Marshal.PtrToStructure<T>(halloc);
            Marshal.FreeCoTaskMem(halloc);            
            return t;
        }
    }
}
