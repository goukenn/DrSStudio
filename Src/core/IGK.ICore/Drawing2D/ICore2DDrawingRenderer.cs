

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingRenderer.cs
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
file:ICore2DDrawingRenderer.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore.Drawing2D
{
    /// <summary>
    /// represent a drawing 2d object renderer
    /// </summary>
    public interface  ICore2DDrawingRenderer : ICoreWorkingRenderer, IDisposable 
    {
        void Render(ICore2DDrawingObject obj);
        Rectanglef GetBound();
        /// <summary>
        /// get the blobal surrounding bound
        /// </summary>
        /// <returns></returns>
        Rectanglef GetGlobalBound();
        /// <summary>
        /// get the selection bounds
        /// </summary>
        /// <returns></returns>
        Rectanglef GetSelectionBound();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="visible"></param>
        void Invalidate(bool visible);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        bool Contains(Vector2f position);
    }
}

