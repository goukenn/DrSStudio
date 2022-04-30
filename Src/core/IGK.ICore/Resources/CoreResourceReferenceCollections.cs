using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Resources
{
    /// <summary>
    /// reprenset resource collection
    /// </summary>
    public class CoreResourceReferenceCollections : ICoreResourceReferenceCollections
    {
        private ICoreWorkingObject m_Owner;
        private List<ICoreWorkingObject> m_references;

        public ICoreWorkingObject Owner
        {
            get { return m_Owner; }           
        }

        public CoreResourceReferenceCollections(ICoreWorkingObject owner)
        {
            this.m_Owner = owner;
            this.m_references = new List<ICoreWorkingObject>();
        }
        public int Count
        {
            get { return this.m_references.Count; }
        }
        public bool IsReference(ICoreWorkingObject obj)
        {
            if (obj == null)
                return false;
            return this.m_references.Contains(obj);
        }
        internal void Add(ICoreWorkingObject target)
        {
            if ((target == null) || (this.m_references.Contains(target)))
                return;
            this.m_references.Add(target);
        }
        internal void Remove(ICoreWorkingObject target)
        {
            if (target == null) return;
            if (this.m_references.Contains(target))
                this.m_references.Remove(target);
        }
        public override string ToString()
        {
            return string.Format("References [#{0}]", this.m_references.Count);
        }

    }
}
