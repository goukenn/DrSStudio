

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXCoreContextMenuItem.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore;using IGK.ICore.ContextMenu;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IXCoreContextMenuItem.cs
*/
using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent core context menu item
    /// </summary>
    public interface IXCoreContextMenuItem : IDisposable 
    {
        CoreContextMenuBase ContextMenuParent { get; set; }
        void Add(Object obj);
        void Remove(object obj);
        bool Available { get; set; }
        bool Enabled { get; set; }
        bool ShowShortcutKeys { get; set; }
        string ShortcutKeyDisplayString { get; set; }
        string Text { get; set; }
        ICoreContextMenu Owner { get; }
        ICore2DDrawingDocument MenuDocument { get; set; }
        event EventHandler AvailableChanged;
        event EventHandler EnabledChanged;
        event EventHandler Click;
        event EventHandler OwnerChanged;
        event EventHandler DropDownClosed;
    }
}

