using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;

namespace IGK.DrSStudio.Android.WinUI
{
    public interface  IAndroidResourceViewAdapterListener
    {
        void OnDataChanged(enuChangedType changeType, int position);
    }
}
