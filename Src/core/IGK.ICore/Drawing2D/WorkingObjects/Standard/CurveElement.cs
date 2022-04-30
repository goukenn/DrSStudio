using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.GraphicModels;
using System.Collections;

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{

    [Core2DDrawingStandardElement("Curve",
 typeof(Mecanism),
 Keys = enuKeys.Shift| enuKeys.A)]
    class CurveElement : PathElement
    {
        new class Mecanism : IGK.ICore.Drawing2D.Mecanism.Core2DDrawingSurfaceMecanismBase<PathElement>,
            IEnumerable<IVector2f>{
            private List<Vector2f> m_points;
            private int factorDistance = 3;
            ///<summary>
            ///public .ctr
            ///</summary>
            public Mecanism()
            {
                this.m_points = new List<Vector2f>();
            }

            protected override PathElement CreateNewElement()
            {
                return new PathElement()
                {
                    Closed = false
                };
            }
            protected override void InitNewCreatedElement(PathElement element, Vector2f location)
            {
                base.InitNewCreatedElement(element, location);
            }
            protected override void BeginDrawing(CoreMouseEventArgs e)
            {
                base.BeginDrawing(e);
            }
            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                if ((this.m_points.Count == 0)||(((Vector2f)this.m_points[this.m_points.Count -1]).Distance(e.FactorPoint) > factorDistance))
                {
                    this.m_points.Add(e.FactorPoint);
                }                
                this.Invalidate();
            }
            protected override void EndDrawing(CoreMouseEventArgs e)
            {
                base.EndDrawing(e);

                var cc = this.Element;
                CoreGraphicsPath pt = new CoreGraphicsPath();
                var c = this.m_points.ToArray();

                if (c!=null)
                pt.AddCurve(c);
                cc.SetDefinition(pt);
                cc.Closed = false;
                pt.Dispose();
                m_points.Clear();
                this.Invalidate();

            }
            public override void Render(ICoreGraphics device)
            {
                if (this.Element == null)
                    return;
                var o = device.Save();
                this.ApplyCurrentSurfaceTransform(device);
                device.DrawCurve(Colorf.Aqua, this);
                device.Restore(o);
                
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                base.OnMouseDown(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                base.OnMouseUp(e);
            }
            protected override void GenerateActions()
            {
                base.GenerateActions();
            }
            protected internal override void GenerateSnippets()
            {
                this.DisableSnippet();
            }
            protected internal override void InitSnippetsLocation()
            {
                base.InitSnippetsLocation();
            }

            public IEnumerator<IVector2f> GetEnumerator()
            {
                List<IVector2f> c = new List<IVector2f>();
                for (int i = 0; i < m_points.Count; i++)
                {
                    c.Add(m_points[i]);
                }
                return c.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.m_points.GetEnumerator();
            }
        }
    }
}
