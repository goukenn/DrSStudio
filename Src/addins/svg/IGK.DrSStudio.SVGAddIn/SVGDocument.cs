using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGAddIn
{
    using IGK.DrSStudio.SVGAddIn.Codec;
    using IGK.ICore.Drawing2D;

    /// <summary>
    /// drawing 2d document
    /// </summary>
    public class SVGDocument : Core2DDrawingLayerDocument, ISVGElementContainer
    {
        protected override void ReadAttributes(ICore.Codec.IXMLDeserializer xreader)
        {
            base.ReadAttributes(xreader);
        }
        protected override void ReadElements(ICore.Codec.IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }
        protected override void WriteAttributes(ICore.Codec.IXMLSerializer xwriter)
        {
            base.WriteAttributes(xwriter);
        }
        protected override void WriteElements(ICore.Codec.IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
        }
        protected override ICore2DDrawingLayer CreateNewLayer()
        {
            return new SVGGElement();
        }

        public void Add(ISVGElement svgElement)
        {
            this.Layers.Add(svgElement as ICore.ICoreLayer);
        }

        public void Remove(ISVGElement svgElement)
        {
            this.Layers.Remove(svgElement as ICore.ICoreLayer);
        }
    }
}
