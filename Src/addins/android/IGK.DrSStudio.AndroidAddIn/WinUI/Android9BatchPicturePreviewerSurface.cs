

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Android9BatchPicturePreviewSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.WinUI
{
    [AndroidSurface ("Android9BatchPicturePreviewerSurface")]
    public class Android9BatchPicturePreviewerSurface : AndroidSurfaceBase 
    {
        private ImageElement m_imageElement;

        public ImageElement ImageElement {
            get {
                return this.m_imageElement;
            }
        }
        public bool Open(string filename)
        {
            Bitmap bmp = null;

            try
            {
                bmp = Bitmap.FromFile(filename) as Bitmap ;
                if ((bmp.Width > 2) && (bmp.Height > 2))
                {
                    Android9BatchPicture pic = new Android9BatchPicture();

                    this.m_imageElement = pic;
                }
            }
            catch { 
            }
            return false;
        }
    }
}
