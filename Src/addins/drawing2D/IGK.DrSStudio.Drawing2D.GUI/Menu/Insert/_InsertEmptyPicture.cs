

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _InsertEmptyPicture.cs
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
file:_InsertEmptyPicture.cs
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
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.Menu;
using IGK.ICore;
using IGK.ICore.Drawing2D;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.Drawing2D.Menu;
namespace IGK.DrSStudio.Drawing2D.Menu.Insert
{
    [DrSStudioMenu(CoreConstant.MENU_INSERT+".EmptyPicture", 1)]
    class _InsertEmptyPicture : Core2DDrawingMenuBase 
    {
        protected override bool PerformAction()
        {
                ImageElement img = ImageElement.CreateFromBitmap (
                    CoreApplicationManager.Application.ResourcesManager.CreateBitmap(
                    this.CurrentSurface.CurrentDocument.Width,
                    this.CurrentSurface.CurrentDocument.Height
                    ));
                if (img != null)
                {
                    this.CurrentSurface.CurrentLayer.Elements.Add(img);
                    this.CurrentSurface.RefreshScene();
                    return true;
                }
            return false;
        }
    }
}

