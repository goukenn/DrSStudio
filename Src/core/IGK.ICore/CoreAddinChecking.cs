

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAddinChecking.cs
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
file:CoreAddinChecking.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Reflection;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI;
    using IGK.ICore.IO;


    /*
     * 
     * this class is used to check every assembly in this before load to main domain
     * */
    /// <summary>
    /// represent the core adding checking
    /// </summary>
    [Serializable ()]
    internal class CoreAddinChecking
    {
        CoreSystem m_coreSystem;
        static string sm_asmDirLocation;
        string[] inifiles;
        string[] files;
        public string[] Addins { get { return files; } }
        internal static string AsmDirectoryLocation { get { return sm_asmDirLocation; } }
        /// <summary>
        /// default constructor
        /// </summary>
        /// <param name="t"></param>
        /// <param name="files"></param>
        public CoreAddinChecking(CoreSystem t,  params string[] files)
        {
            inifiles = files;
            m_coreSystem = t;
            if ((files != null) && (files.Length > 0))
            {
                Check(files);
            }
        }
        void Check(string[] assembly)
        {
            if (AppDomain.CurrentDomain.IsDefaultAppDomain())
                return;
#if DEBUG
            CoreLog.WriteDebug("Check Requested assembly on Domain : " + AppDomain.CurrentDomain.FriendlyName);
#endif 
            List<string > m_outfile = new List<string> ();
            Assembly v_asm = null;
            regDomainEvent();
            CoreAddInAttribute v_attr =null;
            foreach (string asm in assembly)
            {
                try
                {
#if DEBUG
                    if (asm.ToLower ().EndsWith("vshost.exe"))
                        continue;
#endif
                    v_asm = Assembly.LoadFile(asm);
                    sm_asmDirLocation = PathUtils.GetDirectoryName(v_asm.Location);
                    v_attr = CoreAddInAttribute.GetAttribute(v_asm);
                    if (v_attr == null)
                    {
                        continue;
                    }
                    if (v_attr.CheckingType != null)
                    {
                        MethodInfo m = v_attr.CheckingType.GetMethod(
                            CoreConstant.METHOD_CHECKADDING, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                            null,
                            new Type[] { typeof(bool) },
                            null);
                        if (m != null)
                        {
                            bool b = (bool)m.Invoke(null, new object[] { false });
                            if (!b)
                            {
                                //don't register method adding
                                continue;
                            }
                        }
                        else
                        {
#if DEBUG
                            CoreMessageBox.Show("Checking Method not found");
#endif
                            continue;
                        }
                    }
                }
#pragma warning disable
                catch (BadImageFormatException ex)
                {
                    continue;
                }
                catch(Exception ex)
                {
                    CoreLog.WriteDebug("Error When loading ... " + asm + " : " + ex.Message);
                    continue;
                }
                try{
                    this.m_coreSystem.LoadAssembly (v_asm.Location, v_asm .GetName ());
                    m_outfile.Add(asm);
                }
                catch (Exception vx ){
                    CoreLog.WriteError(string.Format("Exceptiion:"+vx.Message));
                }
            }    
            this.files  = m_outfile.ToArray ();
            unregDomainEvent();
        }

        private void regDomainEvent()
        {
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve += _AssemblyResolve;
        }
        private void unregDomainEvent()
        {
            AppDomain.CurrentDomain.AssemblyLoad -= CurrentDomain_AssemblyLoad;
            AppDomain.CurrentDomain.TypeResolve -= CurrentDomain_TypeResolve;
            AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve -= _AssemblyResolve;
        }
        Assembly _AssemblyResolve(object sender, ResolveEventArgs args)
                {
           //load required assembly
            CoreSystemAssemblyResolver resolver = new CoreSystemAssemblyResolver(
                        new string[] { 
                            AsmDirectoryLocation,
                            AsmDirectoryLocation+"/../"
                        });
            Assembly v_asm = resolver.Resolve(args.Name);
            if (v_asm != null)
                return v_asm;
            return null;
        }
        void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
        }
        Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
        }
    }
}

