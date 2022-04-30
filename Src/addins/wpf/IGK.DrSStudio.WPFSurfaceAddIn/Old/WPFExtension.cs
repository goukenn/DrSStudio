

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WPFExtension.cs
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
file:WPFExtension.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
namespace IGK.DrSStudio.WPFSurfaceAddIn
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects;
    public static class WPFExtension
    {
        public static PenLineJoin ToPenLineJoin(this System.Drawing.Drawing2D.LineJoin  LineJoin)
        {
            switch (LineJoin)
            {
                case System.Drawing.Drawing2D.LineJoin.Bevel:
                    return PenLineJoin.Bevel;
                case System.Drawing.Drawing2D.LineJoin.Miter:
                    return PenLineJoin.Miter;
                case System.Drawing.Drawing2D.LineJoin.MiterClipped:
                    break;
                case System.Drawing.Drawing2D.LineJoin.Round:
                    return PenLineJoin.Round;
                default:
                    break;
            }
            return PenLineJoin.Bevel;
        }
        public static Vector2d  ToVector2d(this System.Windows.Point a)
        {
            return new Vector2d(a.X, a.Y);
        }
        public static System.Windows.Point ToWPFPoint(this Vector2d a)
        {
            return new System.Windows.Point(a.X, a.Y);
        }
        public static System.Windows.Size ToWPFSize(this Vector2d a)
        {
            return new System.Windows.Size(Math.Abs (a.X), Math.Abs (a.Y));
        }
        public static System.Windows.Media.Color ToWPFDefinition(this Colorf color)
        {
            System.Drawing.Color cl = color;
            return System.Windows.Media.Color.FromArgb(cl.A, cl.R, cl.G, cl.B);
        }
        public static System.Windows.Media.Brush ToWPFDefinition(this ICoreBrush br)
        {
            System.Windows.Shapes.Shape  element = (br.Owner is WPFLayeredElement) ?
                (br.Owner as WPFLayeredElement).Shape as System.Windows.Shapes.Shape  : null;
            System.Windows.Media.Brush v_br = null;
            switch (br.BrushType)
            {
                case enuBrushType.Solid:
                    System.Drawing.Color cl  = br.Colors[0];
                    v_br =new  System.Windows.Media.SolidColorBrush(
                    System.Windows.Media.Color.FromArgb (cl.A , cl.R, cl.G, cl.B ));
                    break;
                case enuBrushType.Hatch:
                    break;
                case enuBrushType.LinearGradient:
                    GradientStopCollection stp = new GradientStopCollection();
                    GradientStop ps = null;
                    switch (br.LinearMode)
                    {
                        case enuLinearMode.Dual :
                            ps = new GradientStop ();
                            ps.Color = br.Colors[0].ToWPFDefinition();
                            ps.Offset = 0.0f;
                            stp.Add(ps);
                            ps = new GradientStop();
                            ps.Color = br.Colors[1].ToWPFDefinition();
                            ps.Offset = 1.0f;
                            stp.Add(ps);
                            break;
                        case enuLinearMode.DualBlend :
                            break;
                        case enuLinearMode.MultiColor :
                            for (int i = 0; i < br.Colors.Length; i++)
                            {
                                ps = new GradientStop();
                                ps.Color = br.Colors[i].ToWPFDefinition();
                                ps.Offset = br.Positions[i];
                                stp.Add(ps);
                            }
                            break;
                    }
                    LinearGradientBrush lbr = new LinearGradientBrush(
                        stp,
                        br.Angle 
                        );
                    v_br = lbr;
                    break;
                case enuBrushType.PathGradient:
                    break;
                case enuBrushType.Texture:
                    ImageSource src = null;
                    ImageBrush img = new ImageBrush (src );
                    v_br = img;
                    break;
                case enuBrushType.Custom:
                    break;
                default:
                    break;
            }
            if ((br is ICorePen) && (element != null))
            {
                ICorePen pen = br as ICorePen;
                element.StrokeThickness = pen.Width;
                element.StrokeStartLineCap = (PenLineCap)pen.StartCap;
                element.StrokeLineJoin = pen.LineJoin.ToPenLineJoin();         
            }
            return v_br ;
        }
        public static Vector2d[] GetResizePoints(this Rectangled rec)
        {
            Vector2d[] tab = new Vector2d[8];
            double hw = rec.Width / 2.0;
            double hh = rec.Height / 2.0;
            tab[0] = rec.Location;
            tab[1] = new Vector2d(rec.X + hw, rec.Y);
            tab[2] = new Vector2d(rec.Right , rec.Y);
            tab[3] = new Vector2d(rec.Right, rec.Y + hh);
            tab[4] = new Vector2d(rec.Right, rec.Bottom );
            tab[5] = new Vector2d(rec.X + hw, rec.Bottom );
            tab[6] = new Vector2d(rec.X , rec.Bottom);
            tab[7] = new Vector2d(rec.X , rec.Y + hh); 
            return tab;
        }
        public static Vector2d[] GetCornerPoints(this Rectangled rec)
        {
            Vector2d[] tab = new Vector2d[4];
            //double hw = rec.Width / 2.0;
            //double hh = rec.Height / 2.0;
            tab[0] = rec.Location;
            tab[1] = new Vector2d(rec.Right, rec.Y);
            tab[2] = new Vector2d(rec.Right, rec.Bottom);
            tab[3] = new Vector2d(rec.X, rec.Bottom);
            return tab;
        }
        public static Rectangled GetBound(this Vector2d[] points)
        {
            if (points.Length <= 1)
                return Rectangled.Empty;
            double minx;
            double miny;
            double maxx;
            double maxy;
            minx = points[0].X;
            miny = points[0].Y;
            maxx = minx;
            maxy = miny;
            for (int i = 1; i < points.Length; i++)
            {
                minx = Math.Min(minx, points[i].X);
                miny = Math.Min(miny, points[i].Y);
                maxx = Math.Max(maxx, points[i].X);
                maxy = Math.Max(maxy, points[i].Y);
            }
            return new Rectangled(
                minx,
                miny,
                maxx - minx,
                maxy - miny);
        }
        public static Vector2d GetCenter(this Rectangled rec)
        {
            return new Vector2d(rec.X + rec.Width / 2.0,
                rec.Y + rec.Height / 2.0);
        }
        public static System.Windows.Rect ToWPFRect(this Rectangled rec)
        {
            return new System.Windows.Rect(rec.X,
                rec.Y,
                rec.Width,
                rec.Height);
        }
        public static System.Drawing.Drawing2D.Matrix ToGdiMatrix(this System.Windows.Media.Matrix mat)
        {
            return new System.Drawing.Drawing2D.Matrix(
                (float)mat.M11, (float)mat.M12,
                (float)mat.M21, (float)mat.M22,
                (float)mat.OffsetX, (float)mat.OffsetY 
                );
        }
        public static void ScaleAsGdi( this System.Windows.Media.Matrix mat, double ex, double ey)
        {        
            mat.M11 *= ex;
            mat.M22 *= ey;
        }
        public static void Transform(this Vector2d[] tab, System.Windows.Media.Matrix matrix)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = matrix.Transform(tab[i].ToWPFPoint()).ToVector2d ();
            }
        }
        public static void TransformVector(this Vector2d[] tab, System.Windows.Media.Matrix matrix)
        {
            for (int i = 0; i < tab.Length; i++)
            {
                Vector2d d = new Vector2d();
                d.X = matrix.M11 * tab[i].X + matrix.M21 * tab[i].Y ;
                d.Y = matrix.M21 * tab[i].X + matrix.M22 * tab[i].Y ;
                tab[i] = d;
            }
        }
    }
}

