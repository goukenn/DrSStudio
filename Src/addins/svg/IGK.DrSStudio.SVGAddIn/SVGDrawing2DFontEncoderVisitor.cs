using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using System.Text;
using System.Xml;

namespace IGK.DrSStudio.SVGAddIn
{
    [CoreVisitorAttribute("SVGDocument")]
    internal class SVGDrawing2DFontEncoderVisitor : SVGCoreEncoderVisitorBase
    {
        private SVGWriter m_writer;
        private bool m_miss_stored;
        private int m_glyphf_writed;
        public override string Out {
        get {
                return this.m_writer.GetOutput();
            }
        }

        public override void Flush()
        {
            m_writer.Flush();
        }
        ///<summary>
        ///public .ctr. use internally
        ///</summary>
        public SVGDrawing2DFontEncoderVisitor():base()
        {
            StringBuilder sb = new StringBuilder();
            XmlWriterSettings settings = new XmlWriterSettings()
            {
                Indent = true
            };
            SVGWriter sw = new SVGWriter(sb, settings);
            this.m_writer = sw;
    
        }
        ///<summary>
        ///public .ctr
        ///</summary>
        public SVGDrawing2DFontEncoderVisitor(SVGWriter writer):base()
        {
            this.m_writer = writer;
            m_glyphf_writed = 0;
        }

        public void Visit(Core2DDrawingLayerDocument document) {

            if (document.CurrentLayer.Elements[0] is PathElement p){
                var setting = document.GetParam<SVGDocumentFontSettings>();
                CoreGraphicsPath g =(CoreGraphicsPath ) (p.GetPath().Clone());


                //flip y of the graphics path
                IGK.ICore.Rectanglef b = p.GetBound();
                IGK.ICore.Matrix m = new IGK.ICore.Matrix();
                m.Translate(-b.X, -b.Y, IGK.ICore.enuMatrixOrder.Append);
                m.Scale(1, -1, ICore.enuMatrixOrder.Append);
                m.Translate(b.X, b.Y+b.Height, IGK.ICore.enuMatrixOrder.Append);

                g.Transform(m);
                
                //IGK.ICore.Vector2f endLoc = new IGK.ICore.Vector2f(b.X, b.Y + b.Height);
                //Scale(1, -1, endLoc, enuMatrixOrder.Append, false);


                if ((document.Id == "missing-glyph")&& !m_miss_stored)
                {
                    m_miss_stored = true;

                    this.m_writer.WriteStartElement("missing-glyph");
                    this.m_writer.WriteAttributeString("horiz-adv-x", setting?.HorizAdvX);
                    m_writer.WriteStartElement("path");
                    this.m_writer.WriteAttributeString("d",
                      SVGUtils.GetPathDefinition(g)
                      );
                    this.m_writer.WriteEndElement();
                    this.m_writer.WriteEndElement();
                }
                else
                {

                    this.m_writer.WriteStartElement("glyph");
                    this.m_writer.WriteAttributeString("glyph-name", document.Id);
                    this.m_writer.WriteAttributeString("horiz-adv-x", setting?.HorizAdvX);               

                    this.m_writer.WriteAttributeString("unicode", setting?.Unicode ?? ((char)((byte)'a'+m_glyphf_writed)).ToString());

                    this.m_writer.WriteAttributeString("d",
                        SVGUtils.GetPathDefinition(g)
                        );

                    this.m_writer.WriteEndElement();


                    m_glyphf_writed++;
                }
                g.Dispose();
                

                //this.m_writer.Flush();
            }
        }
    }
}