using IGK.ICore;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing2D
{
    [IGKD2DDrawingAdvancedElement("SArc", typeof(Mecanism), ImageKey = "DE_Cone")] 
    class SArcElement : ArcElement 
    {
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();

            Rectanglef v_rc = Rectanglef.Empty;
            if (this.Radius != null)
            {
                if (this.Radius.Length == 1)
                {
                    var r = this.Radius[0];
                    v_rc = new Rectanglef(
                            Center.X - r.X,
                            Center.Y - r.Y,
                            2 * r.X,
                            2 * r.Y);
                    path.AddArc(v_rc,
                        this.StartAngle,
                        this.SweepAngle, this.Closed);
                }
                else
                {
                    var rd = this.Radius;
                    var segment = new IGK.ICore.Drawing2D.Segments.PathSegment();
                    int i = 0;
                    for (i = 0; i < this.Radius.Length - 1; i+=2)
                    {
                        v_rc = new Rectanglef(
                               Center.X - this.Radius[i].X,
                               Center.Y - this.Radius[i].Y,
                               2 * this.Radius[i].X,
                               2 * this.Radius[i].Y);
                        if (v_rc.IsEmpty)
                            continue;
                        segment.AddArc(v_rc,
                            this.StartAngle,
                            this.SweepAngle);

                        v_rc = new Rectanglef(
                        Center.X - this.Radius[i+1].X,
                        Center.Y - this.Radius[i+1].Y,
                        2 * this.Radius[i+1].X,
                        2 * this.Radius[i+1].Y);

                        segment.AddLine(Center + new Vector2f(rd[i].X * Math.Cos(
                            (this.StartAngle + this.SweepAngle)* CoreMathOperation.ConvDgToRadian ), 
                             rd[i].Y * Math.Sin ( (this.StartAngle + this.SweepAngle)*CoreMathOperation.ConvDgToRadian )),
                             Center +new Vector2f(  rd[i+1].X * Math.Cos (
                            (this.StartAngle + this.SweepAngle)* CoreMathOperation.ConvDgToRadian ), 
                             rd[i+1].Y * Math.Sin ( (this.StartAngle + this.SweepAngle)*CoreMathOperation.ConvDgToRadian )));

                        segment.AddArc(v_rc,
                            this.StartAngle + this.SweepAngle,
                            - this.SweepAngle);


                        segment.AddLine(Center + new Vector2f(rd[i + 1].X * Math.Cos(
                        (this.StartAngle ) * CoreMathOperation.ConvDgToRadian),
                         rd[i+1].Y * Math.Sin((this.StartAngle) * CoreMathOperation.ConvDgToRadian)),
                         Center + new Vector2f(rd[i].X * Math.Cos(
                        (this.StartAngle ) * CoreMathOperation.ConvDgToRadian),
                         rd[i].Y * Math.Sin((this.StartAngle ) * CoreMathOperation.ConvDgToRadian)));
                        //path.CloseFigure();
                        //c.AddArc(
                        //    v_rc,
                        //    this.StartAngle,
                        //    this.SweepAngle, this.Closed);
                        segment.CloseFigure();
                    }
                    
                    path.AddSegment(segment);

                    if (this.Radius.Length > i)
                    {
                        var r = this.Radius[this.Radius.Length-1];
                        v_rc = new Rectanglef(
                                Center.X - r.X,
                                Center.Y - r.Y,
                                2 * r.X,
                                2 * r.Y);
                        path.AddArc(v_rc,
                            this.StartAngle,
                            this.SweepAngle, this.Closed);
                    }
                }
                path.FillMode = this.FillMode;
            }
        }
        new class Mecanism : ArcElement.Mecanism 
        { 
        }
    }
}
