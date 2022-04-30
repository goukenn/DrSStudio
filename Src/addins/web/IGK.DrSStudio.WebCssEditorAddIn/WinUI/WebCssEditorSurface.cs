

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebCssEditorSurface.cs
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
using System.Threading.Tasks;

namespace IGK.DrSStudio.WebCssEditorAddIn.WinUI
{
    using ICSharpCode.AvalonEdit;
    using ICSharpCode.AvalonEdit.Editing;
    using ICSharpCode.AvalonEdit.Highlighting;
    using IGK.ICore.Actions;
    using IGK.ICore.Mecanism;
    using IGK.DrSStudio.WebCssEditorAddIn.Actions;
    using IGK.ICore.WinUI.Configuration;
    using IGK.ICore.Wpf;
    using System.IO;
    using System.Windows.Forms.Integration;
    [CoreSurface ("WebCssTextEditor")]
    class WebCssEditorSurface :
        IGKXWinCoreWorkingRecordableSurface,
        ICoreWorkingRecordableSurface
    {
        private ElementHost c_elementHost;
        private TextEditor c_editor;
        private WebCssEditorMecanism m_mecanism;
        public TextArea TextArea{
            get {
                return this.c_editor.TextArea;
            }
        }
        public class WebCssEditorMecanism : CoreMecanismBase
        {
            private WebCssEditorSurface m_webCssEditorSurface;

            protected override void GenerateActions()
            {
                this.Actions.Clear();
                foreach (WebCssEditorActions t in WebCssEditorActions.GetActions(this))
                {
                    this.AddAction(t.Key, t);
                }
            }

            public WebCssEditorMecanism(WebCssEditorSurface webCssEditorSurface)
            {
                this.AllowActions = true;
                this.m_webCssEditorSurface = webCssEditorSurface;
                this.Register(webCssEditorSurface);
                
            }
            public override bool CanProcessActionMessage(ICoreMessage message)
            {
                bool v = m_webCssEditorSurface.Visible;
                var e = CoreSystem.GetWorkbench();
                if (v && (e!=null) && (e.CurrentSurface == this.m_webCssEditorSurface))
                    return true;
                return false;
            }

            protected override ICoreMecanismActionCollections CreateActionMecanismCollections()
            {
                return new AndoirManifestMecanismActionBaseCollections(this);
            }

            protected override ICoreSnippet CreateSnippet(int demand, int index)
            {
                throw new NotImplementedException();
            }

            protected override ICoreSnippetCollections CreateSnippetCollections()
            {
                return null;
            }

            public override void Edit(ICoreWorkingObject e)
            {
            }

            public override void Freeze()
            {
            }

            protected override ICoreWorkingConfigurableObject GetEditElement()
            {
                return null;
            }

            public override void Invalidate()
            {
            }

            public override bool IsFreezed
            {
                get { return false; }
            }

            public override bool Register(ICoreWorkingSurface surface)
            {
                return base.Register(surface);
            }

            public override ICoreWorkingSurface Surface
            {
                get {
                    return this.m_webCssEditorSurface;
                }
            }

            public override void UnFreeze()
            {
            }

            public override bool UnRegister()
            {
                return base.UnRegister();
            }

            sealed class AndoirManifestMecanismActionBaseCollections : CoreMecanismActionBaseCollections
            {
                public AndoirManifestMecanismActionBaseCollections(WebCssEditorMecanism mecanism)
                    : base(mecanism)
                {

                }
                public override bool IsNotAvailable(ICoreMessage m)
                {
                    return base.IsNotAvailable(m);
                }
            }
        }

        public WebCssEditorSurface()
        {
            c_editor = new TextEditor();
            this.InitializeComponent();
            this.Load += _Load;
            this.FileNameChanged += _FileNameChanged;
        }

        private void _FileNameChanged(object sender, EventArgs e)
        {
            this.Title = Path.GetFileName(this.FileName);
        }

        public void LoadFile(string filename)
        {
            if (File.Exists(filename))
            {
                this.c_editor.Text = File.ReadAllText(filename);
                this.FileName = filename;
                
            }
        }

        private void _Load(object sender, EventArgs e)
        {
            this.m_mecanism = new WebCssEditorMecanism(this);
   
            c_editor.SyntaxHighlighting =
                HighlightingManager.Instance.GetDefinitionByExtension(".css");
            WpfExtensions.InitTextEditor(c_editor);
            
            c_editor.ShowLineNumbers = true;
            c_editor.TextChanged += c_editor_TextChanged;
            this.c_elementHost.Child = c_editor;
        }

        void c_editor_TextChanged(object sender, EventArgs e)
        {
            this.NeedToSave = true;
        }

        public override void Save()
        {
            this.SaveAs(this.FileName);
        }
        public override void SaveAs(string filename)
        {
            try
            {
                this.c_editor.Save(filename);
                this.FileName = filename;
                this.NeedToSave = false;
            }
            catch{

            }
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
            this.c_elementHost.Size = new System.Drawing.Size(557, 308);
            this.c_elementHost.TabIndex = 0;
            this.c_elementHost.Text = "elementHost1";
            this.c_elementHost.Child = null;
            // 
            // WebCssEditorSurface
            // 
            this.Controls.Add(this.c_elementHost);
            this.Name = "WebCssEditorSurface";
            this.Size = new System.Drawing.Size(557, 308);
            this.ResumeLayout(false);

        }
    }
}
