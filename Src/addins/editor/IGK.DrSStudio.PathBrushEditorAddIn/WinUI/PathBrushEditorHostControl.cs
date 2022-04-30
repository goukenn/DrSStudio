

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: PathBrushEditorHostControl.cs
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
file:PathBrushEditorHostControl.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace IGK.DrSStudio.PathBrushEditorAddIn.WinUI
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI ;
    public sealed class PathBrushEditorHostControl : IGK.DrSStudio.WinUI.UIXToolConfigControlBase 
    {
        private PathBrushItem[] m_items;
        private XToolStrip c_toolStrip;
        private XPanel c_panel;
        private XToolStripButton c_editstyle;
        private XToolStripButton c_removeStyle;
        private IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle m_PathBrushStyle;
        public bool EditStyle
        {
            get { return this.c_editstyle.Enabled; }
            set
            {
                this.c_editstyle.Enabled = value;
            }
        }
        public bool RemoveStyle
        {
            get { return this.c_removeStyle .Enabled; }
            set
            {
                this.c_removeStyle.Enabled = value;
            }
        }
        public event EventHandler EditStyleClick { 
            add{
                this.c_editstyle.Click += value; 
            }
            remove {
                this.c_editstyle.Click -= value;
            }
        }
        public event EventHandler RemoveStyleClick
        {
            add
            {
                this.c_removeStyle .Click += value;
            }
            remove
            {
                this.c_removeStyle.Click -= value;
            }
        }
        public IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle PathBrushStyle
        {
            get { return m_PathBrushStyle; }
            set
            {
                    m_PathBrushStyle = value;
                    OnPathBrushStyleChanged(EventArgs.Empty);                
            }
        }
        public event EventHandler PathBrushStyleChanged;
        private void OnPathBrushStyleChanged(EventArgs eventArgs)
        {
            if (PathBrushStyleChanged != null)
                PathBrushStyleChanged(this, eventArgs);
        }
        public PathBrushEditorHostControl():base(
            Tools.ToolEditBrushStyle.Instance )
        {
            InitializeComponent();
            this.Load += new EventHandler(_Load);
            this.SizeChanged += new EventHandler(PathBrushEditorHostControl_SizeChanged);
        }
        void PathBrushEditorHostControl_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateItemsBound();
        }
        void _Load(object sender, EventArgs e)
        {
            //load system brushed
            c_editstyle.ImageDocument = IGK.DrSStudio.CoreResources.GetDocument("btn_edit");
            c_removeStyle.ImageDocument = IGK.DrSStudio.CoreResources.GetDocument("btn_remove");
            this.c_toolStrip.Items.Add(c_editstyle);
            this.c_toolStrip.Items.Add(c_removeStyle);
            PathBrushItem item = null;
            IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle[] v_t = Utils.LoadItems();
            m_items = new PathBrushItem[v_t.Length];
            int i = 0;
            foreach (IGK.DrSStudio.Drawing2D.ICore2DPathBrushStyle style in v_t )
            {
                item = new PathBrushItem(style);
                item.Click += new EventHandler(item_Click);
                this.c_panel.Controls.Add(item);
                m_items[i] = item;
                i++;
            }
            UpdateItemsBound();
        }
        void item_Click(object sender, EventArgs e)
        {
            this.PathBrushStyle = (sender as PathBrushItem).Style;
        }
        private void UpdateItemsBound()
        {
            if (this.m_items == null)
                return;
            int x =4;
            int y =4;
            this.SuspendLayout();
            foreach (Control item in this.m_items)
            {
                item.Bounds = new Rectangle(x, y, this.Width-8, PathBrushConstant.ITEM_HEIGHT);
                y += PathBrushConstant.ITEM_HEIGHT + PathBrushConstant.ITEM_SPACE;
            }
            this.ResumeLayout();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            c_editstyle = new XToolStripButton();
            c_removeStyle = new XToolStripButton();
            c_panel = new XPanel();
            c_panel.Dock = DockStyle.Fill;
            this.Controls.Add(c_panel);
            c_toolStrip = new XToolStrip();
            c_toolStrip.Dock = DockStyle.Top;
            this.Controls.Add(c_toolStrip);
            // 
            // PathBrushEditorHostControl
            // 
            this.Name = "PathBrushEditorHostControl";
            this.Size = new System.Drawing.Size(262, 272);
            this.ResumeLayout(false);
        }
    }
}

