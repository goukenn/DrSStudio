

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PaletteDecoder.cs
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:PaletteDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.PaletteBuilder.Codec
{
    using IGK.DrSStudio.PaletteBuilder.Menu;    
    [CoreCodec("PaletteBuilderDecoder", "gkds/gkpal-file-builder", "gkpal")]
    class PaletteDecoder : CoreDecoderBase 
    {
        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {            
            WinCorePalette pal = WinCorePalette.LoadFile(filename);
            if (pal != null)
            {
                NewPalette v_menupal = CoreSystem.GetAction(PaletteBuilderConstant.NEW_PALETTE) as NewPalette;
                v_menupal.Open(pal);
                return true;
            }
            return false;
        }
    }
}

