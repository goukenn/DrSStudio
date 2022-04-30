

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResourceContainer.cs
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
file:ICoreResourceContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.WinUI ;
    /// <summary>
    /// interface that manage a resource 
    /// </summary>
    public interface  ICoreResourceContainer : System.Collections.IEnumerable 
    {
        /// <summary>
        /// get the surface that own a resouces container
        /// </summary>
        ICoreWorkingResourcesContainerSurface Surface { get; }
        /// <summary>
        /// Get the number of resource item in this resources container
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the resource item by name
        /// </summary>
        /// <param name="id">resource id</param>
        /// <returns></returns>
        ICoreResourceItem GetResourceById(string id);
        /// <summary>
        /// get if this resources container contain name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        bool Contains(string name);
        /// <summary>
        /// register the resources
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        bool Register(ICoreResourceItem resource);
        /// <summary>
        /// un register the resource item
        /// </summary>
        /// <param name="coreResourceItemBase"></param>
        void Unregister(ICoreResourceItem resource);
        /// <summary>
        /// check if this contains a resource that match the data
        /// </summary>
        /// <param name="enuCoreResourceType"></param>
        /// <param name="stringDataIdentifier"></param>
        /// <returns></returns>
        bool Contains(enuCoreResourceType enuCoreResourceType, string stringDataIdentifier);
    }
}

