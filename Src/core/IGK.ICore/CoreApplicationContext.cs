

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreApplicationContext.cs
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
file:CoreApplicationContext.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// Represent a application context. passed when application initialize
    /// </summary>
    public class CoreApplicationContext : MarshalByRefObject 
    {
        Dictionary<string, ICoreApplicationProjectInfo> m_project;
        private static CoreApplicationContext sm_instance;

        public string StartupFolder {
            get {
                return CoreApplicationManager.Application.StartupPath;
            }
        }
        public bool IsAssemblyLoaded(string name)
        {
            return false;
        }
        
        private CoreApplicationContext()
        {
            m_project = new Dictionary<string, ICoreApplicationProjectInfo>();
        }
        public static CoreApplicationContext Instance
        {
            get
            {
                return sm_instance;
            }
        }
        static CoreApplicationContext()
        {
            sm_instance = new CoreApplicationContext();
        }
        public bool RegisterProject(string name, ICoreApplicationProjectInfo projectInfo) {
            if (string.IsNullOrEmpty(name) || (this.m_project.ContainsKey(name)))
                return false ;
            this.m_project.Add (name, projectInfo  );
            return true;
        }
        public bool UnregisterProject(string name) {
            if (!this.m_project.ContainsKey(name))
            {
                return false;
            }
            this.m_project.Remove(name);
            return true;
        }
    }
}

