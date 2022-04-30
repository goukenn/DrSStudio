

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XForm.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XForm.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.ICore.WinUI
{
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Windows.Native;

    [DefaultProperty ("Title")]
    public class XForm : Form, ICoreControl, IXForm, ICoreIdentifier, ICoreCaptionItem
    {
        public XForm()
        {
            var c = CoreResources.GetObject(CoreConstant.RES_APP_ICON) as Icon;
            this.Icon = c;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        #region "Caption text"
        protected string m_CaptionKey;
        public virtual string CaptionKey
        {
            get { return m_CaptionKey; }
            set
            {
                if (m_CaptionKey != value)
                {
                    this.m_CaptionKey = value;
                    OnCaptionKeyChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler CaptionChanged;
        private void OnCaptionKeyChanged(EventArgs eventArgs)
        {
            if (this.CaptionChanged != null)
                this.CaptionChanged(this, EventArgs.Empty);
        }
        public void LoadDisplayText()
        {
        }
        #endregion
        #region ICoreControl
        IXForm ICoreControl.FindForm() {
            return this.FindForm() as IXForm;
        }
        System.Collections.IList ICoreControl.Controls
        {
            get { return this.Controls; }
        }
        public Size2i GetMinimumSize()
        {
            return new Size2i(this.MinimumSize.Width, this.MinimumSize.Height);
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ICoreContextMenu AppContextMenu
        {
            get
            {
                return this.ContextMenuStrip as ICoreContextMenu;
            }
            set
            {
                this.ContextMenuStrip = value as ContextMenuStrip;
            }
        }

        public event EventHandler AppContextMenuChanged
        {
            add {
                this.ContextMenuStripChanged += value;
            }
            remove {
                this.ContextMenuStripChanged -= value;
            }
        }
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ICoreWorkbench Workbench
        {
            get { return CoreSystem.GetWorkbench(); }
        }
        ICoreGraphics ICoreControl.CreateGraphics()
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateDevice(this.CreateGraphics());
        }
        Vector2i ICoreControl.PointToScreen(Vector2i pt)
        {
            Point v_pt = this.PointToScreen(new System.Drawing.Point(pt.X, pt.Y));
            return new Vector2i(v_pt.X, v_pt.Y);
        }
        ICoreCursor ICoreControl.Cursor
        {
            get
            {
                return null;
            }
            set
            {
                if (value != null)
                {
                    this.Cursor = new Cursor(value.Handle);
                }
            }
        }
        Size2i ICoreControl.Size
        {
            get
            {
                return new Size2i(this.Width, this.Height);
            }
            set
            {
                this.ClientSize  = new Size(value.Width, value.Height);
            }
        }
        Vector2i ICoreControl.Location
        {
            get
            {
                return new Vector2i(this.Location.X, this.Location.Y);
            }
            set
            {
                this.Location = new Point(value.X, value.Y);
            }
        }
        Rectanglei ICoreControl.Bounds
        {
            get
            {
                return new Rectanglei(this.Bounds.X,
                    this.Bounds.Y,
                    this.Bounds.Width,
                    this.Bounds.Height);
            }
            set
            {
                this.Bounds = new Rectangle(value.X, value.Y, value.Width, value.Height);
            }
        }
        void ICoreControl.Invalidate(Rectanglei rc)
        {
            this.Invalidate(new Rectangle(rc.X,
                rc.Y,
                rc.Width,
                rc.Height));
        }
        Vector2i ICoreControl.PointToClient(Vector2i pt)
        {
            Point rpt = PointToClient(new Point(pt.X, pt.Y));
            return new Vector2i(rpt.X, rpt.Y);
        }
        object ICoreControl.BeginInvoke(Delegate coreMethodInvoker)
        {   
            return this.BeginInvoke(coreMethodInvoker);
        }
        [Browsable(false)]
        public new event EventHandler<CorePaintEventArgs> Paint;
        [Browsable(false)]
        public new event EventHandler<CoreMouseEventArgs> MouseClick;
        [Browsable(false)]
        public new event EventHandler<CoreMouseEventArgs> MouseDoubleClick;
        [Browsable(false)]
        public new event EventHandler<CoreMouseEventArgs> MouseDown;
        [Browsable(false)]
        public new event EventHandler<CoreMouseEventArgs> MouseMove;
        [Browsable(false)]
        public new event EventHandler<CoreMouseEventArgs> MouseUp;
        string ICoreIdentifier.Id
        {
            get { return this.Name; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            __onPaint(e);
        }
        private void __onPaint(PaintEventArgs e)
        {
            if (this.DesignMode) return;
            if (this.BackgroundDocument != null)
            {
                this.BackgroundDocument.Draw(e.Graphics);
            }
            if (this.Paint != null)
                this.Paint(this, new CorePaintEventArgs(CoreApplicationManager.Application.ResourcesManager.CreateDevice(e.Graphics)));
        }
        #endregion
        #region FormProperties
        private ICore2DDrawingDocument m_BackgroundDocument;
        [Browsable (false )]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public ICore2DDrawingDocument BackgroundDocument
        {
            get { return m_BackgroundDocument; }
            set
            {
                if (m_BackgroundDocument != value)
                {
                    m_BackgroundDocument = value;
                    OnBackgroundDocumentChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler BackgroundDocumentChanged;
        private void OnBackgroundDocumentChanged(EventArgs eventArgs)
        {
            this.Invalidate();
            if (this.BackgroundDocumentChanged != null)
                this.BackgroundDocumentChanged(this, eventArgs);
        }
        public static int TITLE_HEIGHT {
            get {
                return CoreRenderer.TitleHeight;
            }
        }
        //[Browsable(false)]
        //public new event EventHandler<CoreFormClosedEventArgs> FormClosed;
        //[Browsable(false)]
        //public new event EventHandler<CoreFormClosingEventArgs> FormClosing;
        [Browsable(false)]
        public new event EventHandler<CoreFormClosedEventArgs> Closed;
        [Browsable(false)]
        public new event EventHandler<CoreFormClosingEventArgs> Closing;
        ICoreIcon IXForm.Icon 
        {
            get
            {
                return null;
            }
            set
            {
                ICoreCursor c = value as ICoreCursor;
                if (c != null)
                {
                    this.Cursor = new System.Windows.Forms.Cursor(c.Handle);
                }
            }
        }
        IXForm IXForm.Owner
        {
            get
            {
                return this.Owner as IXForm;
            }
            set
            {
                this.Owner = value as Form;
            }
        }
        enuFormStartPosition IXForm.StartPosition
        {
            get
            {
                return (enuFormStartPosition)this.StartPosition;
            }
            set
            {
                this.StartPosition = (FormStartPosition)value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden )]
        public ICoreMenu AppMenu
        {
            get
            {
                return  this.MainMenuStrip as ICoreMenu;
            }
            set
            {
                this.MainMenuStrip  = value as MenuStrip;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        [EditorBrowsable (  EditorBrowsableState.Never )]
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
        public string Title
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value;
            }
        }
        public event EventHandler TitleChanged{
            add {
                this.TextChanged += value;
            }
            remove {
                this.TextChanged -= value;
            }
        }
        //ICoreControlCollections IXForm.Controls
        //{
        //    get { return this.Controls as ICoreControlCollections; }
        //}
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new XFormControlCollections(this);
        }
        private bool m_AllowHitTest;
        /// <summary>
        /// Get or set if this element allow hit test
        /// </summary>
        public bool AllowHitTest
        {
            get { return m_AllowHitTest; }
            set
            {
                if (m_AllowHitTest != value)
                {
                    m_AllowHitTest = value;
                }
            }
        }
        /// <summary>
        /// build the rectangle from element element
        /// </summary>
        /// <param name="rc"></param>
        /// <param name="maximized"></param>
        protected virtual void BuildClientRectangle(ref Rectangle rc, bool maximized)
        {
        }
        protected static int GET_X_LPARAM(IntPtr h)
        {
            return User32.GET_X_LPARAM(h);
        }
        protected static int GET_Y_LPARAM(IntPtr h)
        {
            return User32.GET_Y_LPARAM(h);
        }
        protected static Point GetPoint(IntPtr h)
        {
            return User32.GetPoint(h);
        }
        protected virtual void OnNCHitTest(ref Message m)
        {
            //hit test for resizing
            int x = GET_X_LPARAM(m.LParam);
            int y = GET_Y_LPARAM(m.LParam);
            const int BSIZE = 4;
            Point pt = GetPoint(m.LParam);
            if (this.WindowState == FormWindowState.Maximized)
            {
                int TITLE_HEIGHT = CoreRenderer.TitleHeight;
                Rectangle rc = new Rectangle(0, -TITLE_HEIGHT, this.Width, TITLE_HEIGHT);
                Point scr = PointToClient(pt);
                if (rc.Contains(scr))
                {
                    m.Result = (IntPtr)User32.HTCAPTION;
                    return;
                }
            }
            else
            {
                Rectangle rc = new Rectangle(0, -TITLE_HEIGHT, this.Width, TITLE_HEIGHT);
                Point scr = PointToClient(pt);
                if (rc.Contains(scr))
                {
                    m.Result = (IntPtr)User32.HTCAPTION;
                    return;
                }
                //check for 
                if ((x & 0x8000) != 0)
                {
#pragma warning disable IDE0054 // Use compound assignment
                    x = x - ushort.MaxValue;
#pragma warning restore IDE0054 // Use compound assignment
                }
                if ((y > 0) && ((y & 0x8000) != 0))
                {
#pragma warning disable IDE0054 // Use compound assignment
                    y = y - ushort.MaxValue;
#pragma warning restore IDE0054 // Use compound assignment
                }
                Point p = new Point(x - this.Bounds.X, y - this.Bounds.Y);
                rc = new Rectangle(0, 0, this.Width, BSIZE);
                int v_res = 0;
                //check top or bottom
                if (rc.Contains(p))
                {
                    v_res = User32.HTTOP;
                }
                else
                {
                    rc = new Rectangle(0, this.Height - BSIZE, this.Width, BSIZE);
                    if (rc.Contains(p))
                    {
                        v_res = User32.HTBOTTOM;
                    }
                }
                //check lefet or right
                rc = new Rectangle(0, 0, BSIZE, this.Height);
                int v_r = 0;
                if (rc.Contains(p))
                {
                    v_r = User32.HTLEFT;
                }
                else
                {
                    rc = new Rectangle(this.Width - BSIZE, 0, BSIZE, this.Height);
                    if (rc.Contains(p))
                    {
                        v_r = User32.HTRIGHT;
                    }
                }
                if ((v_r != 0) || (v_res != 0))
                {
                    if (v_res == 0)
                    {
                        m.Result = (IntPtr)v_r;
                    }
                    else if (v_r == 0)
                    {
                        m.Result = (IntPtr)v_res;
                    }
                    else
                    {
                        switch (v_res)
                        {
                            case User32.HTTOP:
                                {
                                    switch (v_r)
                                    {
                                        case User32.HTLEFT:
                                            m.Result = (IntPtr)User32.HTTOPLEFT;
                                            break;
                                        default:
                                            m.Result = (IntPtr)User32.HTTOPRIGHT;
                                            break;
                                    }
                                }
                                break;
                            case User32.HTBOTTOM:
                                {
                                    switch (v_r)
                                    {
                                        case User32.HTLEFT:
                                            m.Result = (IntPtr)User32.HTBOTTOMLEFT;
                                            break;
                                        default:
                                            m.Result = (IntPtr)User32.HTBOTTOMRIGHT;
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
        protected void RenderNonClientArea(IntPtr hwnd, IntPtr hreg)
        {
            IntPtr hdc = User32.GetWindowDC(hwnd);
            //IntPtr hdc = User32.GetDCEx(
            //         hwnd,
            //         hreg,
            //         (int)(
            //         User32.DCX_WINDOW
            //         | User32.DCX_INTERSECTRGN
            //     | User32.DCX_CACHE
            //         | User32.DCX_CLIPSIBLINGS
            //         ));
            if (hdc == IntPtr.Zero)
                return;
            Region rg;
            if (hreg.ToInt32() != 1)
                rg = Region.FromHrgn(hreg);
            else
            {
                RECT rc = new RECT();
                User32.GetWindowRect(hwnd, ref rc);
                Rectangle v_rc = User32.GetRectangleFrom(rc);
                v_rc.X -= this.Location.X;
                v_rc.Y -= this.Location.Y;
                rg = new Region(
                    v_rc
                    );
            }
            using (Graphics g = Graphics.FromHdc(hdc))
            {
                OnRenderNonClientArea(new RenderNonClientAreaNonClientAreaEventArgs(g,
                    Rectangle.Round(rg.GetBounds(g)),
                    rg
                    ));
            }
            User32.ReleaseDC(hwnd, hdc);
        }
        protected virtual void OnRenderNonClientArea(RenderNonClientAreaNonClientAreaEventArgs e)
        {
        }
        [Browsable (false )]
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
        public new enuDialogResult DialogResult
        {
            get { return (enuDialogResult)base.DialogResult; }
            set {
                base.DialogResult = (System.Windows.Forms.DialogResult)value;
            }
        }
        public new enuDialogResult ShowDialog()
        {
            enuDialogResult r = (enuDialogResult)base.ShowDialog();
            return r;
        }
        #endregion
        class XFormControlCollections : ControlCollection, ICoreControlCollections
        {
            private XForm xForm;
            public XFormControlCollections(XForm xForm):base(xForm )
            {
                this.xForm = xForm;
            }
            public void Add(object obj)
            {
                base.Add(obj as Control);
            }
            public void Remove(object obj)
            {
                base.Remove(obj as Control);
            }
        }
        protected virtual Vector2f GetFactorLocation(int x, int y)
        {
            return new Vector2f(x, y);
        }
        #region Form closeEvents
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            if (this.Closed != null)
            {
                this.Closed(this, new CoreFormClosedEventArgs());
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (this.Closing != null)
            {
                CoreFormClosingEventArgs c = new CoreFormClosingEventArgs((enuCloseReason)e.CloseReason, e.Cancel );
                this.Closing(this, c);
                e.Cancel = c.Cancel;
            }
        }
        #endregion
        #region "Mouse Overrides"
        protected  override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            OnCoreMouseClick(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected  override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            OnCoreMouseDoubleClick(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected  override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            OnCoreMouseMove(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected  override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            OnCoreMouseUp(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected  override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            OnCoreMouseDown(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected virtual void OnCoreMouseClick(CoreMouseEventArgs e)
        {
            if (this.MouseClick != null)
                this.MouseClick(this, e);
        }
        protected virtual void OnCoreMouseDoubleClick(CoreMouseEventArgs e)
        {
            if (this.MouseDoubleClick != null)
                this.MouseDoubleClick(this, e);
        }
        protected virtual void OnCoreMouseMove(CoreMouseEventArgs e)
        {
            if (this.MouseMove != null)
                this.MouseMove(this, e);
        }
        protected virtual void OnCoreMouseDown(CoreMouseEventArgs e)
        {
            if (this.MouseDown != null)
                this.MouseDown(this, e);
        }
        protected virtual void OnCoreMouseUp(CoreMouseEventArgs e)
        {
            if (this.MouseUp != null)
                this.MouseUp(this, e);
        }
        #endregion
        #region Keys Events
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            OnCoreKeyDown(new CoreKeyEventArgs((enuKeys)e.KeyCode));
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            OnCoreKeyUp(new CoreKeyEventArgs((enuKeys)e.KeyCode));
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            OnCoreKeyPress(new CoreKeyEventArgs((enuKeys)(int)e.KeyChar));
        }
        protected virtual void OnCoreKeyDown(CoreKeyEventArgs e)
        {
            if (KeyDown != null)
                this.KeyDown(this, e);
        }
        protected virtual void OnCoreKeyUp(CoreKeyEventArgs e)
        {
            if (KeyUp != null)
                this.KeyUp(this, e);
        }
        protected virtual void OnCoreKeyPress(CoreKeyEventArgs e)
        {
            if (KeyPress != null)
                this.KeyPress(this, e);
        }
        [Browsable(false)]
        public new event EventHandler<CoreKeyEventArgs> KeyDown;
        [Browsable(false)]
        public new event EventHandler<CoreKeyEventArgs> KeyUp;
        [Browsable(false)]
        public new event EventHandler<CoreKeyEventArgs> KeyPress;
        #endregion
    }
}

