

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebUtils.cs
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
file:WebUtils.cs
*/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using IGK.ICore.IO;
using IGK.ICore.WinCore;
using IGK.ICore.Xml;
namespace IGK.DrSStudio.WebAddIn
{
    static class WebUtils 
    {
        /// <summary>
        /// export this to html document
        /// </summary>
        /// <param name="p">file name</param>
        /// <param name="core2DDrawingDocument"></param>
        internal static bool ExportToHtmlDocument(string p, params ICore2DDrawingDocument[] documents)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting .Indent = true ;
            v_setting.OmitXmlDeclaration = true;
            WebXmlWriter v_writer = WebXmlWriter.Create(sb, v_setting);
            string v_directory = PathUtils.GetDirectoryName(p);
            //v_writer.WriteRaw("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Strict//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd\" />\n");
            v_writer.WriteDocType();//"html", "-//W3C//DTD XHTML 1.0 Strict//EN", "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd", null);
            v_writer.WriteStartElement("html");
            v_writer.WriteStartElement("head");
            v_writer.WriteStartElement("meta");
            
            v_writer.WriteAttributeString("http-equiv", "Content-Type");
            v_writer.WriteAttributeString("content", "text/html; charset=utf-8");
            v_writer.WriteEndElement();


            v_writer.WriteStartElement("style");
            v_writer.WriteAttributeString("type", "text/css");
            v_writer.WriteString("*{margin:0px; padding:0px}");
            v_writer.WriteEndElement();

            v_writer.WriteEndElement();
            v_writer.WriteStartElement("body");
            v_writer.WriteAttributeString("style", "margin:0px; padding:0px; text-align:center; width:100%; height:100%;");
            foreach (ICore2DDrawingDocument  doc in documents )
            {
                new Web2DDocumentVisitor(doc, v_writer, v_directory).Visit();
            }
            v_writer.WriteEndElement();
            v_writer.WriteEndElement();
            v_writer.Flush();
            try
            {
                System.IO.File.WriteAllText(p, sb.ToString());
                return true;
            }
            catch {
                 CoreLog.WriteDebug("Error when writing to file : "+ p);
            }
            return false;
        }
        #region  WebUtils
          public const string GKDATA = "GKDSData";
            public const string WEBITEM = "WEB";
            public const string PRESENTATION = "Presentation";
			const string RES_NAME = "doc_res{0}";
            /// <summary>
            /// convert to base 64 string
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string ConvertToBase64(string str)
            {
                byte[] t = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
                return Convert.ToBase64String(t);
            }
            /// <summary>
            /// get from base 64 string
            /// </summary>
            /// <param name="str"></param>
            /// <returns></returns>
            public static string ConvertFromBase64(string str)
            {
                byte[] t = Convert.FromBase64String(str);
                return System.Text.ASCIIEncoding.ASCII.GetString(t);
            }
            public static string BuildFlyersDocument(string outDir, Core2DDrawingDocumentBase[] documents)
            {
                if ((documents == null) || (documents.Length == 0)) return null;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("<html>");
                sb.AppendLine("<head>");
                sb.AppendLine("<style type=\"text/css\"></style>");
                sb.AppendLine(@"<script type=""text/javascript"">
window.onload = animate;
var index = 0;
var tab  = new Array();
var interval= 1000;
var count = " + documents.Length + @";
");
                for (int i = 0; i < documents.Length; i++)
                {
                    sb.AppendLine("tab[" + i + "] = \"" + documents[i].Id + "\";");
                }
                sb.AppendLine(@"function animate()
{
	for(i = 0 ; i < count ; i++)
	{
		h = document.getElementById(tab[i]);
		h.style.display = ""none"";
	}
	h = document.getElementById(tab[0]);
	h.style.display = ""block"";
	window.setTimeout(""update()"", interval);	
}
function update()
{
	h = document.getElementById(tab[index]);
	h.style.display = ""none"";	
	index ++;
	if ((index % count)==0)
	{
		index = 0;
	}
	h = document.getElementById(tab[index]);
	h.style.display = ""block"";	
	window.setTimeout(""update()"", interval);
}
</script>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                foreach (Core2DDrawingDocumentBase item in documents)
                {
                    sb.AppendLine(GetDocumentInfo(outDir, item));
                }
                sb.AppendLine("</body>");
                sb.AppendLine("</head>");
                return sb.ToString();
            }
            /// <summary>
            /// used for mail to send single div
            /// </summary>
            /// <param name="document"></param>
            /// <param name="ImageResources"></param>
            /// <returns></returns>
            public static string BuildMailSingleDiv(Core2DDrawingDocumentBase document, ref string ImageResources)
            {
                if (document == null) return null;
                StringBuilder sb = new StringBuilder();
                Bitmap bmp = WinCoreBitmapOperation.GetGdiBitmap(document,
                    CoreScreen.DpiX, CoreScreen.DpiY);
                ImageResources = WinCoreBitmapOperation.BitmapToBase64String(bmp, 1);
                bmp.Dispose();
                sb.AppendLine("<html>");
                sb.AppendLine("<head>");
                sb.AppendLine("<style type=\"text/css\"></style>");
                sb.AppendLine("</head>");
                sb.AppendLine("<body>");
                sb.AppendLine(string.Format("<div id=\"" + document.Id + "\" style=\"width:{0}px; height:{1}px; position:relative; border:1px solid black; background-image: url(\"cid:$ref\");\">", document.Width, document.Height));
                //sb.AppendLine(string.Format ("<img src=\"cid:$ref\" width=\"{0}px\" height=\"{1}\" />", document.Width, document.Height ));
                sb.AppendLine("</div>testing");
                sb.AppendLine("</body>");
                sb.AppendLine("</head>");
                return sb.ToString();
            }
            public static System.Net.Mail.MailMessage BuildMailMultiDiv(params ICore2DDrawingDocument[] documents)
            {
                System.Net.Mail.MailMessage m = new System.Net.Mail.MailMessage();
                Dictionary<string, System.IO.MemoryStream> v_mem = new Dictionary<string, MemoryStream>();
                StringBuilder sb = new StringBuilder();
                //build hmtl document content with cid as image resources
                sb.AppendLine("<html>"); ;
                sb.AppendLine("<body>");
                int v_rscounter = 0;
                foreach (ICore2DDrawingDocument doc in documents)
                {
                    sb.AppendLine(string.Format("<div id=\"" + doc.Id + "\" style=\"width:{0}px; height:{1}px; position:relative;\">", doc.Width, doc.Height));
                    //embed layer
                    int v_zindex = 0;
                    int v_itemindex = 0;
                    foreach (ICore2DDrawingLayer layer in doc.Layers)
                    {
                        sb.AppendLine(string.Format("<div id=\"" + layer.Id + "\" style=\"width:{0}px; height:{1}px; position:relative; z-index:{2}\">", doc.Width, doc.Height, v_zindex));
                        v_itemindex = 0;
                        foreach (ICore2DDrawingLayeredElement item in layer.Elements)
                        {
                            if (item is IWebHtmlElement)
                            {
                                sb.AppendLine((item as IWebHtmlElement).GetDefinition());
                            }
                            else
                            {
                                Rectanglei rc = Rectanglef.Round(item.GetBound());
                                ICoreBitmap bmp = WinCoreBitmapOperation.GetBitmap(item,
                                        CoreScreen.DpiX,
                                        CoreScreen.DpiY);
                                //ImageResources = WinCoreBitmapOperation.ImageToBase64String(bmp, 1);
                                string linkname = string.Format(RES_NAME, v_rscounter)
                                  ;
                                Bitmap pb = bmp.ToGdiBitmap();
                                byte[] v_tab = WinCoreBitmapOperation.GetImageData(pb, 1);
                                pb.Dispose();

                                MemoryStream mem = new MemoryStream();
                                mem.Write(v_tab, 0, v_tab.Length);
                                mem.Position = 0;
                                v_mem.Add(linkname, mem);
                                sb.AppendLine(string.Format("<div style=\"left: {0}px; top:{1}px; width:{2}px; height:{3}px; position:absolute;z-index:" + v_itemindex + ";\" >",
                                     rc.X,
                                    rc.Y,
                                    bmp.Width,
                                    bmp.Height));
                                sb.AppendLine(string.Format("<img id=\"" + item.Id + "\"  src=\"cid:" + linkname + "\"  ></img>"));
                                sb.AppendLine("</div>");
                                v_rscounter++;
                                v_itemindex++;
                                bmp.Dispose();
                            }
                        }
                        sb.AppendLine(string.Format("</div>"));
                    }
                    sb.AppendLine(string.Format("</div>"));
                }
                sb.AppendLine("</body>");
                sb.AppendLine("</html>"); ;
                m.IsBodyHtml = true;
                m.AlternateViews.Add(System.Net.Mail.AlternateView.CreateAlternateViewFromString(sb.ToString(), null, "text/html"));
                // m.Body = sb.ToString();
                System.Net.Mail.LinkedResource v_link = null;
                //  System.Net.Mail.Attachment v_attach = null;
                foreach (KeyValuePair<string, MemoryStream> mem in v_mem)
                {
                    //v_attach = new System.Net.Mail.Attachment (mem.Value , mem.Key, "image/png" );
                    v_link = new System.Net.Mail.LinkedResource(mem.Value, "image/png");
                    v_link.ContentId = mem.Key;
                    m.AlternateViews[0].LinkedResources.Add(v_link);
                    //m.Attachments.Add(v_attach);
                }
                return m;
            }
            public static string BuildDocument(string dir, Core2DDrawingDocumentBase document, bool encapsulate)
            {
                if (document == null) return null;

                var div = CoreXmlElement.CreateXmlNode("html");
                div["style"] = "margin:0px;padding:0px";
                
                var b = div.Add("body");
                b["style"] = "margin:0px;padding:0px";
                b.LoadString(GetDocumentInfo(dir, document));
                return div.RenderXML(null);

                //if (encapsulate)
                //{
                //    sb.AppendLine("<html>"); 
                    
                //    sb.AppendLine("<body>");
                //}
                //     sb.AppendLine(GetDocumentInfo(dir, document));
                //if (encapsulate)
                //{
                //    sb.AppendLine("</body>");
                //    sb.AppendLine("</html>");
                //}
                //return sb.ToString();
            }
            private static string GetDocumentInfo(string outDir, Core2DDrawingDocumentBase document)
            {
                StringBuilder sb = new StringBuilder();
                //var div = IGK.ICore.Xml.CoreXmlWebElement.CreateXmlNode("div");
                //div["id"] = document.Id;
                //div["style"] = string.Format("position:relative; overflow:hidden; width:{0}px;height:{1}px", document.Width, document.Height);

                //for document
                sb.AppendLine(string.Format("<div id=\"" + document.Id + "\" style=\"position:relative; overflow:hidden; width:{0}px; height:{1}px; \" >", (int)document.Width, (int)document.Height));
                Rectanglef v_docrc = new Rectanglef(0, 0, document.Width, document.Height);
                foreach (ICore2DDrawingLayer layer in document.Layers)
                {
                    sb.AppendLine(string.Format("<div id=\"" + layer.Id + "\" style=\"width:{0}px; height:{1}px; z-index:{2}; position:absolute;\">", document.Width, document.Height, layer.ZIndex));
                    foreach (ICore2DDrawingLayeredElement l in layer.Elements)
                    {
                        Rectanglei rc = Rectanglef.Round(l.GetBound());
                        if ((rc.Width <= 0) || (rc.Height <= 0))
                            continue;
                        Rectanglei rc_g =  Rectanglef.Round(rc);
                        Bitmap bmp = new Bitmap(rc_g.Width, rc_g.Height);
                        Graphics g = Graphics.FromImage(bmp);
                        GraphicsState s1 = g.Save();
                        //remove translation
                        g.TranslateTransform(
                            -rc.X,
                            -rc.Y,
                        MatrixOrder.Prepend);

                        GraphicsState s2 = g.Save();
                        g.ResetTransform();
                        g.TranslateTransform(
                            -rc_g.X - 0.5f,
                            -rc_g.Y - 0.5f,
                        MatrixOrder.Prepend);
                        var device = WinCoreBitmapDeviceVisitor.Create(g);  
                        l.Draw (device);
                        g.Restore(s2);
                        g.Restore(s1);
                        g.Flush();
                        if (l is IWebHtmlElement)
                        {
                            sb.AppendLine((l as IWebHtmlElement).GetDefinition());//WinCoreBitmapOperation.BitmapToBase64String(bmp, 1)));
                            g.Dispose();
                            bmp.Dispose();
                            continue;
                        }
                        string bmpInfo = WinCoreBitmapOperation.BitmapToBase64String(bmp, 1);
                        string style = string.Empty;
                        if (bmpInfo.Length < 32000)
                        {
                            style = string.Format("width:{0}px; height:{1}px; position:absolute;background-repeat:no-repeat; background-image: url('data:image/png;base64,{2}');",
          bmp.Width,
          bmp.Height,
          bmpInfo);
                        }
                        else
                        {
                            try
                            {
                                string file = outDir + Path.DirectorySeparatorChar + l.Id + ".png";
                                File.WriteAllBytes(file, Convert.FromBase64String(bmpInfo));
                                style = string.Format("width:{0}px; height:{1}px; position:absolute; background-repeat:no-repeat; background-image: url('{2}');",
                                bmp.Width,
                                bmp.Height,
                                Path.GetFileName(file));
                            }
                            catch
                            {
                            }
                        }
                        style += string.Format("left:{0}px; top:{1}px;", rc_g.X, rc_g.Y);
                        sb.AppendLine(
                            string.Format("<div id=\"" + l.Id + "\" style=\"{0}\"></div>",
                            style
                            ));
                        g.Dispose();
                        bmp.Dispose();
                    }
                    sb.AppendLine("</div>");
                }
                sb.AppendLine("</div>");
                return sb.ToString();
            }
            /// <summary>
            /// build a web gkds ressources files . to include into a web file
            /// </summary>
            /// <param name="filename"></param>
            /// <param name="projectInfo"></param>
            /// <param name="document"></param>
            public static void BuildWebResources(string filename, ICoreProject projectInfo, Core2DDrawingDocumentBase document)
            {
                XmlWriter v_xwriter = null;
                CoreEncoder v_codec = CoreEncoder.Instance;
                v_codec.EmbedBitmap = true;
                StringBuilder sb = new StringBuilder();
                //get document 
                sb.Append(
                    WebUtils.BuildDocument(PathUtils.GetDirectoryName(filename),
                    document,
                    false));
                v_xwriter = XmlWriter.Create(filename);
                v_xwriter.WriteStartElement(WEBITEM);
                v_xwriter.WriteStartElement(PRESENTATION);
                v_xwriter.WriteCData(sb.ToString());
                v_xwriter.WriteEndElement();
                v_xwriter.WriteStartElement(GKDATA);
                MemoryStream stream = new MemoryStream();
                v_codec.Save(stream, projectInfo, new ICore2DDrawingDocument[] { document });
                stream.Position = 0;
                StreamReader sr = new StreamReader(stream);
                v_xwriter.WriteCData(sr.ReadToEnd());
                stream.Close();
                stream = null;//free handle
                v_xwriter.WriteEndElement();
                v_xwriter.WriteEndElement();
                v_xwriter.Close();
            }
            internal static void CopyDir(string sourcedir, string destination)
            {
                DirectoryInfo dir = new DirectoryInfo(sourcedir);
                if (!dir.Exists)
                    return;
                string v_endir = Environment.CurrentDirectory;
                Environment.CurrentDirectory = destination;
                foreach (DirectoryInfo d in dir.GetDirectories())
                {
                    Directory.CreateDirectory(d.Name);
                    CopyDir(d.FullName, Path.GetFullPath(destination + Path.DirectorySeparatorChar + d.Name));
                }
                foreach (FileInfo item in dir.GetFiles())
                {
                    item.CopyTo(destination + Path.DirectorySeparatorChar + item.Name, true);
                }
                //restore dir
                Environment.CurrentDirectory = v_endir;
            }
        #endregion
    }
}

