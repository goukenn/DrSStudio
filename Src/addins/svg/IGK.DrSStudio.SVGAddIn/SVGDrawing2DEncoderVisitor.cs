

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGEncoderVisitor.cs
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
file:SVGEncoderVisitor.cs
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.DrSStudio.SVGAddIn
{
    
using IGK.ICore;
    using IGK.ICore.Codec;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Drawing2D.WorkingObjects.Standard;
    using IGK.ICore.GraphicModels;
    using IGK.ICore.Imaging;
    using IGK.ICore.IO;
    using System.Xml;

    [CoreVisitorAttribute("SVG")]
    /// <summary>
    /// represent a visitor base
    /// </summary>
    public class SVGDrawing2DEncoderVisitor : CoreEncoderVisitor
    {
        private SVGWriter m_writer;
        private SVGDefNode m_defition;

        private string m_basesDirectory;
        private bool m_renderInfo;
        private bool m_UseBrushStyle;
        private bool m_MultiSvgDocument;

        public bool MultiSvgDocument
        {
            get { return m_MultiSvgDocument; }
            set
            {
                if (m_MultiSvgDocument != value)
                {
                    m_MultiSvgDocument = value;
                }
            }
        }

        public bool UseBrushStyle
        {
            get { return m_UseBrushStyle; }
            set
            {
                if (m_UseBrushStyle != value)
                {
                    m_UseBrushStyle = value;
                    OnUseBrushStyleChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler UseBrushStyleChanged;
        private int m_mark;
        public override void Flush()
        {
            this.m_writer.Flush();
        }
        protected virtual void OnUseBrushStyleChanged(EventArgs e)
        {
            UseBrushStyleChanged?.Invoke(this, e);
        }

        public override string Out => this.m_writer.GetOutput();

        public SVGDrawing2DEncoderVisitor():base()
        {
            this.m_UseBrushStyle = true;
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };
            SVGWriter sw = new SVGWriter(sb, settings);
            this.m_writer = sw;
        }
        /// <summary>
        /// create a encoder writer .
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="baseDirectory"></param>
        public SVGDrawing2DEncoderVisitor(SVGWriter writer, string baseDirectory):base()
        {
            this.m_UseBrushStyle = true;
            this.m_writer = writer ?? throw new CoreException(enuExceptionType.ArgumentIsNull, nameof(writer));
            this.m_basesDirectory = baseDirectory;

            this.m_writer.WriteString("<?xml version=\"1.0\" standalone=\"no\"?>");
            this.m_writer.WriteDocType("<!DOCTYPE svg PUBLIC \"-//W3C//DTD SVG 1.1//EN\" \"http://www.w3.org/Graphics/SVG/1.1/DTD/svg11.dtd\">");
            this.m_writer.WriteComment($"App : {CoreApplicationManager.Application.AppName}");
            this.m_writer.WriteComment($"Author : {CoreApplicationManager.Application.AppAuthor}");
            this.m_writer.WriteComment("version : 1.0");
            this.m_writer.WriteComment("description : File generate by "+GetType().Name );
        }
        public void Visit(ICore2DDrawingDocument document)
        {
            if ((this.m_mark == 0) && ( this.MultiSvgDocument))
            {
                this.m_writer.WriteStartElement("svgs");
                this.m_writer.WriteAttributeString("version", "1.1");
                this.m_writer.WriteAttributeString("xmlns", "http://www.w3.org/2000/svg");
                this.m_mark = 1;
            }

          
            this.m_writer.WriteStartElement("svg");
            var w = document.Width.ToString ();
            var h = document.Height .ToString ();
            this.m_writer.WriteAttributeString("width", w);
            this.m_writer.WriteAttributeString("height", h);
            this.m_writer.WriteAttributeString("id", document.Id);
            this.m_writer.WriteAttributeString("viewBox", string.Format("0 0 {0} {1}", w,h));
            if (this.UseBrushStyle && !document.BackgroundTransparent )
                this.m_writer.WriteAttributeString("fill", document.BackgroundColor.ToString(true));
            if (this.m_renderInfo == false)
            {
                if (!this.MultiSvgDocument) {
                    this.m_writer.WriteAttributeString("version", "1.1");
                    this.m_writer.WriteAttributeString("xmlns", "http://www.w3.org/2000/svg");
                }
                this.m_renderInfo = true;
               
            }

            this.m_writer.WriteAttributeString("xmlns:xlink", "http://www.w3.org/1999/xlink");

            this.m_defition = new SVGDefNode();
            this.m_writer.AddNode(this.m_defition);
            this.m_defition.SetProperty("multilayer", (document.Layers.Count>1));
            foreach (ICore2DDrawingLayer l in document.Layers)
            {
                if (this.Accept(l))
                {
                    this.Visit(l);
                }
            }
            this.m_writer.WriteEndElement();
           
        }
        internal void Done()
        {
            if (this.MultiSvgDocument)
            {
                this.m_writer.WriteEndElement();
               
            }
            this.m_mark = 0;
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            var m_layer = (bool)this.m_defition.GetPropertyValue("multilayer") || layer.IsClipped;
           if( m_layer)
              this.m_writer.WriteStartElement("g");

           if (layer.IsClipped) {
                List<ICore2DDrawingLayeredElement> clip_i =  this.m_defition.GetPropertyValue("clips")
                    as List<ICore2DDrawingLayeredElement>;
                if (clip_i == null) {
                    clip_i = new List<ICore2DDrawingLayeredElement>();
                }

               var clips = this.m_defition.Add ("clipPath");
               var l = layer.ClippedElement;
               clip_i.Add(l);

               clips["id"] = "clip_" + clip_i.Count;
               var s = this.m_writer;
               var sb = new StringBuilder();
               SVGWriter g = new SVGWriter(sb, new System.Xml.XmlWriterSettings() { 
               });


               this.m_writer = g;
               this.Visit(l);
                  g.Flush();
                  clips.Content = sb.ToString();
               this.m_writer = s;

               this.m_writer.WriteAttributeString("clip-path" , "url(#"+clips["id"]+")");
               this.m_defition.SetProperty ("clips", clip_i);
           }
            foreach (ICore2DDrawingLayeredElement item in layer.Elements)
            {
                if (this.Accept(item) && (item != layer.ClippedElement))
                    this.Visit(item as object);
            }
            if (m_layer)
            this.m_writer.WriteEndElement();
        }
        public void Visit(DocumentBlockElement  doc)
        {
            this.m_writer.WriteStartElement("g");
            this.WriteInfo(doc);
            this.Visit(doc.Document);
            this.m_writer.WriteEndElement();
        }
        public void Visit(GroupElement g)
        {
            this.m_writer.WriteStartElement("g");
            this.WriteInfo(g);
            foreach (ICore2DDrawingLayeredElement  i in g.Elements)
            {
                if (this.Accept(i))
                    this.Visit(i as object);
            }
            this.m_writer.WriteEndElement();
        }
        public void Visit(ICore2DDrawingLayeredElement element)
        {
            this.m_writer.WriteStartElement("path");
            this.WritePathInfo(element);
            this.m_writer.WriteEndElement();
        }

        public void Visit(QuadraticElement e) {
            this.m_writer.WriteStartElement("path");
            this.WritePathInfo(e);
            this.m_writer.WriteEndElement();
        }
        private void WritePathInfo(QuadraticElement element)
        {
            this.WriteInfo(element as Core2DDrawingLayeredElement);
            this.m_writer.WriteAttributeString("d", this.GetPathDefinition(element));
            ICoreGraphicsPath vp = element.GetPath();
            if (this.UseBrushStyle && (vp != null) && (vp.FillMode == enuFillMode.Alternate))
            {
                this.m_writer.WriteAttributeString("fill-rule", "evenodd");
            }
        }
        private void WritePathInfo(ICore2DDrawingLayeredElement element)
        {
            this.WriteInfo(element as Core2DDrawingLayeredElement);
            this.m_writer.WriteAttributeString("d", this.GetPathDefinition(element as Core2DDrawingLayeredElement));
            ICoreGraphicsPath vp = element.GetPath();
            if (this.UseBrushStyle && (vp!=null) && (vp.FillMode == enuFillMode.Alternate))
            {
                this.m_writer.WriteAttributeString("fill-rule", "evenodd");
            }
        }
        public void Visit(RectangleElement rec)
        {
            this.m_writer.WriteStartElement("rect");
            this.WriteInfo(rec);
            this.m_writer.WriteEndElement();
        }
        //public void Visit(MultilineElement line)
        //{
        //    this.m_writer.WriteStartElement("polyline");
        //    this.writeInfo(line);
        //    this.writePoints(line.Points);
        //    this.m_writer.WriteEndElement();
        //}
        public void Visit(PathElement path)
        {
            this.Visit(path as ICore2DDrawingLayeredElement);
        }
        public void Visit(BezierElement bezierElement)
        {
            this.Visit(bezierElement as ICore2DDrawingLayeredElement);
        }
        private string GetPathDefinition(Core2DDrawingLayeredElement element)
        {
            if (element == null)
                return string.Empty;
            ICoreGraphicsPath c = element.GetPath();
            if (c == null)
                return string.Empty;
            ICoreGraphicsPath p = c.Clone() as ICoreGraphicsPath;
            //because the graphics already contains the transformation no need to apply transform
            //Matrix mI = new Matrix(element.GetMatrix().Elements);//.Clone();
            //if (!mI.IsIdentity)
            //{
            //    mI.Invert();
            //    p.Transform(mI);
            //}
            string o  = SVGUtils.GetPathDefinition(p);
            p.Dispose();
            return o;

        }
        protected void WriteMatrix(Matrix matrix)
        {
            if ((matrix != null) && !matrix.IsIdentity)
            {
                var t = matrix.Elements ; 
                float[] svgMatrix = new float[] { 
                    t[0] , t[1],
                    t[4] , t[5],
                    matrix.OffsetX , matrix.OffsetY 
                };
                this.m_writer.WriteAttributeString("transform",
                   string.Format("matrix({0})", string.Join(" ", svgMatrix)));
            }
        }
        public void Visit(RoundRectangleElement rec)
        {
            this.m_writer.WriteStartElement("rect");
            this.WriteInfo(rec);
            if (rec.BottomLeft.Equals(rec.BottomRight) && rec.TopLeft.Equals(rec.TopRight))
            {
                this.m_writer.WriteAttributeString("rx", rec.BottomLeft.X.ToString());
                this.m_writer.WriteAttributeString("ry", rec.BottomLeft.Y.ToString());
            }
            this.m_writer.WriteEndElement();
        }
        private void WriteInfo(Core2DDrawingLayeredElement element)
        {
            if (element == null) return;
            this.WriteCoreAttrib(element);
            this.WriteStyle(element);

            if (!(element is PathElement ))

            this.WriteMatrix(element.GetMatrix());
        }
        private void WriteInfo(RectangleElement  rec)
        {
            this.WriteCoreAttrib(rec);
            this.WriteBound(rec.Bounds);
            this.WriteStyle(rec);
            this.WriteMatrix(rec.GetMatrix());
        }
        private void WriteBound(Rectanglef rc)
        {
            this.m_writer.WriteAttributeString("x", rc.X.ToString());
            this.m_writer.WriteAttributeString("y", rc.Y.ToString());
            this.m_writer.WriteAttributeString("width", rc.Width.ToString());
            this.m_writer.WriteAttributeString("height", rc.Height.ToString());
        }
        private void WriteCoreAttrib(ICoreWorkingObject rec)
        {
            this.m_writer.WriteAttributeString("id", rec.Id);
            //‘id’, ‘xml:base’, ‘xml:lang’ and ‘xml:space’.
        }
        private void WriteStyle(ICore2DDrawingLayeredElement obj)
        {
            if (!UseBrushStyle)
                return;
            GetFillBrushDefinition(obj);
            GetStrokeBrushDefinition(obj);
            if (obj is ICoreBrushOwner t)
            {
                if (t.GetBrush(enuBrushMode.Stroke) is ICorePen br)
                {
                    this.m_writer.WriteAttributeString("stroke-width", br.Width);
                }
            }
        }
        public void Visit(LineElement line)
        {
            this.m_writer.WriteStartElement("line");
            this.WriteCoreAttrib(line);
            this.m_writer.WriteAttributeString("x1", line.StartPoint.X.ToString());
            this.m_writer.WriteAttributeString("y1", line.StartPoint.Y.ToString());
            this.m_writer.WriteAttributeString("x2", line.EndPoint.X.ToString());
            this.m_writer.WriteAttributeString("y2", line.EndPoint.Y.ToString());
            this.WriteStyle(line);
            this.m_writer.WriteEndElement();
        }
        public void Visit(CircleElement circle)
        {
            if (circle.Radius.Length > 1)
            {
                this.m_writer.WriteStartElement("path");
                //this.WriteCoreAttrib(circle);
                //this.WriteStyle(circle);
                //this.WriteMatrix(circle.GetMatrix());
                this.WritePathInfo(circle);
                this.m_writer.WriteEndElement();

                return;
            }
                this.m_writer.WriteStartElement("circle");
            this.WriteCoreAttrib(circle);
            this.m_writer.WriteAttributeString("cx", circle.Center.X);
            this.m_writer.WriteAttributeString("cy", circle.Center.Y);
            this.m_writer.WriteAttributeString("r", circle.Radius[0]);
            this.WriteStyle(circle);
            this.WriteMatrix(circle.GetMatrix());
            this.m_writer.WriteEndElement();
        }
        public void Visit(EllipseElement ellipse)
        {
            this.m_writer.WriteStartElement("ellipse");
            this.WriteCoreAttrib(ellipse);
            this.m_writer.WriteAttributeString("cx", ellipse.Center.X);
            this.m_writer.WriteAttributeString("cy", ellipse.Center.Y);
            this.m_writer.WriteAttributeString("rx", ellipse.Radius[0].X);
            this.m_writer.WriteAttributeString("ry", ellipse.Radius[0].Y);
            this.WriteStyle(ellipse);
            this.WriteMatrix(ellipse.GetMatrix());
            this.m_writer.WriteEndElement();
        }
        public void Visit(PolygonElement polygon)
        {
            this.m_writer.WriteStartElement("polygon");
            this.WriteCoreAttrib(polygon);
            this.WritePoints(polygon.GetPath().PathPoints);
            this.WriteStyle(polygon);
            this.m_writer.WriteEndElement();
        }
        public void Visit(SplineElement element)
        {
            this.Visit(element as ICore2DDrawingLayeredElement);
        }
        private void WritePoints(Vector2f[] pointF)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (Vector2f pt in pointF)
            {
                if (i != 0)
                    sb.Append(" ");
                sb.Append(string.Format("{0},{1}", pt.X, pt.Y));
                i = 1;
            }
            this.m_writer.WriteAttributeString("points", sb.ToString());
        }
        //private void writePoints(Vector2f[] pointF)
        //{
        //    this.m_writer.WriteAttributeString("points", getPoints(pointF));
        //}
        //private string getPoints(Vector2f[] pointF)
        //{
        //    StringBuilder sb = new StringBuilder();
        //    int i = 0;
        //    foreach (Vector2f pt in pointF)
        //    {
        //        if (i != 0)
        //            sb.Append(" ");
        //        sb.Append(string.Format("{0},{1}", pt.X, pt.Y));
        //        i = 1;
        //    }
        //    return sb.ToString();
        //}
        private string GetPoints(Vector2f[] pointF)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (Vector2f pt in pointF)
            {
                if (i != 0)
                    sb.Append(" ");
                sb.Append(string.Format("{0},{1}", pt.X, pt.Y));
                i = 1;
            }
            return sb.ToString();
        }
        public void Visit(CustomPolygonElement cpolygon)
        {
            this.m_writer.WriteStartElement("polygon");
            this.WriteCoreAttrib(cpolygon);
            this.WritePoints(cpolygon.Points);
            this.WriteStyle(cpolygon);
            this.m_writer.WriteEndElement();
        }
        //public void Visit(SplineElement cspline)
        //{
        //    this.Visit(cspline as ICore2DDrawingLayeredElement);
        //}
        public void Visit(ImageElement img)
        {
            m_writer.WriteStartElement ("image");
            this.WriteInfo (img);
            this.WriteBound ( new Rectanglei(Vector2i.Zero , img.Bitmap.Size));
            string id = SaveToRes(img.Id , img.Bitmap );
            this.m_writer.WriteAttributeString ("href", id);
            m_writer.WriteEndElement ();
        }
        private string SaveToRes(string p, ICoreBitmap bitmap)
        {
            if (string.IsNullOrEmpty (this.m_basesDirectory))
              return string.Empty;
            string v_out = p+".png";
            if (PathUtils.CreateDir(Path.Combine(this.m_basesDirectory, "Resources")))
            {
                try
                {
                    bitmap.Save(Path.Combine(this.m_basesDirectory, "Resources", v_out), CoreBitmapFormat.Png);
                    return "Resources/"+v_out;
                }
                catch { 
                }
            }
            return string.Empty;
        }
        public void GetFillBrushDefinition(ICore2DDrawingLayeredElement l)
        {
            Colorf cl = Colorf.Empty;
            if (l is ICoreBrushOwner)
            {
                ICoreBrush br = (l as ICoreBrushOwner).GetBrush(enuBrushMode.Fill);
                if (br == null)
                {
                    return;
                }
                StringBuilder sb = new StringBuilder();
                switch (br.BrushType)
                {
                    case enuBrushType.Solid:
                        cl = br.Colors[0];
                        if (cl.A == 0.0f)
                        {
                            sb.Append("none");
                        }
                        else
                        {
                            if (cl.A != 1.0)
                                this.m_writer.WriteAttributeString("fill-opacity", cl.A.ToString());
                            sb.Append(cl.ToString(true));
                        }
                        break;
                    case enuBrushType.Hatch:
                        break;
                    case enuBrushType.LinearGradient:

                        //SVGLinearGradient b = new SVGLinearGradient();
                        //b.id = br.Id;


                        var def = this.m_defition.Add("linearGradient");
                        var v_angle = br.Angle;
                        def["id"] = br.Id;
                        var tab = br.Colors;
                        var pos = br.Positions;

                        if ((v_angle % 360) != 0) {

                            def["gradientTransform"] = string.Format("rotate({0})", v_angle);
                        }
                        for (int i = 0; i < tab.Length; i++)
                        {
                            cl = tab[i];
                            var v_stop = def.Add("stop");
                            v_stop["offset"] = string.Format("{0}%", pos[i] * 100);
                            v_stop["style"] = string.Format("stop-color:rgb({0},{1},{2}); stop-opacity:{3}",
                                (cl.R % 255).TrimByte(),
                                (cl.G % 255).TrimByte(),
                                (cl.B % 255).TrimByte(),
                                cl.A);

                        }
                        sb.Append("url(#" + br.Id + ")");
                        break;
                    case enuBrushType.PathGradient:
                        break;
                    case enuBrushType.Texture:
                        
                        
                        
                        string v_fn = string.Format( "Resources/{0}", br.Id);
                        if (br.Bitmap.Save(this.m_basesDirectory+"/"+v_fn, CoreBitmapFormat.Png))//;//.Save("img/" + br.Id, CoreBitmapFormat.Png);
                        {
                            var v_pdef = this.m_defition.Add("pattern");
                            var img = v_pdef.Add("image");
                            v_pdef["x"] = img["x"] =
                                v_pdef["y"] = img["y"] = 0;
                            v_pdef["width"] = img["width"] = string.Format("{0}px", br.Bitmap.Width);
                            v_pdef["height"] = img["height"] = string.Format("{0}px", br.Bitmap.Height);
                            
                            

                            v_pdef["id"] = br.Id;
                            
                                img["xlink:href"] = v_fn;
                            sb.Append("url(#" + br.Id + ")");
                          }
                        break;
                    case enuBrushType.Custom:
                        break;
                    default:
                        break;
                }
                this.m_writer.WriteAttributeString("fill", sb.ToString());
                sb.Length = 0;
            }
        }
        public void GetStrokeBrushDefinition(ICore2DDrawingLayeredElement l)
        {
            if (l is ICoreBrushOwner)
            {
                ICorePen br = (l as ICoreBrushOwner).GetBrush(enuBrushMode.Stroke) as CorePen;
                if (br == null)
                {
                    return;
                }
                bool none = false;
                StringBuilder sb = new StringBuilder();
                switch (br.BrushType)
                {
                    case enuBrushType.Solid:
                        Colorf cl = br.Colors[0];
                        if (cl.A == 0.0f)
                        {
                            sb.Append("none");
                            none = true;
                        }
                        else
                        {
                            if (cl.A != 1.0)
                                this.m_writer.WriteAttributeString("stroke-opacity", cl.A.ToString());
                            sb.Append(cl.ToString(true));
                        }
                        break;
                    case enuBrushType.Hatch:
                        break;
                    case enuBrushType.LinearGradient:
                        break;
                    case enuBrushType.PathGradient:
                        break;
                    case enuBrushType.Texture:
                        break;
                    case enuBrushType.Custom:
                        break;
                    default:
                        break;
                }

                this.m_writer.WriteAttributeString("stroke", sb.ToString());
                sb.Length = 0;
                if (none)
                    return;
                //render cap
                switch (br.LineJoin)
                {
                    case enuLineJoin.Bevel:
                        this.m_writer.WriteAttributeString("stroke-linejoin", "bevel");
                        break;
                    case enuLineJoin.Round:
                        this.m_writer.WriteAttributeString("stroke-linejoin", "round");
                        break;
                    default:
                    //case LineJoin.Miter:
                    //    this.m_writer.WriteAttributeString("stroke-linejoin", "miter");
                    //    break;
                        break;
                }
                if (br.StartCap == br.EndCap)
                {
                    switch (br.StartCap)
                    {
                        case enuLineCap.Round:
                            this.m_writer.WriteAttributeString("stroke-linecap", "round");
                            break;
                        case enuLineCap.Square:
                            this.m_writer.WriteAttributeString("stroke-linecap", "scare");
                            break;
                        default:
                            break;
                    }
                }
                if (br.MiterLimit != 4)
                {
                    this.m_writer.WriteAttributeString("stroke-miterlimit", br.MiterLimit.ToString());
                }
            }
        }

       
    }
}

