

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerViewGUI.cs
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
using IGK.ICore.Drawing2D.Tools;
using IGK.ICore.Resources;
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Drawing2D.Tools;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    class LayerViewGUI : IGKXToolConfigControlBase
    {
        private LayerViewPanelGUI c_layerViewPanelGUI;
        private IGKXToolStrip c_toolStripActions;

        private IGKXToolStripButton c_addNewLayer;
        
        private IGKXToolStripButton c_toggleview;
        private IGKXToolStripButton c_duplicateLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private IGKXToolStripButton c_clearCurrentLayer;
        private IGKXToolStripButton c_deleteCurrentLayer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private IGKXToolStripButton c_moveLayerBack;
        private IGKXToolStripButton c_moveLayerUp;
        private IGKXToolStripButton c_movetobegin;
        private IGKXToolStripButton c_movetoend;
        
        private ICore2DDrawingDocument m_editableDocument;
    
        public LayerViewGUI()
            : base(LayerViewTool.Instance)
        {
            this.InitializeComponent();
            this.InitControl();
        }

        private void InitControl()
        {
            this.c_addNewLayer.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_ADD_GKDS);
            this.c_addNewLayer.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;

            this.c_duplicateLayer.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_DUPLICATE_GKDS);
            this.c_toggleview.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_LAYER_VIEW_GKDS);
            this.c_clearCurrentLayer.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLEAR_GKDS);

            this.c_moveLayerBack.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_BACK_GKDS);
            this.c_moveLayerUp.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_UP_GKDS);

            this.c_movetobegin.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_BEGIN_GKDS);
            this.c_movetoend.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_END_GKDS);
            this.c_deleteCurrentLayer.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_DELETE_GKDS);


            this.c_moveLayerBack.Action = CoreSystem.GetAction("2DDrawingLayer.MoveBack");
            this.c_moveLayerUp.Action = CoreSystem.GetAction("2DDrawingLayer.MoveUp");
            this.c_movetobegin.Action = CoreSystem.GetAction("2DDrawingLayer.MoveBegin");
            this.c_movetoend.Action = CoreSystem.GetAction("2DDrawingLayer.MoveEnd");

            this.c_deleteCurrentLayer.Action = CoreSystem.GetAction("2DDrawingLayer.DeleteLayer");

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayerViewGUI));
            this.c_toolStripActions = new IGKXToolStrip();
            this.c_movetobegin = new IGKXToolStripButton();
            this.c_movetoend = new IGKXToolStripButton();
            this.c_deleteCurrentLayer = new IGKXToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.c_addNewLayer = new IGKXToolStripButton();
            this.c_duplicateLayer = new IGKXToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_clearCurrentLayer = new IGKXToolStripButton();
            this.c_toggleview = new IGKXToolStripButton();
            this.c_layerViewPanelGUI = new IGK.DrSStudio.Drawing2D.WinUI.LayerViewPanelGUI();
            this.c_moveLayerUp = new IGKXToolStripButton();
            this.c_moveLayerBack = new IGKXToolStripButton();
            this.c_toolStripActions.SuspendLayout();
            this.SuspendLayout();

            this.c_toggleview.Click += c_toggleview_Click;
            // 
            // c_toolStripActions
            // 
            this.c_toolStripActions.BackgroundDocument = null;
            this.c_toolStripActions.CaptionKey = null;
            this.c_toolStripActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_addNewLayer,
            this.c_duplicateLayer,
            this.c_toggleview ,
            this.toolStripSeparator1,
            this.c_clearCurrentLayer,
            this.c_deleteCurrentLayer ,
            this.toolStripSeparator2 ,
            this.c_moveLayerBack ,
            this.c_moveLayerUp,
            this.c_movetobegin ,
            this.c_movetoend });
            this.c_toolStripActions.Location = new System.Drawing.Point(0, 0);
            this.c_toolStripActions.Name = "c_toolStripActions";
            this.c_toolStripActions.Size = new System.Drawing.Size(303, 25);
            this.c_toolStripActions.TabIndex = 0;
            this.c_toolStripActions.Text = "xToolStrip1";
            // 
            // c_addNewLayer
            // 
            this.c_addNewLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_addNewLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_addNewLayer.Name = "c_addNewLayer";
            this.c_addNewLayer.Size = new System.Drawing.Size(23, 22);
            this.c_addNewLayer.Click += new System.EventHandler(this.c_addNewLayer_Click);
            // 
            // c_duplicateLayer
            // 
            this.c_duplicateLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_duplicateLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_duplicateLayer.Name = "c_duplicateLayer";
            this.c_duplicateLayer.Size = new System.Drawing.Size(23, 22);
            this.c_duplicateLayer.Click += new System.EventHandler(this.c_duplicateLayer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // c_clearCurrentLayer
            // 
            this.c_clearCurrentLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_clearCurrentLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_clearCurrentLayer.Name = "c_clearCurrentLayer";
            this.c_clearCurrentLayer.Size = new System.Drawing.Size(23, 22);
            this.c_clearCurrentLayer.Click += new System.EventHandler(this.c_clearCurrentLayer_Click);
            // 
            // c_layerViewPanelGUI
            // 
            this.c_layerViewPanelGUI.CaptionKey = null;
            this.c_layerViewPanelGUI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_layerViewPanelGUI.ItemHeight = 32;
            this.c_layerViewPanelGUI.Location = new System.Drawing.Point(0, 25);
            this.c_layerViewPanelGUI.Name = "layerViewPanelGUI1";
            this.c_layerViewPanelGUI.Size = new System.Drawing.Size(303, 311);
            this.c_layerViewPanelGUI.TabIndex = 0;
            // 
            // LayerViewDocumentGUI
            // 
            this.Controls.Add(this.c_layerViewPanelGUI);
            this.Controls.Add(this.c_toolStripActions);
            this.Name = "LayerViewDocumentGUI";
            this.Size = new System.Drawing.Size(303, 336);
            this.c_toolStripActions.ResumeLayout(false);
            this.c_toolStripActions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void c_toggleview_Click(object sender, EventArgs e)
        {
            if (this.m_editableDocument != null)
            {
                this.m_editableDocument.CurrentLayer.View = !
                    this.m_editableDocument.CurrentLayer.View;
                var v_s = (this.CurrentSurface as ICore2DDrawingSurface);
                if(v_s!=null)
                    v_s.RefreshScene();
            }
        }

        internal void Setup(ICore2DDrawingDocument dx)
        {
            if (this.m_editableDocument == dx)
            {
                return;
            }
            this.c_toolStripActions.Enable(false);
            if (this.m_editableDocument != null)
                unregiserDocumentEvent();
            this.m_editableDocument = dx;
            if (this.m_editableDocument != null)
                regiserDocumentEvent();

            this.c_layerViewPanelGUI.SuspendLayout();
            this.c_layerViewPanelGUI.Controls.Clear();

            if (dx != null) {
                int x = 0;
                int y = 0;
                int width = this.c_layerViewPanelGUI.Width;
                int height = this.c_layerViewPanelGUI.ItemHeight;
                foreach (Core2DDrawingLayer  item in dx.Layers)
                {
                    this.c_layerViewPanelGUI.Controls.Add(new LayerViewItem(item) { 
                         Bounds = new System.Drawing.Rectangle (x,y, width, height)
                    });
                    y+= height ;
                }
                this.c_toolStripActions.Enable(true);
            }
            this.c_layerViewPanelGUI.ResumeLayout();
        }

        private void regiserDocumentEvent()
        {
            this.m_editableDocument.CurrentLayerChanged += m_editableDocument_CurrentLayerChanged;
            this.m_editableDocument.LayerAdded += m_editableDocument_LayerAdded;
            this.m_editableDocument.LayerRemoved += m_editableDocument_LayerRemoved;
            this.m_editableDocument.LayerZIndexChanged += m_editableDocument_LayerZIndexChanged;
        }
        private void unregiserDocumentEvent()
        {
            this.m_editableDocument.CurrentLayerChanged -= m_editableDocument_CurrentLayerChanged;
            this.m_editableDocument.LayerAdded -= m_editableDocument_LayerAdded;
            this.m_editableDocument.LayerRemoved -= m_editableDocument_LayerRemoved;
            this.m_editableDocument.LayerZIndexChanged -= m_editableDocument_LayerZIndexChanged;
        }

        void m_editableDocument_LayerZIndexChanged(object o, CoreWorkingObjectZIndexChangedEventArgs e)
        {
            //update layer index
            this.c_layerViewPanelGUI.SuspendLayout();

            Core2DDrawingLayer item  = e.Item as Core2DDrawingLayer ;
            if (item != null)
            {
                this.c_layerViewPanelGUI.UpdateIndex(item, e.CurrentIndex);    
                //item.updateIndex(item, e.CurrentIndex);
            }
            this.c_layerViewPanelGUI.ResumeLayout();
        }

        void m_editableDocument_LayerRemoved(object o, Core2DDrawingLayerEventArgs e)
        {
            this.c_layerViewPanelGUI.SuspendLayout();
            this.c_layerViewPanelGUI.Controls.Clear();

            if (m_editableDocument != null)
            {
                int x = 0;
                int y = 0;
                int width = this.c_layerViewPanelGUI.Width;
                int height = this.c_layerViewPanelGUI.ItemHeight;
                foreach (Core2DDrawingLayer item in m_editableDocument.Layers)
                {
                    this.c_layerViewPanelGUI.Controls.Add(new LayerViewItem(item)
                    {
                        Bounds = new System.Drawing.Rectangle(x, y, width, height)
                    });
                    y += height;
                }
            }
            this.c_layerViewPanelGUI.ResumeLayout();
        }

        void m_editableDocument_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            this.c_layerViewPanelGUI.SuspendLayout();
            this.c_layerViewPanelGUI.Controls.Clear();

            if (m_editableDocument != null)
            {
                int x = 0;
                int y = 0;
                int width = this.c_layerViewPanelGUI.Width;
                int height = this.c_layerViewPanelGUI.ItemHeight;
                foreach (Core2DDrawingLayer item in m_editableDocument.Layers)
                {
                    this.c_layerViewPanelGUI.Controls.Add(new LayerViewItem(item)
                    {
                        Bounds = new System.Drawing.Rectangle(x, y, width, height)
                    });
                    y += height;
                }
            }
            this.c_layerViewPanelGUI.ResumeLayout();
        }

        void m_editableDocument_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            this.c_layerViewPanelGUI.Invalidate(true );
        }

      

        private void c_addNewLayer_Click(object sender, EventArgs e)
        {
            CallAction("2DDrawingLayer.AddNewLayer", true );    
        }

        private void c_duplicateLayer_Click(object sender, EventArgs e)
        {
            CallAction("2DDrawingLayer.Duplicate", true ); 
        }

        private void c_clearCurrentLayer_Click(object sender, EventArgs e)
        {
            CallAction("2DDrawingLayer.ClearLayer", true); 
        }

    }
}
