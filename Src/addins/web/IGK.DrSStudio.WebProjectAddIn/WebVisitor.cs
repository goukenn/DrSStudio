

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebVisitor.cs
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
file:WebVisitor.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
namespace IGK.DrSStudio.WebProjectAddIn
{
    using IGK.ICore.IO;
    using IGK.ICore.Xml;
    using IGK.DrSStudio.WebProjectAddIn.WorkingObjects;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Imaging;
    using System.Reflection;
    /// <summary>
    /// represent a base html visitor
    /// </summary>
    public class WebVisitor : IWebVisitor, IDisposable, ICoreWorkingConfigurableObject
    {
        private HtmlDocument m_rootNode;  //m_rootnode
        private CoreXmlElement m_currentNode; //editable node
        private string m_TemporyFolder;
        public string OutputDir { get; set; }
        private enuExportToHTMLType m_ExportType;
        /// <summary>
        /// get export type
        /// </summary>
        public enuExportToHTMLType ExportType
        {
            get { return m_ExportType; }
            set
            {
                if (m_ExportType != value)
                {
                    m_ExportType = value;
                }
            }
        }
        public WebVisitor()
        {
            this.m_rootNode = new HtmlDocument();
            this.m_currentNode = this.m_rootNode.Body;
            this.m_TemporyFolder = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(this.m_TemporyFolder);
        }
        public void Visit(IWebVisitable visitable)
        {


            CoreXmlCommentElement comment = new CoreXmlCommentElement
            {
                Value = "visitable : " + visitable
            };
            this.m_currentNode.AddChild(comment);
        }
        public void Visit(ImageElement imgElement)
        {
            CoreXmlElementBase l = CoreXmlElementBase.CreateXmlNode("img");
            String v_bckdir = Environment.CurrentDirectory;
            if (!string.IsNullOrEmpty (this.OutputDir ) && Directory.Exists (this.OutputDir ))
                Environment.CurrentDirectory = this.OutputDir;
            if (PathUtils.CreateDir("Res"))
            {
                string v_fname = string.Format("Res/{1}.png", this.OutputDir, Path.GetFileNameWithoutExtension(Path.GetTempFileName()));
                imgElement.Bitmap.Save(v_fname, CoreBitmapFormat.Png);
                l["src"] = v_fname;
            }
            this.m_currentNode.AddChild(l);
            Environment.CurrentDirectory = v_bckdir;
        }
        public void Visit(ICore2DDrawingDocument[] documents)
        {
            for (int i = 0; i < documents.Length; i++)
            {
                new WebVisitableDocument(documents[i]).Accept(this);
            }
        }
        public void Visit(ICore2DDrawingLayer layer)
        {
            CoreXmlElement v_div = CoreXmlWebElement.CreateXmlNode("div") as CoreXmlElement;
            v_div["id"] = layer.Id;
            v_div["style"] = "position:relative; width: 100%; height:100%; left:0px; top: 0px;";
            this.m_currentNode.AddChild (v_div);
            this.m_currentNode = v_div;
        }
        public void Visit(RectangleElement element)
        {
            CoreXmlElement v_div = CoreXmlWebElement.CreateXmlNode("div") as CoreXmlElement;
            v_div["id"] = element.Id;
            string s = WebProjectUtils.GetElementStyle(element, this.m_TemporyFolder);
            v_div["style"] = s;
            v_div["class"] = "cl" + element.Id;
            this.m_currentNode.AddChild(v_div);
        }
        public void Visit(Core2DDrawingLayeredElement  element)
        {
            CoreXmlElement v_div = CoreXmlWebElement.CreateXmlNode("div") as CoreXmlElement;
            v_div["id"] = element.Id;
                  Rectanglei v_rc =Rectanglei.Round ( element.GetBound());
            //gen resource
            string v_ts = string.Empty ;
           StringBuilder sb = new StringBuilder ();
            sb.Append ("border:none; ");
            //no layout engine
            sb.Append("position:absolute; ");
            string fname = WebProjectUtils.SaveAsBitmap(element, sb ,this.m_TemporyFolder);
            v_div["style"] = sb.ToString ()+ " background-image: url('Res/" + Path.GetFileName(fname) + "'); background-repeat:no-repeat; ";
            v_div["class"] = "cl" + element.Id;
            this.m_currentNode.AddChild(v_div);
        }
        public void Visit(Core2DDrawingDocumentBase  element)
        {
            CoreXmlElement v_div = CoreXmlWebElement.CreateXmlNode("div") as CoreXmlElement;
            v_div["id"] = element.Id;
            v_div["style"] = string.Format ("position:relative; width: {0}px; height:{1}px; overflow:hidden;", 
                element.Width, element .Height );
            this.m_currentNode.AddChild(v_div);
            this.m_currentNode = v_div;
        }
        public void Visit(WebHtmlElementBase item)
        {
            if (this.m_currentNode != null)
                this.m_currentNode.AddChild(item.Node);
            else
                this.m_rootNode.Body.AddChild(item.Node);
        }
        public void Visit(WebHtmlDiv div)
        {
            CoreXmlElement v_div = CoreXmlWebElement.CreateXmlNode("div") as CoreXmlElement;
            v_div["id"] = "div_" + div.GetHashCode();
            this.m_rootNode.Body.AddChild(v_div);
        }
        public void Visit(WebHtmlFormElement form)
        {
            this.m_currentNode.AddChild(form.Node);
            this.m_currentNode = form.Node as CoreXmlElement ;
        }
        public void Visit(WebHtmlLayoutLayer layer)
        {
            if ((layer == null) || string.IsNullOrWhiteSpace(layer.DefaultOutpuTag ))
                return ;
            CoreXmlElement v_div = CoreXmlElement.CreateXmlNode(layer.DefaultOutpuTag ) as CoreXmlElement;
            v_div["style"] = "position:relative; width: 100%; height:100%; left:0px; top: 0px;";
            this.m_currentNode.AddChild(v_div);
            this.m_currentNode = v_div;
        }
        /// <summary>
        /// save the visitor to file name
        /// </summary>
        /// <param name="filename"></param>
        public void Save(string filename)
        {
            switch (this.m_ExportType)
            {
                case enuExportToHTMLType.WebPage:
                    System.IO.File.WriteAllText(filename, this.m_rootNode.RenderXML(new WebVisitorHtmlOptions()));         
                    break;
                case enuExportToHTMLType.DocumentOnly:
                    System.IO.File.WriteAllText(filename, this.m_rootNode.Body.RenderXML(new WebVisitorHtmlOptions()));         
                    break;
                case enuExportToHTMLType.ElementOnly:
                    System.IO.File.WriteAllText(filename, this.m_rootNode.Body.RenderInnerHTML(new WebVisitorHtmlOptions()));         
                    break;
                default:
                    break;
            }
             //move generate resources
            string target = PathUtils.GetDirectoryName (filename );
            foreach (string d in Directory.GetDirectories (this.m_TemporyFolder))
	        {
                try
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(d);
                    string dir = Path.Combine(target, dirInfo.Name);
                    DirectoryInfo dtarget = null;
                    if (Directory.Exists(dir))
                        Directory.Delete(dir,true );                    
                        dtarget = Directory.CreateDirectory(dir);
                        if (dtarget.Exists)
                        {
                            foreach (FileInfo f in dirInfo.GetFiles())
                            {
                                File.Copy(f.FullName, Path.Combine(dtarget.FullName, f.Name), true);
                            }
                        }
                        else
                        {
                            System.Windows.Forms.MessageBox.Show("can't create file \n"+filename );
                        }
                }
                catch (Exception Exception){
                    CoreLog.WriteDebug(Exception.Message);
                }
	        }    
        }
        class HtmlDocument  : CoreXmlElement {
            CoreXmlElement m_head;
            CoreXmlElement m_body;
            CoreXmlElement m_html;
            CoreXmlElement m_docType;
            public CoreXmlElement Body { get { return this.m_body; } }
            public CoreXmlElement Head { get { return this.m_head; } }
            public HtmlDocument():base()
            {
                this.m_docType = CoreXmlElement.CreateDocType( ) as CoreXmlElement ;
                this.m_head = CoreXmlElement.CreateXmlNode("head") as CoreXmlElement;
                this.m_body = CoreXmlElement.CreateXmlNode("body") as CoreXmlElement ;
                this.m_html = CoreXmlElement.CreateXmlNode("html") as CoreXmlElement;
                this.m_html.AddChild(m_head);
                this.m_html.AddChild(m_body);
            }
            public override string RenderXML(IXmlOptions option)
            {
                StringBuilder sb = new StringBuilder();
                CoreXmlCommentElement comment = new CoreXmlCommentElement
                {
                    Value = "File generate width DrSStudio WebAddinProject Solution"
                };
                sb.Append(this.m_docType.RenderXML(option));
                sb.Append(this.m_html.RenderXML(option));                
                return sb.ToString();
            }
        }
        public void ExitNode()
        {
            if ((this.m_currentNode != null) && (this.m_currentNode.Parent is CoreXmlElement ))
                this.m_currentNode = this.m_currentNode.Parent as CoreXmlElement ;
            else
                this.m_currentNode = this.m_rootNode.Body;
        }
        public void Dispose()
        {
            if (Directory.Exists (this.m_TemporyFolder ))
            Directory.Delete(this.m_TemporyFolder, true);
        }
        public enuParamConfigType GetConfigType()
        {
            return enuParamConfigType.ParameterConfig;
        }
        public ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections parameters)
        {
            var group = parameters.AddGroup("Default");
            Type t = GetType();
            group.AddItem(t.GetProperty ("ExportType"));
            return parameters;
        }
        public ICoreControl GetConfigControl()
        {
            return null;
        }
        public string Id
        {
            get { return "Visitor"; }
        }
    }
}

