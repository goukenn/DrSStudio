

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAndroidResourceItem.cs
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
file:IAndroidResourceItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    public interface  IAndroidResourceItem
    {
        /// <summary>
        /// get/set the resource name
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// get/set the resource value
        /// </summary>
        object Value { get; set; }
        /// <summary>
        /// get/Set the resource type
        /// </summary>
        enuAndroidResourceType ResourceType { get; }
    }
}

