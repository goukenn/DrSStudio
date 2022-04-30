

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreToolHostedControl.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using IGK.ICore.Tools;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICoreToolHostedControl.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    public interface ICoreToolHostedControl : 
        IDisposable ,
        ICoreIdentifier         
    {
        bool Visible { get;set; }
        event EventHandler VisibleChanged;

        Size2i Size { get; set; }

        event EventHandler SizeChanged;

        Vector2i Location { get; set; }

        event EventHandler LocationChanged;
        string CaptionKey { get; set; }
        /// <summary>
        /// get the tool associate with
        /// </summary>
        ICoreTool Tool { get; }
        /// <summary>
        /// get the tool document
        /// </summary>
        ICore2DDrawingDocument ToolDocument { get; }
    }
}

