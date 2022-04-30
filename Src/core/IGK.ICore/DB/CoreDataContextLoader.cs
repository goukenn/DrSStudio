using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// used to initialize data base when loading file
    /// </summary>
    class CoreDataContextLoader
    {
        private CoreDataAdapterBase adapter;

        public CoreDataContextLoader(CoreDataAdapterBase adapter)
        {            
            this.adapter = adapter;
        }

        internal void LoadAssembly(object sender, AssemblyLoadEventArgs args)
        {
            if (args.LoadedAssembly.IsDynamic && (args.LoadedAssembly.GetName().Name == CoreDataConstant.ASM_DYNAMIC_NAME))
            {
                return ;
            }
            CoreDataContext.InitTable(adapter, args.LoadedAssembly );
        }
    }
}
