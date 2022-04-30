

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XDocumentViewPanel.cs
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

  
using System; using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace IGK.DrSStudio.Drawing2D.WinUI
{
    using IGK.DrSStudio.Drawing2D;
    using IGK.ICore.Drawing2D.Tools;
    using IGK.ICore.Resources;
    using System.Windows.Forms.Layout;
    using IGK.DrSStudio.Drawing2D.Tools;
    using IGK.ICore.Actions;
    using IGK.ICore.WinCore.WinUI.Controls;



    [ToolboxBitmap(typeof(Panel))]
    internal sealed class XDocumentViewPanel : IGKXPanel
    {
        private XDocumentViewCollection m_documents;
        private ICore2DDrawingDocument m_SelectedDocument;

        /// <summary>
        /// get or set the selected document
        /// </summary>
        public ICore2DDrawingDocument SelectedDocument
        {
            get
            {
                return this.m_SelectedDocument;
            }
            set
            {
                if (this.m_SelectedDocument != value)
                {
                    this.m_SelectedDocument = value;
                    OnSelectedDocumentChanged(EventArgs.Empty);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        public event EventHandler SelectedDocumentChanged;
        private XDocumentViewLayoutEngine m_documentsLayoutEngine;

        private void OnSelectedDocumentChanged(EventArgs e)
        {
            if (this.SelectedDocumentChanged != null)
                this.SelectedDocumentChanged(this, e);
        }
        public XDocumentViewCollection Documents
        {
            get
            {
                return m_documents;
            }
        }
        public XDocumentViewPanel()
        {
            this.m_documents = new XDocumentViewCollection(this);
            this.InitializeComponent();
            this.MinimumSize = new Size(106, 24);

        }
        private void InitializeComponent()
        {
        }

        internal void Clear()
        {
            this.Controls.Clear();
            this.Documents.Clear();
        }

        public override System.Windows.Forms.Layout.LayoutEngine LayoutEngine
        {
            get
            {
                if (m_documentsLayoutEngine == null)
                    m_documentsLayoutEngine = new XDocumentViewLayoutEngine(this);
                return m_documentsLayoutEngine;
            }
        }

        protected override Size DefaultMinimumSize
        {
            get
            {
                return new Size(106, 24);
            }
        }
        internal void Populate(ICore2DDrawingSurface surface)
        {
            if (surface.Documents == null)
                return;
            this.SuspendLayout();
            XDocumentBlock v_ctr = null;
            int j = this.VerticalScroll.Value;
            this.m_SelectedDocument = surface.CurrentDocument;
            int wscroll = 0;
            int v_gheight = 3 + surface.Documents.Count * (XDocumentBlock.DEFAUL_HEIGHT + 3) + 3;

            if (v_gheight > this.Height)
            {//
                wscroll = System.Windows.Forms.SystemInformation.VerticalScrollBarWidth;
            }
            foreach (ICore2DDrawingDocument v_document in surface.Documents)
            {
                v_ctr = new XDocumentBlock(this, v_document);
                v_ctr.Bounds = new Rectangle(3, j + 3, this.Width - (6 + wscroll),
                    XDocumentBlock.DEFAUL_HEIGHT);

                j += XDocumentBlock.DEFAUL_HEIGHT + 3;
                this.Documents.Add(v_ctr);
            }

            this.ResumeLayout();
        }

        class XDocumentViewLayoutEngine : LayoutEngine
        {
            private XDocumentViewPanel m_owner;
            public XDocumentViewLayoutEngine(XDocumentViewPanel owner)
            {
                this.m_owner = owner;
            }
            public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
            {
                int j = 0;
                int wscroll = 0;
                j -= m_owner.VerticalScroll.Visible ? m_owner.VerticalScroll.Value : 0;
                wscroll = m_owner.VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth  +4 : 2;
                XDocumentViewPanel r = container as XDocumentViewPanel;
                r.Margin = Padding.Empty;
                r.Padding = Padding.Empty;
               // int v_gheight = 3 + r.Documents.Count * (XDocumentBlock.DEFAUL_HEIGHT + 3) + 3;
                foreach (XDocumentBlock item in r.Controls)
                {
                    if (item == null) continue;
                    item.Margin = Padding.Empty;
                    
                    item.Bounds = new Rectangle(2, j + 2, r.Width - wscroll,
                        XDocumentBlock.DEFAUL_HEIGHT);

                    j += XDocumentBlock.DEFAUL_HEIGHT + 2;
                }
                return false;
            }
        }
        
        protected override void OnSizeChanged(EventArgs e)
        {
            //arrange child
            //OrganiseControl();
            base.OnSizeChanged(e);
        }

        //private void OrganiseControl()
        //{
        //    this.VerticalScroll.Value = 0;
        //    this.SuspendLayout();

        //    int j = 0;
        //    int v_c = 0;
          
        //    int wscroll = this.VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0;
        //    int v_offsety = 0;// this.VerticalScroll.Visible ? this.VerticalScroll.Value : 0;
        //    int v_offsetx = this.HorizontalScroll.Visible ? this.HorizontalScroll.Value : 0;
         
        //    int g = -(this.AutoScrollOffset.Y + this.VerticalScroll.Value);
        //    foreach (Control ctr in this.Controls)
        //    {
        //        if (!(ctr is XDocumentBlock)) continue;

        //        j = (v_c * (XDocumentBlock.DEFAUL_HEIGHT + 3)) + 3 ;
        //        ctr.Bounds = new Rectangle(
        //            3 - v_offsetx,
        //            j - v_offsety,
        //            this.Width - (6 + wscroll),
        //            XDocumentBlock.DEFAUL_HEIGHT
        //            );

        //        v_c++;
        //    }


        //    this.ResumeLayout();
        //}



        internal void AddDocument(ICore2DDrawingDocument document)
        {
            if (this.Documents.Contains(document))
                return;
            XDocumentBlock v_ctr = null;
            // int wscroll = 0;
            int v_gheight = 3 + (this.m_documents.Count + 1) * (XDocumentBlock.DEFAUL_HEIGHT + 3) + 3;

            v_ctr = new XDocumentBlock(this, document);
            this.SuspendLayout();
            int v_offsety = this.VerticalScroll.Visible ? this.VerticalScroll.Value : 0;
            int v_offsetx = this.HorizontalScroll.Visible ? this.HorizontalScroll.Value : 0;
            int wscroll = this.VerticalScroll.Visible ? System.Windows.Forms.SystemInformation.VerticalScrollBarWidth : 0;

            int j = 3 + this.Documents.Count * (XDocumentBlock.DEFAUL_HEIGHT + 3) -v_offsety ;
            v_ctr.Bounds = new Rectangle(3 - v_offsetx, j - v_offsety, this.Width - (6 + wscroll), XDocumentBlock.DEFAUL_HEIGHT);
            this.Documents.Add(v_ctr);
            this.ResumeLayout();
            this.PerformLayout();
        }

        internal void RemoveDocument(ICore2DDrawingDocument document)
        {
            this.SuspendLayout();
            this.Documents.Remove(document);
            this.ResumeLayout();
        }

        internal void InsertDocument(ICore2DDrawingDocument document, int p)
        {
            this.SuspendLayout();
            this.Documents.Insert(document, p);            
            this.ResumeLayout(true);
        }


        internal void RefreshDocument()
        {
            if (m_documents.Contains(this.m_SelectedDocument) && (this.m_SelectedDocument != null))
            {
                m_documents[this.m_SelectedDocument].RefreshDocument();
            }
        }


        /// <summary>
        /// document block marging
        /// </summary>
        internal class XDocumentBlock : IGKXControl
        {
            ICore2DDrawingDocument m_document;
            private bool m_selected;
            private bool m_requireRefresh;
            internal const int DEFAUL_HEIGHT = 100;
            internal const int TITLE_BARHEIGHT = 18;
            private XDocumentViewPanel m_panel;
            private Timer m_timer;
            private static Bitmap m_dashBackgroundImage;
            private ICoreBitmap m_documentBmp;

            public bool Selected
            {
                get
                {
                    return this.m_selected;
                }
                set
                {
                    if (this.m_selected != value)
                    {
                        this.m_selected = value;
                        this.Invalidate();
                    }
                }
            }

            static Bitmap DashBackgroundImage
            {
                get
                {
                    if (m_dashBackgroundImage == null)
                        m_dashBackgroundImage = CoreResources.GetDocument("dash").ToBitmap().ToGdiBitmap(true);
                    return m_dashBackgroundImage;
                }
            }
            public ICore2DDrawingDocument Document
            {
                get { return m_document; }
            }
            internal XDocumentBlock(XDocumentViewPanel panel, ICore2DDrawingDocument document)
            {
                this.SetStyle(ControlStyles.ResizeRedraw, true);
                this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                this.SetStyle(ControlStyles.UserPaint, true);
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                this.m_requireRefresh = false;
                this.m_document = document;
                this.m_panel = panel;
                this.m_panel.SelectedDocumentChanged += new EventHandler(m_panel_SelectedDocumentChanged);
                this.m_document.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(document_PropertyChanged);
                this.m_selected = (this.m_document == this.m_panel.SelectedDocument);
                this.m_timer = new Timer();
                this.m_timer.Tick += new EventHandler(m_timer_Tick);
                this.SizeChanged += new EventHandler(XDocumentBlock_SizeChanged);
                this.Disposed += new EventHandler(XDocumentBlock_Disposed);
                this.Paint += _Paint;
            }

            private void _Paint(object sender, CorePaintEventArgs e)
            {

                if (this.m_requireRefresh)
                {
                    this.GetDocumentBmp();
                    this.m_requireRefresh = false;
                }
                RenderBlock(e);
            }

            void XDocumentBlock_Disposed(object sender, EventArgs e)
            {
                //protected for disposing
                if (this.m_document != null)
                {
                    this.m_panel.SelectedDocumentChanged -= new EventHandler(m_panel_SelectedDocumentChanged);
                    this.m_document.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(document_PropertyChanged);
                }
            }

            void XDocumentBlock_SizeChanged(object sender, EventArgs e)
            {
                GetDocumentBmp();
            }
            /// <summary>
            /// update the document image
            /// </summary>
            private void GetDocumentBmp()
            {
                if (this.ClientSize.IsEmpty)
                {
                    this.m_documentBmp = null;
                    return;
                }
                Rectanglef v_rc = CoreMathOperation.GetProportionalRectangle(
                      new Rectanglef(
                          this.ClientRectangle.X,
                          this.ClientRectangle.Y ,
                          this.ClientSize.Width,
                          this.ClientSize.Height
                          ),
                      this.m_document.Bounds);
                if ((v_rc.Width > 0) && (v_rc.Height > 0))
                {
                    if (this.m_documentBmp != null)
                        this.m_documentBmp.Dispose();
                    m_documentBmp =
                        CoreApplicationManager.Application.ResourcesManager.CreateBitmap((int)v_rc.Width,(int) v_rc.Height);
                    var def = CoreApplicationManager.Application.ResourcesManager.CreateDevice(m_documentBmp);
                    if (def != null)
                    {
                        this.m_document.Draw(def, true, new Rectanglei(0, 0, m_documentBmp.Width, m_documentBmp.Height), enuFlipMode.None);
                        def.Dispose();
                    }
                }
            }

            void m_timer_Tick(object sender, EventArgs e)
            {
                this.m_timer.Enabled = false;
                this.GetDocumentBmp();
                this.m_requireRefresh = false;
                this.Invalidate();
            }


            void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.m_requireRefresh = true;
                this.RefreshDocument();
            }

            internal void RefreshDocument()
            {
                if (CoreTool_DocumentView.Instance.Visible && this.Enabled)
                {
                    this.m_timer.Enabled = true;
                }
            }

            void m_panel_SelectedDocumentChanged(object sender, EventArgs e)
            {
                this.Selected = (this.m_panel.SelectedDocument == this.Document);
            }
            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    if (this.m_timer != null)
                    {
                        this.m_timer.Dispose();
                        this.m_timer = null;
                    }
                    this.m_panel.SelectedDocumentChanged -= new EventHandler(m_panel_SelectedDocumentChanged);
                }
                base.Dispose(disposing);
            }

            void RenderBlock(CorePaintEventArgs e)
            {
                Rectanglef v_rc = CoreMathOperation.GetProportionalRectangle(
                    new Rectanglef(
                        new Vector2f(this.ClientRectangle.X, this.ClientRectangle.Y),
                        new Size2f(this.ClientSize.Width, this.ClientSize.Height)),

                    this.m_document.Bounds);
                object s = e.Graphics.Save();
                e.Graphics.PixelOffsetMode = enuPixelOffset.None;
                //Region rg = e.Graphics.Clip.Clone();
                //rg.Intersect(v_rc);
                //e.Graphics.Clip = rg;
                if (this.Selected)
                {
                    //Pen p = WinCoreBrushRegister.GetPen(XDocumentViewPanelRenderer.DocumentViewBorderColor);
                    e.Graphics.Clear(XDocumentViewPanelRenderer.DocumentViewSelectedBackgroundColor);
                }
                if ((DashBackgroundImage != null) && (DashBackgroundImage.PixelFormat != System.Drawing.Imaging.PixelFormat.Undefined))
                {
                    ICoreBitmap bmp = CoreResources.GetDocument (CoreConstant.IMG_DASH).ToBitmap();
                    Brush br = CoreBrushRegisterManager.GetBrush<Brush>(bmp);
                    e.Graphics.FillRectangle(br, v_rc);
                    br.Dispose();
                    bmp.Dispose();
                   
                }

                if (this.m_documentBmp == null)
                {
                    //try to get document bitmap
                    this.GetDocumentBmp();
                }
                this.m_documentBmp.Draw(e.Graphics, new Rectanglei(0,0,
                    this.ClientRectangle.Width,
                    this.ClientRectangle.Height), true , enuFlipMode.None );
             //   this.m_documentBmp.Draw(e.Graphics, Rectanglei.Round(v_rc), true, enuFlipMode.None);

                e.Graphics.Restore(s);
                if (this.Selected)
                {
                    Pen p = WinCoreBrushRegister.GetPen(XDocumentViewPanelRenderer.DocumentViewBorderColor);
                    e.Graphics.DrawRectangle(Colorf.Black,
                        v_rc.X,
                        v_rc.Y,
                        v_rc.Width-1,
                        v_rc.Height-1);                    
                }

            }
        }
        internal class XDocumentViewCollection : System.Collections.IEnumerable
        {
            XDocumentViewPanel m_xpanel;
            Dictionary<ICore2DDrawingDocument, XDocumentBlock> m_rdic;

            public int Count { get { return this.m_rdic.Count; } }

            internal XDocumentBlock this[ICore2DDrawingDocument doc]
            {
                get
                {
                    return this.m_rdic[doc];
                }
            }
            ///<summary>
            ///internal .ctr
            ///<summary>
            internal XDocumentViewCollection(XDocumentViewPanel pan)
            {
                this.m_rdic = new Dictionary<ICore2DDrawingDocument, XDocumentBlock>();
                this.m_xpanel = pan;
            }
            public void Add(XDocumentBlock document)
            {
                if (!this.m_xpanel.Controls.Contains(document))
                {
                    if (!this.m_rdic.ContainsKey(document.Document))
                    {
                        this.m_rdic.Add(document.Document, document);
                        this.m_xpanel.Controls.Add(document);
                        document.Click += new EventHandler(document_Click);
                        document.DoubleClick += new EventHandler(document_DoubleClick);
                    }
                }
            }

            void document_DoubleClick(object sender, EventArgs e)
            {

                XDocumentBlock doc = sender as XDocumentBlock;
                if (this.m_xpanel.SelectedDocument != doc.Document)
                {
                    this.m_xpanel.SelectedDocument = doc.Document;
                }
                //edit property
                ICoreAction ak = CoreSystem.GetAction("2DDrawingDocument.Properties");
                if (ak != null)
                    ak.DoAction();
                else
                    CoreMessageBox.Show("No action with name 2DDrawingDocument.Properties registrated");
            }

            void document_Click(object sender, EventArgs e)
            {
                XDocumentBlock doc = sender as XDocumentBlock;
                this.m_xpanel.SelectedDocument = doc.Document;
                this.m_xpanel.Focus();
            }
            public void Clear()
            {
                this.m_rdic.Clear();
            }


            #region IEnumerable Members

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_xpanel.Controls.GetEnumerator();
            }

            #endregion

            internal void Remove(ICore2DDrawingDocument document)
            {
                if (this.m_rdic.ContainsKey(document))
                {
                    this.m_xpanel.Controls.Remove(this.m_rdic[document]);

                    this.m_rdic[document].Click -= new EventHandler(document_Click);
                    this.m_rdic[document].DoubleClick -= new EventHandler(document_DoubleClick);
                    this.m_rdic.Remove(document);
                    this.m_xpanel.ResumeLayout(true);
                    //this.m_xpanel.OrganiseControl();
                }
            }

            internal bool Contains(ICore2DDrawingDocument document)
            {
                if (document == null) return false;

                return this.m_rdic.ContainsKey(document);
            }

            internal void Insert(ICore2DDrawingDocument document, int p)
            {
                if ((document == null) || (this.m_rdic.ContainsKey(document)))
                    return;
                XDocumentBlock ctr = new XDocumentBlock(this.m_xpanel, document);


                this.m_rdic.Add(document, ctr);
                this.m_xpanel.Controls.Add(ctr);
                this.m_xpanel.Controls.SetChildIndex(ctr, p);
                ctr.Click += new EventHandler(document_Click);
                ctr.DoubleClick += new EventHandler(document_DoubleClick);


            }
        }
    }
}
