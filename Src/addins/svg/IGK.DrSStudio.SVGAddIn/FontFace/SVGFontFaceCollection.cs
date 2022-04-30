using IGK.DrSStudio.SVGAddIn;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Xml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SVGAddIn.FontFace
{
    using IGK.ICore;
    using System.Text.RegularExpressions;

    public class SVGFontFaceCollection : IEnumerable 
    {
        private FontGlyphCollection m_glyphs;
        internal SVGFontFaceCollection()
        {
            m_glyphs = new FontGlyphCollection(this);
        }
        public int Count { get { return this.m_glyphs.Count;  } }
        public void Save(string filename)
        {
            string s = this.Render();
            File.WriteAllText (filename , s);
        }

        private string getHorizX(SVGFontGlyph item)
        {
            return null;
        }

        private string getUnicode(int p)
        {
            StringBuilder sb = new StringBuilder();
            string h = p.ToBase(16);
            sb.Append (string.Format ("&#x{0};", h));
            return sb.ToString();
        }

        private string getSVGPathData(CoreGraphicsPath p)
        {
            if (p == null)
                return null;
            return SVGUtils.GetPathDefinition(p);
        }
        internal void AddGlyph(int unitcode, CoreGraphicsPath path) { 

        }
        public IEnumerator GetEnumerator()
        {
            return this.m_glyphs.GetEnumerator();
        }

        class FontGlyphCollection : IEnumerable
        {
            Dictionary<int, SVGFontGlyph> m_glyphs;
            private SVGFontFaceCollection fontFaceCollection;

            public FontGlyphCollection(SVGFontFaceCollection fontFaceCollection)
            {
                this.fontFaceCollection = fontFaceCollection;
                this.m_glyphs = new Dictionary<int, SVGFontGlyph>();
            }
            
            public int Count { get { return this.m_glyphs.Count;  } }

            public IEnumerator GetEnumerator()
            {
                return this.m_glyphs.Values.GetEnumerator();
            }

            internal void Add(int i, SVGFontGlyph glyh)
            {
                if (this.m_glyphs.ContainsKey(i))
                {
                    if (glyh == null)
                        m_glyphs.Remove(i);
                    else
                        this.m_glyphs[i] = glyh;
                }
                else if (glyh != null)
                    m_glyphs.Add(i, glyh);

            }
        }


        [CoreXMLAttribute()]
        [CoreXMLName("id")]
        public string Id { get; set; }
        [CoreXMLAttribute()]
        [CoreXMLName("horiz-adv-x")]
        public int HorizontalAdvX { get; set; }
        public string Render()
        {
            CoreXmlElement svg = CoreXmlElement.CreateXmlNode("svg");
            svg["xmlns"] = SVGConstant.NAMESPACE;
            var option = new CoreXmlSettingOptions();
            option.Indent = true;
            var defs = svg.Add("defs");
            var font = defs.Add("font") as CoreXmlElement;
            font["id"] = this.Id;
            var face = font.Add("font-face") as CoreXmlElement;
            var missing = font.Add("missing-glyph") as CoreXmlElement;
            foreach (SVGFontGlyph item in this)
            {
                var glyph = font.Add("glyph");
                glyph["unicode"] = getUnicode(item.Unicode).ToLower ();
                glyph["d"] = getSVGPathData(item.Path);
                glyph["horiz-adv-x"] = getHorizX(item);
            }
            return svg.RenderXML(option);
        }

        internal void Load(string fontfaceFile)
        {
            var c = CoreXmlElement.LoadFile(fontfaceFile);
            if (c == null)
                return;
            var e =  c.getElementsByTagName("font").CoreGetValue(0) as CoreXmlElement ;
            if (e == null)
                return;
            this.Id = e.GetAttributeValue<string>("id");
            this.HorizontalAdvX = e.GetAttributeValue<int>("horiz-adv-x", 1200);

            var face = c.getElementsByTagName("font-face").CoreGetValue(0) as CoreXmlElement;
            var missing = c.getElementsByTagName("missing-glyph").CoreGetValue(0) as CoreXmlElement; 

            //load glyph
            foreach(CoreXmlElement dt in  c.getElementsByTagName("glyph"))
            {
                if (!dt.HasAttributes) continue;
                int i = getInt( dt.GetAttributeValue<string>("unicode"));

                var g = SVGUtils.GetPathFromDefinition(dt.GetAttributeValue<string>("d"));

                SVGFontGlyph glyh = new SVGFontGlyph(i, g);

                this.m_glyphs.Add(i, glyh);
            }
        }

        private int getInt(string p)
        {
            if (string.IsNullOrEmpty (p))
                return 0;
            if (p.Length == 1)
                return (int)(char)p[0];
            string s = Regex.Match(p, "&#x(?<value>[0-9a-f]+);?", RegexOptions.IgnoreCase).Groups["value"].Value;
            return int.Parse(s);
        }

    }
}
