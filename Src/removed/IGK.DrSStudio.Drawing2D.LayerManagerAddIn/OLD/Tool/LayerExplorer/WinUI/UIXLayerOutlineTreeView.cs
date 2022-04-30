

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXLayerOutlineTreeView.cs
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
file:UIXLayerOutlineTreeView.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IGK.ICore;using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Codec;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Core.Layers;
    /// <summary>
    /// used to show treeview
    /// </summary>
    partial class UIXLayerOutlineTreeView :
        UIXToolConfigControlBase 
    {
        private void RegisterLayerEvent()
        {
            this.c_treeview.SetLayer(m_layer);
           }
        private void UnRegisterLayerEvent()
        {
            this.c_treeview.SetLayer(null);
        }  
        internal void Diseable(bool p)
        {
            if (p != true)
            {
                //enable itemp
                this.xToolStrip1.Enabled = true;
                this.c_treeview.Enabled = true;
            }
            else
            {
                this.c_treeview.Enabled = false;
                this.xToolStrip1.Enabled = false;
            }
        }
        private void bt_moveup_Click(object sender, EventArgs e)
        {
            //move selected element up
            MainForm.Workbench.CallAction(CoreConstant.MENU_LAYER_MOVEELEMENT_UP);
        }
        private void bt_movedown_Click(object sender, EventArgs e)
        {
            //move selected element back
            MainForm.Workbench.CallAction(CoreConstant.MENU_LAYER_MOVEELEMENT_DOWN );
        }
        private void bt_delete_Click(object sender, EventArgs e)
        {
            this.DeleteItem();
        }
        internal void DeleteItem()
        {
            //deslected selected element
            if (this.c_treeview.SelectedNode == null) return;
            TreeNode v_node = this.c_treeview.SelectedNode;
            Object v_obj = v_node.Tag;
            ICore2DDrawingSurface v_s = (this.CurrentSurface as ICore2DDrawingSurface);
            if (v_obj is ICore2DDrawingLayeredElement)
            {
                ICore2DDrawingLayeredElement v_elt = v_obj as ICore2DDrawingLayeredElement;
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
                        this.c_treeview.RemoveItem(v_elt);
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
                        v_node.Parent.Nodes.Remove(this.c_treeview.SelectedNode);
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
            MainForm.Workbench.CallAction(CoreConstant.MENU_LAYER_CLEAR);
        }
        private ICore2DDrawingLayer m_layer;
        private XTreeview_LayerDocumentOutLine c_treeview;
       // private ToolStripButton bt_find;
        private ToolStripButton bt_moveup;
        private ToolStripButton bt_movedown;
        private ToolStripButton bt_delete;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton bt_clearlayer;
        private XToolStrip xToolStrip1;
        private System.ComponentModel.IContainer components;
        //private bool m_configure; //configure flags
        internal UIXLayerOutlineTreeView(IGK.DrSStudio.Drawing2D.Tools.CoreTool_LayerOutline tool)
            : base(tool)
        {
            InitializeComponent();
            this.c_treeview.ShowNodeToolTips = true;
            this.c_treeview.FullRowSelect = true;
            this.c_treeview.AfterSelect += new TreeViewEventHandler(c_treeview_AfterSelect);
            this.c_treeview.KeyUp += new KeyEventHandler(c_treeview_KeyUp);
        }
        void c_treeview_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F2:
                    //begin edition of the label
                    if (this.c_treeview.SelectedNode != null)
                    {
                        this.c_treeview.LabelEdit = true;
                        this.c_treeview.SelectedNode.BeginEdit();
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
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.InitControl();
        }
        private void InitControl()
        {
            this.bt_movedown.Enabled = false;
            this.bt_moveup.Enabled = false;
            this.bt_delete.Enabled = false;
            bt_clearlayer.Image = CoreResources.GetDocumentImage(CoreConstant .MENU_CLEAR, 16, 16);
            bt_moveup.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_BRINGUP , 16, 16);
            bt_movedown.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_BRINGDOWN , 16, 16);
            bt_delete.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_CLOSE , 16, 16);
            LoadDisplayText();
            this.xToolStrip1.AddRemoveButton(null);
        }
        private void ConfigureElement()
        {
            bool v = false;
            if ((this.m_layer.SelectedElements.Count == 1)||
            ((this.c_treeview .SelectedNode!=null)&&
            (this.c_treeview.SelectedNode.Tag != null)))
            {
                v = true;
            }
            this.bt_movedown.Enabled = v;
            this.bt_moveup.Enabled = v;
            this.bt_delete.Enabled = v;
        }
        #region initialize
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIXLayerOutlineTreeView));
            this.c_treeview = new IGK.DrSStudio.WinUI.XTreeview_LayerDocumentOutLine();
            this.xToolStrip1 = new IGK.DrSStudio.WinUI.XToolStrip();
            this.bt_moveup = new ToolStripButton();
            this.bt_movedown = new ToolStripButton();
            this.bt_delete = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.bt_clearlayer = new ToolStripButton();
            this.xToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xTreeview_LayerDocumentOutLine1
            // 
            this.c_treeview.BorderStyle = System.Windows.Forms.BorderStyle.None;            
            this.c_treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_treeview.LabelEdit = true;
            this.c_treeview.Location = new System.Drawing.Point(0, 25);
            this.c_treeview.Name = "xTreeview_LayerDocumentOutLine1";         
            this.c_treeview.Size = new System.Drawing.Size(368, 309);
            this.c_treeview.TabIndex = 0;
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] 
            {       
            this.bt_moveup,
            this.bt_movedown,
            this.bt_delete,
            this.toolStripSeparator2,
            this.bt_clearlayer});
            this.xToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(368, 25);
            this.xToolStrip1.Stretch = true;
            this.xToolStrip1.TabIndex = 1;
            // 
            // tx_labelfind
            // 
            // 
            // bt_find
            // 
            //this.bt_find.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //this.bt_find.Image = ((System.Drawing.Image)(resources.GetObject("bt_find.Image")));
            //this.bt_find.ImageTransparentColor = System.Drawing.Color.Magenta;
            //this.bt_find.Name = "bt_find";
            //this.bt_find.Size = new System.Drawing.Size(23, 22);
            //this.bt_find.Text = "toolStripButton1";
            //this.bt_find.Click += new System.EventHandler(this.bt_find_Click);
            // 
            // toolStripSeparator1
            //             
            // 
            // bt_moveup
            // 
            this.bt_moveup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_moveup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_moveup.Name = "bt_moveup";
            this.bt_moveup.Size = new System.Drawing.Size(23, 22);
            this.bt_moveup.Text = "toolStripButton2";
            this.bt_moveup.Click += new System.EventHandler(this.bt_moveup_Click);
            // 
            // bt_movedown
            // 
            this.bt_movedown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
           this.bt_movedown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_movedown.Name = "bt_movedown";
            this.bt_movedown.Size = new System.Drawing.Size(23, 22);
            this.bt_movedown.Text = "toolStripButton3";
            this.bt_movedown.Click += new System.EventHandler(this.bt_movedown_Click);
            // 
            // bt_delete
            // 
            this.bt_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.bt_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_delete.Name = "bt_delete";
            this.bt_delete.Size = new System.Drawing.Size(23, 22);
            this.bt_delete.Text = "toolStripButton4";
            this.bt_delete.Click += new System.EventHandler(this.bt_delete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bt_clearlayer
            // 
            this.bt_clearlayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.bt_clearlayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_clearlayer.Name = "bt_clearlayer";
            this.bt_clearlayer.Size = new System.Drawing.Size(23, 22);
            this.bt_clearlayer.Text = "toolStripButton1";
            this.bt_clearlayer.Click += new System.EventHandler(this.bt_clearlayer_Click);
            this.c_treeview.HideSelection = false;
            // 
            // imageList1
            // 
            // 
            // UIXLayerDocumentOutlineControl
            // 
            this.Controls.Add(this.c_treeview);
            this.Controls.Add(this.xToolStrip1);
            this.Name = "UIXLayerDocumentOutlineControl";
            this.Size = new System.Drawing.Size(368, 334);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            bt_clearlayer.ToolTipText = CoreSystem.GetString("Tip.Layer.Clear");
            bt_delete.ToolTipText = CoreSystem.GetString("Tip.Layer.RemoveElement");
            bt_movedown.ToolTipText = CoreSystem.GetString("Tip.LayerElement.MoveBack");
            bt_moveup.ToolTipText = CoreSystem.GetString("Tip.LayerElement.MoveFront");
        }
#endregion
        internal void Configure(ICore2DDrawingLayer layer)
        {
            if (this.c_treeview.IsDisposed) return;
            this.c_treeview.SuspendLayout();
            this.c_treeview.Nodes.Clear();
            if (m_layer != null)
                UnRegisterLayerEvent();
            m_layer = layer;
            if (m_layer != null)
                RegisterLayerEvent();
            this.c_treeview.ResumeLayout();
        }
        internal void ToggleItem(CoreWorkingObjectZIndexChangedArgs e)
        {
            this.c_treeview.ToggleItem(e);
        }
    }
}

