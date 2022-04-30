

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionItemSurface.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.ICore.WinCore.WinUI.Controls;

namespace IGK.DrSStudio.Web.WinUI
{
    /// <summary>
    /// represent a surface for a drawing item element
    /// </summary>
    public class WebSolutionItemSurface : 
        IGKXWinCoreWorkingSurface,
        ICoreWorkingEditorSurface ,
        ICoreWorkingProjectSolutionSurface 
    {
        private WebSolutionSolution m_solution;

        private ICoreWorkingEditorSurface m_Editor;

        public ICoreWorkingEditorSurface Editor
        {
            get { return m_Editor; }
          
        }
        //.ctr      
        public WebSolutionItemSurface(
            WebSolutionSolution webSolutionSolution,
            ICoreWorkingEditorSurface editorSurface)
        {            
            this.m_solution = webSolutionSolution;
            this.m_Editor = editorSurface;
            this.m_Editor.TitleChanged += m_Editor_TitleChanged;
            System.Windows.Forms.Control c = this.m_Editor as System.Windows.Forms.Control;
            c.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Controls.Add(c);
        }
        public override string Title
        {
            get
            {
                return this.m_Editor.Title;
            }
            protected set
            {
                base.Title = value;
            }
        }
      

        void m_Editor_TitleChanged(object sender, EventArgs e)
        {
            OnTitleChanged(e);  
        }


        public bool Open(WebSolutionFile fileItem)
        {
            if (this.Open(fileItem.FileName))
            {
                this.m_fileItem = fileItem;
                return true;
            }
            this.m_fileItem = null;
            return false;
        }

        public WebSolutionSolution Solution
        {
            get { return this.m_solution; }
            set { this.m_solution = value; }
        }

        ICoreWorkingProjectSolution ICoreWorkingProjectSolutionSurface.Solution
        {
            get { return this.m_solution; }
        }

        public bool Open(string filename)
        {
            return this.m_Editor.Open(filename);
        }

        public string FileName
        {
            get
            {
                if (this.m_fileItem !=null)
                    return this.m_fileItem.FileName;
                return null;
            }
            set
            {
                if ((this.m_fileItem!=null) && (this.m_fileItem.FileName != value))
                {
                    this.m_fileItem.FileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler FileNameChanged;
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (this.FileName != null)
            {
                this.FileNameChanged(this, e);
            }
        }

        public void RenameTo(string name)
        {
            throw new NotImplementedException();
        }

        public void ReloadFileFromDisk()
        {
            
        }

        public bool Saving
        {
            get {
                return this.m_Editor.Saving;
            }
        }

        public bool NeedToSave
        {
            get
            {
                return this.m_Editor.NeedToSave;
            }
            set
            {
                this.m_Editor.NeedToSave = value;
            }
        }

        public event EventHandler NeedToSaveChanged {
            add {
                this.m_Editor.NeedToSaveChanged += value;
            }
            remove {
                this.m_Editor.NeedToSaveChanged -= value;
            }
        }

        public event EventHandler Saved{
            add
            {
                this.m_Editor.Saved += value;
            }
            remove
            {
                this.m_Editor.Saved -= value;
            }
        }
        private WebSolutionFile m_fileItem;

        public void Save()
        {
            this.m_Editor.Save();
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return this.m_Editor.GetSaveAsInfo();
        }

        public void SaveAs(string filename)
        {
            this.m_Editor.SaveAs(filename);
        }
    }
}
