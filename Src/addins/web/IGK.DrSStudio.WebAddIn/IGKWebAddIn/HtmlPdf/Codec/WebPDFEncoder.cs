

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebPDFEncoder.cs
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
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebAddIn.IGKWebAddIn.HtmlPdf.Codec
{
    [CoreCodec (nameof(WebPDFEncoder), 
        "igkweb/pdf-source", 
        "iwpdfsrc", 
        Category=CoreConstant.CAT_PICTURE +";"+CoreConstant.CAT_FILE )]
    class WebPDFEncoder : CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            string f = filename;
            if (Path.GetExtension(f).ToLower() != ".iwpdfsrc")
            {
                f += ".iwpdfsrc";
            }
            ICore2DDrawingDocument[] d = documents.ConvertTo<ICore2DDrawingDocument>();
            if (d != null)
            {
                WebPDFEncoderVisitor v = new WebPDFEncoderVisitor();
                for (int i = 0; i < d.Length; i++)
                {
                    v.Visit(d[i]);
                }
                
                v.SaveTo (f);
            }
            return false;
        }
    }
}
