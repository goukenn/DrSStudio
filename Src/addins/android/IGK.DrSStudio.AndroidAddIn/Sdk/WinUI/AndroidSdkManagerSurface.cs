

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace IGK.DrSStudio.Android.Sdk.WinUI
{
    using IGK.ICore.WinCore;
    using IGK.ICore;
    using IGK.ICore.Xml;
    using IGK.ICore.Web;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.DrSStudio.Android.Entities;


    using IGK.DrSStudio.Android.Sdk;
    using IGK.DrSStudio.Android.Settings;
    using IGK.DrSStudio.Android.Tools;
    using IGK.DrSStudio.Android.WinUI;
    using IGK.ICore.Resources;
    using IGK.ICore.WinUI;

    [CoreSurface("{167ECBBC-80E7-45C2-981C-74FA4D8048B0}")]
    /// <summary>
    /// represent a android sdk manager utiliity surface
    /// </summary>
    public class AndroidSdkManagerSurface : AndroidSurfaceBase,
        ICoreWorkingReloadViewSurface ,
        IMessageFilter 
    {
        private IGKXPanel c_pan_content;
        private IGKXPanel c_pan_viewHost;
        private System.Windows.Forms.Splitter c_splitter1;
        private System.Windows.Forms.TreeView c_treeView1;
        private IGKXPanel c_pan_header;
        private IReloadListener m_reloadListener;

      
      
        public class AndroidSdkResource : AndroidSdkItemBase
        {
            public AndroidSdkResource(AndroidTargetInfo target)
                : base(target)
            {

            }
            public string Group
            {   get;
                set;
            }
            public string Folder
            {
                get;
                set;
            }
            public override string ToString()
            {
                return Path.GetFileName(this.Folder);
            }


            public AndroidSdkResourceItem[] GetItems() {
                List<AndroidSdkResourceItem> c = new List<AndroidSdkResourceItem>();
                foreach (var item in Directory.GetFiles(this.Folder))
                {
                    AndroidSdkResourceItem i = new AndroidSdkResourceItem(
               item,                
                this, this.Target );
                    c.Add(i);
                }
                return c.ToArray();
            }

            public class AndroidSdkResourceItem : AndroidSdkItemBase
            {
                private string m_file;
                private AndroidSdkResource m_owner;
                public string File { get { return this.m_file; } }
                public string Group { get { return this.m_owner.Group ; } }
                private AndroidSdkResource Owner{get{return this.m_owner ;}}
                public override string ToString()
                {
                    return Path.GetFileName(this.m_file);
                }
                internal  AndroidSdkResourceItem(string file,  AndroidSdkResource owner
                    ,AndroidTargetInfo target):base(target )
                {
                    this.m_file = file;
                    this.m_owner = owner;
                }
            }
        }
        
       
        public class AndroidActionItem : AndroidSdkItemBase
        {
            public string File
            {
                get;
                set;
            }
            public AndroidActionItem(AndroidTargetInfo target)
                : base(target)
            {

            }
        }
        public class AndroidSkinInfo : AndroidSdkItemBase
        {
            public AndroidSkinInfo(AndroidTargetInfo target)
                : base(target)
            {

            }
        }
     

    
        public AndroidSdkManagerSurface()
        {
            this.InitializeComponent();
            this.Load += _Load;
        }

        void _Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;
            this.Title = "title.androidsdkmanagersurface".R();
            string v_sdk = AndroidSetting.Instance.PlatformSDK;


            if (Directory.Exists(v_sdk))
            {
                //load and inistiliazer sdk folder
                LoadAndInitSDKFolder(v_sdk);
            }

            if (CoreSystem.Instance.Workbench == null)
            {
                Application.AddMessageFilter(this);
            }
        }

        private void LoadAndInitSDKFolder(string folder)
        {
            this.c_treeView1.Nodes.Clear();

            //laod plateforms
            

            //foreach (string d in Directory.GetDirectories(Path.Combine(folder, "platforms")))
            //{
            //    //TreeNode n = this.c_treeView1.Nodes.Add(Path.GetFileName(d));
            //    //n.Tag = new AndroidTargetInfo(){                     
            //    //};
            //}

            foreach (AndroidTargetInfo d in AndroidTool.Instance.GetAndroidTargets())
            {
                TreeNode n = this.c_treeView1.Nodes.Add(d.TargetName);
                n.Tag = d;
                LoadEntities(n, d, folder );
            }
        }

        private void LoadEntities(TreeNode n, AndroidTargetInfo info, string folder)
        {
            string dir = Path.Combine(folder, "platforms/"+info.TargetName);
            if (Directory.Exists(dir))
            {
                foreach(string s in Directory.GetDirectories(dir))
                {
                    TreeNode child = n.Nodes.Add (Path.GetFileName (s));
                    switch (child.Text.ToLower())
                    {
                        case "data":
                            //load data
                            LoadData(child, s, info);
                            break;
                        case "templates":
                            LoadTemplates(child, s, info);
                            break;
                        case "skins":
                            LoadSkins(child, s, info);
                            break;
                        default:
                            break;
                    }

                }
            }
        }

        private void LoadSkins(TreeNode child, string s, AndroidTargetInfo info)
        {
            foreach (string d in Directory.GetDirectories(s))
            {
                var node =  child.Nodes.Add(Path.GetFileName(d));
                node.Tag = new AndroidSkinInfo(info)
                {
                    Name = Path.GetFileName(d)
                };
            }
        }

        private void LoadTemplates(TreeNode child, string s, AndroidTargetInfo info)
        {
            foreach (string d in Directory.GetFiles(s))
            {
                if (Path.GetFileName(d) == "NOTICE.txt")
                    continue;

                var node = child.Nodes.Add(Path.GetFileName(d));
                node.Tag = new AndroidSdkTemplateFile(info)
                {
                    Name = Path.GetFileName(d),
                    File = d
                };
            }
        }

        private void LoadData(TreeNode child, string s, AndroidTargetInfo info)
        {
            Dictionary<string, object> v_entities = new Dictionary<string, object>();
            string n = string.Empty;
            var v_ac = child.Nodes.Add("Actions");
            TreeNode node = null;
            foreach (string d in Directory.GetFiles(s))
            {
                if ((n = Path.GetFileName(d)) == "NOTICE.txt")
                    continue;
                
                switch (n)
                {
                    case "activity_actions.txt":
                    case "service_actions.txt":
                    case "broadcast_actions.txt":
                        node = v_ac.Nodes.Add(Path.GetFileNameWithoutExtension(n));
                        node.Tag = new AndroidActionItem(info) { 
                            Name = n ,
                            File = d
                        };
                        break;
                    case "categories.txt":
                    case "features.txt":
                         node = child.Nodes.Add(Path.GetFileNameWithoutExtension(n));
                         node.Tag = new AndroidActionItem(info) { 
                            Name = n ,
                            File = d
                        };
                        break;
                    case "widgets.txt":
                         node = child.Nodes.Add(Path.GetFileNameWithoutExtension(n));
                         node.Tag = AndroidSdkWidgets.LoadWidgets(d, info);
                        break;
                    default:
                      //  node = child.Nodes.Add(Path.GetFileNameWithoutExtension(n));
                        break;
                }
            }

            LoadResources(child.Nodes.Add ("Resources"), Path.Combine(s, "res"), info);
            LoadAndroidResources(child.Nodes.Add("UtilityDefinition"), Path.Combine(s, "res"),  info);

        }

        private void LoadAndroidResources(TreeNode treeNode, string p, AndroidTargetInfo info)
        {
            CoreXmlElement t = CoreXmlElement.CreateXmlNode("dummy");
            List<CoreXmlElementBase> v_tt = new List<CoreXmlElementBase> ();
            
            LoadColors(treeNode.Nodes.Add("Colors"), p, info);
            LoadThemes(treeNode.Nodes.Add("Themes"), p, info);           
        }

        private void LoadColors(TreeNode treeNode, string p, AndroidTargetInfo info)
        {
            CoreXmlElement t = CoreXmlElement.LoadFile(Path.GetFullPath(p + "/values/colors.xml"));            
            foreach (var i in t.getElementsByTagName("color"))
            {
                    var n = treeNode.Nodes.Add(i["name"]);
                    n.Tag = new AndroidEntityColor(i["name"],
                        i["color"]);
            }
            
            t.Clear();
        }

        private void LoadThemes(TreeNode treeNode,  string p, AndroidTargetInfo info)
        {
            CoreXmlElement t = CoreXmlElement.LoadFile(Path.GetFullPath(p + "/values/themes.xml"));
            if (t==null)return ;
            List<string> tg = new List<string>();
            Dictionary<string, object> ttg = new Dictionary<string, object>();
            foreach (var i in t.getElementsByTagName("style"))
            {
                string  n =i["name"];
                tg.Add(n);
                var g = new AndroidSdkTheme(n, info);
                g.loadAttributes(i);
                ttg.Add(n, g);
            }
            tg.Sort();
            foreach (string item in tg)
            {
                var n = treeNode.Nodes.Add(item);
                n.Name = item;              
                n.Tag = ttg[item ];
            }            
            t.Clear();
        }
        /// <summary>
        /// load resources node
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="p"></param>
        private void LoadResources(TreeNode treeNode, string p, AndroidTargetInfo info)
        {
            string model = "^(?<model>([^-])+)(-(?<info>(.)+)){0,1}$";
            string key = "";
            string g = string.Empty ;
            TreeNode c = null;
            Dictionary<string, TreeNode> lst = new Dictionary<string, TreeNode>();
            foreach (string d in Directory.GetDirectories(p))
            {
                key = Path.GetFileName (d);

                var m = Regex.Match(key, model);
                g = m.Groups["model"].Value;
                if (lst.ContainsKey(g))
                {
                    var cNode = lst[g];
                   c =  cNode.Nodes.Add(key);

                }
                else
                {
                    c = treeNode.Nodes.Add(key);
                    lst.Add (g,c); 
                }
                c.Tag = new AndroidSdkResource(info)
                {
                    Folder= d,
                    Group = g,
                    Name = Path.GetFileName(d)
                };
            }
        }

        private void InitializeComponent()
        {            
            this.c_pan_header = new IGKXPanel();
            this.c_pan_content = new IGKXPanel();
            this.c_pan_viewHost = new IGKXPanel();
            this.c_splitter1 = new System.Windows.Forms.Splitter();
            this.c_treeView1 = new System.Windows.Forms.TreeView();
            this.c_pan_content.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_pan_header
            // 
            this.c_pan_header.CaptionKey = null;
            this.c_pan_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_pan_header.Location = new System.Drawing.Point(0, 0);
            this.c_pan_header.Name = "c_pan_header";
            this.c_pan_header.Size = new System.Drawing.Size(670, 43);
            this.c_pan_header.TabIndex = 0;
            // 
            // c_pan_content
            // 
            this.c_pan_content.CaptionKey = null;
            this.c_pan_content.Controls.Add(this.c_pan_viewHost);
            this.c_pan_content.Controls.Add(this.c_splitter1);
            this.c_pan_content.Controls.Add(this.c_treeView1);
            this.c_pan_content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_content.Location = new System.Drawing.Point(0, 43);
            this.c_pan_content.Name = "c_pan_content";
            this.c_pan_content.Size = new System.Drawing.Size(670, 319);
            this.c_pan_content.TabIndex = 1;
            // 
            // c_pan_viewHost
            // 
            this.c_pan_viewHost.CaptionKey = null;
            this.c_pan_viewHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_pan_viewHost.Location = new System.Drawing.Point(236, 0);
            this.c_pan_viewHost.Name = "c_pan_viewHost";
            this.c_pan_viewHost.Size = new System.Drawing.Size(434, 319);
            this.c_pan_viewHost.TabIndex = 2;
            // 
            // c_splitter1
            // 
            this.c_splitter1.Location = new System.Drawing.Point(233, 0);
            this.c_splitter1.Name = "c_splitter1";
            this.c_splitter1.Size = new System.Drawing.Size(3, 319);
            this.c_splitter1.TabIndex = 1;
            this.c_splitter1.TabStop = false;
            // 
            // c_treeView1
            // 
            this.c_treeView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.c_treeView1.Location = new System.Drawing.Point(0, 0);
            this.c_treeView1.Name = "c_treeView1";
            this.c_treeView1.Size = new System.Drawing.Size(233, 319);
            this.c_treeView1.TabIndex = 0;
            this.c_treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.c_treeView1_AfterSelect);
            // 
            // AndroidSdkManagerSurface
            // 
            this.Controls.Add(this.c_pan_content);
            this.Controls.Add(this.c_pan_header);
            this.Name = "AndroidSdkManagerSurface";
            this.Size = new System.Drawing.Size(670, 362);
            this.c_pan_content.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void c_treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag is IAndroidSdkItem s)
            {
                this.c_pan_viewHost.SuspendLayout();
                this.c_pan_viewHost.Controls.Clear();
                this.ShowItem(s);
                this.c_pan_viewHost.ResumeLayout();
            }
            else
            {
                this.c_pan_viewHost.Controls.Clear();
            }
        }

      

        public float ListFontSize {
            get {
                return
            "14pt".ToPixel();
            }
        }
        #region visitor
        private void ShowItem(IAndroidSdkItem s)
        {
            this.m_reloadListener = null;
            MethodInfo.GetCurrentMethod().Visit(this, s);
        }

        public void ShowItem(AndroidSdkWidgets actions)
        {
            Panel v_pan = new Panel();
            ListBox v_lst = new ListBox();
            Label v_label = new Label();

            v_label.Dock = DockStyle.Top;
            v_label.AutoSize = false;
            v_label.Height = 48;
            v_label.Font = new Font(v_label.Font.FontFamily, 24);
            v_label.Text = actions.Name;

            v_lst.Dock = DockStyle.Fill;

            foreach (AndroidSdkWidgets.AndroidWidget  item in actions)
            {
                v_lst.Items.Add(item);
            }
            v_lst.Sorted = true;
            v_lst.Font = new Font(v_label.Font.FontFamily, this.ListFontSize);

            v_lst.IntegralHeight = false;
            v_pan.SuspendLayout();
            v_pan.Controls.Add(v_lst);
            v_pan.Controls.Add(v_label);
            v_pan.ResumeLayout();
            v_pan.Dock = DockStyle.Fill;
            this.c_pan_viewHost.Controls.Add(v_pan);
        }
        public void ShowItem(AndroidActionItem actions)
        {
            Panel v_pan = new Panel();
            ListBox v_lst = new ListBox();
            Label v_label = new Label();

            v_label.Dock = DockStyle.Top;
            v_label.AutoSize = false;
            v_label.Height = 48;
            v_label.Font = new Font(v_label.Font.FontFamily, 24);
            v_label.Text = actions.Name;
            
            v_lst.Dock = DockStyle.Fill;
            v_lst.Font = new Font(v_label.Font.FontFamily, this.ListFontSize );
            if (File.Exists(actions.File))
            {
                var s = File.ReadAllLines(actions.File);
                Array.Sort(s);
                v_lst.Items.AddRange(s);
            }
            v_lst.IntegralHeight = false;

            v_pan.SuspendLayout();
            v_pan.Controls.Add(v_lst);
            v_pan.Controls.Add(v_label);
            v_pan.ResumeLayout();
            v_pan.Dock = DockStyle.Fill;
            this.c_pan_viewHost.Controls.Add(v_pan);
        }

        public void ShowItem(AndroidSdkResource actions)
        {
            Panel v_pan = new Panel();
            ListBox v_lst = new ListBox();
            Label v_label = new Label();

            v_label.Dock = DockStyle.Top;
            v_label.AutoSize = false;
            v_label.Height = 48;
            v_label.Font = new Font(v_label.Font.FontFamily, 24);
            v_label.Text = actions.ToString();

            v_lst.Dock = DockStyle.Fill;
            v_lst.Font = new Font(v_label.Font.FontFamily, this.ListFontSize);

            foreach (var item in actions.GetItems())
            {
                v_lst.Items.Add(item);
            }

            v_lst.IntegralHeight = false;
            v_lst.MouseDoubleClick +=   (o,e)=>{
                if (v_lst.SelectedItem is AndroidSdkResource.AndroidSdkResourceItem i)
                {

                    v_label.Text = actions.ToString() + " - " + Path.GetFileName(i.File);
                    v_pan.Controls.Remove(v_lst);

                    if (Regex.IsMatch(i.File, "\\.(png|jpg|jpeg)$"))
                    {
                        PictureBox pic = new PictureBox();
                        pic.SizeMode = PictureBoxSizeMode.CenterImage;
                        pic.Image = Image.FromFile(i.File);
                        pic.Dock = DockStyle.Fill;
                        pic.BackColor = Colorf.White.ToGdiColor();
                        v_pan.Controls.Add(pic);
                        v_pan.Controls.SetChildIndex(pic, 0);
                    }
                    else
                    {
                        var b = new ICSharpCode.AvalonEdit.TextEditor();
                        b.IsReadOnly = true;

                        var ehost = new ElementHost();
                        ehost.Child = b;
                        ehost.Dock = System.Windows.Forms.DockStyle.Fill;

                        //setup device 
                        b.FontSize = "12px".ToPixel();
                        b.FontFamily = "consolas".WpfFontFamily();
                        v_pan.Controls.Add(ehost);
                        v_pan.Controls.SetChildIndex(ehost, 0);
                        b.Load(i.File);
                        b.SyntaxHighlighting
                           = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinitionByExtension(
                           Path.GetExtension(i.File));
                    }
                }
            };

            v_pan.SuspendLayout();
            v_pan.Controls.Add(v_lst);
            v_pan.Controls.Add(v_label);
            v_pan.ResumeLayout();
            v_pan.Dock = DockStyle.Fill;
            this.c_pan_viewHost.Controls.Add(v_pan);
        }

        public void ShowItem(AndroidSdkTheme theme)
        {
            Panel v_pan = new Panel();
            System.Windows.Forms.WebBrowser v_lst = new System.Windows.Forms.WebBrowser();
            Label v_label = new Label();

            v_label.Dock = DockStyle.Top;
            v_label.AutoSize = false;
            v_label.Height = 48;
            v_label.Font = new Font(v_label.Font.FontFamily, 24);
            v_label.Text = theme.Name;

            v_lst.Dock = DockStyle.Fill;

            var doc = CoreXmlWebDocument.CreateICoreDocument();
            doc.InitAndroidWebDocument();
            doc.ForWebBrowserDocument = true;
            var rowDef = new RowDef() { 
                Theme = theme
            };
            doc.Body.LoadString (
                CoreWebUtils.EvalWebStringExpression(

          Encoding.UTF8.GetString (CoreResources.GetResource ("android_sdk_theme_view"))

                ,rowDef));

            v_lst.IGKApplyBrowserDocumentText(doc);
            v_lst.ObjectForScripting = new AndroidSdkScriptManager(this, theme);
          

            
            v_pan.SuspendLayout();
            v_pan.Controls.Add(v_lst);
            v_pan.Controls.Add(v_label);
            v_pan.ResumeLayout();
            v_pan.Dock = DockStyle.Fill;
            this.c_pan_viewHost.Controls.Add(v_pan);


            this.m_reloadListener = new ReloadThemeView(this, theme);
        }

        #endregion
        class RowDef
        {
            public string getThemeName() {
                return Theme.Name;
            }
            public string getThemeParentName()
            {
                if (Theme.Parent == null)
                    if (Theme.Name != "theme")
                        return "theme";
                return Theme.Parent ?? "noparent";
            }
            public string getThemeProperties() {
                StringBuilder sb = new StringBuilder();
                foreach (KeyValuePair<string, string> i in this.Theme.GetProperties())
                {
                    sb.Append(string.Format("<li class=\"disptabr\"><span class=\"disptabc f-cell\">{0}</span><span class=\"disptabc s-cell\">{1}</span></li>", i.Key, i.Value));
                }
                return sb.ToString();
            }

            public AndroidSdkTheme Theme { get; set; }
        }

        internal void OpenTheme(string name)
        {
            if (string.IsNullOrEmpty(name)) 
                return;
            var s = this.c_treeView1.SelectedNode.Parent.Nodes[name];
            if (s != null)
            {
                this.c_treeView1.SelectedNode = s;
            }
        }

        public void Reload()
        {
            if (this.m_reloadListener != null)
                this.m_reloadListener.Reload();
        }

        public class  ReloadThemeView : IReloadListener
        {
            private AndroidSdkManagerSurface androidSdkManagerSurface;
            private AndroidSdkTheme theme;

            public ReloadThemeView(AndroidSdkManagerSurface androidSdkManagerSurface, AndroidSdkTheme theme)
            {
                this.androidSdkManagerSurface = androidSdkManagerSurface;
                this.theme = theme;
            }
            public void Reload()
            {
                this.androidSdkManagerSurface.ShowItem(theme);
            }
        }

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg == 0x100)
            {
                if (m.WParam.ToInt32() == 0x74)// 0x74 ==== F5
                {
                    this.Reload();
                    return true;
                }
            }
            return false;
        }
    }
    
    interface IReloadListener
    {
        void Reload();
    }
}
