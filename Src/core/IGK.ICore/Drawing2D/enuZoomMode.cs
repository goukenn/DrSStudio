

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuZoomMode.cs
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
file:enuZoomMode.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public enum enuZoomMode
    {
        /// <summary>
        /// normal view
        /// </summary>
        Normal,
        /// <summary>
        /// normal center view
        /// </summary>
        NormalCenter,
        /// <summary>
        /// center the document
        /// </summary>
        ZoomNormal,
        /// <summary>
        /// zoom item to center
        /// </summary>
        ZoomCenter,
        /// <summary>
        /// strecth the document to view
        /// </summary>
        Stretch
    }
}

