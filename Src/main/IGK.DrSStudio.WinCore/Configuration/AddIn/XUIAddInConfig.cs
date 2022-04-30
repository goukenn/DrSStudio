

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: XUIAddInConfig.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:XUIAddInConfig.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.WinUI
{
    sealed class XUIAddInConfig : XAcceptOrCancelControl 
    {
        private ColumnHeader c_name;
        private ColumnHeader c_path;
        private ColumnHeader c_version;
        private ColumnHeader c_author;
        private ListView c_listview;
        public XUIAddInConfig()
        {
            this.InitializeComponent();
        }
        public override void LoadDisplayText()
        {
            base.LoadDisplayText();
            c_author.Text = "lb.author.caption".R();
            c_name.Text = "lb.name.caption".R();
            c_path .Text = "lb.path.caption".R();
            c_version .Text = "lb.version.caption".R();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadDisplayText();
            this.LoadVisibleAdding();
        }
        private void LoadVisibleAdding()
        {
            ListViewItem item = null;
            CoreAddInAttribute v_attr = null;
            foreach (ICoreAddIn  e in CoreSystem.GetAddIns())
            {
                v_attr = Attribute.GetCustomAttribute(e.Assembly, typeof(CoreAddInAttribute))
                    as CoreAddInAttribute;
                if (v_attr == null)
                    continue;
                item = new ListViewItem (e.Assembly.GetName ().Name );
                item.SubItems.Add(e.Location);
                item.SubItems.Add(v_attr.Version);
                item.SubItems.Add(v_attr.AuthorName );
                item.SubItems.Add(v_attr.Description );
                this.c_listview.Items.Add(item);
            }
        }
        private void InitializeComponent()
        {
            this.c_listview = new System.Windows.Forms.ListView();
            this.c_name = new System.Windows.Forms.ColumnHeader();
            this.c_path = new System.Windows.Forms.ColumnHeader();
            this.c_version = new System.Windows.Forms.ColumnHeader();
            this.c_author = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // c_listview
            // 
            this.c_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.c_name,
            this.c_path,
            this.c_version,
            this.c_author});
            this.c_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_listview.FullRowSelect = true;
            this.c_listview.GridLines = true;
            this.c_listview.HideSelection = false;
            this.c_listview.Location = new System.Drawing.Point(0, 0);
            this.c_listview.Name = "c_listview";
            this.c_listview.Size = new System.Drawing.Size(408, 350);
            this.c_listview.TabIndex = 0;
            this.c_listview.UseCompatibleStateImageBehavior = false;
            this.c_listview.View = System.Windows.Forms.View.Details;
            this.c_listview.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.c_listview_ColumnClick);
            this.c_listview.DrawColumnHeader += new DrawListViewColumnHeaderEventHandler(c_listview_DrawColumnHeader);
            // 
            // c_name
            // 
            this.c_name.Width = 122;
            // 
            // c_path
            // 
            this.c_path.Width = 154;
            // 
            // c_version
            // 
            this.c_version.Width = 77;
            // 
            // XUIAddInConfig
            // 
            this.Controls.Add(this.c_listview);
            this.Name = "XUIAddInConfig";
            this.Size = new System.Drawing.Size(408, 350);
            this.ResumeLayout(false);
        }
        void c_listview_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.Graphics.Clear(System.Drawing.Color.CornflowerBlue);
        }
        private void c_listview_ColumnClick(object sender, ColumnClickEventArgs e)
        {
        }
    }
}

