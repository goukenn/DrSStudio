

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingElementCollections.cs
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
file:ICore2DDrawingElementCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICore2DDrawingElementCollections : 
        System.Collections.IEnumerable ,
        ICoreWorkingElementCollections
    {
        /// <summary>
        /// add element
        /// </summary>
        /// <param name="element"></param>
        void Add(ICore2DDrawingElement element);
        /// <summary>
        /// remove element
        /// </summary>
        /// <param name="element"></param>
        void Remove(ICore2DDrawingElement element);
        /// <summary>
        /// get element by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        new ICore2DDrawingElement this[int index] { get; }
        /// <summary>
        /// get element by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICore2DDrawingElement this[string id] { get; }
   
        /// <summary>
        /// check if the element is contained in this collection
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        bool Contains(ICore2DDrawingElement element);
        /// <summary>
        /// move element to front. one step
        /// </summary>
        /// <param name="element"></param>
        void MoveToFront(ICore2DDrawingElement element);
        /// <summary>
        /// move element to back. one step
        /// </summary>
        /// <param name="element"></param>
        void MoveToBack(ICore2DDrawingElement element);
        /// <summary>
        /// move element to begin the the begin of the list
        /// </summary>
        /// <param name="element"></param>
        void MoveToStart(ICore2DDrawingElement element);
        /// <summary>
        /// move element to the of of the list
        /// </summary>
        /// <param name="element"></param>
        void MoveToEnd(ICore2DDrawingElement element);
    }
}

