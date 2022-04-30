using IGK.DrSStudio.SVGAddIn;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.IO;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.DrSStudio.Balafon.Codec
{
     [CoreCodec("balafon-svg-encoder", "application/svg", 
        "svg",
        Category=CoreConstant.CAT_FILE)]
    class BalafonSvgEncoder: CoreEncoderBase
    {
           public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            if ((documents.Length > 0) && (documents[0] is ICore2DDrawingDocument))
            {
                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;
                SVGWriter w = SVGWriter.Create(sb, settings);
                SVGUtils.ExportToHtmlDocument(new SVGDrawing2DEncoderVisitor(w, PathUtils.GetDirectoryName(filename))
                {
                    UseBrushStyle=false
                },
                    documents.ConvertTo<ICore2DDrawingDocument>());
                w.Flush();
                System.IO.File.WriteAllText(filename, sb.ToString());
                return true;
            }
            return false;
        }
    }
}
