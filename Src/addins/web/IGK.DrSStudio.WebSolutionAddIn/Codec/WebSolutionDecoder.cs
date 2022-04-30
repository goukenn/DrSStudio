

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionDecoder.cs
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
﻿using IGK.DrSStudio.Web.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web.Codec
{
    [CoreCodec ("iGKWebSolution", "application/websolution", WebSolutionConstant.EXT)]
    class WebSolutionDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            ICoreSolutionManagerWorkbench b = bench as ICoreSolutionManagerWorkbench;
            if ((b != null) && b.IsSolutionOpened(filename))
                return false;
            WebSolutionSolution sln =  WebSolutionSolution.Open(filename);
            if (sln != null)
            {
                WebSolutionSurface surface = WebSolutionSurface.Create(sln);
                bench.AddSurface(surface, selectCreatedSurface  );
      
                return true;
            }
            return false;
        }
    }
}
