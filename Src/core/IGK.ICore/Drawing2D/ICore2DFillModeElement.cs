

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DFillModeElement.cs
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
file:ICore2DFillModeElement.cs
*/

﻿using IGK.ICore;using IGK.ICore.ComponentModel;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICore2DFillModeElement: ICore2DDrawingElement 
    {
        [
        IGK.ICore.Codec.CoreXMLAttribute(),
        IGK.ICore.Codec.CoreXMLDefaultAttributeValue (enuFillMode.Alternate  ),
        CoreConfigurableProperty(true, Group=CoreConstant.DEFAULT_CONFIGURATION_GROUP)
        ]
        enuFillMode FillMode { get; set; }
    }
}
