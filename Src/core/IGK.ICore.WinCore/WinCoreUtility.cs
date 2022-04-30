

using IGK.ICore;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreUtility.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    using IGK.ICore.WinCore;

    class WinCoreUtility
    {
        static WinCoreUtility() { 

        }
        /// <summary>
        /// measure string width this font
        /// </summary>
        /// <param name="text">text to measure</param>
        /// <param name="font">font used to measure</param>
        /// <returns>size </returns>
        public static Size2f MeasureString(string text, System.Drawing.Font font)
        {
            Graphics g = Graphics.FromHwnd (IntPtr.Zero);
            WinCoreBitmapDeviceVisitor dev = WinCoreBitmapDeviceVisitor.Create (g);
            Size2f s = dev.MeasureString(text, font, new Size2f(short.MaxValue, short.MaxValue), new System.Drawing.StringFormat());
            g.Dispose();
            return s;
        }
    }
}
