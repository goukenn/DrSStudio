

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreControlFactory.cs
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
file:CoreControlFactory.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class CoreControlFactory
    {
        static  Dictionary<string, Type> sm_load;
        static Type sm_controlType;
        static CoreControlFactory() {
            sm_load = new Dictionary<string, Type>();
            LoadControlType();
        }
        static void LoadControlType()
        {
            AppDomain.CurrentDomain.TypeResolve += CurrentDomain_TypeResolve;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
            Type v_type = AppDomain.CurrentDomain.FindType ("System.Windows.Forms.dll", "System.Windows.Forms.Control");
            if (v_type != null)
            {
                sm_controlType = v_type ;
                foreach (System.Reflection.Assembly item in AppDomain.CurrentDomain.GetAssemblies())
                {
                    __loadAssembly(item);
                }
            }
            AppDomain.CurrentDomain.TypeResolve -= CurrentDomain_TypeResolve;
            AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            CoreLog.WriteDebug("Assembly load : " + args.LoadedAssembly);
            __loadAssembly(args.LoadedAssembly);
        }

        private static void __loadAssembly(System.Reflection.Assembly assembly)
        {
            if (sm_controlType == null)
            {
                return;
            }
            try
            {
                foreach (Type t in assembly.GetTypes())
                {
                    try{



                        if (t.Name == "IGKWinCoreStatusText") {
                    }
                        if (t.IsSubclassOf(sm_controlType))
                        {
                            if (sm_load.ContainsKey(t.Name))
                                continue;
                            if ((Attribute.GetCustomAttribute(t, typeof(CoreRegistrableControlAttribute)) is CoreRegistrableControlAttribute cr) && (cr.IsRegistrable))
                            {
                                sm_load.Add((cr.Name ?? t.Name).ToLower(), t);
                            }
                        }
                    }
                    catch
                    {
                        CoreLog.WriteError("Load control failed on type : " + t.Name);
                    }
                }
            }
            catch (TypeLoadException ex)
            {
                CoreLog.WriteError("Load Control failed on Assembly : " + assembly.FullName + " " + ex.Message);
            }
            catch
            {
            }
        }
        static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        static System.Reflection.Assembly CurrentDomain_TypeResolve(object sender, ResolveEventArgs args)
        {
            return null;
        }
        /// <summary>
        /// create a control by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object  CreateControl(string name)
        {
            Type T = FindControlType(name);
            if (T!=null)         
                return T.Assembly.CreateInstance(T.FullName);
            return null;
        }
        public static object CreateControl<T>()
        {
            return CreateControl(typeof(T).Name);
        }
        /// create a control by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object CreateControl(string name, object[] ctrParams )
        {
            Type T = FindControlType(name);
            if (T != null)
            {
                if (ctrParams !=null) {
                    Type[] v_t = new Type[ ctrParams.Length ];
                    for (int i = 0; i < ctrParams.Length; i++)
			        {
                                if (ctrParams !=null)
                                v_t[i] = ctrParams.GetType ();
			        }
                    return T.GetConstructor (v_t ).Invoke(ctrParams ) ;
                }
                return T.Assembly.CreateInstance(T.FullName);
            }
            return null;
        }
        private static Type FindControlType(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            string v_name =name.ToLower();
            Type t = null;
            if (sm_load.ContainsKey(v_name))
            {
                t = sm_load[v_name];
            }
            else
            {
                t = Type.GetType(v_name, false, true );
                Type q = null;
                if (t == null)
                {//search in assembly for type
                    foreach (System.Reflection.Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        if (asm == System.Reflection.Assembly.GetExecutingAssembly())
                            continue;
                        if ((q = asm.GetType(v_name, false, true)) != null)
                        {
                            t = q;
                            break;
                        }
                    }
                }
                if (t == null)
                {
                    if (sm_load.ContainsKey(v_name))
                    {
                        t = sm_load[v_name];
                    }
                    else
                        return null;
                }
                else
                {
                    if (!sm_load.ContainsKey(v_name))
                    {
                        sm_load.Add(v_name.ToLower(), t);
                    }
                }
            }
            return t;
        }
        public static void RegisterControl(string name, Type type)
        {
            string key = name.ToLower();
            if (sm_load.ContainsKey(name) || sm_load .ContainsKey (key)  || (type == null) )
                return;
            sm_load.Add(key, type);
        }
    }
}

