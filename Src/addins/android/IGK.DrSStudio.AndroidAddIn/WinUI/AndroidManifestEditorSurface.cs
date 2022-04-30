

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidManifestEditorSurface.cs
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
file:AndroidManifestEditorSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace IGK.DrSStudio.Android.WinUI
{
    
using IGK.ICore.WinUI ;
    using ICSharpCode.AvalonEdit;
    using System.Windows.Forms.Integration;
    using IGK.ICore.Settings;
    using IGK.ICore.Mecanism;
    using IGK.ICore.Actions;
    using IGK.DrSStudio.Android.Actions;
    using ICSharpCode.AvalonEdit.Editing;
    
    
    using IGK.ICore.Tools;
    using IGK.ICore;
    using IGK.ICore.WinUI.Configuration;
    using IGK.DrSStudio.Android.Tools;
    using IGK.ICore.IO;

    [CoreSurface ("AndroidManifestEditor",
        EnvironmentName="{461E4685-2561-4F37-A135-6823ADF98766}")]
    /// <summary>
    /// represent a manifest editor surface
    /// </summary>
    public class AndroidManifestEditorSurface :
        AndroidSurfaceBase, 
        ICoreWorkingSurface,
        ICoreWorkingSaveSurface
    {
        private AndroidManifest m_Manifest; // get android manifist
        private string m_FileName;
        private Mecanism m_mecanism;
        public TextArea TextArea { get { return this.c_textEditor.TextArea; } }

        public class Mecanism : CoreMecanismBase
        {
            private AndroidManifestEditorSurface m_surface;
            
            public Mecanism(AndroidManifestEditorSurface surface):base()
            {
                if (surface == null)
                    throw new ArgumentNullException("surface");

                this.m_surface = surface;
                this.Register(surface);



                this.AllowContextMenu = true;
            }
            protected override void GenerateActions()
            {
                if (this.m_surface == null) return;
                foreach(AndroidManifestActions ack in  AndroidManifestActions.GetActions(this.m_surface))
                {
                    this.AddAction(ack.Key, ack);
                }

            }

            public override bool CanProcessActionMessage(ICoreMessage message)
            {
                var e = CoreSystem.GetWorkbench();
                if (e.CurrentSurface == this.m_surface)
                    return true;
                return false ;
//                return !((this.CurrentSurface.IsDisposed) ||
//(message.HWnd != this.CurrentSurface.Handle) ||
//MenuActionMessageFiltering || !this.AllowActions);
            }

            protected override ICoreMecanismActionCollections CreateActionMecanismCollections()
            {
                return new AndoirManifestMecanismActionBaseCollections(this);
            }


            class AndoirManifestMecanismActionBaseCollections : CoreMecanismActionBaseCollections
            {
                public AndoirManifestMecanismActionBaseCollections(Mecanism mecanism):base(mecanism)
                {

                }
            }

            protected override ICoreSnippet CreateSnippet(int demand, int index)
            {
                return null;
            }

            protected override ICoreSnippetCollections CreateSnippetCollections()
            {
                return null;
            }

            protected override ICoreWorkingConfigurableObject GetEditElement()
            {
                return null;
            }

            public override void Invalidate()
            {
                throw new NotImplementedException();
            }

            public override ICoreWorkingSurface Surface
            {
                get {
                    return this.m_surface;
                }
            }

            public override void Edit(ICoreWorkingObject e)
            {
                throw new NotImplementedException();
            }

            public override void Freeze()
            {
                throw new NotImplementedException();
            }

            public override bool IsFreezed
            {
                get { return false; }
            }

            public override bool Register(ICoreWorkingSurface surface)
            {
                base.CurrentSurface = surface;
                this.GenerateActions();
                CoreActionRegisterTool.Instance.AddFilterMessage(this.Actions);
                return true;
            }

            public override void UnFreeze()
            {
                
            }

            public override bool UnRegister()
            {
                CoreActionRegisterTool.Instance.RemoveFilterMessage(this.Actions);
                return true;
            }
        }


        public string FileName
        {
            get { return m_FileName; }
            set
            {
                if (m_FileName != value)
                {
                    m_FileName = value;

                    if (File.Exists(this.m_FileName))
                    {
                        this.Title = Path.GetFileName(value);
                    }
                    OnFileNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FileNameChanged;
        private TextEditor c_textEditor;
        private ElementHost c_elementHost;
        private bool m_configuring;

        ///<summary>
        ///raise the FileNameChanged 
        ///</summary>
        protected virtual void OnFileNameChanged(EventArgs e)
        {
            if (FileNameChanged != null)
                FileNameChanged(this, e);
        }
        /// <summary>
        /// get the manifest
        /// </summary>
        public AndroidManifest Manifest{
             get{ return m_Manifest;}      
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public AndroidManifestEditorSurface()
        {//init
            this.m_mecanism = new Mecanism(this);
            this.InitializeComponent();
            this.m_Manifest = new AndroidManifest();
            this.c_elementHost.Child = c_textEditor;
            this.c_textEditor = new TextEditor();
            this.c_elementHost.Child = this.c_textEditor;
            
            this.Load += _Load;
            
        }

        private void InitializeComponent()
        {
            this.c_elementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            // 
            // c_elementHost
            // 
            this.c_elementHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_elementHost.Location = new System.Drawing.Point(0, 0);
            this.c_elementHost.Name = "c_elementHost";
            this.c_elementHost.Size = new System.Drawing.Size(548, 407);
            this.c_elementHost.TabIndex = 0;
            this.c_elementHost.Child = null;
            // 
            // AndroidManifestEditorSurface
            // 
            this.Controls.Add(this.c_elementHost);
            this.Name = "AndroidManifestEditorSurface";
            this.Size = new System.Drawing.Size(548, 407);
            this.ResumeLayout(false);

        }

        void initTextEditor()
        {
            this.c_textEditor.FontFamily = ((string)CoreSettings.GetSettingValue("CodeEditorEnvironment.FontName", "consolas")).WpfFontFamily();
            this.c_textEditor.FontSize = (float)Convert.ToSingle(CoreSettings.GetSettingValue("CodeEditorEnvironment.FontSize", 12.0f));
          
        }
        void _Load(object sender, EventArgs e)
        {
            this.c_textEditor.SyntaxHighlighting =
                ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(".xml");

            
            this.c_textEditor.Text = this.m_Manifest.Render();
            this.c_textEditor.ShowLineNumbers = true;

            m_foldingManager = ICSharpCode.AvalonEdit.Folding.FoldingManager.Install(this.c_textEditor.TextArea);
            m_xml = new ICSharpCode.AvalonEdit.Folding.XmlFoldingStrategy();
            m_xml.UpdateFoldings(m_foldingManager, c_textEditor.Document);
            
            this.c_textEditor.TextChanged += c_textEditor_TextChanged;
            this.c_textEditor.DocumentChanged += c_textEditor_DocumentChanged;
            this.initTextEditor();
            if (AndroidTargetManagerTool.Instance.TargetInfo == null)
                AndroidTargetManagerTool.Instance.TargetInfo = AndroidSystemManager.GetLastAndroidTargets();
        }

        void c_textEditor_DocumentChanged(object sender, EventArgs e)
        {
            ICSharpCode.AvalonEdit.Folding.FoldingManager.Uninstall (m_foldingManager );            
        }
        private bool m_NeedToSaved;

        public bool NeedToSaved
        {
            get { return m_NeedToSaved; }
            set
            {
                if (m_NeedToSaved != value)
                {
                    m_NeedToSaved = value;
                    OnNeedToSavedChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NeedToSavedChanged;
        private ICSharpCode.AvalonEdit.Folding.XmlFoldingStrategy m_xml;
        private ICSharpCode.AvalonEdit.Folding.FoldingManager m_foldingManager;
        ///<summary>
        ///raise the NeedToSavedChanged 
        ///</summary>
        protected virtual void OnNeedToSavedChanged(EventArgs e)
        {
            if (NeedToSavedChanged != null)
                NeedToSavedChanged(this, e);
        }


        void c_textEditor_TextChanged(object sender, EventArgs e)
        {
            this.NeedToSaved = true;
            if (this.m_configuring)
            {
                return;
            }
            this.m_configuring = true;
            m_xml.UpdateFoldings(m_foldingManager, c_textEditor.Document);
            //bool r = AndroidManifest.CheckManisfest(this.c_textEditor.Text);
            //if (r)
            //{
            //   // this.m_Manifest.LoadContent(this.c_textEditor.Text);
            //}
            //else 
            //{ 

            //}

            this.m_configuring = false;
        }
        public void Save() 
        {
            bool r = AndroidManifest.CheckManisfest(this.c_textEditor.Text);
            if (r)
            {
                this.m_Manifest.LoadContent(this.c_textEditor.Text);
            }
            else { 
                
            }
            this.m_Manifest.SaveToDirectory(PathUtils.GetDirectoryName (this.FileName));
        }
        public void SaveAs(string filename)
        {
            if (this.FileName != filename)
            {
                if (File.Exists(this.FileName))
                {
                    File.Delete(this.FileName);
                }
            }
            this.m_Manifest.SaveToDirectory(PathUtils.GetDirectoryName(filename));
            this.FileName = filename;
            this.NeedToSaved = false;
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo(
                "title.saveandroidmanisttodirectory".R(),
                "android manifest xml|*.xml",
                "AndroidManifest.xml"
                );
        }

        internal void SetUp(AndroidManifest c, string filename)
        {
            this.m_Manifest = c;
            this.m_FileName = filename;
            this.Title = Path.GetFileName(filename);
        }

        
    }
}

