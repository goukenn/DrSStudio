

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingLayeredElement.cs
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
file:ICore2DDrawingLayeredElement.cs
*/

﻿using IGK.ICore;using IGK.ICore.GraphicModels;
using IGK.ICore.WinUI;
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICore2DDrawingLayeredElement:
        ICore2DDrawingElement,        
        ICoreWorkingObjectPropertyEvent, 
        ICore2DDrawingGraphicsProperty ,
        ICore2DDrawingTransformableElement,
        ICore2DSmoothObject,
        ICoreWorkingLayoutElement,
        ICoreWorkingBoundResult
    {
        /// <summary>
        /// get or set the visibility of the element
        /// </summary>
        bool View { get; set; }
        void Invalidate(bool t);
        ICoreWorkingDesignerSurface ParentDesigner { get; }
        /// <summary>
        /// get the parent layer
        /// </summary>
        ICore2DDrawingLayer ParentLayer { get; }
        /// <summary>
        /// get the parent document
        /// </summary>
        ICore2DDrawingDocument ParentDocument { get; }
        ICoreGraphicsPath GetPath();
        /// <summary>
        /// get the region of this path
        /// </summary>
        /// <returns></returns>
        ICoreGraphicsRegion GetRegion();
        /// <summary>
        /// get if this element contains the location
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        bool Contains(Vector2f e);
        /// <summary>
        /// get if this element intersect rectangle
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Rectanglef Intersect(Rectanglef e);     
        Rectanglef GetSelectionBound();
    }
}

