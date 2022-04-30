

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuRendererSettingType.cs
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
file:enuRendererSettingType.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Settings
{
    public enum  enuRendererSettingType
    {
        /// <summary>
        /// unknow rendering setting
        /// </summary>
        Unknow,

        /// <summary>
        /// represent a string value
        /// </summary>
        String,
        /// <summary>
        /// color value
        /// </summary>
        Color,
        /// <summary>
        /// float value
        /// </summary>
        Float,
        /// <summary>
        /// integer value
        /// </summary>
        Int,
        /// <summary>
        /// for file definition
        /// </summary>
        File,
        /// <summary>
        /// for font style definition 
        /// </summary>
        Font
    }
}

