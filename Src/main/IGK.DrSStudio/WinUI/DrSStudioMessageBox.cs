

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: DrSStudioMessageBox.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:DrSStudioMessageBox.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    [CoreMessageBoxAttribute()]
    public class DrSStudioMessageBox : ICoreMessageBox 
    {
        DrSStudioConfirmDialog m_confirmDialog;
        private DrSstudioNotifyBoxForm m_notifyForm;

        public enuDialogResult Show(string p)
        {
            return (enuDialogResult)MessageBox.Show(p, "IGKDEV");
        }
        public enuDialogResult ShowError(CoreException ex)
        {
            return (enuDialogResult)MessageBox.Show(ex.Message , "CoreException");
        }
        public enuDialogResult Show(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ex.Message);
#if DEBUG
            sb.AppendLine(ex.StackTrace);
#endif

            return (enuDialogResult)MessageBox.Show(sb.ToString(), "Exception - "+ex.Source);
        }


        public enuDialogResult Show(string message, string title)
        {
            return (enuDialogResult)MessageBox.Show(message, title);
        }

        public enuDialogResult Show(Exception ex, string title)
        {
            return (enuDialogResult)MessageBox.Show(ex.Message, title);
        }


        public enuDialogResult Confirm(string message)
        {
            if ((this.m_confirmDialog == null)||(this.m_confirmDialog.IsDisposed ))
            {
                this.m_confirmDialog = new DrSStudioConfirmDialog();
            }
            this.m_confirmDialog.Message = message;
            this.m_confirmDialog.Owner = (Form) CoreSystem.GetMainForm();
            return this.m_confirmDialog.ShowDialog();

        }


        public enuDialogResult ShowError(string title, string message)
        {
            return enuDialogResult.Yes;
        }

        public enuDialogResult ShowInfo(string title, string message)
        {
            return enuDialogResult.Yes;
        }

        public enuDialogResult ShowWarning(string title, string message)
        {
            return enuDialogResult.Yes;
        }


        public enuDialogResult Show(string message, string title, enuCoreMessageBoxButtons boxbutton)
        {
            return enuDialogResult.Yes;
        }


        public void NotifyMessage(string title, string message)
        {
            if((m_notifyForm ==null) || (m_notifyForm.IsDisposed ))
            m_notifyForm = new DrSstudioNotifyBoxForm();
            {
                m_notifyForm.Title = title;
                m_notifyForm.Message = message;
                m_notifyForm.ShowDialog();
            }
        }
    }
}

