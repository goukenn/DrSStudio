

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerViewDocumentTool.cs
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
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Tools
{
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Tools;
    [CoreTools("Tool.Drawing2D.LayerViewTool",
        ImageKey = CoreImageKeys.TOOL_LAYER_VIEW_GKDS,
        Description="Tools that view layer")]
    class LayerViewTool : Core2DDrawingToolBase 
    {
        private static LayerViewTool sm_instance;
        private LayerViewTool()
        {
        }

        public static LayerViewTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static LayerViewTool()
        {
            sm_instance = new LayerViewTool();

        }
        public new LayerViewGUI HostedControl {
            get {
                return base.HostedControl as LayerViewGUI;
            }
        }

        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.updateLayerView();
        }
        protected override void GenerateHostedControl()
        {
            base.HostedControl = new LayerViewGUI();
        }
        protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.UnRegisterLayerEvent(layer);
        }
        protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            base.RegisterLayerEvent(layer);
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
            base.UnRegisterSurfaceEvent(surface);
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.LayerZIndexChanged += document_LayerZIndexChanged;
        }

        void document_LayerZIndexChanged(object o, CoreWorkingObjectZIndexChangedEventArgs e)
        {
            this.updateLayerView();
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.LayerZIndexChanged -= document_LayerZIndexChanged;
            base.UnRegisterDocumentEvent(document);
        }

        void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            updateLayerView();
        }

        private void updateLayerView()
        {
            if (this.CurrentSurface != null)
            {
                this.HostedControl.Setup(this.CurrentSurface.CurrentDocument);
            }
            else
                this.HostedControl.Setup(null);
        }
    }
}
