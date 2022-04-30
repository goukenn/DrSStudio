

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFImageMenuBase.cs
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
file:WPFImageMenuBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.Menu
{
    /// <summary>
    /// represent the base wpf image menu
    /// </summary>
    public abstract class WPFImageMenuBase : WPFSurfaceMenuBase
    {
        private IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.ImageElement  m_ImageElement;
        public System.Windows.Controls.Image ImageShape
        {
            get {
                if (this.ImageElement != null)
                    return this.ImageElement.Shape as System.Windows.Controls.Image;
                return null;
            }
        }
        public IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.ImageElement  ImageElement
        {
            get { return m_ImageElement; }
            protected set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                    this.SetupEnableAndVisibility();
                }
            }
        }
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
        protected virtual  void RegisterLayerEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
        {
            wPFLayer.SelectedElementChanged += new EventHandler(wPFLayer_SelectedElementChanged);
        }
        protected virtual void UnRegisterLayerEvent(IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.WPFLayer wPFLayer)
        {
            wPFLayer.SelectedElementChanged -= new EventHandler(wPFLayer_SelectedElementChanged);
        }
        void wPFLayer_SelectedElementChanged(object sender, EventArgs e)
        {
            if (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count == 1)
            {
                this.ImageElement = this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements[0] as 
                    IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.ImageElement ;
            }
            else
                this.ImageElement = null;
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
        protected override bool IsVisible()
        {
            bool v =
                (this.CurrentSurface != null) &&
                (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements.Count == 1) &&
                (this.CurrentSurface.CurrentDocument.CurrentLayer.SelectedElements[0] is IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects.ImageElement);
            return v;
        }
    }
}

