
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.DrSStudio.Balafon
{
    using IGK.DrSStudio.Balafon.Xml;
    using IGK.ICore;
    using IGK.ICore.IO;
    using IGK.ICore.Resources;
    using IGK.ICore.Web;
    using IGK.ICore.Xml;
    using IGK.Net;
    using IGK.Net.Settings;

    /// <summary>
    /// represent balafon build manager
    /// </summary>
    class BalafonManager : IPhpFileServerListener 
    {
        private BalafonProject m_bindProject;
        private BalafonProject BindProject {
            get {
                return m_bindProject;
            }
        }
        public void Generate(BalafonProject project, string outfolder, bool overwriteFile = true)
        {
            if (project == null)
                return;
           this.m_bindProject = project;
           string v_be = Environment.CurrentDirectory;

           Environment.CurrentDirectory = outfolder;

           PathUtils.CreateDir(project.Name);
           Environment.CurrentDirectory = Path.Combine(outfolder, project.Name);
            //load core folder
           this.AddDir("Data");
           this.AddDir("Configs");
           this.AddDir("Lib");
           this.AddDir("Scripts");
           this.AddDir("Mods");

            //extract lib to 
           ExtrackCoreLib(overwriteFile);


           string outfile = Path.GetFullPath(project.Name + BalafonConstant.NEW_FILENAME_EXTENSION);
           project.FileName = outfile;
           Build();
           File.WriteAllText(outfile,
               project.RenderToXml());

           Environment.CurrentDirectory = v_be;

        }

        private void ExtrackCoreLib(bool overwriteFile)
        {
            string v_coreDir = Path.GetFullPath("Lib");
            PathUtils.CreateDir(v_coreDir);
            var data = CoreResources.GetResource("resources/balafon.framework.zip");
            try
            {
                if (data != null)
                {
                    if (CoreZipFile.ExtractZipData(data, v_coreDir, new BalafonExtract() {
                        OverwriteFile = overwriteFile
                    }) == false)
                    {
                        CoreLog.WriteLine("Impossible d'extraire  IGK Core System for android");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteDebug(ex.Message);
                CoreLog.WriteLine("Impossible d'extraire Balafon");
            }
        }
        /// <summary>
        /// build the application
        /// </summary>
        public void Build() {
            var dir = PhpServerSetting.Instance.PlateformSDKFolder;
            if (!Directory.Exists(dir))
                return;
            PhpServer server = new PhpServer();
            server.DocumentRoot =  Environment.CurrentDirectory;
            server.Port = BalafonConstant.BALAFON_MANAGER_PORT;           
            server.TargetSDKFolder = PhpServerSetting.Instance .PlateformSDKFolder ;
            server.SetRespondListener (this);
            server.WebContext = true;
            server.StartServer();

            
            if (server.IsRunning)
            {
               var g =  CoreWebUtils.PostData(string.Format ("http://127.0.0.1:{0}/Lib/igk/igk_init.php",server.Port),null);

                server.StopServer();

                IncludeFileFromFolder(this.BindProject, Path.GetFullPath ("R"), false);
                IncludeFileFromFolder(this.BindProject, Path.GetFullPath("Page"), false);
                IncludeFileFromFolder(this.BindProject, Path.GetFullPath("Mods"), false);
                IncludeFileFromFolder(this.BindProject, Path.GetFullPath("Styles"), false);
                IncludeFileFromFolder(this.BindProject, Path.GetFullPath("Scripts"), false);

            }
            
        }
        /// <summary>
        /// test project
        /// </summary>
        public void Test() { 
        }

        private void AddFile(string cwdfile, enuBalafonItemType fileType) {

            this.m_bindProject.AddFile(PathUtils.TreatDirPath(cwdfile), fileType);
        }
        private void AddDir(string cwdir)
        {
            if (PathUtils.CreateDir(cwdir))
            {
                this.m_bindProject.AddFolder(PathUtils.TreatDirPath(cwdir));
            }
        }

        public void SendResponse(PhpResponseBase d)
        {
            //IPhpQueryResponse d = d as IPhpQueryResponse;
            string f = Path.Combine(d.Server.DocumentRoot, d.RequestFile);
            if (File.Exists(f))
                d.Execute(f);
        }

        class BalafonExtract : ICoreZipFileExtractListener 
        {
            public bool Extract(string filename)
            {
                if (!this.OverwriteFile && File.Exists(filename))
                {
                    return false;
                }
                return true;
            }

            public bool OverwriteFile { get; set; }
        }
    
        public static void IncludeFileFromFolder(BalafonProject project, string directory, bool overwriteexisting){
            if (!Directory.Exists(directory))
                return ;
            string bdir = Path.GetDirectoryName (project.FileName);
            string sdir = Path.GetFullPath (directory)+"\\";//protect directory passage
            Environment.CurrentDirectory = bdir;
            foreach (string g in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                string s = g.Substring(sdir.Length); ;
                if (PathUtils.CreateDir(Path.GetDirectoryName(s)))
                {
                    if (overwriteexisting)
                    {
                        File.Copy(g, s, overwriteexisting);
                    }
                    else {
                        if (!File.Exists(s)) {
                            File.Copy(g, s, overwriteexisting);
                        }
                    }
                    project.AddFile(PathUtils.GetRelativePath(bdir, g).FixRelativeDir(), enuBalafonItemType.None);
                }
            }
        } 
    
    }
}
