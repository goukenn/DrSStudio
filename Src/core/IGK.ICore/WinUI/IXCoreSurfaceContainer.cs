

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IXCoreSurfaceContainer.cs
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
file:IXCoreSurfaceContainer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a surface container
    /// </summary>
    public interface IXCoreSurfaceContainer : ICoreControl 
    {
        ICoreWorkingSurface CurrentSurface { get; set; }
        ICoreWorkingSurface this[int index] { get; }
        event EventHandler<CoreWorkingElementChangedEventArgs<ICoreWorkingSurface>> CurrentSurfaceChanged;
        bool IsRegister(ICoreWorkingSurface surface);
        void RegisterSurface(ICoreWorkingSurface surface);
        void UnregisterSurface(ICoreWorkingSurface surface);
        void MoveToNextTab();
        void MoveToPreviousTab();
    }
}

