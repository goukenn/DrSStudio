

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCodec.cs
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
file:ICoreCodec.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent the default codec
    /// </summary>
    public interface ICoreCodec : ICoreIdentifier 
    {
        string Category { get; }
        string MimeType { get; }
        ICoreCodecExtensionCollections Extensions { get; }
        /// <summary>
        /// get the filter that represent the codec
        /// </summary>
        /// <returns></returns>
        string GetFilter();
    }
}

