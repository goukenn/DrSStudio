

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreAddInCollections.cs
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
file:ICoreAddInCollections.cs
*/

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreAddInCollections :
        System.Collections.IEnumerable 
    {
        /// <summary>
        /// get the number of adding
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the adding
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICoreAddIn this[int index] { get; }
        /// <summary>
        /// get assembly by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Assembly GetAssembly(string name);

        /// <summary>
        /// get loaded assembly addin from fullname
        /// </summary>
        /// <param name="fullName"></param>
        /// <returns></returns>
        Assembly GetAssemblyFromFullName(string fullName);
    }
}

