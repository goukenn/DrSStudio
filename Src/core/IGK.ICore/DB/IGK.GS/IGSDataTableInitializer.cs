using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// used to initialize a data table.
    /// 
    /// </summary>
    /// <remark>
    /// in case of init you must not call GSDB to get element instance
    /// </remark>
    public interface  IGSDataTableInitializer
    {
        void Initialize(Type type, GSDataAdapter adapter);
    }
}
