

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFTransformableElement.cs
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
file:WPFTransformableElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    /// <summary>
    /// represent a transformable element
    /// </summary>
    public abstract class WPFTransformableElement :
        WPFLayeredElement,
        IWPFLayeredBoundElement,
        IWPFElement,
        IGK.DrSStudio.Drawing2D.ICoreTransformObject<double, Rectangled>
    {
        System.Windows.Media.Matrix m_matrix;
        System.Windows.Media.Matrix m_tempMatrix;
        public new System.Windows.UIElement Shape
        {
            get { return base.Shape as System.Windows.UIElement; }
            set { base.Shape = value; }
        }
        protected System.Windows.Media.Matrix GetMatrix()
        {
            return m_matrix;
        }
        protected System.Windows.Media.Matrix GetTempMatrix()
        {
            return this.m_tempMatrix;
        }
        public WPFTransformableElement()
        {
            this.m_matrix = new System.Windows.Media.Matrix();
        }
        #region IWPFElement Members
        IWPFElement IWPFElement.Parent
        {
            get
            {
                return base.Parent as IWPFElement;
            }
            set
            {
                base.Parent = value as WPFElementBase;
            }
        }
        #endregion
        public System.Windows.Media.Matrix GetGlobalMatrix()
        {
            System.Windows.Media.Matrix v_m = new System.Windows.Media.Matrix();
            v_m.Append (this.GetMatrix());
            WPFElementBase l = this.Parent as WPFElementBase;
            while (l != null)
            {
                if (l is WPFTransformableElement)
                {
                    v_m.Prepend ((l as WPFTransformableElement).GetMatrix());
                }
                l = l.Parent;
            }
            return v_m;
        }
        public override bool Contains(Vector2d e)
        {
            Rectangled rc = this.GetTransformBound();
            return rc.Contains(e);// base.Contains(e);
        }
        public override Rectangled GetTransformBound()
        {
            System.Windows.Rect v_rect= this.Shape.RenderTransform.TransformBounds(
            (this.Shape as System.Windows.Shapes.Shape ).RenderedGeometry.Bounds);
            if (v_rect.IsEmpty == false)
            {
                double offsetx = 0.0;
                double offsety = 0.0;
                offsetx = (double)this.Shape.GetValue(System.Windows.Controls.Canvas.LeftProperty);
                offsety = (double)this.Shape.GetValue(System.Windows.Controls.Canvas.TopProperty);
                offsetx = (offsetx.ToString() == double.NaN.ToString()) ? 0.0 : offsetx;
                offsety = (offsety.ToString () == double.NaN.ToString ()) ? 0.0 : offsety;
                return new Rectangled(
                    v_rect.X + offsetx ,
                    v_rect.Y + offsety ,
                    v_rect.Width,
                    v_rect.Height
                    );
            }
            Rectangled rc = this.GetBound();
            System.Windows.Media.Matrix v_m = this.GetGlobalMatrix();
            Rectangled pd = rc;
            Vector2d g = rc.GetCenter();
            Rectangled v_rc = pd.Transform(v_m);// t.TTransformBounds(rc.Transform.ToWPFRect());
            System.Windows.Rect v_rc2 = new System.Windows.Media.MatrixTransform(v_m).TransformBounds (rc.ToWPFRect());
            return v_rc;
        }
        protected internal override void Translate(double dx, double dy, IGK.DrSStudio.Drawing2D.enuTransformOrder order, bool temp)
        {
            if (this.CanTranslate == false) 
                return;
            System.Windows.Media.MatrixTransform transform = null; //new
            //    System.Windows.Media.MatrixTransform(this.m_matrix);
            //this.Shape.RenderTransform = transform;
            if (temp)
            {
                if (this.m_tempMatrix == null)
                    this.m_tempMatrix = new System.Windows.Media.Matrix();
                this.m_tempMatrix.SetIdentity();
                this.m_tempMatrix.Append(this.m_matrix);
                this.m_tempMatrix.Translate(dx, dy);
                transform = new System.Windows.Media.MatrixTransform(this.m_tempMatrix);
                this.Shape.RenderTransform = transform;
            }
            else
            {
                if (order == enuTransformOrder.Append)
                    this.m_matrix.Translate(dx, dy);
                else
                    this.m_matrix.TranslatePrepend(dx, dy);
                transform = new System.Windows.Media.MatrixTransform(this.m_matrix);
                this.Shape.RenderTransform = transform;
                OnPropertyChanged(WPFPropertyChanged.MatrixChanged);
            }
        }
        protected internal override void Rotate(double angle, Vector2d center, enuTransformOrder order, bool temp)
        {
            if (temp)
            { }
            else
            {
                OnPropertyChanged(WPFPropertyChanged.MatrixChanged);
            }
        }
        protected internal override void Scale(double ex, double ey, enuTransformOrder order, bool temp)
        {
            Vector2d rc = this.GetBound().GetCenter() ;
            m_tempMatrix.SetIdentity();
            m_tempMatrix.Append(this.m_matrix);
            double dpx = this.m_tempMatrix.OffsetX;
            double dpy = this.m_tempMatrix.OffsetY;
            m_tempMatrix.Translate(-dpx, -dpy);
            m_tempMatrix.Scale(ex, ey);
            m_tempMatrix.Translate(dpx, dpy);
            //m_tempMatrix.M11 *= ex;
            //m_tempMatrix.M22 *= ey;
            //m_tempMatrix.Translate(rc.X, rc.Y);
            this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(m_tempMatrix);
            if (temp)
            {
                //m_tempMatrix.SetIdentity();
                //m_tempMatrix.Append(this.m_matrix);
                //m_tempMatrix.Translate(-rc.X, -rc.Y);
                //m_tempMatrix.M11 *= ex;
                //m_tempMatrix.M22 *= ey;
                //m_tempMatrix.Translate(rc.X, rc.Y);
                //this.Shape.RenderTransform = new  System.Windows.Media .MatrixTransform (m_tempMatrix );
            }
            else
            {
                m_matrix = m_tempMatrix;
                m_tempMatrix.SetIdentity();
                //m_matrix.Translate(-rc.X, -rc.Y);
                //m_matrix.M11 *= ex;
                //m_matrix.M22 *= ey;
                //m_matrix.Translate(rc.X, rc.Y);
                //this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(m_matrix);
                OnPropertyChanged(WPFPropertyChanged.MatrixChanged);
            }
        }
        public void ResetTransform()
        {
            if (this.m_matrix.IsIdentity)
                return;
            this.BuildBeforeResetTransform();
            this.m_matrix.SetIdentity();           
            this.m_tempMatrix.SetIdentity();            
            this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(this.m_matrix);
            OnPropertyChanged(WPFPropertyChanged.MatrixChanged);
        }
        public void ClearTransform()
        {
            if (this.m_matrix.IsIdentity)
                return;
            this.m_matrix.SetIdentity();
            this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(this.m_matrix);
            OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(
                 enu2DPropertyChangedType.MatrixChanged));
        }
        protected virtual void BuildBeforeResetTransform()
        {
            //this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform();
        }
        public virtual void FlipX()
        {
            if ((this.Locked))
                return;
            Rectangled b = this.GetBound();
            Vector2d  endLoc = new Vector2d (b.X + b.Width, b.Y);
            this.Scale(-1, 1, endLoc, enuTransformOrder.Append, false);
        }
        protected internal virtual void Scale(double ex, double ey,
            Vector2d endLoc,
            enuTransformOrder enuTransformOrder,
            bool temp)
        {
            if ((this.Locked) || (!CanScale))
                return;
            Rectangled rf = Rectangled.Empty;
            if (temp)
            {
                rf = this.GetBound();
                this.m_tempMatrix.SetIdentity();
                this.m_tempMatrix.Append(this.m_matrix);
                this.m_tempMatrix.Translate(-rf.X, -rf.Y);
                this.m_tempMatrix.Scale(ex, ey);
                this.m_tempMatrix.Translate(endLoc.X, endLoc.Y);
                this.Shape.RenderTransform =
                    new System.Windows.Media.MatrixTransform(this.m_tempMatrix);
            }
            else
            {
                rf = this.GetBound();
                this.m_matrix.Translate(-rf.X, -rf.Y);
                this.m_matrix.Scale(ex, ey);
                this.m_matrix.Translate(endLoc.X, endLoc.Y);
                this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(this.m_matrix);
                this.OnPropertyChanged(new Core2DDrawingElementPropertyChangeEventArgs(enu2DPropertyChangedType.MatrixChanged));
            }
        }
        protected internal override void Scale(double  ex, double ey,
            Rectangled Bounds,
            Vector2d endLocation,
            enuTransformOrder  order,
            bool temp)
        {
            if (!CanScale || this.Locked)
                return;
            if ((ex.ToString() == float.NaN.ToString()) ||
                (ey.ToString() == float.NaN.ToString()))
                return;
            Rectangled rf = Bounds;
            if (temp)
            {
                this.m_tempMatrix.SetIdentity();
                this.m_tempMatrix.Append (this.m_matrix);
                this.m_tempMatrix.Translate(-rf.X, -rf.Y);
                this.m_tempMatrix.ScaleAsGdi (ex, ey);
                this.m_tempMatrix.Translate(endLocation.X, endLocation.Y);
                this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(this.m_tempMatrix );
            }
            else
            {
                this.m_matrix.Translate(-rf.X, -rf.Y);
                this.m_matrix.ScaleAsGdi (ex, ey);
                this.m_matrix.Translate(endLocation.X, endLocation.Y);
                this.Shape.RenderTransform = new System.Windows.Media.MatrixTransform(this.m_matrix);
                this.OnPropertyChanged(WPFPropertyChanged.MatrixChanged);
            }
        }
        public virtual void FlipY()
        {
            if (this.Locked)
                return;
            Rectangled b = this.GetBound();
            Vector2d endLoc = new Vector2d(b.X, b.Y + b.Height);
            Scale(1, -1, endLoc, enuTransformOrder.Append, false);
        }
        public void Align(IGK.DrSStudio.Drawing2D.enuCore2DAlignElement alignment, Rectangled Bound)
        {
            if (this.Locked)
                return;
            Rectangled b = this.GetBound();
            Vector2d c = b.GetCenter();
            Vector2d bc = Bound.GetCenter();
            Vector2d p = Vector2d.DistanceP (bc, c);
            Rectangled v_rc = Bound;
            double dx = 0.0f;
            double dy = 0.0f;
            double rx = b.X;
            double ry = b.Y;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    dx = 0;
                    dy = 0;
                    break;
                case enuCore2DAlignElement.CenterHorizontal:
                    ry = 0;
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    this.Translate(dx, dy, enuTransformOrder.Append, false);
                    return;
                case enuCore2DAlignElement.CenterVertical:
                    rx = 0;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    this.Translate(dx, dy, enuTransformOrder.Append, false);
                    return;
                case enuCore2DAlignElement.TopMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    break;
                case enuCore2DAlignElement.TopRight:
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.Center:
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    this.Translate(dx, dy, enuTransformOrder.Append, false);
                    return;
                case enuCore2DAlignElement.MiddleLeft:
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.MiddleRight:
                    dx = v_rc.Width - b.Width;
                    dy = (v_rc.Height - b.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    dy = v_rc.Height - b.Height;
                    break;
                case enuCore2DAlignElement.BottomRight:
                    dy = v_rc.Height - b.Height;
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    dy = v_rc.Height - b.Height;
                    break;
            }
            dx = dx - rx;
            dy = dy - ry;
            this.Translate(dx, dy, enuTransformOrder.Append, false);
        }
        public void Dock(IGK.DrSStudio.Drawing2D.enuCore2DDockElement dock,
            Rectangled docBound)
        {
            if (this.Locked)
                return;
            double v_w = 0;
            double v_h = 0;
            Rectangled v_rec = Rectangled.Empty;
            v_rec = docBound;
            v_w = (double)v_rec.Width;
            v_h = (double)v_rec.Height;
            Rectangled b = this.GetBound();
            double ex = 1.0f;
            double ey = 1.0f;
            Vector2d  endLoc = Vector2d .Zero ;
            switch (dock)
            {
                case enuCore2DDockElement.DockFill:
                    ex = v_w / b.Width;
                    ey = v_h / b.Height;
                    break;
                case enuCore2DDockElement.DockLeft:
                    ey = v_h / b.Height;
                    break;
                case enuCore2DDockElement.DockRight:
                    ey = v_h / b.Height;
                    endLoc = new Vector2d(v_w - b.Width, 0.0f);
                    break;
                case enuCore2DDockElement.DockUp:
                    ex = v_w / b.Width;
                    break;
                case enuCore2DDockElement.DockDown:
                    ex = v_w / b.Width;
                    endLoc = new Vector2d(0, v_h - b.Height);
                    break;
                case enuCore2DDockElement.DockLeftAndRight:
                    ex = v_w / b.Width;
                    ey = 1;
                    endLoc = new Vector2d(0, b.Y);
                    break;
                case enuCore2DDockElement.DockUpAndDown:
                    ey = v_h / b.Height;
                    ex = 1;
                    endLoc = new Vector2d(b.X, 0);
                    break;
            }
            Scale(ex, ey, endLoc, enuTransformOrder.Append, false);
        }
    }
}

