

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingDocumentCollections.cs
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
file:ICoreWorkingDocumentCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    /// <summary>
    /// represent the base document collection
    /// </summary>
    public interface ICoreWorkingDocumentCollections : System.Collections.IEnumerable 
    {
        int Count { get; }
        ICoreWorkingDocument  this[int index] { get; }
        void Add(ICoreWorkingDocument document);
        void Remove(ICoreWorkingDocument document);
        void MoveToFront(ICoreWorkingDocument document);
        void MoveToBack(ICoreWorkingDocument document);
        void MoveToBegin(ICoreWorkingDocument document);
        void MoveToEnd(ICoreWorkingDocument document);
        ICoreWorkingDocument[] ToArray();
        int IndexOf(ICoreWorkingDocument document);
    }
}

