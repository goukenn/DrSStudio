

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBitmapHeaderStruct.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreBitmapHeaderStruct.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
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
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
namespace IGK.DrSStudio
{
    [StructLayout(LayoutKind.Sequential)]
    struct CoreBmpHeader
    {
        internal short bmptype;
        internal int reserved;
        internal int bmpSize;
        internal int offset;
        internal readonly static CoreBmpHeader BMP;
        static CoreBmpHeader()
        {
            BMP = new CoreBmpHeader();
            BMP.bmptype = (short)(('B') | ('M'<<8));
        }
        internal void WriteData(BinaryWriter binW)
        {
            binW.Write(bmptype);
            binW.Write(reserved);
            binW.Write(bmpSize);
            binW.Write(offset);
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    struct CoreBmpHeaderInfo
    {
        internal int size;
        internal int width;
        internal int height;
        internal short plane;
        internal short bitcount;
        internal int compression;
        internal int bitmapSize;
        internal int dpih;
        internal int dpiv;
        internal int colorspalette;
        internal int importantcolor;
        internal const int COMP_NONE = 0;
        internal const int COMP_RLE8 = 1;
        internal const int COMP_RLE4 = 2;
        internal const int COMP_BITF = 3;
        public static CoreBmpHeaderInfo Empty;
        static CoreBmpHeaderInfo()
        {
            Empty = new CoreBmpHeaderInfo();
            Empty.size = 40;
            Empty.bitcount = 32;
            Empty.plane = 1;           
        }
        internal void WriteData(BinaryWriter binW)
        {
            binW.Write(size);
            binW.Write(width);
            binW.Write(height);
            binW.Write(plane);
            binW.Write(bitcount);
            binW.Write(compression);
            binW.Write(bitmapSize);
            binW.Write(dpih);
            binW.Write(dpiv);
            binW.Write(this.colorspalette);
            binW.Write(this.importantcolor);
        }
    }
}

