

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinLauncherContextMenuStrip.cs
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
file:WinLauncherContextMenuStrip.cs
*/
using IGK.ICore;using IGK.DrSStudio.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.DrSStudio.WinUI
{
    using IGK.DrSStudio.WinLauncher;
    using IGK.DrSStudio.WinLauncher.Actions;
    /// <summary>
    /// represent this win laucher context menu strip
    /// </summary>
    sealed class WinLauncherContextMenuStrip : XMainContextMenuStrip
    {
        public WinLauncherContextMenuStrip(WinLauncherLayoutManager manager, ICoreContextMenuCollections col):base(manager , col)
        {
        }
        protected override void OnOpening(System.ComponentModel.CancelEventArgs e)
        {
            base.OnOpening(e);           
        }
    }
}

