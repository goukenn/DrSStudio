

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AnchorElement.cs
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
file:AnchorElement.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Disposition.WorkingObjects
{
    [Core2DDrawingStandardItem ("Anchor", typeof (Mecanism))]
    public class AnchorElement : Core2DDrawingLayeredElement , ICore2DDrawingElementContainer 
    {
        AnchorElementCollections m_elements;
        public override void Draw(System.Drawing.Graphics g)
        {
            GraphicsState v_state = g.Save();
            Vector2f v_loc = Vector2f.Zero;
            Rectanglef v_bound = Rectanglef.Empty;
            Matrix m = new Matrix ();
            foreach (Core2DDrawingLayeredElement  item in this.m_elements)
            {
                v_loc = this.GetLocation(item);
                v_bound = item.GetBound();
                m.Reset();
                m.Translate(-v_bound.X + v_loc .X , -v_bound.Y + v_loc.Y );
                GraphicsContainer gcontainer =  g.BeginContainer();
                g.MultiplyTransform(m);
                item.Draw(g);
                g.EndContainer(gcontainer );
            }
            g.Restore(v_state);
            m.Dispose();
        }
        public override void Dispose()
        {
            foreach (IDisposable item in m_elements)
            {
                item.Dispose();
            }
            m_elements.Clear();
            base.Dispose();
        }
        public ICoreWorkingElementCollections Elements
        {
            get {
                return this.m_elements;
            }
        }
        public AnchorElement()
        {
            m_elements = new AnchorElementCollections(this);
        }
        protected override void WriteElements(DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (ICore2DDrawingLayeredElement item in this.m_elements)
            {
                item.Serialize(xwriter);
            }
        }
        bool ReadAdditionnalPROC(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            Core2DDrawingLayeredElement
                                 obj = CoreSystem.CreateWorkingObject(xreader.Name)
                                 as Core2DDrawingLayeredElement;
            if (obj != null)
            {
                this.m_elements.AddRange(new Core2DDrawingLayeredElement[]{obj});
                obj.Deserialize(xreader.ReadSubtree());
                return true;
            }
            return false;
        }
        protected override void ReadElements(DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent();
            m_elements.Clear();
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, ReadAdditionnalPROC);
        }
        public static AnchorElement CreateElement(params Core2DDrawingLayeredElement[] elements)
        {
            var v_t = from s in elements where (s != null) && !(s is AnchorElement) select s;
            if (v_t.Count() > 0)
            {
                AnchorElement v_c = new AnchorElement();
                v_c.m_elements.AddRange(v_t.ToArray());
                return v_c;
            }
            return null;
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.None; }
        }
        protected override void GeneratePath()
        {
            this.SetPath(null);
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        public override bool Contains(Vector2f position)
        {
            foreach (Core2DDrawingLayeredElement  item in this.m_elements)
            {
                if (item.Contains(position))
                    return true;
            }
            return false;
        }
        class AnchorElementCollections : ICoreWorkingElementCollections
        {
            private AnchorElement m_anchorElement;
            private List<Core2DDrawingLayeredElement> m_elements;
            private Dictionary<Core2DDrawingLayeredElement, Vector2f > m_posdic;
            public AnchorElementCollections(AnchorElement anchorElement)
            {
                this.m_anchorElement = anchorElement;
                this.m_posdic = new Dictionary<Core2DDrawingLayeredElement, Vector2f>();
                this.m_elements = new List<Core2DDrawingLayeredElement>();
            }
            public bool IsReadOnly
            {
                get { return false; }
            }
            public ICoreWorkingObject this[int index]
            {
                get
                {
                    return this.m_elements[index];
                }
            }
            public int Count
            {
                get { return this.m_elements.Count; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_elements.GetEnumerator();
            }
            internal void AddRange(Core2DDrawingLayeredElement[] elements)
            {
                this.m_elements.AddRange(elements);
                foreach (var item in elements)
                {
                    item.Parent = this.m_anchorElement;
                    item.AttachProperty("Location", this.m_anchorElement , typeof(Vector2f));
                    this.m_posdic.Add(item, Vector2f.Zero);
                }
            }
            internal void Clear()
            {
                foreach (var item in this.m_elements)
                {
                    item.DetachProperty("Location", this.m_anchorElement );
                }
                this.m_elements.Clear();
                this.m_posdic.Clear();
            }
            internal void SetLocation(Core2DDrawingLayeredElement element, Vector2f pos)
            {
                if (this.m_posdic.ContainsKey(element))
                    this.m_posdic[element] = pos;
            }
            internal Vector2f GetLocation(Core2DDrawingLayeredElement element)
            {
                if (this.m_posdic.ContainsKey(element))
                    return this.m_posdic[element];
                return Vector2f.Zero;
            }
        }
        public Vector2f GetLocation(Core2DDrawingLayeredElement element)
        {
            return this.m_elements.GetLocation(element);
        }
        public void SetLocation(Core2DDrawingLayeredElement element, Vector2f location)
        {
            this.m_elements.SetLocation(element, location );
        }
        class Mecanism : Core2DDrawingMecanismBase
        {
            protected override void OnMouseDown(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseMove(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(WinUI.CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
        }
    }
}

