using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Android.WinUI
{
    public enum enuChangedType
    {
        DataAdded,
        DataRemoved,
        DataClear,
        /// <summary>
        /// data load 
        /// </summary>
        DataLoaded,
        /// <summary>
        /// value changed
        /// </summary>
        DataValueChanged

    }
}
