

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingDefinitionObject.cs
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
file:ICoreWorkingDefinitionObject.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreWorkingDefinitionObject : ICoreWorkingObject 
    {
        /// <summary>
        /// get the string definition of this brush
        /// </summary>
        /// <returns></returns>
        string GetDefinition();
        /// <summary>
        /// get the definition of this item
        /// </summary>
        /// <param name="str"></param>
        void CopyDefinition(string str);
   }
}

