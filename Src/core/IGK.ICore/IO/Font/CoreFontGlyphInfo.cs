using IGK.ICore;
using IGK.ICore.IO.Font.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IGK.ICore.IO.Font
{
    /// <summary>
    /// represent a font glyf information
    /// </summary>
    public sealed class CoreFontGlyphInfo
    {
        internal CoreFontGlyphPoint[] m_points;
        internal int[] m_contours;
        internal byte[][] m_flags;
        
        

        public CoreFontGlyphInfo()
        {

        }
        public override string ToString()
        {
            return $"CoreGlyfInfo[Countour:{NumberOfContour}]";
        }

        public short maxY { get; set; }
        public short minX { get; set; }
        public short minY { get; set; }
        public short maxX { get; set; }
        /// <summary>
        /// get the number of points in this glysh
        /// </summary>
        public int Count { get; set; }

        internal CoreFontGlyphPoint getPoint(int i)
        {
            return this.m_points[i];
        }

        public int NumberOfContour { get {
            if (this.m_contours !=null)
                return this.m_contours.Length;
            return 0;
        } }

        public int GetPointInContour(int index) {
            if ((index >= 0) && (index < this.m_contours.Length))
                return this.m_contours[index];
            return 0;
        }

        public int ExtractQuadraticPoint(out Vector2f[] pts, out byte[] types) {
            var c = this;
            //List<Vector2f> rpts = new List<Vector2f>();
            List<Vector2f> rpts2 = new List<Vector2f>();
            List<byte> rtypes = new List<byte>();
            int k = 0;
           // bool v_bezier = false;
            // int cb = 0;
            //Vector2f c1 = q0 + ((2 / 3.0f) * (q1 - q0));
            //Vector2f c2 = q2 + ((2 / 3.0f) * (q1 - q2));
            //QuadricBezier bezier = new QuadricBezier();
            //int startx = 0;
            //int starty = 0;
            int startindex = 0;
            //CoreFontGlyphPoint oldpts = new CoreFontGlyphPoint();
            int v_startindex = 0;
            bool on_curve = false;
            //bool v_startinbezier = false;
            for (int i = 0; i < c.NumberOfContour; i++)
            {
                var ln = c.GetPointInContour(i);
                
                v_startindex += ln;
                if (i > 0) {
                    //close the figure 
                    //rpts2.Add(rpts2[startindex]);
                    //rtypes.Add(rtypes[rtypes.Count - 1]);
                    rtypes[rtypes.Count - 1] |= 0x80;
                }
                startindex = rpts2.Count;
                //v_startinbezier = false;
                for (int j = 0; j < ln; j++, k++)
                {
                    var t = c.getPoint(k);//get current point
                    var flag = this.m_flags[i][j];
                    on_curve = ((flag & ttf_native.MASK_ON_CURVE) == ttf_native.MASK_ON_CURVE);
                    rpts2.Add(new Vector2f(t.X, t.Y));
                    //if (j == 0)
                    //{
                    //    rtypes.Add(0);
                    //}
                    //else
                    //{
                        if (on_curve)
                        {
                            rtypes.Add(1);
                        }
                        else
                            rtypes.Add(3);
                    //}
                    //if (j == 0)
                    //{
                    //    startx = t.X;
                    //    starty = t.Y;
                    //    if (!on_curve)
                    //    {
                    //        var kk = c.getPoint(v_startindex - 1);//get previous point
                    //        bezier.q0 = new Vector2f(kk.X, kk.Y);
                    //        bezier.q1 = new Vector2f(t.X, t.Y);
                    //        bezier.haspoint = true;
                    //        v_bezier = true;
                    //        v_startinbezier = true;
                    //    }
                    //    //add start point
                    //    rtypes.Add(0);
                    //    rpts.Add(new Vector2f(t.X, t.Y));

                    //}
                    //else
                    //{
                    //    if (on_curve)
                    //    {
                    //        if (v_bezier)
                    //        {
                    //            //end bezier point
                    //            bezier.q2 = new Vector2f(t.X, t.Y);
                    //            bezier.ToGdiPath(rtypes, rpts);
                    //            bezier = QuadricBezier.empty;
                    //            v_bezier = false;
                    //        }
                    //        else
                    //        {
                    //            //add line point
                    //            rtypes.Add(1);
                    //            rpts.Add(new Vector2f(t.X, t.Y));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //append bezier curve
                    //        if (bezier.haspoint)
                    //        {
                    //            var tt = new Vector2f(t.X, t.Y);
                    //            bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2.0f;
                    //            bezier.ToGdiPath(rtypes, rpts);
                    //            var tmp = new QuadricBezier();
                    //            tmp.q0 = bezier.q2;// -((bezier.q2 - bezier.q1) / 2.0f);
                    //            tmp.q1 = tt;
                    //            tmp.haspoint = true;
                    //            bezier = tmp;
                    //        }
                    //        else
                    //        {
                    //            bezier.q0 = new Vector2f(oldpts.X, oldpts.Y);
                    //            bezier.q1 = new Vector2f(t.X, t.Y);
                    //            bezier.haspoint = true;
                    //        }
                    //        v_bezier = true;
                    //    }
                    //}
                    //oldpts = t;
                }
                //if (v_bezier) //closed path
                //{
                //    //adjust the graphics
                //    var tt = new Vector2f(startx, starty);


                //    if (v_startinbezier)
                //    {
                //        bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2;
                //        bezier.ToGdiPath(rtypes, rpts);
                //        //close the path with a curve to start point
                //        var tmp = new QuadricBezier();
                //        tmp.q0 = bezier.q2;
                //        tmp.q1 = new Vector2f(rpts[startindex].X, rpts[startindex].Y);
                //        tmp.q2 = new Vector2f(rpts[startindex + 3].X, rpts[startindex + 3].Y);
                //        tmp.ToGdiPath(rtypes, rpts);

                //        //remove ranage
                //        rtypes.RemoveRange(startindex, 3);
                //        rpts.RemoveRange(startindex, 3);
                //        //mark the start 
                //        rtypes[startindex] = 0;
                //        ////implied last
                //        //close figure
                //        //rtypes[rtypes.Count - 1] = (byte)(rtypes[rtypes.Count - 1] | 0x80);                        
                //    }
                //    else
                //    {
                //        bezier.q2 = tt;// bezier.q1 + (tt - bezier.q1) / 2;
                //        bezier.ToGdiPath(rtypes, rpts);

                //    }
                //    bezier = QuadricBezier.empty;
                //    v_bezier = false;
                //}
                ////close figure
                //rtypes[rtypes.Count - 1] = (byte)(rtypes[rtypes.Count - 1] | 0x80);
            }
            pts = rpts2.ToArray();
            types = rtypes.ToArray();
            return rtypes.Count;
        }
        public  int ExtractGlyphGraphics(out Vector2f[] pts, out byte[] types)
        {
            var c = this;
            List<Vector2f> rpts = new List<Vector2f>();
            //List<Vector2f> rpts2 = new List<Vector2f>();
            List<byte> rtypes = new List<byte>();
            int k = 0;
            bool v_bezier = false;
           // int cb = 0;
            //Vector2f c1 = q0 + ((2 / 3.0f) * (q1 - q0));
            //Vector2f c2 = q2 + ((2 / 3.0f) * (q1 - q2));
            QuadricBezier bezier = new QuadricBezier();
            int startx = 0;
            int starty = 0;
            int startindex = 0;
            CoreFontGlyphPoint oldpts = new CoreFontGlyphPoint ();
            int v_startindex = 0;
            bool v_startinbezier = false;
            for (int i = 0; i < c.NumberOfContour ; i++)
            {
                var ln =  c.GetPointInContour(i);
                startindex = rpts.Count;
                v_startindex += ln;
                v_startinbezier = false;
                for (int j = 0; j < ln; j++, k++)
                {
                    var t = c.getPoint(k);//get current point
                    bool on_curve = ((this.m_flags[i][j] & ttf_native.MASK_ON_CURVE) == ttf_native.MASK_ON_CURVE);
                    //rpts2.Add(new Vector2f(t.X,t.Y));
                    if (j == 0)
                    {
                        startx = t.X;
                        starty = t.Y;
                        if (!on_curve)
                        {
                            var kk= c.getPoint(v_startindex - 1);//get previous point
                            bezier.q0 = new Vector2f(kk.X, kk.Y);
                            bezier.q1 = new Vector2f(t.X, t.Y);
                            bezier.haspoint = true;
                            v_bezier = true;
                            v_startinbezier = true;
                        }
                        //add start point
                        rtypes.Add(0);
                        rpts.Add(new Vector2f(t.X, t.Y));
                        
                    }
                    else
                    {
                        if (on_curve){
                            if (v_bezier){
                                //end bezier point
                                bezier.q2 = new Vector2f ( t.X , t.Y);                                
                                bezier.ToGdiPath(rtypes, rpts);                                
                                bezier = QuadricBezier.empty;
                                v_bezier = false;
                            }
                            else{
                                //add line point
                                rtypes.Add(1);
                                rpts.Add(new Vector2f(t.X, t.Y));
                            }
                        }
                        else {
                            //append bezier curve
                            if (bezier.haspoint)
                            {
                                var tt = new Vector2f(t.X, t.Y);
                                bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2.0f;
                                bezier.ToGdiPath(rtypes, rpts);
                                var tmp = new QuadricBezier();
                                tmp.q0 = bezier.q2;// -((bezier.q2 - bezier.q1) / 2.0f);
                                tmp.q1 = tt;
                                tmp.haspoint = true;
                                bezier = tmp;
                            }
                            else {
                                bezier.q0 = new Vector2f ( oldpts.X , oldpts.Y);
                                bezier.q1 = new Vector2f(t.X, t.Y);
                                bezier.haspoint = true;
                            }
                            v_bezier = true;                  
                        }
                    }
                    oldpts = t;
                }
                if (v_bezier) //closed path
                {
                    //adjust the graphics
                    var tt = new Vector2f(startx, starty);


                    if (v_startinbezier)
                    {
                        bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2;
                        bezier.ToGdiPath(rtypes, rpts);
                        //close the path with a curve to start point
                        var tmp = new QuadricBezier();
                        tmp.q0 = bezier.q2;
                        tmp.q1 = new Vector2f(rpts[startindex].X, rpts[startindex].Y);
                        tmp.q2 = new Vector2f(rpts[startindex + 3].X, rpts[startindex + 3].Y);
                        tmp.ToGdiPath(rtypes, rpts);

                        //remove range
                        rtypes.RemoveRange(startindex, 3);
                        rpts.RemoveRange(startindex, 3);
                        //mark the start 
                        rtypes[startindex] = 0;
                        ////implied last
                        //close figure
                        //rtypes[rtypes.Count - 1] = (byte)(rtypes[rtypes.Count - 1] | 0x80);                        
                    }
                    else {
                        bezier.q2 = tt;// bezier.q1 + (tt - bezier.q1) / 2;
                        bezier.ToGdiPath(rtypes, rpts);
                
                    }
                    bezier = QuadricBezier.empty;
                    v_bezier = false;
                }
                //close figure
                rtypes[rtypes.Count - 1] = (byte)(rtypes[rtypes.Count - 1] | 0x80); 
            }
            pts = rpts.ToArray();
            types = rtypes.ToArray();
            return rtypes.Count;
        }

        public int Width { get { return this.maxX - this.minX; } }

        public int Height { get { return this.maxY - this.minY; } }

        [StructLayout(LayoutKind.Sequential)]
        public struct QuadricBezier {

            public static readonly QuadricBezier empty;
            internal bool haspoint;
            internal Vector2f q0;
            internal Vector2f q1;
            internal Vector2f q2;
            static QuadricBezier() {
                empty = new QuadricBezier();
                empty.haspoint = false;
            }
            /// <summary>
            /// add tpoint to dgi+
            /// </summary>
            /// <param name="rtypes"></param>
            /// <param name="rpts"></param>
            internal void ToGdiPath(List<byte> rtypes, List<Vector2f> rpts)
            {
                //convert to  gdi  points
                rtypes.Add(3);
                rtypes.Add(3);
                rtypes.Add(3);

                Vector2f c1 = q0 + ((2 / 3.0f) * (q1 - q0));
                Vector2f c2 = q2 + ((2 / 3.0f) * (q1 - q2));

                rpts.Add(new Vector2f(c1.X, c1.Y));
                rpts.Add(new Vector2f(c2.X, c2.Y));
                rpts.Add(new Vector2f(q2.X, q2.Y));
            }
        }

        public CoreFontGlyphPoint [] Points()
        {
            return this.m_points;
        }
    }
}
