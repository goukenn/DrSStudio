

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingToolManagerSurface.cs
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
file:ICoreWorkingToolManagerSurface.cs
*/
/* 
-------------------------------------------------------------------
Company: IGK-DEV
Author : C.A.D. BONDJE DOUE
SITE : http://www.igkdev.be
CoreApplicationManager.Instance : ICore
powered by IGK - DEV &copy; 2008-2012
THIS FILE IS A PART OF THE DRSSTUDIO APPLICATION. SEE "License.txt"
FOR MORE INFORMATION ABOUT THE LICENSE
------------------------------------------------------------------- 
*/
﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.WinUI
{
    public interface ICoreWorkingToolManagerSurface :
        ICoreWorkingSurface 
    {

        /// <summary>
        /// get if this type tool is valid 
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        bool IsToolValid(Type t);
        /// <summary>
        /// get the default tool type
        /// </summary>
        Type DefaultTool { get;  }
        /// <summary>
        /// get or set the default tool
        /// </summary>
        Type CurrentTool { get; set; }
        /// <summary>
        /// get the associate working tool mecanism
        /// </summary>
        ICoreWorkingMecanism Mecanism { get; }
        /// <summary>
        /// get raised when current tool changed
        /// </summary>
        event EventHandler CurrentToolChanged;
    }
}

