

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingShadowElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore;using IGK.ICore.GraphicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// Represent an element that support shadow definition
    /// </summary>
    public interface ICore2DDrawingShadowElement : ICoreBrushOwner
    {
        /// <summary>
        /// used to determine if this object 
        /// </summary>
        bool CanRenderShadow { get; }
        [IGK.ICore.Codec.CoreXMLDefaultAttributeValue(false)]
        bool AllowShadow { get; set; }
        Core2DShadowProperty ShadowProperty { get; }
    }
}
