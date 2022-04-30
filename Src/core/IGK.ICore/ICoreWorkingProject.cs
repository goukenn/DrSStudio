

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingProject.cs
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
file:ICoreWorkingProject.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// get the project.
    /// </summary>
    public interface ICoreWorkingProject : ICoreWorkingObject ,  ICoreIdentifier ,ICoreWorkingProjectItem
    {
        ICoreWorkingProjectItemCollections Items{get;}
        /// <summary>
        /// Get or set the file name where the project is being saved
        /// </summary>
        string FileName { get; set; }
        /// <summary>
        /// raise when the file name changed
        /// </summary>
        event EventHandler FileNameChanged;       
    }
}

