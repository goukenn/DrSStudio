

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DDrawingChildContextMenuBase.cs
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

namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    /// <summary>
    /// represent base child context menu for element
    /// </summary>
    public class IGKD2DDrawingChildContextMenuBase : IGKD2DDrawingContextMenuBase
    {
        public IGKD2DDrawingChildContextMenuBase()
        {
            this.IsRootMenu = false;
        }
    }
}
