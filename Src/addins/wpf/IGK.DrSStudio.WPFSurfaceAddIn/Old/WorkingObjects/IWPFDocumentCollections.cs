

/*
IGKDEV @ 2008 - 2014
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: IWPFDocumentCollections.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
/*
IGKDEV - [2008 - 2014]
author: C.A.D . BONDJE DOUE
file:IWPFDocumentCollections.cs
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace IGK.DrSStudio.WPFSurfaceAddIn.WorkingObjects
{
    public interface IWPFDocumentCollections : 
        ICoreWorkingDocumentCollections 
    {
        void Add(IWPFDocument document);
        void Remove(IWPFDocument document);
        new IWPFDocument this[int index] { get; }     
        void MoveToFront(IWPFDocument document);
        void MoveToBack(IWPFDocument document);
        void MoveToBegin(IWPFDocument document);
        void MoveToEnd(IWPFDocument document);
        int IndexOf(IWPFDocument document);
    }
}

