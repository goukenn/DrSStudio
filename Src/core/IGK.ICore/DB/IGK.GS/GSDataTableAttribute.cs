using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IGK.GS
{
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Interface  )]
    public class GSDataTableAttribute : Attribute 
    {
        private string m_Name;
        private string m_Description;
        private Type m_Initializer;
        private string m_IdentifierKey;
        /// <summary>
        /// get or set the identifier default key
        /// </summary>
        public string IdentifierKey
        {
            get { return m_IdentifierKey; }
            set
            {
                if (m_IdentifierKey != value)
                {
                    m_IdentifierKey = value;
                }
            }
        }
        /// <summary>
        /// get or set the type that will be used to initialize the table
        /// </summary>
        public Type Initializer
        {
            get { return m_Initializer; }
            set
            {
                
                if (m_Initializer != value)
                {
                    m_Initializer = value;
                }
            }
        }
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
        public GSDataTableAttribute (string name)
	    {
            this.m_Name = name.ToLower();
	    }
        /// <summary>
        /// used internally
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sync"></param>
        public GSDataTableAttribute(string name, object sync)
        {
            this.m_Name = name;
        }
        public static string GetTableName(Type t)
        {
            GSDataTableAttribute c = Attribute.GetCustomAttribute(t, typeof(GSDataTableAttribute)) as GSDataTableAttribute;
            if (c != null)
            {
                if (string.IsNullOrEmpty(c.Name))
                {
                    return Path.GetExtension(c.Name);
                }
                return c.Name;              
            }
            return null;
        }
    }
}
