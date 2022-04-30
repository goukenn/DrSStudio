

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: InsertOnSurfaceMenu.cs
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
file:InsertOnSurfaceMenu.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.Drawing2D.Menu.Insert
{
    using IGK.DrSStudio.Menu;
    using IGK.ICore;
    using IGK.ICore.Drawing2D.Menu;
    [DrSStudioMenu(CoreConstant.MENU_INSERT, 0x3)]
    class InsertOnSurfaceMenu : Core2DDrawingMenuBase 
    {
        protected override bool PerformAction()
        {
            return false;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible() && (this.Childs.Count > 0);
        }
        protected override bool IsEnabled()
        {
            return base.IsEnabled() && (this.Childs.Count > 0);
        }
        protected override void OnItemAdded(CoreMenuActionEventArgs e)
        {
            base.OnItemAdded(e);
            this.SetupEnableAndVisibility();
        }
        protected override void OnItemRemoved(CoreMenuActionEventArgs e)
        {
            base.OnItemRemoved(e);
            this.SetupEnableAndVisibility();
        }
    }
}

