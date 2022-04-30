
using IGK.DrSStudio.Android.WinUI;


using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Dispatch;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio.Android.Resources;

namespace IGK.DrSStudio.Android.WinUI
{
    /// <summary>
    /// represent a android design surface
    /// </summary>
    public class AndroidMenuBuilderDesignSurface : 
        IGKD2DDrawingSurface ,
        ICoreWorkingApplicationContextSurface,
        IAndroidResourceViewAdapterListener
        
    {
        private ICoreDispatcher m_dispatcher;

        public override Type DefaultTool
        {
            get
            {
                return typeof(AndroidResourcesSelectionMecanism);
            }
        }
        public override bool IsToolValid(Type t)
        {
            return true;
        }
        public override ICoreWorkingMecanism GetToolMecanism(Type t)
        {
            return t != null ? t.Assembly.CreateInstance(t.FullName) as ICoreWorkingMecanism  : null;
        }
        public AndroidMenuBuilderDesignSurface():base()
        {
            this.ShowScroll = true;
            this.ZoomMode = enuZoomMode.NormalCenter;
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            
        }
        
        protected override IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene CreateScene()
        {
            return new ResourceScene(this);
        }

        [Category("Event")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden )]
        public ICoreDispatcher Dispatcher
        {
            get { return this.m_dispatcher; }
            set {
                if (this.m_dispatcher != value) {
                    this.m_dispatcher = value;
                    OnDispatcherChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler DispatcherChanged;
        private IAndroidResourceViewAdapter m_adapter;
        ///<summary>
        ///raise the DispatcherChanged 
        ///</summary>
        protected virtual void OnDispatcherChanged(EventArgs e)
        {
            if (DispatcherChanged != null)
                DispatcherChanged(this, e);
        }


        /// <summary>
        /// get or set the adapter
        /// </summary>
        public IAndroidResourceViewAdapter Adapter { get {
            return this.m_adapter;
            }
            set {
                if (this.m_adapter != value)
                {
                    this.m_adapter = value;
                    if (value != null)
                    {
                        this.m_adapter.SetNotifyChangedListener(this);
                        OnDataChanged(enuChangedType.DataLoaded, 0);
                    }
                }
            }
        }

        public void OnDataChanged(enuChangedType changeType, int position)
        {
            switch (changeType)
            {
                case enuChangedType.DataValueChanged:
                    {
                        var r = this.Adapter.GetObject(position) as AndroidResourceItemBase;
                        //update view
                        var s = this.Adapter.GetView(this,r==null? null: r.Host, position);
                        this.RefreshScene();
                    }
                    break;
                default:

                    var l = this.CurrentLayer;
                    var p = l.Elements.ToArray();
                    l.Elements.Clear();

                    for (int i = 0; i < this.Adapter.Count; i++)
                    {
                        var r = this.Adapter.GetObject(i) as AndroidResourceItemBase;
                        var s = this.Adapter.GetView(this,
                            (changeType == enuChangedType.DataLoaded) ? null : r.Host,
                            i);
                        this.CurrentLayer.Elements.Add(s);
                    }
                    if (changeType == enuChangedType.DataLoaded)
                    {
                        //remove dispatcher
                        foreach (var item in p)
                        {
                            // this.Dispatcher.UnRegister(item);
                            item.Dispose();
                        }
                        var d = this.Dispatcher;
                    }
                    break;
            }
        }
        /// <summary>
        /// clear all resources
        /// </summary>
        public void ClearResources()
        {
            this.Adapter.Clear();
        }
       

        public bool ShowBorder { get; set; }


        /// <summary>
        /// resource scene
        /// </summary>
        sealed class ResourceScene : IGKD2DDrawingSurfaceBase.IGKD2DDrawingScene
        {
            private Rectanglef m_defaultBound;
            private new AndroidMenuBuilderDesignSurface Owner
            {
                get
                {
                    return base.Owner as AndroidMenuBuilderDesignSurface;
                }
            }
            public ResourceScene(AndroidMenuBuilderDesignSurface surface)
                : base(surface)
            {
                RegisetLayerEvent(surface.CurrentLayer);
            }
            protected override void OnCoreMouseClick(CoreMouseEventArgs e)
            {
                base.OnCoreMouseClick(e);
                ICoreMouseClickDispatcher c = this.Owner.m_dispatcher as
                    ICoreMouseClickDispatcher;
                if (c != null)
                {
                    c.Invoke(e);
                }
            }
            protected override void OnCoreMouseMove(CoreMouseEventArgs e)
            {
                base.OnCoreMouseMove(e);
                ICoreMouseMoveDispatcher c = this.Owner.m_dispatcher as
                   ICoreMouseMoveDispatcher;
                if (c != null)
                {
                    c.Invoke(e);
                }
            }


            private void RegisetLayerEvent(ICore2DDrawingLayer l)
            {
                l.ElementAdded += _ElementAdded;
                l.ElementRemoved += _ElementRm;
            }
            void updateSize()
            {
                //build document size
                var l = this.Owner.CurrentLayer;
                var r = Rectanglef.Empty;
                var v_rc = CoreMathOperation.GetGlobalBounds(l.Elements.ToArray());

                this.m_defaultBound = v_rc;
                v_rc.Inflate("5mm".ToPixel(), "5mm".ToPixel());
                this.Owner.CurrentDocument.SetSize(v_rc.Width, v_rc.Height);
                this.Owner.CurrentDocument.Translate(
                    -v_rc.X,
                   -v_rc.Y);
            }
            void _ElementRm(object sender, CoreItemEventArgs<ICore.Drawing2D.ICore2DDrawingLayeredElement> e)
            {
                this.updateSize();
            }
            void _ElementAdded(object sender, CoreItemEventArgs<ICore.Drawing2D.ICore2DDrawingLayeredElement> e)
            {
                this.updateSize();
            }
            protected override void InitFrames()
            {
                //base.InitFrames();
                this.Frames.Clear();
                this.Frames.Add(new AndroidDocumentRenderer(this));
                this.SnippetLayerFrame = new IGKD2DDrawingSnipperLayerFrame(this);
                this.Frames.Add(this.SnippetLayerFrame);
            }

            /// <summary>
            /// renderer class 
            /// </summary>
            class AndroidDocumentRenderer : IGKD2DDrawingSceneDocumentRender
            {
                public new ResourceScene Scene
                {
                    get
                    {
                        return base.Scene as ResourceScene;
                    }
                }
                public AndroidDocumentRenderer(ResourceScene resourceScene)
                    : base(resourceScene)
                {
                }

                public override void Render(ICoreGraphics device)
                {
                    device.Clear(CoreRenderer.SceneBackground);//Shadow Colorf.White);
                    float ex = this.Scene.ZoomX;
                    float ey = this.Scene.ZoomY;
                    if ((ex <= 0) || (ey <= 0))
                        return;
                    var v_doc = this.Scene.CurrentDocument;
                    Rectanglef v_rc = Rectanglef.Round(this.Scene.GetScreenBound(new Rectanglef(0, 0, v_doc.Width, v_doc.Height)));

                    object obj = device.Save();
                    Rectanglef v_dispArea = this.Scene.DisplayArea;
                    v_dispArea.Height += 4;
                    v_dispArea.Width += 4;
                    //device.SetClip(v_dispArea);
                    Rectanglef v_displayRc = v_rc;
                    //v_displayRc.Intersect(this.Scene.DisplayArea);
                    //Rectanglef v_shadowArea = v_displayRc;
                    //v_shadowArea.X += 4;
                    //v_shadowArea.Y += 4;
                    // device.SetClip(v_displayRc);

                    //shadow background

                    v_dispArea = this.Scene.DisplayArea;
                    //device.SetClip(v_dispArea);
                    //if (v_doc.BackgroundTransparent)
                    //{
                    //    ICoreBitmap bmp = CoreResources.GetBitmapResources("Menu_DASH");
                    //    bmp.RenderTexture(device, v_displayRc.X, v_displayRc.Y, v_displayRc.Width, v_displayRc.Height);
                    //}

                    var def = this.Scene.m_defaultBound;

                    device.ScaleTransform(
                        this.Scene.ZoomX,
                        this.Scene.ZoomY,
                        enuMatrixOrder.Append);
                    device.TranslateTransform(
                        this.Scene.PosX,
                        this.Scene.PosY,
                        enuMatrixOrder.Append);
                  


                    v_doc.CurrentLayer.Draw(device);

                    device.Restore(obj);
                    if (this.Scene.Owner.ShowBorder)
                    {
                        device.DrawRectangle(Colorf.Black,
                        v_displayRc.X, v_displayRc.Y, v_displayRc.Width, v_displayRc.Height);
                    }


                }
            }
      
        
        }


    }
}
