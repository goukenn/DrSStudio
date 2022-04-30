

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerExplorerTool.cs
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
using IGK.ICore.Tools;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D
{

    [CoreTools("Tool.Drawing2D.LayerExplorerTool",
        ImageKey=CoreImageKeys.TOOL_LAYER_EXPLORER_GKDS)]
    public class LayerExplorerTool : Core2DDrawingToolBase
    {
        private static LayerExplorerTool sm_instance;
        
        private LayerExplorerTool()
        {
        }

        public static LayerExplorerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static LayerExplorerTool()
        {
            sm_instance = new LayerExplorerTool();

        }
        public new LayerExplorerViewControl HostedControl {
            get {
                return base.HostedControl as LayerExplorerViewControl;
            }
        }
        protected override void GenerateHostedControl()
        {
            LayerExplorerViewControl c = new LayerExplorerViewControl(this);
            c.CaptionKey = "title.Drawing2DlayerExplorer";
            c.VisibleChanged += c_VisibleChanged;
            base.HostedControl = c;
            //register context menu to host control
            if (this.MainForm.AppContextMenu != null)
            {
                this.HostedControl.ContextMenuStrip = this.MainForm.AppContextMenu as ContextMenuStrip;
            }
            else
            {
                this.MainForm.AppContextMenuChanged += LayerExplorerTool_ContextMenuStripChanged;
            }
              

        }

        void LayerExplorerTool_ContextMenuStripChanged(object sender, EventArgs e)
        {//link context menu
            this.HostedControl.ContextMenuStrip = this.MainForm.AppContextMenu as ContextMenuStrip;
        }

        void c_VisibleChanged(object sender, EventArgs e)
        {
            UpdateEditedLayer();
           
        }
     

        private void UpdateEditedLayer()
        {
            if ((this.HostedControl.Visible) && (this.CurrentSurface != null))
            {
                this.HostedControl.SetLayer(this.CurrentSurface.CurrentLayer);
            }
            else
            {
                this.HostedControl.SetLayer(null);
            }
        }
        protected override void OnCurrentSurfaceChanged(CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>  e)
        {
            base.OnCurrentSurfaceChanged(e);
            this.UpdateEditedLayer();
        }
        protected override void RegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            base.RegisterDocumentEvent(document);
            document.CurrentLayerChanged += document_CurrentLayerChanged;
            this.UpdateEditedLayer();
        }
        protected override void UnRegisterDocumentEvent(ICore2DDrawingDocument document)
        {
            document.CurrentLayerChanged -= document_CurrentLayerChanged;
            base.UnRegisterDocumentEvent(document);
        }

        void document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            this.UpdateEditedLayer();
        }
    }
}
