

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreLocalizable.cs
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
file:ICoreLocalizable.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// represent a localisable element 
    /// </summary>
    public interface  ICoreLocalizable : ICoreWorkingObject 
    {
        /// <summary>
        /// return the object attached to the localize 
        /// </summary>
        /// <returns></returns>
        ICoreWorkingObject Localize();
        ICoreLocalizableReferenceCountCollections References { get; }
    }
}

