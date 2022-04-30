

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreBrushRegister.cs
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
file:ICoreBrushRegister.cs
*/
using IGK.ICore;using IGK.ICore.Drawing2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent a drsstudio brush register
    /// </summary>
    public interface ICoreBrushRegister : IDisposable 
    {
        void Unregister(ICoreBrush coreBrush);
        void Register(ICoreBrush coreBrush);
        T GetPen<T>(Colorf color) where T : class , IDisposable;
        T GetBrush<T>(Colorf color) where T : class , IDisposable  ;
        T GetBrush<T>(enuHatchStyle style, Colorf cl1, Colorf cl2) where T : class , IDisposable ;  
        T GetPen<T>(ICorePen corePen) where T: class, IDisposable ;
        T GetBrush<T>(ICoreBrush brush) where T:class , IDisposable ;
        T GetBrush<T>(ICoreBitmap bitmap) where T : class , IDisposable;
        T GetBrush<T>(ICore2DDrawingDocument document) where T : class , IDisposable;
        void Reload(ICoreBrush coreBrush);
    }
}

