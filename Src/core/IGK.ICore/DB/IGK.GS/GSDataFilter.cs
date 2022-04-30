using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface, Inherited = true, AllowMultiple = false)]
    public class GSDataFilterAttribute : Attribute
    {
        bool m_bool;
        private string m_PropertyName;

        public string PropertyName
        {
            get { return m_PropertyName; }
            set
            {
                if (m_PropertyName != value)
                {
                    m_PropertyName = value;
                }
            }
        }

        public bool IsDataFilter
        {
            get {
                return this.m_bool;
            }
        }
        public GSDataFilterAttribute()
        {
            this.m_bool = true;
        }
        public GSDataFilterAttribute(bool filter)
        {
            this.m_bool = filter;
        }
        public GSDataFilterAttribute(string propertyName):this()
        {
            this.m_PropertyName = propertyName;
        }

        public string[] FilterProperty()
        {
            if (string.IsNullOrEmpty(this.m_PropertyName))
                return null;
            return this.m_PropertyName.Split(';');
        }

        public bool IsFiltered(string p)
        {
            return this.FilterProperty().Contains<string>(p);
        }
    }
}
