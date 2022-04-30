

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLUtils.cs
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
file:GLUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.GLLib
{
    /// <summary>
    /// represent a glib utility
    /// </summary>
    public static class GLUtils
    {
        /// <summary>
        /// get boolean value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T GetBooleanv<T>(uint p)
        {
            int size = Marshal.SizeOf(typeof(T));
            IntPtr alloc = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(T)));
            T c = (T)Marshal.PtrToStructure(alloc, typeof(T));
            Marshal.FreeCoTaskMem(alloc);
            return c;
        }
        /// <summary>
        /// get integer value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p"></param>
        /// <returns></returns>
        public static T GetIntegerv<T>(uint p)
        {
            T d = default(T);
            Type t = typeof(T);
            if (t.IsEnum)
            {
                int[] v_t = new int[1];
                GL.glGetIntegerv(p, v_t);
                d = (T)Enum.ToObject(t, v_t[0]);
            }
            else
            {
                int size = Marshal.SizeOf(t);
                int[] v_t = new int[size / Marshal.SizeOf(typeof(int))];
                IntPtr v_alloc = Marshal.AllocCoTaskMem(size);
                GL.glGetIntegerv(p, v_t);
                Marshal.Copy(v_t, 0, v_alloc, v_t.Length);
                d = (T)Marshal.PtrToStructure(v_alloc, t);
                Marshal.FreeCoTaskMem(v_alloc);
            }
            return d;
        }

        /// <summary>
        /// get the value in int
        /// </summary>
        /// <param name="pname"></param>
        /// <returns></returns>
        public static int GetIntegerv(uint pname) {
            int v = 0;
            GL.glGetIntegerv(pname, ref v);
            return v;

        }

    }
}

