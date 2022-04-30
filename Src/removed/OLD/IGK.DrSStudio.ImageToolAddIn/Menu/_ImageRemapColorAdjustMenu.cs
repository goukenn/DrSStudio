

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _ImageRemapColorAdjustMenu.cs
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
file:_ImageRemapColorAdjustMenu.cs
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
    using IGK.ICore;using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.Drawing2D.WinUI;
    /// <summary>
    /// base class of the tools element
    /// </summary>
    [IGK.DrSStudio.Menu.CoreMenu("Image.Tools.RemapColorAdjustMenu", 1, ImageKey = "Menu_ReplaceColor")]
    sealed class _RemapColorAdjustMenu : ImageMenuBase
    {
        protected override bool PerformAction()
        {
            UIXRemapColor attrib = new UIXRemapColor(this.ImageElement );
            using (ICoreDialogForm dialog = Workbench.CreateNewDialog (attrib))
            {
                dialog.Title = CoreSystem.GetString (Id);
                dialog.StartPosition = enuFormStartPosition.CenterParent;
                if (dialog.ShowDialog() == enuDialogResult.OK)
                {
                    this.CurrentSurface.Invalidate();
                }
            }
            return false;
        }
    }
}
