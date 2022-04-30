using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Balafon.Xml
{
    public class BalafonProject : BalafonCoreXmlElement,
        IBalafonProject
    {
        private BalafonProjectFolderGroup m_FolderGroup;
        private Dictionary<enuBalafonItemType, BalafonProjectItemGroup> m_groupModes;
        private BalafonProjecPropertyGroup m_systemPropertyGroup;

        public BalafonProjecPropertyGroup SystemPropertyGroup {
            get {
                return this.m_systemPropertyGroup;
            }
        }
        public BalafonProjectFolderGroup FolderGroup
        {
            get {
                if (m_FolderGroup == null)
                    m_FolderGroup = CreateFolderCollections();
                return m_FolderGroup; 
            }
        }
        public BalafonProject():base(BalafonConstant.PROJECT_TAG)
        {
            this.m_groupModes = new Dictionary<enuBalafonItemType, BalafonProjectItemGroup>();
            this.m_systemPropertyGroup =
                new BalafonProjecPropertyGroup(this);
            this.Childs.Add(this.m_systemPropertyGroup);
        }
        private string m_Name;

        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                    OnNameChanged(EventArgs.Empty);
                }
            }
        }
        public event EventHandler NameChanged;

        protected virtual void OnNameChanged(EventArgs e)
        {
            if (NameChanged != null)
            {
                NameChanged(this, e);
            }
        }


        public string Prefix { get { return this["Prefix"]; } set { this["Prefix"] = value; } }
        public string AppTitle { get { return this["AppTitle"]; } set { this["AppTitle"] = value; } }

        /// <summary>
        /// load object as Balafong project
        /// </summary>
        /// <param name="item"></param>
        public void Load(CoreXmlElement item)
        {
            //load propject
            if (item == null)
                return;
            //Load Properties group
            bool main = false;
            this.ClearPropertyGroup();
            this.FolderGroup.Clear();
            foreach (CoreXmlElement itemgroup in item.getElementsByTagName(BalafonConstant.PROJECT_PROPERTY_GROUP_TAG))
            {
                if (!main && (!itemgroup.HasAttributes))
                {
                    this.m_systemPropertyGroup.LoadString(itemgroup.RenderInnerHTML(null));
                    this.AddChild(this.m_systemPropertyGroup);
                    main = true;
                }
                else
                {
                    var v_pg = this.AddPropertyGroup();
                    v_pg.LoadAttributes(itemgroup);
                    v_pg.LoadString(itemgroup.RenderInnerHTML(null));
                }
            }

            //load files 
            var v_Type = typeof(enuBalafonItemType);
            foreach (var itemgroup in item.getElementsByTagName(BalafonConstant.PROJECT_ITEM_GROUP_TAG))
            {
                //load child 
                foreach (CoreXmlElement file in itemgroup.Childs)
                {
                    if (file == null)
                        continue;
                    if (Enum.IsDefined(v_Type, file.TagName))
                    {
                        var s = (enuBalafonItemType)Enum.Parse(v_Type, file.TagName);
                        var e = this.GetFileGroups(s);
                        e.LoadString(file.RenderXML(null));
                    }
                    else if (file.TagName == BalafonConstant.PROJECT_FOLDER_TAG)
                    {
                        this.FolderGroup.LoadString(file.RenderXML(null));
                    }
                }
            }
        }

        private BalafonProjecPropertyGroup AddPropertyGroup()
        {
            BalafonProjecPropertyGroup c = new BalafonProjecPropertyGroup(this);
            this.Childs.Add(c);
            return c;
        }

        protected override void ClearPropertyGroup()
        {
            base.ClearPropertyGroup();
        }
        public BalafonProjectItemFolder AddFolder(string folder)
        {
            if (this.FolderGroup.ContainKey(folder) == false)
            {
                var v_folder = new BalafonProjectItemFolder()
                {
                    Include = folder
                };
                this.FolderGroup.AddChild(v_folder);
                return v_folder;
            }
            return null;
        }
        public BalafonProjectItemFile AddFile(string file, enuBalafonItemType mode)
        {
            var files = GetFileGroups(mode);

            if (files.Contains(file) == false)
            {
                var d = BalafonProjectItemFile.CreateFile(mode, file);
                if ((d != null) && files.AddChild(d))
                    return d;
            }
            return null;
        }

        private BalafonProjectItemGroup GetFileGroups(enuBalafonItemType mode)
        {
            if (this.m_groupModes.ContainsKey(mode))
                return this.m_groupModes[mode];
            BalafonProjectItemGroup g = new BalafonProjectItemGroup();
            this.AddChild(g);
            this.m_groupModes.Add(mode, g);
            return g;
        }

        protected BalafonProjectFolderGroup CreateFolderCollections()
        {
            return new BalafonProjectFolderGroup(this);
        }

        IBalafonProjectItem IBalafonProject.AddFolder(string name)
        {
            return this.AddFolder (name);
        }

        IBalafonProjectItem IBalafonProject.AddFile(string filemane, enuBalafonItemType type)
        {
            return this.AddFile (filemane, type);
        }

        public class BalafonProjectFolderGroup : BalafonProjectItemGroup
        {
            private List<string> m_folders;
            private BalafonProject m_owner;

            public BalafonProjectFolderGroup(BalafonProject BalafonProject)
            {
                m_folders = new List<string>();
                this.m_owner = BalafonProject;
                this.m_owner.AddChild(this);

            }
            public override bool AddChild(CoreXmlElementBase c)
            {
                if (c is BalafonProjectItemFolder)
                {
                    BalafonProjectItemFolder g = c as BalafonProjectItemFolder;
                    if (!this.m_folders.Contains(g.Include))
                    {
                        return base.AddChild(g);
                    }
                }
                return false;
            }
            internal bool ContainKey(string p)
            {

                return this.m_folders.Contains(p);
            }
        }


        public string FileName { get; set; }
    }
}
