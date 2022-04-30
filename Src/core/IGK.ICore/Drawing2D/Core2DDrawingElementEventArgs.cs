

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: Core2DDrawingElementEventArgs.cs
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
file:Core2DDrawingElementEventArgs.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public delegate void Core2DDrawingElementEventHandler(object o
    ,Core2DDrawingElementEventArgs e);
    public class Core2DDrawingElementEventArgs : CoreItemEventArgs<ICore2DDrawingLayeredElement >
    {
        public Core2DDrawingElementEventArgs(ICore2DDrawingLayeredElement rlement):base(rlement )
        {
        }
    }
}

