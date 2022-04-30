

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreMenu_DocumentView.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:CoreMenu_DocumentView.cs
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
/* 
-------------------------------------------------------------
This file is part of iGK-DEV-DrawingStudio
-------------------------------------------------------------
-------------------------------------------------------------
-------------------------------------------------------------
view license file in Documentation folder to get more info
Copyright (c) 2008-2010 
Author  : Charles A.D. BONDJE DOUE 
mail : bondje.doue@hotmail.com
-------------------------------------------------------------
-------------------------------------------------------------
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace IGK.DrSStudio.Drawing2D.Menu
{
    using IGK.ICore;using IGK.DrSStudio.Menu;
    using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.DrSStudio.Drawing2D.Tools;
    [CoreMenu("View.Documents",
      31,
      ImageKey = "Menu_Document",
      Shortcut = Keys.Shift | Keys.D,
      ShortcutText = "Shift+D",
      UseShortcut=false)]
    sealed class CoreMenu_DocumentView : Core2DMenuViewToolBase 
    {
        public CoreMenu_DocumentView():base( CoreTool_DocumentView.Instance)
        {
        }
    }
}
