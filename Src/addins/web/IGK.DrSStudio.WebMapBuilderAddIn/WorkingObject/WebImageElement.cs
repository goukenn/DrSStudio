

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebImageElement.cs
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
file:WebImageElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [WebMapElementStandardAttribute("WebMapImage", null, IsVisible=false )]
    class WebImageElement :
        WebMapElementBase , 
        IWebMapElement,
        ICore2DDrawingVisitable 
    {
        private ICore2DDrawingLayeredElement m_ImageElement;
        private string m_Description;
        /// <summary>
        /// get or set the description
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                }
            }
        }
        /// <summary>
        /// get or set the map id
        /// </summary>
        public string MapId
        {
            get {
                return (this.ParentDocument as WebMapDocument).RegionLayer.Id;
            }
        }
        /// <summary>
        /// get or set the image element to render
        /// </summary>
        public ICore2DDrawingLayeredElement  Image
        {
            get { return m_ImageElement; }
            set
            {
                if (m_ImageElement != value)
                {
                    m_ImageElement = value;
                }
            }
        }
        private string m_Source;
        [CoreXMLAttribute ()]
        /// <summary>
        /// get or set the source lement
        /// </summary>
        public string Source
        {
            get { return m_Source; }
            set
            {
                if (m_Source != value)
                {
                    m_Source = value;
                    try{
                        DocumentBlockElement d = DocumentBlockElement.FromFile(value);
                        if (d == null)
                        {
                            ImageElement v = 
                                ImageElement.CreateFromFile (value);
                            if (v!=null){
                            this.Image = v;
                            this.Bounds = new Rectanglef(
                    0, 0, v.Width, v.Height);
                            }
                        }
                        else {
                            this.Image = d;
                            this.Bounds = new Rectanglef(
                                0,0, d.Document.Width, d.Document.Height);
                        }
                    }
                        catch{
                        }
                }
            }
        }

        protected override void InitGraphicPath(CoreGraphicsPath path)
        {
            base.InitGraphicPath(path);
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            (this.Image as ICoreSerializerService).Serialize(xwriter);
        }
       
        public override string Render(IWebMapRendererOption option)
        {
            StringBuilder sb = new StringBuilder();
            System.Xml.XmlWriterSettings v_settings = new System.Xml.XmlWriterSettings();
            v_settings.Indent = true;
            v_settings.OmitXmlDeclaration = true;
            XmlWriter xwriter = XmlWriter.Create(sb, v_settings);
            xwriter.WriteStartElement("img");
            xwriter.WriteAttributeString("id", this.Id);
            xwriter.WriteAttributeString("src", "file://"+this.Source);
            xwriter.WriteAttributeString("alt", this.Description);
            xwriter.WriteAttributeString("usemap", "#"+this.MapId);
            xwriter.Close();
            return sb.ToString();
        }

        public bool Accept(ICore2DDrawingVisitor visitor)
        {
            return visitor != null;
        }

        public void Visit(ICore2DDrawingVisitor visitor)
        {
            visitor.Draw(this.Image);
        }
    }
}

