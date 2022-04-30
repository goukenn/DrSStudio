

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ContourDefinition.cs
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
file:ContourDefinition.cs
*/
using System; 
using IGK.ICore; using IGK.ICore.Drawing2D;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace IGK.DrSStudio.Drawing2D.Contour
{
    partial class ContourElement
    {
        /// <summary>
        /// 
        /// </summary>
        public class ContourDefinition :
            ICoreBrushContainer,              
            ICoreBrushOwner, 
            IDisposable 
        {
            private ICorePen m_Pen;
            private ContourElement m_Element;
            public override string ToString()
            {
                return "Contour : " + m_Element.IndexOf(this);
            }
            public ContourDefinition(float width, ContourElement element)
            {
                this.m_Element = element;
                this.m_Pen = new ContourPen(this);
                this.m_Pen.Width = width;
                this.m_Pen.BrushDefinitionChanged += m_Pens_BrushDefinitionChanged;
            }
            /// <summary>
            /// dispose the current 
            /// </summary>
            public void Dispose()
            {
                this.m_Pen.BrushDefinitionChanged -= m_Pens_BrushDefinitionChanged;
                this.m_Pen.Dispose();
                this.m_Pen = null;
            }
            void m_Pens_BrushDefinitionChanged(object sender, EventArgs e)
            {
                this.m_Element.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.BrushChanged));
            }
            /// <summary>
            /// get the contour element that own this brush definition
            /// </summary>
            public ContourElement Element
            {
                get { return m_Element; }
            }
            public ICorePen Pen
            {
                get { return m_Pen; }
                set
                {
                    if (m_Pen != value)
                    {
                        m_Pen = value;
                    }
                }
            }
            /// <summary>
            /// get or set the contour definition
            /// </summary>
            public float Width
            {
                get { return this.m_Pen.Width; }
                set
                {
                    this.m_Pen.Width = value;
                }
            }
            public ICoreBrush GetBrush(enuBrushMode mode)
            {
                return this.m_Pen;
            }
            public ICoreBrush[] GetBrushes()
            {
                return new ICoreBrush[] { this.m_Pen };
            }
            public string Id
            {
                get
                {
                    return "Contourdefinition";
                }
            }
            public Rectanglef GetBound()
            {
                return this.Element.GetBound();
            }
            public Rectanglef GetDefaultBound()
            {
                return this.Element.GetDefaultBound();
            }
            /// <summary>
            /// get the pen matrix
            /// </summary>
            /// <returns></returns>
            public Matrix GetMatrix()
            {
                return this.Element.PenMatrix;
            }
            public CoreGraphicsPath GetPath()
            {
                return this.m_Element.GetPath();
            }
            public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged
            {
                add
                {
                    this.m_Element.PropertyChanged += value;
                }
                remove
                {
                    this.m_Element.PropertyChanged -= value;
                }
            }
            [Browsable(false)]
            public enuBrushSupport BrushSupport
            {
                get { return enuBrushSupport.All; }
            }
            internal static ContourDefinition CreaeteFromString(ContourElement contourElement, string s)
            {
                if (string.IsNullOrEmpty(s))
                    return null;
                ContourDefinition def = new ContourDefinition(1.0f, contourElement);
                def.Pen.CopyDefinition(s);
                return def;
            }
        }
    }
}

