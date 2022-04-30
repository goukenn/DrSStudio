

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidDeviceManagerGUI.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿

using IGK.DrSStudio.Android.Tools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGK.DrSStudio.Android.WinUI
{
    using IGK.ICore;
    using IGK.ICore.WinCore.WinUI.Controls;
    using IGK.ICore.WinUI;

    public sealed  class AndroidDeviceManagerGUI : IGKXToolConfigControlBase 
    {
        public new AndroidDeviceManagerTool Tool { get { return base.Tool as AndroidDeviceManagerTool; } }
        private System.Windows.Forms.ListView c_items_listview;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private IGKXToolStrip c_toolStrip_options;
    
        public AndroidDeviceManagerGUI()
            : base(AndroidDeviceManagerTool.Instance)
        {
            this.InitializeComponent();
        }
        public AndroidDeviceManagerGUI(AndroidDeviceManagerTool tool):base(tool)
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.c_items_listview = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.c_toolStrip_options = new IGKXToolStrip();
            this.SuspendLayout();
            // 
            // c_items_listview
            // 
            this.c_items_listview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.c_items_listview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.c_items_listview.FullRowSelect = true;
            this.c_items_listview.GridLines = true;
            this.c_items_listview.Location = new System.Drawing.Point(0, 25);
            this.c_items_listview.Name = "c_items_listview";
            this.c_items_listview.Size = new System.Drawing.Size(560, 249);
            this.c_items_listview.TabIndex = 0;
            this.c_items_listview.UseCompatibleStateImageBehavior = false;
            this.c_items_listview.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 190;
            // 
            // c_toolStrip_options
            // 
            this.c_toolStrip_options.BackgroundDocument = null;
            this.c_toolStrip_options.CaptionKey = null;
            this.c_toolStrip_options.Location = new System.Drawing.Point(0, 0);
            this.c_toolStrip_options.Name = "c_toolStrip_options";
            this.c_toolStrip_options.Size = new System.Drawing.Size(560, 25);
            this.c_toolStrip_options.TabIndex = 1;
            this.c_toolStrip_options.Text = "igkxToolStrip1";
            // 
            // AndroidDeviceManagerGUI
            // 
            this.Controls.Add(this.c_items_listview);
            this.Controls.Add(this.c_toolStrip_options);
            this.Name = "AndroidDeviceManagerGUI";
            this.Size = new System.Drawing.Size(560, 274);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.LoadData();
        }
        Thread m_th;
        private void LoadData()
        {
            if (m_th != null)
            {
                m_th.Abort();
                m_th.Join();
            }
            m_th = new Thread(_loadData);
            m_th.IsBackground = true;
            m_th.SetApartmentState(ApartmentState.STA);
            m_th.Start();
            this.Cursor = Cursors.WaitCursor;
        }

        private void _loadData()
        {
            object[] obj = this.Tool.GetInstalledApps();

            List<System.Windows.Forms.ListViewItem> i = new List<System.Windows.Forms.ListViewItem>();
            foreach (var item in obj)
            {
                if (item == null) continue;
                i.Add(new System.Windows.Forms.ListViewItem(item.ToString()) {
                    Tag = item
                });
            }
            this.Invoke(new CoreMethodInvoker(delegate() {
                this.c_items_listview.Sorting = System.Windows.Forms.SortOrder.Ascending;
                this.c_items_listview.Items.Clear();
                this.c_items_listview.Items.AddRange(i.ToArray());
                this.c_items_listview.Sort();
                this.Cursor = Cursors.Default;
            }));


        }
    }
}
