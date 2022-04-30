using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinCore.WinUI.Controls
{
    /// <summary>
    /// reprenset a base file manager surface 
    /// </summary>
    public class IGKXWinCoreWorkingFileManagerSurface : IGKXWinCoreWorkingSurface, ICoreWorkingFilemanagerSurface
    {
        private string m_FileName;

        /// <summary>
        /// get or set the filename of this working file surface
        /// </summary>
        public virtual string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;

        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
            {
                FileNameChanged(this, e);
            }
        }


      
        public virtual void RenameTo(string name)
        {
        }

        public virtual void ReloadFileFromDisk()
        {
        }

        public bool Saving
        {
            get { return this.m_saving; }
            protected set { this.m_saving = value; }
        }
        private bool m_NeedToSave;

        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            set
            {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }

        private void OnNeedToSaveChanged(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, EventArgs.Empty);
        }
        public event EventHandler NeedToSaveChanged;

     
      

        public event EventHandler Saved;
        private bool m_saving;

        protected virtual void OnSaved(EventArgs e) {
            if (this.Saved != null) {
                this.Saved(this, e);
            }
        }

        public virtual void Save()
        {
            
        }

        public virtual ICoreSaveAsInfo GetSaveAsInfo()
        {
            return null;
        }

        public virtual void SaveAs(string filename)
        {
        }


     
    }
}
