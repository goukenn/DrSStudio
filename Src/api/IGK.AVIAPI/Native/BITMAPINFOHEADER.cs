

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BITMAPINFOHEADER.cs
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
file:BITMAPINFOHEADER.cs
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
namespace IGK.AVIApi
{
    using IGK.ICore;using IGK.AVIApi.AVI;
    [StructLayout(LayoutKind.Sequential)]
    internal struct BITMAPINFOHEADER : IAVIFormat 
    {
        public const int BI_RGB = 0;
        public const int BI_RLE8 = 1;
        public const int BI_RLE4 = 2;
        public const int BI_BITFIELDS = 3;
        public const int BI_JPEG = 4;
        public const int BI_PNG = 5;
        //internal short biSize;
        //internal int biWidth;
        //internal int biHeight;
        //internal byte biPlanes;
        //internal byte biBitCount;
        //internal int biCompression;
        //internal int biSizeImage;
        //internal int biXPelsPerMeter;
        //internal int biYPelsPerMeter;
        //internal int biClrUsed;
        //internal int biClrImportant;
        public Int32 biSize;
        public Int32 biWidth;
        public Int32 biHeight;
        public Int16 biPlanes;
        public Int16 biBitCount;
        public Int32 biCompression;
        public Int32 biSizeImage;
        public Int32 biXPelsPerMeter;
        public Int32 biYPelsPerMeter;
        public Int32 biClrUsed;
        public Int32 biClrImportant;
        /// <summary>
        /// return a pointer ot this structure. free the pointer with Marshal.FreeCoTaskMem
        /// </summary>
        /// <returns>Created pointer sturcture</returns>
        public IntPtr ToPointer()
        {
            IntPtr h = Marshal.AllocCoTaskMem(Marshal.SizeOf(this));
            Marshal.StructureToPtr(this, h, false);
            return h;
        }
        public int Size
        {
            get { return Marshal.SizeOf (this); }
        }
    }
}

