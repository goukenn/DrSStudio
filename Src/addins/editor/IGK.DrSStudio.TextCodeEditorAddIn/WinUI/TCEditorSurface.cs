

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: TCEditorSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.TextCodeEditorAddIn.WinUI
{
    [CoreSurface("{BE871DE5-1105-48C8-B077-95E88CAC2ECF}",
        EnvironmentName = "{A4551F3C-A1E3-490E-BF81-F84DF7BA7170}")]
    public class TCEditorSurface : 
        IGKXWinCoreWorkingSurface,
        ICoreWorkingFilemanagerSurface ,
        ICoreWorkingEditorSurface 
    {
        private TCEditorAvalonEditControl c_avalonEdit;
        private bool m_needToSave;
        private string m_fileName;
        private bool m_Saving;

        /// <summary>
        /// get if this surface is in saving mode
        /// </summary>
        public bool Saving
        {
            get { return m_Saving; }
            protected set
            {
                if (m_Saving != value)
                {
                    m_Saving = value;
                }
            }
        }

        public event EventHandler Saved;

        /// <summary>
        /// raise the saved eventhandler
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSaved(EventArgs e)
        {
            this.NeedToSave = false;
            if (this.Saved != null)
                this.Saved(this, e);
        }
        public new string Title
        {
            get { return base.Title; }
            set
            {
                base.Title = value;
            }
        }
        public override string Text
        {
            get
            {
                return c_avalonEdit.Text;
            }
            set
            {
                c_avalonEdit.Text  = value;
            }
        }
        public TCEditorSurface()
        {
            this.InitializeComponent();
            this.FileNameChanged += _FileNameChanged;
            this.c_avalonEdit.TextEditorChanged += c_avalonEdit_TextChanged;
        }

        void c_avalonEdit_TextChanged(object sender, EventArgs e)
        {
            this.NeedToSave = true;
        }

        private void _FileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.FileName);
        }

      

        private void InitializeComponent()
        {
            this.c_avalonEdit = new IGK.DrSStudio.TextCodeEditorAddIn.WinUI.TCEditorAvalonEditControl();
            this.SuspendLayout();
            // 
            // tcEditorAvalonEditControl1
            // 
            this.c_avalonEdit.CaptionKey = null;
            this.c_avalonEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_avalonEdit.Location = new System.Drawing.Point(0, 0);
            this.c_avalonEdit.Name = "tcEditorAvalonEditControl1";
            this.c_avalonEdit.Size = new System.Drawing.Size(562, 310);
            this.c_avalonEdit.TabIndex = 0;
            // 
            // TCEditorSurface
            // 
            this.Controls.Add(this.c_avalonEdit);
            this.Name = "TCEditorSurface";
            this.Size = new System.Drawing.Size(562, 310);
            this.ResumeLayout(false);

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }
        /// <summary>
        /// get or set the hight light definition strategie
        /// </summary>
        public ICSharpCode.AvalonEdit.Highlighting.IHighlightingDefinition
            HightLight { get { return this.c_avalonEdit.HightLight;  }
                set {
                    this.c_avalonEdit.HightLight = value;
                }
            }

        public string FileName
        {
            get
            {
                return this.m_fileName;
            }
            set
            {
                if (this.m_fileName != value )
                {
                    this.m_fileName = value;
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }

        private void OnFileNameChanged(EventArgs eventArgs)
        {
            if (this.FileNameChanged != null)
            {
                this.FileNameChanged(this, eventArgs);
            }
        }

        public event EventHandler FileNameChanged;

        public void RenameTo(string filename)
        {
            
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.SaveFileEditor".R(),
                "FILE | *"+Path.GetExtension(this.FileName)+";",
                this.FileName); 
        }

        public bool NeedToSave
        {
            get {
                return this.m_needToSave;
            }
            set {
                if (this.m_needToSave != value)
                {
                    this.m_needToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }

        private void OnNeedToSaveChanged(EventArgs eventArgs)
        {
            if (this.NeedToSaveChanged != null)
                this.NeedToSaveChanged(this, eventArgs);
        }

        public event EventHandler NeedToSaveChanged;


        public void Save()
        {
            if (File.Exists(this.FileName))
            {
                File.WriteAllText(this.FileName, this.c_avalonEdit.Text);
            }
            else {
                CoreSystem.Instance.Workbench.CallAction("File.SaveAs");                
            }
        }

        public void SaveAs(string filename)
        {
            File.WriteAllText(filename, this.c_avalonEdit.Text);
            this.NeedToSave = false;
            this.FileName = filename;

        }

        public bool Open(string filename)
        {
            if (File.Exists(filename))
            {
                this.FileName = filename;
                this.Text = File.ReadAllText(filename);// "//Enter your text here-----------------------------";
                this.c_avalonEdit.HightLight = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(
                    Path.GetExtension(filename));
                this.NeedToSave = false;
                return true;
            }
            return false;
        }


        public void ReloadFileFromDisk()
        {
            if (File.Exists(this.FileName))
            {
                this.Text = File.ReadAllText(this.FileName);
                this.NeedToSave = false;
            }
        }
    }
}
