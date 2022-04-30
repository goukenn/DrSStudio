

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DTextElement.cs
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
file:ICore2DTextElement.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Codec;
    public interface ICore2DTextElement: 
        ICore2DDrawingLayeredElement ,
        ICoreTextElement
    {
        [CoreXMLElement()]
        [CoreXMLDefaultAttributeValue("text")]
        string Content { get; set; }
        [CoreXMLAttribute ()]
        bool AutoAdjust { get; set; }
        [IGK.ICore.Codec.CoreXMLAttribute()]
        [IGK.ICore.Codec.CoreXMLDefaultAttributeValue(enuTextElementMode.Path)]
        enuTextElementMode RenderingMode { get; set; }
    }
}

