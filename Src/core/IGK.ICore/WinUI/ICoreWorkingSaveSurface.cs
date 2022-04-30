

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingSaveSurface.cs
THIS FILE IS A PART OF IGK Library FOR DRSSTUDION APPLICATION.
Read license.text
THIS SOFTWARE IS PROVIDED "AS IS" AND WITHOUT ANY EXPRESS OR
IMPLIED WARRANTIES, INCLUDING, WITHOUT LIMITATION, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR
PURPOSE.
*/
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IGK.ICore.WinUI
{
    /// <summary>
    /// represent a surface that can be saved 
    /// </summary>
    public interface  ICoreWorkingSaveSurface : ICoreWorkingSurface 
    {
        /// <summary>
        /// save the surface content
        /// </summary>
        void Save();
        /// <summary>
        /// get the save as info
        /// </summary>
        /// <returns></returns>
        ICoreSaveAsInfo GetSaveAsInfo();
        /// <summary>
        /// the property to save as filename
        /// </summary>
        /// <param name="filename"></param>
        void SaveAs(string filename);
    }
}
