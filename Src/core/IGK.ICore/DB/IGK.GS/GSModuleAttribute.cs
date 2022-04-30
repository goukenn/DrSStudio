using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.GS
{
    /// <summary>
    /// represent a gs module attribute
    /// </summary>
    [AttributeUsage (AttributeTargets.Assembly, AllowMultiple=false , Inherited = false  )]
    /// <summary>
    /// get  the module attribute
    /// </summary>
    public class GSModuleAttribute : Attribute 
    {
        private string m_ModuleName;
        private string m_Description;
        private string m_Author;
        /// <summary>
        /// get or set the author of the library
        /// </summary>
        public string Author
        {
            get { return m_Author; }
            set
            {
                if (m_Author != value)
                {
                    m_Author = value;
                }
            }
        }
        /// <summary>
        /// get or set the description
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
        public string ModuleName
        {
            get { return m_ModuleName; }
        }
        /// <summary>
        /// .ctr
        /// </summary>
        /// <param name="ModuleName">the module name</param>
        /// <param nam ="initType" > type used to initialize the module</param>
        public GSModuleAttribute(string ModuleName, Type iniType)
        {
            this.m_ModuleName = ModuleName;
            if ((iniType == null) ||  !iniType.ImplementInterface(typeof(IGSModule)))
            {
                throw new ArgumentException("not valid");
            }
            this.m_moduleType = iniType;
        }
        private Type m_moduleType;
        /// <summary>
        /// get the type that will initialize the module
        /// </summary>
        public Type ModuleType
        {
            get { return m_moduleType; }
        }
    }
}
