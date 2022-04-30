using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Android.Xamarin.WinUI
{
    using IGK.DrSStudio.Android.Xamarin.Xml;
    using IGK.ICore;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;
    using IGK.ICore.Xml;
    using System.IO;

    [CoreSurface("256E9789-89DA-4EB3-85D5-2141E07923A4",
         EnvironmentName="XamarinAndroid")]
    public class XamarinEditorSurface : IGKXWinCoreWorkingSurface,
        ICoreWorkingFilemanagerSurface 
    {

        private IGKXWebBrowserControl c_webControl; // for projet edition
        private ICSharpCode.AvalonEdit.TextEditor c_textEditor; //for text edition
        private XamarinProjectXmlElement m_Project;

        public XamarinProjectXmlElement Project
        {
            get { return m_Project; }
            set
            {
                if (m_Project != value)
                {
                    m_Project = value;
                }
            }
        }

        private XamarinProjectItemElement  m_SelectedItem;

        /// <summary>
        /// get or set the selected item
        /// </summary>
        public XamarinProjectItemElement  SelectedItem
        {
            get { return m_SelectedItem; }
            set
            {
                if (m_SelectedItem != value)
                {
                    m_SelectedItem = value;
                    OnSelectedItemChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedItemChanged;

        private void OnSelectedItemChanged(EventArgs eventArgs)
        {
            if (this.SelectedItemChanged != null)
            {
                this.SelectedItemChanged(this, eventArgs);
            }
        }

     
        
        public XamarinEditorSurface()
        {//xamarin editor
            c_webControl = new IGKXWebBrowserControl();
            c_textEditor = new ICSharpCode.AvalonEdit.TextEditor();

            this.SuspendLayout();

            this.ResumeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e); 
        }
        public override string Title
        {
            get
            {
                if (this.m_Project != null)
                    return Path.GetFileNameWithoutExtension(this.m_Project.FileName);
                return base.Title;
            }
            protected set
            {
                base.Title = value;
            }
        }
        /// <summary>
        /// load and reload properties
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        internal void LoadProject(string name, CoreXmlElement item)
        {
            XamarinProjectXmlElement p = new XamarinProjectXmlElement()
            {
                Name = name 
            };
            p.Load(item);
            this.Project= p;
        }

        public string FileName
        {
            get {
                return this.m_Project.FileName; 
            }
            set
            {
                if (this.m_Project.FileName == value)
                {
                    this.m_Project.FileName = value;
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
        public void RenameTo(string name)
        {
        }

        public void ReloadFileFromDisk()
        {
        }

        public bool Saving
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
        public event EventHandler Saved;
        ///<summary>
        ///raise the Saved 
        ///</summary>
        protected virtual void OnSaved(EventArgs e)
        {
            if (Saved != null)
                Saved(this, e);
        }
        public void Save()
        {
            this.SaveAs(this.FileName);
            this.NeedToSave = true;
        }

        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("title.saveProject".R(), "Android project | *.csproj", this.FileName);
        }

        public void SaveAs(string filename)
        {
            File.WriteAllText(filename, this.Project.RenderXML(null));
            this.NeedToSave = true;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // XamarinEditorSurface
            // 
            this.Name = "XamarinEditorSurface";
            this.Size = new System.Drawing.Size(660, 341);
            this.ResumeLayout(false);

        }
    }
}
