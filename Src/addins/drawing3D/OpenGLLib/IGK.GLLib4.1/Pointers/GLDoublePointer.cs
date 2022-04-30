

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLDoublePointer.cs
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
file:GLDoublePointer.cs
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices ;
namespace IGK.GLLib.Pointers
{
    /// <summary>
    /// reprent a GLDouble Pointer
    /// </summary>
    public sealed class GLDoublePointer
    {
        internal IntPtr handle;
        internal static readonly int size = Marshal.SizeOf (typeof(double));
        private int innersize;
        private GLDoublePointer()
        {
        }
        internal GLDoublePointer(IntPtr h)
        {
            this.handle = h;
            innersize = -1;
        }
        public GLDoublePointer(uint size)
        {
            this.handle = Marshal.AllocCoTaskMem((int)size);
            innersize = (int) size;
        }
        ~GLDoublePointer()
        {
            Marshal.FreeCoTaskMem(this.handle);
        }
        public static GLDoublePointer FromHandle(IntPtr h)
        {
            GLDoublePointer p = new GLDoublePointer(h);
            return p;
        }
        public double this[int index]
        {
            get {
                if (innersize == -1)
                    return 0;
                return (double)Marshal.ReadInt64(this.handle, index * size);
            }
            set {
                if (innersize == -1)
                    return;
                Marshal.WriteInt64 (this.handle ,index,(long) value);
            }
        }
        public static GLDoublePointer operator +(GLDoublePointer d, int i)
        {
            GLDoublePointer v = new GLDoublePointer(new IntPtr(d.handle.ToInt32() + (i * size)));
            return v;
        }
        public static implicit operator IntPtr(GLDoublePointer d)
        {
            return d.handle;
        }
        public static implicit operator GLDoublePointer(double[]  d)
        {
            if ((d == null) || (d.Length <= 0))
                return null;
            int size = Marshal.SizeOf(typeof(double));
            IntPtr alloc = Marshal.AllocCoTaskMem(d.Length*size);
            GLDoublePointer ptr = new GLDoublePointer();
            ptr.handle = alloc;
            ptr.innersize = d.Length;
            Marshal.Copy(d, 0, alloc, d.Length);            
            return ptr;
        }
    }
}

