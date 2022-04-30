
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{

    using IGK.DrSStudio.Drawing3D.FileBrowser.AnimationModel;
    
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.OGLGame;
    using IGK.OGLGame.Graphics;
    using System.Collections;
    using System.Windows.Forms;

    public class FBDisplaySurface :  IGKXControl, IOGLGGraphicsView 
    {
        private FBControlSurface m_surfaceOwner;
        private OGLGraphicsDevice m_Device;
        private Timer m_timer;
        private SpriteBatch m_batch;
        private AnimationModelBase m_CurrentAnimModel;
        /// <summary>
        /// get the animation models collection
        /// </summary>
        public FBAnimModelCollection AnimationModels
        {
            get
            {
                if (this.m_AnimationModels == null)
                    this.m_AnimationModels = new FBAnimModelCollection(this);
                return m_AnimationModels;
            }
        }

        /// <summary>
        /// represent the animation model collection
        /// </summary>
        public class FBAnimModelCollection : IEnumerable
        {
            private List<AnimationModelBase> m_models;
            private FBDisplaySurface m_surfaceMainView;
            public FBAnimModelCollection(FBDisplaySurface surface)
            {
                m_surfaceMainView = surface;
                m_models = new List<AnimationModelBase>();
            }
            public AnimationModelBase this[int index]
            {
                get
                {
                    return this.m_models[index];
                }
            }
            /// <summary>
            /// get the count model
            /// </summary>
            public int Count
            {
                get { return this.m_models.Count; }
            }
            public void Add(AnimationModelBase anim)
            {
                if ((anim == null) || this.m_models.Contains(anim))
                {
                    return;
                }
                this.m_models.Add(anim);
                anim.Surface = this.m_surfaceMainView.m_surfaceOwner;
                anim.PropertyChanged += anim_PropertyChanged;
               // this.m_surfaceMainView.OnAnimModelAdded(new AnimModelEventArgs(anim));
            }

            void anim_PropertyChanged(object sender, EventArgs e)
            {
                AnimationModelBase c = sender as AnimationModelBase;
                if (c == this.m_surfaceMainView.CurrentAnimModel)
                {
                    this.m_surfaceMainView.Initialize();
                }
            }
            public void Remove(AnimationModelBase anim)
            {
                if ((anim == null) || !this.m_models.Contains(anim))
                {
                    return;
                }
                this.m_models.Remove(anim);
                anim.PropertyChanged -= anim_PropertyChanged;
                anim.Surface = null;
               // this.m_surfaceMainView.OnAnimModelRemoved(new AnimModelEventArgs(anim));
            }
            #region IEnumerable Members
            public IEnumerator GetEnumerator()
            {
                return this.m_models.GetEnumerator();
            }
            #endregion
        }
        public AnimationModelBase CurrentAnimModel
        {
            get
            {
                return m_CurrentAnimModel;
            }
            set
            {
                if (m_CurrentAnimModel != value)
                {
                    if (this.m_CurrentAnimModel != null) m_CurrentAnimModel.Dispose();
                    m_CurrentAnimModel = value;
                    if (this.m_CurrentAnimModel != null) InitModel();
                    OnAnimationModelChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler AnimationModelChanged;
        private SpriteFont m_font;
        private FBAnimModelCollection m_AnimationModels;
        ///<summary>
        ///raise the AnimationModelChanged 
        ///</summary>
        protected virtual void OnAnimationModelChanged(EventArgs e)
        {
            if (AnimationModelChanged != null)
                AnimationModelChanged(this, e);
        }

        protected virtual void InitModel()
        {
            if (this.CurrentAnimModel is I3DModel)
            {
                (this.CurrentAnimModel as I3DModel).Initialize(this.Device, 
                    new  Rectanglei (this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height ));
            } 
        }
        public FBDisplaySurface(FBControlSurface owner)
        {
            this.m_surfaceOwner = owner;


            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.SetStyle(ControlStyles.UserPaint, false);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, false);

            //this.SetStyle(ControlStyles.UserPaint, true);
            //this.SetStyle(ControlStyles.ResizeRedraw, true);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.UserPaint, true);
            this.m_timer = new Timer();
            this.m_timer.Tick += this.m_timer_Tick;
            this.m_timer.Interval = 20;
            this.SizeChanged +=  _SizeChanged;
            this.m_surfaceOwner.SelectedFileIndexChanged += m_ownerSurface_SelectedFileIndexChanged;
            this.m_surfaceOwner.SelectedFolderChanged += m_ownerSurface_SelectedFolderChanged;

        }
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (!this.Focused)
                this.Focus();
        }

        private void m_ownerSurface_SelectedFolderChanged(object sender, EventArgs e)
        {
            if (this.CurrentAnimModel != null)
            {
                this.CurrentAnimModel.OrganiseTextures(this.Device);
            }
        }

        private void m_ownerSurface_SelectedFileIndexChanged(object sender, EventArgs e)
        {
            if (this.CurrentAnimModel != null)
            {
                this.CurrentAnimModel.OrganiseTextures(this.Device);
            }
        }

        protected virtual bool RequireDoubleBuffer {
            get {
                return true;
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
        }
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            //no background painting
            //base.OnPaintBackground(pevent);
        }
        protected override void OnResize(EventArgs e)
        {
            if (this.Device != null)
            {
                this.Device.MakeCurrent();
                this.InitView();
                this.Render();
            }
            base.OnResize(e);            
        }
        void _SizeChanged(object sender, EventArgs e)
        {
            if (this.Device != null)
            {
                this.Device.MakeCurrent();
                this.InitView();
            }
        }
        protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
        {
            switch (keyData)
            { 
                case Keys.Left :
                case Keys.Right :
                case Keys.Up :
                case Keys.Down :
                    return true;
            }
            return base.IsInputKey(keyData);
        }
        protected virtual void InitView()
        {
            this.Device.Viewport =
                new Rectanglei(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.m_Device != null)
                {
                    m_Device.Dispose();
                    m_Device = null;
                }
                if (this.m_timer != null)
                {
                    this.m_timer.Dispose();
                    this.m_timer = null;
                }
            }
            base.Dispose(disposing);
        }
        public bool Animated
        {
            get {
                if (this.m_timer !=null)
                return this.m_timer.Enabled;
                return false;
            }
            set {
                if (this.m_timer != null)
                {
                    this.m_timer.Enabled = value;
                }
            }
        }
        public OGLGraphicsDevice Device
        {
            get { return m_Device; }
        }
        public virtual void Render()
        {
            if (Device == null)
                return;
            //Device.MakeCurrent();
            //Device.Clear(enuBufferBit.Depth, Colorf.CornflowerBlue);
            //Device.EndScene();

            if (this.Device == null) return;
            this.Device.MakeCurrent();
            this.Device.Clear(enuBufferBit.Accum | enuBufferBit.Depth,
                //Colorf.CornflowerBlue 
                this.m_surfaceOwner.BackgroundColor
                );

  
            if (this.m_CurrentAnimModel !=null)
            this.CurrentAnimModel.Render(this.Device);
            m_batch.Begin();
            if (this.m_surfaceOwner.ShowText)
            {
                string f = this.m_surfaceOwner.Files[this.m_surfaceOwner.SelectedFileIndex];
                if (!string.IsNullOrEmpty(f))
                {
                    m_batch.DrawString(m_font, f, new Vector2f(1, 1), Colorf.FromFloat(0.3f));
                    m_batch.DrawString(m_font, f, Vector2f.Zero, Colorf.White);
                }
            }
            m_batch.End();
            Device.EndScene();
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            m_Device = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle,
                 32, 24,
                 ((this.RequireDoubleBuffer ) ? enuGLCreationFlag.DoubleBuffer :  enuGLCreationFlag .None)
            | enuGLCreationFlag.DrawToWindow | enuGLCreationFlag.SupportOpenGL,
                   IGK.GLLib.enuGLPixelMode.RGBA,
                    IGK.GLLib.enuGLPlane.MainPlane);
            if (m_Device == null)
            {
                throw new InvalidOperationException("Can't create a gl device");
            }
            this.InitDevice();
            this.m_timer = new Timer();
            this.m_timer.Interval = 1;//20
            this.m_timer.Enabled = IsAnimated ;
            this.m_timer.Tick += new EventHandler(m_timer_Tick);
        }
        protected virtual bool IsAnimated{
            get {
                return true;
            }
    }
        protected virtual void InitDevice()
        {
            m_Device.MakeCurrent();
            m_batch = new SpriteBatch(this.m_Device, this);
            //this.InitFont();
            //init fonts
            m_font = SpriteFont.Create(this.m_Device, 
                "Arial", 12.0f,
                enuFontStyle.Regular, 5, 12);

            if (this.m_CurrentAnimModel == null)
            {
                if (this.AnimationModels.Count == 0)
                {
                    AnimationModelBase m = null;
                    foreach (Type t in AnimationModelBase.GetModels())
                    {
                        m = t.Assembly.CreateInstance(t.FullName) as AnimationModelBase;
                        if (m != null)
                            this.AnimationModels.Add(m);
                    }
                    this.CurrentAnimModel = this.AnimationModels[0];
                }
            }
        }
        void m_timer_Tick(object sender, EventArgs e)
        {
           this.Render();
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
        }
        public virtual Rectanglei GetViewPort()
        {
            return new Rectanglei(this.ClientRectangle.X, this.ClientRectangle.Y, this.ClientRectangle.Width, this.ClientRectangle.Height);
        }

        public virtual bool CanGoLeft
        {
            get
            {
                return this.CurrentAnimModel.CanGoLeft;
            }
        }
        public virtual bool CanGoRight
        {
            get
            {
                return this.CurrentAnimModel.CanGoRight;
            }
        }
        public bool CanGoUp
        {
            get
            {
                return this.CurrentAnimModel.CanGoUp;
            }
        }
        public bool CanGoDown
        {
            get
            {
                return this.CurrentAnimModel.CanGoDown;
            }
        }
        public virtual void GoLeft()
        {
            this.CurrentAnimModel.GoLeft();
        }
        public void GoRight()
        {
            this.CurrentAnimModel.GoRight();
        }
        public void GoUp()
        {
            this.CurrentAnimModel.GoUp();
        }
        public void GoDown()
        {
            this.CurrentAnimModel.GoDown();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
                GoUp();
            else
                GoDown();
        }

        internal void Initialize()
        {
            this.CurrentAnimModel.Initialize(this.Device,
                this.GetViewPort());
        }

        public bool EnabledRendering { get {
            return this.m_timer.Enabled;
        }
            set {
                this.m_timer.Enabled = value;
            }
        }
    }
}
