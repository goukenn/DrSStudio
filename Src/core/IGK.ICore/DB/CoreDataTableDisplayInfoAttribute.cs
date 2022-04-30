using IGK.ICore.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    /// <summary>
    /// used to display info in combobox
    /// </summary>
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Property  | AttributeTargets.Interface  , Inherited = true , AllowMultiple = false )]
    public class CoreDataTableDisplayInfoAttribute : Attribute 
    {
        private string[] m_DisplayProperty;

        public string[] DisplayProperty
        {
            get { return m_DisplayProperty; }        
        }
        /// <summary>
        /// semi column affected property
        /// </summary>
        /// <param name="displayProp"></param>
        public CoreDataTableDisplayInfoAttribute(string displayProp)
        {
            this.m_DisplayProperty = displayProp.Split(';');
        }
        public virtual string GetProperty(ICoreDataRow r)
        {
            if (r == null)
                return null;
            StringBuilder sb = new StringBuilder();
            bool v_r = false;
            foreach (string s in DisplayProperty)
            {
                if (v_r)
                    sb.Append(" ");
                sb.Append(r[s]);
                v_r = true;
               
            }
            return sb.ToString();
        }
        public static CoreDataTableDisplayInfoAttribute GetAttribute(Type t)
        {
            return Attribute.GetCustomAttribute(t, typeof(CoreDataTableDisplayInfoAttribute)) as CoreDataTableDisplayInfoAttribute;
        }
        public static CoreDataTableDisplayInfoAttribute GetAttribute(PropertyInfo t)
        {
            return Attribute.GetCustomAttribute(t, typeof(CoreDataTableDisplayInfoAttribute)) as CoreDataTableDisplayInfoAttribute;
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
