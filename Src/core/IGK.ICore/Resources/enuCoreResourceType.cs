

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuCoreResourceType.cs
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
file:enuCoreResourceType.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Resources
{
    /// <summary>
    /// represent a core system resource type
    /// </summary>
    public enum enuCoreResourceType
    {
        CoreObject, 
        /// <summary>
        /// bitmap 
        /// </summary>
        Bitmap,
        /// <summary>
        /// brush 
        /// </summary>
        Brush,
        /// <summary>
        /// Sound file
        /// </summary>
        SoundFile,
        VideoFile,
        File,
        BinaryData,        
        String,
        Font,
        Texture
    }
}

