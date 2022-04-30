using System;

namespace IGK.ICore.DB
{
    public class ICoreLoadingAdapterEventArgs : EventArgs
    {
        private CoreDataAdapterBase adapter;

        public ICoreLoadingAdapterEventArgs(CoreDataAdapterBase adapter)
        {
            this.adapter = adapter;
        }
    }
}