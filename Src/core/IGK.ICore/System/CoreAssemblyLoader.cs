

using IGK.ICore;using IGK.ICore.IO;
/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreAssemblyLoader.cs
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
file:CoreAssemblyLoader.cs
*/
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// used to load assembly
    /// </summary>
    public sealed class CoreAssemblyLoader
    {
        private string m_directory;
        private CoreSystem m_coresystem;
        private List<string> m_directories;
        private List<string> m_searchDir;

        public void AddSearchDir(string dir){
            if (string.IsNullOrEmpty (dir) || 
                m_directories.Contains(dir)||
                this.m_searchDir.Contains (dir))
                return;
            this.m_searchDir.Add(dir);
        }
        /// <summary>
        /// /start directory
        /// </summary>
        /// <param name="sys"></param>
        /// <param name="directory"></param>
        internal CoreAssemblyLoader(CoreSystem sys, string directory)
        {
            this.m_directory = directory;
            this.m_coresystem = sys;
            this.m_searchDir = new List<string>();
            this.m_directories = new List<string>();
            this.m_directories.Add(this.m_directory);
            AppDomain.CurrentDomain.AssemblyResolve += (o, e) =>
            {
                return ResolvAssembly(e.Name);
            };
#if !__ANDROID__
            Environment.SetEnvironmentVariable("Path", ";" + string.Join(
                ";", new string[]{
                    Environment.GetEnvironmentVariable("Path"),
                    Path.Combine(directory , "AddIn"),
                    Path.Combine(directory , "Lib")

                }));
#else
            //load internal 
            foreach(Assembly _as in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (CoreAddInAttribute.IsAddIn(_as))
                {
                    this.__InitAssembly(_as);
                }
            }
#endif


            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
        }

        Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        private void LoadDirectory(string directory)
        {

            if (Directory.Exists(directory) == false)
                return;
            List<string> m = new List<string>();
            m.AddRange(System.IO.Directory.GetFiles(directory, "*.dll"));
            m.AddRange(System.IO.Directory.GetFiles(directory, "*.exe"));
            string v_dir = Path.Combine(directory, CoreConstant.ADDIN_FOLDER);
            if (Directory.Exists(v_dir))
            {
                m.AddRange(Directory.GetFiles(v_dir, "*.dll"));
                m.AddRange(Directory.GetFiles(v_dir, "*.exe"));
                m_directories.Add(v_dir);
            }
            if (m.Count > 0)
            {
                CoreApplicationManager.Application.OnPrefilterAssemblyList(m);
                InitAssemblies(m.ToArray());
            }
        }
        internal void Load()
        {
            string[] t = this.m_directories.ToArray ();
            foreach (string item in t )
            {
                this.LoadDirectory(item);
            }          
        }
        /// <summary>
        /// initialize assembly list. IN OTHER DOMAIN
        /// </summary>
        /// <param name="assemblyList"></param>
        private void InitAssemblies(string[] assemblyList)
        {
            if (
                AppDomain.CurrentDomain.FriendlyName ==
                CoreConstant.CHECK_ASSEMBLY_DOMAINNAME)
                return;

#if DEBUG
            AssemblyLoadEventHandler _viewDomainLaoaded = (o, e) =>
            {
                CoreLog.WriteLine(e.LoadedAssembly.FullName);
            };
            AppDomain.CurrentDomain.AssemblyLoad += _viewDomainLaoaded;
#endif
            string[] m_files = assemblyList;
            AppDomainSetup v_setupDomain = new AppDomainSetup();
            v_setupDomain.ApplicationBase = PathUtils.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            AppDomain d = AppDomain.CreateDomain(CoreConstant.CHECK_ASSEMBLY_DOMAINNAME, AppDomain.CurrentDomain.Evidence, v_setupDomain);
          
            ObjectHandle obj = d.CreateInstance(
                GetType().Assembly.GetName().FullName,
                typeof(CoreAddinChecking).FullName,
                true,
                 System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public,
                  null,
                  new object[] { CoreSystem.Instance, m_files },
                  Thread.CurrentThread.CurrentCulture,
                  null);

            CoreAddinChecking v_c = (CoreAddinChecking)obj.Unwrap();
            String[] v_l = v_c.Addins;
            AppDomain.Unload(d);
            Assembly item = null;
            //init loaded already loaded assembly
            foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                __InitAssembly(asm);
            }
            if (v_l != null)
            {
                foreach (string i in v_l)
                {
                    //get loaded assembly
                    try
                    {
                        item = Assembly.LoadFile(i);
                        __InitAssembly(item);
                    }
                    catch (Exception ex)
                    {
                        CoreLog.WriteDebug(ex.Message);
                    }
                }
            }
#if DEBUG
            AppDomain.CurrentDomain.AssemblyLoad -= _viewDomainLaoaded;
#endif  
        }
        private void __InitAssembly(Assembly item)
        {
            if (m_coresystem.IsLoadedAssembly(item.FullName))
                return;
            CoreAddInAttribute v_addinAttr = null;
            v_addinAttr = CoreAddInAttribute.GetAttribute(item);
            if (v_addinAttr == null)
            {
                CoreLog.WriteLine("[IGK.ICore] - Assembly is not a valid CoreAddIn [ " + item+ " ]");
                return;
            }
            ICoreInitializer v_inittype = null;
            if (v_addinAttr.Initializer != null)
            {
                v_inittype = v_addinAttr.Initializer.Assembly.CreateInstance(v_addinAttr.Initializer.FullName) as ICoreInitializer;
                if (v_inittype != null)
                {
                    CoreAddInInitializerAttribute cp =
                        CoreAddInInitializerAttribute.GetCustomAttribute(v_addinAttr.Initializer,
                        typeof(CoreAddInInitializerAttribute)) as CoreAddInInitializerAttribute;
                    if ((cp != null) && (cp.Initializer))
                    {
                        if (!v_inittype.Initialize(CoreApplicationContext.Instance))
                        {
                            CoreLog.WriteLine("Initialations failed..." + v_inittype.GetType() + " : " + v_inittype.GetType().Assembly.Location);
                        }
                    }
                }
                else
                {
                    CoreLog.WriteDebug(":::::::::::No InitailiaizationFile inserted");
                }
            }
            InitAssembly(item);
            m_coresystem.m_addins.Add(new CoreAddInItem((CoreAddInAttribute)
            Attribute.GetCustomAttribute(item, typeof(CoreAddInAttribute)), item));
            if (!m_coresystem.m_loadedAssembly.ContainsKey(item.FullName))
            {
                m_coresystem.m_loadedAssembly.Add(item.FullName, item);
            }
#if DEBUG
            else {
                CoreMessageBox.Show("[IGK.ICore] - Assembly already loaded");
            }
#endif
        }
        /// <summary>
        /// init addin asembly
        /// </summary>
        /// <param name="assembly"></param>
        private void InitAssembly(Assembly assembly)
        {
            if (this.m_coresystem.m_asmLoaderHandler != null)
                this.m_coresystem.m_asmLoaderHandler(assembly);
            if (this.m_coresystem.m_typeLoader == null)
                return;

            AppDomain.CurrentDomain.TypeResolve += _initAssemblyTypeResolv;
            AppDomain.CurrentDomain.AssemblyResolve += _initAssemblyTypeResolv;

            Type[] t = null;
            try
            {
                t = assembly.GetTypes();
                foreach (Type item in t)
                {
                    try
                    {
                        this.m_coresystem.m_typeLoader(item);
                    }

                    catch (ReflectionTypeLoadException ex)
                    {
                        string msg =
                        string.Format(ex.Message + " \nInformations:\n"
                        + "\nNombres de type charger dans l'assembly : {0}\n\nNombre d'exception{1}",
                        ex.Types.Length.ToString(), ex.LoaderExceptions.Length);
                        CoreMessageBox.Show(msg, "title.CoreAssemblyLoaderException".R());
                    }
                    catch (Exception ex)
                    {
                        CoreLog.WriteLine("Exception raised : " + ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                CoreLog.WriteLine(assembly.FullName + " " +ex.Message);
                CoreMessageBox.Show(assembly.FullName  + " "+ex.Message);
            }
            AppDomain.CurrentDomain.TypeResolve -= _initAssemblyTypeResolv;
            AppDomain.CurrentDomain.AssemblyResolve -= _initAssemblyTypeResolv;
        }

        private Assembly _initAssemblyTypeResolv(object sender, ResolveEventArgs args)
        {
            return ResolvAssembly(args.Name.Split(',')[0]);
        }
       
        public Assembly ResolvAssembly(string asmName)
        {
            CoreLog.WriteDebug("CoreAssemblyLoader::ResolvAssembly -> " + asmName);
            List<string> m = new List<string>();
            foreach (var item in this.m_directories)
            {
                m.AddRange(
                     new string[]{item,
                    Path.Combine(item , "AddIn"),
                    Path.Combine(item , "Lib")});
            }
            if ((this.m_searchDir != null) && (this.m_searchDir.Count > 0))
            {
                m.AddRange(this.m_searchDir.ToArray());
            }
            CoreSystemAssemblyResolver resolver = new CoreSystemAssemblyResolver(m.ToArray());
            Assembly asm = resolver.Resolve(asmName);
            if (asm != null)
            {
                CoreLog.WriteDebug("[IGK] : Assembly loaded " + asmName);
            }
            return asm;
        }
        /// <summary>
        /// demand to load directory 
        /// </summary>
        /// <param name="p"></param>
        public void LoadDir(string directory)
        {
            if (!Directory.Exists(directory) || this.m_directories.Contains(directory))
                return;
            this.LoadDirectory(directory);
            this.m_directories.Add(directory);
        }
    }
}

