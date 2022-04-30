using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace IGK.ICore.DB
{    
    /// <summary>
    /// dummy internal data row
    /// </summary>
        public sealed class CoreDummyDataRow : CoreDataRowBase, ICoreDataRow
        {
            
            private Type m_sourceType;

            public override Type SourceTableInterface
            {
                get
                {
                    return m_sourceType;
                }
            }
            /// <summary>
            /// .ctr
            /// </summary>
            public CoreDummyDataRow():base()
            {            
            }
            public static ICoreDataRow Create(Type t)
            {
                CoreDummyDataRow r = new CoreDummyDataRow();
                foreach (PropertyInfo prInfo in CoreDataContext.GetPropertiesList(t))
                {
                    if (!r.m_entries.ContainsKey(prInfo.Name))
                        r.m_entries.Add(prInfo.Name, null);
                }
                r.m_sourceType = t;
                return r;
            }
          
            internal static ICoreDataRow Create(string[] t)
            {
                CoreDummyDataRow r = new CoreDummyDataRow();
                foreach (string prInfo in t)
                {
                    if (!r.m_entries.ContainsKey(prInfo))
                        r.m_entries.Add(prInfo, null);
                }
                return r;
            }
            
           
          
        }    
}
