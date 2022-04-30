

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuParameterType.cs
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
file:enuParameterType.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI.Configuration
{
    /// <summary>
    /// represent a parameter item type
    /// </summary>
    public enum enuParameterType
    {
        /// <summary>
        /// 
        /// </summary>
        Text,
        /// <summary>
        /// int number
        /// </summary>
        IntNumber,
        /// <summary>
        /// float number
        /// </summary>
        SingleNumber,
        /// <summary>
        /// checkbox
        /// </summary>
        Bool,
        /// <summary>
        /// color selector
        /// </summary>
        Color,
        /// <summary>
        /// combox box enum type
        /// </summary>
        EnumType,
        /// <summary>
        /// trackback interval value
        /// </summary>
        Interval,
        /// <summary>
        /// pick filename
        /// </summary>
        FileName,
        /// <summary>
        /// pick folder 
        /// </summary>
        Folder,
        /// <summary>
        /// text area
        /// </summary>
        TextArea,
        /// <summary>
        /// password text
        /// </summary>
        Password,
        /// <summary>
        /// multiline textbox
        /// </summary>
        MultiTextLine,
        /// <summary>
        /// a single label info
        /// </summary>
        Label
    }
}

