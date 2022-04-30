

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IconLibExplorerControl.cs
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
file:IconLibExplorerControl.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
/* This file is part of iGK-DrawingSoft.
*    iGK-DrawingSoft is free software: you can redistribute it and/or modify
*    it under the terms of the GNU Lesser General Public License as published by
*    the Free Software Foundation, either version 3 of the License, or
*    (at your option) any later version.
*    iGK-DrawingSoft is distributed in the hope that it will be useful,
*    but WITHOUT ANY WARRANTY; without even the implied warranty of
*    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*    GNU General Public License for more details.
*    You should have received a copy of the GNU Lesser General Public License
*    along with IGK-DRAWING SOFT.  If not, see <http://www.gnu.org/licenses/>.
*
*    Copyright (c) 2008-2009 
*    Author : C.A.D. BONDJE DOUE
*    mail : bondje.doue@hotmail.com
*/
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio
{
    using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.WinUI;
    partial class IconLibExplorerControl : IGKXUserControl
    {
        string filename;
        int type = RS_ICON ;
        const int RS_ICON = 0;
        public IconLibExplorerControl()
        {
            InitializeComponent();
        }
        Icon[] icoTab;
        /// <summary>
        /// open the file name
        /// </summary>
        /// <param name="filename"></param>
        public void OpenFile(string filename)
        {
            if (type == RS_ICON)
            {
                icoTab = Kernell.ExtractIcon(filename);
                this.filename = filename;
                this.listView1.Items.Clear();
                this.DisposeIcons();
                this.icoImageList.Images.Clear();
                System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(PopulateListView));
                th.IsBackground = false;
                th.Start();
            }           
        }
        private void DisposeIcons()
        {
            foreach (Image i in this.icoImageList.Images)
            {
                i.Dispose();
            }
        }
        delegate void PopulateInfo(ListViewItem[] items, Icon[] icons);
        private void PopulateListView()
        {
            if (icoTab== null) return;
            lock (this.listView1 )
            {
                int count = 0;
                ListViewItem item = null;
                List<ListViewItem> lst = new List<ListViewItem>();
                List<Icon> vico = new List<Icon>();
                foreach (Icon ico in icoTab)
                {
                    item = new ListViewItem("ico_" + count);
                    vico.Add(ico);
                    //item.ImageIndex = count;
                    item.ImageIndex = count;
                    lst.Add(item);
                    count++;
                    if ((lst.Count % 10) == 0)
                    {
                        this.listView1.BeginInvoke(new PopulateInfo(PopulateInvoke), new object[] { lst.ToArray(),vico.ToArray ()});
                        lst.Clear();
                        vico.Clear ();
                    }
                }
                if (lst.Count > 0)
                {
                    this.listView1.BeginInvoke(new PopulateInfo(PopulateInvoke), new object[] { lst.ToArray(),vico.ToArray ()});
                    lst.Clear();
                        vico.Clear ();
                }
            }
        }
        private void PopulateInvoke(ListViewItem[] items, Icon[] icons)
        {            
            this.listView1.BeginUpdate();
            this.listView1.Items.AddRange(items);
            for (int i = 0; i < icons.Length; i++)
            {
                this.icoImageList.Images.Add(icons[i]);
            }
            this.listView1.EndUpdate();
        }
        /// <summary>
        /// extract all icon resource to
        /// </summary>
        /// <param name="path">folder path</param>
        public void ExtratTo(string path)
        { 
            int i = 0;
            if (Kernell.ExtractIconsTo(filename, path, ref  i))
            {
                this.lb_job.Text = CoreSystem.GetString("xicon_extraction.ok");
            }
        }
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = this.FindForm();
            if (frm != null)
            {
                frm.DialogResult = DialogResult.None;
                frm.Close();
            }
            //if (this.Parent is Form)
            //{
            //    (this.Parent as Form).Close();
            //    this.Dispose();
            //}
            //else {
            //    this.Dispose();
            //}
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Icon And Library ( *.exe; *.dll;)|*.exe; *.dll;";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    OpenFile(dlg.FileName);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    this.ExtratTo(fbd.SelectedPath);
                }
            }
            if (this.Parent is Form)
            {
                (this.Parent as Form).DialogResult = DialogResult.None;
            }
        }
    }
}

