

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingDocumentMenuBase.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IGKD2DDrawingDocumentMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.Menu;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    /// <summary>
    /// represent a menu drawing 2d document
    /// </summary>
    public class IGKD2DDrawingDocumentMenuBase : Core2DDrawingMenuBase 
    {
        public ICore2DDrawingDocument CurrentDocument {
            get {
                if (this.CurrentSurface==null)
                {
                    return null;
                }
                return this.CurrentSurface.CurrentDocument;
            }
        }
        protected override bool IsEnabled()
        {
            return this.DefaultVisible && (this.CurrentDocument != null);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();
        }
       
    }
}

