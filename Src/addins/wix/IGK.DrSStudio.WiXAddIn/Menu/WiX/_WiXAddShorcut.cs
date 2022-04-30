

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _WiXAddShorcut.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_WiXAddShorcut.cs
*/
using IGK.DrSStudio.WiXAddIn.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinUI.Common;

namespace IGK.DrSStudio.WiXAddIn.Menu.WiX
{
    [DrSStudioMenu("Wix.AddShortcut", 10, SeparatorBefore = true)]
    class _WiXAddShorcut : WiXMenuBase
    {
        protected override bool PerformAction()
        {
            using (ICoreDialogForm d = Workbench.CreateNewDialog())
            {
                WiXNewShortcutControl c = new WiXNewShortcutControl();
                c.Size = new System.Drawing.Size(300, 500);
                c.Dock = DockStyle.Fill;
                d.Title  = CoreSystem.GetString("wix.title.newshortcut");
                d.Controls.Add(c);
                d.Owner = this.MainForm;
                (d as Form).AutoSize = false;
                if (d.ShowDialog() == enuDialogResult.OK)
                {
                    this.CurrentSurface.AddShortCut(c.Shortcut);
                }
            }
            return base.PerformAction();
        }
    }
}

