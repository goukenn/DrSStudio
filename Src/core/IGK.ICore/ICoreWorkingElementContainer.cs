

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingElementContainer.cs
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
file:ICoreWorkingElementContainer.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// reprensent a core working object item container.
    /// </summary>
    /// <note> if 
    /// </note>
    public interface  ICoreWorkingElementContainer : IEnumerable 
    {
        
        T GetElementById<T>(string id) where T : class;
        object GetElementById(string id);
        ICoreWorkingElementCollections Elements { get; }
    }
}
