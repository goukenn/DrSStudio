

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IntPtrExtensions.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿//////////////////////////////////////////////////////////////////////////////////
//	GDI+ Extensions
//	Written by Aaron Lee Murgatroyd (http://home.exetel.com.au/amurgshere/)
//	A CodePlex project (http://csharpgdiplus11.codeplex.com/)
//  Released under the Microsoft Public License (Ms-PL) .
//////////////////////////////////////////////////////////////////////////////////

using System;
using System.Drawing;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// Provides internal extensions for the IntPtr struct.
    /// </summary>
    internal static class IntPtrExtensions
    {
        /// <summary>
        /// Gets a Bitmap object for a native GDI+ bitmap handle.
        /// </summary>
        /// <param name="nativeBitmap">The native handle to get the bitmap for.</param>
        /// <returns>A Bitmap.</returns>
        public static Bitmap NativeBitmapPtrToBitmap(this IntPtr nativeBitmap)
        {
            return typeof(Bitmap).InvokeStaticPrivateMethod<Bitmap>("FromGDIplus", nativeBitmap);
        }
    }
}
