

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioMainFormCaptionbar.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioMainFormCaptionbar.cs
*/
using IGK.ICore.Drawing2D;

using IGK.ICore.Resources;
using IGK.ICore.Windows.Native;
using IGK.ICore.WinUI;
using IGK.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent the main form caption bar
    /// </summary>
    public sealed class XMainFormCaptionBar :
        IGKXControl,
        IXCoreCaptionBarControl
    {
        const int BAR_SIZE = 28;
        const int BUTTONSIZE_SIZE = 16;
        private IGKXButton m_closeButton;
        private IGKXButton m_maximizeButton;
        private IGKXButton m_reduceButton;
        private Vector2i m_dragStart;
        private ICore2DDrawingDocument m_drsLogo;

        public override string Text
        {
            get
            {
                return base.Text;

            }

            set
            {
                base.Text = value;
            }
        }
        public XMainFormCaptionBar():base()
        {
            //init main style
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.BackColor = System.Drawing.Color.Transparent;


            //create button
            m_closeButton = new IGKXButton();
            m_maximizeButton = new IGKXButton();
            m_reduceButton = new IGKXButton();

            m_closeButton.Name = "btn_close";
            m_maximizeButton.Name = "btn_maximize";
            m_reduceButton.Name = "btn_reduce";

            m_closeButton.Paint += _ClosePaint;
            m_closeButton.Name = "closeButton";
            //init button document
            m_closeButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.BTN_CLOSE_GKDS));
            m_maximizeButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.BTN_MAXIMIZE_GKDS));
            m_reduceButton.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(CoreImageKeys.BTN_REDUCE_GKDS));

            //import logo from system
            m_drsLogo = CoreResources.GetDocument(CoreImageKeys.DRSLOGO_GKDS);


            this.MinimumSize = new System.Drawing.Size(0, BAR_SIZE);
            this.MaximumSize = new System.Drawing.Size(0, BAR_SIZE);
            this.MouseDown += _MouseDown;
            this.SizeChanged += new EventHandler(XMainFormCaptionBar_SizeChanged);
            this.MouseDoubleClick += _MouseDoubleClick;
            this.MouseMove += _MouseMove;
            this.Controls.AddRange(
                new Control[]{
                    m_closeButton ,
                    m_maximizeButton ,
                    m_reduceButton 
                });
            this.m_reduceButton.Click += new EventHandler(m_restoreButton_Click);
            this.m_maximizeButton.Click += new EventHandler(m_maximizeButton_Click);
            this.m_closeButton.Click += new EventHandler(m_closeButton_Click);
            InitButtonBound();

            var m = WinCoreControlRenderer.MainFormCaptionBarTextFont;
            this.Font = m.ToGdiFont();
            m.FontDefinitionChanged += (o, e) =>
            {
                this.Font = m.ToGdiFont();
            };
        }

        private void _ClosePaint(object sender, CorePaintEventArgs e)
        {
            m_closeButton.Name = "closeButton";
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
        }
        void _MouseMove(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Left)
            {
                Size s = System.Windows.Forms.SystemInformation.DragSize;
                Rectangle rc =
                    new Rectangle(
                        this.m_dragStart.X, this.m_dragStart.Y ,
                        s.Width , s.Height );
                if (!rc.Contains(e.Location))
                {
                    Form frm = this.FindForm();
                    if (frm.WindowState == FormWindowState.Normal)
                    {
                        this.m_dragStart = Vector2i.Zero;
                        this.Capture = false;
                        User32.SendMessage(frm.Handle,
                        User32.WM_NCLBUTTONDOWN,
                        (IntPtr)User32.HTCAPTION,
                        IntPtr.Zero);
                    }
                }
            }
        }
        void _MouseDoubleClick(object sender, CoreMouseEventArgs e)
        {
            if (e.Button == enuMouseButtons.Left)
            {
                m_maximizeButton_Click(this, EventArgs.Empty);
            }
        }
        void m_closeButton_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm != null)
                frm.Close();
        }
        void m_maximizeButton_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm != null)
            {
                if (frm.WindowState != FormWindowState.Maximized)
                    frm.WindowState = FormWindowState.Maximized;
                else
                    frm.WindowState = FormWindowState.Normal;
            }
            Application.DoEvents();
        }
        void m_restoreButton_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            frm.WindowState = FormWindowState.Minimized;
        }
        void XMainFormCaptionBar_SizeChanged(object sender, EventArgs e)
        {
            InitButtonBound();
        }
        private void InitButtonBound()
        {
            Vector2f rc =
                new Vector2i(
                    this.Width - this.m_closeButton.Width - 2,
                    (this.Height - this.m_closeButton.Height) / 2);
            this.m_closeButton.Location = rc.CoreConvertTo<Point>();
            rc.Y = (this.Height - this.m_maximizeButton.Height) / 2;
            rc.X -= (this.m_maximizeButton.Width + 2);
            this.m_maximizeButton.Location = rc.CoreConvertTo<Point>();
            rc.Y = (this.Height - this.m_reduceButton.Height) / 2;
            rc.X -= (this.m_reduceButton.Width + 2);
            this.m_reduceButton.Location = rc.CoreConvertTo<Point>();
        }
        void _MouseDown(object sender, CoreMouseEventArgs e)
        {
            Form frm = FindForm();
            if (frm != null)
            {
                //move the form
                this.m_dragStart = e.Location;
            }
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
           // RenderMainFormCaptionBar(e.Graphics);
            ////paint bar 
            using (LinearGradientBrush v_br = 
                WinCoreBrushRegister.CreateBrush<LinearGradientBrush> (
                this.ClientRectangle ,
                WinCoreControlRenderer.MainFormCaptionBarStartColor,
                WinCoreControlRenderer.MainFormCaptionBarEndColor,
                90.0f))
            {
                e.Graphics.FillRectangle(v_br, this.ClientRectangle);
            }
            //get parent form 
            Form frm = FindForm();
            if (frm != null)
            {
                Rectangle v_rc = this.ClientRectangle;
                v_rc.Inflate(-2, -2);
                v_rc.X += 32;
                v_rc.Width = v_rc.Width - (v_rc.Width - this.m_reduceButton.Location.X) - 64;
                if (v_rc.Width > 0)
                {
                    //render string text 
                    Colorf v_c = WinCoreControlRenderer.MainFormCaptionBarForeColor;
                    StringFormat v_sf = new StringFormat();
                    v_sf.Alignment = StringAlignment.Center;
                    v_sf.LineAlignment = StringAlignment.Center;
                    //in case that text will be more long that the avalaiable space
                    v_sf.Trimming = StringTrimming.EllipsisWord;
                    //no pass to other line
                    v_sf.FormatFlags = StringFormatFlags.NoWrap;
                            //draw the string text
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit   ;
                    e.Graphics.TextContrast = 12;

                    e.Graphics.DrawString(frm.Text,
                        this.Font,
                        CoreBrushRegisterManager.GetBrush<Brush>(v_c),
                        v_rc,
                        v_sf);
                    e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
              
                    v_sf.Dispose();
                }
            }
            if (this.m_drsLogo != null)
            {
                this.m_drsLogo.BackgroundTransparent = true ;
                //this.m_drsLogo.FillBrush.SetSolidColor(Colorf.CornflowerBlue );
                this.m_drsLogo.Draw(e.Graphics, true, enuFlipMode.None, 2, 2, 32, this.Height-4);
            }
            base.OnPaint(e);
        }
        
        public string Id
        {
            get { return "CaptionBar"; }
        }
    }
}

