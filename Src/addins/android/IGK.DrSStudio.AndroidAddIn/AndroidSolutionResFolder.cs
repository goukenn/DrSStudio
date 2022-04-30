

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionResFolder.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Codec;
using IGK.ICore.WinUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent android resources folder
    /// </summary>
    public sealed class AndroidSolutionResFolder : AndroidSolutionItem, IEnumerable, ICoreWorkingProjectSolutionItemContainer
    {
        List<AndroidSolutionFolder> m_folders;
        public AndroidSolutionResFolder(AndroidProject androidProject):base()
        {
            this.m_folders = new List<AndroidSolutionFolder>();
            this.Project = androidProject;
            this.Name = "res";
        }
        public string FullPath {
            get {
                return Path.Combine(this.Project.TargetLocation, "res");
            }
        }
        public override string ImageKey
        {
            get {
                return AndroidConstant.ANDROID_IMG_RESFOLDER;
            }
        }
        public override string Name
        {
            get
            {
                return base.Name;
            }
            set
            {
                base.Name = value;
            }
        }
        protected override string getName()
        {
            return "res";
        }
        public override void Serialize(IXMLSerializer xwriter)
        {
            base.Serialize(xwriter);
        }

        internal void Add(AndroidSolutionFolder folder)
        {
            if ((folder == null) || this.m_folders.Contains(folder))
                return;
            this.m_folders.Add(folder);
        }
        internal void Remove(AndroidSolutionFolder folder)
        {
            this.m_folders.Remove(folder);
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_folders.GetEnumerator();
        }

        public int Count
        {
            get { return this.m_folders.Count; }
        }
    }
}
