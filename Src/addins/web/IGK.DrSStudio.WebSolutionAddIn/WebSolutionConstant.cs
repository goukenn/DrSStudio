

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: WebSolutionConstant.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;
using IGK.ICore.Codec;
using IGK.ICore.Menu;
using IGK.ICore.WinUI;
using IGK.ICore.Drawing2D;
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.DrSStudio.Web
{
    public static class WebSolutionConstant
    {
        public const string EXT = "igkwebsln";
        public const string WEB_SOLUTION_DEFAULT_NAME = "WebProject";
        public const string WEB_IMG_KEY_DEFAULTITEM = "FILE";
        public const string WEB_IMG_KEY_FOLDER = "FOLDER";        
        public const string WEB_GROUP_SETTING = "WebSolution";
        public const string INITURI =   "Lib/igk/igk_init.php?redirect=1";
        public const string GETLANGKEYS = "?c=lang&f=getlangkeys&format=xml";

        //----------------------------------------------------------------------
        //to add a new controller
        //----------------------------------------------------------------------
        public const string ADDCTRL_URI = "?c=igkctrlmanager&f=add_ctrl";
    }
}
