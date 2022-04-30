

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WiXProjectFile.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
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
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:WiXProjectFile.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WiXAddIn
{
    /// <summary>
    /// represent a project filename
    /// </summary>
    public class WiXProjectFile : IEnumerable 
    {
        string m_filename;
        private string m_Id;
        private bool m_isFile;
        private WiXProjectFile m_Parent;
        public string Description { get; set; }
        public string Target { get; set; }
        public string WorkingDir { get; set; }
        /// <summary>
        /// get or set the parent
        /// </summary>
        public WiXProjectFile Parent
        {
            get { return m_Parent; }
        }
        private enuWiXFileType m_FileType;
        public enuWiXFileType FileType
        {
            get { return m_FileType; }
            set
            {
                if (m_FileType != value)
                {
                    m_FileType = value;
                }
            }
        }
        List<WiXProjectFile> m_childs;
        public override string ToString()
        {
            return "ProjectFile:["+this.Id+"]";
        }
        public string FileName {
            get { return this.m_filename; }
            internal set { this.m_filename = value; }
        }
        public string Id
        {
            get { return m_Id; }
            set
            {
                if (m_Id != value)
                {
                    m_Id = value;
                }
            }
        }
        public bool IsFile
        {
            get { return m_isFile; }
        }
        public bool IsDirectory
        {
            get { return !IsFile; }
        }
        public WiXProjectFile(string filename)
        {
            this.m_filename = filename;
            this.m_Id = Path.GetFileName(filename);
            this.m_isFile = File.Exists(this.m_filename);
            this.m_childs = new List<WiXProjectFile>();
            this.m_FileType = this.IsFile ? enuWiXFileType.File : enuWiXFileType.Directory ;
        }
        private WiXProjectFile()
        {
        }
        public static WiXProjectFile CreateShortcut(string Name, string Target, string description, string workingDir )
        {
            WiXProjectFile f = new WiXProjectFile();
            f.m_Id = "shortcut_" + f.GetHashCode();
            f.m_FileType = enuWiXFileType.Shortcut;
            f.m_filename = Name;
            f.Target = Target;
            f.Description = description;
            f.WorkingDir = workingDir;
            return f;
        }
        /// <summary>
        /// get childs name
        /// </summary>
        /// <returns></returns>
        public string[] GetChildNames()
        {
            List<string> ts = new List<string>();
            foreach (var i in this.m_childs)
            {
                if (!ts.Contains(i.Id))
                {
                    ts.Add(i.Id);
                }
            }
            return ts.ToArray();
 
        }
        public void AddChild(params WiXProjectFile[] files)
        {
            if (files != null)
            {
                this.m_childs.AddRange(files);
                for (int i = 0; i < files.Length; i++)
                {
                    files[i].m_Parent = this;
                }
            }
        }
        public int ChildCount { get { 
            if (this.m_childs !=null)
                return this.m_childs.Count;
            return 0;

        } }
        public IEnumerator GetEnumerator()
        {
            if (this.m_childs!=null)
            return this.m_childs.GetEnumerator ();
            return null;
        }
        internal void Remove(WiXProjectFile c)
        {
            if (c != null)
            {
                this.m_childs.Remove(c);
            }
        }
        /// <summary>
        /// Get if this contains
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public bool Contains(string name)
        {
            foreach (WiXProjectFile  item in this.m_childs )
            {

                if (item.Id == name)
                    return true;
            }
            return false;
        }
    }
}

