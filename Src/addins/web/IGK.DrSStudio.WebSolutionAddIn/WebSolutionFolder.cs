

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionFolder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Web
{
    class WebSolutionFolder : 
        WebSolutionItem ,
        ICoreWorkingProjectSolutionItemContainer
    {
        List<WebSolutionItem> m_items;
        private volatile bool loading;
        private string m_FullPath;

        /// <summary>
        /// get or set the fullpath
        /// </summary>
        public string FullPath
        {
            get { return m_FullPath; }
            set
            {
                if (m_FullPath != value)
                {
                    m_FullPath = value;
                }
            }
        }

        public override string ImageKey
        {
            get
            {
                return "img_folder";
            }
        }
        /// <summary>
        /// .ctr name of the folder
        /// </summary>
        /// <param name="name"></param>
        public WebSolutionFolder(string name):base()
        {

            this.Name = name;
            this.FullPath = string.Empty;
            this.m_items = new List<WebSolutionItem>();
        }
        protected override void OnSolutionChanged(EventArgs e)
        {
            if (this.Solution != null)
            {
                if (!this.loading)
                {
                    //Thread th = new Thread(LoadSolutionFolder);
                    //th.Start();

                    LoadSolutionFolder();
                }
            }
            base.OnSolutionChanged(e);
            
        }

        private void LoadSolutionFolder()
        {
          
            //test?oui:non
            string dir = string.IsNullOrEmpty (this.FullPath) ? Path.Combine(Solution.OutFolder, this.Name) : this.FullPath ;
            if (!Directory.Exists(dir))
                return;
            this.loading = true;
            this.m_items.Clear();

            foreach (string s in Directory.GetFiles(dir))
            {                 
                //load file
                WebSolutionFile sf = new WebSolutionFile(s);
                this.m_items.Add(sf);
                sf.Solution = this.Solution;
            }
            foreach (string s in Directory.GetDirectories (dir))
            {
                //load file

                WebSolutionFolder sf = new WebSolutionFolder(new DirectoryInfo(s).Name);
                sf.m_FullPath = s;
                this.m_items.Add(sf);
                sf.Solution = this.Solution;
            }
            this.m_items.Sort( new CompareItem());
            this.loading = false;
        }



        public int Count
        {
            get { return this.m_items.Count;  }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return this.m_items.GetEnumerator();
        }
    }
}
