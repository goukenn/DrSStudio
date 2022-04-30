

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ACMExtension.cs
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
file:ACMExtension.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
namespace IGK.AVIApi.ACM
{
    using AVI;
    public static class ACMExtension
    {
        public static object ConvertTo(this IntPtr t , Type rType)
        {
            object obj = Marshal.PtrToStructure(t, rType);
            return obj;
        }
        public static T Convert<T>(this object t)
        {
            if (t == null) 
                return default(T);
            int s = 0;
            if (t is IntPtr)
            {               
               return (T)Marshal.PtrToStructure((IntPtr)t, typeof(T));
            }
            s = Marshal.SizeOf(t);
            IntPtr v_h = Marshal.AllocCoTaskMem(s);
            Marshal.StructureToPtr(t, v_h,true );
            T ms = (T)Marshal.PtrToStructure(v_h, typeof(T));
            Marshal.FreeCoTaskMem(v_h);
            return ms;
        }
        public static  IntPtr ToPointer(this object t, int size)
        {
            int s = size;
            if (s == -1)
                s = Marshal.SizeOf(t);
            IntPtr v_h = Marshal.AllocCoTaskMem(s);
            Marshal.StructureToPtr(t, v_h, true);
            return v_h;
        }
    }
}

