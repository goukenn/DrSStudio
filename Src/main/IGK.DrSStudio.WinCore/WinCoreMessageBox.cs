

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WinCoreMessageBox.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio
{
    /// <summary>
    /// represent the base windows message box
    /// </summary>
    public class WinCoreMessageBox : Form, ICoreMessageBox
    {
        public WinCoreMessageBox()
        {

        }
        public virtual enuDialogResult Show(string messageScript)
        {
            return enuDialogResult.None;
        }

        public enuDialogResult ShowError(CoreException ex)
        {
            return enuDialogResult.None;
        }

        public enuDialogResult Show(Exception ex)
        {
            return enuDialogResult.None;
        }

        public enuDialogResult Show(string message, string title)
        {
            return enuDialogResult.None;
        }

        public enuDialogResult Show(Exception ex, string title)
        {
            return enuDialogResult.None;
        }

        public enuDialogResult Confirm(string message)
        {
            return enuDialogResult.None;
        }


        public virtual enuDialogResult ShowError(string title, string message)
        {
            return enuDialogResult.Yes;
        }

        public virtual enuDialogResult ShowInfo(string title, string message)
        {
            return enuDialogResult.Yes;
        }

        public virtual enuDialogResult ShowWarning(string title, string message)
        {
            return enuDialogResult.Yes;
        }


        public virtual enuDialogResult Show(string message, string title, enuCoreMessageBoxButtons boxbutton)
        {
            return enuDialogResult.Yes;
        }


        public void NotifyMessage(string title, string message)
        {
            throw new NotImplementedException();
        }
    }
}
