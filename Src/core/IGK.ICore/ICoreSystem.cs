

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSystem.cs
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
file:ICoreSystem.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.WinUI;
	using IGK.ICore.Settings ;
    using IGK.ICore.Actions;
    /// <summary>
    /// represent the core system interface 
    /// </summary>
    public interface ICoreSystem 
    {
        

        void RegisterTypeLoader(CoreTypeLoader loaderFunc);
        void UnregisterTypeLoader(CoreTypeLoader loaderFunc);
        void RegisterLoadingComplete(CoreMethodHandler LoadAsmResources);
        void UnRegisterLoadingComplete(CoreMethodHandler LoadAsmResources);
        void RegisterAssemblyLoader(CoreAssemblyLoadedHandler assemblyFunc);
        void UnRegisterAssemblyLoader(CoreAssemblyLoadedHandler assemblyFunc);
    }
}

