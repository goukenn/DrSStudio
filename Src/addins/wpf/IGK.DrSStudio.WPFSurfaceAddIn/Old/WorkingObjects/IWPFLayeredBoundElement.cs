

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IWPFLayeredBoundElement.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:IWPFLayeredBoundElement.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    public interface IWPFLayeredBoundElement : 
        IWPFLayeredElement ,
        ICoreWorkingObjectPropertyEvent
    {
        /// <summary>
        /// get the physical bound of this element
        /// </summary>
        /// <returns></returns>
        Rectangled GetBound();
        /// <summary>
        /// get the global transform bound
        /// </summary>
        /// <returns></returns>
        Rectangled GetTransformBound();
    }
}

