using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IGK.DrSStudio.WiXAddIn
{
    public class WiXUtility
    {


        public static void LoadDir(WiXProjectFile v, string directory, string pattern)
        {
            if (v == null)
            {
                return;
            }
            ImportFiles(v, directory, pattern );
        }
        /// <summary>
        /// import directory or file to target directory object
        /// </summary>
        /// <param name="dirFile"></param>
        /// <param name="directoryOrFile"></param>
        private static void ImportFiles(WiXProjectFile dirFile, string directoryOrFile, string pattern)
        {
            string n = Path.GetFileName(directoryOrFile);
            if (dirFile.Contains(n))
                return;

            if (Directory.Exists(directoryOrFile))
            {
                WiXProjectFile lwdir = new WiXProjectFile(directoryOrFile);
                dirFile.AddChild(lwdir);

                foreach (string dir in Directory.GetDirectories(directoryOrFile))
                {
                    WiXProjectFile cwdir = new WiXProjectFile(dir);
                    ImportFiles(lwdir, dir, pattern);
                }
                foreach (string file in Directory.GetFiles(directoryOrFile))
                {
                    ImportFiles(lwdir, file, pattern);
                }
            }
            else if (File.Exists(directoryOrFile))
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(directoryOrFile, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    WiXProjectFile cwdir = new WiXProjectFile(directoryOrFile);
                    dirFile.AddChild(cwdir);
                }
            }
        }


        internal static void LoadDir(WiXProject.FileCollections f, string directory, string pattern)
        {
            if (Directory.Exists(directory))
            {//if directory
                WiXProjectFile wdir = new WiXProjectFile(directory);
                foreach (string dir in Directory.GetDirectories(directory))
                {
                    ImportFiles(wdir, dir, pattern);
                }
                foreach (string dir in Directory.GetFiles(directory))
                {
                    ImportFiles(wdir, dir, pattern);
                }
                f.AddRange(new WiXProjectFile[] { wdir });
            }
            else
            {
                if (File.Exists(directory))
                {//if file
                    if (System.Text.RegularExpressions.Regex.IsMatch(directory, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                    {
                        f.AddRange(new WiXProjectFile[] { new WiXProjectFile(directory) });
                    }
                }
            }
        }


        internal static void LoadDir(WiXDirectory xdir,WiXDirectoryComponent component,  
            string directory, string pattern, 
            string baseDir=null,
            WiXMatchEvaluator eval = null)
        {
            
            WiXDirectoryComponent comp = component ?? new WiXDirectoryComponent();
            List<string> v_ids = new List<string>();
            string v_id = string.Empty;
            string v_n = string.Empty;
            string v_s = string.Empty;
            string basedir = baseDir ?? directory;
          
                foreach (string f in Directory.GetFiles(directory))
                {
                    if (Regex.IsMatch(f, pattern, RegexOptions.IgnoreCase))
                    {
                        if ((eval != null) && (!eval.Invoke(f)))
                        {
                            continue;
                        }
                        v_id = Path.GetFileNameWithoutExtension(f);
                        v_n = Path.GetFileName(f);
                        //comp.Childs.Contains (
                        if (!v_ids.Contains(v_id))
                        {
                            v_s = f.Replace(basedir, "");
                            while (!string.IsNullOrEmpty(v_s) && v_s.StartsWith(Path.DirectorySeparatorChar.ToString()))
                            {
                                v_s = v_s.Substring(1);
                            }
                            WiXFileEntry c = new WiXFileEntry(v_id, v_n, v_s);
                            c.Id = "File_" + c.GetHashCode();
                            comp.Children.Add(c);
                            v_ids.Add(v_id);
                        }
                    }
                }
                
                foreach (string item in Directory.GetDirectories(directory))
                {
                   
                    v_n = Path.GetFileName(item);
                    if (Regex.IsMatch(v_n, "^([_]|[a-z])[a-z0-9_]*$", RegexOptions.IgnoreCase)
                        )
                    {
                        WiXDirectory wdir = xdir.AddDir(v_n, Path.GetFileName(item));
                        wdir.Id = "Dir_" + wdir.GetHashCode();
                        WiXDirectoryComponent v_component = new WiXDirectoryComponent();
                        wdir.Children.Add(v_component);

                        v_component.Guid = Guid.NewGuid().ToString();
                        //permissions
                        v_component.Children.Add(new WiXCreateFolder()
                        {
                            Id = null,
                            Directory = wdir.Id // "[" + this.InstallDirName + "]/"+dir.Id 
                        });

                        WiXFeatureEntry fet = xdir.GetDocument().GetFeature(0);
                        fet.Add(v_component);
                        LoadDir(wdir, v_component, item, pattern, basedir);
                      
                    }                   
                }
            
            //WiXFeatureEntry fet = xdir.GetDocument().GetFeature(0);
            //fet.Add(comp);

            //xdir.AddDir (
            //foreach (WiXProjectFile item in this.ProgramFiles)
            //{
            //    if (f.IsFile)
            //    {
            //        v_target = Path.Combine(v_tempDir,
            //            Path.GetFileName(item.FileName));
            //        File.Copy(item.FileName, v_target, true);
            //        component.Children.Add(new WiXFileEntry()
            //        {
            //            Source = WiXUtils.GetSourceDir(v_tempDir, v_target),
            //            Name = item.Id,
            //            DiskId = "1"
            //        });
            //    }
            //    else if (f.IsDirectory)
            //    {
            //       // BuildDirectory(d, v_dir.AddDir(item.Id, item.Id), Path.Combine(v_tempDir, "__ProgramFiles"), item);
            //    }
            //}
        }
    }
    
}
