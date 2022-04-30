

/*
IGKDEV @ 2008 - 2014
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
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:GroupElement.cs
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
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing;
namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingStandardItem("Group",
      typeof(Mecanism),
      IsVisible = false)]
    public class GroupElement : 
        Core2DDrawingLayeredElement,
        ICore2DElementsContainer
    {
        private GroupElementCollections m_Elements;
        public GroupElementCollections Elements
        {
            get { return m_Elements; }
        }
        private Rectanglei m_Bound;
        public Rectanglei Bound
        {
            get { return m_Bound; }
        }
        private bool m_IsClipped;
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [IGK.DrSStudio.Codec.CoreXMLDefaultAttributeValue (false)]
        public bool IsClipped
        {
            get { return m_IsClipped; }
            set
            {
                if (m_IsClipped != value)
                {
                    m_IsClipped = value;
                }
            }
        }
        protected override void GeneratePath()
        {
            if ((this.m_Elements == null) || (this.m_Elements.Count == 0))
            {
                this.SetPath(null);
                return;
            }
            CoreGraphicsPath v_p = new CoreGraphicsPath();
            v_p.AddRectangle(m_Bound);
            this.SetPath(v_p);
        }
        public static GroupElement CreateElement(ICore2DDrawingLayeredElement[]  elements)
        {
            if ((elements == null) || (elements.Length == 0))
            {
                return null ;
            }
            GroupElement m_doc = new GroupElement();
            m_doc.Elements.AddRange(elements);
            RectangleF[] t = new RectangleF[m_doc.m_Elements.Count];
            for (int i = 0; i < t.Length; i++)
			{
                t[i] = m_doc .m_Elements[i].GetBound();
			}
            m_doc.m_Bound = Rectanglef.Round (CoreMathOperation.GetBounds(t));
            m_doc.InitElement();
            return m_doc;
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity) return;
            //foreach (ICore2DDrawingLayeredElement  item in this.m_Elements)
            //{
            //    item.MultTransform(m, enuMatrixOrder.Append);
            //}
        }
        public override void Draw(Graphics g)
        {
            Matrix mat = GetMatrix();
            GraphicsState s = g.Save();
            this.SetGraphicsProperty(g);
            Region rg = new Region(this.GetBound ());
            if (IsClipped)
                g.IntersectClip(rg);
            //g.MultiplyTransform(mat, enuMatrixOrder.Prepend  );
            foreach (ICore2DDrawingLayeredElement  l  in this.Elements )
            {
                if (l.View )
                    l.Render(g);
            }
            g.ExcludeClip(rg);
            g.Restore(s);
        }
        public GroupElement()
        {
            this.m_Elements = new GroupElementCollections(this);
            this.m_IsClipped = false;
        }
        sealed class Mecanism : Core2DDrawingMecanismBase
        {
        }
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.None; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null; 
        }
        public override object Clone()
        {
            return base.Clone();
        }
        protected override void WriteElements(IGK.DrSStudio.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (ICore2DDrawingLayeredElement  item in this.Elements)
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
                this.m_Elements.AddRange(obj);
                obj.Deserialize(xreader.ReadSubtree());
                return true;
            }
            return false;
        }
        protected override void  ReadElements(IGK.DrSStudio.Codec.IXMLDeserializer xreader)
        {
            if (xreader.NodeType == System.Xml.XmlNodeType.None)
                xreader.MoveToContent();
            m_Elements.Clear();
            IGK.DrSStudio.Codec.CoreXMLSerializerUtility.ReadElements(this, xreader, ReadAdditionnalPROC);
            //get build round bound
            RectangleF[] t = new RectangleF[this.m_Elements.Count];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = this.m_Elements[i].GetBound();
            }
            this.m_Bound = Rectanglef.Round(CoreMathOperation.GetBounds(t));
        }
        public class GroupElementCollections : 
            Core2DElementsContainerCollections,
            ICoreWorkingElementCollections
        {
            new GroupElement Owner { get { return base.Owner as GroupElement ;} }
            public GroupElementCollections(GroupElement owner):base(owner)
            {
            }
            public override void AddRange(params ICore2DDrawingElement[] elements)
            {                
                for (int i = 0; i < elements.Length; i++)
                {
                    ICore2DDrawingLayeredElement l = elements[i] as ICore2DDrawingLayeredElement;
                    (l as ICore2DDrawingObject).Parent = this.Owner;
                    l.PropertyChanged += new CoreWorkingObjectPropertyChangedEventHandler(GroupElementCollections_PropertyChanged);
                }
                base.AddRange(elements);
            }
            void GroupElementCollections_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
            {
                this.Owner.OnPropertyChanged(e);
            }
            public new ICore2DDrawingLayeredElement this[int index]
            {
                get { return base[index] as ICore2DDrawingLayeredElement ; }
            }
            public  override  void Clear()
            {
                if (this.Count == 0)
                    return;
                for (int i = 0; i < Elements.Count ; i++)
                {                    
                    this[i].PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(GroupElementCollections_PropertyChanged);
                }
                base.Clear();
                Owner.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
            public override bool Remove(ICore2DDrawingElement element)
            {
                if (base.Remove(element))
                {
                    (element as ICore2DDrawingLayeredElement ).PropertyChanged -= new CoreWorkingObjectPropertyChangedEventHandler(GroupElementCollections_PropertyChanged);
                    Owner.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                    return true;
                }
                return false;
            }
            public bool IsReadOnly
            {
                get { return true; }
            }
            ICoreWorkingObject ICoreWorkingElementCollections.this[int index]
            {
                get { return this[index]; }
            }
        }
        #region ICore2DElementsContainer Members
        ICore2DElementsContainerCollections ICore2DElementsContainer.Elements
        {
            get { return this.Elements; }
        }
        #endregion
        ICoreWorkingElementCollections ICoreWorkingElementContainer.Elements
        {
            get { return this.m_Elements ; }
        }
    }
}

