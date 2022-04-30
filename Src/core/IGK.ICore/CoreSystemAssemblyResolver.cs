

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreSystemAssemblyResolver.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreSystemAssemblyResolver.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
namespace IGK.ICore
{
    /// <summary>
    /// represent a resolver used to resolve not founded assembly
    /// </summary>
    public class CoreSystemAssemblyResolver
    {
        List<string> m_searchPath;
        /// <summary>
        /// add search assembly path
        /// </summary>
        /// <param name="path"></param>
        public void AddSearchPath(string path)
        {
            this.m_searchPath.Add(path);
        }
        /// <summary>
        /// Represent a search path 
        /// </summary>
        /// <param name="searchPath"></param>
        public CoreSystemAssemblyResolver(string[] searchPath)
        {
            if ((searchPath == null) || (searchPath.Length == 0))
                searchPath = new string[] { "." };
            m_searchPath= new List<string> ();
            m_searchPath.AddRange (searchPath);
        }
        public System.Reflection.Assembly Resolve(string asmFullName)
        {
            AssemblyName n = new AssemblyName(asmFullName);
            string dllfile =null;
            foreach (string item in this.m_searchPath )
            {
                if (Directory.Exists(item))
                {
                    string[] d = Directory.GetFiles (item, n.Name+  ".dll", SearchOption.AllDirectories );
                    if (d.Length == 0)
                    {
                        d = Directory.GetFiles(item, n.Name + ".exe", SearchOption.AllDirectories);
                        if (d.Length == 0)
                            continue;
                    }
                    if (d.Length == 1)
                    {
                        if (File.Exists(d[0]))
                        {
                            return Assembly.LoadFile(d[0]);
                        }
                    }
                    dllfile = item + "\\" + n.Name + ".dll";
                    if (File.Exists (dllfile ))
                        return Assembly.LoadFile(dllfile );
                    dllfile = item + "\\" + n.Name + ".exe";
                    if (File.Exists (dllfile ))
                        return Assembly.LoadFile(dllfile );
                }
            }
            string h = string.Empty;
            foreach (System.Reflection.Assembly v_asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                h = v_asm.FullName.ToLower().Split(',')[0].Trim();
                if (h.ToLower() == n.Name.ToLower ())
                {
                    return v_asm;
                }
            }
            return null;
        }
    }
}

