

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICore2DDrawingLayeredElementCollections.cs
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
file:ICore2DDrawingLayeredElementCollections.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore.Drawing2D
{
    using IGK.ICore;
    public interface ICore2DDrawingLayeredElementCollections :
        ICore2DDrawingElementCollections ,
        ICoreWorkingElementCollections
    {
        new ICore2DDrawingLayeredElement this[int index] { get; }
        ICore2DDrawingLayeredElement[] ToArray();
        int IndexOf(ICore2DDrawingLayeredElement element);
        void Add(ICore2DDrawingLayeredElement element);
        void Remove(ICore2DDrawingLayeredElement element);
        void AddRange(params ICore2DDrawingLayeredElement[] element);
        void RemoveAll(params ICore2DDrawingLayeredElement[] elements);
        void Clear();
    }
}

