

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreJobProgressEventHandler.cs
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
file:CoreJobProgressEventHandler.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// job progress event
    /// </summary>
    /// <param name="obj">object that initiate the Job Progress</param>
    /// <param name="int">job progress value. from 0 to 100</param>
    public delegate void CoreJobProgressEventHandler(object obj, int value);
}

