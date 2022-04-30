using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGK.DrSStudio.Balafon.Xml;
using IGK.ICore.Xml;
using System.IO;

namespace IGK.DrSStudio.Balafon.IO
{
    class Utils
    {
        
        /// <summary>
        /// open file as balafon project
        /// </summary>
        /// <param name="filename">name of file</param>
        /// <returns></returns>
        public  static BalafonProject OpenFile(string filename)
        {
            CoreXmlElement d = CoreXmlElement.LoadFile(filename);
            var s = d.getElementsByTagName(BalafonConstant.PROJECT_TAG)[0] as CoreXmlElement;

            if (s != null)
            {
                BalafonProject b = new BalafonProject();
                b.Load(s);
                return b;
            }
            return null;
        }

        public  static string GetWorbenchFile(string dirName)
        {
            return Path.Combine(dirName, $"{Path.GetFileName (dirName)}.{BalafonConstant.NEW_FILENAME_EXT}");

        }

        public  static BalafonProject LoadWorkbench(string selectedFolder)
        {
            //Special folder
            //Bin * core system
            //Mod * additinal module
            //Inc * use for inclure when required
            //Data * store private site data
            //R * store exposed resources
            //Scripts * store javascript exposed files
            //
            var check = false;
            if (Directory.Exists(Path.Combine(selectedFolder, "Lib/igk"))){

                string libFile = Path.Combine(selectedFolder, "Lib/igk/igk_framework.php");


                if (File.Exists(libFile)) {
                    StringBuilder sb = new StringBuilder ();
                    sb.AppendLine ($"require_once('{libFile }');");
                    sb.AppendLine ($"igk_wl('1');");                

                   string g = BalafonPHPUtils.ExecScript(sb.ToString());
                    if (!string.IsNullOrEmpty(g)&&(g == "1") ){
                        check = true;
                    }
                }

            }
            

            if (!check)
                return null;



            BalafonProject gp = new BalafonProject();
            foreach (string dir in Directory.GetDirectories(selectedFolder)) {
               //string g =  Path.GetFileName (dir);
               // Path.GetR
               GetRootFolderLoader(Path.GetFileName(dir))?.LoadFolder(selectedFolder, dir, gp);
               
                //loader.LoadFolder(dir, gp);
            }
      
            foreach (string d in Directory.GetFiles(selectedFolder))
            {
                string g = Path.GetFileName(d);
                //Console.WriteLine(g);
                gp.AddFile (d, enuBalafonItemType.Include );
            }

           System.IO.File.WriteAllText (GetWorbenchFile(selectedFolder),  gp.RenderXML (null));

            return gp;
        }

        private static IBalafonProjectFolderLoader GetRootFolderLoader(string name)
        {
            Type t = System.Type.GetType($"{typeof(Utils).Namespace}.{BalafonConstant.PROJECT_NAME}{name}FolderLoader",false);
            if (t != null) {
               return  t.Assembly.CreateInstance (t.FullName ) as IBalafonProjectFolderLoader;
            }
            return new BalafonGlobalDirLoaderFile();
        }
    }
}
