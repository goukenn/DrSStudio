

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingLayerCollections.cs
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
file:ICore2DDrawingLayerCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;
    public interface ICore2DDrawingLayerCollections :
        ICoreLayerCollections,
        System.Collections.IEnumerable
    {
        void Clear();
        bool Contains(ICore2DDrawingLayer layer);
        /// <summary>
        /// add a new layer
        /// </summary>
        /// <param name="layer"></param>
        void Add(ICore2DDrawingLayer layer);
        /// <summary>
        /// remove the layer
        /// </summary>
        /// <param name="layer"></param>
        void Remove(ICore2DDrawingLayer layer);
        /// <summary>
        /// get thet layer by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        new ICore2DDrawingLayer this[int index] { get; }
        /// <summary>
        /// get the layer by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICore2DDrawingLayer this[string id] { get; }
        /// <summary>
        /// replace all layer with those in the array
        /// </summary>
        /// <param name="iCore2DDrawingLayer"></param>
        void Replace(ICore2DDrawingLayer[] iCore2DDrawingLayer);
        /// <summary>
        /// get the document contains element by id
        /// </summary>
        /// <param name="p"></param>
        bool Contains(string p);
        /// <summary>
        /// get the index of the layer in current collection
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        int IndexOf(ICore2DDrawingLayer layer);
        new ICore2DDrawingLayer[] ToArray();
        void MoveToFront(ICore2DDrawingLayer layer);
        void MoveToBack(ICore2DDrawingLayer layer);
        void MoveToBegin(ICore2DDrawingLayer core2DDrawingLayer);
        void MoveToEnd(ICore2DDrawingLayer core2DDrawingLayer);

        /// <summary>
        /// insert layer at 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="iCore2DDrawingLayer"></param>
        void InsertAt(int zindex, ICore2DDrawingLayer iCore2DDrawingLayer);

    }
}

