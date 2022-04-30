

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreWorkingProjectAttribute.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:CoreWorkingProjectAttribute.cs
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
namespace IGK.ICore
{

    /// <summary>
    /// Represent a ICore Project attribute.
    /// Project is a container of a list of ProjectTemplate.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly ,
        Inherited=false, 
        AllowMultiple =true )]    
    public class CoreWorkingProjectTemplateAttribute : Attribute 
    {
        private string m_Name;
        private string m_Assembly;
        private Type m_TargetSurfaceType;
        private string m_Params;
        public string Params
        {
            get { return m_Params; }
            set
            {
                if (m_Params != value)
                {
                    m_Params = value;
                }
            }
        }
        private string m_ImageKey;
        private string m_Description;
        private Type m_ConfigType;
        private string m_Group;

        public string Group
        {
            get { return m_Group; }
            set
            {
                if (m_Group != value)
                {
                    m_Group = value;
                }
            }
        }

        /// <summary>
        /// get or set the config type. must implement a ICoreWorkingProjectConfiguration interface
        /// </summary>
        public Type ConfigType
        {
            get { return m_ConfigType; }
            set
            {
                if (m_ConfigType != value)
                {
                    m_ConfigType = value;
                }
            }
        }
        /// <summary>
        /// get or set the description of the project
        /// </summary>
        public string Description
        {
            get { return m_Description; }
            set
            {
                if (m_Description != value)
                {
                    m_Description = value;
                }
            }
        }
        /// <summary>
        /// get or set the image key
        /// </summary>
        public string ImageKey
        {
            get { return m_ImageKey; }
            set
            {
                if (m_ImageKey != value)
                {
                    m_ImageKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the target surface type
        /// </summary>
        public Type TargetSurfaceType
        {
            get { return m_TargetSurfaceType; }
            set
            {
                if (m_TargetSurfaceType != value)
                {
                    m_TargetSurfaceType = value;
                }
            }
        }

        /// <summary>
        /// get or set the full name of the assembly
        /// </summary>
        public string Assembly
        {
            get { return m_Assembly; }
            set
            {
                if (m_Assembly != value)
                {
                    m_Assembly = value;
                }
            }
        }
        
        /// <summary>
        /// get or set the name of the project
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set
            {
                if (m_Name != value)
                {
                    m_Name = value;
                }
            }
        }

        public CoreWorkingProjectTemplateAttribute()
        {

        }
        /// <summary>
        /// .Ctr
        /// </summary>
        public CoreWorkingProjectTemplateAttribute(string name)
        {
            this.m_Name = name;
        }
        static Dictionary<string, CoreWorkingProjectTemplateAttribute> sm_pname;

        static CoreWorkingProjectTemplateAttribute() { 
            //load all projects
     
            foreach(Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                CheckAssembly(asm);
            }
            AppDomain.CurrentDomain.AssemblyLoad += CurrentDomain_AssemblyLoad;
        }

        private static void CheckAssembly(Assembly asm)
        {
            var t = typeof(CoreWorkingProjectTemplateAttribute);
            CoreWorkingProjectTemplateAttribute[] s = GetCustomAttributes(asm, t) as CoreWorkingProjectTemplateAttribute[];
            if (s.Length > 0)
            {
                //register
                RegisterProject(asm, s);
            }
        }

        static void CurrentDomain_AssemblyLoad(object sender, AssemblyLoadEventArgs args)
        {
            CheckAssembly(args.LoadedAssembly);
        }

        /// <summary>
        /// register project to function
        /// </summary>
        /// <param name="project"></param>
        public static void RegisterProject(Assembly asm , params CoreWorkingProjectTemplateAttribute[] project)
        {
            if (sm_pname == null)
                sm_pname = new Dictionary<string, CoreWorkingProjectTemplateAttribute>();

            if ((project == null) || (project.Length == 0)) return;
            foreach (CoreWorkingProjectTemplateAttribute item in project)
            {
                if (sm_pname.ContainsKey(item.Name))
                {
                    CoreLog.WriteLine("Project Already register ["+ item.Name+"]" );
                    continue;
                }                
                sm_pname.Add(item.Name, item);
                item.Assembly = asm.FullName ;
              //var g =   CoreSystem.GetAddIns().GetAssemblyFromFullName(item.Assembly );

               // Assembly asm = /*AssemblyName.GetAssemblyName (item.Assembly)*/.FullName ;
            }

            
        }
        public static CoreWorkingProjectTemplateAttribute[] GetAllProjects()
        {
            List<CoreWorkingProjectTemplateAttribute> p = new List<CoreWorkingProjectTemplateAttribute>();
            if (sm_pname != null) {
                foreach (var item in sm_pname.Values)
                {
                    p.Add(item);
                }
                return p.ToArray();
            }
            return null;
        }
        /// <summary>
        /// get the project by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CoreWorkingProjectTemplateAttribute GetProject(string name)
        {
            if (sm_pname.ContainsKey(name))
                return sm_pname[name];
            return null;
        }
        /// <summary>
        /// get the first childs of this working attribute
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static CoreWorkingProjectTemplateAttribute[] GetChilds(string name)
        {
            if (!sm_pname.ContainsKey(name))
            {
                return null;
            }
            List<CoreWorkingProjectTemplateAttribute> t = new List<CoreWorkingProjectTemplateAttribute>();
            foreach (string item in sm_pname.Keys)
            {
                if ((item != name) && Path.GetFileNameWithoutExtension(item) == name)
                {
                    t.Add(sm_pname[item]);
                }
            }

            return t.ToArray();
        }

        public ICoreInitializatorParam GetInitializationParams()
        {
            if (string.IsNullOrEmpty(this.Params))
                return null;
            CoreInitializationParam p = new CoreInitializationParam(this.Params);
            return p;
        }
    }
}

