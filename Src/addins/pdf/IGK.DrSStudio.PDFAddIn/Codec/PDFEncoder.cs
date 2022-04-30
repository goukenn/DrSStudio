

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PDFEncoder.cs
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
file:PDFEncoder.cs
*/
using IGK.ICore.Codec;
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.PDFConverter.Codec
{
    [CoreCodec ("pdfEncoder", "file/pdf", "pdf", Category= CoreConstant.CAT_PICTURE )]
    class PDFEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            string f = filename;
            if (Path.GetExtension(f).ToLower()!=".pdf")
            {
                f += ".pdf";
            }
            ICore2DDrawingDocument[] d = documents.ConvertTo<ICore2DDrawingDocument>();
            if (d != null)
            {
                PDFEncoderVisitor v = new PDFEncoderVisitor();                
                return v.SaveTo(f, d);
            }
            return false;
        }
    }
}

