

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingScene.cs
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
file:IGKD2DDrawingSurfaceScene.cs
*/
using IGK.ICore.GraphicModels;
using IGK.ICore.Resources;
using IGK.ICore.WinUI;
using IGK.ICore.WinCore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinCore.WinUI;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

#pragma warning disable IDE1006

namespace IGK.ICore.Drawing2D.WinUI
{
    public partial  class IGKD2DDrawingSurfaceBase
    {

        public new event EventHandler GotFocus {
            add {
                this.m_Scene.GotFocus += value;
            }
            remove {
                this.m_Scene.GotFocus -= value;
            }
        }
        public new event EventHandler LostFocus
        {
            add
            {
                this.m_Scene.LostFocus += value;
            }
            remove
            {
                this.m_Scene.LostFocus -= value;
            }
        }
     
        
        /// <summary>
        /// represent the zone where to draw document. IGK Drawing 2D => Drawing Scene
        /// </summary>
        public class IGKD2DDrawingScene : IGKXControl, IIGK2DDrawingSceneTransform
        {

            public class Core2DDRendererFrameCollections : IEnumerable, ICore2DDrawingFrameRendererCollections
            {
                List<ICore2DDrawingFrameRenderer> m_frames;
                private IGKD2DDrawingScene m_scene;
                private Core2DDRendererFrameCollections()
                {
                    this.m_frames = new List<ICore2DDrawingFrameRenderer>();
                }
                public Core2DDRendererFrameCollections(IGKD2DDrawingScene iGKD2DDrawingScene)
                    : this()
                {
                    this.m_scene = iGKD2DDrawingScene;
                }
                /// <summary>
                /// get the number of layer frame
                /// </summary>
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
                /// <summary>
                /// create the frame collection
                /// </summary>
                public void Clear()
                {
                    this.m_frames.Clear();
                }

                /// <summary>
                /// get if the collection already contains a type instance
                /// </summary>
                /// <typeparam name="T"></typeparam>
                /// <returns></returns>
                public bool Contains<T>()
                {
                    for (int i = 0; i < this.m_frames.Count; i++)
                    {
                        if (this.m_frames[i] is T) {
                            return true;
                        }
                    }
                    return false;
                }
            }
           
            public abstract class IGKD2DDrawingSceneFrame : ICore2DDrawingFrameRenderer
            {
                private IGKD2DDrawingScene m_scene;
                /// <summary>
                /// get the scene
                /// </summary>
                public IGKD2DDrawingScene Scene
                {
                    get { return this.m_scene; }
                }
                public IGKD2DDrawingSceneFrame(IGKD2DDrawingScene scene)
                {
                    this.m_scene = scene;
                }
                public abstract void Render(ICoreGraphics device);
            }

            public class IGKD2DDrawingMecanismFrame : IGKD2DDrawingSceneFrame
            {
                public IGKD2DDrawingMecanismFrame(IGKD2DDrawingScene scene)
                    : base(scene)
                {
                }
                public override void Render(ICoreGraphics device)
                {
                    if (this.Scene.Mecanism is ICore2DDrawingFrameRenderer m)
                    {
                        m.Render(device);
                    }
                }
            }
            /// <summary>
            /// represent a document scene renderer
            /// </summary>
            public class IGKD2DDrawingSceneDocumentRender : IGK2DDrawingFrameRendererBase, ICore2DDrawingFrameRenderer
            {
                private IGKD2DDrawingScene m_scene;

                /// <summary>
                /// get or set the scene
                /// </summary>
                public IGKD2DDrawingScene Scene
                {
                    get { return this.m_scene; }
                }
                public IGKD2DDrawingSceneDocumentRender(IGKD2DDrawingScene scene):base()
                {
                    this.m_scene = scene;
                }
                protected virtual void SetupDevice(ICoreGraphics device)
                {
                    device.ScaleTransform(
                        m_scene.ZoomX,
                        m_scene.ZoomY,
                        enuMatrixOrder.Append);
                    device.TranslateTransform(m_scene.PosX,
                        m_scene.PosY,
                        enuMatrixOrder.Append);
                }
                public override void Render(ICoreGraphics device)
                {
                    float ex = m_scene.ZoomX;
                    float ey = m_scene.ZoomY;
                    if ((ex <= 0) || (ey <= 0))
                        return;
                    ICore2DDrawingDocument v_doc = this.m_scene.CurrentDocument;
                    Rectanglef v_rc = this.m_scene.GetScreenBound(new Rectanglef(0, 0, v_doc.Width, v_doc.Height));
                    Rectanglef v_bdispArea = this.m_scene.DisplayArea;


                

                    Rectanglef v_dispArea = v_bdispArea;
                    v_dispArea.Height += 1;
                    v_dispArea.Width += 1;
                    device.SetClip(v_dispArea);
                    Rectanglef v_displayRc = v_rc;
                    v_displayRc.Intersect(v_bdispArea);

                    device.SmoothingMode = enuSmoothingMode.None;
                    device.FillRectangle(Colorf.FromFloat (0.3f),
                        v_displayRc.X-1, v_displayRc.Y-1, v_displayRc.Width+2, v_displayRc.Height+2);
                    device.SmoothingMode = enuSmoothingMode.AntiAliazed;
                    object obj = device.Save();
                 
                    //Rectanglef v_shadowArea = v_displayRc;
                    //v_shadowArea.X += 1;
                    //v_shadowArea.Y += 1;
                    // device.SetClip(v_displayRc);

                    //shadow background
                    //device.FillRectangle(CoreRenderer.SceneBackgroundShadow,
                    //v_shadowArea.X,
                    //v_shadowArea.Y,
                    //v_shadowArea.Width,
                    //v_shadowArea.Height);

                    v_dispArea = v_bdispArea;
                    device.SetClip(v_dispArea);
                    if (v_doc.BackgroundTransparent)
                    {
                        ICoreBitmap bmp = CoreResources.GetBitmapResources(CoreImageKeys.IMG_DASH_GKDS);
                        if (bmp!=null){
                        bmp.RenderTexture(device, v_displayRc.X, v_displayRc.Y, v_displayRc.Width, v_displayRc.Height);
                        }
                    }
                    this.SetupDevice(device);       
                    v_doc.Draw(device);
                    device.Restore(obj);

                    device.SmoothingMode = enuSmoothingMode.None;
                    device.DrawRectangle(Colorf.FromFloat(0.3f),
                        v_displayRc.X - 1, v_displayRc.Y - 1, v_displayRc.Width + 2, v_displayRc.Height + 2);
                    device.SmoothingMode = enuSmoothingMode.AntiAliazed; 
                    
                }
            }
          
    
            private IGKD2DDrawingSurfaceBase c_OwnerSurface;
            private float m_PosX;
            private float m_PosY;
            private float m_ZoomX;
            private float m_ZoomY;
            private enuZoomMode m_ZoomMode;
            private IGKD2DDrawingSnipperLayerFrame m_SnippetLayerFrame;


            void IIGKSceneTransform.Zoom() {
                if (this.m_ZoomMode != enuZoomMode.Stretch)
                {
                    this.SetZoom(1.0f, 1.0f);
                }
            }
            protected override bool IsInputKey(Keys keyData)
            {
                switch (keyData )
                {
                    case Keys.Left:
                    case Keys.Up :
                    case Keys.Right :
                    case Keys.Down :
                        return true;
                    default:
                        break;
                }
                return base.IsInputKey(keyData);
            }
            protected override bool IsInputChar(char charCode)
            {
                return base.IsInputChar(charCode);
            }

            protected override void OnDragEnter(DragEventArgs drgevent)
            {
                c_OwnerSurface.OnDragEnter(drgevent);
            }
            protected override void OnDragDrop(DragEventArgs drgevent)
            {
                c_OwnerSurface.OnDragDrop(drgevent);
            }
            protected override void OnDragLeave(EventArgs e)
            {
                c_OwnerSurface.OnDragLeave(e);
            }
            protected override void OnDragOver(DragEventArgs drgevent)
            {
                c_OwnerSurface.OnDragOver(drgevent);
            }
            protected override void OnQueryContinueDrag(QueryContinueDragEventArgs qcdevent)
            {
                c_OwnerSurface.OnQueryContinueDrag(qcdevent);
            }
            /// <summary>
            /// get the owner surface
            /// </summary>
            public IGKD2DDrawingSurfaceBase Owner
            {
                get { return c_OwnerSurface; }
            }
            public IGKD2DDrawingSnipperLayerFrame SnippetLayerFrame
            {
                get { return m_SnippetLayerFrame; }
                protected set {
                    this.m_SnippetLayerFrame = value;
                }
            }
            public enuZoomMode ZoomMode
            {
                get { return m_ZoomMode; }
                set
                {
                    if (m_ZoomMode != value)
                    {
                        m_ZoomMode = value;
                        this.InitViewMode();
                        this.OnZoomModeChanged(EventArgs.Empty);
                    }
                }
            }
            public event EventHandler ZoomModeChanged;
            public virtual Rectanglei DisplayArea
            {
                get
                {
                    Rectangle c = base.ClientRectangle;
                    //c.Inflate(-1, -1);
                    if (this.Owner.ShowRules)
                    {
                        c.X += this.Owner.RuleWidth;
                        c.Width -= this.Owner.RuleWidth;
                        c.Y += this.Owner.RuleHeight;
                        c.Height -= this.Owner.RuleHeight;
                    }
                    if (this.Owner.ShowScroll)
                    {
                        c.Width -= this.Owner.ScrollWidth;
                        c.Height -= this.Owner.ScrollHeight;
                    }
                    return new Rectanglei(c.X, c.Y, c.Width, c.Height);
                }
            }
            protected override void OnMouseClick(MouseEventArgs e)
            {
                base.OnMouseClick(e);
            }
            protected override void OnCoreMouseClick(CoreMouseEventArgs e)
            {
                this.c_OwnerSurface.OnCoreMouseClick(e);
            }
            protected override void OnCoreKeyPress(CoreKeyEventArgs e)
            {
                this.c_OwnerSurface.OnCoreKeyPress(e);
            }
            protected override void OnCoreKeyUp(CoreKeyEventArgs e)
            {
                this.c_OwnerSurface.OnCoreKeyUp(e);
            }
            protected override void OnCoreMouseDoubleClick(CoreMouseEventArgs e)
            {
                this.c_OwnerSurface.OnCoreMouseDoubleClick(e);
            }
            protected override void OnCoreKeyDown(CoreKeyEventArgs e)
            {
                this.c_OwnerSurface.OnCoreKeyDown(e);
            }
            protected override void OnCoreMouseDown(CoreMouseEventArgs e)
            {
                //base.OnCoreMouseDown(e);
                if (!this.Focused)
                {
                    this.Focus();
                }
                this.c_OwnerSurface.OnCoreMouseDown(e);
            }
            protected override void OnCoreMouseUp(CoreMouseEventArgs e)
            {
                this.c_OwnerSurface.OnCoreMouseUp(e);
            }
            protected override void OnCoreMouseMove(CoreMouseEventArgs e)
            {
                this.c_OwnerSurface.OnCoreMouseMove(e);
            }
            protected override void OnMouseEnter(EventArgs e)
            {
                base.OnMouseEnter(e);
            }
            protected override void OnMouseLeave(EventArgs e)
            {
                base.OnMouseLeave(e);
            }
            ///<summary>
            ///raise the ZoomModeChanged 
            ///</summary>
            protected virtual void OnZoomModeChanged(EventArgs e)
            {
                ZoomModeChanged?.Invoke(this, e);
            }
            private Core2DDRendererFrameCollections m_Frames;
            private Core2DDRendererFrameCollections m_OverLayFrames;

            /// <summary>
            /// get the collection of basics frames
            /// </summary>
            public Core2DDRendererFrameCollections Frames {
                get {
                    return m_Frames;
                }
            }
            /// <summary>
            /// get the collection of overlay frames
            /// </summary>
            public Core2DDRendererFrameCollections OverlayFrames {
                get {
                    return this.m_OverLayFrames;
                }
            }
            private IGKD2DDrawingSelectionLayerFrame m_SelectionFrame;
            /// <summary>
            /// get or set the current document
            /// </summary>
            public Core2DDrawingDocumentBase CurrentDocument
            {
                get { 
                    return this.c_OwnerSurface.CurrentDocument; 
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
            /// <summary>
            /// update the view from mode
            /// </summary>
            private void UpdateViewMode()
            {
                ICore2DDrawingDocument v_doc = this.CurrentDocument;
                Rectanglei v_rc = this.DisplayArea;
                float v_posx = v_rc.X;
                float v_posy = v_rc.Y;
                switch (this.ZoomMode)
                {
                    case enuZoomMode.Normal:
                        break;
                    case enuZoomMode.NormalCenter:
                        v_posx = v_rc.X + ((v_rc.Width - (v_doc.Width * ZoomX )) / 2.0f);
                        v_posy = v_rc.Y + ((v_rc.Height - (v_doc.Height *ZoomY) ) / 2.0f);
                        break;
                    case enuZoomMode.ZoomCenter:
                        break;
                    case enuZoomMode.ZoomNormal:
                        break;
                    default:
                        break;
                }
                this.PosX = v_posx;
                this.PosY = v_posy;
            }
            private void InitViewMode()            
            {
                
                if (this.c_OwnerSurface.Parent == null)
                    return;

                float v_oldzoomx = this.m_ZoomX ;
                float v_oldzoomy = this.m_ZoomY;
                float v_posx = 0.0f;
                float v_posy = 0.0f;
                float v_zoomx = 1.0f;
                float v_zoomy = 1.0f;
                ICore2DDrawingDimensionDocument v_doc = this.CurrentDocument;
                if (v_doc == null)
                    return;
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
      
            
            internal protected IGKD2DDrawingScene(IGKD2DDrawingSurfaceBase surface)
            {
                this.SetStyle(ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint,
                    true);
                this.SetStyle(ControlStyles.Selectable, true);
                this.m_Frames = new Core2DDRendererFrameCollections(this);
                this.m_OverLayFrames = new Core2DDRendererFrameCollections(this);
                this.AllowDrop = true;
                this.c_OwnerSurface = surface;
                this.Dock = DockStyle.Fill;
                this.m_ZoomMode = enuZoomMode.ZoomCenter;
                this.m_PosX = 0.0f;
                this.m_PosY = 0.0f;
                this.m_ZoomX = 1.0f;
                this.m_ZoomY = 1.0f;
                this.InitFrames();
                this.ZoomMode = enuZoomMode.ZoomCenter;
                this.SizeChanged += _SizeChanged;
                this.c_OwnerSurface.CurrentDocumentChanged += c_owner_CurrentDocumentChanged;

                this.c_OwnerSurface.ShowRuleChanged += c_owner_ShowRuleChanged;
                this.c_OwnerSurface.ShowScrollChanged += c_owner_ShowScrollChanged;
                if (this.c_OwnerSurface.CurrentDocument != null)
                    this.RegisterDocumentEvent(this.c_OwnerSurface.CurrentDocument);
            }
            protected virtual IGKD2DDrawingSceneDocumentRender CreateDocumentSceneRenderer() {

                return new IGKD2DDrawingSceneDocumentRender(this);
            }
            /// <summary>
            /// init frames
            /// </summary>
            protected virtual void InitFrames()
            {
                ///init frame mecanism
                this.m_Frames.Clear();
                this.m_Frames.Add(this.CreateDocumentSceneRenderer ());
                this.m_Frames.Add(new IGKD2DDrawingMecanismFrame(this));
                this.m_SelectionFrame = new IGKD2DDrawingSelectionLayerFrame(this);
                this.m_Frames.Add(this.m_SelectionFrame);
                this.m_SnippetLayerFrame = new IGKD2DDrawingSnipperLayerFrame(this);
                this.m_Frames.Add(this.m_SnippetLayerFrame);
            }

            void c_owner_ShowScrollChanged(object sender, EventArgs e)
            {
                this.InitViewMode();
            }

            void c_owner_ShowRuleChanged(object sender, EventArgs e)
            {
                this.InitViewMode();
            }
            protected virtual  void UnRegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                document.PropertyChanged -= document_PropertyChanged;
            }
            protected virtual  void RegisterDocumentEvent(Core2DDrawingDocumentBase document)
            {
                document.PropertyChanged += document_PropertyChanged;
            }
            void document_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                if ((enu2DPropertyChangedType)e.ID == enu2DPropertyChangedType.SizeChanged)
                {
                    InitViewMode();
                }
            }
            void c_owner_CurrentDocumentChanged(object o, CoreWorkingDocumentChangedEventArgs e)
            {
                if (e.OldElement  is Core2DDrawingDocumentBase)
                    UnRegisterDocumentEvent(e.OldElement as Core2DDrawingDocumentBase);
                if (e.NewElement is Core2DDrawingDocumentBase)
                    RegisterDocumentEvent(e.NewElement as Core2DDrawingDocumentBase);
                this.InitViewMode();
                this.Invalidate();
            }
            void _SizeChanged(object sender, EventArgs e)
            {
                InitViewMode();
            }
            protected override void OnMouseDown(MouseEventArgs e)
            {
                if (!this.Focused)
                    this.Focus();
                base.OnMouseDown(e);
            }
            protected override void OnPaintBackground(PaintEventArgs pevent)
            {
                //base.OnPaintBackground(pevent);
                //do nothing
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                if (this.DesignMode) return;
                this.RenderFrames(e.Graphics);
            }
            /// <summary>
            /// render frames
            /// </summary>
            /// <param name="graphics"></param>
            private void RenderFrames(Graphics graphics)
            {
                ICoreGraphics g = CoreApplicationManager.Application.ResourcesManager.CreateDevice(graphics);
                RenderFrames(g);

            }

            protected override void OnGotFocus(EventArgs e)
            {
                base.OnGotFocus(e);
                this.Invalidate();

            }
            protected override void OnLostFocus(EventArgs e)
            {
                base.OnLostFocus(e);
                this.Invalidate();
            }

            protected virtual void RenderFrames(ICoreGraphics g)
            {
                if (g != null)
                {
                    //setup graphics
                    g.Clear(WinCoreControlRenderer.SurfaceContainerBackgroundColor);

                    //render frames
                    foreach (ICore2DDrawingFrameRenderer item in this.m_Frames)
                    {
                        item.Render(g);
                    }
                    //render overlay frames
                    foreach (ICore2DDrawingFrameRenderer item in this.m_OverLayFrames)
                    {
                        item.Render(g);
                    }
                }
            }
            /// <summary>
            /// get screen zoom bounds
            /// </summary>
            /// <param name="zoomBound"></param>
            /// <returns></returns>
            public virtual Rectanglef GetScreenBound(Rectanglef zoomBound)
            {
                Vector2f pts = GetScreenLocation(zoomBound.Location);
                return new Rectanglef(pts.X,
                    pts.Y,
                        zoomBound.Width * ZoomX,
                        zoomBound.Height * ZoomY
                        );
            }
            public Vector2f GetScreenLocation(Vector2f factorLocation)
            {
                return new Vector2f(
        (float)Math.Ceiling((factorLocation.X * ZoomX) + this.PosX),
        (float)Math.Ceiling((factorLocation.Y * ZoomY) + this.PosY)
        );
            }
            public Vector2f GetFactorLocation(Vector2f screenLocation)
            {
                Vector2f pts = new Vector2f(
                   (float)Math.Floor((screenLocation.X - this.PosX) / ZoomX),
                   (float)Math.Floor((screenLocation.Y - this.PosY) / ZoomY));
                return pts;
            }
            public override Vector2f GetFactorLocation(int x, int y)
            {
                Vector2f pts = new Vector2f(
             (float)Math.Floor((x - this.PosX) / ZoomX),
             (float)Math.Floor((y - this.PosY) / ZoomY));
                return pts;
            }
            public Matrix GetMarix()
            {
                return null;
            }
            public  CoreGraphicsPath GetPath()
            {
                return null;
            }
                public ICoreWorkingMecanism  Mecanism { get {
                return this.c_OwnerSurface.Mecanism;
            } }

            protected override void OnMouseWheel(MouseEventArgs e)
            {
                if (!this.Focused )
                    return ;
                base.OnMouseWheel(e);

                SetZoomFromMouseWheel(e.Delta, 2.0f);
            }

            public void SetZoomFromMouseWheel(int delta, float zoomFactor)
            {
                if ((zoomFactor<=0) || !this.Focused || !this.Visible || !((Control.ModifierKeys & Keys.Control ) == Keys.Control))
                    return;
                switch (this.ZoomMode)
                {
                    case enuZoomMode.Normal:
                    case enuZoomMode.NormalCenter:
                        float z = 1.0f;
                        if (delta > 0)
                        {
                            z *= zoomFactor;
                        }
                        else
                            z /= zoomFactor;

                        float x = this.ZoomX * z;
                        float y = this.ZoomY * z;
                        float W = this.CurrentDocument.Width;
                        float H = this.CurrentDocument.Height;
                        float w = x * W;
                        float h = y * H;
                        //float doc_width = this.CurrentDocument.Width;
                        //float doc_height = this.CurrentDocument.Height; 
                        if ((w > short.MaxValue) || (h > short.MaxValue))
                        {
                            //this.SetZoom(1.0f, 1.0f);
                            //reach max zoom
                        }
                        else
                        {
                            if (w <= 16)
                            {
                                x = 16.0f / W;
                                y = x;
                            }
                            else
                            {
                                if (h <= 16)
                                {
                                    y = 16.0f / H;
                                    x = y;
                                }
                            }
                            //limit the zoom size
                            this.SetZoom(x, y);
                        }
                        break;
                }
           
            }         

            public void SetZoom(float zoomx, float zoomy)
            {
                switch (this.ZoomMode)
                {
                    case enuZoomMode.Normal:
                    case enuZoomMode.NormalCenter:

                        this.m_ZoomX = zoomx;
                        this.m_ZoomY = zoomy;
                        this.UpdateViewMode();
                        OnZoomChanged(EventArgs.Empty);
                        break;
                }
            }

            public event EventHandler ZoomChanged;
            ///<summary>
            ///raise the ZoomChanged 
            ///</summary>
            protected virtual void OnZoomChanged(EventArgs e)
            {
                ZoomChanged?.Invoke(this, e);
            }

            /// <summary>
            /// scrol to element
            /// </summary>
            /// <param name="posX"></param>
            /// <param name="posY"></param>
            internal void ScrollTo(float posX, float posY)
            {
                bool v = 
                (this.m_PosX != posX) ||
                (this.m_PosY != posY);
                this.m_PosX = posX;
                this.m_PosY = posY;
                if (v)
                {                    
                    this.Invalidate();
                    this.OnZoomChanged(EventArgs.Empty);
                }
                
            }
        }


    }
}

