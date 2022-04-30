

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingRecordableSurface.cs
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
file:ICoreWorkingRecordableSurface.cs
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
    public interface ICoreWorkingRecordableSurface :
        ICoreWorkingSaveSurface,
        ICoreWorkingSurface 
    {
        /// <summary>
        /// get is saving
        /// </summary>
        bool Saving { get; }
        /// <summary>
        /// get or set if this surface need to be saved
        /// </summary>
        bool NeedToSave { get; set; }
        /// <summary>
        /// event raised when surface NeedToSaveChanged
        /// </summary>
        event EventHandler NeedToSaveChanged;
        /// <summary>
        /// event raised when this surface saved
        /// </summary>
        event EventHandler Saved;
    }
}

