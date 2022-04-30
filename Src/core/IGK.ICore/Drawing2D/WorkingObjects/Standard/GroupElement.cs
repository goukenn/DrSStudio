

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GroupElement.cs
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
file:GroupElement.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Group",
    null, IsVisible = false)]
    public class GroupElement :
        Core2DDrawingLayeredElement ,
        ICore2DDrawingElementContainer ,
        ICore2DDrawingVisitable ,
        ICoreBrushOwner ,
        ICoreBrushContainer ,
        ICoreWorkingModifiableElementContainer,
        ICore2DDrawingSelectionContainer
    {
        private GroupElementCollections m_Elements;
        private bool m_ShowBorder;
        private ICorePen m_BorderBrush;

        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_Elements.GetEnumerator();
        }
      
        public ICorePen BorderBrush
        {
            get { return m_BorderBrush; }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(false)]
        public bool ShowBorder
        {
            get { return m_ShowBorder; }
            set
            {
                if (m_ShowBorder != value)
                {
                    m_ShowBorder = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        public override void Dispose()
        {
            m_Elements.Dispose();
            base.Dispose();
        }
        protected override void BuildBeforeResetTransform()
        {
            using (Matrix m = this.GetMatrix().Clone() as Matrix )
            {
                if (!m.IsIdentity)
                {
                    foreach (Core2DDrawingLayeredElement item in this.m_Elements)
                    {
                        item.MultTransform(m, enuMatrixOrder.Append );
                        if (item is GroupElement)
                        {
                            (item as GroupElement).ResetTransform();
                        }
                    }
                }
            }
            base.BuildBeforeResetTransform();
        }
        public GroupElement():base()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Elements = new GroupElementCollections(this);
            this.m_ShowBorder = false;
            this.m_BorderBrush = new CorePen(this);
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            Rectanglef[] t = new  Rectanglef[this.Elements.Count];

            for (int i = 0; i < t.Length; i++)
			{
                t[i] = (this.Elements[i] as ICoreWorkingBoundResult ).GetBound();
			}         
            
            Rectanglef v_rc = CoreMathOperation.GetGlobalBounds(this.Elements.ToArray());
            Rectanglef v_rc2 = CoreMathOperation.GetBounds (t);
            path.AddRectangle(v_rc2);
        }
        protected override void ReadElements(ICore.Codec.IXMLDeserializer xreader)
        {
            xreader.MoveToElement();
            ICoreWorkingObject[] c = CoreXMLSerializerUtility.GetAllObjects(xreader);
            foreach (Core2DDrawingLayeredElement item in c)
            {
                if (item == null) continue;
                this.m_Elements.Add(item);
            }
        }
        protected override void WriteElements(ICore.Codec.IXMLSerializer xwriter)
        {
            this.Elements.Serialize(xwriter);
        }
        public GroupElementCollections Elements
        {
            get { return m_Elements; }
        }
        protected internal override void Translate(float dx, float dy, enuMatrixOrder order, bool temp)
        {
            base.Translate(dx, dy, order, temp);
        }


        public sealed class GroupElementCollections : 
            CoreWorkingObjectCollections<Core2DDrawingLayeredElement>, 
            ICoreWorkingElementCollections, 
            ICoreSerializable 
        {
            private GroupElement m_groupElement;
            public GroupElementCollections(GroupElement groupElement)
            {
                this.m_groupElement = groupElement;
            }
            public void Serialize(IXMLSerializer seri)
            {
                foreach (ICoreSerializerService item in this.Elements)
                {
                    if (item == null)
                        continue;
                    item.Serialize(seri);
                }
            }
            public ICoreWorkingObject this[int index]
            {
                get {
                    return this.Elements[index];
                }
            }
            public bool IsReadOnly
            {
                get { return false; }
            }
            public override void Add(Core2DDrawingLayeredElement element)
            {
                if (element == null)
                    return;
                if (element.Parent != this.m_groupElement)
                {
                    if (element.Parent is ICoreWorkingModifiableElementContainer)
                    {
                        (element.Parent as ICoreWorkingModifiableElementContainer).Remove(element);
                    }
                    base.Add(element);
                    element.Parent = this.m_groupElement;
                    element.PropertyChanged += element_PropertyChanged;
                    this.m_groupElement.InitElement();
                }
            }
            public override void Remove(Core2DDrawingLayeredElement element)
            {
                base.Remove(element);
                element.PropertyChanged -= element_PropertyChanged;
            }

            void element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                switch ((enu2DPropertyChangedType)e.ID)
                {
                        //ignore data
                    case enu2DPropertyChangedType.BrushChanged:
                    case enu2DPropertyChangedType.IdChanged:
                        break;
                    default:
                        this.m_groupElement.InitElement();
                        break;
                }
            }

            internal object GetElementById(string id)
            {
                foreach (var item in this.Elements )
                {
                    if (item.Id == id)
                        return item;
                    if (item is ICoreWorkingElementContainer)
                    {
                        var b = (item as ICoreWorkingElementContainer).GetElementById(id);
                        if (b != null)
                            return b;
                    }
                }
                return null;
            }
            /// <summary>
            /// dispose all contained elements
            /// </summary>
            internal void Dispose()
            {
                foreach (var t in this.Elements)
                {
                    t.Dispose();
                }
            }
        }
       
        
        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements
        {
            get { return this.m_Elements; }
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return (visitor != null);
                
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            object obj = visitor.Save();
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend  );
            if (this.ShowBorder)
            {
                Rectanglef v_rc = this.GetDefaultBound ();
                visitor.DrawRectangle(this.m_BorderBrush,
                    v_rc.X, v_rc.Y, v_rc.Width, v_rc.Height);

            }
            foreach (ICore2DDrawingLayeredElement item in this.Elements)
            {
                if(item.View )
                visitor.Visit(item);
            }
            visitor.Restore(obj);
        }

        public void Remove(ICoreWorkingObject obj)
        {
            if ((obj is Core2DDrawingLayeredElement s) && (this.m_Elements.Contains(s)))
            {
                this.m_Elements.Remove(s);
                this.InitElement();
                OnPropertyChanged(Core2DDrawingChangement.Definition);
            }
        }

        public void Add(ICoreWorkingObject obj)
        {
            if ((obj is Core2DDrawingLayeredElement s) && (!this.m_Elements.Contains(s)))
            {
                this.m_Elements.Add(s);
                this.InitElement();
                OnPropertyChanged(Core2DDrawingChangement.Definition);
            }
        }

        public  T GetElementById<T>(string id) where T:class 
        {
            var o = this.m_Elements.GetElementById(id);
            return o as T;
        }
        public object GetElementById(string name)
        {
            return GetElementById<Object>(name);
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return mode == enuBrushMode.Stroke ? m_BorderBrush : null;
        }
        [Browsable(false)]
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.Solid | enuBrushSupport.Stroke;
            }
        }

        public ICoreBrush[] GetBrushes()
        {
            return new ICoreBrush[]{ this.m_BorderBrush};
        }


        public static GroupElement CreateElement(ICore2DDrawingLayer layer)
        {
            if (layer != null)
            {
                GroupElement g = new GroupElement();
                foreach (Core2DDrawingLayeredElement m in layer.Elements)
                {
                    g.Elements.Add(m);
                }
                return g;
            }
            return null;
        }
        public static GroupElement CreateElement(ICore2DDrawingDocument document)
        {
            if ((document == null) || (document.Layers .Count == 0))
            return null;
            
            if (document.Layers.Count == 1)
            {
                return CreateElement(document.CurrentLayer);
            }
            else {
                GroupElement g = new GroupElement();
                foreach (ICore2DDrawingLayer  m in document.Layers)
                {
                    g.Elements.Add(CreateElement(m));
                }
                return g;
            }
        }
        ICoreWorkingElementCollections ICore2DDrawingSelectionContainer.Elements
        {
            get { return this.Elements; }
        }
    }
}

