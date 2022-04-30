

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _RecentProject.cs
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
file:_RecentProject.cs
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
    using IGK.ICore;using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Tools;
    [CoreMenu("File.RecentProjects", 501)]
    class _RecentProject : CoreApplicationMenu
    {
        public _RecentProject()
        {
            int v_count = 0;
            foreach (string f in ToolRecentFile.Instance.GetRecentFiles())
            {
                CoreMenuAttribute v_attr = new CoreMenuAttribute(string.Format(this.Id + ".FILE{0}", v_count), v_count);
                v_attr.ImageKey = "Menu_RFile";
                _ProjectMenu fmenu = new _ProjectMenu();
                fmenu.FileName = f;
                fmenu.SetAttribute(v_attr);
                this.Childs.Add(fmenu);
                v_count++;
                if (v_count > 10)
                    break;
            }
        }
        class _ProjectMenu : CoreApplicationMenu
        {
            private string m_FileName;
            /// <summary>
            /// get or set the project file name
            /// </summary>
            public string FileName
            {
                get { return m_FileName; }
                set
                {
                    if (m_FileName != value)
                    {
                        m_FileName = value;
                    }
                }
            }
            protected override bool PerformAction()
            {
                Workbench.OpenProject(this.FileName);
                return false;
            }
        }
    }
}

