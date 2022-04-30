

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFLayeredElement.cs
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
file:WPFLayeredElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    /// <summary>
    /// represent a wpf layered element
    /// </summary>
    public abstract class WPFLayeredElement : WPFElementBase ,
        IWPFLayeredElement,
        IWPFLayeredBoundElement 
    {
        System.Windows.DependencyObject m_Shape;
        private bool m_Visible;
        private bool m_Locked;
        public bool Locked
        {
            get { return m_Locked; }
            set
            {
                if (m_Locked != value)
                {
                    m_Locked = value;
                }
            }
        }
        public bool Visible
        {
            get { return m_Visible; }
            set
            {
                if (m_Visible != value)
                {
                    m_Visible = value;
                }
            }
        }
        /// <summary>
        /// get the parent 
        /// </summary>
        public WPFLayer ParentLayer
        {
            get {
                if (base.Parent is WPFLayer)
                    return base.Parent as WPFLayer;
                else if (base.Parent is IWPFLayeredElement)
                    return (base.Parent as IWPFLayeredElement).ParentLayer;
                return null;
            }
        }
        public System.Windows.DependencyObject Shape { get { return this.m_Shape; } set { this.m_Shape = value; } }
        IWPFElement IWPFElement.Parent
        {
            get
            {
                return base.Parent as IWPFElement ;
            }
            set
            {
                base.Parent = value as WPFElementBase ;
            }
        }
        protected abstract void InitPath();
        protected override void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (!this.IsLoading && (e.ID == enuPropertyChanged.Definition))
                this.InitPath();
            base.OnPropertyChanged(e);
        }
        protected override void OnLoadingComplete(EventArgs eventArgs)
        {
            this.InitPath();
            base.OnLoadingComplete(eventArgs);
        }
        #region ICoreWorkingPositionableObject Members
        public virtual int ZIndex
        {
            get
            {
                return this.Container.IndexOf(this);
            }
        }
        public ICoreWorkingPositionableObjectContainer Container
        {
            get
            {
                return this.Parent as ICoreWorkingPositionableObjectContainer;
            }
        }
        public void MoveBack()
        {
            this.Container.MoveToBack(this);
        }
        public void MoveFront()
        {
            this.Container.MoveToFront(this);
        }
        public void MoveEnd()
        {
            this.Container.MoveToEnd(this);
        }
        public void MoveStart()
        {
            this.Container.MoveToStart(this);
        }
        public void MoveAt(int index)
        {
            this.Container.MoveAt(this, index);
        }
        #endregion
        #region IWPFLayeredBoundElement Members
        public virtual Rectangled GetBound()
        {
            System.Windows.Shapes.Shape p= this.Shape as System.Windows.Shapes.Shape;
                    Rectangled v_rc = Rectangled.Empty;
                    if (p != null)
                    {
                        System.Windows.Rect vb = p.RenderedGeometry.Bounds;
                        if (vb.IsEmpty)
                            return Rectangled.Empty;
                        v_rc = new Rectangled(vb.X,
                            vb.Y,
                            vb.Width, 
                            vb.Height);
                        v_rc.Inflate(2, 2);
                    }
                    else
                    {
                        v_rc = new Rectangled (
                        (double)    this.Shape.GetValue (System.Windows.Controls.Canvas.LeftProperty ),
                        (double)this.Shape.GetValue (System.Windows.Controls.Canvas.TopProperty ),
                            (double)this.Shape.GetValue (System.Windows.Controls.Canvas.WidthProperty ),
                        (double)this.Shape.GetValue (System.Windows.Controls.Canvas.HeightProperty )
                            );
                    }
            return v_rc ;
        }
        public virtual Rectangled GetTransformBound()
        {            
                return this.GetBound();            
        }
        #endregion
        public virtual bool Contains(Vector2d e)
        {
            return this.GetBound().Contains(e);
        }
        internal bool Intersect(Rectangled rc)
        {
            throw new NotImplementedException();
        }
        #region ICoreTransformObject<double,Rectangled> Members
        public virtual bool CanResize
        {
            get { return true; }
        }
        public virtual bool CanRotate
        {
            get { return true; }
        }
        public bool CanScale
        {
            get { return true; }
        }
        public bool CanTranslate
        {
            get { return true; }
        }
        public void Translate(double dx, double dy, IGK.DrSStudio.Drawing2D.enuTransformOrder order)
        {
            if (this.CanTranslate )
            this.Translate(dx, dy, order, false);
        }     
        public void Scale(double ex, double ey, IGK.DrSStudio.Drawing2D.enuTransformOrder order)
        {
            if (this.CanScale == false) return;
            this.Scale(ex, ey, order, false);
        }
        public void Rotate(double angle, IGK.DrSStudio.Drawing2D.enuTransformOrder order)
        {
            if (this.CanRotate == false) 
                return;
            this.Rotate(angle, this.GetBound().GetCenter(), order, false);
        }
        #endregion
        protected internal virtual void Translate(double dx, double dy, IGK.DrSStudio.Drawing2D.enuTransformOrder order, bool temp)
        {
        }
        protected internal virtual void Scale(double ex, double ey, IGK.DrSStudio.Drawing2D.enuTransformOrder order, bool temp)
        {
        }
        protected internal virtual void Scale(double  ex, double ey,
            Rectangled Bounds,
            Vector2d  endLocation,
            IGK.DrSStudio.Drawing2D.enuTransformOrder order,
            bool temp)
        {          
        }
        protected internal virtual void Rotate(double angle, Vector2d center, IGK.DrSStudio.Drawing2D.enuTransformOrder order, bool temp)
        {
        }
    }
}

