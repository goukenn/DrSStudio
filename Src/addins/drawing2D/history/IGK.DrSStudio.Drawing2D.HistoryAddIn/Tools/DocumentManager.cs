

/*
IGKDEV @ 2008-2016
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
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
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
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.DocumentAdded += surface_DocumentAdded;
            surface.DocumentRemoved += surface_DocumentRemoved;
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {

            surface.DocumentAdded -= surface_DocumentAdded;
            surface.DocumentRemoved -= surface_DocumentRemoved;
            base.UnRegisterSurfaceEvent(surface);
        }

     

        private void surface_DocumentRemoved(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {

                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._DocumentRemoved(
                    CurrentSurface,
                    e.Item
                    ));
            }
        }

           private void surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
         
            
            if (Tools.HistorySurfaceManager.Instance.CanAdd)
            {

                Tools.HistorySurfaceManager.Instance.Add(new HistoryActions._DocumentAdded(
                    CurrentSurface,
                    e.Item 
                    ));
            }
        }
    }
}
