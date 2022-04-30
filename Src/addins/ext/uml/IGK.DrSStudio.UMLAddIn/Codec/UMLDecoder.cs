

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UMLDecoder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:UMLDecoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.UMLAddIn.Codec
{
    
using IGK.ICore;using IGK.ICore.Drawing2D.Codec;
    using IGK.ICore.Codec;
    using IGK.ICore.WinUI;
    [CoreCodec("UMLEncoder", "gkds/uml-decoder", "gkds.uml", Category = "File; Pictures")]
    public class UMLDecoder : CoreDecoderBase 
    {
       

        public override bool Open(ICoreWorkbench bench, string filename, bool selectCreatedSurface)
        {
            return false;
        }
    }
}

