

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: VistaIconProject.cs
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
using IGK.ICore.Drawing2D.Menu;
using IGK.ICore.Drawing2D.WinUI;
using IGK.DrSStudio;
using IGK.DrSStudio.Menu;
using IGK.DrSStudio.WinUI;
using IGK.DrSStudio.Drawing2D;
using IGK.DrSStudio.Drawing2D.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:VistaIconProject.cs
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
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace  IGK.DrSStudio.XIcon
{
    class VistaIconProject : XIconProject
    {
        public VistaIconProject()
        {
            Icons = XIcon.CreateIcon(16, 16, enuIconColor.bpp4 );    
            Icons.AddIcon(16, 16, enuIconColor.bpp8);
            Icons.AddIcon(16, 16, enuIconColor.trueColorRGBA );
            Icons.AddIcon(32, 32, enuIconColor.bpp4);
            Icons.AddIcon(32, 32, enuIconColor.bpp8);
            Icons.AddIcon(32, 32, enuIconColor.trueColorRGBA);
            Icons.AddIcon(48, 48, enuIconColor.bpp4);
            Icons.AddIcon(48, 48, enuIconColor.bpp8);
            Icons.AddIcon(48, 48, enuIconColor.trueColorRGBA);
            Icons.AddIcon(256, 256, enuIconColor.trueColorRGBA);           
        }
        internal void Save(string filename)
        {
            Icons.Save(filename);
        }
    }
}

