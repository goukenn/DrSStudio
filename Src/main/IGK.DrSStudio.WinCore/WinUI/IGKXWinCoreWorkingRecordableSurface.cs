

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKXWinCoreWorkingRecordableSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.WinUI
{
    /// <summary>
    /// represent a base recordable surface 
    /// </summary>
    public class IGKXWinCoreWorkingRecordableSurface : 
        IGKXWinCoreWorkingSurface,
        ICoreWorkingRecordableSurface,
        ICoreWorkingFilemanagerSurface 
    {

        private bool m_NeedToSave;
       
        public bool Saving
        {
            get { return this.m_saving; }
            protected set {
                this.m_saving = value;
            }
        }
       

        public bool NeedToSave
        {
            get { return m_NeedToSave; }
            set
            {
                if (m_NeedToSave != value)
                {
                    m_NeedToSave = value;
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

        public event EventHandler Saved;
        private bool m_saving;
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }


        public virtual void Save()
        {
            
        }

        public virtual ICoreSaveAsInfo GetSaveAsInfo()
        {
            return null;
        }

        public virtual void SaveAs(string filename){
        }
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
    }
}
