

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DAddDuplicateDocumentMenu.cs
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
file:IGKD2DAddDuplicateDocumentMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
    [IGKD2DDocumentMenuAttribute("DuplicateDocument", 2)]
    class IGKD2DAddDuplicateDocumentMenu : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            int i = this.CurrentSurface.Documents.Count;
            var d = this.CurrentDocument.Clone() as ICore2DDrawingDocument;

            this.CurrentSurface.Documents.Add(d);
            return i < this.CurrentSurface.Documents.Count;
        }
    }
}

