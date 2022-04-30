

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IWorkingItemGroup.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:IWorkingItemGroup.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace IGK.DrSStudio
{
    using IGK.ICore;using IGK.DrSStudio.WinUI ;
    public interface IWorkingItemGroup
    {
        string Title { get; }
        string ImageKey { get; }
        string Environment { get; }
        bool Visible { get; set; }
        bool Collapsed { get; }       
        IWorkingItemCollections Items { get; }
        IWorkingGroupOwner Owner { get; }         
        void Expand();
        void Collapse();
        event EventHandler VisibleChanged;
        event EventHandler CollapsedChanged;
        Rectanglei Bound { get;  }
        int InnerHeight { get; }
    }
}

