using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SymbolEditorAddIn
{
    [CoreAdvancedElementAttribute("Symbols", typeof (Mecanism)) ]
    /// <summary>
    /// represent group of symbol
    /// </summary>
    public class CombinedPathElement :
        Core2DDrawingDualBrushElement,
        ICore2DDrawingElementContainer ,
        //ICore2DDrawingVisitable ,
        ICoreBrushOwner ,
        ICoreBrushContainer ,
        ICoreWorkingModifiableElementContainer,
        ICore2DDrawingSelectionContainer
    {

        public override bool Contains(Vector2f point)
        {
            point = base.ToDocumentPoint(point);
            foreach (ICore2DDrawingLayeredElement e in this.Elements) {
                if (e.Contains(point)) {
                    return true;
                }
            }
            return base.Contains(point);
        }

        public class SymCombinedElementcollection : 

            CoreWorkingObjectCollections<Core2DDrawingLayeredElement>,
            ICoreSerializable, IEnumerable, ICoreWorkingElementCollections
        {
            private CombinedPathElement m_owner;
          
            public SymCombinedElementcollection(CombinedPathElement symCombinedElement):base()
            {                
                this.m_owner = symCombinedElement;
            }


            public bool IsReadOnly
            {
                get { return false; }
            }

            public override string ToString()
            {
                return this.GetType().Name + "[" + this.Count + "]"; 
            }

            public Core2DDrawingLayeredElement this[int index]
            {
                get { return this.Elements[index]; }
            }

            internal void AddRange(Core2DDrawingLayeredElement[] items)
            {
                foreach (var item in items)
                {
                    this.Add(item);
                }
            }
            public override void Add(Core2DDrawingLayeredElement element)
            {
                if (element == null)
                    return;
                if (element.Parent != this.m_owner)
                {
                    if (element.Parent is ICoreWorkingModifiableElementContainer)
                    {
                        (element.Parent as ICoreWorkingModifiableElementContainer).Remove(element);
                    }
                    base.Add(element);
                    element.Parent = this.m_owner;
                    element.PropertyChanged += element_PropertyChanged;
                    this.m_owner.InitElement();
                }
            }
            public override void Remove(Core2DDrawingLayeredElement element)
            {
                base.Remove(element);
                element.PropertyChanged -= element_PropertyChanged;
            }

            void element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.m_owner.InitElement();
            }

            ICoreWorkingObject ICoreWorkingElementCollections.this[int index]
            {
                get { return this.Elements[index]; }
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

            
        }

        private SymCombinedElementcollection m_Elements;


        [CoreXMLElement()]
        public SymCombinedElementcollection Elements
        {
            get { return m_Elements; }
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
           // base.ReadElements(xreader);
            xreader.MoveToElement();
            ICoreWorkingObject[] c = CoreXMLSerializerUtility.GetAllObjects(xreader);
            foreach (Core2DDrawingLayeredElement item in c)
            {
                if (item == null) continue;
                this.m_Elements.Add(item);
            }
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            //this.Elements.Serialize(xwriter);
        }
        public CombinedPathElement(){
            m_Elements = new SymCombinedElementcollection(this);
 
        }
        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_Elements.GetEnumerator();
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (m_Elements == null)
                return;
            int len = this.Elements.Count;
            for (int i = 0; i < len; i++)
            {
                path.Add(this.Elements[i].GetPath ());
            }

            path.FillMode = enuFillMode.Winding;
        }

        public static CombinedPathElement CreateElement(Core2DDrawingLayeredElement[] items) {
            if ((items ==null) || (items.Length == 0))
            return null;

            CombinedPathElement e = new CombinedPathElement();
            e.Elements.AddRange(items);
            e.InitElement();
            return e;
        }


        public new class Mecanism :Core2DDrawingMecanismBase<CombinedPathElement, ICore2DDrawingSurface>{
            protected override void OnMouseDown(ICore.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(ICore.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(ICore.WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }

            protected override CombinedPathElement CreateNewElement()
            {
                return null;
            }
        }

        public T GetElementById<T>(string id) where T : class
        {
            throw new NotImplementedException();
        }

        public object GetElementById(string id)
        {
            throw new NotImplementedException();
        }

        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements
        {
            get {return this.m_Elements ; }
        }

        public void Add(ICoreWorkingObject obj)
        {
            throw new NotImplementedException();
        }

        public void Remove(ICoreWorkingObject obj)
        {
            this.m_Elements.Remove(obj as Core2DDrawingLayeredElement );// throw new NotImplementedException();
        }

        //public bool Accept(ICore2DDrawingVisitor visitor)
        //{
        //    return (visitor != null);
        //}

        //public void Visit(ICore2DDrawingVisitor visitor)
        //{
        //    //visit
        //    object obj = visitor.Save();
        //    visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);            
        //    foreach (ICore2DDrawingLayeredElement item in this.Elements)
        //    {
        //        if (item.View)
        //            visitor.Visit(item);
        //    }
        //    visitor.Restore(obj);
        //}

        public void Select(params ICore2DDrawingLayeredElement[] items)
        {
            throw new NotImplementedException();
        }

        ICoreWorkingElementCollections ICore2DDrawingSelectionContainer.Elements
        {
            get { return this.Elements; }
        }

        public bool Select(Vector2f point, bool deepSearch)
        {
            return false;
        }
    }
}
