

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CopyDocumentImageElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CopyDocumentImageElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WinUI;
namespace IGK.DrSStudio.Drawing2D.Menu.Document.Copy
{
    [IGKD2DDrawingCopyDocumentMenuAttribute("ImageElement", 0)]
    class CopyImageElement : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            using (ICoreBitmap bmp = this.CurrentDocument.ToBitmap())
            {
                ImageElement img = ImageElement.CreateFromBitmap(bmp);
                bmp.Dispose();
                if (img != null)
                {
                    IGK2DDClipBoard.CopyToClipBoard(IGK2DDClipBoard.TAG_COPY_CONTEXT,
                        this.CurrentSurface,
                       new ICore2DDrawingLayeredElement[] { img });
                    img.Dispose();
                }
            }
            return base.PerformAction();
        }
    }
}

