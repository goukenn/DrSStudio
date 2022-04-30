

/*
IGKDEV @ 2008-2016
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
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Core2DDrawingLayer.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinUI.Configuration;
using System.ComponentModel;
using System.Collections;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent the basics dans 2d layer
    /// </summary>
    [Core2DDrawingObject ("Layer")]
    public class Core2DDrawingLayer : 
        Core2DDrawingObjectBase,
        ICore2DDrawingLayer,
        ICore2DDrawingTransform
    {
        private enuLayerColorChannel m_LayerColorChannel;
        private Matrix m_Matrix;
        private Core2DDrawingLayeredElement m_ClippedElement;
        private enu2DLayerBlending m_Blending;
        private float m_GammaCorrection;


        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(0.0f)]
        [CoreConfigurableProperty()]
        public float GammaCorrection
        {
            get { return m_GammaCorrection; }
            set
            {
                if (m_GammaCorrection != value)
                {
                    m_GammaCorrection = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
      
        [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (enu2DLayerBlending.None)]
        [CoreConfigurableProperty ()]
        /// <summary>
        /// get or set the layer blending. in pixel mode rendering by defeault
        /// </summary>
        public enu2DLayerBlending Blending
        {
            get { return m_Blending; }
            set
            {
                if (m_Blending != value)
                {
                    m_Blending = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public Core2DDrawingLayeredElement ClippedElement
        {
            get { return m_ClippedElement; }
            set
            {
                if (m_ClippedElement != value)
                {
                    m_ClippedElement = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }

        private string m_Clipped;
        [Browsable(false)]
        [CoreXMLAttribute]
        public string Clipped
        {
            get {
                if (this.ClippedElement != null)
                    return string.Format("#{0}", this.ClippedElement.Id);
                return m_Clipped; 
            }
            set
            {
                if (m_Clipped != value)
                {
                    this.LoadingComplete -= _ClippedChanged;
                    m_Clipped = value;
                    if (this.IsLoading)
                    {
                        m_ClippedElement = null;
                        this.LoadingComplete += _ClippedChanged;
                    }
                    else {
                        this.ClippedElement = this.GetElementById(value) as Core2DDrawingLayeredElement;
                    }
                }
            }
        }

        private void _ClippedChanged(object sender, EventArgs e)
        {
            this.LoadingComplete -= _ClippedChanged;
            this.ClippedElement = this.GetElementById(this.Clipped) as Core2DDrawingLayeredElement ;
        }
        ///// <summary>
        ///// get layer's matrix
        ///// </summary>
        public Matrix Matrix
        {
            get { return m_Matrix.Clone () as Matrix; }
        }

        public void ResetMatrix() {
            this.m_Matrix.Reset();
        }
        /// <summary>
        /// get or set the layer channel
        /// </summary>
        public enuLayerColorChannel LayerColorChannel
        {
            get { return m_LayerColorChannel; }
            set
            {
                if (m_LayerColorChannel != value)
                {
                    m_LayerColorChannel = value;
                }
            }
        }
        public Core2DDrawingLayer ()
	    {
            this.m_SelectedElements = CreateSelectedElementCollections();
            this.m_Elements = CreateElementCollections();
            this.m_View = true;
            this.m_Matrix = new Matrix();
            this.m_Blending = enu2DLayerBlending.None;
            this.m_Opacity = 1.0f;
            this.m_GammaCorrection = 0.0f;
            //this.m_ColorChannel = LayerColorChannel.None;
            //this.ElementAdded += _Layer_ElementAdded;
            //this.ElementRemoved += _Layer_ElementRemoved;
	    }
    
        public override void Dispose()
        {
            this.m_disposing = true;
            foreach (IDisposable item in this.Elements)
            {
                item.Dispose();
            }
            this.Clear();            
            this.Matrix.Dispose();
            base.Dispose();
            this.m_disposing = false;
        }


        public override void LoadXml(string text)
        {
            base.LoadXml(text);
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            if (xreader.NodeType != System.Xml.XmlNodeType.Element)
                xreader.MoveToContent();
            ICoreWorkingObject[] c = CoreXMLSerializerUtility.GetAllObjects(xreader);
            foreach (var item in c)
            {
                if(item is Core2DDrawingLayeredElement )
                    this.Elements.Add(item as Core2DDrawingLayeredElement );
            }            
        }
        protected void UnregisterToIdManager(ICoreWorkingObject obj)
        {
                ICoreWorkingObjectIdManager man = this.IdManager;
            if (man != null)
                man.Unregister(obj);
        }
        void _Layer_ElementRemoved(object o, Core2DDrawingElementEventArgs e)
        {
            UnregisterToIdManager(e.Item);
        }
        void _Layer_ElementAdded(object o, Core2DDrawingElementEventArgs e)
        {
            RegisterToIdManager(e.Item);
        }
        private void RegisterToIdManager(ICore2DDrawingLayeredElement item)
        {
            ICoreWorkingObjectIdManager man = this.IdManager;
            if (man != null)
                man.Register(item);
        }
        public class Core2DDrawingLayerCollections : 
            CoreItemCollections<Core2DDrawingLayeredElement>,
            ICore2DDrawingLayeredElementCollections,
            ICoreSerializable 
        {
            private Core2DDrawingLayer m_layer;
            public override string ToString()
            {
                return "Core2DLayeredElement[Count:"+this.Count+"]";
            }
            public Core2DDrawingLayerCollections(Core2DDrawingLayer core2DDrawingLayer):base()
            {                
                this.m_layer = core2DDrawingLayer;
            }
            public class ParentChanger : Core2DDrawingObjectBase {

                internal void ChangeParent(Core2DDrawingLayeredElement item, Core2DDrawingLayer core2DDrawingLayer)
                {
                    item.Parent = core2DDrawingLayer;
                }
            }
            public override void Add(Core2DDrawingLayeredElement item)
            {
                if ((item!=null) && !this.Items.Contains(item))
                {
                    this.Items.Add(item);
                    new ParentChanger().ChangeParent(item, this.m_layer );
                    //item.Parent = this.m_layer;
                    RegisterElementEvent(item);
                    this.m_layer.OnElementAdded(new CoreItemEventArgs<ICore2DDrawingLayeredElement>(item));
                }
            }
            public override void AddRange(params Core2DDrawingLayeredElement[] items)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
            private void RegisterElementEvent(Core2DDrawingLayeredElement item)
            {
                item.PropertyChanged += item_PropertyChanged;
            }
            private void UnRegisterElementEvent(Core2DDrawingLayeredElement item)
            {
                item.PropertyChanged -= item_PropertyChanged;
            }
            void item_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                //raise definition changed event
                this.m_layer.OnPropertyChanged(Core2DDrawingChangement.Definition);
            }
            public override void Remove(Core2DDrawingLayeredElement item)
            {                
                if (this.Items.Contains(item))
                {
                    this.Items.Remove(item);
                    if (item.Parent == this.m_layer )
                        item.Parent = null;
                    this.UnRegisterElementEvent(item);
                    this.m_layer.OnElementRemoved(new CoreItemEventArgs<ICore2DDrawingLayeredElement>(item));
                    //must dispose element if no required any more
                }
            }
            public int IndexOf(ICore2DDrawingLayeredElement element)
            {
                return this.Items.IndexOf(element as Core2DDrawingLayeredElement );
            }
            void ICore2DDrawingLayeredElementCollections.AddRange(params ICore2DDrawingLayeredElement[] c)
            {
                if (c != null)
                {
                    List<Core2DDrawingLayeredElement> b = new List<Core2DDrawingLayeredElement>();
                    foreach (Core2DDrawingLayeredElement item in c)
                    {
                        if (item == null) continue;
                        b.Add(item);
                    }
                    if (b.Count > 0)
                        this.AddRange(b.ToArray());
                }
            }
            void ICore2DDrawingElementCollections.Add(ICore2DDrawingElement element)
            {
                this.Add(element as Core2DDrawingLayeredElement );
            }
            void ICore2DDrawingElementCollections.Remove(ICore2DDrawingElement element)
            {
                this.Remove(element as Core2DDrawingLayeredElement);
            }
            ICore2DDrawingElement ICore2DDrawingElementCollections.this[int index]
            {
                get { return this[index]; }
            }
            public ICore2DDrawingElement this[string id]
            {
                get {
                    foreach (var item in this.Items)
                    {
                        if (item.Id == id)
                            return item;
                    }
                    return null;
                }
            }
            public bool Contains(ICore2DDrawingElement element)
            {
                return this.Items.Contains(element);
            }


            //public void MoveToFront(object element)
            //{
            //    if (this.Items.MoveToFront<Core2DDrawingLayeredElement>(element))
            //    {
 
            //    }
            //}
            public void MoveToFront(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;

                if (!this.Contains(t))
                    return;
                if (m == this.m_layer)
                {
                    int i = this.IndexOf(t);
                    if (i < this.Count - 1)
                    {
                        this.Items.Remove(t);
                        this.Items.Insert(i + 1, t);
                        this.m_layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedEventArgs(t,
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
                if (m == this.m_layer)
                {
                    int i = this.IndexOf(t);
                    if (i > 0)
                    {
                        this.Items.Remove(t);
                        this.Items.Insert(i - 1, t);
                        this.m_layer.OnElementZIndexChanged(
                            new CoreWorkingObjectZIndexChangedEventArgs(t, i, t.ZIndex)
                            );
                    }
                }
                else
                {
                    m.Elements.MoveToBack(t);
                }
            }
            public void MoveToStart(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;

                if (!this.Contains(element as Core2DDrawingLayeredElement))
                    return;
                if (m == this.m_layer )
                {
                    int i = this.Items.IndexOf(element as Core2DDrawingLayeredElement);
                    if (i > 0)
                    {
                        this.Items.Remove(t);
                        this.Items.Insert(0, t);
                        this.m_layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedEventArgs(t,
                            i, t.ZIndex));
                    }
                }
                else
                    m.Elements.MoveToStart(element);
            }
            public void MoveToEnd(ICore2DDrawingElement element)
            {
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;
           
                if (!this.Contains(t))
                    return;
                if (m == this.m_layer)
                {
                    int i = this.Items.IndexOf(element as Core2DDrawingLayeredElement);
                    if ((i >= 0) && (i < (this.Items.Count - 1)))
                    {
                        this.Items.Remove(t);
                        this.Items.Insert(this.Items.Count, t);
                        this.m_layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedEventArgs(t, i,
                             t.ZIndex));
                    }
                }
                else
                {
                    m.Elements.MoveToEnd(element);
                }
            }
            public void MoveAt(ICore2DDrawingElement element, int index)
            {
                //move layer as index
                Core2DDrawingLayeredElement t = element as Core2DDrawingLayeredElement;
                ICore2DDrawingLayer m = t.ParentLayer;

                if (!this.Contains(element as Core2DDrawingLayeredElement))
                    return;
                if (m == this.m_layer)
                {
                    int i = this.IndexOf(element as Core2DDrawingLayeredElement);
                    Core2DDrawingLayeredElement y = this.Items[index];
                    if (i > 0)
                    {
                        this.Items.Remove(y);
                        this.Items.Insert(index, t);
                        this.Items.Insert(i, y);
                        this.m_layer.OnElementZIndexChanged(new CoreWorkingObjectZIndexChangedEventArgs(t,
                            i, t.ZIndex));
                    }
                }

            }
            


            public new ICore2DDrawingLayeredElement this[int index]
            {
                get { return base[index];  }
            }
            public new ICore2DDrawingLayeredElement[] ToArray()
            {
                return base.ToArray().ConvertTo<ICore2DDrawingLayeredElement>();
            }
            void ICoreSerializable.Serialize(IXMLSerializer seri)
            {
                foreach (ICoreSerializerService item in this.Items)
                {
                    if (item == null)
                        continue;
                    item.Serialize(seri);
                }
            }
            /// <summary>
            /// clear the current layer
            /// </summary>
            public  void Clear()
            {
                Core2DDrawingLayeredElement[] v_t = this.Items.ToArray();
                foreach (var item in v_t)
                {
                    this.Remove(item);
                }
                if (v_t.Length > 0)
                    this.m_layer.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }


            public void RemoveAll(params ICore2DDrawingLayeredElement[] elements)
            {
                if ((elements == null) || (elements.Length == 0))
                    return;
                for (int i = 0; i < elements.Length; i++)
                {
                    Remove(elements[i] as Core2DDrawingLayeredElement );
                }
            }


            void ICore2DDrawingLayeredElementCollections.Add(ICore2DDrawingLayeredElement element)
            {
                this.Add(element as Core2DDrawingLayeredElement);
            }

            void ICore2DDrawingLayeredElementCollections.Remove(ICore2DDrawingLayeredElement element)
            {
                this.Remove(element as Core2DDrawingLayeredElement);
            }         

            public bool IsReadOnly
            {
                get { return false; }
            }

            ICoreWorkingObject ICoreWorkingElementCollections.this[int index]
            {
                get { return this[index]; }
            }
        }

        /// <summary>
        /// represent a collection for selected items on the current layer
        /// </summary>
        public class Core2DDrawingLayerSelectedElementCollections :
            CoreItemCollections<Core2DDrawingLayeredElement>,
            ICore2DDrawingSelectedElementCollections 
	    {
            private Core2DDrawingLayer m_layer;
            public override string ToString()
            {
                return "Core2DSelectedLayeredElement[Count:"+this.Count+"]";
            }
            public Core2DDrawingLayerSelectedElementCollections(Core2DDrawingLayer core2DDrawingLayer):base()
            {                
                this.m_layer = core2DDrawingLayer;
                this.m_layer.ElementRemoved += m_layer_ElementRemoved;
            }

            void m_layer_ElementRemoved(object sender, CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
            {
                //reset the current layer collection on selected element changed
                if (this.Count > 0)
                {
                    this.Items.Clear();
                    this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                }
            }
            public override void Add(Core2DDrawingLayeredElement item)
            {
                if ((item != null) && (this.m_layer.m_Elements.Contains(item) && !this.Items.Contains(item)))
                {
                    this.Items.Add(item);
                    this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                }
            }
            public override void Remove(Core2DDrawingLayeredElement item)
            {
                if (this.Items.Contains(item))
                {
                    this.Items.Remove(item);
                    this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                }
            }
            void ICore2DDrawingSelectedElementCollections .Add(ICore2DDrawingLayeredElement element)
            {
                this.Add(element as Core2DDrawingLayeredElement);
            }
            void ICore2DDrawingSelectedElementCollections .Remove(ICore2DDrawingLayeredElement element)
            {
                  base.Remove (element as Core2DDrawingLayeredElement);
            }
            ICore2DDrawingLayeredElement ICore2DDrawingSelectedElementCollections.this[int index]
            {
                get { return base[index]; }
            }
            ICore2DDrawingLayeredElement ICore2DDrawingSelectedElementCollections.this[string id]
            {
                get {
                    foreach (var i in this.Items)
                    {
                        if (i.Id == id)
                            return i;
                    }
                    return null;
                }
            }
            bool ICore2DDrawingSelectedElementCollections.Contains(ICore2DDrawingLayeredElement element)
            {
                return base.Contains(element as Core2DDrawingLayeredElement);
            }
            ICore2DDrawingLayeredElement[] ICore2DDrawingSelectedElementCollections.ToArray()
            {
                return base.ToArray();
            }
            public override void AddRange(params Core2DDrawingLayeredElement[] items)
            {
                if (items != null)
                {
                    bool alreadyContains = true;
                    if (items.Length == this.Count)
                    {
                        for (int i = 0; i < items.Length; i++)
                        {
                            if (!this.Items.Contains(items[i]))
                            {
                                alreadyContains = false;
                                break;
                            }
                        }
                    }
                    else
                        alreadyContains = false;
                    if (alreadyContains == false)
                    {
                        this.Items.Clear();
                        this.Items.AddRange(items);
                        this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                    }
                }
                else
                {
                    if (this.Count > 0)
                    {
                        this.Items.Clear();
                        this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                    }
                }
            }
            void ICore2DDrawingSelectedElementCollections.AddRange(ICore2DDrawingLayeredElement[] items)
            {
                if (items != null)
                {
                    this.AddRange(items.ConvertTo<Core2DDrawingLayeredElement>());
                }
                else {
                    if (this.Count > 0)
                    {
                        this.Items.Clear();
                        this.m_layer.OnSelectedElementChanged(EventArgs.Empty);
                    }
                }
            }
        }

        protected virtual Core2DDrawingLayerCollections CreateElementCollections()
        {
            return new Core2DDrawingLayerCollections(this);
        }
        protected virtual Core2DDrawingLayerSelectedElementCollections CreateSelectedElementCollections()
        {
            return new Core2DDrawingLayerSelectedElementCollections(this);
        }
        public enuCoreLayerOperation LayerOption
        {
            get
            {
                return this.m_LayerOption;
            }
            set
            {
                if (this.m_LayerOption != value)
                {
                    this.m_LayerOption = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingLayeredElement>> ElementAdded;
        public event EventHandler<CoreItemEventArgs<ICore2DDrawingLayeredElement>> ElementRemoved;
        public event EventHandler SelectedElementChanged;
        ///<summary>
        ///raise the SelectedElementChanged 
        ///</summary>
        protected virtual void OnSelectedElementChanged(EventArgs e)
        {
            //if (SelectedElementChanged != null)
                SelectedElementChanged?.Invoke(this, e);
        }
        private bool m_View;
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
        [CoreXMLElement ()]
        public ICore2DDrawingLayeredElementCollections Elements
        {
            get { return this.m_Elements; }
        }
        public ICore2DDrawingSelectedElementCollections SelectedElements
        {
            get { return this.m_SelectedElements; }
        }
        public void Select(params ICore2DDrawingLayeredElement[] items)
        {
            this.SelectedElements.AddRange(items);
        }
        public ICore2DDrawingObject GetElementById(string id)
        {
            //remove # at the begining
            while (!string.IsNullOrEmpty (id) && id.StartsWith("#"))
            {
                id = id.Substring(1);
            }
                
            return this.Elements[id];
         }
        /// <summary>
        /// clear the current layer
        /// </summary>
        public virtual void Clear()
        {
            this.m_Elements.Clear();
        }
        public new ICore2DDrawingDocument Parent
        {
            get
            {
                return base.Parent as ICore2DDrawingDocument;
            }
            set
            {
                base.Parent = value as Core2DDrawingObjectBase ;
            }
        }
        public event CoreWorkingObjectZIndexChangedHandler ElementZIndexChanged;
        ///<summary>
        ///raise the ElementZIndexChanged 
        ///</summary>
        protected virtual void OnElementZIndexChanged(CoreWorkingObjectZIndexChangedEventArgs e)
        {
            ElementZIndexChanged?.Invoke(this, e);
        }
        public event EventHandler ClippedChanged;
        private Core2DDrawingLayerSelectedElementCollections m_SelectedElements;
        private Core2DDrawingLayerCollections m_Elements;
        private enuCoreLayerOperation m_LayerOption;
        protected virtual void OnClippedChanged(EventArgs e)
        {
            this.ClippedChanged?.Invoke(this, e);
        }

        /// <summary>
        /// used to change id of the contained element
        /// </summary>
        /// <param name="element"></param>
        /// <param name="newId"></param>
        /// <returns></returns>
        public bool ChangeIdOf(ICore2DDrawingLayeredElement element, string newId)
        {
            ICoreWorkingIdManager manager =  GetIdManager();
            if (manager != null)
            {
                if (!manager.Contains(newId))
                { 
                    return manager.ChangeId (element as CoreWorkingObjectBase , newId);
                }
            }
            else
            {
                Core2DDrawingLayeredElement e = element as Core2DDrawingLayeredElement;
                if (this.m_Elements.Contains(e))
                {
                    if (this.GetElementById(newId) == null)
                    {
                        e.Id = newId;
                        return true;
                    }
                }
            }
            return false;
        }
       
        
        private float m_Opacity;
        private bool m_disposing;
         [CoreXMLAttribute ()]
        [CoreXMLDefaultAttributeValue (1.0f)]
        [CoreConfigurableProperty()]
        public float Opacity
        {
            get { return m_Opacity; }
            set
            {
                if (m_Opacity != value)
                {
                    m_Opacity = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
         public void TranslateMatrix(float dx, float dy, enuMatrixOrder mode) {
             this.m_Matrix.Translate(dx, dy, mode);
         }
         public void ScaleMatrix(float dx, float dy, enuMatrixOrder mode)
         {
             this.m_Matrix.Scale (dx, dy, mode);
         }
         public void RotateMatrix(float angle, enuMatrixOrder mode) {
             this.m_Matrix.Rotate(angle ,  mode);
         }
        public void RotateMatrix(float dx, float dy, enuMatrixOrder mode){
            this.m_Matrix.Share(dx, dy);
         }
        /// <summary>
        /// translate all item
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Translate(float dx, float dy)
        {
          //  this.m_Matrix.Translate(dx, dy);
            foreach (Core2DDrawingLayeredElement item in this.Elements)
            {
                if (item.IsDisposed)
                    continue;

                var m = item.GetMatrix().Clone() as Matrix ;
                if (m.IsIdentity)
                {
                    item.SuspendLayout();
                    item.Translate(dx, dy, enuMatrixOrder.Prepend);
                    item.ResumeLayout();
                    item.ResetTransform(false);
                }
                else{
                    //m.Translate(dx, dy, enuMatrixOrder.Prepend);
                    //item.MultTransform(m, enuMatrixOrder.Append );   
                    item.SuspendLayout();
                    item.Translate(dx, dy, enuMatrixOrder.Append );
                    item.ResumeLayout();
                }
                m.Dispose();
            }

        }
        public void SetClip(ICore2DDrawingLayeredElement v_element)
        {
            if (this.m_ClippedElement != v_element)
            {
                if (v_element == null)
                {
                    this.m_ClippedElement = null;
                }
                else
                {
                    Core2DDrawingLayeredElement v_c = v_element as Core2DDrawingLayeredElement;
                    if (this.m_Elements.Contains(v_c))
                    {
                        this.m_ClippedElement = v_c;
                    }
                    else
                    {
                        this.Elements.Add(v_element);
                        this.SetClip(v_element);
                    }
                }
            }
        }

        public ICoreLayeredDocument ParentDocument
        {
            get {
                ICore2DDrawingObject  q = this.Parent;
                while ((q != null) && !(q is ICoreLayeredDocument))
                {
                    q = q.Parent;
                }
                return q as  ICoreLayeredDocument ;
            }
        }
        public int ZIndex
        {
            get {
                if (this.ParentDocument is ICore2DDrawingDocument v_doc)
                    return v_doc.IndexOf(this);
                return -1;
            }
        }
       
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            return parameters;
        }
        public override ICore.WinUI.ICoreControl GetConfigControl()
        {
            return null;
        }
        /// <summary>
        /// get the id manager
        /// </summary>
        public virtual ICoreWorkingObjectIdManager IdManager
        {
            get {
                return null;
            }
        }
        public bool IsClipped
        {
            get { return this.m_ClippedElement != null; }
        }
        protected virtual void OnElementAdded(CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            if (this.ElementAdded != null)
                ElementAdded(this, e);
        }
        protected virtual void OnElementRemoved(CoreItemEventArgs<ICore2DDrawingLayeredElement> e)
        {
            if (m_disposing)
                return;
            if (this.IsClipped)
            {
                if (e.Item == this.ClippedElement)
                {
                    this.ClippedElement = null;
                }
            }
            this.ElementRemoved?.Invoke(this, e);
        }
        public int IndexOf(ICoreWorkingObject item)
        {
            return this.m_Elements.IndexOf(item as ICore2DDrawingLayeredElement);
        }


        ICore2DDrawingLayeredElement ICore2DDrawingLayer.ClippedElement
        {
            get
            {
                return this.ClippedElement ;
            }
            set
            {
                this.ClippedElement = value as Core2DDrawingLayeredElement ;
            }
        }


        public ICoreWorkingElementContainer Container
        {
            get {
                return Parent as ICoreWorkingElementContainer;
            }
        }

        public virtual void Translate(float dx, float dy, enuMatrixOrder mat)
        {
            foreach (ICore2DDrawingTransform  item in this.Elements)
            {
                item.Translate(dx, dy, mat);
            }
        }


        public void Scale(float ex, float ey, enuMatrixOrder mat)
        {
            foreach (Core2DDrawingLayeredElement item in this.Elements)
            {
                item.Scale(ex, ey, mat);
            }
        }

        public void Rotate(float angle, Vector2f center, enuMatrixOrder mat)
        {
            foreach (ICore2DDrawingTransform item in this.Elements)
            {
                item.Rotate(angle, center, mat);
            }
        }
        /// <summary>
        /// select item in current surface
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool Select(Vector2f point)
        {
            return Select(point, false);
        }
        public bool Select(Vector2f point, bool deepContainer){
            bool ch = false;
            if (this.SelectedElements.Count > 0)
                ch = true;
            List<ICore2DDrawingLayeredElement > b = new List<ICore2DDrawingLayeredElement> ();
            if (Core2DDrawingUtility.ReverseSelect(point, this, this.Elements.ToArray(), b, deepContainer)) { 
                ch = true;
            }           
            return ch;

        }

        public T GetElementById<T>(string id) where T : class
        {
            return Elements.GetElementById(id) as T;
        }

        object ICoreWorkingElementContainer.GetElementById(string id)
        {
            return Elements.GetElementById(id);
        }

        public IEnumerator GetEnumerator()
        {
            return this.Elements?.GetEnumerator();
        }

        ICoreWorkingElementCollections ICore2DDrawingSelectionContainer.Elements
        {
            get { return this.Elements; }
        }

        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements => this.Elements;
    }
}

