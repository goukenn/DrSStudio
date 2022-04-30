

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXDocumentViewControl.cs
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
file:UIXDocumentViewControl.cs
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
namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Actions;
    using IGK.DrSStudio.Tools ;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.DrSStudio.Resources;
    sealed class UIXDocumentViewControl :
        UIXToolConfigControlBase        
    {
        private XToolStrip xToolStrip1;
        private XToolStripButton btn_cleardocument;
        private XToolStripButton btn_makebackgroundtransparent;
        private XToolStripButton btn_removedocument;
        private XDocumentViewPanel m_documentPanel;        
        private XToolStripButton btn_addnewdocument;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private XToolStripButton btn_moveup;
        private XToolStripButton btn_moveDown;
        private ToolStripButton btn_duplicate;
        private XPanel c_bottomView;
        private IGKXLabel xLabel1;
        private bool m_configuring;
        public new ICore2DDrawingSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }
        public UIXDocumentViewControl()
            : base(CoreTool_DocumentView.Instance )
        {
            this.InitializeComponent();            
            this.m_documentPanel.SelectedDocumentChanged += new EventHandler(m_documentPanel_SelectedDocumentChanged);
            this.Tool.VisibleChanged += new EventHandler(Tool_VisibleChanged);
            this.xToolStrip1.AddRemoveButton(null);
        }
        void Tool_VisibleChanged(object sender, EventArgs e)
        {
            SetUpSurface();
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.btn_addnewdocument.Image = CoreResources.GetDocumentImage("Menu_Add", 16, 16);
            this.btn_removedocument.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_CLOSE, 16, 16);
            this.btn_makebackgroundtransparent.Image = CoreResources.GetDocumentImage("dash", 16, 16);
            this.btn_cleardocument.Image = CoreResources.GetDocumentImage("Menu_Clear", 16, 16);
            this.btn_moveup.Image = CoreResources.GetDocumentImage(CoreConstant.MENU_BRINGUP, 16, 16);
            this.btn_moveDown .Image = CoreResources.GetDocumentImage(CoreConstant.MENU_BRINGDOWN , 16, 16);
            this.btn_duplicate.Image = CoreResources.GetDocumentImage("Menu_DuplicateDocument", 16, 16);
            this.LoadDisplayText();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.btn_addnewdocument.ToolTipText = CoreSystem.GetString("ToolTip.AddNewDocument");
            this.btn_removedocument.ToolTipText = CoreSystem.GetString("ToolTip.RemoveDocument");
            this.btn_makebackgroundtransparent.ToolTipText = CoreSystem.GetString("ToolTip.MakeBackgroundTransparent");
            this.btn_cleardocument.ToolTipText = CoreSystem.GetString("ToolTip.ClearDocument");
        }
        void m_documentPanel_SelectedDocumentChanged(object sender, EventArgs e)
        {
            if (this.CurrentSurface != null)
            {
                this.m_configuring = true;
                this.CurrentSurface.CurrentDocument = this.m_documentPanel.SelectedDocument;
                this.m_configuring = false;
                this.CheckButtonState();
            }
        }
        internal void SetUpSurface()
        {
            if (!this.Tool.Visible || (this.CurrentSurface == null))
            {
                this.m_documentPanel.Clear();
                this.c_bottomView.CaptionKey = string.Empty;
                this.c_bottomView.Text = "";
                this.xToolStrip1.Enable(false);
            }
            else
            {
                this.xToolStrip1.Enable(true);
                this.m_documentPanel.Clear();
                this.m_documentPanel.Populate(this.CurrentSurface);
                this.CheckButtonState();
            }
        }
        private void CheckButtonState()
        {
            this.xLabel1.CaptionKey = string.Empty;
            this.xLabel1.Text = "Documents : " + (this.CurrentSurface.Documents.IndexOf(this.CurrentSurface.CurrentDocument) +1) + "/" + this.CurrentSurface.Documents.Count;
            this.btn_makebackgroundtransparent.Checked = this.CurrentSurface.CurrentDocument.BackgroundTransparent;
            this.btn_addnewdocument.Enabled = this.CurrentSurface.AllowMultipleDocument;
            this.btn_removedocument.Enabled = this.CurrentSurface.AllowMultipleDocument;
        }
        private void InitializeComponent()
        {
            this.xToolStrip1 = new IGK.DrSStudio.WinUI.XToolStrip();
            this.btn_addnewdocument = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.btn_duplicate = new System.Windows.Forms.ToolStripButton();
            this.btn_removedocument = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_makebackgroundtransparent = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.btn_cleardocument = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_moveup = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.btn_moveDown = new IGK.DrSStudio.WinUI.XToolStripButton();
            this.m_documentPanel = new IGK.DrSStudio.Drawing2D.WinUI.XDocumentViewPanel();
            this.c_bottomView = new IGK.DrSStudio.WinUI.XPanel();
            this.xLabel1 = new IGK.DrSStudio.WinUI.IGKXLabel();
            this.xToolStrip1.SuspendLayout();
            this.c_bottomView.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_addnewdocument,
            this.btn_duplicate,
            this.btn_removedocument,
            this.toolStripSeparator1,
            this.btn_makebackgroundtransparent,
            this.btn_cleardocument,
            this.toolStripSeparator2,
            this.btn_moveup,
            this.btn_moveDown});
            this.xToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(298, 25);
            this.xToolStrip1.TabIndex = 0;
            // 
            // btn_addnewdocument
            // 
            this.btn_addnewdocument.Action = null;
            this.btn_addnewdocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_addnewdocument.EnumValue = null;
            this.btn_addnewdocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_addnewdocument.Name = "btn_addnewdocument";
            this.btn_addnewdocument.Size = new System.Drawing.Size(23, 22);
            this.btn_addnewdocument.Text = "toolStripButton1";
            this.btn_addnewdocument.Click += new System.EventHandler(this.btn_addnewdocument_Click);
            // 
            // btn_duplicate
            // 
            this.btn_duplicate.Name = "btn_duplicate";
            this.btn_duplicate.Size = new System.Drawing.Size(23, 22);
            // 
            // btn_removedocument
            // 
            this.btn_removedocument.Action = null;
            this.btn_removedocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_removedocument.EnumValue = null;
            this.btn_removedocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_removedocument.Name = "btn_removedocument";
            this.btn_removedocument.Size = new System.Drawing.Size(23, 22);
            this.btn_removedocument.Text = "toolStripButton3";
            this.btn_removedocument.Click += new System.EventHandler(this.btn_removedocument_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_makebackgroundtransparent
            // 
            this.btn_makebackgroundtransparent.Action = null;
            this.btn_makebackgroundtransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_makebackgroundtransparent.EnumValue = null;
            this.btn_makebackgroundtransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_makebackgroundtransparent.Name = "btn_makebackgroundtransparent";
            this.btn_makebackgroundtransparent.Size = new System.Drawing.Size(23, 22);
            this.btn_makebackgroundtransparent.Text = "toolStripButton2";
            this.btn_makebackgroundtransparent.Click += new System.EventHandler(this.btn_makebackgroundtransparent_Click);
            // 
            // btn_cleardocument
            // 
            this.btn_cleardocument.Action = null;
            this.btn_cleardocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_cleardocument.EnumValue = null;
            this.btn_cleardocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cleardocument.Name = "btn_cleardocument";
            this.btn_cleardocument.Size = new System.Drawing.Size(23, 22);
            this.btn_cleardocument.Text = "toolStripButton1";
            this.btn_cleardocument.Click += new System.EventHandler(this.btn_cleardocument_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_moveup
            // 
            this.btn_moveup.Action = null;
            this.btn_moveup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_moveup.EnumValue = null;
            this.btn_moveup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_moveup.Name = "btn_moveup";
            this.btn_moveup.Size = new System.Drawing.Size(23, 22);
            this.btn_moveup.Text = "toolStripButton1";
            this.btn_moveup.Click += new System.EventHandler(this.btn_moveup_Click);
            // 
            // btn_moveDown
            // 
            this.btn_moveDown.Action = null;
            this.btn_moveDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btn_moveDown.EnumValue = null;
            this.btn_moveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_moveDown.Name = "btn_moveDown";
            this.btn_moveDown.Size = new System.Drawing.Size(23, 22);
            this.btn_moveDown.Text = "toolStripButton2";
            this.btn_moveDown.Click += new System.EventHandler(this.btn_moveDown_Click);
            // 
            // m_documentPanel
            // 
            this.m_documentPanel.AutoScroll = true;
            this.m_documentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_documentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_documentPanel.Location = new System.Drawing.Point(0, 25);
            this.m_documentPanel.Margin = new System.Windows.Forms.Padding(0);
            this.m_documentPanel.Name = "m_documentPanel";
            this.m_documentPanel.SelectedDocument = null;
            this.m_documentPanel.Size = new System.Drawing.Size(298, 315);
            this.m_documentPanel.TabIndex = 1;
            // 
            // c_bottomView
            // 
            this.c_bottomView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_bottomView.Controls.Add(this.xLabel1);
            this.c_bottomView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_bottomView.Location = new System.Drawing.Point(0, 340);
            this.c_bottomView.Margin = new System.Windows.Forms.Padding(0);
            this.c_bottomView.Name = "c_bottomView";
            this.c_bottomView.Size = new System.Drawing.Size(298, 28);
            this.c_bottomView.TabIndex = 0;
            // 
            // xLabel1
            // 
            this.xLabel1.AutoSize = true;
            this.xLabel1.CaptionKey = "lb.documents.caption";
            this.xLabel1.Location = new System.Drawing.Point(14, 8);
            this.xLabel1.Name = "xLabel1";
            this.xLabel1.Size = new System.Drawing.Size(108, 13);
            this.xLabel1.TabIndex = 0;
            // 
            // UIXDocumentViewControl
            // 
            this.Controls.Add(this.m_documentPanel);
            this.Controls.Add(this.c_bottomView);
            this.Controls.Add(this.xToolStrip1);
            this.Name = "UIXDocumentViewControl";
            this.Size = new System.Drawing.Size(298, 368);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.c_bottomView.ResumeLayout(false);
            this.c_bottomView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        void btn_duplicate_Click(object sender, EventArgs e)
        {
           this.Workbench.CallAction ("Document.Duplicate");
        }
        //action
        internal void AddNewDocument(ICore2DDrawingDocument document)
        {
            if (document == null) return;
            this.m_documentPanel.AddDocument(document);
            this.CheckButtonState();
        }
        internal void RemoveDocument(ICore2DDrawingDocument document)
        {
            if (document == null) return;
            this.m_documentPanel.RemoveDocument(document);
            this.CheckButtonState();
        }
        internal void SelectDocument(ICore2DDrawingDocument document)
        {
            if (this.m_configuring) 
                return;
            this.m_documentPanel.SelectedDocument = document;
            this.CheckButtonState();
        }
        private void btn_makebackgroundtransparent_Click(object sender, EventArgs e)
        {
            if (this.CurrentSurface != null)
            {
                this.CurrentSurface.CurrentDocument.BackgroundTransparent =
                    !this.CurrentSurface.CurrentDocument.BackgroundTransparent;
                this.btn_makebackgroundtransparent .Checked = this.CurrentSurface.CurrentDocument.BackgroundTransparent;                
            }
        }
        private void btn_addnewdocument_Click(object sender, EventArgs e)
        {
            ICoreAction ak = CoreSystem.GetMenu("Document.AddNew");
            if (ak != null)
                ak.DoAction();
            else
                CoreMessageBox.Show("No action Menu.Document.AddDocument registrated");
        }
        private void btn_cleardocument_Click(object sender, EventArgs e)
        {
            ICoreAction ak = CoreSystem.GetMenu("Document.Clear");
            if (ak != null)
                ak.DoAction();
            else
                CoreMessageBox.Show("No action Menu.Document.ClearDocument registrated");
        }
        private void btn_removedocument_Click(object sender, EventArgs e)
        {
            ICoreAction ak = CoreSystem.GetMenu("Document.Delete");
            if (ak != null)
                ak.DoAction();
            else
                CoreMessageBox.Show("No action Document.RemoveDocument registrated");
        }
        internal void SetDocumentNewIndex(ICore2DDrawingDocument document)
        {
            this.RemoveDocument(document);
            this.m_documentPanel.InsertDocument(document, document.ZIndex);
        }
        private void btn_moveup_Click(object sender, EventArgs e)
        {
            CoreSystem.Instance.Workbench.CallAction ("Document.MoveUp");
        }
        private void btn_moveDown_Click(object sender, EventArgs e)
        {
            CoreSystem.Instance.Workbench.CallAction("Document.MoveDown");
        }
    }
}

