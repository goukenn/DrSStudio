

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreMainMenuItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:ICoreMainMenuItem.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    public interface  ICoreMainMenuItem : IDisposable 
    {
        bool Available { get; set; }
        bool Enabled { get; set; }
        bool Visible { get; set; }
        bool ShowShortcutKeys { get; set; }
        string ShortcutKeyDisplayString { get; set; }
        string Text { get; set; }
        /// <summary>
        /// get or set the owner
        /// </summary>
        IXCoreMenuItem Owner { get; set; }
        event EventHandler VisibleChanged;
        event EventHandler EnabledChanged;
        event EventHandler OwnerChanged;
        event EventHandler Click;
        event EventHandler AvailableChanged;
    }
}

