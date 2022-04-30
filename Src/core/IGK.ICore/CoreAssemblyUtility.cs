

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAssemblyUtility.cs
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

namespace IGK.ICore
{
    public static  class CoreAssemblyUtility
    {
        /// <summary>
        /// fin loaded assembly on current domain
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Assembly[] FindAssembly(string name)
        {
            return FindAssembly(name, AppDomain.CurrentDomain);
        }
        /// <summary>
        /// fin loaded assembly on current domain
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Assembly[] FindAssembly(string name, AppDomain domain)
        {
            List<Assembly> lasm = new List<Assembly>();
            name = name.ToLower();
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (asm.FullName.Split(',')[0].ToLower() == name)
                {
                    lasm.Add(asm);
                }
            }
            return lasm.ToArray();
        }
        /// <summary>
        /// find type in loaded assembly
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Type FindType(string name)
        {
            
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type t = asm.GetType(name, false, true);
                if (t != null)
                    return t;
            }
             return null;
        }
    }
}
