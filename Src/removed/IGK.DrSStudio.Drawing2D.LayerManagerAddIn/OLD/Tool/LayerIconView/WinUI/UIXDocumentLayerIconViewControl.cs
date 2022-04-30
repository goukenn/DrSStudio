

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXDocumentLayerIconViewControl.cs
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
file:UIXDocumentLayerIconViewControl.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// represent class that configure layer properties
    /// </summary>
    public class UIXDocumentLayerOutlineControl :
        UIXToolConfigControlBase  
    {
        public new ICore2DDrawingSurface CurrentSurface {
            get { return base.CurrentSurface as ICore2DDrawingSurface ; }
        }
        private bool m_configOpacity;
        private bool m_configure;
        private ICore2DDrawingDocument m_document;
        private XLayerPanel xLayerPanel1;
        private ToolStripButton bt_clearlayer;
        private ToolStripButton bt_removelayer;
        private ToolStripButton bt_newLayer;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton bt_view;
        private System.Windows.Forms.ToolStripComboBox cmb_layeroption;
        private ToolStripButton bt_moveup;
        private ToolStripButton bt_moveback;
        private System.Windows.Forms.ToolStripTextBox tx_opacity;
        private XToolStrip xToolStrip1;
        internal ICore2DDrawingDocument Document {
            get {
                return this.xLayerPanel1.Document;
            }
        }
        internal UIXDocumentLayerOutlineControl(Tools.CoreTool_LayerIconView tool)
            : base(tool )
        {
            InitializeComponent();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            InitControl();
        }
        private void InitControl()
        {          
            foreach (enuCoreLayerOperation lop in Enum.GetValues(typeof(enuCoreLayerOperation)))
            {
                this.cmb_layeroption.Items.Add(lop);
            }
            this.cmb_layeroption.SelectedItem = enuCoreLayerOperation.None;
            this.cmb_layeroption.ComboBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_layeroption.ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_layeroption.ComboBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(ComboBox_DrawItem);
            this.cmb_layeroption.SelectedIndexChanged += new EventHandler(cmb_layeroption_SelectedIndexChanged);
            //init this control image
            this.bt_clearlayer.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_CLEAR , 16, 16);
            this.bt_newLayer.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_ADD, 16, 16);
            this.bt_removelayer.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_CLOSE  , 16, 16);
            this.bt_view.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_VIEW , 16, 16);
            this.bt_moveback.Image = CoreResources.GetDocumentImage( CoreConstant.MENU_BRINGDOWN  , 16, 16);
            this.bt_moveup.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_BRINGUP , 16, 16);
            this.LoadDisplayText();
        }
        void cmb_layeroption_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.m_configure) return;
            ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
            l.LayerOption  = (enuCoreLayerOperation)this.cmb_layeroption.SelectedItem;
            this.CurrentSurface.Invalidate();
        }
        void ComboBox_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            //draw item
            e.DrawBackground();
            if (!this.cmb_layeroption.Enabled)
            {
                e.Graphics.FillRectangle(CoreBrushRegister.GetBrush(System.Drawing.SystemColors.InactiveCaptionText ),
                    e.Bounds);
            }
            if (e.Index >= 0)
            {
                object obj = this.cmb_layeroption.Items[e.Index];
                //draw text
                string v_txt = CoreSystem.GetString("enum." + obj.ToString());
                e.Graphics.DrawString(v_txt,
                    e.Font,
                    CoreBrushRegister.GetBrush(e.ForeColor),
                    e.Bounds
                    );
            }
            if (this.cmb_layeroption.SelectedIndex == e.Index)
            {
                e.DrawFocusRectangle();
            }
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIXDocumentLayerOutlineControl));
            this.xToolStrip1 = new IGK.DrSStudio.WinUI.XToolStrip();
            this.bt_newLayer = new ToolStripButton();
            this.toolStripSeparator1 = new ToolStripSeparator();
            this.bt_moveup = new ToolStripButton();
            this.bt_moveback = new ToolStripButton();
            this.bt_clearlayer = new ToolStripButton();
            this.bt_removelayer = new ToolStripButton();
            this.toolStripSeparator2 = new ToolStripSeparator();
            this.bt_view = new ToolStripButton();
            this.cmb_layeroption = new System.Windows.Forms.ToolStripComboBox();
            this.xLayerPanel1 = new XLayerPanel(this);
            this.tx_opacity = new System.Windows.Forms.ToolStripTextBox();
            this.xToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bt_newLayer,
            this.toolStripSeparator1,
            this.bt_moveup,
            this.bt_moveback,
            this.bt_clearlayer,
            this.bt_removelayer,
            this.toolStripSeparator2,
            this.bt_view,
            this.cmb_layeroption,
            this.tx_opacity});
            this.xToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(309, 25);
            this.xToolStrip1.Stretch = true;
            this.xToolStrip1.TabIndex = 0;
            // 
            // bt_newLayer
            // 
            this.bt_newLayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_newLayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_newLayer.Name = "bt_newLayer";
            this.bt_newLayer.Size = new System.Drawing.Size(23, 22);
            this.bt_newLayer.Click += new System.EventHandler(this.bt_newLayer_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bt_moveup
            // 
            this.bt_moveup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_moveup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_moveup.Name = "bt_moveup";
            this.bt_moveup.Size = new System.Drawing.Size(23, 22);
            this.bt_moveup.Text = "toolStripButton1";
            this.bt_moveup.Click += new System.EventHandler(this.bt_moveup_Click);
            // 
            // bt_moveback
            // 
            this.bt_moveback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_moveback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_moveback.Name = "bt_moveback";
            this.bt_moveback.Size = new System.Drawing.Size(23, 22);
            this.bt_moveback.Click += new System.EventHandler(this.bt_moveback_Click);
            // 
            // bt_clearlayer
            // 
            this.bt_clearlayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_clearlayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_clearlayer.Name = "bt_clearlayer";
            this.bt_clearlayer.Size = new System.Drawing.Size(23, 22);
            this.bt_clearlayer.Click += new System.EventHandler(this.bt_clearlayer_Click);
            // 
            // bt_removelayer
            // 
            this.bt_removelayer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_removelayer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_removelayer.Name = "bt_removelayer";
            this.bt_removelayer.Size = new System.Drawing.Size(23, 22);
            this.bt_removelayer.Click += new System.EventHandler(this.bt_removelayer_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bt_view
            // 
            this.bt_view.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bt_view.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.bt_view.Name = "bt_view";
            this.bt_view.Size = new System.Drawing.Size(23, 22);
            this.bt_view.Click += new System.EventHandler(this.bt_view_Click);
            // 
            // cmb_layeroption
            // 
            this.cmb_layeroption.Name = "cmb_layeroption";
            this.cmb_layeroption.Size = new System.Drawing.Size(80, 25);
            // 
            // xLayerPanel1
            // 
            this.xLayerPanel1.AutoScroll = true;
            this.xLayerPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.xLayerPanel1.CaptionKey = null;            
            this.xLayerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLayerPanel1.Location = new System.Drawing.Point(0, 25);
            this.xLayerPanel1.Name = "xLayerPanel1";
            this.xLayerPanel1.SelectedIndex = -1;
            this.xLayerPanel1.Size = new System.Drawing.Size(309, 328);
            this.xLayerPanel1.TabIndex = 1;
            // 
            // tx_opacity
            // 
            this.tx_opacity.MaxLength = 5;
            this.tx_opacity.Name = "tx_opacity";
            this.tx_opacity.Size = new System.Drawing.Size(50, 25);
            this.tx_opacity.Text = "100%";
            this.tx_opacity.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tx_opacity.TextChanged += new System.EventHandler(this.tx_opacity_TextChanged);
            // 
            // UIXDocumentLayerOutlineControl
            //             
            this.Controls.Add(this.xLayerPanel1);
            this.Controls.Add(this.xToolStrip1);            
            this.Size = new System.Drawing.Size(309, 353);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.bt_clearlayer.ToolTipText = CoreSystem.GetString("ToolTip.clearlayer");
            this.bt_newLayer.ToolTipText = CoreSystem.GetString("ToolTip.newlayer");
            this.bt_removelayer.ToolTipText = CoreSystem.GetString("ToolTip.removelayer");
            this.bt_view.ToolTipText = CoreSystem.GetString("ToolTip.viewlayer");
            this.bt_moveup.ToolTipText = CoreSystem.GetString("ToolTip.moveup");
            this.bt_moveback.ToolTipText = CoreSystem.GetString("ToolTip.moveback");
        }
        internal void Diseable(bool diseable)
        {
            if (diseable)
            {
                this.Enabled = false;
            }
            else {
                this.Enabled = true;
            }
        }
        internal void Configure(ICore2DDrawingDocument document)
        {
            if (this.m_configure) return;
            this.m_configure = true;
            this.m_configOpacity = true;
            UnRegisterDocumentEvent();
            this.m_document = document;
            RegisterDocumentEvent();
            this.xLayerPanel1.Configure(document);
            if (document != null)
            {
                //this.cmb_layeroption.SelectedItem = document.CurrentLayer.LayerOperation;
                this.tx_opacity.Text = (document.CurrentLayer.Opacity * 100) + "%";
                this.bt_view.Checked = document.CurrentLayer.View;
                ConfigureGraphicMode(document);
            }
            this.CheckLayerProperty();
            this.m_configOpacity = false;
            this.m_configure = false;
        }
        private void RegisterDocumentEvent()
        {
            if (m_document == null) return;
            this.m_document.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler (m_document_CurrentLayerChanged);            
            this.m_document.LayerAdded += new Core2DDrawingLayerEventHandler(m_document_LayerAdded);
            this.m_document.LayerRemoved += new Core2DDrawingLayerEventHandler(m_document_LayerRemoved);
            this.m_document.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(m_document_PropertyChanged);
        }
        private void UnRegisterDocumentEvent()
        {
            if (m_document == null) return;
            this.m_document.CurrentLayerChanged -= new Core2DDrawingLayerChangedEventHandler(m_document_CurrentLayerChanged);
            this.m_document.LayerAdded -= new Core2DDrawingLayerEventHandler(m_document_LayerAdded);
            this.m_document.LayerRemoved -= new Core2DDrawingLayerEventHandler(m_document_LayerRemoved);
            this.m_document.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(m_document_PropertyChanged);
        }
        void m_document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            ConfigureGraphicMode(this.m_document);
        }
        void m_document_LayerRemoved(object o, Core2DDrawingLayerEventArgs e)
        {
            CheckLayerProperty();
        }
        void m_document_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            CheckLayerProperty();
        }
        private void CheckLayerProperty()
        {//used to configure available button 
            bool v =( this.Document!=null)&&(this.Document.Layers.Count > 1);
            this.bt_moveback.Enabled = v;
            this.bt_moveup.Enabled = v;
            this.bt_removelayer.Enabled = v;
        }
        void m_document_GraphicsModeChanged(object sender, EventArgs e)
        {
            ICore2DDrawingDocument document = sender as ICore2DDrawingDocument;
            ConfigureGraphicMode(document);
        }
        private void ConfigureGraphicMode(ICore2DDrawingDocument document)
        {//used to activate or not layer configuration
            bool v = (document.RenderingMode == enuRenderingMode.Pixel);
            this.cmb_layeroption.Enabled = v;
            this.tx_opacity.Enabled = v;
        }
        void m_document_CurrentLayerChanged(object sender, Core2DDrawingLayerChangedEventArgs e)
        {
            ConfigureLayer(e.NewLayer as Core2DDrawingLayer );    
        }
        private void ConfigureLayer(Core2DDrawingLayer layer)
        {
            if ((this.m_configure) || (layer == null))
                return;
            this.m_configure = true;
            this.m_configOpacity = true;
            this.cmb_layeroption.SelectedItem = null;// layer.LayerOperation;
            this.tx_opacity.Text = (layer.Opacity * 100) + "%";
            this.bt_view.Checked = layer.View;
            this.m_configOpacity = false;
            this.m_configure = false;
        }
        private void bt_newLayer_Click(object sender, EventArgs e)
        {
            //create new layer
            MainForm.Workbench.CallAction("Layer.AddNew");
        }
        private void bt_clearlayer_Click(object sender, EventArgs e)
        {
            MainForm.Workbench.CallAction("Layer.Clear");
        }
        private void bt_removelayer_Click(object sender, EventArgs e)
        {
            MainForm.Workbench.CallAction("Layer.Delete");
        }
        private void bt_view_Click(object sender, EventArgs e)
        {
            ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
            l.View = !l.View ;
            this.bt_view.Checked = l.View;
            this.CurrentSurface .Invalidate ();
        }
        private void bt_moveup_Click(object sender, EventArgs e)
        {
            MainForm.Workbench.CallAction("Layer.ToggleFront");
        }
        private void bt_moveback_Click(object sender, EventArgs e)
        {
            MainForm.Workbench.CallAction("Layer.ToggleBack");
        }
        private void tx_opacity_TextChanged(object sender, EventArgs e)
        {
            if (m_configOpacity) return;
            m_configOpacity = true;
            float opacity = 100.0f;
            string v = tx_opacity.Text.Replace("%", "");
            if (float.TryParse(v, out opacity))
            {
                if ((opacity >= 0) && (opacity <= 100.0f))
                {
                    int i = this.tx_opacity.SelectionStart ;
                    this.CurrentSurface.CurrentLayer.Opacity = opacity / 100.0F;
                    this.CurrentSurface.Invalidate();
                    this.tx_opacity.Text = (this.CurrentSurface.CurrentLayer.Opacity  * 100) + "%";
                    this.tx_opacity.SelectionStart = i;
                }
            }
            m_configOpacity = false ;
        }
    }
}

