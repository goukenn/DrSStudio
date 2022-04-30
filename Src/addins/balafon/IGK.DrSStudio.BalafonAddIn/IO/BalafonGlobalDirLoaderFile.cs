using IGK.ICore.IO;
using System;
using System.Collections.Generic;
using System.IO;

namespace IGK.DrSStudio.Balafon.IO
{
    using IGK.ICore.Xml;
    internal class BalafonGlobalDirLoaderFile : IBalafonProjectFolderLoader
    {
        public void LoadFolder(string rootFolder, string dir, IBalafonProject pg)
        {
            Stack<string> dirs = new Stack<string> ();//Directory.GetDirectories(dir));
           // var g = pg.AddFolder(Path.GetFileName(dir));             
            //foreach( string gdir in Directory.GetDirectories(dir)){
            //    dirs.Enqueue (
            //} 
           dirs.Push (dir);
            IBalafonProjectItem g = null;
            while (dirs.Count > 0)
            {
                dir = dirs.Pop();
                g = pg.AddFolder(dir);
                foreach (string ss in Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly))
                {
                   var f = pg.AddFile(ss, enuBalafonItemType.Include);
                    if (f != null)
                    {
                        f.Parent = g;
                    }
                    //if (f is CoreXmlElement)
                    //    (f as CoreXmlElement)["File"] = PathUtils.GetRelativePath (rootFolder, ss);
                }
                //push directory
                foreach (string d in
                   Directory.GetDirectories(dir)) {
                    dirs.Push (d);
                }
         
            }

           
        }
    }
}