using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGK.ICore.DB
{
    /// <summary>
    /// define a query result where T is defined
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface  ICoreDataQueryResult<T>  : ICoreDataQueryResult where  T : ICoreDataTable
    {
        new T GetRowAt(int index);    
    }
}
