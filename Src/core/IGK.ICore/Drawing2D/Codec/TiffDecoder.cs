

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TiffDecoder.cs
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
file:TiffDecoder.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Codec
{
    [CoreCodec("TIFF-DECODER", 
        "image/tiff", 
        "tiff", 
        Category = CoreConstant.CAT_PICTURE)]
    class TiffDecoder : CoreBitmapDecoder
    {
        private ICoreBitmap[] GetAllPages(string file)
        {
            ICoreBitmap c = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(file);
            if (c == null)
                return null;
            if (c.FrameCount == 0)
                return null;
            ICoreBitmap[] t = new ICoreBitmap[c.FrameCount];
            for (int i = 0; i < c.FrameCount; i++)
            {
                t[i] = c.GetFrame(i);
            }
            return t;
        }
        public override bool Open(ICore.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            return base.Open(bench, filename, selectCreatedSurface );
        }
    }
}

