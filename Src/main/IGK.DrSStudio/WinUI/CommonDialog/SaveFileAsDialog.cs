using IGK.ICore.WinUI;
using IGK.ICore.WinUI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WinUI.CommonDialog
{

    public sealed class SaveFileAsDialog : IXCommonDialog, IXCommonSaveFileAsDialog
    {
        global::System.Windows.Forms.SaveFileDialog m_sfd;

        public SaveFileAsDialog()
        {
            this.m_sfd = new System.Windows.Forms.SaveFileDialog();
        }
        public string Title
        {
            get
            {
                return this.m_sfd.Title;
            }
            set
            {
                this.m_sfd.Title = value;
            }
        }

        public enuDialogResult ShowDialog()
        {
            return (enuDialogResult)this.m_sfd.ShowDialog();
        }

        public void Dispose()
        {
            if (this.m_sfd != null)
            {
                this.m_sfd.Dispose();
                this.m_sfd = null;
            }
        }

        public string FileName
        {
            get
            {
                return this.m_sfd.FileName;
            }
            set
            {
                this.m_sfd.FileName = value;
            }
        }

        public string Filter
        {
            get
            {
                return this.m_sfd.Filter;
            }
            set
            {
                this.m_sfd.Filter = value;
            }
        }
    }
}
