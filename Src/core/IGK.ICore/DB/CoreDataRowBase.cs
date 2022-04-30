using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent a data row base class 
    /// </summary>
    public abstract class CoreDataRowBase : ICoreDataRow
    {
        protected Dictionary<string, object> m_entries;

        public virtual Type SourceTableInterface
        {
            get { return null; }
        }
        public bool Contains(string key)
        {
            return this.m_entries.ContainsKey(key);
        }
        public string GetName(int index)
        {
            return this.m_entries.Keys.ToArray()[index];
        }
        public string Primary
        {
            get;
            set;
        }

        public int FieldCount
        {
            get { return this.m_entries.Keys.Count; }
        }
        public CoreDataRowBase()
        {
            this.m_entries = new Dictionary<string, object>();
        }

        public string this[string name]
        {
            get
            {
                return Convert.ToString(m_entries[name]);
            }
            set
            {
                m_entries[name] = value;
            }
        }

        /// <summary>
        /// get dictionary entries
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, object> ToDictionary()
        {
            return m_entries;
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return m_entries.GetEnumerator();
        }

        public virtual T GetValue<T>(string name, T defaultValue = default(T))
        {
            return DBUtils.Convert<T>(this.m_entries, name);
        }

        public virtual void UpdateValue(ICoreDataTable tItem, CoreDataAdapterBase adapter)
        {
            if (adapter == null)
                return;
            PropertyDescriptorCollection c = TypeDescriptor.GetProperties(tItem);
            string[] f = this.m_entries.Keys.ToArray();
            foreach (var item in f)
            {
                var s = c[item].GetValue(tItem);
                if (item == CoreDataConstant.CL_ID)
                {
                    if (Int32.Parse(s.ToString()) == 0)
                    {
                        this.m_entries[item] = adapter.GetValue(c[item].PropertyType, null);
                        continue;
                    }
                }

                this.m_entries[item] = adapter.GetValue(c[item].PropertyType, s);
            }
        }

        //public void UpdateValue(ICoreDataTable tItem, CoreDataAdapterBase dataAdapter)
        //{
        //}
    }
}
