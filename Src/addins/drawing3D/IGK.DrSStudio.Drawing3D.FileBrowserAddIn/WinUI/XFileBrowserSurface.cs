

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XFileBrowserSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:XFileBrowserSurface.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
namespace IGK.DrSStudio.FBAddIn.WinUI
{
    
using IGK.DrSStudio.WinUI;
    using IGK.DrSStudio.FBAddIn.AnimationModel;
    using IGK.ICore.WinUI;
    using IGK.ICore;
    using IGK.DrSStudio.Drawing3D.FileBrowserAddIn;
    /// <summary>
    /// represent the control that will host a file browser surface
    /// </summary>
    public class XFileBrowserSurface : IGKXUserControl, 
        ICoreWorkingSurface,
        IFBSurface 
    {
        XFileSurfaceMainView c_mainView;
        FBToolBar  c_fileBarAddress;
        XFileMultiview c_fileMultiview;
        XFileDirectoryBrowser c_fileDirectoryBrowser;
        FileSystemWatcher c_fileWatcher;
        private bool m_ShowText;
        private Colorf m_BackgroundColor;
        ListOfFilesCollections m_Files;
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
        public Colorf BackgroundColor
        {
            get { return CoreRenderer.GetColor("FileBrowserBgColor", Colorf.Black); }
            set
            {
                CoreRenderer.SetColor("FileBrowserBgColor", value);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.c_mainView.Dispose();
            }
            base.Dispose(disposing);
        }
        #region ICoreWorkingObjectPropertyEvent Members
        public event CoreWorkingObjectPropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(CoreWorkingObjectPropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, e);
        }
        #endregion
        FBControlSurfaceMecanism m_mecanism;
        public FBControlSurfaceMecanism Mecanism {
            get {
                return this.m_mecanism;
            }
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
        public ListOfFilesCollections  Files
        {
            get {
                if (this.m_Files == null)
                    this.m_Files = CreateFileCollections();
                return this.m_Files ; 
            }
        }
        private ListOfFilesCollections CreateFileCollections()
        {
            return new ListOfFilesCollections(this);
        }
        protected virtual void OnSelectedFolderChanged(System.EventArgs e)
        {         
            if (SelectedFolderChanged != null)
                SelectedFolderChanged(this, e);
        }
        protected override System.Drawing.Size DefaultMinimumSize
        {
            get
            {
                return new System.Drawing.Size(40, 0);
            }
        }
        public XFileBrowserSurface()
        {
            this.m_Files = CreateFileCollections();
            this.m_mecanism = new FBControlSurfaceMecanism (this);
            this.m_BackgroundColor = Colorf.CornflowerBlue ;
            c_mainView = new XFileSurfaceMainView(this);
            c_fileBarAddress = new FBToolBar(this);
            c_fileMultiview = new XFileMultiview(this);
            c_fileDirectoryBrowser = new XFileDirectoryBrowser(this);
            c_mainView.CreateControl();
            c_fileMultiview.CreateControl();
            //share list demo 
            //IGK.GLLib.WGL.wglShareLists(this.c_mainView.Device.HGLDC, this.c_fileMultiview.Device.HGLDC);
            c_fileWatcher = new FileSystemWatcher();
            //init file watch
            this.c_fileWatcher.IncludeSubdirectories = false;           
            this.c_fileWatcher.Deleted += new FileSystemEventHandler(c_fileWatcher_Deleted);
            this.c_fileWatcher.Created += new FileSystemEventHandler(c_fileWatcher_Created);
            this.c_fileWatcher.Renamed += new RenamedEventHandler(c_fileWatcher_Renamed);
            this.c_fileWatcher.Changed += new FileSystemEventHandler(c_fileWatcher_Changed);
            this.SelectedFolderChanged += _SelectedFolderChanged;
            c_fileMultiview.MinimumSize = new System.Drawing.Size(64, 64);
            this.InistializeComponent();
            this.c_fileDirectoryBrowser.Visible = false;
            this.c_fileMultiview.Visible = false;
        }
        void _SelectedFolderChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(this.SelectedFolder))
            {
                try
                {
                    this.c_fileWatcher.IncludeSubdirectories = false;
                    this.c_fileWatcher.Path = this.SelectedFolder;
                    this.c_fileWatcher.EnableRaisingEvents = true;
                }
                catch
                {
                    this.c_fileWatcher.EnableRaisingEvents = false;
                }
            }
            else
                this.c_fileWatcher.EnableRaisingEvents = false;
        }
        void c_fileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            if(e.ChangeType == WatcherChangeTypes.Deleted)
            {
                this.m_Files.Remove(e.FullPath);
            }
        }
        void c_fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (this.SelectedFileIndex != -1)
            {
                if (this.m_Files[this.SelectedFileIndex] == e.FullPath)
                {
                    this.MainView.RefreshView();
                    this.MultiFileView.RefreshView();
                }
            }
        }
        void c_fileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Created)
            {
                this.m_Files.Add(e.FullPath);
            }
        }
        void c_fileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            this.SelectedFolder = e.FullPath;
        }
        void _Load(object sender, EventArgs e)
        {
        }
        private void InistializeComponent()
        {
            c_mainView.Dock = System.Windows.Forms.DockStyle.Fill;
            c_fileBarAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.SuspendLayout();
            this.Controls.Add(this.c_mainView);
            this.Controls.Add(this.c_fileBarAddress);
            this.ResumeLayout();
        }
        #region IFBSurface Members
        public IFBDirectoryBrowser DirectoryBrowser
        {
            get { return this.c_fileDirectoryBrowser; }
        }
        public IFBAddressBar AddressBar
        {
            get { return this.c_fileBarAddress; }
        }
        public IFileMultifileView MultiFileView
        {
            get { return c_fileMultiview; }
        }
        public IFileMainView MainView
        {
            get { return c_mainView; }
        }
        #endregion
        #region ICoreWorkingSurface Members
        public string SurfaceEnvironment
        {
            get { return FileBrowserConstant.SURFACE_ENVIRONMENT ;}
        }
        public  string DisplayName
        {
            get { 
                if (string.IsNullOrEmpty (this.SelectedFolder ) || !System.IO.Directory .Exists (SelectedFolder ))
                    return CoreSystem.GetString (FileBrowserConstant.FILEBROWSER_TITLE);
                else 
                return CoreSystem.GetString (FileBrowserConstant.FILEBROWSER_TITLE_DIR, this.SelectedFolder );
            }
        }
        #endregion
        private int m_SelectedFileIndex = -1;
        public int SelectedFileIndex
        {
            get { return m_SelectedFileIndex; }
            set {
                if (this.m_SelectedFileIndex != value)
                {
                    if ((value == -1)|| ((value >=0) && (value < this.m_Files.Count)))
                    {
                        this.m_SelectedFileIndex = value;
                        OnSelectedFileIndexChanged(EventArgs.Empty);
                    }
                }
            }
        }
        public event EventHandler SelectedFileIndexChanged;
        protected virtual void OnSelectedFileIndexChanged(System.EventArgs e)
        {
            if (SelectedFileIndexChanged != null)
                SelectedFileIndexChanged(this, e);
        }
        public class ListOfFilesCollections : IEnumerable, IListOfFilesCollections 
        {
            List<string> m_files;
            XFileBrowserSurface m_owner;
            public int Count
            {
                get
                {
                    return this.m_files.Count;
                }
            }
            /// <summary>
            /// get the file name
            /// </summary>
            /// <param name="index">index of the file</param>
            /// <returns>return the filename or null if not spécify</returns>
            public string this[int index] {
                get {
                    if ((index >= 0) && (index < this.Count))
                    {
                        return this.m_files[index];
                    }
                    return null;
                }
            }
            public ListOfFilesCollections(XFileBrowserSurface owner)
            {
                this.m_files = new List<string>();
                this.m_owner = owner;
                this.m_owner.SelectedFolderChanged += new EventHandler(m_owner_SelectedFolderChanged);
            }
            void m_owner_SelectedFolderChanged(object sender, EventArgs e)
            {                
                ReloadFileCollection();
            }
            /// <summary>
            /// reload file collection
            /// </summary>
            internal  void ReloadFileCollection()
            {
                this.m_files.Clear();
                try
                {
                    string[] f = Directory.GetFiles(m_owner.SelectedFolder);
                    IGK.DrSStudio.Codec.ICoreCodec[] t = null;
                    for (int i = 0; i < f.Length; i++)
                    {
                        t = IGK.DrSStudio.CoreSystem.GetDecoders(Path.GetExtension(f[i]));
                        if ((t != null) && (t.Length > 0) && t[0] is IGK.DrSStudio.Codec.ICoreBitmapDecoder)
                        {
                            m_files.Add(f[i]);
                        }
                    }
                    if (this.m_files.Count > 0)
                    {
                        this.m_owner.SelectedFileIndex = 0;
                    }
                    else
                        this.m_owner.SelectedFileIndex = -1;
                }
                catch {
                    this.m_owner.SelectedFileIndex = -1;
                }
            }
            public override string ToString()
            {
                return base.ToString();
            }
            #region IEnumerable Members
            public IEnumerator GetEnumerator()
            {
                return this.m_files.GetEnumerator();
            }
            #endregion
            internal void Add(string p)
            {
                if (string.IsNullOrEmpty(p))
                    return;
                if (this.m_files.Contains(p))
                    return;
                IGK.DrSStudio.Codec.ICoreCodec[] t = null;
                t = IGK.DrSStudio.CoreSystem.GetDecoders(Path.GetExtension(p));
                if ((t != null) && (t.Length > 0) && t[0] is IGK.DrSStudio.Codec.ICoreBitmapDecoder)
                {
                    m_files.Add(p);
                }
            }
            internal void Remove(string p)
            {
                if (this.m_files.Contains(p))
                {
                    int i = this.m_files.IndexOf(p);
                    this.m_files .Remove (p);
                    if (this.m_owner.SelectedFileIndex == i)
                    {
                        if (i == 0)
                        {
                            if (Count == 0)
                                this.m_owner.SelectedFileIndex = -1;
                        }
                        else
                            this.m_owner.SelectedFileIndex = i - 1;
                    }
                }
            }
        }
        #region IFBSurface Members
        IListOfFilesCollections IFBSurface.Files
        {
            get { return this.Files; }
        }
        #endregion
        internal void SetupNormalMode()
        {
            this.c_fileBarAddress.Visible = true ;
        }
        internal void SetupFullScreen()
        {
            this.c_fileBarAddress.Visible = false;
            this.c_fileDirectoryBrowser.Visible = false;
            this.c_fileMultiview.Visible = false;
        }
        #region IFBSurface Members
        public void Render()
        {
            this.c_mainView.Render();
        }
        #endregion
        public void ReloadFileList()
        {
            this.m_Files.ReloadFileCollection();
        }
        internal void toogleFullScreen()
        {
            FileBrowserForm frm = this.FindForm() as FileBrowserForm;
            if (frm !=null)
            {
                frm.FullScreen = !frm.FullScreen;
            }
        }
        internal void InitFontSetting(FBSettingObject configFontElement)
        {
            this.c_mainView.InitFontSetting(configFontElement);
        }
        public event EventHandler DisplayNameChanged;
        void OnDisplayNameChanged(EventArgs e)
        {
            if (DisplayNameChanged != null)
                DisplayNameChanged(this, e);
        }
    }
}

