

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SVGEncoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:SVGEncoder.cs
*/
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.SVGAddIn.Codec
{
    [CoreCodec ("SVG", "image/svg", "svg", Category=CoreConstant.CAT_PICTURE )]
    class SVGEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            if ((documents.Length > 0) && (documents[0] is ICore2DDrawingDocument))
            {
                SVGUtils.ExportToHtmlDocument(filename,
                    documents.ConvertTo<ICore2DDrawingDocument>());
                return true;
            }
            return false;
        }
    }
}

