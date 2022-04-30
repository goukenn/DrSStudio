

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingPositionableObjectContainer_1.cs
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
file:ICoreWorkingPositionableObjectContainer_1.cs
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace IGK.ICore
{
    /// <summary>
    /// represent a positionable object container
    /// </summary>
    public interface ICoreWorkingPositionableObjectContainer<T> : IEnumerable
    {
        void Remove(T item);
        int IndexOf(T item);
        void MoveToBack(T item);
        void MoveToFront(T item);
        void MoveToBegin(T item);
        void MoveToEnd(T item);
        void MoveAt(T item, int index);
    }
}

