using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.GS
{
    /// <summary>
    /// loading event args data adapter
    /// </summary>
    public class IGSLoadingEventArgs : EventArgs 
    {
        private GSDataAdapter adapter;
        /// <summary>
        /// get the data adapter
        /// </summary>
        public GSDataAdapter DataAdapter {
            get {
                return adapter;
            }
        }
        public IGSLoadingEventArgs(GSDataAdapter adapter)
        {
            
            this.adapter = adapter;
        }
    }
}
