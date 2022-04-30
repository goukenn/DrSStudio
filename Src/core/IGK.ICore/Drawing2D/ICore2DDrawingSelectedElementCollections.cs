

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingSelectedElementCollections.cs
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
file:ICore2DDrawingSelectedElementCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;
    public interface ICore2DDrawingSelectedElementCollections :        
        System.Collections.IEnumerable 
    {
        /// <summary>
        /// add element
        /// </summary>
        /// <param name="element"></param>
        void Add(ICore2DDrawingLayeredElement element);
        /// <summary>
        /// remove element
        /// </summary>
        /// <param name="element"></param>
        void Remove(ICore2DDrawingLayeredElement element);
        /// <summary>
        /// get element by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        ICore2DDrawingLayeredElement this[int index] { get; }
        /// <summary>
        /// get element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICore2DDrawingLayeredElement this[string id] { get; }
        /// <summary>
        /// get the number of element in the collection
        /// </summary>        
        int Count { get; }
        /// <summary>
        /// check if the element is contained in this collection
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Contains(ICore2DDrawingLayeredElement element);
        ICore2DDrawingLayeredElement[] ToArray();
        void AddRange(ICore2DDrawingLayeredElement[] items);
    }
}

