

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResourceReferenceCollections.cs
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
file:ICoreResourceReferenceCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    /// <summary>
    /// represent interface to count a resources references. if resource reference number is equal 0
    /// the reference will not be saved in resource category group of the gkds file document
    /// </summary>
    public interface  ICoreResourceReferenceCollections
    {
        /// <summary>
        /// get the number of references
        /// </summary>
        int Count { get; }
        /// <summary>
        /// check if this working objet reference this refesources
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool IsReference(ICoreWorkingObject obj);
    }
}
