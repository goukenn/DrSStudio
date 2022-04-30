

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: LayerExplorerViewControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/


using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.Resources;
using System;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    /// <summary>
    /// represent a layer explorer treeview control
    /// </summary>
    public class LayerExplorerViewControl : IGKXToolConfigControlBase 
    {
        private System.ComponentModel.IContainer components;
        private IGKXToolStrip c_toolStrip_Options;
        private LayerExplorerTreeView c_trv_layerExplorer;
        private IGKXToolStripButton c_btn_clearLayer;
        private IGKXToolStripButton c_btn_moveUp;
        private IGKXToolStripButton c_btn_moveDown;
        private IGKXToolStripButton c_btn_delete;

        

        private ICore2DDrawingLayer m_Layer;
        /// <summary>
        /// get or set the layer
        /// </summary>
        public ICore2DDrawingLayer Layer
        {
            get { return m_Layer; }
            set
            {
                if (m_Layer != value)
                {
                    m_Layer = value;
                }
            }
        }


        public LayerExplorerViewControl(LayerExplorerTool tool):base(tool)
        {
            this.InitializeComponent();
            this.c_trv_layerExplorer.ShowNodeToolTips = true;
            this.c_trv_layerExplorer.FullRowSelect = true;
            this.c_trv_layerExplorer.AfterSelect += new TreeViewEventHandler(c_treeview_AfterSelect);
            this.c_trv_layerExplorer.KeyUp += new KeyEventHandler(c_treeview_KeyUp);
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            InitControl();
        }

        private void InitControl()
        {
            this.c_btn_moveDown.Enabled = false;
            this.c_btn_moveUp.Enabled = false;
            this.c_btn_delete.Enabled = false;

            c_btn_clearLayer.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLEAR_GKDS);
            c_btn_moveUp.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_BRINGUP_GKDS);
            c_btn_moveDown.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_BRINGDOWN_GKDS);
            c_btn_delete.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLOSE_GKDS);
            LoadDisplayText();
            this.c_toolStrip_Options.AddRemoveButton(null);
        }
        void c_treeview_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    //begin edition of the label
                    if (this.c_trv_layerExplorer.SelectedNode != null)
                    {
                        this.c_trv_layerExplorer.LabelEdit = true;
                        this.c_trv_layerExplorer.SelectedNode.BeginEdit();
                    }
                    break;
                case Keys.Delete:
                    this.DeleteItem();
                    break;
            }
        }

        void c_treeview_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.ConfigureElement();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }
        private void ConfigureElement()
        {
            bool v = false;
            if ((this.m_Layer.SelectedElements.Count == 1) ||
            ((this.c_trv_layerExplorer.SelectedNode != null) &&
            (this.c_trv_layerExplorer.SelectedNode.Tag != null)))
            {
                v = true;
            }
            this.c_btn_moveDown.Enabled = v;
            this.c_btn_moveUp.Enabled = v;
            this.c_btn_delete.Enabled = v;

        }
        private void RegisterLayerEvent()
        {
            this.c_trv_layerExplorer.SetLayer(m_Layer);
            this.c_toolStrip_Options.Enable (true );
        }
        private void UnRegisterLayerEvent()
        {
            this.c_trv_layerExplorer.SetLayer(null);
        }
        internal void Diseable(bool p)
        {
            if (p != true)
            {
                //enable itemp
                this.c_toolStrip_Options.Enabled = true;
                this.c_trv_layerExplorer.Enabled = true;
            }
            else
            {
                this.c_trv_layerExplorer.Enabled = false;
                this.c_toolStrip_Options.Enabled = false;
            }
        }

        private void bt_moveup_Click(object sender, EventArgs e)
        {
            //move selected element up
            CallAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_UP);
        }

        private void bt_movedown_Click(object sender, EventArgs e)
        {
            //move selected element back
            CallAction(IGKElementTransformConstant.MENU_LAYER_MOVE_ELEMENT_DOWN);
        }

      

        private void bt_delete_Click(object sender, EventArgs e)
        {
            this.DeleteItem();

        }

        internal void DeleteItem()
        {
            //deslected selected element
            if (this.c_trv_layerExplorer.SelectedNode == null) return;
            TreeNode v_node = this.c_trv_layerExplorer.SelectedNode;
            Object v_obj = v_node.Tag;
            ICore2DDrawingSurface v_s = (this.CurrentSurface as ICore2DDrawingSurface);

            if (v_obj is ICore2DDrawingLayeredElement)
            {
                ICore2DDrawingLayeredElement v_elt = v_obj as ICore2DDrawingLayeredElement;
                
                if (v_elt.Container != null)
                {
                    var m = v_elt.Container as ICoreWorkingModifiableElementContainer;
                    if (m != null)
                    {
                        m.Remove(v_elt);
                        this.c_trv_layerExplorer.RemoveItem(v_elt);
                    }
                }
                else
                {
                    if (v_elt.Container == v_s.CurrentLayer)
                    {
                        v_s.CurrentLayer.Elements.Remove(v_obj as ICore2DDrawingLayeredElement);
                    }
                    else
                    {
                        ICore2DDrawingLayer v_Player = v_elt.ParentLayer;
                        if (v_Player != null)
                        {
                            v_Player.Elements.Remove(v_elt);
                            this.c_trv_layerExplorer.RemoveItem(v_elt);
                        }
                    }
                }
            }
            else
            {
                if (v_obj is ICore2DDrawingLayer)
                {//remove layer 
                    if (v_node.Parent != null)
                    {
                        ICore2DDrawingLayer l = v_obj as Core2DDrawingLayer;
                        l.Parent.Layers.Remove(l);
                        //
                        v_node.Parent.Nodes.Remove(this.c_trv_layerExplorer.SelectedNode);
                    }
                }
            }
            //invalidate the surface
            v_s.Invalidate();
        }

        private void bt_clearlayer_Click(object sender, EventArgs e)
        {
            //call the action Clear layer
            //---------------------------
            this.m_Layer.Clear();
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c_trv_layerExplorer = new IGK.DrSStudio.Drawing2D.WinUI.LayerExplorerTreeView();
            this.c_toolStrip_Options = new IGKXToolStrip();
            this.c_btn_moveDown = new IGKXToolStripButton();
            this.c_btn_moveUp = new IGKXToolStripButton();
            this.c_btn_delete = new IGKXToolStripButton();
            this.c_btn_clearLayer = new IGKXToolStripButton();
            this.c_toolStrip_Options.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_trv_layerExplorer
            // 
            this.c_trv_layerExplorer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_trv_layerExplorer.ImageIndex = 0;
            this.c_trv_layerExplorer.Location = new System.Drawing.Point(0, 25);
            this.c_trv_layerExplorer.Name = "c_trv_layerExplorer";
            this.c_trv_layerExplorer.SelectedImageIndex = 0;
            this.c_trv_layerExplorer.Size = new System.Drawing.Size(332, 239);
            this.c_trv_layerExplorer.TabIndex = 0;
            // 
            // c_toolStrip_Options
            // 
            this.c_toolStrip_Options.CaptionKey = null;
            this.c_toolStrip_Options.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.c_toolStrip_Options.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_btn_moveDown,
            this.c_btn_moveUp,
            this.c_btn_delete,
            this.c_btn_clearLayer});
            this.c_toolStrip_Options.Location = new System.Drawing.Point(0, 0);
            this.c_toolStrip_Options.Name = "c_toolStrip_Options";
            this.c_toolStrip_Options.Size = new System.Drawing.Size(332, 25);
            this.c_toolStrip_Options.TabIndex = 1;
            this.c_toolStrip_Options.Text = "xToolStrip1";
            // 
            // c_btn_moveDown
            // 

            this.c_btn_moveDown.Name = "c_btn_moveDown";
            this.c_btn_moveDown.Size = new System.Drawing.Size(23, 22);
            this.c_btn_moveDown.Click += new System.EventHandler(this.bt_movedown_Click);
            // 
            // c_btn_moveUp
            // 

            this.c_btn_moveUp.Name = "c_btn_moveUp";
            this.c_btn_moveUp.Size = new System.Drawing.Size(23, 22);
            this.c_btn_moveUp.Click += new System.EventHandler(this.bt_moveup_Click);
            // 
            // c_btn_delete
            // 

            this.c_btn_delete.Name = "c_btn_delete";
            this.c_btn_delete.Size = new System.Drawing.Size(23, 22);
            this.c_btn_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // c_btn_clearLayer
            // 

            this.c_btn_clearLayer.Name = "c_btn_clearLayer";
            this.c_btn_clearLayer.Size = new System.Drawing.Size(23, 22);
            this.c_btn_clearLayer.Click += new System.EventHandler(this.bt_clearlayer_Click);
            // 
            // LayerExplorerViewControl
            // 
            this.Controls.Add(this.c_trv_layerExplorer);
            this.Controls.Add(this.c_toolStrip_Options);
            this.Name = "LayerExplorerViewControl";
            this.Size = new System.Drawing.Size(332, 264);
            this.c_toolStrip_Options.ResumeLayout(false);
            this.c_toolStrip_Options.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        internal void SetLayer(ICore2DDrawingLayer layer)
        {
            if (this.c_trv_layerExplorer.IsDisposed) 
                return;
            if (this.m_Layer != layer)
            {
                this.c_toolStrip_Options.Enable(false);
                this.c_trv_layerExplorer.SuspendLayout();
                this.c_trv_layerExplorer.Nodes.Clear();

                if (c_trv_layerExplorer != null)
                    UnRegisterLayerEvent();
                m_Layer = layer;
                if (m_Layer != null)
                    RegisterLayerEvent();

                this.c_trv_layerExplorer.ResumeLayout();
            }
        }
        
    }
}
