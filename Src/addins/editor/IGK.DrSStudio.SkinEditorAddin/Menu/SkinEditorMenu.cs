

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

    [DrSStudioMenu("Tools.SkinEditor", 92)]
    public class SkinEditorMenu : CoreApplicationMenu, IUIXSkineEditorPropertyListener
    {
        private ICoreDialogForm m_dialog;

        protected override bool PerformAction()
        {
            if ((m_dialog == null) || (m_dialog.IsDisposed))
            {
                var v_ctr = new UIXSkinEditor();
                v_ctr.Dock = System.Windows.Forms.DockStyle.Fill;
                v_ctr.SetPropertyListener (this);
                ICoreDialogForm frm = Workbench.CreateNewDialog(v_ctr);
                {
                    frm.Title = "Dlg.SkinEditor".R();
                    frm.StartPosition = enuFormStartPosition.CenterParent;
                    frm.Owner = MainForm;
                    var v_frm = (frm as System.Windows.Forms.Form);
                    v_frm.ClientSize = v_ctr.PreferredSize;
                    v_frm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow  ;
                    m_dialog = frm;
                    frm.Show();
                }
            }
            else {
                m_dialog.Show();
            }
            return false;
        }

        public ICore.Settings.ICoreRendererSetting[] GetRendererSetting()
        {
            return CoreRenderer.GetCurrentRendererSettings();
        }
    }
}

