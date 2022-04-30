using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{

    /// <summary>
    /// reprensent attribute to mark for data table
    /// </summary>
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Interface )]
    public class CoreDataTableAttribute : Attribute 
    {
        public string Name { get;set;}
        /// <summary>
        /// get or set the identifier default key
        /// </summary>
        public string IdentifierKey
        {
            get;
            set;
        }
        /// <summary>
        /// get or set the type that will be used to initialize the table
        /// </summary>
        public Type Initializer
        {
            get;set;
        }
        public string Description
        {
            get;set;
        }
        public CoreDataTableAttribute(string name)
        {
            this.Name = name ;
        }
        public static string GetTableName(Type t)
        {
            CoreDataTableAttribute c = Attribute.GetCustomAttribute(t, typeof(CoreDataTableAttribute)) as
                CoreDataTableAttribute;
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
