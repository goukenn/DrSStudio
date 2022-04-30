

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IGKD2DrawingConstant.cs
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
file:IGKD2DConstant.cs
*/
using IGK.ICore.WinCore;
using IGK.ICore;using IGK.ICore.Drawing2D.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Drawing2D
{
    public  static class IGKD2DrawingConstant
    {
        internal const string CONTEXT_MENU = "Drawing2D";
        internal const int CONTEXT_MENU_BASE_INDEX = 0x0;
        internal const string MENU_ZOOMFIT = "View.ZoomMode.ZoomFit";
        internal const string MENU_SPLIT_PATH = "Tools.Drawing2DPath.SplitPath";

        public const string MENU_LAYER = "2DDrawingLayer";
        public const string MENU_GROUP = "Edit.Group";
        public const string MENU_UNGROUP = "Edit.UnGroup";

        public const string CMENU_GROUP = "Drawing2DEdit.Group";
        public const string CMENU_GROUP_ITEM = CMENU_GROUP + ".Group";
        public const string CMENU_UNGROUP_ITEM = CMENU_GROUP + ".UnGroup";
        public const string CMENU_CONVERTO = "Drawing2DEdit.ConvertTo";
    }
}

