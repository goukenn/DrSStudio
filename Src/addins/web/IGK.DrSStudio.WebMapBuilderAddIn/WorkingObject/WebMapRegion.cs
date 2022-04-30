

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapRegion.cs
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
file:WebMapRegion.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
namespace IGK.DrSStudio.Web.WorkingObject
{
    public abstract class WebMapRegion : WebMapElementBase 
    {
        private string m_Href;
        private string m_Alt;
        public override enuBrushSupport BrushSupport
        {
            get
            {
                return enuBrushSupport.None;
            }
        }
        public override bool CanRenderShadow
        {
            get
            {
                return false;
            }
        }
        public override bool CanReSize
        {
            get
            {
                return false;
            }
        }
        public override bool CanRotate
        {
            get
            {
                return false;
            }
        }
        public override bool CanScale
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// Get or set the alt
        /// </summary>
        public string Alt
        {
            get { return m_Alt; }
            set
            {
                if (m_Alt != value)
                {
                    m_Alt = value;
                }
            }
        }
        /// <summary>
        /// Get or set the href
        /// </summary>
        public string Href
        {
            get { return m_Href; }
            set
            {
                if (m_Href != value)
                {
                    m_Href = value;
                }
            }
        }
        public abstract enuWebMapAreaType Type { get; }
        //public override void Draw(ICoreGraphicsDevice g)
        //{
        //    base.Draw(g);
        //}
        //public override void Draw(System.Drawing.Graphics g)
        //{
        //    GraphicsPath v_p = this.GetPath();
        //    if(v_p !=null)
        //    g.DrawPath(CoreBrushRegister.GetPen(Colorf.Black),
        //        v_p);
        //}
        public override string Render(IWebMapRendererOption option)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xwriter = XmlWriter.Create(sb, this.GetXmlWriterSetting());
            xwriter.WriteStartElement("area");
            xwriter.WriteAttributeString("shape", GetShape(this.Type));
            xwriter.WriteAttributeString("coords", GetCoords());            
            xwriter.WriteAttributeString("href", this.Href);
            xwriter.WriteAttributeString("alt", this.Alt);
            xwriter.WriteEndElement();
            xwriter.Flush();
            return sb.ToString();
        }
        protected abstract  string GetCoords();
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections defaultParam)
        {
            var g = defaultParam.AddGroup("Default");
            Type t = this.GetType();
            g.AddItem(t.GetProperty("Href"));
            g.AddItem(t.GetProperty("Alt"));
            g = defaultParam.AddGroup("Scripts");
            return defaultParam;
        }
        private string GetShape(enuWebMapAreaType t)
        {
            switch (t)
            {
                case enuWebMapAreaType.Rectangle:
                    return "rect";
                case enuWebMapAreaType.Circle:
                    return "circle";
                case enuWebMapAreaType.Poly:
                    return "poly";
                default:
                    break;
            }
            return string.Empty;
        }
        public class Mecanism : WebMapElementBase.Mecanism<WebMapRegion>
        { 
        }
    }
}

