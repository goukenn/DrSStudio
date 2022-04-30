
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    [Core2DDrawingTextGrouplement ("ListView", typeof (Mecanism))]
    public class ListViewElement : RectangleElement, ICore2DDrawingVisitable
    {
        private float m_Padding;
        private CoreUnit m_Radius;
        private List<ICore2DDrawingLayeredElement> m_elements;
        private RoundRectangleElement c_rc;
        private IListViewDataAdapter m_Adapter;
        private bool m_initPath;
        private CoreUnit m_LineHeight;
        [CoreConfigurableProperty()]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue("1px")]
        public CoreUnit LineHeight
        {
            get { return m_LineHeight; }
            set
            {
                if (m_LineHeight != value)
                {
                    m_LineHeight = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        /// <summary>
        /// get or set the list view data adapter
        /// </summary>
        public IListViewDataAdapter Adapter
        {
            get { return m_Adapter; }
            set {
                if (this.m_Adapter != value)
                {
                    this.m_Adapter = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeValue("5mm")]
        public CoreUnit Radius
        {
            get { return m_Radius; }
            set
            {
                if (m_Radius != value)
                {
                    m_Radius = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreConfigurableProperty()]
        [CoreXMLAttribute]
        [CoreXMLDefaultAttributeUnitValueAttribute("2mm")]
        public float Padding
        {
            get { return m_Padding; }
            set
            {

                if ((m_Padding != value) && (value >= 0))
                {
                    m_Padding = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
     
        
        
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_Radius = "5mm";
            this.m_Padding = "2mm".ToPixel();
            this.m_LineHeight = "1px";
            this.c_rc = new RoundRectangleElement();
            this.m_elements = new List<ICore2DDrawingLayeredElement>();
            this.c_rc.PropertyChanged += (o, e) => { OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition); };
        }
     
   
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {
            if (  this.m_initPath )
                return ;
            this.m_initPath = true ;
            p.Reset();
            if (this.m_Adapter == null)
            {
                p.AddRectangle(this.Bounds);
                this.m_initPath = false;
                return;
            }
            ICoreWorkingApplicationContextSurface v_surface=  this.ParentDesigner as ICoreWorkingApplicationContextSurface;
            Rectanglef v_rc = new Rectanglef(this.Bounds.Location, Size2f.Empty);
            Rectanglef v_rci = Rectanglef.Empty;
            float v_lineheight = this.LineHeight.GetPixel();
            for (int i = 0; i < this.m_Adapter.Count; i++)
            {
                var s = this.m_Adapter.GetView(v_surface, i);
                if (s != null)
                {
                    this.m_elements.Add(s);

                    v_rci = s.GetBound();
                    s.Translate(-v_rci.X, -v_rci.Y + v_rc.Y + v_lineheight, enuMatrixOrder.Append);
                    v_rc.Y += v_rci.Height + v_lineheight;
                    v_rc.Height += v_rc.Height + v_lineheight;
                    v_rc.Width = Math.Max(v_rc.Width, v_rci.Width );
                }

            }

            var v_textrc = v_rc;
            v_rc.Inflate(Padding, Padding);

            float v_radius = this.Radius.GetPixel();
            v_textrc.Inflate(v_radius, v_radius);

            var v_rc2 = new Rectanglef(v_rc.X, v_rc.Y, Math.Max(v_radius * 2, v_rc.Width), Math.Max(v_radius * 2, v_rc.Height));
            v_rc2.Width = Math.Max(v_textrc.Width, v_rc2.Width);
            v_rc2.Height = Math.Max(v_textrc.Height, v_rc2.Height);

      
          
            if (v_rc.IsEmptyOrSizeNegative)
            {
                Rectanglef v_p = new Rectanglef(v_rc.Location, Size2f.Empty);
                v_p.Inflate(Padding, Padding);
                p.AddRectangle(v_p);
                this.m_initPath = false;
                return;
            }

            c_rc.SuspendLayout();
            c_rc.Bounds = v_rc2;
            c_rc.RoundStyle = enuRoundStyle.Standard;
            c_rc.SetAllRadius(this.Radius, this.Radius);

            c_rc.ResumeLayout();
            p.Add(c_rc.GetPath());
            this.m_initPath = false;
        }

        new class Mecanism : RectangleElement.Mecanism
        { 
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }
        protected override void Translate(float dx, float dy, enuMatrixOrder order, bool temp)
        {
            base.Translate(dx, dy, order, temp);
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m  = GetMatrix();

            if (!m.IsIdentity)
            {
                foreach (var item in this.m_elements)
                {
                    item.MultTransform(m, enuMatrixOrder.Append);
                }
            }
            base.BuildBeforeResetTransform();
        }
        public void Visit(ICore2DDrawingVisitor visitor)
        {
            if (!Accept(visitor))
                return;
            var g = visitor.Save();
            visitor.MultiplyTransform(this.GetMatrix(), enuMatrixOrder.Prepend);
            foreach (var item in this.m_elements)
            {
                visitor.Visit(item);
            }
            visitor.Restore(g);
        }
    }
}
