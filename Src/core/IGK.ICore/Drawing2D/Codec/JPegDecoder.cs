

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: JPegDecoder.cs
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
file:JPegDecoder.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D.Codec
{
    [CoreCodec("jpeg-decoder", "image/jpeg", "jpeg;jpg", Category=CoreConstant.CAT_PICTURE )]
    class JPegDecoder : 
        CoreBitmapDecoder  ,
        ICoreBitmapDecoder
    {
        public override bool Open(ICore.WinUI.ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            return base.Open(bench, filename, selectCreatedSurface);
        }

        
    }
}

