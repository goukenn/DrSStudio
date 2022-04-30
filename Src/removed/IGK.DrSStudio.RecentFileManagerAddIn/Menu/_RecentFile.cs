

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RecentFile.cs
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
file:_RecentFile.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Menu
{
    using IGK.ICore;using IGK.DrSStudio.WinUI;    
    using IGK.DrSStudio.Tools;
    using IGK.DrSStudio.Resources;
    [CoreMenu ("File.RecentFiles", 500, SeparatorBefore = true )]
    class _RecentFile : CoreApplicationMenu 
    {
        const string Menu_File = "Menu_File";
        public _RecentFile()
        {         
        }
        protected override void InitMenu()
        {
            base.InitMenu();
            this.LoadRecentList();
        }
        private void LoadRecentList()
        {
            this.Childs.Clear();
            int v_count = 0;
            RecentFileMenuItem m = null;
            foreach (string f in ToolRecentFile.Instance.GetRecentFiles())
            {

                m = new RecentFileMenuItem(f);
                this.Childs.Add(m);
                m.MenuItem.Text = f;


                //m = new System.Windows.Forms.ToolStripMenuItem();
                //m.Text = string.Format("{0} : {1}", v_count, f);
                //m.Tag = f;
                //m.Image = CoreResources.GetDocumentImage(Menu_File);
                //m.Click += new EventHandler(m_Click);
                //this.MenuItem.DropDownItems.Add(m);
                v_count++;
                if (v_count > 10)
                    break;
            }
        }
        void m_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.ToolStripMenuItem i = sender as System.Windows.Forms.ToolStripMenuItem;
            this.Workbench.OpenFile (new string[]{i.Tag.ToString()});
        }


        internal abstract class RecentFileMenuBase : CoreApplicationMenu
        {
            protected void SetWorkbench(ICoreWorkbench workbench)
            {
                this.Workbench = workbench;
            }
        }
        internal class RecentFileMenuItem : RecentFileMenuBase
        {
            private string s;
            public RecentFileMenuItem(string s)
            {
                // TODO: Complete member initialization
                this.s = s;
            }
            protected override bool PerformAction()
            {
                this.Workbench.OpenFile(this.s);
                return base.PerformAction();
            }
            protected override void InitMenu()
            {
                base.InitMenu();
            }
        }
    }
}

