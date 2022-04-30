

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: GlobalSolutionExplorerGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Tools;
    using IGK.ICore.Actions;
    using IGK.ICore.Drawing2D;
    using IGK.ICore.Resources;
    using IGK.ICore.Tools;
    using IGK.ICore.WinUI;
    using IGK.ICore.WinCore.WinUI.Controls;

    /// <summary>
    /// solution explorer
    /// </summary>
    public class GlobalSolutionExplorerGUI : IGKXToolConfigControlBase 
    {
        private IGKXToolStrip c_toolStrip;
        private IGKXPanel c_panel;
        private IGKXToolStripButton c_deleteElement;
        private IGKXTextBox c_textBox;
        private GlobalSolutionTreeView c_treeBox;
        private ICoreWorkingProjectSolution m_solutionExplorer;


       /// <summary>
        /// get or set the solution
        /// </summary>
        public ICoreWorkingProjectSolution Solution { 
            get {
            return this.m_solutionExplorer;
        }
            set {
                if (this.m_solutionExplorer != value )
                {
                    this.m_solutionExplorer = value;
                    this.InitViewItems();
                    OnSolutionExplorerChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler SolutionExplorerChanged;

        private void OnSolutionExplorerChanged(EventArgs eventArgs)
        {
            if (this.SolutionExplorerChanged != null)
                this.SolutionExplorerChanged(this, eventArgs);
        }
        new GlobalSolutionExplorerTool Tool {
            get {
                return base.Tool as GlobalSolutionExplorerTool;
            }
            set {
                base.Tool = value;
            }
        }
        public GlobalSolutionExplorerGUI()
        {
            //for testing
            this.InitializeComponent();
            this.Tool = GlobalSolutionExplorerTool.Instance;
            this.Load += _Load;
        }
        public GlobalSolutionExplorerGUI(GlobalSolutionExplorerTool tool):base(tool )
        {
            this.InitializeComponent();
            this.Load += _Load;
        }

        private void _Load(object sender, EventArgs e)
        {
            this.InitViewItems();
          //  this.c_treeBox.NodeMouseDoubleClick += c_treeBox_NodeMouseDoubleClick;
            this.c_treeBox.NodeMouseClick += c_treeBox_NodeMouseDoubleClick;
        }

        void c_treeBox_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                ICoreWorkingProjectSolutionItem v_b = e.Node.Tag as ICoreWorkingProjectSolutionItem;
                if ((v_b != null) && (Tool != null))
                {
                    Tool.Open(v_b);
                }
            }
        }


        private void InitViewItems()
        {
            if (this.IsHandleCreated == false)
                return;
            //build solutions list 
            this.c_treeBox.Nodes.Clear ();
            this.c_toolStrip.Items.Clear();
            if (this.Solution == null)
                return;

            GlobalSolutionTreeNode node = null;
            //load the list a solutions

            //var e = IGK.DrSStudio.Settings.SolutionExplorerSetting.Instance.Solutions;
   
            //foreach (IGK.DrSStudio.Settings.SolutionExplorerSetting.SolutionItem item in e)
            //{
            //    node = new GlobalSolutionTreeNode(new CoreWorkingFileSolution(item.Location));

            //    this.c_treeBox.Nodes.Add(node);   

            //}

            var v_solution = this.Solution;
            var acks =  v_solution.GetSolutionToolActions();
            if (acks != null)
            {
                foreach (ICoreAction ack in acks)
                {
                    IGKXToolStripButton btn = new IGKXToolStripButton()
                    {
                        Action = ack,
                        Name = ack.Id,
                        ImageKey = null,
                        ImageDocument = CoreResources.GetDocument(ack.ImageKey)
                    };
                    this.c_toolStrip.Items.Add(btn);
                }
            }
            node = new GlobalSolutionTreeNode(v_solution);
            node.ImageKey = v_solution.ImageKey;
            node.SelectedImageKey = v_solution.ImageKey;
            node.StateImageKey = v_solution.ImageKey;
            node.Text = v_solution.Name;
            node.Tag = v_solution;
            this.c_treeBox.Nodes.Add(node);            
            
             this.RegisterImage(this.Solution.ImageKey);
             
             foreach ( ICoreWorkingProjectSolutionItem item in this.Solution.Items)
             {
                 LoadItem(item, node);
             }

            // node.Expand();
             //this.c_treeBox.ExpandAll();
        }

        private void LoadItem(ICoreWorkingProjectSolutionItem item, TreeNode node)
        {
            TreeNode cnode = new TreeNode();
            cnode.Text = item.Name;
            cnode.Tag = item;
            cnode.Name = item.Name;
            cnode.ImageKey = item.ImageKey;
            cnode.SelectedImageKey = item.ImageKey;
            cnode.StateImageKey = item.ImageKey;
            this.RegisterImage(item.ImageKey);
            node.Nodes.Add(cnode);

            ICoreWorkingProjectSolutionItemContainer r = item as ICoreWorkingProjectSolutionItemContainer;
            if (r!=null)
            {
                foreach (ICoreWorkingProjectSolutionItem c in r)
                {
                    LoadItem(c, cnode);
                }
            }
        }

        private void RegisterImage(string imagekey)
        {
            this.c_treeBox.RegisterImageList(imagekey);
        }

        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalSolutionExplorerGUI));
            this.c_toolStrip = new IGKXToolStrip();
            this.c_deleteElement = new IGKXToolStripButton();
            this.c_panel = new IGKXPanel();
            this.c_treeBox = new GlobalSolutionTreeView();
            this.c_textBox = new IGKXTextBox();
            this.c_toolStrip.SuspendLayout();
            this.c_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // c_toolStrip
            // 
            this.c_toolStrip.BackgroundDocument = null;
            this.c_toolStrip.CaptionKey = null;
            this.c_toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.c_toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.c_deleteElement});
            this.c_toolStrip.Location = new System.Drawing.Point(0, 0);
            this.c_toolStrip.Name = "c_toolStrip";
            this.c_toolStrip.Size = new System.Drawing.Size(211, 25);
            this.c_toolStrip.TabIndex = 0;
            this.c_toolStrip.Text = "xToolStrip1";
            // 
            // toolStripButton1
            // 
            this.c_deleteElement.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.c_deleteElement.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.c_deleteElement.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.c_deleteElement.Name = "toolStripButton1";
            this.c_deleteElement.Size = new System.Drawing.Size(23, 22);
            this.c_deleteElement.Text = "toolStripButton1";
            // 
            // c_panel
            // 
            this.c_panel.CaptionKey = null;
            this.c_panel.Controls.Add(this.c_treeBox);
            this.c_panel.Controls.Add(this.c_textBox);
            this.c_panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_panel.Location = new System.Drawing.Point(0, 25);
            this.c_panel.Name = "c_panel";
            this.c_panel.Size = new System.Drawing.Size(211, 318);
            this.c_panel.TabIndex = 1;
            // 
            // igkxListBox1
            // 
            this.c_treeBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_treeBox.Location = new System.Drawing.Point(0, 20);
            this.c_treeBox.Name = "igkxListBox1";
            this.c_treeBox.Size = new System.Drawing.Size(211, 298);
            this.c_treeBox.TabIndex = 0;
            // 
            // igkxTextBox1
            // 
            this.c_textBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.c_textBox.Location = new System.Drawing.Point(0, 0);
            this.c_textBox.Name = "igkxTextBox1";
            this.c_textBox.Size = new System.Drawing.Size(211, 20);
            this.c_textBox.TabIndex = 1;
            // 
            // SolutionGlobalExplorerGUI
            // 
            this.Controls.Add(this.c_panel);
            this.Controls.Add(this.c_toolStrip);
            this.Name = "SolutionGlobalExplorerGUI";
            this.Size = new System.Drawing.Size(211, 343);
            this.c_toolStrip.ResumeLayout(false);
            this.c_toolStrip.PerformLayout();
            this.c_panel.ResumeLayout(false);
            this.c_panel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        
    }
}
