

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IDockingPage.cs
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
file:IDockingPage.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    /// <summary>
    /// represent a docking page
    /// </summary>
    public interface IDockingPage
    {
        /// <summary>
        /// get the layout tool display
        /// </summary>
        enuLayoutToolDisplay ToolDisplay { get; set; }
        IDockingPanel Panel { get; set; }
        /// <summary>
        /// get the tool associate with the display
        /// </summary>
        ICoreTool Tool { get; }
        /// <summary>
        /// get or set the docking form
        /// </summary>
        IDockingForm DockingForm { get; set; }
        /// <summary>
        /// get the title of the page
        /// </summary>
        string Title { get; }
        /// <summary>
        /// get the document that represent the page
        /// </summary>
        ICore2DDrawingDocument Document { get; }
        /// <summary>
        /// get the hosted control
        /// </summary>
        ICoreToolHostedControl HostedControl { get; }
        /// <summary>
        /// get the layout managet
        /// </summary>
        WinCoreLayoutManagerBase LayoutManager { get; }
        /// <summary>
        /// dock the current page to...
        /// </summary>
        /// <param name="dock"></param>
        void DockTo(enuDockingDirection dock);
        /// <summary>
        /// undock the current page
        /// </summary>
        void Undock();
    }
}

