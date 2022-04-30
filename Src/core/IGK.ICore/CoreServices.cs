

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreServices.cs
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
file:CoreServices.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.Resources;
    using System.Text.RegularExpressions;
    /// <summary>
    /// represent a core service utility
    /// </summary>
    public class CoreServices
    {
        /// <summary>
        /// get resouces by name
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static ICoreResourceItem GetRes(string p)
        {
            return null;
            /*
            ICoreWorkingResourcesContainerSurface v_cs = CoreRegister.CurrentSurface as ICoreWorkingResourcesContainerSurface;
            ICoreWorkingResourcesContainerSurface v_rcs = null;
            if (v_cs ==null)
                return null;
             ICoreResourceItem rs = CoreResourceFileManager.GetRes(v_cs, p);
             if (rs != null)
                 return rs;
             foreach (ICoreWorkingSurface s in CoreRegister.Workbench.Surfaces)
             {
                 if ((s == v_cs) || !(s is ICoreWorkingResourcesContainerSurface))
                     continue;
                 v_rcs = s as ICoreWorkingResourcesContainerSurface;
                 if (v_rcs.Resources.Contains(p))
                 {
                     rs =  v_rcs.Resources.GetRes(p);
                     if (v_cs.Resources.Register(rs))
                     {
                         return rs;
                     }
                 }
             }
             return null;
            */
        }
        /// <summary>
        /// analyse and return the pattern string
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string GetString(string pattern)
        {
            string v_str = string.Empty;
            Regex rg = new Regex(@"\[[a-z\.]+\]", RegexOptions.IgnoreCase );
            MatchEvaluator e = (m) => {
                return CoreSystem.GetString (m.Value);
            } ;
            v_str = rg.Replace(pattern, e);
            return v_str;
        }
        /// <summary>
        /// utility to retreive an application service
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public static T GetApplicationService<T>() where T : ICoreApplicationService 
        {
            ICoreApplicationServices t = CoreApplicationManager.Application as ICoreApplicationServices;
            if (t != null) {
                return t.GetService<T>();
            }
            return default (T);
        }
    }
}

