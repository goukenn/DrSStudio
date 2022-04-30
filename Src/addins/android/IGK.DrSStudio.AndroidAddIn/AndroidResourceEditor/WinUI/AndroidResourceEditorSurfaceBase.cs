
using IGK.ICore;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Android.WinUI
{
    /// <summary>
    /// present a android surface editor base
    /// </summary>
    public class AndroidResourceEditorSurfaceBase : IGKXWinCoreWorkingSurface, ICoreWorkingFilemanagerSurface
    {
        private string m_FileName;

        public string FileName
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
        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }

        public virtual void RenameTo(string name)
        {
        }

        public virtual void ReloadFileFromDisk()
        {
            
        }

        public virtual  bool Saving
        {
            get { return false; }
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
        public event EventHandler NeedToSaveChanged;
        ///<summary>
        ///raise the NeedToSaveChanged 
        ///</summary>
        protected virtual void OnNeedToSaveChanged(EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
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
            this.FileName = filename;
            this.Save();
        }


        public event EventHandler Saved;
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }

    }
}
