

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: enuCoreScreenResponse.cs
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
file:enuCoreScreenResponse.cs
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
    public enum enuCoreScreenResponse : int
    {
         ChangeSucced       = CoreScreen.DISP_CHANGE_SUCCESSFUL ,
         ChangeRestart = CoreScreen.DISP_CHANGE_RESTART,
         ChangeFailed = CoreScreen.DISP_CHANGE_FAILED ,
         ChangedBadMode = CoreScreen.DISP_CHANGE_BADMODE,
         ChangeNotUpdated = CoreScreen.DISP_CHANGE_NOTUPDATED,
         ChangeBadFlags = CoreScreen.DISP_CHANGE_BADFLAGS ,
         ChangeBadParam = CoreScreen.DISP_CHANGE_BADPARAM 
    }
}

