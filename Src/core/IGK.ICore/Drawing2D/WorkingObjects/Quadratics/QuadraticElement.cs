using IGK.ICore.Codec;
using IGK.ICore.IO.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static IGK.ICore.IO.Font.CoreFontGlyphInfo;
using IGK.ICore.WinUI;
using IGK.ICore.ComponentModel;
using System.IO;


#pragma warning disable IDE0017 
#pragma warning disable IDE0018 

namespace IGK.ICore.Drawing2D.WorkingObjects.Standard
{

    [Core2DDrawingStandardElement("Quadratic", typeof(Mecanism), IsVisible = true,
        Keys = enuKeys.Q)]
    public class QuadraticElement :     
        Core2DDrawingDualBrushElement,
        ICore2DFillModeElement,
        ICore2DClosableElement,
        ICorePathStringDefinition ,
        ICoreQuadraticPath,
        ICore2DDrawingInvetibleElement
    {
        protected Vector2f[] m_Points;
        protected byte[] m_PointTypes;
        private enuFillMode m_FillMode;
        private bool m_Closed;
        private int m_numberOfCountour;

        public int NumberOfCountour {
            get {
                return this.m_numberOfCountour;
            }
        }
        [CoreXMLDefaultAttributeValue(false)]
        [CoreXMLAttribute()]
        public bool Closed
        {
            get { return m_Closed; }
            set
            {
                if (m_Closed != value)
                {
                    m_Closed = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLDefaultAttributeValue(enuFillMode.Alternate)]
        public enuFillMode FillMode
        {
            get { return m_FillMode; }
            set
            {
                if (m_FillMode != value)
                {
                    m_FillMode = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        [CoreXMLElement()]
        public byte[] PointTypes
        {
            get { return m_PointTypes; }
            set
            {
                if (m_PointTypes != value)
                {
                    m_PointTypes = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }
        [CoreXMLElement()]
        public Vector2f[] Points
        {
            get { return m_Points; }
            set
            {
                if (m_Points != value)
                {
                    m_Points = value;
                    OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs.Definition);
                }
            }
        }

        /// <summary>
        /// create element from gdi definition
        /// </summary>
        /// <param name="vector2f"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static QuadraticElement CreateFromGdiDefinition(Vector2f[] points, byte[] types)
        {
            QuadraticElement v_o = null;
            if ((points != null) && (types != null) && (points.Length == types.Length)) {

                CurveToQuadrics(ref points, ref types);

                v_o = new QuadraticElement()
                {
                    m_Points = points,
                    m_PointTypes = types
                };
                v_o.InitElement();
            }

            return v_o;
        }

        public QuadraticElement():base(){

        }
        protected override void InitializeElement()
        {
            base.InitializeElement();
            this.m_FillMode = enuFillMode.Winding;
            this.m_Closed = true;
        }
        protected override CoreGraphicsPath CreateGraphicsPath()
        {
            return base.CreateGraphicsPath();
        }
        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            path.Reset();
            if ((m_Points == null) || (m_Points.Length ==0))
                return;
            //path.AddDefinition(this.Points, this.PointTypes);
            //return;

            CoreFontGlyphInfo.QuadricBezier bezier = new CoreFontGlyphInfo.QuadricBezier();
            CoreFontGlyphInfo.QuadricBezier v_sbezier = new CoreFontGlyphInfo.QuadricBezier();//start bezier
            m_numberOfCountour = 1;
            List<Vector2f> v_cf = new List<Vector2f>();
            List<byte> v_tf = new List<byte>();
           
            bool v_bezier = false;
            Vector2f v_oldpts = Vector2f.Zero;
            Vector2f v_curveStart = m_Points[0];
            float startx=0;
            float starty=0;
            List<Vector2f> rpts = new List<Vector2f>();
            List<byte> rtypes = new List<byte>();
            Vector2f oldpts = Vector2f.Zero;
            bool v_startinbezier = false;
            int startindex = 0;
            bool v_start = true;
            int curve = 0;
            //convert to quadric bezier
            for (int i = 0; i < m_Points.Length; i++)
            {
                var t = m_Points[i];// c.getPoint(k);//get current point
                bool on_curve = (m_PointTypes[i] & 0x3) != 0x3;// ((this.m_flags[i][j] & ttf_native.MASK_ON_CURVE) == ttf_native.MASK_ON_CURVE);
                bool on_close = (m_PointTypes[i] & 0x80) == 0x80;// ((this.m_flags[i][j] & ttf_native.MASK_ON_CURVE) == ttf_native.MASK_ON_CURVE);


                bool on_start = m_PointTypes[i] == 0;

                if (on_start) {
                    if (i>= 0)
                    {
                        v_start = true;
                        //if (v_bezier)
                        //{
                        //}
                        ////add start
                        //v_start = false;
                        //rtypes.Add(0);
                        //rpts.Add(t);
                        //continue;
                    }
                }


                if (v_start)
                {
                    startx = t.X;
                    starty = t.Y;
                    if (!on_curve)
                    {
                        int close_index = i+1;
                        for (; close_index < m_PointTypes.Length; close_index++)
                        {
                            if ((m_PointTypes[close_index] & 0x80)==0x80){
                                break;
                             }
                        }
                        var kk = m_Points[close_index - 1];//get previous point
                        bezier.q0 = new Vector2f(kk.X, kk.Y);
                        bezier.q1 = new Vector2f(t.X, t.Y);
                        bezier.haspoint = true;
                        v_bezier = true;
                        v_startinbezier = true;
                        v_sbezier = bezier;
                    }
                    //add start point
                    rtypes.Add(0);
                    rpts.Add(t);
                    v_start = false;
                }
                else
                {
                    if (on_curve)
                    {
                        if (v_bezier)
                        {
                            //end bezier point
                            bezier.q2 = new Vector2f(t.X, t.Y);
                            bezier.ToGdiPath(rtypes, rpts);
                            bezier = QuadricBezier.empty;
                            v_bezier = false;
                        }
                        else
                        {
                            //add line point
                            rtypes.Add(1);
                            rpts.Add(new Vector2f(t.X, t.Y));
                        }
                    }
                    else
                    {
                        //
                        //append bezier curve
                        //
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
                        else
                        {//new bezier point
                            bezier.q0 = new Vector2f(oldpts.X, oldpts.Y);
                            bezier.q1 = new Vector2f(t.X, t.Y);
                            bezier.haspoint = true;
                        }
                        v_bezier = true;
                    }
                }
                oldpts = t;
                if (on_close || ((i+1)>= m_Points.Length))
                {
                 
                    if (v_bezier) //closed path
                    {
                        //adjust the graphics
                        var tt = new Vector2f(startx, starty);                        
                        if (v_startinbezier)
                        {
                            bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2;
                            bezier.ToGdiPath(rtypes, rpts);
                            //close the path with a curve to start point
// Simplify object initialization
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
                        else
                        {
                            bezier.q2 =  bezier.q1 + (tt - bezier.q1) / 2;//tt
                            bezier.ToGdiPath(rtypes, rpts);
                        }
                        bezier = QuadricBezier.empty;
                        v_bezier = false;
                    }
                    //close figure
                    rtypes[rtypes.Count - 1] = (byte)(rtypes[rtypes.Count - 1] | 0x80);
                    v_startinbezier = false;                    
                    v_start = true;
                    curve++;
                    startindex = rpts.Count;
                    //if (curve > 1)
                     // break;
                }
            }

          
            //}
            //    v_flag = m_PointTypes[i];
            //    var t = m_Points[i];
            //    if ((i > 0) && (v_flag == 0))
            //    {
            //        v_curveStart = t;
            //        m_numberOfCountour++;
            //    }


            //    if ((i > 0) && ((v_flag & 0x80) == 0x80)) {
            //        //close the figure
            //        //oncurve
            //        if (v_bezier)
            //        {
            //            bezier.q2 = t;
            //            bezier.ToGdiPath(v_tf, v_cf);
            //            bezier = QuadricBezier.empty;
            //            v_bezier = false;
            //            //bezier.q0 = v_curveStart;
            //            //bezier.q1 = v_curveStart;
            //            //bezier.q2 = v_curveStart;

            //            //bezier.ToGdiPath(v_tf, v_cf);
            //            //  v_tf[v_tf.Count - 1] |= 0x80;
            //            break;
            //        }
            //        else
            //        {
            //            //close the curve with the start point
            //            if ((v_flag & 0x3) == 0x3)
            //            {
            //                var bz = new QuadricBezier();
            //                bz.q0 = v_oldpts;
            //                bz.q1 = t;
            //                bz.q2 = v_curveStart;
            //                bz.ToGdiPath(v_tf, v_cf);
            //            }
            //            else
            //            {
            //                v_tf.Add(1);
            //                v_cf.Add(t);
            //            }


            //            v_tf[v_tf.Count - 1] |= 0x80;




            //            //v_tf.Add(v_flag);
            //            //v_cf.Add(new Vector2f(t.X, t.Y));
            //        }
            //        continue;
            //    }

            //    if ((v_flag & 0x3) == 0x3)
            //    {
            //        //not on curve
            //        if (i == 0)
            //        {//event 
            //            //v_tf.Add(0);
            //            //v_cf.Add(new Vector2f(t.X, t.Y));

            //            bezier = new QuadricBezier();
            //            bezier.q0 = new Vector2f(v_oldpts.X, v_oldpts.Y);
            //            bezier.q1 = new Vector2f(t.X, t.Y);
            //            bezier.haspoint = true;
            //            v_bezier = true;
            //        }
            //        else {
            //            if (v_bezier)
            //            {

            //                var tt = new Vector2f(t.X, t.Y);
            //                bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2.0f;
            //                bezier.ToGdiPath(v_tf, v_cf);
            //                var tmp = new QuadricBezier();
            //                tmp.q0 = bezier.q2;// -((bezier.q2 - bezier.q1) / 2.0f);
            //                tmp.q1 = tt;
            //                tmp.haspoint = true;
            //                bezier = tmp;


            //                //bezier.q2 = new Vector2f(t.X, t.Y);
            //                //bezier.ToGdiPath(v_tf, v_cf);
            //                //bezier = QuadricBezier.empty;
            //                //v_bezier = false;
            //            }
            //            else {
            //                bezier = new QuadricBezier();
            //                bezier.q0 = new Vector2f(v_oldpts.X, v_oldpts.Y);
            //                bezier.q1 = new Vector2f(t.X, t.Y);
            //                bezier.haspoint = true;
            //            }
            //            v_bezier = true;
            //        }
            //        //if (bezier.haspoint)
            //        //{
            //        //    var tt = new Vector2f(t.X, t.Y);
            //        //    bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2.0f;
            //        //    bezier.ToGdiPath(v_tf, v_cf);
            //        //    var tmp = new QuadricBezier();
            //        //    tmp.q0 = bezier.q2;// -((bezier.q2 - bezier.q1) / 2.0f);
            //        //    tmp.q1 = tt;
            //        //    tmp.haspoint = true;
            //        //    bezier = tmp;
            //        //}
            //        //else
            //        //{
            //        //    bezier.q0 = new Vector2f(v_oldpts.X, v_oldpts.Y);
            //        //    bezier.q1 = new Vector2f(t.X, t.Y);
            //        //    bezier.haspoint = true;
            //        //}
            //        //v_bezier = true;
            //    }
            //    else
            //    {
            //        //oncurve
            //        if (v_bezier)
            //        {
            //            bezier.q2 = new Vector2f(t.X, t.Y);
            //            bezier.ToGdiPath(v_tf, v_cf);
            //            bezier = QuadricBezier.empty;
            //            v_bezier = false;
            //        }
            //        else {
            //            v_tf.Add(v_flag);
            //            v_cf.Add(new Vector2f(t.X, t.Y));
            //        }

            //    }
            //    v_oldpts = t;
            //    //if (i == 0)
            //    //{
            //    //    startx = t.X;
            //    //    starty = t.Y;
            //    //    if (!on_curve)
            //    //    {
            //    //        var kk = c.getPoint(v_startindex - 1);//get previous point
            //    //        bezier.q0 = new Vector2f(kk.X, kk.Y);
            //    //        bezier.q1 = new Vector2f(t.X, t.Y);
            //    //        bezier.haspoint = true;
            //    //        v_bezier = true;
            //    //        v_startinbezier = true;
            //    //    }
            //    //    //add start point
            //    //    rtypes.Add(0);
            //    //    rpts.Add(new Vector2f(t.X, t.Y));

            //    //}
            //    //else
            //    //{
            //    //    if (on_curve)
            //    //    {
            //    //        if (v_bezier)
            //    //        {
            //    //            //end bezier point
            //    //            bezier.q2 = new Vector2f(t.X, t.Y);
            //    //            bezier.ToGdiPath(rtypes, rpts);
            //    //            bezier = QuadricBezier.empty;
            //    //            v_bezier = false;
            //    //        }
            //    //        else
            //    //        {
            //    //            //add line point
            //    //            rtypes.Add(1);
            //    //            rpts.Add(new Vector2f(t.X, t.Y));
            //    //        }
            //    //    }
            //    //    else
            //    //    {
            //    //        //append bezier curve
            //    //        if (bezier.haspoint)
            //    //        {
            //    //            var tt = new Vector2f(t.X, t.Y);
            //    //            bezier.q2 = bezier.q1 + (tt - bezier.q1) / 2.0f;
            //    //            bezier.ToGdiPath(rtypes, rpts);
            //    //            var tmp = new QuadricBezier();
            //    //            tmp.q0 = bezier.q2;// -((bezier.q2 - bezier.q1) / 2.0f);
            //    //            tmp.q1 = tt;
            //    //            tmp.haspoint = true;
            //    //            bezier = tmp;
            //    //        }
            //    //        else
            //    //        {
            //    //            bezier.q0 = new Vector2f(oldpts.X, oldpts.Y);
            //    //            bezier.q1 = new Vector2f(t.X, t.Y);
            //    //            bezier.haspoint = true;
            //    //        }
            //    //        v_bezier = true;
            //    //    }
            //    //}
            //    //oldpts = t;


            //    //if ((v_flag & 0x3) == 0x3)
            //    //{

            //    //}
            //    //else {

            //    //}

            //}
            //if (!Closed ) {
            //    if (bezier.haspoint)
            //    {

            //        bezier.q2 = v_curveStart;// v_cf[0];
            //        bezier.ToGdiPath(v_tf, v_cf);
            //        bezier = QuadricBezier.empty;
            //        //v_cf.Add(v_cf[0]);
            //        //v_tf.Add(0x81);

            //    }
            //    //else {
            //    //    //v_cf.Add(v_cf[0]);
            //    //    v_tf[v_tf.Count-1] |= 0x80;
            //    //}
            //    v_tf[v_tf.Count - 1] |= 0x80;
            //}

            if (rtypes.Count > 0)
            {
                if (Closed)
                rtypes[rtypes.Count - 1] |= 0x80; 
                path.AddDefinition(rpts.ToArray(), rtypes.ToArray());
            }
            path.FillMode = this.FillMode;
        }
        protected override void BuildBeforeResetTransform()
        {
            var m = this.GetMatrix();
            if (!m.IsIdentity) {
                this.m_Points =  CoreMathOperation.TransformVector2fPoint(m, m_Points);                
            }
            base.BuildBeforeResetTransform();
        }

        public static QuadraticElement CreateElement(Vector2f[] points, byte[] types) {

            if ((points?.Length > 0) && (types?.Length == points.Length)) {
                QuadraticElement p = new QuadraticElement();
                p.m_Points = points;
                p.m_PointTypes = types;
                p.InitElement();
                return p;
            }
            
            return null;
        }

        public void Invert()
        {
            CoreGraphicsPath p = new CoreGraphicsPath();
            p.AddDefinition(this.m_Points, this.m_PointTypes);
            p.Invert();
            this.m_Points = p.PathPoints;
            this.m_PointTypes = p.PathTypes;
            this.InitElement();
            this.OnPropertyChanged(Core2DDrawingElementPropertyChangeEventArgs.Definition);
        }

        public static void CurveToQuadrics(ref Vector2f[] pts, ref Byte[] t)
        {

            string g = new CoreVector2fArrayTypeConverter().ConvertToString(pts);
            string mm = CoreTypeDescriptor.GetConverter(t).ConvertToString(t);
            //string m = new CoreVector2fArrayTypeConverter().ConvertToString(t);

            //File.WriteAllText(CoreConstant.DEBUG_TEMP_FOLDER+"\\outdata.dat", g + "\n");
            //File.AppendAllText(CoreConstant.DEBUG_TEMP_FOLDER+"\\outdata.dat", mm);

            Vector2f c0, c1, c2, c3;
            Vector2f q0, q1, q2;
            List<Vector2f> v_optst = new List<Vector2f>();
            List<byte> v_ot = new List<byte>();

            int ln = pts.Length;
            bool quadric = false;
            int jackv = 0;
            for (int i = 0; i < ln; i++)
            {
                if (t[i] == 0)
                {
                    if (quadric)
                    {
                        v_optst.Add(pts[i - 1]);
                        v_ot.Add(0x01);
                    }

                    v_optst.Add(pts[i]);
                    v_ot.Add(0);
                    quadric = false;
                    continue;
                }

                if (((t[i] & 0x3) == 0x3) && ((i + 2) < pts.Length))
                {
                    if (quadric)
                    {
                        //    v_ot[v_ot.Count - 1] = 0x03;
                        //jackv--;
                        ////remove quadric object
                        //v_optst.RemoveAt(v_optst.Count - 1);
                        //v_ot.RemoveAt(v_ot.Count - 1);
                        //v_ot[v_optst.Count - 1] = 0x3;

                    }
                    c0 = pts[i - 1];
                    c1 = pts[i];
                    c2 = pts[i + 1];
                    c3 = pts[i + 2];
                    q0 = c0;
                    q1 = (3 / 4.0f) * ((c1 + c2) - (c0 / 3.0f) - (c3 / 3.0f));
                    //q1 = (3 / 2.0f) * (c1 + ((-1 / 3.0f) * c0));

                    //q1 = c0;
                    q2 = c3;


                    v_optst.Add(q1);
                    v_ot.Add(0x3);
                    //v_optst.Add(q2);
                    //v_ot.Add(0x1);


                    if (((t[i + 2] & 0x80) == 0x80) || (i + 2 >= ln))
                    {
                        Console.WriteLine("last point is on line");
                        //v_optst.Add(q2);
                        //v_ot.Add(0x3);// | 0x80);
                        jackv++;
                    }
                    else
                    {
                        v_optst.Add(q2);
                        v_ot.Add((byte)0x01);

                        jackv += 2;
                        //v_optst.Add(q2);
                        //v_ot.Add((byte)(0x03));// | (((t[i + 2] & 0x80) == 0x80) ? 0x80 : 0)));

                    }


                    //q2 = c3;
                    //q1 = (3 / 2.0f) * (c2 - ((1 / 3.0f) * c3));
                    //q0 = 3 * (c1 - (c2 - ((1 / 3.0f) * c3)));


                    //pts[i] = q0;
                    //pts[i + 1] = q1;
                    //pts[i + 2] = q2;

                    //t[i] = 1;// (byte)(t[i + 2] & ~2);
                    //t[i + 1] = 1;// (byte)(t[i + 2] & ~2);
                    //t[i + 2] = 1;// (byte)(t[i + 2] & ~2);
                    i += 2;
                    quadric = true;
                }
                else
                {
                    v_optst.Add(pts[i]);
                    v_ot.Add(t[i]);
                    quadric = false;
                    jackv++;
                }
            }

            pts = v_optst.ToArray();
            t = v_ot.ToArray();

        }

        public void SetDefinition(CoreGraphicsPath path)
        {
            if (path == null) return;

            Vector2f[] c = null;
            byte[] t = null;
            path.GetAllDefinition(out c, out t);
            this.m_Points = c;
            this.m_PointTypes = t;
            this.InitElement();
        }

        public new sealed class Mecanism : IGK.ICore.Drawing2D.Mecanism.Core2DDrawingSurfaceMecanismBase<QuadraticElement> {

            const byte LINE = 1;
            const byte CONTROL = 2;
            byte C_MODE = LINE;
            private int m_index;

            protected override QuadraticElement CreateNewElement()
            {
                QuadraticElement l = QuadraticElement.CreateElement(
                new Vector2f[] {
                    Vector2f.Zero,
                    Vector2f.Zero,
                    Vector2f.Zero
                }, new byte[] {
                    0,3,1
                });
                return l;
            }
            protected override void InitNewCreatedElement(QuadraticElement element, Vector2f location)
            {
                base.InitNewCreatedElement(element, location);
                element.m_Points[0] = location;
                element.m_Points[1] = location;
                element.m_Points[2] = location;
                m_index = 2;

            }
            protected internal override void GenerateSnippets()
            {
                base.GenerateSnippets();
                var p = this.Element;
                if (p == null)
                {
                    return;
                }
                Vector2f[] t = p.Points;
                byte[] v_types = p.PointTypes;
                if (t != null)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        var e = this.CurrentSurface.CreateSnippet(this, 0, i);
                        this.AddSnippet(e);
                        if ((v_types[i] == 0)|| (i==0))
                        {
                            e.Shape = ICore.WinUI.enuSnippetShape.Diadmond;
                        }
                        else
                        {
                            if ((v_types[i] & 0x3) == 0x3)
                            {
                                e.Shape = ICore.WinUI.enuSnippetShape.Circle;
                            }

                            if ((v_types[i] & 0x80) == 0x80) {
                                e.Color = Colorf.Red;
                            }
                        }

                    }
                }
            }

            protected internal override void InitSnippetsLocation()
            {
                if (this.Element == null) return;
                Vector2f[] t = this.Element.Points;
                var s = this.RegSnippets;

                if (s.Count >= t.Length)
                {
                    for (int i = 0; i < t.Length; i++)
                    {
                        this.RegSnippets[i].Location = CurrentSurface.GetScreenLocation(t[i]);
                    }
                }
            }

            protected override void UpdateDrawing(CoreMouseEventArgs e)
            {
                this.EndPoint = e.FactorPoint;
                QuadraticElement v_element = this.Element;
                if (v_element == null)
                    return;
                switch (this.State)
                {
                    case ST_CREATING:
                        var mid = (this.EndPoint + v_element.m_Points[0]) / 2.0f;

                        v_element.m_Points[1] = mid;
                        v_element.m_Points[2] = e.FactorPoint;
                        v_element.InitElement();
                        break;
                    case ST_CREATING + 0x100:
                        this.UpdateIndex(e.FactorPoint, m_index);
                        if (this.Snippet != null)
                            this.Snippet.Location = e.Location;
                        break;
                }
                Invalidate();
            }

            private void UpdateIndex(Vector2f factorPoint, int m_index)
            {
                QuadraticElement v_element = this.Element;
                if ((m_index >= 0) && (v_element.m_Points.Length > m_index))
                {
                    v_element.m_Points[m_index] = factorPoint;
                }
                v_element.InitElement();
            }

            protected override void UpdateSnippetEdit(ICore.WinUI.CoreMouseEventArgs e)
            {
                if (this.Snippet == null)
                    return;
                QuadraticElement v_element = this.Element;
                switch (this.State)
                {
                    case ST_CREATING + 10:
                        byte[] s = this.Element.m_PointTypes;
                        Vector2f[] t = this.Element.m_Points;
                        List<Vector2f> tpts = new List<Vector2f>();
                        tpts.AddRange(t);
                        tpts.Insert(this.Snippet.Index, e.FactorPoint);
                        List<Byte> tbytes = new List<byte>();
                        tbytes.AddRange(s);
                        tbytes.Insert(this.Snippet.Index + 1, 1);

                        v_element.m_Points = tpts.ToArray();
                        v_element.m_PointTypes = tbytes.ToArray();
                        v_element.InitElement();
                        this.DisableSnippet();

                        //snippet index to edit
                        this.State = ST_CREATING + 11;
                        break;
                    case ST_CREATING + 11:
                        v_element.m_Points[this.Snippet.Index + 1] = e.FactorPoint;
                        this.Element.InitElement();
                        this.Snippet.Location = e.Location;
                        break;
                    case ST_EDITING:
                        v_element.m_Points[this.Snippet.Index] = e.FactorPoint;
                        v_element.InitElement();
                        this.Snippet.Location = e.Location;

                        break;
                    case ST_CREATING:
                        v_element.m_Points[m_index] = e.FactorPoint;
                        v_element.InitElement();
                        break;
                }
                this.Invalidate();
            }


            protected override void OnMouseDown(CoreMouseEventArgs e)
            {
                var l = this.Element;

                if (l != null) {

                    switch (e.Button)
                    {
                        case enuMouseButtons.Left:
                            if (this.Snippet == null)
                            {

                                if (this.IsControlKey)
                                {

                                    //start new graphics
                                    this.State = ST_CREATING + 0x100;
                                    this.append_Curve(e.FactorPoint);
                                    return;
                                }

                                this.append_Point(e.FactorPoint, C_MODE);
                                this.GenerateSnippets();
                                this.InitSnippetsLocation();
                                this.State = ST_CREATING + 0x100;//edit last point
                                this.Snippet = this.RegSnippets[m_index];
                                return;
                            }
                            else {
                                int v_idx = this.Snippet.Index;
                                if (this.IsControlKey)
                                {
                                    tooglePointDefinition(l, v_idx);
                                    m_index = v_idx;
                                    return;
                                }
                                else if (this.IsShiftKey) {
                                    //mark it for close point or toggle glocase
                                    if (v_idx > 0) {
                                        //toggle point
                                        if ((l.m_PointTypes[v_idx] & 0x80) == 0x80)
                                        {
                                            l.m_PointTypes[v_idx] = (byte)(l.m_PointTypes[v_idx] & ~0x80);
                                        }
                                        else
                                        {
                                            l.m_PointTypes[v_idx] |= 0x80;// (byte)(l.m_PointTypes[v_idx] & ~1);
                                        }
                                        this.UpdateSnippets();
                                        l.InitElement();
                                        this.Invalidate();
                                        return;
                                    }
                                }

                            }



                            if (this.State == ST_CREATING + 0x100) {
                                this.State = ST_EDITING;
                            }
                            break;
                        case enuMouseButtons.Right:
                           
                            break;
                    }



                }
                base.OnMouseDown(e);
            }

            protected override void OnMouseClick(CoreMouseEventArgs e)
            {
                var l = this.Element;
                switch (e.Button)
                {
                    case enuMouseButtons.Right:
                        if (l != null)
                        {
                            if (this.Snippet != null)
                            {
                                int v_idx = this.Snippet.Index;                               
                                if (l.m_Points.Length > 2)
                                {
                                    removePoint(l, v_idx);                                  
                                    return;
                                }
                            }
                        }
                        break;

                }


                base.OnMouseClick(e);
            }

            private void removePoint(QuadraticElement l, int v_idx)
            {
                var d = GetSegmentInfo(l.m_Points, l.m_PointTypes);
                
                if (d.ContainsKey(v_idx))
                {
                    var e = d[v_idx];//get segment
                                     //remove point
                    var g = new List<Vector2f>(l.m_Points);
                    g.RemoveAt(v_idx);
                    var tg = new List<Byte>(l.m_PointTypes);
                    tg.RemoveAt(v_idx);


                    if (e.points.Count <= 2)
                    {
                        //remove segment
                        for (int i = 0; i < e.points.Count-1; i++)
                        {
                            g.RemoveAt(e.start);
                            tg.RemoveAt(e.start);
                        }
                        var t = d.Values.Last();
                        if( (e.segment > 0)&&(t.segment==e.segment)) {
                            //deative close if present
                            tg[e.start - 1] = (byte)(tg[e.start - 1] & ~0x80);// e.segment[]
                        }
                    }
                

                    l.m_PointTypes = tg.ToArray();
                    l.m_Points = g.ToArray();
                    l.InitElement();
                    this.UpdateSnippets();
                    this.Invalidate();
                }
            }

            protected override void OnMouseDoubleClick(CoreMouseEventArgs e)
            {
                base.OnMouseDoubleClick(e);
            }

            /// <summary>
            /// store segment information
            /// </summary>
            sealed class QuadricSegmentInfo {
                internal int segment;
                internal int start;
                internal List<Vector2f> points;
                ///<summary>
                ///public .ctr
                ///</summary>
                public QuadricSegmentInfo()
                {
                    points = new List<Vector2f>();
                    start = 0;
                }
            }
            /// <summary>
            /// get dictionary of segment
            /// </summary>
            /// <param name="pts"></param>
            /// <param name="type"></param>
            /// <returns>ictionary<int, QuadricSegmentInfo> int is the index of the point , segment infor</returns>
            private Dictionary<int, QuadricSegmentInfo> GetSegmentInfo(Vector2f[] pts, byte[] type){
                var sl = new QuadricSegmentInfo();
                int seg = 0;
                Dictionary<int, QuadricSegmentInfo> dic = new Dictionary<int, QuadricSegmentInfo>();

                for (int i = 0; i<pts.Length; i++)
			    {
                    if ((type[i] == 0) && (i > 0)) {
                        seg++;
                        sl = new QuadricSegmentInfo() {
                            segment = seg,
                            start = i
                        };
                       
                    }
                    sl.points.Add(pts[i]);
                    dic[i] = sl;

			    }
                return dic;

            }

            private void tooglePointDefinition(QuadraticElement l, int v_idx)
            {

                //if (l.m_PointTypes[v_idx] == 0)//don't  change start point
                //    return;

                //toggle point
                if ((l.m_PointTypes[v_idx] & 0x3) == 0x3)
                {
                    l.m_PointTypes[v_idx] = (byte)(l.m_PointTypes[v_idx] & ~2);
                }
                else
                {
                    l.m_PointTypes[v_idx] |= 0x3;// (byte)(l.m_PointTypes[v_idx] & ~1);
                }
                this.State = ST_CREATING | 0x101;//toggling mode
                l.InitElement();
                this.UpdateSnippets();
                this.Snippet = this.RegSnippets[v_idx];
                this.Invalidate();
            }

            protected override void OnMouseMove(CoreMouseEventArgs e)
            {
                switch (e.Button)
                {
                    case enuMouseButtons.Left:
                        switch (this.State)
                        {
                            case ST_CREATING:
                            case ST_CREATING + 0x100:
                                if (this.Element != null)
                                {
                                    UpdateDrawing(e);
                                    return;
                                }
                                break;
                        }
                        break;
                }
                base.OnMouseMove(e);
            }
            protected override void OnMouseUp(CoreMouseEventArgs e)
            {
                var l = this.Element;
                switch (e.Button) {
                    case enuMouseButtons.Left:
                        if (this.Snippet == null) {
                            switch (this.State) {
                                case ST_CREATING + 0x100:
                                case ST_CREATING:
                                    if (l!= null)
                                        this.EndDrawing(e);
                                    else
                                        this.State = ST_NONE;
                                    break;
                            }
                        }
                        break;
                    case enuMouseButtons.Right:
                        if ((this.State &  0x100) == 0x100) {
                            this.State = ST_EDITING;
                            return;
                        }
                        break;
                }
                base.OnMouseUp(e);
            }
            public void append_Curve(Vector2f point) {
                var l = this.Element;
                List<Vector2f> vl = new List<Vector2f>();
                vl.AddRange(l.Points);
                vl.Add(point);
                vl.Add(point);
                vl.Add(point);

                l.m_PointTypes[l.m_PointTypes.Length-1] |= 0x80; //close curve
                var tl = new List<Byte>(l.m_PointTypes)
                {
                    0,
                    3,
                    1
                };
                l.m_Points = vl.ToArray();
                l.m_PointTypes = tl.ToArray();
                l.InitElement();
                m_index = vl.Count-1;
                Invalidate();


            }
            private void append_Point(Vector2f points, byte t) {
                var l = this.Element;
                List<Vector2f> vl = new List<Vector2f>();
                vl.AddRange(l.Points);
                vl.Add(points);

                var tl = new List<Byte>(l.m_PointTypes)
                {
                    t
                };
                l.m_Points = vl.ToArray();
                l.m_PointTypes = tl.ToArray();
                l.InitElement();
                m_index = vl.Count - 1;
                Invalidate();
            }
            private void toggle_Mode() {
                if (C_MODE == LINE)
                {
                    C_MODE = CONTROL;
                }
                else {
                    C_MODE = LINE;
                }
            }
        }
    }
}
