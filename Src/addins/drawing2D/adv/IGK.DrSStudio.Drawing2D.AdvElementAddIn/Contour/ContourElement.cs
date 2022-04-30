

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContourElement.cs
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
file:ContourElement.cs
*/

using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGK.DrSStudio.Drawing2D.Contour
{
    using IGK.ICore;
    using IGK.ICore.Actions;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.Actions;
    using IGK.ICore.Drawing2D.Mecanism;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinUI.Configuration;
    using IGK.DrSStudio.Drawing2D.Contour.MecanismActions;
    using IGK.DrSStudio.Drawing2D.Contour.Actions;

    [Core2DDrawingStandardElement ("Contour", 
        typeof (Mecanism), 
        ImageKey ="DE_CONTOUR",
        IsVisible=false )]
    public partial class ContourElement : 
        Core2DDrawingLayeredElement,
        ICore2DDrawingSingleElementContainer,
        ICore2DDrawingVisitable ,
        ICoreBrushOwner 
    {
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            CoreXMLSerializerUtility.ReadElements(this, xreader, __ReadAdditionnalElementProc);
        }
        protected override void ReadAttributes(IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        protected override void WriteAttributes(IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
        }
        
        private bool __ReadAdditionnalElementProc(IXMLDeserializer deseri)
        {
            switch (deseri.Name .ToLower ())
            {
                case "element":
                    ICoreWorkingObject[] t =  CoreXMLSerializerUtility.GetAllObjects (deseri.ReadInnerXml ());
                    if ((t != null) && (t.Length >= 1))
                    {
                        if ((t[0] is ICore2DDrawingLayeredElement) && ((t[0] is ContourElement) == false ))
                        {
                            this.m_Element = t[0] as Core2DDrawingLayeredElement;
                            this.m_Element.Parent = this;
                            this.Init();
                        }
                    }
                    break;
                default:
                    break;
            }
            return false;
        }
        
        
        private ContourDefinitionCollection m_contours;
        private Core2DDrawingLayeredElement m_Element;
        private Matrix m_PenMatrix;
        private float  m_defaultWidth; //for configuration
        private float m_ScaleX;
        private float m_ScaleY;
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue(1.0f)]
        public float ScaleY
        {
            get { return m_ScaleY; }
            set
            {
                if (m_ScaleY != value)
                {
                    m_ScaleY = value;
                    this.OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute()]
        [CoreXMLDefaultAttributeValue (1.0f)]
        public float ScaleX
        {
            get { return m_ScaleX; }
            set
            {
                if (m_ScaleX != value)
                {
                    m_ScaleX = value;
                    this.OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition);
                }
            }
        }
        [System.ComponentModel.Browsable(false)]
        public override bool CanTranslate
        {
            get
            {
                return true;
            }
        }
        [System.ComponentModel.Browsable(false)]
        public override bool CanRotate
        {
            get
            {
                return base.CanRotate;
            }
        }
        [System.ComponentModel.Browsable(false)]
        public override bool CanScale
        {
            get
            {
                return base.CanScale;
            }
        }
        [System.ComponentModel.Browsable(false)]
        /// <summary>
        /// get the pen matrix
        /// </summary>
        public Matrix PenMatrix
        {
            get {
                this.m_PenMatrix.Reset();
                this.m_PenMatrix.Scale ( this.m_ScaleX, this.m_ScaleY);
                return m_PenMatrix; 
            }           
        }
        public ContourElement()
        {
            this.m_contours = new ContourDefinitionCollection(this);
            this.m_PenMatrix = new Matrix();
            this.m_Element = null;
            this.m_defaultWidth = 1.0f;
            this.m_ScaleX = 1.0f;
            this.m_ScaleY = 1.0f;
        }

        /// <summary>
        /// get the only element for this countour element
        /// </summary>
        [CoreXMLElement ()]
        public Core2DDrawingLayeredElement Element {
            get {
                return this.m_Element;
            }
        }
        [CoreXMLElement()]
        public ContourDefinitionCollection Definitions {
            get {
                return this.m_contours;
            }
        }
        public override bool Contains(Vector2f position)
        {
            return CoreMathOperation.ApplyMatrix (this.m_Element.GetBound(), this.GetMatrix()).Contains(position);            
        }
     
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if (this.m_Element == null)
            {
                return;
            }
            float w = GetMaxWidth()/2.0f;
            CoreGraphicsPath p = this.m_Element.GetPath();
            Rectanglef v_rc = p.GetBounds();
            v_rc.Inflate(w, w);
            //path.Add(p);
            path.AddRectangle(v_rc);
        }
          public void Visit(ICore2DDrawingVisitor visitor)
        {
            
            //draw all pen
            if (this.m_Element == null) return;
            float w = GetMaxWidth();
            object s = visitor.Save();
            visitor.SetupGraphicsDevice(this);
            Matrix m = this.GetMatrix();
            //if (m.IsGdiMatrixValid())
            visitor.MultiplyTransform(m, enuMatrixOrder.Prepend);
            CoreGraphicsPath v_p = this.m_Element.GetPath();
            ICorePen v_pen = null;
            float bck = 0.0f;
            foreach (ContourDefinition item in m_contours)
            {
                 
                v_pen = item.Pen;
                if (v_pen != null)
                {
                    bck = item.Pen.Width;
                    v_pen.Width = w;
                    visitor.DrawPath(v_pen, v_p);
                    //restore the pen width
                    v_pen.Width = bck;
                    w -= item.Width;
                }
            }
            visitor.Visit(this.m_Element);
            visitor.Restore(s);
        }

       
        public static ContourElement CreateElement(Core2DDrawingLayeredElement l)
        {
            if (l is ContourElement)
                return null;
            ContourElement element = new ContourElement();
            element.m_Element = l;
            element.AddContour(3);
            element.AddContour(4);
            element.m_contours[1].Pen.SetSolidColor(Colorf.White);
            element.m_contours[0].Pen.SetSolidColor(Colorf.Black);
            l.Parent = element;
            element.Init();
            return element;
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }
        
        private void Init()
        {
            this.m_Element.PropertyChanged += m_Element_PropertyChanged;
        }
        void m_Element_PropertyChanged(object o, CoreWorkingObjectPropertyChangedEventArgs e)
        {
            OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
        }



        public override void Dispose()
        {
            if (this.m_PenMatrix != null)
            {
                this.m_PenMatrix.Dispose();
                this.m_PenMatrix = null;
            }
            this.m_Element.PropertyChanged -= m_Element_PropertyChanged;
        }
        public override Rectanglef GetBound()
        {
            return base.GetBound();// this.m_Element.GetBound();
        }
       
        /// <summary>
        /// for configuration parametersr
        /// </summary>
        /// <returns></returns>
        public override enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            parameters = base.GetParameters(parameters);
            var group = parameters.AddGroup("Contour");
            group.AddItem(this.GetType().GetProperty("ScaleX"));
            group.AddItem(this.GetType().GetProperty("ScaleY"));
            group.AddEnumItem("ContourEditSelectBrush", "lb.contourSelectedEditBrush.caption", this.m_contours ,  new CoreParameterChangedEventHandler(__contourParameterSelectBrushHandler));
            group.AddActions (new CoreParameterActionBase("EditContourBrush", "lb.editContourBrush.Caption", new ContourEditBrushAction(this)));
            group.AddItem("ContourAddWidth", "lb.contourAddWith.caption",this.m_defaultWidth , enuParameterType.SingleNumber , new CoreParameterChangedEventHandler(__contourAddWidthParameterHandler));
            group.AddActions(new CoreParameterActionBase("AddContour", "lb.AddContour.caption", new AddContourAction(this)) { 
                Reload = true
            });
            group.AddActions(new CoreParameterActionBase("ClearContour", "lb.ClearContour.caption", new ClearContourAction(this)) { 
                Reload = true
            });
            group = parameters.AddGroup("Element");
            group.AddActions(new CoreParameterActionBase("EditElement", "lb.EditElement.caption", new EditContourElement(this)));
            return parameters;
        }
        int index;
        private void __contourParameterSelectBrushHandler(object sender, CoreParameterChangedEventArgs e)
        {
            this.index = this.IndexOf ( e.Value as ContourDefinition );
        }
        private void __contourAddWidthParameterHandler(object sender, CoreParameterChangedEventArgs e)
        {
            this.m_defaultWidth = (float)((CoreUnit)e.Value.ToString()).GetValue (enuUnitType.px);
        }
       
     
        public void AddContour(float width)
        {
            if (width > 0.0f)
            {
                ContourDefinition def = new ContourDefinition(width, this);                
                this.m_contours.Add(def);
                this.InitElement();
                this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        public void ClearContour()
        {
            if (this.m_contours.Count > 0)
            {
                //remove brush defintion
                foreach (ContourDefinition def in this.m_contours )
                {
                    def.Dispose();
                }
                this.m_contours.Clear();
                this.InitElement();
                this.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }
        }
        /// <summary>
        /// retreive the maximun with
        /// </summary>
        /// <returns></returns>
        private float GetMaxWidth()
        {
            if (this.m_Element == null)
            {
                return 0;
            }
            ICorePen b = this.m_Element.GetBrush(enuBrushMode.Stroke) as CorePen ;
            float w = 0.0f;
            if (b == null)
            {
                w = 1;
            }
            else
                w = b.Width;
            foreach (ContourDefinition  item in this.m_contours)
            {
                w += item.Width;
            }
            return w;
        }
        
        public override enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.StrokeOnly; }
        }
        public override ICoreBrush GetBrush(enuBrushMode mode)
        {
            //return the element brush
            return (this.m_Element as ICoreBrushOwner ).GetBrush(mode);            
        }
        /// <summary>
        /// return the host element
        /// </summary>
        ICore2DDrawingElement ICore2DDrawingSingleElementContainer.Element
        {
            get { return this.m_Element; }
        }
        /// <summary>
        /// get the index of this contour definition
        /// </summary>
        /// <param name="contourDefinition"></param>
        /// <returns></returns>
        internal int IndexOf(ContourDefinition contourDefinition)
        {
            return this.m_contours.IndexOf(contourDefinition);
        }
        ICoreWorkingObject ICoreWorkingSingleElementHost.Element
        {
            get { return this.Element; }
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }


        class ContourEditBrushAction : CoreActionBase
        {
            private ContourElement contourElement;
            public ContourEditBrushAction(ContourElement contourElement)
            {
                this.contourElement = contourElement;
            }
            protected override bool PerformAction()
            {
                int i = this.contourElement.index;
                ContourDefinition def = this.contourElement.m_contours[i];
                if (def != null)
                    new CoreEditBrushAction(def.Pen, def.BrushSupport).DoAction();
                return false;
            }
            public override string Id
            {
                get { return "{73FFCD05-44EE-4DA7-8086-A03DF9E622FD}"; }
            }
        }
        public class ContourDefinitionCollection : IEnumerable, ICoreSerializerService, IList
        {
            List<ContourDefinition> m_contours;
            private ContourElement m_element;
            public bool IsValid
            {
                get { return true; }
            }
            public ContourDefinition this[int index]
            {
                get
                {
                    if (this.m_contours.IndexExists(index))
                        return this.m_contours[index];
                    return null;
                }
            }
            public ContourDefinitionCollection(ContourElement element)
            {
                this.m_element = element;
                this.m_contours = new List<ContourDefinition>();
            }
            public ContourDefinition[] ToArray()
            {
                return this.m_contours.ToArray();
            }
            public int Count
            {
                get
                {
                    return this.m_contours.Count;
                }
            }
            public void Clear()
            {
                this.m_contours.Clear();
            }
            public void Add(ContourDefinition def)
            {
                this.m_contours.Add(def);
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_contours.GetEnumerator();
            }
            internal int IndexOf(ContourDefinition contourDefinition)
            {
                return this.m_contours.IndexOf(contourDefinition);
            }
            public void Deserialize(IXMLDeserializer xreader)
            {
                xreader.ReadSubtree();
                this.m_contours.Clear();
                while (xreader.Read())
                {
                    switch (xreader.NodeType)
                    {
                        case System.Xml.XmlNodeType.Element:
                            if (xreader.Name.ToLower() == "definition")
                            {
                                string s = xreader.GetAttribute("Stroke");
                                this.m_contours.Add(ContourDefinition.CreaeteFromString(this.m_element, s));
                            }
                            break;
                    }
                }
            }
            public void Serialize(IXMLSerializer xwriter)
            {
                foreach (ContourDefinition def in this.m_contours)
                {
                    xwriter.WriteStartElement("Definition");
                    xwriter.WriteAttributeString("Stroke", def.Pen.GetDefinition());
                    xwriter.WriteEndElement();
                }
            }
            public string Id
            {
                get { return null; }
            }
            int IList.Add(object value)
            {
                return 0;
            }
            public bool Contains(object value)
            {
                return this.m_contours.Contains(value);
            }
            int IList.IndexOf(object value)
            {
                return this.m_contours.IndexOf(value as ContourDefinition);
            }
            void IList.Insert(int index, object value)
            {
                ContourDefinition r = value as ContourDefinition;
                if (r != null)
                    this.m_contours.Insert(index, r);
            }
            public bool IsFixedSize
            {
                get { return false; }
            }
            public bool IsReadOnly
            {
                get { return true; }
            }
            public void Remove(object value)
            {
                this.m_contours.Remove(value as ContourDefinition);
            }
            public void RemoveAt(int index)
            {
            }
            object IList.this[int index]
            {
                get
                {
                    return this[index];
                }
                set
                {
                }
            }
            public void CopyTo(Array array, int index)
            {
            }
            public bool IsSynchronized
            {
                get { return false; }
            }
            public object SyncRoot
            {
                get { return null; }
            }
        }

        class AddContourAction : CoreActionBase
        {
            private ContourElement contourElement;
            protected override bool PerformAction()
            {
                contourElement.AddContour(this.contourElement.m_defaultWidth);
                return true;
            }
            /// <summary>
            /// .ctr
            /// </summary>
            /// <param name="contourElement"></param>
            public AddContourAction(ContourElement contourElement)
            {
                this.contourElement = contourElement;
            }
            public override string Id
            {
                get { return "{BF0C5501-B58B-4E7E-A59C-E0B5E5A59481}"; }
            }
        }


        /// <summary>
        /// Mecanism for contour element
        /// </summary>
        internal sealed class Mecanism : Core2DDrawingSurfaceMecanismBase<ContourElement>
        {
            protected override ContourElement CreateNewElement()
            {
                return null;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
                this.AddAction(enuKeys.A, new AddCourtourElementMatrix());
                this.AddAction(enuKeys.C, new ClearContourElementMecanism());
            }
            /// <summary>
            /// add a contour element
            /// </summary>
            internal void AddContour()
            {
                if (this.Element != null)
                    this.Element.AddContour(1.0f);
            }
            internal void ClearContour()
            {
                if (this.Element != null)
                    this.Element.ClearContour();
            }
        }
    }
}

