

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SkinEditorMenu.cs
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
file:SkinEditorMenu.cs
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
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.SkinEditorAddin.Menu
{
    using IGK.ICore.WinCore;
using IGK.ICore.WinUI;
    using IGK.ICore.Menu ;
    using IGK.ICore;
    using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.SkinEditorAddin.WinUI;
    using IGK.ICore.WinUI.Common;

    [DrSStudioMenu("Tools.ThemeEditor", 93)]
    public class ThemeEditorMenu : CoreApplicationMenu 
    {
        private ICoreDialogForm m_dialog;

        protected override bool PerformAction()
        {
            if ((m_dialog == null) || (m_dialog.IsDisposed))
            {
                ApplicationThemeEditor v_ctr = new ApplicationThemeEditor();
                v_ctr.Dock = System.Windows.Forms.DockStyle.None ;
                v_ctr.Size = new System.Drawing.Size(400, 300);
                ICoreDialogForm frm = Workbench.CreateNewDialog(v_ctr);
                {
                    frm.Title = "title.ThemeEditor".R();
                    frm.StartPosition = enuFormStartPosition.CenterParent;
                    frm.Owner = MainForm;
                    var v_frm = (frm as System.Windows.Forms.Form);
                    v_frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
                    v_frm.Owner = this.MainForm as System.Windows.Forms.Form;
                   // (frm as System.Windows.Forms.Form).ClientSize = v_ctr.PreferredSize;
                    m_dialog = frm;
                    frm.ShowDialog();//.Show();
                }
            }
            else {
                m_dialog.ShowDialog();
            }
            return false;
        }
    }
}

