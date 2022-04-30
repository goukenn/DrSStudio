

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFDocument.cs
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
file:WPFDocument.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    [Serializable()]
    [WPFFBaseObject("Document")]
    public class WPFDocument :
        WPFElementBase,
        ICoreWorkingDocument,
        IWPFDocument
    {
        private System.Windows.UIElement m_RootElement;
        private WPFLayer m_CurrentLayer;
        private WPFLayerCollection  m_Layers;
        public event WPFElementEventHandler<IWPFLayer> LayerAdded;
        public event WPFElementEventHandler<IWPFLayer> LayerRemoved;
        public event WPFLayerChangeEventHandler CurrentLayerChanged;
        private double m_Width;
        private double  m_Height;
        [IGK.DrSStudio.Codec.CoreXMLAttribute()]
        public double  Height
        {
            get { return m_Height; }
            set
            {
                if (m_Height != value)
                {
                    m_Height = value;
                    this.RootElement.SetValue(System.Windows.Controls.Canvas.HeightProperty , value);
                    OnPropertyChanged( new CoreWorkingObjectPropertyChangedEventArgs (
                        CoreWorkingObjectPropertyChangedEventArgs.Definition.ID  + 100));
                }
            }
        }
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        public double Width
        {
            get { return m_Width; }
            set
            {
                if (m_Width != value)
                {
                    m_Width = value;
                    this.RootElement.SetValue(System.Windows.Controls.Canvas.WidthProperty, value);
                    OnPropertyChanged(new CoreWorkingObjectPropertyChangedEventArgs(
                        CoreWorkingObjectPropertyChangedEventArgs.Definition.ID + 100));
                }
            }
        }
        IWPFLayerCollections IWPFDocument.Layers { get { return this.Layers; } }
        protected override void ReadAttributes(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        protected override void OnLoadingComplete(EventArgs eventArgs)
        {
            this.RootElement.SetValue(System.Windows.Controls.Canvas.WidthProperty, this.Width);
            this.RootElement.SetValue(System.Windows.Controls.Canvas.HeightProperty , this.Height );
            base.OnLoadingComplete(eventArgs);
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (IGK.DrSStudio.Codec.ICoreSerializerService   item in this.Layers)
            {
                item.Serialize(xwriter);
            }
        }
        protected override void ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
          //  base.ReadElements(xreader);
            List<WPFLayer> v_list = new List<WPFLayer>();
            while (xreader.Read())
            {
                switch (xreader.NodeType)
                {
                    case System.Xml.XmlNodeType.Element:
                        WPFLayer
                                   obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                   as WPFLayer;
                        if (obj != null)
                        {
                            v_list.Add(obj);
                            obj.Deserialize(xreader.ReadSubtree());
                        }
                        else
                        {
                            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader.ReadSubtree(), null);
                        }
                        break;
                }
            }
            if (v_list.Count > 0)
                this.Layers.Replace(v_list.ToArray());
        }
        protected virtual void OnLayerAdded(WPFElementEventArgs<IWPFLayer> e)
        {
            if (this.LayerAdded != null)
                this.LayerAdded(this, e);
        }
        protected virtual void OnLayerRemoved(WPFElementEventArgs<IWPFLayer> e)
        {
            if (this.LayerRemoved != null)
                this.LayerRemoved(this, e);
        }
        protected virtual void OnCurrentLayerChanged(WPFLayerChangeEventArgs e)
        {
            if (this.CurrentLayerChanged != null)
                this.CurrentLayerChanged(this, e);
        }
        public override IGK.DrSStudio.WinUI.ICoreParameterConfigCollections GetParameters(IGK.DrSStudio.WinUI.ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Document");
            group.AddItem(GetType().GetProperty("Width"));
            group.AddItem(GetType().GetProperty("Height"));
            return parameters;
        }
        /// <summary>
        /// get the layers
        /// </summary>
        public WPFLayerCollection  Layers
        {
            get { return m_Layers; }
        }
        /// <summary>
        /// get the current layer
        /// </summary>
        public WPFLayer CurrentLayer
        {
            get { return m_CurrentLayer; }
            set
            {
                if (m_CurrentLayer != value)
                {
                    WPFLayerChangeEventArgs e = new WPFLayerChangeEventArgs(this.CurrentLayer, value);
                    if (m_CurrentLayer != null) UnRegisterlayerEvent();
                    m_CurrentLayer = value;
                    if (m_CurrentLayer != null) RegisterlayerEvent();
                    OnCurrentLayerChanged(e);
                }
            }
        }
        protected virtual void RegisterlayerEvent()
        {
            this.CurrentLayer.ElementAdded += new WPFElementEventHandler<IWPFLayeredElement>(CurrentLayer_ElementAdded);
            this.CurrentLayer.ElementRemoved += new WPFElementEventHandler<IWPFLayeredElement>(CurrentLayer_ElementRemoved);
        }
        protected virtual void UnRegisterlayerEvent()
        {
            this.CurrentLayer.ElementAdded -= new WPFElementEventHandler<IWPFLayeredElement>(CurrentLayer_ElementAdded);
            this.CurrentLayer.ElementRemoved -= new WPFElementEventHandler<IWPFLayeredElement>(CurrentLayer_ElementRemoved);
        }
        void CurrentLayer_ElementRemoved(object sender, WPFElementEventArgs<IWPFLayeredElement> e)
        {
        }
        void CurrentLayer_ElementAdded(object sender, WPFElementEventArgs<IWPFLayeredElement> e)
        {
        }
        public System.Windows.UIElement RootElement { get { return m_RootElement; } }
        //.ctr
        public WPFDocument()
        {
            this.m_RootElement = new System.Windows.Controls.Canvas();
            this.m_Layers = CreateLayerCollection();
            this.LayerRemoved += new WPFElementEventHandler<IWPFLayer>(_LayerRemoved);
            this.m_Height = 300;
            this.m_Width = 400;
        }
        void _LayerRemoved(object sender, WPFElementEventArgs<IWPFLayer> e)
        {
            if (e.Element == this.CurrentLayer)
            {
                this.CurrentLayer = this.Layers[0];
            }
        }
        private WPFLayerCollection CreateLayerCollection()
        {
            return new WPFLayerCollection(this);
        }
        /// <summary>
        /// Clear the current document
        /// </summary>
        public void Clear()
        {
            this.Layers.Clear();
        }
        public class WPFLayerCollection : IWPFLayerCollections
        {
            List<WPFLayer> m_layers;
            WPFDocument owner;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public WPFLayer this[int index] {
                get { return this.m_layers[index]; }
            }
            public WPFLayerCollection(WPFDocument owner)
            {
                this.m_layers = new List<WPFLayer>();
                this.owner = owner;
                WPFLayer c = owner.CreateNewLayer();
                this.m_layers.Add(c);
                c.Parent = this.owner;
                this.owner.CurrentLayer = c;
                (this.owner.RootElement as
                    System.Windows.Controls.Canvas).Children.Add(c.Canvas);
            }
            #region IWPFLayerCollections Members
            public int Count
            {
                get { return this.m_layers.Count; }
            }
            #endregion
            #region IEnumerable Members
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_layers.GetEnumerator();
            }
            #endregion
            void IWPFLayerCollections.Add(IWPFLayer layer) {
                this.Add(layer as WPFLayer);
            }
            void IWPFLayerCollections.Remove(IWPFLayer layer) {
                this.Remove(layer as WPFLayer);
            }
            public void Add(WPFLayer layer)
            {
                if ((layer == null) || (this.m_layers.Contains(layer)))
                    return;
                this.m_layers.Add(layer);
                layer.Parent = this.owner;
                this.owner.OnLayerAdded(new WPFElementEventArgs<IWPFLayer>(layer));
            }
            public  void Remove(WPFLayer layer)
            {
                if ((layer == null) || (!this.m_layers.Contains(layer)))
                    return;
                this.m_layers.Remove (layer);
                layer.Parent = null;
                this.owner.OnLayerRemoved (new WPFElementEventArgs<IWPFLayer>(layer));
            }
            public int IndexOf(WPFLayer wPFLayer)
            {
                return this.m_layers.IndexOf(wPFLayer);
            }
            public void Clear()
            {
                if (this.Count == 1)
                {
                    this.m_layers[0].Clear();
                }
                else {
                    WPFLayer[] l =  this.m_layers.ToArray();
                    foreach (var item in l)
                    {
                        item.Clear();
                        if (item == owner.CurrentLayer)
                            continue;
                        m_layers.Remove(item);
                        this.owner.OnLayerRemoved(new WPFElementEventArgs<IWPFLayer>(item ));
                    }
                }
            }
            internal void Replace(WPFLayer[] wPFLayer)
            {
                if ((wPFLayer == null) || (wPFLayer.Length == 0))
                    return ;
                this.m_layers.Clear();
                System.Windows.Controls.Panel p = 
                    this.owner.RootElement as System.Windows.Controls.Panel;
                p.Children.Clear();
                foreach (var i in wPFLayer)
                {
                    this.m_layers.Add(i);
                    i.Parent = this.owner;
                    p.Children.Add(i.Canvas);
                }
                this.owner.CurrentLayer = wPFLayer[0];
            }
        }
        IWPFLayer IWPFDocument.CurrentLayer
        {
            get
            {
                return this.CurrentLayer;
            }
            set
            {
                this.CurrentLayer = value as WPFLayer ;
            }
        }
        protected virtual WPFLayer CreateNewLayer()
        {
            return new WPFLayer();
        }
        /// <summary>
        /// add new layer
        /// </summary>
        public virtual void AdddNewLayer()
        {
            WPFLayer l = this.CreateNewLayer();
            this.Layers.Add(l);
        }
        public virtual void DropCurrentLayer()
        {
            if (this.Layers.Count > 1)
            {
                this.Layers.Remove(this.CurrentLayer);
            }
        }
        public Rectangled GetBound()
        {
            return new Rectangled(0, 0, this.Width, this.Height);
        }
        public Type DefaultSurfaceType
        {
            get { return typeof(WPFHostSurface); }
        }
    }
}

