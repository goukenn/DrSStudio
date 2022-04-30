

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFLayer.cs
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
file:WPFLayer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Core.Layers;
    [WPFFBaseObject("Layer")]
    /// <summary>
    /// represent a wpf layer
    /// </summary>
    public class WPFLayer : WPFElementBase,
        IWPFLayer, 
        ICoreLayer ,
        ICoreWorkingPositionableObject 
    {
        WPFElementCollections m_elements;
        WPFSelectedElementCollections m_selectedElements;
        private System.Windows.Controls.Canvas m_Canvas;
        private bool m_Visible;
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (IGK.DrSStudio.Codec.ICoreSerializerService item in this.Elements )
            {
                item.Serialize(xwriter);
            }
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType != System.Xml.XmlNodeType.Element)
                xreader.MoveToContent();
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        WPFLayeredElement 
                                   obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                   as WPFLayeredElement;
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
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                    OnVisibleChanged(EventArgs.Empty);
                }
            }
        }
        public event WPFElementEventHandler<IWPFLayeredElement> ElementAdded;
        public event WPFElementEventHandler<IWPFLayeredElement> ElementRemoved;
        public event EventHandler VisibleChanged;
        ///<summary>
        ///raise the VisibleChanged 
        ///</summary>
        protected virtual void OnVisibleChanged(EventArgs e)
        {
            if (VisibleChanged != null)
                VisibleChanged(this, e);
        }
        protected virtual void OnElementRemoved(WPFElementEventArgs<IWPFLayeredElement> e)
        {
            if (this.ElementRemoved != null)
                this.ElementRemoved(this, e);
        }
        protected virtual void OnElementAdded(WPFElementEventArgs<IWPFLayeredElement> e)
        {
            if (this.ElementAdded != null)
                this.ElementAdded(this, e);
        }
        public event EventHandler SelectedElementChanged;
        ///<summary>
        ///raise the SelectedElementChanged 
        ///</summary>
        protected virtual void OnSelectedElementChanged(EventArgs e)
        {
            if (SelectedElementChanged != null)
                SelectedElementChanged(this, e);
        }
        public new WPFDocument Parent { get { return base.Parent as WPFDocument; }
            internal protected set { base.Parent = value; }
        }
        public System.Windows.Controls.Canvas  Canvas
        {
            get { return m_Canvas; }
        }
        public WPFElementCollections Elements { get { return this.m_elements; } }
        public WPFSelectedElementCollections SelectedElements { get { return this.m_selectedElements; } }
        public WPFLayer()
        {
            this.m_elements = CreateElementCollection();
            this.m_selectedElements = CreateSelectedElementCollections();
            this.m_Canvas = new System.Windows.Controls.Canvas();
            this.VisibleChanged += new EventHandler(_VisibleChanged);
            this.ElementAdded += new WPFElementEventHandler<IWPFLayeredElement>(WPFLayer_ElementAdded);
            this.ElementRemoved += new WPFElementEventHandler<IWPFLayeredElement>(WPFLayer_ElementRemoved);
        }
        void WPFLayer_ElementRemoved(object sender, WPFElementEventArgs<IWPFLayeredElement> e)
        {            
            System.Windows.UIElement c = e.Element.Shape as System.Windows.UIElement;
            this.Canvas.Children.Remove (c);
        }
        void WPFLayer_ElementAdded(object sender, WPFElementEventArgs<IWPFLayeredElement> e)
        {
            System.Windows.UIElement c = e.Element.Shape as System.Windows.UIElement;
            this.Canvas.Children.Add(c);
        }
        void _VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible)
                this.Canvas.Visibility = System.Windows.Visibility.Visible;
            else
                this.Canvas.Visibility = System.Windows.Visibility.Hidden;
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters  = base.GetParameters(parameters);
            return parameters;
        }
        private WPFElementCollections CreateElementCollection()
        {
            return new WPFElementCollections(this);
        }
        private WPFSelectedElementCollections CreateSelectedElementCollections()
        {
            return new WPFSelectedElementCollections(this);
        }
        #region ICoreLayer Members
        ICoreLayeredDocument ICoreLayer.ParentDocument
        {
            get { return this.Parent as ICoreLayeredDocument ; }
        }
      
        #endregion
        #region IWPFLayer Members
        IWPFElementCollections IWPFLayer.Elements
        {
            get { return this.m_elements; }
        }
        IWPFSelectedElementCollections IWPFLayer.SelectedElements
        {
            get { return this.m_selectedElements; }
        }
        #endregion
        #region IWPFElement Members
        IWPFElement IWPFElement.Parent
        {
            get
            {
                return Parent as IWPFElement ;
            }
            set
            {
                base.Parent = value as WPFElementBase ;
            }
        }
        #endregion
        /// <summary>
        /// get element by the specified id . or null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IWPFLayeredElement getElementById(string id)
        {
            IWPFLayeredElement i = this.Elements[id];
            return i;
        }
        public abstract class WPFElementBaseCollections : IWPFElementCollections
        {
            List<WPFLayeredElement> m_elements;
            WPFLayer m_owner;
            public WPFLayer Owner { get { return this.m_owner; } }
            protected List<WPFLayeredElement> Elements {
                get {
                    return m_elements;
                }
            }
            public WPFElementBaseCollections(WPFLayer layer)
            {
                m_elements = new List<WPFLayeredElement>();
                this.m_owner = layer;
            }
            public WPFLayeredElement[] ToArray()
            {
                return this.Elements.ToArray();
            }
            public abstract void Add(WPFLayeredElement element);
            public abstract void Remove(WPFLayeredElement element);
            public int Count { get { return this.m_elements.Count; } }
            public System.Collections.IEnumerator GetEnumerator() { return m_elements.GetEnumerator(); }
            public bool Contains(WPFLayeredElement element) { return this.Elements.Contains(element); }
        }
        public class WPFElementCollections : WPFElementBaseCollections,
            ICoreWorkingPositionableObjectContainer,
            IWPFElementCollections,
            ICoreWorkingPositionableObjectContainer<WPFLayeredElement>
        {
            private Dictionary<string, WPFLayeredElement > m_dics;
            public WPFLayeredElement this[string id] {
                get {
                    if (m_dics.ContainsKey(id))
                        return m_dics[id];
                    return null;
                }
            }
            public WPFElementCollections(WPFLayer layer):base(layer)
            {
                m_dics = new Dictionary<string, WPFLayeredElement>();
            }
            public override void Add(WPFLayeredElement element)
            {
                if (element == null) return;
                this.Elements.Add(element);
                element.Parent = this.Owner;
                this.Owner.OnElementAdded(new WPFElementEventArgs<IWPFLayeredElement>(element));
            }
            public override void Remove(WPFLayeredElement element)
            {
                this.Elements.Remove (element);
                this.Owner.OnElementRemoved(new WPFElementEventArgs<IWPFLayeredElement>(element));
            }
            
            public int IndexOf(WPFLayeredElement wPFLayeredElement)
            {
                return this.Elements.IndexOf(wPFLayeredElement);
            }
            public void Clear()
            {
                WPFLayeredElement[] t= this.Elements.ToArray();
                foreach (var item in t)
                {
                    this.Remove(item);
                }
            }
                internal void LoadElementsId()
            {
                m_dics.Clear();
                foreach (WPFLayeredElement  i in this)
                {
                    m_dics.Add(i.Id, i);
                }
            }
                #region ICoreWorkingPositionableObjectContainer<WPFLayeredElement> Members
                public void MoveAt(WPFLayeredElement item, int index)
                {
                    throw new NotImplementedException();
                }
                public void MoveToBack(WPFLayeredElement item)
                {
                    throw new NotImplementedException();
                }
                public void MoveToEnd(WPFLayeredElement item)
                {
                    throw new NotImplementedException();
                }
                public void MoveToFront(WPFLayeredElement item)
                {
                    throw new NotImplementedException();
                }
                public void MoveToStart(WPFLayeredElement item)
                {
                    throw new NotImplementedException();
                }
                public int IndexOf(ICoreWorkingPositionableObject item)
                {
                    throw new NotImplementedException();
                }
                #endregion
        }
        public class WPFSelectedElementCollections : 
            WPFElementBaseCollections,
            IWPFSelectedElementCollections
            
        {
            /// <summary>
            /// get the layered element
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public WPFLayeredElement this[int index] {
                get { return this.Elements[index]; }
            }
            public WPFSelectedElementCollections(WPFLayer layer):base(layer )
            {
            }
            public override string ToString()
            {
                return "SelectedElement [" + this.Count + "]";
            }
            public override void Add(WPFLayeredElement element)
            {
                if ((element == null)||(!this.Owner .Elements .Contains (element ))) 
                    return;
                this.Elements.Add(element);
            }
            public override void Remove(WPFLayeredElement element)
            {
                this.Elements.Remove(element);
            }
            /// <summary>
            /// select element on current layer
            /// </summary>
            /// <param name="elements"></param>
            public void Select(params WPFLayeredElement[] elements)
            {
                int i = this.Count;
                bool v_raise = false;
                v_raise = (i > 0);
                this.Elements.Clear();
                if (elements != null) {
                    foreach (var item in elements)
                    {
                        if (this.Owner.Elements.Contains (item ))
                        {
                            this.Elements.Add(item);
                        }                        
                    }
                    v_raise |= (this.Count > 0);
                }
                if (v_raise )
                this.Owner.OnSelectedElementChanged(EventArgs.Empty);
            }
        }
        #region ICoreWorkingPositionableObject Members
        public int ZIndex
        {
            get {
                if (this.Parent != null)
                return this.Parent.Layers.IndexOf(this);
                return int.MinValue;
            }
        }
        public ICoreWorkingPositionableObjectContainer Container
        {
            get {
                return this.Parent as ICoreWorkingPositionableObjectContainer;
            }
        }
        public void MoveBack()
        {
            this.Container.MoveToBack(this);
        }
        public void MoveFront()
        {
            this.Container.MoveToFront(this);
        }
        public void MoveEnd()
        {
            this.Container.MoveToEnd(this);
        }
        public void MoveStart()
        {
            this.Container.MoveToStart(this);
        }
        public void MoveAt(int index)
        {
            this.Container.MoveAt(this, index);
        }
        #endregion
        /// <summary>
        /// select elements
        /// </summary>
        /// <param name="elements"></param>
        public void Select(params WPFLayeredElement[] elements)
        {
            this.SelectedElements.Select(elements);
        }
        /// <summary>
        /// clear the current layer
        /// </summary>
        public void Clear()
        {
            this.Select(null);
            this.Elements.Clear();
        }
        internal Rectangled GetBound()
        {
            return this.Parent.GetBound();
        }
        internal void RemoveSelectedItem()
        {
            if (this.SelectedElements.Count > 0)
            {
                var tab = this.SelectedElements.ToArray();
                this.Select(null);
                for (int i = 0; i < tab.Length; i++)
                {
                    this.Elements.Remove(tab[i]);
                }
            }
        }
        internal void SelectAll()
        {
            var tab = this.Elements.ToArray();
            this.Select(tab);
        }
    }
}

