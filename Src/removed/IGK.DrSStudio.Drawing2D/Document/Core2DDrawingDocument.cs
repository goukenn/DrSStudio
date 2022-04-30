

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingDocument.cs
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
file:Core2DDrawingDocument.cs
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
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Text;
using System.Drawing.Printing;
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Core.Layers;
    using IGK.DrSStudio.WinUI.Configuration;
using IGK.DrSStudio.Rendering;
    [Serializable()]
    public abstract class Core2DDrawingDocument :
        Core2DDrawingObjectContainer,
        ICore2DDrawingDocument,
        ICoreWorkingConfigurableObject,
        ICoreLayeredDocument,
        ICoreBrushOwner,
        ICoreBitmapView
    {
        private int m_width;
        private int m_height;
        private ICoreBrush m_fillBrush;
        private ICore2DDrawingLayer m_CurrentLayer;
        private enuRenderingMode m_RenderingMode;
        private ICore2DDrawingLayerCollections m_Layers;
        private ICore2DDrawingRegionElement m_ClippedItem;
        private ICoreExtensionContextCollections m_Extensions;
        private bool m_BackgroundTransparent;
        private Colorf m_BackgroundColor;
        private enuSmoothingMode m_SmoothingMode;
        private enuTextRenderingMode m_TextRenderingMode;
        private enuPixelOffset m_PixelOffset;
        private int m_TextContrast;
        private enuInterpolationMode m_InterpolationMode;
        private enuUnitType m_DocumentBaseUnit;
        private ResourceCollections m_Resources;
        private bool m_ClipView;
        protected override ICoreWorkingObjectIdManager CreateIdManager()
        {
            return new Core2DDocumentIdManager();
        }
        [CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(false)]
        public bool ClipView
        {
            get { return m_ClipView; }
            set
            {
                if (m_ClipView != value)
                {
                    m_ClipView = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue("Type:Solid;Color:white;")]       
        public ICoreBrush FillBrush {
            get {
                return this.m_fillBrush;
            }
        }
        public override void Dispose()
        {
            if (this.m_fillBrush != null)
            {
                this.m_fillBrush.Dispose();
                this.m_fillBrush = null;
            }
            base.Dispose();
        }
        //[CoreXMLElement()]
        /// <summary>
        /// Get the resources collections
        /// </summary>
        public ResourceCollections Resources
        {
            get
            {
                return this.m_Resources;
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLElement()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue(enuUnitType.px)]
        /// <summary>
        /// get the document base unit
        /// </summary>
        public enuUnitType DocumentBaseUnit
        {
            get { return m_DocumentBaseUnit; }
            set
            {
                if (value != enuUnitType.percent)
                {
                    if (value != this.m_DocumentBaseUnit)
                    {
                        this.m_DocumentBaseUnit = value;
                        OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    }
                }
            }
        }
        [CoreXMLElement()]
        public ICoreExtensionContextCollections Extensions
        {
            get
            {
                return this.m_Extensions;
            }
        }
        public override object Clone()
        {
            return base.Clone();
        }
        public void SetClip(ICore2DDrawingLayeredElement element)
        {
            this.m_ClippedItem = element;
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuInterpolationMode.Pixel)]
        public enuInterpolationMode InterpolationMode
        {
            get { return m_InterpolationMode; }
            set
            {
                if (m_InterpolationMode != value)
                {
                    m_InterpolationMode = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(4)]
        public int TextContrast
        {
            get { return this.m_TextContrast; }
            set
            {
                if ((this.m_TextContrast != value) && (value > 0))
                {
                    this.m_TextContrast = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuSmoothingMode.None)]
        public enuSmoothingMode SmoothingMode
        {
            get { return m_SmoothingMode; }
            set
            {
                if (m_SmoothingMode != value)
                {
                    m_SmoothingMode = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuTextRenderingMode.AntiAliazed)]
        public enuTextRenderingMode TextRenderingMode
        {
            get { return m_TextRenderingMode; }
            set
            {
                if (m_TextRenderingMode != value)
                {
                    m_TextRenderingMode = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuPixelOffset.HightQuality)]
        public enuPixelOffset PixelOffset
        {
            get { return this.m_PixelOffset; }
            set
            {
                if (this.m_PixelOffset != value)
                {
                    this.m_PixelOffset = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// get if the document can be resize
        /// </summary>
        public virtual bool CanResize { get { return true; } }
        [CoreXMLAttribute()]
        [CoreXMLDefaultColorfValue("White")]
        public Colorf BackgroundColor
        {
            get { return m_BackgroundColor; }
            set
            {
                if (!m_BackgroundColor.Equals(value))
                {
                    m_BackgroundColor = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        public bool BackgroundTransparent
        {
            get { return m_BackgroundTransparent; }
            set
            {
                if (m_BackgroundTransparent != value)
                {
                    m_BackgroundTransparent = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        /// <summary>
        /// get the bound of the document
        /// </summary>
        public Rectanglei Bounds
        {
            get
            {
                return new Rectanglei(0, 0, m_width,
                    m_height);
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(enuRenderingMode.Vector)]
        public enuRenderingMode RenderingMode
        {
            get { return m_RenderingMode; }
            set
            {
                if (this.m_RenderingMode != value)
                {
                    m_RenderingMode = value;
                    OnPropertyChanged(IGK.DrSStudio.CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #region ICore2DDrawingDocument Members
        /// <summary>
        /// get the width of this document
        /// </summary>
        [CoreXMLDefaultAttributeValue(400)]
        public int Width
        {
            get { return this.m_width; }
        }
        /// <summary>
        /// get thte height of this document
        /// </summary>
        [CoreXMLDefaultAttributeValue(300)]
        public int Height
        {
            get { return this.m_height; }
        }
        /// <summary>
        /// get or set the current layer
        /// </summary>
        public virtual ICore2DDrawingLayer CurrentLayer
        {
            get
            {
                if ((this.m_CurrentLayer == null))
                {
                    if (this.Layers.Count > 0)
                    {
                        this.m_CurrentLayer = this.Layers[0];
                        this.RegisterLayerEvent(this.m_CurrentLayer);
                    }
                    else
                    {
                        return null;
                    }
                }
                return this.m_CurrentLayer;
            }
            set
            {
                if (value == null) return;
                if (this.Layers.Contains(value))
                {
                    if (this.m_CurrentLayer != value)
                    {
                        Core2DDrawingLayerChangedEventArgs e = new Core2DDrawingLayerChangedEventArgs(
                            this.m_CurrentLayer, value);
                        this.m_CurrentLayer = value;
                        OnCurrentLayerChanged(e);
                    }
                }
            }
        }
        protected virtual void OnCurrentLayerChanged(Core2DDrawingLayerChangedEventArgs e)
        {
            if (this.CurrentLayerChanged != null)
                this.CurrentLayerChanged(this, e);
        }
        protected override void OnParentChanged(EventArgs eventArgs)
        {
            if (this.Parent != null)
            {
                ICore2DDrawingSurface c = (this.Parent as ICore2DDrawingSurface);
                if (c != null)
                {//check that all element with id not the document
                    foreach (ICore2DDrawingDocument doc in c.Documents)
                    {
                        if ((doc.Id == this.Id) && (doc != this))
                        {
                            this.Id = null;
                        }
                    }
                }
            }
            base.OnParentChanged(eventArgs);
        }
        //[CoreXMLElement ()]
        public ICore2DDrawingLayerCollections Layers
        {
            get
            {
                if (this.m_Layers == null)
                {
                    ICore2DDrawingLayerCollections v_layer = CreateLayerCollections();
                    if (v_layer == null)
                        throw new IGK.DrSStudio.CoreException(
                             IGK.DrSStudio.enuExceptionType.OperationNotValid,
                             IGK.DrSStudio.CoreConstant.ERR_DOCUMENT_MUSTCREATEALAYER);
                    this.m_Layers = v_layer;
                }
                return this.m_Layers;
            }
        }
        public bool IsClipped
        {
            get
            {
                return (this.m_ClippedItem != null);
            }
        }
        public ICoreGraphicsRegion GetClippedRegion()
        {
            if (this.IsClipped)
                return this.m_ClippedItem.GetRegion();
            return null;
        }
        #endregion
        #region ICoreWorkingPositionableObject Members
        public int ZIndex
        {
            get
            {
                if (Parent is ICore2DDrawingSurface)
                {
                    return (Parent as ICore2DDrawingSurface).Documents.IndexOf(this);
                }
                return -1;
            }
        }
        /// <summary>
        /// raize the ZIndexChanged event
        /// </summary>
        /// <param name="eventArgs"></param>
        protected virtual void OnLayerZIndexChanged(CoreWorkingObjectZIndexChangedArgs e)
        {
            if (this.LayerZIndexChanged != null)
                this.LayerZIndexChanged(this, e);
        }
        public event CoreWorkingObjectZIndexChangedHandler LayerZIndexChanged;
        #endregion
        public virtual void Draw(Graphics graphic)
        {
            lock (this)
            {
                GraphicsState v_s = graphic.Save();
                SetGraphicsProperty(graphic);
                if (this.ClipView)
                {
                    Rectanglei v_rc = new Rectanglei(0, 0, this.Width, this.Height);
                    Region rg = new Region(v_rc);
                    graphic.Clip = rg;
                }
                switch (this.RenderingMode)
                {
                    case enuRenderingMode.Pixel:
                        //create a offscreen bmp
                        Bitmap v_img = new Bitmap((int)this.Width, (int)this.Height);
                        Graphics v_g = Graphics.FromImage(v_img);
                        Rectangle v_rc = new Rectangle(Point.Empty, v_img.Size);
                        if (this.IsClipped)
                        {
                            v_g.Clip = this.GetClippedRegion();
                        }
                        this._RenderBackground(graphic);
                        foreach (ICore2DDrawingLayer layer in this.Layers)
                        {
                            v_g.Clear(Color.Empty);
                            if (layer.View)
                            {
                                layer.Draw(v_g);
                                graphic.DrawImage(v_img,
                                    v_rc,
                                    0,
                                    0,
                                    v_rc.Width,
                                    v_rc.Height,
                                    GraphicsUnit.Pixel,
                                    layer.GetImageAttributes());
                            }
                            else
                            {
                                graphic.DrawImage(v_img,
                                  v_rc);
                            }
                        }
                        v_g.Dispose();
                        v_img.Dispose();
                        break;
                    case enuRenderingMode.Vector:
                    default:
                        if (this.IsClipped)
                        {
                            graphic.Clip = this.GetClippedRegion();
                        }
                        this._RenderBackground(graphic);
                        foreach (ICore2DDrawingLayer layer in this.Layers)
                        {
                            if (!layer.View)
                                continue;
                            layer.Draw(graphic);
                        }
                        break;
                }
                graphic.Restore(v_s);
            }
        }
        private void _RenderBackground(Graphics graphic)
        {
            if (this.BackgroundTransparent == false)
            {
                //
                graphic.FillRectangle(
                    this.m_fillBrush.GetBrush(),
                    new Rectangle(Point.Empty, new Size(this.Width, this.Height)));
            }
        }
        public void Draw(Graphics graphic, Rectanglei rectangle)
        {
            Draw(graphic, true, rectangle, enuFlipMode.None);
        }
        GraphicsState BeginDraw(Graphics g, bool proportional,
            Rectanglei rectangle, enuFlipMode flipmode)
        {
            if (rectangle.IsEmpty)
                return null;
            float fx = 0.0f;
            float fy = 0.0f;
            fx = rectangle.Width / (float)this.Width;
            fy = rectangle.Height / (float)this.Height;
            if ((fx == 0) || (fy == 0))
                return null;
            //for saving
            GraphicsState s = g.Save();
            Matrix m = new Matrix();
            //g.ResetTransform();
            //g.TranslateTransform(-rectangle.X, -rectangle.Y, enuMatrixOrder.Append);
            //this.ZoomX = fx;
            //this.ZoomY = fy;
            //this.PosX = rectangle.X;
            //this.PosY = rectangle.Y;
            switch (flipmode)
            {
                case enuFlipMode.None:
                    break;
                case enuFlipMode.FlipVertical:
                    fy *= -1;
                    break;
                case enuFlipMode.FlipHorizontal:
                    fx *= -1;
                    break;
                default:
                    break;
            }
            if (proportional == false)
            {
                m.Scale(fx, fy, enuMatrixOrder.Append);
                m.Translate(rectangle.X, rectangle.Y, enuMatrixOrder.Append);
                switch (flipmode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                fx = Math.Min(fx, fy);
                fy = fx;
                m.Scale(fx, fy, enuMatrixOrder.Append);
                int posx = (int)(((-this.Width * fx) / 2.0f) + (rectangle.Width / 2.0f));
                int posy = (int)(((-this.Height * fx) / 2.0f) + (rectangle.Height / 2.0f));
                m.Translate(posx + rectangle.X, posy + rectangle.Y, enuMatrixOrder.Append);
                switch (flipmode)
                {
                    case enuFlipMode.None:
                        break;
                    case enuFlipMode.FlipVertical:
                        m.Translate(0, rectangle.Height, enuMatrixOrder.Append);
                        break;
                    case enuFlipMode.FlipHorizontal:
                        m.Translate(rectangle.Width, 0, enuMatrixOrder.Append);
                        break;
                    default:
                        break;
                }
            }
            g.MultiplyTransform(m);
            m.Dispose();
            return s;
        }
        void EndDraw(Graphics g, GraphicsState state)
        {
            g.Restore(state);
        }
        public void Draw(Graphics g, bool proportional,
            Rectanglei rectangle, enuFlipMode flipmode)
        {
            GraphicsState state = BeginDraw(g, proportional, rectangle, flipmode);
            this.Draw(g);
            EndDraw(g, state);
        }
//        public virtual void Draw(ICoreGraphicsDevice g, bool proportional,
//        Rectanglei rectangle, enuFlipMode flipmode)
//        {
//#if WINDOW
//            ICore2DGdiGraphicsDevice device = g as ICore2DGdiGraphicsDevice;
//            if (device != null)
//            {
//                GraphicsState st = BeginDraw(device.Graphics, proportional, rectangle, flipmode);
//                this.Draw(device);
//                EndDraw(device.Graphics, st);
//            }
//#endif
//            if (rectangle.IsEmpty)
//                return;
//        }
        public void Draw(Graphics g, bool proportional, int with, int height, enuFlipMode flipMode)
        {
            this.Draw(g, proportional, new Rectanglei(0, 0, with, height), flipMode);
        }
        #region ICore2DDrawingDocument Members
        public event Core2DDrawingLayerChangedEventHandler CurrentLayerChanged;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="width">width unit </param>
        /// <param name="height">height unit</param>
        public Core2DDrawingDocument(CoreUnit width, CoreUnit height)
            : this()
        {
            this.m_width = (int)Math.Round((width as ICoreUnitPixel).Value);
            this.m_height = (int)Math.Round((height as ICoreUnitPixel).Value);
            if (this.m_width == 0)
                throw new ArgumentException("width");
            if (this.m_height == 0)
                throw new ArgumentException("height");
        }
        /// <summary>
        /// .ctr static contructor
        /// </summary>
        public Core2DDrawingDocument()
        {
            if (CoreSystem.Instance.Settings["Drawing2D"] != null)
            {
                var s = CoreSystem.Instance.Settings["Drawing2D"];
                this.m_height = Convert.ToInt32(s["DefaultHeight"].Value == null ? 300 : s["DefaultHeight"].Value);// 300;
                this.m_width = Convert.ToInt32(s["DefaultWidth"].Value == null ? 400 : s["DefaultWidth"].Value);//400
            }
            else
            {
                this.m_width = 400;
                this.m_height = 300;
            }
            this.m_fillBrush = new CoreBrush(this);
            this.m_fillBrush.SetSolidColor(Colorf.White);
            this.m_DocumentBaseUnit = enuUnitType.px;
            this.m_BackgroundColor = Colorf.White;
            this.m_TextContrast = 4;
            this.m_TextRenderingMode = enuTextRenderingMode.AntiAliazed;
            this.m_SmoothingMode = enuSmoothingMode.None;
            this.m_RenderingMode = enuRenderingMode.Vector;
            this.m_PixelOffset = enuPixelOffset.HightQuality;
            this.m_InterpolationMode = enuInterpolationMode.Pixel;
            this.LayerAdded += _LayerAddOrRemoved;
            this.LayerAdded += Core2DDrawingDocument_LayerAdded;
            this.LayerRemoved += _LayerAddOrRemoved;
            this.LayerRemoved += Core2DDrawingDocument_LayerRemoved;
            this.CurrentLayerChanged += new Core2DDrawingLayerChangedEventHandler(Core2DDrawingDocument_CurrentLayerChanged);
            this.m_Extensions = CreateExtionsCollection();
            this.m_Resources = new ResourceCollections(this);
            this.m_fillBrush.BrushDefinitionChanged += m_fillBrush_BrushDefinitionChanged;
        }
        void Core2DDrawingDocument_LayerRemoved(object o, Core2DDrawingLayerEventArgs e)
        {
            this.IdManager.Unregister(e.Layer);
        }
        void Core2DDrawingDocument_LayerAdded(object o, Core2DDrawingLayerEventArgs e)
        {
            this.IdManager.Register(e.Layer);
        }
        void m_fillBrush_BrushDefinitionChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
        }
        void Core2DDrawingDocument_CurrentLayerChanged(object o, Core2DDrawingLayerChangedEventArgs e)
        {
            if (e.OldLayer != null)
                UnRegisterLayerEvent(e.OldLayer);
            if (e.NewLayer != null)
                RegisterLayerEvent(e.NewLayer);
        }
        private void UnRegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(_ClayerPropertyChanged);
            layer.ElementRemoved -= layer_ElementRemoved;
        }
        private void RegisterLayerEvent(ICore2DDrawingLayer layer)
        {
            layer.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(_ClayerPropertyChanged);
            layer.ElementRemoved += layer_ElementRemoved;
        }
        void layer_ElementRemoved(object sender, CoreWorkingObjectEventArgs<ICore2DDrawingLayeredElement> e)
        {
            if (this.IsClipped && (e.Element == this.m_ClippedItem))
            {
                this.m_ClippedItem = null;
            }
        }
        void _ClayerPropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        void _LayerAddOrRemoved(object o, Core2DDrawingLayerEventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        //public static Core2DDrawingDocument CreateDocument(ICore2DDrawingSurface surface)
        //{
        //    Core2DDrawingDocument v_document =
        //        new Core2DDrawingDocument();
        //    v_document.Parent = v_document;
        //    return v_document;
        //}
        protected virtual ICore2DDrawingLayerCollections CreateLayerCollections()
        {
            return new Core2DDrawingLayerCollections(this);
        }
        protected virtual ICore2DDrawingLayer CreateNewLayer()
        {
            return new Core2DDrawingLayer();
        }
        public void SetSize(int width, int height)
        {
            if (!CanResize)
                return;
            bool v_w = (width > 0) && (this.m_width != width);
            bool v_h = (height > 0) && (this.m_height != height);
            bool v_raise = v_w || v_h;
            if (v_w) this.m_width = width;
            if (v_h) this.m_height = height;
            if (v_raise)
                OnPropertyChanged(
                    new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.SizeChanged)
                    );
        }
        /// <summary>
        /// change the size for descendant only. canresize is fale
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected void SetPrimarySize(CoreUnit width, CoreUnit height)
        {
            int w = (int)Math.Floor((width as ICoreUnitPixel).Value);
            int h = (int)Math.Floor((height as ICoreUnitPixel).Value);
            bool v_w = (w > 0) && (this.m_width != w);
            bool v_h = (h > 0) && (this.m_height != h);
            bool v_raise = v_w && v_h;
            if (v_w) this.m_width = w;
            if (v_h) this.m_height = h;
            if (v_raise)
                OnPropertyChanged(
                    new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.SizeChanged)
                    );
        }
        protected virtual void OnLayerAdded(Core2DDrawingLayerEventArgs e)
        {
            if (this.LayerAdded != null)
                this.LayerAdded(this, e);
        }
        internal void OnLayerRemoved(Core2DDrawingLayerEventArgs e)
        {
            if ((e.Layer == this.CurrentLayer) && (this.Layers.Count > 0))
            {
                this.CurrentLayer = this.Layers[0];
            }
            if (this.LayerRemoved != null)
                this.LayerRemoved(this, e);
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
            int w = Convert.ToInt32(xreader.GetAttribute("Width"));
            int h = Convert.ToInt32(xreader.GetAttribute("Height"));
            this.SetSize(w, h);
            string clipId = xreader.GetAttribute("Clipped");
            if (!string.IsNullOrEmpty(clipId))
            {
                this.LoadingComplete += new EventHandler(new LoadClip(this, clipId).LoadComplete);
            }
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (ICore2DDrawingLayer item in this.Layers)
            {
                item.Serialize(xwriter);
            }
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
            if (this.IsClipped)
                xwriter.WriteAttributeString("Clipped", "#" + this.m_ClippedItem.Id);
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            List<ICore2DDrawingLayer> v_list = new List<ICore2DDrawingLayer>();
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        ICore2DDrawingLayer
                                   obj = xreader.CreateWorkingObject(xreader.Name)
                                   as ICore2DDrawingLayer;
                        if (obj != null)
                        {
                            v_list.Add(obj);
                            obj.Deserialize(xreader.ReadSubtree());
                        }
                        else
                        {
                            CoreXMLSerializerUtility.ReadElements(this, xreader.ReadSubtree(), null);
                        }
                        break;
                }
            }
            if (v_list.Count > 0)
                this.Layers.Replace(v_list.ToArray());
        }
        #region ICore2DDrawingDocument Members
        public event Core2DDrawingLayerEventHandler LayerAdded;
        public event Core2DDrawingLayerEventHandler LayerRemoved;
        #endregion
        #region ICore2DDrawingDocument Members
        #endregion
        #region ICore2DDrawingDesignRendering Members
        public virtual void RenderSelection(Graphics graphics, ICore2DDrawingSurface surface)
        {
            this.CurrentLayer.RenderSelection(graphics, surface);
        }
        #endregion
        #region ICore2DDrawingDocument Members
        /// <summary>
        /// retreive the first element with the id
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public virtual ICore2DDrawingObject GetElementById(string p)
        {
            if (string.IsNullOrEmpty(p))
                return null;
            p = p.Replace("#", "");
            ICore2DDrawingObject v_obj = null;
            ICore2DDrawingObject v_p = null;
            foreach (ICore2DDrawingLayer l in this.Layers)
            {
                if (l.Id == p) return l;
                if ((v_p = l.GetElementById(p)) != null) return v_p;
            }
            return v_obj;
        }
        #endregion
        public void SetGraphicsProperty(Graphics graphics)
        {
            graphics.SmoothingMode = (SmoothingMode)this.SmoothingMode;
            graphics.TextContrast = this.TextContrast;
            graphics.TextRenderingHint = (TextRenderingHint)TextRenderingMode;
            graphics.PixelOffsetMode = (PixelOffsetMode)this.PixelOffset;
            graphics.InterpolationMode = (InterpolationMode)this.InterpolationMode;
        }
        public class Core2DDrawingLayerCollections :
            ICore2DDrawingLayerCollections,
            ICoreLayerCollections,
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer,
            ICoreWorkingPositionableObjectContainer<ICore2DDrawingLayer>
        {
            List<ICore2DDrawingLayer> m_layers;
            Core2DDrawingDocument m_document;
            /// <summary>
            /// get the document that is the parent of this collections
            /// </summary>
            public Core2DDrawingDocument Document { get { return this.m_document; } }
            /// <summary>
            /// get the index of the layer in current collection
            /// </summary>
            /// <param name="layer"></param>
            /// <returns></returns>
            public int IndexOf(ICore2DDrawingLayer layer)
            {
                return m_layers.IndexOf(layer);
            }
            public Core2DDrawingLayerCollections(Core2DDrawingDocument document)
            {
                this.m_document = document;
                this.m_layers = new List<ICore2DDrawingLayer>();
                this.InitDefaultCollection();
            }
            /// <summary>
            /// init default layer collection. use AddLayer function do layer collection
            /// </summary>
            protected virtual void InitDefaultCollection()
            {
                ICore2DDrawingLayer layer = this.Document.CreateNewLayer();
                this.AddLayer(layer);
            }
            public bool Contains(ICore2DDrawingLayer layer)
            {
                return this.m_layers.Contains(layer);
            }
            public void Add(ICore2DDrawingLayer layer)
            {
                if (this.AllowMultiLayers && !this.Contains(layer))
                {
                    this.AddLayer(layer);
                }
            }
            protected virtual void AddLayer(ICore2DDrawingLayer layer)
            {
                if (layer != null)
                {
                    this.m_layers.Add(layer);
                    RegisterLayer(layer);
                    this.m_document.OnLayerAdded(new Core2DDrawingLayerEventArgs(layer, IndexOf(layer)));
                }
            }
            private void RegisterLayer(ICore2DDrawingLayer layer)
            {
                layer.Parent = this.m_document;
                layer.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(layer_PropertyChanged);
            }
            void layer_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                //raise the document layer event
                this.m_document.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.LayerChanged));
            }
            public void Remove(ICore2DDrawingLayer layer)
            {
                if (this.m_layers.Contains(layer))
                {
                    int index = IndexOf(layer);
                    this.m_layers.Remove(layer);
                    UnregisterLayer(layer);
                    this.m_document.OnLayerRemoved(new Core2DDrawingLayerEventArgs(layer, index));
                }
            }
            private void UnregisterLayer(ICore2DDrawingLayer layer)
            {
                this.m_document.UnRegisterLayerEvent(layer);
                layer.Parent = null;
                layer.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(layer_PropertyChanged);
            }
            public int Count
            {
                get { return this.m_layers.Count; }
            }
            public ICore2DDrawingLayer this[int index]
            {
                get { return this.m_layers[index]; }
            }
            public ICore2DDrawingLayer this[string id]
            {
                get
                {
                    foreach (ICore2DDrawingLayer item in this.m_layers)
                    {
                        if (item.Id == id)
                            return item;
                    }
                    return null;
                }
            }
            public void MoveToStart(ICore2DDrawingLayer layer)
            {
                IGK.DrSStudio.Core.Utils.Collections<ICore2DDrawingLayer>.MoveToStart(this.m_layers, layer);
            }
            public void MoveToEnd(ICore2DDrawingLayer iCore2DDrawingLayer)
            {
                throw new NotImplementedException();
            }
            public void MoveAt(ICore2DDrawingLayer iCore2DDrawingLayer, int index)
            {
                throw new NotImplementedException();
            }
            public void MoveToFront(ICore2DDrawingLayer layer)
            {
                if (this.Contains(layer))
                {
                    //back up the current layer
                    int i = layer.ZIndex;
                    if (i < Count - 1)
                    {
                        m_layers.Remove(layer);
                        m_layers.Insert(i + 1, layer);
                        this.m_document.OnLayerZIndexChanged(
                   new CoreWorkingObjectZIndexChangedArgs(
                       layer,
                       i,
                       layer.ZIndex));
                    }
                }
            }
            public void MoveToBack(ICore2DDrawingLayer layer)
            {
                if (this.Contains(layer))
                {
                    //save the index
                    int i = layer.ZIndex;
                    if (i > 0)
                    {
                        m_layers.Remove(layer);
                        m_layers.Insert(i - 1, layer);
                        this.m_document.OnLayerZIndexChanged(
                            new CoreWorkingObjectZIndexChangedArgs(
                                layer,
                                i,
                                layer.ZIndex));
                    }
                }
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_layers.GetEnumerator();
            }
            #endregion
            public void Replace(ICore2DDrawingLayer[] layers)
            {
                if (layers.Length > 0)
                {
                    if (this.m_document.LayerRemoved != null)
                    {
                        ICore2DDrawingLayer[] v_l = this.m_layers.ToArray();
                        this.m_layers.Clear();
                        for (int i = 0; i < v_l.Length; i++)
                        {
                            this.m_document.OnLayerRemoved(new Core2DDrawingLayerEventArgs(v_l[i], i));
                        }
                    }
                    this.m_layers.Clear();
                    foreach (ICore2DDrawingLayer item in layers)
                    {
                        this.AddLayer(item);
                    }
                    //change the current layer
                    this.m_document.CurrentLayer = layers[0];
                }
            }
            #region ICore2DDrawingLayerCollections Members
            public bool Contains(string id)
            {
                return this[id] != null;
            }
            #endregion
            #region ICore2DDrawingLayerCollections Members
            public ICore2DDrawingLayer[] ToArray()
            {
                return this.m_layers.ToArray();
            }
            #endregion
            #region ICoreLayerCollections Members
            public virtual bool AllowMultiLayers
            {
                get { return true; }
            }
            void ICoreLayerCollections.Add(ICoreLayer layer)
            {
                this.Add(layer as ICore2DDrawingLayer);
            }
            void ICoreLayerCollections.Remove(ICoreLayer layer)
            {
                this.Remove(layer as ICore2DDrawingLayer);
            }
            int ICoreLayerCollections.IndexOf(ICoreLayer layer)
            {
                return this.IndexOf(layer as Core2DDrawingLayer);
            }
            ICoreLayer[] ICoreLayerCollections.ToArray()
            {
                return this.m_layers.ToArray().ConvertTo<ICoreLayer>();
            }
            #endregion
            #region ICoreWorkingPositionableObjectContainer Members
            void ICoreWorkingPositionableObjectContainer.MoveToBack(ICoreWorkingPositionableObject item)
            {
                this.MoveToBack(item as ICore2DDrawingLayer);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToFront(ICoreWorkingPositionableObject item)
            {
                this.MoveToFront(item as ICore2DDrawingLayer);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToStart(ICoreWorkingPositionableObject item)
            {
                this.MoveToStart(item as ICore2DDrawingLayer);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToEnd(ICoreWorkingPositionableObject item)
            {
                this.MoveToEnd(item as ICore2DDrawingLayer);
            }
            void ICoreWorkingPositionableObjectContainer.MoveAt(ICoreWorkingPositionableObject item, int index)
            {
                this.MoveAt(item as ICore2DDrawingLayer, index);
            }
            #endregion
            #region ICoreWorkingPositionableObjectContainer Members
            public int IndexOf(ICoreWorkingPositionableObject item)
            {
                return this.m_layers.IndexOf(item as ICore2DDrawingLayer);
            }
            #endregion
            #region ICore2DDrawingLayerCollections Members
            /// <summary>
            /// insert layer at spécified position
            /// </summary>
            /// <param name="zindex"></param>
            /// <param name="iCore2DDrawingLayer"></param>
            public void InsertAt(int zindex, ICore2DDrawingLayer layer)
            {
                if ((layer == null) || this.Contains(layer))
                    return;
                this.m_layers.Add(layer);
                this.RegisterLayer(layer);
                this.m_document.OnLayerAdded(new Core2DDrawingLayerEventArgs(layer, IndexOf(layer)));
            }
            #endregion
        }
        public void SetClippedElement(ICore2DDrawingRegionElement element)
        {
            if ((element != null) && (this.m_ClippedItem != element))
            {
                this.m_ClippedItem = element;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public void RemoveClippedElement()
        {
            if (this.m_ClippedItem != null)
            {
                this.m_ClippedItem = null;
                OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        #region ICore2DDrawingDocument Members
        public ICore2DDrawingLayer AddNewLayer()
        {
            ICore2DDrawingLayer l = CreateNewLayer();
            if (l != null)
                this.Layers.Add(l);
            return l;
        }
        #endregion
        /// <summary>
        /// used to load document clip
        /// </summary>
        internal class LoadClip
        {
            Core2DDrawingDocument m_document;
            string m_id;
            public LoadClip(Core2DDrawingDocument document, string id)
            {
                this.m_document = document;
                this.m_id = id;
            }
            internal void LoadComplete(object o, EventArgs e)
            {
                this.m_document.SetClip(
                    this.m_document.GetElementById(this.m_id) as ICore2DDrawingLayeredElement);
                this.m_document.LoadingComplete -= LoadComplete;
            }
        }
        #region ICore2DDrawingDocument Members
        public virtual void Clear()
        {
            //remove all layer 
            ICore2DDrawingLayer[] v_tab = this.Layers.ToArray();
            ICore2DDrawingLayer c_l = this.CurrentLayer;
            for (int i = 0; i < v_tab.Length; i++)
            {
                if (c_l == v_tab[i])
                {
                    c_l.Clear();
                }
                else
                {
                    //remove extra layer
                    //------------------
                    this.Layers.Remove(v_tab[i]);
                }
            }
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public virtual enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public virtual ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            CoreParameterChangedEventHandler d = (object o, CoreParameterChangedEventArgs e) =>
        {
            switch (e.Item.Name)
            {
                case "Width":
                    if (e.Value != null)
                    {
                        CoreUnit unit = e.Value.ToString();
                        this.SetSize((int)((ICoreUnitPixel)unit).Value, this.Height);
                    }
                    break;
                case "Height":
                    if (e.Value != null)
                    {
                        CoreUnit unit = e.Value.ToString();
                        this.SetSize(this.Width, (int)((ICoreUnitPixel)unit).Value);
                    }
                    break;
                case "BackgroundTransparent":
                    this.BackgroundTransparent = Convert.ToBoolean(e.Value);
                    break;
            }
        };
            ICoreParameterGroup group =
            parameters.AddGroup(CoreConstant.PARAM_DEFINITION, string.Format(CoreConstant.LB_CAPTION, CoreConstant.PARAM_DEFINITION));
            group.AddItem(GetType().GetProperty("Id"));
            group.AddItem("BackgroundTransparent", "lb.BackgroundTransparent", this.BackgroundTransparent, enuParameterType.Bool, d);
            group.AddItem("ClipView", "lb.Document.ClipView", this.ClipView, enuParameterType.Bool, (object o, CoreParameterChangedEventArgs e) =>
            {
                this.ClipView = Convert.ToBoolean(e.Value);
            });
            group = parameters.AddGroup("Size", "lb.Size");
            group.AddItem("Width", "lb.Width", ((CoreUnit)this.Width.ToString()).ToString(enuUnitType.px), enuParameterType.Text, d);
            group.AddItem("Height", "lb.Height", ((CoreUnit)this.Height.ToString()).ToString(enuUnitType.px), enuParameterType.Text, d);
            group = parameters.AddGroup("GraphicsProperty", "lb.GraphicsProperties");
            group.AddItem(GetType().GetProperty("RenderingMode"));
            group.AddItem(GetType().GetProperty("SmoothingMode"));
            group.AddItem(GetType().GetProperty("InterpolationMode"));
            group.AddItem(GetType().GetProperty("PixelOffset"));
            group.AddItem(GetType().GetProperty("TextRenderingMode"));
            group.AddItem(GetType().GetProperty("TextContrast"));
            return parameters;
        }
        public virtual ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
        #region ICore2DDrawingDocument Members
        bool ICore2DDrawingDocument.ChangeIdOf(ICore2DDrawingLayer layer, string p)
        {
            //return this.ChangeIdOf(layer as Core2DDrawingLayer , p);
            throw new NotImplementedException();
        }
        //public virtual bool ChangeIdOf(Core2DDrawingLayer layer, string p)
        //{
        //    throw new NotImplementedException();
        //    if ( (layer !=null) && !this.m_Layers.Contains(layer) || this.m_Layers.Contains(p))
        //        return false;
        //    if (!(this.IdManager  != null) && (this.IdManager.ChangeId(layer, p)))
        //    {
        //        return false;
        //    }
        //    Core2DDrawingLayer d = layer as Core2DDrawingLayer;
        //    d.Id = p;
        //    return true;
        //}
        #endregion
        /// <summary>
        /// translate all element of this document
        /// </summary>
        /// <param name="y"></param>
        /// <param name="y"></param>
        public void Translate(float x, float y)
        {
            foreach (ICore2DDrawingLayer layer in this.Layers)
            {
                layer.Translate(x, y);
            }
        }
        protected virtual ICoreExtensionContextCollections CreateExtionsCollection()
        {
            return new Core2DDrawingDocumentExtensionCollections(this);
        }
        #region ICoreLayeredDocument Members
        ICoreLayer ICoreLayeredDocument.CurrentLayer
        {
            get
            {
                return this.CurrentLayer;
            }
            set
            {
                this.CurrentLayer = value as ICore2DDrawingLayer;
            }
        }
        ICoreLayerCollections ICoreLayeredDocument.Layers
        {
            get { return this.Layers; }
        }
        #endregion
        #region ICoreWorkingPositionableObject Members
        ICoreWorkingPositionableObjectContainer ICoreWorkingPositionableObject.Container
        {
            get
            {
                return this.Parent as ICoreWorkingPositionableObjectContainer;
            }
        }
        public virtual void MoveBack()
        {
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer obj = (this.Parent as IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer);
            if (obj != null)
            {
                obj.MoveToBack(this);
            }
        }
        public virtual void MoveFront()
        {
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer obj = (this.Parent as IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer);
            if (obj != null)
            {
                obj.MoveToFront(this);
            }
        }
        public virtual void MoveStart()
        {
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer obj = (this.Parent as IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer);
            if (obj != null)
            {
                obj.MoveToStart(this);
            }
        }
        public virtual void MoveEnd()
        {
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer obj = (this.Parent as IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer);
            if (obj != null)
            {
                obj.MoveToEnd(this);
            }
        }
        public virtual void MoveAt(int i)
        {
            IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer obj = (this.Parent as IGK.DrSStudio.Core.ICoreWorkingPositionableObjectContainer);
            if (obj != null)
            {
                obj.MoveAt(this, i);
            }
        }
        #endregion
        /// <summary>
        /// represent the base document extension collections
        /// </summary>
        public class Core2DDrawingDocumentExtensionCollections :
            ICoreExtensionContextCollections,
            ICoreSerializerService
        {
            Core2DDrawingDocument m_owner;
            Dictionary<string, ICoreExtensionContext> m_items;
            public Core2DDrawingDocumentExtensionCollections(Core2DDrawingDocument owner)
            {
                this.m_owner = owner;
                this.m_items = new Dictionary<string, ICoreExtensionContext>();
            }
            private bool m_IsLoading;
            public bool IsLoading
            {
                get { return m_IsLoading; }
            }
            public event EventHandler LoadingComplete;
            ///<summary>
            ///raise the LoadingComplete 
            ///</summary>
            protected virtual void OnLoadingComplete(EventArgs e)
            {
                if (LoadingComplete != null)
                    LoadingComplete(this, e);
            }
            #region ICoreExtensionContextCollections Members
            public ICoreExtensionContext this[string key]
            {
                get
                {
                    if (this.m_items.ContainsKey(key))
                        return m_items[key];
                    return null;
                }
            }
            public string[] ContextNames()
            {
                return this.m_items.Keys.ToArray<string>();
            }
            public int Count
            {
                get { return this.m_items.Count; }
            }
            public void Add(ICoreExtensionContext extension)
            {
                if ((extension == null) || !this.SupportExtension(extension))
                    return;
                if (!this.m_items.ContainsKey(extension.Id))
                {
                    this.m_items.Add(extension.Id, extension);
                    extension.Target = this.m_owner;
                }
            }
            public void Remove(ICoreExtensionContext extension)
            {
                if (extension == null)
                    return;
                if (this.m_items.ContainsKey(extension.Id))
                {
                    this.m_items.Remove(extension.Id);
                    extension.Target = null;
                }
            }
            public virtual bool SupportExtension(string name)
            {
                return true;
            }
            public virtual bool SupportExtension(ICoreExtensionContext extension)
            {
                return true;
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
            public void Serialize(IXMLSerializer xwriter)
            {
                foreach (KeyValuePair<string, ICoreExtensionContext> i in this)
                {
                    if (i.Value.IsActive)
                    {
                        xwriter.WriteStartElement(i.Key);
                        i.Value.Serialize(xwriter);
                        xwriter.WriteEndElement();
                    }
                }
            }
            public void Deserialize(IXMLDeserializer xreader)
            {
                this.m_IsLoading = true;
                IXMLDeserializer d = xreader.ReadSubtree();
                ICoreExtensionContext context = null;
                this.m_items.Clear();
                while (d.Read())
                {
                    switch (d.NodeType)
                    {
                        case System.Xml.XmlNodeType.Element:
                            context = CoreSystem.CreateWorkingObject(d.Name) as ICoreExtensionContext;
                            if (context != null)
                            {
                                this.Add(context);
                                context.Deserialize(xreader);
                            }
                            break;
                    }
                }
                this.m_IsLoading = false;
                OnLoadingComplete(EventArgs.Empty);
            }
            public string Id
            {
                get { return null; }
            }
            #endregion
        }
        /// <summary>
        /// represent a resources collection
        /// </summary>
        public class ResourceCollections :
            ICoreSerializerService,
            IEnumerable
        {
            Core2DDrawingDocument m_document;
            Dictionary<string, ICore2DDrawingResource> m_resources;
            /// <summary>
            /// .ctr represent a resources collection
            /// </summary>
            /// <param name="document"></param>
            public ResourceCollections(Core2DDrawingDocument document)
            {
                this.m_document = document;
                this.m_resources = new Dictionary<string, ICore2DDrawingResource>();
            }
            public void Add(ICore2DDrawingResource resource)
            {
                if ((resource == null) || (m_resources.ContainsKey(resource.Name))) return;
                m_resources.Add(resource.Name, resource);
            }
            /// <summary>
            /// get the drawing 2d resources
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public ICore2DDrawingResource this[string key]
            {
                get
                {
                    if (this.m_resources.ContainsKey(key))
                        return this.m_resources[key];
                    return null;
                }
            }
            /// <summary>
            /// serialize the resources
            /// </summary>
            /// <param name="xwriter"></param>
            public void Serialize(IXMLSerializer xwriter)
            {
            }
            /// <summary>
            /// deserialize the resources
            /// </summary>
            /// <param name="xreader"></param>
            public void Deserialize(IXMLDeserializer xreader)
            {
            }
            public string Id
            {
                get { return string.Empty; }
            }
            /// <summary>
            /// get the values enumerator
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return this.m_resources.Values.GetEnumerator();
            }
        }
        /// <summary>
        /// override if this document allow multi layer
        /// </summary>
        public bool AllowMultiLayer
        {
            get { return this.Layers.AllowMultiLayers; }
        }
        public virtual Type DefaultSurfaceType
        {
            get { return CoreSystem.GetWorkingObjectType(CoreConstant.DRAWING2D_SURFACE); }
        }
        public Rectanglef GetBound()
        {
            return GetDefaultBound();
        }
        public Rectanglef GetDefaultBound()
        {
            return new Rectangle(0, 0, this.Width, this.Height);
        }
        public Matrix GetMatrix()
        {
            return CoreMathOperation.MatrixIdentity;
        }
        public enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.FillOnly; }
        }
        public ICoreBrush GetBrush(enuBrushMode mode)
        {
            if (mode == enuBrushMode.Fill)
                return this.m_fillBrush;
            return null;
        }
        public ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[] { this.m_fillBrush };
        }
        public class Core2DDocumentIdManager : CoreWorkingObjectIdManagerBase, ICoreWorkingObjectIdManager
        {
            public Core2DDocumentIdManager()
            {
            }
        }
        /// <summary>
        /// return the bitmap présentation
        /// </summary>
        /// <returns></returns>
        public ICoreBitmap ToBitmap()
        {
            return this.ToBitmap(this.Width, this.Height, false, CoreScreen.DpiX, CoreScreen.DpiY);
        }
        public CoreGraphicsPath GetPath()
        {
            CoreGraphicsPath p = new CoreGraphicsPath();
            p.AddRectangle(this.GetDefaultBound());
            return p;
        }
    }
}

