

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IHistoryChainCollection.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.History;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Drawing2D
{
    /// <summary>
    /// represent a history chain collection
    /// </summary>
    public interface IHistoryChainCollection : IEnumerable 
    {

        IHistoryChainItem SelectedHistory { get; }
        /// <summary>
        /// get the number of history chain item in this collection
        /// </summary>
        int Count { get; }
        /// <summary>
        /// enqueu a history chain
        /// </summary>
        /// <param name="item"></param>
        void Enqueue(IHistoryChainItem item);
        /// <summary>
        /// get or set if this hitory type chain is enabled
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool Enabled(Type type);
    }
}
