

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreResources.cs
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
file:ICoreResources.cs
*/

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace IGK.ICore
{
    using IGK.ICore;using IGK.ICore.Drawing2D;    
    using IGK.ICore.IO.Files;
using IGK.ICore.WinUI;
    /// <summary>
    /// represent a core resource manager interface
    /// </summary>
    public interface ICoreResources
    {
        string GetString(string key);
        string GetString(string key, params object[] param);
        T GetCursor<T>(string key) where T : class;
        T GetDocument<T>(string key, int index);        
        T[] GetAllDocument<T>(string p);
        byte[] GetDefinition(string key);
        void ReloadString();
    }
}

