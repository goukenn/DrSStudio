

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioHelpMenu.cs
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
file:DrSStudioHelpMenu.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.HelpManagerAddIn.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.HelpManagerAddIn.WinUI ;
    using IGK.DrSStudio.WinUI;
    [IGK.DrSStudio.Menu.CoreMenu ("Help.ViewHelp", 0xFFFF)]
    class DrSStudioHelpMenu : IGK.DrSStudio.Menu.CoreApplicationMenu
    {
        XHelpViewerSurface v_surface;
        ICoreDialogForm c_dialog;

        public DrSStudioHelpMenu()
        {
        }
        protected override bool PerformAction()
        {
            if ((v_surface == null) || v_surface.IsDisposed)
            {
                v_surface = new XHelpViewerSurface();
            }
                v_surface.Dock = System.Windows.Forms.DockStyle.Fill;

                if ((c_dialog == null) || (c_dialog.IsDisposed))
                    c_dialog = Workbench.CreateNewDialog();

                c_dialog.Controls.Add (v_surface);
                c_dialog.CanMaximize = true;
                c_dialog.DialogStyle = enuDialogStyle.Sizeable;
                c_dialog.Title = CoreSystem.GetString("Title." + this.Id);
                c_dialog.Show ();
            
            return false;
        }
    }
}

