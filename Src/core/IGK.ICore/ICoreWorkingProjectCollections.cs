

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingProjectCollections.cs
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
file:ICoreWorkingProjectCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
namespace IGK.ICore
{
    /// <summary>
    /// Represent opened drsstudio opened project.
    /// </summary>
    public interface ICoreWorkingProjectCollections : IEnumerable 
    {
        int Count { get; }
        /// <summary>
        /// represent the project
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICoreWorkingProject this[int index] { get; }
        void AddProject(ICoreWorkingProject project);
        /// <summary>
        /// diseable project
        /// </summary>
        /// <param name="project"></param>
        void UnLoadProject(ICoreWorkingProject project);
        /// <summary>
        /// remove project
        /// </summary>
        /// <param name="project"></param>
        void RemoveProject(ICoreWorkingProject project);
    }
}

