

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IDockingPanel.cs
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
file:IDockingPanel.cs
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
using System.Drawing;
using System.Windows.Forms;
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// rerpresent the panel 
    /// </summary>
    public interface IDockingPanel : 
        ICoreControl  
    {
        DockStyle Dock { get; set; }
        enuDockingDirection DockingDirection { get; }
        /// <summary>
        /// get the docking tab orientation
        /// </summary>
        enuDockingTabOrientation TabOrientation { get; set; }
        event EventHandler DockChanged;
        IDockingPage SelectedPage{get;set;}
        IDockingPageCollections Pages { get; }
        /// <summary>
        /// get the attached splitter
        /// </summary>
        XDockingSplitterControl Splitter { get; }
        event EventHandler SelectedPageChanged;
        event DockingPageEventHandler  PageAdded;
        event DockingPageEventHandler PageRemoved;
        /// <summary>
        /// get or set the docking layout Manager
        /// </summary>
        IDockingManager LayoutManager { get; }
        /// <summary>
        /// get a docking owner
        /// </summary>
        IDockingOwner DockingOwner { get; }
        //for resize panel       
        int GetMinSize();
        int GetMaxSize();
        void Reduce();
        void Expand();
        void EndDragContent();       
    }
}

