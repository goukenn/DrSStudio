

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMathOperation.cs
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
file:CoreMathOperation.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.GraphicModels;
    /// <summary>
    /// represent most used mathematical operation
    /// </summary>
    public static class CoreMathOperation
    {
        public const float ConvRdToDEGREE = (float)(180 / System.Math.PI);
        public const float ConvDgToRadian = (float)(System.Math.PI / 180.0F);
        public static readonly Matrix MatrixIdentity;
        static CoreMathOperation()
        {
            MatrixIdentity = new Matrix();
        }
        public static string ToHex(byte t)
        {
            int h = ((h = (t / 16)) < 10) ? h + '0' : (h - 10) + 'A';
            int v = ((v = t & 0x0f) < 10) ? v + '0' : (v - 10) + 'A';
            string stv = (char)h + "" + (char)v;
            return stv;
        }

        /// <summary>
        /// flip rectangel 
        /// </summary>
        /// <param name="v_pts"></param>
        /// <returns></returns>
        public static Vector2f[] FlipY(Vector2f[] v_pts)
        {
            global::IGK.ICore.Matrix m = new Matrix();
            Rectanglef b = GetBounds(v_pts);
            Vector2f endLoc = new Vector2f(b.X, b.Y + b.Height);
            m.Translate(-endLoc.X, -endLoc.Y, enuMatrixOrder.Append);
            m.Scale(1, -1, enuMatrixOrder.Append);
            m.Translate(endLoc.X, b.Height, enuMatrixOrder.Append);
            //m.Translate(endLoc.X, endLoc.Y, enuMatrixOrder.Append);
            return CoreMathOperation.MultMatrixTransformVector(m, v_pts);
        }

        /// <summary>
        /// get factor location
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="PosX">Posx Offset</param>
        /// <param name="PosY">PosY Offset</param>
        /// <param name="ZoomX">factor x</param>
        /// <param name="ZoomY">factor y</param>
        /// <returns></returns>
        public static Vector2f GetFactorLocation(float x, float y, float PosX, float PosY, float ZoomX, float ZoomY)
        {
            Vector2f pts = new Vector2f(
(float)Math.Floor((x - PosX) / ZoomX),
(float)Math.Floor((y - PosY) / ZoomY));
            return pts;
        }
        private static Vector2f GetFactorLocation(Vector2f vector2f, float PosX, float PosY, float ZoomX, float ZoomY)
        {
            return GetFactorLocation(vector2f.X, vector2f.Y, PosX, PosY, ZoomX, ZoomY);
        }

        public static void AlignCircle(ICoreAlignmentCircle item, enuCore2DAlignElement alignment, Rectanglef bounds, out float dx, out float dy)
        {
            Matrix m = item.GetMatrix();
            Rectanglef b = item.GetAlignmentBound();
            Vector2f c = item.Center; //CoreMathOperation.GetCenter(b);
            Vector2f bc = CoreMathOperation.GetCenter(bounds);
            Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            Rectanglef v_rc = bounds;
            dx = 0.0f;
            dy = 0.0f;
            //float rx = b.X;
            //float ry = b.Y;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    dx = 0;
                    dy = 0;
                    break;
                case enuCore2DAlignElement.CenterHorizontal:
                    //ry = 0;
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    //this.Translate(dx, dy, enuMatrixOrder.Append, false);
                    break;
                case enuCore2DAlignElement.CenterVertical:
                   // rx = 0;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    //this.Translate(dx, dy, enuMatrixOrder.Append, false);
                    break;
                case enuCore2DAlignElement.TopMiddle:
                    dx = (v_rc.Width - b.Width) / 2.0f;
                    break;
                case enuCore2DAlignElement.TopRight:
                    dx = v_rc.Width - b.Width;
                    break;
                case enuCore2DAlignElement.Center:
                    dx = p.X;// (v_rc.Width - b.Width) / 2.0f;
                    dy = p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    //this.Translate(dx, dy, enuMatrixOrder.Append, false);
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
        }

        public static Rectanglef GetScreenBounds(Rectanglef zoomBound, float PosX, float PosY, float ZoomX, float ZoomY)
        {
             Vector2f pts = GetScreenLocation(zoomBound.Location, PosX , PosY, ZoomX , ZoomY);
                return new Rectanglef(pts.X,
                    pts.Y,
                        zoomBound.Width * ZoomX,
                        zoomBound.Height * ZoomY
                        );
            
        }

        public static Vector2f GetScreenLocation(Vector2f vector2f, float PosX, float PosY, float ZoomX, float ZoomY)
        {
            return new Vector2f(
(int)((vector2f.X * ZoomX) + PosX),
(int)((vector2f.Y * ZoomY) + PosY)
);
        }

    
        /// <summary>
        /// get angle ° Vector2i definition
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Vector3f GetAngle(Vector3f start, Vector3f end)
        {
            float az_xy = CoreMathOperation.ConvRdToDEGREE * CoreMathOperation.GetAngle(
                    new Vector2f((float)start.X, (float)start.Y),
                    new Vector2f((float)end.X, (float)end.Y));
            float ax_zy = CoreMathOperation.ConvRdToDEGREE * CoreMathOperation.GetAngle(
                    new Vector2f((float)start.Z, (float)start.Y),
                    new Vector2f((float)end.Z, (float)end.Y));
            float ay_zx = CoreMathOperation.ConvRdToDEGREE * CoreMathOperation.GetAngle(
                    new Vector2f((float)start.Z, (float)start.X),
                    new Vector2f((float)end.Z, (float)end.X));
            return new Vector3f(ax_zy, ay_zx, az_xy);
        }
        /// <summary>
        /// get angle ° Vector2i definition
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Vector3f GetAngle(Vector3f start)
        {
            return GetAngle(Vector3f.Zero, start);
        }
        //public static ColorMatrix MultMatrix(ColorMatrix mat1, ColorMatrix mat2)
        //{
        //    ColorMatrix rv = new ColorMatrix();
        //    int c = 0;
        //    int l = 0;
        //    float ev = 0.0f;
        //    for (l = 0; l < 5; l++)
        //    {
        //        for (c = 0; c < 5; c++)
        //        {
        //            ev = 0.0f;
        //            for (int i = 0; i < 5; i++)
        //            {
        //                ev += mat1[l, i] * mat2[i, c];
        //            }
        //            rv[l, c] = ev;
        //        }
        //    }
        //    return rv;
        //}
        //public static ColorMatrix MultMatrix(float factor, ColorMatrix mat)
        //{
        //    for (int c = 0; c < 5; c++)
        //    {
        //        for (int i = 0; i < 5; i++)
        //        {
        //            mat[i, c] *= factor;
        //        }
        //    }
        //    return mat;
        //}
        public static Matrix MultMatrix(Matrix mat1, Matrix mat2)
        {
            Matrix rv = new Matrix();
            rv.Multiply(mat1);
            rv.Multiply(mat2);
            return rv;
        }
        /// <summary>
        /// get Vector2i
        /// </summary>
        /// <param name="center">center of this cicle</param>
        /// <param name="radius">radius of this circle</param>
        /// <param name="angle">angle in degree</param>
        /// <returns></returns>
        public static Vector2f GetPoint(Vector2f center, float radius, float angle)
        {
            float x = (float)(radius * Math.Cos(angle * Math.PI / 180.0f));
            float y = (float)(radius * Math.Sin(angle * Math.PI / 180.0f));
            Vector2f pts = new Vector2f(center.X + x, center.Y + y);
            return pts;
        }
        /// <summary>
        /// get Vector2i
        /// </summary>
        /// <param name="center">center of this cicle</param>
        /// <param name="radius">radius of this circle</param>
        /// <param name="angle">angle in degree</param>
        /// <returns></returns>
        public static Vector2f GetPoint(Vector2f center, Vector2f radius, float angle)
        {
            float x = (float)(radius.X  * Math.Cos(angle * Math.PI / 180.0f));
            float y = (float)(radius.Y  * Math.Sin(angle * Math.PI / 180.0f));
            Vector2f pts = new Vector2f(center.X + x, center.Y + y);
            return pts;
        }
        /// <summary>
        /// get Vector2i
        /// </summary>
        /// <param name="center">center of this cicle</param>
        /// <param name="radiusx">radiusx of this ellipse</param>
        /// <param name="radiusy">radiusy of this ellipse</param>
        /// <param name="angle">angle in degree</param>
        /// <returns></returns>
        public static Vector2f GetPoint(Vector2f center, float radiusx, float radiusy, float angle)
        {
            float x = (float)(radiusx * Math.Cos(angle * Math.PI / 180.0f));
            float y = (float)(radiusy * Math.Sin(angle * Math.PI / 180.0f));
            Vector2f pts = new Vector2f(center.X + x, center.Y + y);
            return pts;
        }
        //get the disttance beetween 2 Vector2is
        public static double GetDistance(Vector2d EndVector2i, Vector2d StartVector2i)
        {
            double  dx = EndVector2i.X - StartVector2i.X;
            double  dy = EndVector2i.Y - StartVector2i.Y;
            //carcul du rayon
            return Math.Sqrt((dx * dx) + (dy * dy));
        }
        //get the disttance beetween 2 Vector2is
        public static float GetDistance(Vector2f EndVector2i, Vector2f StartVector2i)
        {
            float dx = EndVector2i.X - StartVector2i.X;
            float dy = EndVector2i.Y - StartVector2i.Y;
            //carcul du rayon
            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }
        //get the distance beetween 2 Vector2is
        public static Vector2f GetDistanceP(Vector2f EndVector2i, Vector2f StartVector2i)
        {
            float dx = EndVector2i.X - StartVector2i.X;
            float dy = EndVector2i.Y - StartVector2i.Y;
            //carcul du rayon
            return new Vector2f(dx, dy);
        }
        /// <summary>
        /// get angle in radians beetween two Vector2is
        /// </summary>
        /// <param name="p1">start point</param>
        /// <param name="p2">end point</param>
        /// <returns></returns>
        /// <remarks>angle is between 0 and 270 and 0-90</remarks>
        public static float GetAngle(Vector2f  startVector2i, Vector2f endVector2i)
        {
            float dx, dy;
            dx = endVector2i.X - startVector2i.X;
            dy = endVector2i.Y - startVector2i.Y;
            if ((dx == 0.0f) && (dy == 0.0f))
            {
                return 0.0f;
            }
            if (dx == 0.0f)
            {
                if (dy > 0)
                {
                    return (float)(Math.PI / 2.0f);
                }
                else
                    return (float)(-Math.PI / 2.0f);
            }
            float angle = (float)Math.Atan(dy / dx);
            if (dx < 0)
                angle += (float)Math.PI;
            return angle;
        }
        //get bounds
        public static Rectanglef GetBounds(Vector2f startVector2i, Vector2f endVector2i)
        {
            return new Rectanglef(
                Math.Min(startVector2i.X, endVector2i.X),
                Math.Min(startVector2i.Y, endVector2i.Y),
                Math.Abs(startVector2i.X - endVector2i.X),
                Math.Abs(startVector2i.Y - endVector2i.Y)
                );
        }
        public static Rectangled GetBounds(Vector2d  startVector2i, Vector2d endVector2i)
        {
            return new Rectangled(
                Math.Min(startVector2i.X, endVector2i.X),
                Math.Min(startVector2i.Y, endVector2i.Y),
                Math.Abs(startVector2i.X - endVector2i.X),
                Math.Abs(startVector2i.Y - endVector2i.Y)
                );
        }
        //get bound from center and radis
        public static Rectanglef GetBounds(Vector2f center, float radius)
        {
            return new Rectanglef(
                center.X - radius,
                center.Y - radius,
                2 * radius,
                2 * radius
                );
        }
        /// <summary>
        /// utiliser pour déterminer si le rectangle prBounds englobe le rectangle chBounds
        /// </summary>
        /// <param name="prBounds"></param>
        /// <param name="chBounds"></param>
        /// <returns></returns>
        public static bool IsBoundContains(Rectanglef prBounds, Rectanglef chBounds)
        {
            bool vresult = false;
            Vector2f endpr = new Vector2f(prBounds.X + prBounds.Width, prBounds.Y + prBounds.Height);
            Vector2f endch = new Vector2f(chBounds.X + chBounds.Width, chBounds.Y + chBounds.Height);
            if ((prBounds.X < chBounds.X) &&
                (prBounds.Y < chBounds.Y))
            {
                if (
                (endpr.X > endch.X) &&
                (endpr.Y > endch.Y))
                {
                    vresult = true;
                }
            }
            return vresult;
        }
        internal static Rectanglef GetBounds(float x1, float y1, float x2, float y2)
        {
            return GetBounds(new Vector2f(x1, y1), new Vector2f(x2, y2));
        }
        public static Rectanglef GetBounds(params Rectanglef[] rectangle)
        {
            if (rectangle.Length == 0)
                return Rectanglef.Empty;
            float minx = rectangle[0].X;
            float miny = rectangle[0].Y;
            float maxx = rectangle[0].Right;
            float maxy = rectangle[0].Bottom;
            for (int i = 1; i < rectangle.Length; i++)
            {
                minx = Math.Min(minx, rectangle[i].X);
                miny = Math.Min(miny, rectangle[i].Y);
                maxx = Math.Max(maxx, rectangle[i].Right);
                maxy = Math.Max(maxy, rectangle[i].Bottom);
            }
            return new Rectanglef(
                minx,
                miny,
                maxx - minx,
                maxy - miny);
        }
        /// <summary>
        /// get the outline bounds
        /// </summary>
        /// <param name="Vector"></param>
        /// <returns></returns>
        public static Rectanglef GetBounds(Vector2f[] Vector)
        { 
            if ((Vector == null) || (Vector.Length == 0))
                return Rectanglef.Empty;
            float minx;
            float miny;
            float maxx;
            float maxy;
            minx = Vector[0].X;
            miny = Vector[0].Y;
            maxx = minx;
            maxy = miny;
            for (int i = 1; i < Vector.Length; i++)
            {
                minx = Math.Min(minx, Vector[i].X);
                miny = Math.Min(miny, Vector[i].Y);
                maxx = Math.Max(maxx, Vector[i].X);
                maxy = Math.Max(maxy, Vector[i].Y);
            }
            return new Rectanglef(
                minx,
                miny,
                maxx - minx,
                maxy - miny);
        }
        //get bounds from Vector2is
        public static Rectanglef GetBounds(Vector2i[] Vector2is)
        {
            if ((Vector2is == null) || (Vector2is.Length == 0))
                return Rectanglef.Empty;
            float minx;
            float miny;
            float maxx;
            float maxy;
            minx = Vector2is[0].X;
            miny = Vector2is[0].Y;
            maxx = minx;
            maxy = miny;
            for (int i = 1; i < Vector2is.Length; i++)
            {
                minx = Math.Min(minx, Vector2is[i].X);
                miny = Math.Min(miny, Vector2is[i].Y);
                maxx = Math.Max(maxx, Vector2is[i].X);
                maxy = Math.Max(maxy, Vector2is[i].Y);
            }
            return new Rectanglef(
                minx,
                miny,
                maxx - minx,
                maxy - miny);
        }
        //get Vector2is associate to rectangle
        public static Vector2f [] GetPoints(Rectanglef Rectanglef)
        {
            return new Vector2f[]{
                Rectanglef.Location ,
                new Vector2f (Rectanglef.X + Rectanglef.Width  , Rectanglef.Y),
                new Vector2f (Rectanglef.X + Rectanglef.Width  , Rectanglef.Y + Rectanglef.Height ),
                new Vector2f (Rectanglef.X   , Rectanglef.Y + Rectanglef.Height )
            };
        }
        ////get Vector2is associate to rectangle
        //public static Vector2f [] GetResizePoints(Rectanglef Rectanglef)
        //{
        //    return new Vector2f[]{
        //       /*0*/ Rectanglef.Location ,
        //       /*1*/ new Vector2f (Rectanglef.X + (Rectanglef.Width /2.0f) , Rectanglef.Y),
        //        new Vector2f (Rectanglef.X + Rectanglef.Width  , Rectanglef.Y),
        //        new Vector2f (Rectanglef.X + Rectanglef.Width  , Rectanglef.Y + (Rectanglef.Height / 2.0f) ),
        //        new Vector2f (Rectanglef.X + Rectanglef.Width  , Rectanglef.Y + Rectanglef.Height ),
        //        new Vector2f (Rectanglef.X + (Rectanglef.Width/2.0f)  , Rectanglef.Y + Rectanglef.Height ),
        //        new Vector2f (Rectanglef.X   , Rectanglef.Y + Rectanglef.Height ),
        //        new Vector2f (Rectanglef.X   , (Rectanglef.Y + Rectanglef.Height/2.0f) )
        //    };
        //}
        /// <summary>
        ///get the center of the rectangle 
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Vector2f GetCenter(Rectanglef rectangle)
        {
            Vector2f pts = new Vector2f(
                rectangle.X + (rectangle.Width / 2.0f),
                rectangle.Y + (rectangle.Height / 2.0f)
                );
            return pts;
        }
        /// <summary>
        /// get the internal circle of this rectangle
        /// </summary>
        /// <param name="rectangle"></param>
        /// <returns></returns>
        public static Rectanglef GetInternalCircle(Rectanglef rectangle)
        {
            float d = Math.Min(rectangle.Width, rectangle.Height) / 2.0f;
            Vector2f center = GetCenter(rectangle);
            return new Rectanglef(
                center.X - d,
                center.Y - d,
                d * 2.0f,
                d * 2.0f
                );
        }
        public static Rectanglef GetOuterEllipseRectangle(Rectanglef rectangle)
        {
            float w = rectangle.Width / 2.0f;
            float h = rectangle.Height / 2.0f;
            float r = (float)Math.Sqrt((w * w) + (h * h));
            Vector2f center = GetCenter(rectangle);
            return new Rectanglef(
                center.X - r,
                center.Y - r,
                r * 2,
                r * 2
                );
        }
        public static Rectanglef GetInternalCircle(Vector2f center, Rectanglef rectangle)
        {
            //get distance
            Vector2f[] tab = GetPoints(rectangle);
            float d1 = Math.Abs(GetDistance(center, tab[0]));
            float d2 = Math.Abs(GetDistance(center, tab[1]));
            float d3 = Math.Abs(GetDistance(center, tab[2]));
            float d4 = Math.Abs(GetDistance(center, tab[3]));
            float d = Math.Min(Math.Min(d1, d2), Math.Min(d3, d4)) / 2.0f;
            //center = GetCenter(rectangle);
            return new Rectanglef(
                center.X - d,
                center.Y - d,
                d * 2.0f,
                d * 2.0f
                );
        }
        public static Vector2f GetReverseVector2formMatrix(Vector2f Vector2is, Matrix mat)
        {
            float a, b, c, d, e, f, A, B;
            //Matrix m = mat.Clone () as Matrix ;
            //if (m.IsInvertible )
            //{
            //    m.Invert ();
            //    Vector2f[] tab = new Vector2f[]{Vector2is };
            //    m.TransformVector2is (tab );
            //    return tab[0];
            //}
            a = mat.Elements[0];
            b = mat.Elements[1];
            c = mat.Elements[2];
            d = mat.Elements[3];
            e = mat.Elements[4];
            f = mat.Elements[5];
            A = Vector2is.X - e;
            B = Vector2is.Y - f;
            Vector2f _out = Vector2f.Zero;
            _out.Y = ((c * A) - (a * B)) / ((c * b) - (d * a));
            _out.X = ((d * A) - (b * B)) / ((d * a) - (c * b));
            return _out;
        }
        public static Vector2f[] GetStartAndEndVector2i(Rectanglef rectangle)
        {
            Vector2f[] pts = new Vector2f[2];
            pts[0] = rectangle.Location;
            pts[1] = new Vector2f(rectangle.Location.X + rectangle.Width, rectangle.Height + rectangle.Y);
            return pts;
        }
        /// <summary>
        /// get the angle between 2 Vector2i and a center
        /// </summary>
        /// <param name="center"></param>
        /// <param name="Vector2i1"></param>
        /// <param name="Vector2i2"></param>
        /// <returns>angle in radian</returns>
        public static float GetAngle(Vector2f center, Vector2f Vector2i1, Vector2f Vector2i2)
        {
            //caculate vector
            Vector2f pts1 = new Vector2f(Vector2i1.X - center.X, Vector2i1.Y - center.Y);
            Vector2f pts2 = new Vector2f(Vector2i2.X - center.X, Vector2i2.Y - center.Y);
            float a2 = GetAngle(pts1, pts2);            
            return a2;
        }
        public static Rectanglef ApplyZoom(Rectanglef f, float ZoomX, float ZoomY, float PosX, float PosY)
        {
            f.X = ((f.X * ZoomX) + PosX);
            f.Y = ((f.Y * ZoomY) + PosY);
            f.Width = ((f.Width * ZoomX));
            f.Height = ((f.Height * ZoomY));
            return f;
        }
        internal static Vector2f[] GetPoints(float x1, float y1, float x2, float y2)
        {
            Vector2f[] ptsTab = new Vector2f[2];
            ptsTab[0] = new Vector2f(x1, y1);
            ptsTab[1] = new Vector2f(x2, y2);
            return ptsTab;
        }
        public static Rectanglef ApplyMatrix(Rectanglef Rectanglef, Matrix matrix)
        {
            Vector2f[] v_pts = GetPoints(Rectanglef);
            v_pts = TransformVector2fPoint(matrix, v_pts);
            //matrix.TransformVector2is(Vector2is);
            return GetBounds(v_pts);
        }
        public static Vector2f Add(Vector2f Vector2f, float x, float y)
        {
            Vector2f ptr = Vector2f;
            ptr.X = Vector2f.X + x;
            ptr.Y = Vector2f.Y + y;
            return ptr;
        }
        public static Rectanglef GetBounds(Vector2f center, float radiusx, float radiusy)
        {
            return new Rectanglef(center.X - Math.Abs (radiusx),
                center.Y - Math.Abs (radiusy),
                2.0F * Math.Abs (radiusx),
                2 * Math.Abs (radiusy));
        }
        /// <summary>
        /// get the maximum value;
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static float GetMax(float[] p)
        {
            if ((p == null) || (p.Length == 0))
                return 0.0F;
            float max = p[0];
            for (int i = 1; i < p.Length; ++i)
            {
                if (max < p[i])
                    max = p[i];
            }
            return max;
        }
        /// <summary>
        /// get the minimum value
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static float GetMin(float[] p)
        {
            float min = p[0];
            for (int i = 1; i < p.Length; ++i)
            {
                if (min > p[i])
                    min = p[i];
            }
            return min;
        }
        /// <summary>
        /// get the minimaximu radis data
        /// </summary>
        /// <param name="coreRadiusData"></param>
        /// <returns></returns>
        public static Vector2f  GetMax(Vector2f[] coreRadiusData)
        {
            if ((coreRadiusData == null) || (coreRadiusData.Length == 0)) return Vector2f.Zero;
            float radiusx = coreRadiusData[0].X;
            float radiusy = coreRadiusData[0].Y; 
            for (int i = 1; i < coreRadiusData.Length; i++)
            {
                radiusx = Math.Max(coreRadiusData[i].X, radiusx);
                radiusy = Math.Max(coreRadiusData[i].Y, radiusy);
            }
            return new Vector2f(radiusx, radiusy);
        }
        /// <summary>
        /// get the mininum radius data
        /// </summary>
        /// <param name="coreRadiusData"></param>
        /// <returns></returns>
        public static Vector2f GetMin(Vector2f[] coreRadiusData)
        {
            if ((coreRadiusData == null) || (coreRadiusData.Length == 0)) return Vector2f.Zero;
            float radiusx = coreRadiusData[0].X;
            float radiusy = coreRadiusData[1].Y;
            for (int i = 1; i < coreRadiusData.Length; i++)
            {
                radiusx = Math.Min(coreRadiusData[i].X, radiusx);
                radiusy = Math.Min(coreRadiusData[i].Y, radiusy);
            }
            return new Vector2f(radiusx, radiusy);
        }
        /// <summary>
        /// get middle Vector2i
        /// </summary>
        /// <param name="Vector2f"></param>
        /// <param name="Vector2f_2"></param>
        /// <returns></returns>
        public static Vector2f GetMiddlePoint(Vector2f startVector2i, Vector2f endVector2i)
        {
            return new Vector2f(
                (startVector2i.X + endVector2i.X) / 2.0f,
                (startVector2i.Y + endVector2i.Y) / 2.0f
                );
        }
        public static Vector2f GetInnerPoint(Vector2f startVector2i, Vector2f endVector2i, float divisior)
        {
            if (divisior == 0)
                throw new CoreException(
                    enuExceptionType .ArgumentNotValid  
                    , "divisor");
            return new Vector2f(
                startVector2i.X + Math.Abs(startVector2i.X - endVector2i.X) / divisior,
                startVector2i.Y + Math.Abs(startVector2i.Y - endVector2i.Y) / divisior
                );
        }
        /// <summary>
        /// get proportinal rectangle
        /// </summary>
        /// <param name="rcDestination"></param>
        /// <param name="rcCurrent"></param>
        /// <returns></returns>
        public static Rectanglef GetProportionalRectangle(Rectanglef rcDestination, Rectanglef rcCurrent)
        {
            float zoomx;
            float zoomy;
            int x = 0;
            int y = 0;
            int w = 0;
            int h = 0;
            //for saving
            zoomx = rcDestination.Width / rcCurrent.Width;
            zoomy = rcDestination.Height / rcCurrent.Height;
            zoomx = Math.Min(zoomx, zoomy);
            zoomy = zoomx;
            x = (int)(((-rcCurrent.Width * zoomx) / 2.0f) + (rcDestination.Width / 2.0f));
            y = (int)(((-rcCurrent.Height * zoomx) / 2.0f) + (rcDestination.Height / 2.0f));
            w = (int)(rcCurrent.Width * zoomx);
            h = (int)(rcCurrent.Height * zoomx);
            return new Rectanglef(x, y, w, h);
        }
        public static Vector2f[] MultMatrixTransformPoint(Matrix m, params Vector2f[] t)
        {
            t = m.TransformPoints(t);
            return t;
        }
        public static Vector2f[] MultMatrixTransformVector(Matrix m, params Vector2f[] t)
        {
            t = m.TransformVectors(t);
            return t;
        }
        /// <summary>
        /// get the polygon Vector2i
        /// </summary>
        /// <param name="center">center of the required polygon</param>
        /// <param name="radius">center of the required polygon</param>
        /// <param name="count">number of Vector2i </param>
        /// <param name="angle">offset angle in degree</param>
        /// <returns></returns>
        public static Vector2f[] GetPolygons(Vector2f center, float radius, int count, float angle)
        {
            Vector2f[] vtab = new Vector2f[count];
            float step = (float)((360 / (float)count) * (Math.PI / 180.0f));
            float vangle = (float)(angle * (Math.PI / 180.0f));
            for (int i = 0; i < count; i++)
            {
                vtab[i] = new Vector2f(
                    (float)(center.X + radius * Math.Cos(i * step + vangle)),
                    (float)(center.Y + radius * Math.Sin(i * step + vangle)));
            }
            return vtab;
        }
        /// <summary>
        /// get the polygon Vector2i
        /// </summary>
        /// <param name="center">center of the required polygon</param>
        /// <param name="radius">center of the required polygon</param>
        /// <param name="count">number of Vector2i </param>
        /// <param name="angle">offset angle in degree</param>
        /// <returns></returns>
        public static Vector2f[] GetPolygons(Vector2f center, float radiusx, float radiusy, int count, float angle)
        {
            Vector2f[] vtab = new Vector2f[count];
            float step = (float)((360 / (float)count) * (Math.PI / 180.0f));
            float vangle = (float)(angle * (Math.PI / 180.0f));
            for (int i = 0; i < count; i++)
            {
                vtab[i] = new Vector2f(
                    (float)(center.X + radiusx * Math.Cos(i * step + vangle)),
                    (float)(center.Y + radiusy * Math.Sin(i * step + vangle)));
            }
            return vtab;
        }
        public static bool GetIntersection(Vector2f A, Vector2f B, Vector2f C, Vector2f D, bool free , ref Vector2f Out)
        {
            float x = 0.0f;
            float y = 0.0f;
            float a1 = 0.0f;
            float a2 = 0.0f;
            float b1 = 0.0f;
            float b2 = 0.0f;
            if (A.X == B.X)
            {
                //pente vertical
                if (C.X != D.X)
                {
                    a2 = (C.Y - D.Y) / (C.X - D.X);
                    b2 = ((C.Y * D.X) - (D.Y * C.X)) / (D.X - C.X);
                    x = A.X;
                    y = (a2 * x) + b2;
                }
                else return false;
            }
            else if (C.X == D.X)
            {
                a1 = (A.Y - B.Y) / (A.X - B.X);
                b1 = ((A.Y * B.X) - (B.Y * A.X)) / (B.X - A.X);
                x = C.X;
                y = (a1 * x) + b1;
            }
            else
            {
                //calcul de la pente
                a1 = ((A.X - B.X) == 0) ? 0 : (A.Y - B.Y) / (A.X - B.X);
                a2 = ((C.X - D.X) == 0) ? 0 : (C.Y - D.Y) / (C.X - D.X);
                //pente paralle
                if (a1 == a2)
                    return false;
                b1 = ((B.X - A.X) == 0) ? 0 : ((A.Y * B.X) - (B.Y * A.X)) / (B.X - A.X);
                b2 = ((D.X - C.X) == 0) ? 0 : ((C.Y * D.X) - (D.Y * C.X)) / (D.X - C.X);
                x = (b2 - b1) / (a1 - a2);
                y = (b2 * a1 - b1 * a2) / (a1 - a2);
            }
            //check for Vector2i inclusion
            if (!free)
            {
                if (
                     (x < Math.Min(A.X, B.X)) ||
                     (x > Math.Max(A.X, B.X)) ||
                     (x < Math.Min(C.X, D.X)) ||
                     (x > Math.Max(C.X, D.X)) ||
                     (y < Math.Min(A.Y, B.Y)) ||
                     (y > Math.Max(A.Y, B.Y)) ||
                     (y < Math.Min(C.Y, D.Y)) ||
                     (y > Math.Max(C.Y, D.Y))
                    )
                {
                    return false;
                }
            }
            Out = new Vector2f(x, y);
            return true;
        }
        /// <summary>
        /// get the intersection Vector2i
        /// </summary>
        /// <param name="A">d1 start</param>
        /// <param name="B">d1 end</param>
        /// <param name="C">d2 start</param>
        /// <param name="D">d2 end</param>
        /// <param name="Out">ref out variable</param>
        /// <returns>return true if succed and the Vector2i is in Out variable</returns>
        public static bool GetIntersection(Vector2f A, Vector2f B, Vector2f C, Vector2f D, ref Vector2f Out)
        {
            return GetIntersection(A, B, C, D, false, ref Out);
        }
        public static Vector2f[] GetGlobalVector2i(Vector2f[] p)
        {
            List<Vector2f> t = new List<Vector2f>();
            Rectanglef v_rc =  GetBounds(p);
            Vector2f center = CoreMathOperation.GetCenter(v_rc);
            float d =0.0f;
            float angle = 0.0f;
            float[] tangle  = new float [p.Length ];
            int i = 0;
            foreach (Vector2f h in p)
            {
                d = Vector2f.Distance(center, h);
                angle = GetAngle(center, h) * ConvRdToDEGREE ;
                tangle[i] = angle;
                t.Add(h);
                i++;
            }
            Vector2f[] c = t.ToArray ();
            Array.Sort(tangle, c, 0, c.Length);
            return c;
        }
        /// <summary>
        /// get fan Vector2i from Vector2i
        /// </summary>
        /// <param name="Vector2i">array of Vector2i min 3</param>
        /// <param name="Angle">rotation agnle > 0</param>
        /// <returns></returns>
        public static Vector2f [] GetFanVector2is(Vector2f[] Vector2i, float Angle)
        {
            if ((Vector2i == null) || (Vector2i.Length < 3))
            {
                throw new ArgumentException($"{nameof(Vector2i)}");
            }
            if (Angle == 0.0f)
                throw new  ArgumentException ($"{nameof(Angle)}");
            List<Vector2f> list = new List<Vector2f>();
            Vector2f c = Vector2f.Zero;
            Rectanglef rectangle = GetBounds(Vector2i);
            Vector2f v_center = new Vector2f(rectangle.X + rectangle.Width / 2,
                rectangle.Y + rectangle.Height / 2.0f);
            float v_radius = GetDistance(rectangle.Location, v_center);
            Vector2f[,] tab = new Vector2f[Vector2i.Length, 2];
            for (int i = 0; i < Vector2i.Length - 1; i++)
            {
                tab[i, 0] = Vector2i[i];
                tab[i, 1] = Vector2i[i + 1];
            }
            //last segment
            tab[Vector2i.Length - 1, 0] = tab[Vector2i.Length - 2, 1];
            tab[Vector2i.Length - 1, 1] = tab[0, 0];
            for (float angle = 0; angle < 360; angle += Angle)
            {
                for (int i = 0; i < Vector2i.Length; i++)
                {
                    if (GetIntersection(tab[i, 0],
                        tab[i, 1],
                        v_center,
                        new Vector2f(
                            (float)(v_center.X + v_radius * Math.Cos(angle * Math.PI / 180.0f)),
                            (float)(v_center.Y + v_radius * Math.Sin(angle * Math.PI / 180.0f))
                            ),
                        ref c))
                    {
                        list.Add(c);
                        break;
                    }
                }
            }
            return list.ToArray();
        }
        public static Vector2f [] GetFanVector2is(Rectanglef rectangle, float Angle)
        {
            return GetFanVector2is(CoreMathOperation.GetPoints(rectangle), Angle);
        }
        public static Rectanglef Increase(Rectanglef rectangle, int size)
        {
            Rectanglef v_rc = rectangle;
            v_rc.Inflate(size, size);
            return v_rc;
        }
        /// <summary>
        /// get diamond rectangle
        /// </summary>
        /// <param name="vr"></param>
        /// <returns></returns>
        public static Vector2f[] GetDiamond(Rectanglef vr)
        {
            float v_hw = vr.Width / 2.0f;
            float v_hh = vr.Height / 2.0f;
            return new Vector2f[] { 
                new Vector2f(vr.X +  v_hw, vr.Y ),
                new Vector2f(vr.X +  vr.Width , vr.Y + v_hh ),
                new Vector2f(vr.X +  v_hw, vr.Y + vr.Height  ),
                new Vector2f(vr.X , vr.Y  + v_hh)
                   };
        }
        /// <summary>
        /// align this elmenent to parent document
        /// </summary>
        /// <param name="alignment"></param>
        public static Rectanglef Align(Rectanglef source, 
            Rectanglef bounds, 
            enuCore2DAlignElement 
            alignment)
        {
            Vector2f c = CoreMathOperation.GetCenter(source);
            Vector2f bc = CoreMathOperation.GetCenter(bounds);
            Vector2f p = CoreMathOperation.GetDistanceP(bc, c);
            Rectanglef v_rc = bounds;
            float dx = 0.0f;
            float dy = 0.0f;
            float rx = source.X;
            float ry = source.Y;
            switch (alignment)
            {
                case enuCore2DAlignElement.TopLeft:
                    dx = bounds.X;
                    dy = bounds.Y;
                    return new Rectanglef(dx, dy, source.Width, source.Height);
                case enuCore2DAlignElement.CenterHorizontal:
                    ry = bounds.Y;
                    dx = bounds.X + p.X;
                    return new Rectanglef(dx, dy, source.Width, source.Height);
                case enuCore2DAlignElement.CenterVertical:
                    dx = bounds.X;
                    dy = bounds.Y;
                    return new Rectanglef(dx, dy, source.Width, source.Height);
                case enuCore2DAlignElement.TopMiddle:
                    dy = bounds.Y;
                    dx = bounds.X + (v_rc.Width - source.Width) / 2.0f;
                    break;
                case enuCore2DAlignElement.TopRight:
                    dy = bounds.Y;
                    dx = bounds.Right - source.Width;
                    break;
                case enuCore2DAlignElement.Center:
                    dx = source.X + p.X;// (v_rc.Width - b.Width) / 2.0f;
                    dy = source.Y + p.Y;// (v_rc.Height - b.Height) / 2.0f;
                    return new Rectanglef(dx, dy, source.Width, source.Height);
                case enuCore2DAlignElement.MiddleLeft:
                    dx = bounds.X;
                    dy = bounds.Y + (v_rc.Height - source.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.MiddleRight:
                    dx = bounds.Right - source.Width;
                    dy = bounds.Y + (v_rc.Height - source.Height) / 2.0f;
                    break;
                case enuCore2DAlignElement.BottomLeft:
                    dy = bounds.Bottom - source.Height;
                    dx = bounds.Left;
                    break;
                case enuCore2DAlignElement.BottomRight:
                    dy = bounds.Bottom - source.Height;
                    dx = bounds.Right - source.Width;
                    break;
                case enuCore2DAlignElement.BottomMiddle:
                    dx = bounds.Left + (v_rc.Width - source.Width) / 2.0f;
                    dy = bounds.Bottom - source.Height;
                    break;
            }
            //dx = bounds.X + dx - rx;
            //dy = bounds.Y + dy - ry;
            return new Rectanglef(dx, dy, source.Width, source.Height);
        }
        /// <summary>
        /// transform vector2f Vector2i array with offset x and y.
        /// </summary>
        /// <param name="m"></param>
        /// <param name="vector2f"></param>
        /// <returns></returns>
        public static Vector2f[] TransformVector2fPoint(Matrix m, params Vector2f[] vector2f)
        {
            if ((m==null) || (m.Elements ==null) || (vector2f == null) || (vector2f.Length == 0))
                return null;
            Vector2f[] t = new Vector2f[vector2f.Length];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = new Vector2f();
                t[i].X = vector2f[i].X * m.Elements[0] + vector2f[i].Y * m.Elements[4] + m.OffsetX;
                t[i].Y = vector2f[i].X * m.Elements[1] + vector2f[i].Y * m.Elements[5] + m.OffsetY;
            }
            return t;
        }
        /// <summary>
        /// transform with no offset x and offset y
        /// </summary>
        /// <param name="m"></param>
        /// <param name="vector2f"></param>
        /// <returns></returns>
        public static Vector2f[] TransformVector2fVector(Matrix m, params Vector2f[] vector2f)
        {
            if ((vector2f == null) || (vector2f.Length == 0))
                return null;
            Vector2f[] t = new Vector2f[vector2f.Length];
            for (int i = 0; i < t.Length; i++)
            {
                t[i] = new Vector2f();
                t[i].X = vector2f[i].X * m.Elements[0] + vector2f[i].Y * m.Elements[4] ;
                t[i].Y = vector2f[i].X * m.Elements[1] + vector2f[i].Y * m.Elements[5] ;
            }
            return t;
        }
        public static Rectangled GetBounds(Vector2d vector2d, double p, double p_3)
        {
            return new Rectangled(vector2d.X - p, vector2d.Y - p_3, 2.0 * p, 2.0 * p_3);
        }
        /// <summary>
        /// dock a graphics path to fill specified rectangle
        /// </summary>
        /// <param name="cvp"></param>
        /// <param name="Rectanglef"></param>
        public static Rectanglef DockPath(Rectanglef cvp, Rectanglef Rectanglef)
        {
            Rectanglef rc = cvp;
            Vector2f v_center = GetCenter(rc);
            Vector2f v_dt = GetDistanceP(Rectanglef.Location, rc.Location);
            Matrix m = new Matrix();
            float ex = Rectanglef.Width  / rc.Width;
            float ey = Rectanglef.Height  / rc.Height ;
            m.Translate(-v_center.X, -v_center.Y, enuMatrixOrder.Append);
            m.Scale(ex, ey, enuMatrixOrder.Append);
            m.Translate(v_center.X, v_center.Y, enuMatrixOrder.Append);
            m.Translate(v_dt.X, v_dt.Y, enuMatrixOrder.Append);
            cvp = Matrix.Tranform(m, cvp);
            return cvp;
        }
        /// <summary>
        /// dock a graphics path to fill specified rectangle
        /// </summary>
        /// <param name="cvp"></param>
        /// <param name="rectangleF"></param>
        public static void DockPath(ICoreGraphicsPath cvp, Rectanglef rectangleF)
        {

            Rectanglef rc = cvp.GetBounds();
            Vector2f v_center = GetCenter(rc);
            Vector2f v_dt = GetDistanceP(rectangleF.Location, rc.Location);
            Matrix m = new Matrix();
            float ex = rectangleF.Width / rc.Width;
            float ey = rectangleF.Height / rc.Height;

            m.Translate(-v_center.X, -v_center.Y, enuMatrixOrder.Append);
            m.Scale(ex, ey, enuMatrixOrder.Append);
            m.Translate(v_center.X, v_center.Y, enuMatrixOrder.Append);
            m.Translate(v_dt.X, v_dt.Y, enuMatrixOrder.Append);
            cvp.Transform(m);

        }
  
        /// <summary>
        /// 
        /// </summary>
        /// <param name="vector2f1"></param>
        /// <param name="vector2f2"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2f GetAngleVector2i(Vector2f refVector2i, Vector2f currentVector2i, int angle)
        {
            if (angle == 0)
                return Vector2f.Zero;
            float v_angle = CoreMathOperation.GetAngle(refVector2i , currentVector2i) * CoreMathOperation.ConvRdToDEGREE;
            v_angle = (float)(Math.Ceiling(v_angle / angle ) * angle);
            float v_x = 0.0f;
            float v_y = 0.0f;
            switch ((int)v_angle)
            {
                case 0:
                case 180:
                    v_x = currentVector2i.X;
                    v_y = refVector2i.Y;
                    break;
                case 90:
                case 270:
                    v_x = refVector2i.X;
                    v_y = currentVector2i.Y;
                    break;
                case 45:
                    {
                        Vector2f pt = CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        float d = Math.Min(pt.X, pt.Y);// CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        v_x = refVector2i.X + d;
                        v_y = refVector2i.Y + d;
                    }
                    break;
                case 135:
                    {
                        Vector2f pt = CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        float d = Math.Min(pt.X, pt.Y);// CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        v_x = refVector2i.X + d;
                        v_y = refVector2i.Y - d;
                    }
                    break;
                case 225:
                    {
                        Vector2f pt = CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        float d = Math.Min(pt.X, pt.Y);// CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        v_x = refVector2i.X + d;
                        v_y = refVector2i.Y + d;
                    }
                    break;
                case 315:
                case -45:
                    {
                        Vector2f pt = CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        float d = Math.Min(pt.X, pt.Y);// CoreMathOperation.GetDistanceP(currentVector2i, refVector2i);
                        v_x = refVector2i.X - d;
                        v_y = refVector2i.Y + d;
                    }
                    break;
                default:
                    break;
            }
            return new Vector2f(v_x, v_y);
        }
        //public static Vector2f[] GetPoints(Rectanglef rectanglef)
        //{
        //    return new Vector2f[]{
        //        rectanglef.Location ,
        //        new  Vector2f (rectanglef.X + rectanglef.Width, rectanglef.Y ),
        //        new  Vector2f (rectanglef.X + rectanglef.Width , rectanglef.Y + rectanglef.Height ),
        //        new  Vector2f (rectanglef.X , rectanglef.Y  + rectanglef.Height ),
        //    };
        //}
        /// <summary>
        /// get angle point
        /// </summary>
        /// <param name="vector2f1"></param>
        /// <param name="vector2f2"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2f GetAnglePoint(Vector2f refPoint, Vector2f currentPoint, float angle)
        {
            if (angle == 0)
                return Vector2f.Zero;

            Vector2f pt = CoreMathOperation.GetDistanceP(currentPoint, refPoint);
            float v_angle = CoreMathOperation.GetAngle(refPoint, currentPoint) * CoreMathOperation.ConvRdToDEGREE;
            v_angle = (float)(Math.Ceiling(v_angle / angle) * angle);
            float v_x = 0.0f;
            float v_y = 0.0f;
            float d = 0.0f;
            d = Math.Min(pt.X, pt.Y);// CoreMathOperation.GetDistanceP(currentPoint, refPoint);
            switch ((int)v_angle)
            {
                case 0:
                case 180:
                    v_x = currentPoint.X;
                    v_y = refPoint.Y;
                    break;
                case 90:
                case 270:
                    v_x = refPoint.X;
                    v_y = currentPoint.Y;
                    break;
                case 45:
                    {
                        v_x = refPoint.X + d;
                        v_y = refPoint.Y + d;
                    }
                    break;
                case 135:
                    {
                        v_x = refPoint.X + d;
                        v_y = refPoint.Y - d;
                    }
                    break;
                case 225:
                        v_x = refPoint.X + d;
                        v_y = refPoint.Y + d;                    
                    break;
                case 315:
                case -45:
                        v_x = refPoint.X - d;
                        v_y = refPoint.Y + d;                    
                    break;
            }
            return new Vector2f(v_x, v_y);
        }
        //get Points associate to rectangle
        public static Vector2f[] GetResizePoints(Rectanglef rectangleF)
        {
            return new Vector2f[]{
               /*0*/ rectangleF.Location ,
               /*1*/ new Vector2f (rectangleF.X + (rectangleF.Width /2.0f) , rectangleF.Y),
                new Vector2f (rectangleF.X + rectangleF.Width  , rectangleF.Y),
                new Vector2f (rectangleF.X + rectangleF.Width  , rectangleF.Y + (rectangleF.Height / 2.0f) ),
                new Vector2f (rectangleF.X + rectangleF.Width  , rectangleF.Y + rectangleF.Height ),
                new Vector2f (rectangleF.X + (rectangleF.Width/2.0f)  , rectangleF.Y + rectangleF.Height ),
                new Vector2f (rectangleF.X   , rectangleF.Y + rectangleF.Height ),
                new Vector2f (rectangleF.X   , (rectangleF.Y + rectangleF.Height/2.0f) )
            };
        }
        public static float[] MultMatrix(float[] m1, float[] m2, int dimx, int dimy)
        {
            float[] rtb = new float[dimx * dimy ];
            int k = 0;
            int offsetx = 0;
            int offsety = 0;
            float v_som = 0;
            for (k = 0; k < rtb.Length; )
            {
                for (int i = 0; i < dimx ; i++)
                {//columns
                    v_som = 0.0f;
                    for (int j = 0; j < dimy ; j++)
                    {
                        offsety = (4 * j) + i;//calculate column index
                        v_som += m1[offsetx + j] * m2[offsety];
                    }
                    rtb[k] = v_som;
                    k++;
                }
                offsetx += 4;
            }
            return rtb;
        }
        /// <summary>
        /// return the global bounds for element
        /// </summary>
        /// <param name="core2DDrawingLayeredElement"></param>
        /// <returns></returns>
        public static Rectanglef GetGlobalBounds(ICoreWorkingBoundResult[] elements)
        {
            if ((elements == null) || (elements.Length==0 ))
                return Rectanglef.Empty;

            float minx = 0.0f;
            float miny = 0.0f;
            float maxx = 0.0f;
            float maxy = 0.0f;
            Rectanglef t_rc = Rectanglef.Empty ;
            bool v_def = false;
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i]==null)
                    continue;
                t_rc = elements[i].GetBound();
                if (!v_def)
                {
                    minx =t_rc.X;
                    miny =t_rc.Y;
                    maxx =t_rc.Right;
                    maxy =t_rc.Bottom;
                    v_def = true;
                }
                else
                {
                    minx = Math.Min(minx, t_rc.X);
                    miny = Math.Min(miny, t_rc.Y);
                    maxx = Math.Max(maxx, t_rc.Right);
                    maxy = Math.Max(maxy, t_rc.Bottom);
                }
            }
            return new Rectanglef(
                minx,
                miny,
                maxx - minx,
                maxy - miny);
         
        }
        /// <summary>
        /// get bounds 
        /// </summary>
        /// <param name="center"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        public  static Rectanglef GetBounds(Vector2f center, Vector2f[] radius)
        {
            Vector2f rd = Vector2f.Zero;
            for (int i = 0; i < radius.Length; i++)
            {
                var v_rd = radius[i] - center;
                rd.X = Math.Max(v_rd.X, rd.X);
                rd.Y = Math.Max(v_rd.Y, rd.Y);
            }
            return GetBounds(center, rd.X, rd.Y);
        }
    }

  
}

