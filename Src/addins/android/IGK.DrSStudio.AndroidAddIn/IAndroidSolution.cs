

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IAndroidSolution.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/

using IGK.ICore;using IGK.ICore.WinUI;
/*
IGKDEV @ 2008-2016
author: C.A.D . BONDJE DOUE
file:IAndroidProject.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.Android
{
    /// <summary>
    /// represent a android project
    /// </summary>
    public interface IAndroidSolution : ICoreWorkingProjectSolution
    {
        
        /// <summary>
        /// get/set the android project target location
        /// </summary>
        string TargetLocation { get; set; }
        /// <summary>
        /// get/set the android target version
        /// </summary>
        AndroidTargetInfo AndroidTargetVersion { get; set; }
        /// <summary>
        /// get the android resources collection
        /// </summary>
        IAndroidResourceCollections Resources { get; }
    }
}

