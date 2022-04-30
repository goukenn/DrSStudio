

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ConnectorKey.cs
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
file:ConnectorKey.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// used internally
    /// </summary>
    internal struct ConnectorKey
    {
        internal ICore2DDrawingLayeredElement lStart;
        internal ICore2DDrawingLayeredElement lTarget;
    }
}

