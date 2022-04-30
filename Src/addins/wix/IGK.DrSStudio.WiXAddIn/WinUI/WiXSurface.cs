

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
file:WiXSurface.cs
*/
using IGK.ICore.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using IGK.ICore.Windows.Native;
namespace IGK.DrSStudio.WiXAddIn.WinUI
{
    [CoreSurface ("WixSurface", DisplayName="WixProject", EnvironmentName= "{67E3CCE4-C8A4-42D6-8DDD-798CA88870EA}")]
    /// <summary>
    /// represent a user control 
    /// </summary>
    public class WiXSurface : 
        IGKXWinCoreWorkingSurface ,
        ICoreWorkingSurface,
        ICoreWorkingFilemanagerSurface 
    {
        const string PROG_FILES = "programfiles";
        const string PROG_DESKTOP = "desktop";
        const string PROG_STARTMENU = "startmenu";
        WiXConfigControl c_config;
        WiXCategoryControl c_category;
        private System.Windows.Forms.PropertyGrid clearAll;
        private Splitter c_splitter;
        private Splitter xSplitter1;
        private ListView c_filemanagerListView;
        private TreeView c_treeView1;
        private ImageList m_imageList1;
        private System.ComponentModel.IContainer c_components;
        private IGKXPanel c_directoryView;
        private IGKXToolStrip c_xToolStrip1;
        private IGKXToolStripButton c_addFile;
        private IGKXToolStripButton c_addDir;
        private ToolStripSeparator c_toolStripSeparator1;
        private IGKXToolStripButton c_btn_delete;
        private IGKXToolStripButton c_btn_clearAll;
        private WiXProject m_Project;
        private ToolStripSeparator toolStripSeparator2;
        private IGKXToolStripButton c_generate;
        private IGKXToolStripButton c_addShortcut;
        private string m_SelectedDirectory;
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
        public override string Title
        {
            get
            {
                return Path.GetFileName (this.FileName);
            }
            protected set
            {
                base.Title = value;
            }
        }
        /// <summary>
        /// get the list view zone
        /// </summary>
        internal ListView ListViewZone{
            get {
                return this.c_filemanagerListView;
            }
        }
        /// <summary>
        /// get or set the selected directory
        /// </summary>
        public string SelectedDirectory
        {
            get { return m_SelectedDirectory; }
            set
            {
                if (m_SelectedDirectory != value)
                {
                    m_SelectedDirectory = value;
                    OnSelectedDirectoryChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedDirectoryChanged;
        ///<summary>
        ///raise the SelectedDirectoryChanged 
        ///</summary>
        protected virtual void OnSelectedDirectoryChanged(EventArgs e)
        {
            if (SelectedDirectoryChanged != null)
                SelectedDirectoryChanged(this, e);
        }
        public WiXProject Project
        {
            get { return m_Project; }
            set
            {
                if (m_Project != value)
                {
                    if (m_Project != null)
                        UnregisterProjectEvent(m_Project);
                    m_Project = value;
                    if (m_Project != null)
                        RegisterProjectEvent(m_Project);
                    OnProjectChanged(EventArgs.Empty);
                }
            }
        }
        private void RegisterProjectEvent(WiXProject project)
        {
            project.FileCollectionChanged += project_FileCollectionChanged;
        }
        void project_FileCollectionChanged(object sender, WiXProjectFileCollectionEventArgs e)
        {
            if (e.Collections == GetFileCollections())
            {
                this.PopulateListView();
            }
        }
        private void UnregisterProjectEvent(WiXProject project)
        {
            project.FileCollectionChanged -= project_FileCollectionChanged;
        }
        public event EventHandler ProjectChanged;
        ///<summary>
        ///raise the ProjectChanged 
        ///</summary>
        protected virtual void OnProjectChanged(EventArgs e)
        {
            if (ProjectChanged != null)
                ProjectChanged(this, e);
        }
        public WiXSurface()
        {
            this.InitializeComponent();
            this.Project = new WiXProject();
            this.Project.Manufacteur = "IGKDEV";
            this.Project.Name = "Application Name";
            this.m_FileName = "WixProject";
            m_selector = new Dictionary<string, WiXSurfaceSelectorDirectory>();
            m_selector.Add(PROG_FILES, new WiXSurfaceSelectorDirectory());
            m_selector.Add(PROG_DESKTOP, new WiXSurfaceSelectorDirectory());
            m_selector.Add(PROG_STARTMENU, new WiXSurfaceSelectorDirectory());
        }
        private void InitializeComponent()
        {
            this.c_components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WiXSurface));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("AppDir");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("DesktopDir");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("StartMenuDir");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("FileSystem", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Conditions");
            this.c_splitter = new System.Windows.Forms.Splitter();
            this.clearAll = new System.Windows.Forms.PropertyGrid();
            this.xSplitter1 = new System.Windows.Forms.Splitter();
            this.m_imageList1 = new System.Windows.Forms.ImageList(this.c_components);
            this.c_config = new IGK.DrSStudio.WiXAddIn.WinUI.WiXConfigControl();
            this.c_directoryView = new IGKXPanel();
            this.c_filemanagerListView = new System.Windows.Forms.ListView();
            this.c_xToolStrip1 = new IGKXToolStrip();
            this.c_addFile = new IGKXToolStripButton();
            this.c_addDir = new IGKXToolStripButton();
            this.c_addShortcut = new IGKXToolStripButton();
            this.c_toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.c_btn_delete = new IGKXToolStripButton();
            this.c_btn_clearAll = new IGKXToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.c_generate = new IGKXToolStripButton();
            this.c_category = new IGK.DrSStudio.WiXAddIn.WinUI.WiXCategoryControl();
            this.c_treeView1 = new System.Windows.Forms.TreeView();
            this.c_config.SuspendLayout();
            this.c_directoryView.SuspendLayout();
            this.c_xToolStrip1.SuspendLayout();
            this.c_category.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_splitter
            // 
            this.c_splitter.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.c_splitter.Location = new System.Drawing.Point(200, 133);
            this.c_splitter.Name = "c_splitter";
            this.c_splitter.Size = new System.Drawing.Size(5, 215);
            this.c_splitter.TabIndex = 1;
            this.c_splitter.TabStop = false;
            // 
            // clearAll
            // 
            this.clearAll.Dock = System.Windows.Forms.DockStyle.Top;
            this.clearAll.Location = new System.Drawing.Point(0, 0);
            this.clearAll.Name = "clearAll";
            this.clearAll.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.clearAll.Size = new System.Drawing.Size(661, 130);
            this.clearAll.TabIndex = 0;
            this.clearAll.ToolbarVisible = false;
            // 
            // xSplitter1
            // 
            this.xSplitter1.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.xSplitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.xSplitter1.Location = new System.Drawing.Point(0, 130);
            this.xSplitter1.Name = "xSplitter1";
            this.xSplitter1.Size = new System.Drawing.Size(661, 3);
            this.xSplitter1.TabIndex = 3;
            this.xSplitter1.TabStop = false;
            // 
            // imageList1
            // 
            this.m_imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.m_imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.m_imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // c_config
            // 
            this.c_config.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_config.Controls.Add(this.c_directoryView);
            this.c_config.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_config.Location = new System.Drawing.Point(205, 133);
            this.c_config.Margin = new System.Windows.Forms.Padding(0);
            this.c_config.Name = "c_config";
            this.c_config.Size = new System.Drawing.Size(456, 215);
            this.c_config.TabIndex = 0;
            // 
            // c_directoryView
            // 
            this.c_directoryView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_directoryView.Controls.Add(this.c_filemanagerListView);
            this.c_directoryView.Controls.Add(this.c_xToolStrip1);
            this.c_directoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_directoryView.Location = new System.Drawing.Point(2, 2);
            this.c_directoryView.Margin = new System.Windows.Forms.Padding(0);
            this.c_directoryView.Name = "c_directoryView";
            this.c_directoryView.Size = new System.Drawing.Size(452, 211);
            this.c_directoryView.TabIndex = 1;
            // 
            // c_filemanagerListView
            // 
            this.c_filemanagerListView.AllowDrop = true;
            this.c_filemanagerListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_filemanagerListView.LargeImageList = this.m_imageList1;
            this.c_filemanagerListView.Location = new System.Drawing.Point(2, 27);
            this.c_filemanagerListView.Name = "c_filemanagerListView";
            this.c_filemanagerListView.Size = new System.Drawing.Size(448, 182);
            this.c_filemanagerListView.SmallImageList = this.m_imageList1;
            this.c_filemanagerListView.TabIndex = 0;
            this.c_filemanagerListView.UseCompatibleStateImageBehavior = false;
            this.c_filemanagerListView.View = System.Windows.Forms.View.SmallIcon;
            this.c_filemanagerListView.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.c_filemanagerListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // xToolStrip1
            // 
            this.c_xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_addFile,
            this.c_addDir,
            this.c_addShortcut,
            this.c_toolStripSeparator1,
            this.c_btn_delete,
            this.c_btn_clearAll,
            this.toolStripSeparator2,
            this.c_generate});
            this.c_xToolStrip1.Location = new System.Drawing.Point(2, 2);
            this.c_xToolStrip1.Name = "xToolStrip1";
            this.c_xToolStrip1.ShowItemToolTips = false;
            this.c_xToolStrip1.Size = new System.Drawing.Size(448, 25);
            this.c_xToolStrip1.TabIndex = 1;
            this.c_xToolStrip1.Text = "xToolStrip1";
            // 
            // c_addFile
            // 
            this.c_addFile.Action = null;
            this.c_addFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_addFile.Image = ((System.Drawing.Image)(resources.GetObject("c_addFile.Image")));
            this.c_addFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_addFile.Name = "c_addFile";
            this.c_addFile.Size = new System.Drawing.Size(23, 22);
            this.c_addFile.Text = "addFile";
            this.c_addFile.Click += new System.EventHandler(this.addFile_Click);
            // 
            // c_addDir
            // 
            this.c_addDir.Action = null;
            this.c_addDir.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_addDir.Image = ((System.Drawing.Image)(resources.GetObject("c_addDir.Image")));
            this.c_addDir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_addDir.Name = "c_addDir";
            this.c_addDir.Size = new System.Drawing.Size(23, 22);
            this.c_addDir.Text = "addDir";
            this.c_addDir.Click += new System.EventHandler(this.addDir_Click);
            // 
            // c_addShortcut
            // 
            this.c_addShortcut.Action = null;
            this.c_addShortcut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_addShortcut.Image = ((System.Drawing.Image)(resources.GetObject("c_addShortcut.Image")));
            this.c_addShortcut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_addShortcut.Name = "c_addShortcut";
            this.c_addShortcut.Size = new System.Drawing.Size(23, 22);
            this.c_addShortcut.Text = "addshortcut";
            this.c_addShortcut.Click += new System.EventHandler(this.c_addShortcut_Click);
            // 
            // toolStripSeparator1
            // 
            this.c_toolStripSeparator1.Name = "toolStripSeparator1";
            this.c_toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // delete
            //             
            this.c_btn_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_btn_delete.Image = ((System.Drawing.Image)(resources.GetObject("delete.Image")));
            this.c_btn_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_delete.Name = "delete";
            this.c_btn_delete.Size = new System.Drawing.Size(23, 22);
            this.c_btn_delete.Text = "delete";
            this.c_btn_delete.Click += new System.EventHandler(this.delete_Click);
            // 
            // btn_clearAll
            //             
            this.c_btn_clearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_btn_clearAll.Image = ((System.Drawing.Image)(resources.GetObject("btn_clearAll.Image")));
            this.c_btn_clearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_btn_clearAll.Name = "btn_clearAll";
            this.c_btn_clearAll.Size = new System.Drawing.Size(23, 22);
            this.c_btn_clearAll.Text = "clearAll";
            this.c_btn_clearAll.ToolTipText = "wix.tip.clearAll";
            this.c_btn_clearAll.Click += new System.EventHandler(this.btn_clearAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // c_generate
            // 
            this.c_generate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_generate.Image = ((System.Drawing.Image)(resources.GetObject("c_generate.Image")));
            this.c_generate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_generate.Name = "c_generate";
            this.c_generate.Size = new System.Drawing.Size(23, 22);
            this.c_generate.Text = "generate";
            this.c_generate.Click += new System.EventHandler(this.c_generate_Click);
            // 
            // c_category
            // 
            this.c_category.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.c_category.Controls.Add(this.c_treeView1);
            this.c_category.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_category.Location = new System.Drawing.Point(0, 133);
            this.c_category.Margin = new System.Windows.Forms.Padding(0);
            this.c_category.Name = "c_category";
            this.c_category.Size = new System.Drawing.Size(200, 215);
            this.c_category.TabIndex = 2;
            // 
            // treeView1
            // 
            this.c_treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_treeView1.Location = new System.Drawing.Point(2, 2);
            this.c_treeView1.Name = "treeView1";
            treeNode1.Name = "AppDir";
            treeNode1.Tag = "programfiles";
            treeNode1.Text = "AppDir";
            treeNode2.Name = "DesktopDir";
            treeNode2.Tag = "desktop";
            treeNode2.Text = "DesktopDir";
            treeNode3.Name = "StartMenuDir";
            treeNode3.Tag = "startmenu";
            treeNode3.Text = "StartMenuDir";
            treeNode4.Name = "FileSystem";
            treeNode4.Tag = "FILESYSTEM";
            treeNode4.Text = "FileSystem";
            treeNode5.Name = "Condition";
            treeNode5.Tag = "CONDITIONS";
            treeNode5.Text = "Conditions";
            this.c_treeView1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode5});
            this.c_treeView1.Size = new System.Drawing.Size(196, 211);
            this.c_treeView1.TabIndex = 0;
            this.c_treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._AfterSelect);
            // 
            // WiXSurface
            // 
            this.Controls.Add(this.c_config);
            this.Controls.Add(this.c_splitter);
            this.Controls.Add(this.c_category);
            this.Controls.Add(this.xSplitter1);
            this.Controls.Add(this.clearAll);
            this.Name = "WiXSurface";
            this.Size = new System.Drawing.Size(661, 348);
            this.Load += new System.EventHandler(this._Load);
            this.c_config.ResumeLayout(false);
            this.c_directoryView.ResumeLayout(false);
            this.c_directoryView.PerformLayout();
            this.c_xToolStrip1.ResumeLayout(false);
            this.c_xToolStrip1.PerformLayout();
            this.c_category.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                return base.DisplayRectangle;
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.m_imageList1.Images.Clear();
                this.m_imageList1.Dispose();
            }
            base.Dispose(disposing);
        }
        private void _Load(object sender, EventArgs e)
        {
            this.clearAll.SelectedObject = this.Project;
            this.SelectedDirectoryChanged += _SelectedDirectoryChanged;
            this.c_addFile.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_FILE_GKDS);
            this.c_btn_clearAll.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_CLEAR_GKDS);
            this.c_btn_delete.ImageDocument = CoreResources.GetDocument(CoreImageKeys.MENU_CLOSE_GKDS);
            this.c_addDir.ImageDocument = CoreResources.GetDocument(CoreImageKeys.BTN_FOLDER_GKDS);
            this.c_generate.ImageDocument = CoreResources.GetDocument(CoreImageKeys.GENERATE_GKDS);
            this.c_addShortcut.ImageDocument = CoreResources.GetDocument(CoreImageKeys.FILE_SHORTCUT_GKDS);
            this.GenerateListViewAction();
        }
        private void GenerateListViewAction()
        {
            this.c_filemanagerListView.KeyPress += c_filemanagerListView_KeyPress;
            this.c_filemanagerListView.KeyUp += c_filemanagerListView_KeyUp;
        }
        void c_filemanagerListView_KeyUp(object sender, KeyEventArgs e)
        {
            switch  (e.KeyCode)
            { 
                case Keys.Delete :
                    __deleteSelectedItem();
                    break;
                case Keys.Enter :
                    __EnterSelectedElement();
                    break;
                case Keys.Control | Keys.V :
                    __PasteCopiedElement();
                    break;
            }
        }
        private void __PasteCopiedElement()
        {
            var F = this.GetFileCollections();
            if (F == null) return;
        }
        void c_filemanagerListView_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        void _SelectedDirectoryChanged(object sender, EventArgs e)
        {
            this.PopulateListView();
        }
        /// <summary>
        /// of after select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _AfterSelect(object sender, TreeViewEventArgs e)
        {
            string str = e.Node.Tag as string;
            if (!string.IsNullOrEmpty(str))
            {
                switch (str.ToLower())
                { 
                    case "filesystem":  
                        //do nothing  
                        break;
                    case "conditions":
                        EditCondition();
                        break;
                    default:
                        this.SelectedDirectory = str;
                        break;
                }
            }
        }
        private void EditCondition()
        {
            using (WiXNewConditionControl ctr = new WiXNewConditionControl())
            {

                ctr.Size = new Size(400, 300);
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(ctr))
                {
                    if (frm.ShowDialog() == enuDialogResult.OK)
                    {
                        this.m_Project.Conditions.Add(ctr.Condition);
                    }
                }
            }
        }
        private void addFile_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedDirectory))
                return;
            Workbench.CallAction("Wix.AddFiles");
        }
        private void addDir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedDirectory))
                return;
            Workbench.CallAction("Wix.AddDir");
        }
        /// <summary>
        /// add files
        /// </summary>
        /// <param name="wiXProjectFile"></param>
        internal void AddFiles(params WiXProjectFile[] wiXProjectFile)
        {
            if (string.IsNullOrEmpty(SelectedDirectory))
                return;
               WiXProjectFile v = this.m_selector[this.SelectedDirectory].GetFileCollections() as WiXProjectFile;
               if (v == null)
               {
                   WiXProject.FileCollections f = GetFileCollections();
                   f.AddRange(wiXProjectFile);
               }
               else {
                   v.AddChild(wiXProjectFile);
                   this.PopulateListView();
               }
        }
        /// <summary>
        /// add directory
        /// </summary>
        /// <param name="p">directory</param>
        internal void AddDir(string p)
        {
            if (string.IsNullOrEmpty(SelectedDirectory))
                return;
            WiXProjectFile v = this.m_selector[this.SelectedDirectory].GetFileCollections() as WiXProjectFile;
            if (v == null)
            {
                WiXProject.FileCollections f = GetFileCollections();
                if (f != null)
                {
                    f.AddRange(new WiXProjectFile[] { 
                    new WiXProjectFile (p)
                });
                }
            }
            else {
                v.AddChild(new WiXProjectFile(p));
                this.PopulateListView();
            }
        }
        void PopulateListView()
        {
            this.c_filemanagerListView.SuspendLayout();
            this.c_filemanagerListView.Items.Clear();
            this.c_filemanagerListView.View = View.LargeIcon;
            IEnumerable  f = GetFileCollections();
            string v_imgkeyId = string.Empty;
            if (f != null)
            {
                //if subfolder
                if (this.m_selector[this.SelectedDirectory].CurrentDirectory != ".")
                {
                    RegisterImageKey("folder");
                    this.c_filemanagerListView.Items.Add(
                        new ListViewItem()
                        {
                            Text = "..",
                            ImageKey = "folder"
                        });
                    f = this.m_selector[this.SelectedDirectory].GetFileCollections();
                }
                foreach (WiXProjectFile s in f)
                {
                    if (s.IsFile)
                    {
                        switch (Path.GetExtension(s.FileName).ToLower())
                        {
                            case ".exe":
                                Icon ico = WinCoreWinUIUtils.ExtractIcon(s.FileName, 0, 32, 32);
                                //IntPtr hicon = IntPtr.Zero;
                                //int picoid = 0;
                                //var r = User32.PrivateExtractIcons(s.FileName, 0, 32, 32, ref hicon, ref picoid, 0, 0);
                                if (ico != null)
                                {
                                    v_imgkeyId = s.FileName;
                                    this.m_imageList1.Images.Add(s.FileName, ico);
                                }
                                else {
                                    v_imgkeyId = "defaultexe";
                                    RegisterImageKey("defaultexe");
                                }
                                break;
                            case ".ico":
                                Icon c = new Icon(s.FileName);
                                if (c != null)
                                {
                                    v_imgkeyId = s.FileName;
                                    this.m_imageList1.Images.Add(s.FileName, c);
                                }
                                break;
                            case ".jpeg":
                            case ".png":
                            case ".bmp":
                            default:
                                v_imgkeyId = Path.GetExtension(s.FileName);
                                RegisterImageKey(Path.GetExtension(s.FileName));
                                break;
                        }
                        this.c_filemanagerListView.Items.Add(
                            new ListViewItem()
                            {
                                Text = Path.GetFileName(s.FileName),
                                ImageKey = v_imgkeyId,
                                Tag = s
                            });
                    }
                    else if (s.IsDirectory)
                    {
                        if (s.FileType == enuWiXFileType.Directory)
                        {
                            RegisterImageKey("folder");
                            this.c_filemanagerListView.Items.Add(
                                new ListViewItem()
                                {
                                    Text = Path.GetFileName(s.FileName),
                                    ImageKey = "folder",
                                    Tag = s
                                });
                        }
                        else {
                            RegisterImageKey("shortcut");
                            this.c_filemanagerListView.Items.Add(
                                new ListViewItem()
                                {
                                    Text = s.FileName  ,
                                    ImageKey = "shortcut",
                                    Tag = s
                                });
                        }
                    }
                }
            }
            this.c_filemanagerListView.ResumeLayout();
        }
        private WiXProject.FileCollections GetFileCollections()
        {
            WiXProject.FileCollections f = null;
            if (!string.IsNullOrEmpty(this.SelectedDirectory))
            {
                switch (this.SelectedDirectory.ToLower())
                {
                    case PROG_FILES:
                        f = this.Project.ProgramFiles;
                        break;
                    case PROG_DESKTOP:
                        f = this.Project.DesktopFiles;
                        break;
                    case PROG_STARTMENU:
                        f = this.Project.StartMenuFiles;
                        break;
                    default:
                        break;
                }
            }
            return f;
        }
        private void RegisterImageKey(string p)
        {
            if (string.IsNullOrEmpty(p) || this.m_imageList1.Images.ContainsKey(p))
                return;
            Icon ico = null;
            switch (p)
            {
                case "defaultexe":
                    ico = WinCoreWinUIUtils.GetRegSystemIcon("exefile\\DefaultIcon", 32, 32);
                    break;
                case "folder":
                    {
                        ico = WinCoreWinUIUtils.GetRegSystemIcon("Directory\\DefaultIcon", 32, 32);
                    }
                    break;
                case "shortcut":
                    Image g = CoreResources.GetImage("file_shortcut") as Image;
                    if (g != null)
                    {
                        this.m_imageList1.Images.Add(p, g);
                    }
                    return;
                default:
                    ico = WinCoreWinUIUtils.GetRegSystemIcon(p, 32, 32);
                    break ;
            }
            if (ico != null)
            {
                this.m_imageList1.Images.Add(p, ico);
            }
        }
        private void btn_clearAll_Click(object sender, EventArgs e)
        {
            this.Project.ProgramFiles.Clear();
            this.Project.DesktopFiles.Clear();
            this.Project.StartMenuFiles.Clear();
            this.m_selector[PROG_FILES].Clear();
            this.m_selector[PROG_DESKTOP ].Clear();
            this.m_selector[PROG_STARTMENU ].Clear();
            this.PopulateListView();
        }
        Dictionary<string, WiXSurfaceSelectorDirectory> m_selector;
        class WiXSurfaceSelectorDirectory
        {
            private string m_CurrentDirectory;
            private WiXProjectFile  m_selectedDirectory;
            public string CurrentDirectory
            {
                get { return m_CurrentDirectory; }
                set
                {
                    if (m_CurrentDirectory != value)
                    {
                        m_CurrentDirectory = value;
                    }
                }
            }
            public WiXSurfaceSelectorDirectory()
            {
                this.m_CurrentDirectory = ".";//root directory
                this.m_selectedDirectory = null;
            }
            public void GoToParent()
            {
                string[] t = this.m_CurrentDirectory.Split(Path.DirectorySeparatorChar);
                if (t.Length == 1)
                {
                    this.m_CurrentDirectory = ".";
                    this.m_selectedDirectory = null;
                }
                else
                {
                    string[] s = new String[t.Length - 1];
                    Array.Copy(t, s, t.Length - 1);
                    this.m_CurrentDirectory = Path.Combine(s);
                    this.m_selectedDirectory = this.m_selectedDirectory.Parent;
                }
            }
            public void NavigateTo(string subfolder, WiXProjectFile file)
            {
                if (string.IsNullOrEmpty(subfolder))
                    return;
                if (this.m_CurrentDirectory == ".")
                    this.m_CurrentDirectory = subfolder;
                else
                    this.m_CurrentDirectory += Path.DirectorySeparatorChar + subfolder;
                this.m_selectedDirectory = file;
            }
            internal IEnumerable GetFileCollections()
            {
                return m_selectedDirectory;
            }
            internal void Clear()
            {
                this.m_selectedDirectory = null;
                this.m_CurrentDirectory = ".";
            }
        }
        private void c_generate_Click(object sender, EventArgs e)
        {
            Workbench.CallAction("Wix.Generate");
        }
        private void c_addShortcut_Click(object sender, EventArgs e)
        {
            Workbench.CallAction("Wix.AddShortcut");
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void delete_Click(object sender, EventArgs e)
        {
            __deleteSelectedItem();
        }
        private void __deleteSelectedItem()
        {
            if (this.c_filemanagerListView.SelectedItems.Count >= 1)
            {
                bool v_changed = false;
                for (int i = 0; i < this.c_filemanagerListView.SelectedItems.Count; i++)
                {
                    WiXProjectFile c = this.c_filemanagerListView.SelectedItems[i].Tag as WiXProjectFile;
                    if (c != null)
                    {
                        if (c.Parent != null)
                        {
                            c.Parent.Remove(c);
                        }
                        else
                        {
                            WiXProject.FileCollections f = GetFileCollections();
                            if (f != null)
                            {
                                f.Remove(c);
                            }
                            //this.listView1.Remothis.listView1.SelectedItems[0].Index 
                        }
                        v_changed = true;
                    }
                }
                if (v_changed) PopulateListView();
            }
        }
        private void __EnterSelectedElement()
        {
            if (this.c_filemanagerListView.SelectedItems.Count == 1)
            {
                WiXProjectFile c = this.c_filemanagerListView.SelectedItems[0].Tag as WiXProjectFile;
                if (c != null)
                {
                    if (c.FileType == enuWiXFileType.Directory)
                    {
                        this.m_selector[this.SelectedDirectory].NavigateTo(c.Id, c);
                    }
                    else if (c.FileType == enuWiXFileType.Shortcut)
                    {
                        //show shortcut info
                        this.ShowShortcutInfo(c);                        
                        PopulateListView();
                        return;
                    }
                }
                else if (this.c_filemanagerListView.SelectedItems[0].Text == "..")
                {
                    this.m_selector[this.SelectedDirectory].GoToParent();
                }
                PopulateListView();
            }
        }
        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                __EnterSelectedElement();
            }
        }
        private void ShowShortcutInfo(WiXProjectFile c)
        {
            if ((c == null) || (c.FileType != enuWiXFileType.Shortcut))
                return;
            using (ICoreDialogForm d = Workbench.CreateNewDialog())
            {
                WiXNewShortcutControl r = new WiXNewShortcutControl();
                r.Shortcut  = new WiXNewShortcutControl.WixShortcutInfo() {
                     Name = c.FileName  ,
                     WorkingDir = c.WorkingDir ,
                     Description = c.Description ,
                     Target = c.Target 
                };
                d.Title  = CoreResources.GetString("wix.title.shortcutinfo");
                r.Dock = DockStyle.Fill;
                d.Controls.Add(r);
                //d.Owner = this.MainForm as IXForm ;
                if (d.ShowDialog() == enuDialogResult.OK)
                { 
                    //update shortcut info
                    //c.FileName = r.Shortcut.Name;
                    c.FileName = r.Shortcut.Name;
                    c.Description = r.Shortcut.Description;
                    c.Target = r.Shortcut.Target;
                    c.WorkingDir  = r.Shortcut.WorkingDir;
                }
            }
        }
        internal void AddShortCut(IWixShortcutInfo p)
        {
            WiXProjectFile c = WiXProjectFile.CreateShortcut(p.Name,p.Target,p.Description,p.WorkingDir );
            this.AddFiles(new WiXProjectFile[] { c });
        }
        public bool CanDoDragDrop { get {
            return (this.GetFileCollections() != null);
        } }
        private string  m_FileName;
        public string  FileName
        {
            get { 
                return m_FileName; 
            }
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
        public void RenameTo(string p)
        {
            this.FileName = p;
        }
        public ICoreSaveAsInfo GetSaveAsInfo()
        {
            return new CoreSaveAsInfo("Save Wix Project",
                "IGKDEV Wix File | *.igkwix",
                this.FileName);
        }
        public bool NeedToSave
        {
            get { return m_needToSave; 
            }
            set {
                if (this.m_needToSave != value)
                {
                    this.m_needToSave = value;
                    OnNeedToSaveChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NeedToSaveChanged;
        private bool m_needToSave;
        private void OnNeedToSaveChanged(EventArgs e) {
            if (NeedToSaveChanged != null)
            {
                this.NeedToSaveChanged(this, e);
            }
        }
        public void Save()
        {
            if (File.Exists(this.FileName))
            {
                this.Saving = true;
                this.m_Project.Save(this.FileName);
                this.Saving = false;
                OnSaved(EventArgs.Empty);
            }
            else
            {
                this.SaveAs(this.FileName);
            }

        }
        public void SaveAs(string filename)
        {
            this.Saving = true; 
            this.Project.Save(filename);
            this.FileName = filename;
            this.Saving = false;
            OnSaved(EventArgs.Empty);
        }

        internal void LoadDir(string directory)
        {
            if (!Directory.Exists(directory))
                return;
            foreach (string dir in Directory.GetDirectories(directory))
            {
                this.LoadDir(dir, this.SelectedDirectory);    
            }
            foreach (string f in Directory.GetFiles (directory))
            {
                this.LoadDir (f, this.SelectedDirectory );
                
            }  
            this.PopulateListView();
        }
        public string GetRegexPattern()
        {
            return  "(" + this.Project.WixFileFilter.Replace(".", "\\.").Replace("*", "(.)+") + ")$";
        }
         private void LoadDir(string directory, string selectedDir)
        {
            if (string.IsNullOrEmpty(selectedDir ))
                return ;
            string pattern = GetRegexPattern();
            WiXProjectFile v = this.m_selector[selectedDir].GetFileCollections() as WiXProjectFile;
            if (v == null)
            {
                WiXProject.FileCollections f = GetFileCollections();
                WiXUtility.LoadDir(f, directory, pattern);
            }
            else
            {
                WiXUtility.LoadDir(v, directory, pattern);
            }
           
        }


        public void ReloadFileFromDisk()
        {
            if (File.Exists(this.FileName))
            {
                WiXProject prf = WiXProject.LoadFromFile(this.FileName);
                if (prf != null)
                {
                    this.m_Project = prf;
                    this._Load(this, EventArgs.Empty);
                }
            }

        }
    }
}

