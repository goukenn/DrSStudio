
using IGK.DrSStudio.SVGAddIn.Codec;
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.SVGAddIn
{
    /// <summary>
    /// represent a svg g element
    /// </summary>
    public class SVGGElement : Core2DDrawingLayer, ICore2DDrawingLayer, ISVGElement, ISVGElementContainer  
    {

        internal void Load(SVGFileDecoder sVGFileDecoder, System.Xml.XmlReader reader)
        {
            
        }
        public void Add(ISVGElement svgElement)
        {
            this.Elements.Add(svgElement as ICore2DDrawingElement);
        }

        public void Remove(ISVGElement svgElement)
        {
            this.Elements.Remove(svgElement as ICore2DDrawingElement);
        }
    }
}
