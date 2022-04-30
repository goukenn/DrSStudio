

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreGraphicsPathUtils.cs
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
file:WinCoreGraphicsPathUtils.cs
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;


namespace IGK.ICore.WinCore
{
    using IGK.ICore;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Drawing2D;
    using System.Runtime.InteropServices;
    using System.Reflection;/// <summary>
                            /// represent a graphics path utility class
                            /// </summary>
    public class WinCoreGraphicsPathUtils : ICoreD2DPathUtils, IDisposable
    {
        GraphicsPath m_paths;
        private WinCoreApplication m_app;
        internal WinCoreGraphicsPathUtils()
        {
            this.m_paths = new GraphicsPath();
        }

        public WinCoreGraphicsPathUtils(WinCoreApplication winCoreApplication) : this()
        {
            this.m_app = winCoreApplication;
        }
        public bool IsVisible(Vector2f[] points, byte[] tab, enuFillMode mode, Vector2f point)
        {
            if ((points == null) || (points.Length == 0))
                return false;
            m_paths.Reset();
            using (GraphicsPath v_p = new GraphicsPath(points.CoreConvertTo<Point[]>(), tab, (FillMode)mode))
            {
                if (v_p.PointCount == 0)
                    return false;
                m_paths.AddPath(v_p, false);
            }
            return this.m_paths.IsVisible(new PointF(point.X, point.Y));
        }
        public bool IsOutlineVisible(Vector2f[] points, byte[] tab,
            enuFillMode mode, Vector2f point, float penWidth)
        {
            if ((points == null) || (points.Length == 0))
                return false;
            m_paths.Reset();
            using (GraphicsPath v_p = new GraphicsPath(points.CoreConvertTo<Point[]>(), tab, (FillMode)mode))
            {
                if (v_p.PointCount == 0)
                    return false;
                m_paths.AddPath(v_p, false);
                Pen p = new Pen(Color.Black, penWidth);
                bool c = this.m_paths.IsOutlineVisible(point.X, point.Y, p);
                p.Dispose();
                return c;
            }
        }
        public bool IsOutlineVisible(ICoreGraphicsPath path, Vector2f point)
        {
            m_paths.Reset();
            using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
            {
                Pen p = WinCoreBrushRegister.GetPen(path);
                return v_p.IsOutlineVisible(point.X, point.Y, p ?? Pens.Black);
            }
        }
        public bool IsOutlineVisible(ICoreGraphicsPath path, Vector2f point, float penWidth)
        {
            m_paths.Reset();
            using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
            {
                Pen p = WinCoreBrushRegister.GetPen(path);
                return v_p.IsOutlineVisible(point.X, point.Y, p ?? Pens.Black);
            }
        }
        public void Dispose()
        {
            this.m_paths.Dispose();
        }
        public bool IsVisible(ICoreGraphicsPath path, Vector2f point)
        {
            using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
            {
                return v_p.IsVisible(point.X, point.Y);
            }
        }


        public ICore2DPathDefinition Widen(CoreGraphicsPath path, ICorePen pen,
            IGK.ICore.Matrix matrix, float flatness)
        {
            using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
            {
                Pen p = this.m_app.BrushRegister.GetPen<Pen>(pen);
                if (p != null)
                {
                    v_p.Widen(p, matrix.ToGdiMatrix(), flatness);
                }
                var v_o = new Core2DPathDefinition(
                    v_p.PathPoints.CoreConvertFrom<Vector2f[]>(),
                    v_p.PathTypes);
                return v_o;
            }
        }

        public ICore2DPathDefinition Flatten(CoreGraphicsPath path, ICorePen pen,
            IGK.ICore.Matrix matrix, float flatness)
        {
            lock (path)
            {
                using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
                {
                    Pen p = this.m_app.BrushRegister.GetPen<Pen>(pen);
                    if (p != null)
                    {
                        using (global::System.Drawing.Drawing2D.Matrix m = matrix.ToGdiMatrix())
                        {
                            v_p.Flatten(m, flatness);
                        }
                    }
                    var v_o = new Core2DPathDefinition(
                        v_p.PathPoints.CoreConvertFrom<Vector2f[]>(),
                        v_p.PathTypes);
                    return v_o;
                }
            }
        }

        public ICore2DPathDefinition Warp(CoreGraphicsPath path, Vector2f[] tab,
            Rectanglef bounds,
            IGK.ICore.Matrix matrix, enuWarpMode warpMode, float Flatness)
        {
            lock (path)
            {
                using (GraphicsPath v_p = path.WinCoreToGdiGraphicsPath())
                {

                    using (global::System.Drawing.Drawing2D.Matrix m = matrix.ToGdiMatrix())
                    {
                        v_p.Warp(tab.CoreConvertTo<PointF[]>(),
                            bounds.CoreConvertTo<RectangleF>(),
                        m,
                        (WarpMode)warpMode,
                        Flatness);
                    }

                    var v_o = new Core2DPathDefinition(
                        v_p.PathPoints.CoreConvertFrom<Vector2f[]>(),
                        v_p.PathTypes);
                    return v_o;
                }
            }
        }

        /// <summary>
        /// get proportional fetch matrix
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bound"></param>
        /// <returns></returns>
        public static IGK.ICore.Matrix ProportionalFitMatrix(ICoreGraphicsPath path, Rectanglef bound)
        {
            float zoomx;
            float zoomy;
            int x = 0;
            int y = 0;
            Rectanglef rcCurrent = path.GetBounds();
            //for saving
            zoomx = bound.Width / rcCurrent.Width;
            zoomy = bound.Height / rcCurrent.Height;
            zoomx = Math.Min(zoomx, zoomy);
            zoomy = zoomx;

            x = (int)Math.Round(((-rcCurrent.Width * zoomx) / 2.0f) + (bound.Width / 2.0f));
            y = (int)Math.Round(((-rcCurrent.Height * zoomx) / 2.0f) + (bound.Height / 2.0f));
            //w = (int)(rcCurrent.Width * zoomx);
            //h = (int)(rcCurrent.Height * zoomx);


            Matrix mat = new Matrix();
            mat.Translate(-rcCurrent.X, -rcCurrent.Y, enuMatrixOrder.Prepend  );
            mat.Scale(zoomx, zoomy, enuMatrixOrder.Append );
            mat.Translate(x, y, enuMatrixOrder.Append);
            return mat;
        }
            /// <summary>
            /// proportional fit the bound
            /// </summary>
            /// <param name="path"></param>
            /// <param name=""></param>
            /// <returns></returns>
            public static ICore2DPathDefinition ProportionalFit(ICoreGraphicsPath path, Rectanglef bound){

            var mat = ProportionalFitMatrix(path, bound);
            var c = CoreMathOperation.MultMatrixTransformVector(mat, path.PathPoints);

            var v_o = new Core2DPathDefinition(
                      c, path.PathTypes);
            return v_o;
        }
        public virtual ICoreDrawingPath CreateNewPath()
        {
            return new WinCoreGraphicsPath();
        }


        public ICore2DDrawingPathMeasurer Measurer
        {
            get { return null; }
        }


        public ICoreGraphicsPath Reverse(ICoreGraphicsPath coreGraphicsPath)
        {
            using (GraphicsPath v_p = coreGraphicsPath.WinCoreToGdiGraphicsPath())
            {
                v_p.Reverse();
                var v_o = new Core2DPathDefinition(
                      v_p.PathPoints.CoreConvertFrom<Vector2f[]>(),
                      v_p.PathTypes);
                var g =  new CoreGraphicsPath(){

                };
                g.AddDefinition(v_o.Points, v_o.Types );//AddSegment (
                return g;
            }
        }

        public void CreateNewRegion(CoreGraphicsPath cp, ICoreRegionBuildListener coreRegionData)
        {
            Region rg = new Region();
            coreRegionData.RegionBuildItem(new RegionDataList(rg));
           var rc =   rg.GetRegionScans(new global::System.Drawing.Drawing2D.Matrix());
            GraphicsPath v_cp = new GraphicsPath();
            v_cp.AddRectangles(rc);
            GetOutlinePath(v_cp, out byte[] t, out Vector2f[] vt);

            cp.AddDefinition(vt, t);
            v_cp.Dispose();
            rg.Dispose();
        }


        // Declaration required for interop
        [DllImport(@"gdiplus.dll")]
        public static extern int GdipWindingModeOutline(HandleRef path, IntPtr matrix, float flatness);

        public static bool GetOutlinePath(GraphicsPath path, out byte[] t , out Vector2f[] vt) {
            HandleRef handle = new HandleRef(path, (IntPtr)path.GetType().GetField("nativePath", BindingFlags.NonPublic | BindingFlags.Instance)
                .GetValue(path));
            // Change path so it only contains the outline
            GdipWindingModeOutline(handle, IntPtr.Zero, 0.25F);
            t = path.PathTypes;
            vt = path.PathPoints.CoreConvertFrom<Vector2f[]>();
            return true;
        }
        public static CoreGraphicsPath GetOutlinePath(CoreGraphicsPath path)
        {
            GraphicsPath c = path.WinCoreToGdiGraphicsPath();

            GetOutlinePath(c, out byte[] t, out Vector2f[] vt);

            c.Dispose();

            CoreGraphicsPath h = new CoreGraphicsPath();
            h.AddDefinition(vt, t);
            return h;
        }

        class RegionDataList : ICoreRegionBuildAction
        {
            private Region rg;

            public RegionDataList(Region rg)
            {
                this.rg = rg;
            }

            public void Complement(ICoreGraphicsPath data)
            {
                this._Generate(MethodBase.GetCurrentMethod().Name, data);
            }

            public void Exclude(ICoreGraphicsPath data)
            {
                this._Generate(MethodBase.GetCurrentMethod().Name, data);
            }

            public void Init(ICoreGraphicsPath data)
            {
                this.rg.MakeEmpty();
                this.Union(data);
                //this.rg.MakeInfinite();
                //this.Intersect(data);
            }

            public void Intersect(ICoreGraphicsPath data)
            {
                this._Generate(MethodBase.GetCurrentMethod().Name, data);
            }

            public void Union(ICoreGraphicsPath data)
            {
                this._Generate(MethodBase.GetCurrentMethod().Name, data);
            }

            public void Xor(ICoreGraphicsPath data)
            {
                this._Generate(MethodBase.GetCurrentMethod().Name, data);
            }

            private void _Generate(string v, ICoreGraphicsPath data)
            {
                var s = rg.GetType().GetMethod(v, new Type[]{
                    typeof (GraphicsPath)
                });
                if (s != null) { 
                using (var g = data.WinCoreToGdiGraphicsPath())
                {
                   s.Invoke(this.rg, new object[] { g });
                }
               }
            }
        }
    }
}

