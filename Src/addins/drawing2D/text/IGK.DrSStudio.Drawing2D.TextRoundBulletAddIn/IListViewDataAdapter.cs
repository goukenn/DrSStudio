
using IGK.ICore.Drawing2D;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D
{
    public interface IListViewDataAdapter
    {
        /// <summary>
        /// get the number of item in this adapter
        /// </summary>
        int Count { get; }
        /// <summary>
        /// get the layered application context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        ICore2DDrawingLayeredElement GetView(ICoreWorkingApplicationContextSurface context, int position);
    }
}
