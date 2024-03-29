

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _AddNewPinceauMenu.cs
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
file:_AddNewPinceauMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    [IGK.DrSStudio.Menu.CoreMenu("File.New.Pinceau", 2, ImageKey="app_pinceau")]
    public sealed class _AddNewPinceauMenu : IGK.DrSStudio.Menu.CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            WinUI.CorePinceauSurface surface = new IGK.DrSStudio.Drawing2D.WinUI.CorePinceauSurface();
            this.Workbench.Surfaces.Add(surface);
            return false;
        }
    }
}

