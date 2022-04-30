

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingSurface.cs
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
file:ICoreWorkingSurface.cs
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
using System.IO;
namespace IGK.ICore.WinUI
{
    using IGK.ICore;using IGK.ICore.Codec;
    /// <summary>
    /// represent the default drs studio surface
    /// </summary>
    public interface ICoreWorkingSurface : 
        ICoreControl ,
        ICoreIdentifier,
        ICoreWorkingObject
    {

        /// <summary>
        /// get the project element
        /// </summary>
        /// <returns></returns>
        ProjectElement GetProjectElement();
        /// <summary>
        /// get the surface that is the parent of this working surface
        /// </summary>
        ICoreWorkingSurface ParentSurface { get; set; }
        /// <summary>
        /// get the current surface
        /// </summary>
        ICoreWorkingSurface CurrentChild { get; }
        /// <summary>
        /// get the target environment
        /// </summary>
        string SurfaceEnvironment { get; }
        /// <summary>
        /// get the display name of the working surface
        /// </summary>
        string Title { get; }
        /// <summary>
        /// to initialize the surface
        /// </summary>
        /// <param name="p"></param>
        void SetParam(ICoreInitializatorParam p);
        /// <summary>
        /// display name changed
        /// </summary>
        event EventHandler TitleChanged;
        /// <summary>
        /// get if this surface can process the message. used in CoreActionRegisterCollection to call Action
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        bool CanProcess(ICoreMessage msg);
    }
}

