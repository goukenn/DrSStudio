using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.WinUI.Controls;
using System.IO;
using System.Threading;

namespace IGK.DrSStudio.FileBrowser.WinUI
{
    public partial class FBSurface : IGKXWinCoreWorkingSurface
    {
        private FBPreview c_preview;
        private string m_Folder;
        private IFBFileOpenListener m_fileOpenListener;
        /// <summary>
        /// set the file open listener
        /// </summary>
        /// <param name="listener"></param>
        public void SetFileOpenListener(IFBFileOpenListener listener) {
            this.m_fileOpenListener = listener;
        }
        public string Folder
        {
            get { return m_Folder; }
            set
            {
                if (m_Folder != value)
                {
                    m_Folder = value;
                    OnFolderChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler FolderChanged;
        private Thread worker;
        ///<summary>
        ///raise the FolderChanged 
        ///</summary>
        protected virtual void OnFolderChanged(EventArgs e)
        {
            if (FolderChanged != null)
                FolderChanged(this, e);
        }

        public FBSurface()  {
            InitializeComponent();
            this.Load +=_Load;
            this.c_lst_file.MultiSelect = false;
            this.c_lst_file.SelectedIndexChanged += c_lst_file_SelectedIndexChanged;
        }

        void c_lst_file_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.c_lst_file.SelectedItems.Count == 1)
            {
                string f = Path.Combine(this.Folder, this.c_lst_file.SelectedItems[0].Name);
                if (File.Exists(f)) {
                    this.c_preview.FileName = f;
                }
            }
        }

        private void _Load(object sender, EventArgs e)
        {
               this.FolderChanged += FBSurface_FolderChanged;
            

            this.c_treeview_imglist.Images.Add("drive", TUIKLib.GetRegIcon("drive"));
            this.c_treeview_imglist.Images.Add("folder", TUIKLib.GetRegIcon("folder"));
            //this.c_treeview_imglist.Images.Add("exefile", TUIKLib.GetRegIcon("exefile"));
            this.c_treeview_imglist.Images.Add("default", TUIKLib.GetRegIcon("Unknown"));


            c_list_imglist.Images.Add("drive", TUIKLib.GetRegIcon("drive"));
            c_list_imglist.Images.Add("folder", TUIKLib.GetRegIcon("folder"));
            c_list_imglist.Images.Add("dllfile", TUIKLib.GetRegIcon("dllfile"));
            c_list_imglist.Images.Add("default", TUIKLib.GetRegIcon("Unknown"));

            this.c_trv_explorer.AfterSelect += c_trv_explorer_AfterSelect;
            this.c_lst_file.DoubleClick += c_lst_file_DoubleClick;
            this.c_lst_file.KeyUp += c_lst_file_KeyUp;
            __initTreeNode();
            //this.Folder = Environment.CurrentDirectory;
            
        }

        void c_lst_file_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode) { 
                case Keys.Delete :
                    if (this.c_lst_file.SelectedItems.Count >0)
                    {
                        foreach (ListViewItem  s
                        in this.c_lst_file.SelectedItems)
                        {
                            string f = Path.Combine(this.Folder, s.Text);
                            if (File.Exists(f))
                            {
                               Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile (f, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin );
                            }
                        }
                        this.__loadListView();
                    }
                    break;
            }
        }

      

        void c_lst_file_DoubleClick(object sender, EventArgs e)
        {
            if (this.c_lst_file.SelectedItems.Count == 1) {
                var s = this.c_lst_file.SelectedItems[0];
                if (s.Tag.ToString() == "0")
                {
                    var n = this.c_trv_explorer.SelectedNode.Nodes[s.Text];
                    this.c_trv_explorer.SelectedNode = n;
                }
                else {
                    string f = Path.Combine(this.Folder, s.Text);
                    if (File.Exists(f)) {
                        if (this.m_fileOpenListener != null)
                            this.m_fileOpenListener.Open(f);
                        else
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(f);
                            }
                            catch{

                            }
                        }
                    }
                }
            
            }
        }

        void c_trv_explorer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var n = this.c_trv_explorer.SelectedNode;
            if (n != null) {
                string s = n.FullPath;
                //load tree node
                loadTreeNode(n, s);
                this.Folder = s;
            }
        }

        private void loadTreeNode(TreeNode n, string s)
        {
            n.Nodes.Clear();
            try
            {
                foreach (var d in Directory.GetDirectories(s))
                {
                    var m = n.Nodes.Add(Path.GetFileName(d));
                    m.Name = Path.GetFileName(d);
                    m.ImageKey = "folder";
                    m.SelectedImageKey = "folder";
                }
            }
            catch { 
            }
        }

        private void __initTreeNode()
        {
            this.c_trv_explorer.Nodes.Clear();

            foreach (string s in Environment.GetLogicalDrives()) {
                var n = this.c_trv_explorer.Nodes.Add(s);
                n.ImageKey = "drive";
                n.SelectedImageKey = "drive";
            }
        }

        void FBSurface_FolderChanged(object sender, EventArgs e)
        {
            this.__loadListView();
          
        }

        private void __loadListView()
        {
            if (this.worker != null)
            {
                this.worker.Abort();
                this.worker.Join();
            }
            this.c_lst_file.Items.Clear();
            Thread th = new Thread(Dir);
            th.IsBackground = false;
            th.Start();
            this.worker = th;
        }
        private void Dir() {
            if (!Directory.Exists(this.Folder))
                return;

            string q = this.Folder;
            bool complete = false;
            List<ListViewItem> b = new List<ListViewItem>();
            MethodInvoker d = () => {
                        c_lst_file.Items.AddRange(b.ToArray());
                        b.Clear();
                        if (complete) {
                            c_lst_file.Sort();
                        }
                    };
            try
            {
                foreach (var item in Directory.GetDirectories(this.Folder))
                {
                    var t = new ListViewItem(Path.GetFileName(item));
                    t.Name = Path.GetFileName(item);
                    t.Tag = 0;
                    _init(t);
                    b.Add(t);

                    if ((b.Count % 10) == 0)
                    {
                        this.Invoke(d);
                    }

                }
                foreach (var item in Directory.GetFiles(this.Folder))
                {
                    var t = new ListViewItem(Path.GetFileName(item));
                    t.Name = Path.GetFileName(item);
                    t.Tag = 1;
                    _init(t);
                    b.Add(t);

                    if ((b.Count % 10) == 0)
                    {
                        this.Invoke(d);
                    }
                }
            }
            catch { 
                //access deny
            }

            if (b.Count > 0)
            {
                this.Invoke(d);
            }
        }

        private void _init(ListViewItem t)
        {
            int tag = int.Parse (t.Tag.ToString());
            switch (tag)
            {
                case 0:
                    t.ImageKey = "folder";                    
                    break;
                case 1:
                    var ex = Path.GetExtension(t.Text).ToLower();
                    switch (ex)
	                {
                        case ".dll":
                            t.ImageKey = "dllfile";
                            break;
                        case ".exe":
                            {
                                Icon c = Icon.ExtractAssociatedIcon(Path.Combine(this.Folder, t.Text));
                                this.Invoke((MethodInvoker)delegate()
                                {
                                    this.c_list_imglist.Images.Add(t.Text, c);
                                });
                               
                                t.ImageKey = t.Text;
                            }
                            break;                        
                        default:
                            //get default image key
                            var key = ex;
                            if (!this.c_list_imglist.Images.ContainsKey(ex))
                            {
                                try
                                {
                                    Icon c = TUIKLib.GetShellExtensionIcon(ex);// Icon.ExtractAssociatedIcon(Path.Combine(this.Folder, t.Text));
                                    if ((c != null) && (c.Width > 0 ) && (c.Height > 0))
                                    {
                                        this.Invoke((MethodInvoker)delegate()
                                        {
                                            this.c_list_imglist.Images.Add(ex, c);
                                        });
                                    }
                                    else
                                        key = "default";
                                }
                                catch {
                                    key = "default";
                                }
                            }
                            t.ImageKey = key;
                            
                            break;
	                }
                    
                    break;
                default:
                    break;
            }

        }
    }
}
