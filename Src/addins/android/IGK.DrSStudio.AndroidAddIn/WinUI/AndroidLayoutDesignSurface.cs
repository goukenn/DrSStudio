

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidLayoutDesignSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.WinUI
{
    
using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    using System.Collections;
    using System.IO;
    using System.Drawing;
    using IGK.ICore.Resources;
    using System.Diagnostics;


    using IGK.ICore.WinCore;

    [CoreSurface("Android.DesignSurface", EnvironmentName="AndroidSurfaceEnvironment")]
    public class AndroidLayoutDesignSurface :
        AndroidSurfaceBase ,
        ICoreWorkingFilemanagerSurface, 
        ICoreWorkingToolManagerSurface //to manage design tool
    {
        AndroidLayoutDocument m_layoutDocument;
        
        public AndroidLayoutDocument LayoutDocument
        {
            get {
                return this.m_layoutDocument;
            }
        }
        public override Vector2f GetFactorLocation(int x, int y)
        {
            return CoreMathOperation.GetFactorLocation(x, y, this.PosX, this.PosY, ZoomX, ZoomY);  
        }
        public Rectanglef GetScreenBound(Rectanglef rc)
        {
            return CoreMathOperation.GetScreenBounds(rc, this.PosX, this.PosY, ZoomX, ZoomY);
        }
        public void LoadDocument(string nodeContent)
        {
            this.m_layoutDocument.LoadDocument(nodeContent);
        }
        public void LoadFile(string filename)
        {
            this.m_layoutDocument.LoadFile(filename);
            this.FileName = filename;

        }
        private float m_PosX;
        private float m_PosY;
        private float m_ZoomX;
        private float m_ZoomY;
        private enuZoomMode m_ZoomMode;

        public enuZoomMode ZoomMode
        {
            get { return m_ZoomMode; }
            set
            {
                if (m_ZoomMode != value)
                {
                    m_ZoomMode = value;
                }
            }
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
            set
            {
                if (m_PosX != value)
                {
                    m_PosX = value;
                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            this.initViewMode();
            base.OnSizeChanged(e);
            this.Invalidate();
        }
        private void initViewMode()
        {
            if (this.m_androidDocumentFrame == null)
                return;
            float v_oldzoomx = this.m_ZoomX;
            float v_oldzoomy = this.m_ZoomY;
            float v_posx = 0.0f;
            float v_posy = 0.0f;
            float v_zoomx = 1.0f;
            float v_zoomy = 1.0f;
            Rectanglef v_doc = this.m_androidDocumentFrame.Bounds;
            Rectanglei v_rc = this.DisplayArea;
            v_posx = v_rc.X;
            v_posy = v_rc.Y;
            switch (this.ZoomMode)
            {
                case enuZoomMode.Normal:
                    if (v_oldzoomx != 0)
                        v_zoomx = v_oldzoomx;
                    if (v_oldzoomy != 0)
                        v_zoomy = v_oldzoomy;
                    break;
                case enuZoomMode.NormalCenter:
                    if (v_oldzoomx != 0)
                        v_zoomx = v_oldzoomx;
                    if (v_oldzoomy != 0)
                        v_zoomy = v_oldzoomy;

                    v_posx = v_rc.X + ((v_rc.Width - v_doc.Width * v_zoomx) / 2.0f);
                    v_posy = v_rc.Y + ((v_rc.Height - v_doc.Height * v_zoomy) / 2.0f);

                    break;
                case enuZoomMode.Stretch:
                    v_zoomx = ((float)v_rc.Width / (float)v_doc.Width);
                    v_zoomy = ((float)v_rc.Height / (float)v_doc.Height);
                    break;
                case enuZoomMode.ZoomCenter:
                    v_zoomx = ((float)v_rc.Width / (float)v_doc.Width);
                    v_zoomy = ((float)v_rc.Height / (float)v_doc.Height);
                    v_zoomx = Math.Min(v_zoomx, v_zoomy);
                    v_zoomy = v_zoomx;
                    v_posx = v_rc.X + ((v_rc.Width - v_doc.Width * v_zoomx) / 2.0f);
                    v_posy = v_rc.Y + ((v_rc.Height - v_doc.Height * v_zoomy) / 2.0f);
                    break;
                case enuZoomMode.ZoomNormal:
                    v_zoomx = ((float)v_rc.Width / (float)v_doc.Width);
                    v_zoomy = ((float)v_rc.Height / (float)v_doc.Height);
                    v_zoomx = Math.Min(v_zoomx, v_zoomy);
                    v_zoomy = v_zoomx;
                    if (v_oldzoomx != 0)
                        v_zoomx = v_oldzoomx;
                    if (v_oldzoomy != 0)
                        v_zoomy = v_oldzoomy;
                    break;
                default:
                    break;
            }
            this.ZoomX = v_zoomx;
            this.ZoomY = v_zoomy;
            this.PosX = v_posx;
            this.PosY = v_posy;
            this.Invalidate();
        }
        public AndroidLayoutDesignSurface()
        {
            this.m_layoutDocument = new AndroidLayoutDocument();
            this.m_Frames = new AndroidFrameCollections(this);
            this.InitializeComponent();
            this.Paint += _Paint;
            this.m_layoutDocument.PropertyChanged += _layoutDocument_PropertyChanged;
            this.FileNameChanged += _FileNameChanged;
            this.FileName = "androidlayout.xml";
            this.CurrentTool = DefaultTool;
            this.m_androidMecanismFrame = new AndroidMecanismFrame(this);
            this.m_androidDocumentFrame = new AndroidDocumentFrame(this);
            this.m_ZoomMode = enuZoomMode.ZoomCenter;
            this.m_Frames.Add(this.m_androidDocumentFrame);
            this.m_Frames.Add(this.m_androidMecanismFrame);
            _FileNameChanged(this, EventArgs.Empty);
            this.initViewMode();
        }

        void _FileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.FileName);
        }

        void _layoutDocument_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            this.NeedToSave = true;
        }

        void _Paint(object sender, CorePaintEventArgs e)
        {
            foreach (ICore2DDrawingFrameRenderer item in this.m_Frames)
            {
                item.Render(e.Graphics);
            }
        }
        private void InitializeComponent()
        { 
            this.SuspendLayout();
            // 
            // AndroidLayoutDesignSurface
            // 
            this.Name = "AndroidLayoutDesignSurface";
            this.Size = new System.Drawing.Size(561, 304);
            this.ResumeLayout(false);
        }

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //base.OnPaintBackground(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
         
        }
        private string m_FileName;

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

        public event EventHandler FileNameChanged;
        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }


        public void ReloadFileFromDisk()
        {
            
        }

        public void RenameTo(string name)
        {
            
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.android.savelayoutas".R(), "android layout|*.xml", this.FileName);
        }
        private bool m_NeedToSave;

        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            set
            {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
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
            
            if (File.Exists(this.FileName))
            {
                this.SaveAs(this.FileName);
            }
            else {
                ICoreWorkbench v_bench = CoreSystem.GetWorkbench();
                if (v_bench !=null)
                    v_bench.CallAction(CoreConstant.MENUI_SAVEAS);
            }
        }

        public void SaveAs(string filename)
        {
            this.m_saving = true;
            this.m_layoutDocument.Save(filename);
            OnSaved(EventArgs.Empty);
            this.NeedToSave = false;
            this.m_saving = false;
        }

        private bool m_saving;
        private AndroidFrameCollections m_Frames;

        public event EventHandler Saved;
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }

        public bool Saving
        {
            get { return this.m_saving; }
        }


        public class AndroidFrameCollections : IEnumerable
        {
            List<ICore2DDrawingFrameRenderer> m_frames;
            private AndroidLayoutDesignSurface iGKD2DDrawingScene;
            private AndroidFrameCollections()
            {
                this.m_frames = new List<ICore2DDrawingFrameRenderer>();
            }
            public AndroidFrameCollections(AndroidLayoutDesignSurface iGKD2DDrawingScene)
                : this()
            {
                this.iGKD2DDrawingScene = iGKD2DDrawingScene;
            }
            public int Count { get { return this.m_frames.Count; } }
            public void Add(ICore2DDrawingFrameRenderer frame)
            {
                if ((frame == null) || this.m_frames.Contains(frame))
                    return;
                this.m_frames.Add(frame);
            }
            public void Remove(ICore2DDrawingFrameRenderer frame)
            {
                this.m_frames.Remove(frame);
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_frames.GetEnumerator();
            }
        }
        public abstract class AndroidSceneFrame : ICore2DDrawingFrameRenderer
        {
            private AndroidLayoutDesignSurface m_scene;
            /// <summary>
            /// get the scene
            /// </summary>
            public AndroidLayoutDesignSurface Scene
            {
                get { return this.m_scene; }
            }
            public AndroidSceneFrame(AndroidLayoutDesignSurface scene)
            {
                this.m_scene = scene;
            }
            public abstract void Render(ICoreGraphics device);
        }
      
        /// <summary>
        /// android mecanism frame rendererd
        /// </summary>
        internal class AndroidMecanismFrame : AndroidSceneFrame
        {
            public AndroidMecanismFrame(AndroidLayoutDesignSurface scene)
                : base(scene)
            {
            }
            public override void Render(ICoreGraphics device)
            {
                ICore2DDrawingFrameRenderer m = this.Scene.Mecanism as ICore2DDrawingFrameRenderer;
                if (m != null)
                {
                    m.Render(device);
                }
            }
        }
        /// <summary>
        /// android frame renderer
        /// </summary>
        internal class AndroidDocumentFrame : AndroidSceneFrame
        {
            private ImageElement m_img;
            private ICore2DDrawingDocument m_androiddevice;
            private ViewBoxElement m_viewBox;
            private RectangleElement m_background;

            public Rectanglef  Bounds
            {
                get { 
                    
                    return m_androiddevice.Bounds; 
                }
              
            }

            public AndroidDocumentFrame(AndroidLayoutDesignSurface scene) :base(scene)
            {
                m_img = ImageElement.CreateFromFile(@"D:\Pictures\Android\un001.jpg");
                m_androiddevice = CoreResources.GetDocument(AndroidConstant.ANDROID_IMG_DEVICE_KITKAT);
                Debug.Assert(m_androiddevice != null, "androiddevice.not.found".R());

                if (m_androiddevice == null)
                    throw new Exception("Application Error");
                this.m_viewBox = m_androiddevice.GetElementById("viewbox") as ViewBoxElement;
                m_androiddevice.BackgroundTransparent = true;
                this.m_background = m_androiddevice.GetElementById("background") as RectangleElement;
                if (this.m_background != null) 
                this.m_background.FillBrush.SetSolidColor(Colorf.FromFloat (0.6f, Colorf.CornflowerBlue));
                this.m_viewBox.Elements.Add(this.Scene.LayoutDocument.View);


                this.Scene.LayoutDocument.DeviceWidth = this.m_androiddevice.Width;
                this.Scene.LayoutDocument.DeviceHeight = this.m_androiddevice.Height;
                this.Scene.LayoutDocument.View.Dock(enuCore2DDockElement.DockFill);
                this.Scene.LayoutDocument.View.ResetTransform();


            }
            public override void Render(ICoreGraphics device)
            {
                //setup graphics
                Rectangle rc = Scene.DisplayRectangle ;
                device.Clear(Colorf.CornflowerBlue);
             

                //render background android image
               // ImageElement img = ImageElement.CreateFromFile(@"D:\Pictures\Android\un001.jpg");
                object s = device.Save();
   
                float ex = rc.Width / (float)m_img.Width;
                float ey = ex;
                device.ScaleTransform(ex, ey, enuMatrixOrder.Append);
                device.TranslateTransform(0,( rc.Height - (m_img.Height*ey)) / 2.0f, enuMatrixOrder.Append);
                device.Visit(m_img);


                device.Restore(s);

                device.FillRectangle(
             WinCoreBrushRegister.GetBrush(enuHatchStyle.Percent05, Colorf.FromFloat(0.5f), Colorf.Transparent),
             rc.X, rc.Y, rc.Width, rc.Height);
                


                if (this.m_androiddevice != null)
                {
                    s = device.Save();
                    device.ScaleTransform(this.Scene.ZoomX, this.Scene.ZoomY, enuMatrixOrder.Append);
                    device.TranslateTransform(this.Scene.PosX, this.Scene.PosY, enuMatrixOrder.Append);

                    Matrix m = this.Scene.LayoutDocument.Matrix;
                    if (!m.IsIdentity)
                        device.MultiplyTransform(m, enuMatrixOrder.Prepend);
                    this.m_androiddevice.Draw(device);
                    device.Restore(s);
                }

            }
        }

        private System.Type m_CurrentTool;

        public System.Type CurrentTool
        {
            get { return m_CurrentTool; }
            set
            {

                if ((value != null) && (value != this.m_CurrentTool))
                {
                    ICoreWorkingMecanism bck = this.m_Mecanism;
                    ICoreWorkingMecanism nmk = this.GetToolMecanism(value);
                    if (nmk != null)
                    {
                        if (bck != null)
                        {
                            bck.UnRegister();
                        }
                        this.m_Mecanism = nmk;
                        this.m_CurrentTool = value;
                        this.m_Mecanism.Register(this);
                        OnCurrentToolChanged(EventArgs.Empty);
                    }
                    //create new mecanism
                }

                //if (m_CurrentTool != value)
                //{
                //    m_CurrentTool = value;
                //    OnCurrentToolChanged(EventArgs.Empty);
                //}
            }
        }
        public virtual bool  IsToolValid(Type t)
        {
            AndroidGroupAttribute v_attr = Core2DDrawingObjectAttribute.GetCustomAttribute
                (t, typeof(AndroidGroupAttribute)) as AndroidGroupAttribute;
            if (v_attr == null)
                return false ;
            if (v_attr.MecanismType == null)
                return false ;
            return true;
        }
        public virtual ICoreWorkingMecanism GetToolMecanism(Type t)
        {
            AndroidGroupAttribute v_attr = Core2DDrawingObjectAttribute.GetCustomAttribute
                (t, typeof(AndroidGroupAttribute)) as AndroidGroupAttribute;
            if (v_attr == null)
                return null;
            if (v_attr.MecanismType == null)
                return null;
            Type v_t = v_attr.MecanismType;
            return v_t.Assembly.CreateInstance(v_t.FullName) as ICoreWorkingMecanism;
        }
        public event EventHandler CurrentToolChanged;
        private ICoreWorkingMecanism m_Mecanism;
        private AndroidMecanismFrame m_androidMecanismFrame;
        private AndroidDocumentFrame m_androidDocumentFrame;
        ///<summary>
        ///raise the CurrentToolChanged 
        ///</summary>
        protected virtual void OnCurrentToolChanged(EventArgs e)
        {
            if (CurrentToolChanged != null)
                CurrentToolChanged(this, e);
        }


        public Type DefaultTool
        {
            get {
                return typeof(AndroidSelectionElement);
            }
        }

        public ICoreWorkingMecanism Mecanism
        {            
            get {
                return this.m_Mecanism;
            }
        }

        public Rectanglei DisplayArea { get {
            Rectangle rc = this.DisplayRectangle;
            rc.Inflate(-10, -10);
            return new Rectanglei(rc.X, rc.Y, rc.Width, rc.Height);
        } }

        public void RefreshScene()
        {
            this.Invalidate();            
        }
        bool ICoreWorkingToolManagerSurface.IsToolValid(Type t)
        {
            return (t != null) && t.IsSubclassOf(typeof(AndroidLayoutItem));
        }
    }
}
