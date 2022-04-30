

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: SelectFileAction.cs
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
file:SelectFileAction.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;

using IGK.ICore.Actions;
using IGK.ICore.WinUI.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Actions
{
    /// <summary>
    /// selected file
    /// </summary>
    public class SelectFileAction : CoreParameterActionBase , 
        IGK.ICore.WinUI.Configuration.ICoreParameterAction
    {
        private SelectFileDialog m_selectFileDialog;
        private string m_SelectedFile;
        private string m_Filter;
        private string m_Title;
        public event EventHandler SelectedFileChanged;
        ///<summary>
        ///raise the SelectedFileChanged 
        ///</summary>
        protected virtual void OnSelectedFileChanged(EventArgs e)
        {
            if (SelectedFileChanged != null)
                SelectedFileChanged(this, e);
        }
        /// <summary>
        /// get or set the title of the visible dialog box
        /// </summary>
        public string Title
        {
            get { return m_Title; }
            set
            {
                if (m_Title != value)
                {
                    m_Title = value;
                }
            }
        }
        /// <summary>
        /// Get or set the selected filter
        /// </summary>
        public string Filter
        {
            get { return m_Filter; }
            set
            {
                if (m_Filter != value)
                {
                    m_Filter = value;
                }
            }
        }
        /// <summary>
        /// get or set the selected file name
        /// </summary>
        public string SelectedFile
        {
            get { return m_SelectedFile; }
            set
            {
                if (m_SelectedFile != value)
                {
                    m_SelectedFile = value;
                    OnSelectedFileChanged(EventArgs.Empty);                    
                }
            }
        }
        public SelectFileAction(string title , string filter):base(title, title, null)
        {
            this.m_selectFileDialog = new SelectFileDialog(this);
            this.m_Filter = filter;
            this.m_Title = title;
        }
        public override string ToString()
        {
            return base.ToString();
        }
        class SelectFileDialog : CoreActionBase , ICoreAction
        {
            private SelectFileAction m_selectFileAction;
            public SelectFileDialog(SelectFileAction selectFileAction)
            {
                this.m_selectFileAction = selectFileAction;
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            protected override bool PerformAction()
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.FileName = this.m_selectFileAction.SelectedFile;
                    ofd.Filter = this.m_selectFileAction.Filter;
                    ofd.Title = this.m_selectFileAction.Title;
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        this.m_selectFileAction.SelectedFile = ofd.FileName;
                        return true;
                    }
                }
                return false;
            }
            public override string Id
            {
                get {                    
                    return "{1AAA397E-3913-4CDB-B34A-602BE7AD81F5}";
                }
            }
        }
    }
}

