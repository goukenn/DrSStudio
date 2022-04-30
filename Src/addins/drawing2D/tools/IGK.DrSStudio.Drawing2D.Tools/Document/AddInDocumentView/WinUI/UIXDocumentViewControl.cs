

/*
IGKDEV @ 2008-2016
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
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Actions;
    using IGK.ICore.Tools;
    using IGK.ICore.Drawing2D.Tools;
    using System.Drawing;
    using IGK.ICore.Resources;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.ICore.WinCore.WinUI.Controls;

    sealed class UIXDocumentViewControl : IGKXToolConfigControlBase
    {
        private IGKXToolStrip c_toolstrip;
        private IGKXToolStripButton btn_cleardocument;
        private IGKXToolStripButton btn_makebackgroundtransparent;
        private IGKXToolStripButton btn_removedocument;
        private XDocumentViewPanel m_documentPanel;
        private IGKXToolStripButton btn_addnewdocument;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private IGKXToolStripButton btn_moveup;
        private IGKXToolStripButton btn_moveback;
        private IGKXToolStripButton btn_movebegin;
        private IGKXToolStripButton btn_moveend;
        private IGKXToolStripButton btn_duplicate;
        private IGKXPanel c_bottomView;
        private IGKXLabel c_label;
        private bool m_configuring;

        public new ICore2DDrawingSurface CurrentSurface
        {
            get
            {
                return base.CurrentSurface as ICore2DDrawingSurface;
            }
        }



        public UIXDocumentViewControl()
            : base(CoreTool_DocumentView.Instance)
        {

            this.InitializeComponent();
            this.m_documentPanel.SelectedDocumentChanged += new EventHandler(m_documentPanel_SelectedDocumentChanged);
            this.Tool.VisibleChanged += new EventHandler(Tool_VisibleChanged);
            this.c_toolstrip.AddRemoveButton(null);
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

            this.btn_addnewdocument.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_ADD_GKDS);
            this.btn_removedocument.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLOSE_GKDS);
            this.btn_makebackgroundtransparent.ImageDocument = CoreResources.GetDocument(CoreImageKeys.DASH_GKDS);
            this.btn_cleardocument.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLEAR_GKDS);
            this.btn_moveup.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_BRINGUP_GKDS);
            this.btn_moveback.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_BRINGDOWN_GKDS);
            this.btn_duplicate.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_DUPLICATEDOCUMENT_GKDS);



            this.btn_moveup.Action = CoreSystem.GetAction("2DDrawingDocument.MoveUp");
            this.btn_moveback.Action = CoreSystem.GetAction("2DDrawingDocument.MoveBack");
            this.btn_movebegin.Action = CoreSystem.GetAction("2DDrawingDocument.MoveBegin");
            this.btn_moveend.Action = CoreSystem.GetAction("2DDrawingDocument.MoveEnd");

            this.btn_movebegin.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_BEGIN_GKDS);
            this.btn_moveend.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_MOVE_END_GKDS);

            this.LoadDisplayText();
        }


        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            this.btn_addnewdocument.ToolTipText = "tip.d2d.AddNewDocument".R();
            this.btn_removedocument.ToolTipText = "tip.d2d.RemoveDocument".R();
            this.btn_makebackgroundtransparent.ToolTipText = "tip.d2d.MakeBackgroundTransparent".R();
            this.btn_cleardocument.ToolTipText = "tip.d2d.ClearDocument".R();
            this.btn_duplicate.ToolTipText = "tip.d2d.duplicatedocument".R();
            this.btn_moveback.ToolTipText = "tip.d2d.movedoc_back".R();
            this.btn_moveend.ToolTipText = "tip.d2d.movedoc_end".R();
            this.btn_moveup.ToolTipText = "tip.d2d.movedoc_up".R();
            this.btn_movebegin.ToolTipText = "tip.d2d.movedoc_begin".R();

        }
        void m_documentPanel_SelectedDocumentChanged(object sender, EventArgs e)
        {
            if (this.m_configuring)
                return;

            if ((this.CurrentSurface != null)&&(this.CurrentSurface.CurrentDocument != this.m_documentPanel.SelectedDocument))
            {
                this.m_configuring = true;
                this.CurrentSurface.CurrentDocument = this.m_documentPanel.SelectedDocument;
                this.m_configuring = false;
                this.CheckButtonState();
            }
        }
        /// <summary>
        /// setup the surfaces
        /// </summary>
        internal void SetUpSurface()
        {
            if (!this.Tool.Visible || (this.CurrentSurface == null))
            {
                this.m_documentPanel.Clear();
                this.c_bottomView.CaptionKey = string.Empty;
                this.c_bottomView.Text = "";
                this.c_label.CaptionKey = string.Empty;
                this.c_label.Text = string.Empty;// "Documents : " + (this.CurrentSurface.Documents.IndexOf(this.CurrentSurface.CurrentDocument) + 1) + "/" + this.CurrentSurface.Documents.Count;
                this.c_toolstrip.Enable(false);
            }
            else
            {
                this.c_toolstrip.Enable(true);
                this.m_documentPanel.Clear();
                this.m_documentPanel.Populate(this.CurrentSurface);
                this.CheckButtonState();
            }
        }
        private void CheckButtonState()
        {
            this.c_label.CaptionKey = string.Empty;
            this.c_label.Text = "Documents : " + (this.CurrentSurface.Documents.IndexOf(this.CurrentSurface.CurrentDocument) + 1) + "/" + this.CurrentSurface.Documents.Count;

            this.btn_makebackgroundtransparent.Checked = this.CurrentSurface.CurrentDocument.BackgroundTransparent;
            this.btn_addnewdocument.Enabled = this.CurrentSurface.CanAddDocument;
            this.btn_removedocument.Enabled = this.CurrentSurface.CanAddDocument;

        }
        private void InitializeComponent()
        {
            this.c_toolstrip = new IGKXToolStrip();
            this.btn_addnewdocument = new IGKXToolStripButton();
            this.btn_duplicate = new IGKXToolStripButton();
            this.btn_removedocument = new IGKXToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_makebackgroundtransparent = new IGKXToolStripButton();
            this.btn_cleardocument = new IGKXToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btn_moveup = new IGKXToolStripButton();
            this.btn_moveback = new IGKXToolStripButton();
            this.btn_movebegin= new IGKXToolStripButton();
            this.btn_moveend = new IGKXToolStripButton();

            this.m_documentPanel = new IGK.DrSStudio.Drawing2D.WinUI.XDocumentViewPanel();
            this.c_bottomView = new IGKXPanel();
            this.c_label = new IGKXLabel();
            this.c_toolstrip.SuspendLayout();
            this.c_bottomView.SuspendLayout();
            this.SuspendLayout();
            // 
            // xToolStrip1
            // 
            this.c_toolstrip.BackgroundDocument = null;
            this.c_toolstrip.CaptionKey = null;
            this.c_toolstrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.c_toolstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btn_addnewdocument,
            this.btn_duplicate,
            this.btn_removedocument,
            this.toolStripSeparator1,
            this.btn_makebackgroundtransparent,
            this.btn_cleardocument,
            this.toolStripSeparator2,
            this.btn_moveup,
            this.btn_moveback,
            this.btn_movebegin,
            this.btn_moveend
            });
            this.c_toolstrip.Location = new System.Drawing.Point(0, 0);
            this.c_toolstrip.Name = "xToolStrip1";
            this.c_toolstrip.Size = new System.Drawing.Size(298, 25);
            this.c_toolstrip.TabIndex = 0;
            // 
            // btn_addnewdocument
            // 
            this.btn_addnewdocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;

            this.btn_addnewdocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_addnewdocument.Name = "btn_addnewdocument";
            this.btn_addnewdocument.Size = new System.Drawing.Size(23, 22);
            this.btn_addnewdocument.Click += new System.EventHandler(this.btn_addnewdocument_Click);
            // 
            // btn_duplicate
            // 
            this.btn_duplicate.Name = "btn_duplicate";
            this.btn_duplicate.Size = new System.Drawing.Size(23, 22);
            this.btn_duplicate.Click += new System.EventHandler(this.btn_duplicate_Click);
            // 
            // btn_removedocument
            // 
            this.btn_removedocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
  
            this.btn_removedocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_removedocument.Name = "btn_removedocument";
            this.btn_removedocument.Size = new System.Drawing.Size(23, 22);
            this.btn_removedocument.Click += new System.EventHandler(this.btn_removedocument_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_makebackgroundtransparent
            // 
            this.btn_makebackgroundtransparent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;

            this.btn_makebackgroundtransparent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_makebackgroundtransparent.Name = "btn_makebackgroundtransparent";
            this.btn_makebackgroundtransparent.Size = new System.Drawing.Size(23, 22);
            this.btn_makebackgroundtransparent.Text = "toolStripButton2";
            this.btn_makebackgroundtransparent.Click += new System.EventHandler(this.btn_makebackgroundtransparent_Click);
            // 
            // btn_cleardocument
            // 
            this.btn_cleardocument.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;

            this.btn_cleardocument.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_cleardocument.Name = "btn_cleardocument";
            this.btn_cleardocument.Size = new System.Drawing.Size(23, 22);
            this.btn_cleardocument.Click += new System.EventHandler(this.btn_cleardocument_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btn_moveup
            // 
            this.btn_moveup.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;

            this.btn_moveup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_moveup.Name = "btn_moveup";
            this.btn_moveup.Size = new System.Drawing.Size(23, 22);
            this.btn_moveup.Text = "toolStripButton1";
            // 
            // btn_moveDown
            // 
            this.btn_moveback.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;

            this.btn_moveback.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btn_moveback.Name = "btn_moveDown";
            this.btn_moveback.Size = new System.Drawing.Size(23, 22);
            // 
            // m_documentPanel
            // 
            this.m_documentPanel.AutoScroll = true;
            this.m_documentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.m_documentPanel.CaptionKey = null;
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
            this.c_bottomView.CaptionKey = null;
            this.c_bottomView.Controls.Add(this.c_label);
            this.c_bottomView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.c_bottomView.Location = new System.Drawing.Point(0, 340);
            this.c_bottomView.Margin = new System.Windows.Forms.Padding(0);
            this.c_bottomView.Name = "c_bottomView";
            this.c_bottomView.Size = new System.Drawing.Size(298, 28);
            this.c_bottomView.TabIndex = 0;
            // 
            // xLabel1
            // 
            this.c_label.CaptionKey = "lb.documents.caption";
            this.c_label.Location = new System.Drawing.Point(14, 8);
            this.c_label.Name = "xLabel1";
            this.c_label.Size = new System.Drawing.Size(108, 13);
            this.c_label.TabIndex = 0;
            this.c_label.TabStop = false;
            // 
            // UIXDocumentViewControl
            // 
            this.Controls.Add(this.m_documentPanel);
            this.Controls.Add(this.c_bottomView);
            this.Controls.Add(this.c_toolstrip);
            this.Name = "UIXDocumentViewControl";
            this.Size = new System.Drawing.Size(298, 368);
            this.c_toolstrip.ResumeLayout(false);
            this.c_toolstrip.PerformLayout();
            this.c_bottomView.ResumeLayout(false);
            this.c_bottomView.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void btn_duplicate_Click(object sender, EventArgs e)
        {
            this.Workbench.CallAction("2DDrawingDocument.DuplicateDocument");
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
                this.m_configuring = true;
                this.CurrentSurface.CurrentDocument.BackgroundTransparent =
                    !this.CurrentSurface.CurrentDocument.BackgroundTransparent;
                this.btn_makebackgroundtransparent.Checked = this.CurrentSurface.CurrentDocument.BackgroundTransparent;
                this.m_configuring = false;
            }
        }

        private void btn_addnewdocument_Click(object sender, EventArgs e)
        {
           CallAction ("2DDrawingDocument.AddNewDocument", true );
        }

        private void btn_cleardocument_Click(object sender, EventArgs e)
        {
            CallAction("2DDrawingDocument.Clear", true);
        }

        private void btn_removedocument_Click(object sender, EventArgs e)
        {
            CallAction("2DDrawingDocument.Delete", true);

        }

        internal void SetDocumentNewIndex(ICore2DDrawingDocument document)
        {
            this.RemoveDocument(document);
            this.m_documentPanel.InsertDocument(document, document.ZIndex);
        }

        //setup button document properties
        internal void SetupDocumentProperty()
        {
            this.CheckButtonState();
        }
    }
}
