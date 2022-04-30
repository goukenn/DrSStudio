using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore
{
    public interface ICoreSystemAssemblyResolver
    {
        /// <summary>
        /// get a core system assembly resolver
        /// </summary>
        CoreSystemAssemblyResolver AssemblyResolver { get; }
    }
}
