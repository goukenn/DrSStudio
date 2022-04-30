

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingLayer.cs
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
file:Core2DDrawingLayer.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Reflection;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Drawing2D.WinUI ;
    using IGK.DrSStudio.Codec;
    using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.WinUI.Configuration;
    using IGK.DrSStudio.Core;
    using IGK.DrSStudio.Core.Layers;
    [Core2DDrawingObjectAttribute("Layer")]
    public class Core2DDrawingLayer :
        Core2DDrawingObjectContainer,
        ICore2DDrawingLayer  ,
        ICoreLayer
    {
        private ImageAttributes m_imageAttribute;
        private SelectedElementCollections m_selectedElements;
        private ElementCollections m_elements;
        private ICore2DDrawingLayeredElement m_ClippedElement;
        private LayerTransparencyKey m_TransparencyKey;
        private float m_GammaCorrection;
        private LayerColorChannel m_ColorChannel;
        private bool m_View;
        private float m_Opacity ;
        private enuCoreLayerOperation m_LayerOption;
        /// <summary>
        /// represnent the layer operation for pixel mode
        /// </summary>
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(enuCoreLayerOperation.None )]
        public enuCoreLayerOperation LayerOption
        {
            get { return m_LayerOption; }
            set
            {
                if (m_LayerOption != value)
                {
                    m_LayerOption = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public bool IsClipped { get {
            return (this.m_ClippedElement != null);
        } }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if ((m_Opacity != value)&&(value >=0.0f) && (value <= 1.0f))
                {
                    m_Opacity = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue (LayerColorChannel.None)]
        public LayerColorChannel ColorChannel
        {
            get { return m_ColorChannel; }
            set {
                if (this.m_ColorChannel != value)
                {
                    this.m_ColorChannel = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [CoreXMLDefaultAttributeValue (0.0f)]
        public float GammaCorrection
        {
            get { return m_GammaCorrection; }
            set {
                if (m_GammaCorrection != value)
                {
                    m_GammaCorrection = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute(true)]
        [LayerTransparencyKeyDefaultValueAttribute()]
        /// <summary>
        /// it represent layer transparency key
        /// </summary>
        public LayerTransparencyKey TransparencyKey
        {
            get { return m_TransparencyKey; }
            set {
                if (!m_TransparencyKey.Equals (value))
                {
                    m_TransparencyKey = value;
                    this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        #region ICore2DDrawingLayer Members
        public ICore2DDrawingLayeredElementCollections Elements
        {
            get {
                return this.m_elements;
            }
        }
        public ICore2DDrawingSelectedElementCollections SelectedElements
        {
            get { return this.m_selectedElements; }
        }
        #endregion
        #region ICoreWorkingPositionableObject Members
        public int ZIndex
        {
            get {
                if (this.Parent is ICore2DDrawingDocument)
                {
                    return (this.Parent as ICore2DDrawingDocument).Layers.IndexOf(this);
                }
                return -1; 
            }
        }
        #endregion
        #region ICore2DDrawingDocumentRendering Members
        public void Draw(Graphics graphic, Rectanglei rectangle)
        {
            throw new NotImplementedException();
        }
        public void Draw(Graphics graphic, bool proportional, Rectanglei rectangle, enuFlipMode flipmode)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region ICore2DDrawingRendering Members
        /// <summary>
        /// drawing the layer with a gdi graphics object 
        /// </summary>
        /// <param name="graphic">graphics device where to draw</param>
        public virtual  void Draw(Graphics graphic)
        {
            GraphicsState st = graphic.Save();
            if (this.IsClipped)
            {
                // graphic.Clip = this.m_ClippedElement.GetRegion();
            }
            foreach (ICore2DDrawingLayeredElement  item in this.Elements)
            {
                if (item == this.m_ClippedElement) continue;
                if (item.View)
                {
                    item.Render(graphic);
                }
            }
            if (this.IsClipped)
            {
                graphic.ResetClip();
                //this.m_ClippedElement.Draw(graphic);
            }
            graphic.Restore(st);
        }
        #endregion
        #region ICore2DDrawingLayer Members
        [CoreXMLDefaultAttributeValue (true )]
        public bool View
        {
            get { return m_View; }
            set
            {
                if (m_View != value)
                {
                    m_View = value;
                }
            }
        }
        #endregion
        #region ICore2DDrawingDesignRendering Members
        #endregion
        #region ICore2DDrawingDesignRendering Members
        public virtual void RenderSelection(Graphics g, IGK.DrSStudio.Drawing2D.WinUI.ICore2DDrawingSurface surface)
        {
            foreach (ICore2DDrawingLayeredElement item in this.SelectedElements)
            {
                item.RenderSelection(g , surface );
            }
        }
        public virtual ICore2DDrawingObject GetElementById(string p)
        {
            p = p.Replace("#", "");
            ICore2DDrawingObject t = this.Elements[p];
            return t;
        }
        public System.Drawing.Imaging.ImageAttributes GetImageAttributes()
        {
            if (this.m_imageAttribute == null)
                this.m_imageAttribute = new ImageAttributes();
            this.m_imageAttribute.ClearColorMatrix(ColorAdjustType.Bitmap);
            this.m_imageAttribute.ClearColorKey(ColorAdjustType.Bitmap);
            this.m_imageAttribute.ClearOutputChannel(ColorAdjustType.Bitmap);
            this.m_imageAttribute.ClearNoOp(ColorAdjustType.Bitmap);
            float r = 1.0f;
            float g = 1.0f;
            float b = 1.0f;
            float offr = 0.0f;
            float offg = 0.0f;
            float offb = 0.0f;
            switch (this.LayerOption)
	{
		case enuCoreLayerOperation.None:
 break;
case enuCoreLayerOperation.Negate:
 r = g = b = -1;
 offr = offb = offg = 1;
 break;
case enuCoreLayerOperation.Add:
 break;
case enuCoreLayerOperation.Replace:
 break;
case enuCoreLayerOperation.Xor:
 break;
case enuCoreLayerOperation.SubSrc:
 break;
case enuCoreLayerOperation.SubDest:
 break;
case enuCoreLayerOperation.AddMask:
 break;
case enuCoreLayerOperation.ReplaceMask:
 break;
case enuCoreLayerOperation.XorMask:
 break;
case enuCoreLayerOperation.SubsrcMask:
 break;
case enuCoreLayerOperation.SubdestMask:
 break;
default:
 break;
	}
            float[][] tab = new float[][]{
                        new float []{r,0,0,0,0},
                        new float []{0,g,0,0,0},
                        new float []{0,0,b,0,0},
                        new float []{0,0,0,Opacity ,0},
                        new float []{offr ,offg ,offb ,0,1.0F}};
            this.m_imageAttribute.SetColorMatrix(new ColorMatrix(
             tab
                ), ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            if (!this.TransparencyKey.Equals(LayerTransparencyKey.Empty))
            {
                this.m_imageAttribute.SetColorKey(
                    this.TransparencyKey.Color1.ToColor(),
                    this.TransparencyKey.Color2.ToColor ()                    ,
                    ColorAdjustType.Bitmap);
            }
            if (this.GammaCorrection > 0.0f)
                this.m_imageAttribute.SetGamma(this.GammaCorrection, ColorAdjustType.Bitmap);
            if (this.ColorChannel != LayerColorChannel.None)
                this.m_imageAttribute.SetOutputChannel((ColorChannelFlag)this.ColorChannel, ColorAdjustType.Bitmap);
            return this.m_imageAttribute;
        }
        #endregion
        public override void Dispose()
        {
            base.Dispose();
            if (this.m_imageAttribute != null)
            {
                this.m_imageAttribute.Dispose();
                this.m_imageAttribute = null;
            }
        }
        public event EventHandler ClippedChanged;
        /// <summary>
        /// raised when clipped changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClippedChanged(EventArgs e)
        {
            if (this.ClippedChanged != null)
            {
                this.ClippedChanged(this, e);
            }
        }
        public void Select(params ICore2DDrawingLayeredElement[] items)
        {
            this.m_selectedElements.Select(items);
        }
        /// <summary>
        /// represent the base abstract  element collection class
        /// </summary>
        public abstract  class ElementCollectionsBase : System.Collections.IEnumerable 
        {
            private Core2DDrawingLayer m_layer;
            private List<Core2DDrawingLayeredElement> m_elements;
            protected Core2DDrawingLayer Layer { get { return this.m_layer; } }
            protected List<Core2DDrawingLayeredElement > List { get { return this.m_elements; } }
            protected ElementCollectionsBase(Core2DDrawingLayer layer)
            {
                this.m_layer = layer;
                m_elements = new List<Core2DDrawingLayeredElement>();
            }
            #region ICore2DDrawingElementCollections Members
            public void Add(Core2DDrawingLayeredElement element)
            {
                if (element == null) return;
                if (!this.m_elements .Contains (element ))
                {
                    //check already containt element with name
                    if (this.m_layer.GetElementById(element.Id) != null)
                    {//change the id
                        element.Id = null;
                    }
                    this.m_elements.Add(element);
                    OnElementAdded(element );
                }
            }
            public void Remove(Core2DDrawingLayeredElement  element)
            {
                if (element == null) return;
                if (this.m_elements.Contains(element))
                {
                    this.m_elements.Remove(element);
                    OnElementRemoved(element);
                }
                else {
                    if (element.Parent is ICore2DElementsContainer)
                    {
                        (element.Parent as ICore2DElementsContainer).Elements.Remove(element);
                    }
                }
            }
           protected abstract void OnElementRemoved(Core2DDrawingLayeredElement element);
           protected abstract void OnElementAdded(Core2DDrawingLayeredElement element);
           public ICore2DDrawingLayeredElement  this[int index]
            {
                get { 
                    if ((index>=0) && (index<this.m_elements .Count))
                    return this.m_elements[index];
                    return null;
                }
            }
            public ICore2DDrawingLayeredElement this[string id]
            {
                get 
                {
                    for (int i = 0; i < this.Count; i++)
                    {
                        if (this[i].Id == id) return this[i];
                    }
                    return null;
                }
            }
            public int Count
            {
                get { return this.m_elements.Count; }
            }
            public bool Contains(Core2DDrawingLayeredElement  element)
            {
                if (element == null)
                    return false;
                bool v = this.m_elements.Contains(element);
                if (!v)
                {
                    ICore2DDrawingObject l = element.Parent;
                    while ((l != null) && (l != this.m_layer))
                    {
                        l = l.Parent;
                    }
                    if (l == null)
                        return false;
                    return true;
                }
                return v;
            }
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_elements.GetEnumerator();
            }
            #endregion
        }
        /// <summary>
        /// represent the element collections
        /// </summary>
        public class ElementCollections : 
            ElementCollectionsBase ,
            ICore2DDrawingLayeredElementCollections ,
            ICoreWorkingPositionableObjectContainer 
        {
            private Dictionary<string, ICore2DDrawingLayeredElement> m_dics;
            public ElementCollections(Core2DDrawingLayer layer):base(layer)
            {
                m_dics = new Dictionary<string,ICore2DDrawingLayeredElement> ();
            }
            internal void LoadElementsId()
            {
                m_dics.Clear();
                foreach (ICore2DDrawingLayeredElement i in this)
                {
                    m_dics.Add(i.Id, i);
                }
            }
            int ICore2DDrawingLayeredElementCollections.IndexOf(ICore2DDrawingLayeredElement element)
            {
                return this.IndexOf(element as Core2DDrawingLayeredElement);
            }
            #endregion
            public int IndexOf(Core2DDrawingLayeredElement element)
            {
                return this.List.IndexOf(element);
            }
            public void Remove(params Core2DDrawingLayeredElement[] elements)
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    base.Remove(elements[i]);
                }
            }
            public override string ToString()
            {
                return string.Format ("Count:[{0}]", this.Count );
            }
            void ICore2DDrawingElementCollections.Add(ICore2DDrawingElement element)
            {
                this.Add(element as Core2DDrawingLayeredElement);
            }
            public void MoveToFront(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
                if (!this.Contains(t))
                    return;
                if (m == this.Layer)
                {
                    int i = this.List.IndexOf(t);
                    if (i < this.Count - 1)
                    {
                        this.List.Remove(t);
                        this.List.Insert(i + 1, t);
                        this.Layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(t,
                            i, t.ZIndex));
                    }
                }
                else
                    m.Elements.MoveToFront(element);
            }
            public void MoveToBack(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
                if (!this.Contains(t))
                    return;
                if (m == this.Layer )
                {
                    int i = this.List.IndexOf(t);
                    if (i > 0)
                    {
                        this.List.Remove(t);
                        this.List.Insert(i - 1, t);
                        this.Layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(t,
                            i, t.ZIndex));
                    }
                }
                else {
                    m.Elements.MoveToBack(t);
                }
            }
            /// <summary>
            /// move to the begin of the list
            /// </summary>
            /// <param name="element"></param>
            public void MoveToStart(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
                if (!this.Contains(element as Core2DDrawingLayeredElement))
                    return;
                if (m == this.Layer)
                {
                    int i = this.List.IndexOf(element as Core2DDrawingLayeredElement);
                    if (i > 0)
                    {
                        this.List.Remove(t);
                        this.List.Insert(0, t);
                        this.Layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(t,
                            i, t.ZIndex));
                    }
                }
                else
                    m.Elements.MoveToStart(element);
            }
            /// <summary>
            /// move to end list
            /// </summary>
            /// <param name="element"></param>
            public void MoveToEnd(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
                if (!this.Contains(t))
                    return;
                if (m == this.Layer)
                {
                    int i = this.List.IndexOf(element as Core2DDrawingLayeredElement);
                    if ((i >= 0) && (i < (this.List.Count - 1)))
                    {
                        this.List.Remove(t);
                        this.List.Insert(this.List.Count, t);
                        this.Layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(t, i,
                             t.ZIndex));
                    }
                }
                else {
                    m.Elements.MoveToEnd(element);
                }
           }
            public void MoveAt(ICore2DDrawingElement element, int index)
            {
                //move layer as index
                Core2DDrawingLayeredElement t = element  as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
                if (!this.Contains(element as Core2DDrawingLayeredElement))
                    return;
                if (m == this.Layer)
                {
                    int i = this.List.IndexOf(element as Core2DDrawingLayeredElement);
                    Core2DDrawingLayeredElement y = this.List[index];
                    if (i > 0)
                    {
                        this.List.Remove(y);
                        this.List.Insert(index, t);
                        this.List.Insert(i, y);
                        this.Layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedArgs(t,
                            i, t.ZIndex));
                    }
                }
            }
            public void AddRange(params ICore2DDrawingLayeredElement[] elements)
            {
                if (elements == null) return;
                foreach (ICore2DDrawingLayeredElement item in elements)
                {
                    this.Add(item as Core2DDrawingLayeredElement );
                }
            }
            protected override void OnElementAdded(Core2DDrawingLayeredElement element)
            {
                element.Parent = this.Layer;
                if (!this.Layer.IsLoading)
                {
                    bool v = true;
                    if (this.m_dics.ContainsKey(element.Id))
                    {
                        if (this.m_dics[element.Id] != element)
                        {
                            element.Id = null;
                        }
                        else v = false;
                    }
                    if (v)
                        this.m_dics.Add(element.Id, element);
                }                           
                element.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
                this.Layer.OnElementAdded ( new Core2DDrawingElementEventArgs ( element));
            }
            void element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.Layer.OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(enuPropertyChanged.Definition));
            }
            protected override void OnElementRemoved(Core2DDrawingLayeredElement element)
            {
                this.m_dics.Remove (element.Id );
                element.PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(element_PropertyChanged);
                element.Parent = null;
                this.Layer.OnElementRemoved (new Core2DDrawingElementEventArgs(element));
            }
            #region ICore2DDrawingElementCollections Members
            ICore2DDrawingElement ICore2DDrawingElementCollections.this[int index]
            {
                get { return this[index]; }
            }
            ICore2DDrawingElement ICore2DDrawingElementCollections.this[string id]
            {
                get { return this[id]; }
            }
            bool ICore2DDrawingElementCollections.Contains(ICore2DDrawingElement element)
            {
                return this.Contains(element as Core2DDrawingLayeredElement);
            }
            #endregion
            #region ICore2DDrawingLayeredElementCollections Members
            public ICore2DDrawingLayeredElement[] ToArray()
            {
                return this.List.ToArray();
            }
            #endregion
            #region ICore2DDrawingElementCollections Members
            public void Remove(params ICore2DDrawingElement[] elements)
            {
                for (int i = 0; i < elements .Length; i++)
                {
                    this.Remove(elements[i] as Core2DDrawingLayeredElement);
                }
            }
            #endregion
            internal bool ChangedId(ICore2DDrawingLayeredElement element, string newId)
            {
                Core2DDrawingLayeredElement v_l = element as Core2DDrawingLayeredElement;
                bool v = System.Xml.XmlReader.IsName(newId);
                if ((v_l != null) && 
                    this.Contains (v_l ) && v && (m_dics.ContainsKey(v_l.Id)))
                {
                    this.m_dics.Remove(element.Id);
                    v_l.Id = newId;
                    this.m_dics.Add(newId, element);
                    return true;
                }
                return false;
            }
            internal bool Contains(string newId)
            {
                return this.m_dics.ContainsKey(newId);
            }
            #region ICoreWorkingPositionableObjectContainer Members
            void ICoreWorkingPositionableObjectContainer.MoveToBack(ICoreWorkingPositionableObject item)
            {
                this.MoveToBack(item as ICore2DDrawingElement);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToFront(ICoreWorkingPositionableObject item)
            {
                this.MoveToFront(item as ICore2DDrawingElement);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToStart(ICoreWorkingPositionableObject item)
            {
                this.MoveToStart(item as ICore2DDrawingElement);
            }
            void ICoreWorkingPositionableObjectContainer.MoveToEnd(ICoreWorkingPositionableObject item)
            {
                this.MoveToEnd(item as ICore2DDrawingElement);
            }
            void ICoreWorkingPositionableObjectContainer.MoveAt(ICoreWorkingPositionableObject item, int index)
            {
                this.MoveAt(item as ICore2DDrawingElement, index );
            }
            #endregion
            #region ICoreWorkingPositionableObjectContainer Members
            public int IndexOf(ICoreWorkingPositionableObject item)
            {
                return this.List.IndexOf(item as Core2DDrawingLayeredElement );
            }
            #endregion
        }
        class SelectedElementCollections : 
            ElementCollectionsBase ,
            ICore2DDrawingSelectedElementCollections
        {
            public SelectedElementCollections(Core2DDrawingLayer layer):base(layer )
            {
            }
            protected override void OnElementAdded(Core2DDrawingLayeredElement element)
            {
                this.Layer.OnSeletedElementChanged(EventArgs.Empty);
            }
            protected override void OnElementRemoved(Core2DDrawingLayeredElement element)
            {
                this.Layer.OnSeletedElementChanged(EventArgs.Empty);
            }
            #region ICore2DDrawingSelectedElementCollections Members
            public bool Contains(ICore2DDrawingElement element)
            {
                return base.Contains(element as Core2DDrawingLayeredElement);
            }
            public void Sort()
            {
                CoreZIndexComparer<Core2DDrawingLayeredElement> v_com 
                    = new CoreZIndexComparer<Core2DDrawingLayeredElement>();
                this.List.Sort(v_com);
            }
            public ICore2DDrawingElement[] ToArray()
            {
                return this.List.ToArray();
            }
            #endregion
            internal void Select(ICore2DDrawingLayeredElement[] items)
            {
                if (items == null)
                {
                    if (this.Count > 0)
                    {
                        this.List.Clear();
                        this.Layer.OnSeletedElementChanged(EventArgs.Empty);
                    }
                }
                else {
                    int v_p = this.List.Count;
                    this.List.Clear();
                    foreach (Core2DDrawingLayeredElement  item in items)
                    {
                        if (this.Layer.Elements.Contains(item))
                        {
                            this.List.Add(item);
                        }
                    }
                    if (((v_p == 0) && (this.List.Count > 0))|| (v_p !=0))
                    this.Layer.OnSeletedElementChanged(EventArgs.Empty);
                }
            }
            #region ICore2DDrawingSelectedElementCollections Members
            ICore2DDrawingLayeredElement[] ICore2DDrawingSelectedElementCollections.ToArray()
            {
                return this.List.ToArray();
            }
            #endregion
            #region ICore2DDrawingSelectedElementCollections Members
            public void Add(ICore2DDrawingLayeredElement element)
            {
                base.Add(element as Core2DDrawingLayeredElement);
            }
            public void Remove(ICore2DDrawingLayeredElement element)
            {
                base.Remove (element as Core2DDrawingLayeredElement);
            }
            public bool Contains(ICore2DDrawingLayeredElement element)
            {
              return base.Contains (element as Core2DDrawingLayeredElement);
            }
            #endregion
        }
        protected virtual void OnElementAdded(Core2DDrawingElementEventArgs e)
        {
            if (this.ElementAdded != null)
                this.ElementAdded(this, e);
        }
        protected virtual void OnElementRemoved(Core2DDrawingElementEventArgs e)
        {
            if (this.IsClipped)
            {
                if (this.m_ClippedElement == e.Element)
                {
                    this.m_ClippedElement = null;
                }
            }
            if (this.ElementRemoved != null)
                this.ElementRemoved(this, e);
        }
        internal void OnSeletedElementChanged(EventArgs e)
        {
            if (this.SelectedElementChanged != null)
                this.SelectedElementChanged (this, e);
        }
        #region ICore2DDrawingLayer Members
        public event Core2DDrawingElementEventHandler ElementAdded;
        public event Core2DDrawingElementEventHandler ElementRemoved;
        public event EventHandler SelectedElementChanged;
        #endregion
        public Core2DDrawingLayer()
        {
            this.m_elements = CreateElementCollections(); 
            this.m_selectedElements = new SelectedElementCollections(this);
            this.m_View = true;
            this.m_Opacity = 1.0f;
            this.m_GammaCorrection = 0.0f;
            this.m_ColorChannel = LayerColorChannel.None;
            this.ElementAdded += new Core2DDrawingElementEventHandler(_ElementAddOrRemoved);
            this.ElementRemoved  += new Core2DDrawingElementEventHandler(_ElementAddOrRemoved);
            this.ElementAdded += _Layer_ElementAdded;
            this.ElementRemoved += _Layer_ElementRemoved;
        }
        void _Layer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
        {
            this.IdManager.Unregister(e.Element);
        }
        void _Layer_ElementAdded(object o, Core2DDrawingElementEventArgs e)
        {            
                this.IdManager.Register(e.Element);            
        }
        /// <summary>
        /// create a element collections
        /// </summary>
        /// <returns></returns>
        protected virtual ElementCollections CreateElementCollections()
        {
            return new ElementCollections(this);
        }
        void _ElementAddOrRemoved(object o, Core2DDrawingElementEventArgs e)
        {
            this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }
        public override string ToString()
        {
            return string.Format("Layer : #{0}", this.Id);
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
            string clipId = xreader.GetAttribute("Clipped");
            if (!string.IsNullOrEmpty(clipId))
            {
                this.LoadingComplete += new EventHandler(new LoadClip(this, clipId).LoadComplete);
            }
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
            if (this.IsClipped)
                xwriter.WriteAttributeString("Clipped", "#" + this.m_ClippedElement.Id);
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            ICore2DDrawingLayeredElement[] tls = this.Elements.ToArray();
            foreach (ICore2DDrawingLayeredElement item in tls)
            {
                item.Serialize(xwriter);
            }
        }
         protected override void  ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType != System.Xml.XmlNodeType.Element )
                xreader.MoveToContent();
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        Core2DDrawingLayeredElement
                                   obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                   as Core2DDrawingLayeredElement;
                        if (obj != null)
                        {
                            this.m_elements.Add(obj);
                            obj.Deserialize(xreader.ReadSubtree());
                        }
                        else
                            xreader.Skip();
                        break;
                }
            }
            this.m_elements.LoadElementsId();
        }
        #region ICore2DDrawingLayer Members
        public virtual void Clear()
        {
            ICore2DDrawingLayeredElement[] v_tab = this.Elements.ToArray();
            this.Select(null);
            this.Elements.Remove(v_tab);
        }
        #endregion
        #region ICoreWorkingConfigurableObject Members
        public IGK.DrSStudio.WinUI.Configuration.enuParamConfigType GetConfigType()
        {
            return IGK.DrSStudio.WinUI.Configuration.enuParamConfigType.ParameterConfig;
        }
        public virtual IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(
            IGK.DrSStudio.WinUI.ICoreParameterConfigCollections defaultParam)
        {
            //defaultParam.Clear();
            Type t = this.GetType();
            ICoreParameterGroup v_group = defaultParam.AddGroup("Default");
            v_group.AddItem(t.GetProperty("Id"));
            v_group = defaultParam.AddGroup("Description");            
            v_group.AddItem(t.GetProperty("GammaCorrection"));
            v_group.AddTrackbar("Opacity", "lb.opacity.caption", 0, 100, Convert.ToInt32 (this.Opacity * 100.0f), OpacityChangedProc);
            //v_group = defaultParam.AddGroup("TransparencyKey");
            //XBulletIntervalTrackBar r = new XBulletIntervalTrackBar();
            //XBulletIntervalTrackBar b = new XBulletIntervalTrackBar();
            //XBulletIntervalTrackBar g = new XBulletIntervalTrackBar();
            //r.MinValue = 0;
            //r.MaxValue = 255;
            //r.IntervalChanged += new EventHandler(r_IntervalChanged);
            //r.Interval = new BulletInterval(TransparencyKey.Color1.R, TransparencyKey.Color2.R);
            //g.MinValue = 0;
            //g.MaxValue = 255;
            //g.Interval = new BulletInterval(TransparencyKey.Color1.G, TransparencyKey.Color2.G);
            //g.IntervalChanged += new EventHandler(g_IntervalChanged);
            //b.MinValue = 0;
            //b.MaxValue = 255;
            //b.Interval = new BulletInterval(TransparencyKey.Color1.B, TransparencyKey.Color2.B);
            //b.IntervalChanged += new EventHandler(b_IntervalChanged);
            //v_group.AddItem("TransparencyRColor", "lb.R.caption", r);
            //v_group.AddItem("TransparencyGColor", "lb.G.caption", g);
            //v_group.AddItem("TransparencyBColor", "lb.B.caption", b);
            v_group = defaultParam.AddGroup("Channel");
            v_group.AddItem(t.GetProperty("ColorChannel"));
            return defaultParam;
        }
        void OpacityChangedProc(object sender, IGK.DrSStudio.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            this.Opacity = Convert.ToSingle(e.Value) / 100.0f ;
        }
        //void r_IntervalChanged(object sender, EventArgs e)
        //{
        //    XBulletIntervalTrackBar c = sender as XBulletIntervalTrackBar ;
        //    this.StransparencyStartKey((byte)c.Interval.Min, TransparencyKey.Color1.G , TransparencyKey.Color1.B );
        //    this.StransparencyEndKey((byte)c.Interval.Max, TransparencyKey.Color2.G , TransparencyKey.Color2.B );
        //    InvalidateParent();
        //}
        //void g_IntervalChanged(object sender, EventArgs e)
        //{
        //    XBulletIntervalTrackBar c = sender as XBulletIntervalTrackBar;
        //    this.StransparencyStartKey(TransparencyKey.Color1.R, (byte)c.Interval.Min, TransparencyKey.Color1.B);
        //    this.StransparencyEndKey(TransparencyKey.Color2.R, (byte)c.Interval.Max, TransparencyKey.Color2.B);
        //    InvalidateParent();
        //}
        //void b_IntervalChanged(object sender, EventArgs e)
        //{
        //    XBulletIntervalTrackBar c = sender as XBulletIntervalTrackBar;
        //    this.StransparencyStartKey(TransparencyKey.Color1.R,TransparencyKey.Color1.G,(byte)c.Interval.Min );
        //    this.StransparencyEndKey(TransparencyKey.Color2.R,  TransparencyKey.Color2.G, (byte)c.Interval.Max   );
        //    InvalidateParent();
        //}
        void InvalidateParent()
        {
            ICore2DDrawingSurface v_s =  this.GetParentSurface();
            if (v_s != null)
                v_s.Invalidate();
        }
        private void StransparencyStartKey(byte r,byte g,byte b)
        {
            LayerTransparencyKey c = TransparencyKey;
         	c.Color1 = new LayerTransparencyKey.LTColor (r,g,b);
            this.TransparencyKey  = c;
        }
        private void StransparencyEndKey(byte r,byte g,byte b)
        {
         	LayerTransparencyKey c = TransparencyKey;
         	c.Color2 = new LayerTransparencyKey.LTColor (r,g,b);
            this.TransparencyKey  = c;
        }
        void transparencyStartColor(object o, CoreParameterChangedEventArgs e)
        {
            LayerTransparencyKey c = TransparencyKey;
            c.Color1 = (LayerTransparencyKey.LTColor)(Colorf)e.Value;
            this.TransparencyKey = c;
        }
        void transparencyEndColor(object o, CoreParameterChangedEventArgs e)
        {
            LayerTransparencyKey c = TransparencyKey;
            c.Color2 = (LayerTransparencyKey.LTColor)(Colorf)e.Value;
            this.TransparencyKey = c;
        }
        public virtual  ICoreControl GetConfigControl()
        {
            return null;
        }
        #endregion
        #region ICore2DDrawingLayer Members
        /// <summary>
        /// represent the parent document
        /// </summary>
        public new ICore2DDrawingDocument Parent
        {
            get { return base.Parent as ICore2DDrawingDocument  ; }
            set { base.Parent = value; }
        }
        #endregion
        #region ICore2DDrawingLayer Members
        public event CoreWorkingObjectZIndexChangedHandler ElementZIndexChanged;
        #endregion
        protected virtual void OnElementZIndexChanged(CoreWorkingObjectZIndexChangedArgs e)
        {
            if (this.ElementZIndexChanged !=null){
                this.ElementZIndexChanged(this, e);
            }
        }
        #region ICore2DDrawingLayer Members
        public bool ChangeIdOf(ICore2DDrawingLayeredElement element, string newId)
        {
            if (this.m_elements.Contains(newId))
                return false;            
            return     this.m_elements.ChangedId(element, newId);
        }
        #endregion
        public virtual void Translate(float x, float y)
        {
            foreach (ICore2DDrawingLayeredElement  item in this.Elements)
            {
                if (item.CanTranslate)
                {
                    item.Translate(x, y, enuMatrixOrder.Append);
                }
            }
        }
        #region ICore2DDrawingLayer Members
        /// <summary>
        /// set the clip element
        /// </summary>
        /// <param name="element"></param>
        public void SetClip(ICore2DDrawingLayeredElement element)
        {
            if ((this.m_ClippedElement != element)            
                && ((element == null)|| this.Elements.Contains (element )))
            {
                this.m_ClippedElement = element;
                this.OnClippedChanged(EventArgs.Empty);
            }
        }
        #endregion
        /// <summary>
        /// used to load layer clip element
        /// </summary>
        internal class LoadClip
        {
            Core2DDrawingLayer  m_layer;
            string m_id;
            public LoadClip(Core2DDrawingLayer document, string id)
            {
                this.m_layer = document;
                this.m_id = id;
            }
            internal void LoadComplete(object o, EventArgs e)
            {
                this.m_layer.SetClip(
                    this.m_layer.GetElementById(this.m_id) as ICore2DDrawingLayeredElement);
                //remove the clipped event
                this.m_layer.LoadingComplete -= LoadComplete;
            }
        }
        #region ICore2DDrawingLayer Members
        public Region GetClippedRegion()
        {
            if (this.m_ClippedElement == null)
                return null;
            return this.m_ClippedElement.GetRegion();
        }
        #endregion
        #region ICoreLayer Members
        ICoreLayeredDocument ICoreLayer.ParentDocument
        {
            get { return this.Parent; }
        }
        ICoreLayerElementCollections ICoreLayer.Elements
        {
            get { return this.Elements; }
        }
        ICoreLayerSelectedElementCollections ICoreLayer.SelectedElements
        {
            get { return this.SelectedElements; }
        }
        #endregion
        public void MoveBack()
        {
            if (this.Parent != null)
            this.Parent.Layers.MoveToBack(this);
        }
        public void MoveFront()
        {
            if (this.Parent != null)
            this.Parent.Layers.MoveToFront(this);
        }
        ICoreWorkingPositionableObjectContainer ICoreWorkingPositionableObject.Container
        {
            get
            {
                return this.Parent.Layers ;
            }
        }
        #region ICoreWorkingPositionableObject Members
        public void MoveEnd()
        {
            if (this.Parent != null)
                this.Parent.Layers.MoveToEnd(this);
        }
        public void MoveStart()
        {
            if (this.Parent != null)
                this.Parent.Layers.MoveToStart(this);
        }
        public void MoveAt(int index)
        {
            if (this.Parent != null)
                this.Parent.Layers.MoveAt(this, index );
        }
        #endregion
    }
}

