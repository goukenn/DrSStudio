

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Pfloat.cs
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
file:Pfloat.cs
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
    /// Represent a float float pointer . remark it used AllocHG
    /// </summary>
    public class PFloat : IDisposable
    {
        static Dictionary<IntPtr, int> rPfloat;
        int offset; //used to spécified 
        IntPtr handle;
        static readonly int size = Marshal.SizeOf(typeof(float));
        int length;
        static PFloat()
        {
            rPfloat = new Dictionary<IntPtr, int>();
        }
        public long HandleValue {
            get {
                if (Environment.Is64BitOperatingSystem)
                    return handle.ToInt64();
                return handle.ToInt32();
            }
        }
        public PFloat(int length)
        {
            this.length = length;
            handle = Marshal.AllocHGlobal(Size * length);
            //init
            for (int i = 0; i < length; i++)
            {
                this[i] = 0;
            }
            this.offset = 0;
        }
        public PFloat(PFloat f, int index)
        {
            if (rPfloat.ContainsKey(f.handle))
            {
                rPfloat[f.handle]++;
            }
            else {
                rPfloat.Add(f.handle, 1);
            }
            this.handle = f.handle;
            this.offset = index;
            this.length = f.length - index;
        }
        ~PFloat()
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
        public float this[int index]
        {
            get
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                float[] d = new float[1];
                Marshal.Copy(new IntPtr(this.HandleValue + size * index), d, 0, 1);
                return d[0];
            }
            set
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                Marshal.Copy(new float[] { value }, 0, new IntPtr(HandleValue + index * size), 1);
            }
        }
        public PFloat ToPointer(int index)
        {
            if ((index < 0) || (index >= length))
                throw new System.ArgumentOutOfRangeException("index out of range");
            return NewPointer(this, index);
        }
        private static PFloat NewPointer(PFloat pointer, int index)
        {
            PFloat v = null;
            v = new PFloat(pointer,index);                        
            return v;
        }
        public static implicit operator PFloat(float[] array)
        {
            PFloat v = null;
            if ((array == null) || (array.Length == 0))
                return v;
            v = new PFloat(array.Length);
            Marshal.Copy(array, 0, v.handle, v.length);
            return v;
        }
        //public static implicit operator PFloat(vect3f[] array)
        //{
        //    PFloat v = null;
        //    if ((array == null) || (array.Length == 0))
        //        return v;
        //    v = new PFloat(array.Length * 3);
        //    int o = 0;
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        o = i * 3;
        //        v[o] = array[i].X;
        //        v[o+1] = array[i].Y;
        //        v[o + 2] = array[i].Z;
        //    }            
        //    return v;
        //}
        public static implicit operator IntPtr(PFloat value)
        {
            return value.Handle;
        }
        public static PFloat operator +(PFloat p, int offset)
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
                return new IntPtr(HandleValue+ offset * size); ;
            }
        }
        /// <summary>
        /// dispose this element
        /// </summary>
        public void Dispose()
        {
            if (handle != IntPtr.Zero)
            {
                if (rPfloat.ContainsKey(handle))
                {
                    if (rPfloat[handle] == 0)
                    {
                        rPfloat.Remove(handle);
                        Marshal.FreeHGlobal(handle);
                        handle = IntPtr.Zero;
                    }
                    else
                    {
                        rPfloat[handle]--;
                    }
                }
                else {
                    //destroy handle
                    rPfloat.Remove(handle);
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;                    
                }
            }
        }
        public static implicit operator PFloat(vect3f[] vecs)
        {
            if ((vecs == null) || (vecs.Length == 0)) return null;
            PFloat v = new PFloat(vecs.Length * 3 * size);
            int offset = 0;
            for (int i = 0; i < vecs.Length; i++)
            {
                v[offset + 0] = vecs[i].X;
                v[offset + 1] = vecs[i].Y;
                v[offset + 2] = vecs[i].Z;
                offset += 3;
            }
            return v;
        }
        public static implicit operator PFloat(vect4f[] vecs)
        {
            if ((vecs == null) || (vecs.Length == 0)) return null;
            PFloat v = new PFloat(vecs.Length * 4 * size);
            int offset = 0;
            for (int i = 0; i < vecs.Length; i++)
            {
                v[offset + 0] = vecs[i].X;
                v[offset + 1] = vecs[i].Y;
                v[offset + 2] = vecs[i].Z;
                v[offset + 3] = vecs[i].Q;
                offset += 4;
            }
            return v;
        }
        public float[] ToArray()
        {
            List<float> flist = new List<float>();
            for (int i = 0; i < this.Length; i++)
            {
                flist.Add(this[i]);
            }
            return flist.ToArray();
        }
    }
}

