using IGK.ICore;using IGK.ICore.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    internal class CoreActionContextCollections : ICoreActionContextCollections
    {
        Dictionary<string, ICoreActionContext> m_dics;
        private CoreSystem m_owner;
        internal CoreActionContextCollections(CoreSystem owner)
        {
            this.m_owner = owner;
            this.m_dics = new Dictionary<string, ICoreActionContext>();
        }

        public ICoreActionContext this[string name]
        {
            get {
                if (string.IsNullOrEmpty (name ))
                    return null;
                name = name.ToLower();
                if (this.m_dics.ContainsKey(name))
                    return this.m_dics[name];
                return null;
            }
        }

        internal bool Register(string name, ICoreActionContext service)
        {
            if (string.IsNullOrEmpty(name))
                return false;
            name = name.ToLower();
            if (this.m_dics.ContainsKey(name))
            {
                if (service == null)
                    this.m_dics.Remove(name);
                this.m_dics[name] = service;
            }
            else {
                if (service == null) {
                    return false;
                }
                this.m_dics.Add(name, service);
            }
            return true;
        }
    }
}
