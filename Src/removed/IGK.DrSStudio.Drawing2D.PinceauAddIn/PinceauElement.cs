

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PinceauElement.cs
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
file:PinceauElement.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Drawing2D.Actions;
    using IGK.DrSStudio.WinUI.Configuration;
    /// <summary>
    /// represent pinceau brush element and mecanism used to draw on bitmap
    /// </summary>
    [Core2DDrawingImageItem ("Pinceau",
        typeof(Mecanism))]
    public sealed class PinceauElement : 
        Core2DDrawingLayeredDualBrushElement 
    {
        internal const int PEN_WIDTH = 32;
        internal const int PEN_HEIGHT = 32;
        /// <summary>
        /// mecanism to render
        /// </summary>
        new internal class Mecanism :
            Core2DDrawingMecanismBase
        {
            class EditPinceauMecanism : Core2DDrawingMecanismAction, ICoreWorkingConfigurableObject 
            {
                Mecanism m_mecanism;
                public EditPinceauMecanism(Mecanism mecanism):base()
                {
                    this.m_mecanism = mecanism;
                }
                protected override bool PerformAction()
                {
                    m_mecanism.CurrentSurface.Workbench.ConfigureWorkingObject(this);
                    return false;
                }
                public override string ToString()
                {
                    return "Edit PinceauMecanism";
                }
                #region ICoreWorkingConfigurableObject Members
                public Control GetConfigControl()
                {
                    throw new NotImplementedException();
                }
                public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
                {
                    return enuParamConfigType.ParameterConfig;
                }
                public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
                {
                    var group = parameters.AddGroup("default");
                    group.AddEnumItem("InterpolationMode", "lb.InterpolationMode", typeof(System.Drawing.Drawing2D.InterpolationMode),
                         this.m_mecanism.InterpolationMode,
                         interpolationChanged);
                    group.AddEnumItem("CompositingQuality", "lb.CompositingQuality", typeof(System.Drawing.Drawing2D.CompositingQuality ),
                         this.m_mecanism.CompositingQuality,
                         compositingChanged);
                    return parameters;
                }
                void interpolationChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
                {
                    InterpolationMode m = (InterpolationMode)e.Value;
                    if (m != InterpolationMode.Invalid)
                        this.m_mecanism.InterpolationMode = m;
                }
                void compositingChanged(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
                {
                    CompositingQuality  m = (CompositingQuality )e.Value;
                    if (m != CompositingQuality.Invalid)
                        this.m_mecanism.CompositingQuality = m;
                }
                #endregion
                #region ICoreIdentifier Members
                public override string Id
                {
                    get { return "ConfigurePinceauMecanism"; }
                }
                #endregion
            }
            //private List<Vector2f> m_list; // list of points                     
            private ImageElement m_imageElement;// the image element
            private Bitmap m_oldBitmap; //saved bitmap
            private Bitmap m_newBitmap; //new bitmap
            private Graphics m_imageGraphics;//image graphics attached to the imagelement
            private PinceauItem m_Pinceau;    //pinceau utiliser. il 
            private float m_PinceauSize;                //taille du pinceau
            private float m_Step;                       //pas de distance            
            private CompositingMode m_compositingMode;
            private bool m_monoDirection;
            private bool m_bitmapChanged;
            //drawing mode
            private int mode = MD_FREE;
            const int MD_FREE = 0;
            const int MD_HORIZONTAL = 1;
            const int MD_VERTICAL = 2;
            /// <summary>
            /// get or set the smoothing mode of the item
            /// </summary>
            public enuSmoothingMode SmoothingMode
            {
                get
                {
                    return this.m_Pinceau.SmoothingMode;
                }
                set
                {
                    this.m_Pinceau.SmoothingMode = value;
                }
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.Actions[Keys.Control | Keys.E] = new EditPinceauMecanism (this);
            }
            public ImageElement ImageElement {
                get { return this.m_imageElement; }
                set {
                    if (this.m_imageElement != value)
                    {
                        if (this.m_imageElement != null) UnRegisterImageElementEvent(this.m_imageElement);
                        this.m_imageElement = value;
                        if (this.m_imageElement != null) RegisterImageElementEvent(this.m_imageElement);
                        OnImageChanged(EventArgs.Empty);
                    }
                }
            }
            private void RegisterImageElementEvent(ImageElement imageElement)
            {
                imageElement.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(imageElement_PropertyChanged);
            }
            private void UnRegisterImageElementEvent(ImageElement imageElement)
            {
                imageElement.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(imageElement_PropertyChanged);
            }
            void imageElement_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                if ((enu2DPropertyChangedType)e.ID == enu2DPropertyChangedType.BitmapChanged)
                {
                    InitImageProperty();
                }
            }
            protected override void RegisterLayerEvent(ICore2DDrawingLayer layer)
            {
                base.RegisterLayerEvent(layer);
                layer.ElementRemoved += new Core2DDrawingElementEventHandler(layer_ElementRemoved);
            }
            void layer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
            {
                if (e.Element == this.m_imageElement)
                {
                    this.ImageElement = null;
                }
            }
            protected override void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
            {
                base.UnRegisterLayerEvent(layer);
            }
            private void OnImageChanged(EventArgs eventArgs)
            {
                if (this.ImageElement != null)
                {
                    InitImageProperty();
                }
            }
            private void InitImageProperty()
            {
                this.CurrentSurface.CurrentLayer.Select(this.m_imageElement);
                DisposeOldBitmap();
                this.m_bitmapChanged = false;
                this.m_oldBitmap = this.ImageElement.Bitmap.Clone() as Bitmap;
                this.m_newBitmap = this.m_oldBitmap.Clone() as Bitmap;
                this.Element = this.m_imageElement;
                //change element to configure
                this.CurrentSurface.ElementToConfigure = this.BrushStyle;
            }
            private void DisposeOldBitmap()
            {
                if ((this.m_oldBitmap != null)&&(this.m_bitmapChanged ))
                {
                    this.m_oldBitmap.Dispose();                    
                }
                this.m_oldBitmap = null;
            }
            /// <summary>
            /// get or set the pinceau type
            /// </summary>
            public Core2DDrawingLayeredDualBrushElement BrushStyle
            {
                get
                {
                    return m_Pinceau;
                }
                set
                {
                    if (this.m_Pinceau != null)
                    {
                        //change element to draw
                        this.m_Pinceau.Element = value;
                    }
                    else
                    {
                        //dispose old pinceau
                        DisposePinceau();
                        if (value != null)
                        {
                            this.m_Pinceau = new PinceauItem(value);
                        }
                        SetupPinceau();
                    }
                }
            }
            private void SetupPinceau()
            {
                Tools.ToolPinceauManager.Instance.MecanismRegister = true;
                this.CurrentSurface.ElementToConfigure = this.m_Pinceau;
                Tools.ToolPinceauManager.Instance.Element = this.m_Pinceau;
            }
            /// <summary>
            /// get if this element is mono direction
            /// </summary>
            public bool MonoDirection
            {
                get
                {
                    return this.m_monoDirection;
                }
                set
                {
                    this.m_monoDirection = value;
                }
            }
            public CompositingMode CompositingMode
            {
                get
                {
                    return m_compositingMode;
                }
                set
                {
                    m_compositingMode = value;
                }
            }
            public float PinceauSize
            {
                get
                {
                    if (this.m_PinceauSize <= 0.0f)
                        m_PinceauSize = 1.0f;
                    return m_PinceauSize;
                }
                set
                {
                    if (value > 0)
                    {
                        m_PinceauSize = value;
                    }
                }
            }
            /// <summary>
            /// get or set the step in pixel
            /// </summary>
            public float Step
            {
                get
                {
                    return m_Step;
                }
                set
                {
                    if (value > 0)
                    {
                        m_Step = value;
                    }
                }
            }
            private void DisposePinceau()
            {
                if (m_Pinceau != null)
                {
                    m_Pinceau.Dispose();
                    m_Pinceau = null;
                }
            }
            public override void Dispose()
            {
                this.DisposeOldBitmap();
                this.DisposePinceau();
                base.Dispose();
            }
            public Mecanism()
            {
                //init some default property
//                m_list = new List<Vector2f>();
                m_Step = 1;
                m_PinceauSize = 1.0F;
                InitPinceau();
                this.DesignMode = true;
                //set the mecanism hosted control
            }
            private void InitPinceau()
            {
                CircleElement cl = new CircleElement();
                cl.Center = new Vector2f(16, 16);
                cl.Radius = new float[] { 16.0f };
                m_Pinceau = new PinceauItem(cl);
                if (m_Pinceau == null)
                    throw new CoreException(enuExceptionType.OperationNotValid, "Circle Not Found");
                m_Pinceau.StrokeBrush.SetSolidColor(Color.Black);
                m_Pinceau.FillBrush.SetSolidColor(Color.White);
            }
            /// <summary>
            /// get the point associate to the image element
            /// </summary>
            /// <param name="fPoint"></param>
            /// <returns></returns>
            private Point GetRealPoint(Vector2f fPoint)
            {
                if (m_imageElement == null) return Point.Empty;
                Matrix m = m_imageElement.GetMatrix().Clone() as Matrix;
                Point pt = Point.Empty;
                if (m.IsInvertible && !m.IsIdentity)
                {
                    Vector2f[] tps = new Vector2f[] { fPoint };
                    m.Invert();
                    m.TransformPoints(tps);
                    pt = new Point((int)tps[0].X, (int)tps[0].Y);
                }
                else
                {
                    pt = new Point((int)fPoint.X, (int)fPoint.Y);
                }
                return pt;
            }
            public override void Edit(ICoreWorkingObject element)
            {                
                if (element is ImageElement)
                {
                    this.ImageElement = element as ImageElement;
                }
            }
            protected  override string GetHelpMessage()
            {
                string vstr = "";
                string vmode = "";
                switch (mode)
                {
                    case MD_FREE:
                        vmode = CoreSystem.GetString("pinceau.mode.free");
                        break;
                    case MD_HORIZONTAL:
                        vmode = CoreSystem.GetString("pinceau.mode.horizontal");
                        break;
                    case MD_VERTICAL:
                        vmode = CoreSystem.GetString("pinceau.mode.vertical");
                        break;
                }
                vstr = string.Format("(S): {0},(V):(H):(F)", this.CompositingMode);
                return vstr;
            }
            protected override void OnKeyUp(KeyEventArgs e)
            {
                switch (e.KeyCode)
                {
                    case Keys.V:
                        mode = MD_VERTICAL;
                        e.Handled = true;
                        break;
                    case Keys.H:
                        mode = MD_HORIZONTAL;
                        e.Handled = true;
                        break;
                    case Keys.F:
                        mode = MD_FREE;
                        e.Handled = true;
                        break;
                    case Keys.S: //changed source mode 
                        if (this.m_compositingMode == CompositingMode.SourceCopy)
                            this.m_compositingMode = CompositingMode.SourceOver;
                        else
                            this.m_compositingMode = CompositingMode.SourceCopy;
                        e.Handled = true;
                        break;
                }
                base.OnKeyUp(e);
            }
            protected override void RegisterSurface(ICore2DDrawingSurface surface)
            {
                base.RegisterSurface(surface);
                SetupPinceau();
            }
            protected override void UnRegisterSurface(ICore2DDrawingSurface surface)
            {
                if (this.m_imageElement != null)
                {
                    UnRegisterImageElementEvent(this.m_imageElement);
                }
                base.UnRegisterSurface(surface);
                Tools.ToolPinceauManager.Instance.MecanismRegister = false;
                //end pinceau drawing
                surface.Invalidate();
            }
            public override void EndEdition()
            {
                base.EndEdition();
            }
            protected override void OnMouseDown(IGK.DrSStudio.Drawing2D.WinUI.CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        this.StartPoint = e.FactorPoint;
                        this.EndPoint = e.FactorPoint;
                        if (this.IsShiftKey)
                        {
                            BeginGetSize(e);
                        }
                        else
                        {
                            if (this.m_imageElement == null)
                            {
                                //check first image element
                                ICore2DDrawingLayer l = this.CurrentSurface.CurrentLayer;
                                for (int i = l.Elements.Count - 1; i >= 0; --i)
                                {
                                    if (l.Elements[i].Contains(e.FactorPoint) && (l.Elements[i] is ImageElement))
                                    {
                                        //capture the image elment
                                        this.ImageElement  = l.Elements[i] as ImageElement;
                                        break;
                                    }
                                }
                            }
                            if (this.m_imageElement !=null) 
                            {
                                if (!CurrentSurface.CurrentLayer.SelectedElements.Contains(this.ImageElement ))
                                {                                
                                this.CurrentSurface.CurrentLayer.Select(this.ImageElement);
                                }
                                this.CurrentSurface.ElementToConfigure = this.m_Pinceau;
                                BeginPinceau(e);
                            }
                        }
                        break;
                }
            }
            private void BeginPinceau(CoreMouseEventArgs e)
            {
                if ((this.m_oldBitmap == null) || (this.m_oldBitmap.PixelFormat == PixelFormat.Undefined))
                {
                    this.State = ST_NONE;
                    return;
                }
                this.State = ST_EDITING;
                if (this.m_imageGraphics != null)
                {
                    this.m_imageGraphics.Dispose();
                    this.m_imageGraphics = null;
                }
                if ((this.m_newBitmap == null) || (this.m_newBitmap .PixelFormat == PixelFormat.Undefined ))
                {
                    this.m_newBitmap = m_oldBitmap.Clone() as Bitmap;
                }
                this.ImageElement.SetBitmap(this.m_newBitmap, true);
                this.ImageElement.Invalidate(true);
                this.m_imageGraphics = Graphics.FromImage(this.m_newBitmap);
                this.StartPoint = GetRealPoint(e.FactorPoint);
                this.UpdatePinceau(e);
            }
            private void EndPinceau(CoreMouseEventArgs e)
            {
                //this.m_list.Add(e.FactorPoint);
                this.ClearPinceauTransform();
                float ex = this.m_PinceauSize / (PEN_WIDTH);
                float ey = this.m_PinceauSize / (PEN_HEIGHT);
                m_Pinceau.Scale(ex, ey, enuMatrixOrder.Append);
                this.DrawPinceau(false);
                this.ClearPinceauTransform();
                if (this.m_imageGraphics == null)
                {
                    //this.m_list.Clear();
                    this.ClearPinceauTransform();
                    return;
                }
                if (!IsControlKey)
                {
                    if (this.State == ST_CONFIGURING)
                        this.UpdatePinceau(e);
                }
                else
                {
                    UpdatePinceau(e);
                }
                this.State = ST_NONE;                
                this.m_imageGraphics.Dispose();
                this.m_imageGraphics = null;
                //to update bitmap data
                this.ImageElement.SetBitmap(this.m_newBitmap.Clone () as Bitmap, false );
                //setup new bitmap               
                this.m_bitmapChanged = true;
                this.CurrentSurface.Invalidate(this.ImageElement);
            }
            private void UpdatePinceau(CoreMouseEventArgs e)
            {
                //if (this.m_list.Count > 0)
                //{
                //    //disallow the possibility to add element that match the target
                //    if (this.m_list[this.m_list.Count - 1].Equals(e.FactorPoint))
                //        return;
                //}
                //this.m_list.Add(e.FactorPoint);
                this.ClearPinceauTransform();         
                float ex = this.m_PinceauSize / (PEN_WIDTH);
                float ey = this.m_PinceauSize / (PEN_HEIGHT);
                m_Pinceau.Scale(ex, ey, enuMatrixOrder.Append);
                this.EndPoint = GetRealPoint(e.FactorPoint);
                this.DrawPinceau(true);
                this.StartPoint = this.EndPoint;
                this.ClearPinceauTransform();
                this.CurrentSurface.Invalidate();
            }
            private void DrawPinceau(bool tempx)
            {
                if (this.m_imageGraphics == null)
                    return;
                this.m_imageGraphics.Clear(Color.Transparent);
                this.m_imageGraphics.DrawImage(this.m_oldBitmap, Point.Empty);
                if (this.m_imageGraphics == null)
                    return;
                switch (mode)
                {
                    case MD_FREE:
                        break;
                    case MD_HORIZONTAL:
                        this.EndPoint = new Vector2f(this.StartPoint.X, this.EndPoint.Y);
                        break;
                    case MD_VERTICAL:
                        this.EndPoint = new Vector2f(this.EndPoint.X, this.StartPoint.Y);
                        break;
                }
                //for simplicity
                Graphics g = this.m_imageGraphics;
                float x1 = this.StartPoint.X;
                float y1 = this.StartPoint.Y;
                float x2 = this.EndPoint.X;
                float y2 = this.EndPoint.Y;
                float distance = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                float angle = CoreMathOperation.GetAngle(this.StartPoint, this.EndPoint);
                Vector2f v_center = new Vector2f(PEN_WIDTH/2.0f, PEN_HEIGHT/2.0f); //CoreMathOperation.GetCenter(m_Pinceau.GetBound ());
                float hw =  v_center.X;
                float hh =  v_center.Y;
                //g.CompositingMode = (CompositingMode)CompositingMode;
                //this.m_Pinceau.Element.CompositingMode = this.CompositingMode;
                //if ((distance == 0) || (m_Step <= 0))
                //{
                //    x2 = (float)(x1);
                //    y2 = (float)(y1);
                    g.TranslateTransform(x2 - hw, y2 - hh, enuMatrixOrder.Append);
                    m_Pinceau.Draw(g);
                    g.TranslateTransform(-x2 + hw, -y2 + hh, enuMatrixOrder.Append);
                    g.Flush();
                    //free hold bitmap
                    //if (this.m_oldBitmap != null)
                    //    m_oldBitmap.Dispose();
                //copy the resulted bitmap to the old
                Rectangle rc = new Rectangle (Point.Empty , m_oldBitmap .Size );
                //IntPtr i_src = g.GetHdc();
                Graphics dg = Graphics.FromImage(m_oldBitmap);
                dg.Clear(Color.Transparent);
                SetGraphicProperty(dg);
                dg.DrawImage(m_newBitmap, Point.Empty);
                //IntPtr dh = dg.GetHdc();
                //CoreBitmapOperation.CopyBitBlt(i_src, dh, 0, 0,
                //    rc.Width, rc.Height, 0, 0);
                //g.ReleaseHdc(i_src);
                //dg.ReleaseHdc(dh);
                dg.Flush();
                dg.Dispose();
                //System.Runtime.InteropServices.Marshal.Copy(bd_src.Scan0 ,
                //    new IntPtr[]{bd_dest.Scan0 }, 0, bd_src.Stride * bd_src.Height);
                //this.m_newBitmap.UnlockBits(bd_src);
                //this.m_oldBitmap.UnlockBits(bd_dest);
                //    return;
                //}
                //if (Step < 0)
                //    Step = 1;
                //if (this.m_list.Count <= 1)
                //    return;
                //GraphicsPath v_path = new GraphicsPath();
                //v_path.AddCurve(this.m_list.ToArray());
                //Matrix v_matrix = new Matrix();
                //v_path.Flatten(v_matrix, Step);
                //foreach (Vector2f pts in this.m_list )
                //{
                //    x2 = (float)(pts.X);
                //    y2 = (float)(pts.Y);
                //    g.TranslateTransform(x2 - hw, y2 - hh, enuMatrixOrder.Append);
                //    m_Pinceau.Draw(g);
                //    g.TranslateTransform(-x2 + hw, -y2 + hh, enuMatrixOrder.Append);
                //}
                //this.m_list.Clear();
            }
            private void SetGraphicProperty(Graphics dg)
            {
                dg.SmoothingMode =(System.Drawing.Drawing2D.SmoothingMode ) this.ImageElement.SmoothingMode;
                dg.CompositingMode = CompositingMode.SourceOver;
                dg.CompositingQuality = this.CompositingQuality;
                dg.InterpolationMode = this.InterpolationMode;
            }
            private InterpolationMode  m_IntermolationMode = InterpolationMode.NearestNeighbor ;
            private CompositingQuality m_CompositingQuality = CompositingQuality.Default ;
            public CompositingQuality CompositingQuality
            {
                get { return m_CompositingQuality; }
                set
                {
                    if (m_CompositingQuality != value)
                    {
                        m_CompositingQuality = value;
                    }
                }
            }
            public InterpolationMode  InterpolationMode
            {
                get { return m_IntermolationMode; }
                set
                {
                    if (m_IntermolationMode != value)
                    {
                        m_IntermolationMode = value;
                    }
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons .None :
                        if (!this.IsShiftKey && !this.IsControlKey)
                        {
                            this.StartPoint = e.FactorPoint;//.Location;
                            this.CurrentSurface.Invalidate();
                        }
                        break;
                    case MouseButtons.Left:
                        if (this.IsShiftKey)
                        {
                            UpdateGetSize(e);
                        }
                        else
                        {
                            //this.EndPoint = e.FactorPoint;
                            //this.StartPoint = e.FactorPoint;
                            //get real point
                            switch (State)
                            {
                                case ST_CONFIGURING:
                                case ST_EDITING:
                                    if (this.ImageElement != null)
                                    {
                                        this.State = ST_CONFIGURING;
                                        UpdatePinceau(e);
                                    }
                                    else
                                        this.State = ST_NONE;
                                    break;
                            }
                        }
                        return;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case MouseButtons.Left:
                        if ((this.State == -11) || this.IsShiftKey)
                        {
                            EndGetSize(e);
                        }
                        else
                        {
                            if (this.m_imageElement == null)
                            {
                                this.SelectOne(e.FactorPoint);
                                if (this.CurrentSurface.CurrentLayer.SelectedElements.Count == 1)
                                {
                                    this.CurrentSurface.ElementToConfigure = this.CurrentSurface.CurrentLayer.SelectedElements[0];
                                }
                                return;
                            }
                            EndPinceau(e);
                        }
                        break;
                }
            }
            protected override void OnPaint(PaintEventArgs e)
            {
                //if (this.m_drawElement == false)
                //    return;
                GraphicsState s = e.Graphics.Save();
                float radius = 0.0f;
                if (this.IsShiftKey)
                {
                    radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                    Rectanglef rc = CurrentSurface.GetScreenBound(CoreMathOperation.GetBounds(this.StartPoint, radius));
                    e.Graphics.DrawRectangle(Pens.Blue, rc.X, rc.Y, rc.Width, rc.Height);
                    e.Graphics.FillEllipse(CoreBrushRegister.GetBrush(Color.FromArgb(120, Color.Black)), rc);
                }
                if (this.m_Pinceau != null)
                {
                    Vector2f pt = this.CurrentSurface.GetScreenLocation (this.StartPoint );
                    GraphicsPath p = this.m_Pinceau.Element.GetPath().Clone () as GraphicsPath ;
                    Matrix m = new Matrix();
                    //half matrix
                    m.Translate(-16, -16);
                    m.Scale (m_PinceauSize/32.0f, m_PinceauSize/32.0f);
                    m.Translate(+16, +16);
                    m.Scale(this.CurrentSurface.ZoomX, this.CurrentSurface.ZoomY);
                    p.Transform(m);
                    Vector2f dx = CoreMathOperation.GetCenter (p.GetBounds());
                    e.Graphics.TranslateTransform(pt.X-dx.X , pt.Y-dx.Y);
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    e.Graphics.FillPath(CoreBrushRegister.GetBrush(Color.FromArgb(150, Color.SkyBlue)), p);
                    e.Graphics.DrawPath(Pens.Blue, p);
                    p.Dispose();
                    m.Dispose();
                    e.Graphics.Restore(s);
                }
                e.Graphics.Restore(s);
            }
            private void EndGetSize(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                float radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);
                this.m_PinceauSize = Math.Max(1.0F, radius*2);
                this.CurrentSurface.Invalidate();
                this.CurrentSurface.ElementToConfigure = this.m_Pinceau;
                this.State = ST_EDITING;
                this.CurrentSurface.Workbench.HelpMessage = CoreSystem.GetString ("MSG.EndGetPencilSize");
            }
            private void BeginGetSize(CoreMouseEventArgs e)
            {
                ClearPinceauTransform();
                this.CurrentSurface.Workbench.HelpMessage = CoreSystem.GetString("MSG.GetPencilSize");
                this.State = -11;
            }
            private void ClearPinceauTransform()
            {
                m_Pinceau.ClearTransform();
            }
            private void UpdateGetSize(CoreMouseEventArgs e)
            {
                float radius = 0.0f;
                this.EndPoint = e.FactorPoint;
                radius = CoreMathOperation.GetDistance(this.EndPoint, this.StartPoint);                
                this.CurrentSurface.Invalidate();
            }
        }
        protected override void GeneratePath()
        {
            //do nothing
        }
        [CoreWorkingObject ("PinceauItem")]
        public class PinceauItem : Core2DDrawingDualBrushBoundElement, ICore2DSymbolElement
        {
            private Core2DDrawingLayeredDualBrushElement  m_element;
            public Core2DDrawingLayeredDualBrushElement Element
            {
                get
                {
                    return this.m_element;
                }
                set
                {
                    if (this.m_element != value)
                    {
                        UnregisterElementEvent();
                        this.m_element = value;
                        RegisterElementEvent();
                    }
                }
            }
            private void RegisterElementEvent()
            {
                if (this.m_element == null)
                    return;
                this.m_element.FillBrush.Copy(this.FillBrush);
                this.m_element.StrokeBrush.Copy(this.StrokeBrush);
            }
            private void UnregisterElementEvent()
            {
                if (this.m_element == null)
                    return;
                this.m_element.Dispose();
            }
            protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs  e)
            {
                base.OnPropertyChanged(e);
                switch( (enu2DPropertyChangedType)e.ID )                
                {
                    case  enu2DPropertyChangedType.BrushChanged :
                    {
                        this.m_element.FillBrush.Copy(this.FillBrush);
                        this.m_element.StrokeBrush.Copy(this.StrokeBrush);
                    }
                    break ;
                    default :
                    this.m_element.SmoothingMode = this.SmoothingMode;
                    break ;
                }
            }
            public PinceauItem(Core2DDrawingLayeredDualBrushElement element):base()
            {                
                this.m_element = element;
                this.InitElement();
            }
            public override void Draw(Graphics g)
            {
                if (this.Element == null)
                    return;
                GraphicsState s = g.Save();
                Matrix m = this.GetMatrix();
                try
                {
                    //go to half before transform to maintain proportionality
                    //g.ResetTransform();
                    g.TranslateTransform(16, 16, enuMatrixOrder.Prepend  );
                    g.MultiplyTransform(m, enuMatrixOrder.Prepend  );
                    g.TranslateTransform(-16, -16, enuMatrixOrder.Prepend );
                    this.Element.Draw(g);
                }
                catch { 
                }
                g.Restore(s);
            }
            protected override void GeneratePath()
            {
                GraphicsPath vpath = new GraphicsPath();
                vpath.AddRectangle (new Rectangle (Point.Empty , new Size (PEN_WIDTH , PEN_HEIGHT )));
                this.SetPath(vpath);
            }
            #region ICore2DSymbolElement Members
            ICore2DDrawingLayeredElement ICore2DSymbolElement.SymbolItem
            {
                get
                {
                    return this.Element;
                }
                set
                {
                    this.Element = value as Core2DDrawingLayeredDualBrushElement ;
                }
            }
            #endregion
        }
    }
}

