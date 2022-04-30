

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: _Undo.cs
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
file:_Undo.cs
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
    using IGK.ICore;using IGK.DrSStudio.Drawing2D.Menu;
    using IGK.DrSStudio.Drawing2D;
    using IGK.DrSStudio.Menu;
    [CoreMenu ("Edit.Drawing2DUndo", 40,
        Shortcut= Keys.Control | Keys.Z,
        ImageKey = "Menu_Undo",
        SeparatorBefore=true )]
    class _Undo : Core2DMenuBase 
    {
        protected override bool PerformAction()
        {
            Tools.HistorySurfaceManager.Instance.Undo();
            return true;
        }
    }
}

