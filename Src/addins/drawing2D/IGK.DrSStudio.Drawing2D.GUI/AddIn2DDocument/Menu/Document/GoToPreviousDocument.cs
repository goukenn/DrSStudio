

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GoToPreviousDocument.cs
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
file:GoToPreviousDocument.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Document
{
    [IGKD2DDocumentMenuAttribute("GoToPrevious", 0X20, IsShortcutMenuChild = true, Shortcut = enuKeys.Shift | enuKeys.P)]
    class GoToPreviousDocument : IGKD2DDrawingDocumentMenuBase
    {
        protected override bool PerformAction()
        {
            int i = this.CurrentSurface.Documents.IndexOf(this.CurrentDocument);
            if (i>0)
            {
                this.CurrentSurface.CurrentDocument = this.CurrentSurface.Documents[i - 1];
                return true;
            }
            return false;
        }
    }
}

