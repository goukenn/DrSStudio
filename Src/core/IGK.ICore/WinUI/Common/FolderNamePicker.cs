using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI.Common
{
    public abstract class FolderNamePicker : IXCommonDialog
    {
        private string m_SelectedFolder;

        public string SelectedFolder
        {
            get { return m_SelectedFolder; }
            set
            {
                if (m_SelectedFolder != value)
                {
                    m_SelectedFolder = value;
                }
            }
        }

        public string Title
        {
            get;
            set;
        }
        public abstract enuDialogResult ShowDialog();

        public virtual void Dispose() { }
    }
}
