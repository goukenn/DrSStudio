

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _SplitDocument.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore.WinCore;

using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D.Menu;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:_SplitDocument.cs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore.WinUI.Common;
    using System.Windows.Forms;
    using WinUI;
    [DrSStudioMenu("Tools.SplitDocument", 11)]
    class _SplitDocument : Core2DDrawingMenuBase 
    {
        ICoreDialogForm m_dialog;
        protected override bool PerformAction()
        {
            if ((this.m_dialog == null) ||(this.m_dialog.IsDisposed ))
            {
                UIXSplitDocumentControl v_ctr = new UIXSplitDocumentControl(this.CurrentSurface.CurrentDocument);
                this.m_dialog = Workbench.CreateNewDialog(v_ctr);
                this.m_dialog.Title ="title.SplitDocumentDialog".R();
                m_dialog.Owner = this.MainForm;
                m_dialog.Closing += m_dialog_FormClosing;
                
                m_dialog.Show();
            }
            else {
                m_dialog.Show();
                this. m_dialog.Activate();
            }            
            return false;
        }

        private void m_dialog_FormClosing(object sender, CoreFormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case enuCloseReason.UserClose:
                case enuCloseReason.ApplicationExit :
                    break;
                default:
                    e.Cancel = true;
                    (sender as Form).Hide();
                    break;
            }
        }
    }
}

