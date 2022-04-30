using IGK.ICore.Codec;
using IGK.ICore.Xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace IGK.VSLib
{
    public class CSSolution : CSItemBase
    {
        public static readonly string CSHARP_LIB_GUID = "{0A130E89-CFDE-46EE-8657-59C5D9ED66CC}";
        public static readonly string CSHARP_LIB_CORE_GUID =  "{95E437A5-4A87-4FAB-B15F-4A0FDE8B7DCF}";
        [CoreXMLAttribute]
        public string DefaultTargets
        {
            get
            {
                return (string)this[nameof(DefaultTargets)];
            }
            set
            {
                this[nameof(DefaultTargets)] = value;
            }
        }

        [CoreXMLDefaultAttributeValue("1.0")]
        [CoreXMLAttribute]
        public String ToolsVersion
        {
            get
            {
                return (String)this[nameof(ToolsVersion)];
            }
            set
            {
                this[nameof(ToolsVersion)] = value;
            }
        }
        public string NameSpace
        {
            get
            {
                return (string)this["xmlns"];
            }
            set
            {
                this["xmlns"] = value;
            }
        }


        /// <summary>
        /// .ctr
        /// </summary>
        public CSSolution() :base(CSConstants.PROJECT_TAG){
            this.ToolsVersion = CSConstants.TOOL_VERSION;
            this.NameSpace = CSConstants.SCHEMA;
            this.DefaultTargets = "Build";

        }
        public static CSSolution CreateFromFile(string filename) {
            CSSolution c = new CSSolution();
            return c;
        }

        public void Save(string filename) {

            using (StreamWriter writer = new StreamWriter(filename, false)){
                //XmlWriter xwriter = XmlWriter.Create(writer);
                //CoreXmlWriter s = CoreXmlWriter.Create(writer);
                CoreXMLSerializer seri = CoreXMLSerializer.Create(writer.BaseStream, Path.GetDirectoryName(filename));
                //seri.Settings.Indent = true;
                this.RenderXmlTo(seri,null);
                
                seri.Flush();
            }
            //System.IO.File.WriteAllText(filename, this.RenderXML(null));
        }

        public static CSSolution CreateCSharpLibrary()
        {
            CSSolution sol = new CSSolution();
            //order is important
            //global 
            if (sol.Add("GlobalPropertyGroup") is CSGlobalPropertyGroup mgs)
            {
                //mgs.Condition = null;// "'$(Configuration)|$(Platform)' == 'Release|AnyCPU'";
                var c = mgs.Add("Configuration");
                c["Condition"] = "  '$(Configuration)' == '' ";
                c.Content = "Debug";

                c = mgs.Add("Platform");
                c["Condition"] = "  '$(Platform)' == '' ";
                c.Content = "AnyCPU";
                mgs.OutputType = "Library";

                //mgs.Add("OutputType").Content = "Library";
                mgs.ProjectGuid = $"{{{Guid.NewGuid().ToString("D")}}}";// CSHARP_LIB_CORE_GUID;
                mgs.TargetFrameworkVersion = "v4.7";
                mgs.AssemblyName = "com.igkdev.datalib";
                mgs.RootNamespace = "com.igkdev.datalib";

                mgs.Add("FileAlignment").Content = "512";

            }
            if (sol.Add("PropertyGroup") is CSPropertyGroup ss)
            {
                ss.Condition = " '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ";
                ss.OutputPath = @"bin\Debug\";
                ss.DebugSymbols = true;
                ss.PlatformTarget = "x86";
            }

            if (sol.Add("PropertyGroup") is CSPropertyGroup gs)
            {
                gs.Condition = " '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ";
                gs.DebugType = enuCSDebugType.pdbonly;
                gs.OutputPath = @"bin\Release\";
                gs.PlatformTarget = "x86";
            }


            if (sol.Add("Import") is CSImport i)
                i.Project = @"$(MSBuildToolsPath)\Microsoft.CSharp.targets";

            return sol;
        }

        public CoreXmlElement AddProjectFileRefence(string name, string path)
        {
            //add solution file reference
            var gt = this.Add("ItemGroup").Add(CSConstants.REFERENCE_TAG);
            gt["Include"] = name;
            gt.Add("HintPath").Content = path;//%path
            gt.Add("Private").Content = "True";
            return gt;
        }

        public CSReference AddReferenceProject(CSItemGroup g, string name, string path, bool Private=false)
        {
            if (g.Add(CSConstants.REFERENCE_TAG) is CSReference m)
            {
                m.Include = name;
                m.Add("HintPath").Content = path;
                if (Private)
                    m.Add("Private").Content = CSConstants.FALSE;
                return m;
            }
            return null;
        }
    }
}
