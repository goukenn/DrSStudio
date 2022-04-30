

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ViewToolSelector.cs
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
file:ViewToolSelector.cs
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
using System.Windows.Forms;
namespace IGK.DrSStudio.Menu
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Tools;
    [CoreMenu ("View.Tools",1000,ImageKey="Menu_Tools",
        Shortcut = Keys.T,
        ShortcutText="T",
        SeparatorBefore=true)]
    sealed class ViewToolSelector : CoreMenuViewToolBase 
    {
        public new ICoreWorkingToolManagerSurface CurrentSurface {
            get {
                return base.CurrentSurface as ICoreWorkingToolManagerSurface;
            }
        }
        public ViewToolSelector():base(ToolManager.Instance)
        {
        }
        protected override void OnSurfaceChanged(EventArgs eventArgs)
        {
            base.OnSurfaceChanged(eventArgs);
            bool v = (this.CurrentSurface != null);
            this.Visible = v;
            this.Enabled = v;
        }
    }
}

