

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidTargetManagerTool.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using IGK.ICore.WinCore;
using IGK.ICore;using IGK.DrSStudio.Android.JAVA;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using IGK.ICore.Tools;
using IGK.ICore.IO;

namespace IGK.DrSStudio.Android.Tools
{
    [CoreTools("Tool.AndroidTargetManager")]
    public sealed class AndroidTargetManagerTool : AndroidToolBase 
    {
        private object sync_obj = new object();
        private static AndroidTargetManagerTool sm_instance;
        private AndroidTargetInfo m_TargetInfo;
        private AndroidTargetManagerToolResourcesCollection m_ResourceInfos;
        private AndroidTargetManagerDirectoryCollection m_Directories;

        public AndroidTargetManagerToolResourcesCollection ResourceInfos
        {
            get { return m_ResourceInfos; }
        }

        public AndroidTargetManagerDirectoryCollection Directories {
            get {
                return this.m_Directories;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class AndroidTargetManagerToolResourcesCollection : IEnumerable
        {
            Dictionary<string, AndroidTargetManagerResourceItem> m_items;
            private AndroidTargetManagerTool m_owner;
            

            public AndroidTargetManagerToolResourcesCollection(AndroidTargetManagerTool owner)
            {
                this.m_items = new Dictionary<string, AndroidTargetManagerResourceItem>();
                this.m_owner = owner;
            }
            public void Clear()
            {
                this.m_items.Clear();
            }
            /// <summary>
            /// get enumarator
            /// </summary>
            /// <returns></returns>
            public IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
            /// <summary>
            /// add item to the list
            /// </summary>
            /// <param name="name"></param>
            /// <param name="p"></param>
            internal void Add(string name, AndroidTargetManagerResourceItem p)
            {
                if(string.IsNullOrEmpty (name) || m_items.ContainsKey(name ) || (p == null))
                    return;
                this.m_items.Add(name, p);
            }
            public AndroidTargetManagerResourceItem this[string key] {
                get {
                    if (this.m_items.ContainsKey (key))
                        return this.m_items[key];
                    return null;
                }
            }
        }
        /// <summary>
        /// represent android directory manager collections
        /// </summary>
        public class AndroidTargetManagerDirectoryCollection : IEnumerable
        {
            Dictionary<string, AndroidTargetManagerResourceDirectory>
                m_dictionary = new Dictionary<string, AndroidTargetManagerResourceDirectory>();
            private AndroidTargetManagerTool m_tool;

            public AndroidTargetManagerDirectoryCollection(AndroidTargetManagerTool tool)
            {                
                this.m_tool = tool;
                this.m_dictionary = new Dictionary<string, AndroidTargetManagerResourceDirectory>();
            }

            public AndroidTargetManagerResourceDirectory this[string key] {
                get {
                    if (this.m_dictionary .ContainsKey (key))
                        return this.m_dictionary[key];
                    return null;
                }
            }
            public IEnumerator GetEnumerator()
            {
                return m_dictionary.GetEnumerator();
            }
            internal void Clear() {
                this.m_dictionary.Clear();
            }
            public void Add(string dir, AndroidTargetManagerResourceDirectory dObject)
            {
                if (string.IsNullOrEmpty(dir) || this.m_dictionary.ContainsKey (dir) || (dObject == null))
                    return ;
                this.m_dictionary.Add(dir, dObject);
            }
            public void Remove(string dir)
            {
                if (this.m_dictionary.ContainsKey (dir))
                    this.m_dictionary.Remove(dir);
            }
            public string[] Keys { get { return this.m_dictionary.Keys.ToArray(); } }

            /*
             * get if this dictionary list contains resources
             * */
            public  bool Contains(string name)
            {
                return this.m_dictionary.ContainsKey(name);
            }
        }
        public AndroidTargetInfo TargetInfo
        {
            get { return m_TargetInfo; }
            set
            {
                if (m_TargetInfo != value)
                {
                    m_TargetInfo = value;
                    LoadRourcesInfo();
                    this.OnTargetInfoChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler TargetInfoChanged;
        private void OnTargetInfoChanged(EventArgs eventArgs)
        {
            if (this.TargetInfoChanged != null)
                this.TargetInfoChanged(this, EventArgs.Empty);
        }

        private void LoadRourcesInfo()
        {
            this.ResourceInfos.Clear();
            GC.Collect();

            AndroidTargetInfo target = this.TargetInfo;
            if (target == null)
                return;
            string v_dir = Path.GetFullPath(Path.Combine(SDK, "platforms", target.GetAPIName() + "/data/res"));
            string v_regex = ".(xml|j(e{0,1})pg|png)$";
            string v_n = string.Empty;
            string v_dirname = string.Empty;
            if (Directory.Exists(v_dir))
            {
                int l = v_dir.Length;
                foreach (string f in Directory.GetFiles(v_dir, "*.*", SearchOption.AllDirectories))
                {

                    if (Regex.IsMatch(f, v_regex))
                    {
                        System.Diagnostics.Debug.WriteLine("Loading ... " + f);
                        v_dirname = PathUtils.GetDirectoryName(f).Substring(l + 1);
                        v_n = v_dirname + "." + Path.GetFileNameWithoutExtension(f);

                        AndroidTargetManagerResourceItem r = null;

                        switch (Path.GetExtension(f).ToLower())
                        {
                            case ".xml":
                                r = AndroidTargetManagerResourceItem.LoadXMLResources(f);
                                this.m_ResourceInfos.Add(v_n, r);
                                break;
                            case ".png":
                            case ".jpg":
                                r = AndroidTargetManagerResourceItem.LoadImageFile(f);
                                this.m_ResourceInfos.Add(v_n, r);
                                break;
                            default:
                                break;
                        }
                        if (r != null)
                        {
                            if (this.Directories.Contains(v_dirname))
                            {
                                this.Directories[v_dirname].Register(Path.GetFileNameWithoutExtension(f), r);
                            }
                            else
                            {
                                AndroidTargetManagerResourceDirectory rs = new AndroidTargetManagerResourceDirectory(v_dirname);
                                rs.Register(Path.GetFileNameWithoutExtension(f), r);
                                this.Directories.Add(rs.Name, rs);
                            }

                        }
                    }
                    else {
                        CoreLog.WriteDebug("File Not Loaded : " + f );
                    }
                }
            }

        }
        public static string[] GetInfo(AndroidTargetInfo target, string file)
        {
             if (target == null)
                return null;
            string v_dir = Path.GetFullPath(Path.Combine(SDK, "platforms", target.GetAPIName()+"/data"));
            string v_file = Path.Combine (v_dir , file);
            if (File.Exists (v_file )){
            return File.ReadAllText(v_file).Split (
                new string[]{"\n"},StringSplitOptions.RemoveEmptyEntries  );
            }
            return null;
        }
        public string[] GetAndroidActions()
        {
            if (this.TargetInfo == null)
                return null;
            if (this.TargetInfo == null)
                return null;
            return GetInfo(this.TargetInfo, AndroidConstant.ANDROID_ACTIVITY_ACTIONS_FILE );
        }
        public string[] GetAndroidCategories()
        {
            if (this.TargetInfo == null)
                return null;
            return GetInfo(this.TargetInfo, AndroidConstant.ANDROID_CATEGORIES_FILE);
        }
        public JAVAClass[] GetAndroidWidgets()
        {
            if (this.TargetInfo == null)
                return null;
            string[] t = GetInfo(this.TargetInfo, AndroidConstant.ANDROID_WIDGETS_FILE);
            if (t != null)
            {
                List<JAVAClass> rList = new List<JAVAClass>();
                foreach (string i in t)
                {
                    string[] e = i.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (e.Length > 0)
                    {
                        string n = e[0];
                        if (char.IsUpper(n[0]))
                        {
                            n = n.Substring(1);
                        }
                        JAVAClass cl = JAVAClass.GetClass(n);
                        rList.Add(cl);
                        for (int f = 1; f < e.Length; f++)
                        {
                            cl.Parent = JAVAClass.GetClass(e[f]);
                            cl = cl.Parent;
                        }
                    }
                }
                return rList.ToArray();
            }
            return null;
        }
        public string[] GetAndroidServices()
        {
            string[] t = GetInfo(this.TargetInfo, AndroidConstant.ANDROID_SERVICE_ACTIONS_FILE);
            return t;
        }
        public string[] GetAndroidFeatures()
        {
            string[] t = GetInfo(this.TargetInfo, AndroidConstant.ANDROID_FEATURES_FILE);
            return t;
        }
        private AndroidTargetManagerTool()
        {
            this.m_ResourceInfos = new AndroidTargetManagerToolResourcesCollection(this);
            this.m_Directories = new AndroidTargetManagerDirectoryCollection(this);
        }

        public static AndroidTargetManagerTool Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static AndroidTargetManagerTool()
        {
            sm_instance = new AndroidTargetManagerTool();
        }

        /*
         * load attribute from string data
         * exemple:
         * key= values.attrs to get the attrs definitions
         * 
         * 
         * */
        /// <summary>
        /// load atrribute
        /// </summary>
        /// <param name="key"></param>
        /// <param name="m_attributeDefinitions"></param>
        public void LoadAttributes(string key, Dictionary<string, AndroidAttributeDefinition> m_attributeDefinitions)
        {            
                //AndroidTargetManagerResourceDirectory dir =  AndroidTargetManagerTool.Instance.Directories["values"];
                AndroidTargetManagerResourceItem i = AndroidTargetManagerTool.Instance.ResourceInfos[key];
                
                //load filename
                if (i != null)
                {
                    System.Xml.XmlReader v_rXml = System.Xml.XmlReader.Create(i.FileName);
                    while (v_rXml.Read())
                    {
                        switch (v_rXml.NodeType)
                        {
                            case System.Xml.XmlNodeType.Element:
                                CoreLog.WriteLine("Loading definition .... " + v_rXml.Name);
                                switch (v_rXml.Name)
                                {
                                    case "declare-styleable":
                                    case "attr":
                                        //load decreate stylable
                                        AndroidAttributeDefinition v_def = new AndroidAttributeDefinition();
                                        var v = v_rXml.ReadSubtree();
                                        v.MoveToContent();
                                        v_def.Load(v);
                                        m_attributeDefinitions.Add(v_def.Name, v_def);
                                        break;
                                    case "resources":
                                        //good 
                                        break;
                                    case "eat-comment":
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    v_rXml.Close();
                    foreach (var item in m_attributeDefinitions.Values)
                    {
                        item.UpdateInfo(m_attributeDefinitions);
                    }
                }
        }
    }
}
