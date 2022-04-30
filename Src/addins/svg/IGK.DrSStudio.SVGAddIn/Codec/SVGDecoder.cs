

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.SVGAddIn.Codec
{
    [CoreCodec ("SVGDecoder", "image/svg+xml", "svg")]
    class SVGDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            ICore2DDrawingDocument[] document = SVGFileDecoder.Decode(filename);
            if ((document != null) && (document.Length > 0))
            {

                
                IGKD2DDrawingSurface c = IGKD2DDrawingSurface.CreateSurface
                    (
                    GKDSElement.Create (null, document)
                    );
                if (c is ICoreWorkingFilemanagerSurface gg)
                {
                    gg.FileName = filename;
                }

                bench.AddSurface(c, selectCreatedSurface);
                return true;               
            }

            return false;
        }
    }
}
