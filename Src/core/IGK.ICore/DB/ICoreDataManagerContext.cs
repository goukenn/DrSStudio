using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.DB
{
    /// <summary>
    /// represent a TO DbContext
    /// </summary>
    public interface ICoreDataManagerContext
    {
        string CreateTableQuery(string name, ICoreDataColumnInfo[] togoDBColumnInfo, string description);
    }
}
