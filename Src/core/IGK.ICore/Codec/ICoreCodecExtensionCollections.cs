

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCodecExtensionCollections.cs
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
file:ICoreCodecExtensionCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Codec
{
    /// <summary>
    /// represent the extensions collection
    /// </summary>
    public interface ICoreCodecExtensionCollections:
        System.Collections.IEnumerable 
    {
        int Count { get; }
        bool Contains(string ext);
        void Add(string ext);
        void Remove(string ext);
    }
}

