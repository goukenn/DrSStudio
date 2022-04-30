

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PUbyte.cs
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
file:PUbyte.cs
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
    public class PUbyte : IDisposable
    {
        static Dictionary<IntPtr, int> rPUbyte;
        int offset; //used to spécified 
        IntPtr handle;
        static readonly int size = Marshal.SizeOf(typeof(byte));
        int length;
        static PUbyte()
        {
            rPUbyte = new Dictionary<IntPtr, int>();
        }
        public PUbyte(int length)
        {
            this.length = length;
            handle = Marshal.AllocHGlobal(Size * length);
            this.offset = 0;
        }
        public PUbyte(PUbyte f, int index)
        {
            if (rPUbyte.ContainsKey(f.handle))
            {
                rPUbyte[f.handle]++;
            }
            else
            {
                rPUbyte.Add(f.handle, 1);
            }
            this.handle = f.handle;
            this.offset = index;
            this.length = f.length - index;
        }
        ~PUbyte()
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
        public byte this[int index]
        {
            get
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                byte[] d = new byte[1];
                Marshal.Copy(new IntPtr(this.handle.ToInt32() + size * index), d, 0, 1);
                return d[0];
            }
            set
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                Marshal.Copy(new float[] { value }, 0, new IntPtr(handle.ToInt32() + index * size), 1);
            }
        }
        public PUbyte ToPointer(int index)
        {
            if ((index < 0) || (index >= length))
                throw new System.ArgumentOutOfRangeException("index out of range");
            return NewPointer(this, index);
        }
        private static PUbyte NewPointer(PUbyte pointer, int index)
        {
            PUbyte v = null;
            v = new PUbyte(pointer, index);
            return v;
        }
        public static implicit operator PUbyte(float[] array)
        {
            PUbyte v = null;
            if ((array == null) || (array.Length == 0))
                return v;
            v = new PUbyte(array.Length);
            Marshal.Copy(array, 0, v.handle, v.length);
            return v;
        }
        public static implicit operator IntPtr(PUbyte value)
        {
            return value.Handle;
        }
        public static PUbyte operator +(PUbyte p, int offset)
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
                return new IntPtr(handle.ToInt32() + offset * size); ;
            }
        }
        /// <summary>
        /// dispose this element
        /// </summary>
        public void Dispose()
        {
            if (handle != IntPtr.Zero)
            {
                if (rPUbyte.ContainsKey(handle))
                {
                    if (rPUbyte[handle] == 0)
                    {
                        rPUbyte.Remove(handle);
                        Marshal.FreeHGlobal(handle);
                        handle = IntPtr.Zero;
                    }
                    else
                    {
                        rPUbyte[handle]--;
                    }
                }
                else
                {
                    //destroy handle
                    rPUbyte.Remove(handle);
                    Marshal.FreeHGlobal(handle);
                    handle = IntPtr.Zero;
                }
            }
        }
        public static explicit operator PUbyte(vect3f[] vecs)
        {
            if ((vecs == null) || (vecs.Length == 0)) return null;
            PUbyte v = new PUbyte(vecs.Length * 3 * size);
            int offset = 0;
            for (int i = 0; i < vecs.Length; i++)
            {
                v[offset + 0] = (byte)vecs[i].X;
                v[offset + 1] = (byte)vecs[i].Y;
                v[offset + 2] = (byte)vecs[i].Z;
                offset += 3;
            }
            return v;
        }
        public static implicit operator PUbyte(byte[] data)
        {
            if ((data == null) || (data.Length == 0))
                return null;
            PUbyte v_result = new PUbyte(data.Length);
            for (int i = 0; i < data.Length; i++)
			{
                v_result[i] = data[i];
			}
            return v_result;
        }
    }
}

