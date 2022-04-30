

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GLTessException.cs
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
file:GLTessException.cs
*/
/*
 * GLTessException.cs
 * 
 * Author: C.A.D. BONDJE DOUE
 * copyright : IGKDEV
 * */
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.GLLib
{
    /// <summary>
    /// represent a tessalation exception
    /// </summary>
    public class GLTessException : Exception 
    {
        public GLTessException(string message):base(message)
        {
        }
    }
}

