

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ConvertToTextBlock.cs
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
file:_ConvertToTextBlock.cs
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
namespace IGK.DrSStudio.Drawing2D.ContextMenu
{
    using IGK.ICore;using IGK.DrSStudio.ContextMenu;
    using IGK.DrSStudio.Drawing2D.WinUI;
    [CoreContextMenu("Drawing2D.Convert.ToTextBlock", 1,
    CaptionKey = ConverterConstant.MENU_CONVERTTO_TEXTBLOCK)]
    public class _ConvertToTextBlock :  Core2DContextMenuBase 
    {
        public _ConvertToTextBlock()
        {
            this.IsRootMenu = false;
        }
        protected override bool PerformAction()
        {
            Workbench.CallAction(ConverterConstant.MENU_CONVERTTO_TEXTBLOCK);
            return false;
        }
        protected override void OnOpening(EventArgs e)
        {
            bool v = false;
            ICore2DDrawingSurface v_s =
                this.CurrentSurface;
            v = (v_s != null) && v_s.Mecanism.AllowContextMenu;
            this.Visible = v && (this.OwnerContext.SourceControl == v_s) && v_s.Mecanism.AllowContextMenu;
            this.Enabled = v;
        }
    }
}

