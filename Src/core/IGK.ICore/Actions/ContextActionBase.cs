using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.Actions
{
    /// <summary>
    /// represent a context action base
    /// </summary>
    public abstract class ContextActionBase :
        MarshalByRefObject 
    {
        private List<string> m_context;

        public virtual object GetActionContext(string name)
        {
            return CoreSystem.GetActionContext(name);
        }
        public void BindExecutionContext(string name)
        {
            if (m_context == null)
                m_context = new List<string>();
            m_context.Clear();
            if (!string.IsNullOrEmpty(name))
                m_context.AddRange(name.ToLower().Split(','));

        }
        public bool IsInContext(string name)
        {
            if (!string.IsNullOrEmpty(name))
                return m_context.Contains(name.ToLower());
            return false;

        }
    }
}
