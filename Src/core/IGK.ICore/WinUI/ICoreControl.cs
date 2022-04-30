

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreControl.cs
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
file:ICoreControl.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using IGK.ICore;using IGK.ICore.IO.Files;
using IGK.ICore.GraphicModels;
using System.Collections;
namespace IGK.ICore.WinUI
{
    public interface ICoreControl :         
        ICoreWindowKeyEvent ,
        ICoreWindowMouseEvent ,
        ICoreIdentifier ,
        ICoreDisposableObject 
    {
        ICoreContextMenu AppContextMenu { get; set; }
        event EventHandler AppContextMenuChanged;
        ICoreGraphics CreateGraphics();
        ///// <summary>
        ///// get the basics Workbench
        ///// </summary>
        //ICoreWorkbench Workbench { get; }
        // Summary:
        //     Gets the handle to the window represented by the implementer.
        //
        // Returns:
        //     A handle to the window represented by the implementer.
        IntPtr Handle { get; }
        Vector2i PointToScreen(Vector2i pt); 
        ICoreCursor Cursor { get; set; }
        Size2i Size { get; set; }
        Vector2i Location { get; set; }
        Rectanglei Bounds { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        bool Capture { get; set; }
        bool Focused { get; }
        bool Focus();
        bool Visible { get; set; }
        bool InvokeRequired { get; }
        void Refresh();
        void Invalidate();
        void Invalidate(bool inChildControl);
        void Invalidate(Rectanglei rc);
        Vector2i PointToClient(Vector2i pt);
        object  Invoke(Delegate d);
        object BeginInvoke(Delegate d);
        void SuspendLayout();
        void ResumeLayout();
        IList Controls { get; }
        event EventHandler VisibleChanged;
        event EventHandler SizeChanged;
        event EventHandler LocationChanged;
        event EventHandler Disposed;
        event EventHandler GotFocus;
        event EventHandler LostFocus;
        event EventHandler<CorePaintEventArgs> Paint;
        Size2i GetMinimumSize();
        IXForm FindForm();
    }
}

