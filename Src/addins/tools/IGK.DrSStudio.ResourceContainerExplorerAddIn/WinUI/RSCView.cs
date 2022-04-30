using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Tools;
using IGK.DrSStudio.WinUI;
using IGK.ICore.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IGK.ICore.WinCore.Controls;

namespace IGK.DrSStudio.WinUI
{
    class RSCView: IGKXToolHostedControl
    {
        private RSCTool rSCTool;
        private ICoreResourceContainer m_resources;
        private RSCTreeView c_treeView;

        public RSCView()
        {
            this.InitializeComponent();
        }

        public RSCView(DrSStudio.Tools.RSCTool rSCTool):this()
        {
            this.rSCTool = rSCTool;
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.ResourceChanged += RSCView_ResourceChanged;
        }

        void RSCView_ResourceChanged(object sender, EventArgs e)
        {
            this.c_treeView.SuspendLayout();
            this.c_treeView.Nodes.Clear();

            if (this.Resources != null)
            {
                //
                ///Generate Resources
                //
                GenereateResource(this.Resources);
            }
            this.c_treeView.ResumeLayout();
        }

        private void GenereateResource(ICoreResourceContainer rscontainer)
        {
            Dictionary<string, TreeNode> v_node = new Dictionary<string, TreeNode>();
            TreeNode v_n = null;
            string v_key = string.Empty ;
            foreach(KeyValuePair<string,ICoreResourceItem> c in rscontainer)
            {
                v_key = c.Value.ResourceType.ToString();

                if (v_node.ContainsKey(v_key))
                {
                    v_n = v_node[v_key];
                }
                else {
                    v_n = new TreeNode(v_key);
                    v_node.Add(v_key, v_n);
                    this.c_treeView.Nodes.Add(v_n);
                }

                var v_c =v_n.Nodes.Add(c.Value.Id);
                v_c.Tag = c;
            }
        }

        private void InitializeComponent()
        {
            this.c_treeView = new RSCTreeView();
            this.c_treeView.Dock = System.Windows.Forms.DockStyle.Fill;            
            this.Controls.Add(this.c_treeView);
        }
        /// <summary>
        /// get the resource managed
        /// </summary>
        public ICoreResourceContainer Resources
        {
            get
            {
                return this.m_resources;
            }
            set
            {
                if (this.m_resources != value)
                {
                    this.m_resources = value;
                    OnResourceChanged(EventArgs.Empty);
                }
            }
        }
   

        public event EventHandler ResourceChanged;
        ///<summary>
        ///raise the ResourceChanged 
        ///</summary>
        protected virtual void OnResourceChanged(EventArgs e)
        {
            if (ResourceChanged != null)
                ResourceChanged(this, e);
        }

    }
}
