

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreShowContextMenuAction.cs
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
file:CoreShowContextMenuAction.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Menu
{
    [DrSStudioMenu("File.ShowContextMenu", Int32.MaxValue  , IsVisible=false, Shortcut= (enuKeys)93)]
    class CoreShowContextMenuAction : CoreApplicationMenu 
    {
        protected override bool PerformAction()
        {
            //  this.Workbench.MainForm.ContextMenuStrip.Show(Vector2i .Zero );
            //if (this.Workbench.MainForm.ContextMenuStrip.Bounds .Height <= 5)
            //  this.Workbench.MainForm.ContextMenuStrip.Close();
 	         return base.PerformAction();
        }
    }
}

