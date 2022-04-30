

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreSnippetCollections.cs
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
file:ICoreSnippetCollections.cs
*/
using IGK.ICore;using IGK.ICore.WinUI;

﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
namespace IGK.ICore
{
    public interface ICoreSnippetCollections : IDisposable , IEnumerable
    {
        void Enable();
        void Disabled();
        int Count { get; }
        bool Contains(int index);
        void Add(ICoreSnippet snippet);
        void Add(int index, ICoreSnippet snippet);
        void Remove(int index);
        ICoreSnippet this[int index] { get; }
    }
}

