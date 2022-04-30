

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerStatusToolInfo.cs
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
using IGK.DrSStudio.Drawing2D;
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Resources;
using IGK.ICore.Tools;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D.Tools
{

    [CoreTools("Tool.Drawing2D.LayerStatusInfoTool")]
    class LayerStatusInfoTool : Core2DDrawingToolBase 
    {
        private static LayerStatusInfoTool sm_instance;
        private LayerStatusInfoTool()
        {
        }

        public static LayerStatusInfoTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static LayerStatusInfoTool()
        {
            sm_instance = new LayerStatusInfoTool();
        }
        IGKWinCoreStatusTextItem m_info;
        protected override void GenerateHostedControl()
        {
            m_info = new IGKWinCoreStatusTextItem();
            m_info.Bounds = new Rectanglef(0, 0, 120, 24);
            m_info.Index = 0x0600;
            m_info.Visible = false;
            this.Workbench.GetLayoutManager()?.StatusControl.Items.Add(m_info);
            base.GenerateHostedControl();
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {
            base.OnCurrentSurfaceChanged(e);
            UpdateTextInfo();
        }
        protected override void UnRegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            surface.CurrentDocumentChanged -= surface_CurrentDocumentChanged;
            surface.DocumentAdded -= surface_DocumentAdded;
            surface.DocumentRemoved -= surface_DocumentRemoved;
            base.UnRegisterSurfaceEvent(surface);
        }
        protected override void RegisterSurfaceEvent(ICore2DDrawingSurface surface)
        {
            base.RegisterSurfaceEvent(surface);
            surface.CurrentDocumentChanged += surface_CurrentDocumentChanged;
            surface.DocumentAdded += surface_DocumentAdded;
            surface.DocumentRemoved += surface_DocumentRemoved;
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {            
            document.CurrentLayerChanged += document_CurrentLayerChanged;
            document.LayerAdded += document_LayerAdded;
            document.LayerRemoved += document_LayerRemoved;
        }

        void document_LayerRemoved(object o, Core2DDrawingLayerEventArgs e)
        {
            UpdateTextInfo();
        }

        void document_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            UpdateTextInfo();
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {            
            document.CurrentLayerChanged -= document_CurrentLayerChanged;
            document.LayerAdded -= document_LayerAdded;
            document.LayerRemoved -= document_LayerRemoved;
        }

        void document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            UpdateTextInfo();   
        }

        void surface_DocumentRemoved(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            this.UpdateTextInfo();
        }

        void surface_DocumentAdded(object sender, CoreItemEventArgs<ICore2DDrawingDocument> e)
        {
            this.UpdateTextInfo();
        }

        void surface_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
        {
            UpdateTextInfo();
        }

        private void UpdateTextInfo()
        {
            var e = this.CurrentSurface;
            if (e!=null)
            {
                this.m_info.Text = string.Format("Layer:{0}/{1}",
                    e.CurrentLayer.ZIndex +1 ,
                    e.CurrentDocument.Layers.Count
                    );
                this.m_info.Visible = true;
            }
            else {
                this.m_info.Text = CoreResources.GetString ("tip.NoLayer");
                this.m_info.Visible = false;
            }
        }
    }
}
