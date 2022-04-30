

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebMapLayer.cs
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
file:WebMapLayer.cs
*/
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Web.WorkingObject
{
    [CoreWorkingObject ("WebMapLayer")]
    public class WebMapLayer : Core2DDrawingLayer, IWebMapElement
    {
        private enuWebMapLayerType  m_LayerType;
        /// <summary>
        /// get or set the layer type
        /// </summary>
        public enuWebMapLayerType  LayerType
        {
            get { return m_LayerType; }
            set
            {
                if (m_LayerType != value)
                {
                    m_LayerType = value;
                }
            }
        }
        public WebMapLayer()
        {
        }
        public virtual string Render(IWebMapRendererOption option)
        {
            try
            {
                switch (this.LayerType)
                {
                    case enuWebMapLayerType.Image:
                        return (this.Elements[0] as IWebMapElement).Render(option);
                    case enuWebMapLayerType.Map:
                        StringBuilder sb = new StringBuilder();
                        System.Xml.XmlWriterSettings v_settings = new System.Xml.XmlWriterSettings();
                        v_settings.Indent = true;
                        v_settings.OmitXmlDeclaration = true;
                        System.Xml.XmlWriter v_xwriter = System.Xml.XmlWriter.Create(sb, v_settings);
                        v_xwriter.WriteStartElement("map");
                        v_xwriter.WriteAttributeString("id", this.Id);
                        foreach (var item in this.Elements)
                        {
                            IWebMapElement l = item as IWebMapElement;
                            if (l != null)
                            {
                                v_xwriter.WriteRaw(l.Render(option));
                                v_xwriter.Flush();
                            }
                            if (v_settings.Indent)
                            {
                                v_xwriter.WriteRaw("\n");
                                v_xwriter.Flush();
                            }
                        }
                        v_xwriter.WriteEndElement();
                        v_xwriter.Flush();
                        return sb.ToString();
                    default:
                        break;
                }
            }
            catch { 
            }
            return string.Empty;
        }
        public override ICoreParameterConfigCollections GetParameters(ICoreParameterConfigCollections defaultParam)
        {
            var g = defaultParam.AddGroup("Default");
            Type t = this.GetType();
            g.AddItem(t.GetProperty("Id"));
            return defaultParam;
        }
    }
}

