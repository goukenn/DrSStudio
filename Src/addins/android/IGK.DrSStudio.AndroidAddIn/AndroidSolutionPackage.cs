

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidSolutionPackage.cs
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
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android
{
    class AndroidSolutionPackage : AndroidSolutionItem, ICoreWorkingProjectSolutionItemContainer
    {
        private AndroidProject androidProject;
        private AndroidSolutionPackageFileCollection m_files;

        public class AndroidSolutionPackageFileCollection : IEnumerable 
        {
            private AndroidSolutionPackage androidSolutionPackage;
            private List<AndroidSolutionJScriptFile> m_files;

            public AndroidSolutionPackageFileCollection(AndroidSolutionPackage androidSolutionPackage)
            {
                this.androidSolutionPackage = androidSolutionPackage;
                this.m_files = new List<AndroidSolutionJScriptFile>();
            }


            public IEnumerator GetEnumerator()
            {
                return this.m_files.GetEnumerator();
            }
            public int Count { get { return this.m_files.Count; } }
            public void Add(AndroidSolutionJScriptFile file) {
                this.m_files.Add(file);
                file.Project = this.androidSolutionPackage.Project;
            }
            public void Remove(AndroidSolutionJScriptFile file)
            {
                if (this.m_files.Contains(file))
                {
                    this.m_files.Remove(file);
                    file.Project = null;
                }
            }
            public void Clear() { this.m_files.Clear(); }

            public AndroidSolutionJScriptFile[] ToArray() {
                return this.m_files.ToArray();
            }
        }
        public AndroidSolutionPackageFileCollection Files {
            get {
                return this.m_files;
            }
        }

        public AndroidSolutionPackage(AndroidProject androidProject)
        {
            this.androidProject = androidProject;
            this.Name = androidProject.PackageName;
            this.m_files = new AndroidSolutionPackageFileCollection(this);
            
        }
        [CoreXMLAttribute()]
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
        


        public override string ImageKey
        {

            get { return AndroidConstant.ANDROID_IMG_PACKAGEFOLDER; }
        }
        public override void Serialize(IXMLSerializer xwriter)
        {
            xwriter.WriteStartElement(this.getName());
            foreach (AndroidSolutionJScriptFile item in this.m_files)
            {
                item.Serialize(xwriter);
            }
            xwriter.WriteEndElement();
        }
        public override void Deserialize(IXMLDeserializer xreader)
        {
            base.Deserialize(xreader);
        }

        public int Count
        {
            get { return this.m_files.Count; }
        }

        public IEnumerator GetEnumerator()
        {
            return this.m_files.GetEnumerator();
        }
    }
}
