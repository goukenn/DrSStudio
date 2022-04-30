

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreContextMenu.cs
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
file:ICoreContextMenu.cs
*/
using IGK.ICore;using IGK.ICore.ContextMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a context menu
    /// </summary>
    public interface ICoreContextMenu  
    {
        int IndexOf(IXCoreContextMenuItemContainer item);
        void Insert(int i, IXCoreMenuItemSeparator item);

        void Add(IXCoreMenuItemSeparator separator);
        void Add(CoreContextMenuBase menu);
        void Remove(CoreContextMenuBase menu);

        ICoreContextMenu ContextMenuStrip { get; }
        ICoreControl SourceControl { get; }
        Vector2f MouseOpeningLocation { get; }
        ICoreMenuItemCollections Items { get; }
        event EventHandler CheckForVisibility; //raise to check for visibility before showing the menu
        event EventHandler<CoreCancelEventArgs> Opening;
        event EventHandler Opened;
        event EventHandler<CoreClosingEventArgs> Closing;
        event EventHandler<CoreContextClosedEventArgs> Closed;
        event EventHandler<CoreItemEventArgs<CoreContextMenuBase>> ItemAdded;
        event EventHandler<CoreItemEventArgs<CoreContextMenuBase>> ItemRemoved;

    }
}

