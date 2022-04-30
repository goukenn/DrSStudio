

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ImagingUtils.cs
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
﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Imaging
{
    /// <summary>
    /// represent the Imaging Utility Class
    /// </summary>
    public static class ImagingUtils
    {
        /// <summary>
        /// get the cloned bitmap
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Bitmap GetClonedBitmap(ICoreBitmap bitmap)
        { 
            if (bitmap == null)return null;
            ICoreBitmap c = (bitmap.Clone() as ICoreBitmap);
            Bitmap bmp = c.ToGdiBitmap();
            c.Dispose();
            return bmp;
        }

        public static void SetBitmap(this ImageElement element, Bitmap bmp, bool temp)
        {
            //
            if ((element !=null) && (bmp !=null))
            element.SetBitmap(WinCoreBitmap.Create(bmp), temp);
        }
    }
}
