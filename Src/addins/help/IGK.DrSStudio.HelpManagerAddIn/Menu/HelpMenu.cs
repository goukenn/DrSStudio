

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: HelpMenu.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.DrSStudio.HelpManagerAddIn.WinUI;
using IGK.DrSStudio.Menu;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.HelpManagerAddIn.Menu
{
    //[DrSStudioMenu("Help.ShowHelp", 
    //    int.MaxValue, 
    //    ImageKey= "menu_help_book")]
    public sealed class HelpMenu : CoreApplicationMenu
    {
        private ICoreDialogForm m_dial;

        protected override bool IsEnabled()
        {
            return true;
        }
        protected override bool IsVisible()
        {
            return base.IsVisible();
        }

        protected override void InitMenu()
        {
            base.InitMenu();
        }
        public HelpMenu()
        {
            this.Enabled = true;
        }

        protected override bool PerformAction()
        {
            //if ((m_dial != null) && (!m_dial.IsDisposed))
            //{
            //    m_dial.Title = "title.help".R();
            //    m_dial.Show();
            //    m_dial.Activate();
            //    return false ;
            //}
            if (Workbench == null)
                return false;

            ICoreControl v_ctrl = new HelpControlGUI();            
            var dial = Workbench.CreateNewDialog(v_ctrl);            
            dial.Title = "title.help".R();
            dial.Owner = MainForm;
            dial.CanMaximize = true;
            dial.Show();
            this.m_dial = dial;
            this.m_dial.Closing += (o, e) =>
            {
                switch (e.CloseReason)
                {
                    case enuCloseReason.UserClose:
                        e.Cancel = true;
                        this.m_dial.Hide();
                        break;
                }
            };
            return false;
        }
    }
}
