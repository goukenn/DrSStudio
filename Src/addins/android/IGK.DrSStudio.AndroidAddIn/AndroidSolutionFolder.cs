

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionFolder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    public class AndroidSolutionFolder : AndroidSolutionItem, IEnumerable, ICoreWorkingProjectSolutionItemContainer
    {
        private string m_fullPath;
        private List<AndroidSolutionItem> m_items;
        public override string ImageKey
        {
            get { return AndroidConstant.ANDROID_IMG_FOLDER; }
        }
        public override string ToString()
        {
            return this.Name;
        }
        public override void Open(ICoreSystemWorkbench bench)
        {
            if (Directory.Exists(m_fullPath))
            {
                Process.Start(this.m_fullPath);
            }
        }
        public AndroidSolutionFolder(AndroidProject project, string dir)
        {
            this.m_items = new List<AndroidSolutionItem>();
            this.Name = Path.GetFileName(dir);
            this.m_fullPath = Path.GetFullPath(dir);
            this.Project  = project;
        }
        public int Count { get { return this.m_items.Count; } }
        public AndroidSolutionItem[] ToArray() { return this.m_items.ToArray();  }
        internal void Add(AndroidSolutionFile c)
        {
            if ((c == null)||(this.m_items.Contains(c)))
                return;
            this.m_items.Add(c);
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_items.GetEnumerator();
        }
    }
}
