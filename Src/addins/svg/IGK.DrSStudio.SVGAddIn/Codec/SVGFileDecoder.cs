
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.DrSStudio.SVGAddIn.Codec
{
    using IGK.ICore;
    using IGK.ICore.ComponentModel;
    using System.Text.RegularExpressions;

    public class SVGFileDecoder
    {
        private XmlReader m_reader;
        private string m_fileName;
        private SVGElementFactory m_factory;
        private List<ICore2DDrawingDocument> m_listOfDocument;
        private ISVGElementContainer m_svgElementContainer;
        private SVGInheritanceProperties m_inherit;
        private ICore2DDrawingDocument m_document;

        private SVGFileDecoder()
        {
            m_factory = new SVGElementFactory();
            m_inherit = new SVGInheritanceProperties();
        }

        public static ICore2DDrawingDocument[] Decode(string filename)
        {
            if (!File.Exists(filename))
                return null;

            SVGFileDecoder c = new SVGFileDecoder();
            c.m_fileName = filename;
            XmlReaderSettings setting = new XmlReaderSettings();
            setting.IgnoreComments = true;
            setting.IgnoreProcessingInstructions = true;
            setting.DtdProcessing = DtdProcessing.Ignore;

            c.m_reader = XmlReader.Create(filename, setting);
            var d = c.Decode();
            c.m_reader.Close();
            return d;
        }

        private ICore2DDrawingDocument[] Decode()
        {
            this.m_listOfDocument = new List<ICore2DDrawingDocument>();
            while (this.m_reader.Read())
            {
                switch (this.m_reader.NodeType)
                {                 
                    case XmlNodeType.Element:
                        this.Visit(this.m_reader);
                        break;                 
                    default:
                        break;
                }
            }
            return this.m_listOfDocument.ToArray();
        }

        private string CapitalizeFirstWord(string w)
        {
            if (w.Length > 1)
                return w[0].ToString().ToUpper() + w.Substring(1);
            return w[0].ToString().ToUpper();
        }
        private void Visit(XmlReader xmlReader)
        {
            string g =  CapitalizeFirstWord(xmlReader.Name);
            MethodInfo m = GetType().GetMethod("Visit" + g,
                 BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic );
            if (m != null)
            {
                m.Invoke(this, new object[] {
                    xmlReader
                });
            }
            else {
                Type t = GetType().Assembly.GetType("SVG" + g + "Element");
                if (t != null)
                {
                    t.GetMethod("Load", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public).Invoke(null,
                        new object[] {
                            this,
                            xmlReader
                        });
                }
            }
        }
     
        public void VisitSvg(XmlReader reader)
        {
            Core2DDrawingLayerDocument c = new Core2DDrawingLayerDocument();
            this.Load(c, reader.ReadSubtree());          
            this.m_listOfDocument.Add(c);
        }

        private void Load(ICoreWorkingObject obj, XmlReader xmlReader)
        {
            MethodInfo.GetCurrentMethod().Visit(this, obj, xmlReader);
        }
        private void Load(SvgClipPathElement p, XmlReader xmlReader)
        {

            var s = xmlReader;
            s.MoveToElement();
            bool vread = false; //start reading
            while (s.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (s.Name.ToLower() == "clippath") {
                            vread = true;
                            if (s.HasAttributes)
                            {
                                var dic = GetAttributes(s);
                                BindStyleSettings(p, dic);
                            }
                            break;
                        }
                        if (!vread) break;

                        if (s.Name == "use")
                        {
                            var dic = GetAttributes(s);
                            BindStyleSettings(p, dic);
                            if (dic.ContainsKey("xlink:href"))
                            {
                                string tag = dic["xlink:href"];

                                if (Regex.IsMatch(tag, SVGConstant.HASH_ATTRIB_REGEX))
                                {
                                    var m = this.m_inherit.Defs.GetElementById(tag.Replace("#", ""));
                                    if (m is ICore2DDrawingLayeredElement)
                                    {
                                        this.m_document.CurrentLayer.Elements.Add(m as ICore2DDrawingLayeredElement);
                                        this.m_document.CurrentLayer.ClippedElement =
                                            m as ICore2DDrawingLayeredElement;
                                    }
                                }

                            }
                        }
                        else {
                            var lobj = this.m_factory.Create(s.Name) as ICoreWorkingObject;
                            if (lobj != null) {
                                this.Load(lobj, s.ReadSubtree());

                                if (lobj is ICore2DDrawingLayeredElement)
                                {
                                    this.m_document.CurrentLayer.Elements.Add(lobj as ICore2DDrawingLayeredElement);
                                    this.m_document.CurrentLayer.ClippedElement =
                                       lobj as ICore2DDrawingLayeredElement;
                                }

                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        public void Load(ICore2DDrawingDocument document, XmlReader xmlReader)
        {
            this.m_document = document;
            this.m_svgElementContainer = document as ISVGElementContainer;
            document.BackgroundTransparent = true;
            var s = xmlReader;
            s.MoveToElement();
            if (s.Read() && s.HasAttributes)
            {
                var dic = GetAttributes(xmlReader);

                if (dic.ContainsKey("viewbox"))
                {
                    Rectanglei ic = Rectanglei.ConvertFromString(dic["viewbox"].ToString());
                    if ((ic.X == 0) && (ic.Y == 0) && (ic.Width > 0) && (ic.Height>0))
                    {
                        document.SetSize(ic.Width,ic.Height);
                        this.m_inherit.SetDefinition("width", ic.Width.ToString());
                        this.m_inherit.SetDefinition("height", ic.Height.ToString());
                    }

                }
                else
                {

                    if (dic.ContainsKey("width") &&
                        dic.ContainsKey("height"))
                    {
                        int w = (int)(dic["width"].ToString().ToPixel());
                        int h = (int)(dic["height"].ToString().ToPixel());
                        document.SetSize(w, h);
                        this.m_inherit.SetDefinition("width", w.ToString());
                        this.m_inherit.SetDefinition("height", h.ToString());
                    }
                }
            }
         
            List<ICore2DDrawingLayer> b = new List<ICore2DDrawingLayer>();
            bool layerAdd = false;
            while (s.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        CoreLog.WriteDebug("loading : "+s.Name);
                        ICoreWorkingObject l = this.m_factory.Create(s.Name) as ICoreWorkingObject;
                        if (l != null)
                        {

                            if (l is SVGDefElement def)
                            {
                                if (this.m_inherit.Defs == null)
                                {
                                    this.m_inherit.Defs = def;
                                    this.Load(def, s.ReadSubtree());
                                }
                                else {
                                    CoreLog.WriteDebug("Already got defs");
                                    s.Skip();
                                }
                            }
                            else
                            {
                                this.Load(l as ICoreWorkingObject, s.ReadSubtree());
                                if (l is ICore2DDrawingLayer)
                                {
                                    layerAdd = true;
                                    b.Add(l as ICore2DDrawingLayer);
                                }
                                else
                                {

                                   
                                    document.CurrentLayer.Elements.Add(l as ICore2DDrawingElement);
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (layerAdd)
            {
                document.Layers.Replace(b.ToArray());
            }
        }

        private void Load(SVGDefElement def, XmlReader xmlReader) {
            var s = xmlReader;
            s.MoveToElement();
            while (s.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (s.Name == "defs")
                            continue;

                        ICoreWorkingObject l = this.m_factory.Create(s.Name) as ICoreWorkingObject;
                        if (l != null) {
                            this.Load(l as ICoreWorkingObject, s.ReadSubtree());
                        }
                        def.Add(l);
                        break;
                }
            }
        }


        public void Load(RectangleElement rc, XmlReader reader)
        {
            /*
                  style="fill:#00ff00;stroke:#6d6200;stroke-width:2;stroke-linejoin:bevel;stroke-miterlimit:4;stroke-opacity:1;stroke-dasharray:none"
                  id="rect2985"
                  width="328.57144"
                  height="348.57144"
                  x="171.42857"
                  y="172.36218" 
             */
            if (reader.Read()) 
            {
                if (reader.HasAttributes)
                { 
                    ReadAttributes(rc, reader);
                }
            }
        }
        public void Load(SVGGroupElement group, XmlReader xmlReader) {
            var s = xmlReader;

            bool sreading = false;

            while (xmlReader.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (sreading)
                        {
                           // CoreLog.WriteDebug("loading : " + s.Name);
                            if (m_factory.Create(s.Name) is Core2DDrawingLayeredElement l)
                            {
                                Load(l as ICoreWorkingObject, s.ReadSubtree());
                                group.Elements.Add(l);
                            }
                        }
                        else
                        {
                            if (xmlReader.HasAttributes)
                            {
                                var dic = GetAttributes(xmlReader);
                                BindStyleSettings(group, dic);
                                if (dic.ContainsKey("fill"))
                                this.m_inherit.SetDefinition("fill", dic["fill"]);
                                if (dic.ContainsKey("stroke"))
                                    this.m_inherit.SetDefinition("stroke", dic["stroke"]);
                            }
                            sreading = true;
                        }
                        break;
                    default:

                        break;
                }
            }

            this.m_inherit.SetDefinition("fill", null);
            this.m_inherit.SetDefinition("stroke", null);
        }
       
        public void Load(ICore2DDrawingLayer layer, XmlReader xmlReader)
        {
            //load Element
            var s = xmlReader;

            bool sreading=false;

            while (xmlReader.Read() )
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (sreading)
                        {
                            CoreLog.WriteDebug("loading : " + s.Name);
                            ICore2DDrawingLayeredElement l = this.m_factory.Create(s.Name) as ICore2DDrawingLayeredElement;
                            if (l != null)
                            {
                                this.Load(l as ICoreWorkingObject, s.ReadSubtree());
                                layer.Elements.Add(l);
                            }
                        }
                        else
                        {
                            if (xmlReader.HasAttributes) {
                                var dic = GetAttributes(xmlReader);
                               // LoadAttributes(layer, dic);
                            }
                            sreading = true;
                        }
                        break;
                    default:

                        break;
                }
            }
        }


        private static Dictionary<string, string> GetStyleAttributes(string inStr) {

            Dictionary<string, string> g = new Dictionary<string, string>();
            string[] tg = inStr.Trim().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            string n = string.Empty;
            string v = string.Empty;
            string ch = string.Empty;
            StringReader sr = new StringReader(inStr);
            int i = 0;
            char kch = '\0';
            int mode = 0;
            while ( (i = sr.Read())!=-1) {
                kch=(char)i;
                switch (kch) {
                    case ':'://start read value
                        if (mode == 0)
                            mode = 1;
                        break;
                    case ';': //end if value 
                        g.Add(n, v);
                        n = string.Empty;
                        v = string.Empty;
                        mode = 1;
                        break;
                    default:
                        if (mode == 0) {
                            n += kch;
                        } else {
                            v += kch;
                        }
                        break;
                }

            }
            

            return g;
        }
        private static Dictionary<string,string> GetAttributes(XmlReader xmlReader)
        {
            Dictionary<string, string> g = new Dictionary<string, string>();
            if (xmlReader.MoveToFirstAttribute()) {
                g.Add(xmlReader.Name.ToLower(), xmlReader.Value);
                while (xmlReader.MoveToNextAttribute()) {
                    string n = xmlReader.Name.ToLower();
                    if (g.ContainsKey(n))
                        g[n] = xmlReader.Value;
                    else
                    g.Add(xmlReader.Name.ToLower(), xmlReader.Value);
                }
            }
            return g;
        }

        public void Load(PathElement p, XmlReader xmlReader) {
            var s = xmlReader;

            bool sreading = false;

            while (xmlReader.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (sreading)
                        {
                        }
                        else
                        {
                            if (xmlReader.HasAttributes)
                            {
                                var dic = GetAttributes(xmlReader);
                                if (dic.ContainsKey("d")) {
                                    try
                                    {
                                        p.FillBrush.SetSolidColor(Colorf.Black);
                                        p.StrokeBrush.SetSolidColor(Colorf.Black);
                                        var g = SVGUtils.GetPathFromDefinition(dic["d"]);
                                        p.SetDefinition(g);
                                    }
                                    catch (Exception ex) {
                                        CoreLog.WriteDebug(ex.Message);
                                    }
                                  //Vector2f[] defs =  dic["d"]
                                }
                                BindStyleSettings(p, dic);
                                // LoadAttributes(layer, dic);
                            }
                            sreading = true;
                        }
                        break;
                }
            }
        }
        public void Load(CustomPolygonElement p, XmlReader xmlReader)
        {
            var s = xmlReader;

            bool sreading = false;

            while (xmlReader.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (sreading)
                        {
                        }
                        else
                        {
                            if (xmlReader.HasAttributes)
                            {
                                var dic = GetAttributes(xmlReader);
                                if (dic.ContainsKey("points"))
                                {
                                    
                                    CoreVector2fArrayTypeConverter c = new CoreVector2fArrayTypeConverter();
                                    Vector2f[] g = (Vector2f[])c.ConvertFromString(dic["points"]);
                                    p.Points = g;

                                    //var g = SVGUtils.GetPathFromDefinition(dic["d"]);
                                    //p.SetDefinition(g);
                                    //Vector2f[] defs =  dic["d"]
                                }
                                BindStyleSettings(p, dic);
                                // LoadAttributes(layer, dic);
                            }
                            sreading = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }


        public void Load(CircleElement  p, XmlReader xmlReader)
        {
            var s = xmlReader;

            bool sreading = false;

            while (xmlReader.Read())
            {
                switch (s.NodeType)
                {
                    case XmlNodeType.Element:
                        if (sreading)
                        {
                        }
                        else
                        {
                            if (xmlReader.HasAttributes)
                            {
                                var dic = GetAttributes(xmlReader);
                                if ((dic.ContainsKey("cx")||
                                    dic.ContainsKey("cy"))
                                    && dic.ContainsKey("r")
                                    )
                                {
                                    //cx = "256" cy = "256" r = "256"
                                    
                                    var r  = float.Parse(dic["r"]);
                                    float cx = dic.ContainsKey("cx") ? float.Parse ( dic["cx"] ): 0.0f;
                                    float cy = dic.ContainsKey("cy") ? float.Parse ( dic["cy"] ): 0.0f;
                                    p.Center = new Vector2f(cx, cy);
                                    p.Radius = new float[] { r };

                                    BindStyleSettings(p, dic);
                                    //var g = SVGUtils.GetPathFromDefinition(dic["d"]);
                                    //p.SetDefinition(g);
                                    //Vector2f[] defs =  dic["d"]
                                }
                                // LoadAttributes(layer, dic);
                            }
                            sreading = true;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private void BindStyleSettings(ICore2DDrawingLayeredElement  p, Dictionary<string, string> dic)
        {
            if (dic.ContainsKey("id"))
                p.GetType().GetProperty("Id").SetValue(p,  dic["id"]);

            if (this.m_inherit.GetDefinition("fill") is string l) {
                dic["fill"] = l;
            }
            if (this.m_inherit.GetDefinition("stroke") is string sl)
            {
                dic["stroke"] = sl;
            }

            if (dic.ContainsKey("style"))
            {
                var gdir = SVGStyleAttributeReader.ReadAll(dic["style"]);
                if (!dic.ContainsKey("fill") && gdir.ContainsKey("fill")) {
                    dic["fill"] = gdir["fill"];
                }
                if (!dic.ContainsKey("stroke") && gdir.ContainsKey("stroke"))
                {
                    dic["stroke"] = gdir["stroke"];
                }
            }

            if (dic.ContainsKey("fill"))
            {
                string s = dic["fill"];
                Colorf c = Colorf.FromString(s);

                (p as ICoreBrushOwner)?.GetBrush(enuBrushMode.Fill)?.SetSolidColor(c);
            }
            else {
                (p as ICoreBrushOwner)?.GetBrush(enuBrushMode.Fill)?.SetSolidColor(Colorf.Transparent);
            }
            if (dic.ContainsKey("stroke") && (dic["stroke"]!="none"))
            {
                string s = dic["stroke"];
                Colorf c = Colorf.FromString(s);
                (p as ICoreBrushOwner)?.GetBrush(enuBrushMode.Stroke)?.SetSolidColor(c);
            }
            else {
                (p as ICoreBrushOwner)?.GetBrush(enuBrushMode.Stroke)?.SetSolidColor(Colorf.Transparent);
            }

            if (dic.ContainsKey("clip-path")) {
                string g = dic["clip-path"];
                if (Regex.IsMatch(g, SVGConstant.URL_ATTRIB_REGEX, RegexOptions.IgnoreCase)) {

                    string s = Regex.Matches(g, SVGConstant.URL_ATTRIB_REGEX, RegexOptions.IgnoreCase)[0].Groups["value"]?.ToString().Replace("#", "");
                    SVGDefNode def = this.m_inherit.GetObject<SVGDefNode>(null);

                    if (this.m_inherit.Defs != null) {
                        var v_g  = this.m_inherit.Defs.GetElementById(s);
                        if (v_g != null) {
                            if (this.m_document.CurrentLayer.ClippedElement == v_g) {

                            }
                        }

                    }

                }
            }


            // LoadAttributes(layer, dic);
            //bind properties
            if (dic.ContainsKey("transform"))
            {
                var m = Regex.Matches(dic["transform"], @"(?<name>(.)+)\((?<value>(.)+)\)");
                if (m.Count > 0)
                {

                    foreach (Match mi in m)
                    {
                        string data = mi.Groups["value"].ToString().Replace(',', ' ');
                        switch (mi.Groups["name"].ToString().Trim())
                        {
                            case "translate":
                                Vector2f g = Vector2f.ConvertFromString(data);
                                p.Translate(g.X, g.Y, enuMatrixOrder.Append);
                                break;
                            case "matrix":
                                Matrix mat = Matrix.ConvertFromString(data.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                                p.Transform(mat);//.Matrix.Multiply(mat);
                                break;

                        }
                    }

                }

            }
        }

        private void ReadAttributes(ICoreWorkingObject rc, XmlReader reader)
        {            
            var id = reader.GetAttribute ("id");
            //rc.SetId(id);
            MethodInfo.GetCurrentMethod().Visit (this, rc, reader );
        }
        private void ReadAttributes(RectangleElement rc, XmlReader reader)
        {
            var width = reader.GetAttribute("width").ToPixel();
            var height = reader.GetAttribute("height").ToPixel();
            var x = reader.GetAttribute("x").ToPixel();
            var y = reader.GetAttribute("y").ToPixel();

            string s = reader.GetAttribute("style");
            if (!string.IsNullOrEmpty(s))
            {
                CoreBrush fb = null;
                CorePen fp = null;
                GetBrushDefinition(s, out fb, out fp);
                rc.FillBrush.Copy(fb);
                rc.StrokeBrush.Copy(fp);
                fb.Dispose();
                fp.Dispose();
            }
            rc.Bounds = new Rectanglef(x, y, width, height);
        }
        /// <summary>
        /// get brush definition 
        /// </summary>
        /// <param name="s">style definition </param>
        /// <param name="fb"></param>
        /// <param name="fp"></param>
        private bool GetBrushDefinition(string s, out CoreBrush fb, out CorePen fp)
        {
            fb = new CoreBrush(null);
            fp = new CorePen(null);

            var v_b = s.ToLower().Split(new char[] { ';', ':' });
            int v_mode = 0;//fill brush
            for (int i = 0; i < v_b.Length; i+=2)
            {
                switch(v_b[i])
                {
                    case "fill":
                        v_mode = 0;
                        //get definition
                        fb.SetSolidColor (Colorf.Convert(v_b[i + 1]));
                        break ;
                    case "stroke":
                        v_mode = 1;
                        fp.SetSolidColor(Colorf.Convert(v_b[i + 1]));
                        break;
                    case "stroke-width":
                        fp.Width = float.Parse(v_b[i + 1]);
                        break;
                    case "stroke-linejoin":
                        //bevel;
                    case "stroke-miterlimit":
                        //4;
                        break;
                    case "stroke-opacity":
                        //1;
                        break;
                    case "stroke-dasharray":
                        //none
                        break;
                    default :
                        CoreLog.WriteDebug("property ignored " + v_b[i]);
                        break;
                }
            }
            if (v_mode == 1) { 
            }

            return true;
        }
      

        public void VisitRect(XmlReader reader)
        {
            RectangleElement l = new RectangleElement();
            this.Load(l, reader.ReadSubtree());
            this.AddToContainer(l);
        }

        private void AddToContainer(ICoreWorkingObject  l)
        {
            
        }
    }
}
