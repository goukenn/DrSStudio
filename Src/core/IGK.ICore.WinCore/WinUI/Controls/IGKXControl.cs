

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXControl.cs
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
file:IGKXControl.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.GraphicModels;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using IGK.ICore.Settings;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    [DefaultProperty("CaptionKey")]
    public class IGKXControl : Control, ICoreControl , ICoreMouseStateObject 
    {
       
        [Browsable (false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        public IGKXControl()
        {
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            CoreApplicationSetting.Instance.LangReloaded += Instance_LangReloaded;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CoreApplicationSetting.Instance.LangReloaded -= Instance_LangReloaded;
            }
            base.Dispose(disposing);
        }

        void Instance_LangReloaded(object sender, EventArgs e)
        {
           // CoreLog.WriteDebug ("Lang require to reload resource string");
            LoadDisplayText();
        }

        [Browsable (false )]
        [EditorBrowsable (  EditorBrowsableState.Never )]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden  )]
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
        protected ICoreGraphics CreateGraphics(Graphics g)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateDevice(g);
        }

        private enuMouseState m_MouseState;
        [Browsable (false )]
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
        public enuMouseState MouseState
        {
            get { return m_MouseState; }
            set
            {
                if (m_MouseState != value)
                {
                    m_MouseState = value;
                    this.Invalidate();
                    OnMouseStateChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler MouseStateChanged;
        protected virtual void OnMouseStateChanged(EventArgs eventArgs)
        {
            if (this.MouseStateChanged != null)
                this.MouseStateChanged(this, eventArgs);
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
                    this.LoadDisplayText();
                    OnCaptionKeyChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// event raised when caption key changed
        /// </summary>
        public event EventHandler CaptionChanged;

        protected virtual void OnCaptionKeyChanged(EventArgs eventArgs)
        {
            if (this.CaptionChanged != null)
                this.CaptionChanged(this, EventArgs.Empty);
        }
        public virtual void LoadDisplayText()
        {
            if (!string.IsNullOrEmpty(this.CaptionKey))
            {
                this.Text = this.CaptionKey.R();
            }
        }
        #endregion
        #region ICoreControl
        IXForm ICoreControl.FindForm()
        {
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
        [DesignerSerializationVisibility ( DesignerSerializationVisibility.Hidden )]
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
                this.Size = new Size(value.Width, value.Height);
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
        #region Keys Events 
        protected  override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            OnCoreKeyDown(new CoreKeyEventArgs((enuKeys) e.KeyCode));
        }
        protected  override void OnKeyUp(KeyEventArgs e)
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
            if ((CoreApplicationManager.Application ==null))
                return;
            try
            {
                CorePaintEventArgs c = new CorePaintEventArgs(CreateGraphics(e.Graphics));
                OnCorePaint(c);
            }
            catch { 
            }
        }
        protected virtual  void OnCorePaint(CorePaintEventArgs e)
        {
            if (this.Paint !=null)
                this.Paint (this,e);
        }
        #endregion
        public virtual Vector2f GetFactorLocation(int x, int y)
        {
            return new Vector2f(x, y);
        }
        #region "Mouse Overrides"
        protected  override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            OnCoreMouseClick(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation (e.X, e.Y ),
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


        public event EventHandler AppContextMenuChanged
        {
            add
            {
                this.ContextMenuStripChanged += value;
            }
            remove
            {
                this.ContextMenuStripChanged -= value;
            }
        }
    }
}

