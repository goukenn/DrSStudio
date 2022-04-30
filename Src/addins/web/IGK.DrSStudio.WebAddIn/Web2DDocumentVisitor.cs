

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Web2DDocumentVisitor.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:Web2DDocumentVisitor.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WebAddIn
{
    using IGK.DrSStudio.WebAddIn.WorkingObject;
    using IGK.ICore.IO;
    using IGK.ICore.WinCore;
    using IGK.ICore.GraphicModels;
    /// <summary>
    /// visitisor of document for saving as drawing 2d document to HtmlDocument
    /// </summary>
    class Web2DDocumentVisitor
    {
        private ICore2DDrawingDocument doc;
        private WebXmlWriter v_writer;
        private string v_directory;
        private bool textStyle; //for text style properties
        private bool noborder;
        private CoreEncoderVisitor m_svgVisitor;

        public Web2DDocumentVisitor(ICore2DDrawingDocument doc, WebXmlWriter v_writer, string v_directory)
        {
            this.doc = doc;
            this.v_writer = v_writer;
            this.v_directory = v_directory;
        }
        public void Visit()
        {
            v_writer.WriteStartElement("div");
            v_writer.WriteAttributeString("id", doc.Id);
            v_writer.WriteAttributeString("class","doc");
            v_writer.WriteAttributeString("style", 
             "position:relative;"+
             "display:inline-block;"+
             "width:"+doc.Width +"px;"+
             "height: "+ doc.Height +"px;"
             );
            foreach (ICore2DDrawingLayer layer in this.doc.Layers)
            {
                if (Accept (layer ))
                this.Visit (layer );        
            }
            v_writer.WriteEndElement();
        }
        public bool  Accept(object obj)
        {
            if (obj == null)
                return false;
            MethodInfo minfo =  GetType().GetMethod("Visit", new Type[] { obj.GetType() });
            return (minfo !=null);
        }
        private void Visit(object obj)
        {
            MethodInfo minfo = GetType().GetMethod("Visit", new Type[] { obj.GetType() });
            minfo.Invoke (this, new object[]{obj });
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            v_writer.WriteStartElement("div");
            v_writer.WriteAttributeString("style", "position:absolute; left:0px; right:0px; bottom:0px; top:0px; overflow:hidden; z-index:"+layer.ZIndex) ;
            foreach (object  item in layer.Elements)
            {
                if (Accept(item))
                    this.Visit(item);
            }
            v_writer.WriteEndElement();
        }
        public void Visit(ICore2DDrawingLayeredElement element)
        {            
            v_writer.WriteStartElement("div");
            this.WriteBaseInfo(element);
            StringBuilder sb = new StringBuilder();
            float w = 1.0f; 
            ICoreBrushOwner c = element as ICoreBrushOwner;
            if (c != null)
            {
                ICorePen p = c.GetBrush(enuBrushMode.Stroke) as ICorePen;
                if (p != null)
                {
                    w = p.Width;
                }
            }
            sb.Append(string.Format("position:absolute;"));
      

            var svg = false;
            var g = this.m_svgVisitor ??
                (this.m_svgVisitor = CoreVisitorManager.GetVisitor("SVG"));
            if (g != null) {
                g.Visit(element);
                g.Flush();
                string t = g.Out;
                if (!string.IsNullOrEmpty(t))
                {
                    svg = true; 
                    var ww = this.doc.Width;
                    var hh = this.doc.Height;
                    writeBound(sb, new Rectanglef(0,0, ww, hh ));
                    v_writer.WriteAttributeString("style", sb.ToString());
                    v_writer.WriteStartElement("svg");
                    v_writer.WriteAttributeString("version", "1.1");
                    v_writer.WriteAttributeString("xmlns", "http://www.w3.org/2000/svg");

                    v_writer.WriteAttributeString("viewBox", $"0 0 {ww} {hh}");
                    v_writer.WriteAttributeString("style", "overflow:visible");
                    v_writer.WriteString(t);
                    v_writer.WriteEndElement();
                }
            }
            if (!svg) {
                generateAndRenderImage(
          element,
          sb,
          element.GetBound(),
          w);
                v_writer.WriteAttributeString("style", sb.ToString());
            }

       
            //get svg
            //ICoreVisitor visit = CoreSystemVisitor.GetVisitors("SVGD");
            //rawing2DFontEncoderVisitor
            //CoreServices.GetApplicationService


            v_writer.WriteEndElement(); 
        }
        public void Visit(WebHtmlLink link)
        {
            v_writer.WriteStartElement("a");
            this.WriteBaseInfo(link);
            v_writer.WriteAttributeString("href", link.Href);
            v_writer.WriteAttributeString("target", link.HrefTarget);
            this.Visit(link.Target);
            v_writer.WriteEndElement();
        }
        public void Visit(WebHtmlText text)
        {
            v_writer.WriteStartElement("div");
            this.WriteBaseInfo(text );
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("position:absolute;"));
            if ((text.Matrix.IsIdentity) &&                
                (text.FillBrush.BrushType == enuBrushType.Solid) &&
                (text.StrokeBrush.BrushType == enuBrushType.Solid) &&
                (text.FillBrush.Colors[0].A == 1.0f) &&
                (text.StrokeBrush.Colors[0].A == 1.0f)
                )
            {
                writeBound(sb, text.Bounds);
                Colorf cl = text.FillBrush.Colors[0];
                sb.Append("color:" + cl.ToString(true)+";");
                sb.Append("font-size:" + text.Font.FontSize + "pt;");
                sb.Append("font-family:'" + text.Font.FontName + "';");
                if (text.AllowShadow)
                {
                    sb.Append(string.Format("text-shadow:{0}px {1}px {2};",
                        Math.Round(text.ShadowProperty.Offset.X),
                        Math.Round(text.ShadowProperty.Offset.Y),
                        text.ShadowProperty.Brush .Colors[0].ToString(true)));
                }
                v_writer.WriteString(text.Text);
            }
            else
            {
                generateAndRenderImage(text , sb, text.GetBound(), 0);
            }
            v_writer.WriteAttributeString("style", sb.ToString());
            v_writer.WriteEndElement();
        }
        public void Visit(ImageElement c) {
            v_writer.WriteStartElement("div");
            StringBuilder sb = new StringBuilder();
            generateAndRenderImage(c, sb, c.GetBound(), 0);
            v_writer.WriteAttributeString("style", sb.ToString());
            v_writer.WriteEndElement();
        }
        public void Visit(TextElement item) {
            v_writer.WriteStartElement("div");
            textStyle = true;
            noborder = true;
            this.WriteBaseInfo(item);
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("position:absolute;"));
            sb.Append("font-size: " + Math.Ceiling(item.Font.FontSize) + "px;");
            sb.Append("font-family: '" + item.Font.FontName + "', arial , sans-serif;");
            BindGlobalStyle(sb, item);
            //BindStyle(sb, item);

            v_writer.WriteAttributeString("style", sb.ToString());

            
            v_writer.WriteString(item.Text);
            v_writer.WriteEndElement();
            textStyle = false;
            noborder = false;
        }
        public void Visit(RectangleElement recElement)
        {
            v_writer.WriteStartElement("div");
            this.WriteBaseInfo(recElement );
            StringBuilder sb = new StringBuilder ();

         
   

            sb.Append(string.Format("position:absolute;"));
            BindGlobalStyle(sb, recElement);

            v_writer.WriteAttributeString ("style", sb.ToString());
            v_writer.WriteEndElement();
        }

        private void BindGlobalStyle(StringBuilder sb, RectangleElement recElement)
        {
            var v_bound = recElement.Bounds;
            var sa = recElement.StrokeBrush.Colors[0].A;
            var v_mat = recElement.Matrix;
            bool v_genBitmap = !recElement.AllowShadow &&
                (recElement.FillBrush.BrushType == enuBrushType.Solid) &&
                (recElement.StrokeBrush.BrushType == enuBrushType.Solid) &&
                (recElement.FillBrush.Colors[0].A == 1.0f) &&
                ((sa == 1.0f) || (sa == 0));

            if (v_mat.IsIdentity)
            {
                if (v_genBitmap)
                {
                    BindStyle(sb, recElement);
                    writeBound(sb, v_bound);
                }
                else
                {
                    generateAndRenderImage(recElement, sb, recElement.GetBound(), recElement.StrokeBrush.Width);
                }
            }
            else
            {
                if (v_genBitmap)
                {
                    BindStyle(sb, recElement);
                    BindTransform(sb, recElement, v_bound);
                    writeBound(sb, v_bound);

                }
                else
                {
                    generateAndRenderImage(recElement, sb, recElement.GetBound(), recElement.StrokeBrush.Width);

                }
            }
        }

        private void BindTransform(StringBuilder sb, Core2DDrawingLayeredElement recElement, Rectanglef v_bound)
        {
            var v_mat = recElement.Matrix; 
            var g = recElement.GetBound();

            Matrix m = new Matrix(v_mat.Elements); 
            var t = (float[])m.Elements.Clone();
            var pt = g.Center - v_bound.Center;

            t[12] = (float)Math.Floor(pt.X);// v_bound.Left;
            t[13] = (float)Math.Floor(pt.Y);// 0;// v_bound.Top;
            sb.Append(GetTransform(t));
        }

        private void BindStyle(StringBuilder sb, ICore2DDualBrushObject recElement)
        {

            string propBg = "background-color";
            if (this.textStyle)
                propBg = "color";
            var sa = recElement.StrokeBrush.Colors[0].A;
            //bind style
            sb.Append($"{propBg} :" + recElement.FillBrush.Colors[0].ToString(true) + ";");
            Colorf cl = recElement.StrokeBrush.Colors[0];
            if (!noborder && (sa == 1.0f))
                sb.Append("border: " +
                    (int)recElement.StrokeBrush.Width + "px " +
                    getBorderStyle(recElement.StrokeBrush) +
                    " " + cl.ToString(true) + ";");
            else
                sb.Append("border: none;");
        }

        private static string GetTransform(float[] elements)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("transform: ");
            sb.Append("matrix(");
            switch (elements.Length)
            {
                case 16:
                    sb.Append($"{elements[0]},");
                    sb.Append($"{elements[1]},");
                    sb.Append($"{elements[4]},");
                    sb.Append($"{elements[5]},");
                    sb.Append($"{elements[12]},");
                    sb.Append($"{elements[13]}");
                    break;
                case 6:
                    sb.Append($"{elements[0]},");
                    sb.Append($"{elements[1]},");
                    sb.Append($"{elements[2]},");
                    sb.Append($"{elements[3]},");
                    sb.Append($"{elements[4]},");
                    sb.Append($"{elements[5]}");
                    break;
            }
            sb.Append(");");
            return sb.ToString();


        }

        private void generateAndRenderImage(ICore2DDrawingLayeredElement obj, 
            StringBuilder sb, Rectanglef rec, float width)
        {
            float w = width;
            float hw = width / 2.0f;
            Rectanglei rc = Rectanglei.Ceiling(rec);
            if (w <= 1)
                rc.Inflate(1, 1);
            else
                rc.Inflate((int)Math.Ceiling (hw), (int)Math.Ceiling(hw));
            ICore2DDrawingShadowElement p = obj as ICore2DDrawingShadowElement;
            if ((p!=null) && p.AllowShadow)
            { 
                rc.Inflate ((int)Math.Ceiling (Math.Abs (p.ShadowProperty.Offset.X)),
                    (int)Math.Ceiling(Math.Abs (p.ShadowProperty.Offset.Y)));
            }
            string id = createResourcesId(obj) + ".png";
            string v_p = "R/"+this.doc.Id +"_Resources/" + id;
            sb.Append("background-image:url('" + v_p + "'); background-repeat:no-repeat; background-position: 0px 0px;  ");
            writeBound(sb, rc);

           // ICoreGraphics device = WinCoreBitmapDeviceVisitor.Create ( 
            using (Bitmap bmp = new Bitmap(rc.Width, rc.Height))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    var device = WinCoreBitmapDeviceVisitor.Create ( g);
                    g.TranslateTransform(-rc.X , -rc.Y );                    
                    obj.Draw (device);
                    g.Flush();
                }
                string path = Path.Combine(v_directory, v_p);
                PathUtils.CreateDir(PathUtils.GetDirectoryName(path));
                bmp.Save(path, ImageFormat.Png);
            }
        }
        private string getBorderStyle(ICorePen corePen)
        {
            string r = "solid";
            switch (corePen.DashStyle)
            {
                case enuDashStyle.Dash:
                    r = "dashed";
                    break;
                case enuDashStyle.DashDot:
                    break;
                case enuDashStyle.DashDotDot:
                    break;
                case enuDashStyle.Dot:
                    r = "dotted";
                    break;
                case enuDashStyle.Solid:                    
                default:
                    break;
            }
            return r;
        }
        private void writeBound(StringBuilder sb, Rectanglef rec)
        {
            //write rectangle bound
            sb.Append(string.Format("left:{0}px;", Math.Round (rec.Left)));
            sb.Append(string.Format("top:{0}px;", Math.Round (rec.Top)));
            sb.Append(string.Format("width:{0}px;", Math.Round (rec.Width)));
            sb.Append(string.Format("height:{0}px;",Math.Round( rec.Height)));
        }
        private string createResourcesId(ICoreWorkingObject recElement)
        {
            return "id_" + recElement.Id;
        }
        private void WriteBaseInfo(ICore2DDrawingLayeredElement e)
        {
            this.v_writer.WriteAttributeString("id", e.Id); 
        }
    }
}

