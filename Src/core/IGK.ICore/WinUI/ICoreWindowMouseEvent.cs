

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWindowMouseEvent.cs
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
file:ICoreWindowMouseEvent.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{

    /// <summary>
    /// represent a window mouse event supported for ICore Core Lib
    /// </summary>
    public interface ICoreWindowMouseEvent
    {
        event EventHandler Click;
        event EventHandler DoubleClick;
        event EventHandler MouseEnter;
        event EventHandler MouseLeave;
        event EventHandler<CoreMouseEventArgs> MouseClick;
        event EventHandler<CoreMouseEventArgs> MouseDoubleClick;
        event EventHandler<CoreMouseEventArgs> MouseDown;
        event EventHandler<CoreMouseEventArgs> MouseMove;
        event EventHandler<CoreMouseEventArgs> MouseUp;
    }
}

