

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreAddIn.cs
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
file:ICoreAddIn.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
namespace IGK.ICore
{
    public interface ICoreAddIn
    {
        CoreAddInAttribute Attribute { get; }
        /// <summary>
        /// get the addin Location
        /// </summary>
        string Location { get; }
        /// <summary>
        /// get if this assembly is vital
        /// </summary>
        bool IsVital { get; }
        /// <summary>
        /// get the assembly
        /// </summary>
        Assembly Assembly { get; }
    }
}

