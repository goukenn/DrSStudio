

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DocumentManager.cs
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
file:DocumentManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Tools
{
    /// <summary>
    /// represent the document manager
    /// </summary>
    class DocumentManager : HistoryToolManagerBase
    {
        public DocumentManager(HistorySurfaceManager tool):base(tool)
        {
        }
        protected override void RegisterSurfaceEvent(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.DocumentAdded += new Core2DDrawingDocumentEventHandler(surface_DocumentAdded);
            surface.DocumentRemoved += new Core2DDrawingDocumentEventHandler(surface_DocumentRemoved);
        }
        protected override void UnRegisterSurfaceEvent(IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
        {
            surface.DocumentAdded -= new Core2DDrawingDocumentEventHandler(surface_DocumentAdded);
            surface.DocumentRemoved -= new Core2DDrawingDocumentEventHandler(surface_DocumentRemoved);
            base.UnRegisterSurfaceEvent(surface);
        }
        void surface_DocumentRemoved(object o, Core2DDrawingDocumentEventArgs e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {
                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._DocumentRemoved(
                    CurrentSurface,
                    e.Document ,
                    e.ZIndex 
                    ));
            }
        }
        void surface_DocumentAdded(object o, Core2DDrawingDocumentEventArgs e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {
                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._DocumentAdded(
                    CurrentSurface,
                    e.Document 
                    ));
            }
        }
    }
}

