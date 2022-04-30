

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: FtpManagerSurface.cs
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
file:FtpManagerSurface.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace IGK.DrSStudio.FtpManagerAddIn.WinUI
{
    
using IGK.ICore.WinUI;
    using IGK.ICore.Resources;
    using IGK.DrSStudio.WinUI;
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    [CoreSurface ("FtpManagerSurface",
        EnvironmentName = FtpConstant.SURFACE_ENVIRONMENT)]
    /// <summary>
    /// represent a manager surface
    /// </summary>
    public class FtpManagerSurface : IGKXWinCoreWorkingSurface, ICoreWorkingSurface 
    {
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private IGKXPanel xPanel3;
        private IGKXPanel xPanel2;
        private IGKXPanel xPanel1;
        private IGKXToolStripButton toolStripButton1;
        private IGKXToolStripButton toolStripButton2;
        private System.Windows.Forms.ListView c_serverListFile;
        private System.Windows.Forms.ListView c_clientListFile;
        private IGKXButton btn_upload;
        private IGKXButton btn_download;
        private IGKXToolStrip xToolStrip1;
        private string m_Login;
        private string m_Server;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private string m_PassWord;
        private int m_Port;
        private IGKXToolStrip xToolStrip3;
        private IGKXToolStrip xToolStrip2;
        private IGKXToolStripButton c_tls_newFolder;
        private IGKXToolStripButton c_tls_delete;
        private IGKXToolStripButton toolStripButton3;
        private ImageList c_img_List;
        private System.ComponentModel.IContainer components;
        private RichTextBox richTextBox1;
        private ColumnHeader c_colAuthorization;
        private string m_SelectedPath;
        private ToolStripButton c_tls_chmod;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripTextBox toolStripTextBox1;
        private string m_SelectedServerPath;
        /// <summary>
        /// get or set the selected server path
        /// </summary>
        public string SelectedServerPath
        {
            get { return m_SelectedServerPath; }
            set
            {
                if (m_SelectedServerPath != value)
                {
                    m_SelectedServerPath = value;
                    OnSelectedServerPathChanged(EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// get or set the selected path
        /// </summary>
        public string SelectedPath
        {
            get { return m_SelectedPath; }
            set
            {
                if (m_SelectedPath != value)
                {
                    m_SelectedPath = value;
                    OnSelectedPathChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SelectedServerPathChanged;
        ///<summary>
        ///raise the SelectedServerPathChanged 
        ///</summary>
        protected virtual void OnSelectedServerPathChanged(EventArgs e)
        {
            if (SelectedServerPathChanged != null)
                SelectedServerPathChanged(this, e);
        }
        public event EventHandler SelectedPathChanged;
        ///<summary>
        ///raise the SelectedPathChanged 
        ///</summary>
        protected virtual void OnSelectedPathChanged(EventArgs e)
        {
            if (SelectedPathChanged != null)
                SelectedPathChanged(this, e);
        }
        /// <summary>
        /// get or set the port
        /// </summary>
        public int Port
        {
            get { return m_Port; }
            set
            {
                if (m_Port != value)
                {
                    m_Port = value;
                }
            }
        }
        private bool m_SaveConnectionEntry;
        /// <summary>
        /// save connection info
        /// </summary>
        public bool SaveConnectionEntry
        {
            get { return m_SaveConnectionEntry; }
            set
            {
                if (m_SaveConnectionEntry != value)
                {
                    m_SaveConnectionEntry = value;
                }
            }
        }
        public string PassWord
        {
            get { return m_PassWord; }
            set
            {
                if (m_PassWord != value)
                {
                    m_PassWord = value;
                }
            }
        }
        public string Server
        {
            get { return m_Server; }
            set
            {
                if (m_Server != value)
                {
                    m_Server = value;
                }
            }
        }
        public string Login
        {
            get { return m_Login; }
            set
            {
                if (m_Login != value)
                {
                    m_Login = value;
                }
            }
        }
        public FtpManagerSurface()
        {
            this.InitializeComponent();
            this.SelectedPathChanged += FtpManagerSurface_SelectedPathChanged;
            this.InitView();
            
            this.btn_upload.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FtpConstant .UPLOAD));
            this.btn_download.ButtonDocument = CoreButtonDocument.Create(CoreResources.GetAllDocuments(FtpConstant.DOWNLOAD));
        }
        void FtpManagerSurface_SelectedPathChanged(object sender, EventArgs e)
        {
            this.c_clientListFile.Items.Clear();
            this.PopulateSelectedFolderList();
        }
        private void PopulateSelectedFolderList()
        {
            if (!Directory.Exists(this.SelectedPath))
                return;
            ListViewItem v_item = null;
            string[] v_dirs = Directory.GetDirectories (this.SelectedPath );
            string[] v_files = Directory.GetFiles (this.SelectedPath );
            Array.Sort(v_dirs);
            Array.Sort(v_files);
            DirectoryInfo v_dirinfo =null;
            List<ListViewItem> v_items = new List<ListViewItem> ();
            for (int i = 0; i < v_dirs.Length ; i++)
            {
                v_dirinfo = new DirectoryInfo(v_dirs[i]);
                v_item = new ListViewItem();
                v_item.Name = v_dirinfo.Name;
                v_item.Text = v_dirinfo.Name;
                v_item.Tag = v_dirinfo;
                v_item.ImageKey = "Folder";
                v_items.Add(v_item);
            }
            FileInfo v_fileinfo = null;
            for (int i = 0; i < v_files.Length; i++)
            {
                v_fileinfo = new FileInfo(v_files[i]);
                v_item = new ListViewItem();                
                v_item.Tag = v_fileinfo;
                v_item.Name = v_fileinfo.Name;
                v_item.Name = v_fileinfo.Name;
                v_item.ImageKey = GetImagekey(v_item);
                v_items.Add(v_item);
            }
            this.c_clientListFile.Items.AddRange(v_items.ToArray());
        }
        private string GetImagekey(ListViewItem v_item)
        {
            return "File";
        }
        private void InitView()
        {
            this.c_tls_delete.ImageDocument = CoreResources.GetDocument("ftp_deleteF");
            this.c_tls_newFolder.ImageDocument = CoreResources.GetDocument("ftp_newFolder");
            this.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FtpManagerSurface));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.xPanel3 = new IGKXPanel();
            this.c_serverListFile = new System.Windows.Forms.ListView();
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_colAuthorization = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_img_List = new System.Windows.Forms.ImageList(this.components);
            this.xToolStrip3 = new IGKXToolStrip();
            this.c_tls_newFolder = new IGKXToolStripButton();
            this.c_tls_delete = new IGKXToolStripButton();
            this.c_tls_chmod = new System.Windows.Forms.ToolStripButton();
            this.xPanel2 = new IGKXPanel();
            this.c_clientListFile = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xToolStrip2 = new IGKXToolStrip();
            this.toolStripButton3 = new IGKXToolStripButton();
            this.xPanel1 = new IGKXPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_download = new IGKXButton();
            this.btn_upload = new IGKXButton();
            this.xToolStrip1 = new IGKXToolStrip();
            this.toolStripButton1 = new IGKXToolStripButton();
            this.toolStripButton2 = new IGKXToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.xPanel3.SuspendLayout();
            this.xToolStrip3.SuspendLayout();
            this.xPanel2.SuspendLayout();
            this.xToolStrip2.SuspendLayout();
            this.xPanel1.SuspendLayout();
            this.xToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.xPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.xPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.xPanel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 43);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(668, 417);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // xPanel3
            // 
            this.xPanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel3.Controls.Add(this.c_serverListFile);
            this.xPanel3.Controls.Add(this.xToolStrip3);
            this.xPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel3.Location = new System.Drawing.Point(334, 0);
            this.xPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel3.Name = "xPanel3";
            this.xPanel3.Padding = new System.Windows.Forms.Padding(3);
            this.xPanel3.Size = new System.Drawing.Size(334, 369);
            this.xPanel3.TabIndex = 2;
            // 
            // c_serverListFile
            // 
            this.c_serverListFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.c_colAuthorization});
            this.c_serverListFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_serverListFile.FullRowSelect = true;
            this.c_serverListFile.GridLines = true;
            this.c_serverListFile.LargeImageList = this.c_img_List;
            this.c_serverListFile.Location = new System.Drawing.Point(5, 44);
            this.c_serverListFile.Margin = new System.Windows.Forms.Padding(0);
            this.c_serverListFile.Name = "c_serverListFile";
            this.c_serverListFile.Size = new System.Drawing.Size(324, 320);
            this.c_serverListFile.TabIndex = 1;
            this.c_serverListFile.UseCompatibleStateImageBehavior = false;
            this.c_serverListFile.View = System.Windows.Forms.View.Details;
            this.c_serverListFile.SelectedIndexChanged += new System.EventHandler(this.c_serverListFile_SelectedIndexChanged);
            this.c_serverListFile.DoubleClick += new System.EventHandler(this.c_serverListFile_DoubleClick);
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Nom";
            this.columnHeader4.Width = 114;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type";
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Taille";
            // 
            // c_colAuthorization
            // 
            this.c_colAuthorization.Text = "Autorisation";
            // 
            // c_img_List
            // 
            this.c_img_List.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.c_img_List.ImageSize = new System.Drawing.Size(32, 32);
            this.c_img_List.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xToolStrip3
            // 
            this.xToolStrip3.CaptionKey = null;
            this.xToolStrip3.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xToolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_tls_newFolder,
            this.c_tls_delete,
            this.c_tls_chmod});
            this.xToolStrip3.Location = new System.Drawing.Point(5, 5);
            this.xToolStrip3.Name = "xToolStrip3";
            this.xToolStrip3.Size = new System.Drawing.Size(324, 39);
            this.xToolStrip3.TabIndex = 2;
            this.xToolStrip3.Text = "xToolStrip3";
            // 
            // c_tls_newFolder
            // 
            this.c_tls_newFolder.Action = null;
            this.c_tls_newFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_tls_newFolder.Image = ((System.Drawing.Image)(resources.GetObject("c_tls_newFolder.Image")));
            this.c_tls_newFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_tls_newFolder.Name = "c_tls_newFolder";
            this.c_tls_newFolder.Size = new System.Drawing.Size(36, 36);
            this.c_tls_newFolder.Text = "c_tls_NewFolder";
            this.c_tls_newFolder.ToolTipText = "new folder";
            this.c_tls_newFolder.Click += new System.EventHandler(this.c_tls_newFolder_Click);
            // 
            // c_tls_delete
            // 
            this.c_tls_delete.Action = null;
            this.c_tls_delete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.c_tls_delete.Image = ((System.Drawing.Image)(resources.GetObject("c_tls_delete.Image")));
            this.c_tls_delete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_tls_delete.Name = "c_tls_delete";
            this.c_tls_delete.Size = new System.Drawing.Size(36, 36);
            this.c_tls_delete.ToolTipText = "delete";
            this.c_tls_delete.Click += new System.EventHandler(this.toolStripButton4_Click);
            // 
            // c_tls_chmod
            // 
            this.c_tls_chmod.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_tls_chmod.Image = ((System.Drawing.Image)(resources.GetObject("c_tls_chmod.Image")));
            this.c_tls_chmod.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_tls_chmod.Name = "c_tls_chmod";
            this.c_tls_chmod.Size = new System.Drawing.Size(36, 36);
            this.c_tls_chmod.Text = "toolStripButton4";
            this.c_tls_chmod.ToolTipText = "chmod";
            this.c_tls_chmod.Click += new System.EventHandler(this.c_tls_chmod_Click);
            // 
            // xPanel2
            // 
            this.xPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.xPanel2.Controls.Add(this.c_clientListFile);
            this.xPanel2.Controls.Add(this.xToolStrip2);
            this.xPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel2.Location = new System.Drawing.Point(0, 0);
            this.xPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel2.Name = "xPanel2";
            this.xPanel2.Padding = new System.Windows.Forms.Padding(3);
            this.xPanel2.Size = new System.Drawing.Size(334, 369);
            this.xPanel2.TabIndex = 1;
            // 
            // c_clientListFile
            // 
            this.c_clientListFile.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.c_clientListFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_clientListFile.FullRowSelect = true;
            this.c_clientListFile.GridLines = true;
            this.c_clientListFile.LargeImageList = this.c_img_List;
            this.c_clientListFile.Location = new System.Drawing.Point(5, 44);
            this.c_clientListFile.Margin = new System.Windows.Forms.Padding(0);
            this.c_clientListFile.Name = "c_clientListFile";
            this.c_clientListFile.Size = new System.Drawing.Size(324, 320);
            this.c_clientListFile.TabIndex = 0;
            this.c_clientListFile.UseCompatibleStateImageBehavior = false;
            this.c_clientListFile.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 123;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Type";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Taille";
            this.columnHeader3.Width = 110;
            // 
            // xToolStrip2
            // 
            this.xToolStrip2.CaptionKey = null;
            this.xToolStrip2.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xToolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton3,
            this.toolStripSeparator1,
            this.toolStripTextBox1});
            this.xToolStrip2.Location = new System.Drawing.Point(5, 5);
            this.xToolStrip2.Name = "xToolStrip2";
            this.xToolStrip2.Size = new System.Drawing.Size(324, 39);
            this.xToolStrip2.TabIndex = 2;
            this.xToolStrip2.Text = "xToolStrip2";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Action = null;
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton3.Text = "c_tsl_selectFolder";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // xPanel1
            // 
            this.xPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.tableLayoutPanel1.SetColumnSpan(this.xPanel1, 2);
            this.xPanel1.Controls.Add(this.richTextBox1);
            this.xPanel1.Controls.Add(this.btn_download);
            this.xPanel1.Controls.Add(this.btn_upload);
            this.xPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xPanel1.Location = new System.Drawing.Point(0, 369);
            this.xPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.xPanel1.Name = "xPanel1";
            this.xPanel1.Padding = new System.Windows.Forms.Padding(3);
            this.xPanel1.Size = new System.Drawing.Size(668, 48);
            this.xPanel1.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(5, -2);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(343, 47);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            // 
            // btn_download
            // 
            this.btn_download.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_download.ButtonImageSize = new Size2i(32, 32);
            this.btn_download.CaptionKey = null;
            this.btn_download.Checked = false;
            this.btn_download.DialogResult = enuDialogResult.None;
            this.btn_download.Location = new System.Drawing.Point(354, 3);
            this.btn_download.Name = "btn_download";
            this.btn_download.ShowButtonImage = false;
            this.btn_download.Size = new System.Drawing.Size(150, 32);
            this.btn_download.State = enuButtonState.Normal;
            this.btn_download.TabIndex = 1;
            this.btn_download.Click += new System.EventHandler(this.btn_download_Click);
            // 
            // btn_upload
            // 
            this.btn_upload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_upload.ButtonImageSize = new Size2i(32, 32);
            this.btn_upload.CaptionKey = null;
            this.btn_upload.Checked = false;
            this.btn_upload.DialogResult = enuDialogResult.None;
            this.btn_upload.Location = new System.Drawing.Point(510, 3);
            this.btn_upload.Name = "btn_upload";
            this.btn_upload.ShowButtonImage = false;
            this.btn_upload.Size = new System.Drawing.Size(150, 32);
            this.btn_upload.State = enuButtonState.Normal;
            this.btn_upload.TabIndex = 0;
            this.btn_upload.Click += new System.EventHandler(this.btn_upload_Click);
            // 
            // xToolStrip1
            // 
            this.xToolStrip1.CaptionKey = null;
            this.xToolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.xToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2});
            this.xToolStrip1.Location = new System.Drawing.Point(4, 4);
            this.xToolStrip1.Name = "xToolStrip1";
            this.xToolStrip1.Size = new System.Drawing.Size(668, 39);
            this.xToolStrip1.TabIndex = 1;
            this.xToolStrip1.Text = "xToolStrip1";
            this.xToolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.xToolStrip1_ItemClicked);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Action = null;
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;            
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Action = null;
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(36, 36);
            this.toolStripButton2.Text = "toolStripButton2";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(200, 39);
            // 
            // FtpManagerSurface
            // 
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.xToolStrip1);
            this.Name = "FtpManagerSurface";
            this.Padding = new System.Windows.Forms.Padding(4);
            this.Size = new System.Drawing.Size(676, 464);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.xPanel3.ResumeLayout(false);
            this.xPanel3.PerformLayout();
            this.xToolStrip3.ResumeLayout(false);
            this.xToolStrip3.PerformLayout();
            this.xPanel2.ResumeLayout(false);
            this.xPanel2.PerformLayout();
            this.xToolStrip2.ResumeLayout(false);
            this.xToolStrip2.PerformLayout();
            this.xPanel1.ResumeLayout(false);
            this.xToolStrip1.ResumeLayout(false);
            this.xToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private void xToolStrip1_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {
        }
        
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (FtpConnectFrame v_connect = new FtpConnectFrame())
            {
                //set property
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(v_connect))
                {
                    frm.Title = CoreSystem.GetString ("Title.ConnectToFtpServerFrame");
                    if (frm.ShowDialog() == enuDialogResult.OK)
                    {
                        this.Login = v_connect.Login;
                        this.Server = string.Format ("ftp://{0}", v_connect.Server);
                        this.PassWord = v_connect.PassWord;
                        this.SaveConnectionEntry = v_connect.SaveConnectionEntry;
                        this.Port = v_connect.Port;
                        ConnectToServer();
                    }
                }
            }
        }
        FtpConnexionManager m_FtpConnexionManager;
        public FtpConnexionManager FtpConnexionManager {
            get {
                return this.m_FtpConnexionManager;
            }
            internal set {
                this.m_FtpConnexionManager = value;
                OnConnexionManagerChanged(EventArgs.Empty);
            }
        }
        public event EventHandler ConnexionManagerChanged;
        ///<summary>
        ///raise the ConnexionManagerChanged 
        ///</summary>
        protected virtual void OnConnexionManagerChanged(EventArgs e)
        {
            if (ConnexionManagerChanged != null)
                ConnexionManagerChanged(this, e);
        }
        private void ConnectToServer()
        {
            FtpConnexionManager man = FtpConnexionManager.Create(this.Server, this.Login, this.PassWord, 21, false  );
            if (man !=null)
            {
                    this.FtpConnexionManager = man;
                   LoadServerInfo();
            }
            else {
                System.Windows.Forms.MessageBox.Show("Connexion failed");
            }
        }
        private void LoadServerInfo()
        {
            this.c_serverListFile.Items.Clear();
            this.c_serverListFile.SuspendLayout();
            this.Cursor = Cursors.WaitCursor;
            ListViewItem item = null;
            foreach(string str in this.m_FtpConnexionManager.ListADir ())
            {
                item = new ListViewItem();
                ServerFileInfo c = ServerFileInfo.Create(str);
                item.Tag = c;
                item.Text = c.Name;
                item.Name = c.Name;
                item.SubItems.Add("");
                item.SubItems.Add(c.Size);
                item.SubItems.Add(c.Authorization );
                item.Tag = c;
                this.c_serverListFile.Items.Add(item);
            }
            this.Cursor = Cursors.Default;
            this.c_serverListFile.ResumeLayout();
        }
        void StartLoadServerInfo()
        { 
        }
        /// <summary>
        /// represent a server type info
        /// </summary>
        internal class ServerFileInfo
        {
            private string m_Name;
            private string m_Authorization;
            private string m_Size;
            private string m_Type;
            private string m_Detail;
            private bool m_IsDirectory;
            /// <summary>
            /// 
            /// </summary>
            public string Name {
                get { return this.m_Name; }
            }
            /// <summary>
            /// get indication if this file is directory
            /// </summary>
            public bool IsDirectory
            {
                get { return m_IsDirectory; }
            }
            public string Details { get { return this.m_Detail; } }
            public string Size { get { return this.m_Size; } }
            public string Type { get { return this.m_Type; } }
            public string Authorization { get { return this.m_Authorization; } }
            private ServerFileInfo()
            {
                this.m_Size = null;
                this.m_Type = null;
                this.m_Detail = null;
            }
            public static ServerFileInfo Create(string details)
            {
                //build from details
                string[] tab = details.Split(new char[]{' '}, StringSplitOptions.RemoveEmptyEntries);
                ServerFileInfo sf = new ServerFileInfo();
                sf.m_Authorization = tab[0];
                sf.m_Name = tab[tab.Length - 1];
                sf.m_IsDirectory = (sf.m_Authorization.ToLower()[0] == 'd');
                return sf;
            }
            public override string ToString()
            {
                return this.Name;
            }
        }
        internal class ClientFileInfo
        { 
        }
        private void c_tls_newFolder_Click(object sender, EventArgs e)
        {
            if (this.m_FtpConnexionManager == null)
            {
                return;
            }
            using (FtpNewFolderFrame frame = new FtpNewFolderFrame())
            {
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(frame))
                {
                    frm.Title = CoreSystem.GetString("Title.NewFolder");
                    if (frm.ShowDialog() == enuDialogResult.OK)
                    {
                        this.m_FtpConnexionManager.MakeDirs(frame.DirectoryName);
                        this.ConnectToServer();
                    }
                }
            }
        }
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (this.m_FtpConnexionManager == null)
            {
                return;
            }
            foreach (ListViewItem item in this.c_serverListFile.SelectedItems)
            {
                if ((item.Name == ".") || (item.Name == ".."))
                {
                    continue;
                }
                else { 
                    ServerFileInfo sf =  item.Tag as ServerFileInfo ;
                    if (sf.IsDirectory)
                    {
                        DeleteDirectory(sf.Name, this.m_FtpConnexionManager);                  
                    }
                    else {
                        this.m_FtpConnexionManager.DeleteFiles(sf.Name);
                    }
                    LoadServerInfo();
                }
            }
        }
        private void DeleteDirectory(string name, WinUI.FtpConnexionManager manager)
        {
            FtpConnexionManager v_man = CreateNewManagerFrom(manager, name);
            try
            {
                foreach (string file in v_man.ListDir())
                {
                    if ((file == ".") || (file == ".."))
                        continue;
                    try
                    {
                        if (v_man.DeleteFiles(file) == false)
                        {
                            FtpConnexionManager man = CreateNewManagerFrom(v_man, file);
                            DeleteDirectory(file, v_man);
                        }
                    }
                    catch
                    {
                    }
                }
            }
            catch { 
            }
            manager.DeleteDirs(name);      
        }
        private FtpConnexionManager CreateNewManagerFrom(WinUI.FtpConnexionManager manager, string file)
        {
            FtpConnexionManager man = FtpConnexionManager.Create(manager.Uri + "/"+file, this.Login, this.PassWord, this.Port, false); ;
            return man;
        }
        private void btn_upload_Click(object sender, EventArgs e)
        {
            //upload selected files or directory
            if (this.c_clientListFile.SelectedItems.Count == 0)
            {
                using (FolderBrowserDialog ofd = new FolderBrowserDialog())
                {
                    ofd.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        UploadDirectory(ofd.SelectedPath, this.m_FtpConnexionManager);
                        LoadServerInfo();
                    }
                }
            }
            else { 
                //....
                foreach (ListViewItem  item in this.c_clientListFile.SelectedItems )
                {
                    if (item.Tag is DirectoryInfo)
                    {
                        UploadDirectory((item.Tag as DirectoryInfo).FullName, this.m_FtpConnexionManager);
                    }
                    else if (item.Tag is FileInfo)
                        this.m_FtpConnexionManager.UploadFile((item.Tag as FileInfo).FullName);
                }
            }
        }
        private void btn_download_Click(object sender, EventArgs e)
        {
          //download file from the web site
        }
        private void UploadDirectory(string directory, FtpConnexionManager manager)
        {
            if (this.m_FtpConnexionManager == null)
                return;
            if (!Directory.Exists(directory))
                return;
            string name = new DirectoryInfo(directory).Name;
            manager.Mkdir(name);
            {
                FtpConnexionManager man =FtpConnexionManager.Create (manager.Uri + "/" + name, this.Login, this.PassWord , this.Port, false);
                foreach (string dir in Directory.GetDirectories (directory ))
                {
                    UploadDirectory (dir, man);
                }    
                foreach (string v_files in Directory.GetFiles (directory ))
                {
                    man.UploadFile(v_files);
                }
                man.Close();
            }
        }
        private void c_tls_chmod_Click(object sender, EventArgs e)
        {
            if ((this.m_FtpConnexionManager == null)||(this.c_serverListFile.SelectedItems .Count == 0))
                return;
            //
            using (FtpChmodManagerFrame frame = new FtpChmodManagerFrame())
            {
                using (ICoreDialogForm frm = Workbench.CreateNewDialog(frame ))
                {
                    frm.Title = CoreSystem.GetString("Title.FtpChangeMode");
                    if (frm.ShowDialog() == enuDialogResult.OK)
                    {
                        foreach (ListViewItem  item in this.c_serverListFile.SelectedItems)
                        {
                            if ((item.Name == "." ) || (item.Name == ".."))
                                continue ;
                            ChangeAutorization(this.m_FtpConnexionManager , item.Name , frame.AutorizationToString(), true);
                        }
                        LoadServerInfo();
                    }
                }
            }
        }
        private void ChangeAutorization(WinUI.FtpConnexionManager manager, string name, string authorization, bool recursif)
        {
            if (!manager.Chmod(name, authorization))
            {
            }
            else {
                if (recursif)
                {
                    FtpConnexionManager f = CreateNewManagerFrom(manager, name);
                    if (f != null)
                    {
                        try
                        {
                            foreach (string item in f.ListDir())
                            {
                                if ((item == ".") || (item == ".."))
                                    continue;
                                ChangeAutorization(f, item, authorization, recursif);
                            }
                            f.Close();
                        }
                        catch { 
                        }
                    }                    
                }
            }
        }
        private void c_serverListFile_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void c_serverListFile_DoubleClick(object sender, EventArgs e)
        {
            if (this.m_FtpConnexionManager == null)
                return;
            if (this.c_serverListFile.SelectedItems.Count == 1)
            {
                ListViewItem v_lsvi = this.c_serverListFile.SelectedItems[0];
                ServerFileInfo f = v_lsvi.Tag as ServerFileInfo;
                if ((f.Name ==".." ) || (f.IsDirectory))
                {
                    NavigateServerTo(f.Name);
                }
            }
        }
        private void NavigateServerTo(string p)
        {
            if (p == "..")
            {
                NavigateServerToParent();
            }
            else
            {
                FtpConnexionManager c = CreateNewManagerFrom(this.m_FtpConnexionManager, p);
                if (c != null)
                {
                    this.m_FtpConnexionManager = c;
                    LoadServerInfo();
                }
            }
        }
        private void NavigateServerToParent()
        {
            if (this.m_FtpConnexionManager == null) return;
            Uri uri = new Uri (this.m_FtpConnexionManager.Uri);
            string v_uri = uri.AbsoluteUri.Remove(uri.AbsoluteUri.Length - uri.Segments.Last().Length);
            //string uri = this.m_FtpConnexionManager.Uri.Replace(Uri.UriSchemeFtp, "");
            FtpConnexionManager c = FtpConnexionManager.Create(v_uri, this.Login, this.PassWord, this.Port, false);
            if (c != null)
            {
                this.m_FtpConnexionManager = c;
                LoadServerInfo();
            }
        }
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = this.SelectedPath;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.SelectedPath = fbd.SelectedPath;
                }
            }
        }    
    }
}

