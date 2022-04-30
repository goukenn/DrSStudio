

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CirclePathTextElement.cs
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
file:CirclePathTextElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D ;
namespace IGK.DrSStudio.Drawing2D
{
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.WinUI;
    using IGK.DrSStudio.Drawing2D.Text;
    /// <summary>
    /// use to transform text element to circle path element
    /// </summary>
    [TextElementCategory("CirclePathText", typeof(Mecanism), IsVisible = false, ImageKey = "DE_TextCircle")]
    class CirclePathTextElement : Core2DDrawingObjectBase
    {
        internal  class Mecanism :Core2DDrawingMecanismBase
        {
            TextElement  m_text;
            bool m_drawdesign;
            public new Core2DDrawingLayeredElement Element
            {
                get
                {
                    return base.Element as Core2DDrawingLayeredElement;
                }
                set
                {
                    base.Element = value;
                }
            }
            //.constructor
            internal Mecanism(TextElement  text, Core2DDrawingMecanismBase ParentMecanism)
            {
                this.m_text = text;
                m_drawdesign = false;
                this.ParentMecanism = ParentMecanism;
                this.ParentMecanism.Freeze();
                this.Register(this.ParentMecanism.CurrentSurface);
            }
            protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
            {
                if (!m_drawdesign)
                {
                    return;
                }
                Rectanglef rc = CoreMathOperation.GetBounds(this.EndPoint, this.StartPoint);
                rc = CurrentSurface.GetScreenBound(rc);
                e.Graphics.DrawEllipse(Pens.Black, rc);
                Pen p = new Pen(Color.White);
                p.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;
                e.Graphics.DrawEllipse(p, rc);
                p.Dispose();
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        this.StartPoint = e.FactorPoint;
                        this.EndPoint = e.FactorPoint;
                        //get element path
                        if (this.Element == null)
                        {
                            for (int i = this.CurrentSurface.CurrentLayer.Elements.Count - 1; i >= 0; --i)
                            {
                                if (this.CurrentSurface.CurrentLayer.Elements[i].Contains(e.FactorPoint) &&
                                    !(this.CurrentSurface.CurrentLayer.Elements[i] is TextElement)
                                    )
                                {
                                    this.Element = this.CurrentSurface.CurrentLayer.Elements[i] as TextElement;
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Left:
                        if (this.Element == null)
                        {
                            m_drawdesign = false;
                            this.EndPoint = e.FactorPoint;
                            m_drawdesign = true;
                            this.CurrentSurface.Invalidate();
                        }
                        else
                            m_drawdesign = false;
                        break;
                }
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case System.Windows.Forms.MouseButtons.Right:
                        //un select element
                        if (this.Element != null)
                        {
                            this.EndEdition();
                        }
                        break;
                    case System.Windows.Forms.MouseButtons.Left:
                        string[] line = m_text.Content.Split('\n');
                        List<PathElement> elements = new List<PathElement>();// null;// CoreDrawingElement_textblock.CreateElement(m_text);
                        PathElement v_element = null;
                        GraphicsPath v_path = new GraphicsPath();
                        Vector2f v_center = Point.Empty;
                        //if (this.Element == null)
                        //{
                            Rectanglef v_rc = CoreMathOperation.GetBounds(this.EndPoint, this.StartPoint);
                            v_center = CoreMathOperation.GetCenter(v_rc);
                            v_path.AddEllipse(v_rc);
                            v_path.Flatten();
                            v_path.Reverse();
                        //}
                        //else
                        //{
                        //    GraphicsPath v_p = this.Element.GetPath();
                        //    v_path.AddPath(v_p, false);
                        //    v_center = CoreMathOperation.GetCenter(v_path.GetBounds());
                        //    v_path.Flatten();
                        //}
                        bool v_rotate = (this.Element == null);
                        if ((v_path.PointCount > 0))
                        {
                            this.CurrentSurface.CurrentLayer.Elements.Remove(
                                this.CurrentLayer.SelectedElements.ToArray()
                                );
                            this.CurrentSurface.Invalidate();
                            StringFormat v_sf = new StringFormat();
                            int c = 0;
                            int k = 0;//character
                            for (int i = 0; i < line.Length; i++)
                            {
                                elements.Clear();
                                c = (int)(v_path.PointCount / (float)line[i].Length);
                                k = 0;
                                foreach (char ch in line[i])
                                {
                                    //
                                    if (char.IsWhiteSpace(ch))
                                    {
                                        k++;
                                        continue;
                                    }
                                    GraphicsPath v_cpath = new GraphicsPath();
                                    v_cpath .AddString (
                                        ch.ToString(),
                                        m_text.Font.GetFont ().FontFamily,
                                        (int) m_text.Font.FontStyle ,
                                        m_text.Font.FontSize ,Point.Empty ,
                                        v_sf);
                                    v_rc = v_cpath.GetBounds();
                                    Matrix matrix = new Matrix();
                                    matrix.Translate (
                                        v_rc.X - v_rc.Width / 2.0f,
                                         v_rc.Y - v_rc.Height / 2.0f,
                                          enuMatrixOrder.Append );
                                    //(ch,
                                    //    m_text.FontName, m_text.FontSize,
                                    //    m_text.FontStyle, v_path.PathPoints[c * k]);
                                    //v_element.Location =
                                    //    new Vector2f(v_element.Location.X - v_element.Bounds.Width / 2.0f,
                                    //        v_element.Location.Y - v_element.Bounds.Height / 2.0f);
                                    if (v_rotate)
                                    {
                                        matrix.Rotate (-90 + (CoreMathOperation.GetAngle(v_center,
                                        v_path.PathPoints[c * k]) * CoreMathOperation.ConvRdToDEGREE));
                                        //v_cpathv_element.Rotate(-90 + (CoreMathOperation.GetAngle(v_center,
                                        //v_path.PathPoints[c * k]) * CoreMathOperation.ConvRdToDEGREE));
                                    }
                                    matrix.Translate(v_center.X, v_center.Y, enuMatrixOrder.Append);
                                    v_cpath.Transform(matrix);
                                    v_element = PathElement.Create(v_cpath);
                                    k++;
                                    elements.Add(v_element);
                                }
                                this.CurrentSurface.CurrentLayer.Elements.AddRange(elements.ToArray());
                            }
                        }
                        this.m_drawdesign = false;
                        this.CurrentSurface.Invalidate();
                        this.UnRegister();
                        this.ParentMecanism .UnFreeze( );
                        this.Dispose();
                        this.ParentMecanism = null;
                        //this.EndEdition();
                        v_path.Dispose();
                        break;
                }
            }
        }
    }
}

