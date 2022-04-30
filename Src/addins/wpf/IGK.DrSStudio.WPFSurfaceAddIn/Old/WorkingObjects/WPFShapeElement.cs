

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFShapeElement.cs
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
file:WPFShapeElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes ;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WPFSurfaceAddIn.WinUI;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    using IGK.DrSStudio.Drawing2D;
    using System.Drawing;
    /// <summary>
    /// represent shape element
    /// </summary>
    public abstract class WPFShapeElement :
        WPFTransformableElement ,        
        ICoreBrushOwner 
    {
        public ICoreBrush[] GetBrushes()
        {
            return null;
        }
        public new Shape Shape
        {
            get { return base.Shape as Shape ; }
            protected set
            {
                if (base.Shape != value)
                {
                    base.Shape = value;
                }
            }
        }
        public override  bool Contains(Vector2d point)
        {
            return this.Shape.IsMouseDirectlyOver;
            //System.Windows.Media.Tranform t = new System.Windows.Media.MatrixTransform(this.GetMatrix());
            //return t.TransformBounds (this.Shape.RenderedGeometry.Bounds).Contains(point.ToWPFPoint());            
        }
        public override void Dispose()
        {
            base.Dispose();
        }
        public WPFShapeElement()
        {
            this.Shape = CreateShape();
            if (this.Shape == null)
                throw new CoreException(enuExceptionType.OperationNotValid, "CreateShape");
            this.Shape.Stroke = System.Windows.Media.Brushes.Black;
            this.Shape.Fill = System.Windows.Media.Brushes.White;
        }
        protected abstract Shape CreateShape();
        public class Mecanism : WPFBaseMecanism
        {
            public new WPFShapeElement Element
            {
                get { return base.Element as WPFShapeElement ; }
                set
                {
                    base.Element = value;
                }
            }
            protected override void OnMouseDown(WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton )
                {
                    case System.Windows.Input.MouseButton.Left :
                        switch (this.State)
                        {
                            case ST_NONE:
                            case ST_CREATING:
                                this.Element = this.CreateElement() as WPFShapeElement;
                                if (this.Element != null)
                                {
                                    this.CurrentLayer.Elements.Add(this.Element);
                                    this.CurrentLayer.Select(this.Element);
                                    this.State = ST_CREATING;
                                    this.StartPoint = e.Location;
                                    this.EndPoint = e.Location;
                                    this.BeginCreateElement(e);
                                }
                                break;
                            case ST_EDITING :
                                if (this.Snippet == null)
                                {
                                    goto case ST_NONE;
                                }
                                break;
                        }
                        break;
                }
            }
            protected override  void BeginCreateElement(WPFMouseButtonEventArgs e)
            {
            }
            protected override void OnMouseUp(WPFMouseButtonEventArgs e)
            {
                switch (e.ChangedButton)
                {
                    case System.Windows.Input.MouseButton.Left:
                                switch (this.State)
                                {
                                    case ST_CREATING:
                                        this.State = ST_EDITING;
                                        this.GenerateSnippets();
                                        this.InitSnippetsLocation ();
                                        this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                                        break;
                                }
                        break;
                    case System.Windows.Input.MouseButton.Right :
                             if (this.Element == null)
                                   this.GotoDefaultTool();
                                else
                                {
                                    this.EndEdition();
                                }
                                break;
                }
            }
            protected override void OnMouseMove(WPFMouseEventArgs e)
            {
                switch (e.LeftButton)
                {
                    case System.Windows.Input.MouseButtonState.Pressed :
                        switch (this.State)
                        {
                            case ST_CREATING :                            
                                UpdateCreateElement(e);
                                break;
                            case ST_EDITING:
                                if (this.Snippet == null)
                                    UpdateCreateElement(e);
                                else
                                    UpdateSnippetElement(e);
                                break;
                        }
                        break;
                }
            }
            protected override void UpdateCreateElement(WPFMouseEventArgs e)
        {
            this.EndPoint = e.Location;
            Rectangled f = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
            System.Windows.Shapes.Shape sp = this.Element.Shape;
            sp.SetValue(System.Windows.Controls.Canvas.LeftProperty, f.X);
            sp.SetValue(System.Windows.Controls.Canvas.TopProperty, f.Y);
            sp.Width = f.Width;
            sp.Height = f.Height;
        }
        }
        #region ICore2DDrawingBrushSupportElement Members
        public virtual ICoreBrush GetBrush(enuBrushMode mode)
        {
            return null;
        }
        public virtual enuBrushSupport BrushSupport
        {
            get { return enuBrushSupport.All; }
        }
        #endregion
        #region ICoreBrushOwner Members
        
        public virtual Graphics GetPath()
        {
            Graphics p = new Graphics();
            p.AddRectangle(this.GetBound());
            return p;
        }
        #endregion
        #region ICoreBrushOwner Members
        Matrix ICoreBrushOwner.GetMatrix()
        {
            return this.GetMatrix().ToGdiMatrix();
        }
        #endregion
        public Rectanglef GetDefaultBound()
        {
            Rectangled d = this.GetBound ();
            return new Rectanglef((float)d.X, (float)d.Y,(float) d.Width, (float)d.Height);
        }
    }
}

