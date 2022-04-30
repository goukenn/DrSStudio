
using IGK.DrSStudio.WinUI;
using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Drawing3D.FileBrowser.WinUI
{
    [CoreSurface("FBControlSurface",
        GUID = "{5827D12D-1A89-48BE-AA41-1435FD82A7B0}")]
    public class FBControlSurface : 
        IGKXWinCoreWorkingSurface,
        IFBSurface,
        IFBDirectoryBrowser 
    {
        private FBDisplaySurface c_scene;
        private IListOfFilesCollections m_fileList;
        private FBControlSurfaceMecanism m_Mecanism;
        
        public FBControlSurfaceMecanism Mecanism
        {
            get { return m_Mecanism; }
        }
        public FBDisplaySurface Scene {
            get {
                return c_scene;
            }
        }
        private bool m_ShowText;
        private Colorf m_BackgroundColor;

        public Colorf BackgroundColor
        {
            get { return m_BackgroundColor; }
            set
            {
                if (m_BackgroundColor != value)
                {
                    m_BackgroundColor = value;
                }
            }
        }
        public bool ShowText
        {
            get { return m_ShowText; }
            set
            {
                if (m_ShowText != value)
                {
                    m_ShowText = value;
                }
            }
        }

        public override string Title
        {
            get
            {
                return FBConstant.FILEBROWSER_TITLE.R();
            }
            protected set
            {
                base.Title = value;
            }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        public FBControlSurface()
        {
            this.m_SelectedFileIndex = -1;
            this.m_fileList = new FBFileListCollections(this);
            this.m_Mecanism = new FBControlSurfaceMecanism(this);
            //this.c_barAddress = new FBAddressBar(this);
            c_scene = new FBDisplaySurface(this);
            this.Controls.Add(c_scene);
            c_scene.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_scene.Click += c_scene_Click;
            this.c_scene.MouseClick += c_scene_MouseClick;
            this.InitializeComponent();
        }

        void c_scene_MouseClick(object sender, CoreMouseEventArgs e)
        {
            base.OnCoreMouseClick(e);
        }

        void c_scene_Click(object sender, EventArgs e)
        {
            this.OnClick(EventArgs.Empty);
        }

        void InitializeComponent()
        {
            c_toolBar = new FBToolBar(this);
            c_toolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Controls.Add(c_toolBar);
        }


        public IFBDirectoryBrowser DirectoryBrowser { get { return null; } }
        public IFBAddressBar AddressBar { get { return this.c_toolBar; } }
        //public IFileMultifileView MultiFileView { get { return 
        //    m_multiViewProperty
        //    ; } }
        public IFileMainView MainView { get { return null; } }
        private int m_SelectedFileIndex;

        public int SelectedFileIndex
        {
            get {
                if (this.Files.Count == 0)
                {
                    m_SelectedFileIndex = -1;
                }
                 return m_SelectedFileIndex; }
            set
            {
                if ((m_SelectedFileIndex != value) && ( (value >=0) && (value <this.Files.Count ) || (value == -1)))
                {
                    m_SelectedFileIndex = value;
                    OnSelectedFileIndexChanged(EventArgs.Empty);
                }
            }
        }

        
        public IListOfFilesCollections Files { get { return this.m_fileList; } }

        public event EventHandler SelectedFileIndexChanged;
        ///<summary>
        ///raise the SelectedFileIndexChanged 
        ///</summary>
        protected virtual void OnSelectedFileIndexChanged(EventArgs e)
        {
            if (SelectedFileIndexChanged != null)
                SelectedFileIndexChanged(this, e);
        }

        public void Render() {
            this.c_scene.Render();
        }

        public void ReloadFileList() {
            this.OnSelectedFolderChanged(EventArgs.Empty);
        }

        private string m_SelectedFolder;

        public string SelectedFolder
        {
            get { return m_SelectedFolder; }
            set
            {
                if (m_SelectedFolder != value)
                {
                    m_SelectedFolder = value;
                    OnSelectedFolderChanged(EventArgs.Empty);
                }
            }
        }
        
        public event EventHandler SelectedFolderChanged;        
        private FBToolBar c_toolBar;
        //private IFileMultifileView m_multiViewProperty;
        ///<summary>
        ///raise the SelectedFolderChanged 
        ///</summary>
        protected virtual void OnSelectedFolderChanged(EventArgs e)
        {
            this.m_SelectedFileIndex = -1;
            if (SelectedFolderChanged != null)
                SelectedFolderChanged(this, e);
        }


        class FBFileListCollections : IEnumerable, IListOfFilesCollections 
        {
            private FBControlSurface m_surface;
            private  List<string> m_list;

            public string this[int index] {
                get
                {
                    if ((index >= 0) && (index < this.Count ))
                        return this.m_list[index];
                    return null;
                }
            }
            public override string ToString()
            {
                return this.GetType().Name + "[Count:" + this.Count + "]";
            }
            public FBFileListCollections(FBControlSurface surface)
            {
                this.m_surface = surface;
                this.m_list = new List<string>();
                this.m_surface.SelectedFolderChanged += _selectedFolderChanged;
            }

            private void _selectedFolderChanged(object sender, EventArgs e)
            {
                this.ReleadList();
            }

            private void ReleadList()
            {
                this.m_list.Clear();
                try
                {
                    string[] f = Directory.GetFiles(this.m_surface.SelectedFolder);
                    ICoreCodec[] t = null;
                    for (int i = 0; i < f.Length; i++)
                    {
                        t = CoreSystem.GetDecodersByExtension(Path.GetExtension(f[i]));
                        if ((t != null) && (t.Length > 0) && (t[0] is ICoreBitmapDecoder))
                        {
                            this.m_list.Add(f[i]);
                        }
                    }
                    if (this.m_list.Count > 0)
                    {
                        this.m_surface.SelectedFileIndex = 0;
                    }
                    else
                        this.m_surface.SelectedFileIndex = -1;
                }
                catch
                {
                    this.m_surface.SelectedFileIndex = -1;
                }
            }
            public int Count {
                get {
                    return this.m_list.Count;
                }
            }
            public void Add(string file)
            {
                if (!(string.IsNullOrEmpty(file) && File.Exists(file)))
                {
                    this.m_list.Add(file);
                }
            }
            public void Remove(string file) {
                this.m_list.Remove(file);
            }
            public void Clear() {
                this.m_list.Clear();
            }
            public IEnumerator GetEnumerator()
            {
                return this.m_list.GetEnumerator ();
            }
        }

        internal void ToogleFullScreen()
        {
            
        }



        public bool EnabledRendering { get {
            return this.c_scene.EnabledRendering;
        }
            set {
                this.c_scene.EnabledRendering = value;
            }
        }
    }
}
