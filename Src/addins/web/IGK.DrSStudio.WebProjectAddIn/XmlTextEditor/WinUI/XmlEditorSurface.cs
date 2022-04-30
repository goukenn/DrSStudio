

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XmlEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XmlEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.XmlTextEditor.WinUI
{
    using IGK.DrSStudio.XmlTextEditor.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;
    [CoreEditor ("XmlEditor")]
    /// <summary>
    /// xml Editor
    /// </summary>
    public class XmlEditorSurface : 
        IGKXWinCoreWorkingSurface, 
        ICoreWorkingEditorSurface
    {
        public XmlEditorSurface()
        {
        }
        public bool Open(string filename)
        {
            return false;
        }
        private string m_FileName;
        private bool m_saving;

        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;
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


        public void RenameTo(string name)
        {
            
        }

        public void ReloadFileFromDisk()
        {
            
        }

        public bool Saving
        {
            get {return this.m_saving; }
            set { this.m_saving = value; }
        }
        private bool m_NeedToSaved;

        public bool NeedToSave
        {
            get { return m_NeedToSaved; }
            set
            {
                if (m_NeedToSaved != value)
                {
                    m_NeedToSaved = value;
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
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }


        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.SaveXMLAs".R(),
                "XML File |*.xml",
                this.FileName);
        }

        public void SaveAs(string filename)
        {
            
        }
        public void Save()
        {
        }
    }
}

