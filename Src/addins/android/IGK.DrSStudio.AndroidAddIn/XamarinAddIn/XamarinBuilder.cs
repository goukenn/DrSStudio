using IGK.DrSStudio.Android.Xamarin.Xml;
using IGK.ICore;
using IGK.ICore.IO;
using IGK.ICore.Resources;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace IGK.DrSStudio.Android.Xamarin
{
    public class XamarinBuilder
    {
        private XamarinProjectXmlElement m_bindProject;
        private string m_bindFileName;
        private  CoreXmlElement m_bindNode;

        public  CoreXmlElement BindNode { get { return m_bindNode; } }

        public void GenerateProjet(XamarinProjectXmlElement project, string outFolder, bool overrideExistingFile)
        {
            if (!PathUtils.CreateDir(outFolder))
                return;
            this.m_bindProject = project;
          
            //generate manifest file
            Environment.CurrentDirectory = outFolder;
            
            //android folder
            PathUtils.CreateDir("Resources");

            //IGK.ICore system 

            this.BuildDir("Menu");
            this.AddFile("Menu/readme.txt", enuXamarinBuildMode.None);
            this.BuildDir("Tools");
            this.BuildDir("WinUI");
            this.BuildDir("Docs");

            //add refence
            this.m_bindProject.AddReference("System");
            this.m_bindProject.AddReference("System.Core");
            this.m_bindProject.AddReference("System.Xml");
            this.m_bindProject.AddReference("Mono.Android");

            //extract core system.
            string v_coreDir = Path.GetFullPath("Core");
            PathUtils.CreateDir(v_coreDir);
            var data = CoreResources.GetResource(XamarinConstant.CORELIB_RES);
            try
            {
                if (data != null)
                {
                    if (CoreZipFile.ExtractZipData(data, v_coreDir) == false)
                    {
                        CoreLog.WriteLine("Impossible d'extraire l' IGK Core System for android");
                        return;
                    }
                }
            }
            catch (Exception ex){
                CoreLog.WriteDebug(ex.Message);
                CoreLog.WriteLine("Impossible d'extraire l' IGK Core System");
            }

            this.m_bindProject.AddReference("IGK.ICore", PathUtils.GetRelativePath(Environment.CurrentDirectory, Path.GetFullPath("Core/IGK.ICore.dll")).FixRelativeDir());
            this.m_bindProject.AddReference("IGK.ICore.Android", PathUtils.GetRelativePath(Environment.CurrentDirectory, Path.GetFullPath("Core/IGK.ICore.Android.dll")).FixRelativeDir());


            //touch file 

            var g = CoreXmlElement.CreateFromString(CoreResources.GetResourceString("xamarin_build_project", GetType().Assembly ));
            var evalutuator = new XamarinBuilderEvaluator(this);
            if (g != null)
            {
                foreach (CoreXmlElement v_es in g.getElementsByTagName("File"))
                {
                    string n = Path.GetFullPath(v_es["Name"]);
                 
                    bool noprefix = v_es.GetAttributeValue<bool>("NoPrefix", false);
                    bool noeval = v_es.GetAttributeValue<bool>("NoEval", false);
                    enuXamarinBuildMode build = v_es.GetAttributeValue<enuXamarinBuildMode>("BuildMode", enuXamarinBuildMode.None );
                    string d = Path.GetDirectoryName(n);
                    if (!PathUtils.CreateDir(d))
                        break;
                    string p = v_es.Content != null ? v_es.Content.ToString() : null ?? string.Empty;
                    string fname = string.Format("{0}{1}", noprefix ? "": project.Prefix, Path.GetFileName(n));
                    
                    this.m_bindNode =v_es ;
                    string dname = Path.Combine(d, fname);

                    if (overrideExistingFile || !File.Exists(dname))
                    {
                        this.m_bindFileName = dname;
                        if (string.IsNullOrEmpty(p))
                        {
                            //touch file 
                            File.WriteAllText(dname, string.Empty);

                        }
                        else
                        {
                            if (noeval)
                            {
                                var td = CoreResources.GetResource(p);
                                if (td != null)
                                    File.WriteAllBytes(dname, td);
                                else
                                    File.WriteAllText(dname, string.Empty);
                            }
                            else
                            {
                                var td = CoreResources.GetResourceString(p);
                                File.WriteAllText(dname,
                           td != null ?
                           (noeval ? td :
                         IGK.ICore.Web.CoreWebUtils.EvalWebStringExpression(
                           td,
                           evalutuator)) : string.Empty);
                            }
                        }
                    }
                    string cwdir = PathUtils.GetRelativePath(Environment.CurrentDirectory, dname);
                    this.AddFile(cwdir.FixRelativeDir(), build);

                }
            }
            string outfile = Path.Combine(outFolder, project.Name + XamarinConstant.CSPROJ_EXT);
            File.WriteAllText(outfile, project.RenderXML(null));

            AndroidManifest c = new AndroidManifest();
            string s = Path.GetFullPath(project.SystemPropertyGroup.AndroidManifest);
            PathUtils.CreateDir(Path.GetDirectoryName(s));
            c.SaveToFile(s);
            this.m_bindProject.FileName = outfile;
            this.BuildProject();
            
        }
        /// <summary>
        /// build the current project to 
        /// </summary>
        /// <param name="outfile"></param>
        /// <param name="Verbosity"></param>
        public void BuildProject(
            enuXamarinBuildVerbosity Verbosity=enuXamarinBuildVerbosity.normal
            ) {
            string msbuild = @"C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe";
            if (!File.Exists(msbuild) || (this.m_bindProject ==null ) )
            {
                return ;
            }
                StringBuilder sb = new StringBuilder();
                StringBuilder Properties = new StringBuilder(string.Format("/p:AndroidSdkDirectory=\"{0}\" /p:JavaSdkDirectory=\"{1}\" /p:Configuration={2}",
                    IGK.DrSStudio.Android.Settings.AndroidSetting.Instance.PlatformSDK,
                    IGK.DrSStudio.Android.Settings.AndroidSetting.Instance.JavaSDK,
                    "Debug")
                    );

                //add verbose level

                //build type
                //support Build, Run, BuildCompile, SingAndroidPackage
                string buildtype = "BuildCompile;BuildApk;SignAndroidPackage";
                //buildtype = string.Join(";",
                //    XamarinBuilderMSBuildTargets.Build,
                //    XamarinBuilderMSBuildTargets.BuildApk,
                //    XamarinBuilderMSBuildTargets.BuildCompile,
                //    XamarinBuilderMSBuildTargets.SignAndroidPackage
                //    );
                string s = //"\"{2}\" /t:{0} {3} \"{1}\" > d:\\temp\\msbuild.result.txt";
                 s = "\"{2}\" /t:{0} {3} \"{1}\" ";
                sb.AppendLine(string.Format(s, 
                    buildtype, 
                    this.m_bindProject .FileName , 
                    msbuild,
                   string.Format ("/v:{0} {1}",
                   Verbosity.ToString()
                   /*enuXamarinBuildVerbosity.detailed .ToString()*//* Verbosity.ToString()*/,  Properties.ToString())
                   ));
                //sb.AppendLine("pause");
                String d = PathUtils.GetTempFileWithExtension ("bat");//d:\\temp\\ex.bat";

                File.WriteAllText(d, sb.ToString());
                ProcessStartInfo sinfo = new ProcessStartInfo();

                sinfo.FileName = d;
                sinfo.UseShellExecute = true;
                var p = Process.Start(sinfo);
                p.WaitForExit();
                File.Delete(d);
            
        }

        /// <summary>
        /// add file to project
        /// </summary>
        /// <param name="cwdir">file according to project</param>
        /// <param name="build">build mode</param>
        private void AddFile(string cwdir, enuXamarinBuildMode build)
        {
            this.m_bindProject.AddFile(PathUtils.TreatDirPath(cwdir), build);
        }
        /// <summary>
        /// add dirctory
        /// </summary>
        /// <param name="cwdir"></param>
        private void BuildDir(string cwdir)
        {
            PathUtils.CreateDir(cwdir);
            this.m_bindProject.AddFolder(cwdir);
        }
        public XamarinProjectXmlElement Project { get { return this.m_bindProject; } set { this.m_bindProject = value; } }

        public string FileName { get { return this.m_bindFileName; } }
    }
}

