

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IRectanglei.cs
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
file:IRectanglei.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public interface  IRectanglei : IVector2i 
    {
        int Width { get; set; }
        int Height { get; set; }
        int Right { get; }
        int Bottom { get; }
        int Top { get; }
        int Left { get; }
    }
}

