using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.ICore.WinCore.WinUI.Controls
{

    [CoreRegistrableControl()]
    /// <summary>
    /// list box 
    /// </summary>
    public class IGKXListBox : ListBox, ICoreListBox
    {
        public IGKXListBox()
        {
        }
        public void AddItem(object vc)
        {
            this.Items.Add(vc);
        }
        public object GetItemAt(int index)
        {
            return this.Items[index];
        }

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
            System.Drawing.Point v_pt = this.PointToScreen(new System.Drawing.Point(pt.X, pt.Y));
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
                this.Size = new System.Drawing.Size(value.Width, value.Height);
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
                this.Location = new System.Drawing.Point(value.X, value.Y);
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
                this.Bounds = new System.Drawing.Rectangle(value.X, value.Y, value.Width, value.Height);
            }
        }
        void ICoreControl.Invalidate(Rectanglei rc)
        {
            this.Invalidate(new System.Drawing.Rectangle(rc.X,
                rc.Y,
                rc.Width,
                rc.Height));
        }
        Vector2i ICoreControl.PointToClient(Vector2i pt)
        {
            System.Drawing.Point rpt = PointToClient(new System.Drawing.Point(pt.X, pt.Y));
            return new Vector2i(rpt.X, rpt.Y);
        }
        object ICoreControl.BeginInvoke(Delegate coreMethodInvoker)
        {
            return this.BeginInvoke(coreMethodInvoker);
        }
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

        protected ICoreGraphics CreateGraphics(System.Drawing.Graphics g)
        {
            return CoreApplicationManager.Application.ResourcesManager.CreateDevice(g);
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
            if ((CoreApplicationManager.Application == null))
                return;
            try
            {
                CorePaintEventArgs c = new CorePaintEventArgs(CreateGraphics(e.Graphics));
                OnCorePaint(c);
            }
            catch
            {
            }
        }
        protected virtual void OnCorePaint(CorePaintEventArgs e)
        {
            if (this.Paint != null)
                this.Paint(this, e);
        }

        public virtual Vector2f GetFactorLocation(int x, int y)
        {
            return new Vector2f(x, y);
        }
        #region "Mouse Overrides"
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            OnCoreMouseClick(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            OnCoreMouseDoubleClick(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            OnCoreMouseMove(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            OnCoreMouseUp(new CoreMouseEventArgs((enuMouseButtons)e.Button,
                new Vector2i(e.X, e.Y),
                GetFactorLocation(e.X, e.Y),
                e.Delta,
                e.Clicks));
        }
        protected override void OnMouseDown(MouseEventArgs e)
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
