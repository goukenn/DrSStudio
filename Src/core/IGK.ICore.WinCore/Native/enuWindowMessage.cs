

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuWindowMessage.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Windows.Native;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:enuWindowMessage.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
Application : DrSStudio
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.DrSStudio.WinUI
{
    internal enum enuWindowMessage
    {        
        WM_NULL = User32.WM_NULL ,
        WM_CREATE = User32.WM_CREATE ,
        WM_DESTROY =User32.WM_DESTROY,
        WM_MOVE = User32.WM_MOVE ,
        WM_SIZE =User32.WM_SIZE  ,
        WM_ACTIVATE = User32.WM_ACTIVATE ,
        WM_ACTIVATEAPP = User32.WM_ACTIVATEAPP ,
        WM_NCCREATE = User32.WM_NCCREATE ,
        WM_NCDESTROY = User32.WM_NCDESTROY ,
        WM_NCCALCSIZE = User32.WM_NCCALCSIZE ,
        WM_NCHITTEST = User32.WM_NCHITTEST ,
        WM_NCPAINT = User32.WM_NCPAINT ,
        WM_NCACTIVATE = User32.WM_NCACTIVATE ,
        WM_KILLFOCUS = User32.WM_KILLFOCUS ,
        WM_GETMINMAXINFO = User32.WM_GETMINMAXINFO ,
        WM_GETTEXT = User32.WM_GETTEXT ,
        WM_GETTEXTLENGTH = User32.WM_GETTEXTLENGTH ,
        WM_MOUSEMOVE = User32.WM_MOUSEMOVE ,
        WM_LBUTTONUP = User32.WM_LBUTTONUP ,
        WM_RBUTTONUP = User32.WM_RBUTTONUP ,
        WM_ERASEBKGND = User32.WM_ERASEBKGND ,
        WM_ENTERSIZEMOVE = User32.WM_ENTERSIZEMOVE ,
        WM_EXITSIZEMOVE = User32.WM_EXITSIZEMOVE ,
        WM_PAINT = User32.WM_PAINT ,
        WM_SIZING = User32.WM_SIZING ,
        WM_MOVING = User32.WM_MOVING ,
        WM_SYSCOMMAND = User32.WM_SYSCOMMAND ,
        
    }
}

