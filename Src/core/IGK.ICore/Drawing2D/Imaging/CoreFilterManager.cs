

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreFilterManager.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.Drawing2D.Imaging
{
    /// <summary>
    /// represent a filter manager
    /// </summary>
    public sealed class CoreFilterManager
    {
        private static Dictionary<string, ICore2DDrawingFilter> sm_filter;

        static CoreFilterManager() {
            sm_filter = new Dictionary<string, ICore2DDrawingFilter>();
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
            InitFilters();
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        static Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs e)
        {
            LoadFilters(e.LoadedAssembly);
        }

        private static void LoadFilters(Assembly assembly)
        {
            if (assembly == null) return;

            try
            {
                Type[] b = assembly.GetTypes();
                foreach (Type item in b)
                {
                    if (item == typeof(ICore2DDrawingFilter))
                        continue;
                    if (item.IsClass && !item.IsAbstract && item.ImpletementInterface(typeof(ICore2DDrawingFilter)))
                    {
                        ICore2DDrawingFilter filter = item.Assembly.CreateInstance(item.FullName)
                            as ICore2DDrawingFilter;
                        if ((filter != null) && (!sm_filter.ContainsKey(filter.Name)))
                        {
                            sm_filter.Add(filter.Name, filter);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(ex.Message);
            }
        }

        private static void InitFilters()
        {
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                LoadFilters(asm);
            }
        }
        /// <summary>
        /// get the registrated filters
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ICore2DDrawingFilter GetFilter(string name)
        {
            if (sm_filter.ContainsKey(name))
            {
                return sm_filter[name];
            }
            return null;
        }
        /// <summary>
        /// get the filter list
        /// </summary>
        /// <returns></returns>
        public static string[] GetFilterList() {
            return sm_filter.Keys.ToArray<string>();
        }
        
    }
}
