

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CopyDocumentBitmap.cs
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
file:CopyDocumentBitmap.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WinUI;
namespace IGK.DrSStudio.Drawing2D.Menu.Document.Copy
{
    [IGKD2DDrawingCopyDocumentMenuAttribute("Bitmap", 0)]
    class CopyBitmap : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            using (ICoreBitmap bmp = this.CurrentDocument.ToBitmap())
            {
                WinCoreBitmap v = bmp as WinCoreBitmap;
                if (v != null)
                {
                    IGK2DDClipBoard.CopyToClipBoard(v.Bitmap);
                }
            }
        
            return base.PerformAction();
        }
    }
}

