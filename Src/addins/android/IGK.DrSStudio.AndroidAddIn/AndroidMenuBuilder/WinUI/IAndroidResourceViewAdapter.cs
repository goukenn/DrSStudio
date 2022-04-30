
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IGK.DrSStudio.Android.AndroidMenuBuilder.WinUI;

namespace IGK.DrSStudio.Android.WinUI
{
    public interface IAndroidResourceViewAdapter
    {
        int Count { get; }
        object GetObject(int position);
        int IndexOf(object n);
        void SetNotifyChangedListener(IAndroidResourceViewAdapterListener listerner);
        ICore2DDrawingLayeredElement GetView(ICoreWorkingApplicationContextSurface context, ICore2DDrawingLayeredElement createdview, int position);
        /// <summary>
        /// clear all resources
        /// </summary>
        void Clear();

        
    }
}
