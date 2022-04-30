

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _FileNew.cs
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
file:_New.cs
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
using System.Text;
namespace IGK.DrSStudio.Menu.File
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Resources;
    using IGK.ICore.Menu;
    [DrSStudioMenu("File.New", 0x10,
        Shortcut = enuKeys.Control | enuKeys.N,
        ImageKey=CoreImageKeys.MENU_NEW_GKDS)]  
    class _FileNew : CoreApplicationMenu 
    {
        NewFile m_prj = null;
        /// <summary>
        /// override only call action when child not visible
        /// </summary>
        protected override void OnMenuItemClicked()
        {
            if (this.Childs.Count == 0)
            {
                base.OnMenuItemClicked();
            }
            else {
                this.MenuItem.PerformHits();//
            }
        }
        protected override bool PerformAction()
        {
            if (this.Childs.Count == 0)
            {
                return Workbench.CreateNewFile();
            }
            else
            {
                m_prj.DoAction();
                this.MenuItem.PerformHits();//.HideMenu();
            }
            return false;
        }
        protected override void OnItemAdded(CoreMenuActionEventArgs e)
        {
            base.OnItemAdded(e);
            if (m_prj == null)
            {
                m_prj = new NewFile();
                this.Childs.Add(m_prj);
            }
        }
        class NewFile : CoreApplicationMenu
        {
            public NewFile()
            {
                this.Id = "File.New.File";
                CoreMenuAttribute v_ttr = new CoreMenuAttribute("File.New.File", 1)
                {
                    ShortcutText = CoreResources.GetShortcutText(
                enuKeys.Control | enuKeys.N)
                };
                this.SetAttribute(v_ttr);
            }
            protected override bool PerformAction()
            {
                return  Workbench.CreateNewFile();                
            }
        }
    }
}

