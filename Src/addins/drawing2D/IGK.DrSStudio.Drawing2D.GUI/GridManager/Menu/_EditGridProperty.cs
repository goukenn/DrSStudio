

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _EditGridProperty.cs
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
file:_EditGridProperty.cs
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
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinCore;
using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.DrSStudio.Menu;
    using IGK.ICore;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu("View.ShowGrid.Property", 0)]
    class _EditGridProperty : Core2DDrawingMenuBase 
    {
        protected override bool PerformAction()
        {
            Workbench.ConfigureWorkingObject(GridManager.GridTool.Instance, "title.editGridProperty".R(), false, Size2i.Empty);
            return false;
        }
    }
}

