using IGK.ICore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.PDF
{
    using IGK.ICore;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Imaging;
    using IGK.ICore.Resources;
    using IGK.ICore.Xml;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// graphics of the document
    /// </summary>
    /// <remark >for pratical reason we have to change the transformation to fit our need</remark>
    public class PDFGraphics
    {
        private PDFPage m_Page;
        private PDFStream m_objectStream;
        private PDFObject m_Target;
        private GraphicInfo m_graphicInfo;
        private StringBuilder m_buffer;
        private float m_h;
        private float m_w;
        private PDFDictionary m_states;
        private bool _fontChanged;
        private static Dictionary<string, PDFFontDefinition> sm_definition;
        private bool _rg;
        private bool _RG;
        private float m_MarginLeft;
        private float m_MarginTop;

        public float MarginTop
        {
            get { return m_MarginTop; }
            set
            {
                if (m_MarginTop != value)
                {
                    m_MarginTop = value;
                }
            }
        }
        public float MarginLeft
        {
            get { return m_MarginLeft; }
            set
            {
                if (m_MarginLeft != value)
                {
                    m_MarginLeft = value;
                }
            }
        }

        static PDFGraphics() {
            sm_definition = new Dictionary<string, PDFFontDefinition>();
            //load font defintion
            var s = CoreResources.GetResourceString("pdf_fontdef", MethodInfo.GetCurrentMethod().GetType().Assembly);
            CoreXmlElement l = CoreXmlElement.CreateFromString(s);

            if (l != null) {
                foreach (CoreXmlElement d in l.Childs)
                {
                    PDFFontDefinition def = new PDFFontDefinition();

                    var i = d.getElementsByTagName("cw")[0];
                    int[] cw = new int[i.Childs.Count];
                    for (int x = 0; x < cw.Length; x++)
                    {   
                        CoreXmlElement el =i.Childs[x] as CoreXmlElement;

                        int k = el.GetAttributeValue<int>("key");
                        cw[k] = Convert.ToInt32(i.Childs[x].Value);
                    }
                    def.cw = cw;
                    def.up = Convert.ToInt32 (d.getElementsByTagName("up")[0].Value );
                    def.ut  = Convert.ToInt32(d.getElementsByTagName("ut")[0].Value);
                    sm_definition.Add(d.TagName , def);
                }
            }
        }
        private PDFGraphics()
        {
            m_states = new PDFDictionary();
            m_buffer = new StringBuilder();
            m_graphicInfo = new GraphicInfo();
            m_graphicInfo.fontSize = 12;
            m_graphicInfo.fillColor = Colorf.Black;
            m_graphicInfo.textColor = Colorf.Black;
            m_graphicInfo.drawColor  = Colorf.Black;
            m_graphicInfo.fontDefinition = "helvetica";
            ChangeUnitType = true;
            _fontChanged = true;
            _rg = true;
            _RG = true;
            _initSetting();
            
        }

        private void _initSetting()
        {
            var ci = GetFinfo ();
            if (this._fontChanged)
            {
                if (ci.fontSize != ci.currentFontSize)
                {
                    m_buffer.AppendLine(string.Format("BT /F13 {0} Tf ET",
                        ci.fontSize
                        ));
                    ci.currentFontSize = ci.fontSize;
                }
                this._fontChanged = false;
            }
            if (_rg) {
                //fill color, fill and text color
                this.m_buffer.AppendLine(string.Format("{0} rg", GetColorfString(ci.cfillColor)));
                ci.cfillColor = ci.fillColor;
                _rg = false;
            }
            if (_RG)
            {//stroke color
                this.m_buffer.AppendLine(string.Format("{0} RG", GetColorfString(ci.drawColor)));
                _RG = false;
            }
            this.SetInfo(ci);
        }
        /// <summary>
        /// create a graphics on a cibling page to rende document text
        /// </summary>
        /// <param name="pDFPage"></param>
        /// <returns></returns>
        internal static PDFGraphics CreateGraphics(PDFPage pDFPage)
        {
            if (pDFPage == null)
                return null;

            PDFGraphics v_rGraphics = new PDFGraphics();
            if (pDFPage.Contents == null)
            {
                var p = pDFPage.CreateObject();
                pDFPage.Contents = p;
                v_rGraphics.m_Target = p;
                v_rGraphics.m_objectStream = new PDFStream();
                v_rGraphics.m_Page = pDFPage;
                PDFRectangle b =  !pDFPage.MediaBox .Equals (PDFRectangle.Empty) ? 
                    pDFPage.MediaBox : pDFPage.Document.Pages.MediaBox;
                v_rGraphics.m_h = b.Height;
                v_rGraphics.m_w = b.Width ;
                v_rGraphics.m_MarginLeft = v_rGraphics.getUnit(10);
                v_rGraphics.m_MarginTop = v_rGraphics.getUnit(10);
                v_rGraphics.setXY(0, 0);
                return v_rGraphics;
            }
            return null;
        }


        public void Flush(bool compress=false) {
            var v_length = m_Page.CreateObject();
            m_Target.Add(new PDFDictionary().Add(PDFNames.Length, v_length));
            m_Target.Add(m_objectStream);
            var obj = m_Target.CreateObject();
            var font = m_Target.CreateObject();
            font.Add(new PDFDictionary().Add(PDFNames.Type, PDFNames.Font).
                Add(PDFNames.BaseFont, "/Helvetica")
                .Add(PDFNames.Subtype, "/Type1")
                .Add(PDFNames.Encoding, PDFNames.WinAnsiEncoding)
                );
            obj.Add(new PDFDictionary().Add(PDFNames.Font, font));
            m_Page.Dictionary.Add(PDFNames.Resources, 
                this.m_Page.Document .Resources 
//                new PDFDictionary().Add(PDFNames.Font, new PDFDictionary().Add(PDFNames.F13, font)
                );
            this.m_Page.Document.Resources.Dictionary.Add(PDFNames.Font, new PDFDictionary().Add(PDFNames.F13, font));
            this.m_objectStream.Value = this.m_buffer.ToString();
            v_length.Add(new PDFValue(m_objectStream.Value.Length));
        }
        //drawing function

        public void drawCText(int width, int height, string text, int border = 0, int linefeed = 1,
            string Alignment = "L")
        {
            _initSetting();
            var ci = GetFinfo();
            //Font ft = GetFont(ci);
            float dx = 0;
            var s = this.MeasureString(text);
            //s.Width = Math.Max(width, s.Width);

            //StringFormat sf = new StringFormat();
            //sf.LineAlignment = StringAlignment.Center;
            switch (Alignment.ToLower())
            {
                case "r":
                    dx = getUnit( width) - s.Width;
                    break;
                case "l":
                    dx = 0;
                    break;
                case "c":
                    dx = (getUnit( width) - s.Width)/ 2.0f;
                    break;
                default:
                    break;
            }
            //var s = g.MeasureString(text, ft);
            //s.Width = Math.Max(x, s.Width);
            //g.DrawString(text, ft,
            //    CoreBrushRegister.GetBrush<Brush>(ci.textColor),
            //    ci.x, ci.y, s.Width, height, sf);
            this._addText(getUnit(ci.x) + dx, getUnit(ci.y + height - (height /2.0f) ), text);
            //drawRect(ci.x +dx/this.getKUnit() , ci.y, width, height);
            if (border == 1)
            {
                drawRect(ci.x, ci.y, width, height);
               // drawRect(10, 10, 190, 197 - 20);
            }


            if (linefeed == 1)
            {
                ci.y = height + ci.y;
                ci.x = ci.X;
            }
            else
                ci.x += width;
            SetInfo( ci);
        }

        private Size2f MeasureString(string text)
        {
            Size2f s = new Size2f();
            var cw = this.getFontDefinition().cw;
            var up = this.getFontDefinition().up;
            float w = 0;
            var ci = GetFinfo ();
            for (int i = 0; i < text.Length; i++)
			{
                w += cw[(byte)text[i]];
			}
	        w = w * ci.fontSize/1000.0f;
            s.Width = w;
            s.Height = (-up / 1000.0f) * ((PDFUnit)(ci.fontSize +  "mm")).GetPoint();
            return s;
        }

        private PDFFontDefinition getFontDefinition()
        {
            var ci = GetFinfo ();
            if(sm_definition.ContainsKey (ci.fontDefinition ))
                return sm_definition[ci.fontDefinition];
            return PDFFontDefinition.Empty;
        }

        private void _addText(float x, float y, string text)
        {
            if (string.IsNullOrEmpty(text))
                return;
            StringBuilder sb = new StringBuilder();
            var v_kr = this.getFontDefinition();
            float up = v_kr.up;
            float down = v_kr.ut;
            var r = this.GetFinfo();
            _rg =  !r.cfillColor.Equals (r.textColor);
            if (_rg)
                r.cfillColor = r.textColor;
            var k = getKUnit();
            
            float h = this.m_h - y;            
            sb.Append(string.Format ("BT {0} {1} Td ({2}) Tj ET ",
                x,h, text.Replace ("(","\\(").Replace (")","\\)")
                ));
            this.SetInfo (r);
            this._addBuffer(sb.ToString());
            
        }

        private void save() {
            this.m_buffer.Append("q ");
        }
        private void restore() {
            this.m_buffer.Append("Q ");
        }
        public void setXY(float x, float y)
        {
            GraphicInfo c = GetFinfo();
            //x = getUnit(x);// +MarginLeft;
            //y = getUnit(y);// +MarginTop;
            c.x = x;
            c.X = x;
            c.y = y;
            c.Y = y;
            SetInfo( c);
        }
        private GraphicInfo GetFinfo()
        {
            return m_graphicInfo;

        }
      
        private  void SetInfo(GraphicInfo c)
        {
            this.m_graphicInfo = c;
        }
#pragma warning disable 649
        struct GraphicInfo
        {
            internal Colorf fillColor;
            internal Colorf textColor;
            internal Colorf drawColor;
            internal Colorf cfillColor;
            internal float x;
            internal float y;
            internal float fontSize;
            internal string fontDefinition;
            internal float X;//saved x
            internal float Y;//saved y 
            public float currentFontSize;
            public float width;

            public bool RenderBorder() {
                return (this.drawColor.A > 0.0f);
            }
            public bool RenderFill() {
                return (this.cfillColor.A > 0.0f);
            }
            public bool RenderText() {
                return (this.textColor.A > 0.0f);
            }
        }


        public float getUnit(float d, float k=1.0f){            
            switch (this.m_Page.Document.Unit)
            {
                case enuPDFPageUnit.mm:
                    return ((PDFUnit)(d + "mm")).GetPoint() * k;                    
                case enuPDFPageUnit.pt:
                    return ((PDFUnit)(d + "pt")).GetPoint() *k;                  
                case enuPDFPageUnit.cm:
                    return ((PDFUnit)(d + "cm")).GetPoint() *k;                    
                default:
                    break;
            }
            return d * k;
        }

        private float getKUnit()
        {
           switch (this.m_Page.Document.Unit)
            {
                case enuPDFPageUnit.mm:
                    return (72.0f)/25.4f ;
                case enuPDFPageUnit.pt:
                    return 1;
                case enuPDFPageUnit.cm:
                    return (72.0f)/2.54f ;     
               case enuPDFPageUnit.inch:
                    break;
           }
            return 1.0f;
        }
       
        private void setFColor(Colorf colorf)
        {
             var gi = GetFinfo();
            if (!gi.fillColor.Equals (colorf))
            {
                gi.fillColor = colorf;
                gi.cfillColor = colorf;
                _rg = true;
                this.SetInfo(gi);
            }
        }
        internal void setTextColor(Colorf colorf)
        {
            var gi = GetFinfo();
            if (!gi.textColor.Equals (colorf))
            {
                gi.textColor = colorf;
                _rg = true;
                this.SetInfo(gi);
            }
        }
        public void setStrokeColor(Colorf colorf)
        {
            var gi = GetFinfo();
            if (!gi.drawColor.Equals (colorf))
            {
                gi.drawColor = colorf;
                _RG = true;
                this.SetInfo(gi);
            }            
        }
      
        private string GetColorfString(Colorf colorf)
        {
            return string.Format("{0} {1} {2}", colorf.R, colorf.G, colorf.B);
        }
        internal void fillRect(Colorf colorf, int x, int y, int width, int height)
        {
            this.setFColor(colorf);
            this._addBuffer(string.Format("{0} {1} {2} {3} re f",
                getUnit(x), this.Height - getUnit(y) - getUnit(height),
                getUnit(width), 
                getUnit(height)));
        }

        internal void fillRect(float x, float y, float width, float height)
        {
            var ci = GetFinfo();
            _rg = !ci.cfillColor.Equals (ci.fillColor);
            if (_rg) {
                ci.cfillColor = ci.fillColor;
                SetInfo(ci);
            }
            this._addBuffer(string.Format("{0} {1} {2} {3} re f",
               getUnit(x), this.Height - getUnit(y) - getUnit(height),
               getUnit(width),
               getUnit(height)));
        }

        internal void drawText(string text, float x, float y)
        {
            _initSetting();
            var ci = GetFinfo();
            var _cx = getUnit(x);
            var _cy = getUnit(y);
            _addText( _cx ,_cy, text);
            ci.x += _cx;
            ci.y += _cy;
            SetInfo(ci);
        }
        internal void drawRect(Colorf colorf, int x, int y, int width, int height)
        {
            this.setStrokeColor(colorf);

            this._addBuffer(string.Format("{0} {1} {2} {3} re s",
                getUnit(x), this.Height - getUnit(y) - getUnit(height),
                getUnit(width),
                getUnit(height)));
        }
        internal void drawRect( float x, float y, float width, float height)
        {
            var gi = GetFinfo();
            this._addBuffer(string.Format("{0} {1} {2} {3} re s",
                getUnit(x), this.Height - getUnit(y) - getUnit(height),
                getUnit(width),
                getUnit(height)));
        }
        //append to to buffer with line field
        private void _addBuffer(string p)
        {
            this._initSetting();            
            this.m_buffer.AppendLine(p);
        }



        public float Height { get {
            return this.m_h;
        } }
        public float Width
        {
            get
            {
                return this.m_w;
            }
        }

        public void setFontSize(float p)
        {
            var gi = GetFinfo();
            if (gi.fontSize != p)
            {
                this._fontChanged =true ;
                gi.fontSize = p;
                this.SetInfo(gi);
            }
        }

        internal void setStrokeWidth(float p)
        {
            var gi = GetFinfo();
            if (gi.width != p) {
                gi.width = p;
                this.m_buffer .AppendLine (string.Format ("{0} w", getUnit (p)));
            }
        }

        internal void settColorw(string p)
        {
            this.setTextColor(Colorf.FromString(p));
        }
        /// <summary>
        /// set draw collor
        /// </summary>
        /// <param name="p"></param>
        internal void setdColorw(string p)
        {
            this.setStrokeColor(Colorf.FromString(p));
        }
        /// <summary>
        /// set fill color
        /// </summary>
        /// <param name="p"></param>
        internal void setfColorw(string p)
        {
            this.setFColor(Colorf.FromString(p));
        }

        public float getY()
        {
            var ci = this.GetFinfo();
            return ci.y;
        }
        public  float getX()
        {
            var ci = this.GetFinfo();
            return ci.x;
        }


        //futur function
        //add image
        //visitor image function


        public void drawInlineImage(string filename, int width, int height) {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine (string.Format("BI"));
            //begin inmage

            sb.AppendLine(string.Format("ID"));
            //data image
            sb.AppendLine(string.Format("EI"));

            this.m_buffer.Append(sb.ToString());
        }
        public void drawImage(ICoreBitmap bmp, int x, int y)
        {
            if (bmp == null)
                return;
            MemoryStream mem = new MemoryStream();
            bmp.Save(mem, CoreBitmapFormat.JPEG);
            mem.Seek(0, SeekOrigin.Begin);
            byte[] tab = new byte[mem.Length];
            mem.Read (tab, 0, tab .Length );
            mem.Dispose();
            drawImage(tab,bmp.Width, bmp.Height , x, y);

        }
        public void drawImage(string filename, int x, int y)
        {
            var bmp = CoreApplicationManager.Application.ResourcesManager.CreateBitmapFromFile(filename);
            drawImage(File.ReadAllBytes (filename), bmp.Width, bmp.Height, x, y);
        }
        public void drawImage(byte[] data, int width, int height,  int x, int y)
        {

            var p = this.m_Page.CreateObject<PDFImageObject>("ImageObject");
            p.Value = Encoding.Default.GetString(data);// File.ReadAllBytes(filename));
            p.Width = width;
            p.Height = height;
            p.ColorSpace = PDFNames.DeviceRGB;
            p.BitsPerComponent = 8;
            //jpg
            p.Filter = PDFNames.DCTDecode;

            //png
            //p.Colors = 3;
            //p.Predictor = 15;
            //p.Columns = bmp.Width;
            float w = ((PDFUnit)(p.Width + "px")).GetPoint();
            float h = ((PDFUnit)(p.Height + "px")).GetPoint();
            this.m_buffer.AppendLine(string.Format("q {1} 0 0 {2} {3} cm {0} Do Q",
                p.Name,
                w,
                h,
                string.Format("{0} {1}",
                    getUnit(x),
                    this.Height - getUnit(y) - h
                    )
                ));
            p.Length = p.Value.Length;            
        }

        public void Draw(Core2DDrawingLayeredElement element)
        {
            var p = element.GetPath();
            save();
            
            __initGraphicsElement(element);
            _initSetting();
            //StringBuilder sb = new StringBuilder();
            var def = p.GetAllDefinition();
            var closed = false;
            if (ChangeUnitType)
            {
                for (int i = 0; i < def.Points.Length; i++)
                {
                    var pt = def.Points[i];
                    pt.X = ((PDFUnit)(pt.X + "px")).GetMm();
                    pt.Y = ((PDFUnit)(pt.Y + "px")).GetMm();
                    def.Points[i] = pt;
                }
            }
            for (int i = 0; i < def.Points.Length; i++)
            {
                var pt = def.Points [i];
                if ((i == 0)|| closed )
                {
                    move_to(pt.X, pt.Y);
                    closed = false;
                }
                else {
                    var v_t = def.Types[i];
                    closed = (v_t & 0x80) == 0x80;
                    if ((v_t & 3) == 3)
                        v_t = 3;
                    else if ((v_t & 1 ) == 1)
                        v_t = 1;
                    switch (v_t)
                    {
                        case 1:
                            add_line(pt.X, pt.Y);
                            break;
                        case 3:
                            add_bezier(pt.X, pt.Y,
                                def.Points [i + 1].X, def.Points[i + 1].Y,
                                def.Points [i + 2].X, def.Points[i + 2].Y);
                            closed = (def.Types [i + 2] & 0x80) == 0x80 ;
                            i += 2;
                            break;
                        default:
                            break;
                    }
                    if (closed) {
                        this.m_buffer.Append(" h ");
                    }
                }
            }
            //b fill , stroke using widing rule
            //b* fill , stroke using widing rule
            //B fill, stroke using event-odd
            //f fill , using widing rule
            var gi = GetFinfo ();
            string mode = string.Empty;
            if (gi.RenderBorder() && gi.RenderFill())
                mode = p.FillMode == enuFillMode.Winding ? " b " : " b* ";
            else {
                if (gi.RenderBorder())
                {
                    mode = " S ";
                }
                if (gi.RenderFill())
                {
                    mode = p.FillMode == enuFillMode.Winding ? " f " : " f* ";
                }
            }
            this.m_buffer.Append(string.Format (" {0} ", mode));
            this.restore();
            this.m_buffer.AppendLine();
        }

        private void __initGraphicsElement(Core2DDrawingLayeredElement element)
        {
            SetUpBrush(element as ICoreBrushOwner);
         
        }

        private void __setupMatrix(Core2DDrawingLayeredElement element)
        {
            var v_m = element.GetMatrix();
            if (!v_m.IsIdentity)
            {
                //transform this w
            }
        }

        private void SetUpBrush(ICoreBrushOwner item)
        {
            if (item == null)
                return ;
            _fillBrush(item.GetBrush(enuBrushMode.Fill));
            _stokeBrush(item.GetBrush(enuBrushMode.Stroke)as ICorePen );
        }

        private void _stokeBrush(ICorePen brush)
        {
            if (brush == null)
                return ;
            this.setStrokeWidth(((PDFUnit )(brush.Width+"px")).GetPoint());
            switch (brush.BrushType)
            {
                case enuBrushType.Solid:
                    this.setStrokeColor(brush.Colors[0]);
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
        }

        private void _fillBrush(ICoreBrush brush)
        {
            switch (brush.BrushType)
            {
                case enuBrushType.Solid:
                    this.setFColor(brush.Colors[0]);
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
        }


        private void add_line(float x, float y)
        {
            this.m_buffer.Append(string.Format("{0} {1} l ", getUnit(x), this.Height - getUnit(y)));
        }
        private void move_to(float x, float  y) {
            this.m_buffer.Append(string.Format("{0} {1} m ", getUnit(x), this.Height - getUnit(y)));
        }
        private void add_bezier(float c1x, float c1y, float c2x, float c2y, float x, float y)
        {
            this.m_buffer.Append(string.Format("{0} {1} ", getUnit(c1x), this.Height - getUnit(c1y)));
            this.m_buffer.Append(string.Format("{0} {1} ", getUnit(c2x), this.Height - getUnit(c2y)));
            this.m_buffer.Append(string.Format("{0} {1} c ", getUnit(x), this.Height - getUnit(y)));
        }

        public void Draw(ICore2DDrawingDocument doc)
        {
            foreach (var item in doc.Layers)
            {
                Draw(item);
            }
        }
        public void Draw(ICore2DDrawingLayer layer)
        {
            foreach (Core2DDrawingLayeredElement  item in layer.Elements)
            {
                if (item.View)
                {
                    Draw((object)item);
                }
            }
        }
        private void Draw(object item)
        {
            MethodInfo.GetCurrentMethod().Visit(this, item);
        }

        public bool ChangeUnitType { get; set; }
    }
}
