

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFBaseLayerMenuSurface.cs
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
file:WPFBaseLayerMenuSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu
{
    public abstract class WPFBaseLayerMenuSurface : WPFSurfaceMenuBase 
    {
        protected override void RegisterSurfaceEvent(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFHostSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentDocumentChanged += new CoreElementChangedEventHandler<IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument>(surface_CurrentDocumentChanged);
            RegisterDocumentEvent(surface.CurrentDocument);
            RegisterLayerEvent(surface.CurrentDocument.CurrentLayer);
        }
        protected override void UnRegisterSurfaceEvent(IGK.DrSStudio.WPFSurfaceAddIn.WinUI.WPFHostSurface surface)
        {
            UnRegisterDocumentEvent(surface.CurrentDocument);
            UnRegisterLayerEvent(surface.CurrentDocument.CurrentLayer);
            surface.CurrentDocumentChanged -= new CoreElementChangedEventHandler<IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument>(surface_CurrentDocumentChanged);
            base.UnRegisterSurfaceEvent(surface);
        }
        private void RegisterLayerEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
        {
        }
        private void UnRegisterLayerEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
        {
        }
        void surface_CurrentDocumentChanged(object o, CoreWorkingElementChangedEventArgs<IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument> e)
        {
            if (e.OldElement != null) UnRegisterDocumentEvent(e.OldElement);
            if (e.NewElement != null) RegisterDocumentEvent(e.NewElement);
        }
        protected virtual void UnRegisterDocumentEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument wPFDocument)
        {
            wPFDocument.CurrentLayerChanged += new IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayerChangeEventHandler(wPFDocument_CurrentLayerChanged);
        }
        void wPFDocument_CurrentLayerChanged(object o, IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayerChangeEventArgs e)
        {
            if (e.OldLayer != null) UnRegisterLayerEvent(e.OldLayer);
            if (e.NewLayer != null) RegisterLayerEvent(e.NewLayer);
        }
        protected virtual void RegisterDocumentEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFDocument wPFDocument)
        {
            wPFDocument.CurrentLayerChanged += new IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayerChangeEventHandler(wPFDocument_CurrentLayerChanged);
        }
    }
}

