

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: CoreProject.cs
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
file:CoreProject.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// represent the base Project class 
    /// </summary>
    public abstract class CoreProject
    {
        /// <summary>
        /// init project
        /// </summary>
        /// <param name="bench"></param>
        public abstract void InitProject(ICoreWorkbench bench);
        /// <summary>
        /// unload project
        /// </summary>
        /// <param name="bench"></param>
        public abstract void UnloadProject(ICoreWorkbench bench);
    }
}

