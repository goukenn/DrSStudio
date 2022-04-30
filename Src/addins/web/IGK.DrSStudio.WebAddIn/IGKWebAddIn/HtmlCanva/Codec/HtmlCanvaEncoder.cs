

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HtmlCanvaEncoder.cs
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebAddIn.HtmlCanva.Codec
{

    /// <summary>
    /// converter compatible with html5 specification
    /// </summary>
    [CoreCodec("iGKWeb.Drawing2DCanvaEncoder", 
        "text/html5-canva-script", 
        "iwcjs",
        Category=CoreConstant.CAT_PICTURE+";"+CoreConstant.CAT_FILE)]//igkweb canva js
    class HtmlCanvaEncoder :  CoreEncoderBase
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, params ICoreWorkingDocument[] documents)
        {
            ICore2DDrawingDocument[] c = documents.ConvertTo<ICore2DDrawingDocument>();
            if ((c == null) || (c .Length ==0))
            return false;
            WebDrawing2DCanvaVisitor canvisitor = new WebDrawing2DCanvaVisitor();
            for (int i = 0; i < c.Length; i++)
            {
                canvisitor.Visit(c[i]);
            }
            canvisitor.SaveTo(filename);
            return true;
        }
    }
}