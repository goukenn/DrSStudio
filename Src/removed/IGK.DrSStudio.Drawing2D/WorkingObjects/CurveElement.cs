

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CurveElement.cs
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
file:CurveElement.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a curve element
    /// </summary>
    [Core2DDrawingStandardItem ("Curve", typeof (Mecanism ))]
    class CurveElement : Core2DDrawingLayeredDualBrushElement, ICore2DFillModeElement,
        ICore2DClosableElement 
    {
        List<Vector2f> m_points;
        /// <summary>
        /// represent a attribute to save
        /// </summary>
        [IGK.DrSStudio.Codec.CoreXMLAttribute ()]
        [System.ComponentModel.TypeConverter(typeof(Vector2f.Vector2fArrayTypeConverter))]
        public Vector2f[] Points {
            get {
                return this.m_points.ToArray();
            }
        }
        public CurveElement()
        {
            this.m_points = new List<Vector2f>();
        }
        protected override void GeneratePath()
        {
            if (this.m_points.Count <= 1)
            {
                this.SetPath(null);
                    return ;
            }
            CoreGraphicsPath path = new CoreGraphicsPath();
            PointF[] tab = this.m_points.ToArray().ToGdiPoint ();
            path.AddCurve(tab);
            if (this.Closed)
                path.CloseFigure();
            path.enuFillMode = this.enuFillMode;
            this.SetPath(path);
        }
        public override void RenderSelection(Graphics g, WinUI.ICore2DDrawingSurface surface)
        {
            if ((this.EditorMecanism != null) && this.EditorMecanism.DesignMode)
                return;
            base.RenderSelection(g, surface);
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity)
                return;
            Vector2f[] t  =  CoreMathOperation.TransformVector2fPoint(m, this.m_points.ToArray());
            this.m_points.Clear ();
            this.m_points.AddRange(t);
        }
        protected override void ReadAttributeValue(string name, string value)
        {
            switch (name.ToLower ())
            {
                case "points":
                    Vector2f.Vector2fArrayTypeConverter c = new Vector2f.Vector2fArrayTypeConverter();
                    Vector2f[] t = c.ConvertFromString(value) as Vector2f[];
                    if ((t!=null) && (t.Length > 1))
                    {
                        this.m_points.Clear();
                        this.m_points.AddRange(t);
                    }
                    return;
                default:
                    break;
            }
            base.ReadAttributeValue(name, value);
        }
        /// <summary>
        /// represent the curve element mecanism
        /// </summary>
        public new class Mecanism : Core2DDrawingMecanismBase
        {
            public new CurveElement Element {
                get {
                    return base.Element as CurveElement;
                }
                set {
                    this.Element = value;
                }
            }
            protected override void UpdateCreateElement(WinUI.CoreMouseEventArgs e)
            {
                this.Element.m_points.Add(e.FactorPoint);
                this.Element.InitElement();
                this.CurrentSurface.Invalidate();
            }
            protected override void UpdateSnippetElement(WinUI.CoreMouseEventArgs e)
            {
                if ((this.Snippet != null) && this.Element.m_points.IndexExists(this.Snippet.Index))
                    this.Element.m_points[this.Snippet.Index] = e.FactorPoint;
            }
            protected override void GenerateSnippets()
            {
                base.GenerateSnippets();
                CurveElement l = this.Element;
                if (l == null) return;
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
        }
        private enuFillMode m_FillMode;
        public enuFillMode enuFillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition);
                }
            }
        }
        private bool m_Closed;
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition);
                }
            }
        }
    }
}

