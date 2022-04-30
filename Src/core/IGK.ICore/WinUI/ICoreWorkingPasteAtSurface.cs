

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingPasteAtSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// surface that support paste at location element
    /// </summary>
    public interface ICoreWorkingPasteAtSurface : ICoreWorkingSurface , ICoreWorkingEditableSurface 
    {
        void PasteAt(Vector2f location);
    }
}
