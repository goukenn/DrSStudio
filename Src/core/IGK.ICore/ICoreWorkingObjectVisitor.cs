

/*
IGKDEV @ 2008-2016
Project : IGK 
author: C.A.D . BONDJE DOUE
site: http://www.igkdev.be
file: ICoreWorkingObjectVisitor.cs
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

namespace IGK.ICore.Drawing2D
{

    /// <summary>
    /// represent a object visitor
    /// </summary>
    public interface  ICoreWorkingObjectVisitor
    {
        /// <summary>
        /// object to visit
        /// </summary>
        /// <param name="obj"></param>
        void Visit(ICoreWorkingObject obj);
        /// <summary>
        /// visit this object as 
        /// </summary>
        /// <param name="dElement"> element to visit</param>
        /// <param name="type">request type to visist</param>
        void Visit(ICoreWorkingObject dElement, Type type);
    }
}