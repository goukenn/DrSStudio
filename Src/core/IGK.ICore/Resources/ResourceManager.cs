

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ResourceManager.cs
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
file:ResourceManager.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace IGK.ICore.Resources
{
    using IGK.ICore;using IGK.ICore.Drawing2D;
    using IGK.ICore.IO;
    using IGK.ICore.WinUI ;
    /// <summary>
    /// represent a resource file manager
    /// </summary>
    public sealed class ResourceFileManager
    {
        private static ResourceFileManager sm_instance;
        List<ICoreWorkingResourcesContainerSurface> m_dic;
        string m_targetDir;
        private ResourceFileManager()
        {
            m_dic = new List<ICoreWorkingResourcesContainerSurface>();
            m_targetDir = IGK.ICore.IO.PathUtils.GetPath("%startup%/Temp/Res");
        }
        internal  static ResourceFileManager Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static ResourceFileManager()
        {
            sm_instance = new ResourceFileManager();
            CoreApplicationManager.Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            Instance.ClearDir();
        }
        /// <summary>
        /// clear dir
        /// </summary>
        private void ClearDir()
        {
            try
            {
                this.m_dic.Clear();
                Directory.Delete(this.m_targetDir, true);
            }
            catch { 
            }
        }
        public static ICoreResourceItem GetRes(ICoreWorkingResourcesContainerSurface surface, string name)
        {
            if (surface == null)
                return null;
            return surface.Resources.GetResourceById(name);
        }
        public static void Register(ICoreWorkingResourcesContainerSurface surface, ICoreResourceItem item)
        {
            if (surface ==null )
                return ;
            ICoreTextureResource tx = item as ICoreTextureResource;
            if (!Instance.m_dic.Contains(surface ))
            {
                sm_instance.m_dic.Add(surface );
            }
            //if (!Instance.m_dic.Contains (surface) && (surface is ICoreWorkingResourcesContainerSurface))
            //{
            //    sm_instance.m_dic.Add(surface, new Dictionary<string, ICoreResourceItem>());
            //}
            //if ((tx != null) && !Instance.m_dic[surface] .ContainsKey (item.Id))
            //{
            //    //if (PathUtils.GetDirectoryName(tx.Source).ToLower() != Instance.m_targetDir.ToLower())
            //    //{ 
            //    //    string destdir = Instance.m_targetDir+ "/"+ Path.GetFileName(tx.Source );
            //    //    if (PathUtils.CreateDir(PathUtils.GetDirectoryName(destdir)))
            //    //    {
            //    //        File.Copy(tx.Source, destdir);
            //    //        tx.Source = destdir;
            //    //    }
            //    //}
                surface.Resources.Register(item);
            //}
        }
        public static ICoreTextureResource CreateTextureRes(ICoreWorkingResourcesContainerSurface  target , string filename, string name, ICoreBitmap bitmap)
        {
            if (target  == null)
                return null;
            if (!Instance.m_dic.Contains(target ))
            {
                sm_instance.m_dic.Add(target);
            }
            if (target.Resources.Contains(name))
                return target.Resources.GetResourceById (name) as ICoreTextureResource ;
            TextureResource c = new TextureResource(
                    Path.GetFullPath(filename),
                    name,
                    bitmap);
            Register(target,c);
            return c;
        }
    }
}

