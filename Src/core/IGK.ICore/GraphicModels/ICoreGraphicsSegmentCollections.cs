

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreGraphicsSegmentCollections.cs
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
file:ICoreGraphicsSegmentCollections.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.ICore.GraphicModels
{
    /// <summary>
    /// represent a segment collections
    /// </summary>
    public interface ICoreGraphicsSegmentCollections : IEnumerable 
    {
        ICoreGraphicsSegment this[int index] { get; }
        /// <summary>
        /// clear segment collection
        /// </summary>
        void Clear();        
        /// <summary>
        /// get the owner of this graphics segmemnt collection
        /// </summary>
        ICoreGraphicsPath CoreGraphicsPath { get; }
        /// <summary>
        /// get the number of segment in this collections
        /// </summary>
        int Count { get; }
        /// <summary>
        /// add a new segement
        /// </summary>
        /// <param name="segment"></param>
        void Add(ICoreGraphicsSegment segment);
        /// <summary>
        /// remove segement
        /// </summary>
        /// <param name="segment"></param>
        void Remove(ICoreGraphicsSegment segment);
    }
}

