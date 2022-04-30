using System;
using System.Collections.Generic;

namespace IGK.ICore.DB
{
    /// <summary>
    /// reprent the data context
    /// </summary>
    public interface ICoreDataContext
    {
        ICoreDataQueryResult  SelectAll(Type propertyType, Dictionary<string, object> dictionary);
    }
}