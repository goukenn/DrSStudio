

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLMarshal.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:GLMarshal.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.OGLGame
{
    /// <summary>
    /// used to marshal data
    /// </summary>
    public static class GLMarshal
    {
        public static IntPtr CreateTaskMemoryPointer(Type t, int size)
        {
            IntPtr c = Marshal.AllocCoTaskMem(Marshal.SizeOf(t)* size);
            return c;
        }
        public static void FreeTaskMemoryPointer(IntPtr alloc)
        {
            Marshal.FreeCoTaskMem(alloc);            
        }
        public static IntPtr GetObjectPointer(object structure)
        {
            if (structure == null)
                return IntPtr.Zero;
            IntPtr c = CreateTaskMemoryPointer (structure.GetType (), 1);
            Marshal.StructureToPtr(structure, c, false);
            return c;
        }
        public static T[] ToArray<T>(IntPtr buffer, int size)
        {
            Type t = default(T).GetType();
            T[] c = new T[size];
            //int v_size = Marshal.SizeOf(t) * size ;
            int v_offset =Marshal.SizeOf (t);
            int v_off = 0;
            //Byte[] tdata = new byte[size];
            for (int i = 0; i < size; i++)
            {
                c[i] =(T) Marshal.PtrToStructure(new IntPtr(buffer.ToInt32() + v_off), t);             
                v_off += v_offset;
            }
            return c;
        }
        /// <summary>
        /// get data from pointer
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T GetData<T>(IntPtr p)
            where T :
   struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {

            T[] d = new T[1];
            var s = Marshal .SizeOf (typeof(T));
            var k = Marshal.UnsafeAddrOfPinnedArrayElement(d,0);
            int ofs = 0;
            while (s>0)
	        {
	            Marshal.WriteByte (k, Marshal.ReadByte(p, ofs));
                s -= 8;
                ofs += 8;
	        }            
            return d[0];
        }
    }
}

