

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: RectangleElement.cs
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
file:RectangleElement.cs
*/
using IGK.ICore;
using IGK.ICore.Drawing2D.Mecanism;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.Drawing2D.WorkingObjects.Standard;
using IGK.ICore.GraphicModels;

namespace IGK.ICore.Drawing2D
{
    [Core2DDrawingStandardElement("Rectangle",
        typeof(Mecanism),
        Keys=enuKeys.R)]
    public class RectangleElement : Core2DDrawingDualBrushElement, ICore2DRectangleElement 
    {
        private Rectanglef m_Bounds;


        public RectangleElement():base()
        {
        
        }
        protected override void InitializeElement() {
            base.InitializeElement();
            this.m_Bounds = new Rectanglef(0, 0, 1, 1);
        }
        protected override void BuildBeforeResetTransform()
        {
            Matrix m = this.GetMatrix();
            if (m.IsIdentity)
                return;
            Rectanglef rc = CoreMathOperation.GetBounds(CoreMathOperation.ApplyMatrix(this.Bounds, m));
            rc.X = Math.Abs(rc.X) < 0.0001f ? 0 : rc.X;
            rc.Y = Math.Abs(rc.Y) < 0.0001f ? 0 : rc.Y;
            this.Bounds = rc;

            
            //this .m_Bounds = CoreMathOperation.GetBounds(
            //    CoreMathOperation.MultMatrixTransformPoint(this.Matrix ,CoreMathOperation.GetPoints(this.Bounds)));
            //base.BuildBeforeResetTransform();
        }
       

        /// <summary>
        /// get the bound of this rectangle
        /// </summary>
        public Rectanglef  Bounds
        {
            get { return m_Bounds; }
            set
            {
                if (m_Bounds != value)
                {
                    m_Bounds = value;
                    OnPropertyChanged(Core2DDrawingChangement.Definition);
                }
            }
        }
        protected override void InitGraphicPath(CoreGraphicsPath p)
        {            
            p.Reset();
            p.AddRectangle(this.Bounds);
        }
        public new class Mecanism : Core2DDrawingRectangleMecanismBase<RectangleElement>
        {
            protected override void InitNewCreatedElement(RectangleElement element, Vector2f defPoint)
            {
                base.InitNewCreatedElement(element, defPoint);
                this.StartPoint = defPoint;
                this.EndPoint = defPoint;
                element.Bounds = CoreMathOperation.GetBounds(defPoint, defPoint);
            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();                
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 0, 0));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 1, 1));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 2, 2));
                AddSnippet(this.CurrentSurface.CreateSnippet(this, 3, 3));
            }
            protected internal override void InitSnippetsLocation()
            {
                if ((this.Element == null) || (this.RegSnippets.Count < 4))
                    return;
                Vector2f[] t = CoreMathOperation.GetResizePoints(this.Element.Bounds);                
                this.RegSnippets[0].Location = CurrentSurface.GetScreenLocation(t[1]);
                this.RegSnippets[1].Location = CurrentSurface.GetScreenLocation(t[3]);
                this.RegSnippets[2].Location = CurrentSurface.GetScreenLocation(t[5]);
                this.RegSnippets[3].Location = CurrentSurface.GetScreenLocation(t[7]);
            }

         
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                this.DisableSnippet();
                this.StartPoint = e.FactorPoint;
                this.EndPoint = e.FactorPoint;
                this.Element.SuspendLayout();
                this.Element.Bounds = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);                                
                this.State = ST_CREATING;
                this.Invalidate();
            }
            protected override void UpdateDrawing(ICore.WinUI.CoreMouseEventArgs e)
            {
                if (this.Element == null){
                    this.State = ST_NONE; return;
                }
                this.EndPoint = e.FactorPoint;
                if (this.IsControlKey)
                {
                    Vector2f g = this.StartPoint;
                    float r = this.EndPoint.Distance(g) / 2;
                    var x = g.X ;
                    var y = g.Y;

                    this.Element.Bounds = new Rectanglef(x, y, (2*r),( 2* r) ); // this.StartPoint
                }
                else 
                this.Element.Bounds = CoreMathOperation.GetBounds(this.StartPoint, this.EndPoint);
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                this.UpdateDrawing(e);
                this.Element.ResumeLayout();
                this.GenerateSnippets();
                this.InitSnippetsLocation();
                this.Invalidate();
                this.State = ST_EDITING;
            }
            protected override void BeginSnippetEdit(CoreMouseEventArgs e)
            {
                this.Element.SuspendLayout();
            }
            protected override void UpdateSnippetEdit(CoreMouseEventArgs e)
            {
                Rectanglef v_rc = this.Element.Bounds;
                this.Snippet.Location = e.Location;
                switch (this.Snippet.Index)
                {
                    case 3:
                        if (e.FactorPoint.X < v_rc.Right)
                        {
                            v_rc.Width = Math.Abs(v_rc.Right - e.FactorPoint.X);
                            v_rc.X = e.FactorPoint.X;
                        }
                        break;
                    case 0:
                        if (e.FactorPoint.Y < v_rc.Bottom)
                        {
                            v_rc.Height = Math.Abs(v_rc.Bottom - e.FactorPoint.Y);
                            v_rc.Y = e.FactorPoint.Y;
                        }
                        break;
                    case 2:
                        if (e.FactorPoint.Y > v_rc.Top)
                        {
                            v_rc.Height = Math.Abs(e.FactorPoint.Y - v_rc.Top);
                        }
                        break;
                    case 1:
                        if (e.FactorPoint.X > v_rc.Left)
                        {
                            v_rc.Width = Math.Abs(e.FactorPoint.X - v_rc.Left);
                            //v_rc.X = e.FactorPoint.X;
                        }
                        break;
                }
                this.Element.Bounds = v_rc;
                this.Element.InitElement();
                this.Invalidate();
            }
            protected override void EndSnippetEdit(CoreMouseEventArgs e)
            {
                this.UpdateSnippetEdit(e);
                this.Element.ResumeLayout();
                this.InitSnippetsLocation();
                this.Element.OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
            }

            public override void Render(ICoreGraphics device)
            {
                base.Render(device);
            }

        }
    }
}

