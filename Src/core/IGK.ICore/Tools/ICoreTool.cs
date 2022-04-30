

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreTool.cs
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
file:ICoreTool.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Tools
{
    using IGK.ICore;using IGK.ICore.WinUI ;
    public interface ICoreTool : ICoreIdentifier , ICoreWorkbenchHost
    {
        string ToolImageKey { get; }        
        /// <summary>
        /// get if this tool can be show
        /// </summary>
        bool CanShow { get; }
        /// <summary>
        /// get or set if this tool is enabled
        /// </summary>
        bool Enabled { get; set; }
        /// <summary>
        /// get or set if this tool is visible
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// get the hosted control
        /// </summary>
        ICoreToolHostedControl HostedControl { get; }
        /// <summary>
        /// configure the tool
        /// </summary>
        void Configure();
        event EventHandler VisibleChanged;
        event EventHandler EnabledChanged;
    }
}

