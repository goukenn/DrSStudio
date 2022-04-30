

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreBitmapManager.cs
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
file:CoreBitmapManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using IGK.DrSStudio.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore.WinCore;
    using IGK.ICore.Codec;
    using System.IO;

    public static class CoreBitmapManager
    {
        public static Bitmap GetBitmapFormFile(string filename, int index, bool clipped, int width, int height)
        {
            if (!File.Exists(filename))
            {
                return null;
            }
            ICoreWorkingDocument[] doc = CoreDecoder.Instance.GetDocuments(File.ReadAllBytes(filename));
            if ((doc != null) && (doc.Length > 0) && (doc.Length>=index))
            {
                ICore2DDrawingDocument v_doc = doc[index] as ICore2DDrawingDocument;
                if (v_doc != null)
                {
                    ICoreBitmap bmp = v_doc.ToBitmap(width,height,true, CoreScreen.DpiX, CoreScreen.DpiY ) ;
                    return bmp.ToGdiBitmap();
                }
            }
            return null;
        }
        public static Region GetClipRegion(ICore2DDrawingDocument doc, Rectanglei rectangle)
        {
            float fx = rectangle.Width / (float)doc.Width;
            float fy = rectangle.Height / (float)doc.Height;
            fx = Math.Min(fx, fy);
            fy = fx;
            int posx = (int)(((-doc.Width * fx) / 2.0f) + (rectangle.Width / 2.0f));
            int posy = (int)(((-doc.Height * fx) / 2.0f) + (rectangle.Height / 2.0f));
            return new Region(new RectangleF(posx, posy, doc.Width * fx, doc.Height * fy));
        }
    }
}

