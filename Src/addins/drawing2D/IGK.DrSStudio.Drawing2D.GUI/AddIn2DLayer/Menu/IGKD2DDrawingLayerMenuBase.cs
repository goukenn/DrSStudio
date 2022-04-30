

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingLayerMenuBase.cs
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
file:IGKD2DDrawingLayerMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Menu;
    
    public class IGKD2DDrawingLayerMenuBase : Core2DDrawingMenuBase
    {
        public ICore2DDrawingLayer CurrentLayer {
            get {
                if (this.CurrentSurface != null)
                return this.CurrentSurface.CurrentLayer;
                return null;
            }
        }
        public ICore2DDrawingDocument CurrentDocument
        {
            get
            {
                if (this.CurrentSurface == null)
                {
                    return null;
                }
                return this.CurrentSurface.CurrentDocument;
            }
        }
        protected override bool IsEnabled()
        {
            return this.DefaultVisible && (this.CurrentLayer != null);
        }
        protected override bool IsVisible()
        {
            return this.IsEnabled();
        }
    }
}

