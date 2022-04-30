

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CirclesElementBase.cs
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
file:CirclesElementBase.cs
*/
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.ComponentModel;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.Drawing2D.MecanismActions;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public abstract class CirclesElementBase :
        Core2DDrawingDualBrushElement ,
        ICore2DCircleElement ,
        ICoreAlignmentCircle
    {
        protected CirclesElementBase()
        {
            
        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_FillMode = enuFillMode.Alternate;
            this.m_Radius = new float[1];
            this.m_Center = Vector2f.Zero;
        }
        protected Vector2f m_Center;
        protected float[] m_Radius;
        protected enuFillMode m_FillMode;
         [
        CoreXMLAttribute(),
        CoreXMLDefaultAttributeValue(enuFillMode.Alternate)
        ]
        /// <summary>
        /// get or set the fillmode
        /// </summary>
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLAttribute ()]
        /// <summary>
        /// get or set the radius
        /// </summary>
        public float[] Radius
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
        [CoreXMLAttribute()]
        public Vector2f Center
        {
            get { return m_Center; }
            set
            {
                if (m_Center != value)
                {
                    m_Center = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        public override void Align(enuCore2DAlignElement alignment, Rectanglef bounds)
        {
            if (this.Locked)
                return;
            //float dx = 0.0f;
            //float dy = 
            CoreMathOperation.AlignCircle(this, alignment, bounds, out float  dx, out float dy );

            //Matrix m = this.GetMatrix();
            //Rectanglef b = this.GetAlignmentBound();
            //Vector2f c = this.m_Center; //CoreMathOperation.GetCenter(b);
            //Vector2f bc = CoreMathOperation.GetCenter(bounds);
            //Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            //Rectanglef v_rc = bounds;
            //float dx = 0.0f;
            //float dy = 0.0f;
            //float rx = b.X;
            //float ry = b.Y;
            //switch (alignment)
            //{
            //    case enuCore2DAlignElement.TopLeft:
            //        dx = 0;
            //        dy = 0;
            //        break;
            //    case enuCore2DAlignElement.CenterHorizontal:
            //        ry = 0;
            //        dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
            //        this.Translate(dx, dy, enuMatrixOrder.Append, false);
            //        return;
            //    case enuCore2DAlignElement.CenterVertical:
            //        rx = 0;
            //        dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
            //        this.Translate(dx, dy, enuMatrixOrder.Append, false);
            //        return;
            //    case enuCore2DAlignElement.TopMiddle:
            //        dx = (v_rc.Width - b.Width) / 2.0f;
            //        break;
            //    case enuCore2DAlignElement.TopRight:
            //        dx = v_rc.Width - b.Width;
            //        break;
            //    case enuCore2DAlignElement.Center:
            //        dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
            //        dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
            //        this.Translate(dx, dy, enuMatrixOrder.Append, false);
            //        return;
            //    case enuCore2DAlignElement.MiddleLeft:
            //        dy = (v_rc.Height - b.Height) / 2.0f;
            //        break;
            //    case enuCore2DAlignElement.MiddleRight:
            //        dx = v_rc.Width - b.Width;
            //        dy = (v_rc.Height - b.Height) / 2.0f;
            //        break;
            //    case enuCore2DAlignElement.BottomLeft:
            //        dy = v_rc.Height - b.Height;
            //        break;
            //    case enuCore2DAlignElement.BottomRight:
            //        dy = v_rc.Height - b.Height;
            //        dx = v_rc.Width - b.Width;
            //        break;
            //    case enuCore2DAlignElement.BottomMiddle:
            //        dx = (v_rc.Width - b.Width) / 2.0f;
            //        dy = v_rc.Height - b.Height;
            //        break;
            //}
            //dx = dx - rx;
            //dy = dy - ry;
            this.Translate(dx, dy, enuMatrixOrder.Append, false);
        }
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity || m.IsDisposed )
                return;
             this.m_Center = CoreMathOperation.TransformVector2fPoint(m, new Vector2f[] { this.m_Center })[0];
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            ICoreParameterConfigCollections p = base.GetParameters(parameters);
            var g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            string v_str = conv.ConvertToString(this.Radius);
            g = p.AddGroup(CoreConstant.PARAM_DEFINITION);
            g.AddItem("Radius", "lb.Radius.caption", v_str, enuParameterType.Text, RadiusParameterChanged);
            g.AddItem(GetType().GetProperty("enuFillMode"));
            g.AddItem(GetType().GetProperty("Model"));
            return p;
        }
        private void RadiusParameterChanged(object sender, IGK.ICore.WinUI.Configuration.CoreParameterChangedEventArgs e)
        {
            if (e.Value == null) return;
            string v = e.Value.ToString();
            CoreFloatArrayTypeConverter conv = new CoreFloatArrayTypeConverter();
            try
            {
                float[] t = (float[])conv.ConvertFromString(v);
                if (t.Length > 0)
                {
                    this.Radius = t;
                }
            }
            catch
            {
            }
        }
        public class Mecanims<T> : Core2DDrawingCircleMecanismBase<T>
            where T : CirclesElementBase, ICore2DDrawingLayeredElement, ICore2DCircleElement 
        {
                private bool m_radiusAdded;
                protected override void GenerateActions()
                {
                    base.GenerateActions();
                    this.AddAction(enuKeys.F, new Core2DDToogleFillModeMecanismAction());                    
                }
                protected override void InitNewCreatedElement(T element, Vector2f defPoint)
                {
                    base.InitNewCreatedElement(element, defPoint);
                    element.FillBrush.Copy(this.CurrentSurface.FillBrush);
                    element.StrokeBrush.Copy(this.CurrentSurface.StrokeBrush);
                }
                protected override void OnElementChanged(CoreWorkingElementChangedEventArgs<T> e)
                {
                    base.OnElementChanged(e);
                }
                protected internal override void GenerateSnippets()
                {
                    base.GenerateSnippets();
                    this.AddSnippet(CreateSnippet(0, 0));
                    for (int i = 0; i < this.Element.Radius.Length; i++)
                    {
                        this.AddSnippet(this.CreateSnippet(1, 1 + i));
                    }
                }
                protected override void BeginDrawing(CoreMouseEventArgs e)
                {
                    base.BeginDrawing(e);
                    this.Element.SuspendLayout();
                    this.State = ST_CREATING;
                }
                protected override void UpdateDrawing(CoreMouseEventArgs e)
                {
                    if (this.Element == null)
                    {
                        this.State = ST_NONE;
                        return;
                    }
                    this.Element.Center = this.StartPoint;
                    this.EndPoint = e.FactorPoint;
                    this.Element.Radius[0] = CoreMathOperation.GetDistance(this.StartPoint, this.EndPoint);
                    this.Element.InitElement();
                    this.Invalidate();
                }
                protected override void EndDrawing(CoreMouseEventArgs e)
                {
                    this.UpdateDrawing(e);
                    this.Element.ResumeLayout();
                    this.State = ST_EDITING;
                    this.ShowSnippets();
                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
                protected override void BeginSnippetEdit(CoreMouseEventArgs e)
                {
                    if (this.Snippet.Demand == 0)
                    {
                        //move center
                    }
                    else
                    {
                        if (this.IsShiftKey)
                        {
                            //add new Point
                            List<float> v_pts = new List<float>();
                            v_pts.AddRange(this.Element.m_Radius);
                            v_pts.Insert(this.Snippet.Index - 1, CoreMathOperation.GetDistance(this.Element.m_Center, e.FactorPoint));
                            this.Element.m_Radius = v_pts.ToArray();
                            this.m_radiusAdded = true;
                        }
                    }
                    base.BeginSnippetEdit(e);
                }
                
                protected override void EndSnippetEdit(CoreMouseEventArgs e)
                {
                    if (!this.SnippetResolve(e))
                        return;

                    UpdateSnippetEdit(e); 
                    this.Element.ResumeLayout();
                    if (this.m_radiusAdded)
                    {
                        this.GenerateSnippets();
                    }
                    this.InitSnippetsLocation();
                    this.m_radiusAdded = false;
                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
                 private bool m_RemovePoint;
                 protected override void OnMouseClick(CoreMouseEventArgs e)
                 {
                     if (!this.m_RemovePoint)
                     {
                         base.OnMouseClick(e);
                     }
                     else {
                     m_RemovePoint= false;
                     }
                 }
                protected override void OnMouseDown(CoreMouseEventArgs e)
                {
                    if (e.Button == enuMouseButtons.Right)
                    {
                        if ((this.Element != null) && ((this.Snippet != null) && (this.Snippet.Demand == 1) && this.Element.m_Radius.Length > 1))
                        {
                            this.AllowContextMenu = false;
                            this.m_RemovePoint = true;
                            RemoveRadius();
                            return;
                        }
                    }
                    base.OnMouseDown(e);
                }
                protected override void OnMouseUp(CoreMouseEventArgs e)
                {
                    base.OnMouseUp(e);
                }
                private void RemoveRadius()
                {
                    List<float> v_pts = new List<float>();
                    v_pts.AddRange(this.Element.m_Radius);
                    v_pts.RemoveAt(this.Snippet.Index - 1);
                    this.Element.m_Radius = v_pts.ToArray();
                    this.Element.InitElement();
                    this.Invalidate();
                    this.GenerateSnippets();
                    this.InitSnippetsLocation();
                    this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }    
}

