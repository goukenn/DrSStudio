

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGUtils.cs
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
file:SVGUtils.cs
*/

using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.GraphicModels;
using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// represent a svg utility class
    /// </summary>
    public class SVGUtils
    {
        public static void ExportToHtmlDocument(SVGDrawing2DEncoderVisitor visitor, params  ICore2DDrawingDocument[] documents) {

            if (documents != null)
            {
                visitor.MultiSvgDocument = documents.Length > 1;
                foreach (ICore2DDrawingDocument item in documents)
                {
                    if (visitor.Accept(item))
                    {
                        visitor.Visit(item);
                    }
                }
                visitor.Done();
            }

        }
        public static void ExportToHtmlDocument(string filename, params  ICore2DDrawingDocument[] documents)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                OmitXmlDeclaration = true
            };
            SVGWriter w = SVGWriter.Create(sb, settings);
            w.OutputFolder = Path.GetDirectoryName(filename);

            ExportToHtmlDocument ( new SVGDrawing2DEncoderVisitor (w, PathUtils.GetDirectoryName(filename)), documents);
            w.Flush();
            File.WriteAllText(filename, sb.ToString());
        }

        internal static string GetPathDefinition(ICoreGraphicsPath p)
        {      
            StringBuilder sb = new StringBuilder();
            Vector2f[] v_pts = p.PathPoints;
            byte[] v_types = p.PathTypes;
            string v_mode = string.Empty;
            for (int i = 0; i < v_pts.Length; i++)
            {
                switch ((enuGdiGraphicPathType)v_types[i])
                {
                    case enuGdiGraphicPathType.StartFigure:
                        if (v_mode != "M")
                        {
                            sb.Append("M ");
                            v_mode = "M";
                        }
                        sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y));
                        sb.Append(" ");
                        break;
                    case enuGdiGraphicPathType.ControlPoint:
                        if (v_mode != "C")
                        {
                            sb.Append("C ");
                            v_mode = "C";
                        }
                        sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y));
                        sb.Append(" ");
                        break;
                    case enuGdiGraphicPathType.LinePoint:
                        if (v_mode != "L")
                        {
                            sb.Append("L ");
                            v_mode = "L";
                        }
                        sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y));
                        sb.Append(" ");
                        break;
                    case enuGdiGraphicPathType.Marker | enuGdiGraphicPathType.ControlPoint:
                        sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y) + " ");
                        //  sb.Append(" z ");
                        break;
                    case enuGdiGraphicPathType.Marker:
                        break;
                    case enuGdiGraphicPathType.Mask:
                        break;
                    case enuGdiGraphicPathType.EndPoint:
                        break;
                    default:
                        byte v_tp = v_types[i];
                        bool v_end = false;
                        if ((v_tp & 32) == 32)
                        { 
                             //point is marker
                            v_tp -= 32;
                        }
                        if ((v_tp & 128) == 128)
                        {
                            //point is end
                            v_tp -= 128;
                            v_end = true;
                        }

                        if (v_end )
                        {
                            if (v_tp == 1)
                            {
                                sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y));
                                if (v_mode == "C") {
                                    //append twice
                                    sb.Append(" "+ string.Join(",", v_pts[i].X, v_pts[i].Y));
                                    sb.Append(" "+string.Join(",", v_pts[i].X, v_pts[i].Y));
                                }
                                sb.Append(" z");
                            }
                            else if (v_tp == 3) {
                                if (v_mode != "C")
                                {
                                    sb.Append("C ");
                                    v_mode = "C";
                                }
                                sb.Append(string.Join(",", v_pts[i].X, v_pts[i].Y));
                                sb.Append(" z");
                                v_mode = "";
                            }
                        }
                        break;
                }
            }
            //if (element.Closed)
            //    sb.Append(" z");
            return sb.ToString();
        }

        class GListPoint   {
            List<Vector2f> m_pts;
            Vector2f m_current;
            private SVGUtils sVGUtils;
            private IsRelativeCallback m_callback;
            private bool m_modeFlag;

            public Vector2f Current => m_current;
            public int Count => m_pts.Count;

            public Vector2f this[int i] {
            get {
                    return this.m_pts[i];
                }
                set
                {
                     this.m_pts[i] =value;
                }
            }

            public void UpdateCurrent() {
                if (this.Count > 0) {
                    this.m_current = this.m_pts[this.Count - 1];
                }
            }
            ///<summary>
            ///public .ctr
            ///</summary>
            public GListPoint(IsRelativeCallback callback)
            {
                this.m_pts = new List<Vector2f>();
                this.m_callback = callback;
            }

            public GListPoint(SVGUtils sVGUtils)
            {
                this.sVGUtils = sVGUtils;
            }

            public void Add(Vector2f pts) {
                //this.m_pts.Add(pts);
                if (this.m_callback())//is relative path
                {
                    
                    this.m_pts.Add(
                        m_current + pts
                    );
                    if (m_modeFlag) {
                       // m_current = this.m_pts[this.m_pts.Count - 1];
                        m_modeFlag = false;
                    }
                }
                else {
                    m_current = pts;
                    this.m_pts.Add(m_current);
                }

            }
            public void Clear() {
                this.m_pts.Clear();
                this.m_current = Vector2f.Zero;
            }

            internal Vector2f[] ToArray()
            {
                return this.m_pts.ToArray();
            }

            internal void ChangeMode()
            {
                m_modeFlag = true;
            }
        }

        delegate bool IsRelativeCallback();

        /// <summary>
        /// get graphics path from definition
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static CoreGraphicsPath GetPathFromDefinition(string p)
        {
            p = p.Replace(",", " ");
            CoreGraphicsPath v_p = new CoreGraphicsPath();

            if (string.IsNullOrEmpty(p))
            {
                return null;
            }
            string v_mode = string.Empty;
            char v_test = '\0';
            Vector2f v_point = Vector2f.Zero;
            List<byte> m = new List<byte>();
            //List<Vector2f> pt = new List<Vector2f>();

            GListPoint pt = new GListPoint(new IsRelativeCallback(() =>
            {
                return char.IsLower(v_mode[0]);
            }));

            byte v_enum_cpt = (byte)enuGdiGraphicPathType.ControlPoint;
            Vector2f c_point = Vector2f.Zero; //current point;
            int v_idx = 0;
            int v_counttime = 4;
            for (int i = 0; i < p.Length; i++)
            {
                if (i == v_idx)
                {
                    if ((v_counttime--) <= 0)
                        throw new Exception("Point failed to read");
                }
                else
                {
                    v_idx = i;
                    v_counttime = 4;
                }
                v_test = p[i];
                //if (char.IsLetter(v_test)) {
                //    pt.ChangeMode();
                //}
                switch (v_test)
                {
                    case 'l':
                    case 'L':
                        i++;
                        v_mode = v_test.ToString();
                        Add_line_point(p, m, pt, ref i);

                        //v_point = readPoint(p, ref i);
                        //    m.Add((byte)enuGdiGraphicPathType.LinePoint);
                        //    pt.Add(v_point);
                        //pt.UpdateCurrent();
                        break;
                    case 's':
                    case 'S':
                        i++;
                        //#if DEBUG
                        //                        if (v_mode.ToLower() != "c")
                        //                        {
                        //                        }
                        //#endif
                        // string bck = v_mode;//backup previous mode
                        v_mode = v_test.ToString();
                        bool lmode = char.IsLower(v_test);
                        var v_point1 = ReadPoint(p, ref i);
                        var v_point2 = ReadPoint(p, ref i);

                        //calculate the symetric point
                        var s_point = (2 * pt.Current) -
                            pt[pt.Count - 2];
                        if (lmode)
                        {
#pragma warning disable IDE0054 // Use compound assignment
                            s_point = s_point - pt.Current;// - s_point;
#pragma warning restore IDE0054 // Use compound assignment
                        }
                        //( 2* pt[pt.Count - 1]) - (
                        //    pt[pt.Count - 2] );
                        //if (lmode)
                        //{
                        //    c_point = pt[pt.Count - 1];
                        //    AddPoint(ref v_point1, ref c_point);
                        //    AddPoint(ref v_point2, ref c_point);
                        //}


                        pt.Add(s_point);
                        pt.Add(v_point1);
                        pt.Add(v_point2);
                        pt.UpdateCurrent();
                        m.Add(v_enum_cpt);
                        m.Add(v_enum_cpt);
                        m.Add(v_enum_cpt);
                        //v_mode = bck; //restore the saved mode
                        break;
                    case 'h':
                    case 'H'://horizontal absolute

                        v_mode = v_test.ToString();
                        i++;
                        v_point = Vector2f.Zero;
                        if (v_test == 'h')
                        {
                            v_point.X = ReadFloat(p, ref i);
                        }
                        else
                            v_point = new Vector2f(ReadFloat(p, ref i), pt.Current.Y);

                        m.Add((byte)enuGdiGraphicPathType.LinePoint);
                        pt.Add(v_point);

                        pt.UpdateCurrent();
                        break;
                    case 'C'://control point
                    case 'c':
                        i++;
                        {//read the first 3 point and addIt 
                            //read_curve_point(p, pt, ref i);

                            //v_point = readPoint(p, ref i);
                            v_mode = v_test.ToString();
                            Add_curve_point(p, m, pt, ref i);
                            //if (v_mode == "S") {
                            //}
                            //var pt2 = readPoint(p, ref i);
                            //var pt3 = readPoint(p, ref i);
                            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
                            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
                            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );

                            //pt.Add(v_point);
                            //pt.Add(pt2);
                            //pt.Add(pt3);// readPoint(p, ref i));
                            //pt.UpdateCurrent();
                        }
                        break;
                    case 'v'://vertical relative
                    case 'V'://to vertial absolute
                        i++;
                        v_mode = v_test.ToString();
                        v_point = Vector2f.Zero;
                        if (v_test == 'v')
                            v_point.Y += ReadFloat(p, ref i);
                        else
                            v_point = new Vector2f(pt.Current.X, ReadFloat(p, ref i));
                        m.Add((byte)enuGdiGraphicPathType.LinePoint);
                        pt.Add(v_point);
                        pt.UpdateCurrent();
                        break;

                    case 'M'://start point
                    case 'm':
                        //case enuGdiGraphicPathType.StartFigure:
                        i++;
                        v_point = ReadPoint(p, ref i);
                        v_mode = v_test.ToString();// "M";
                        m.Add((byte)enuGdiGraphicPathType.StartFigure);
                        pt.Add(v_point);
                        //
                        pt.UpdateCurrent();
                        //v
                        //i--;
                        break;
                    case 'z':
                    case 'Z':
                        v_mode = v_test.ToString();// "Z";
                        m[m.Count - 1] = (byte)(m[m.Count - 1] | (byte)enuGdiGraphicPathType.EndPoint);
                        //close path
                        v_p.AddDefinition(pt.ToArray(), m.ToArray());
                        pt.Clear();
                        m.Clear();

                        v_mode = null;
                        break;
                    case 'q': //quadric relative
                    case 'Q':
                        {
                            switch (v_mode)
                            {
                                case "Q":
                                    //previous mode is q
                                    break;
                                default:
                                    break;
                            }
                            v_mode = v_test.ToString();
                            i++;
                            Vector2f q0 = v_point;
                            Vector2f q1 = ReadPoint(p, ref i);
                            Vector2f q2 = ReadPoint(p, ref i);
                            if (v_test == 'q')
                            {
                                q1 += q0;
                                q2 += q0;
                            }

                            Vector2f c1 = q0 + ((2 / 3.0f) * (q1 - q0));
                            Vector2f c2 = q2 + ((2 / 3.0f) * (q1 - q2));


                            m.Add((byte)enuGdiGraphicPathType.ControlPoint);
                            m.Add((byte)enuGdiGraphicPathType.ControlPoint);
                            m.Add((byte)enuGdiGraphicPathType.ControlPoint);
                            pt.Add(c1);
                            pt.Add(c2);
                            pt.Add(q2);
                            v_point = q2;
                        }
                        break;
                    case 't':
                    case 'T':
                        {
                            i++;
                            switch (v_mode)
                            {
                                case "q":
                                case "t":
                                case "T":
                                case "Q":
                                    v_mode = "t";
                                    if (pt.Count < 2)
                                        throw new CoreException("not valid");

                                    Vector2f pp = v_point;
                                    Vector2f c1 = pt[pt.Count - 3];
                                    Vector2f c2 = pt[pt.Count - 2];
                                    Vector2f dst = ReadPoint(p, ref i);//add point for minus
                                    if (v_test == 't')
                                        dst += pp;
                                    //reflect p1
                                    //reflect point
                                    //c1 = 2* pp - c1;// pp + CoreMathOperation.GetDistanceP(pp,c1);
                                    //c2 = 2*pp -c2;// pp + CoreMathOperation.GetDistanceP(pp, c2);

                                    m.Add((byte)enuGdiGraphicPathType.ControlPoint);
                                    m.Add((byte)enuGdiGraphicPathType.ControlPoint);
                                    m.Add((byte)enuGdiGraphicPathType.ControlPoint);

                                    //calculate the previous ref points
                                    var ocomm = (1.5f * c2) - (0.5f * pp);
                                    var refcomm = (2 * pp) - ocomm;
                                    c1 = pp + ((2 / 3.0f) * (refcomm - pp));
                                    c2 = dst + ((2 / 3.0f) * (refcomm - dst));
                                    //pt.Add(pp);
                                    //pt.Add(pp);
                                    pt.Add(c1);
                                    pt.Add(c2);
                                    pt.Add(dst);
                                    v_point = dst;
                                    break;
                                default:
                                    //consider the control point to be the last point
                                    //throw new CoreException("not valid operation");
                                    break;

                            }

                        }
                        break;
                    case 'a':
                    case 'A':
                        i++;
                        v_mode = v_test.ToString();
                        System.Diagnostics.Debug.WriteLine(i+ " reading a data");
                        Add_arc_point(p, m, pt, ref i);
                        break;
                    case ' '://continue reading
                    default:
                        System.Diagnostics.Debug.WriteLine("Reading default " +v_test + " at "+i);
                        if (v_test != ' ')
                        {


                            switch (v_mode)
                            {
                                case "C":
                                case "c":
                                    //continue add curve point
                                    Add_curve_point(p, m, pt, ref i);
                                    break;
                                case "L":
                                case "l":
                                    //continue add line
                                    Add_line_point(p, m, pt, ref i);
                                    break;
                                case "a":
                                case "A":
                                    Add_arc_point(p, m, pt, ref i);
                                    break;
                                default:
                                    System.Diagnostics.Debug.WriteLine("Not Handle ");
                                    break;
                                    //    throw new Exception("not valid");

                            }
                        }
                        break;

                }
            }
            if (m.Count > 0)
            {
                v_p.AddDefinition(pt.ToArray(), m.ToArray());
                pt.Clear();
                m.Clear();
            }
            return v_p;
        }

        private static void Add_line_point(string p, List<byte> m, GListPoint pt, ref int i)
        {
            var v_point = ReadPoint(p, ref i);
            m.Add((byte)enuGdiGraphicPathType.LinePoint);
            pt.Add(v_point);
            pt.UpdateCurrent();
        }

        private static void Add_curve_point(string p, List<byte> m, GListPoint pt, ref int i)
        {
            var v_point = ReadPoint(p, ref i);
            var pt2 = ReadPoint(p, ref i);
            var pt3 = ReadPoint(p, ref i);
            var v_enum_cpt = (byte)enuGdiGraphicPathType.ControlPoint;
            m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            pt.Add(v_point);
            pt.Add(pt2);
            pt.Add(pt3);// readPoint(p, ref i));
            pt.UpdateCurrent();
        }

        private static void Add_arc_point(string p, List<byte> m, GListPoint pt, ref int i)
        {
            var v_point = ReadPoint(p, ref i);
            var v_xaxis = ReadFloat(p, ref i);
            var v_angle = ReadPoint(p, ref i);
            var v_endpoint = ReadPoint(p, ref i);
            // var x = ReadFloat(p, ref i);

            //var v_enum_cpt = (byte)enuGdiGraphicPathType.ControlPoint;
            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            //m.Add(v_enum_cpt);// (byte)enuGdiGraphicPathType.ControlPoint );
            //pt.Add(v_point);
            //pt.Add(v_point);
            //pt.Add(v_point);// readPoint(p, ref i));
            //pt.UpdateCurrent();
        }

            private static void AddPoint(ref Vector2f v_point1, ref Vector2f c_point)
        {
            v_point1 += c_point;
            c_point = v_point1;
        }

        private static Vector2f ReadPoint(string p, ref int i)
        {
            float x = ReadFloat(p, ref i);

            float y = 0;

            if ((i < (p.Length - 1)) && char.IsLetter(p[i + 1]))
            { 
            } else {
                y = ReadFloat(p, ref i); 
            } 

            return new Vector2f(x, y);
        }

        private static float ReadFloat(string p, ref int i)
        {
            string b = string.Empty;
            char ch = (char)0;
            int v_dec = 0;
            while ( i < p.Length ) 
            {
                ch = p[i];
              

                if (Regex.IsMatch(ch.ToString(),"[0-9\\-\\.eE]"))
                {//check valid char 
                    if ((ch == '0') && string.IsNullOrEmpty(b)) { 
                        b += ch;
                        i++;
                        break;
                    }

                    if (ch == '.'){
                        if (v_dec==1) 
                            break;
                        v_dec = 1;
                    }
                    if ((ch == 'e' || ch == 'E')) {
                        //read number exponant
                        StringBuilder sb = new StringBuilder();
                        char ch2 = '\0';
                        while (i < p.Length)
                        {
                            i++;
                            ch2 = p[i];
                            if (
                                ((ch2 == '-') && (sb.Length == 0)) ||
                                (char.IsDigit(ch2))
                                )
                                sb.Append(ch2);
                            else
                            {
                                //i--;
                                break;
                            }
                        }
                        b += ch+sb.ToString();
                        break;
                    }
                    
                    if (ch=='-' ) {
                        if (Regex.IsMatch(b, @"(-){0,1}[0-9]+(\.[0-9]+)?"))
                        {//stop the reading but the index stay on the index
                            //i--;
                            break;
                        }
                    }

                        b += ch;
                        i++;
                        continue;
                    
                }
                switch (ch)
                {
                    case ' ':
                    case ',':
                        {
                            if (b == string.Empty)
                            {
                                i++;
                                continue;
                            }
                        }
                        break;
                }
                //else
                //    i--;
                if (char.IsLetter(ch))
                    i--;
                //System.Diagnostics.Debug.WriteLine("[SVGUtils] Failed to parse : " + ch);
                break;
            }

           


            if (string.IsNullOrEmpty(b))
                return 0.0f;
            if (float.TryParse(b, out float f))
            {
                return f;// var f = Convert.ToSingle(b);
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine("[SVGUtils] - readFloat : failed " + f);
#endif
            return 0.0f;
        }

        internal static void ExportToSVGFont(string fileName, ICore2DDrawingDocumentCollections documents)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };
            SVGWriter sw = new SVGWriter(sb, settings);


            SVGDrawing2DFontEncoderVisitor p =  new SVGDrawing2DFontEncoderVisitor(sw);
            sw.WriteStartElement("svg");
            //init svg header
            sw.WriteAttributeString("xmlns", "http://www.w3.org/2000/svg");
            sw.WriteAttributeString("version", "1.1");
            sw.WriteStartElement("metadata");
            sw.WriteEndElement();


            sw.WriteStartElement("defs");

            sw.WriteStartElement("font");
            sw.WriteAttributeString("id", Path.GetFileNameWithoutExtension(fileName));
            sw.WriteAttributeString("horiz-adv-x", "1080");


            sw.WriteStartElement("font-face");
            sw.WriteAttributeString("font-family", "testfont");
            sw.WriteAttributeString("font-weight", "400");
            sw.WriteAttributeString("font-stretch", "normal");
            //sw.WriteAttributeString("units-per-em", "2048");//important for size
            sw.WriteAttributeString("units-per-em", "100");//important for size
            //EXPORT ITEM TO FONT FACE

            //        font - family = "Corbel"
            //font - weight = "400"
            //font - stretch = "normal"
            //units - per - em = "2048"
            //panose - 1 = "2 11 5 3 2 2 4 2 2 4"
            //ascent = "1523"
            //descent = "-525"
            //x - height = "950"
            //cap - height = "1338"
            //bbox = "-1059 -490 2273 1949"
            //underline - thickness = "120"
            //underline - position = "-250"
            //unicode - range = "U+000D-FB04"

            sw.WriteAttributeString("panose", "2 11 5 3 2 2 4 2 2 4");
            sw.WriteAttributeString("ascent", 1523);
            sw.WriteAttributeString("descent", -525);
            sw.WriteAttributeString("x-height", "950");
            sw.WriteAttributeString("cap-height", "1338");
            sw.WriteAttributeString("bbox", "-1059 -490 2273 1949");
            sw.WriteAttributeString("underline-thinkness", 120);
            sw.WriteAttributeString("underline-position", -250);
            sw.WriteAttributeString("unicode-range", "U+000D-FB04");
            sw.WriteEndElement();//end font-face
            foreach (var item in documents)
            {
                p.Visit(item);
            }


            sw.WriteEndElement();//end font

            sw.WriteEndElement();

            //IGK.ICore.Xml.CoreXmlElement e = ICore.Xml.CoreXmlElement.CreateXmlNode("text");
            //e["x"] = "100";
            //e["y"] = "100";
            //e["style"] = "font-family:'testfont', sans-serif; font-size:24pt;";
            //e.Content = "text using embe @dded font! a b c";
            //sw.WriteString(e.RenderXML(null));


             sw.WriteEndElement();
            sw.Flush();

            File.WriteAllText(fileName, sb.ToString());
        }
    }
}

