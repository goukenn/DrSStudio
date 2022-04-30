

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResourceItem.cs
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
file:ICoreResourceItem.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Resources
{
    /// <summary>
    /// represent a resources info
    /// </summary>
    public interface ICoreResourceItem  : ICoreIdentifier , ICloneable 
    {
        /// <summary>
        /// get the resource container that contains this resources
        /// </summary>
        ICoreResourceContainer ResourceContainer { get; }
        /// <summary>
        /// reference of this item
        /// </summary>
        ICoreResourceReferenceCollections References { get; }
        /// <summary>
        /// get the resources type
        /// </summary>
        enuCoreResourceType ResourceType { get; }        

        /// <summary>
        /// get if this resources is registered
        /// </summary>
        /// <returns></returns>
        bool IsRegistered();
        /// <summary>
        /// get if this resources is registered
        /// </summary>
        /// <returns></returns>
        bool IsRegistered(ICoreResourceContainer container);
        /// <summary>
        /// container
        /// </summary>
        /// <param name="container"></param>
        bool Register(ICoreResourceContainer container);
        /// <summary>
        /// retrieve the string representation of this data
        /// </summary>
        /// <returns></returns>
        string GetDefinition();
        /// <summary>
        /// retrieve the object link for this data
        /// </summary>
        /// <returns></returns>
        object GetData();

        /// <summary>
        /// used to match with data passed in param
        /// </summary>
        /// <param name="stringDataIdentifier"></param>
        /// <returns></returns>
        bool IsMatch(string stringDataIdentifier);
    }
}

