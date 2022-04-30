

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PInt4.cs
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
file:PInt4.cs
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
    /// Represent signed float pointer
    /// </summary>
    public class PInt4 : IDisposable
    {
        IntPtr handle;
        static readonly int size = Marshal.SizeOf(typeof(int));
        int length;
        private PInt4()
        {
        }
        ~PInt4()
        {
            this.Dispose();
        }
        public PInt4(int size)
        {
            this.length = size;
            this.handle = Marshal.AllocHGlobal(size * PInt4.size);
        }
        public static int Size
        {
            get
            {
                return size;
            }
        }
        public int Length
        {
            get
            {
                return this.length;
            }
        }
        public int this[int index]
        {
            get
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                int[]  d = new int[1];
                Marshal.Copy(new IntPtr(this.handle.ToInt32() + size * index), d, 0, 1);
                return d[0];
            }
            set
            {
                if ((index < 0) || (index >= length))
                    throw new System.ArgumentOutOfRangeException("index out of range");
                Marshal.Copy(new int[] { value }, 0, new IntPtr(handle.ToInt32() + index * size), 1);
            }
        }
        public PInt4 ToPointer(int index)
        {
            if ((index < 0) || (index >= length))
                throw new System.ArgumentOutOfRangeException("index out of range");
            return NewPointer(this, index);
        }
        private static PInt4 NewPointer(PInt4 pointer, int index)
        {
            PInt4 v = null;
            v = new PInt4();
            v.length = pointer.Length - index;
            v.handle = new IntPtr(pointer.handle.ToInt32() + index * size);
            return v;
        }
        public static implicit operator PInt4(int[] array)
        {
            PInt4 v = null;
            if ((array == null) || (array.Length == 0))
                return v;
            v = new PInt4();
            v.length = array.Length;
            v.handle = Marshal.AllocHGlobal(v.length * size);
            Marshal.Copy(array, 0, v.handle, v.length);
            return v;
        }
        public static implicit operator PInt4(uint[] array)
        {
            PInt4 v = null;
            if ((array == null) || (array.Length == 0))
                return v;
            v = new PInt4();
            v.length = array.Length;
            v.handle = Marshal.AllocHGlobal(v.length * size);
            for (int i = 0; i < v.Length; i++)
            {
                Marshal.WriteInt32(v.handle, i*size,(int) array[i]);
            }            
            return v;
        }
        public static implicit operator IntPtr(PInt4 v)
        {
            return v.handle;
        }
        public static PInt4 operator +(PInt4 p, int offset)
        {
            return p.ToPointer(offset);
        }
        public IntPtr Handle
        {
            get
            {
                return handle;
            }
        }
        public void Dispose()
        {
            Marshal.FreeHGlobal (handle);
            handle = IntPtr.Zero;
        }
        public int[] ToArray()
        {
            int[] h = new int[this.length];
            Marshal.Copy(this.handle, h, 0, h.Length);
            return h;
        }
    }
}

