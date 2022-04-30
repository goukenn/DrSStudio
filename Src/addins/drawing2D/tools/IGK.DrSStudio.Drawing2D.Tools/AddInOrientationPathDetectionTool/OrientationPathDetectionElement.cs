using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.GraphicModels;
using IGK.ICore;
using System.Collections;
using IGK.ICore.WinUI;

namespace IGK.DrSStudio.Drawing2D.AddInOrientationPathDetectionTool
{
    [Core2DDrawingSelection("OrientationTool", typeof(Mecanism))]
    class OrientationPathDetectionElement : RectangleElement
    {
        struct OrientationDef {
            public Vector2f Center;
            public float Angle;
            public ICoreGraphicsPath Path;

            public static OrientationDef Empty;
            static OrientationDef() {
                Empty = new OrientationDef();
            }
        }
        new class Mecanism : RectangleElement.Mecanism
        {
            ICorePathElement m_edition;
            Vector2f[] curve = new Vector2f[] {
                new Vector2f(0,4),
                new Vector2f(4,0),
                new Vector2f(0,-4)
                };
            private CoreGraphicsPath gp;
            private List<OrientationDef> m_oDef;

            ///<summary>
            ///public .ctr
            ///</summary>
            public Mecanism()
            {
                m_oDef = new List<OrientationDef>();
                gp = new CoreGraphicsPath();
                gp.AddPolygon(curve);
            }
            protected override RectangleElement CreateNewElement()
            {
                return null;
            }
            protected override void InitNewCreatedElement(RectangleElement element, Vector2f defPoint)
            {                
            }
            protected override void GenerateSnippets()
            {                
            }
            protected override void InitSnippetsLocation()
            {                
            }

            protected override void OnLayerSelectedElementChanged(EventArgs eventArgs)
            {
                if (this.CurrentLayer.SelectedElements.Count > 0)
                {
                    m_edition = this.CurrentLayer.SelectedElements[0] as ICorePathElement;
                    BuildOrientation();
                }
                else {
                    m_edition = null;
                }
                this.Invalidate();
            }
            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                if ((e.Button == enuMouseButtons.Left) && (this.m_edition != null)) {

                    Rectanglef v_rc;

                    for (int i = 0; i < m_oDef.Count; i++)
                    {
                        if (m_oDef[i].Equals(OrientationDef.Empty))continue;
                        v_rc = new Rectanglef(CurrentSurface.GetScreenLocation(m_oDef[i].Center), Size2f.Empty);
                        v_rc.Inflate(4, 4);
                        if (v_rc.Contains(e.Location)) {
                            //change orientation

                            m_oDef[i].Path.Invert();

                            m_edition.SetDefinition(GetEditPath());


                            BuildOrientation();
                            this.Invalidate();
                            return;
                            //continue;
                        }
                    }
                }
                base.OnMouseClick(e);
            }

            private CoreGraphicsPath GetEditPath()
            {
                CoreGraphicsPath g = new CoreGraphicsPath();
                Vector2f[] pts = null;
                byte[] types = null;
                for (int i = 0; i < m_oDef.Count; i++)
                {
                    m_oDef[i].Path.GetAllDefinition(out pts, out types);
                    g.AddDefinition(pts, types);
                }
                return g;
            }
            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                switch(e.Button){
                    case enuMouseButtons.Left:
                        if (this.m_edition != null) {
                            if (this.m_edition.Contains(e.FactorPoint))
                                return;

                        }
                        this.CurrentLayer.Select(e.FactorPoint, true);
                        return;
                }
                base.OnMouseDown(e);
            }

            void BuildOrientation()
            {
                m_oDef.Clear();
                if (m_edition == null)
                    return;

                var p = m_edition.GetPath();
                var t = p.Explode();
                foreach (ICoreGraphicsPath item in t)
                {
                    var cp = item.PathPoints;
                    if (item.IsEmpty || cp.Length <= 1)
                    {
                        m_oDef.Add(OrientationDef.Empty);
                        continue;
                    }

                    var gpt = CoreMathOperation.GetMiddlePoint(cp[0], cp[1]);
                    var agpt = CoreMathOperation.GetAngle(cp[0], cp[1]) * CoreMathOperation.ConvRdToDEGREE;
                    m_oDef.Add(new OrientationDef()
                    {
                        Center = gpt,
                        Angle = agpt,
                        Path = item
                    });
                }
            }

            public override void Render(ICoreGraphics device)
            {
                if (m_edition == null)
                    return;


                var p = m_edition.GetPath();
                var t = p.Explode();
                //var i_state = device.Save();
                //this.ApplyCurrentSurfaceTransform(device);
                foreach (ICoreGraphicsPath item in t)
                {
                    var cp = item.PathPoints;
                    if (item.IsEmpty || cp.Length <= 1)
                        continue;

                    var gpt = CurrentSurface.GetScreenLocation(CoreMathOperation.GetMiddlePoint(cp[0], cp[1]));
                    var agpt = CoreMathOperation.GetAngle(cp[0], cp[1]) * CoreMathOperation.ConvRdToDEGREE;
                    var ii_state = device.Save();
                    device.TranslateTransform(gpt.X, gpt.Y, enuMatrixOrder.Prepend);
                    device.RotateTransform(agpt, enuMatrixOrder.Prepend);

                    device.FillPath(Colorf.White, gp);
                    device.DrawPath(Colorf.Blue, gp);
                    device.Restore(ii_state);
                }
                //device.Restore(i_state);
                base.Render(device);


                //var p = m_edition.GetPath();
                //var t = p.Explode();
                //var i_state = device.Save();
                //this.ApplyCurrentSurfaceTransform(device);
                //foreach (ICoreGraphicsPath item in t)
                //{
                //    var cp = item.PathPoints;
                //    if (item.IsEmpty || cp.Length <= 1)
                //        continue;

                //    var gpt = CoreMathOperation.GetMiddlePoint(cp[0], cp[1]);
                //    var agpt = CoreMathOperation.GetAngle(cp[0], cp[1]) * CoreMathOperation.ConvRdToDEGREE;
                //    var ii_state = device.Save();
                //    device.TranslateTransform(gpt.X, gpt.Y, enuMatrixOrder.Prepend);
                //    device.RotateTransform(agpt, enuMatrixOrder.Prepend);

                //    device.FillPath(Colorf.White, gp);
                //    device.DrawPath(Colorf.Blue, gp);
                //    device.Restore(ii_state);    
                //}
                //device.Restore(i_state);
                //base.Render(device);
            }
            public override void Dispose()
            {
                gp.Dispose();
                base.Dispose();
            }

          
        }
    }
}
