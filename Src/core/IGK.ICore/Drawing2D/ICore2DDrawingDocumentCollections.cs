

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingDocumentCollections.cs
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
file:ICore2DDrawingDocumentCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    public interface ICore2DDrawingDocumentCollections :
        System.Collections.IEnumerable ,
        ICoreWorkingDocumentCollections
    {
        new ICore2DDrawingDocument this[int index] { get; }
        void Add(ICore2DDrawingDocument document);
        void Remove(ICore2DDrawingDocument document);
        void MoveToFront(ICore2DDrawingDocument document);
        void MoveToBack(ICore2DDrawingDocument document);
        void MoveToBegin(ICore2DDrawingDocument document);
        void MoveToEnd(ICore2DDrawingDocument document);        
        int IndexOf(ICore2DDrawingDocument document);
        void InsertAt(int p, ICore2DDrawingDocument Document);
        new ICore2DDrawingDocument[] ToArray();
    }
}

