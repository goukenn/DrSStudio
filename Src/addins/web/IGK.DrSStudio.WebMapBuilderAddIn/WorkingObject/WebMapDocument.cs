

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapDocument.cs
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
file:WebMapDocument.cs
*/
using IGK.ICore.GraphicModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [CoreWorkingObject("WebMapDocument")]
    /// <summary>
    /// represent a web map document
    /// </summary>
    public class WebMapDocument : Core2DDrawingDocumentBase, IWebMapElement 
    {
        public WebMapLayer ImageLayer {
            get {
                return this.Layers[0] as WebMapLayer;
            }
        }
        public WebMapLayer RegionLayer
        {
            get {
                return this.Layers[1] as WebMapLayer;
            }
        }
        public WebMapDocument():base()
        {
        }
        protected override ICore2DDrawingLayerCollections CreateLayerCollections()
        {
            return new WebMapLayerCollection(this);
        }
        protected override ICore2DDrawingLayer CreateNewLayer()
        {
            return new WebMapLayer();
        }
        public  void Draw(ICoreGraphics g, bool proportional, Rectanglei rectangle, enuFlipMode flipmode)
        {
        }
        public class WebMapLayerCollection : Core2DDrawingLayerCollections
        {
            private WebMapDocument webMapDocument;
            public override bool AllowMultiLayers
            {
                get
                {
                    return false;
                }
            }
            public new IEnumerator GetEnumerator()
            {
                return base.GetEnumerator();
            }
            public WebMapLayerCollection(WebMapDocument webMapDocument):base(webMapDocument )
            {
                this.webMapDocument = webMapDocument;
            }
            protected override void InitDefaultCollection()
            {
                WebMapDocument doc = this.Document as WebMapDocument;
                WebMapLayer v_l = doc.CreateNewLayer() as WebMapLayer;
                v_l.LayerType = enuWebMapLayerType.Image;
                base.AddLayer(v_l);
                v_l = doc.CreateNewLayer() as WebMapLayer;
                v_l.LayerType = enuWebMapLayerType.Map;
                base.AddLayer(v_l);
            }
        }
        internal void LoadImage(string p)
        {
            WebImageElement i = new WebImageElement();
            i.Source = p;
            this.ImageLayer.Elements.Add(i);
            this.SetSize((int)i.Bounds.Width , (int)i.Bounds.Height );
        }
        public string Render(IWebMapRendererOption option)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings v_setting = new XmlWriterSettings();
            v_setting.OmitXmlDeclaration = true;
            v_setting.Indent = true;
            XmlWriter writer = XmlWriter.Create(sb, v_setting);
            writer.WriteStartElement("div");
            writer.WriteRaw(this.ImageLayer.Render(option));
            writer.WriteRaw(this.RegionLayer.Render(option));
            writer.WriteEndElement();
            writer.Flush();
            return sb.ToString();
        }
    }
}

