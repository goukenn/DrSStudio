

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDouble.cs
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
file:PDouble.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2011
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igk-dev.be
App : DrSStudio
powered by IGK - DEV 2008-2011
------------------------------------------------------------------- 
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.GLLib.Pointers
{
    /// <summary>
    /// Represent a double pointer . remark it used AllocHG
    /// </summary>
    public class PDouble : IDisposable
    {
        static Dictionary<IntPtr, int> rPDouble;
        int offset; //used to spécified 
        IntPtr handle;
        static readonly int size = Marshal.SizeOf(typeof(double));
        int length;
        static PDouble()
        {
            rPDouble = new Dictionary<IntPtr, int>();
        }
        public PDouble(int length)
        {
            this.length = length;
            handle = Marshal.AllocHGlobal(Size * length);
            this.offset = 0;
        }
        public PDouble(PDouble f, int index)
        {
            if (rPDouble.ContainsKey(f.handle))
            {
                rPDouble[f.handle]++;
            }
            else
            {
                rPDouble.Add(f.handle, 1);
            }
            this.handle = f.handle;
            this.offset = index;
            this.length = f.length - index;
        }
        ~PDouble()
        {
            this.Dispose();
        }
        /// <summary>
        /// get the default size of this pointer
        /// </summary>
        public static int Size
        {
            get
            {
                return size;
            }
        }
        /// <summary>
        /// get the lenght of the pointer
        /// </summary>
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        public double this[int index]
        {
            get
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                double[] d = new double[1];
                Marshal.Copy(new IntPtr(this.handle.ToInt32() + size * index), d, 0, 1);
                return d[0];
            }
            set
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                Marshal.Copy(new double[] { value }, 0, new IntPtr(handle.ToInt32() + index * size), 1);
            }
        }
        public PDouble ToPointer(int index)
        {
            if ((index < 0) || (index >= length))
                throw new System.ArgumentOutOfRangeException("index out of range");
            return NewPointer(this, index);
        }
        private static PDouble NewPointer(PDouble pointer, int index)
        {
            PDouble v = null;
            v = new PDouble(pointer, index);
            return v;
        }
        public static implicit operator PDouble(double[] array)
        {
            PDouble v = null;
            if ((array == null) || (array.Length == 0))
                return v;
            v = new PDouble(array.Length);
            Marshal.Copy(array, 0, v.handle, v.length);
            return v;
        }
        public static implicit operator IntPtr(PDouble value)
        {
            return value.handle;
        }
        public static PDouble operator +(PDouble p, int offset)
        {
            if ((p.offset + offset) < p.Length)
                return NewPointer(p, offset);
            else
                return null;
        }
        /// <summary>
        /// get the handle attached to this element
        /// </summary>
        public IntPtr Handle
        {
            get
            {                
               return handle;                
            }
        }
        /// <summary>
        /// dispose this element
        /// </summary>
        public void Dispose()
        {
            if (handle != IntPtr.Zero)
            {
                if (rPDouble.ContainsKey(handle))
                {
                    if (rPDouble[handle] == 0)
                    {
                        rPDouble.Remove(handle);
                        Marshal.FreeHGlobal(handle);
                        handle = IntPtr.Zero;
                    }
                    else
                    {
                        rPDouble[handle]--;
                    }
                }
                else
                {
                    //destroy handle
                    rPDouble.Remove(handle);
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }
        //public static explicit operator IGK.GLLib.PDouble(vec3d[] vecs)
        //{
        //    if ((vecs == null) || (vecs.Length == 0)) return null;
        //    PDouble v = new PDouble(vecs.Length * 3 * size);
        //    int offset = 0;
        //    for (int i = 0; i < vecs.Length; i++)
        //    {
        //        v[offset + 0] = vecs[i].X;
        //        v[offset + 1] = vecs[i].Y;
        //        v[offset + 2] = vecs[i].Z;
        //        offset += 3;
        //    }
        //    return v;
        //}
    }
}

