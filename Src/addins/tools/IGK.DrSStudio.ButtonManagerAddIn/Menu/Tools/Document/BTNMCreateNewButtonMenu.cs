

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: BTNMCreateNewButtonMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.DrSStudio.Drawing2D.WinUI;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Menu.Tools
{
    [DrSStudioMenu("Tools.Document.CreateNewButton", 2,
        Description="create a new button surface from the current document")]
    class BTNMCreateNewButtonMenu : Core2DDrawingMenuBase
    {
        protected override bool PerformAction()
        {
            if (this.CurrentSurface.CurrentDocument != null)
            {
                CoreButtonDocument btn = CoreButtonDocument.Create(this.CurrentSurface.CurrentDocument);
                IGKD2DButtonSurface btnsurface = IGKD2DButtonSurface.CreateSurface (btn);
                
                this.Workbench.AddSurface (btnsurface,true );
                
            }
            return false;
        }
    }
}
