

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingElementCollections.cs
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
file:ICoreWorkingElementCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// 
    /// </summary>
    public interface  ICoreWorkingElementCollections : IEnumerable
    {
        /// <summary>
        /// number of element in the collection
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the working object
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICoreWorkingObject this[int index] { get; }
        /// <summary>
        /// get is collection is a readonly collection
        /// </summary>
        bool IsReadOnly { get; }
    }
}

