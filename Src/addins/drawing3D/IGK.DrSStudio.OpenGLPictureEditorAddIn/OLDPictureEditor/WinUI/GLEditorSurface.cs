

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLEditorSurface.cs
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
file:GLEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
using System.Drawing.Imaging;
using System.Windows.Forms;
namespace IGK.DrSStudio.GLPictureEditorAddIn.WinUI
{
    
using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.GLLib;
    using IGK.OGLGame.Graphics;
    using IGK.OGLGame;
    /// <summary>
    /// represent the GL Editor surface.
    /// Filemanager for saving,
    /// Undo effect because effect are cumulative
    /// </summary>
    public class GLEditorSurface : 
        IGKXWinCoreWorkingSurface ,
        ICoreWorkingSurface ,
        ICoreWorkingFilemanagerSurface ,
        ICoreWorkingUndoableSurface ,
        ICoreWorkingPrintingSurface 
    {
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save pictures",
                "Pictures | *.bmp; *.png; *.jpeg",
                this.FileName);
        }
        public class  GLEffectCollection : System.Collections.IEnumerable  
        {
            List<GLEffectBase> m_effects;
            GLEditorSurface m_surface;
            public GLEffectBase this [int index]
            {
                get { 
                    if ((index >=0) && (index < this.Count ))
                    return this.m_effects[index ];
                    return null;
                }
            }
            public int Count
            {
                get { return this.m_effects.Count ; }
            }
            public GLEffectCollection (GLEditorSurface owner)
	        {
                this.m_effects = new List<GLEffectBase>();
                this.m_surface = owner;
                this.m_surface.Disposed += new EventHandler(m_surface_Disposed);
	        }
            void m_surface_Disposed(object sender, EventArgs e)
            {
                foreach (var item in this.m_effects)
                {
                    item.Dispose();
                }
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_effects.GetEnumerator();
            }
            #endregion
            public void Remove(GLEffectBase c)
            {
                if (c == null) return;
                this.m_effects.Remove(c);
                this.m_surface.OnEffectRemoved(new EffectEventArgs(c));
            }
            public void Add(GLEffectBase c)
            {
                if ((c != null) && (this.m_effects.Contains(c)))
                    return;
                this.m_effects.Add(c);
                this.m_surface.OnEffectAdded(new EffectEventArgs(c));
            }
            internal void Clear()
            {
                this.m_effects.Clear();
            }
        }
        private bool m_needToSave;
        private GLEditorDocument m_Document;
        private GLEffectCollection m_Effects;
        private enuRenderMode m_RenderMode;
        /// <summary>
        /// get or set the current document
        /// </summary>
        public GLEditorDocument CurrentDocument
        {
            get { return m_Document; }
            set
            {
                if (m_Document != value)
                {
                    m_Document = value;
                }
            }
        }
        public enuRenderMode RenderMode
        {
            get { return m_RenderMode; }
            set
            {
                if (m_RenderMode != value)
                {
                    m_RenderMode = value;
                    OnRenderModeChagned(EventArgs.Empty);
                }
            }
        }
        private void OnRenderModeChagned(EventArgs eventArgs)
        {
            this.Render();
            if (this.RenderModeChanged != null)
                this.RenderModeChanged(this, eventArgs);
        }
        public GLEffectCollection Effects
        {
            get { return m_Effects; }
        }
        private IGK.OGLGame.Graphics.OGLGraphicsDevice  m_Device;
        public IGK.OGLGame.Graphics.OGLGraphicsDevice Device
        {
            get { return m_Device; }
        }
        private string m_FileName;
        private float m_ZoomX;
        private float m_ZoomY;
        private float m_PosX;
        private float m_PosY;
        private float m_BitmapWidth;
        private float m_BitmapHeight;
        public float BitmapHeight
        {
            get { return m_BitmapHeight; }
            set
            {
                if (m_BitmapHeight != value)
                {
                    m_BitmapHeight = value;
                }
            }
        }
        public float BitmapWidth
        {
            get { return m_BitmapWidth; }
        }
        public float PosY
        {
            get { return m_PosY; }
            set
            {
                if (m_PosY != value)
                {
                    m_PosY = value;
                }
            }
        }
        public float PosX
        {
            get { return m_PosX; }
        }
        public float ZoomY
        {
            get { return m_ZoomY; }
            set
            {
                if (m_ZoomY != value)
                {
                    m_ZoomY = value;
                }
            }
        }
        public float ZoomX
        {
            get { return m_ZoomX; }
            set
            {
                if (m_ZoomX != value)
                {
                    m_ZoomX = value;
                }
            }
        }
        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, EventArgs.Empty);
            }
        }
        public event EventHandler  FileNameChanged;
        public virtual  GLEditorDocument CreateNewDocument()
        {
            return new GLEditorDocument();
        }
        public GLEditorSurface()
        {
            //Single Buffering Strategy
            //this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, false);
            //this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, false );
            //this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            //this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            //Single Buffering Double Buffering Strategy
            this.SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, false ); //allow double bufferent fo opengl
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer, false ); //as true you must make a loop for rendering effor
            this.SetStyle(System.Windows.Forms.ControlStyles.ResizeRedraw, true);
            this.SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
            this.InitializeComponent();
            this.m_ZoomX = 1.0f;
            this.m_ZoomY = 1.0f;
            this.m_needToSave = false;
            this.m_RenderMode = enuRenderMode.Pixel ;
            this.m_Effects = new GLEffectCollection(this);
            this.m_Documents = CreateDocumentCollections();
            this.HandleCreated += new EventHandler(GLEditorSurface_HandleCreated);
            this.HandleDestroyed += new EventHandler(GLEditorSurface_HandleDestroyed);
            this.Paint += new System.Windows.Forms.PaintEventHandler(GLEditorSurface_Paint);
            this.SizeChanged += new EventHandler(GLEditorSurface_SizeChanged);
            this.FileNameChanged += new EventHandler(_FileNameChanged);
            this.RenderModeChanged += new EventHandler(_RenderModeChanged);
        }
        public event EventHandler RenderModeChanged;
        void _RenderModeChanged(Object sender, EventArgs e)
        {
            this.LoadBitmap();
        }
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Graphics.Clear(Colorf.CornflowerBlue);
            }
            else
            {
                if (this.m_Device == null)
                {
                    e.Graphics.Clear(Colorf.CornflowerBlue);
                    e.Graphics.DrawString("No OpenGL Device Created", this.Font, Brushes.White, Point.Empty);
                    base.OnPaint(e);
                }
                else {
                    this.Render();
                }
            }
        }
        protected override void OnPaintBackground(System.Windows.Forms.PaintEventArgs e)
        {
            //do nothing
        }
        void _FileNameChanged(object sender, EventArgs e)
        {
        }
        void GLEditorSurface_SizeChanged(object sender, EventArgs e)
        {
            this.InitView();
            this.Render();
        }
        void GLEditorSurface_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (this.Device != null)
            {
                this.Render();
            }
        }
        void GLEditorSurface_HandleDestroyed(object sender, EventArgs e)
        {
            this.DestroyDevice();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DestroyDevice();
            }
            base.Dispose(disposing);
        }
        private void DestroyDevice()
        {
            if (this.Device != null)
            {
                foreach (GLEditorDocument  doc in this.Documents )
                {
                    doc.FreeRessources(this.Device);
                }
                this.Device.Dispose();
                this.m_Device = null;
            }
        }
        void GLEditorSurface_HandleCreated(object sender, EventArgs e)
        {
            this.CreateDevice();
        }
        private void InitializeComponent()
        {
        }

        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        #endregion
        void CreateDevice()
        {
            //create device with double buffering
            //m_Device = IGK.OGLGame.Graphics.OGLGraphicsDevice.CreateDeviceFromHWND(
            //    this.Handle, 32, 32, OGLGame.enuGLCreationFlag.DoubleBuffer | OGLGame.enuGLCreationFlag.DrawToWindow | OGLGame.enuGLCreationFlag.SupportOpenGL,
            //     enuGLPixelMode.RGBA ,
            //      enuGLPlane.MainPlane );
            //no double buffering
            m_Device = OGLGraphicsDevice.CreateDeviceFromHWND(this.Handle,
                32, 32,
                 enuGLCreationFlag.DrawToWindow | enuGLCreationFlag.SupportOpenGL);
            if (m_Device == null)
                throw new Exception("Device Not Created");
        }

        public new Rectanglei ClientRectangle {
            get { 
                Rectangle rc = base.ClientRectangle;
                return new Rectanglei(rc.X, rc.Width, rc.Width, rc.Height);
            }
        }
        void InitView()
        {
            this.Device.Viewport = this.ClientRectangle;
            this.Device.Projection.MatrixMode = IGK.OGLGame.Graphics.MatrixMode.Projection;
            this.Device.Projection.LoadIdentity();
            this.Device.Projection.SetOrtho2D (0, this.Width, this.Height,0);
            this.Device.Projection.MatrixMode = IGK.OGLGame.Graphics.MatrixMode.ModelView;
            this.Device.Projection.LoadIdentity();
            ///calculate the corresponding zoom factor
            System.Drawing.Rectangle r = this.ClientRectangle ;
            if (!r.IsEmpty && (this.m_BitmapHeight != 0) && (this.m_BitmapWidth !=0))
            {
                m_PosX = r.X;
                m_PosY = r.Y;
                float zw = r.Width ;
                float zh = r.Height;
                this.m_ZoomX = zw / (float)this.BitmapWidth;
                this.m_ZoomY = zh / (float)this.BitmapHeight;
                this.m_ZoomX = Math.Min(this.m_ZoomX, this.m_ZoomY);
                this.m_ZoomY = this.m_ZoomX;
                m_PosX += (int)Math.Ceiling((zw - (this.BitmapWidth * m_ZoomX)) / 2.0f);
                m_PosY += (int)Math.Ceiling((zh - (this.BitmapHeight * m_ZoomY)) / 2.0f);
            }
        }
        uint m_BitmapList;
        protected internal uint BitmapList
        { 
            get{return this.m_BitmapList;}
            set { this.m_BitmapList = value; }
        }
        void LoadBitmap()
        {
            if (System.IO.File.Exists(this.FileName) == false)
                return;
            try
            {
             IGK.DrSStudio.Codec.ICoreCodec[] d=    IGK.DrSStudio.CoreSystem.Instance.Codecs.GetDecodersByExtension(System.IO.Path.GetExtension(this.FileName));
             System.Drawing.Bitmap bmp = null;
             if (d.Length == 1)
             {
                 bmp = (d[0] as IGK.DrSStudio.Codec.ICoreBitmapDecoder).GetBitmap(this.FileName);
             }
             else
                 bmp = System.Drawing.Bitmap.FromFile(this.FileName) as System.Drawing.Bitmap;
             if (BitmapList == 0)
                 BitmapList = Device.GenList(1);
             this.m_BitmapHeight = bmp.Height;
             this.m_BitmapWidth = bmp.Width;
             switch (this.RenderMode)
             {
                 case enuRenderMode.Pixel:
                     if (bmp != null)
                     {
                         Device.BeginNewList(BitmapList, IGK.OGLGame.Graphics.ListMode.Compile);
                         Device.DrawBitmap(bmp);
                         Device.EndList();
                     }
                     break;
                 case enuRenderMode.Texture :
                     IGK.OGLGame.Graphics.Texture2D text =  IGK.OGLGame.Graphics.Texture2D.Load(this.Device, bmp);
                     Device.BeginNewList(BitmapList, IGK.OGLGame.Graphics.ListMode.Compile);
                     text.Bind();
                     Device.Capabilities.Texture2D = true;
                     Device.Begin(enuGraphicsPrimitives.Quads);
                     Device.SetColor(Color.White);
                     Device.SetTexCoord(0, 0);
                     Device.SetVertex(0, 0);
                     Device.SetTexCoord(1, 0);
                     Device.SetVertex(BitmapWidth , 0);
                     Device.SetTexCoord(1, 1);
                     Device.SetVertex(BitmapWidth, BitmapHeight );
                     Device.SetTexCoord(0, 1);
                     Device.SetVertex(0, BitmapHeight);
                     Device.End();
                     Device.EndList();
                     break;
             }
            }
            catch(Exception )
            {
                BitmapList = 0;
            }
            this.InitView();
        }
        /// <summary>
        /// render the current 
        /// </summary>
        public void Render()
        {
            if (this.Device == null)
                return;
            lock (this.Device)
            {
                this.Device.MakeCurrent();
                SetupDevice();
                //clear the scene
                this.Device.SetClearDepth(0.0);
                this.Device.Clear(GLPictureEditorRenderer.GLPictureEditorSceneBackgroundColor);
                this.Device.PushAttrib(enuAttribBit.All);
                this.SetupDevice();
                this.BindEffect();
                this.RenderScene();
                this.UnBindEffect();
                this.Device.PopAttrib();
                //finish scene
                this.Device.Flush();
            }
        }
        private void SetupDevice()
        {
            this.Device.MakeCurrent();
            this.SetupBlending();            
            this.Device.RenderState.ZoomX = this.ZoomX;
            this.Device.RenderState.ZoomY = -this.ZoomY;
            this.Device.SetRasterPos(this.PosX, this.PosY);
        }
        private void BindEffect()
        {
            foreach (GLEffectBase item in this.Effects)
            {
                item.Bind(this.Device);
            }          
        }
        private void UnBindEffect()
        {
            foreach (GLEffectBase item in this.Effects)
            {
                item.UnBind(this.Device);
            }          
        }
        void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, e);
            }
        #region ICoreWorkingFilemanagerSurface Members
        public void RenameTo(string p)
        {
            this.m_FileName = p;
        }
        #endregion
        #region ICoreWorkingRecordableSurface Members
        public bool NeedToSave
        {
            get {
                return m_needToSave;
            }
            private set {
                if (this.m_needToSave != value)
                {
                    m_needToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NeedToSaveChanged;
        ///<summary>
        ///raise the NeedToSaveChanged 
        ///</summary>
        protected virtual void OnNeedToSaveChanged(EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
        }
        public void Save()
        {
            SaveAs(this.FileName );
        }
        public void SaveAs(string filename)
        {
                    IGK.DrSStudio.Codec.ICoreCodec[] codecs =
                        IGK.DrSStudio.Codec.CoreEncoderBase.GetEncodersByExtension(System.IO.Path.GetExtension(filename));
                    try
                    {
                        using (Bitmap bmp = this.SaveToBitmap())
                        {
                            if (codecs.Length == 1)
                            {
                                (codecs[0] as IGK.DrSStudio.Drawing2D.Codec.ICoreBitmapEncoder).SavePicture(
                                    filename, bmp);
                            }
                            else 
                                bmp.Save(filename );
                        }
                        this.NeedToSave = false;
                        this.FileName = filename ;
                    }
                    catch (Exception)
                    { 
                    }
        }
        public Bitmap SaveToBitmap()
        {
            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)this.BitmapWidth, (int)this.BitmapHeight);
            Rectanglei v_rc = new Rectanglei(0,0, bmp.Width, bmp.Height );
            BitmapData v_data = bmp.LockBits(new Rectangle(Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            float posx = this.PosX ;
            float posy =this.PosY ;
            float zoomx = this.ZoomX ;
            float zoomy = this.ZoomY ;
            this.Visible = false;
            this.Size = new Size(v_rc.Width, v_rc.Height);////set up de size
            this.Device.RenderState.ZoomX = 1;
            this.Device.RenderState.ZoomY = -1;
            this.Device.SetRasterPos(0, 0);            
            this.Device.PushAttrib( enuAttribBit.All  );
            this.SetupBlending();
            this.BindEffect();
            this.RenderScene();
            //this.Device.CallList(this.BitmapList);
            this.Device.PopAttrib();
            this.Device.ReadPixels(v_rc, GraphicsPixelColorMode.BGRA, GraphicsPixelColorType.UByte, v_data.Scan0);
          //  this.Device.EndScene();
            bmp.UnlockBits(v_data);
            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
            this.Visible = true;
            return bmp;
        }
        private void SetupBlending()
        {
            this.Device.Blending.Enabled = true;
            this.Device.Blending.BlendColor = Colorf.Red;
            this.Device.Blending.BlendSource = BlendFactor.One;
            this.Device.Blending.BlendDestination = BlendFactor.SourceColor;
        }
        private void RenderScene()
        {
            //draw background
            Device.DrawRectangle(Colorf.White, PosX - 1, PosY - 1, this.ZoomX * this.BitmapWidth + 2, this.ZoomY * this.BitmapHeight + 2);
            if (this.RenderMode == enuRenderMode.Texture)
            {
                Device.Projection.PushMatrix();
                Device.Projection.Translate(PosX, PosY, 0.0f);
                Device.Projection.Scale(ZoomX, ZoomY, 1.0f);
                if (BitmapList != 0)
                {
                   Device.CallList(BitmapList);
                }
                this.CurrentDocument.Render(Device);
                Device.Projection.PopMatrix();
            }
            else
            {
                if (BitmapList != 0)
                {
                   Device.CallList(BitmapList);
                }
                this.CurrentDocument.Render(Device);
            } 
        }
        #endregion
        #region ICoreWorkingUndoableSurface Members
        public bool CanRedo
        {
            get {
                return false;
            }
        }
        public bool CanUndo
        {
            get { return (this.Effects.Count > 0); }
        }
        public void Redo()
        {
        }
        public void Undo()
        {
            this.Effects.Remove(this.Effects[this.Effects.Count - 1]);
            this.Render();
        }
        #endregion
        #region ICoreWorkingPrintingSurface Members
        public void Print()
        {
            (new GLPrintingManager(this)).Print();
        }
        public void PrintPreview()
        {
            (new GLPrintingManager(this)).PrintPreview();
        }
        #endregion
        internal void Import(Bitmap bmp)
        {
            GLEditorBitmapElement v_l = new GLEditorBitmapElement();
            v_l.LoadBitmap(this.Device, bmp);
            this.CurrentDocument.CurrentLayer.Elements.Add(v_l);
        }
        private GLEditorDocumentCollections  m_Documents;
        public GLEditorDocumentCollections  Documents
        {
            get { return m_Documents; }
        }
        protected virtual GLEditorDocumentCollections CreateDocumentCollections()
        {
            return new GLEditorDocumentCollections(this);
        }
        public class GLEditorDocumentCollections : System.Collections.IEnumerable 
        {
            GLEditorSurface m_owner;
            List<GLEditorDocument> m_documents;
            internal protected GLEditorDocumentCollections(GLEditorSurface owner)
            {
                this.m_owner = owner;
                m_documents = new List<GLEditorDocument>();
                this.m_documents.Add(owner.CreateNewDocument());
                this.m_owner.m_Document = this.m_documents[0];
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_documents.GetEnumerator();
            }
            #endregion
        }
        internal void LoadFile(string filename)
        {
            this.FileName = filename;
            LoadBitmap();
        }
        public event EffectEventHandler EffectAdded;
        public event EffectEventHandler EffectRemoved;
        internal void OnEffectAdded(EffectEventArgs effectEventArgs)
        {
            if (this.EffectAdded != null)
                this.EffectAdded(this, effectEventArgs);
        }
        internal void OnEffectRemoved(EffectEventArgs effectEventArgs)
        {
            if (this.EffectRemoved != null)
                this.EffectRemoved(this, effectEventArgs);
        }

        public bool CanPrint
        {
            get { return true; }
        }
    }
}

