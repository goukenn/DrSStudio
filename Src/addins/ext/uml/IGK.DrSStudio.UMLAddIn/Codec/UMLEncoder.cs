

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UMLEncoder.cs
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
file:UMLEncoder.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.UMLAddIn.Codec
{
    
using IGK.ICore.Drawing2D.Codec;
    using IGK.ICore.Codec ;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    [CoreCodec("UMLEncoder", "gkds/uml-encoder", "cd;gkdsuml",
        Category=CoreConstant.CAT_FILE+";"+CoreConstant.CAT_PICTURE)]
    /// <summary>
    /// represent a uml encoder base
    /// </summary>
    public class UMLEncoder : CoreEncoderBase , ICoreEncoder 
    {
        public override bool Save(ICoreWorkingSurface surface, string filename, ICoreWorkingDocument[] documents)
        {
            return false;
        }
    }
}

