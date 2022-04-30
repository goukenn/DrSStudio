using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Dependency
{
    /// <summary>
    /// represent a dependency info
    /// </summary>
    public class CoreDependencyInfo
    {
        private string m_Name;
        private enuDependencyType m_Type;
        private CoreDependencyInfo m_Parent;
        private Type m_DeclaringType;
        private List<CoreDependencyInfo> m_childs;

public Type DeclaringType{
get{return m_DeclaringType;}
}
        public CoreDependencyInfo Parent
        {
            get { return m_Parent; }
            internal  set
            {
                if (m_Parent != value)
                {
                    m_Parent = value;
                    value.m_childs.Add(this);
                }
            }
        }
        public enuDependencyType Type
        {
            get { return m_Type; }
        }
        public string Name
        {
            get { return m_Name; }
        }
        private CoreDependencyInfo()
        {
            m_childs = new List<CoreDependencyInfo>();
        }
        internal static CoreDependencyInfo Create(string name, enuDependencyType type, Type declaringType)
        {
            CoreDependencyInfo c = new CoreDependencyInfo();
            c.m_Name = name;
            c.m_Type = type;
            c.m_DeclaringType  = declaringType;            
            return c;
        }
        public override string ToString()
        {
            return this.GetFullName();
        }

        public  string GetFullName()
        {
            if (this.Parent ==null)
                return this.Name ;
            return string.Format("{0}.{1}",  this.Parent.GetFullName(), this.Name);
        }
        /// <summary>
        /// get the child
        /// </summary>
        public IEnumerable<CoreDependencyInfo> Childs { get {
            return this.m_childs;
        } }
    }
}
