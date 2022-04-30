

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PinceauDecoder.cs
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
file:PinceauDecoder.cs
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
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D.Codec
{
    using IGK.ICore;using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI ;    
    using IGK.DrSStudio.WinUI;
    [CoreCodec("GKPS", "picture/gkds-pen-style", "gkps")]
    class PinceauDecoder :         
        CoreDecoderBase
    {
        public System.Drawing.Image GetBitmap(string filename)
        {//get the first bitmap image
            ICore2DDrawingElement[] i = PinceauDocument.GetStyle(filename);
            /*if (i.Length == 1)
            {
                int w = Math.Max((int)i[0].Bounds.Width, 1);
                int h = Math.Max((int)i[0].Bounds.Height, 1);
                Bitmap bmp = new Bitmap(w,
                    h);
                Graphics g = Graphics.FromImage(bmp);
                i[0].Draw(g);
                g.Dispose();
                return bmp;
            }*/
            return null;
        }
        public bool OpenFile(string filename, ICoreWorkbench workbench)
        {
            ICore2DDrawingElement[] i = PinceauDocument.GetStyle(filename);
            if ((i == null) || (i.Length == 0))
                return false;
            CorePinceauSurface surface = new CorePinceauSurface();
            Core2DDrawingLayeredElement[] l = new
                Core2DDrawingLayeredElement[i.Length];
            Array.Copy(i, l, i.Length);
            surface.CurrentDocument.CurrentLayer.Elements .AddRange (
                l);
            workbench.Surfaces.Add(surface);
            return true;
        }
        public  ICoreWorkingDocument[] OpenFileDocument(string filename)
        {
            ICore2DDrawingLayeredElement [] i = PinceauDocument.GetStyle(filename);
            if ((i == null) || (i.Length == 0))
                return null;
            Core2DDrawingLayeredElement[] l = new
                Core2DDrawingLayeredElement[i.Length];
            Array.Copy(i, l, i.Length);
            PinceauDocument doc = new PinceauDocument();
            doc.CurrentLayer.Elements.AddRange (l);
            return new ICoreWorkingDocument[] { doc };
        }
        public override bool Open(ICoreWorkbench bench, string filename)
        {
            return OpenFile(filename, bench);            
        }
    }
}

