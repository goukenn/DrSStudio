

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreCaret.cs
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
file:ICoreCaret.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent all caret in drsstudio
    /// </summary>
    public interface ICoreCaret : IDisposable 
    {
        Vector2f Location { get; }
        void Hide();
        void Show();
        void SetPosition(float x, float y);
        void Activate(bool activate);
        void SetSize(int width, int height);
    }
}

