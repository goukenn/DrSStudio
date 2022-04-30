using System;

namespace IGK.ICore.DB
{
    /// <summary>
    /// used to initialize data base
    /// </summary>
    public interface ICoreDataTableInitializer
    {
        void Initialize(Type type, CoreDataAdapterBase adapter);
    }
}