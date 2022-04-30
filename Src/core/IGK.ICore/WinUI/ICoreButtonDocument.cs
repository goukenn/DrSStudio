

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreButtonDocument.cs
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
file:ICoreButtonDocument.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    public interface ICoreButtonDocument: 
        ICoreDisposableObject 
    {
        ICore2DDrawingDocument Normal { get; }
        ICore2DDrawingDocument Hover { get; }
        ICore2DDrawingDocument Down { get; }
        ICore2DDrawingDocument Up { get; }
        ICore2DDrawingDocument Disabled { get; }
    }
}

