

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXFileDecoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
using IGK.DrSStudio.WiXAddIn.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXFileDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WiXAddIn.Codec
{
    [FileCodec("WiX", "application/igk-wix-file", WiXConstant.WIXPROJECTFILE_EXTENSION)]
    class WiXFileDecoder : CoreDecoderBase
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool attachToDesigner)
        {
            WiXProject prj = WiXProject.LoadFromFile(filename);
            if (prj != null)
            {
                WiXSurface v_surface = new WiXSurface();
                //change the project
                v_surface.Project = prj;
                bench.AddSurface(v_surface,true);
                bench.CurrentSurface = v_surface;
                return true;
            }
            return false;
        }
    }
}

