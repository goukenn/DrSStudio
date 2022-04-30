

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLControlBase.cs
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
file:GLControlBase.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel ;
namespace IGK.OGLGame.WinUI.GLControls
{
    
    using IGK.ICore;
    using IGK.OGLGame.Graphics;
    using System.Drawing;
    using IGK.OGLGame.Native;
    /// <summary>
    /// represent the base class of the graphics controls
    /// </summary>
    public class GLControlBase : System.Windows.Forms.Control , IGLControl 
    {
        private IGK.OGLGame.Graphics.OGLGraphicsDevice m_Device;
        private Timer m_timer;
        private Colorf m_SceneBackgroundColor;
        private GLControlItemCollection m_controls;
        private GLControlTime m_controlTime;
        private bool m_AutoRefresh;
        private int m_autoRefreshRate;
        private GLControlItemBase m_currentControl;
        private enuBufferBit m_ClearBit;
        private IGK.OGLGame.enuGLCreationFlag m_CreationFlag;
        [Category ("OGLSetting")]
        /// <summary>
        /// get or set the Creation Flag
        /// </summary>
        public IGK.OGLGame.enuGLCreationFlag CreationFlag
        {
            get { return m_CreationFlag; }
            set
            {
                if (m_CreationFlag != value)
                {
                    m_CreationFlag = value;
                }
            }
        }
        [Category("OGLSetting")]
        public enuBufferBit  ClearBit
        {
            get { return m_ClearBit; }
            set
            {
                if (m_ClearBit != value)
                {
                    m_ClearBit = value;
                }
            }
        }
        public event EventHandler<GLControlEventArgs> ItemAdded;
        public event EventHandler<GLControlEventArgs> ItemRemoved;
        public event EventHandler InitializeDevice;
        public event EventHandler LoadDeviceContent;
        public event EventHandler UnloadDeviceContent;
        [Category(GLControlConstant.PROP_CATEGORY)]
        [DefaultValue (false)]
        /// <summary>
        /// get or set if auto refreshing
        /// </summary>
        public bool AutoRefresh
        {
            get { return m_AutoRefresh; }
            set
            {
                if (m_AutoRefresh != value)
                {
                    m_AutoRefresh = value;
                    OnAutoRefreshChanged(EventArgs.Empty);
                }
            }
        }
        [Category(GLControlConstant.PROP_CATEGORY)]
        [DefaultValue(20)]      
        public int  AutoRefreshRate
        {
            get {
                return this.m_autoRefreshRate;
            }
            set {
                this.m_autoRefreshRate = value;
                OnAutoRefreshRateChanged(EventArgs.Empty);
            }
        }
        private void OnAutoRefreshRateChanged(EventArgs eventArgs)
        {
            this.m_timer.Interval = AutoRefreshRate;
        }
        private void OnAutoRefreshChanged(EventArgs eventArgs)
        {
            if (!this.DesignMode)
                this.m_timer.Enabled = this.m_AutoRefresh;
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            this.Invalidate();
        }
        [Category (GLControlConstant .PROP_CATEGORY)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible )]
        [DefaultValue("CornflowerBlue")]        
        public Colorf SceneBackgroundColor
        {
            get
            {                
                return m_SceneBackgroundColor;
            }
            set
            {
                if (!this.m_SceneBackgroundColor.Equals(value))
                {
                    this.m_SceneBackgroundColor = value;
                    this.Invalidate();
                }
            }
        }
     
        [Category(GLControlConstant.PROP_CATEGORY)]
        [Browsable (false )]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden )]
        public IGK.OGLGame.Graphics.OGLGraphicsDevice  GraphicsDevice
        {
            get { return m_Device; }
            protected set
            {
                if (m_Device != value)
                {
                    m_Device = value;
                }
            }
        }
        [DesignerSerializationVisibility (DesignerSerializationVisibility .Hidden)]
        [Browsable (false )]
        public override System.Drawing.Color BackColor
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
        public GLControlBase()
        {
            //this.SetStyle(System.Windows.Forms.ControlStyles.SupportsTransparentBackColor, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint , true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, true);
            //this.BackColor = System.Drawing.Color.Transparent;
            this.m_ClearBit = enuBufferBit.Color;
            this.m_CreationFlag = IGK.OGLGame.enuGLCreationFlag.DrawToWindow |
                    IGK.OGLGame.enuGLCreationFlag.SupportOpenGL |
                    IGK.OGLGame.enuGLCreationFlag.DoubleBuffer;
            this.m_SceneBackgroundColor = Colorf.CornflowerBlue;
            this.HandleCreated += new EventHandler(_HandleCreated);
            this.HandleDestroyed += new EventHandler(GLControlBase_HandleDestroyed);
            this.m_autoRefreshRate = 20;
            this.m_AutoRefresh = false;
            this.SizeChanged += new EventHandler(_SizeChanged);
            this.m_timer = new Timer();
            this.m_timer.Tick += new EventHandler(m_timer_Tick);
            this.m_timer.Interval = this.m_autoRefreshRate;
            this.m_timer.Enabled =false;
            this.m_controls = CreateItemCollections();
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            if (this.GraphicsDevice != null)
            {
                Rectangle rc = this.ClientRectangle;
                this.GraphicsDevice.Viewport =
                    new Rectanglei(rc.X, rc.Y, rc.Width, rc.Height);
                this.Invalidate();
            }
        }
        void m_timer_Tick(object sender, EventArgs e)
        {
            if (this.Visible && !DesignMode )
            {
                this.Render();
            }
        }
        void GLControlBase_HandleDestroyed(object sender, EventArgs e)
        {
            if (this.m_Device != null)
            {
                this.DisposeDevice();
            }
        }
        private void DisposeDevice()
        {
            this.UnloadContent();
            this.m_Device.Dispose();
            this.m_Device = null;
        }
        void _HandleCreated(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                if (this.m_Device != null)
                {
                    this.m_Device.Dispose();
                }
                this.m_Device = IGK.OGLGame.Graphics.OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle, 32,24, 
                    this.CreationFlag );
                if (this.m_Device != null)
                {
                    this.Initialize();
                    this.LoadContent();
                }
            }
        }
        protected internal  virtual void Initialize()
        {
            this.m_controlTime = GLControlTime.NewTimeControl();            
            this.m_timer.Enabled = this.m_AutoRefresh;
            this.OnInitializeDevice(EventArgs .Empty );
        }
        protected virtual void OnInitializeDevice(EventArgs eventArgs)
        {
            if (this.InitializeDevice!=null)
            {
                this.InitializeDevice(this, eventArgs); 
            }
        }
        protected internal virtual void LoadContent()
        {
            OnLoadDeviceContent(EventArgs.Empty);
        }
        protected virtual void OnLoadDeviceContent(EventArgs eventArgs)
        {
            if (this.LoadDeviceContent != null)
                this.LoadDeviceContent(this, eventArgs);
        }
        protected virtual void OnUnLoadContent(EventArgs eventArgs)
        {
            if (this.UnloadDeviceContent  != null)
                this.UnloadDeviceContent(this, eventArgs);
        }
        protected internal virtual void UnloadContent()
        {
            OnUnLoadContent(EventArgs.Empty);
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs pevent)
        {
            //base.OnPaintBackground(pevent);
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (!DesignMode)
            {
                this.Render();
            }
            else {
                this.DrawWithGDIPlus(e);
            }
        }
        protected virtual void DrawWithGDIPlus(PaintEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.FromArgb (this.SceneBackgroundColor.IntValue()));
        }
        /// <summary>
        /// render the items on surface
        /// </summary>
        public void Render()
        {
            if ((this.GraphicsDevice == null)||(!this.Visible ))
                return;
            if (!this.GraphicsDevice.IsCurrent)
                this.GraphicsDevice.MakeCurrent();
#if DEBUG
            System.Diagnostics.Debug.WriteLine("Call::Render ");
#endif
            GraphicsDevice.Clear(ClearBit, this.SceneBackgroundColor);
            this.m_controlTime.Tick();
            this.Render(this.GraphicsDevice, this.m_controlTime);
            GraphicsDevice.EndScene();
            this.AddItionalGdiRendering();
        }
        protected virtual void AddItionalGdiRendering()
        {
            //override this to render gdi compoenent
        }
        protected virtual void Render(IGK.OGLGame.Graphics.OGLGraphicsDevice device, IGK.OGLGame.WinUI.GLControls.IGLControlTime Time)
        {
            foreach (GLControlItemBase item in this.m_controls)
            {
                if (item.Visible )
                item.Render(this.m_controlTime);
            }
        }
        protected virtual GLControlItemCollection CreateItemCollections()
        {
            return new GLControlItemCollection(this);
        }
        protected override void WndProc(ref Message m)
        {
            if (this.Visible)
            {
                switch (m.Msg)
                {
                    case User32Lib.WM_CREATE:
                        break;
                    case User32Lib.WM_ERASEBKGND:
                        return;
                    case User32Lib.WM_PAINT:
                        this.Render();
                        break;
                    case User32Lib.WM_MOVE:
                    case User32Lib.WM_SIZE:
                        this.OnMouseMove(new Vector2i(-1, -1));
                        break;
                    case User32Lib.WM_MOUSEMOVE:
                        Vector2i mousepos = GLUtils.GetMousePosition(m.LParam);
                        this.OnMouseMove(mousepos);
                        break;
                }
            }
#if DEBUG
            else {
                System.Diagnostics.Debug.WriteLine("Not Visible");
            }
#endif
            base.WndProc(ref m);
        }
        private void OnMouseMove(Vector2i vector2i)
        {
            GLControlItemBase v_cControl = GetControlAt(vector2i);
            if (v_cControl == m_currentControl)
                return;
            if (this.m_currentControl != null)
                this.m_currentControl.OnMouseLeave();
            this.m_currentControl = v_cControl;
            if (this.m_currentControl != null)
                this.m_currentControl.OnMouseHover();
        }
        private GLControlItemBase  GetControlAt(Vector2i vector2i)
        {
            for (int i = this.GLControls.Count - 1; i >= 0; i--)
            {
                if (this.GLControls[i].Contains(vector2i))
                {
                    return this.GLControls[i];
                }
            }
            return null;
        }
        public class GLControlItemCollection : IGLControlItemCollections
        {
            GLControlBase m_owner;
            List<IGK.OGLGame.WinUI.GLControls.GLControlItemBase> m_controls;
            bool m_isLoadContent;
            public IGK.OGLGame.WinUI.GLControls.GLControlItemBase this[int index]{
                get{
                    return m_controls[index];
                }
            }
            protected internal  GLControlItemCollection(GLControlBase control)
            {
                this.m_owner = control;
                this.m_isLoadContent = (this.m_owner.GraphicsDevice != null);
                this.m_controls = new List<GLControlItemBase>();
                this.m_owner.HandleCreated += new EventHandler(m_owner_HandleCreated);
                this.m_owner.HandleDestroyed += new EventHandler(m_owner_HandleDestroyed);
            }
            void m_owner_HandleDestroyed(object sender, EventArgs e)
            {
            }
            void m_owner_HandleCreated(object sender, EventArgs e)
            {
                if (!m_isLoadContent)
                {
                    foreach (var item in this.m_controls)
                    {
                        item.LoadContent();
                    }
                    m_isLoadContent = false;
                }
            }
            public void Remove(GLControlItemBase control)
            {
                if (!this.m_controls.Contains(control))
                {
                    control.UnloadContent();                    
                    this.m_controls.Remove(control);
                    this.m_owner.OnItemsRemoved(new GLControlEventArgs(control));
                }
            }
            public void Add(GLControlItemBase control)
            {
                if ((control == null) || this.m_controls.Contains(control))
                    return;
                control.Parent  = this.m_owner;
                control.m_id = this.Count;
                control.Initialize();
                if (this.m_owner.GraphicsDevice != null)
                {
                    control.LoadContent();
                }
                this.m_controls.Add(control);
                this.m_owner.OnItemsAdded(new GLControlEventArgs(control ));
            }
            public GLControlItemBase GetItemByName(string name)
            {
                for (int i = 0; i < Count; i++)
                {
                    if (this[i].Name == name)
                        return this[i];
                }
                return null;
            }
            #region IGLControlItemCollections Members
            public int Count
            {
                get { return this.m_controls.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_controls.GetEnumerator();
            }
            #endregion
            #region IGLControlItemCollections Members
            void IGLControlItemCollections.Add(IGLControlItem item)
            {
                this.Add(item as GLControlItemBase);
            }
            void IGLControlItemCollections.Remove(IGLControlItem item)
            {
                this.Remove(item as GLControlItemBase);
            }
            IGLControlItem IGLControlItemCollections.this[int index]
            {
                get { return this[index]; }
            }
            IGLControlItem IGLControlItemCollections.GetItemByName(string name)
            {
                return GetItemByName(name );
            }
            #endregion
        }
        [Browsable (false )]
        [DesignerSerializationVisibility (DesignerSerializationVisibility.Hidden)]
        public GLControlItemCollection  GLControls
        {
            get
            {
                return m_controls;
            }
        }
        #region IGLControl Members
        IGLControlItemCollections IGLControl.GLControls
        {
            get { return this.m_controls; }
        }
        #endregion
        internal protected void OnItemsAdded(GLControlEventArgs  eventArgs)
        {
            if (this.ItemAdded != null)
                this.ItemAdded(this, eventArgs);
        }
        internal protected void OnItemsRemoved(GLControlEventArgs eventArgs)
        {
            if (this.ItemRemoved != null)
                this.ItemRemoved (this, eventArgs);
        }
        #region IGLControl Members
        public virtual Rectanglei GetViewPort()
        {
            Rectangle rc = this.ClientRectangle;                
            return  new Rectanglei(rc.X, rc.Y, rc.Width, rc.Height);
        }
        #endregion
    }
}

