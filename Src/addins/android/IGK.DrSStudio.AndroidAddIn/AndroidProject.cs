

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: AndroidProject.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/


using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:AndroidProject.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    using IGK.ICore.Xml;
    using IGK.ICore.Codec;
    using System.Reflection;
    using System.Diagnostics;
    
    using System.Threading;
    using System.Windows.Forms;
    using IGK.DrSStudio.Android.Actions;
    using IGK.ICore;
    using IGK.DrSStudio.Android.Tools;
    using IGK.ICore.IO;

    [CoreWorkingObject("AndroidSolution")]
    /// <summary>
    /// repesent the android project
    /// </summary>
    public class AndroidProject : CoreWorkingObjectBase ,  IAndroidSolution, ICoreWorkingProjectSolution,
        ICoreSerializerService ,
        ICoreIdentifier
    {
        private string m_Name;
        private string m_TargetLocation;
        private IAndroidResourceCollections m_Resources;
        private string m_apkFilename;
        private string m_PackageName;
        private string m_MaintActivity;
        private AndroidTargetInfo m_TargetVersion;
        private string m_filename;
        private string m_imagekey;
        private AndroidItemCollections m_items;
        private DateTime m_Created;

        [CoreXMLAttribute()]
        public DateTime Created
        {
            get { return m_Created; }
            set
            {
                if (m_Created != value)
                {
                    m_Created = value;
                }
            }
        }

        [CoreXMLAttribute ()]
        public string MaintActivity
        {
            get { return m_MaintActivity; }
            set
            {
                if (m_MaintActivity != value)
                {
                    m_MaintActivity = value;
                }
            }
        }
      
        [CoreXMLAttribute()]
        public string PackageName
        {
            get { return m_PackageName; }
            set
            {
                if (m_PackageName != value)
                {
                    m_PackageName = value;
                }
            }
        }

public string apkFilename{
get{ return m_apkFilename;}
set{ 
if (m_apkFilename !=value)
{
m_apkFilename =value;
}
}
}
        public string TargetLocation
        {
            get { return m_TargetLocation; }
            set
            {
                if (m_TargetLocation != value)
                {
                    m_TargetLocation = value;
                }
            }
        }
        [CoreXMLAttribute()]
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <param name="location"></param>
            /// <param name="package"></param>
            /// <param name="mainActivityClass"></param>
            /// <param name="target">index in target id</param>
            /// <returns></returns>
        
        
        public static AndroidProject CreateProject(
            string name,
            string location,
            string package,
            string mainActivityClass,
            int target)
        {

            var t = AndroidTool.Instance.GetAndroidTargets();
            if (t.IndexExists(target))
            {
                return CreateProject(
                    name ,
                    location,
                    package,
                    mainActivityClass,
                    t[target],
                    null
                    );
            }
            return null;
        }

        /// <summary>
        /// create an android project
        /// </summary>
        /// <param name="name">name of the project</param>
        /// <param name="location">location dir </param>
        /// <param name="package">package name</param>
        /// <param name="mainActivityClass"></param>
        /// <param name="target"></param>
        /// <param name="minTarget"></param>
        /// <returns></returns>
        public static AndroidProject CreateProject(
            string name, 
            string location,
            string package,
            string mainActivityClass,
            AndroidTargetInfo target,
            AndroidTargetInfo minTarget
            )
        {
           string response =  AndroidTool.Instance.CreateProject(
                name,
                location,
                package,
                mainActivityClass, target ,minTarget );
           if (response == "SUCESSFULL")
           {
               AndroidProject project = new AndroidProject();
               project.m_Name = name;
               project.m_TargetLocation = location;
               project.m_PackageName = package;
               project.m_MaintActivity = mainActivityClass;
               project.m_TargetVersion = target;
               project.InitProject();
               return project;
           }
           return null;

        }
        private AndroidProject()
        {
            this.m_Resources = new AndroidProjectResourcesCollection(this);
            this.m_filename = string.Empty;
            this.m_items = new AndroidItemCollections(this);
            this.m_imagekey = AndroidConstant.ANDROID_IMG_SOLUTION;
            this.m_Created = DateTime.Now;
          
        }
        
        void InitProject()
        {
            List<string> packages = new List<string>();
            List<string> res = new List<string>();
            //load packages
            this._loadPackage();
            this._loadResFolder();      

            
            this.m_items.Add(new AndroidSolutionBinaryFolder(this));
            this.m_items.Add(new AndroidSolutionManifest(this));
            string dir =  Path.Combine (this.TargetLocation , "drss_art");
            if (PathUtils.CreateDir (dir)){
                this.m_items.Add(new AndroidSolutionFolder(this, dir));
            }
            

            RegisterResources("values", AndroidResourceFactory.CreateResourceContainer("string"));
            RegisterResources("layout", AndroidResourceFactory.CreateResourceContainer("layout"));
            RegisterResources("drawable-hdpi", AndroidResourceFactory.CreateResourceContainer("drawable-hdpi"));
            RegisterResources("drawable-ldpi", AndroidResourceFactory.CreateResourceContainer("drawable-ldpi"));
            RegisterResources("drawable-mdpi", AndroidResourceFactory.CreateResourceContainer("drawable-mdpi"));
            RegisterResources("drawable-xhdpi", AndroidResourceFactory.CreateResourceContainer("drawable-xhdpi"));
        }

        private void _loadResFolder()
        {
            var v_res = new AndroidSolutionResFolder(this);
            string dir = Path.Combine(this.TargetLocation, "res");
            if (Directory.Exists(dir))
            {
                Dictionary<string, AndroidSolutionFolder> v_files = new Dictionary<string, AndroidSolutionFolder>();
                AndroidSolutionFile c = null;
                AndroidSolutionFolder folder = null;
                string v_hdir = null;
                foreach (string f in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                {
                    v_hdir = PathUtils.GetDirectoryName(f);
                    string n = v_hdir.Substring(dir.Length + 1);

                    if (v_files.ContainsKey(n))
                    {
                        folder = v_files[n];

                    }
                    else
                    {
                        folder = new AndroidSolutionFolder(this, v_hdir);
                        v_files.Add(n, folder);
                        v_res.Add(folder);
                    }

                    c =
                new AndroidSolutionFile(this, f)
                {
                    Name = Path.GetFileName(f) 
                };
                    folder.Add(c);


                }
            }
            this.m_items.Add(v_res);
        }

        private void _loadPackage()
        {
            string dir = Path.Combine(this.TargetLocation, "src");
            if (Directory.Exists(dir))
            {
                Dictionary<string, AndroidSolutionPackage> m_package = new Dictionary<string, AndroidSolutionPackage>();
                AndroidSolutionPackage c = null;
                foreach (string f in Directory.GetFiles(dir, "*.java", SearchOption.AllDirectories))
                {
                    string n = PathUtils.GetDirectoryName(f).Substring(dir.Length + 1);
                    n = n.Replace(Path.DirectorySeparatorChar, '.');

                    if (m_package.ContainsKey(n))
                        c = m_package[n];
                    else
                    {
                        c =
                    new AndroidSolutionPackage(this)
                    {
                        Name = n
                    };
                        m_package.Add(n, c);
                        this.m_items.Add(c);
                    }
                    c.Files.Add(new AndroidSolutionJScriptFile(this, f));
                }
            }
        }
        private void RegisterResources(string name, AndroidResourceContainerBase resContainer)
        {
            AndroidProjectResourcesCollection c = this.m_Resources as AndroidProjectResourcesCollection;
            if (c.Contains(name)) 
                return;
            c.Add(name, resContainer);
        }
        public AndroidTargetInfo AndroidTargetVersion
        {
            get
            {
                return m_TargetVersion;
            }
            set
            {
                m_TargetVersion = value;
            }
        }
        public IAndroidResourceCollections Resources
        {
            get { return m_Resources; }
        }
        /// <summary>
        /// represent a android project collection
        /// </summary>
        class AndroidProjectResourcesCollection : IAndroidResourceCollections
        {
            AndroidProject m_project;
            Dictionary<string, AndroidResourceContainerBase> m_resTab;
            public AndroidProjectResourcesCollection(AndroidProject project)
            {
                this.m_project = project;
                this.m_resTab = new Dictionary<string, AndroidResourceContainerBase>();
            }
            public AndroidResourceContainerBase this[string key]
            {
                get
                {
                    if (this.Contains(key))
                        return this.m_resTab[key];
                    return null;
                }
            }
            public bool Contains(string name)
            {
                return this.m_resTab.ContainsKey(name);
            }
            IAndroidResourceContainer IAndroidResourceCollections.this[string restypeName]
            {
                get { return this[restypeName]; }
            }
            public int Count
            {
                get { return this.m_resTab.Count; }
            }
            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_resTab.Values.GetEnumerator();
            }
            internal void Add(string name, AndroidResourceContainerBase resContainer)
            {
                this.m_resTab.Add(name, resContainer);
            }
            public override string ToString()
            {
                return string.Format ("Resources:[#{0}]",this.Count );
            }
        }
        /*
         * 
         * Build the current project instance
         * */
        public bool Build()
        {
            return AndroidTool.Instance.CompileProject(this.TargetLocation, false);
        }
        /// <summary>
        /// debug project
        /// </summary>
        public void Debug()
        {
            AndroidTool.Instance.CompileProject(this.TargetLocation, true);
        }
        public void BuildAndRun()
        {
            this.Build();
            string v_releasefile = Path.GetFullPath( Path.Combine(this.TargetLocation + "/bin/" + this.ReleaseFile));
            if (File.Exists(v_releasefile))
            {
                if (AndroidTool.Instance.DeployInstall(v_releasefile))
                {
                    AndroidTool.Instance.Cmd(string.Format("adb shell am start {0}/.{1}", this.PackageName, this.MaintActivity));
                }
            }
        }
        public void DebugAndRun()
        {
            this.Debug();
            string v_debugfile =Path.GetFullPath(  Path.Combine(this.TargetLocation + "/bin/" + this.DebugFile));
            if (File.Exists(v_debugfile))
            {
                var i = AndroidTool.Instance.Cmd(string.Format("adb uninstall {0}", this.PackageName));
                if (AndroidTool.Instance.DeployInstall(v_debugfile))
                {
                    string s = AndroidTool.Instance.Cmd(string.Format("adb shell am start {0}/.{1}", this.PackageName, this.MaintActivity));
                    System.Diagnostics.Debug.WriteLine(s);
                }
            }
        }

        public string DebugFile { get {
            return this.Name + "-debug.apk";
        } }

        public string ReleaseFile { get { 
             return this.Name  + "-release-unsigned.apk";
        } }

        /// <summary>
        /// get the project collection
        /// </summary>
        public ICoreWorkingProjectCollections Projects
        {
            get {
                return null;
            }
        }
        /// <summary>
        /// get the project name
        /// </summary>
        string ICoreIdentifier.Id
        {
            get { return this.m_Name; }
        }

        public string FileName
        {
            get {
                return this.m_filename;
            }
        }

        public string ImageKey
        {
            get
            {
                return this.m_imagekey;
            }
            set
            {
                this.m_imagekey = value;
            }
        }

        public ICoreWorkingProjectSolutionItemCollections Items
        {
            get { return this.m_items; }
        }

        #region "OPEN REGION"
        public void Open(ICoreSystemWorkbench coreWorkbench, ICoreWorkingProjectSolutionItem item)
        {
            new AndroidSolutionItemOpener(this).Open(coreWorkbench, item);
        }
   
        
        #endregion

        class AndroidItemCollections : ICoreWorkingProjectSolutionItemCollections
        {
            private AndroidProject m_project;
            List<ICoreWorkingProjectSolutionItem> m_items;
            public AndroidItemCollections(AndroidProject androidProject)
            {
                this.m_project = androidProject;
                this.m_items = new List<ICoreWorkingProjectSolutionItem>();
            }

            public void Add(ICoreWorkingProjectSolutionItem item)
            {
                AndroidSolutionItem i = item as AndroidSolutionItem;
                if (i != null)
                {
                    this.m_items.Add(item);
                    i.Project = this.m_project;
                }
            }
            public void Remove(ICoreWorkingProjectSolutionItem item)
            {
                   AndroidSolutionItem i = item as AndroidSolutionItem;
                   if (i != null)
                   {
                       this.m_items.Remove(item);
                       i.Project = null;
                   }
            }
            public int Count
            {
                get { return this.m_items.Count; }
            }

            public System.Collections.IEnumerator GetEnumerator()
            {
                return this.m_items.GetEnumerator();
            }
        }
        public void SaveAs(string filename)
        {
            System.Xml.XmlWriterSettings v_setting = new System.Xml.XmlWriterSettings()
            {
                Indent = true
            };
            global::System.Xml.XmlWriter swriter = global::System.Xml.XmlWriter.Create(filename, v_setting);
            IXMLSerializer s = CoreXMLSerializer.Create(swriter);
            (this as ICoreSerializerService).Serialize(s);
            s.Close();
        }
        /// <summary>
        /// save the solution project
        /// </summary>
        public void Save()
        {
            string v_file = Path.Combine(this.m_TargetLocation, this.m_Name + ".igkandroidsln");
            this.SaveAs(v_file);
         
        }
        protected override void WriteElements(IXMLSerializer xwriter)
        {
            base.WriteElements(xwriter);
            foreach (var i in this.m_items)
            {
                ICoreSerializerService q = (i as ICoreSerializerService);
                if(q!=null)
                {
                    q.Serialize(xwriter);
                }
            }
        }
        protected override void ReadElements(IXMLDeserializer xreader)
        {
            base.ReadElements(xreader);
        }





        public virtual System.Collections.IEnumerable GetSolutionToolActions()
        {
            List<AndroidActionBase> actions = new List<AndroidActionBase>();
            actions.Add(new AndroidBuildAction() { Id = "build", ImageKey = AndroidConstant.ANDROID_IMG_APP_ANDROID });
            actions.Add(new AndroidDebugAction() { Id = "debug", ImageKey = AndroidConstant.ANDROID_IMG_APP_ANDROID });
            actions.Add(new AndroidDeployDebugAction() { Id = "deploy_debug", ImageKey = AndroidConstant.ANDROID_IMG_APP_DEPLOY});

            actions.Add(new AndroidRunSDKManager() { Id = "sdkmanager", ImageKey = AndroidConstant.ANDROID_IMG_SDK_MANAGER });
            actions.Add(new AndroidRunADVManager() { Id = "advmanager", ImageKey = AndroidConstant.ANDROID_IMG_ADV_MANAGER });
            return actions;
        }

        internal static AndroidProject OpenProject(string filename)
        {
            throw new NotImplementedException();
        }




        public ICoreSaveAsInfo GetSolutionSaveAsInfo()
        {
            return null;
        }

        public void Close()
        {
            throw new NotImplementedException();
        }
    }
}

