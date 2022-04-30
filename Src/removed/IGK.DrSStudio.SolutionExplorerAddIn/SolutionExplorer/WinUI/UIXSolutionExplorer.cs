

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: UIXSolutionExplorer.cs
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
file:UIXSolutionExplorer.cs
*/
using IGK.ICore;using IGK.DrSStudio.Tools.SolutionExplorer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Tool.SolutionExplorer.WinUI
{
    using IGK.DrSStudio.Resources;
    using IGK.DrSStudio.WinUI;
    using System.Drawing;
    /// <summary>
    /// represent the solution explorer surface
    /// </summary>
    public class UIXSolutionExplorer :         
        IGKXToolConfigControlBase
    {
        private IGKXTreeView c_treeview;
        private XToolStrip c_toolstrip;
        private ImageList c_imagelist;
        private System.ComponentModel.IContainer components;
        private ContextMenuStrip c_contextMenu;
        private const string IMG_SOLUTION = "Solution";
        public UIXSolutionExplorer()
            : base(CoreSolutionExplorerTool.Instance )
        {
            this.InitializeComponent();
            c_contextMenu = new ContextMenuStrip();
            this.ContextMenuStrip = c_contextMenu;

                WinCoreControlPaint.RegisterImgList(
                    this.c_imagelist, IMG_SOLUTION,
                    CoreResources.GetImage ("Btn_solutions") as Bitmap);
        }
        /// <summary>
        /// add button to the tool strip
        /// </summary>
        /// <param name="button"></param>
        public void AddButton(ToolStripButton button)
        {
            this.c_toolstrip.Items.Add(button);
        }
        public void RemoveButton(ToolStripButton button)
        {
            this.c_toolstrip.Items.Remove(button);
        }
        public ToolStripSeparator  AddSeparator()
        {
            ToolStripSeparator sep = new ToolStripSeparator();
            this.c_toolstrip.Items.Add(sep);
            return sep;
        }
        public void LoadSolution(IGK.DrSStudio.Codec.ICoreProject solution)
        {
            this.c_treeview.SuspendLayout();
            this.Cursor = Cursors.WaitCursor;
            this.c_treeview.Nodes.Clear();
            this.c_contextMenu.Items.Clear();
            if (solution != null)
            {
                TreeNode rootNode = new TreeNode ("SolutionExplorer");
                rootNode.ImageKey = IMG_SOLUTION;
                TreeNode node = null;
                IGK.DrSStudio.Codec.ICoreProjectDesignableItem v_t = null;
                foreach (KeyValuePair<string,IGK.DrSStudio.Codec.ICoreProjectItem> item in solution)
                {
                    v_t = item.Value as IGK.DrSStudio.Codec.ICoreProjectDesignableItem;
                    if (v_t == null)
                        continue;
                    if (!v_t.IsVisible)
                        continue;
                    node = new TreeNode (item.Key );
                    node.Tag = item ;
                    if (v_t.HasChild)
                    {
                        SetUpNode(node, v_t );
                    }
                    rootNode.Nodes.Add(node);
                }
                this.c_treeview.Nodes.Add(rootNode);
                this.LoadContextMenu(solution);
            }
            this.c_treeview.ResumeLayout();
            this.Cursor = Cursors.Default;
        }
        private void LoadContextMenu(IGK.DrSStudio.Codec.ICoreProject solution)
        {
            if (solution.Actions != null)
            {
                foreach (IGK.DrSStudio.Actions.ICoreProjectAction ack in solution.Actions)
                {
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Tag = ack;
                    item.Click += new EventHandler(item_Click);
                    this.c_contextMenu.Items.Add(item);
                }
            }
        }
        void item_Click(object sender, EventArgs e)
        {
            IGK.DrSStudio.Actions.ICoreProjectAction ack = (sender as ToolStripMenuItem).Tag as
                IGK.DrSStudio.Actions.ICoreProjectAction ;
            ack.DoAction();
        }
        private void SetUpNode(TreeNode node, IGK.DrSStudio.Codec.ICoreProjectDesignableItem  item)
        {
            TreeNode cnode = null;
            if (!item.HasChild)
                return;
            foreach (string keys in item.GetKeys ())
            {
                cnode = new TreeNode(keys);
                cnode.Tag = item;
                node.Nodes.Add(cnode);
                foreach (IGK.DrSStudio.Codec.ICoreProjectDesignableItem hitem in item.GetChildsToArray(keys))
                {
                    if (hitem == null) continue;
                    if (hitem.IsVisible)
                    {
                        SetUpNode(cnode, hitem);
                    }
                }
            }
        }
        #region component
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.c_treeview = new IGK.DrSStudio.WinUI.IGKXTreeView();
            this.c_toolstrip = new IGK.DrSStudio.WinUI.XToolStrip();
            this.c_imagelist = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // c_treeview
            // 
            this.c_treeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_treeview.ImageIndex = 0;
            this.c_treeview.ImageList = this.c_imagelist;
            this.c_treeview.Location = new System.Drawing.Point(0, 25);
            this.c_treeview.Name = "c_treeview";
            this.c_treeview.SelectedImageIndex = 0;
            this.c_treeview.Size = new System.Drawing.Size(293, 303);
            this.c_treeview.TabIndex = 0;
            // 
            // c_toolstrip
            // 
            this.c_toolstrip.CaptionKey = null;
            this.c_toolstrip.Location = new System.Drawing.Point(0, 0);
            this.c_toolstrip.Name = "c_toolstrip";
            this.c_toolstrip.Size = new System.Drawing.Size(293, 25);
            this.c_toolstrip.TabIndex = 1;
            this.c_toolstrip.Text = "xToolStrip1";
            // 
            // c_imagelist
            // 
            this.c_imagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.c_imagelist.ImageSize = new System.Drawing.Size(16, 16);
            this.c_imagelist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // UIXSolutionExplorer
            // 
            this.Controls.Add(this.c_treeview);
            this.Controls.Add(this.c_toolstrip);
            this.Name = "UIXSolutionExplorer";
            this.Size = new System.Drawing.Size(293, 328);
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}

