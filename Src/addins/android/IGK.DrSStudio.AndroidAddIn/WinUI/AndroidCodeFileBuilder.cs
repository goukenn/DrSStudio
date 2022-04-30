

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidCodeFileBuilder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.CodeCompletion;

using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms.Integration;

namespace IGK.DrSStudio.Android.WinUI
{

    using IGK.ICore;

    [CoreSurface ("AndroidFileSurface")]
    /// <summary>
    /// represent the base class code file builder
    /// </summary>
    public class AndroidCodeFileBuilder : AndroidSurfaceBase, ICoreWorkingFilemanagerSurface 
    {
         private TextEditor c_textEditor;
        private ElementHost c_elementHost;
        protected TextEditor TextEditor {
            get {
                return this.c_textEditor;
            }
        }
        
        public override string Title
        {
            get
            {
                return base.Title;
            }
            protected set
            {
                base.Title = value;
            }
        }

        public AndroidCodeFileBuilder()
            : base()
        {
            this.InitializeComponent();
            this.c_textEditor = new ICSharpCode.AvalonEdit.TextEditor();
            this.c_elementHost.Child = this.c_textEditor;
            this.Dock = System.Windows.Forms.DockStyle.Fill;

            //setup device 
            this.c_textEditor.FontSize = "12px".ToPixel();
            this.c_textEditor.FontFamily = "consolas".WpfFontFamily();
    
            this.c_textEditor.ShowLineNumbers = true;
            this.c_textEditor.WordWrap = false;
            this.c_textEditor.TextArea.TextEntering += TextArea_TextEntering;
            this.c_textEditor.TextArea.KeyUp += TextArea_KeyUp;
            this.Title = Path.GetFileName(this.FileName);
            this.FileNameChanged += _FileNameChanged;
            this.InitControl();
                
        }
        protected virtual void InitControl()
        { 
        }

        void _FileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.FileName);
        }

        void TextArea_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            if (e.Key == System.Windows.Input.Key.Space)
            { 
            }
        }
        //CompletionWindow m_completionWindow;

      

        void TextArea_TextEntering(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            //if (e.Text.Length > 0 && m_completionWindow != null)
            //{
            //    if (!char.IsLetterOrDigit(e.Text[0]))
            //    {
            //        // Whenever a non-letter is typed while the completion window is open,
            //        // insert the currently selected element.
            //        m_completionWindow.CompletionList.RequestInsertion(e);
            //    }
            //}
        }

        private void InitializeComponent()
        {
            this.c_elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.c_elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_elementHost.Location = new System.Drawing.Point(0, 0);
            this.c_elementHost.Name = "elementHost1";
            this.c_elementHost.Size = new System.Drawing.Size(150, 150);
            this.c_elementHost.TabIndex = 0;
            this.c_elementHost.Text = "elementHost1";
            this.c_elementHost.Child = null;
            // 
            // AndroidJavaFileCodeBuilder
            // 
            this.Controls.Add(this.c_elementHost);
            this.Name = "AndroidJavaFileCodeBuilder";
            this.ResumeLayout(false);

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

        

        public void ReloadFileFromDisk()
        {
            this.LoadFile(this.FileName);
        }

        public virtual void LoadFile(string filename)
        {
            this.c_textEditor.Text = File.ReadAllText(filename);
            this.FileName = filename;
            this.TextEditor.SyntaxHighlighting =
                ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(
                Path.GetExtension(this.FileName));
        }

        public void RenameTo(string name)
        {
            
        }

        public virtual  ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "Save file As...",
                "All files|*.*",
                this.FileName 
                );
        }

        public bool NeedToSave
        {
            get
            {
                return this.m_needToSave;
            }
            set
            {
                if (this.m_needToSave != value)
                {
                    this.m_needToSave = value;

                }
            }
        }
        public event EventHandler NeedToSaveChanged;
        ///<summary>
        ///raise the NeedToSave 
        ///</summary>
        protected virtual void OnNeedToSave(EventArgs e)
        {
            if (NeedToSaveChanged != null)
                NeedToSaveChanged(this, e);
        }



        public void Save()
        {
            this.SaveAs(this.FileName);
        }

        public void SaveAs(string filename)
        {
            this.Saving = true;
            File.WriteAllText(filename, this.c_textEditor.Text);
            this.OnSaved(EventArgs.Empty);
            this.Saving = false;
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

        private bool m_saving;
        private bool m_needToSave;

        public bool Saving
        {
            get {
                return this.m_saving;
            }
            internal set {
                this.m_saving = value;
            }
        }
    }
}
