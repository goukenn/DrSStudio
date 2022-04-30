using IGK.ICore.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{
    [Core2DDrawingStandardElementAttribute("LayerObject", null,
        IsVisible = false)]
    public class LayerObjectElement : Core2DDrawingLayeredElement, 
        ICore2DDrawingSelectionContainer,
        ICoreWorkingElementContainer
    {
        List<Core2DDrawingLayer> m_items;

        public override bool CanReSize => false;
        public override bool CanScale => false;

        public Core2DDrawingLayer[] Layers => m_items.ToArray();
        ///<summary>
        ///public .ctr
        ///</summary>
        public LayerObjectElement()
        {
            this.m_items = new List<Core2DDrawingLayer>();
        }
        public void AddLayer(Core2DDrawingLayer layer) {
            if ((layer == null) || this.m_items.Contains(layer))
                return;
            this.m_items.Add(layer);
        }
        public void RemoveLayer(Core2DDrawingLayer layer) {
            this.m_items.Remove(layer);
        }

        public int Count => m_items.Count;

        public ICoreWorkingElementCollections Elements => GetWorkingCollection();


        private ICoreWorkingElementCollections GetWorkingCollection() {
            if (this.GetAttribute(nameof(GetWorkingCollection)) is ICoreWorkingElementCollections d)
                return d;


            d = new  Collection(this);
            this.SetAttribute(nameof(GetWorkingCollection), d);
            return d;
        }

        class Collection : ICoreWorkingElementCollections
        {
            private LayerObjectElement layerObjectElement;

            public Collection(LayerObjectElement layerObjectElement)
            {
                this.layerObjectElement = layerObjectElement;
            }

            public ICoreWorkingObject this[int index] =>  this.layerObjectElement.m_items [index];

            public int Count => this.layerObjectElement.m_items.Count;

            public bool IsReadOnly => true;

            public IEnumerator GetEnumerator()
            {
                return this.layerObjectElement.m_items.GetEnumerator();
            }
        }
        public override string Render()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var g in this.m_items) {
                sb.Append(g.Render());
            }
            return sb.ToString();
        }
       
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            var doc = this.ParentDocument;
            if (doc != null)
            {
                path.AddRectangle(0, 0, doc.Width, doc.Height);
            }

        }
        public override bool Contains(Vector2f point)
        {//dispatch to item
            return base.Contains(point);
        }

        public T GetElementById<T>(string id) where T : class
        {
            return m_items.GetElementById<T>(id);
        }

        public object GetElementById(string id)
        {
            return m_items.GetElementById(id);
        }

        public IEnumerator GetEnumerator()
        {
            return GetWorkingCollection().GetEnumerator();
        }
    }

   
}
