using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    using IGK.ICore;

    public abstract class CoreDataTableInitializerBase : ICoreDataTableInitializer 
    {
        private Type m_initType;
        private CoreDataAdapterBase m_adapter;

        public Type InitType { get { return this.m_initType; } }
        public CoreDataAdapterBase Adapter { get { return this.m_adapter; } }

        public void Initialize(Type type, CoreDataAdapterBase adapter)
        {
            this.m_initType = type;
            this.m_adapter = adapter;
            var t = CoreDBManager.CreateInterfaceInstance(type, null);
            MethodInfo.GetCurrentMethod().Visit(this, new object[] { t });
        }
    }
}
