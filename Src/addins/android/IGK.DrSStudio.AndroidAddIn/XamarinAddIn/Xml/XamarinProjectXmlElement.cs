
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.DrSStudio.Android.Xamarin.Xml
{
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.Resources;
    using IGK.ICore.Web;
    using IGK.ICore.Codec;
    using IGK.ICore.Xml;
    using IGK.DrSStudio.Android.Xamarin.Settings;

    /// <summary>
    /// represent the base xamarin project file
    /// </summary>
    public class XamarinProjectXmlElement : XamarinProjectXmlElementBase
    {        
        private Dictionary<enuXamarinBuildMode, XamarinProjectItemGroup> m_groupModes;
        private XamarinProjectXmlPropertyGroup m_systemPropertyGroup;
        private CoreXmlElement m_ImportNode;
        private XamarinProjectFolderGroup m_FolderCollections;
        private string m_AppTitle;
        private string m_MainTheme;

        public string MainTheme
        {
            get { return m_MainTheme; }
            set
            {
                if (m_MainTheme != value)
                {
                    m_MainTheme = value;
                }
            }
        }
        public string AppTitle
        {
            get { return m_AppTitle; }
            set
            {
                if (m_AppTitle != value)
                {
                    m_AppTitle = value;
                }
            }
        }
        internal XamarinProjectFolderGroup FolderGroup { get {
            if (this.m_FolderCollections == null)
                this.m_FolderCollections = CreateFolderCollections();
            return this.m_FolderCollections;
        } }
        public XamarinProjectXmlPropertyGroup SystemPropertyGroup { get { return this.m_systemPropertyGroup; } }
        
        /// <summary>
        /// get or set the name of this project
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// get or set the file name of this project
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// get or set the application prefix name
        /// </summary>
        public string Prefix { get; set; }

        public XamarinProjectXmlElement()
            : base(XamarinConstant.PROJECT_TAG)
        {
            this.m_groupModes = new Dictionary<enuXamarinBuildMode, XamarinProjectItemGroup>();
            
            this.setAttribute("DefaultTargets", "Build")
            .setAttribute("ToolsVersion", "4.0")
            .setAttribute("xmlns", "http://schemas.microsoft.com/developer/msbuild/2003");

            var d = this.AddPropertyGroup();

            //d.Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
            //d.Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
            d.add("Configuration").setAttribute("Condition", " '$(Configuration)' == '' ").Content = "Debug";
            d.add("Platform").setAttribute("Condition", " '$(Platform)' == '' ").Content = "AnyCPU";
            d.ProjectTypeGuids = XamarinConstant.PROJECT_TYPES_GUIDS;// "{EFBA0AD7-5A72-4C68-AF49-83D382785DCF};{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}";
            d.ProjectGuid = string.Format("{{{0}}}", Guid.NewGuid().ToString()).ToUpper();// "{1D771BA0-D6FF-4AEE-BCB9-E4F794C2B848}";
            d.OutputType = "Library";
            d.RootNamespace = "";
            d.MonoAndroidResourcePrefix = "Resources";
            d.MonoAndroidAssetsPrefix = "Assets";
            d.AndroidUseLatestPlatformSdk = "False";
            d.AndroidApplication = "True";
            d.AndroidResgenFile = "Resources\\AR.designer.cs";
            d.AndroidResgenClass = "AR";
            d.AssemblyName = "IGK.JB.Browser";


            //default 
            d.TargetFrameworkVersion = XamarinSettings.Instance.DefaultTargetFrameWork;// "v4.0.3";
            
            d.AndroidManifest = "Properties\\AndroidManifest.xml";

            this.m_systemPropertyGroup = d;
            d = this.AddPropertyGroup();
            d.setAttribute("Condition", " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ");
            d.SetElementProperty("DebugType", "full");
            d.SetElementProperty("Optimize", "false");
            d.SetElementProperty("OutputPath", "bin\\Debug");
            d.SetElementProperty("ErrorReport", "prompt");
            d.SetElementProperty("WarningLevel", "4");
            d.SetElementProperty("ConsolePause", "false");
            d.SetElementProperty("AndroidUseSharedRuntime", "false");              
            d.SetElementProperty("DebugSymbols", "true");
            d.SetElementProperty("DefineConstants", "DEBUG");
            d.SetElementProperty("AndroidLinkMode", "None");
            //<AndroidLinkMode>None</AndroidLinkMode>
            //<ConsolePause>false</ConsolePause>

            d = this.AddPropertyGroup();
            d["Condition"] = " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ";
            d.SetElementProperty("DebugType", "full");
            d.SetElementProperty("Optimize", "true");
            d.SetElementProperty("OutputPath", "bin\\Release");
            d.SetElementProperty("ErrorReport", "prompt");
            d.SetElementProperty("WarningLevel", "4");
            d.SetElementProperty("ConsolePause", "false");
            d.SetElementProperty("AndroidUseSharedRuntime", "false");

            this.m_ImportNode = this.add("Import");
            this.m_ImportNode.setAttribute("Project", XamarinConstant.MSBUID_ANDROID);


        }

     
        //public void BuildProjet(string outFolder)
        //{
        //    if (!PathUtils.CreateDir(outFolder))
        //        return;

        //    File.WriteAllText(Path.Combine(outFolder, this.Name + XamarinConstant.CSPROJ_EXT), this.Render(null));

        //    //generate manifest file
        //    Environment.CurrentDirectory = outFolder;
        //    //android folder
        //    PathUtils.CreateDir("Resources");

        //    //drsstudio out folder
        //    PathUtils.CreateDir("Menu");
        //    PathUtils.CreateDir("Tools");
        //    PathUtils.CreateDir("WinUI");

        //    AndroidManifest c = new AndroidManifest();
        //    string s = Path.GetFullPath (this.m_systemPropertyGroup.AndroidManifest);
        //    PathUtils.CreateDir(Path.GetDirectoryName(s));
        //    c.SaveToFile(s);


        //    //touch file 
        //    var d = CoreResources.GetResource("xamarin_main_activity");
        //    File.WriteAllText(string.Format("{0}{1}", this.Prefix, "MainActivity.cs"),
        //        d !=null? 
        //      IGK.ICore.Web.CoreWebUtils.EvalWebStringExpression(
        //        Encoding.UTF8.GetString (d),
        //        this)  : string.Empty );
        //}

        public sealed class XamarinProjectFiles : IEnumerable
        {
            private XamarinProjectXmlElement m_owner;
            Dictionary<string, CoreXmlElement> m_files;
            public XamarinProjectFiles(XamarinProjectXmlElement xamarinProjectXmlElement)
            {
                this.m_owner = xamarinProjectXmlElement;
                this.m_files = new Dictionary<string, CoreXmlElement>();
            }

            internal void Remove(string file)
            {
                var e = this.m_files[file];
                this.m_files.Remove(file);
            }

            public IEnumerator GetEnumerator()
            {
                return this.m_files.GetEnumerator();
            }
        }

        protected override void ReadElements(IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }
      

        public XamarinProjectXmlPropertyGroup AddPropertyGroup() {
            XamarinProjectXmlPropertyGroup c = new XamarinProjectXmlPropertyGroup(this);
            this.Childs.Add(c);
            return c;
        }
        /// <summary>
        /// remove all properties group group this project
        /// </summary>
        private void ClearPropertyGroup()
        {
            var g = this.getElementsByTagName(XamarinConstant.PROJECT_PROPERTY_GROUP_TAG);
            if (g != null) {
                for (int i = 0; i < g.Length; i++)
                {
                    this.Remove(g[i]);
                }
            }
        }
        /// <summary>
        /// load object as xamaring project
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
            if (this.m_FolderCollections != null)
                this.m_FolderCollections.Clear();
            foreach (CoreXmlElement  itemgroup in item.getElementsByTagName(XamarinConstant.PROJECT_PROPERTY_GROUP_TAG))
            {
                if (!main && (!itemgroup.HasAttributes))
                {
                    this.m_systemPropertyGroup.LoadString(itemgroup.RenderInnerHTML(null));
                    this.AddChild(this.m_systemPropertyGroup);
                    main = true;
                }
                else {
                    var v_pg = this.AddPropertyGroup();
                    v_pg.LoadAttributes(itemgroup);
                    v_pg.LoadString(itemgroup.RenderInnerHTML(null));
                }
            }

            //load files 
            var v_Type = typeof(enuXamarinBuildMode);
            foreach (var itemgroup in item.getElementsByTagName(XamarinConstant.PROJECT_PROPERTY_ITEM_GROUP_TAG))
            {
                //load child 
                foreach (CoreXmlElement  file in itemgroup.Childs )
                {
                    if (file == null)
                        continue ;
                    if (Enum.IsDefined(v_Type, file.TagName))
                    {
                        var s = (enuXamarinBuildMode)Enum.Parse(typeof(enuXamarinBuildMode), file.TagName);
                        var e = this.GetFileGroups(s);
                        e.LoadString(file.RenderXML(null));
                    }
                    else if (file.TagName == XamarinConstant.PROJET_FOLDER_TAG)
                    {
                        this.FolderGroup.LoadString(file.RenderXML(null));
                    }
                    }
            }
            //load files 
            foreach (var itemgroup in item.getElementsByTagName(XamarinConstant.PROJET_IMPORT_TAG))
            {
                this.m_ImportNode = this.add(XamarinConstant.PROJET_IMPORT_TAG);
                this.m_ImportNode.LoadAttributes(itemgroup as CoreXmlElement );
            }
        }
        internal void AddFolder(string folder)
        {
            if (this.FolderGroup.ContainKey(folder) == false )
            {
                var v_folder = new XamarinProjectItemFolder()
                {
                    Include = folder
                };
                this.m_FolderCollections.AddChild(v_folder);
            }
        }
        public XamarinProjectItemFile  AddFile(string file, enuXamarinBuildMode mode)
        {
            var files = GetFileGroups(mode);

            if (files.Contains(file) == false)
            {
                var d = XamarinProjectItemFile.CreateFile(mode, file);
                if ((d!=null) && files.AddChild(d))
                    return d;
            }
            return null;
        }

        private XamarinProjectItemGroup GetFileGroups(enuXamarinBuildMode mode)
        {
            if (this.m_groupModes.ContainsKey(mode))
                return this.m_groupModes[mode];
            XamarinProjectItemGroup g = new XamarinProjectItemGroup();
            this.AddChild(g);
            this.m_groupModes.Add(mode, g);
            return g;
        }

        protected  XamarinProjectFolderGroup CreateFolderCollections()
        {
            return new XamarinProjectFolderGroup(this);
        }


        public class XamarinProjectFolderGroup : XamarinProjectItemGroup
        {
            private List<string> m_folders;
            private XamarinProjectXmlElement m_owner;

            public XamarinProjectFolderGroup(XamarinProjectXmlElement xamarinProjectXmlElement)
            {
                m_folders = new List<string>();
                this.m_owner = xamarinProjectXmlElement;
                this.m_owner.AddChild(this);

            }
            public override bool AddChild(CoreXmlElementBase c)
            {
                if (c is XamarinProjectItemFolder)
                {
                    XamarinProjectItemFolder g = c as XamarinProjectItemFolder;
                    if (!this.m_folders.Contains(g.Include))
                    {
                        return base.AddChild(g);
                    }
                }
                return false;
            }        
            internal  bool ContainKey(string p)
            {
                
                return this.m_folders.Contains(p);
            }
        }


        /// <summary>
        /// remove file 
        /// </summary>
        /// <param name="p"></param>
        public void RemoveFile(string p)
        {
            
        }

        public void AddReference(string refName)
        {
            this.AddFile(refName, enuXamarinBuildMode.Reference);
        }

        internal void AddReference(string Name, string hintPath)
        {
            var g = this.AddFile(Name, enuXamarinBuildMode.Reference) as XamarinProjectReferenceItemFile ;
            g.HintPath = hintPath;
        }

        public static XamarinProjectXmlElement CreateXamarinProjectFromFile(string file) {
            if (!File.Exists(file))
                return null;
            var d = CoreXmlElement.LoadFile(file);
            if (d == null) //file not loaded
                return null;
            var td = d.TagName ==XamarinConstant.PROJECT_TAG? d : d.getElementsByTagName(XamarinConstant.PROJECT_TAG).CoreGetValue(0, null) as CoreXmlElement ;
            if (td == null)//no project found
                return null;

            XamarinProjectXmlElement v_o = new XamarinProjectXmlElement();
            v_o.Clear();
            v_o.LoadAttributes(d);
            v_o.Load(td);
            v_o.Name = Path.GetFileNameWithoutExtension(file);
            v_o.FileName = file;
            return v_o;


        }

      
    }
}
